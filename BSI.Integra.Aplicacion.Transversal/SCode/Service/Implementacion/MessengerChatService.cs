using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: MessengerChatService
    /// Autor: Jonathan Caipo
    /// Fecha: 18/08/2022
    /// <summary>
    /// Gestión general de T_MessengerChat
    /// </summary>
    public class MessengerChatService : IMessengerChatService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MessengerChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMessengerChat, MessengerChat>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public MessengerChat Add(MessengerChat entidad)
        {
            try
            {
                var modelo = _unitOfWork.MessengerChatRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MessengerChat>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MessengerChat Update(MessengerChat entidad)
        {
            try
            {
                var modelo = _unitOfWork.MessengerChatRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MessengerChat>(modelo);
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
                _unitOfWork.MessengerChatRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MessengerChat> Add(List<MessengerChat> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MessengerChatRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MessengerChat>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MessengerChat> Update(List<MessengerChat> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MessengerChatRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MessengerChat>>(modelo);
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
                _unitOfWork.MessengerChatRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
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
        /// Obtiene todos los registros de T_MessengerChat
        /// </summary>
        /// <returns> List<MessengerChatDTO> </returns>
        public IEnumerable<MessengerChatDTO> ObtenerMessengerChat()
        {
            try
            {
                return _unitOfWork.MessengerChatRepository.ObtenerMessengerChat();
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
                return _unitOfWork.MessengerChatRepository.ObtenerHistorialMessengerChatPorPersonal(idPersonal, idAlumno);
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
                return _unitOfWork.MessengerChatRepository.ObtenerMessengerChatEnviadoPorPersonal(idPersonal);
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
        /// Obtiene el chat de Messenger recibido por personal mediante el idPersonal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<MessengerChatHistorialDTO> ObtenerMessengerChatRecibidoPorPersonal(int idPersonal)
        {
            try
            {
                return _unitOfWork.MessengerChatRepository.ObtenerMessengerChatRecibidoPorPersonal(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Chat messenger

        public bool InsertarMensajeEnviado(MessengerMensajeEnviadoDTO datos)
        {
            try
            {
                return _unitOfWork.MessengerChatRepository.InsertarMensajeEnviado(datos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool InsertarMensajeRecibido(MessengerMensajeRecibidoDTO datos)
        {
            try
            {
                return _unitOfWork.MessengerChatRepository.InsertarMensajeRecibido(datos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool InsertarEventMensaje(EventoMensajeMessengerDTO datos)
        {
            try
            {
                return _unitOfWork.MessengerChatRepository.InsertarEventMensaje(datos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
