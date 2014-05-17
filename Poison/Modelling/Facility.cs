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

        private event EventHandler<Facility, Transact> _Released;
        public event EventHandler<Facility, Transact> Released
        {
            add { _Released += value; }
            remove { _Released -= value; }
        }

        private void OnReleased(Transact transact)
        {
            if (_Released != null)
                _Released(this, transact);
        }

        private event EventHandler<Facility, Transact> _Releasing;
        public event EventHandler<Facility, Transact> Releasing
        {
            add { _Releasing += value; }
            remove { _Releasing -= value; }
        }

        private void OnReleasing(Transact transact)
        {
            if (_Releasing != null)
                _Releasing(this, transact);
        }

        private event EventHandler<Facility, Transact> _Seizing;
        public event EventHandler<Facility, Transact> Seizing
        {
            add { _Seizing += value; }
            remove { _Seizing -= value; }
        }

        private void OnSeizing(Transact transact)
        {
            if (_Seizing != null)
                _Seizing(this, transact);
        }

        private event EventHandler<Facility, Transact> _Seized;
        public event EventHandler<Facility, Transact> Seized
        {
            add { _Seized += value; }
            remove { _Seized -= value; }
        }

        private void OnSeized(Transact transact)
        {
            if (_Seized != null)
                _Seized(this, transact);
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
            

            State = FacilityState.Busy;
            Owner = transact;

            OnSeized(transact);
        }

        public Transact Release()
        {
            if (State == FacilityState.Free)
            {
                throw new InvalidOperationException("Facility is not seized.");
            }

            OnReleasing(Owner);

            State = FacilityState.Free;

            Transact transact = Owner;
            Owner = null;

            OnReleased(transact);

            return transact;
        }

        internal void Init()
        {
            OnInit();
        }

        internal void Final()
        {
            OnFinal();
        }
    }
}
