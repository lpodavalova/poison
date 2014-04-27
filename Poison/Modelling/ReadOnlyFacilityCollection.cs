namespace Poison.Modelling
{
    public class ReadOnlyFacilityCollection : ReadOnlyModelEntityCollection<Facility>
    {
        public ReadOnlyFacilityCollection(FacilityCollection facilityCollection)
            : base(facilityCollection)
        {

        }
    }
}
