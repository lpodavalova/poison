using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Extensions
{
    static class DoubleExt
    {
        public static double SmartDiv(this double a, int b)
        {
            if (b == 0)
            {
                return 0.0;
            }

            return a / b;
        }
    }
}
