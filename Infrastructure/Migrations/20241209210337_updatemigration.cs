using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "SupportAgents",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupportAgents_DepartmentId",
                table: "SupportAgents",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportAgents_Departments_DepartmentId",
                table: "SupportAgents",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportAgents_Departments_DepartmentId",
                table: "SupportAgents");

            migrationBuilder.DropIndex(
                name: "IX_SupportAgents_DepartmentId",
                table: "SupportAgents");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "SupportAgents");
        }
    }
}
