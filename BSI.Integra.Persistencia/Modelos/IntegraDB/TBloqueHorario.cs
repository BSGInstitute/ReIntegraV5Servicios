using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TBloqueHorario
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la seccion del dia
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion del bloque nombrado
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Hora de inicio del bloque
        /// </summary>
        public TimeSpan HoraInicio { get; set; }
        /// <summary>
        /// Hora de fin del bloque
        /// </summary>
        public TimeSpan HoraFin { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_ConfiguracionBIC
        /// </summary>
        public int? IdConfiguracionBic { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
    }
}
