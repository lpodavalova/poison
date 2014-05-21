using Poison.Stochastic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poison.Modelling;
using System.Globalization;

namespace Poison.Train
{
    public class Train : Model
    {
        private const int _IntervalCount = 100;
        private const string _TrainGenerator = "train";
        private const string _SemaphorePrefix = "semaphore";
        private const string _IntervalPrefix = "interval";

    //    private const double _GeneratingAvgTime = 3.0;
    //    private const double _GeneratingStdDevTime = 2.0;

        private const double _IntervalAvgTime80 = 1.5;
        private const double _IntervalAvgTime60 = 2.0;
        private const double _IntervalStdDevTime80 = 0.1;
        private const double _IntervalStdDevTime60 = 0.1;

        private IDistribution _IntervalTime80 = new Normal(_IntervalAvgTime80, _IntervalStdDevTime80);
        private IDistribution _IntervalTime60 = new Normal(_IntervalAvgTime60, _IntervalStdDevTime60);

        /// <summary>
        /// Время жизни модели в минутах.
        /// </summary>
        private int _LifeTime = 60 * 24 * 7;
        
        protected override bool IsAlive()
        {
            return Time <= _LifeTime;
        }

        private double _GeneratingAvgTime = 3.0;
        public double GeneratingAvgTime
        {
            get
            {
                return _GeneratingAvgTime;
            }
            set
            {
                if (Math.Sign(value) < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Generating average time cannot be less than zero.");
                }
            }
        }

        public double GeneratingStdDevTime
        {
            get
            {
                return _GeneratingAvgTime * 0.1; // 10%
            }            
        }

        public int InputTrainCount
        {
            get;
            private set;
        }

        public int OutputTrainCount
        {
            get;
            private set;
        }

        protected override void Describe()
        {
            Initialization += Train_Initialization;
            Generator generator = new Generator(_TrainGenerator, new Normal(_GeneratingAvgTime, GeneratingStdDevTime));

            generator.Entered += generator_Entered;

            Generators.Add(generator);

            for (int i = 0; i < _IntervalCount; i++)
            {
                Queue semaphore = new Queue(GetPrefixedName(_SemaphorePrefix,i));

                semaphore.Enqueued += semaphore_Enqueued;
                semaphore.Dequeued += semaphore_Dequeued;

                Queues.Add(semaphore);

                Facility interval = new Facility(GetPrefixedName(_IntervalPrefix,i));

                interval.Released += interval_Released;

                Facilities.Add(interval);
            }
        }

        private void Train_Initialization(Model obj)
        {
            InputTrainCount = OutputTrainCount = 0;
        }

        private static string GetPrefixedName(string prefix, int i)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}_{1}", prefix, i);
        }

        private static int ExtractNumber(string prefixedName, out string prefix)
        {
            prefix = null;
            int index = prefixedName.LastIndexOf('_');

            if (index < 0)
            {
                return -1;
            }

            prefix = prefixedName.Substring(0, index);

            return int.Parse(prefixedName.Substring(index + 1));
        }

        private static int ExtractNumber(string prefixedName)
        {
            string value;
            return ExtractNumber(prefixedName, out value);
        }

        void interval_Released(Facility interval, Transact train)
        {
            int intervalNum = ExtractNumber(interval.Name);
            int nextIntervalNum = intervalNum + 1;

            // последний интервал? выводим поезд из системы
            if (nextIntervalNum >= _IntervalCount)
            {
                OutputTrainCount++;
                return;
            }

            Queues[GetPrefixedName(_SemaphorePrefix, nextIntervalNum)].Enqueue(train);

            Queue currentSemaphore = Queues[GetPrefixedName(_SemaphorePrefix, intervalNum)];

            // нет поездов на семафоре? выходим
            if (currentSemaphore.Count <= 0)
            {
                return;
            }

            // пытаемся обработать следующий поезд, стоящий на семафоре
            ProceedNextTrain(currentSemaphore);
        }

        private void semaphore_Dequeued(Queue semaphore, Transact train, double timeInQueue)
        {
            int intervalNumber = ExtractNumber(semaphore.Name);

            Facilities[GetPrefixedName(_IntervalPrefix, intervalNumber)].Seize(train);
        }

        private void ProceedNextTrain(Queue obj)
        {
            // извлекаем номер интервала
            int intervalNum = ExtractNumber(obj.Name);

            // получаем текущий интервал
            Facility currentInterval = Facilities[GetPrefixedName(_IntervalPrefix, intervalNum)];

            // интервал занят? остаемся в очереди
            if (currentInterval.State == FacilityState.Busy)
            {
                return;
            }

            // интервал последний? проходим на полном ходу
            if (intervalNum + 1 >= _IntervalCount)
            {
                obj.Dequeue();
                Advance(_IntervalTime80.Next(), interval_Train, intervalNum);

                return;
            }

            Queue nextSemaphore = Queues[GetPrefixedName(_SemaphorePrefix, intervalNum + 1)];
            Facility nextInterval = Facilities[GetPrefixedName(_IntervalPrefix, intervalNum + 1)];

            // перед следующим семафором, а значит на текущем интервале стоит другой поезд? остаемся в очереди
            if (nextSemaphore.Count > 0)
            {
                return;
            }

            // сейчас в любом случае двигаемся по интервалу, т.к. семафор либо желтый, либо зеленый
            // поездов гарантировано нет
            obj.Dequeue();

            // на следующем интерале есть поезд, либо поезд стоит перед следующим семафором?
            // едем медленно
            if (nextInterval.State == FacilityState.Busy || intervalNum + 2 < _IntervalCount &&
                Queues[GetPrefixedName(_SemaphorePrefix, intervalNum + 2)].Count > 0)
            {
                Advance(_IntervalTime60.Next(), interval_Train, intervalNum);
                return;
            }

            // едем быстро, преград нет
            Advance(_IntervalTime80.Next(), interval_Train, intervalNum);
        }
       
        private void semaphore_Enqueued(Queue obj, Transact train)
        {
            // Если новый транзакт оказывается не первым на семафоре (то есть есть другие транзакты,
            // ожидающие своей обработки, то транзакт остается в очереди ждать
            if (obj.Count > 1)
            {
                return;
            }

            ProceedNextTrain(obj);
        }

        private void interval_Train(object param)
        {
            int intervalNumber = (int)param;

            Facilities[GetPrefixedName(_IntervalPrefix, intervalNumber)].Release();
        }

        private void generator_Entered(Transact obj)
        {
            InputTrainCount++;

            if (_IntervalCount > 0)
            {
                Queues[GetPrefixedName(_SemaphorePrefix,0)].Enqueue(obj);
            }
        }
    }
}
