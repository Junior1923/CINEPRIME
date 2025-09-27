using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class PlaneSettings : IEntityTypeConfiguration<Plane>
    {
        public void Configure(EntityTypeBuilder<Plane> builder)
        {
            builder.HasKey(p => p.PlanId);

            builder.Property(p => p.Nombre).HasMaxLength(100);
            builder.Property(p => p.Precio).HasColumnType("decimal(18,2)");
            builder.Property(p => p.MesesPeriodo);
            builder.Property(p => p.MaxListaPendiente);
            builder.Property(p => p.CaracteristicasJson).HasColumnType("nvarchar(max)");
            builder.Property(p => p.Activo);

            // Relación con Suscripciones
            builder.HasMany(p => p.Suscripciones)
                   .WithOne(s => s.Plan)
                   .HasForeignKey(s => s.PlanId);

        }
    }
}
