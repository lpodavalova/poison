using System;

namespace Poison.Stochastic
{
    public class Uniform : IDistribution
    {
        public double Min
        {
            get;
            private set;
        }

        public double Max
        {
            get;
            private set;
        }

        public Uniform(double min, double max)
        {
            throw new NotImplementedException();            
        }

        public double Next()
        {
            throw new NotImplementedException();
        }
    }
}
