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
using System.Collections.Generic;
using Poison.Extensions;

namespace Poison.Modelling
{
    public class Queue : IModelEntity
    {
        private readonly Queue<TransactQueueInfo> _Queue = new Queue<TransactQueueInfo>();

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

        public void Enqueue(Transact transact)
        {
            if (transact == null)
            {
                throw new ArgumentNullException("transact");
            }

            OnEnqueueing(transact);                       

            _Queue.Enqueue(new TransactQueueInfo(transact, Model.Time));

            OnEnqueued(transact);
        }

        private event EventHandler<Queue> _Init;
        public event EventHandler<Queue> Initialization
        {
            add { _Init += value; }
            remove { _Init -= value; }
        }

        private void OnInit()
        {
            if (_Init != null)
                _Init(this);
        }

        private event EventHandler<Queue> _Final;
        public event EventHandler<Queue> Finalization
        {
            add { _Final += value; }
            remove { _Final -= value; }
        }

        private void OnFinal()
        {
            if (_Final != null)
                _Final(this);
        }

        private event EventHandler<Queue, Transact> _Enqueued;
        public event EventHandler<Queue, Transact> Enqueued
        {
            add { _Enqueued += value; }
            remove { _Enqueued -= value; }
        }

        private void OnEnqueued(Transact transact)
        {
            if (_Enqueued != null)
                _Enqueued(this, transact);
        }

        private event EventHandler<Queue, Transact> _Enqueueing;
        public event EventHandler<Queue, Transact> Enqueueing
        {
            add { _Enqueueing += value; }
            remove { _Enqueueing -= value; }
        }

        private void OnEnqueueing(Transact transact)
        {
            if (_Enqueueing != null)
                _Enqueueing(this, transact);
        }

        private event EventHandler<Queue, Transact, double> _Dequeued;
        public event EventHandler<Queue, Transact, double> Dequeued
        {
            add { _Dequeued += value; }
            remove { _Dequeued -= value; }
        }

        private void OnDequeued(Transact transact, double timeInQueue)
        {
            if (_Dequeued != null)
                _Dequeued(this, transact, timeInQueue);
        }

        private event EventHandler<Queue, Transact, double> _Dequeueing;
        public event EventHandler<Queue, Transact, double> Dequeueing
        {
            add { _Dequeueing += value; }
            remove { _Dequeueing -= value; }
        }

        private void OnDequeueing(Transact transact, double timeInQueue)
        {
            if (_Dequeueing != null)
                _Dequeueing(this, transact, timeInQueue);
        }

        public Transact Dequeue()
        {
            TransactQueueInfo info = _Queue.Peek();

            double timeInQueue = Model.Time - info.QueuingTime;

            OnDequeueing(info.Transact, timeInQueue);

            _Queue.Dequeue();
            Transact transact = info.Transact;

            OnDequeued(transact, timeInQueue);

            return transact;
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

        public bool Empty
        {
            get
            {
                return Count == 0;
            }
        }

        public int Count
        {
            get
            {
                return _Queue.Count;
            }
        }

        internal void Init()
        {
            _Queue.Clear();
            OnInit();
        }

        internal void Final()
        {
            OnFinal();
        }
    }
}
