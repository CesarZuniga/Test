using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.api.Models
{
    [Table("cuentas")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public partial class Cuentas
    {
        [Key]
        [Column("cuenta_id", TypeName = "int(11)")]
        public int CuentaId { get; set; }
        [Column("cliente_id", TypeName = "int(11)")]
        public int ClienteId { get; set; }
        [Column("numero_cuenta", TypeName = "int(11)")]
        public int NumeroCuenta { get; set; }
        [Column("saldo_actual")]
        public double SaldoActual { get; set; }
    }
}
