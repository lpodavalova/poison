namespace Poison.Model
{
    public interface IModelEntity
    {
        string Name
        {
            get;
        }

        Model Model
        {
            get;
            set;
        }
    }
}
