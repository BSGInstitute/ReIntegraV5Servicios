using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReportePendienteMesCoordinadoraService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePendienteMesCoordinadora
    /// </summary>
    public class ReportePendienteMesCoordinadoraService : IReportePendienteMesCoordinadoraService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReportePendienteMesCoordinadoraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReportePendienteMesCoordinadora, ReportePendienteMesCoordinadora>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReportePendienteMesCoordinadoraRecibidoDTO, ReportePendienteMesCoordinadora>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Griselberto Huaman C.
        /// Fecha: 29/09/2023
        /// <summary>
        /// Obtiene el reporte de pendientes por mes coordinador
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        public List<ReportePendientePeriodoyCoordinadorSeparadoDTO> ObtenerReportePendientePeriodoyCoordinadorPorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente)
        {
            try
            {
                var _repPeriodo = _unitOfWork.PeriodoRepository;
                var XfechaInicio = _repPeriodo.ObtenerFechaInicial(filtroPendiente.PeriodoInicial);
                var XfechaFin = _repPeriodo.ObtenerFechaFinal(filtroPendiente.PeriodoFin);

                var fechaInicio = Convert.ToDateTime(XfechaInicio.Valor);
                var fechaFin = Convert.ToDateTime(XfechaFin.Valor);

                filtroPendiente .FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                filtroPendiente.FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);

                return _unitOfWork.ReportePendienteMesCoordinadoraRepository.ObtenerReportePendientePeriodoyCoordinadorPorMesCoordinador(filtroPendiente);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReportePendientePeriodoyCoordinadorSeparadoCierreDTO> ObtenerReportePendienteCierrePorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente)
        {
            try
            {
                var _repPeriodo = _unitOfWork.PeriodoRepository;
                var XfechaInicio = _repPeriodo.ObtenerFechaInicial(filtroPendiente.PeriodoInicial);
                var XfechaFin = _repPeriodo.ObtenerFechaFinal(filtroPendiente.PeriodoFin);

                var fechaInicio = Convert.ToDateTime(XfechaInicio.Valor);
                var fechaFin = Convert.ToDateTime(XfechaFin.Valor);

                filtroPendiente.FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                filtroPendiente.FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);

                return _unitOfWork.ReportePendienteMesCoordinadoraRepository.ObtenerReportePendienteCierrePorMesCoordinador(filtroPendiente);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public ReportePendienteCompuestoDTO ProcesarReporteMesCoordinadora(ResultadoGeneralMesCoordinadoraDTO respuestaGeneral)
        {
            try
            {
                var listRpta = GenerarReportePendientePorPeriodoOperacionesMesCoordinador(respuestaGeneral);
                var agrupado = (from p in listRpta
                                group p by p.Periodo into grupo
                                select new ReportePendienteDTO { g = grupo.Key, l = grupo.ToList() });

                var listRpta4 = GenerarReportePendientePeriodoyCoordinadorOperacionesMesCoordinador(respuestaGeneral);
                var agrupado4 = (from p in listRpta4
                                 group p by p.Periodo into grupo
                                 select new ReportePendientePorCoordinadorDTO { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReportePendienteCompuestoDTO reporte = new ReportePendienteCompuestoDTO();
                reporte.ReportePendientePorPeriodo = agrupado.ToList();
                reporte.ReportePendientePeriodoYCoordinador = agrupado4.ToList();
                return reporte;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }




        /// Autor: Griselberto H.
        /// Fecha: 2023-10-03
        /// <summary>
        /// Obtiene el Reporte de Pendientes Por Mes Coordinador para el reporte por mes
        /// </summary>
        /// <returns>ReportePendienteDetalleFinalDTO</returns>
        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendientePorPeriodoOperacionesMesCoordinador(ResultadoGeneralMesCoordinadoraDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.Periodo
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReportePendientePeriodoyCoordinadorSeparadoDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       ProyectadoInicialCoordinador = grupo.Select(x => x.ProyectadoInicialCoordinador).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       Diferencia = grupo.Select(x => x.Diferencia).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                       DiferenciaRetirosCD = grupo.Select(x => x.DiferenciaRetirosCD).Sum(),
                                       DiferenciaRetirosSD = grupo.Select(x => x.DiferenciaRetirosSD).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       IngresoVentas = grupo.Select(x => x.IngresoVentas).Sum(),
                                       MontoRecuperadoMes = grupo.Select(x => x.MontoRecuperadoMes).Sum(),
                                       PagosAdelantadoAcumulado = grupo.Select(x => x.PagosAdelantadoAcumulado).Sum(),
                                       PendientePorFactura = grupo.Select(x => x.PendientePorFactura).Sum(),
                                       PendienteSinFactura = grupo.Select(x => x.PendienteSinFactura).Sum(),
                                       ProyectadoInicialMes = grupo.Select(x => x.ProyectadoInicialMes).Sum(),
                                       Modificacion = grupo.Select(X => X.Modificacion).Sum(),
                                   });

            var agrupadoGeneralCierre = (from r in respuestaGeneral.Cierre
                                         group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                         select new ReportePendientePeriodoDTO
                                         {
                                             PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                             Proyectado = grupo.Select(x => x.ProyectadoInicialMes).Sum(),
                                             Actual = grupo.Select(x => x.Actual).Sum(),
                                             Diferencia = grupo.Select(x => x.Diferencia).Sum(),
                                             DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                             DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                             DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                             DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                             DiferenciaRetirosCD = grupo.Select(x => x.DiferenciaRetirosCD).Sum(),
                                             DiferenciaRetirosSD = grupo.Select(x => x.DiferenciaRetirosSD).Sum(),
                                             MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                             IngresoVentas = grupo.Select(x => x.IngresoVentas).Sum(),
                                             MontoRecuperadoMes = grupo.Select(x => x.MontoRecuperadoMes).Sum(),
                                             PagosAdelantadoAcumulado = grupo.Select(x => x.ProyectadoInicialCoordinadora).Sum(),
                                             PendientePorFactura = grupo.Select(x => x.PendientePorFactura).Sum(),
                                             PendienteSinFactura = grupo.Select(x => x.PendienteSinFactura).Sum(),
                                         });

            var entitiesCierre = agrupadoGeneralCierre.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var entitiesMontos = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);

            var unionGeneralCierre = (from montos in agrupadoGeneral
                                      join cierre in agrupadoGeneralCierre on montos.PeriodoPorFechaVencimiento equals cierre.PeriodoPorFechaVencimiento
                                      into gj
                                      from subcierre in gj.DefaultIfEmpty()

                                      select new ReportePendientePeriodoCierreDTO
                                      {
                                          PeriodoPorFechaVencimiento = montos.PeriodoPorFechaVencimiento,
                                          Proyectado = montos.ProyectadoInicialCoordinador,
                                          Actual = montos.Actual,
                                          Diferencia = montos.Diferencia,
                                          DiferenciaCambioFecha = montos.DiferenciaCambioFecha,
                                          DiferenciaCambioMonto = montos.DiferenciaCambioMonto,
                                          DiferenciaConsiderarMoraAdelantoSgteCuota = montos.DiferenciaConsiderarMoraAdelantoSgteCuota,
                                          DiferenciaModificacionNroCuotas = montos.DiferenciaModificacionNroCuotas,
                                          DiferenciaRetirosCD = montos.DiferenciaRetirosCD,
                                          DiferenciaRetirosSD = montos.DiferenciaRetirosSD,
                                          MontoPagado = montos.MontoPagado,
                                          IngresoVentas = montos.IngresoVentas,
                                          MontoRecuperadoMes = montos.MontoRecuperadoMes,
                                          PagosAdelantadoAcumulado = montos.PagosAdelantadoAcumulado,
                                          PendientePorFactura = montos.PendientePorFactura,
                                          PendienteSinFactura = montos.PendienteSinFactura,
                                          ProyectadoInicial = montos.ProyectadoInicialMes,
                                          Modificacion = montos.Modificacion,
                                          ProyectadoCierre = subcierre == null ? 0 : subcierre.Proyectado,
                                          ActualCierre = subcierre == null ? 0 : subcierre.Actual,
                                          MontoPagadoCierre = subcierre == null ? 0 : subcierre.MontoPagado,
                                          IngresoVentasCierre = subcierre == null ? 0 : subcierre.IngresoVentas,
                                          MontoRecuperadoMesCierre = subcierre == null ? 0 : subcierre.MontoRecuperadoMes,
                                          PagosAdelantadoAcumuladoCierre = subcierre == null ? 0 : subcierre.PagosAdelantadoAcumulado,
                                      });


            var entities = unionGeneralCierre.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReportePendienteDetalleDTO> detalles = new List<ReportePendienteDetalleDTO>();

            ReportePendienteDetalleDTO detalle1 = new ReportePendienteDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle1);



            ReportePendienteDetalleDTO detalle30 = new ReportePendienteDetalleDTO();
            detalle30.Tipo = "Modificacion mes actual($)";
            detalle30.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle30);

            ReportePendienteDetalleDTO detalle31 = new ReportePendienteDetalleDTO();
            detalle31.Tipo = "Modificacion mes anterior($)";
            detalle31.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle31);
            //////////////////////////////////inicio
            ReportePendienteDetalleDTO detalle6 = new ReportePendienteDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReportePendienteDetalleDTO detalle7 = new ReportePendienteDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle7);
            ///////////////////////////////////fin
            ReportePendienteDetalleDTO detalle8 = new ReportePendienteDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReportePendienteDetalleDTO detalle12 = new ReportePendienteDetalleDTO();
            detalle12.Tipo = "Ingreso Ventas($)";
            detalle12.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle12);

            ReportePendienteDetalleDTO detalle13 = new ReportePendienteDetalleDTO();
            detalle13.Tipo = "Proyectado Inicial menos Ventas($)";
            detalle13.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle13);

            ReportePendienteDetalleDTO detalle18 = new ReportePendienteDetalleDTO();
            detalle18.Tipo = "Proyectado Actual menos Ventas($)";
            detalle18.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle18);

            ReportePendienteDetalleDTO detalle9 = new ReportePendienteDetalleDTO();
            detalle9.Tipo = "Real al " + respuestaGeneral.FechaCierreActual + " ($)";
            detalle9.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReportePendienteDetalleDTO detalle14 = new ReportePendienteDetalleDTO();
            detalle14.Tipo = "Recuperacion en el mes al " + respuestaGeneral.FechaCierreActual + " ($)";
            detalle14.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle14);

            ReportePendienteDetalleDTO detalle15 = new ReportePendienteDetalleDTO();
            detalle15.Tipo = "Pendiente al " + respuestaGeneral.FechaCierreActual + " ($)";
            detalle15.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle15);

            ReportePendienteDetalleDTO detalle16 = new ReportePendienteDetalleDTO();
            detalle16.Tipo = "Pendiente por Factura al " + respuestaGeneral.FechaCierreActual + " ($)";
            detalle16.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle16);

            ReportePendienteDetalleDTO detalle17 = new ReportePendienteDetalleDTO();
            detalle17.Tipo = "Pendiente sin Factura al " + respuestaGeneral.FechaCierreActual + " ($)";
            detalle17.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle17);

            ReportePendienteDetalleDTO detalle10 = new ReportePendienteDetalleDTO();
            detalle10.Tipo = "% Sobre Proyectado Inicial al " + respuestaGeneral.FechaCierreActual;
            detalle10.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle10);

            ReportePendienteDetalleDTO detalle11 = new ReportePendienteDetalleDTO();
            detalle11.Tipo = "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierreActual;
            detalle11.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle11);

            // Fecha Corte 2 

            ReportePendienteDetalleDTO detalle25 = new ReportePendienteDetalleDTO();
            detalle25.Tipo = "Proyectado Inicial menos Ventas Cierre($)";
            detalle25.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle25);

            ReportePendienteDetalleDTO detalle26 = new ReportePendienteDetalleDTO();
            detalle26.Tipo = "Proyectado Actual menos Ventas Cierre($)";
            detalle26.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle26);

            ReportePendienteDetalleDTO detalle20 = new ReportePendienteDetalleDTO();
            detalle20.Tipo = "Real al " + respuestaGeneral.FechaCierrePrevio + " ($)";
            detalle20.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle20);

            ReportePendienteDetalleDTO detalle21 = new ReportePendienteDetalleDTO();
            //detalle21.Tipo = "Recuperacion en el mes al " + respuestaGeneral.FechaCierrePrevio + " ($)";
            detalle21.Tipo = "Recuperacion en el mes al " + respuestaGeneral.FechaCierrePrevio + " ($)";
            detalle21.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle21);

            ReportePendienteDetalleDTO detalle23 = new ReportePendienteDetalleDTO();
            detalle23.Tipo = "Pendiente al " + respuestaGeneral.FechaCierrePrevio + " ($)";
            detalle23.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle23);

            ReportePendienteDetalleDTO detalle27 = new ReportePendienteDetalleDTO();
            detalle27.Tipo = "% Sobre Proyectado Inicial al " + respuestaGeneral.FechaCierrePrevio;
            detalle27.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle27);

            ReportePendienteDetalleDTO detalle24 = new ReportePendienteDetalleDTO();
            detalle24.Tipo = "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierrePrevio;
            detalle24.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle24);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReportePendienteDetallesMesesDTO detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.ProyectadoInicial.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);


                ReportePendienteDetallesMesesDTO detallemes30 = new ReportePendienteDetallesMesesDTO();
                detallemes30.Mes = item.PeriodoPorFechaVencimiento;
                detallemes30.Monto = item.Modificacion.ToString();
                detalles.Where(w => w.Tipo == "Modificacion mes actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes30);

                ReportePendienteDetallesMesesDTO detallemes31 = new ReportePendienteDetallesMesesDTO();
                detallemes31.Mes = item.PeriodoPorFechaVencimiento;
                detallemes31.Monto = ((item.Actual + item.IngresoVentas - item.ProyectadoInicial + item.DiferenciaRetirosCD + item.DiferenciaRetirosSD) - (item.Modificacion)).ToString();
                detalles.Where(w => w.Tipo == "Modificacion mes anterior($)").FirstOrDefault().ListaMontosMeses.Add(detallemes31);
                /////////////////////inicio

                //Retiros Con Devolucion
                ReportePendienteDetallesMesesDTO detallemes6 = new ReportePendienteDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.DiferenciaRetirosCD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReportePendienteDetallesMesesDTO detallemes7 = new ReportePendienteDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.DiferenciaRetirosSD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                /////////////////////fin

                //Actual
                ReportePendienteDetallesMesesDTO detallemes8 = new ReportePendienteDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = (item.Actual + item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);


                //Ingreso Ventas
                ReportePendienteDetallesMesesDTO detallemes12 = new ReportePendienteDetallesMesesDTO();
                detallemes12.Mes = item.PeriodoPorFechaVencimiento;
                detallemes12.Monto = item.IngresoVentas.ToString();
                detalles.Where(w => w.Tipo == "Ingreso Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //Proyectado Inicial menos Ventas
                ReportePendienteDetallesMesesDTO detallemes13 = new ReportePendienteDetallesMesesDTO();
                detallemes13.Mes = item.PeriodoPorFechaVencimiento;
                detallemes13.Monto = (item.ProyectadoInicial - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes13);

                //Proyectado Actual menos Ventas
                ReportePendienteDetallesMesesDTO detallemes18 = new ReportePendienteDetallesMesesDTO();
                detallemes18.Mes = item.PeriodoPorFechaVencimiento;
                detallemes18.Monto = (item.Actual).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes18);

                //MontoPagado
                ReportePendienteDetallesMesesDTO detallemes9 = new ReportePendienteDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = (item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Real al " + respuestaGeneral.FechaCierreActual + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Recuperacion en el Mes
                ReportePendienteDetallesMesesDTO detallemes14 = new ReportePendienteDetallesMesesDTO();
                detallemes14.Mes = item.PeriodoPorFechaVencimiento;
                detallemes14.Monto = item.MontoRecuperadoMes.ToString();
                detalles.Where(w => w.Tipo == "Recuperacion en el mes al " + respuestaGeneral.FechaCierreActual + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes14);


                //Pendiente
                ReportePendienteDetallesMesesDTO detallemes15 = new ReportePendienteDetallesMesesDTO();
                detallemes15.Mes = item.PeriodoPorFechaVencimiento;
                detallemes15.Monto = (item.PendientePorFactura + item.PendienteSinFactura).ToString();
                detalles.Where(w => w.Tipo == "Pendiente al " + respuestaGeneral.FechaCierreActual + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                //Pendiente por factura
                ReportePendienteDetallesMesesDTO detallemes16 = new ReportePendienteDetallesMesesDTO();
                detallemes16.Mes = item.PeriodoPorFechaVencimiento;
                detallemes16.Monto = item.PendientePorFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente por Factura al " + respuestaGeneral.FechaCierreActual + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes16);

                //Pendiente sin factura
                ReportePendienteDetallesMesesDTO detallemes17 = new ReportePendienteDetallesMesesDTO();
                detallemes17.Mes = item.PeriodoPorFechaVencimiento;
                detallemes17.Monto = item.PendienteSinFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente sin Factura al " + respuestaGeneral.FechaCierreActual + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes17);

                //%CobradoInicial
                ReportePendienteDetallesMesesDTO detallemes10 = new ReportePendienteDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                if (item.ProyectadoInicial - item.IngresoVentas == 0)
                {
                    detallemes10.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    var resPY_IV = item.ProyectadoInicial - item.IngresoVentas;
                    detallemes10.Monto = "% " + (((item.MontoPagado) / (resPY_IV==0?1: resPY_IV)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Inicial al " + respuestaGeneral.FechaCierreActual).FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //%CobradoActual
                ReportePendienteDetallesMesesDTO detallemes11 = new ReportePendienteDetallesMesesDTO();
                detallemes11.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Actual == 0)
                {
                    detallemes11.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes11.Monto = "% " + (((item.MontoPagado) / (item.Actual == 0 ? 1 : item.Actual)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierreActual).FirstOrDefault().ListaMontosMeses.Add(detallemes11);

                //// FECHA CORTE 2 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //Proyectado Inicial menos Ventas FECHA CORTE
                ReportePendienteDetallesMesesDTO detallemes25 = new ReportePendienteDetallesMesesDTO();
                detallemes25.Mes = item.PeriodoPorFechaVencimiento;
                detallemes25.Monto = (item.ProyectadoCierre).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas Cierre($)").FirstOrDefault().ListaMontosMeses.Add(detallemes25);

                //Proyectado Actual menos Ventas FECHA CORTE
                ReportePendienteDetallesMesesDTO detallemes26 = new ReportePendienteDetallesMesesDTO();
                detallemes26.Mes = item.PeriodoPorFechaVencimiento;
                detallemes26.Monto = (item.ActualCierre).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas Cierre($)").FirstOrDefault().ListaMontosMeses.Add(detallemes26);

                //MontoPagado FECHA CORTE
                ReportePendienteDetallesMesesDTO detallemes20 = new ReportePendienteDetallesMesesDTO();
                detallemes20.Mes = item.PeriodoPorFechaVencimiento;
                detallemes20.Monto = (item.MontoPagadoCierre).ToString();
                detalles.Where(w => w.Tipo == "Real al " + respuestaGeneral.FechaCierrePrevio + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes20);

                //Recuperacion en el Mes FECHA CORTE
                ReportePendienteDetallesMesesDTO detallemes21 = new ReportePendienteDetallesMesesDTO();
                detallemes21.Mes = item.PeriodoPorFechaVencimiento;
                detallemes21.Monto = (item.MontoRecuperadoMesCierre).ToString();
                detalles.Where(w => w.Tipo == "Recuperacion en el mes al " + respuestaGeneral.FechaCierrePrevio + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes21);

                //Pendiente FECHA CORTE
                ReportePendienteDetallesMesesDTO detallemes23 = new ReportePendienteDetallesMesesDTO();
                detallemes23.Mes = item.PeriodoPorFechaVencimiento;
                detallemes23.Monto = ((item.ActualCierre - item.IngresoVentasCierre) - (item.MontoPagadoCierre - item.IngresoVentasCierre)).ToString();
                detalles.Where(w => w.Tipo == "Pendiente al " + respuestaGeneral.FechaCierrePrevio + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes23);

                //%CobradoInicial FECHA CORTE
                ReportePendienteDetallesMesesDTO detallemes27 = new ReportePendienteDetallesMesesDTO();
                detallemes27.Mes = item.PeriodoPorFechaVencimiento;
                if (item.ProyectadoCierre == 0)
                {
                    detallemes27.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes27.Monto = "% " + (((item.MontoPagadoCierre.Value) / (item.ProyectadoCierre.Value == 0 ? 1 : item.ProyectadoCierre.Value)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Inicial al " + respuestaGeneral.FechaCierrePrevio).FirstOrDefault().ListaMontosMeses.Add(detallemes27);


                //%CobradoActual FECHA CORTE
                ReportePendienteDetallesMesesDTO detallemes24 = new ReportePendienteDetallesMesesDTO();
                detallemes24.Mes = item.PeriodoPorFechaVencimiento;
                if (item.ActualCierre == 0)
                {
                    detallemes24.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes24.Monto = "% " + (((item.MontoPagadoCierre.Value) / (item.ActualCierre.Value == 0 ? 1 : item.ActualCierre.Value)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierrePrevio).FirstOrDefault().ListaMontosMeses.Add(detallemes24);

            }

            List<ReportePendienteDetalleFinalDTO> finales = new List<ReportePendienteDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReportePendienteDetalleFinalDTO item = new ReportePendienteDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;
        }

        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 21/01/2021
        /// <summary>
        /// Obtiene el Reporte de Pendientes Por Mes Coordinador para el reporte de coordinador
        /// </summary>
        /// <returns>ReportePendienteDetalleFinalPorCoordinadorDTO</returns>
        public IList<ReportePendienteDetalleFinalPorCoordinadorDTO> GenerarReportePendientePeriodoyCoordinadorOperacionesMesCoordinador(ResultadoGeneralMesCoordinadoraDTO respuestaGeneral)
        {
            var entitiesCierre = respuestaGeneral.Cierre.OrderBy(x => x.PeriodoPorFechaVencimiento);
            var entitiesMontos = respuestaGeneral.Periodo.OrderBy(x => x.PeriodoPorFechaVencimiento);

            var unionGeneralCierre = (from montos in entitiesMontos
                                      join cierre in entitiesCierre on new { montos.PeriodoPorFechaVencimiento, montos.Coordinador } equals new { cierre.PeriodoPorFechaVencimiento, cierre.Coordinador } into gj
                                      from subcierre in gj.DefaultIfEmpty()
                                      select new ReportePendientePeriodoCierreDTO
                                      {
                                          PeriodoPorFechaVencimiento = montos.PeriodoPorFechaVencimiento,
                                          Coordinador = montos.Coordinador,
                                          Proyectado = montos.ProyectadoInicialCoordinador,
                                          Actual = montos.Actual,
                                          Diferencia = montos.Diferencia,
                                          DiferenciaCambioFecha = montos.DiferenciaCambioFecha,
                                          DiferenciaCambioMonto = montos.DiferenciaCambioMonto,
                                          Modificacion = montos.Modificacion,
                                          DiferenciaConsiderarMoraAdelantoSgteCuota = montos.DiferenciaConsiderarMoraAdelantoSgteCuota,
                                          DiferenciaModificacionNroCuotas = montos.DiferenciaModificacionNroCuotas,
                                          DiferenciaRetirosCD = montos.DiferenciaRetirosCD,
                                          DiferenciaRetirosSD = montos.DiferenciaRetirosSD,
                                          MontoPagado = montos.MontoPagado,
                                          IngresoVentas = montos.IngresoVentas,
                                          MontoRecuperadoMes = montos.MontoRecuperadoMes,
                                          PagosAdelantadoAcumulado = montos.PagosAdelantadoAcumulado,
                                          PendientePorFactura = montos.PendientePorFactura,
                                          PendienteSinFactura = montos.PendienteSinFactura,
                                          ProyectadoInicial = montos.ProyectadoInicialMes,
                                          ProyectadoCierre = subcierre == null ? 0 : subcierre.ProyectadoInicialMes,
                                          ActualCierre = subcierre == null ? 0 : subcierre.Actual,
                                          MontoPagadoCierre = subcierre == null ? 0 : subcierre.MontoPagado,
                                          IngresoVentasCierre = subcierre == null ? 0 : subcierre.IngresoVentas,
                                          MontoRecuperadoMesCierre = subcierre == null ? 0 : subcierre.MontoRecuperadoMes,
                                          PagosAdelantadoAcumuladoCierre = subcierre == null ? 0 : subcierre.ProyectadoInicialCoordinadora,
                                      });

            var entities = unionGeneralCierre.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);


            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReportePendienteDetallePorCoordinadorDTO> detalles = new List<ReportePendienteDetallePorCoordinadorDTO>();
            var listaCoordinadoras = entities.Select(x => x.Coordinador).Distinct();
            ReportePendienteDetallePorCoordinadorDTO detalle1;
            ReportePendienteDetallePorCoordinadorDTO detalle6;
            ReportePendienteDetallePorCoordinadorDTO detalle7;
            ReportePendienteDetallePorCoordinadorDTO detalle8;
            ReportePendienteDetallePorCoordinadorDTO detalle12;
            ReportePendienteDetallePorCoordinadorDTO detalle13;
            ReportePendienteDetallePorCoordinadorDTO detalle18;
            ReportePendienteDetallePorCoordinadorDTO detalle9;
            ReportePendienteDetallePorCoordinadorDTO detalle14;
            ReportePendienteDetallePorCoordinadorDTO detalle15;
            ReportePendienteDetallePorCoordinadorDTO detalle16;
            ReportePendienteDetallePorCoordinadorDTO detalle17;
            ReportePendienteDetallePorCoordinadorDTO detalle10;
            ReportePendienteDetallePorCoordinadorDTO detalle11;
            ReportePendienteDetallePorCoordinadorDTO detalle28;
            ReportePendienteDetallePorCoordinadorDTO detalle26;
            ReportePendienteDetallePorCoordinadorDTO detalle27;
            ReportePendienteDetallePorCoordinadorDTO detalle20;
            ReportePendienteDetallePorCoordinadorDTO detalle21;
            ReportePendienteDetallePorCoordinadorDTO detalle23;
            ReportePendienteDetallePorCoordinadorDTO detalle24;
            ReportePendienteDetallePorCoordinadorDTO detalle25;
            ReportePendienteDetallePorCoordinadorDTO detalle29;
            ReportePendienteDetallePorCoordinadorDTO detalle30;
            ReportePendienteDetallePorCoordinadorDTO detalle31;

            foreach (var item in listaCoordinadoras)
            {
                detalle1 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle1.Tipo = "Proyectado Inicial($)";
                detalle1.Coordinador = item;
                detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle1);


                detalle30 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle30.Tipo = "Modificacion mes actual($)";
                detalle30.Coordinador = item;
                detalle30.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle30);

                detalle31 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle31.Tipo = "Modificacion mes anterior($)";
                detalle31.Coordinador = item;
                detalle31.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle31);
                //////////////////////////////////inicio
                detalle6 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle6.Tipo = "Retiros Con Devolucion($)";
                detalle6.Coordinador = item;
                detalle6.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle6);

                detalle7 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle7.Tipo = "Retiros Sin Devolucion($)";
                detalle7.Coordinador = item;
                detalle7.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle7);
                ///////////////////////////////////fin
                detalle8 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle8.Tipo = "Proyectado Actual($)";
                detalle8.Coordinador = item;
                detalle8.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle8);

                detalle12 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle12.Tipo = "Ingreso Ventas($)";
                detalle12.Coordinador = item;
                detalle12.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle12);

                detalle13 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle13.Tipo = "Proyectado Inicial menos Ventas($)";
                detalle13.Coordinador = item;
                detalle13.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle13);

                detalle18 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle18.Tipo = "Proyectado Actual menos Ventas($)";
                detalle18.Coordinador = item;
                detalle18.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle18);

                detalle9 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle9.Tipo = "Real al " + respuestaGeneral.FechaCierreActual + " ($)";
                detalle9.Coordinador = item;
                detalle9.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle9);

                detalle14 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle14.Tipo = "Recuperacion en el mes al " + respuestaGeneral.FechaCierreActual + " ($)";
                detalle14.Coordinador = item;
                detalle14.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle14);

                detalle15 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle15.Tipo = "Pendiente al " + respuestaGeneral.FechaCierreActual + " ($)";
                detalle15.Coordinador = item;
                detalle15.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle15);

                detalle16 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle16.Tipo = "Pendiente por Factura al " + respuestaGeneral.FechaCierreActual + " ($)";
                detalle16.Coordinador = item;
                detalle16.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle16);

                detalle17 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle17.Tipo = "Pendiente sin Factura al " + respuestaGeneral.FechaCierreActual + " ($)";
                detalle17.Coordinador = item;
                detalle17.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle17);

                detalle10 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle10.Tipo = "% Sobre Proyectado Inicial al " + respuestaGeneral.FechaCierreActual;
                detalle10.Coordinador = item;
                detalle10.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle10);

                detalle11 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle11.Tipo = "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierreActual;
                detalle11.Coordinador = item;
                detalle11.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle11);

                detalle28 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle28.Tipo = "% Recuperado al " + respuestaGeneral.FechaCierreActual;
                detalle28.Coordinador = item;
                detalle28.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle28);

                //Fecha Corte 2 

                detalle26 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle26.Tipo = "Proyectado Inicial menos Ventas Cierre($)";
                detalle26.Coordinador = item;
                detalle26.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle26);

                detalle27 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle27.Tipo = "Proyectado Actual menos Ventas Cierre($)";
                detalle27.Coordinador = item;
                detalle27.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle27);

                detalle20 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle20.Tipo = "Real al " + respuestaGeneral.FechaCierrePrevio + " ($)";
                detalle20.Coordinador = item;
                detalle20.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle20);

                detalle21 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle21.Tipo = "Recuperacion en el mes al " + respuestaGeneral.FechaCierrePrevio + " ($)";
                detalle21.Coordinador = item;
                detalle21.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle21);

                detalle23 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle23.Tipo = "Pendiente al " + respuestaGeneral.FechaCierrePrevio + " ($)";
                detalle23.Coordinador = item;
                detalle23.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle23);

                detalle24 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle24.Tipo = "% Sobre Proyectado Inicial al " + respuestaGeneral.FechaCierrePrevio;
                detalle24.Coordinador = item;
                detalle24.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle24);

                detalle25 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle25.Tipo = "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierrePrevio;
                detalle25.Coordinador = item;
                detalle25.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle25);

                detalle29 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle29.Tipo = "% Recuperado al " + respuestaGeneral.FechaCierrePrevio;
                detalle29.Coordinador = item;
                detalle29.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle29);
            }

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReportePendienteDetallesMesesDTO detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                if (item.MontoRecuperadoMes == 0) detallemes1.Monto = (item.Proyectado - item.MontoRecuperadoMesCierre).ToString();
                else detallemes1.Monto = (item.Proyectado).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                ReportePendienteDetallesMesesDTO detallemes30 = new ReportePendienteDetallesMesesDTO();
                detallemes30.Mes = item.PeriodoPorFechaVencimiento;
                detallemes30.Monto = item.Modificacion.ToString();
                detalles.Where(w => w.Tipo == "Modificacion mes actual($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes30);

                ReportePendienteDetallesMesesDTO detallemes31 = new ReportePendienteDetallesMesesDTO();
                detallemes31.Mes = item.PeriodoPorFechaVencimiento;
                detallemes31.Monto = ((item.Actual + item.IngresoVentas - item.Proyectado + item.DiferenciaRetirosCD + item.DiferenciaRetirosSD) - (item.Modificacion)).ToString();
                detalles.Where(w => w.Tipo == "Modificacion mes anterior($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes31);

                /////////////////////inicio

                //Retiros Con Devolucion
                ReportePendienteDetallesMesesDTO detallemes6 = new ReportePendienteDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.DiferenciaRetirosCD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReportePendienteDetallesMesesDTO detallemes7 = new ReportePendienteDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.DiferenciaRetirosSD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                /////////////////////fin

                //Actual
                ReportePendienteDetallesMesesDTO detallemes8 = new ReportePendienteDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                if (item.MontoRecuperadoMes == 0) detallemes8.Monto = (item.Actual + item.IngresoVentas - item.MontoRecuperadoMesCierre).ToString();
                else detallemes8.Monto = (item.Actual + item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Ingreso Ventas
                ReportePendienteDetallesMesesDTO detallemes12 = new ReportePendienteDetallesMesesDTO();
                detallemes12.Mes = item.PeriodoPorFechaVencimiento;
                detallemes12.Monto = item.IngresoVentas.ToString();
                detalles.Where(w => w.Tipo == "Ingreso Ventas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //Proyectado Inicial menos Ventas
                ReportePendienteDetallesMesesDTO detallemes13 = new ReportePendienteDetallesMesesDTO();
                detallemes13.Mes = item.PeriodoPorFechaVencimiento;
                if (item.MontoRecuperadoMes == 0) detallemes13.Monto = (item.Proyectado - item.IngresoVentas - item.MontoRecuperadoMesCierre).ToString();
                else detallemes13.Monto = (item.Proyectado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes13);

                //Proyectado Actual menos Ventas
                ReportePendienteDetallesMesesDTO detallemes18 = new ReportePendienteDetallesMesesDTO();
                detallemes18.Mes = item.PeriodoPorFechaVencimiento;
                if (item.MontoRecuperadoMes == 0) detallemes18.Monto = (item.Actual - item.MontoRecuperadoMesCierre).ToString();
                else detallemes18.Monto = (item.Actual).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes18);

                //MontoPagado
                ReportePendienteDetallesMesesDTO detallemes9 = new ReportePendienteDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                if (item.MontoRecuperadoMes == 0) detallemes9.Monto = (item.MontoPagado - item.MontoRecuperadoMesCierre).ToString();
                else detallemes9.Monto = (item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Real al " + respuestaGeneral.FechaCierreActual + " ($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Recuperacion en el Mes
                ReportePendienteDetallesMesesDTO detallemes14 = new ReportePendienteDetallesMesesDTO();
                detallemes14.Mes = item.PeriodoPorFechaVencimiento;
                detallemes14.Monto = (item.MontoRecuperadoMes).ToString();
                detalles.Where(w => w.Tipo == "Recuperacion en el mes al " + respuestaGeneral.FechaCierreActual + " ($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes14);

                //Pendiente
                ReportePendienteDetallesMesesDTO detallemes15 = new ReportePendienteDetallesMesesDTO();
                detallemes15.Mes = item.PeriodoPorFechaVencimiento;
                detallemes15.Monto = ((item.Actual) - (item.MontoPagado)).ToString();
                detalles.Where(w => w.Tipo == "Pendiente al " + respuestaGeneral.FechaCierreActual + " ($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                //Pendiente por factura
                ReportePendienteDetallesMesesDTO detallemes16 = new ReportePendienteDetallesMesesDTO();
                detallemes16.Mes = item.PeriodoPorFechaVencimiento;
                detallemes16.Monto = item.PendientePorFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente por Factura al " + respuestaGeneral.FechaCierreActual + " ($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes16);

                //Pendiente sin factura
                ReportePendienteDetallesMesesDTO detallemes17 = new ReportePendienteDetallesMesesDTO();
                detallemes17.Mes = item.PeriodoPorFechaVencimiento;
                detallemes17.Monto = item.PendienteSinFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente sin Factura al " + respuestaGeneral.FechaCierreActual + " ($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes17);

                //%CobradoInicial
                ReportePendienteDetallesMesesDTO detallemes10 = new ReportePendienteDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Proyectado - item.IngresoVentas == 0)
                {
                    detallemes10.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    var resPY_IV = item.Proyectado - item.IngresoVentas;
                    detallemes10.Monto = "% " + (((item.MontoPagado) / (resPY_IV==0?1: resPY_IV)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Inicial al " + respuestaGeneral.FechaCierreActual && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //%CobradoActual
                ReportePendienteDetallesMesesDTO detallemes11 = new ReportePendienteDetallesMesesDTO();
                detallemes11.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Actual == 0)
                {
                    detallemes11.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes11.Monto = "% " + (((item.MontoPagado) / (item.Actual==0?1: item.Actual)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierreActual && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes11);

                //%Recuperado
                ReportePendienteDetallesMesesDTO detallemes28 = new ReportePendienteDetallesMesesDTO();
                detallemes28.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Actual == 0)
                {
                    detallemes28.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes28.Monto = "% " + (((item.MontoRecuperadoMes.Value) / (item.Actual == 0 ? 1 : item.Actual)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Recuperado al " + respuestaGeneral.FechaCierreActual && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes28);

                //FECHA CORTE 2 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //Proyectado Inicial menos Ventas FECHA CORTE 
                ReportePendienteDetallesMesesDTO detallemes26 = new ReportePendienteDetallesMesesDTO();
                detallemes26.Mes = item.PeriodoPorFechaVencimiento;
                if (item.MontoRecuperadoMes == 0) detallemes26.Monto = (item.PagosAdelantadoAcumuladoCierre - item.MontoRecuperadoMesCierre).ToString();
                else detallemes26.Monto = (item.PagosAdelantadoAcumuladoCierre).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas Cierre($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes26);

                //Proyectado Actual menos Ventas FECHA CORTE 
                ReportePendienteDetallesMesesDTO detallemes27 = new ReportePendienteDetallesMesesDTO();
                detallemes27.Mes = item.PeriodoPorFechaVencimiento;
                if (item.MontoRecuperadoMes == 0) detallemes27.Monto = (item.ActualCierre - item.MontoRecuperadoMesCierre).ToString();
                else detallemes27.Monto = (item.ActualCierre).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas Cierre($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes27);

                //MontoPagado FECHA CORTE 
                ReportePendienteDetallesMesesDTO detallemes20 = new ReportePendienteDetallesMesesDTO();
                detallemes20.Mes = item.PeriodoPorFechaVencimiento;
                if (item.MontoRecuperadoMes == 0) detallemes20.Monto = (item.MontoPagadoCierre - item.MontoRecuperadoMesCierre).ToString();
                else detallemes20.Monto = (item.MontoPagadoCierre).ToString();
                detalles.Where(w => w.Tipo == "Real al " + respuestaGeneral.FechaCierrePrevio + " ($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes20);

                //Recuperacion en el Mes FECHA CORTE 
                ReportePendienteDetallesMesesDTO detallemes21 = new ReportePendienteDetallesMesesDTO();
                detallemes21.Mes = item.PeriodoPorFechaVencimiento;
                if (item.MontoRecuperadoMes == 0) detallemes21.Monto = (0).ToString();
                else detallemes21.Monto = (item.MontoRecuperadoMesCierre).ToString();
                detalles.Where(w => w.Tipo == "Recuperacion en el mes al " + respuestaGeneral.FechaCierrePrevio + " ($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes21);

                //Pendiente FECHA CORTE 
                ReportePendienteDetallesMesesDTO detallemes23 = new ReportePendienteDetallesMesesDTO();
                detallemes23.Mes = item.PeriodoPorFechaVencimiento;
                detallemes23.Monto = ((item.ActualCierre - item.IngresoVentasCierre) - (item.MontoPagadoCierre - item.IngresoVentasCierre)).ToString();
                detalles.Where(w => w.Tipo == "Pendiente al " + respuestaGeneral.FechaCierrePrevio + " ($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes23);

                //%CobradoInicial FECHA CORTE 
                ReportePendienteDetallesMesesDTO detallemes24 = new ReportePendienteDetallesMesesDTO();
                detallemes24.Mes = item.PeriodoPorFechaVencimiento;
                if (item.PagosAdelantadoAcumuladoCierre == 0)
                {
                    detallemes24.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes24.Monto = "% " + (((item.MontoPagadoCierre.Value) / (item.PagosAdelantadoAcumuladoCierre.Value==0?1: item.PagosAdelantadoAcumuladoCierre.Value)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Inicial al " + respuestaGeneral.FechaCierrePrevio && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes24);

                //%CobradoActual FECHA CORTE 
                ReportePendienteDetallesMesesDTO detallemes25 = new ReportePendienteDetallesMesesDTO();
                detallemes25.Mes = item.PeriodoPorFechaVencimiento;
                if (item.ActualCierre == 0)
                {
                    detallemes25.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes25.Monto = "% " + (((item.MontoPagadoCierre.Value) / (item.ActualCierre.Value==0?1: item.ActualCierre.Value)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierrePrevio && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes25);

                //%Recuperado FECHA CORTE 
                ReportePendienteDetallesMesesDTO detallemes29 = new ReportePendienteDetallesMesesDTO();
                detallemes29.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Actual == 0)
                {
                    detallemes29.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes29.Monto = "% " + (((item.MontoRecuperadoMesCierre.Value) / (item.ActualCierre.Value == 0 ? 1 : item.ActualCierre.Value)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Recuperado al " + respuestaGeneral.FechaCierrePrevio && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes29);


            }
            List<ReportePendienteDetalleFinalPorCoordinadorDTO> finales = new List<ReportePendienteDetalleFinalPorCoordinadorDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReportePendienteDetalleFinalPorCoordinadorDTO item = new ReportePendienteDetalleFinalPorCoordinadorDTO();
                    item.TipoMonto = det.Tipo;
                    item.Coordinador = det.Coordinador;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }


    }

}
