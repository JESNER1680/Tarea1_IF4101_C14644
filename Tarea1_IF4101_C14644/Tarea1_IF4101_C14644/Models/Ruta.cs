namespace Tarea1_IF4101_C14644.Models
{
    public class Ruta
    {
        public int IdRuta { get; set; }
        public string CodigoRuta { get; set; }
        public int OrigenId { get; set; }
        public int DestinoId { get; set; }
        public DateTime Fecha { get; set; }
        public float Precio { get; set; }
        public string Duracion { get; set; }
        public int Kilometros { get; set; }
        public int Paradas { get; set; }
        public int CantidadAsientos { get; set; }
    }
}
