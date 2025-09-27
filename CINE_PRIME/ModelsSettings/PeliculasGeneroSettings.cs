using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class PeliculasGeneroSettings : IEntityTypeConfiguration<PeliculasGenero>
    {
        public void Configure(EntityTypeBuilder<PeliculasGenero> builder)
        {

            builder.HasKey(e => new { e.PeliculaId, e.GeneroId });


            //relacion pelicula-genero
            builder.HasOne(e => e.Pelicula)
                   .WithMany(p => p.PeliculasGeneros)
                   .HasForeignKey(pg => pg.PeliculaId);

            builder.HasOne(e => e.Genero)
                   .WithMany(g => g.PeliculasGeneros)
                   .HasForeignKey(pg => pg.GeneroId);
        }
    }
}
