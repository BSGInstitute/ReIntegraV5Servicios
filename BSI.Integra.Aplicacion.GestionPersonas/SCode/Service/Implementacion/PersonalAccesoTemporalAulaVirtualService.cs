using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Web.Helpers;
using System.Web.WebPages.Html;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class PersonalAccesoTemporalAulaVirtualService : IPersonalAccesoTemporalAulaVirtualService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PersonalAccesoTemporalAulaVirtualService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionProceso, PersonalAccesoTemporalAulaVirtual>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioEvaluacionProceso, PersonalAccesoTemporalAulaVirtualDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalAccesoTemporalAulaVirtual, PersonalAccesoTemporalAulaVirtualDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPersonal, Personal>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        
    }
}
