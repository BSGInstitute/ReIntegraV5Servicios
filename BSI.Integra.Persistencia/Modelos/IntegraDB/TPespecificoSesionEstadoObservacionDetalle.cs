using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la observacion del estado de la sesion
    /// </summary>
    public partial class TPespecificoSesionEstadoObservacionDetalle
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Observacion por Estado de Curso
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Foreign Key con la tabla de T_PEspecificoSesionEstadoObservacionDetalle
        /// </summary>
        public int IdPespecificoSesionEstadoObservacion { get; set; }
        /// <summary>
        /// Orden de las Observaciones
        /// </summary>
        public int Orden { get; set; }
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

        public virtual TPespecificoSesionEstadoObservacion IdPespecificoSesionEstadoObservacionNavigation { get; set; } = null!;
    }
}
