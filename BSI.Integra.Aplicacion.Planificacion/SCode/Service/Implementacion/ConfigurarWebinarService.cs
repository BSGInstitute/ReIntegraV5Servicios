using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion
{
    /// Service: ConfigurarWebinarService
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 31/05/2023
    /// <summary>
    /// Gestión general de T_ConfigurarWebinar
    /// </summary>
    public class ConfigurarWebinarService : IConfigurarWebinarService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ConfigurarWebinarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TConfigurarWebinar, ConfigurarWebinar>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TConfigurarWebinar, ConfigurarWebinarDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ConfigurarWebinar, ConfigurarWebinarDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2023
        /// <summary>
        /// Obtiene las configuraciones webinar por id pespecifico padre
        /// </summary>
        public IEnumerable<ConfigurarWebinarDTO> ObtenerPorIdPespecificoPadre(int idPEspecificoPadre)
        {
            return _unitOfWork.ConfigurarWebinarRepository.ObtenerPorIdPespecificoPadre(idPEspecificoPadre);
        }
    }
}
