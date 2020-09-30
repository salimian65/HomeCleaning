using HomeCleaning.Domain;
using Microsoft.EntityFrameworkCore;

namespace HomeCleaning.Persistance
{
    public class HomeCleaningContext : DbContext
    {
        public HomeCleaningContext(DbContextOptions<HomeCleaningContext> options)
            : base(options)
        {
        }

        public DbSet<CleaningCategory> CleaningCategory { get; set; }

        public DbSet<CleaningOption> CleaningOption { get; set; }

        public DbSet<SpaceSize> SpaceSize { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cleaningCategory1 = new CleaningCategory { Id = 1, Name = "Home Cleaning" };
            var cleaningCategory2 = new CleaningCategory { Id = 2, Name = "Empty Home Cleaning" };
            var cleaningCategory3 = new CleaningCategory { Id = 3, Name = "Cleaning after construction" };
            var cleaningCategory4 = new CleaningCategory { Id = 4, Name = "Villa Cleaning" };
            var cleaningCategory5 = new CleaningCategory { Id = 5, Name = "Office Cleaning" };

            modelBuilder.Entity<CleaningCategory>().HasData(
                cleaningCategory1,
                cleaningCategory2,
                cleaningCategory3,
                cleaningCategory4,
                cleaningCategory5
            );

            modelBuilder.Entity<SpaceSize>().HasData(
                new SpaceSize { Id = 1, CleaningCategoryId = 1, Name = "1 + 1" },
                new SpaceSize { Id = 2, CleaningCategoryId = 1, Name = "2 + 1" },
                new SpaceSize { Id = 3, CleaningCategoryId = 1, Name = "3 + 1" },
                new SpaceSize { Id = 4, CleaningCategoryId = 1, Name = "4 + 1" }
            //  new SpaceSize { CleaningCategory = cleaningCategory4, Name = "Office Cleaning" }
            );

            modelBuilder.Entity<CleaningOption>().HasData(
                new SpaceSize { Id = 1, CleaningCategoryId = 1, Name = "ironing" },
                new SpaceSize { Id = 2, CleaningCategoryId = 1, Name = "refrigerator cleaning" },
                new SpaceSize { Id = 3, CleaningCategoryId = 1, Name = "wardrobe cleaning" },
                new SpaceSize { Id = 4, CleaningCategoryId = 1, Name = "4 + 1" }
                //  new SpaceSize { CleaningCategory = cleaningCategory4, Name = "Office Cleaning" }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
        }
    }
}