/*
* The Poison: discrete event simulation system.
* Copyright (C) 2006-2013 Poison team.
*
* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace Poison.Stochastic
{
    public class Normal : IDistribution
    {
        private bool isSpareReady = false;
        private double spare;

        public double Mean
        {
            get;
            private set;
        }

        public double StdDev
        {
            get;
            private set;
        }

        public Normal(double mean, double stdDev)
        {
            if (Math.Sign(stdDev) < 0)
            {
                throw new ArgumentException("stdDev should be more or equal to zero");
            }

            Mean = mean;
            StdDev = stdDev;
        }

        public double Next()
        {
            if (isSpareReady)
            {
                isSpareReady = false;
                return spare * StdDev + Mean;
            }
            else
            {
                double u, v, s;
                do
                {
                    u = RandomFactory.Randomizer.Next() * 2 - 1;
                    v = RandomFactory.Randomizer.Next() * 2 - 1;
                    s = u * u + v * v;
                } while (s >= 1 || s == 0);

                double mul = Math.Sqrt(-2.0 * Math.Log(s) / s);
                spare = v * mul;
                isSpareReady = true;
                return Mean + StdDev * u * mul;
            }
        }
    }
}
