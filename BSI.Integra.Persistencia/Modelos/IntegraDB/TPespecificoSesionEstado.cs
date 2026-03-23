using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena el estado de la sesion
    /// </summary>
    public partial class TPespecificoSesionEstado
    {
        public TPespecificoSesionEstado()
        {
            TPespecificoSesionEstadoObservacions = new HashSet<TPespecificoSesionEstadoObservacion>();
            TPespecificoSesions = new HashSet<TPespecificoSesion>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripcion del control en el que se encuentra la ejecucion
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Estado del SubCargo
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del Control Envio
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion del Control Envio
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion del Control Envio
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del Control Envio
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del Control Envio
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TPespecificoSesionEstadoObservacion> TPespecificoSesionEstadoObservacions { get; set; }
        public virtual ICollection<TPespecificoSesion> TPespecificoSesions { get; set; }
    }
}
