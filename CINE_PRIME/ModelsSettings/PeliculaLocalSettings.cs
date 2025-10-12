using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CINE_PRIME.Models;

namespace CINE_PRIME.ModelsSettings
{
    public class PeliculaLocalSettings : IEntityTypeConfiguration<PeliculaLocal>
    {
        public void Configure(EntityTypeBuilder<PeliculaLocal> builder)
        {
            // Primary Key
            builder.HasKey(p => p.Id);

            
            builder.Property(p => p.TmdbMovieId).IsRequired();
            
            // Configuración de propiedades
            builder.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Descripcion).HasMaxLength(1000);
            builder.Property(p => p.ImagenUrl).HasMaxLength(500);



            // Índice para optimizar búsquedas por TmdbMovieId
            builder.HasIndex(p => p.TmdbMovieId).IsUnique();
        }
    }

}
