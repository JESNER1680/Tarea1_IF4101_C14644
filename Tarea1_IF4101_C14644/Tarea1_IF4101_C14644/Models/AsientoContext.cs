using Microsoft.EntityFrameworkCore;

namespace Tarea1_IF4101_C14644.Models
{
    public class AsientoContext: DbContext
    {
        public AsientoContext(DbContextOptions<AsientoContext> options) : base(options)
        {

        }
        public DbSet<Asiento> asientos { get; set; }
    }
}
