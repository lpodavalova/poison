namespace Poison.Modelling
{
    public class ReadOnlyGeneratorCollection : ReadOnlyModelEntityCollection<Generator>
    {
        public ReadOnlyGeneratorCollection(GeneratorCollection generatorCollection)
            : base(generatorCollection)
        {

        }
    }
}
