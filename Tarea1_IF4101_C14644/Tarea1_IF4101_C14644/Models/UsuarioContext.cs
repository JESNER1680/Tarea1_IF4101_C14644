using Microsoft.EntityFrameworkCore;

namespace Tarea1_IF4101_C14644.Models
{
    public class UsuarioContext: DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext>options) : base(options)
        {
            
        }
        public DbSet<Usuario> usuarios { get; set; }
    }
}
