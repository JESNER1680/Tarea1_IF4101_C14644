using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarea1_IF4101_C14644.Models;

namespace Tarea1_IF4101_C14644.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioContext _dataContext;

        public UsuarioController(UsuarioContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _dataContext.usuarios.ToListAsync();
            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound();
            }
            return usuarios;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _dataContext.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _dataContext.usuarios.Add(usuario);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario}, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            _dataContext.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        private bool UsuarioExists(int id)
        {
            return _dataContext.usuarios.Any(e => e.IdUsuario == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _dataContext.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _dataContext.usuarios.Remove(usuario);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}
