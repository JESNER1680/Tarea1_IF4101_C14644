using Microsoft.EntityFrameworkCore;

namespace Tarea1_IF4101_C14644.Models
{
    public class CompraContext:DbContext
    {
        public CompraContext(DbContextOptions<CompraContext> options) : base(options)
        {

        }
        public DbSet<Compra> compras { get; set; }
    }
}
