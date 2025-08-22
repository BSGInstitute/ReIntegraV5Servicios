using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPlantillaRevisionPw
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key T_Plantilla_PW
        /// </summary>
        public int IdPlantillaPw { get; set; }
        /// <summary>
        /// Es Foreing Key T_RevisionNivel
        /// </summary>
        public int IdRevisionNivelPw { get; set; }
        /// <summary>
        /// Es Foreing Key T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
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
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }

        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
        public virtual TPlantillaPw IdPlantillaPwNavigation { get; set; } = null!;
        public virtual TRevisionNivelPw IdRevisionNivelPwNavigation { get; set; } = null!;
    }
}
