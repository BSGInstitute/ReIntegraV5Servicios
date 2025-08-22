using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TEstadoSolicitud
    {
        public TEstadoSolicitud()
        {
            TSolicitudAlumnos = new HashSet<TSolicitudAlumno>();
        }

        /// <summary>
        /// Pk de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de estado solicitud
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion   registro atuomatico
        /// </summary>
        public string? UsuarioCreacion { get; set; }
        /// <summary>
        /// Usuario Modificacion   registro automatico
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion  registro automatico
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion  registro automatico
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Version de registro automatico
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id tabla migracion
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual ICollection<TSolicitudAlumno> TSolicitudAlumnos { get; set; }
    }
}
