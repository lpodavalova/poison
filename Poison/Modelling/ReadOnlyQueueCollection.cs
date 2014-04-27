namespace Poison.Modelling
{
    public class ReadOnlyQueueCollection : ReadOnlyModelEntityCollection<Queue>
    {
        public ReadOnlyQueueCollection(QueueCollection queueCollection)
            : base(queueCollection)
        {

        }
    }
}
