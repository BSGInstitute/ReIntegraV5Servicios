
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ProgramaGeneralModeloCertificadoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 19/09/2022
    /// <summary>
    /// Gestión general de ProgramaGeneralModeloCertificado
    /// </summary>
    public class ProgramaGeneralModeloCertificadoService : IProgramaGeneralModeloCertificadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralModeloCertificadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralModeloCertificado, ProgramaGeneralModeloCertificado>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de modelos de certificados
        /// </summary>
        /// <returns> List<PGeneralPublicoObjetivoDTO> </returns>
        public List<PGeneralModeloCertificadoDTO> ObtenerModeloCertificadoPrograma(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralModeloCertificadoRepository.ObtenerModeloCertificadoPrograma(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
