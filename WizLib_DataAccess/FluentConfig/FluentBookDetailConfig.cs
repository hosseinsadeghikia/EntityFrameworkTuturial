using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookDetailConfig : IEntityTypeConfiguration<Fluent_BookDetail>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookDetail> modelBuilder)
        {
            //BookDetail
            modelBuilder.HasKey(bd => bd.BookDetail_Id);
            modelBuilder.Property(bd => bd.NumberOfChapters).IsRequired();
        }
    }
}
