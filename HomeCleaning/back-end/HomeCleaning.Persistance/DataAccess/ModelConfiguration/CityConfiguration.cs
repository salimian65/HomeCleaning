//using Domain.Model;using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Domain.ModelConfiguration
//{
//    public class CityConfiguration : IEntityTypeConfiguration<City>
//    {
//        public void Configure(EntityTypeBuilder<City> builder)
//        {
//            builder.HasKey(c => c.Id);
//            builder.Property(c => c.Id).ValueGeneratedOnAdd();
//            builder.Property(c => c.Name).HasMaxLength(50);
//        }
//    }
//}