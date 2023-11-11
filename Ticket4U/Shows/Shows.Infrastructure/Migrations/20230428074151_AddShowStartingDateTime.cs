using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shows.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShowStartingDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowMessage_Shows_ShowId1",
                table: "ShowMessage");

            migrationBuilder.DropIndex(
                name: "IX_ShowMessage_ShowId1",
                table: "ShowMessage");

            migrationBuilder.DropColumn(
                name: "ShowId1",
                table: "ShowMessage");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingDateTime",
                table: "Shows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartingDateTime",
                table: "Shows");

            migrationBuilder.AddColumn<Guid>(
                name: "ShowId1",
                table: "ShowMessage",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowMessage_ShowId1",
                table: "ShowMessage",
                column: "ShowId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowMessage_Shows_ShowId1",
                table: "ShowMessage",
                column: "ShowId1",
                principalTable: "Shows",
                principalColumn: "Id");
        }
    }
}
