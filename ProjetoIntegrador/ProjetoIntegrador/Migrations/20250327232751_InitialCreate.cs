using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoIntegrador.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especialidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CRM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPerfil = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Perfis_IdPerfil",
                        column: x => x.IdPerfil,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agendamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    IdMedico = table.Column<int>(type: "int", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Usuarios_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);  // Mudado para NoAction
                    table.ForeignKey(
                        name: "FK_Agendamentos_Usuarios_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);  // Mudado para NoAction
                });

            migrationBuilder.CreateTable(
                name: "Relatorios",
                columns: table => new
                {
                    IdRelatorio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMedico = table.Column<int>(type: "int", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    DataGeracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Conteudo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatorios", x => x.IdRelatorio);
                    table.ForeignKey(
                        name: "FK_Relatorios_Usuarios_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);  // Mudado para NoAction
                    table.ForeignKey(
                        name: "FK_Relatorios_Usuarios_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);  // Mudado para NoAction
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAgendamento = table.Column<int>(type: "int", nullable: false),
                    DataConsulta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusConsulta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnostico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prescricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    MedicoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Agendamentos_IdAgendamento",
                        column: x => x.IdAgendamento,
                        principalTable: "Agendamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);  // Mudado para NoAction
                    table.ForeignKey(
                        name: "FK_Consultas_Usuarios_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);  // Mudado para NoAction
                    table.ForeignKey(
                        name: "FK_Consultas_Usuarios_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);  // Mudado para NoAction
                });

            migrationBuilder.CreateTable(
                name: "Exames",
                columns: table => new
                {
                    IdExame = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdConsulta = table.Column<int>(type: "int", nullable: false),
                    TipoExame = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRealizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArquivoExame = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusExame = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.IdExame);
                    table.ForeignKey(
                        name: "FK_Exames_Consultas_IdConsulta",
                        column: x => x.IdConsulta,
                        principalTable: "Consultas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);  // Mudado para NoAction
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_IdMedico",
                table: "Agendamentos",
                column: "IdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_IdPaciente",
                table: "Agendamentos",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_IdAgendamento",
                table: "Consultas",
                column: "IdAgendamento");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_MedicoId",
                table: "Consultas",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PacienteId",
                table: "Consultas",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_IdConsulta",
                table: "Exames",
                column: "IdConsulta");

            migrationBuilder.CreateIndex(
                name: "IX_Relatorios_IdMedico",
                table: "Relatorios",
                column: "IdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_Relatorios_IdPaciente",
                table: "Relatorios",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdPerfil",
                table: "Usuarios",
                column: "IdPerfil");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropTable(
                name: "Relatorios");

            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Agendamentos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Perfis");
        }
    }
}
