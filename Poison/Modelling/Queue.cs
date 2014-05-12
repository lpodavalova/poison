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
        private readonly Queue<TransactQueueInfo> queue = new Queue<TransactQueueInfo>();

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

            //bool fireNewItem = queue.Count == 0;

            OnEnqueueing(transact);

            //UpdateLastCountChanged();

            queue.Enqueue(new TransactQueueInfo(transact, Model.Time));
            //EntryCount++;

            //if (Max < queue.Count)
            //{
            //    Max = queue.Count;
            //}

            //if (fireNewItem)
            //{
            OnEnqueued(transact);
            //}
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

        private event EventHandler<Queue, Transact> _Dequeued;
        public event EventHandler<Queue, Transact> Dequeued
        {
            add { _Dequeued += value; }
            remove { _Dequeued -= value; }
        }

        private void OnDequeued(Transact transact)
        {
            if (_Dequeued != null)
                _Dequeued(this, transact);
        }

        private event EventHandler<Queue, Transact> _Dequeueing;
        public event EventHandler<Queue, Transact> Dequeueing
        {
            add { _Dequeueing += value; }
            remove { _Dequeueing -= value; }
        }

        private void OnDequeueing(Transact transact)
        {
            if (_Dequeueing != null)
                _Dequeueing(this, transact);
        }

        public Transact Dequeue()
        {
            TransactQueueInfo info = queue.Peek();

            OnDequeueing(info.Transact);

            //if (info.QueuingTime == Model.Time)
            //{
            //    EntryCountZero++;
            //}

            //UpdateLastCountChanged();
            //sumTransactQueueStayTime += Model.Time - info.QueuingTime;

            queue.Dequeue();
            Transact transact = info.Transact;

            OnDequeued(transact);

            //if (queue.Count > 0)
            //{   
            //    Enqueued();
            //}

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
                return queue.Count;
            }
        }

        //#region Statistics

        //public int Max
        //{
        //    get;
        //    private set;
        //}

        //public int EntryCount
        //{
        //    get;
        //    private set;
        //}

        //public int EntryCountZero
        //{
        //    get;
        //    private set;
        //}

        //public double AverageCount
        //{
        //    get
        //    {
        //        return sumCountTimeMul / Model.Time;
        //    }
        //}

        //public double AverageTime
        //{
        //    get
        //    {
        //        return sumTransactQueueStayTime.SmartDiv(EntryCount);
        //    }
        //}

        //public double AverageTimeNonZero
        //{
        //    get
        //    {
        //        return sumTransactQueueStayTime.SmartDiv(EntryCount - EntryCountZero);
        //    }
        //}

        //private double lastCountChangedTime;
        //private double sumCountTimeMul;
        //private double sumTransactQueueStayTime;

        //private void UpdateLastCountChanged()
        //{
        //    sumCountTimeMul += queue.Count * (Model.Time - lastCountChangedTime);
        //    lastCountChangedTime = Model.Time;
        //}

        //#endregion

        internal void Init()
        {
            queue.Clear();
            OnInit();
            //Max = 0;
            //EntryCount = 0;
            //EntryCountZero = 0;
            //lastCountChangedTime = 0.0;
            //sumCountTimeMul = 0.0;
            //sumTransactQueueStayTime = 0.0;
        }

        internal void Final()
        {
            OnFinal();
        }
    }
}
