using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Tools
{
    public class MapeadorReplace
    {
        private static Dictionary<string, string> VocalesMap = null;
        private static Dictionary<string, string> OrigenMap = null;

        public static string MapVocal(string str)
        {
            if (VocalesMap == null)
            {
                InitVocales();
            }
            if (!VocalesMap.ContainsKey(str))
                return str;
            else return VocalesMap[str];
        }

        /// <summary>
        /// Apartir del nombre del proveedor se obtiene y retorna el codigo que le corresponde de acuerdo al dicionario OrigenMap
        /// </summary>
        /// <param name="str">nombre del proveedor</param>
        /// <returns>Cadena con el origen del mailing</returns>
        public static string MapOrigen(string str)
        {
            if (OrigenMap == null)
            {
                InitOrigen();
            }
            if (str.Equals("mailing descuento"))
            {
                return OrigenMap["mailing descuento"];
            }
            else if (str.Contains("mail"))
            {
                return OrigenMap["mailing"];
            }
            else if (!OrigenMap.ContainsKey(str))
                return "RVR1";
            else return OrigenMap[str];
        }

        /// <summary>
        /// retorna las 3 primeras letras del nombre del pais
        /// en caso que el pais sea El Salvador las tres primeras letras seran SAL
        /// </summary>
        /// <param name="str">nombre del pais</param>
        /// <returns>Cadena con los tres digitos de origen del pais</returns>
        public static string MapOrigenPais(string str)
        {
            str = str.ToLower();
            if (str.Equals("el salvador"))
            {
                return "SAL";
            }
            else return str.Substring(0, 3).ToUpper();
        }

        /// <summary>
        /// Recibe un numero telefonico y extrae solo los numeros y elimina los demas caracteres
        /// </summary>
        /// <param name="numero">numero telefonico o de celular</param>
        /// <returns>Cadena con el numero de telefono celular eliminado 0 a la izquierda</returns>
        public static string MapTelefonoCelular(string numero)
        {
            // Solo numeros
            StringBuilder num = new StringBuilder();

            for (var i = 0; i < numero.Length; i++)
            {
                if (Char.IsNumber(numero[i]))
                {
                    num.Append(numero[i].ToString());
                }
            }
            return num.ToString();
        }

        /// <summary>
        /// Diccionario para reemplazar los caracteres extraños y/o tildes por su equivalente
        /// </summary>
        private static void InitVocales()
        {
            VocalesMap = new Dictionary<string, string>
            {
                { "á", "a" },
                { "é", "e" },
                { "í", "i" },
                { "ó", "o" },
                { "ú", "u" },
                { "Á", "A" },
                { "É", "E" },
                { "Í", "I" },
                { "Ó", "O" },
                { "Ú", "U" },
                { "Ñ", "N" },
                { "ñ", "n" },
                { ",", String.Empty },
                { ".", String.Empty }
            };
        }

        /// <summary>
        /// Lista de proveedores asociados con sus respectivos codigos
        /// </summary>

        public static void InitOrigen()
        {
            OrigenMap = new Dictionary<string, string>
            {
                { "formulario facebook", "PFBK1" },
                { "formulario facebook extranjero", "PFBK1-2" },
                { "correo exalumnos", "MLG3-3" },
                { "facebook", "FBK1" },
                { "adwords", "ADS1" },
                { "instagram", "ISG1" },
                { "twitter", "TWT1" },
                { "mailing", "MLG3" },
                { "portal web", "ORG2" },
                { "adwords busqueda", "ADSB1" },
                { "adwords gmail", "ADSG1" },
                { "formulario facebook peru con precio", "FBK1-2" },
                { "formulario facebook peru sin precio", "FBK1" },
                { "facebook con precio", "FBK1-2" },
                { "linkedin", "LKD1" },
                { "bumeran", "BUM3" },
                { "trabajando.com", "TBJ3" },
                { "universia", "UNV3" },
                { "laborum", "LBM3" },
                { "adwords display", "ADSD1" },
                { "computrabajo", "CPT3" },
                { "organico", "ORG2" },
                { "organico - pruebagratis", "ORG2-5" },
                { "organico - matricularme", "ORG2-4" },
                { "fomulario facebook 3 campos", "CPFBK1" },
                { "facebook descuento", "FBK-DESC" },
                { "mailing descuento", "MLG3-DESC" },
                { "facebook cp descuento", "CPFBK-DESC" },
                { "Portal Web Colombia", "PW1" },//Solo para paginas de colombia
                { "descuento promocional formulario propio facebook", "FBK-DES1" },
                { "descuento promocional formulario propio adwords", "ADSB1-DES1" },

                { "adwords descuento busqueda", "ADSB1-DES1" },
                { "adwords descuento display", "ADSD1-DES1" }
            };
        }
    }
}
