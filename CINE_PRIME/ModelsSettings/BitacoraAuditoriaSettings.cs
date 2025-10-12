using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class BitacoraAuditoriaSettings : IEntityTypeConfiguration<BitacoraAuditoria>
    {
        public void Configure(EntityTypeBuilder<BitacoraAuditoria> builder)
        {
            // Primary Key
            builder.HasKey(b => b.Id);

            // Configuración de propiedades
            builder.Property(b => b.UserId).HasMaxLength(450);
            builder.Property(b => b.Accion).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Detalle).HasMaxLength(1000);

            // Relacion muchos a uno con ApplicationUser
            builder.HasOne(b => b.Usuario)
                   .WithMany()
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.SetNull);


        }

    }


}
