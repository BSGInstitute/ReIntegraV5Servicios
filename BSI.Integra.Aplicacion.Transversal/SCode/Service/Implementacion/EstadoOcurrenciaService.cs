using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: EstadoOcurrenciaService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_EstadoOcurrencia
    /// </summary>
    public class EstadoOcurrenciaService : IEstadoOcurrenciaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EstadoOcurrenciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEstadoOcurrencium, EstadoOcurrencia>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public EstadoOcurrencia Add(EstadoOcurrencia entidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoOcurrenciaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoOcurrencia>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoOcurrencia Update(EstadoOcurrencia entidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoOcurrenciaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoOcurrencia>(modelo);
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
                _unitOfWork.EstadoOcurrenciaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoOcurrencia> Add(List<EstadoOcurrencia> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoOcurrenciaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoOcurrencia>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoOcurrencia> Update(List<EstadoOcurrencia> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoOcurrenciaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoOcurrencia>>(modelo);
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
                _unitOfWork.EstadoOcurrenciaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EstadoOcurrencia para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.EstadoOcurrenciaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EstadoOcurrencia
        /// </summary>
        /// <returns> List<EstadoOcurrenciaDTO> </returns>
        public IEnumerable<EstadoOcurrenciaDTO> ObtenerEstadoOcurrencia()
        {
            try
            {
                return _unitOfWork.EstadoOcurrenciaRepository.ObtenerEstadoOcurrencia();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
