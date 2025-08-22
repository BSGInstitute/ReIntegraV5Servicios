using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TActividadMarcadorLog
    {
        /// <summary>
        /// (PK) Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de la Oportunidad
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// Id de la Actividad Detalle
        /// </summary>
        public int IdActividadDetalle { get; set; }
        /// <summary>
        /// Fecha programada de la Actividad
        /// </summary>
        public DateTime? FechaProgramada { get; set; }
        /// <summary>
        /// Total de intentos de llamada
        /// </summary>
        public int? TotalIntento { get; set; }
        /// <summary>
        /// Intentos contestados
        /// </summary>
        public int? Contestado { get; set; }
        /// <summary>
        /// Intentos no contestados
        /// </summary>
        public int? NoContestado { get; set; }
        /// <summary>
        /// Id de la agenda tab
        /// </summary>
        public int? IdAgendaTab { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de la tabla
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
