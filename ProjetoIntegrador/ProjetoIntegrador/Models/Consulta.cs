using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoIntegrador.Models
{
    [Table("Consultas")]
    public class Consulta
    {
        [Key]
        public int Id { get; set; } // Chave primária única

        [ForeignKey("Agendamento")]
        public int IdAgendamento { get; set; } // Relacionamento com o Agendamento

        public DateTime DataConsulta { get; set; } // Data e Hora da consulta real
        public string StatusConsulta { get; set; } // Status da consulta (Realizada, Cancelada, etc.)
        public string Diagnostico { get; set; }   // Diagnóstico (após a consulta)
        public string Prescricao { get; set; }    // Prescrição médica (após a consulta)
        public DateTime DataCriacao { get; set; } // Data em que o agendamento foi criado
        public string Observacoes { get; set; }   // Observações adicionais sobre a consulta

        // Propriedades de navegação (relacionamento com Usuário)
        public Usuario Paciente { get; set; } // Relacionamento com o paciente
        public Usuario Medico { get; set; }   // Relacionamento com o médico
        public Agendamento Agendamento { get; set; } // Relacionamento com o agendamento
    }
}
