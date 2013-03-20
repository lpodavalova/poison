using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Model
{
    /// <summary>
    /// Транзакт
    /// </summary>
    public class Transact
    {
        public Guid TransactID
        {
            get;
            private set;
        }

        public int TransactPriority
        {
            get;
            private set;
        }
    }
}
