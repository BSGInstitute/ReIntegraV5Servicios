using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: TiempoCapacitacionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión general de T_TiempoCapacitacion
    /// </summary>
    public class TiempoCapacitacionService : ITiempoCapacitacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TiempoCapacitacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTiempoCapacitacion, TiempoCapacitacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TiempoCapacitacion Add(TiempoCapacitacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.TiempoCapacitacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TiempoCapacitacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TiempoCapacitacion Update(TiempoCapacitacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.TiempoCapacitacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TiempoCapacitacion>(modelo);
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
                _unitOfWork.TiempoCapacitacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TiempoCapacitacion> Add(List<TiempoCapacitacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TiempoCapacitacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TiempoCapacitacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TiempoCapacitacion> Update(List<TiempoCapacitacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TiempoCapacitacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TiempoCapacitacion>>(modelo);
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
                _unitOfWork.TiempoCapacitacionRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TiempoCapacitacion
        /// </summary>
        /// <returns> List<TiempoCapacitacionDTO> </returns>
        public IEnumerable<TiempoCapacitacionDTO> ObtenerTiempoCapacitacion()
        {
            try
            {
                return _unitOfWork.TiempoCapacitacionRepository.ObtenerTiempoCapacitacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TiempoCapacitacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<TiempoCapacitacionComboDTO> </returns>
        public List<TiempoCapacitacionComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TiempoCapacitacionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Detalle de Tiempo Capacitacion para Agenda asociado a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<TiempoCapacitacionComboDTO> </returns>
        public TiempoCapacitacionAgendaDTO ObtenerTiempoCapacitacionParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var tiemposCapacitacion = _unitOfWork.TiempoCapacitacionRepository.ObtenerCombo();
                var oportunidadService = new OportunidadService(_unitOfWork);
                var capacitacionOportunidad = oportunidadService.ObtenerTiempoCapacitacionPorIdOportunidad(idOportunidad);
                var tiempoCapacitacionAgenda = new TiempoCapacitacionAgendaDTO()
                {
                    IdTiempoCapacitacion = capacitacionOportunidad.IdTiempoCapacitacion,
                    IdTiempoCapacitacionValidacion = capacitacionOportunidad.IdTiempoCapacitacionValidacion,
                    TiemposCapacitacion = tiemposCapacitacion.ToList()
                };
                return tiempoCapacitacionAgenda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
