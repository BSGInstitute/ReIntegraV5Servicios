using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfiguracionEnvioMailing
    {
        public TConfiguracionEnvioMailing()
        {
            TConfiguracionEnvioMailingDetalles = new HashSet<TConfiguracionEnvioMailingDetalle>();
        }

        /// <summary>
        /// Clave Primaria de la Tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la configuracion
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion de la configuracion
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_ConjuntoListaDetalle
        /// </summary>
        public int IdConjuntoListaDetalle { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_Plantilla
        /// </summary>
        public int IdPlantilla { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
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
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de la tabla
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Indica si es el registro activo
        /// </summary>
        public bool Activo { get; set; }

        public virtual ICollection<TConfiguracionEnvioMailingDetalle> TConfiguracionEnvioMailingDetalles { get; set; }
    }
}
