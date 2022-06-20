using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoerberExercise.Data.Migrations
{
    public partial class AddedParentRelationToMachine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Machines_ParentId",
                table: "Machines",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Machines_ParentId",
                table: "Machines",
                column: "ParentId",
                principalTable: "Machines",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Machines_ParentId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_ParentId",
                table: "Machines");
        }
    }
}
