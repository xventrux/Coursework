using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Migrations.Migrations
{
    public partial class updateGroupFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_DirectionOfTraining_DirectionOfTrainingId",
                table: "Group");

            migrationBuilder.AlterColumn<Guid>(
                name: "DirectionOfTrainingId",
                table: "Group",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_DirectionOfTraining_DirectionOfTrainingId",
                table: "Group",
                column: "DirectionOfTrainingId",
                principalTable: "DirectionOfTraining",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_DirectionOfTraining_DirectionOfTrainingId",
                table: "Group");

            migrationBuilder.AlterColumn<Guid>(
                name: "DirectionOfTrainingId",
                table: "Group",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_DirectionOfTraining_DirectionOfTrainingId",
                table: "Group",
                column: "DirectionOfTrainingId",
                principalTable: "DirectionOfTraining",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
