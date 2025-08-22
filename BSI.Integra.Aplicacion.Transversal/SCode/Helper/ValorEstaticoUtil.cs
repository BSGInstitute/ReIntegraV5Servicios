using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Servicios.Helpers
{
    public static class ValorEstaticoUtil
    {
        private static readonly List<ClaveValorDTO> ListaClaveValor = new List<ClaveValorDTO>();

        private static void InitValorEstatico()
        {
            //archivos
            ListaClaveValor.Add(new ClaveValorDTO { Clave = "{ArchivoAdjunto.ManualIngresoAulaVirtual}", Valor = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/Manual%20Alumnos%20Aula%20Virtual.pdf" });
            ListaClaveValor.Add(new ClaveValorDTO { Clave = "{ArchivoAdjunto.ManualBSPlay}", Valor = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/Manual%20BS%20Play.pdf" });
            ListaClaveValor.Add(new ClaveValorDTO { Clave = "{ArchivoAdjunto.ManualConectarseSesionWebinar}", Valor = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/Manual%20para%20conectarse%20a%20la%20Sesi%C3%B3n%20Webinar.pdf" });
            ListaClaveValor.Add(new ClaveValorDTO { Clave = "{ArchivoAdjunto.ManualConectarseSesionVirtual}", Valor = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/Manual%20para%20Conectarse%20a%20la%20Sesi%C3%B3n%20Virtual.pdf" });

            //url

            ListaClaveValor.Add(new ClaveValorDTO { Clave = "{Link.UrlPortalWeb}", Valor = "https://bsginstitute.com/" });
            ListaClaveValor.Add(new ClaveValorDTO { Clave = "{Link.UrlManualBSPlay}", Valor = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/Manual%20BS%20Play.pdf" });
            ListaClaveValor.Add(new ClaveValorDTO { Clave = "{Link.UrlAulaVirtual}", Valor = "http://virtual.bsginstitute.com/" });
            ListaClaveValor.Add(new ClaveValorDTO { Clave = "{Link.UrlDescargarAplicativoAndroid}", Valor = "http://d33a1bwwgr1uvf.cloudfront.net/BS%20Play_.apk" });
            ListaClaveValor.Add(new ClaveValorDTO { Clave = "{Link.UrlDescargarAplicativoIOS}", Valor = "https://itunes.apple.com/pe/app/bs-play/id974621455?mt=8" });
            ListaClaveValor.Add(new ClaveValorDTO { Clave = "{Link.UrlGuiaAccederSesionWebinarPorVideo}", Valor = "https://virtual.bsginstitute.com/webinar.html" });
            ListaClaveValor.Add(new ClaveValorDTO { Clave = "{Link.UrlImagenFelizCumpleanios}", Valor = "https://repositorioweb.blob.core.windows.net/repositorioweb/img/tarjeta-cumpleanos.png" });

        }

        /// <summary>
        /// Obtiene el valor del diccionario por clave
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            if (ListaClaveValor == null || (ListaClaveValor != null && ListaClaveValor.Count == 0))
            {
                InitValorEstatico();
            }
            if (!ListaClaveValor.Any(x => x.Clave == key))
            {
                return "";
            }
            else
            {
                return ListaClaveValor.Where(x => x.Clave == key).FirstOrDefault().Valor;
            }
        }
    }
}
