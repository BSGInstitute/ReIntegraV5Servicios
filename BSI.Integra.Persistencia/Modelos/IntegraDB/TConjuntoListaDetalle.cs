using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConjuntoListaDetalle
    {
        public TConjuntoListaDetalle()
        {
            TConjuntoListaDetalleValors = new HashSet<TConjuntoListaDetalleValor>();
            TConjuntoListaResultados = new HashSet<TConjuntoListaResultado>();
            TSmsConfiguracionEnvios = new HashSet<TSmsConfiguracionEnvio>();
            TWhatsAppConfiguracionEnvios = new HashSet<TWhatsAppConfiguracionEnvio>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la lista
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion de la lista
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Llave foranea a la tabla mkt.T_ConjuntoLista
        /// </summary>
        public int IdConjuntoLista { get; set; }
        /// <summary>
        /// Indica la prioridad de ejecucion entre listas
        /// </summary>
        public byte Prioridad { get; set; }
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

        public virtual TConjuntoListum IdConjuntoListaNavigation { get; set; } = null!;
        public virtual ICollection<TConjuntoListaDetalleValor> TConjuntoListaDetalleValors { get; set; }
        public virtual ICollection<TConjuntoListaResultado> TConjuntoListaResultados { get; set; }
        public virtual ICollection<TSmsConfiguracionEnvio> TSmsConfiguracionEnvios { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionEnvio> TWhatsAppConfiguracionEnvios { get; set; }
    }
}
