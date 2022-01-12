namespace test.api.Models
{
    public class MovimientosBO: Movimientos
    {
        public string Nombre { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public int NumeroCuenta { get; set; }
    }
}
