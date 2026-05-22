using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catalogo global de metricas reutilizables para evaluacion de CVs. Cada metrica define un criterio de evaluacion (ej: Experiencia Laboral, Skills Tecnicas). Contiene el PromptBase: instruccion general para la IA evaluadora que aplica a todos los puestos y puede ser afinado por PromptOverride en T_EsquemaMetrica.
    /// </summary>
    public partial class TMetricaCatalogo
    {
        public TMetricaCatalogo()
        {
            TEsquemaMetricas = new HashSet<TEsquemaMetrica>();
            TMetricaCampos = new HashSet<TMetricaCampo>();
        }

        /// <summary>
        /// Identificador unico autoincrementado.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave tecnica del sistema (ej: experiencia_laboral, skills_tecnicas). Unico. Usado para mapeo con el frontend.
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Nombre legible de la metrica (ej: Experiencia Laboral, Skills Tecnicas).
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Explicacion de que evalua esta metrica. Visible en el catalogo del frontend.
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Instruccion en lenguaje natural para la IA evaluadora. Aplica a todos los puestos. Puede ser afinado o reemplazado por PromptOverride en T_EsquemaMetrica para un puesto especifico.
        /// </summary>
        public string? PromptBase { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TEsquemaMetrica> TEsquemaMetricas { get; set; }
        public virtual ICollection<TMetricaCampo> TMetricaCampos { get; set; }
    }
}
