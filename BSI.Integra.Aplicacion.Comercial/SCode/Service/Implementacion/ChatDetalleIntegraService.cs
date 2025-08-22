using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ChatDetalleIntegraService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/07/2022
    /// <summary>
    /// Gestión general de T_ChatDetalleIntegra
    /// </summary>
    public class ChatDetalleIntegraService : IChatDetalleIntegraService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ChatDetalleIntegraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TChatDetalleIntegra, ChatDetalleIntegra>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ChatDetalleIntegraDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ChatDetalleIntegra Add(ChatDetalleIntegra entidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ChatDetalleIntegra>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChatDetalleIntegra Update(ChatDetalleIntegra entidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ChatDetalleIntegra>(modelo);
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
                _unitOfWork.ChatDetalleIntegraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChatDetalleIntegra> Add(List<ChatDetalleIntegra> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ChatDetalleIntegra>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChatDetalleIntegra> Update(List<ChatDetalleIntegra> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ChatDetalleIntegra>>(modelo);
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
                _unitOfWork.ChatDetalleIntegraRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 18/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ChatDetalleIntegra
        /// </summary>
        /// <returns> List<ChatDetalleIntegraDTO> </returns>
        public IEnumerable<ChatDetalleIntegraDTO> ObtenerChatDetalleIntegra()
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerChatDetalleIntegra();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ChatDetalleIntegra para mostrarse en combo.
        /// </summary>
        /// <returns> List<ChatDetalleIntegraComboDTO> </returns>
        public IEnumerable<ChatDetalleIntegraComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerCombo();
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
        /// Obtiene historial de chat para pantalla2
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> HistorialChatRecibidosDTO </returns>
        public HistorialChatRecibidosDTO ObtenerHistorialChatRecibidos(int idPersonal, int idAlumno)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerHistorialChatRecibidos(idPersonal, idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccion
        /// </summary>
        /// <param name="idInteraccion"></param>
        /// <returns> Lista de Entidad List<ChatDetalleIntegra> </returns>
        public List<ChatDetalleIntegra> DetalleChatPorIdInteraccion(int idInteraccion)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerDetalleChatPorIdInteraccion(idInteraccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por IdAlumno
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public List<ChatDetalleIntegra> ObtenerDetalleChatPorIdInteraccionControlMensajesSoporte(int idAlumno)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerDetalleChatPorIdInteraccionControlMensajesSoporte(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


 
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por IdAlumno
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public bool FinalizarChatAtc(int idMatriculaCabecera,string userName)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.FinalizarChatAtc(idMatriculaCabecera, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Gilmer Quispe.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por IdAlumno
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public bool FinalizarChatComercial(int idOportunidad, string userName)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.FinalizarChatComercial(idOportunidad, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ChatDetalleIntegra.
        /// </summary>
        /// <returns> ChatDetalleIntegra </returns>
        public ChatDetalleIntegra ObtenerPorIntegraChatYRemintente(int idInteraccionChatIntegra, string idRemitente)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerPorIntegraChatYRemintente(idInteraccionChatIntegra, idRemitente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ChatDetalleIntegra
        /// </summary>
        /// <returns> List<ChatDetalleIntegraDTO> </returns>
        public List<HistorialChatDetalleIntegraDTO> ObtenerHistorialChatDetalleIntegra(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerHistorialChatDetalleIntegra(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor:Joseph Llanque
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene id de ultima interaccion de cchat alumno
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public List<DatosSesionChatDTO> GetIdUltimaInteraccion(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.GetIdUltimaInteraccion(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene id de ultima interaccion de cchat alumno
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public List<DatosSesionChatComercialDTO> GetIdUltimaInteraccionComercial(int idAlumno)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.GetIdUltimaInteraccionComercial(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
