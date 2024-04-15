using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarea1_IF4101_C14644.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarea1_IF4101_C14644.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly HorarioContext _horarioContext;

        public HorarioController(HorarioContext horarioContext)
        {
            _horarioContext = horarioContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horario>>> GetHorarios()
        {
            var horarios = await _horarioContext.horarios.ToListAsync();
            if (horarios == null || horarios.Count == 0)
            {
                return NotFound();
            }
            return horarios;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Horario>> GetHorario(int id)
        {
            var horario = await _horarioContext.horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }
            return horario;
        }

        [HttpPost]
        public async Task<ActionResult<Horario>> PostHorario(Horario horario)
        {
            _horarioContext.horarios.Add(horario);
            await _horarioContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHorario), new { id = horario.IdHorario }, horario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, Horario horario)
        {
            if (id != horario.IdHorario)
            {
                return BadRequest();
            }

            _horarioContext.Entry(horario).State = EntityState.Modified;

            try
            {
                await _horarioContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw; // Lanza la excepción para que se maneje a un nivel superior
                }
            }

            return NoContent();
        }

        private bool HorarioExists(int id)
        {
            return _horarioContext.horarios.Any(e => e.IdHorario == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var horario = await _horarioContext.horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }

            _horarioContext.horarios.Remove(horario);
            await _horarioContext.SaveChangesAsync();
            return Ok();
        }
    }
}
