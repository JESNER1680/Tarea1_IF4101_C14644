using Microsoft.EntityFrameworkCore;

namespace Tarea1_IF4101_C14644.Models
{
    public class HorarioContext:DbContext
    {
        public HorarioContext(DbContextOptions<HorarioContext> options) : base(options)
        {

        }
        public DbSet<Horario> horarios { get; set; }
    }
}
