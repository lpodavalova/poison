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

using Poison.Extensions;
using Poison.Modelling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Statistics
{
    public class QueueStat
    {
        private double lastCountChangedTime;
        private double sumCountTimeMul;
        private double sumTransactQueueStayTime;

        public ModelStat ModelStat
        {
            get;
            private set;
        }

        public Queue Queue
        {
            get;
            private set;
        }
        
        public QueueStat(ModelStat modelStat, Queue queue)
        {
            if (modelStat == null)
            {
                throw new ArgumentNullException("modelStat");
            }

            if (queue == null)
            {
                throw new ArgumentNullException("queue");
            }

            ModelStat = modelStat;
            Queue = queue;

            Queue.Enqueueing += _Queue_Enqueueing;
            Queue.Enqueued += _Queue_Enqueued;
            Queue.Dequeueing += _Queue_Dequeueing;
        }
        
        private void _Queue_Dequeueing(Queue queue, Transact transact, double timeInQueue)
        {
            if (timeInQueue == 0.0)
            {
                EntryCountZero++;
            }

            UpdateLastCountChanged();
            sumTransactQueueStayTime += timeInQueue;
        }

        private void _Queue_Enqueued(Queue queue, Transact transact)
        {
            EntryCount++;

            if (Max < Queue.Count)
            {
                Max = Queue.Count;
            }
        }

        private void _Queue_Enqueueing(Queue queue, Transact transact)
        {
            UpdateLastCountChanged();
        }

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

        public double AverageCount
        {
            get
            {
                return sumCountTimeMul / ModelStat.Model.Time;
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

        private void UpdateLastCountChanged()
        {
            sumCountTimeMul += Queue.Count * (ModelStat.Model.Time - lastCountChangedTime);
            lastCountChangedTime = ModelStat.Model.Time;
        }
    }
}
