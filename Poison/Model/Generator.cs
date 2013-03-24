using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poison.Stochastic;

namespace Poison.Model
{
    public class Generator
    {
        public Model Model
        {
            get;
            internal set;
        }

        public Generator(IDistribution distribution, TransactHandler handler)
        {
            throw new NotImplementedException();
        }        
    }
}
