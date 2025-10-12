using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class BitacoraAuditoriumSettings : IEntityTypeConfiguration<BitacoraAuditoria>
    {
        public void Configure(EntityTypeBuilder<BitacoraAuditoria> builder)
        {

            builder.HasKey(b => b.LogId);

            builder.Property(b => b.UsuarioId).HasMaxLength(50);
            builder.Property(b => b.Accion).HasMaxLength(200);
            builder.Property(b => b.Entidad).HasMaxLength(100);
            builder.Property(b => b.EntidadId).HasMaxLength(50);
            builder.Property(b => b.FechaCreacion).HasColumnType("datetime");
            builder.Property(b => b.Ip).HasMaxLength(50);

            // Relación muchos a uno con Usuario (opcional)
            builder.HasOne(b => b.Usuario)
                   .WithMany(u => u.BitacoraAuditoria)
                   .HasForeignKey(b => b.UsuarioId);


        }

    }


}
