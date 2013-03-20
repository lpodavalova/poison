using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Stochastic
{
    public class Normal : IDistribution
    {
        public double Mean
        {
            get;
            private set;
        }

        public double StDev
        {
            get;
            private set;
        }

        public Normal(double mean, double stdDev)
        {
            throw new NotImplementedException();
        }

        public double Next()
        {
            throw new NotImplementedException();
        }
    }
}
