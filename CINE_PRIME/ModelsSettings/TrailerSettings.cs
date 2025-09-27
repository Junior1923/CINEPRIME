using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class TrailerSettings : IEntityTypeConfiguration<Trailer>
    {
        public void Configure(EntityTypeBuilder<Trailer> builder)
        {

            builder.HasKey(t => t.TrailerId);

            builder.Property(t => t.Titulo).HasMaxLength(200);
            builder.Property(t => t.Descripcion).HasMaxLength(500); 
            builder.Property(t => t.UrlVideo).HasMaxLength(400);
            builder.Property(t => t.DuracionSeg);
            builder.Property(t => t.FechaPublicacion).HasColumnType("datetime");    
            builder.Property(t => t.Tipo).HasMaxLength(50);

            // Relaciones
            builder.HasOne(t => t.Pelicula)
                   .WithMany(p => p.Trailers)
                   .HasForeignKey(t => t.PeliculaId);
        }
    }
}
