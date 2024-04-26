using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APILabb3.Migrations
{
    /// <inheritdoc />
    public partial class _2tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "interests",
                columns: table => new
                {
                    InterestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FK_PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interests", x => x.InterestId);
                    table.ForeignKey(
                        name: "FK_interests_persons_FK_PersonId",
                        column: x => x.FK_PersonId,
                        principalTable: "persons",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "links",
                columns: table => new
                {
                    LinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FK_InterestsId = table.Column<int>(type: "int", nullable: true),
                    InterestsInterestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_links", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_links_interests_InterestsInterestId",
                        column: x => x.InterestsInterestId,
                        principalTable: "interests",
                        principalColumn: "InterestId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_interests_FK_PersonId",
                table: "interests",
                column: "FK_PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_links_InterestsInterestId",
                table: "links",
                column: "InterestsInterestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "links");

            migrationBuilder.DropTable(
                name: "interests");

            migrationBuilder.DropTable(
                name: "persons");
        }
    }
}
