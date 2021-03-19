using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> modelBuilder)
        {
            //Name Of Table


            //Book
            modelBuilder.HasKey(b => b.Book_Id);
            modelBuilder.Property(b => b.Title).IsRequired();
            modelBuilder.Property(b => b.Price).IsRequired();
            modelBuilder.Property(b => b.ISBN).IsRequired().HasMaxLength(15);
            //one to one relation between book and book detail
            modelBuilder.HasOne(b => b.Fluent_BookDetail)
                .WithOne(bd => bd.Fluent_Book).HasForeignKey<Fluent_Book>("BookDetail_Id");

            //one to many relation between book and publisher
            modelBuilder.HasOne(x => x.Fluent_Publisher)
                .WithMany(x => x.Fluent_Books)
                .HasForeignKey(x => x.Publisher_Id);
        }
    }
}
