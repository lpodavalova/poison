using System;
using Poison.Stochastic;

namespace Poison.Model
{
    public class Generator : IModelEntity
    {
        public Generator(string name, IDistribution distribution, TransactHandler handler)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (distribution == null)
            {
                throw new ArgumentNullException("distribution");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

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
