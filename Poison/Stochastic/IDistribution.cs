﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Stochastic
{
    public interface IDistribution
    {
        double Next();
    }
}
