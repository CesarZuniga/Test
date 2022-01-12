using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.api.Models
{
    [Table("clientes")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public partial class Clientes
    {
        [Key]
        [Column("cliente_id", TypeName = "int(11)")]
        public int ClienteId { get; set; }
        [Column("nombre")]
        [StringLength(60)]
        public string Nombre { get; set; } = null!;
        [Column("numero_identificacion")]
        [StringLength(20)]
        public string NumeroIdentificacion { get; set; } = null!;
    }
}
