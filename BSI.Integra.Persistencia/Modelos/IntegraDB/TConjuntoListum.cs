using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConjuntoListum
    {
        public TConjuntoListum()
        {
            TConjuntoListaDetalles = new HashSet<TConjuntoListaDetalle>();
            TSeguimientoPreProcesoListaWhatsApps = new HashSet<TSeguimientoPreProcesoListaWhatsApp>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del conjunto de listas
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion del conjunto de listas
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Llave foranea  la tabla mkt.T_CategoriaObjetoFiltro
        /// </summary>
        public int IdCategoriaObjetoFiltro { get; set; }
        /// <summary>
        /// Llave foranea  la tabla mkt.T_FiltroSegmento
        /// </summary>
        public int IdFiltroSegmento { get; set; }
        /// <summary>
        /// Indica el numero de listas en el que puede estar un contacto
        /// </summary>
        public byte NroListasRepeticionContacto { get; set; }
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
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Indica si se consideraran a los ya enviados
        /// </summary>
        public int? ConsiderarYaEnviados { get; set; }

        public virtual ICollection<TConjuntoListaDetalle> TConjuntoListaDetalles { get; set; }
        public virtual ICollection<TSeguimientoPreProcesoListaWhatsApp> TSeguimientoPreProcesoListaWhatsApps { get; set; }
    }
}
