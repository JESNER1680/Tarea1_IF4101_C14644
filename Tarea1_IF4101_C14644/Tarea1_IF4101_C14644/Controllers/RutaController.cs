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
    public class RutaController : ControllerBase
    {
        private readonly RutaContext _rutaContext;

        public RutaController(RutaContext rutaContext)
        {
            _rutaContext = rutaContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ruta>>> GetRutas()
        {
            var rutas = await _rutaContext.rutas.ToListAsync();
            if (rutas == null || rutas.Count == 0)
            {
                return NotFound();
            }
            return rutas;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ruta>> GetRuta(int id)
        {
            var ruta = await _rutaContext.rutas.FindAsync(id);
            if (ruta == null)
            {
                return NotFound();
            }
            return ruta;
        }

        [HttpPost]
        public async Task<ActionResult<Ruta>> PostRuta(Ruta ruta)
        {
            _rutaContext.rutas.Add(ruta);
            await _rutaContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRuta), new { id = ruta.IdRuta }, ruta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRuta(int id, Ruta ruta)
        {
            if (id != ruta.IdRuta)
            {
                return BadRequest();
            }

            _rutaContext.Entry(ruta).State = EntityState.Modified;

            try
            {
                await _rutaContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RutaExists(id))
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

        private bool RutaExists(int id)
        {
            return _rutaContext.rutas.Any(e => e.IdRuta == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuta(int id)
        {
            var ruta = await _rutaContext.rutas.FindAsync(id);
            if (ruta == null)
            {
                return NotFound();
            }

            _rutaContext.rutas.Remove(ruta);
            await _rutaContext.SaveChangesAsync();
            return Ok();
        }
    }
}
