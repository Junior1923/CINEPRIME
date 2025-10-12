using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class FavoritoSettings : IEntityTypeConfiguration<Favorito>
    {
        public void Configure(EntityTypeBuilder<Favorito> builder)
        {
            // Primary Key
            builder.HasKey(f => f.Id);

            // Configuración de propiedades
            builder.Property(f => f.UserId).IsRequired().HasMaxLength(450);
            builder.Property(f => f.TmdbMovieId).IsRequired();


            // Índice único para evitar duplicados de la misma película favorita por usuario
            builder.HasIndex(f => new { f.UserId, f.TmdbMovieId }).IsUnique();

            // Relación muchos a uno con ApplicationUser
            builder.HasOne(f => f.Usuario)
                   .WithMany()
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }


}
