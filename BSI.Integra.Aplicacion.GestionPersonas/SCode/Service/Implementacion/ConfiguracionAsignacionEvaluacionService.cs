using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class ConfiguracionAsignacionEvaluacionService : IConfiguracionAsignacionEvaluacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ConfiguracionAsignacionEvaluacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionAsignacionEvaluacion, ConfiguracionAsignacionEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TConfiguracionAsignacionEvaluacion, ConfiguracionAsignacionEvaluacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConfiguracionAsignacionEvaluacion, ConfiguracionAsignacionEvaluacionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
    }
}
