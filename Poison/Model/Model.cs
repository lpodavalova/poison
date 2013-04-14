using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poison.Stochastic;
using Poison.Collections;

namespace Poison.Model
{
    public class Model
    {
        public Model()
        {
            Queues = new QueueCollection();
            Devices = new DeviceCollection();
            Generators = new GeneratorCollection();
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
