using System.Net;

namespace BSI.Integra.Aplicacion.Base.BO
{
    public static class ExtendedWebClient
    {
        /// <summary>
        /// Obtiene un archivo desde la web
        /// </summary>
        /// <param name="urlFile"></param>
        /// <returns></returns>
        public static byte[] GetFile(string urlFile)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    return webClient.DownloadData(urlFile);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
