using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Stochastic
{
    public static class RandomFactory
    {
        private static IRandom random = new Random();

        public static IRandom Randomizer
        {
            get
            {
                return random;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                random = value;
            }
        }
    }
}
