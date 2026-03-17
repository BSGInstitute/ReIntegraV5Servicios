using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la observacion del estado de la sesion
    /// </summary>
    public partial class TPespecificoSesionEstadoObservacion
    {
        public TPespecificoSesionEstadoObservacion()
        {
            TPespecificoSesionEstadoObservacionDetalles = new HashSet<TPespecificoSesionEstadoObservacionDetalle>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripcion de las Observaciones por Estado de Curso
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Foreign Key con la tabla de T_PEspecificoSesionEstado
        /// </summary>
        public int IdPespecificoSesionEstado { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TPespecificoSesionEstado IdPespecificoSesionEstadoNavigation { get; set; } = null!;
        public virtual ICollection<TPespecificoSesionEstadoObservacionDetalle> TPespecificoSesionEstadoObservacionDetalles { get; set; }
    }
}
