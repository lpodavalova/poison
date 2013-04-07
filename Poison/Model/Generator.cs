using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poison.Stochastic;

namespace Poison.Model
{
    public class Generator
    {
        public Generator(string name, IDistribution distribution, TransactHandler handler)
        {
            Distribution = distribution;
            this.handler = handler;
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

        public IDistribution Distribution
        {
            get;
            private set;
        }

        private TransactHandler handler;

        private void EnterTransact()
        {
            Transact transact = new Transact(Model, this);

            GenerateEvent();

            handler(Model,transact);
        }

        public void GenerateEvent()
        {
            double time = Distribution.Next();

            Event ev = new Event(Model.Time + time, EnterTransact);

            Model.EventQueue.Enqueue(ev);
        }
    }
}
