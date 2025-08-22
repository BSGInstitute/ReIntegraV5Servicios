using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TRevisionNivelPw
    {
        public TRevisionNivelPw()
        {
            TBandejaPendientePws = new HashSet<TBandejaPendientePw>();
            TPlantillaRevisionPws = new HashSet<TPlantillaRevisionPw>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del nivel de revision
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Prioridad que tienen las revisiones
        /// </summary>
        public int Prioridad { get; set; }
        /// <summary>
        /// Es Foreing Key T_TipoRevision_PW
        /// </summary>
        public int IdTipoRevisionPw { get; set; }
        /// <summary>
        /// Es Foreing Key T_Revision_PW
        /// </summary>
        public int IdRevisionPw { get; set; }
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
        public Guid? IdMigracion { get; set; }

        public virtual TRevisionPw IdRevisionPwNavigation { get; set; } = null!;
        public virtual ICollection<TBandejaPendientePw> TBandejaPendientePws { get; set; }
        public virtual ICollection<TPlantillaRevisionPw> TPlantillaRevisionPws { get; set; }
    }
}
