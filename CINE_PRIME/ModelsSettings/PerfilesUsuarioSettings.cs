using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class PerfilesUsuarioSettings : IEntityTypeConfiguration<PerfilesUsuario>
    {
        public void Configure(EntityTypeBuilder<PerfilesUsuario> builder)
        {
            builder.HasKey(p => p.UsuarioId);

            builder.Property(p => p.UsuarioId).HasMaxLength(50); 
            builder.Property(p => p.NombreMostrar).HasMaxLength(100);
            builder.Property(p => p.UrlAvatar).HasMaxLength(500);
            builder.Property(p => p.FechaCreacion).HasColumnType("datetime");

            // Relación uno a uno con Usuario
            builder.HasOne(p => p.Usuario)
                   .WithOne(u => u.PerfilesUsuario)
                   .HasForeignKey<PerfilesUsuario>(p => p.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }

}
