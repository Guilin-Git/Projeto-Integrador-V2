namespace ProjetoIntegrador.Data
{
    using Microsoft.EntityFrameworkCore;
    using ProjetoIntegrador.Models;  // Ajuste para os modelos do seu projeto
    using System.Xml.Linq;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
    }
}