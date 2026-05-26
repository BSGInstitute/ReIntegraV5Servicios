using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Metricas incluidas en cada version de esquema. Almacena el peso porcentual de la metrica y el PromptOverride opcional para afinar la instruccion de IA para este puesto. Resolucion del prompt efectivo en tiempo de evaluacion: si PromptOverride IS NOT NULL se usa el override; si es NULL se usa el PromptBase del catalogo; si ambos son NULL la IA solo usa los parametros estructurados.
    /// </summary>
    public partial class TEsquemaMetrica
    {
        public TEsquemaMetrica()
        {
            TEsquemaMetricaParametros = new HashSet<TEsquemaMetricaParametro>();
        }

        /// <summary>
        /// Identificador unico autoincrementado.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK a T_EsquemaVersion. Version del esquema a la que pertenece esta metrica.
        /// </summary>
        public int IdEsquemaMetricaPlantillaVersion { get; set; }
        /// <summary>
        /// FK a T_MetricaCatalogo. Metrica del catalogo incluida en este esquema. UQ con IdEsquemaVersion.
        /// </summary>
        public int IdMetricaCatalogo { get; set; }
        /// <summary>
        /// Peso porcentual de esta metrica en el esquema (0-100). La suma de todos los pesos de una version debe ser 100. Validado en SP_T_EsquemaVersion_Publicar antes de publicar.
        /// </summary>
        public decimal? Peso { get; set; }
        /// <summary>
        /// Instruccion especifica para este puesto y version que complementa o reemplaza el PromptBase del catalogo. NULL = heredar PromptBase sin modificaciones.
        /// </summary>
        public string? PromptOverride { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;

        public virtual TEsquemaMetricaPlantillaVersion IdEsquemaMetricaPlantillaVersionNavigation { get; set; } = null!;
        public virtual TMetricaCatalogo IdMetricaCatalogoNavigation { get; set; } = null!;
        public virtual ICollection<TEsquemaMetricaParametro> TEsquemaMetricaParametros { get; set; }
    }
}
