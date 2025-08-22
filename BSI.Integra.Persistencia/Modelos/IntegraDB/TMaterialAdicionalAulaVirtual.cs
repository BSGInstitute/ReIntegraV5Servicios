using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMaterialAdicionalAulaVirtual
    {
        public TMaterialAdicionalAulaVirtual()
        {
            TMaterialAdicionalAulaVirtualPespecificos = new HashSet<TMaterialAdicionalAulaVirtualPespecifico>();
            TMaterialAdicionalAulaVirtualRegistros = new HashSet<TMaterialAdicionalAulaVirtualRegistro>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es nombre de la configuracion
        /// </summary>
        public string NombreConfiguracion { get; set; } = null!;
        /// <summary>
        /// Es foreign key de T_Pgeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Indica si es Online 
        /// </summary>
        public bool? EsOnline { get; set; }
        /// <summary>
        /// Para saber si el registro fue eliminado de forma logica
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

        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
        public virtual ICollection<TMaterialAdicionalAulaVirtualPespecifico> TMaterialAdicionalAulaVirtualPespecificos { get; set; }
        public virtual ICollection<TMaterialAdicionalAulaVirtualRegistro> TMaterialAdicionalAulaVirtualRegistros { get; set; }
    }
}
