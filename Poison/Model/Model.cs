﻿/*
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
using Poison.Collections;

namespace Poison.Model
{
    public class Model
    {
        public Model()
        {
            Queues = new QueueCollection(this);
            Devices = new DeviceCollection(this);
            Generators = new GeneratorCollection(this);

            EventQueue = new PriorityQueue<Event>();

            Time = 0;
        }

        public int RemainingCounter
        {
            get;
            private set;
        }

        public double Time
        {
            get;
            private set;
        }

        public QueueCollection Queues
        {
            get;
            private set;
        }

        public DeviceCollection Devices
        {
            get;
            private set;
        }

        public GeneratorCollection Generators
        {
            get;
            private set;
        }

        internal PriorityQueue<Event> EventQueue
        {
            get;
            private set;            
        }

        public void Simulate(int initialRemainingCounter)
        {
            RemainingCounter = initialRemainingCounter;

            foreach (Generator g in Generators)
            {
                g.GenerateEvent();                
            }

            while (IsAlive())
            {
                ProcessEvent();
            }
        }

        internal bool IsAlive()
        {
            return RemainingCounter > 0 && EventQueue.Count > 0;
        }

        public void Advance(double value, Transact transact, TransactHandler transactHandler)
        {
            EventQueue.Enqueue(new Event(Time + value, new EventHandler(delegate() 
                {
                    transactHandler(this, transact);
                })));

            while (IsAlive())
            {
                ProcessEvent();
            }
        }

        public void Terminate(int count)
        {
            RemainingCounter -= count;
        }

        internal void ProcessEvent()
        {
            Event ev = EventQueue.Dequeue();

            Time = ev.Time;

            ev.Handler();
        }
    }
}
