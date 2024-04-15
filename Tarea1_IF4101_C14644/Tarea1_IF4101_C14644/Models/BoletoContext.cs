using Microsoft.EntityFrameworkCore;

namespace Tarea1_IF4101_C14644.Models
{
    public class BoletoContext:DbContext
    {
        public BoletoContext(DbContextOptions<BoletoContext> options) : base(options)
        {

        }
        public DbSet<Boleto> boletos { get; set; }
    }
}
