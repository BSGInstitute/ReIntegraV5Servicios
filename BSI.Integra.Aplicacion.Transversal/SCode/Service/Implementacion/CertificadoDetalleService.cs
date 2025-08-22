using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CertificadoDetalleService
    /// Autor: Jonathan Caipo
    /// Fecha: 28/11/2022
    /// <summary>
    /// Gestión general de CertificadoDetalle
    /// </summary>
    public class CertificadoDetalleService : ICertificadoDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CertificadoDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCertificadoDetalle, CertificadoDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 28/11/2022
        /// Version: 1.0
        /// <summary>
        /// Guarda el Archivo Certificado
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public string GuardarArchivoCertificado(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string nombreLink = string.Empty;
                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";
                    string _direccionBlob = @"operaciones/comprobantes/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = tipo;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);
                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        nombreLink = "";
                    }
                    return nombreLink;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return "";
            }
        }
        public string GuardarArchivoCertificadoFisico(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                string _direccionBlob = @"operaciones/CertificadoSinFondo/";

                //Generar entrada al blob storage
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                blockBlob.Properties.ContentType = tipo;
                blockBlob.Metadata["filename"] = nombreArchivo;
                blockBlob.Metadata["filemime"] = tipo;
                Stream stream = new MemoryStream(archivo);
                var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                objRegistrado.Wait();
                var correcto = objRegistrado.IsCompletedSuccessfully;

                if (correcto)
                    _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
                    
                else
                    _nombreLink = "";

                return _nombreLink;
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return "";
            }
        }
    }
}
