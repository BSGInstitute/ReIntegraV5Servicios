using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Nancy.Json;
using System.Net;
using System.Text;
using System.Transactions;

//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoRegistroArchivoStorageDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: RegistroArchivoStorageService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_RegistroArchivoStorage
    /// </summary>
    public class RegistroArchivoStorageService : IRegistroArchivoStorageService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public RegistroArchivoStorageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRegistroArchivoStorage, RegistroArchivoStorage>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public RegistroArchivoStorage Add(RegistroArchivoStorage entidad)
        {
            try
            {
                var modelo = _unitOfWork.RegistroArchivoStorageRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RegistroArchivoStorage>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RegistroArchivoStorage Update(RegistroArchivoStorage entidad)
        {
            try
            {
                var modelo = _unitOfWork.RegistroArchivoStorageRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RegistroArchivoStorage>(modelo);
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
                _unitOfWork.RegistroArchivoStorageRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RegistroArchivoStorage> Add(List<RegistroArchivoStorage> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RegistroArchivoStorageRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RegistroArchivoStorage>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RegistroArchivoStorage> Update(List<RegistroArchivoStorage> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RegistroArchivoStorageRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RegistroArchivoStorage>>(modelo);
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
                _unitOfWork.RegistroArchivoStorageRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_RegistroArchivoStorage para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<RegistroArchivoStorageComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.RegistroArchivoStorageRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_RegistroArchivoStorage
        /// </summary>
        /// <returns> List<RegistroArchivoStorageDTO> </returns>
        public IEnumerable<RegistroArchivoStorageDTO> ObtenerRegistroArchivoStorage()
        {
            try
            {
                return _unitOfWork.RegistroArchivoStorageRepository.ObtenerRegistroArchivoStorage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RegistroArchivoStoragePermisosDTO> ObtenerTodoPorPermisosRegistroArchivoStorage(RegistroArchivoMostrarFiltroDTO registroArchivoMostrarFiltro)
        {
            try
            {
                return _unitOfWork.RegistroArchivoStorageRepository.ObtenerTodoPorPermisosRegistroArchivoStorage(registroArchivoMostrarFiltro);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<RegistroArchivoMostrarFiltroDTO> ObtenerMostrarFiltroArchivoStorage()
        {
            try
            {
                return _unitOfWork.RegistroArchivoStorageRepository.ObtenerMostrarFiltroArchivoStorage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboContenedorArchivoDTO> ObtenerContenedores(int IdPersonal)
        {
            try
            {
                return _unitOfWork.RegistroArchivoStorageRepository.ObtenerContenedores(IdPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboSubcontenedorArchivoDTO> ObtenerSubcontenedores(int IdPersonal)
        {
            try
            {
                return _unitOfWork.RegistroArchivoStorageRepository.ObtenerSubcontenedores(IdPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboTipoSubcontenedorArchivoDTO> ObtenerTipoSubcontenedores(int IdPersonal)
        {
            try
            {
                return _unitOfWork.RegistroArchivoStorageRepository.ObtenerTipoSubcontenedores(IdPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboSubcontenedorArchivoDTO> ObtenerSubcontRegistroArchivoStorageenedores(int IdPersonal)
        {
            throw new NotImplementedException();
        }

        public RegistroArchivoStorageRepositorio InsertarNuevoRegistro(RegistroArchivoStorageRepositorio entidad)
        {
            throw new NotImplementedException();
        }


        public string SubirArchivo(byte[] archivo, string mimeType, string nombreArchivo, string rutaCompleta, string rutaBlob)
        {
            try
            {
                string _nombreLink = string.Empty;
                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(rutaBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = mimeType;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = mimeType;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    _nombreLink = correcto ? rutaCompleta + nombreArchivo : string.Empty;

                    return _nombreLink.Replace(" ", "%20");
                    //return rutaCompleta + nombreArchivo;
                    //return "https://repositorioweb.blob.core.windows.net/repositorioweb/img/programas/mensajerecibidos.png";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //
        private string urlToBase = "https://api.cloudflare.com/client/v4/";
        private string token = "4rTsnwjbDloxBPd0jWoG8_CIv7YIS8nDdC--1pn_";
        public bool LimpiarCacheBsgInstitute()
        {

            string resultado;
            var parametros = new { purge_everything = true };

            using (WebClient client = new WebClient())
            {
                string urlToPost = urlToBase + "zones/ff0e3e3d87d144ba4592189b6dacbbe9/purge_cache";
                client.Encoding = Encoding.UTF8;
                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(parametros);
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                resultado = client.UploadString(urlToPost, serializedResult);
            }

            return resultado.Contains("true,");
        }
        //instalacion de paquete Nancy.jason.javaScrip



        public string RegistroArchivoStorageSubirArchivo(RegistroArchivoStorageSubirArchivoDTO registroSubirArchivo)
        {

            try
            {
                IEnumerable<RegistroArchivoStorage> registroArchivoStorage;
                var _repRegistroArchivoStorage = _unitOfWork.RegistroArchivoStorageRepository;
                var _repUrlBlockStorage = new UrlBlockStorageService(_unitOfWork);

                var archivosOperaciones = new Dictionary<string, IList<IFormFile>> {
                    { "-b.", registroSubirArchivo.ArchivoBol },
                    { "-c.", registroSubirArchivo.ArchivoCol },
                    { "-i.", registroSubirArchivo.ArchivoInt },
                    { "-pl.", registroSubirArchivo.ArchivoPeLima },
                    { "-pa.", registroSubirArchivo.ArchivoPeAqp }
                };

                var contenedorSubcontenedor = _repUrlBlockStorage.ObtenerInformacionPorIdUrlSubcontenedor(registroSubirArchivo.IdUrlSubContenedor).First();

                //GestionArchivoBO gestionArchivoBo = new GestionArchivoBO();

                //CloudflareBO cloudflareBo = new CloudflareBO();
                string varUrl = string.Empty;

                foreach (var archivo in registroSubirArchivo.Archivos)
                {
                    registroSubirArchivo.NombreArchivo = registroSubirArchivo.IdUrlSubContenedor == 25/*Firma Mailing*/ ? string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), '-', registroSubirArchivo.NombreArchivo) : registroSubirArchivo.NombreArchivo;

                    if (registroSubirArchivo.IdUrlSubContenedor == 24 || registroSubirArchivo.IdUrlSubContenedor == 48 || registroSubirArchivo.IdUrlSubContenedor == 49)/*Firma Operaciones*/
                        varUrl = this.SubirArchivo(archivo.ConvertToByte(), archivo.ContentType, registroSubirArchivo.NombreArchivo, registroSubirArchivo.RutaCompleta.Replace("V4/", string.Empty), registroSubirArchivo.RutaBlob.Replace("V4/", string.Empty));
                    else
                    {
                        varUrl = this.SubirArchivo(archivo.ConvertToByte(), archivo.ContentType, registroSubirArchivo.NombreArchivo, registroSubirArchivo.RutaCompleta, registroSubirArchivo.RutaBlob);
                        if (contenedorSubcontenedor.IdProveedorNube == 2)/*Subcontenedores que se guardan en el portal*/
                        {
                            try
                            {
                                WebClient wc = new WebClient();
                                var url = "https://proceso-pago.bsginstitute.com/GestionArchivo/AlmacenarArchivo?urlArchivo=" + varUrl + "&rutaBlob=" + registroSubirArchivo.RutaBlob + "&subdominio=" + contenedorSubcontenedor.Subdominio + "&nombreArchivo=" + registroSubirArchivo.NombreArchivo;

                                string urlimg = wc.DownloadString(url);
                                //string urlimg = varUrl;

                                if (!string.IsNullOrEmpty(urlimg))

                                {
                                    //LlamadaRegularizacionDTO al insert
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        var listadoRegistroExistente = _repRegistroArchivoStorage.GetBy(w => w.Ruta == urlimg).ToList();
                                        registroArchivoStorage = _mapper.Map<List<RegistroArchivoStorage>>(listadoRegistroExistente);
                                        foreach (var registroArchivo in registroArchivoStorage)
                                        {
                                            var respuesta = this.Delete(registroArchivo.Id, registroSubirArchivo.NombreUsuario);
                                                if (!respuesta) throw new Exception("error");
                                        }
                                        scope.Complete();


                                    }
                                    var RegistroArchivoStorage = new RegistroArchivoStorage()
                                    {
                                        IdUrlSubContenedor = registroSubirArchivo.IdUrlSubContenedor,
                                        NombreArchivo = registroSubirArchivo.NombreArchivo,
                                        Ruta = urlimg,
                                        Estado = true,
                                        UsuarioCreacion = registroSubirArchivo.NombreUsuario,
                                        UsuarioModificacion = registroSubirArchivo.NombreUsuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now
                                    };

                                    RegistroArchivoStorage = this.Add(RegistroArchivoStorage);
                                    this.LimpiarCacheBsgInstitute();
                                    return (urlimg);
                                }
                                else
                                {
                                    throw new Exception("No hubo respuesta");
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                        }
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        var listadoRegistroExistente = _repRegistroArchivoStorage.GetBy(w => w.Ruta == varUrl);
                        registroArchivoStorage = _mapper.Map<List<RegistroArchivoStorage>>(listadoRegistroExistente);

                        foreach (var registroArchivo in registroArchivoStorage)
                            _repRegistroArchivoStorage.Delete(registroArchivo.Id, registroSubirArchivo.NombreUsuario);

                        scope.Complete();
                    }
                    var RegistroArchivoStorageInsert = new RegistroArchivoStorage()

                    {
                        IdUrlSubContenedor = registroSubirArchivo.IdUrlSubContenedor == 24/*Firma Operaciones*/ ? 23/*Firma Simple*/ : registroSubirArchivo.IdUrlSubContenedor,
                        NombreArchivo = registroSubirArchivo.NombreArchivo,
                        Ruta = varUrl,
                        Estado = true,
                        UsuarioCreacion = registroSubirArchivo.NombreUsuario,
                        UsuarioModificacion = registroSubirArchivo.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    RegistroArchivoStorageInsert = this.Add(RegistroArchivoStorageInsert);
                }

                if (contenedorSubcontenedor.AplicaSubidaMultiple)/*Firma Operaciones*/
                {
                    foreach (var archivo in archivosOperaciones)
                    {
                        if (archivo.Value != null)
                        {
                            foreach (var elemento in archivo.Value)
                            {
                                string nombreArchivoRuta = registroSubirArchivo.NombreArchivo.Replace(".", archivo.Key);
                                varUrl = this.SubirArchivo(elemento.ConvertToByte(), elemento.ContentType, nombreArchivoRuta, registroSubirArchivo.RutaCompleta, registroSubirArchivo.RutaBlob);

                                using (TransactionScope scope = new TransactionScope())
                                {
                                    var listadoRegistroExistente = _repRegistroArchivoStorage.GetBy(w => w.Ruta == varUrl);

                                    foreach (var registroArchivo in listadoRegistroExistente)
                                        _repRegistroArchivoStorage.Delete(registroArchivo.Id, registroSubirArchivo.NombreUsuario);

                                    scope.Complete();
                                }

                                var RegistroArchivoStorage = new RegistroArchivoStorage()

                                {
                                    IdUrlSubContenedor = registroSubirArchivo.IdUrlSubContenedor,
                                    NombreArchivo = nombreArchivoRuta,
                                    Ruta = varUrl,
                                    Estado = true,
                                    UsuarioCreacion = registroSubirArchivo.NombreUsuario,
                                    UsuarioModificacion = registroSubirArchivo.NombreUsuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };

                                RegistroArchivoStorage = this.Add(RegistroArchivoStorage);
                            }
                        }
                    }
                }
                this.LimpiarCacheBsgInstitute();

                return (varUrl);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_RegistroArchivoStorage para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>

        public IEnumerable<RegistroArchivoObtenerUrlComboDTO> ObtenerComboFirma()
        {
            try
            {
                return _unitOfWork.RegistroArchivoStorageRepository.ObtenerComboFirma();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RegistroArchivoObtenerUrlComboDTO> ObtenerRegistroArchivoStoragePorIdUrlSubContenedor(int idUrlSubContenedor)
        {
            try
            {
                return _unitOfWork.RegistroArchivoStorageRepository.ObtenerRegistroArchivoStoragePorIdUrlSubContenedor(idUrlSubContenedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public string Insertar( RegistroArchivoStorage_RegistrarDTO NuevoRegistro)
        //{

        //    try
        //    {
        //        IEnumerable<RegistroArchivoStorage_RegistrarDTO> registroArchivoStorage_Registrar;
        //        var _repRegistroArchivoStorage = _unitOfWork.RegistroArchivoStorageRepository;
        //        /*25: Subcontenedor Mailing*/
        //        string nombreArchivo = NuevoRegistro.IdUrlSubContenedor != 25 ? NuevoRegistro.NombreArchivo : string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), '-', NuevoRegistro.NombreArchivo);

        //        var RegistroArchivoStorageNuevo = new RegistroArchivoStorage()
        //        {
        //            IdUrlSubContenedor = NuevoRegistro.IdUrlSubContenedor,
        //            NombreArchivo = nombreArchivo,
        //            Ruta = string.Concat(NuevoRegistro.Ruta, nombreArchivo).Replace(" ", "%20"),

        //            Estado = true,
        //            UsuarioCreacion = NuevoRegistro.NombreUsuario,
        //            UsuarioModificacion = NuevoRegistro.NombreUsuario,
        //            FechaCreacion = DateTime.Now,
        //            FechaModificacion = DateTime.Now
        //        };

        //        if (RegistroArchivoStorageNuevo.HasError)
        //        {
        //            RegistroArchivoStorageNuevo = this.Add(RegistroArchivoStorageNuevo);
        //        }
        //        else
        //        {
        //            return BadRequest(RegistroArchivoStorageNuevo);
        //        }
        //        return O(true);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        //private string BadRequest(object value)
        //{
        //    throw new Exception();
        //}
    }
}









