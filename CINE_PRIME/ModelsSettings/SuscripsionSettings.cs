using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CINE_PRIME.ModelsSettings
{
    public class SuscripsionSettings : IEntityTypeConfiguration<Suscripcion>
    {
        public void Configure(EntityTypeBuilder<Suscripcion> builder)
        {
            // Primary Key
            builder.HasKey(s => s.Id);

            // Configuración para que el GUID se genere automáticamente
            builder.Property(s => s.Id)
                   .HasColumnType("uniqueidentifier")
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWID()");


            // Configuración de propiedades
            builder.Property(s => s.UserId).IsRequired().HasMaxLength(450);
            builder.Property(s => s.PlanId).IsRequired();


            //relacion muchos a uno con ApplicationUser
            builder.HasOne(s => s.Usuario)
                   .WithMany()
                   .HasForeignKey(s => s.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            //relacion muchos a uno con Plan
            builder.HasOne(s => s.Plan)
                   .WithMany()
                   .HasForeignKey(s => s.PlanId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
