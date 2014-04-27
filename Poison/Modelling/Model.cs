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
using System.Linq;
using Poison.Collections;
using Poison.Stochastic;

namespace Poison.Modelling
{
    public abstract class Model
    {
        private QueueCollection queues;
        private FacilityCollection facilities;
        private GeneratorCollection generators;

        public Model()
        {
            queues = new QueueCollection(this);
            facilities = new FacilityCollection(this);
            generators = new GeneratorCollection(this);

            Queues = new ReadOnlyQueueCollection(queues);
            Facilities = new ReadOnlyFacilityCollection(facilities);
            Generators = new ReadOnlyGeneratorCollection(generators);

            EventQueue = new PriorityQueue<Event>();

            Time = 0;
        }
        
        public double Time
        {
            get;
            private set;
        }

        protected ReadOnlyQueueCollection Queues
        {
            get;
            private set;
        }

        protected ReadOnlyFacilityCollection Facilities
        {
            get;
            private set;
        }

        protected ReadOnlyGeneratorCollection Generators
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
            ModelObjects objects = new ModelObjects(this);
            Time = 0;

            queues.Clear();
            facilities.Clear();
            generators.Clear();

            EventQueue.Clear();

            Describe(objects);

            Facility[] facilityArray = objects.Facilities.ToArray();
            objects.Facilities.Clear();

            foreach (Facility f in facilityArray)
            {
                facilities.Add(f);
            }

            Generator[] GeneratorArray = objects.Generators.ToArray();
            objects.Generators.Clear();

            foreach (Generator g in GeneratorArray)
            {
                generators.Add(g);
            }

            Queue[] queueArray = objects.Queues.ToArray();
            objects.Queues.Clear();

            foreach (Queue q in queueArray)
            {
                queues.Add(q);
            }

            foreach (Facility f in facilities)
            {
                f.Init();
            }

            foreach (Generator g in generators)
            {
                g.Init();
            }

            foreach (Queue q in queues)
            {
                q.Init();
            }

            foreach (Generator g in generators)
            {
                g.GenerateEvent();                
            }

            while (IsAlive() && EventQueue.Count > 0)
            {
                ProcessEvent();
            }

            foreach (Queue q in queues)
            {
                q.Final();
            }

            foreach (Generator g in generators)
            {
                g.Final();
            }

            foreach (Facility f in facilities)
            {
                f.Final();
            }
        }

        protected abstract bool IsAlive();

        protected abstract void Describe(ModelObjects modelObjects);

        protected void Advance(double value, EventHandler eventHandler)
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
