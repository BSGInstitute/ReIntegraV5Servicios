using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AlumnoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Alumno
    /// </summary>
    public class EstadoCertificadoFisicoService : IEstadoCertificadoFisicoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EstadoCertificadoFisicoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoCertificadoFisico, EstadoCertificadoFisico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Estado para Filtro
        /// </summary>
        /// <returns></returns>
        public List<EstadoCertificadoFisicoDTO> ObtenerEstadParaFiltro()
        {
            try
            {
                return _unitOfWork.EstadoCertificadoFisicoRepository.ObtenerEstadParaFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
