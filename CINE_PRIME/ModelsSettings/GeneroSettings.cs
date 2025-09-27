using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class GeneroSettings : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.HasKey(e => e.GeneroId);
            builder.Property(e => e.Nombre).HasMaxLength(30);

            //relacion genero-peliculagenero
            builder.HasMany(e => e.PeliculasGeneros)
                     .WithOne(e => e.Genero)
                     .HasForeignKey(e => e.GeneroId);

        }
    }
}
