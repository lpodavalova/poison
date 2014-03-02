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
using Poison.Extensions;

namespace Poison.Model
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

            bool fireNewItem = queue.Count == 0;

            UpdateLastCountChanged();

            queue.Enqueue(new TransactQueueInfo(transact, Model.Time));
            EntryCount++;

            if (Max < queue.Count)
            {
                Max = queue.Count;
            }

            if (fireNewItem)
            {
                OnNewItem(transact);
            }
        }

        private event InitFinalHandler<Queue> _Init;
        public event InitFinalHandler<Queue> Initialization
        {
            add { _Init += value; }
            remove { _Init -= value; }
        }

        private void OnInit()
        {
            if (_Init != null)
                _Init(this);
        }

        private event InitFinalHandler<Queue> _Final;
        public event InitFinalHandler<Queue> Finalization
        {
            add { _Final += value; }
            remove { _Final -= value; }
        }

        private void OnFinal()
        {
            if (_Final != null)
                _Final(this);
        }


        private event TransactHandler<Queue>  _NewItem;
        public event TransactHandler<Queue>  NewItem
        {
            add { _NewItem += value; }
            remove { _NewItem -= value; }
        }

        private void OnNewItem(Transact transact)
        {
            if (_NewItem != null)
                _NewItem(this, transact);
        }

        public Transact Dequeue()
        {
            if (queue.Count <= 0)
            {
                return null;
            }

            TransactQueueInfo info = queue.Peek();

            if (info.QueuingTime == Model.Time)
            {
                EntryCountZero++;
            }

            UpdateLastCountChanged();
            sumTransactQueueStayTime += Model.Time - info.QueuingTime;

            queue.Dequeue();
            Transact transact = info.Transact;

            if (queue.Count > 0)
            {
                info = queue.Peek();
                OnNewItem(info.Transact);
            }

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

        #region Statistics

        public int Max
        {
            get;
            private set;
        }

        public int EntryCount
        {
            get;
            private set;
        }

        public int EntryCountZero
        {
            get;
            private set;
        }

        public int Count
        {
            get
            {
                return queue.Count;
            }
        }

        public double AverageCount
        {
            get
            {
                return sumCountTimeMul / Model.Time;
            }
        }

        public double AverageTime
        {
            get
            {
                return sumTransactQueueStayTime.SmartDiv(EntryCount);
            }
        }

        public double AverageTimeNonZero
        {
            get
            {
                return sumTransactQueueStayTime.SmartDiv(EntryCount - EntryCountZero);
            }
        }

        private double lastCountChangedTime;
        private double sumCountTimeMul;
        private double sumTransactQueueStayTime;

        private void UpdateLastCountChanged()
        {
            sumCountTimeMul += queue.Count * (Model.Time - lastCountChangedTime);
            lastCountChangedTime = Model.Time;
        }

        #endregion

        internal void Init()
        {
            queue.Clear();
            Max = 0;
            EntryCount = 0;
            EntryCountZero = 0;
            lastCountChangedTime = 0.0;
            sumCountTimeMul = 0.0;
            sumTransactQueueStayTime = 0.0;
        }

        internal void Final()
        {

        }
    }
}
