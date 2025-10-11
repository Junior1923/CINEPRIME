using CINE_PRIME.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CINE_PRIME.Data
{
    public class CinePrimeContext : IdentityDbContext<ApplicationUser>
    {

        #region DBSETS
        public virtual DbSet<Actor> Actores { get; set; }

        public virtual DbSet<BitacoraAuditorium> BitacoraAuditoria { get; set; }

        public virtual DbSet<Favorito> Favoritos { get; set; }

        public virtual DbSet<Genero> Generos { get; set; }

        public virtual DbSet<ListaPendiente> ListaPendientes { get; set; }

        public virtual DbSet<Pago> Pagos { get; set; }

        public virtual DbSet<Pelicula> Peliculas { get; set; }

        public virtual DbSet<PeliculasGenero> PeliculasGeneros { get; set; }

        public virtual DbSet<PeliculasActore> PeliculasActores { get; set; }

        public virtual DbSet<PerfilesUsuario> PerfilesUsuarios { get; set; }

        public virtual DbSet<Plane> Planes { get; set; }

        public virtual DbSet<Suscripcione> Suscripciones { get; set; }

        public virtual DbSet<Trailer> Trailers { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        #endregion

        public CinePrimeContext(DbContextOptions<CinePrimeContext> options)
            : base(options) 
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llama al método base para asegurarse de que las configuraciones de Identity se apliquen correctamente
            base.OnModelCreating(modelBuilder);

            // Aplica todas las configuraciones del assembly automáticamente (personalizadas)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinePrimeContext).Assembly);


        }





    }


}
