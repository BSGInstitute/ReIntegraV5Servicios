using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class ConfiguracionAsignacionExamenService : IConfiguracionAsignacionExamenService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ConfiguracionAsignacionExamenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionProceso, ConfiguracionAsignacionExamen>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioEvaluacionProceso, ConfiguracionAsignacionExamenDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConfiguracionAsignacionExamen, ConfiguracionAsignacionExamenDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
    }
}
