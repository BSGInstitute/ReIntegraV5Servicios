using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ChatDetalleIntegraArchivoService
    /// Autor: Jonathan Caipo
    /// Fecha: 18/10/2022
    /// <summary>
    /// Gestión general de T_ChatIntegraHistorialAsesor
    /// </summary>
    public class ChatIntegraHistorialAsesorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ChatIntegraHistorialAsesorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TChatIntegraHistorialAsesor, ChatIntegraHistorialAsesor>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los chats de un asesor
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<ChatHistorialAsesorDTO> ObtenerTodoHistorialChatPorAsesor(int idPersonal)
        {
            try
            {
                return _unitOfWork.ChatIntegraHistorialAsesorRepository.ObtenerTodoHistorialChatPorAsesor(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 01/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los chats de un asesor soporte
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto : List<ChatHistorialAsesor> </returns>
        public List<ChatHistorialAsesorDTO> ObtenerTodoHistorialChatsPorAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.ChatIntegraHistorialAsesorRepository.ObtenerTodoHistorialChatsPorAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
