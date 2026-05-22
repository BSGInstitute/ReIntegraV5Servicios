using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Opciones seleccionadas activamente para un parametro de tipo select o tags. Referencia T_EsquemaMetricaCampoOpcion (pool propio del esquema) — nunca al catalogo de sugerencias. Para campos tipo select: exactamente 1 fila activa por parametro. Para campos tipo tags: 1 a N filas activas por parametro (multivalor).
    /// </summary>
    public partial class TEsquemaMetricaParametroOpcion
    {
        /// <summary>
        /// Identificador unico autoincrementado.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK a T_EsquemaMetricaParametro. Parametro raiz al que pertenece esta seleccion.
        /// </summary>
        public int IdEsquemaMetricaParametro { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int IdMetricaCampoOpcion { get; set; }

        public virtual TEsquemaMetricaParametro IdEsquemaMetricaParametroNavigation { get; set; } = null!;
        public virtual TMetricaCampoOpcion IdMetricaCampoOpcionNavigation { get; set; } = null!;
    }
}
