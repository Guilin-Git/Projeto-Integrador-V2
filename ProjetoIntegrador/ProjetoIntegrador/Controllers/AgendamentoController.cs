using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Data;
using ProjetoIntegrador.Models;
using System.Security.Claims;

namespace ProjetoIntegrador.Controllers
{
    [Route("agendamento")]
    public class AgendamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgendamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Medico")]
        [HttpGet("consultas-medico")]
        public async Task<IActionResult> VerConsultasMedico()
        {
            var medicoId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(medicoId))
                return Unauthorized();

            var agendamentos = await _context.Agendamentos
                .Include(a => a.Paciente)
                .Where(a => a.IdMedico == medicoId)
                .OrderBy(a => a.DataHora)
                .Select(a => new AgendamentoDto
                {
                    Id = a.Id,
                    DataHora = a.DataHora,
                    Status = a.Status,
                    Paciente = new PacienteDto
                    {
                        Id = a.Paciente.Id,
                        Nome = a.Paciente.Nome
                    }
                })
                .ToListAsync();

            return Ok(agendamentos);
        }
    }

    public class AgendamentoDto
    {
        public string Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; }
        public PacienteDto Paciente { get; set; }
    }

    public class PacienteDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
    }
}
