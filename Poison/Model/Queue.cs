﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Model
{
    public class Queue
    {
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

        public void Enqueue(Transact transact)
        {
            throw new NotImplementedException();
        }

        public void Dequeue(Transact transact)
        {
            throw new NotImplementedException();
        }
    }
}
