/*
* The Poison: discrete event simulation system.
* Copyright (C) 2006-2013 Poison team.
*
* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using Poison.Model.Enums;
using Poison.Extensions;

namespace Poison.Model
{
    public class Facility : IModelEntity
    {
        public Facility(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        public Model Model
        {
            get;
            internal set;
        }

        string IModelEntity.Name
        {
            get
            {
                return Name;
            }
        }

        Model IModelEntity.Model
        {
            get
            {
                return Model;
            }
            set
            {
                Model = value;
            }
        }

        public FacilityState State
        {
            get;
            private set;
        }

        public Transact Owner
        {
            get;
            private set;
        }

        //#region Statistics

        //public int Entries
        //{
        //    get;
        //    internal set;
        //}

        //public double AverageTime
        //{
        //    get
        //    {
        //        return seizeTime.SmartDiv(Entries);
        //    }
        //}

        //public double Utilization
        //{
        //    get
        //    {
        //        return seizeTime / Model.Time;
        //    }
        //}

        //public Transact LastOwner
        //{
        //    get;
        //    private set;
        //}

        //private double seizeTime;
        //private double timeStart;

        //#endregion

        private event InitFinalHandler<Facility> _Init;
        public event InitFinalHandler<Facility> Initialization
        {
            add { _Init += value; }
            remove { _Init -= value; }
        }

        private void OnInit()
        {
            if (_Init != null)
                _Init(this);
        }

        private event InitFinalHandler<Facility> _Final;
        public event InitFinalHandler<Facility> Finalization
        {
            add { _Final += value; }
            remove { _Final -= value; }
        }

        private void OnFinal()
        {
            if (_Final != null)
                _Final(this);
        }

        private event TransactHandler<Facility> _Released;
        public event TransactHandler<Facility> Released
        {
            add { _Released += value; }
            remove { _Released -= value; }
        }

        private void OnReleased(Transact transact)
        {
            if (_Released != null)
                _Released(this, transact);
        }

        private event TransactHandler<Facility> _Releasing;
        public event TransactHandler<Facility> Releasing
        {
            add { _Releasing += value; }
            remove { _Releasing -= value; }
        }

        private void OnReleasing(Transact transact)
        {
            if (_Releasing != null)
                _Releasing(this, transact);
        }

        private event TransactHandler<Facility> _Seizing;
        public event TransactHandler<Facility> Seizing
        {
            add { _Seizing += value; }
            remove { _Seizing -= value; }
        }

        private void OnSeizing(Transact transact)
        {
            if (_Seizing != null)
                _Seizing(this, transact);
        }

        private event TransactHandler<Facility> _Seized;
        public event TransactHandler<Facility> Seized
        {
            add { _Seized += value; }
            remove { _Seized -= value; }
        }

        private void OnSeized(Transact transact)
        {
            if (_Seized != null)
                _Seized(this, transact);
        }

        public void Seize(Transact transact, double advanceTime)
        {
            if (transact == null)
            {
                throw new ArgumentNullException("transact");
            }

            // TODO: exception if already seized

            OnSeizing(transact);

            if (Math.Sign(advanceTime) < 0)
            {
                advanceTime = 0;
            }

            Model.EventQueue.Enqueue(new Event(Model.Time + advanceTime, Release));

            //Entries++;
            //timeStart = Model.Time;
            //LastOwner = transact;

            State = FacilityState.Busy;
            Owner = transact;

            OnSeized(transact);
        }

        private void Release()
        {
            OnReleasing(Owner);

            State = FacilityState.Free;

            //seizeTime += Model.Time - timeStart;

            Transact transact = Owner;
            Owner = null;

            OnReleased(transact);
        }

        internal void Init()
        {
            OnInit();
            //Entries = 0;
            //seizeTime = 0.0;
            //LastOwner = null;
        }

        internal void Final()
        {
            OnFinal();
        //    if (State != FacilityState.Free)
        //    {
        //        seizeTime += Model.Time - timeStart;
        //    }
        }
    }
}
