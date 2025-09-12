using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFaseOportunidad
    {
        public TFaseOportunidad()
        {

            TTransicionFaseIdFaseOportunidadDestinoNavigations = new HashSet<TTransicionFase>();
            TTransicionFaseIdFaseOportunidadOrigenNavigations = new HashSet<TTransicionFase>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int? NroMinutos { get; set; }
        public int? IdActividad { get; set; }
        public int? MaxNumDias { get; set; }
        public int? MinNumDias { get; set; }
        public int? TasaConversionEsperada { get; set; }
        public int? Meta { get; set; }
        public bool? Final { get; set; }
        public bool? ReporteMeta { get; set; }
        public bool? EnSeguimiento { get; set; }
        /// <summary>
        /// Indica si la Fase es de Tipo Cierre
        /// </summary>
        public bool? EsCierre { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Descripción de Fase
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Validación de visualización en Reportes
        /// </summary>
        public bool? VisibleEnReporte { get; set; }

        public virtual ICollection<TTransicionFase> TTransicionFaseIdFaseOportunidadDestinoNavigations { get; set; }
        public virtual ICollection<TTransicionFase> TTransicionFaseIdFaseOportunidadOrigenNavigations { get; set; }
    }
}
    