using AutoMapper;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ProgramaGeneralArgumentoRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralArgumentoRepository(IntegraDBContext context, IDapperRepository dapperRepository) : base(context, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TCourier, Courier>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public object ObtenerPorId(int id)
        {
            try
            {
                //var respuesta = _unitOfWork.CourierRepository.ObtenerCourier();
                return null; // _mapper.Map<List<CourierDTO>>(respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
