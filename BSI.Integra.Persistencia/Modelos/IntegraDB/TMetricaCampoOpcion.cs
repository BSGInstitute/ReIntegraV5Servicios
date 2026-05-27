using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Sugerencias de opciones del catalogo para campos de tipo select y tags. Son valores orientativos no vinculantes: el UI los ofrece como autocompletado al configurar un esquema, pero el esquema no esta obligado a usarlos. Las opciones propias de cada esquema viven en T_EsquemaMetricaCampoOpcion.
    /// </summary>
    public partial class TMetricaCampoOpcion
    {
        public TMetricaCampoOpcion()
        {
            TEsquemaMetricaParametroOpcions = new HashSet<TEsquemaMetricaParametroOpcion>();
        }

        /// <summary>
        /// Identificador unico autoincrementado.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK a T_MetricaCampo. Solo campos con TipoCampo = select o tags.
        /// </summary>
        public int IdMetricaCampo { get; set; }
        /// <summary>
        /// Valor sugerido disponible para este campo. Unico por campo. No vinculante para los esquemas: cada esquema define sus propias opciones en T_EsquemaMetricaCampoOpcion.
        /// </summary>
        public string Opcion { get; set; } = null!;
        /// <summary>
        /// Posicion de la sugerencia en el selector de autocompletado del UI. Default 0.
        /// </summary>
        public int Orden { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;

        public virtual TMetricaCampo IdMetricaCampoNavigation { get; set; } = null!;
        public virtual ICollection<TEsquemaMetricaParametroOpcion> TEsquemaMetricaParametroOpcions { get; set; }
    }
}
