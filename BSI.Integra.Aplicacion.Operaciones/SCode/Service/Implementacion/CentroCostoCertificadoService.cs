
using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;

using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Operacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
 
    public class CentroCostoCertificadoService : ICentroCostoCertificadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CentroCostoCertificadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TNotum, Notum>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

      
    }
}
