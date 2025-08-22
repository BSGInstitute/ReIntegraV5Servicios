using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: FrecuenciaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_Frecuencia
    /// </summary>
    public class FrecuenciaService : IFrecuenciaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public FrecuenciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFrecuencium, Frecuencia>(MemberList.None).ReverseMap();
                cfg.CreateMap<FrecuenciaDTO, Frecuencia>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        } 
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Frecuencia
        /// </summary>
        /// <returns> List<FrecuenciaDTO> </returns>
        public IEnumerable<FrecuenciaDTO> ObtenerFrecuencia()
        {
            try
            {
                return _unitOfWork.FrecuenciaRepository.ObtenerFrecuencia();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DatosFrecuenciaGeneralDTO> ObtenerListaFrecuenciaActividad()
        {
            try
            {
                return _unitOfWork.FrecuenciaRepository.ObtenerListaFrecuenciaActividad();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DatosFrecuenciaGeneralDTO> ObtenerFrecuenciaReporteDocumentos()
        {
            try
            {
                return _unitOfWork.FrecuenciaRepository.ObtenerFrecuenciaReporteDocumentos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
