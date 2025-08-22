using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Operacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Operacion;
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
    public class EsquemaEvaluacionDetalleService:IEsquemaEvaluacionDetalleService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EsquemaEvaluacionDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEsquemaEvaluacionDetalle, EsquemaEvaluacionDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<EsquemaEvaluacionDetalle, TEsquemaEvaluacionDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<EsquemaEvaluacionDetalleDTO, EsquemaEvaluacionDetalle>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);

        }


        public EsquemaEvaluacionDetalleDTO? ObtenerPorId(int id)
        {
            try
            {

                return _mapper.Map<EsquemaEvaluacionDetalleDTO>(_unitOfWork.EsquemaEvaluacionDetalleRepository.ObtenerPorId(id));
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<EsquemaEvaluacionDetalleDTO> ObtenerPorIdEsquemaEvaluacion(int idEsquemaEvaluacion)
        {
            
            try
            {
                if (idEsquemaEvaluacion == 0)
                {
                    throw new BadRequestException("Id 0 no valido");
                }
                var lista = _unitOfWork.EsquemaEvaluacionDetalleRepository.ObtenerPorIdEsquemaEvaluacion(idEsquemaEvaluacion);

                return _mapper.Map<IEnumerable<EsquemaEvaluacionDetalleDTO>>(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
