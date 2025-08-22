using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class ExamenAsignadoService : IExamenAsignadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ExamenAsignadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExamenAsignado, ExamenAsignado>(MemberList.None).ReverseMap();
                cfg.CreateMap<TExamenAsignado, ExamenAsignadoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenAsignado, ExamenAsignadoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
    }
}
