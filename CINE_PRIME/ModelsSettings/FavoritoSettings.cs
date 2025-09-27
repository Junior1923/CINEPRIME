using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class FavoritoSettings : IEntityTypeConfiguration<Favorito>
    {
        public void Configure(EntityTypeBuilder<Favorito> builder)
        {
            builder.HasKey(e => e.FavoritoId);
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");

            //relación usuario-favortio
            builder.HasOne(e => e.Usuario)
                   .WithMany(e => e.Favoritos)
                   .HasForeignKey(e => e.UsuarioId);

            //relacion favorito-pelicula
            builder.HasOne(e => e.Pelicula)
                   .WithMany(e => e.Favoritos)
                   .HasForeignKey(e => e.PeliculaId);

            //Indice para no colocar la misma pelicula en favoritos dos veces
            builder.HasIndex(e => new { e.UsuarioId, e.PeliculaId })
                   .IsUnique();


        }
    }


}
