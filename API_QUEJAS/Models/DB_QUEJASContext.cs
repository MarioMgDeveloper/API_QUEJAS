using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_QUEJAS.Models
{
    public partial class DB_QUEJASContext : DbContext
    {
        public DB_QUEJASContext()
        {
        }

        public DB_QUEJASContext(DbContextOptions<DB_QUEJASContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbCategoriaQueja> TbCategoriaQueja { get; set; }
        public virtual DbSet<TbDepartamento> TbDepartamento { get; set; }
        public virtual DbSet<TbEmpresa> TbEmpresa { get; set; }
        public virtual DbSet<TbEstablecimiento> TbEstablecimiento { get; set; }
        public virtual DbSet<TbEstado> TbEstado { get; set; }
        public virtual DbSet<TbMunicipio> TbMunicipio { get; set; }
        public virtual DbSet<TbPersona> TbPersona { get; set; }
        public virtual DbSet<TbQueja> TbQueja { get; set; }
        public virtual DbSet<TbRegion> TbRegion { get; set; }
        public virtual DbSet<TbRol> TbRol { get; set; }
        public virtual DbSet<TbUsuario> TbUsuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-OI9HJ7U;Database=DB_QUEJAS;User Id=DBADIACO; Password=D14C02020");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbCategoriaQueja>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaQueja);

                entity.ToTable("TB_CATEGORIA_QUEJA");

                entity.Property(e => e.IdCategoriaQueja).HasColumnName("ID_CATEGORIA_QUEJA");

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbDepartamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento);

                entity.ToTable("TB_DEPARTAMENTO");

                entity.Property(e => e.IdDepartamento).HasColumnName("ID_DEPARTAMENTO");

                entity.Property(e => e.CodigoDepartamento)
                    .HasColumnName("CODIGO_DEPARTAMENTO")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IdRegion).HasColumnName("ID_REGION");

                entity.Property(e => e.NombreDepartamento)
                    .IsRequired()
                    .HasColumnName("NOMBRE_DEPARTAMENTO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRegionNavigation)
                    .WithMany(p => p.TbDepartamento)
                    .HasForeignKey(d => d.IdRegion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_DEPARTAMENTO_TB_REGION");
            });

            modelBuilder.Entity<TbEmpresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa);

                entity.ToTable("TB_EMPRESA");

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.DireccionFiscal)
                    .HasColumnName("DIRECCION_FISCAL")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.Nit)
                    .HasColumnName("NIT")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreEmpresa)
                    .HasColumnName("NOMBRE_EMPRESA")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbEstablecimiento>(entity =>
            {
                entity.HasKey(e => e.IdEstablecimiento);

                entity.ToTable("TB_ESTABLECIMIENTO");

                entity.Property(e => e.IdEstablecimiento).HasColumnName("ID_ESTABLECIMIENTO");

                entity.Property(e => e.Direccion)
                    .HasColumnName("DIRECCION")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.IdMunicipio).HasColumnName("ID_MUNICIPIO");

                entity.Property(e => e.NombreComplementario)
                    .HasColumnName("NOMBRE_COMPLEMENTARIO")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PatenteComercio)
                    .HasColumnName("PATENTE_COMERCIO")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.TbEstablecimiento)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_ESTABLECIMIENTO_TB_EMPRESA");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TbEstablecimiento)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_ESTABLECIMIENTO_TB_ESTADO");
            });

            modelBuilder.Entity<TbEstado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("TB_ESTADO");

                entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.NombreEstado)
                    .HasColumnName("NOMBRE_ESTADO")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbMunicipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio);

                entity.ToTable("TB_MUNICIPIO");

                entity.Property(e => e.IdMunicipio).HasColumnName("ID_MUNICIPIO");

                entity.Property(e => e.CodigoMunicipio)
                    .HasColumnName("CODIGO_MUNICIPIO")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IdDepartamento).HasColumnName("ID_DEPARTAMENTO");

                entity.Property(e => e.NombreMunicipio)
                    .HasColumnName("NOMBRE_MUNICIPIO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.TbMunicipio)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_MUNICIPIO_TB_DEPARTAMENTO");
            });

            modelBuilder.Entity<TbPersona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("TB_PERSONA");

                entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");

                entity.Property(e => e.ApellidoCasada)
                    .HasColumnName("APELLIDO_CASADA")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasColumnName("CORREO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasColumnName("DIRECCION")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dpi)
                    .HasColumnName("DPI")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Genero)
                    .HasColumnName("GENERO")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PrimerApellido)
                    .HasColumnName("PRIMER_APELLIDO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PrimerNombre)
                    .HasColumnName("PRIMER_NOMBRE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SegundoApellido)
                    .HasColumnName("SEGUNDO_APELLIDO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SegundoNombre)
                    .HasColumnName("SEGUNDO_NOMBRE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasColumnName("TELEFONO")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbQueja>(entity =>
            {
                entity.HasKey(e => e.IdQueja);

                entity.ToTable("TB_QUEJA");

                entity.Property(e => e.IdQueja).HasColumnName("ID_QUEJA");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DescripcionResuelve)
                    .HasColumnName("DESCRIPCION_RESUELVE")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION")
                    .HasColumnType("date");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnName("FECHA_MODIFICACION")
                    .HasColumnType("date");

                entity.Property(e => e.IdCategoriaQueja).HasColumnName("ID_CATEGORIA_QUEJA");

                entity.Property(e => e.IdEstablecimiento).HasColumnName("ID_ESTABLECIMIENTO");

                entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.IdUsuarioResuleve).HasColumnName("ID_USUARIO_RESULEVE");

                entity.HasOne(d => d.IdCategoriaQuejaNavigation)
                    .WithMany(p => p.TbQueja)
                    .HasForeignKey(d => d.IdCategoriaQueja)
                    .HasConstraintName("FK_TB_QUEJA_TB_CATEGORIA_QUEJA");

                entity.HasOne(d => d.IdEstablecimientoNavigation)
                    .WithMany(p => p.TbQueja)
                    .HasForeignKey(d => d.IdEstablecimiento)
                    .HasConstraintName("FK_TB_QUEJA_TB_ESTABLECIMIENTO");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TbQueja)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_QUEJA_TB_ESTADO");

                entity.HasOne(d => d.IdUsuarioResuleveNavigation)
                    .WithMany(p => p.TbQueja)
                    .HasForeignKey(d => d.IdUsuarioResuleve)
                    .HasConstraintName("FK_TB_QUEJA_TB_USUARIO");
            });

            modelBuilder.Entity<TbRegion>(entity =>
            {
                entity.HasKey(e => e.IdRegion);

                entity.ToTable("TB_REGION");

                entity.Property(e => e.IdRegion).HasColumnName("ID_REGION");

                entity.Property(e => e.NombrRegion)
                    .HasColumnName("NOMBR_REGION")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbRol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.ToTable("TB_ROL");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.NombreRol)
                    .HasColumnName("NOMBRE_ROL")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("TB_USUARIO");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnName("CORREO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TbUsuario)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_USUARIO_TB_ESTADO");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.TbUsuario)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_USUARIO_TB_PERSONA");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.TbUsuario)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_USUARIO_TB_ROL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
