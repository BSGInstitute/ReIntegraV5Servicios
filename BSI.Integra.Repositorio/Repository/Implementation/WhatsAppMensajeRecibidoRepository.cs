using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppMensajeRecibidoRepositorio
    /// Autor: Jonathan Caipo
    /// Fecha: 18/10/2022
    /// <summary>
    /// Repositorio para consultas de mensajes recibidos vía WhatsApp
    /// </summary>
    public class WhatsAppMensajeRecibidoRepository : GenericRepository<TWhatsAppMensajeRecibido>, IWhatsAppMensajeRecibidoRepository
    {
        private Mapper _mapper;

        public WhatsAppMensajeRecibidoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppMensajeRecibido, WhatsAppMensajeRecibido>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TWhatsAppMensajeRecibido MapeoEntidad(WhatsAppMensajeRecibido entidad)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppMensajeRecibido modelo = _mapper.Map<TWhatsAppMensajeRecibido>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppMensajeRecibido Add(WhatsAppMensajeRecibido entidad)
        {
            try
            {
                var WhatsAppMensajeRecibido = MapeoEntidad(entidad);
                base.Insert(WhatsAppMensajeRecibido);
                return WhatsAppMensajeRecibido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppMensajeRecibido Update(WhatsAppMensajeRecibido entidad)
        {
            try
            {
                var WhatsAppMensajeRecibido = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                WhatsAppMensajeRecibido.RowVersion = entidadExistente.RowVersion;

                base.Update(WhatsAppMensajeRecibido);
                return WhatsAppMensajeRecibido;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TWhatsAppMensajeRecibido> Add(IEnumerable<WhatsAppMensajeRecibido> listadoEntidad)
        {
            try
            {
                List<TWhatsAppMensajeRecibido> listado = new List<TWhatsAppMensajeRecibido>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TWhatsAppMensajeRecibido> Update(IEnumerable<WhatsAppMensajeRecibido> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppMensajeRecibido> listado = new List<TWhatsAppMensajeRecibido>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
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
        /// Version: 1.0
        /// <summary>
        /// Obtiene Lista de último mensaje de chat de alumnos por IdPersonal ordenado por Fecha Modificación para Control de Mensajes Ofensivos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatRecibidoControlMensaje(int idPersonal)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensaje = new List<WhatsAppMensajesDTO>();
                var query = string.Empty;
                query = "mkt.SP_UltimoChatWhatsAppContactoRecibidoControlMensajes";
                var credencialTokenExpiraDB = _dapperRepository.QuerySPDapper(query, new { idPersonal });
                listaMensaje = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(credencialTokenExpiraDB)!;
                return listaMensaje;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
            List<WhatsAppMensajesRecibidosOperacionesDTO> conversacion = new List<WhatsAppMensajesRecibidosOperacionesDTO>();
            string query = "[ope].[SP_MensajesRecibidosWhatsAppOperacionesVersionTemp_eliminar27042026]";
            var queryConversacion = _dapperRepository.QuerySPDapper(query, new { idPersonal });
            if (queryConversacion == null || queryConversacion == "")
            {
                return null;
            }
            else
            {
                conversacion = JsonConvert.DeserializeObject<List<WhatsAppMensajesRecibidosOperacionesDTO>>(queryConversacion)!;
                return conversacion;
            }
        }
        public string guardarArchivos(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

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
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
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
            List<WhatsAppMensajeRecibido> whatsAppMensajeRecibidos = new List<WhatsAppMensajeRecibido>();
            var _queryAsesorById = @"SELECT Id,
                                               WaFrom,
                                               WaId,
                                               WaTimeStamp,
                                               WaType,
                                               WaTypeMensaje,
                                               WaIdTypeMensaje,
                                               WaBody,
                                               WaFile,
                                               WaFileName,
                                               WaMimeType,
                                               WaSha256,
                                               WaCaption,
                                               IdPais,
                                               IdPersonal,
                                               IdAlumno,
                                               EsMigracion,
                                               Estado,
                                               UsuarioCreacion,
                                               UsuarioModificacion,
                                               FechaCreacion,
                                               FechaModificacion,
                                               RowVersion,
                                               IdMigracion,
                                               MensajeOfensivo
                                        FROM mkt.T_WhatsAppMensajeRecibido
                                        WHERE Estado = 1
                                              AND WaId = @WaId";
            var registrosBD = _dapperRepository.QueryDapper(_queryAsesorById, new { WaId = waId });
            if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
            {
                whatsAppMensajeRecibidos = JsonConvert.DeserializeObject<List<WhatsAppMensajeRecibido>>(registrosBD);
            }
            return whatsAppMensajeRecibidos;
        }
    }
}
