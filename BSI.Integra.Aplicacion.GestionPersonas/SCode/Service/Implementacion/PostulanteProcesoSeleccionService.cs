using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class PostulanteProcesoSeleccionService : IPostulanteProcesoSeleccionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PostulanteProcesoSeleccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulanteProcesoSeleccion, PostulanteProcesoSeleccion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPostulanteProcesoSeleccion, PostulanteProcesoSeleccionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteProcesoSeleccion, PostulanteProcesoSeleccionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
    }
}
