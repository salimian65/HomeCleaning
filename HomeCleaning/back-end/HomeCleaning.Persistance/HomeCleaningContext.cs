using Microsoft.EntityFrameworkCore;

namespace HomeCleaning.Persistance
{
    public class HomeCleaningContext : DbContext
    {
        public HomeCleaningContext (DbContextOptions<HomeCleaningContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.CleaningCategory> CleaningCategory { get; set; }

        public DbSet<Domain.CleaningOption> CleaningOption { get; set; }

        public DbSet<Domain.SpaceSize> SpaceSize { get; set; }

        public DbSet<Domain.Address> Address { get; set; }

        public DbSet<Domain.Order> Order { get; set; }
    }
}
