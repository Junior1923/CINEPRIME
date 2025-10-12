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

            // Configuración de propiedades
            builder.Property(l => l.UserId).IsRequired().HasMaxLength(450);
            builder.Property(l => l.TmdbMovieId).IsRequired();


            // Índice único para evitar duplicados de la misma película en la lista pendiente por usuario
            builder.HasIndex(l => new { l.UserId, l.TmdbMovieId }).IsUnique();

            // Relación muchos a uno con ApplicationUser
            builder.HasOne(l => l.Usuario)
                   .WithMany()
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }


}
