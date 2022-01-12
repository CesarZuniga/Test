using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.api.Models
{
    [Table("tipo_movimiento")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public partial class TipoMovimiento
    {
        [Key]
        [Column("tipo_id", TypeName = "int(11)")]
        public int TipoId { get; set; }
        [Column("descripcion")]
        [StringLength(60)]
        public string Descripcion { get; set; } = null!;
    }
}
