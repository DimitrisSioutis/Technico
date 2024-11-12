using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Professionals_ProfessionalVATNumber",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Properties_PropertyId",
                table: "Repairs");

            migrationBuilder.DropTable(
                name: "OwnerProperty");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_ProfessionalVATNumber",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_PropertyId",
                table: "Repairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professionals",
                table: "Professionals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owners",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "ProfessionalVAT",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "ProfessionalVATNumber",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "RepairType",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Professionals");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Repairs",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CurrentStatus",
                table: "Repairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyIDNumber",
                table: "Repairs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Repairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyIDNumber",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Professionals",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Professionals",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Professionals",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Professionals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Owners",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Owners",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Owners",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Owners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professionals",
                table: "Professionals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owners",
                table: "Owners",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_PropertyIDNumber",
                table: "Repairs",
                column: "PropertyIDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerId",
                table: "Properties",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_Email",
                table: "Professionals",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_VATNumber",
                table: "Professionals",
                column: "VATNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_Email",
                table: "Owners",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_VATNumber",
                table: "Owners",
                column: "VATNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Owners_OwnerId",
                table: "Properties",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Properties_PropertyIDNumber",
                table: "Repairs",
                column: "PropertyIDNumber",
                principalTable: "Properties",
                principalColumn: "PropertyIDNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Owners_OwnerId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Properties_PropertyIDNumber",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_PropertyIDNumber",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Properties_OwnerId",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professionals",
                table: "Professionals");

            migrationBuilder.DropIndex(
                name: "IX_Professionals_Email",
                table: "Professionals");

            migrationBuilder.DropIndex(
                name: "IX_Professionals_VATNumber",
                table: "Professionals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owners",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_Email",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_VATNumber",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "PropertyIDNumber",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Professionals");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Owners");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Repairs",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalVAT",
                table: "Repairs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalVATNumber",
                table: "Repairs",
                type: "nvarchar(9)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RepairType",
                table: "Repairs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Repairs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyIDNumber",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "Professionals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "PhoneNumber",
                table: "Professionals",
                type: "bigint",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Professionals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Professionals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "PhoneNumber",
                table: "Owners",
                type: "bigint",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professionals",
                table: "Professionals",
                column: "VATNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owners",
                table: "Owners",
                column: "VATNumber");

            migrationBuilder.CreateTable(
                name: "OwnerProperty",
                columns: table => new
                {
                    OwnersVATNumber = table.Column<string>(type: "nvarchar(9)", nullable: false),
                    PropertiesPropertyIDNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerProperty", x => new { x.OwnersVATNumber, x.PropertiesPropertyIDNumber });
                    table.ForeignKey(
                        name: "FK_OwnerProperty_Owners_OwnersVATNumber",
                        column: x => x.OwnersVATNumber,
                        principalTable: "Owners",
                        principalColumn: "VATNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerProperty_Properties_PropertiesPropertyIDNumber",
                        column: x => x.PropertiesPropertyIDNumber,
                        principalTable: "Properties",
                        principalColumn: "PropertyIDNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_ProfessionalVATNumber",
                table: "Repairs",
                column: "ProfessionalVATNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_PropertyId",
                table: "Repairs",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerProperty_PropertiesPropertyIDNumber",
                table: "OwnerProperty",
                column: "PropertiesPropertyIDNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Professionals_ProfessionalVATNumber",
                table: "Repairs",
                column: "ProfessionalVATNumber",
                principalTable: "Professionals",
                principalColumn: "VATNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Properties_PropertyId",
                table: "Repairs",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyIDNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
