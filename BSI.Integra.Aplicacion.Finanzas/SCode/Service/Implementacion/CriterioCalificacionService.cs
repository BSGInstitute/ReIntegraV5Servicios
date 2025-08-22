using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CriterioCalificacionService
    /// Autor: Jonathan Caipo
    /// Fecha: 05/10/2022
    /// <summary>
    /// Gestión general de T_CriterioCalificacion
    /// </summary>
    public class CriterioCalificacionService : ICriterioCalificacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CriterioCalificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TCriterioCalificacion, CriterioCalificacion>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 21/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Combo Criterio Calificacion
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerCombo()
        {
            return _unitOfWork.CriterioCalificacionRepository.ObtenerCombo();
        }

    }
}
