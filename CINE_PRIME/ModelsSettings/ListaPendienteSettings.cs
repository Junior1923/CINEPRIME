using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class ListaPendienteSettings : IEntityTypeConfiguration<ListaPendiente>
    {
        public void Configure(EntityTypeBuilder<ListaPendiente> builder)
        {

            builder.HasKey(lp => lp.ListaId);
            builder.Property(lp => lp.FechaCreacion).HasColumnType("datetime");

            // Relación con Usuario
            builder.HasOne(lp => lp.Usuario)
                   .WithMany(u => u.ListaPendientes) //Usuario tiene ICollection<ListaPendiente>
                   .HasForeignKey(lp => lp.UsuarioId);

            // Relación con Pelicula
            builder.HasOne(lp => lp.Pelicula)
                   .WithMany(p => p.ListaPendientes)//Pelicula tiene ICollection<ListaPendiente>
                   .HasForeignKey(lp => lp.PeliculaId);
        }

    }


}
