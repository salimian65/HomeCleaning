//using Domain.Model.People;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Domain.ModelConfiguration
//{
//    public class PersonConfiguration : IEntityTypeConfiguration<Person>
//    {
//        public void Configure(EntityTypeBuilder<Person> builder)
//        {
//            builder.Property(p => p.Id).ValueGeneratedOnAdd();
////            builder.Property(p => p.Cellphone);
////            builder.Property(p => p.Email);
////            builder.Property(p => p.Surname);
////            builder.Property(p => p.FirstName);
//            builder.Property(p => p.NationalCode);
//            builder.HasKey(p => p.Id);
//            builder.HasIndex(p => p.NationalCode).IsUnique();
//        }
//    }
//}