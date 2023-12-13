using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPI.Migrations
{
    public partial class CinemaeFilme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessao_Cinema_CinemaId",
                table: "Sessao");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessao_Filmes_FilmeId",
                table: "Sessao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessao",
                table: "Sessao");

            migrationBuilder.DropIndex(
                name: "IX_Sessao_FilmeId",
                table: "Sessao");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sessao");

            migrationBuilder.AlterColumn<int>(
                name: "FilmeId",
                table: "Sessao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CinemaId",
                table: "Sessao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessao",
                table: "Sessao",
                columns: new[] { "FilmeId", "CinemaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Sessao_Cinema_CinemaId",
                table: "Sessao",
                column: "CinemaId",
                principalTable: "Cinema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessao_Filmes_FilmeId",
                table: "Sessao",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessao_Cinema_CinemaId",
                table: "Sessao");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessao_Filmes_FilmeId",
                table: "Sessao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessao",
                table: "Sessao");

            migrationBuilder.AlterColumn<int>(
                name: "CinemaId",
                table: "Sessao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FilmeId",
                table: "Sessao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Sessao",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessao",
                table: "Sessao",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sessao_FilmeId",
                table: "Sessao",
                column: "FilmeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessao_Cinema_CinemaId",
                table: "Sessao",
                column: "CinemaId",
                principalTable: "Cinema",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessao_Filmes_FilmeId",
                table: "Sessao",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id");
        }
    }
}
