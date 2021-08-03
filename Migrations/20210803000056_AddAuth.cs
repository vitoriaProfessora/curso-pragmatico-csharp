using Microsoft.EntityFrameworkCore.Migrations;

namespace curso_pragmatico_csharp.Migrations
{
    public partial class AddAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Filmes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ano",
                table: "Filmes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Filmes",
                type: "TEXT",
                nullable: true);
        }
    }
}
