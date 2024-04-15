using Microsoft.EntityFrameworkCore;

namespace Tarea1_IF4101_C14644.Models
{
    public class RutaContext:DbContext
    {
        public RutaContext(DbContextOptions<RutaContext> options) : base(options)
        {

        }
        public DbSet<Ruta> rutas { get; set; }
    }
}
