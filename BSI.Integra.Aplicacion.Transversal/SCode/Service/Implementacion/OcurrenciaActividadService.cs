using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OcurrenciaActividadService
    /// Autor: Jonathan Caipo
    /// Fecha: 15/12/2022
    /// <summary>
    /// Gestión general de T_OcurrenciaActividad
    /// </summary>
    public class OcurrenciaActividadService : IOcurrenciaActividadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OcurrenciaActividadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOcurrenciaActividad, OcurrenciaActividad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de Ocurrencias de acuerdo al IdActividadCabecera y IdOcurrenciaPadre de la actividad 
        /// abierta por el Asesor
        /// </summary>
        /// <param name="ocurrenciaActividad"></param>
        public List<ArbolOcurrenciaDTO> ObtenerArbolOcurrencia(int idActividadCabecera, int idOcurrenciaActividadPadre)
        {
            try
            {
                return _unitOfWork.OcurrenciaActividadRepository.ObtenerArbolOcurrencia(idActividadCabecera, idOcurrenciaActividadPadre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
