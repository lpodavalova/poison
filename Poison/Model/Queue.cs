using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Model
{
    public class Queue
    {
        private Queue<Transact> queue = new Queue<Transact>();

        public Queue(string name)
        {
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

        public void Enqueue(Transact transact, TransactHandler transactHandler)
        {
            queue.Enqueue(transact);
            while (Model.IsAlive() && queue.Peek() != transact)
            {
                Model.ProcessEvent();
            }

            if (!Model.IsAlive())
            {
                return;
            }

            transactHandler(Model, transact);
        }

        public void Dequeue(Transact transact)
        {
            if (queue.Peek() != transact)
            {
                // TODO: throw exception
            }
            queue.Dequeue();
        }
    }
}
