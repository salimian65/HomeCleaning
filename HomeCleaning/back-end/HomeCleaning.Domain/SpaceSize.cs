namespace HomeCleaning.Domain
{
    public class SpaceSize : Entity
    {
        public string Name { get; set; }

        public CleaningCategory CleaningCategory { get; set; }
    }
}
