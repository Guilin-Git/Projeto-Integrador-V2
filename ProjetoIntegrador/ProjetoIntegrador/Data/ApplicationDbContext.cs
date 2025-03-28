﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Models;  // Ajuste para os modelos do seu projeto
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Xml.Linq;



namespace ProjetoIntegrador.Data
{

    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Corrige o problema definindo DeleteBehavior.Restrict
            builder.Entity<Agendamento>()
                .HasOne(a => a.Paciente)
                .WithMany()
                .HasForeignKey(a => a.IdPaciente)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Agendamento>()
                .HasOne(a => a.Medico)
                .WithMany()
                .HasForeignKey(a => a.IdMedico)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Consulta>()
                .HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.IdPaciente)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Consulta>()
                .HasOne(c => c.Medico)
                .WithMany()
                .HasForeignKey(c => c.IdMedico)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Relatorio>()
                .HasOne(r => r.Paciente)
                .WithMany()
                .HasForeignKey(r => r.IdPaciente)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Relatorio>()
                .HasOne(r => r.Medico)
                .WithMany()
                .HasForeignKey(r => r.IdMedico)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}