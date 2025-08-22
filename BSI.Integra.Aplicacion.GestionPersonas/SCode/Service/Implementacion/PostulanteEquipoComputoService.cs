using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class PostulanteEquipoComputoService : IPostulanteEquipoComputoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PostulanteEquipoComputoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionProceso, PostulanteEquipoComputo>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioEvaluacionProceso, PostulanteEquipoComputoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteEquipoComputo, PostulanteEquipoComputoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
    }
}
