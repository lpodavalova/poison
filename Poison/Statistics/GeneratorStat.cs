﻿/*
* The Poison: discrete event simulation system.
* Copyright (C) 2013-2014 Poison team.
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

using Poison.Modelling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Statistics
{
    public class GeneratorStat
    {
        public ModelStat ModelStat
        {
            get;
            private set;
        }

        public Generator Generator
        {
            get;
            private set;
        }

        public GeneratorStat(ModelStat modelStat, Generator generator)
        {
            if (modelStat == null)
            {
                throw new ArgumentNullException("modelStat");
            }

            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }

            ModelStat = modelStat;
            Generator = generator;

            generator.Initialization += generator_Initialization;
            generator.Entered += generator_Entered;
        }

        public int GeneratedTransactCount
        {
            get;
            private set;
        }

        private void generator_Entered(Transact obj)
        {
            GeneratedTransactCount++;
        }

        private void generator_Initialization(Generator obj)
        {
            GeneratedTransactCount = 0;
        }
    }
}
