using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AwesomeDevEvents.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SetupDevEventSpeakerAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DevEventsSpeakers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "TaskDescription",
                table: "DevEventsSpeakers",
                newName: "talk_description");

            migrationBuilder.RenameColumn(
                name: "TalkTitle",
                table: "DevEventsSpeakers",
                newName: "task_title");

            migrationBuilder.RenameColumn(
                name: "LinkedInProfile",
                table: "DevEventsSpeakers",
                newName: "linked_id_profile");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DevEventsSpeakers",
                newName: "dev_event_speaker_id");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "DevEventsSpeakers",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "talk_description",
                table: "DevEventsSpeakers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "task_title",
                table: "DevEventsSpeakers",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "linked_id_profile",
                table: "DevEventsSpeakers",
                type: "varchar(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "DevEventsSpeakers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "task_title",
                table: "DevEventsSpeakers",
                newName: "TalkTitle");

            migrationBuilder.RenameColumn(
                name: "talk_description",
                table: "DevEventsSpeakers",
                newName: "TaskDescription");

            migrationBuilder.RenameColumn(
                name: "linked_id_profile",
                table: "DevEventsSpeakers",
                newName: "LinkedInProfile");

            migrationBuilder.RenameColumn(
                name: "dev_event_speaker_id",
                table: "DevEventsSpeakers",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DevEventsSpeakers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "TalkTitle",
                table: "DevEventsSpeakers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "TaskDescription",
                table: "DevEventsSpeakers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LinkedInProfile",
                table: "DevEventsSpeakers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldMaxLength: 70,
                oldNullable: true);
        }
    }
}
