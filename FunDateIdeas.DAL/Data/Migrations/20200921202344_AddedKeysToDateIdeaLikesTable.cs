using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FunDateIdeas.DAL.Data.Migrations
{
    public partial class AddedKeysToDateIdeaLikesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DateIdeaLikes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    DateIdeaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateIdeaLikes", x => new { x.DateIdeaId, x.UserId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DateIdeaLikes");
        }
    }
}
