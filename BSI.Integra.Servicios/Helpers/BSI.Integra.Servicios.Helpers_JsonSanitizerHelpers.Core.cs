using System;
using System.Linq;
using System.Collections;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace BSI.Integra.Servicios.Helpers
{
    /// <summary>
    /// Helper para saneamiento y normalización de JSON.
    /// Autor: Jose Vega
    /// </summary>
    public static partial class JsonSanitizerHelpers
    {
        /// <summary>
        /// Convierte una propiedad que contiene JSON serializado (string) en un JToken;
        /// si el contenido no es JSON válido, limpia el HTML básico del string;
        /// si la propiedad ya es un objeto/array, aplica limpieza recursiva.
        /// </summary>
        public static void ReemplazarSiJson(JObject obj, string nombreProp)
        {
            if (obj == null) return;
            var prop = GetPropertyCaseInsensitive(obj, nombreProp);
            if (prop == null) return;

            if (prop.Value == null) return;

            if (prop.Value.Type == JTokenType.String)
            {
                var valor = prop.Value.Value<string>()?.Trim();

                if (EsJson(valor))
                {
                    var jtok = ParsearJTokenSeguro(valor!);
                    if (jtok != null) prop.Value = jtok;
                    return;
                }

                prop.Value = LimpiarHtml(valor ?? string.Empty);
            }
            else
            {
                LimpiarHtmlRecursivo(prop.Value);
            }
        }

        /// <summary>
        /// Obtiene un JToken desde un JObject por nombre (case-insensitive).
        /// </summary>
        /// <returns>JToken encontrado o null.</returns>
        public static JToken? ObtenerToken(JObject obj, string nombre)
        {
            if (obj == null) return null;
            return GetPropertyCaseInsensitive(obj, nombre)?.Value;
        }

        // ---------------- Internals ----------------

        /// <summary>
        /// Busca una propiedad en un JObject de forma case-insensitive.
        /// </summary>
        private static JProperty? GetPropertyCaseInsensitive(JObject obj, string name)
        {
            return obj?.Properties().FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Determina si una cadena representa JSON (objeto o array) válido.
        /// </summary>
        /// <returns>True si la cadena es JSON válido; false en caso contrario.</returns>
        private static bool EsJson(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return false;
            var s = valor.Trim();
            if (!(s.StartsWith("{") && s.EndsWith("}")) && !(s.StartsWith("[") && s.EndsWith("]"))) return false;
            try { JToken.Parse(s); return true; } catch { return false; }
        }

        /// <summary>
        /// Parsea de forma segura una cadena JSON a JToken.
        /// </summary>
        /// <returns>JToken parseado o null si ocurre un error.</returns>
        private static JToken? ParsearJTokenSeguro(string json)
        {
            try { return JToken.Parse(json); } catch { return null; }
        }

        // ---------------- Simple HTML cleaning ----------------

        // Expresiones regulares compiladas para eliminación/normalización de contenido HTML/JS/CSS.
        private static readonly Regex RxScript = new("<script.*?</script>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex RxStyle = new("<style.*?</style>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex RxComment = new("<!--.*?-->", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex RxTags = new("<.*?>", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex RxSpaces = new(@"\s+", RegexOptions.Compiled);

        /// <summary>
        /// Elimina etiquetas y fragmentos comunes de HTML/CSS/JS y decodifica entidades HTML para obtener texto plano.
        /// </summary>
        /// <returns>Cadena limpia y normalizada.</returns>
        public static string LimpiarHtml(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return valor ?? string.Empty;
            var s = System.Net.WebUtility.HtmlDecode(valor);
            s = RxScript.Replace(s, string.Empty);
            s = RxStyle.Replace(s, string.Empty);
            s = RxComment.Replace(s, string.Empty);
            s = RxTags.Replace(s, " ");
            s = RxSpaces.Replace(s, " ").Trim();
            return s;
        }

        /// <summary>
        /// Recorre recursivamente un objeto/colección y aplica LimpiarHtml a todos los valores string encontrados.
        /// </summary>
        public static void LimpiarHtmlRecursivo(object? obj)
        {
            if (obj == null) return;

            switch (obj)
            {
                case string:
                    return;

                case JValue jv:
                    if (jv.Type == JTokenType.String)
                        jv.Value = LimpiarHtml(jv.Value<string>() ?? string.Empty);
                    return;

                case JObject jo:
                    foreach (var p in jo.Properties())
                    {
                        if (p.Value is JValue jsv && jsv.Type == JTokenType.String)
                            jsv.Value = LimpiarHtml(jsv.Value<string>() ?? string.Empty);
                        else
                            LimpiarHtmlRecursivo(p.Value);
                    }
                    return;

                case JArray ja:
                    foreach (var it in ja) LimpiarHtmlRecursivo(it);
                    return;

                case IDictionary dict:
                    var keys = new System.Collections.ArrayList(dict.Keys);
                    foreach (var key in keys)
                    {
                        var val = dict[key];
                        if (val is string sv) dict[key] = LimpiarHtml(sv);
                        else LimpiarHtmlRecursivo(val);
                    }
                    return;

                case System.Collections.IEnumerable enumerable:
                    foreach (var item in enumerable) LimpiarHtmlRecursivo(item);
                    return;

                default:
                    var tipo = obj.GetType();
                    if (tipo.IsClass && tipo != typeof(string))
                    {
                        var props = tipo.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        foreach (var p in props)
                        {
                            if (!p.CanRead) continue;
                            var val = p.GetValue(obj, null);
                            if (val == null) continue;

                            if (p.PropertyType == typeof(string))
                            {
                                if (p.CanWrite) p.SetValue(obj, LimpiarHtml((string)val));
                            }
                            else
                            {
                                LimpiarHtmlRecursivo(val);
                            }
                        }
                    }
                    return;
            }
        }

        /// <summary>
        /// Normaliza secuencias de espacios reduciéndolas a un único espacio y eliminando espacios al inicio/fin.
        /// </summary>
        /// <returns>Cadena con espacios normalizados.</returns>
        public static string NormalizarEspacios(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return string.Empty;
            return Regex.Replace((s ?? "").Trim(), @"\s+", " ");
        }
    }
}