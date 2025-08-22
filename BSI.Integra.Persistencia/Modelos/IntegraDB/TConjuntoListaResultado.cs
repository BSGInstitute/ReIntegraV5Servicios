using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConjuntoListaResultado
    {
        public TConjuntoListaResultado()
        {
            TWhatsAppConfiguracionEnvioDetalles = new HashSet<TWhatsAppConfiguracionEnvioDetalle>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_Alumno
        /// </summary>
        public int IdAlumno { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_ConjuntoListaDetalle
        /// </summary>
        public int IdConjuntoListaDetalle { get; set; }
        /// <summary>
        /// Indica si el registro proviene de venta cruzada
        /// </summary>
        public bool? EsVentaCruzada { get; set; }
        /// <summary>
        /// Indica el numero de ejecucion del conjunto lista
        /// </summary>
        public int NroEjecucion { get; set; }
        /// <summary>
        /// Indicara si el registro es activo (resultado de la ultima ejecucion)
        /// </summary>
        public bool Activo { get; set; }
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
        /// Llave foranea a la tabla com.T_Oportunidad
        /// </summary>
        public int? IdOportunidad { get; set; }

        public virtual TConjuntoListaDetalle IdConjuntoListaDetalleNavigation { get; set; } = null!;
        public virtual ICollection<TWhatsAppConfiguracionEnvioDetalle> TWhatsAppConfiguracionEnvioDetalles { get; set; }
    }
}
