
using AutoMapper;

using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class MaterialAdicionalAulaVirtualRegistroService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MaterialAdicionalAulaVirtualRegistroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    //cfg.CreateMap<MaterialAdicionalAulaVirtual, MaterialAdicionalAulaVirtualDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
    }
}
