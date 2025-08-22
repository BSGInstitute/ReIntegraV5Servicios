using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConfiguracionAccesoPersonalService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 23/07/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionAccesoPersonal
    /// </summary>
    public class ConfiguracionAccesoPersonalService : IConfiguracionAccesoPersonalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper mapperConfiguracionAccesoPersonal;

        public ConfiguracionAccesoPersonalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConfiguracionAccesoPersonal, ConfiguracionAccesoPersonal>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
            mapperConfiguracionAccesoPersonal = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ConfiguracionAccesoPersonal por idPersonal
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> Lista ConfiguracionAccesoPersonalDTO </returns>
        public IEnumerable<ConfiguracionAccesoPersonalDTO> ObtenerPorIdPersonal(int idPersonal)
        {
            try
            {
                return _unitOfWork.ConfiguracionAccesoPersonalRepository.ObtenerPorIdPersonal(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ConfiguracionAccesoPersonal por idPersonal idmodulo
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <param name="idModulo">Id del Modulo</param>
        /// <returns> Lista ConfiguracionAccesoPersonalDTO </returns>
        public ConfiguracionAccesoPersonalDTO? ObtenerPorIdPersonalIdModulo(int idPersonal, int idModulo)
        {
            try
            {
                return _unitOfWork.ConfiguracionAccesoPersonalRepository.ObtenerPorIdPersonalIdModulo(idPersonal, idModulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ConfiguracionAccesoPersonal por idPersonal idmodulo
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <param name="idModulo">Id del Modulo</param>
        /// <returns> Lista ConfiguracionAccesoPersonalDTO </returns>
        public int ObtenerIdPersonalAcceso(int idPersonal, string urlModulo)
        {
            try
            {
                var resultado = _unitOfWork.ConfiguracionAccesoPersonalRepository.ObtenerPorIdPersonalUrlModulo(idPersonal, urlModulo);
                if (resultado != null && resultado.Id != 0)
                {
                    if (resultado.FechaExpiracion != null)
                    {
                        if (resultado.FechaExpiracion > DateTime.Now)
                        {
                            return resultado.IdPersonalAcceso;
                        }
                        else
                        {
                            return idPersonal;
                        }
                    }
                    else
                    {
                        return resultado.IdPersonalAcceso;
                    }
                }
                else
                {
                    return idPersonal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
