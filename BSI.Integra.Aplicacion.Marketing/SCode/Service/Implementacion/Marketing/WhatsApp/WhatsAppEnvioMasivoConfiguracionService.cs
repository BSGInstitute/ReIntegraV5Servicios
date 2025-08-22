using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp
{
    public class WhatsAppEnvioMasivoConfiguracionService: IWhatsAppEnvioMasivoConfiguracionService
    {
        private Mapper _mapper;
        private readonly IUnitOfWork unitOfWork;

        public WhatsAppEnvioMasivoConfiguracionService(IUnitOfWork unitOfWork)
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TConfiguracionDeEnvioParaWhatsApp, ConfiguracionDeEnvioParaWhatsAppDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
            this.unitOfWork = unitOfWork;
        }
        public void CalcularCantidadDeContactoPorPrioridad(int prioridad)
        {

        }
    }
}
