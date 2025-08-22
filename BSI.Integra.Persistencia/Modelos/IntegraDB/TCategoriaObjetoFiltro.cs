using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCategoriaObjetoFiltro
    {
        public TCategoriaObjetoFiltro()
        {
            TCampaniaGenerals = new HashSet<TCampaniaGeneral>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del nivel
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Nombre del objeto de base de datos
        /// </summary>
        public string NombreObjeto { get; set; } = null!;
        /// <summary>
        /// Indica si el objeto es una tabla, en caso sea false es una vista
        /// </summary>
        public bool EsTabla { get; set; }
        /// <summary>
        /// Indica si el registro aplica al modulo de conjunto listas
        /// </summary>
        public bool AplicaConjuntoLista { get; set; }
        /// <summary>
        /// Indica si el registro aplica al modulo de filtro segmento
        /// </summary>
        public bool AplicaFiltroSegmento { get; set; }
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

        public virtual ICollection<TCampaniaGeneral> TCampaniaGenerals { get; set; }
    }
}
