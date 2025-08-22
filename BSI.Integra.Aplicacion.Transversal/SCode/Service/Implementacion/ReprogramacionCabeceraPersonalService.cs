using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ReprogramacionCabeceraPersonalService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ReprogramacionCabeceraPersonal
    /// </summary>
    public class ReprogramacionCabeceraPersonalService : IReprogramacionCabeceraPersonalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReprogramacionCabeceraPersonalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReprogramacionCabeceraPersonal, ReprogramacionCabeceraPersonal>(MemberList.None).ReverseMap();
                cfg.CreateMap<ReprogramacionCabeceraPersonal, ReprogramacionCabeceraPersonalDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ReprogramacionCabeceraPersonal Add(ReprogramacionCabeceraPersonal entidad)
        {
            try
            {
                var modelo = _unitOfWork.ReprogramacionCabeceraPersonalRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ReprogramacionCabeceraPersonal>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReprogramacionCabeceraPersonal Update(ReprogramacionCabeceraPersonal entidad)
        {
            try
            {
                var modelo = _unitOfWork.ReprogramacionCabeceraPersonalRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ReprogramacionCabeceraPersonal>(modelo);
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
                _unitOfWork.ReprogramacionCabeceraPersonalRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReprogramacionCabeceraPersonal> Add(List<ReprogramacionCabeceraPersonal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ReprogramacionCabeceraPersonalRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ReprogramacionCabeceraPersonal>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReprogramacionCabeceraPersonal> Update(List<ReprogramacionCabeceraPersonal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ReprogramacionCabeceraPersonalRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ReprogramacionCabeceraPersonal>>(modelo);
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
                _unitOfWork.ReprogramacionCabeceraPersonalRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ReprogramacionCabeceraPersonal
        /// </summary>
        /// <returns> List<ReprogramacionCabeceraPersonalDTO> </returns>
        public IEnumerable<ReprogramacionCabeceraPersonalDTO> ObtenerReprogramacionCabeceraPersonal()
        {
            try
            {
                return _unitOfWork.ReprogramacionCabeceraPersonalRepository.ObtenerReprogramacionCabeceraPersonal();
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
        /// Obtiene el registro de T_ReprogramacionCabeceraPersonal segun los parametros enviados.
        /// </summary>
        /// <param name="idActividadCabecera">Id de Actividad Cabecera</param>
        /// <param name="idCategoriaOrigen">Id de Categoria Origen</param>
        /// <param name="idPersonal">Id de Personal</param>
        /// <returns> ReprogramacionCabeceraPersonalDTO </returns>
        public ReprogramacionCabeceraPersonal ObtenerReprogramacionCabeceraPersonalAutomatica(int idActividadCabecera, int idCategoriaOrigen, int idPersonal)
        {
            try
            {
                return _unitOfWork.ReprogramacionCabeceraPersonalRepository.ObtenerPorIdActividadCabeceraIdCategoriaOrigenIdPersonal(idActividadCabecera, idCategoriaOrigen, idPersonal);
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
        /// Mapea de un ReprogramacionCabeceraPersonalDTO a ReprogramacionCabeceraPersonal
        /// </summary>
        /// <returns> ReprogramacionCabeceraPersonal </returns>
        public ReprogramacionCabeceraPersonal MapeoEntidadDesdeDTO(ReprogramacionCabeceraPersonalDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<ReprogramacionCabeceraPersonal>(dto);
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
