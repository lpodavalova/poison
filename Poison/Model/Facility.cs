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

        public int Entries
        {
            get;
            internal set;
        }

        #region Statistics

        public double AverageTime
        {
            get
            {
                return seizeTime.SmartDiv(Entries);
            }
        }

        public double Utilization
        {
            get
            {
                return seizeTime / Model.Time;
            }
        }

        public Transact LastOwner
        {
            get;
            private set;
        }

        #endregion

        private double seizeTime;
        private double timeStart;

        public void Seize(Transact transact, TransactHandler transactHandler)
        {
            if (transact == null)
            {
                throw new ArgumentNullException("transact");
            }

            if (transactHandler == null)
            {
                throw new ArgumentNullException("transactHandler");
            }

            while (Model.IsAlive() && State != FacilityState.Free)
            {
                Model.ProcessEvent();
            }

            if (!Model.IsAlive())
            {
                return;
            }

            Entries++;
            timeStart = Model.Time;
            LastOwner = transact;

            State = FacilityState.Busy;
            transactHandler(Model, transact);            
        }

        public void Release(Transact transact)
        {
            if (transact == null)
            {
                throw new ArgumentNullException("transact");
            }

            if (State != FacilityState.Free)
            {
                State = FacilityState.Free;

                seizeTime += Model.Time - timeStart;
            }
        }

        internal void Init()
        {
            Entries = 0;
            seizeTime = 0.0;
            LastOwner = null;
        }

        internal void Final()
        {
            if (State != FacilityState.Free)
            {
                seizeTime += Model.Time - timeStart;
            }
        }
    }
}
