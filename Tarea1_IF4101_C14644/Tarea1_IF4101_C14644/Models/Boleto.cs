namespace Tarea1_IF4101_C14644.Models
{
    public class Boleto
    {
        public int IdBoleto { get; set; }
        public string TipoServicio { get; set; }
        public int IdRuta { get; set; }
        public int PrecioBoleto { get; set; }
        public DateTime FechaTiquete { get; set; }
        public int NumeroAsiento { get; set; }
        public int IdUsuario { get; set; }
    }
}
