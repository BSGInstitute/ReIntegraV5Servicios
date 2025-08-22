using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TUrlBlockStorage
    {
        public TUrlBlockStorage()
        {
            TRegistroCertificadoFisicoGenerados = new HashSet<TRegistroCertificadoFisicoGenerado>();
            TUrlContenedorPermisos = new HashSet<TUrlContenedorPermiso>();
            TUrlSubContenedors = new HashSet<TUrlSubContenedor>();
        }

        /// <summary>
        /// Llave primaria de la Tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Ruta de almacenamiento
        /// </summary>
        public string Ruta { get; set; } = null!;
        /// <summary>
        /// Nombre de blockStorage
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Llave Foranea de la tabla ope.T_ProveedorNube
        /// </summary>
        public int IdProveedorNube { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Flag de visibilidad para el modulo de archivos
        /// </summary>
        public bool? EsVisibleModuloArchivos { get; set; }
        /// <summary>
        /// Flag de subcontenedores para el modulo de archivos
        /// </summary>
        public bool? AplicaSubcontenedores { get; set; }
        /// <summary>
        /// Flag de subida multiple para el modulo de archivos
        /// </summary>
        public bool? AplicaSubidaMultiple { get; set; }
        /// <summary>
        /// Flag de validacion para los contenedores
        /// </summary>
        public bool? AplicaValidacion { get; set; }
        /// <summary>
        /// Subdominio visible para el modulo de subida de archivos
        /// </summary>
        public string? Subdominio { get; set; }

        public virtual ICollection<TRegistroCertificadoFisicoGenerado> TRegistroCertificadoFisicoGenerados { get; set; }
        public virtual ICollection<TUrlContenedorPermiso> TUrlContenedorPermisos { get; set; }
        public virtual ICollection<TUrlSubContenedor> TUrlSubContenedors { get; set; }
    }
}
