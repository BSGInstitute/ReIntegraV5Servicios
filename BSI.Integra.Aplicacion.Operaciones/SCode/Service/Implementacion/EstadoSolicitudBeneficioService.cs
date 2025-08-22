using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: EstadoSolicitudBeneficioService
    /// Autor: Jorge Gamero.
    /// Fecha: 02/09/2024
    /// <summary>
    /// Gestión general de T_EstadoSolicitudBeneficio
    /// </summary>
    public class EstadoSolicitudBeneficioService : IEstadoSolicitudBeneficioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EstadoSolicitudBeneficioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEstadoSolicitudBeneficio, EstadoSolicitudBeneficio>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        
        /// Autor: Jorge Gamero
        /// Fecha: 02/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.EstadoSolicitudBeneficioRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
