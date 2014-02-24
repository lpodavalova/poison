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
using Poison.Stochastic;

namespace Poison.Model
{
    public class Generator : IModelEntity
    {
        public Generator(string name, IDistribution distribution)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (distribution == null)
            {
                throw new ArgumentNullException("distribution");
            }

            Distribution = distribution;
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

        public IDistribution Distribution
        {
            get;
            private set;
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

        private event TransactHandler _Entered;
        public event TransactHandler Entered 
        {
            add { _Entered += value; }
            remove { _Entered -= value; }
        }

        private void OnEntered(Transact transact)
        {
            if (_Entered != null)
                _Entered(Model, transact);
        }

        private void EnterTransact()
        {
            Transact transact = new Transact(Model, this);

            GenerateEvent();

            OnEntered(transact);
        }

        internal void GenerateEvent()
        {
            double time = Distribution.Next();

            if (Math.Sign(time) < 0)
            {
                time = 0;
            }

            Event ev = new Event(Model.Time + time, EnterTransact);

            Model.EventQueue.Enqueue(ev);
        }

        internal void Init()
        {

        }

        internal void Final()
        {

        }
    }
}
