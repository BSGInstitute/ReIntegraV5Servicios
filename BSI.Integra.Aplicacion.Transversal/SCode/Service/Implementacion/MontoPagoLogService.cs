using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public class MontoPagoLogService : IMontoPagoLogService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public MontoPagoLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMontoPagoLog, MontoPagoLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<MontoPagoLog, MontoPagoLogDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<MontoPagoLogDTO, MontoPagoLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMontoPagoLog, MontoPagoLogDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        public IEnumerable<MontoPagoLogDTO> ObtenerReporteMontoPagoHistorico(FiltroMontoPagoHistoricoDTO filtro)
        {
            return _unitOfWork.MontoPagoLogRepository.ObtenerReporteMontoPagoHistorico(filtro);
        }


    }
}
