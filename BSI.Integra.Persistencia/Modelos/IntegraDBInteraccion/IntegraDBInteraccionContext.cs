using Microsoft.EntityFrameworkCore;

namespace BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion
{
    public partial class IntegraDBInteraccionContext : DbContext
    {
        public IntegraDBInteraccionContext()
        {
        }

        public IntegraDBInteraccionContext(DbContextOptions<IntegraDBInteraccionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TInteraccionModulo> TInteraccionModulos { get; set; } = null!;
        public virtual DbSet<TRegistroInicioSesion> TRegistroInicioSesions { get; set; } = null!;
        public virtual DbSet<TRegistroInicioSesionEstado> TRegistroInicioSesionEstados { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CI_AS");

            modelBuilder.Entity<TInteraccionModulo>(entity =>
            {
                entity.ToTable("T_InteraccionModulo", "ibck");

                entity.Property(e => e.Id).HasComment("Llave primaria");

                entity.Property(e => e.Contenido)
                    .HasMaxLength(2000)
                    .HasComment("Contenido de la interacción en caso de input");

                entity.Property(e => e.ControlNombre)
                    .HasMaxLength(500)
                    .HasComment("Dirección MAC del equipo de origen");

                entity.Property(e => e.ControlTipo)
                    .HasMaxLength(50)
                    .HasComment("Dirección MAC del equipo de origen");

                entity.Property(e => e.DireccionMac)
                    .HasMaxLength(50)
                    .HasColumnName("DireccionMAC")
                    .HasComment("Dirección MAC del equipo de origen");

                entity.Property(e => e.Estado).HasComment("Estado del registro (creado o eliminado)");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasComment("Sistema Automatico Fecha de creacion");

                entity.Property(e => e.FechaInteraccion)
                    .HasColumnType("datetime")
                    .HasComment("Fecha de registro de la interacción en integra");

                entity.Property(e => e.FechaInteraccionEntera).HasComment("Fecha de registro de la interacción en formato entero");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasComment("Sistema Automatico Fecha de modificacion");

                entity.Property(e => e.HoraInteraccionEntera).HasComment("Hora de registro de la interacción en formato entero");

                entity.Property(e => e.IdUsuario).HasComment("Identificador principal de la tabla conf.T_Usuario");

                entity.Property(e => e.InteraccionJson).HasComment("Dirección MAC del equipo de origen");

                entity.Property(e => e.IpLocal)
                    .HasMaxLength(25)
                    .HasComment("Ip Local de equipo de origen");

                entity.Property(e => e.IpPublica)
                    .HasMaxLength(25)
                    .HasComment("Ip Publica del equipo de origen");

                entity.Property(e => e.NombreModulo)
                    .HasMaxLength(150)
                    .HasComment("Dirección MAC del equipo de origen");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasComment("Campo de sistema automatico que guarda la version del registro");

                entity.Property(e => e.UrlActual)
                    .HasMaxLength(75)
                    .HasComment("Url actual de navegación en Integra");

                entity.Property(e => e.UrlAnterior)
                    .HasMaxLength(75)
                    .HasComment("Url anterior de navegación en Integra");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Sistema Automatico Usuario de creacion");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Sistema Automatico Usuario de modificacion");
            });

            modelBuilder.Entity<TRegistroInicioSesion>(entity =>
            {
                entity.ToTable("T_RegistroInicioSesion", "lgv");

                entity.Property(e => e.Id).HasComment("Llave primaria");

                entity.Property(e => e.Clave)
                    .HasMaxLength(100)
                    .HasComment("Clave ingresada para inicio de sesión en Integra");

                entity.Property(e => e.DireccionMac)
                    .HasMaxLength(50)
                    .HasColumnName("DireccionMAC")
                    .HasComment("Dirección MAC del equipo de origen");

                entity.Property(e => e.Estado).HasComment("Estado del registro (creado o eliminado)");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasComment("Sistema Automatico Fecha de creacion");

                entity.Property(e => e.FechaInteraccionEntera).HasComment("Fecha de registro de la interacción en formato entero");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasComment("Sistema Automatico Fecha de modificacion");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasComment("Fecha de registro de la interacción");

                entity.Property(e => e.HoraInteraccionEntera).HasComment("Hora de registro de la interacción en formato entero");

                entity.Property(e => e.IdPersonal).HasComment("Identificador principal de la tabla gp.T_Personal");

                entity.Property(e => e.IpLocal)
                    .HasMaxLength(25)
                    .HasComment("Ip Local de equipo de origen");

                entity.Property(e => e.IpPublica)
                    .HasMaxLength(25)
                    .HasComment("Ip Publica del equipo de origen");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasComment("Campo de sistema automatico que guarda la version del registro");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(100)
                    .HasComment("Usuario ingresado para inicio de sesión en Integra");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Sistema Automatico Usuario de creacion");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Sistema Automatico Usuario de modificacion");
            });

            modelBuilder.Entity<TRegistroInicioSesionEstado>(entity =>
            {
                entity.ToTable("T_RegistroInicioSesionEstado", "lgv");

                entity.Property(e => e.Id).HasComment("Llave primaria");

                entity.Property(e => e.Descripcion).HasComment("Descripción del inicio de sesión generada en el logueo");

                entity.Property(e => e.Estado).HasComment("Estado del registro (creado o eliminado)");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasComment("Sistema Automatico Fecha de creacion");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasComment("Sistema Automatico Fecha de modificacion");

                entity.Property(e => e.IdRegistroInicioSesion).HasComment("Identificador principal de la tabla lgv.T_RegistroInicioSesion");

                entity.Property(e => e.InicioSesionCorrecta).HasComment("Estado de inicio de sesión correcto o no");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasComment("Campo de sistema automatico que guarda la version del registro");

                entity.Property(e => e.TokenGenerada)
                    .HasMaxLength(1000)
                    .HasComment("Token generada en el inicio de sesión");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Sistema Automatico Usuario de creacion");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Sistema Automatico Usuario de modificacion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
