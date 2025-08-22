using AutoMapper;
using BSI.Integra.Aplicacion.DTO;

using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class ProgramaGeneralPresentacionArgumentoModalidadService : IProgramaGeneralPresentacionArgumentoModalidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralPresentacionArgumentoModalidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralPresentacionArgumentoModalidad, ProgramaGeneralPresentacionArgumentoModalidad>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TProgramaGeneralPresentacionArgumentoModalidad, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
    }
}
