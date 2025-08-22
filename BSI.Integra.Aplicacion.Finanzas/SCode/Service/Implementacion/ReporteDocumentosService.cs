using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReporteDocumentosService
    /// Autor Modificacion: Adriana Chipana.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteDocumentos
    /// </summary>
    public class ReporteDocumentosService : IReporteDocumentosService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReporteDocumentosService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReporteDocumentos, ReporteDocumentos>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReporteDocumentosRecibidoDTO, ReporteDocumentos>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public ReporteDocumentosCompuestoDTO ObtenerReporteDocumentos(ReporteDocumentosFiltroDTO FiltroControlDocumentos)
        {
            try
            {
 
                IEnumerable<ReporteDocuemntosAgrupadoDTO> agrupado = null;
                IEnumerable<ReporteDocuemntosAgrupadoDTO> agrupado2 = null;
                IEnumerable<ReporteDocuemntosAgrupadoDTO> agrupado3 = null;

                var reporteControlDocumentos = _unitOfWork.ReporteDocumentosRepository.ObtenerReporteDocumentos(FiltroControlDocumentos).OrderByDescending(x => x.FechaCierre);

                if (FiltroControlDocumentos.Desglose == 1)
                {
                    agrupado = reporteControlDocumentos.GroupBy(x => _unitOfWork.InteraccionChatIntegraRepository.ObtenerNumeroSemana(x.FechaCierre))
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = "Semana_" + g.Key,
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            NombrePersonalAsesor = y.NombrePersonalAsesor,
                            Coordinador = y.NombrePersonalCoordinador,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP,
                            Observacion = y.Observacion,
                            PagoContado = y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1 ? 1 : 0,
                            Empresa = (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 1 && y.Empresa == 1) ? 1 : 0
                        }).ToList()

                    });

                    agrupado2 = reporteControlDocumentos.GroupBy(x => _unitOfWork.InteraccionChatIntegraRepository.ObtenerNumeroSemana(x.FechaCierre))
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = "Semana_" + g.Key,
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            NombrePersonalAsesor = y.NombrePersonalAsesor,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP,
                            Observacion = y.Observacion,
                            PagoContado = y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1 ? 1 : 0,
                            Empresa = (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 1 && y.Empresa == 1) ? 1 : 0

                        }).ToList()
                    });

                    agrupado3 = reporteControlDocumentos.GroupBy(x => _unitOfWork.InteraccionChatIntegraRepository.ObtenerNumeroSemana(x.FechaCierre))
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = "Semana_" + g.Key,
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            Coordinador = y.NombrePersonalCoordinador,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP,
                            Observacion = y.Observacion,
                            PagoContado = y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1 ? 1 : 0,
                            Empresa = (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 1 && y.Empresa == 1) ? 1 : 0

                        }).ToList()
                    });
                }
                else if (FiltroControlDocumentos.Desglose == 3)
                {
                    agrupado = reporteControlDocumentos.GroupBy(x => x.FechaCierre.Month)
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = _unitOfWork.InteraccionChatIntegraRepository.ObtenerNombreMes(g.Key),
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            NombrePersonalAsesor = y.NombrePersonalAsesor,
                            Coordinador = y.NombrePersonalCoordinador,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP,
                            Observacion = y.Observacion,
                            PagoContado = y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1 ? 1 : 0,
                            Empresa = (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 1 && y.Empresa == 1) ? 1 : 0

                        }).ToList()
                    });

                    agrupado2 = reporteControlDocumentos.GroupBy(x => x.FechaCierre.Month)
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = _unitOfWork.InteraccionChatIntegraRepository.ObtenerNombreMes(g.Key),
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            NombrePersonalAsesor = y.NombrePersonalAsesor,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP,
                            Observacion = y.Observacion,
                            PagoContado = y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1 ? 1 : 0,
                            Empresa = (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 1 && y.Empresa == 1) ? 1 : 0

                        }).ToList()
                    });

                    agrupado3 = reporteControlDocumentos.GroupBy(x => x.FechaCierre.Month)
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = _unitOfWork.InteraccionChatIntegraRepository.ObtenerNombreMes(g.Key),
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            Coordinador = y.NombrePersonalCoordinador,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP,
                            Observacion = y.Observacion,
                            PagoContado = y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1 ? 1 : 0,
                            Empresa = (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 0 && y.SinDocumentacion == 1 && y.Empresa == 1) || (y.Deuda == 0 && y.Observacion == 1 && y.SinDocumentacion == 0 && y.Empresa == 1) || (y.Deuda == 1 && y.Observacion == 1 && y.SinDocumentacion == 1 && y.Empresa == 1) ? 1 : 0

                        }).ToList()
                    });
                }

                ReporteDocumentosCompuestoDTO reporte = new ReporteDocumentosCompuestoDTO();
                reporte.ReporteDocumentosEquipo = agrupado.ToList();
                reporte.ReporteDocumentosAsesor = agrupado2.ToList();
                reporte.ReporteDocumentosCoordinador = agrupado3.ToList();

                return reporte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

}
}
