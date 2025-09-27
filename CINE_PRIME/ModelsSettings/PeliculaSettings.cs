using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class PeliculaSettings : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            builder.HasKey(p => p.PeliculaId);

            builder.Property(p => p.Titulo).HasMaxLength(200);
            builder.Property(p => p.Sinopsis).HasMaxLength(2000);
            builder.Property(p => p.Anio).HasColumnType("int");
            builder.Property(p => p.DuracionMin).HasColumnType("int");
            builder.Property(p => p.UrlPoster).HasMaxLength(500);
            builder.Property(p => p.PromedioCalificacion).HasColumnType("decimal(3,2)");
            builder.Property(p => p.EsPremium);


            // Relaciones uno a muchos
            builder.HasMany(p => p.Favoritos)
                .WithOne(f => f.Pelicula)
                .HasForeignKey(f => f.PeliculaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ListaPendientes)
                .WithOne(l => l.Pelicula)
                .HasForeignKey(l => l.PeliculaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PeliculasActores)
                .WithOne(pa => pa.Pelicula)
                .HasForeignKey(pa => pa.PeliculaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Trailers)
                .WithOne(t => t.Pelicula)
                .HasForeignKey(t => t.PeliculaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PeliculasGeneros)
                .WithOne(pg => pg.Pelicula)
                .HasForeignKey(pg => pg.PeliculaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
