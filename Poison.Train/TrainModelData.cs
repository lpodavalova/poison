using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poison.Train
{
    class TrainModelData
    {
        public int Pmax 
        {
            get; 
            set; 
        }

        public int Pcur
        {
            get;
            set;
        }

        public int Pavg
        {
            get;
            set;
        }

        public double GeneratingAvgTime
        {
            get;
            set;
        }

        public int InputTrainCount
        {
            get;
            set;
        }

        public int OutputTrainCount
        {
            get;
            set;
        }

        public List<double> IntervalsUtil
        {
            get;
            private set;
        }

        public TrainModelData()
        {
            IntervalsUtil = new List<double>();
        }
    }
}
