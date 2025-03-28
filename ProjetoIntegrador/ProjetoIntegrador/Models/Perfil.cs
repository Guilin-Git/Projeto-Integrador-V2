using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoIntegrador.Models
{
    [Table("Perfis")]
    public class Perfil
    {
        [Key]
        public int Id { get; set; } // Chave primária
        public string Tipo { get; set; }  // Ex: Paciente, Medico, Recepcionista, Admin
    }
}
