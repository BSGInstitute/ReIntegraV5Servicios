using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MessengerChatRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/08/2022
    /// <summary>
    /// Gestión general de T_MessengerChat
    /// </summary>
    public class MessengerChatRepository : GenericRepository<TMessengerChat>, IMessengerChatRepository
    {
        private Mapper _mapper;

        public MessengerChatRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMessengerChat, MessengerChat>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMessengerChat MapeoEntidad(MessengerChat entidad)
        {
            try
            {
                //crea la entidad padre
                TMessengerChat modelo = _mapper.Map<TMessengerChat>(entidad);

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

        public TMessengerChat Add(MessengerChat entidad)
        {
            try
            {
                var MessengerChat = MapeoEntidad(entidad);
                base.Insert(MessengerChat);
                return MessengerChat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMessengerChat Update(MessengerChat entidad)
        {
            try
            {
                var MessengerChat = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MessengerChat.RowVersion = entidadExistente.RowVersion;

                base.Update(MessengerChat);
                return MessengerChat;
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


        public IEnumerable<TMessengerChat> Add(IEnumerable<MessengerChat> listadoEntidad)
        {
            try
            {
                List<TMessengerChat> listado = new List<TMessengerChat>();
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

        public IEnumerable<TMessengerChat> Update(IEnumerable<MessengerChat> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMessengerChat> listado = new List<TMessengerChat>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MessengerChat.
        /// </summary>
        /// <returns> List<MessengerChatDTO> </returns>
        public IEnumerable<MessengerChatDTO> ObtenerMessengerChat()
        {
            try
            {
                List<MessengerChatDTO> rpta = new List<MessengerChatDTO>();
                var query = @"
                    SELECT TOP 10
	                    Id,IdMeseengerUsuario,IdPersonal,Mensaje,Tipo,FacebookId,FechaInteraccion,IdTipoMensajeMessenger,UrlArchivoAdjunto,Leido,
	                    FechaLectura,IdFacebookAnuncio,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM com.T_MessengerChat
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MessengerChatDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene historial de messenger chat
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="idAlumno"></param>
        /// <returns>List<MessengerChatHistorialDTO></returns>
        public List<MessengerChatHistorialDTO> ObtenerHistorialMessengerChatPorPersonal(int idPersonal, int idAlumno)
        {
            try
            {
                List<MessengerChatHistorialDTO> rpta = new List<MessengerChatHistorialDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_HisotrialChatMessenger", new { idPersonal, idAlumno });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MessengerChatHistorialDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el chat de Messenger enviado por personal mediante el idPersonal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<MessengerChatHistorialDTO> ObtenerMessengerChatEnviadoPorPersonal(int idPersonal)
        {
            try
            {

                string queryMessenger = string.Empty;
                queryMessenger = "com.SP_MessengerChatsEnviadosByPersonal";
                var messenger = _dapperRepository.QuerySPDapper(queryMessenger, new { idPersonal });

                return JsonConvert.DeserializeObject<List<MessengerChatHistorialDTO>>(messenger)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el chat de Messenger recibido por personal mediante el idPersonal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<MessengerChatHistorialDTO> ObtenerMessengerChatRecibidoPorPersonal(int idPersonal)
        {
            try
            {

                string queryMessenger = string.Empty;
                queryMessenger = "com.SP_MessengerChatsByPersonal";
                var messenger = _dapperRepository.QuerySPDapper(queryMessenger, new { idPersonal });

                return JsonConvert.DeserializeObject<List<MessengerChatHistorialDTO>>(messenger)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Chat Messenger

        public bool InsertarMensajeRecibido(MessengerMensajeRecibidoDTO datos)
        {
            try
            {

                datos.Estado = true;
                datos.Usuario = "MessengerComplementos";

                string queryMessenger = string.Empty;
                queryMessenger = "mkt.SP_InsertarMensajeRecibidoMessenger";
                var messenger = _dapperRepository.QuerySPDapper(queryMessenger, new { datos.Sender, datos.Recipient, datos.Type,datos.Is_echo, datos.App_id, datos.Mid, datos.Text, datos.Url, datos.Estado, datos.Usuario });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertarMensajeEnviado(MessengerMensajeEnviadoDTO datos)
        {
            try
            {

                string queryMessenger = string.Empty;
                datos.Estado = true;
                datos.Usuario = "MessengerComplementos";

                queryMessenger = "mkt.SP_InsertarMensajeEnviadoMessenger";
                var messenger = _dapperRepository.QuerySPDapper(queryMessenger, new { datos.Sender, datos.Recipient, datos.Type, datos.Mid, datos.Text, datos.Url, datos.Estado,datos.Usuario });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool InsertarEventMensaje(EventoMensajeMessengerDTO datos)
        {
            try
            {

                string queryMessenger = string.Empty;
                queryMessenger = "mkt.SP_InsertarDatosEventoMensajeMessenger";
                var messenger = _dapperRepository.QuerySPDapper(queryMessenger, new { datos.SenderId, datos.RecipientId, datos.EventType, datos.Mids, datos.Estado, datos.Usuario });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
