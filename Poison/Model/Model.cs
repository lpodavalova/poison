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

        public void Simulate(int counterStartValue)
        {
            throw new NotImplementedException();
        }

        public void Advance(double value)
        {
            throw new NotImplementedException();
        }

        public void Terminate(int count)
        {
            throw new NotImplementedException();
        }

        internal void ProcessEvent()
        {
            throw new NotImplementedException();
        }
    }
}
