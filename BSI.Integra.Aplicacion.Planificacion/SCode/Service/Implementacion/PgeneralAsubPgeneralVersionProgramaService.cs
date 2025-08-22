using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion
{
    /// Service: PgeneralAsubPgeneralVersionProgramaService
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 25/07/2023
    /// <summary>
    /// Gestión general de T_PgeneralAsubPgeneralVersionPrograma
    /// </summary>
    public class PgeneralAsubPgeneralVersionProgramaService : IPgeneralAsubPgeneralVersionProgramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PgeneralAsubPgeneralVersionProgramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TPgeneralAsubPgeneralVersionPrograma, PgeneralAsubPgeneralVersionPrograma>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TPgeneralAsubPgeneralVersionPrograma, PgeneralAsubPgeneralVersionProgramaDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<PgeneralAsubPgeneralVersionPrograma, PgeneralAsubPgeneralVersionProgramaDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
    }
}
