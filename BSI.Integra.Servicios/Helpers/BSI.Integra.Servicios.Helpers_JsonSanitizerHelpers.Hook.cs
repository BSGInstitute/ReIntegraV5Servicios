using BSI.Integra.Servicios.Helpers.InformacionProgramaEstructurada;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BSI.Integra.Servicios.Helpers
{
    /// <summary>
    /// Normaliza propiedades JSON-encapsuladas, transforma InformacionPrograma HTML a JSON estructurado
    /// y realiza limpieza HTML recursiva sobre el objeto JSON.
    /// </summary>
    /// <author>Jose Vega</author>
    public static partial class JsonSanitizerHelpers
    {
        /// <summary>
        /// Reemplaza propiedades que contienen JSON serializado, parsea InformacionPrograma desde HTML
        /// a una estructura JSON y limpia HTML en todo el grafo.
        /// </summary>
        /// <author>Jose Vega</author>
        public static void NormalizarEstructura(JObject root)
        {
            if (root == null) return;

            ReemplazarSiJson(root, "EtiquetaBeneficiosInversion");
            ReemplazarSiJson(root, "EtiquetaFormasPago");
            ReemplazarSiJson(root, "EtiquetaTarifarios");
            ReemplazarSiJson(root, "EtiquetaDuracionHorarios");
            ReemplazarSiJson(root, "EtiquetaExpositores");
            ReemplazarSiJson(root, "EtiquetaBeneficios");

            var propInfo = root.Properties().FirstOrDefault(p => string.Equals(p.Name, "InformacionPrograma", System.StringComparison.OrdinalIgnoreCase));
            if (propInfo != null && propInfo.Value.Type == JTokenType.String)
            {
                var estructurado = InformacionProgramaHtmlParser.ParseHtmlToJson(propInfo.Value.ToString());
                propInfo.Value = JToken.Parse(estructurado);
            }

            LimpiarHtmlRecursivo(root);
        }
    }
}