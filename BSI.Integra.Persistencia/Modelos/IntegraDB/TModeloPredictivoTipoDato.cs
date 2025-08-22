using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TModeloPredictivoTipoDato
    {
        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_PROGRAMA
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_TipoDato
        /// </summary>
        public int IdTipoDato { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
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

        public virtual TPgeneral? IdPgeneralNavigation { get; set; }
    }
}
