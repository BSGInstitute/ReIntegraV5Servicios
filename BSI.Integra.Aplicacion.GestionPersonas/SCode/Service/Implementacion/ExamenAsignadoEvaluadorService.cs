using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class ExamenAsignadoEvaluadorService : IExamenAsignadoEvaluadorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ExamenAsignadoEvaluadorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExamenAsignadoEvaluador, ExamenAsignadoEvaluador>(MemberList.None).ReverseMap();
                cfg.CreateMap<TExamenAsignadoEvaluador, ExamenAsignadoEvaluadorDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenAsignadoEvaluador, ExamenAsignadoEvaluadorDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
    }
}
