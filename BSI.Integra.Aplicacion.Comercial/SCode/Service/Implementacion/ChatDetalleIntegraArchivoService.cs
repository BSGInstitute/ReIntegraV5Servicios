using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ChatDetalleIntegraArchivoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/07/2022
    /// <summary>
    /// Gestión general de T_ChatDetalleIntegraArchivo
    /// </summary>
    public class ChatDetalleIntegraArchivoService : IChatDetalleIntegraArchivoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ChatDetalleIntegraArchivoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TChatDetalleIntegraArchivo, ChatDetalleIntegraArchivo>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ChatDetalleIntegraArchivoDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ChatDetalleIntegraArchivo Add(ChatDetalleIntegraArchivo entidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraArchivoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ChatDetalleIntegraArchivo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChatDetalleIntegraArchivo Update(ChatDetalleIntegraArchivo entidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraArchivoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ChatDetalleIntegraArchivo>(modelo);
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
                _unitOfWork.ChatDetalleIntegraArchivoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChatDetalleIntegraArchivo> Add(List<ChatDetalleIntegraArchivo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraArchivoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ChatDetalleIntegraArchivo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChatDetalleIntegraArchivo> Update(List<ChatDetalleIntegraArchivo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraArchivoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ChatDetalleIntegraArchivo>>(modelo);
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
                _unitOfWork.ChatDetalleIntegraArchivoRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_ChatDetalleIntegraArchivo
        /// </summary>
        /// <returns> List<ChatDetalleIntegraArchivoDTO> </returns>
        public IEnumerable<ChatDetalleIntegraArchivoDTO> ObtenerChatDetalleIntegraArchivo()
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraArchivoRepository.ObtenerChatDetalleIntegraArchivo();
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
        /// Obtiene registros de T_ChatDetalleIntegraArchivo para mostrarse en combo.
        /// </summary>
        /// <returns> List<ChatDetalleIntegraArchivoComboDTO> </returns>
        public IEnumerable<ChatDetalleIntegraArchivoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraArchivoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
