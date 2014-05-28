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
        public static readonly double[] IntervalLengthsInKm =
            new double[]
            {
                0.8358854403 * 1.6+1,
                0.1216813703 * 1.6+1,
                0.2330374705 * 1.6+1,
                0.0571244662 * 1.6+1,
                0.3476348755 * 1.6+1,
                0.4997535600 * 1.6+1,
                0.5129380646 * 1.6+1,
                0.5322464456 * 1.6+1,
                0.1890527418 * 1.6+1,
                0.3496752406 * 1.6+1,
                0.9629224925 * 1.6+1,
                0.1829069365 * 1.6+1,
                0.2426321571 * 1.6+1,
                0.8760952125 * 1.6+1,
                0.9878292621 * 1.6+1,
                0.5044391008 * 1.6+1,
                0.8757501045 * 1.6+1,
                0.9557661109 * 1.6+1,
                0.6383585684 * 1.6+1,
                0.5165922558 * 1.6+1,
                0.8488683349 * 1.6+1,
                0.9434606185 * 1.6+1,
                0.3105023191 * 1.6+1,
                0.8982669488 * 1.6+1,
                0.5570659308 * 1.6+1,
                0.2697462705 * 1.6+1,
                0.9390240834 * 1.6+1,
                0.1927943249 * 1.6+1,
                0.4176402765 * 1.6+1,
                0.4415296679 * 1.6+1,
                0.9809981151 * 1.6+1,
                0.2703538659 * 1.6+1,
                0.4456346185 * 1.6+1,
                0.4560164579 * 1.6+1,
                0.9147085437 * 1.6+1,
                0.4007749883 * 1.6+1,
                0.1604338091 * 1.6+1,
                0.6596279598 * 1.6+1,
                0.2565392761 * 1.6+1,
                0.4383823467 * 1.6+1,
                0.0097629967 * 1.6+1,
                0.1886852152 * 1.6+1,
                0.6035210471 * 1.6+1,
                0.1690094885 * 1.6+1,
                0.6222445149 * 1.6+1,
                0.5831706138 * 1.6+1,
                0.2569727053 * 1.6+1,
                0.3566422077 * 1.6+1,
                0.7813262912 * 1.6+1,
                0.6497981478 * 1.6+1,
                0.7010518545 * 1.6+1,
                0.6399629845 * 1.6+1,
                0.0351156535 * 1.6+1,
                0.0107699229 * 1.6+1,
                0.1327171523 * 1.6+1,
                0.0430031896 * 1.6+1,
                0.1997887991 * 1.6+1,
                0.6772777872 * 1.6+1,
                0.1171699134 * 1.6+1,
                0.1558473528 * 1.6+1,
                0.5557253616 * 1.6+1,
                0.1429705768 * 1.6+1,
                0.9439222043 * 1.6+1,
                0.1645378165 * 1.6+1,
                0.9224512024 * 1.6+1,
                0.6312082083 * 1.6+1,
                0.2216790582 * 1.6+1,
                0.6458599263 * 1.6+1,
                0.9957717306 * 1.6+1,
                0.0452644867 * 1.6+1,
                0.5931025426 * 1.6+1,
                0.9000647102 * 1.6+1,
                0.4621185031 * 1.6+1,
                0.8675012227 * 1.6+1,
                0.2944391861 * 1.6+1,
                0.8718264691 * 1.6+1,
                0.6009588230 * 1.6+1,
                0.4974695450 * 1.6+1,
                0.6525170881 * 1.6+1,
                0.8721299113 * 1.6+1,
                0.0473346944 * 1.6+1,
                0.7023415592 * 1.6+1,
                0.5391547762 * 1.6+1,
                0.1984565230 * 1.6+1,
                0.1566836246 * 1.6+1,
                0.6592246196 * 1.6+1,
                0.0850610191 * 1.6+1,
                0.3611559016 * 1.6+1,
                0.0468437109 * 1.6+1,
                0.7513642235 * 1.6+1,
                0.2234823808 * 1.6+1,
                0.3549097176 * 1.6+1,
                0.3579152176 * 1.6+1,
                0.3225554462 * 1.6+1,
                0.6298354904 * 1.6+1,
                0.2016795819 * 1.6+1,
                0.6307696689 * 1.6+1,
                0.3465889066 * 1.6+1,
                0.7924637565 * 1.6+1,
                0.5339119421 * 1.6+1
            };

        private const string _TrainGenerator = "train";
        private const string _SemaphorePrefix = "semaphore";
        private const string _IntervalPrefix = "interval";

        private const double _1KmAvgTime80 = 0.75;
        private const double _1KmAvgTime60 = 1.0;
        private const double _1KmStdDevTime80 = 0.05;
        private const double _1KmStdDevTime60 = 0.05;

        private IDistribution _1KmTime80 = new Normal(_1KmAvgTime80, _1KmStdDevTime80);
        private IDistribution _1KmTime60 = new Normal(_1KmAvgTime60, _1KmStdDevTime60);

        /// <summary>
        /// Время жизни модели в минутах.
        /// </summary>
        private int _LifeTime = 60 * 24 * 7;
        
        protected override bool IsAlive()
        {
            return Time <= _LifeTime;
        }

        private double _GeneratingAvgTime = 3.44;
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

                _GeneratingAvgTime = value;
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

            for (int i = 0; i < IntervalLengthsInKm.Length; i++)
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

        public static string GetSemaphoreName(int i)
        {
            return GetPrefixedName(_SemaphorePrefix, i);
        }

        public static string GetIntervalName(int i)
        {
            return GetPrefixedName(_IntervalPrefix, i);
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
            if (nextIntervalNum >= IntervalLengthsInKm.Length)
            {
                OutputTrainCount++;
            }
            else
            {
                Queues[GetPrefixedName(_SemaphorePrefix, nextIntervalNum)].Enqueue(train);
            }

            Queue currentSemaphore = Queues[GetPrefixedName(_SemaphorePrefix, intervalNum)];

            // есть поезда на семафоре
            if (currentSemaphore.Count > 0)
            {
                // пытаемся обработать следующий поезд, стоящий на семафоре
                ProceedNextTrain(currentSemaphore);
            }
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
            if (intervalNum + 1 >= IntervalLengthsInKm.Length)
            {
                obj.Dequeue();
                Advance(_1KmTime80.Next() * IntervalLengthsInKm[intervalNum], interval_Train, intervalNum);

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
            if (nextInterval.State == FacilityState.Busy || intervalNum + 2 < IntervalLengthsInKm.Length &&
                Queues[GetPrefixedName(_SemaphorePrefix, intervalNum + 2)].Count > 0)
            {
                Advance(_1KmTime60.Next() * IntervalLengthsInKm[intervalNum], interval_Train, intervalNum);
                return;
            }

            // едем быстро, преград нет
            Advance(_1KmTime80.Next() * IntervalLengthsInKm[intervalNum], interval_Train, intervalNum);
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

            if (IntervalLengthsInKm.Length > 0)
            {
                Queues[GetPrefixedName(_SemaphorePrefix,0)].Enqueue(obj);
            }
        }
    }
}
