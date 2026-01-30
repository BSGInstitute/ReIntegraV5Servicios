using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BSI.Integra.Servicios.Helpers.InformacionProgramaEstructurada
{
    /// <summary>
    /// Parsea HTML o texto plano de "Información de programa" en JSON estructurado por secciones y bloques.
    /// Autor: Jose Vega
    /// </summary>
    public static class InformacionProgramaHtmlParser
    {
        private static readonly string[] KnownHeadings = {
            "Modalidades","Inversión","Inversion","Beneficios","Presentación","Presentacion","Objetivos",
            "Público Objetivo","Publico Objetivo","Prerrequisitos","Estructura Curricular",
            "Duración y Horarios","Duracion y Horarios","Certificación","Certificacion","Expositores",
            "Material del Curso","Pautas Complementarias","Bibliografía","Bibliografia"
        };

        private static readonly Regex RxHeadingWord = new(@"\b(" + string.Join("|", KnownHeadings.Select(Regex.Escape)) + @")\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex RxTablePipe = new(@"\|", RegexOptions.Compiled);
        private static readonly Regex RxMultipleSpacesCols = new(@"\s{2,}", RegexOptions.Compiled);
        private static readonly Regex RxNewlines = new(@"\r\n|\r|\n", RegexOptions.Compiled);

        /// <summary>
        /// Parsea el input (HTML o texto plano) y devuelve un JSON con la estructura de secciones.
        /// </summary>
        /// <returns>JSON con las secciones estructuradas.</returns>
        public static string ParseHtmlToJson(string input)
        {
            var secciones = string.IsNullOrWhiteSpace(input)
                ? new List<SeccionJson>()
                : LooksLikeHtml(input) ? ParseHtmlSections(input) : ParsePlainTextSections(input);

            var infoFinal = TransformarEstructura(secciones);
            return JsonConvert.SerializeObject(
                new { Secciones = infoFinal.Secciones },
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented, ContractResolver = new CamelCasePropertyNamesContractResolver() }
            );
        }

        /// <summary>
        /// Detecta si la cadena de entrada parece contener HTML.
        /// </summary>
        /// <returns>True si parece HTML; false en caso contrario.</returns>
        private static bool LooksLikeHtml(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            return s.Contains('<') && s.Contains('>') || Regex.IsMatch(s, @"<h\d|<p\b|<table\b|<\/h\d|<\/p|<\/table", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Extrae secciones del HTML y las convierte en bloques intermedios.
        /// </summary>
        /// <returns>Lista de secciones intermedias.</returns>
        private static List<SeccionJson> ParseHtmlSections(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var secciones = new List<SeccionJson>();
            SeccionJson? current = null;

            var blockElements = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "h1","h2","h3","h4","h5","h6","p","ul","ol","table","div","section","article","header","footer"
            };

            foreach (var child in doc.DocumentNode.ChildNodes)
            {
                if (child.NodeType != HtmlNodeType.Element) continue;
                ProcessNode(child, secciones, ref current, blockElements);
            }

            AddCurrentSection(secciones, ref current);

            if (secciones.Count == 0)
            {
                var general = new SeccionJson("General");
                foreach (var p in doc.DocumentNode.SelectNodes("//p") ?? Enumerable.Empty<HtmlNode>())
                    general.Contenido.Add(ContentBlock.Parrafo(NormalizeSpaces(HtmlEntity.DeEntitize(p.InnerText ?? ""))));
                foreach (var ul in doc.DocumentNode.SelectNodes("//ul|//ol") ?? Enumerable.Empty<HtmlNode>())
                {
                    var lis = ul.SelectNodes(".//li");
                    if (lis != null)
                    {
                        var items = lis.Select(li => NormalizeSpaces(HtmlEntity.DeEntitize(li.InnerText ?? ""))).Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
                        if (items.Count > 0) general.Contenido.Add(ContentBlock.Lista(items));
                    }
                }
                foreach (var t in doc.DocumentNode.SelectNodes("//table") ?? Enumerable.Empty<HtmlNode>())
                {
                    var parsed = ParseHtmlTable(t);
                    if (parsed != null)
                    {
                        var (headers, rows) = parsed.Value;
                        general.Contenido.Add(ContentBlock.Tabla(headers, rows));
                    }
                }
                if (general.Contenido.Count > 0) secciones.Add(general);
            }

            DeduplicateSections(secciones);
            return secciones;
        }

        /// <summary>
        /// Procesa un nodo HTML (recursivo) y añade bloques a las secciones según el tipo de nodo.
        /// </summary>
        private static void ProcessNode(HtmlNode node, List<SeccionJson> secciones, ref SeccionJson? current, HashSet<string> blockElements)
        {
            var name = node.Name.ToLowerInvariant();

            // Secciones: h1..h4
            if (name is "h1" or "h2" or "h3" or "h4")
            {
                AddCurrentSection(secciones, ref current);
                current = new SeccionJson(NormalizeSpaces(HtmlEntity.DeEntitize(node.InnerText ?? "")));
                return;
            }

            // Sub-encabezados h5/h6 -> subtitulo
            if (name is "h5" or "h6")
            {
                current ??= new SeccionJson();
                var txt = NormalizeSpaces(HtmlEntity.DeEntitize(node.InnerText ?? ""));
                if (!string.IsNullOrWhiteSpace(txt)) current.Contenido.Add(ContentBlock.Subtitulo(txt));
                return;
            }

            // Párrafo: procesar múltiples <strong> dentro del mismo <p>
            if (name == "p")
            {
                current ??= new SeccionJson();
                var pNode = node;
                if (string.IsNullOrWhiteSpace(HtmlEntity.DeEntitize(pNode.InnerText ?? ""))) return;

                var buffer = "";
                var childNodes = pNode.ChildNodes.ToList();
                foreach (var child in childNodes)
                {
                    if (child.NodeType == HtmlNodeType.Text)
                    {
                        var t = NormalizeSpaces(HtmlEntity.DeEntitize(child.InnerText ?? ""));
                        if (!string.IsNullOrWhiteSpace(t))
                        {
                            if (buffer.Length > 0) buffer += " ";
                            buffer += t;
                        }
                        continue;
                    }

                    if (child.NodeType == HtmlNodeType.Element)
                    {
                        var childName = child.Name.ToLowerInvariant();
                        if (childName == "strong" || childName == "b")
                        {
                            if (!string.IsNullOrWhiteSpace(buffer))
                            {
                                current.Contenido.Add(ContentBlock.Parrafo(buffer));
                                buffer = "";
                            }

                            var strongText = NormalizeSpaces(HtmlEntity.DeEntitize(child.InnerText ?? "")).Trim();
                            var st = strongText.TrimEnd(':');
                            if (!string.IsNullOrWhiteSpace(st))
                                current.Contenido.Add(ContentBlock.Subtitulo(st));
                            continue;
                        }

                        if (childName == "br")
                        {
                            // Tratar br como salto de párrafo (vaciar buffer)
                            if (!string.IsNullOrWhiteSpace(buffer))
                            {
                                current.Contenido.Add(ContentBlock.Parrafo(buffer));
                                buffer = "";
                            }
                            continue;
                        }

                        var inlineText = NormalizeSpaces(HtmlEntity.DeEntitize(child.InnerText ?? ""));
                        if (!string.IsNullOrWhiteSpace(inlineText))
                        {
                            if (buffer.Length > 0) buffer += " ";
                            buffer += inlineText;
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(buffer))
                {
                    current.Contenido.Add(ContentBlock.Parrafo(buffer));
                    buffer = "";
                }

                return;
            }

            // Listas
            if (name is "ul" or "ol")
            {
                current ??= new SeccionJson();
                var lis = node.SelectNodes(".//li");
                if (lis != null)
                {
                    var items = new List<string>();
                    foreach (var li in lis)
                    {

                        var innerP = li.SelectSingleNode(".//p");
                        if (innerP != null)
                            items.Add(NormalizeSpaces(HtmlEntity.DeEntitize(innerP.InnerText ?? "")));
                        else
                            items.Add(NormalizeSpaces(HtmlEntity.DeEntitize(li.InnerText ?? "")));
                    }
                    items = items.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
                    if (items.Count > 0)
                    {

                        if (current.Contenido.Count > 0 && current.Contenido.Last().Tipo == "lista")
                        {
                            var lastList = current.Contenido.Last();
                            if (lastList.Items == null) lastList.Items = new List<string>();
                            lastList.Items.AddRange(items);
                        }
                        else
                        {
                            current.Contenido.Add(ContentBlock.Lista(items));
                        }
                    }
                }
                return;
            }

            // Tablas: procesar completo y no descender en celdas
            if (name == "table")
            {
                current ??= new SeccionJson();
                var parsed = ParseHtmlTable(node);
                if (parsed != null)
                {
                    var (headers, rows) = parsed.Value;
                    var headersLow = string.Join("|", headers).ToLowerInvariant();

                    if (headersLow.Contains("versi") && headersLow.Contains("benefici"))
                    {
                        foreach (var fila in node.SelectNodes(".//tr")?.Skip(1) ?? Enumerable.Empty<HtmlNode>())
                        {
                            var cells = fila.SelectNodes(".//td|.//th");
                            if (cells == null || cells.Count == 0) continue;

                            var version = NormalizeSpaces(HtmlEntity.DeEntitize(cells[0].InnerText ?? ""));
                            var benefitsCell = cells.Count > 1 ? cells[1] : null;
                            var items = new List<string>();
                            string strongText = null;
                            if (benefitsCell != null)
                            {
                                var strongNode = benefitsCell.SelectSingleNode(".//strong|.//b");
                                if (strongNode != null)
                                {
                                    strongText = NormalizeSpaces(HtmlEntity.DeEntitize(strongNode.InnerText ?? ""));
                                    strongNode.Remove();
                                }
                                var liNodes = benefitsCell.SelectNodes(".//li");
                                if (liNodes?.Count > 0)
                                {
                                    items.AddRange(liNodes.Select(li => NormalizeSpaces(HtmlEntity.DeEntitize(li.InnerText ?? ""))).Where(t => !string.IsNullOrWhiteSpace(t)));
                                }
                                else
                                {
                                    var raw = NormalizeSpaces(HtmlEntity.DeEntitize(benefitsCell.InnerText ?? ""));
                                    if (raw.StartsWith("(*)") || raw.StartsWith("* "))
                                        current.Contenido.Add(ContentBlock.Parrafo(raw));
                                    else if (!string.IsNullOrWhiteSpace(raw))
                                        current.Contenido.Add(ContentBlock.Subtitulo(raw));
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(version)) current.Contenido.Add(ContentBlock.Subtitulo(version));
                            if (!string.IsNullOrWhiteSpace(strongText)) current.Contenido.Add(ContentBlock.Subtitulo(strongText));
                            if (items.Count > 0) current.Contenido.Add(ContentBlock.Lista(items));
                        }
                    }
                    else
                    {
                        current.Contenido.Add(ContentBlock.Tabla(headers, rows));
                    }
                }
                return;
            }

            // strong/b fuera de <p>: tratar como subtítulo y capturar texto siguiente (texto plano y <p> adyacente)
            if (name == "strong" || name == "b")
            {

                if (node.ParentNode != null && node.ParentNode.Name.Equals("p", StringComparison.OrdinalIgnoreCase))
                    return;

                current ??= new SeccionJson();

                var strongText = NormalizeSpaces(HtmlEntity.DeEntitize(node.InnerText ?? ""));
                var st = strongText.Trim().TrimEnd(':');
                if (!string.IsNullOrWhiteSpace(st))
                    current.Contenido.Add(ContentBlock.Subtitulo(st));
                var collected = new List<string>();
                var sib = node.NextSibling;
                while (sib != null)
                {
                    if (sib.NodeType == HtmlNodeType.Text)
                    {
                        var t = NormalizeSpaces(HtmlEntity.DeEntitize(sib.InnerText ?? ""));
                        if (!string.IsNullOrWhiteSpace(t)) collected.Add(t);
                        sib = sib.NextSibling;
                        continue;
                    }

                    if (sib.NodeType == HtmlNodeType.Element)
                    {
                        var sibName = sib.Name.ToLowerInvariant();

                        if (sibName == "p")
                        {
                            var t = NormalizeSpaces(HtmlEntity.DeEntitize(sib.InnerText ?? ""));
                            if (!string.IsNullOrWhiteSpace(t)) collected.Add(t);
                            sib = sib.NextSibling;
                            continue;
                        }

                        if (blockElements.Contains(sibName))
                            break;

                        var inlineText = NormalizeSpaces(HtmlEntity.DeEntitize(sib.InnerText ?? ""));
                        if (!string.IsNullOrWhiteSpace(inlineText)) collected.Add(inlineText);
                        sib = sib.NextSibling;
                        continue;
                    }

                    sib = sib.NextSibling;
                }

                if (collected.Count > 0)
                {
                    var para = NormalizeSpaces(string.Join(" ", collected));
                    if (!string.IsNullOrWhiteSpace(para))
                        current.Contenido.Add(ContentBlock.Parrafo(para));
                }
                return;
            }

            if (!string.IsNullOrWhiteSpace(node.InnerHtml))
            {
                foreach (var child in node.ChildNodes)
                {
                    if (child.NodeType != HtmlNodeType.Element) continue;
                    ProcessNode(child, secciones, ref current, blockElements);
                }
            }
        }

        /// <summary>
        /// Parsea texto plano en secciones y bloques (encabezados, tablas, listas, párrafos).
        /// </summary>
        /// <returns>Lista de secciones intermedias.</returns>
        private static List<SeccionJson> ParsePlainTextSections(string text)
        {
            var s = HtmlEntity.DeEntitize(text ?? "").Trim();
            if (!s.Contains("\n") && RxHeadingWord.IsMatch(s)) s = RxHeadingWord.Replace(s, m => "\n" + m.Value.Trim());
            s = Regex.Replace(s, @"\bTabla\s*:\s*", "\nTabla:\n", RegexOptions.IgnoreCase);
            var rawLines = RxNewlines.Split(s).Select(NormalizeSpaces).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

            var secciones = new List<SeccionJson>();
            SeccionJson? current = null;

            int i = 0;
            while (i < rawLines.Count)
            {
                var line = rawLines[i];
                var headingMatch = RxHeadingWord.Match(line);
                if (headingMatch.Success && (line.Equals(headingMatch.Value, StringComparison.InvariantCultureIgnoreCase) || line.StartsWith(headingMatch.Value, StringComparison.InvariantCultureIgnoreCase)))
                {
                    AddCurrentSection(secciones, ref current);
                    current = new SeccionJson(ToTitleCase(headingMatch.Value));
                    var remainder = line.Substring(headingMatch.Index + headingMatch.Length).Trim();
                    if (!string.IsNullOrWhiteSpace(remainder)) rawLines.Insert(i + 1, remainder);
                    i++; continue;
                }
                current ??= new SeccionJson("General");

                if (RxTablePipe.IsMatch(line) || (RxMultipleSpacesCols.IsMatch(line) && LooksLikeTableLine(line)))
                {
                    var tableLines = new List<string> { line };
                    int j = i + 1;
                    while (j < rawLines.Count && (RxTablePipe.IsMatch(rawLines[j]) || RxMultipleSpacesCols.IsMatch(rawLines[j]) || LooksLikeTableLine(rawLines[j])))
                        tableLines.Add(rawLines[j++]);
                    i = j - 1;
                    var parsed = ParseTableFromTextLines(tableLines);
                    if (parsed != null)
                    {
                        var (headers, rows) = parsed.Value;
                        current.Contenido.Add(ContentBlock.Tabla(headers, rows));
                    }
                    else current.Contenido.Add(ContentBlock.Parrafo(string.Join(" ", tableLines)));
                }
                else if (Regex.IsMatch(line, @"^\s*(•|\-|\*|\d+\.)\s+"))
                {
                    var items = new List<string>();
                    int j = i;
                    while (j < rawLines.Count && Regex.IsMatch(rawLines[j], @"^\s*(•|\-|\*|\d+\.)\s+"))
                    {
                        var item = Regex.Replace(rawLines[j], @"^\s*(•|\-|\*|\d+\.)\s+", "").Trim();
                        if (!string.IsNullOrWhiteSpace(item)) items.Add(NormalizeSpaces(item));
                        j++;
                    }
                    i = j - 1;
                    if (items.Count > 0) current.Contenido.Add(ContentBlock.Lista(items));
                }
                else
                {
                    var paraBuilder = new List<string> { line };
                    int k = i + 1;
                    while (k < rawLines.Count && !RxHeadingWord.IsMatch(rawLines[k]) && !RxTablePipe.IsMatch(rawLines[k]) && !Regex.IsMatch(rawLines[k], @"^\s*(•|\-|\*|\d+\.)\s+"))
                    {
                        if (RawLineLooksLikeHeadingOnly(rawLines[k])) break;
                        paraBuilder.Add(rawLines[k]);
                        k++;
                    }
                    i = k - 1;
                    var paragraph = NormalizeSpaces(string.Join(" ", paraBuilder));
                    if (!string.IsNullOrWhiteSpace(paragraph)) current.Contenido.Add(ContentBlock.Parrafo(paragraph));
                }
                i++;
            }
            AddCurrentSection(secciones, ref current);
            DeduplicateSections(secciones);
            return secciones;
        }

        /// <summary>
        /// Parsea una tabla HTML y devuelve encabezados y filas.
        /// </summary>
        /// <returns>Tupla con headers y rows, o null si no se detecta una tabla válida.</returns>
        private static (List<string> Headers, List<List<string>> Rows)? ParseHtmlTable(HtmlNode tableNode)
        {
            var rowsNodes = tableNode.SelectNodes(".//tr");
            if (rowsNodes == null || rowsNodes.Count == 0) return null;
            var headers = rowsNodes[0].SelectNodes(".//th|.//td")?.Select(n => NormalizeSpaces(HtmlEntity.DeEntitize(n.InnerText ?? ""))).ToList() ?? new();
            var dataRows = rowsNodes.Skip(1).Select(tr =>
                tr.SelectNodes(".//td|.//th")?.Select(c => NormalizeSpaces(HtmlEntity.DeEntitize(c.InnerText ?? ""))).ToList() ?? new List<string>()
            ).ToList();
            return (headers, dataRows);
        }

        /// <summary>
        /// Intenta parsear tablas a partir de líneas de texto separadas (pipes o espacios múltiples).
        /// </summary>
        /// <returns>Tupla con headers y rows, o null si no se detecta una tabla válida.</returns>
        private static (List<string> Headers, List<List<string>> Rows)? ParseTableFromTextLines(List<string> lines)
        {
            if (lines.Count == 0) return null;
            if (lines.Any(RxTablePipe.IsMatch))
            {
                var rows = lines.Select(l => l.Trim().Trim('|').Split('|').Select(p => NormalizeSpaces(p.Trim())).Where(p => p != "").ToList()).Where(r => r.Count > 0).ToList();
                return rows.Count == 0 ? null : (rows[0], rows.Skip(1).ToList());
            }
            var splitRows = lines.Select(l => RxMultipleSpacesCols.Split(l).Select(p => NormalizeSpaces(p.Trim())).Where(p => p != "").ToList()).ToList();
            if (splitRows.Count == 0) return null;
            var first = splitRows[0];
            bool firstIsHeader = first.Any(c => Regex.IsMatch(c, @"\b(Versi[oó]n|Precio|Modalidad|Centro|Fecha|Monto|Descripción|CONCEPTO|Nº|Contado|Cr[eé]dito)\b", RegexOptions.IgnoreCase));
            if (firstIsHeader && splitRows.Count >= 2) return (first, splitRows.Skip(1).ToList());
            var colCount = splitRows[0].Count;
            return splitRows.All(r => r.Count == colCount) ? (splitRows[0], splitRows.Skip(1).ToList()) : (first, splitRows.Skip(1).ToList());
        }

        /// <summary>
        /// Transforma la estructura intermedia de secciones en el modelo final usado para serializar.
        /// </summary>
        /// <returns>Modelo final con secciones y contenido listo para JSON.</returns>
        private static InformacionProgramaFinal TransformarEstructura(List<SeccionJson> secciones)
        {
            var info = new InformacionProgramaFinal();
            foreach (var sec in secciones)
            {
                var sf = new SeccionFinal { Titulo = sec.Titulo, Contenido = new List<ContenidoFinal>() };
                foreach (var cb in sec.Contenido)
                {
                    switch (cb.Tipo)
                    {
                        case "parrafo":
                            if (!string.IsNullOrWhiteSpace(cb.Texto))
                                sf.Contenido.Add(new ContenidoFinal { Tipo = "parrafo", Texto = cb.Texto });
                            break;
                        case "subtitulo":
                            if (!string.IsNullOrWhiteSpace(cb.Texto))
                                sf.Contenido.Add(new ContenidoFinal { Tipo = "subtitulo", Texto = cb.Texto });
                            break;
                        case "lista":
                            if (cb.Items != null && cb.Items.Count > 0)
                                sf.Contenido.Add(new ContenidoFinal { Tipo = "lista", Items = new List<string>(cb.Items) });
                            break;
                        case "tabla":
                            if (cb.Headers != null && cb.Rows != null)
                                sf.Contenido.Add(new ContenidoFinal { Tipo = "tabla", Tabla = new TablaJson { Headers = cb.Headers, Rows = cb.Rows } });
                            break;
                    }
                }
                info.Secciones.Add(sf);
            }
            return info;
        }

        /// <summary>
        /// Elimina duplicados dentro de cada sección manteniendo el orden y aplicando reglas heurísticas.
        /// </summary>
        private static void DeduplicateSections(List<SeccionJson> secciones)
        {
            foreach (var sec in secciones)
            {
                var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var newContent = new List<ContentBlock>();
                ContentBlock? prevPar = null;
                foreach (var cb in sec.Contenido)
                {
                    if (cb.Tipo == "parrafo")
                    {
                        var norm = NormalizeForComparison(cb.Texto ?? "");
                        if (IsIntroParagraph(cb.Texto ?? "")) { newContent.Add(cb); prevPar = cb; continue; }
                        if (!string.IsNullOrWhiteSpace(norm) && seen.Contains(norm)) continue;
                        if (prevPar != null && string.Equals(NormalizeForComparison(prevPar.Texto ?? ""), norm, StringComparison.OrdinalIgnoreCase)) continue;
                        newContent.Add(cb); prevPar = cb;
                        seen.Add(norm);
                    }
                    else
                    {
                        if (cb.Items != null) foreach (var it in cb.Items) seen.Add(NormalizeForComparison(it));
                        if (cb.Headers != null) foreach (var h in cb.Headers) seen.Add(NormalizeForComparison(h));
                        if (cb.Rows != null) foreach (var row in cb.Rows) foreach (var cell in row) seen.Add(NormalizeForComparison(cell));
                        newContent.Add(cb); prevPar = null;
                    }
                }
                sec.Contenido = newContent;
            }
        }

        // ---------- Utilidades ----------
        private static string NormalizeSpaces(string s) => string.IsNullOrWhiteSpace(s) ? "" : Regex.Replace(s.Trim(), @"\s+", " ");
        private static string NormalizeForComparison(string s) => string.IsNullOrWhiteSpace(s) ? "" : Regex.Replace(s.Trim(), @"\s+", " ").ToLowerInvariant().TrimEnd('.', ':', ';');
        private static bool IsIntroParagraph(string s) => !string.IsNullOrWhiteSpace(s) && (
            s.Trim().ToLowerInvariant().StartsWith("al finalizar") ||
            s.Trim().ToLowerInvariant().StartsWith("el curso está dirigido a") ||
            s.Trim().ToLowerInvariant().StartsWith("para llevar el curso") ||
            s.Trim().ToLowerInvariant().StartsWith("para obtener la certificación")
        );
        private static bool RawLineLooksLikeHeadingOnly(string line) => !string.IsNullOrWhiteSpace(line) && line.Length < 50 && RxHeadingWord.IsMatch(line);
        private static string ToTitleCase(string s) => string.IsNullOrWhiteSpace(s)
            ? s ?? "" : string.Join(' ', s.Trim().ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(str => char.ToUpperInvariant(str[0]) + str.Substring(1)));

        private static bool LooksLikeTableLine(string line)
        {
            if (Regex.IsMatch(line, @"\b(MXN\$|S\/|US\$|\bPrecio\b|\bVersi[oó]n\b|\bContado\b|\bCr[eé]dito\b)\b", RegexOptions.IgnoreCase)) return true;
            return Regex.Matches(line, @"\d{1,4}").Count >= 2;
        }

        /// <summary>
        /// Añade la sección en curso a la lista si contiene contenido y reinicia el puntero.
        /// </summary>
        private static void AddCurrentSection(List<SeccionJson> secciones, ref SeccionJson? current)
        {
            if (current != null && current.Contenido.Count > 0)
                secciones.Add(current);
            current = null;
        }

        // ---------- POCOs ----------
        private class SeccionJson
        {
            public string Titulo { get; set; }
            public List<ContentBlock> Contenido { get; set; }
            public SeccionJson(string titulo = "") { Titulo = titulo; Contenido = new List<ContentBlock>(); }
        }

        private class ContentBlock
        {
            public string Tipo { get; set; } = "";
            public string? Texto { get; set; }
            public List<string>? Items { get; set; }
            public List<string>? Headers { get; set; }
            public List<List<string>>? Rows { get; set; }

            public static ContentBlock Parrafo(string texto) => new() { Tipo = "parrafo", Texto = texto };
            public static ContentBlock Subtitulo(string texto) => new() { Tipo = "subtitulo", Texto = texto };
            public static ContentBlock Lista(List<string> items) => new() { Tipo = "lista", Items = items };
            public static ContentBlock Tabla(List<string> headers, List<List<string>> rows) => new() { Tipo = "tabla", Headers = headers, Rows = rows };
        }

        private class InformacionProgramaFinal
        {
            public List<SeccionFinal> Secciones { get; set; } = new();
        }
        private class SeccionFinal
        {
            public string Titulo { get; set; } = "";
            public List<ContenidoFinal> Contenido { get; set; } = new();
        }
        private class ContenidoFinal
        {
            // tipo: "parrafo" | "subtitulo" | "lista" | "tabla"
            public string Tipo { get; set; } = "";
            public string? Texto { get; set; }
            public List<string>? Items { get; set; }
            public TablaJson? Tabla { get; set; }
        }
        private class TablaJson
        {
            public List<string> Headers { get; set; } = new();
            public List<List<string>> Rows { get; set; } = new();
        }
    }
}