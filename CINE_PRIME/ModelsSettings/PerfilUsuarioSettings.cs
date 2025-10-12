using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class PerfilUsuarioSettings : IEntityTypeConfiguration<PerfilUsuario>
    {
        public void Configure(EntityTypeBuilder<PerfilUsuario> builder)
        {
            //primary key
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId)
                   .IsRequired()
                   .HasMaxLength(450); // coincide con AspNetUsers.Id default


            // Configuración para que el GUID se genere automáticamente
            builder.Property(p => p.Id)
                   .HasColumnType("uniqueidentifier")
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWID()");

            // Configuración de propiedades
            builder.Property(p => p.Nombre).HasMaxLength(50);
            builder.Property(p => p.Apellido).HasMaxLength(100);
            builder.Property(p => p.FotoPerfil).HasMaxLength(500);


            // Índice único en UserId para asegurar que cada usuario tenga un solo perfil
            builder.HasIndex(p => p.UserId).IsUnique(); // uno-a-uno aproximado

            // Relación uno a uno con ApplicationUser
            builder.HasOne(p => p.Usuario)
                   .WithOne()
                   .HasForeignKey<PerfilUsuario>(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }

}
