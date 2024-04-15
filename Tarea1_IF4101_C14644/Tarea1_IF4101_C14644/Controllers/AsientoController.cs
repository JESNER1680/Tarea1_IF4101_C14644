using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarea1_IF4101_C14644.Models;

namespace Tarea1_IF4101_C14644.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsientoController : ControllerBase
    {
        private readonly AsientoContext _asientoContext;

        public AsientoController(AsientoContext asientoContext)
        {
            _asientoContext = asientoContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asiento>>> GetAsientos()
        {
            var asientos = await _asientoContext.asientos.ToListAsync();
            if (asientos == null || asientos.Count == 0)
            {
                return NotFound();
            }
            return asientos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asiento>> GetAsiento(int id)
        {
            var asiento = await _asientoContext.asientos.FindAsync(id);
            if (asiento == null)
            {
                return NotFound();
            }
            return asiento;
        }

        [HttpPost]
        public async Task<ActionResult<Asiento>> PostAsiento(Asiento asiento)
        {
            _asientoContext.asientos.Add(asiento);
            await _asientoContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAsiento), new { id = asiento.IdAsiento }, asiento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsiento(int id, Asiento asiento)
        {
            if (id != asiento.IdAsiento)
            {
                return BadRequest();
            }

            _asientoContext.Entry(asiento).State = EntityState.Modified;

            try
            {
                await _asientoContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientoExists(id))
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

        private bool AsientoExists(int id)
        {
            return _asientoContext.asientos.Any(e => e.IdAsiento == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsiento(int id)
        {
            var asiento = await _asientoContext.asientos.FindAsync(id);
            if (asiento == null)
            {
                return NotFound();
            }

            _asientoContext.asientos.Remove(asiento);
            await _asientoContext.SaveChangesAsync();
            return Ok();
        }
    }
}
