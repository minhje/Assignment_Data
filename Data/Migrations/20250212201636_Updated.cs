using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_StatusTypes_StatusId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusTypes",
                table: "StatusTypes");

            migrationBuilder.RenameTable(
                name: "StatusTypes",
                newName: "Statuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Statuses_StatusId",
                table: "Projects",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Statuses_StatusId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "StatusTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusTypes",
                table: "StatusTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_StatusTypes_StatusId",
                table: "Projects",
                column: "StatusId",
                principalTable: "StatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
