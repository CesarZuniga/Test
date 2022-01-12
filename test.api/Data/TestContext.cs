using Microsoft.EntityFrameworkCore;
using test.api.Models;

namespace test.api.Data
{
    public class TestContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TestContext() { }
        public TestContext(DbContextOptions<TestContext> options)
          : base(options)
        {

        }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Cuentas> Cuentas { get; set; } 
        public virtual DbSet<Movimientos> Movimientos { get; set; }
        public virtual DbSet<TipoMovimiento> TipoMovimientos { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.Property(e => e.ClienteId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Cuentas>(entity =>
            {
                entity.HasKey(e => e.CuentaId)
                    .HasName("PRIMARY");

                entity.Property(e => e.CuentaId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Movimientos>(entity =>
            {
                entity.Property(e => e.MovimientoId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TipoMovimiento>(entity =>
            {
                entity.HasKey(e => e.TipoId)
                    .HasName("PRIMARY");

                entity.Property(e => e.TipoId).ValueGeneratedNever();
            });

        }

    }
}
