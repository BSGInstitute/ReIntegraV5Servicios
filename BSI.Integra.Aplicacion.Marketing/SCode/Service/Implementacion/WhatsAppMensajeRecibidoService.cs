using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: WhatsAppMensajeRecibidoService
    /// Autor: Jonathan Caipo
    /// Fecha: 18/10/2022
    /// <summary>
    /// Repositorio para consultas de mensajes recibidos vía WhatsApp
    /// </summary>
    public class WhatsAppMensajeRecibidoService : IWhatsAppMensajeRecibidoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public WhatsAppMensajeRecibidoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TWhatsAppMensajeRecibido, WhatsAppMensajeRecibido>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public WhatsAppMensajeRecibido Add(WhatsAppMensajeRecibido entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajeRecibidoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppMensajeRecibido>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WhatsAppMensajeRecibido Update(WhatsAppMensajeRecibido entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajeRecibidoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppMensajeRecibido>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.WhatsAppMensajeRecibidoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppMensajeRecibido> Add(List<WhatsAppMensajeRecibido> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajeRecibidoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppMensajeRecibido>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppMensajeRecibido> Update(List<WhatsAppMensajeRecibido> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajeRecibidoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppMensajeRecibido>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.WhatsAppMensajeRecibidoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        ///Autor: Jonathan Caipo
        ///Fecha: 18/10/2022
        /// <summary>
        /// Obtiene Lista de último mensaje de chat de alumnos por IdPersonal ordenado por Fecha Modificación para Control de Mensajes Ofensivos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatRecibidoControlMensaje(int idPersonal)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeRecibidoRepository.ListaUltimoMensajeChatRecibidoControlMensaje(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene mensajes recibidos en operaciones.
        /// </summary>
        /// <param name="idPersonal"> Id de personal </param>
        /// <returns> List<WhatsAppMensajesRecibidosOperacionesDTO> </returns>
        public List<WhatsAppMensajesRecibidosOperacionesDTO> ObtenerMensajesRecibidosOperaciones(int idPersonal)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeRecibidoRepository.ObtenerMensajesRecibidosOperaciones(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GuardarArchivos(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                nombreArchivo = nombreArchivo.Replace("á", "a");
                nombreArchivo = nombreArchivo.Replace("é", "e");
                nombreArchivo = nombreArchivo.Replace("í", "i");
                nombreArchivo = nombreArchivo.Replace("ó", "o");
                nombreArchivo = nombreArchivo.Replace("ú", "u");

                nombreArchivo = nombreArchivo.Replace("Á", "A");
                nombreArchivo = nombreArchivo.Replace("É", "E");
                nombreArchivo = nombreArchivo.Replace("Í", "I");
                nombreArchivo = nombreArchivo.Replace("Ó", "O");
                nombreArchivo = nombreArchivo.Replace("Ú", "U");

                //Elimina las Ñ
                nombreArchivo = nombreArchivo.Replace("ñ", "n");
                nombreArchivo = nombreArchivo.Replace("Ñ", "N");


                //Elimina los caracteres con tilde
                nombreArchivo = nombreArchivo.Replace("á", "a");
                nombreArchivo = nombreArchivo.Replace("é", "e");
                nombreArchivo = nombreArchivo.Replace("í", "i");
                nombreArchivo = nombreArchivo.Replace("ó", "o");
                nombreArchivo = nombreArchivo.Replace("ú", "u");

                nombreArchivo = nombreArchivo.Replace("Á", "A");
                nombreArchivo = nombreArchivo.Replace("É", "E");
                nombreArchivo = nombreArchivo.Replace("Í", "I");
                nombreArchivo = nombreArchivo.Replace("Ó", "O");
                nombreArchivo = nombreArchivo.Replace("Ú", "U");

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";
                    string _direccionBlob = @"repositorioweb/whatsapp/enviados/";

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
                    {
                        _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        _nombreLink = "";
                    }
                    return _nombreLink;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 14/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros asociados al WaId
        /// </summary>
        /// <param name="waId"> waId </param>
        /// <returns> List<WhatsAppMensajeRecibido> </returns>
        public List<WhatsAppMensajeRecibido> ObtenerPorWaId(string waId)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeRecibidoRepository.ObtenerPorWaId(waId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 14/03/2023
        /// Version: 1.0
        /// <summary>
        /// Guarda los archivos wsp en el RepositorioWeb
        /// </summary>
        /// <param name="archivo"> Archivo que se guardara en el repositorio </param>
        /// <param name="carpetaArchivo"> Carpeta donde se guardará el archivo </param>
        /// <param name="tipo"> Tipo de archivo </param>
        /// <param name="nombreArchivo"> Nombre del archivo </param>
        /// <param name="IdPais"> IdPais del dato </param>
        /// <returns> List<WhatsAppMensajeRecibido> </returns>
        public string guardarArchivos(byte[] archivo, string carpetaArchivo, string tipo, string nombreArchivo, int IdPais)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"repositorioweb/whatsapp/";

                    switch (IdPais)
                    {
                        case 51: // Peru
                            _direccionBlob += "peru/" + carpetaArchivo;
                            break;
                        case 57: // Colombia
                            _direccionBlob += "colombia/" + carpetaArchivo;
                            break;
                        case 591: // Bolivia
                            _direccionBlob += "bolivia/" + carpetaArchivo;
                            break;
                        case 0: // Internacional
                            _direccionBlob += "internacional/" + carpetaArchivo;
                            break;
                        default:
                            _direccionBlob += "pruebas/" + carpetaArchivo;
                            break;
                    }

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
                    blockBlob.UploadFromStreamAsync(stream); 
                    _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + "/" + nombreArchivo.Replace(" ", "%20");
                     
                }
                catch (Exception ex)
                {
                    //Logger.Error(ex.ToString());
                }
                return _nombreLink;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
