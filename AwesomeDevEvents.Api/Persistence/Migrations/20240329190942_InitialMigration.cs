using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AwesomeDevEvents.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DevEvents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    starts_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevEvents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DevEventsSpeakers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TalkTitle = table.Column<string>(type: "text", nullable: true),
                    TaskDescription = table.Column<string>(type: "text", nullable: true),
                    LinkedInProfile = table.Column<string>(type: "text", nullable: true),
                    DevEventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevEventsSpeakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevEventsSpeakers_DevEvents_DevEventId",
                        column: x => x.DevEventId,
                        principalTable: "DevEvents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevEventsSpeakers_DevEventId",
                table: "DevEventsSpeakers",
                column: "DevEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevEventsSpeakers");

            migrationBuilder.DropTable(
                name: "DevEvents");
        }
    }
}
