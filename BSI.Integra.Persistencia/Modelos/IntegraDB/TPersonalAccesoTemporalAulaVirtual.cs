using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalAccesoTemporalAulaVirtual
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla gp.T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Llave foranea de la tabla pla.T_PEspecifico
        /// </summary>
        public int IdPespecificoPadre { get; set; }
        /// <summary>
        /// Llave foranea de la tabla pla.T_PEspecifico
        /// </summary>
        public int IdPespecificoHijo { get; set; }
        /// <summary>
        /// Fecha de inicio del acceso temporal
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha de fin del acceso temporal
        /// </summary>
        public DateTime FechaFin { get; set; }
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
        /// Flag para determinar si el alumno contara con evaluaciones
        /// </summary>
        public bool? EvaluacionHabilitada { get; set; }
    }
}
