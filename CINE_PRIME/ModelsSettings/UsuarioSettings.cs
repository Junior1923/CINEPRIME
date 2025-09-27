using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class UsuarioSettings : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.UsuarioId);

            builder.Property(u => u.UsuarioId).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Correo).HasMaxLength(100).IsRequired();
            builder.Property(u => u.ContrasenaHash).HasMaxLength(200).IsRequired();
            builder.Property(u => u.FechaRegistro).HasColumnType("datetime");

            // Relaciones uno a muchos
            builder.HasMany(u => u.BitacoraAuditoria)
                .WithOne(b => b.Usuario)
                .HasForeignKey(b => b.UsuarioId);

            builder.HasMany(u => u.Favoritos)
                .WithOne(f => f.Usuario)
                .HasForeignKey(f => f.UsuarioId);

            builder.HasMany(u => u.ListaPendientes)
                .WithOne(l => l.Usuario)
                .HasForeignKey(l => l.UsuarioId);

            builder.HasMany(u => u.Suscripciones)
                .WithOne(s => s.Usuario)
                .HasForeignKey(s => s.UsuarioId);

            // Relación uno a uno opcional
            builder.HasOne(u => u.PerfilesUsuario)
                .WithOne(p => p.Usuario)
                .HasForeignKey<PerfilesUsuario>(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }

}
