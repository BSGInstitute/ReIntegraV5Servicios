using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Un registro por cada campo configurado de una metrica en un esquema. El tipo del campo lo determina IdMetricaCampo via JOIN con T_MetricaCampo. Mapeo de columnas segun TipoCampo: number -&gt; ValorNumerico, boolean -&gt; ValorBooleano, text/textarea -&gt; ValorTextoLibre, select/tags -&gt; valores en T_EsquemaMetricaCampoOpcion y T_EsquemaMetricaParametroOpcion (todas las columnas directas quedan NULL). CHK garantiza que como maximo una columna directa tenga valor simultaneamente.
    /// </summary>
    public partial class TEsquemaMetricaParametro
    {
        public TEsquemaMetricaParametro()
        {
            TEsquemaMetricaParametroOpcions = new HashSet<TEsquemaMetricaParametroOpcion>();
        }

        /// <summary>
        /// Identificador unico autoincrementado.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK a T_EsquemaMetrica. Metrica del esquema a la que pertenece este parametro.
        /// </summary>
        public int IdEsquemaMetrica { get; set; }
        /// <summary>
        /// FK a T_MetricaCampo. Determina que tipo de campo es y en que columna de valor se almacena el dato. UQ con IdEsquemaMetrica.
        /// </summary>
        public int IdMetricaCampo { get; set; }
        /// <summary>
        /// Valor entero para campos con TipoCampo = number. Ej: anos_minimos = 5. NULL para otros tipos.
        /// </summary>
        public int? ValorNumerico { get; set; }
        /// <summary>
        /// Valor booleano para campos con TipoCampo = boolean. Ej: penalizar_brechas = 1. NULL para otros tipos.
        /// </summary>
        public bool? ValorBooleano { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public string? ValorTexto { get; set; }

        public virtual TEsquemaMetrica IdEsquemaMetricaNavigation { get; set; } = null!;
        public virtual TMetricaCampo IdMetricaCampoNavigation { get; set; } = null!;
        public virtual ICollection<TEsquemaMetricaParametroOpcion> TEsquemaMetricaParametroOpcions { get; set; }
    }
}
