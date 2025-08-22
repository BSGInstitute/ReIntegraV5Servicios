using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    /// Service: ReporteMensajesWhatsAppService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de Informacion de Oportunidades
    /// </summary>
    public class ReporteMensajesWhatsAppService : IReporteMensajesWhatsAppService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AgendaDTO agendaBo = new AgendaDTO();
        public ReporteMensajesWhatsAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, Entity>(MemberList.None).ReverseMap());
            //_mapper = new Mapper(config);
        }
        public List<ObtenerReporteMensajesWhatsAppPorTipoDTO> ObtenerReporteMensajesWhatsApp(ReporteMensajesWhatsAppFiltrosDTO filtros)
        {
            try
            {
                ReporteMensajesWhatsAppFiltrosDTO _new = new ReporteMensajesWhatsAppFiltrosDTO();
                List<int> asesoresFinal = new List<int>();

                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var data = _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.ObtenerReporteMensajesWhatsApp(_new);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivosPorArea(ReporteMensajesWhatsAppPorAreaFiltrosDTO filtros)
        {
            try
            {
                ReporteMensajesWhatsAppFiltrosDTO FiltroSoloFechas = new ReporteMensajesWhatsAppFiltrosDTO();
                FiltroSoloFechas.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                FiltroSoloFechas.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);



                List<ReporteWhatsAppEnvioMasivoDTO> data;
                if (filtros.IdArea == 0)
                    data = _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.GenerarReporteMensajesMasivos(FiltroSoloFechas);
                else
                    data = _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.GenerarReporteMensajesMasivosPorArea(filtros);


                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivosConjuntoLista(ReporteWhatsAppMasivoFiltrosDTO filtros)
        {
            try
            {
                ReporteWhatsAppMasivoFiltrosDTO _new = new ReporteWhatsAppMasivoFiltrosDTO();
                List<int> asesoresFinal = new List<int>();

                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                _new.IdPersonal = filtros.IdPersonal;
                _new.IdPais = filtros.IdPais;

                var data = _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.GenerarReporteMensajesMasivosConjuntoLista(_new);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
