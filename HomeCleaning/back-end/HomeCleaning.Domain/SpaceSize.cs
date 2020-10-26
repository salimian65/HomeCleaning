

using Framework.Domain;

namespace HomeCleaning.Domain
{
    public class SpaceSize : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; } 

        public int CleaningCategoryId { get; set; }

        public CleaningCategory CleaningCategory { get; set; }
    }
}
