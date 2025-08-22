using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPreguntaIntento
    {
        public TPreguntaIntento()
        {
            TPreguntaIntentoDetalles = new HashSet<TPreguntaIntentoDetalle>();
            TPreguntaProgramaCapacitacions = new HashSet<TPreguntaProgramaCapacitacion>();
        }

        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Numero maximo de intentos para realizar la pregunta
        /// </summary>
        public int? NumeroMaximoIntento { get; set; }
        /// <summary>
        /// True: muestra el feedback si alcanza numero de intentos maximo, False: si no muestra nada
        /// </summary>
        public bool? ActivarFeedbackMaximoIntento { get; set; }
        /// <summary>
        /// Mensaje de feedback por numero de intentos
        /// </summary>
        public string? MensajeFeedback { get; set; }
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

        public virtual ICollection<TPreguntaIntentoDetalle> TPreguntaIntentoDetalles { get; set; }
        public virtual ICollection<TPreguntaProgramaCapacitacion> TPreguntaProgramaCapacitacions { get; set; }
    }
}
