﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoIntegrador.Models
{
    [Table("Agendamentos")]
    public class Agendamento
    {
        [Key]
        public int Id { get; set; } // Chave primária

        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }    // Relacionamento com o Paciente

        [ForeignKey("Medico")]
        public int IdMedico { get; set; }      // Relacionamento com o Médico

        public DateTime DataHora { get; set; }  // Data e hora do agendamento da consulta
        public string Status { get; set; }     // Ex: Agendado, Confirmado, Cancelado

        // Relacionamentos
        public Usuario Paciente { get; set; }
        public Usuario Medico { get; set; }
    }
}
