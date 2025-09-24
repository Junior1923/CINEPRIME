using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CINE_PRIME.Models;

public partial class cineprimeContext : DbContext
{
    public cineprimeContext()
    {
    }

    public cineprimeContext(DbContextOptions<cineprimeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actore> Actores { get; set; }

    public virtual DbSet<BitacoraAuditorium> BitacoraAuditoria { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<ListaPendiente> ListaPendientes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<PeliculasActore> PeliculasActores { get; set; }

    public virtual DbSet<PerfilesUsuario> PerfilesUsuarios { get; set; }

    public virtual DbSet<Plane> Planes { get; set; }

    public virtual DbSet<Suscripcione> Suscripciones { get; set; }

    public virtual DbSet<Trailer> Trailers { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI-KATANAGF66\\SQLEXPRESS;Database=cineprime;Trusted_Connection=True;TrustServerCertificate=True;");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actore>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actores__57B3EA4B2E259E14");

            entity.Property(e => e.Nombre).HasMaxLength(120);
            entity.Property(e => e.UrlFoto).HasMaxLength(400);
        });

        modelBuilder.Entity<BitacoraAuditorium>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Bitacora__5E5486483262FBA5");

            entity.Property(e => e.Accion).HasMaxLength(120);
            entity.Property(e => e.Entidad).HasMaxLength(120);
            entity.Property(e => e.EntidadId).HasMaxLength(120);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Ip).HasMaxLength(45);
            entity.Property(e => e.UsuarioId).HasMaxLength(450);

            entity.HasOne(d => d.Usuario).WithMany(p => p.BitacoraAuditoria)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__BitacoraA__Usuar__75A278F5");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasKey(e => e.FavoritoId).HasName("PK__Favorito__CFF711E5B7F395C3");

            entity.HasIndex(e => new { e.UsuarioId, e.PeliculaId }, "UQ_Favoritos").IsUnique();

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Pelicula).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.PeliculaId)
                .HasConstraintName("FK__Favoritos__Pelic__71D1E811");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Favoritos__Usuar__70DDC3D8");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroId).HasName("PK__Generos__A99D02484D8164B3");

            entity.HasIndex(e => e.Nombre, "UQ__Generos__75E3EFCF76F17878").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(80);
        });

        modelBuilder.Entity<ListaPendiente>(entity =>
        {
            entity.HasKey(e => e.ListaId).HasName("PK__ListaPen__2B0A741F62CB3E83");

            entity.ToTable("ListaPendiente");

            entity.HasIndex(e => new { e.UsuarioId, e.PeliculaId }, "UQ_ListaPendiente").IsUnique();

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Pelicula).WithMany(p => p.ListaPendientes)
                .HasForeignKey(d => d.PeliculaId)
                .HasConstraintName("FK__ListaPend__Pelic__6C190EBB");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ListaPendientes)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__ListaPend__Usuar__6B24EA82");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pagos__F00B6138715442F5");

            entity.Property(e => e.Estado).HasMaxLength(30);
            entity.Property(e => e.Moneda).HasMaxLength(10);
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Proveedor).HasMaxLength(30);
            entity.Property(e => e.ReferenciaProveedor).HasMaxLength(120);

            entity.HasOne(d => d.Suscripcion).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.SuscripcionId)
                .HasConstraintName("FK__Pagos__Suscripci__5812160E");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.PeliculaId).HasName("PK__Pelicula__5AC6FCCC66ED5D3F");

            entity.Property(e => e.PromedioCalificacion).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Titulo).HasMaxLength(200);
            entity.Property(e => e.UrlPoster).HasMaxLength(400);

            entity.HasMany(d => d.Generos).WithMany(p => p.Peliculas)
                .UsingEntity<Dictionary<string, object>>(
                    "PeliculasGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("GeneroId")
                        .HasConstraintName("FK__Peliculas__Gener__619B8048"),
                    l => l.HasOne<Pelicula>().WithMany()
                        .HasForeignKey("PeliculaId")
                        .HasConstraintName("FK__Peliculas__Pelic__60A75C0F"),
                    j =>
                    {
                        j.HasKey("PeliculaId", "GeneroId");
                        j.ToTable("PeliculasGeneros");
                    });
        });

        modelBuilder.Entity<PeliculasActore>(entity =>
        {
            entity.HasKey(e => new { e.PeliculaId, e.ActorId });

            entity.Property(e => e.NombrePersonaje).HasMaxLength(120);

            entity.HasOne(d => d.Actor).WithMany(p => p.PeliculasActores)
                .HasForeignKey(d => d.ActorId)
                .HasConstraintName("FK__Peliculas__Actor__6754599E");

            entity.HasOne(d => d.Pelicula).WithMany(p => p.PeliculasActores)
                .HasForeignKey(d => d.PeliculaId)
                .HasConstraintName("FK__Peliculas__Pelic__66603565");
        });

        modelBuilder.Entity<PerfilesUsuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Perfiles__2B3DE7B8C0414B26");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.NombreMostrar).HasMaxLength(120);
            entity.Property(e => e.UrlAvatar).HasMaxLength(400);

            entity.HasOne(d => d.Usuario).WithOne(p => p.PerfilesUsuario)
                .HasForeignKey<PerfilesUsuario>(d => d.UsuarioId)
                .HasConstraintName("FK_PerfilesUsuarios_Usuarios");
        });

        modelBuilder.Entity<Plane>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Planes__755C22B76C9950D2");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CaracteristicasJson).HasColumnName("CaracteristicasJSON");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Suscripcione>(entity =>
        {
            entity.HasKey(e => e.SuscripcionId).HasName("PK__Suscripc__814D76AB3E3EB828");

            entity.Property(e => e.Estado).HasMaxLength(30);
            entity.Property(e => e.UsuarioId).HasMaxLength(450);

            entity.HasOne(d => d.Plan).WithMany(p => p.Suscripciones)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Suscripci__PlanI__5535A963");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Suscripciones)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Suscripci__Usuar__5441852A");
        });

        modelBuilder.Entity<Trailer>(entity =>
        {
            entity.HasKey(e => e.TrailerId).HasName("PK__Trailers__1B041D23D4B7B65B");

            entity.Property(e => e.FechaPublicacion).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasDefaultValue("Trailer");
            entity.Property(e => e.Titulo).HasMaxLength(200);
            entity.Property(e => e.UrlVideo).HasMaxLength(400);

            entity.HasOne(d => d.Pelicula).WithMany(p => p.Trailers)
                .HasForeignKey(d => d.PeliculaId)
                .HasConstraintName("FK_Trailers_Peliculas");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B8B406A3E0");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A19BC556F5D").IsUnique();

            entity.Property(e => e.ContrasenaHash).HasMaxLength(200);
            entity.Property(e => e.Correo).HasMaxLength(120);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(sysutcdatetime())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
