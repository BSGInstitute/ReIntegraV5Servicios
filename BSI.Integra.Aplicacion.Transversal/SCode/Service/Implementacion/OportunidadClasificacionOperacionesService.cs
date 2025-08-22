using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OportunidadClasificacionOperacionesService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_OportunidadClasificacionOperaciones
    /// </summary>
    public class OportunidadClasificacionOperacionesService : IOportunidadClasificacionOperacionesService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OportunidadClasificacionOperacionesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TOportunidadClasificacionOperacione, OportunidadClasificacionOperaciones>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los datos de T_OportunidadClasificacionOperaciones asociados a un IdOportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadDTO </returns>
        public OportunidadClasificacionOperaciones ObtenerPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadClasificacionOperacionesRepository.ObtenerPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
