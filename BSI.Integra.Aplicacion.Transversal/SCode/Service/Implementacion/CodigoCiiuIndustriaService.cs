using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CodigoCiiuIndustriaService
    /// Autor: Gilmer Quispe
    /// Fecha: 07/12/2022
    /// <summary>
    /// Gestión general de T_CodigoCiiuIndustria
    /// </summary>
    public class CodigoCiiuIndustriaService : ICodigoCiiuIndustriaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CodigoCiiuIndustriaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCodigoCiiuIndustrium, CodigoCiiuIndustria>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_CodigoCiiuIndustria con el estado=1.
        /// </summary> 
        /// <returns> CodigoCiiuIndustria </returns>
        public CodigoCiiuIndustria ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.CodigoCiiuIndustriaRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_CodigoCiiuIndustria con el estado=1 y filtro de Nombre
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public List<ComboDTO> ObtenerPorNombre(string filtro)
        {
            try
            {
                return _unitOfWork.CodigoCiiuIndustriaRepository.ObtenerPorNombre(filtro).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
