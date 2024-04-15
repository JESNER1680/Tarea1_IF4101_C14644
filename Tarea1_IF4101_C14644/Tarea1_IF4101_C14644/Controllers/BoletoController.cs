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
    public class BoletoController : ControllerBase
    {
        private readonly BoletoContext _boletoContext;

        public BoletoController(BoletoContext boletoContext)
        {
            _boletoContext = boletoContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Boleto>>> GetBoletos()
        {
            var boletos = await _boletoContext.boletos.ToListAsync();
            if (boletos == null || boletos.Count == 0)
            {
                return NotFound();
            }
            return boletos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Boleto>> GetBoleto(int id)
        {
            var boleto = await _boletoContext.boletos.FindAsync(id);
            if (boleto == null)
            {
                return NotFound();
            }
            return boleto;
        }

        [HttpPost]
        public async Task<ActionResult<Boleto>> PostBoleto(Boleto boleto)
        {
            _boletoContext.boletos.Add(boleto);
            await _boletoContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBoleto), new { id = boleto.IdBoleto }, boleto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoleto(int id, Boleto boleto)
        {
            if (id != boleto.IdBoleto)
            {
                return BadRequest();
            }

            _boletoContext.Entry(boleto).State = EntityState.Modified;

            try
            {
                await _boletoContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoletoExists(id))
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

        private bool BoletoExists(int id)
        {
            return _boletoContext.boletos.Any(e => e.IdBoleto == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoleto(int id)
        {
            var boleto = await _boletoContext.boletos.FindAsync(id);
            if (boleto == null)
            {
                return NotFound();
            }

            _boletoContext.boletos.Remove(boleto);
            await _boletoContext.SaveChangesAsync();
            return Ok();
        }
    }
}
