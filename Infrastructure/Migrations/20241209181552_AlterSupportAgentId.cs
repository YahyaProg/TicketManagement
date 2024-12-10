using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterSupportAgentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Tickets_SupportAgents_SupportAgentId", table: "Tickets");

            migrationBuilder.AlterColumn<long>(name: "SupportAgentId", table: "Tickets", nullable: true, oldClrType: typeof(long));


            migrationBuilder.AddForeignKey(name: "FK_Tickets_SupportAgents_SupportAgentId", table: "Tickets", column: "SupportAgentId", principalTable: "SupportAgents", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Tickets_SupportAgents_SupportAgentId", table: "Tickets");
            migrationBuilder.AlterColumn<long>(name: "SupportAgentId", table: "Tickets", nullable: false, oldClrType: typeof(long), oldNullable: true);

            migrationBuilder.AddForeignKey(name: "FK_Tickets_SupportAgents_SupportAgentId", table: "Tickets", column: "SupportAgentId", principalTable: "SupportAgents", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
        }
    }
}
