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
        private const int intervalCount = 100;
        private const string trainGenerator = "train";
        private const string semaphorePrefix = "semaphore";
        private const string intervalPrefix = "interval";

        private const double intervalAvgTime80 = 1.5;
        private const double intervalAvgTime60 = 2.0;
        private const double intervalStdDevTime80 = 0.1;
        private const double intervalStdDevTime60 = 0.1;

        private IDistribution intervalTime80 = new Normal(intervalAvgTime80, intervalStdDevTime80);
        private IDistribution intervalTime60 = new Normal(intervalAvgTime60, intervalStdDevTime60);

        /// <summary>
        /// Время жизни модели в минутах.
        /// </summary>
        private int lifeTime = 60 * 24 * 7;
        
        protected override bool IsAlive()
        {
            return Time <= lifeTime;
        }

        protected override void Describe(ModelObjects modelObjects)
        {
            Generator generator = new Generator(trainGenerator, new Normal(10, 2));

            generator.Entered += generator_Entered;

            modelObjects.Generators.Add(generator);

            for (int i = 0; i < intervalCount; i++)
            {
                Queue semaphore = new Queue(GetPrefixedName(semaphorePrefix,i));

                semaphore.Enqueued += semaphore_Enqueued;
                semaphore.Dequeued += semaphore_Dequeued;

                modelObjects.Queues.Add(semaphore);

                Facility interval = new Facility(GetPrefixedName(intervalPrefix,i));

                interval.Released += interval_Released;

                modelObjects.Facilities.Add(interval);
            }
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
            if (nextIntervalNum >= intervalCount)
            {
                return;
            }

            Queues[GetPrefixedName(semaphorePrefix, nextIntervalNum)].Enqueue(train);

            Queue currentSemaphore = Queues[GetPrefixedName(semaphorePrefix, intervalNum)];

            // нет поездов на семафоре? выходим
            if (currentSemaphore.Count <= 0)
            {
                return;
            }

            // пытаемся обработать следующий поезд, стоящий на семафоре
            ProceedNextTrain(currentSemaphore);
        }

        private void semaphore_Dequeued(Queue semaphore, Transact train)
        {
            int intervalNumber = ExtractNumber(semaphore.Name);

            Facilities[GetPrefixedName(intervalPrefix, intervalNumber)].Seize(train);
        }

        private void ProceedNextTrain(Queue obj)
        {
            // извлекаем номер интервала
            int intervalNum = ExtractNumber(obj.Name);

            // получаем текущий интервал
            Facility currentInterval = Facilities[GetPrefixedName(intervalPrefix, intervalNum)];

            // интервал занят? остаемся в очереди
            if (currentInterval.State == FacilityState.Busy)
            {
                return;
            }

            // интервал последний? проходим на полном ходу
            if (intervalNum + 1 >= intervalCount)
            {
                obj.Dequeue();
                Advance(intervalStdDevTime80, interval_Train, intervalNum);

                return;
            }

            Queue nextSemaphore = Queues[GetPrefixedName(semaphorePrefix, intervalNum + 1)];
            Facility nextInterval = Facilities[GetPrefixedName(intervalPrefix, intervalNum + 1)];

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
            if (nextInterval.State == FacilityState.Busy || intervalNum + 2 < intervalCount &&
                Queues[GetPrefixedName(semaphorePrefix, intervalNum + 2)].Count > 0)
            {
                Advance(intervalStdDevTime60, interval_Train, intervalNum);
                return;
            }

            // едем быстро, преград нет
            Advance(intervalStdDevTime80, interval_Train, intervalNum);
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

            Facilities[GetPrefixedName(intervalPrefix, intervalNumber)].Release();
        }

        private void generator_Entered(Transact obj)
        {
            if (intervalCount > 0)
            {
                Queues[GetPrefixedName(semaphorePrefix,0)].Enqueue(obj);
            }
        }
    }
}
