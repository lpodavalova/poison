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
using System.Collections.Generic;

namespace Poison.Model
{
    public class Queue : IModelEntity
    {
        private Queue<Transact> queue = new Queue<Transact>();

        public Queue(string name)
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

        public void Enqueue(Transact transact, TransactHandler transactHandler)
        {
            if (transact == null)
            {
                throw new ArgumentNullException("transact");
            }

            if (transactHandler == null)
            {
                throw new ArgumentNullException("transactHandler");
            }

            queue.Enqueue(transact);
            while (Model.IsAlive() && queue.Peek() != transact)
            {
                Model.ProcessEvent();
            }

            if (!Model.IsAlive())
            {
                return;
            }

            transactHandler(Model, transact);
        }

        public void Dequeue(Transact transact)
        {
            if (transact == null)
            {
                throw new ArgumentNullException("transact");
            }

            if (queue.Peek() != transact)
            {
                // TODO: throw exception
            }
            queue.Dequeue();
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
    }
}
