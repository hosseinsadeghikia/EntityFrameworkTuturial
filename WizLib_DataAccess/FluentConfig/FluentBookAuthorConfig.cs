using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookAuthorConfig : IEntityTypeConfiguration<Fluent_BookAuthor>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookAuthor> modelBuilder)
        {
            //Book Author
            //many to many relation
            modelBuilder.HasKey(ba => new { ba.Author_Id, ba.Book_Id });

            modelBuilder.HasOne(x => x.Fluent_Book)
                .WithMany(x => x.Fluent_BookAuthors)
                .HasForeignKey(x => x.Book_Id);

            modelBuilder.HasOne(x => x.Fluent_Author)
                .WithMany(x => x.Fluent_BookAuthors)
                .HasForeignKey(x => x.Author_Id);
        }
    }
}
