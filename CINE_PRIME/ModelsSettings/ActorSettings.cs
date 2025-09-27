using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class ActorSettings : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(e => e.ActorId);
            builder.Property(e => e.Nombre).HasMaxLength(100);
            builder.Property(e => e.Biografia).HasMaxLength(300);
            builder.Property(e => e.UrlFoto).HasMaxLength(500);
            builder.Property(e => e.FechaNacimiento).HasColumnType("date");

            builder.HasMany(e => e.PeliculasActores)
                   .WithOne(e => e.Actor)
                   .HasForeignKey(e => e.ActorId);


        }



    }


}
