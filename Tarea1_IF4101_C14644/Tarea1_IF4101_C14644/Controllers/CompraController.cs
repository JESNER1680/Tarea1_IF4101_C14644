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
    public class CompraController : ControllerBase
    {
        private readonly CompraContext _compraContext;

        public CompraController(CompraContext compraContext)
        {
            _compraContext = compraContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compra>>> GetCompras()
        {
            var compras = await _compraContext.compras.ToListAsync();
            if (compras == null || compras.Count == 0)
            {
                return NotFound();
            }
            return compras;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Compra>> GetCompra(int id)
        {
            var compra = await _compraContext.compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            return compra;
        }

        [HttpPost]
        public async Task<ActionResult<Compra>> PostCompra(Compra compra)
        {
            _compraContext.compras.Add(compra);
            await _compraContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCompra), new { id = compra.IdCompra }, compra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra(int id, Compra compra)
        {
            if (id != compra.IdCompra)
            {
                return BadRequest();
            }

            _compraContext.Entry(compra).State = EntityState.Modified;

            try
            {
                await _compraContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(id))
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

        private bool CompraExists(int id)
        {
            return _compraContext.compras.Any(e => e.IdCompra == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            var compra = await _compraContext.compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }

            _compraContext.compras.Remove(compra);
            await _compraContext.SaveChangesAsync();
            return Ok();
        }
    }
}
