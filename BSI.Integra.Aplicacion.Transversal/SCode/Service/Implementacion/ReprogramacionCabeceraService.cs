using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ReprogramacionCabeceraService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión general de T_ReprogramacionCabecera
    /// </summary>
    public class ReprogramacionCabeceraService : IReprogramacionCabeceraService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReprogramacionCabeceraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReprogramacionCabecera, ReprogramacionCabecera>(MemberList.None).ReverseMap();
                cfg.CreateMap<ReprogramacionCabecera, ReprogramacionCabeceraSinAuditoriaDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ReprogramacionCabecera Add(ReprogramacionCabecera entidad)
        {
            try
            {
                var modelo = _unitOfWork.ReprogramacionCabeceraRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ReprogramacionCabecera>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReprogramacionCabecera Update(ReprogramacionCabecera entidad)
        {
            try
            {
                var modelo = _unitOfWork.ReprogramacionCabeceraRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ReprogramacionCabecera>(modelo);
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
                _unitOfWork.ReprogramacionCabeceraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReprogramacionCabecera> Add(List<ReprogramacionCabecera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ReprogramacionCabeceraRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ReprogramacionCabecera>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReprogramacionCabecera> Update(List<ReprogramacionCabecera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ReprogramacionCabeceraRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ReprogramacionCabecera>>(modelo);
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
                _unitOfWork.ReprogramacionCabeceraRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ReprogramacionCabecera
        /// </summary>
        /// <returns> List<ReprogramacionCabeceraDTO> </returns>
        public IEnumerable<ReprogramacionCabeceraDTO> ObtenerReprogramacionCabecera()
        {
            try
            {
                return _unitOfWork.ReprogramacionCabeceraRepository.ObtenerReprogramacionCabecera();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el intervalo minimo y la maxima cantidad de programaaciones que se puede hacer por dia segun la categoria.
        /// </summary>
        /// <param name="idActividadCabecera"></param>
        /// <param name="idCategoria"></param>
        /// <returns> List<ReprogramacionCabeceraDTO> </returns>
        public ReprogramacionCabeceraRADTO ObtenerCantidadIntervaloYReprogramacionPorDiaPermitida(int idActividadCabecera, int idCategoria)
        {
            try
            {
                return _unitOfWork.ReprogramacionCabeceraRepository.ObtenerCantidadIntervaloYReprogramacionPorDiaPermitida(idActividadCabecera, idCategoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el intervalo minimo y la maxima cantidad de programaaciones que se puede hacer por dia segun la categoria.
        /// </summary>
        /// <param name="idActividadCabecera"></param>
        /// <param name="idCategoria"></param>
        /// <returns> List<ReprogramacionCabeceraPersonalRADTO> </returns>
        public ReprogramacionCabeceraPersonalRADTO ObtenerCantidadReprogramacionDelDiaPorAsesor(int idActividadCabecera, int idCategoria, int idPersonal)
        {
            try
            {
                return _unitOfWork.ReprogramacionCabeceraRepository.ObtenerCantidadReprogramacionDelDiaPorAsesor(idActividadCabecera, idCategoria, idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Todos Los Datos de la Reprogramacion Por Actividad  y Categoria.
        /// </summary>
        /// <param name="idActividadCabecera">Id de Actividad Cabecera</param>
        /// <param name="idCategoriaOrigen">Id de CategoriaOrigen</param>
        /// <returns> ReprogramacionCabeceraSinAuditoriaDTO </returns>
        public ReprogramacionCabecera ObtenerPorIdCabeceraIdCategoriaOrigen(int idActividadCabecera, int idCategoriaOrigen)
        {
            try
            {
                return _unitOfWork.ReprogramacionCabeceraRepository.ObtenerPorIdCabeceraIdCategoriaOrigen(idActividadCabecera, idCategoriaOrigen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReprogramacionCabecera MapeoEntidadDesdeDTOReprogramacion(ReprogramacionCabeceraSinAuditoriaDTO objetoDto)
        {
            try
            {
                var entidad = _mapper.Map<ReprogramacionCabecera>(objetoDto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
