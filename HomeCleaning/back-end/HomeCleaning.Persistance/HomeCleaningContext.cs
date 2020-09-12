using Microsoft.EntityFrameworkCore;

namespace HomeCleaning.Persistance
{
    public class HomeCleaningContext : DbContext
    {
        public HomeCleaningContext (DbContextOptions<HomeCleaningContext> options)
            : base(options)
        {
        }

        public DbSet<HomeCleaning.Domain.CleaningCategory> CleaningCategory { get; set; }

        public DbSet<HomeCleaning.Domain.CleaningOption> CleaningOption { get; set; }

        public DbSet<HomeCleaning.Domain.SpaceSize> SpaceSize { get; set; }

    }
}
