using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class ListaPendienteSettings : IEntityTypeConfiguration<ListaPendiente>
    {
        public void Configure(EntityTypeBuilder<ListaPendiente> builder)
        {
            // Primary Key
            builder.HasKey(l => l.Id);

            // Configuración para que el GUID se genere automáticamente
            builder.Property(l => l.Id)
                   .HasColumnType("uniqueidentifier")
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWID()");

            // Configuración de propiedades
            builder.Property(l => l.UserId).IsRequired().HasMaxLength(450);
            builder.Property(l => l.MediaId).IsRequired();
            builder.Property(l => l.MediaType).IsRequired().HasMaxLength(20);


            // Índice único para evitar duplicados de la misma película en la lista pendiente por usuario
            builder.HasIndex(l => new { l.UserId, l.MediaId, l.MediaType }).IsUnique();

            // Relación muchos a uno con ApplicationUser
            builder.HasOne(l => l.Usuario)
                   .WithMany()
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
