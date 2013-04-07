using System;

namespace Poison.Model
{
    public class Queue : IModelEntity
    {
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
            throw new NotImplementedException();
        }

        public void Dequeue(Transact transact)
        {
            throw new NotImplementedException();
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
    }
}
