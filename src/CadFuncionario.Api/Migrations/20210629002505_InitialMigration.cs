using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CadFuncionario.Api.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "profissao",
                columns: table => new
                {
                    ProfissaoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Descricao = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalarioBase = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profissao", x => x.ProfissaoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "stepprofissao",
                columns: table => new
                {
                    StepProfissaoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProfissaoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PercentualAumento = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stepprofissao", x => x.StepProfissaoId);
                    table.ForeignKey(
                        name: "FK_stepprofissao_profissao_ProfissaoId",
                        column: x => x.ProfissaoId,
                        principalTable: "profissao",
                        principalColumn: "ProfissaoId",
                        onDelete: ReferentialAction.NoAction);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "funcionario",
                columns: table => new
                {
                    FuncionarioId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StepProfissaoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Cpf = table.Column<string>(type: "varchar(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rg = table.Column<string>(type: "varchar(10)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ctps = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataNascimento = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionario", x => x.FuncionarioId);
                    table.ForeignKey(
                        name: "FK_funcionario_stepprofissao_StepProfissaoId",
                        column: x => x.StepProfissaoId,
                        principalTable: "stepprofissao",
                        principalColumn: "StepProfissaoId",
                        onDelete: ReferentialAction.NoAction);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_funcionario_StepProfissaoId",
                table: "funcionario",
                column: "StepProfissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_stepprofissao_ProfissaoId",
                table: "stepprofissao",
                column: "ProfissaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "funcionario");

            migrationBuilder.DropTable(
                name: "stepprofissao");

            migrationBuilder.DropTable(
                name: "profissao");
        }
    }
}
