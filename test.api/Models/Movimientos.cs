using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.api.Models
{
    [Table("movimientos")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public partial class Movimientos
    {
        [Key]
        [Column("movimiento_id", TypeName = "int(11)")]
        public int MovimientoId { get; set; }
        [Column("cuenta_id", TypeName = "int(11)")]
        public int CuentaId { get; set; }
        [Column("tipo_id", TypeName = "int(11)")]
        public int TipoId { get; set; }
        [Column("monto")]
        public double Monto { get; set; }
    }
}
