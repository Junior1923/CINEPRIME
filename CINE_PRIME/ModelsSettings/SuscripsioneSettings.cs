using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class SuscripsioneSettings : IEntityTypeConfiguration<Suscripcione>
    {
        public void Configure(EntityTypeBuilder<Suscripcione> builder)
        {
            builder.HasKey(s => s.SuscripcionId);
            
            builder.Property(s => s.UsuarioId);
            builder.Property(s => s.FechaInicio).HasColumnType("datetime");

            builder.Property(s => s.FechaFin).HasColumnType("datetime");

            builder.Property(s => s.Estado).HasMaxLength(30);

            // Relación con Usuario
            builder.HasOne(s => s.Usuario)
                   .WithMany(u => u.Suscripciones) // ICollection<Suscripcione> en Usuario
                   .HasForeignKey(s => s.UsuarioId);

            // Relación con Plan
            builder.HasOne(s => s.Plan)
                   .WithMany(p => p.Suscripciones) //ICollection<Suscripcione> en Plane
                   .HasForeignKey(s => s.PlanId);

            // Relación con Pagos
            builder.HasMany(s => s.Pagos)
                   .WithOne(p => p.Suscripcion)
                   .HasForeignKey(p => p.SuscripcionId);

        }
    }
}
