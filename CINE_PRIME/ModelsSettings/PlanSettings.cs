using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class PlanSettings : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            // Primary Key
            builder.HasKey(p => p.Id);

            // Configuración de propiedades
            builder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(p => p.PrecioMensual).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Descripcion).HasMaxLength(500);

        }
    }
}
