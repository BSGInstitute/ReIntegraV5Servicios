using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAsignacionAutomaticaConfiguracion
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_FaseOportunidad
        /// </summary>
        public int IdFaseOportunidad { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_TipoDato
        /// </summary>
        public int? IdTipoDato { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Origen
        /// </summary>
        public int? IdOrigen { get; set; }
        /// <summary>
        /// Filtro si incluye estado 1 y estado 0 no incluye
        /// </summary>
        public bool Inclusivo { get; set; }
        /// <summary>
        /// Si esta habilitado la asignacion
        /// </summary>
        public bool Habilitado { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
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
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
    }
}
