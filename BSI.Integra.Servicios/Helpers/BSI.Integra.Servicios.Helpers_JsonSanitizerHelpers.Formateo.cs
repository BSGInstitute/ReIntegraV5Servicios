using System.Net;
using System.Text.RegularExpressions;

namespace BSI.Integra.Servicios.Helpers
{
    /// <summary>
    /// Formatea HTML a texto plano legible, preservando saltos de párrafo y normalizando espacios.
    /// </summary>
    /// <author>Jose Vega</author>
    public static partial class JsonSanitizerHelpers
    {
        // Formatea texto HTML a texto plano legible (mantiene saltos de párrafo)
        private static readonly Regex RxScript2 = new("<script.*?</script>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex RxStyle2 = new("<style.*?</style>", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex RxComment2 = new("<!--.*?-->", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex RxBlockToNl = new(@"<(br|/p|p|/div|div|/li|li|/h\d|h\d|/tr|tr|/ul|ul|/ol|ol)\b[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex RxAnyTag = new("<.*?>", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex RxCommentStyle = new(@"/\*.*?\*/", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex RxCssRule = new(@"(?s)(?:^|\s)([.#][A-Za-z0-9\-\s]+)\s*\{.*?\}", RegexOptions.Compiled);
        private static readonly Regex RxJsTrace1 = new(@"document\.[^\r\n]+", RegexOptions.Compiled);
        private static readonly Regex RxJsTrace2 = new(@"addEventListener\([^\r\n]+", RegexOptions.Compiled);
        private static readonly Regex RxNL2 = new(@"(\r?\n)\s*(\r?\n)+", RegexOptions.Compiled);
        private static readonly Regex RxNL3 = new(@"(\r?\n){3,}", RegexOptions.Compiled);
        private static readonly Regex RxSpaces2 = new(@"[ \t]+", RegexOptions.Compiled);

        private static readonly Regex RxCursos = new(@"\s*(Curso\s+\d{3,5}\s*:)", RegexOptions.Compiled);
        private static readonly Regex RxExamen = new(@"\s*(Examen\s+\d{2,3}-\d{2,3}\s*:?)", RegexOptions.Compiled);
        private static readonly Regex RxCert = new(@"\s*(Certificaci[oó]n\s*:)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RxMetod = new(@"\s*(Metodolog[ií]a\s*:)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RxModal = new(@"\s*(Modalidades\s*:)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RxPrecio = new(@"\s*(Precio\s+Normal\s*:|Contado\s*:|Cr[eé]dito\s*:)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RxNota = new(@"\s*(Nota\s*:)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        /// <summary>
        /// Convierte HTML a texto plano legible, preservando saltos de párrafo y aplicando normalizaciones.
        /// </summary>
        /// <author>Jose Vega</author>
        public static string FormatearInformacionPrograma(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return string.Empty;

            var s = WebUtility.HtmlDecode(valor);
            s = RxScript2.Replace(s, string.Empty);
            s = RxStyle2.Replace(s, string.Empty);
            s = RxComment2.Replace(s, string.Empty);

            s = RxBlockToNl.Replace(s, "\n");
            s = RxAnyTag.Replace(s, " ");

            s = RxCommentStyle.Replace(s, string.Empty);
            s = RxCssRule.Replace(s, string.Empty);
            s = RxJsTrace1.Replace(s, string.Empty);
            s = RxJsTrace2.Replace(s, string.Empty);

            s = s.Replace(" ▾ ", ":\n");

            s = RxCursos.Replace(s, "\n$1");
            s = RxExamen.Replace(s, "\n$1");
            s = RxCert.Replace(s, "\n$1");
            s = RxMetod.Replace(s, "\n$1");
            s = RxModal.Replace(s, "\n$1");
            s = RxPrecio.Replace(s, "\n$1");
            s = RxNota.Replace(s, "\n$1");

            s = RxSpaces2.Replace(s, " ");
            s = RxNL2.Replace(s, "\n");
            s = RxNL3.Replace(s, "\n\n");

            return s.Trim();
        }
    }
}