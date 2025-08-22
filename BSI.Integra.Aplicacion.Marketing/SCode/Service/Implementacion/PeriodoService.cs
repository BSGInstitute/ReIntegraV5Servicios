using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: PeriodoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 05/10/2022
    /// <summary>
    /// Gestión general de T_Periodo
    /// </summary>
    public class PeriodoService : IPeriodoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PeriodoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPeriodo, Periodo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene toda la lista de Periodos 
        /// </summary>
        /// <returns> List<PeriodoFiltroDTO> </returns>
        public List<PeriodoFiltroDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PeriodoRepository.ObtenerCombo2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene id Periodo de la fecha actual.
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        public List<FiltroDTO> ObtenerIdPeriodoFechaActual()
        {
            try
            {
                return _unitOfWork.PeriodoRepository.ObtenerIdPeriodoFechaActual();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 11/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene id y nombre
        /// </summary>
        /// <returns> DateTime: lista </returns>
        public List<FiltroIdNombreDTO> ObtenerUltimoPeriodo()
        {
            try
            {
                return _unitOfWork.PeriodoRepository.ObtenerUltimoPeriodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
