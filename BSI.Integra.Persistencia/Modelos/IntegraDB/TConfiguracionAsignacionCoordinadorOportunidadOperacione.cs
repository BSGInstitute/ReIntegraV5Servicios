using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfiguracionAsignacionCoordinadorOportunidadOperacione
    {
        /// <summary>
        /// Primary Key de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de la tabla T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Fk de la tabla T_CentroCosto
        /// </summary>
        public int IdCentroCosto { get; set; }
        /// <summary>
        /// Fk tabla T_CentroCosto
        /// </summary>
        public int? IdCentroCostoHijo { get; set; }
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
        /// FK de T_EstadoMatricula
        /// </summary>
        public int? IdEstadoMatricula { get; set; }
        /// <summary>
        /// FK de T_SubEstadoMatricula
        /// </summary>
        public int? IdSubEstadoMatricula { get; set; }
    }
}
