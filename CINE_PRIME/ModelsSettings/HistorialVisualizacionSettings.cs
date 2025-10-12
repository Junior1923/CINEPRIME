using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class HistorialVisualizacionSettings : IEntityTypeConfiguration<HistorialVisualizacion>
    {
        public void Configure(EntityTypeBuilder<HistorialVisualizacion> builder)
        {
            // Primary Key
            builder.HasKey(h => h.Id);

            // Configuración de propiedades
            builder.Property(h => h.UserId).IsRequired().HasMaxLength(450);
            builder.Property(h => h.TmdbMovieId).IsRequired();

            
            // Índice para optimizar consultas por usuario y fecha de visualización
            builder.HasIndex(h => new { h.UserId, h.FechaVisualizacion });


            // Relación muchos a uno con ApplicationUser
            builder.HasOne(h => h.Usuario)
                   .WithMany()
                   .HasForeignKey(h => h.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }

}
