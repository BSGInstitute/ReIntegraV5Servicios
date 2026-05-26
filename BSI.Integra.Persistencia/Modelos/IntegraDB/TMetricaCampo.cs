using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Campos configurables de cada metrica del catalogo. Define estructura, tipo y comportamiento visual del campo. No almacena valores ni opciones definitivas: las opciones sugeridas van en T_MetricaCampoOpcion, las opciones del esquema en T_EsquemaMetricaCampoOpcion y los valores del esquema en T_EsquemaMetricaParametro.
    /// </summary>
    public partial class TMetricaCampo
    {
        public TMetricaCampo()
        {
            TEsquemaMetricaParametros = new HashSet<TEsquemaMetricaParametro>();
            TMetricaCampoOpcions = new HashSet<TMetricaCampoOpcion>();
        }

        /// <summary>
        /// Identificador unico autoincrementado.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK a T_MetricaCatalogo. Metrica a la que pertenece este campo.
        /// </summary>
        public int IdMetricaCatalogo { get; set; }
        /// <summary>
        /// Etiqueta legible para el UI del formulario (ej: Anos minimos, Skills obligatorias).
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Tipo de control del campo. Valores validos: number | select | boolean | tags | textarea | text. Determina en que columna de T_EsquemaMetricaParametro se almacena el valor y si usa tabla de opciones.
        /// </summary>
        public string TipoCampo { get; set; } = null!;
        /// <summary>
        /// Campo que hacer referencia si un campo es requerido o no
        /// </summary>
        public bool Requerido { get; set; }
        /// <summary>
        /// Posicion del campo en el formulario de configuracion de la metrica. Default 0.
        /// </summary>
        public int Orden { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;

        public virtual TMetricaCatalogo IdMetricaCatalogoNavigation { get; set; } = null!;
        public virtual ICollection<TEsquemaMetricaParametro> TEsquemaMetricaParametros { get; set; }
        public virtual ICollection<TMetricaCampoOpcion> TMetricaCampoOpcions { get; set; }
    }
}
