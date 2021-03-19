using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentPublisherConfig : IEntityTypeConfiguration<Fluent_Publisher>
    {
        public void Configure(EntityTypeBuilder<Fluent_Publisher> modelBuilder)
        {
            //Publisher
            modelBuilder.HasKey(p => p.Publisher_Id);
            modelBuilder.Property(p => p.Name).IsRequired();
            modelBuilder.Property(p => p.Location).IsRequired();
        }
    }
}
