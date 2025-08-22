using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Tools
{
    public class Parser
    {
        public string ParserCaracteres(string str)
        {
            StringBuilder cad = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                cad.Append(MapeadorReplace.MapVocal(str[i].ToString()));
            }
            return cad.ToString();
        }
    }

    
}
