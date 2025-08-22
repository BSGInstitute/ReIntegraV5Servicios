using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: OportunidadInformacionService
    /// Autor: Gilmer Qm.
    /// Fecha: 02/05/2023
    /// <summary>
    /// Gestión general de Parametro Seo Pw
    /// </summary>
    public class ParametroSeoPwService : IParametroSeoPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ParametroSeoPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, Entity>(MemberList.None).ReverseMap());
            //_mapper = new Mapper(config);
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 02/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo de T_ParametroSeoPw
        /// </summary> 
        /// <returns> List<ParametroSeoPwDTO> </returns>
        public IEnumerable<ParametroSeoPwDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ParametroSeoPwRepository.ObtenerCombo();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
