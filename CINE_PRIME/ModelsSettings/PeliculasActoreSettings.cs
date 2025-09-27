using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class PeliculasActoreSettings : IEntityTypeConfiguration<PeliculasActore>
    {
        public void Configure(EntityTypeBuilder<PeliculasActore> builder)
        {

            builder.HasKey(pa => new { pa.PeliculaId, pa.ActorId });

            builder.Property(pa => pa.NombrePersonaje).HasMaxLength(200); 


            // Relación con Pelicula (muchos a uno)
            builder.HasOne(pa => pa.Pelicula)
                   .WithMany(p => p.PeliculasActores)
                   .HasForeignKey(pa => pa.PeliculaId);

            // Relación con Actor (muchos a uno)
            builder.HasOne(pa => pa.Actor)
                   .WithMany(a => a.PeliculasActores)
                   .HasForeignKey(pa => pa.ActorId);
        }
    }
}
