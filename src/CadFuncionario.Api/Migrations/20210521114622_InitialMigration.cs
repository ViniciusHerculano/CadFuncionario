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
            migrationBuilder.CreateTable(
                name: "profissao",
                columns: table => new
                {
                    ProfissaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(80)", nullable: false),
                    SalarioBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profissao", x => x.ProfissaoId);
                });

            migrationBuilder.CreateTable(
                name: "stepprofissao",
                columns: table => new
                {
                    StepProfissaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfissaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PercentualAumento = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stepprofissao", x => x.StepProfissaoId);
                    table.ForeignKey(
                        name: "FK_stepprofissao_profissao_ProfissaoId",
                        column: x => x.ProfissaoId,
                        principalTable: "profissao",
                        principalColumn: "ProfissaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "funcionario",
                columns: table => new
                {
                    FuncionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StepProfissaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(11)", nullable: false),
                    Rg = table.Column<string>(type: "varchar(10)", nullable: true),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Ctps = table.Column<string>(type: "varchar(20)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionario", x => x.FuncionarioId);
                    table.ForeignKey(
                        name: "FK_funcionario_stepprofissao_StepProfissaoId",
                        column: x => x.StepProfissaoId,
                        principalTable: "stepprofissao",
                        principalColumn: "StepProfissaoId",
                        onDelete: ReferentialAction.Cascade);
                });

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
