using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using HtmlAgilityPack;

namespace BSI.Integra.Aplicacion.Transversal.SCode.Helper
{
    public static class HtmlToJsonHelper
    {
        public static List<object>? ConvertHtmlToJson(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return new List<object>();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var contentList = new List<object>();

            foreach (var node in doc.DocumentNode.ChildNodes)
            {
                if (node.NodeType != HtmlNodeType.Element)
                    continue;

                string type = "";
                string text = "";
                List<string>? listItems = null;

                // --- Procesar párrafos ---
                if (node.Name == "p")
                {
                    string inner = HtmlEntity.DeEntitize(node.InnerText.Trim());
                    if (string.IsNullOrWhiteSpace(inner)) continue;

                    var strong = node.SelectSingleNode(".//strong");
                    if (strong != null)
                    {
                        if (inner.ToLower().StartsWith("nota"))
                            type = "note";
                        else
                            type = "title";
                    }
                    else
                    {
                        type = "paragraph";
                    }

                    text = inner;
                }
                // --- Procesar listas ---
                else if (node.Name == "ul" || node.Name == "ol")
                {
                    type = "list";
                    listItems = node.SelectNodes(".//li")
                        ?.Select(li => HtmlEntity.DeEntitize(li.InnerText.Trim()))
                        .Where(t => !string.IsNullOrEmpty(t))
                        .ToList();
                }

                if (string.IsNullOrEmpty(type))
                    continue;

                // Agregar al resultado
                if (type == "list")
                {
                    contentList.Add(new ScaffoldHtmlDTO
                    {
                        Type = "list",
                        Items = listItems ?? new List<string>()
                    });
                }
                else
                {
                    contentList.Add(new ScaffoldHtmlDTO
                    {
                        Type = type,
                        Text = text
                    });
                }
            }

            return contentList;
        }
    }
}
