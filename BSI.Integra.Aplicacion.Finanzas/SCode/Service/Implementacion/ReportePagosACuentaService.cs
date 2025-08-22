using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReportePagosACuentaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePagosACuenta
    /// </summary>
    public class ReportePagosACuentaService : IReportePagosACuentaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReportePagosACuentaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReportePagosACuenta, ReportePagosACuenta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReportePagosACuentaRecibidoDTO, ReportePagosACuenta>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// <summary>
        /// Obtiene el Reporte Pagos a Cuenta Contable
        /// </summary>
        /// <returns></returns>
        public ReportePagosFinalDTO ObtenerReportePagosACuenta(string Anio)
        {
            try
            {
                ReportePagosFinalDTO agrupadoFinal = new ReportePagosFinalDTO();
                var reportePagos = new ReporteService(_unitOfWork).ObtenerReportePagosACuenta(Anio);

                var agrupacionReporteGeneral = new ReportePagosACuentaGeneralDTO
                {
                    ListaPrincipal = reportePagos
                        .GroupBy(r => new { r.Rubro, r.NroCuenta, r.Cuenta })
                        .Select(g => new ReportePrincipalDTO
                        {
                            Rubro = g.Key.Rubro,
                            NroCuenta = g.Key.NroCuenta,
                            Cuenta = g.Key.Cuenta
                        }).ToList(),
                    Anios = reportePagos
                        .GroupBy(r => r.AnioPago)
                        .OrderByDescending(ag => ag.Key) // Ordenar por año de mayor a menor
                        .Select(ag => new AnioAgrupadoDTO
                        {
                            AnioPago = ag.Key,
                            Periodos = ag.GroupBy(p => p.MesPago)
                                        .Select(pg => new PeriodoAgrupadoDTO
                                        {
                                            MesPago = pg.Key,
                                            Detalles = pg
                                            .GroupBy(pgc => new { pgc.NroCuenta, pgc.Cuenta, pgc.MesPago })
                                            .Select(d => new PeriodoPagoCuentaDTO
                                            {
                                                NroCuenta = d.Key.NroCuenta,
                                                Cuenta = d.Key.Cuenta,
                                                Periodo = d.Key.MesPago,
                                                MontoPago = d.Sum(r => r.MontoPagadoDolar)
                                            }).ToList()
                                        }).ToList()
                        }).ToList()
                };

                var agrupacionReporteDetalle = reportePagos
                    .GroupBy(r => r.AnioPago)
                    .OrderByDescending(ag => ag.Key)
                    .Select(ag => new ReportePagosACuentaDetalleDTO
                    {
                        AnioPago = ag.Key,
                        ListaPrincipal = ag
                            .GroupBy(r => new { r.Rubro, r.NroCuenta, r.Cuenta })
                            .Select(g => new ReportePrincipalDTO
                            {
                                Rubro = g.Key.Rubro,
                                NroCuenta = g.Key.NroCuenta,
                                Cuenta = g.Key.Cuenta
                            }).ToList(),
                        Periodos = ag
                            .GroupBy(r => r.MesPago)
                            .Select(pg => new PeriodoAgrupadoDeudaDTO
                            {
                                Periodo = pg.Key,
                                AniosDeuda = pg.GroupBy(d => d.AnioPagoEjecucion)
                                            .OrderBy(d => d.Key) // Ordenar por año de deuda de menor a mayor
                                            .Select(ad => new AnioDeudaDTO
                                            {
                                                AnioDeuda = ad.Key,
                                                PeriodosDeuda = ad.GroupBy(dp => dp.MesPagoEjecucion)
                                                                .Select(dp => new PeriodoDeudaDTO
                                                                {
                                                                    MesDeuda = dp.Key,
                                                                    DetallesDeuda = dp
                                                                    .GroupBy(pgc => new { pgc.NroCuenta, pgc.Cuenta, pgc.MesPagoEjecucion })
                                                                    .Select(d => new DetalleDeudaDTO
                                                                    {
                                                                        NroCuenta = d.Key.NroCuenta,
                                                                        Cuenta = d.Key.Cuenta,
                                                                        PeriodoDeuda = d.Key.MesPagoEjecucion,
                                                                        MontoDeuda = d.Sum(de => de.MontoPagadoDolar)
                                                                    }).ToList()
                                                                }).ToList()
                                            }).ToList()
                            }).ToList()
                    }).ToList();

                agrupadoFinal.ReporteGeneral = agrupacionReporteGeneral;
                agrupadoFinal.ReporteDetallado = agrupacionReporteDetalle;

                return agrupadoFinal;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

        }

        /// <summary>
        /// Obtiene las tasas de Cambio para El reporte Pagos Por Cuenta Contable
        /// </summary>
        /// <returns></returns>
        public List<TasaCambioReportePagosDTO> ObtenerTasaCambioReportePagoACuenta(string Anios)
        {
            try
            {
                var repositoriReporte = _unitOfWork.ReportesRepository;
                return repositoriReporte.ObtenerTasaCambioReportePagoACuenta(Anios);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
