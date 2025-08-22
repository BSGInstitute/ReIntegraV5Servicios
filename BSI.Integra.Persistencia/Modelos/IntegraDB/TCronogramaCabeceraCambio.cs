using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCronogramaCabeceraCambio
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_CronogramaTipoModificacion
        /// </summary>
        public int IdCronogramaTipoModificacion { get; set; }
        /// <summary>
        /// Solicitado por, relacion con T_Personal
        /// </summary>
        public int? SolicitadoPor { get; set; }
        /// <summary>
        /// Aprobado por, relacion con T_Personal
        /// </summary>
        public int? AprobadoPor { get; set; }
        /// <summary>
        /// Indica si el cambio fue aprobado
        /// </summary>
        public bool Aprobado { get; set; }
        /// <summary>
        /// Indica si el cambio fue Cancelado
        /// </summary>
        public bool Cancelado { get; set; }
        /// <summary>
        /// Obervaciones
        /// </summary>
        public string? Observacion { get; set; }
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
        /// Sistema Automatico Fecha de Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Relacion con el id de la tabla original
        /// </summary>
        public Guid? IdMigracion { get; set; }
    }
}
