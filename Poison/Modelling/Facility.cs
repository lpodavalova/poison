/*
* The Poison: discrete event simulation system.
* Copyright (C) 2013-2014 Poison team.
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
using Poison.Extensions;

namespace Poison.Modelling
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

        private event EventHandler<Facility> _Init;
        public event EventHandler<Facility> Initialization
        {
            add { _Init += value; }
            remove { _Init -= value; }
        }

        private void OnInit()
        {
            if (_Init != null)
                _Init(this);
        }

        private event EventHandler<Facility> _Final;
        public event EventHandler<Facility> Finalization
        {
            add { _Final += value; }
            remove { _Final -= value; }
        }

        private void OnFinal()
        {
            if (_Final != null)
                _Final(this);
        }

        private event EventHandler<Facility> _Released;
        public event EventHandler<Facility> Released
        {
            add { _Released += value; }
            remove { _Released -= value; }
        }

        private void OnReleased()
        {
            if (_Released != null)
                _Released(this);
        }

        private event EventHandler<Transact> _Releasing;
        public event EventHandler<Transact> Releasing
        {
            add { _Releasing += value; }
            remove { _Releasing -= value; }
        }

        private void OnReleasing(Transact transact)
        {
            if (_Releasing != null)
                _Releasing(transact);
        }

        private event EventHandler<Transact> _Seizing;
        public event EventHandler<Transact> Seizing
        {
            add { _Seizing += value; }
            remove { _Seizing -= value; }
        }

        private void OnSeizing(Transact transact)
        {
            if (_Seizing != null)
                _Seizing(transact);
        }

        private event EventHandler<Facility> _Seized;
        public event EventHandler<Facility> Seized
        {
            add { _Seized += value; }
            remove { _Seized -= value; }
        }

        private void OnSeized()
        {
            if (_Seized != null)
                _Seized(this);
        }

        public void Seize(Transact transact)
        {
            if (transact == null)
            {
                throw new ArgumentNullException("transact");
            }

            if (State == FacilityState.Busy)
            {
                throw new InvalidOperationException("Facility is already seized.");
            }

            OnSeizing(transact);

            //Model.EventQueue.Enqueue(new Event(Model.Time + advanceTime, Release));

            //Entries++;
            //timeStart = Model.Time;
            //LastOwner = transact;

            State = FacilityState.Busy;
            Owner = transact;

            OnSeized();
        }

        public Transact Release()
        {
            if (State == FacilityState.Free)
            {
                throw new InvalidOperationException("Facility is not seized.");
            }

            OnReleasing(Owner);

            State = FacilityState.Free;

            //seizeTime += Model.Time - timeStart;

            Transact transact = Owner;
            Owner = null;

            OnReleased();

            return transact;
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
