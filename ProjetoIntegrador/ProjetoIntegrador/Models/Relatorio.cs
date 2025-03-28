using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoIntegrador.Models
{
    [Table("Relatorios")]
    public class Relatorio
    {
        [Key]
        public int IdRelatorio { get; set; } // Chave primária

        [ForeignKey("Medico")]
        public int IdMedico { get; set; }    // Relacionamento com Médico

        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }  // Relacionamento com Paciente

        public DateTime DataGeracao { get; set; }
        public string Conteudo { get; set; }

        // Relacionamentos
        public Usuario Medico { get; set; }
        public Usuario Paciente { get; set; }
    }
}
