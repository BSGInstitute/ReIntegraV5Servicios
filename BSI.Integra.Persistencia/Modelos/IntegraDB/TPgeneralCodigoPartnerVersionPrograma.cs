using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPgeneralCodigoPartnerVersionPrograma
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// clave foranea de la tabla pla.T_PgeneralCodigoPartner
        /// </summary>
        public int IdPgeneralCodigoPartner { get; set; }
        /// <summary>
        /// Clave foranea de la tabla pla.T_VersionPrograma
        /// </summary>
        public int? IdVersionPrograma { get; set; }
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
        public int IdMigracion { get; set; }

        public virtual TPgeneralCodigoPartner IdPgeneralCodigoPartnerNavigation { get; set; } = null!;
        public virtual TVersionPrograma? IdVersionProgramaNavigation { get; set; }
    }
}
