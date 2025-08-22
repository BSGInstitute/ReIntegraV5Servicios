using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSubAreaCapacitacion
    {
        public TSubAreaCapacitacion()
        {
            TCampaniaGeneralDetalleSubAreas = new HashSet<TCampaniaGeneralDetalleSubArea>();
            TSubAreaCampaniaMailingDetalles = new HashSet<TSubAreaCampaniaMailingDetalle>();
            TSubAreaParametroSeoPws = new HashSet<TSubAreaParametroSeoPw>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la sub area de capacitacion
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion de la sub area
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_AreaCapacitacion
        /// </summary>
        public int IdAreaCapacitacion { get; set; }
        /// <summary>
        /// Estado de la sub area
        /// </summary>
        public bool EsVisibleWeb { get; set; }
        /// <summary>
        /// Identificador del sistema antiguo de la pagina web
        /// </summary>
        public int? IdSubArea { get; set; }
        public string? DescripcionHtml { get; set; }
        /// <summary>
        /// Creado o eliminado
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
        /// Sistema Automatico Fecha creacion
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
        /// <summary>
        /// Alias del SubArea en Facebook
        /// </summary>
        public string AliasFacebook { get; set; } = null!;

        public virtual ICollection<TCampaniaGeneralDetalleSubArea> TCampaniaGeneralDetalleSubAreas { get; set; }
        public virtual ICollection<TSubAreaCampaniaMailingDetalle> TSubAreaCampaniaMailingDetalles { get; set; }
        public virtual ICollection<TSubAreaParametroSeoPw> TSubAreaParametroSeoPws { get; set; }
    }
}
