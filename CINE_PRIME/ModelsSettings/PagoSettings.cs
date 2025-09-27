using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class PagoSettings : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {
            builder.HasKey(p => p.PagoId);
            builder.Property(p => p.Monto).HasColumnType("decimal(18,2)");

            builder.Property(p => p.Moneda).HasMaxLength(10);
            builder.Property(p => p.Proveedor).HasMaxLength(50);
            builder.Property(p => p.ReferenciaProveedor).HasMaxLength(100);

            builder.Property(p => p.Estado).HasMaxLength(30);

            builder.Property(p => p.FechaPago).HasColumnType("datetime");

            // Relación con Suscripción (1:N)
            builder.HasOne(p => p.Suscripcion)
                   .WithMany(s => s.Pagos) //Suscripcione tiene ICollection<Pago>
                   .HasForeignKey(p => p.SuscripcionId);
        }

    }

}
