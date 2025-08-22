using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSeccionPw
    {
        public TSeccionPw()
        {
            TDocumentoSeccionPws = new HashSet<TDocumentoSeccionPw>();
            TSeccionTipoDetallePws = new HashSet<TSeccionTipoDetallePw>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la seccion
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion de la seccion
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Contenido de la seccion
        /// </summary>
        public string Contenido { get; set; } = null!;
        /// <summary>
        /// Es Foreing Key T_Plantilla_Pw
        /// </summary>
        public int IdPlantillaPw { get; set; }
        /// <summary>
        /// Si es visible en la pagina web
        /// </summary>
        public bool VisibleWeb { get; set; }
        /// <summary>
        /// La zona donde se visualizara en la web (programa)
        /// </summary>
        public int ZonaWeb { get; set; }
        /// <summary>
        /// El orden en que aparece las secciones en la web (programa)
        /// </summary>
        public int OrdenEeb { get; set; }
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
        /// <summary>
        /// Llave foranea de la tabla T_SeccionTipoContenido
        /// </summary>
        public int? IdSeccionTipoContenido { get; set; }

        public virtual TPlantillaPw IdPlantillaPwNavigation { get; set; } = null!;
        public virtual TSeccionTipoContenidoPw? IdSeccionTipoContenidoNavigation { get; set; }
        public virtual ICollection<TDocumentoSeccionPw> TDocumentoSeccionPws { get; set; }
        public virtual ICollection<TSeccionTipoDetallePw> TSeccionTipoDetallePws { get; set; }
    }
}
