using CINE_PRIME.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CINE_PRIME.Data
{
    public class CinePrimeContext : IdentityDbContext<ApplicationUser>
    {

        #region DbSets
        public DbSet<PerfilUsuario> PerfilesUsuarios { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<ListaPendiente> ListasPendientes { get; set; }
        public DbSet<HistorialVisualizacion> HistorialesVisualizacion { get; set; }
        public DbSet<PeliculaLocal> PeliculasLocales { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Suscripcion> Suscripciones { get; set; }
        public DbSet<BitacoraAuditoria> BitacorasAuditoria { get; set; }

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
