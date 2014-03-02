using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Model
{
    public delegate void InitFinalHandler<in T>(T obj);
}
