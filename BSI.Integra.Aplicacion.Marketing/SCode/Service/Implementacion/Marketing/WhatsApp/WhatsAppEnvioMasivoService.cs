using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp
{
    public class WhatsAppEnvioMasivoService: IWhatsAppEnvioMasivoService
    {
        private Mapper _mapper;
        private readonly IUnitOfWork unitOfWork;

        public WhatsAppEnvioMasivoService(IUnitOfWork unitOfWork)
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TActividadDetalle, ActividadDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
            this.unitOfWork = unitOfWork;
        }
    }
}
