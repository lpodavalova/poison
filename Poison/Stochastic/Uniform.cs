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
            if (min >= max)
            {
                throw new ArgumentException("min should be less than max");
            }

            Min = min;
            Max = max;
        }

        public double Next()
        {
            return Min + (Max - Min) * RandomFactory.Randomizer.Next();
        }
    }
}
