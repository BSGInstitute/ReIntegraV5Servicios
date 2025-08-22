using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: GestionArchivoLlamadaService
    /// Autor: Jonathan Caipo
    /// Fecha: 19/10/2022
    /// <summary>
    /// Gestión general de Reportes
    /// </summary>
    public class GestionArchivoLlamadaService : IGestionArchivoLlamadaService
    {
        private IUnitOfWork _unitOfWork;

        public GestionArchivoLlamadaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/10/2022
        /// Versión: 1.2
        /// <summary>
        /// Sube el archivo en Azure Blob Storage
        /// </summary>
        /// <param name="archivo">Archivo a subir</param>
        /// <param name="mimeType">Tipo de mime del archivo a subir</param>
        /// <param name="nombreArchivo">Nombre del archivo a subir</param>
        /// <param name="rutaCompleta">Ruta del archivo completa</param>
        /// <param name="rutaBlob">Ruta del blob</param>
        /// <returns>Cadena del archivo subido en Azure Blog Storage</returns>
        public string SubirArchivoAudioLlamada(byte[] archivo, string mimeType, string nombreArchivo, string rutaCompleta, string rutaBlob)
        {
            try
            {
                string nombreLink = string.Empty;
                try
                {
                    string azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioaudiollamada;AccountKey=I36vJHayP1gja6+IsxnQAFGXHzgp9hn6zOxZ/hq4C7b2roXMdOdpke64UV8c+Lsdc0ekzAEJU0rB0Rr03Awp0w==;EndpointSuffix=core.windows.net";

                    MontoPagoCronogramaService montoPagoCronogramaService = new MontoPagoCronogramaService(_unitOfWork);
                    string nombreValidado = montoPagoCronogramaService.NormalizarCadena(nombreArchivo);

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(rutaBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreValidado);
                    blockBlob.Properties.ContentType = mimeType;
                    blockBlob.Metadata["filename"] = nombreValidado;
                    blockBlob.Metadata["filemime"] = mimeType;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    nombreLink = correcto ? rutaCompleta + nombreValidado : string.Empty;

                    return nombreLink.Replace(" ", "%20");
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
