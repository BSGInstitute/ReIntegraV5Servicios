using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCabeceraConfiguracionLlamadaAutomatica
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de segmento
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Id de T_IdIvrPlantilla
        /// </summary>
        public int IdIvrPlantilla { get; set; }
        /// <summary>
        /// Id de T_IvrTipoConfiguracion
        /// </summary>
        public int IdIvrTipoConfiguracion { get; set; }
        /// <summary>
        /// Id de T_PEspecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Id de T_IvrEjecucion
        /// </summary>
        public int IdIvrEjecucion { get; set; }
        /// <summary>
        /// Hora inicio ejecución
        /// </summary>
        public TimeSpan HoraInicio { get; set; }
        /// <summary>
        /// Hora fin ejecución
        /// </summary>
        public TimeSpan HoraFin { get; set; }
        /// <summary>
        /// Estado de Proceso
        /// </summary>
        public string EstadoProceso { get; set; } = null!;
        /// <summary>
        /// Congelamiento de la configuracion
        /// </summary>
        public string CongelamientoConfiguracion { get; set; } = null!;
        /// <summary>
        /// Campo auditoria Estado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Campo auditoria UsuarioCreacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Campo auditoria UsuarioModificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo auditoria FechaCreacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Campo auditoria FechaModificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo auditoria RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
