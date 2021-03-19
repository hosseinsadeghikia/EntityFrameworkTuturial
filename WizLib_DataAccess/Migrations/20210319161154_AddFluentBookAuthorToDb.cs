using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class AddFluentBookAuthorToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_BookAuthor_Fluent_Authors_Author_Id",
                table: "Fluent_BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_BookAuthor_Fluent_Books_Book_Id",
                table: "Fluent_BookAuthor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fluent_BookAuthor",
                table: "Fluent_BookAuthor");

            migrationBuilder.RenameTable(
                name: "Fluent_BookAuthor",
                newName: "Fluent_BookAuthors");

            migrationBuilder.RenameIndex(
                name: "IX_Fluent_BookAuthor_Book_Id",
                table: "Fluent_BookAuthors",
                newName: "IX_Fluent_BookAuthors_Book_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fluent_BookAuthors",
                table: "Fluent_BookAuthors",
                columns: new[] { "Author_Id", "Book_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_BookAuthors_Fluent_Authors_Author_Id",
                table: "Fluent_BookAuthors",
                column: "Author_Id",
                principalTable: "Fluent_Authors",
                principalColumn: "Author_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_BookAuthors_Fluent_Books_Book_Id",
                table: "Fluent_BookAuthors",
                column: "Book_Id",
                principalTable: "Fluent_Books",
                principalColumn: "Book_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_BookAuthors_Fluent_Authors_Author_Id",
                table: "Fluent_BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_BookAuthors_Fluent_Books_Book_Id",
                table: "Fluent_BookAuthors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fluent_BookAuthors",
                table: "Fluent_BookAuthors");

            migrationBuilder.RenameTable(
                name: "Fluent_BookAuthors",
                newName: "Fluent_BookAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_Fluent_BookAuthors_Book_Id",
                table: "Fluent_BookAuthor",
                newName: "IX_Fluent_BookAuthor_Book_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fluent_BookAuthor",
                table: "Fluent_BookAuthor",
                columns: new[] { "Author_Id", "Book_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_BookAuthor_Fluent_Authors_Author_Id",
                table: "Fluent_BookAuthor",
                column: "Author_Id",
                principalTable: "Fluent_Authors",
                principalColumn: "Author_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_BookAuthor_Fluent_Books_Book_Id",
                table: "Fluent_BookAuthor",
                column: "Book_Id",
                principalTable: "Fluent_Books",
                principalColumn: "Book_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
