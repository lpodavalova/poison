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
using Poison.Stochastic;

namespace Poison.Modelling
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

        private event EventHandler<Generator> _Init;
        public event EventHandler<Generator> Initialization
        {
            add { _Init += value; }
            remove { _Init -= value; }
        }

        private void OnInit()
        {
            if (_Init != null)
                _Init(this);
        }

        private event EventHandler<Generator> _Final;
        public event EventHandler<Generator> Finalization
        {
            add { _Final += value; }
            remove { _Final -= value; }
        }

        private void OnFinal()
        {
            if (_Final != null)
                _Final(this);
        }

        private event EventHandler<Transact> _Entered;
        public event EventHandler<Transact> Entered 
        {
            add { _Entered += value; }
            remove { _Entered -= value; }
        }

        private void OnEntered(Transact transact)
        {
            if (_Entered != null)
                _Entered(transact);
        }

        private void EnterTransact(object param)
        {
            Transact transact = new Transact(this);

            GenerateEvent();

            OnEntered(transact);
        }

        internal void GenerateEvent()
        {
            double time = Math.Abs(Distribution.Next());

            Event ev = new Event(Model.Time + time, EnterTransact);

            Model.EventQueue.Enqueue(ev);
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
