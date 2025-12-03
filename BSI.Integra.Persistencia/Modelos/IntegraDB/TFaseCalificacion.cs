using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena las fases de evaluacion de llamada
    /// </summary>
    public partial class TFaseCalificacion
    {
        public TFaseCalificacion()
        {
            TCriterioCalificacionLlamada = new HashSet<TCriterioCalificacionLlamadum>();
        }

        /// <summary>
        /// Identificador único de la fase
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la fase
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Orden de la Fase
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Descripción de la fase
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro (activo o inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Versión del registro para control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// (FK) Referencia al personal/área de trabajo correspondiente a la fase
        /// </summary>
        public int? IdPersonalAreaTrabajo { get; set; }

        public virtual TPersonalAreaTrabajo? IdPersonalAreaTrabajoNavigation { get; set; }
        public virtual ICollection<TCriterioCalificacionLlamadum> TCriterioCalificacionLlamada { get; set; }
    }
}
