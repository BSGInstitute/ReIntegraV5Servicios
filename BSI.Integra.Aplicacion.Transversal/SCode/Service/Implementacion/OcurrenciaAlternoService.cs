using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OcurrenciaAlternoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/08/2022
    /// <summary>
    /// Gestión general de T_OcurrenciaAlterno
    /// </summary>
    public class OcurrenciaAlternoService : IOcurrenciaAlternoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OcurrenciaAlternoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TOcurrenciaAlterno, OcurrenciaAlterno>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public OcurrenciaAlterno Add(OcurrenciaAlterno entidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaAlternoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OcurrenciaAlterno>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OcurrenciaAlterno Update(OcurrenciaAlterno entidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaAlternoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OcurrenciaAlterno>(modelo);
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
                _unitOfWork.OcurrenciaAlternoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OcurrenciaAlterno> Add(List<OcurrenciaAlterno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaAlternoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OcurrenciaAlterno>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OcurrenciaAlterno> Update(List<OcurrenciaAlterno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaAlternoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OcurrenciaAlterno>>(modelo);
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
                _unitOfWork.OcurrenciaAlternoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OcurrenciaAlterno
        /// </summary>
        /// <returns> List<OcurrenciaAlternoDTO> </returns>
        public IEnumerable<OcurrenciaAlternoDTO> ObtenerOcurrenciaAlterno()
        {
            try
            {
                return _unitOfWork.OcurrenciaAlternoRepository.ObtenerOcurrenciaAlterno();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_OcurrenciaAlterno para mostrarse en combo.
        /// </summary>
        /// <returns> List<OcurrenciaAlternoComboDTO> </returns>
        public IEnumerable<OcurrenciaAlternoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.OcurrenciaAlternoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Información de Ocurrencia por Id
        /// </summary>
        /// <param name="idOcurrencia">Id de la Ocurrencia</param>
        /// <returns> OcurrenciaAlternoDTO </returns>
        public OcurrenciaAlternoDTO ObtenerOcurrenciaPorActividad(int idOcurrencia)
        {
            try
            {
                return _unitOfWork.OcurrenciaAlternoRepository.ObtenerOcurrenciaPorActividad(idOcurrencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
