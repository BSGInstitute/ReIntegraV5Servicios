using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class FeedbackConfigurarDetalleService:IFeedbackConfigurarDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
      

        public FeedbackConfigurarDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFeedbackConfigurarDetalle, FeedbackConfigurarDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFeedbackConfigurarDetalle, FeedbackConfigurarDetalleDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<FeedbackConfigurarDetalleDTO, FeedbackConfigurarDetalle>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }
        public IEnumerable<FeedbackConfigurarDetalleDTO> ObtenerDetallePorIdFeedbackConfigurar(int IdFeedbackConfigurar)
        {
            try
            {
                if (IdFeedbackConfigurar == 0)
                {
                    throw new BadRequestException("Id 0 no valido");
                }
                var lista = _unitOfWork.FeedbackConfigurarDetalleRepository.ObtenerDetallePorIdFeedbackConfigurar(IdFeedbackConfigurar);

                return _mapper.Map<IEnumerable<FeedbackConfigurarDetalleDTO>>(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }


      
    }

}
