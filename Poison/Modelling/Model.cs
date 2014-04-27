﻿/*
* The Poison: discrete event simulation system.
* Copyright (C) 2006-2014 Poison team.
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
using Poison.Stochastic;

namespace Poison.Modelling
{
    public abstract class Model
    {
        public Model()
        {
            Queues = new QueueCollection(this);
            Facilities = new FacilityCollection(this);
            Generators = new GeneratorCollection(this);

            EventQueue = new PriorityQueue<Event>();

            Time = 0;
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

        public FacilityCollection Facilities
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

        public void Simulate()
        {
            foreach (Facility f in Facilities)
            {
                f.Init();
            }

            foreach (Generator g in Generators)
            {
                g.Init();
            }

            foreach (Queue q in Queues)
            {
                q.Init();
            }

            foreach (Generator g in Generators)
            {
                g.GenerateEvent();                
            }

            while (IsAlive() && EventQueue.Count > 0)
            {
                ProcessEvent();
            }

            foreach (Queue q in Queues)
            {
                q.Final();
            }

            foreach (Generator g in Generators)
            {
                g.Final();
            }

            foreach (Facility f in Facilities)
            {
                f.Final();
            }
        }

        protected abstract bool IsAlive();

        public void Advance(double value, EventHandler eventHandler)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException("eventHandler");
            }

            if (Math.Sign(value) < 0)
            {
                throw new ArgumentException("Parameter `value` cannot be less than zero");
            }

            EventQueue.Enqueue(new Event(Time + value, eventHandler));
        }
        
        internal void ProcessEvent()
        {
            Event ev = EventQueue.Dequeue();

            Time = ev.Time;

            ev.Handler();
        }
    }
}