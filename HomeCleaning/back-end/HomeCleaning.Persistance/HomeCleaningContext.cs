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

        public DbSet<SpaceSize> SpaceSize { get; set; }

        public DbSet<CleaningPackage> CleaningPackage { get; set; }

        public DbSet<CleaningExtraOption> CleaningExtraOption { get; set; }

        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelBuilder.ApplyConfiguration(new ProvinceConfiguration());

            //modelBuilder.Entity<Order>().HasOne(p => p.SpaceSize).WithOne().HasForeignKey<SpaceSize>(a=>a.Id);
            //modelBuilder.Entity<Order>().HasOne(p => p.CleaningPackage).WithOne().HasForeignKey<CleaningPackage>(a => a.Id);
            //modelBuilder.Entity<Order>().HasOne(p => p.CleaningExtraOption).WithOne().HasForeignKey<CleaningExtraOption>(a => a.Id);
            //  modelBuilder.Entity<Order>().HasOne(p => p.);

            modelBuilder.Entity<Order>().OwnsOne(p => p.Customer);
            modelBuilder.Entity<Order>().OwnsOne(p => p.Address);
            //  modelBuilder.Entity<Customer>(eb =>{eb.HasNoKey();});

            modelBuilder.Entity<CleaningCategory>().HasData(
                 new CleaningCategory { Id = 1, Name = "Home Cleaning" },
                 new CleaningCategory { Id = 2, Name = "Empty Home Cleaning" },
                 new CleaningCategory { Id = 3, Name = "Cleaning after construction" },
                 new CleaningCategory { Id = 4, Name = "Villa Cleaning" },
                 new CleaningCategory { Id = 5, Name = "Office Cleaning" });



            modelBuilder.Entity<SpaceSize>().HasData(
                new SpaceSize { Id = 1, CleaningCategoryId = 1, Name = "1 + 1" },
                new SpaceSize { Id = 2, CleaningCategoryId = 1, Name = "2 + 1" },
                new SpaceSize { Id = 3, CleaningCategoryId = 1, Name = "3 + 1" },
                new SpaceSize { Id = 4, CleaningCategoryId = 1, Name = "4 + 1" },

                new SpaceSize { Id = 5, CleaningCategoryId = 2, Name = "1 + 1" },
                new SpaceSize { Id = 6, CleaningCategoryId = 2, Name = "2 + 1" },
                new SpaceSize { Id = 7, CleaningCategoryId = 2, Name = "3 + 1" },
                new SpaceSize { Id = 8, CleaningCategoryId = 2, Name = "4 + 1" },

                new SpaceSize { Id = 9, CleaningCategoryId = 3, Name = "1 + 1" },
                new SpaceSize { Id = 10, CleaningCategoryId = 3, Name = "2 + 1" },
                new SpaceSize { Id = 11, CleaningCategoryId = 3, Name = "3 + 1" },
                new SpaceSize { Id = 12, CleaningCategoryId = 3, Name = "4 + 1" },

                new SpaceSize { Id = 13, CleaningCategoryId = 5, Name = "100 m2 and below" },
                new SpaceSize { Id = 14, CleaningCategoryId = 5, Name = "100-150 m2" },
                new SpaceSize { Id = 15, CleaningCategoryId = 5, Name = "150-200 m2" },
                new SpaceSize { Id = 16, CleaningCategoryId = 5, Name = "200-250 m2" },
                new SpaceSize { Id = 17, CleaningCategoryId = 5, Name = "250 m2 above" },

                new SpaceSize { Id = 18, CleaningCategoryId = 4, Name = "200-275 m2" },
                new SpaceSize { Id = 19, CleaningCategoryId = 4, Name = "275-350 m2" },
                new SpaceSize { Id = 20, CleaningCategoryId = 4, Name = "350-425 m2" },
                new SpaceSize { Id = 21, CleaningCategoryId = 4, Name = "425 m2 above" }
            );



            modelBuilder.Entity<CleaningPackage>().HasData(
                new CleaningPackage { Id = 1, CleaningCategoryId = 1, Name = "First Cleaning Package" },
                new CleaningPackage { Id = 2, CleaningCategoryId = 1, Name = "Second Cleaning Package" },
                new CleaningPackage { Id = 3, CleaningCategoryId = 1, Name = "Third Cleaning Package" },

                new CleaningPackage { Id = 4, CleaningCategoryId = 4, Name = "First Cleaning Package" },
                new CleaningPackage { Id = 5, CleaningCategoryId = 4, Name = "Second Cleaning Package" },
                new CleaningPackage { Id = 6, CleaningCategoryId = 4, Name = "Third Cleaning Package" }
                );



            modelBuilder.Entity<CleaningExtraOption>().HasData(
                new CleaningExtraOption { Id = 1, CleaningCategoryId = 1, Name = "ironing" },
                new CleaningExtraOption { Id = 2, CleaningCategoryId = 1, Name = "refrigerator cleaning" },
                new CleaningExtraOption { Id = 3, CleaningCategoryId = 1, Name = "wardrobe cleaning" },

                new CleaningPackage { Id = 4, CleaningCategoryId = 5, Name = "interior glass cleaning" },

                new CleaningExtraOption { Id = 5, CleaningCategoryId = 4, Name = "ironing" },
                new CleaningExtraOption { Id = 6, CleaningCategoryId = 4, Name = "refrigerator cleaning" },
                new CleaningExtraOption { Id = 7, CleaningCategoryId = 4, Name = "wardrobe cleaning" }
            );

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging(true);
        }
    }
}