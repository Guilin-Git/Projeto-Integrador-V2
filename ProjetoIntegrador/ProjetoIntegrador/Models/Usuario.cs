using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoIntegrador.Models
{
    [Table("Usuarios")]
    public class Usuario { 
        [Key]
        public int Id { get; set; } // Chave primária
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string? Especialidade { get; set; }// Apenas se for Médico
        public string? CRM { get; set; }  // Apenas se for Médico

        [ForeignKey("Perfil")]
        public int IdPerfil { get; set; }

        // Relacionamento com Perfil
        public Perfil Perfil { get; set; }
    }
}
