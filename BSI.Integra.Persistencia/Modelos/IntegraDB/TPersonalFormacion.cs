using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalFormacion
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Fk T_CentroEstudio
        /// </summary>
        public int IdCentroEstudio { get; set; }
        /// <summary>
        /// Fk T_TipoEstudio
        /// </summary>
        public int IdTipoEstudio { get; set; }
        /// <summary>
        /// Fk T_AreaFormacion
        /// </summary>
        public int? IdAreaFormacion { get; set; }
        /// <summary>
        /// FechaInicio formacion
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha fin formacion
        /// </summary>
        public DateTime? FechaFin { get; set; }
        /// <summary>
        /// flag para ssaber si el personal estudia a la actualidad
        /// </summary>
        public bool? AlaActualidad { get; set; }
        /// <summary>
        /// Fk T_EstadoEstudio
        /// </summary>
        public int IdEstadoEstudio { get; set; }
        /// <summary>
        /// Logro del personal
        /// </summary>
        public string? Logro { get; set; }
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
        /// FK de T_PersonalArchivo
        /// </summary>
        public int? IdPersonalArchivo { get; set; }
    }
}
