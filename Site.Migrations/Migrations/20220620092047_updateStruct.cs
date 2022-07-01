using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Migrations.Migrations
{
    public partial class updateStruct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Faculty_FacultyId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_StructuralDivision_StructuralDivisionId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_FacultyId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Group");

            migrationBuilder.RenameColumn(
                name: "StructuralDivisionId",
                table: "Group",
                newName: "DirectionOfTrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_Group_StructuralDivisionId",
                table: "Group",
                newName: "IX_Group_DirectionOfTrainingId");

            migrationBuilder.CreateTable(
                name: "DirectionOfTraining",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StructuralDivisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionOfTraining", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectionOfTraining_Faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectionOfTraining_StructuralDivision_StructuralDivisionId",
                        column: x => x.StructuralDivisionId,
                        principalTable: "StructuralDivision",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectionOfTraining_FacultyId",
                table: "DirectionOfTraining",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectionOfTraining_StructuralDivisionId",
                table: "DirectionOfTraining",
                column: "StructuralDivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_DirectionOfTraining_DirectionOfTrainingId",
                table: "Group",
                column: "DirectionOfTrainingId",
                principalTable: "DirectionOfTraining",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_DirectionOfTraining_DirectionOfTrainingId",
                table: "Group");

            migrationBuilder.DropTable(
                name: "DirectionOfTraining");

            migrationBuilder.RenameColumn(
                name: "DirectionOfTrainingId",
                table: "Group",
                newName: "StructuralDivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_Group_DirectionOfTrainingId",
                table: "Group",
                newName: "IX_Group_StructuralDivisionId");

            migrationBuilder.AddColumn<Guid>(
                name: "FacultyId",
                table: "Group",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_FacultyId",
                table: "Group",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Faculty_FacultyId",
                table: "Group",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_StructuralDivision_StructuralDivisionId",
                table: "Group",
                column: "StructuralDivisionId",
                principalTable: "StructuralDivision",
                principalColumn: "Id");
        }
    }
}
