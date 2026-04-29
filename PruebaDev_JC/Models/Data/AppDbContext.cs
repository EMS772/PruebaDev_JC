using Microsoft.EntityFrameworkCore;
using PruebaDev_JC.Models.Entities;

namespace PruebaDev_JC.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TasaEdad> TasasEdad { get; set; }
        public DbSet<ConsultaLog> ConsultasLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TasaEdad>()
                .Property(x => x.Tasa)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ConsultaLog>(entity =>
            {
                entity.ToTable("ConsultaLog");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .HasColumnName("IdConsulta");

                entity.Property(x => x.FechaConsulta)
                    .HasColumnType("datetime");

                entity.Property(x => x.IpConsulta)
                    .HasColumnName("IP_de_Consulta")
                    .HasMaxLength(45);

                entity.Property(x => x.Monto)
                    .HasPrecision(18, 2);

                entity.Property(x => x.ValorCuota)
                    .HasPrecision(18, 2);

                entity.Property(x => x.MensajeError)
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<TasaEdad>().HasData(
                new TasaEdad { Id = 1, Edad = 18, Tasa = 1.20m },
                new TasaEdad { Id = 2, Edad = 19, Tasa = 1.18m },
                new TasaEdad { Id = 3, Edad = 20, Tasa = 1.16m },
                new TasaEdad { Id = 4, Edad = 21, Tasa = 1.14m },
                new TasaEdad { Id = 5, Edad = 22, Tasa = 1.12m },
                new TasaEdad { Id = 6, Edad = 23, Tasa = 1.10m },
                new TasaEdad { Id = 7, Edad = 24, Tasa = 1.08m },
                new TasaEdad { Id = 8, Edad = 25, Tasa = 1.05m }
            );
        }
    }
}