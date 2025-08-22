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
    /// Service: ReportePendienteV2Service
    /// Autor Modificacion: Adriana Chipana.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePendienteV2
    /// </summary>
    public class ReportePendienteV2Service : IReportePendienteV2Service
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReportePendienteV2Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReportePendienteV2, ReportePendienteV2>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReportePendienteV2RecibidoDTO, ReportePendienteV2>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente)
        {
            var _reporteRepositorio = _unitOfWork.ReportePendienteV2Repository;
            var entities = _reporteRepositorio.ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo(filtroPendiente).ToList();

            return entities;
        }

        public async Task<bool> ObtenerReportePendienteCierrePorPeriodo(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente)
        {
            try
            {
                var _reporteRepositorio = _unitOfWork.ReportePendienteV2Repository;
                _reporteRepositorio.ObtenerReportePendienteCierrePorPeriodo(filtroPendiente);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo_Matriculados(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente)
        {
            var _reporteRepositorio = _unitOfWork.ReportePendienteV2Repository;
            var entities = _reporteRepositorio.ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo_Matriculados(filtroPendiente).ToList();

            return entities;
        }


        public ReportePendienteGeneralPeriodoDTO GenerarReportePendientePorPeriodoOperacionesGeneralPrueba(EnvioDarosPruebaDTO datos)
        {
            var _reporteRepositorio = _unitOfWork.ReportePendienteV2Repository;
            var entities =datos.PeriodoCoordinador;
            var matriculados = datos.Matriculados;
            var cierre = datos.Periodo;
            string fechaCierre = datos.FechaCorte.Day.ToString() + "/" + datos.FechaCorte.Month.ToString();
            string fechaCierrePrevio = datos.FechaCortePrevio.Day.ToString() + "/" + datos.FechaCortePrevio.Month.ToString();

            ReportePendienteGeneralPeriodoDTO general = new ReportePendienteGeneralPeriodoDTO();
            general.Periodo = entities;
            general.Matriculados = matriculados;
            general.Cierre = cierre;
            general.FechaCierreActual = fechaCierre;
            general.FechaCierrePrevio = fechaCierrePrevio;
            return general;

        }

        public ReportePendienteGeneralPeriodoDTO GenerarReportePendientePorPeriodoOperacionesGeneral(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente)
        {
            var _reporteRepositorio = _unitOfWork.ReportePendienteV2Repository;
            var entities = _reporteRepositorio.ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo(filtroPendiente).ToList();
            var matriculados = _reporteRepositorio.ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo_Matriculados(filtroPendiente).ToList();
            //var cambios = _reporteRepositorio.ObtenerReportePendienteCambiosPorCoordinadorPorPeriodo(filtroPendiente).ToList();
            // var modificaciones = _reporteRepositorio.ObtenerReportePendienteDiferenciasPorPeriodo(filtroPendiente).ToList();
            //var cierre = _reporteRepositorio.ObtenerReportePendienteCierrePorPeriodoPrueba().ToList();
            string fechaCierre = filtroPendiente.FechaCorte.Day.ToString() + "/" + filtroPendiente.FechaCorte.Month.ToString();
            string fechaCierrePrevio = filtroPendiente.FechaCortePrevio.Day.ToString() + "/" + filtroPendiente.FechaCortePrevio.Month.ToString();

            ReportePendienteGeneralPeriodoDTO general = new ReportePendienteGeneralPeriodoDTO();
            general.Periodo = entities;
            general.Matriculados = matriculados;
            //general.Cambios = cambios;
            //general.Diferencias = modificaciones;
            //general.Cierre = cierre;
            general.FechaCierreActual = fechaCierre;
            general.FechaCierrePrevio = fechaCierrePrevio;
            return general;

        }

        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendienteIngresoVentasPorPeriodoOperaciones(ReportePendienteGeneralPeriodoDTO respuestaGeneral)
        {
            
            var agrupadoGeneral = (from r in respuestaGeneral.Matriculados
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReportePendientePeriodoTotalizadoDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       IngresoVentas = grupo.Select(x => x.MatriculadosFechaPago).Sum(),
                                       PagoEnFechaVenc = grupo.Select(x => x.MatriculadosFechaVencimiento).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);

            /*CONVIERTO EL FORMATO A EL FORMATO NECESITADO*/

            List<ReportePendienteDetalleDTO> detalles = new List<ReportePendienteDetalleDTO>();

            ReportePendienteDetalleDTO detalle1 = new ReportePendienteDetalleDTO();
            detalle1.Tipo = "Ingreso Matriculas según Fecha de Cronograma($)";
            detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReportePendienteDetalleDTO detalle2 = new ReportePendienteDetalleDTO();
            detalle2.Tipo = "Ingreso según Fecha de Pago($)";
            detalle2.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle2);
            ReportePendienteDetallesMesesDTO detallemes1;
            ReportePendienteDetallesMesesDTO detallemes2;
            foreach (var items in entities)
            {
                //Proyectado Inicial
                detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = items.PeriodoPorFechaVencimiento;
                detallemes1.Monto = items.PagoEnFechaVenc.ToString();
                detalles.Where(w => w.Tipo == "Ingreso Matriculas según Fecha de Cronograma($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                detallemes2 = new ReportePendienteDetallesMesesDTO();
                detallemes2.Mes = items.PeriodoPorFechaVencimiento;
                detallemes2.Monto = items.IngresoVentas.ToString();
                detalles.Where(w => w.Tipo == "Ingreso según Fecha de Pago($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

            }
            List<ReportePendienteDetalleFinalDTO> finales = new List<ReportePendienteDetalleFinalDTO>();
            ReportePendienteDetalleFinalDTO item;
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    item = new ReportePendienteDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            return finales;

        }


        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendientePorCoordinadoraOperacionesPorPeriodo(ReportePendienteGeneralPeriodoDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.Periodo
                                   group r by new { r.Coordinador } into grupo
                                   select new ReportePendientePeriodoPorCoordinadorDTO
                                   {
                                       Coordinador = grupo.Key.Coordinador,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
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
                                       PendientePorFactura = grupo.Select(x => x.PendientePorFactura).Sum(),
                                       PendienteSinFactura = grupo.Select(x => x.PendienteSinFactura).Sum(),
                                       MontoVencido = grupo.Select(x => x.MontoVencido).Sum(),
                                       MontoPorVencer = grupo.Select(x => x.MontoPorVencer).Sum(),
                                       PagoPrevio = grupo.Select(x => x.PagoPrevio).Sum(),
                                       PagoDentroMes = grupo.Select(x => x.PagoDentroMes).Sum(),
                                       ProyectadoInicial = grupo.Select(x => x.ProyectadoInicial).Sum(),
                                       Modificacion = grupo.Select(x => x.Modificacion).Sum(),
                                       RetiradosInicial = grupo.Select(x => x.RetiradosInicial).Sum(),
                                   });

            var agrupadoGeneralCierre = (from r in respuestaGeneral.Cierre
                                         group r by new { r.Coordinador } into grupo
                                         select new ReportePendientePeriodoPorCoordinadorDTO
                                         {
                                             Coordinador = grupo.Key.Coordinador,

                                             Proyectado = grupo.Select(x => x.Proyectado).Sum(),
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
                                             PendientePorFactura = grupo.Select(x => x.PendientePorFactura).Sum(),
                                             PendienteSinFactura = grupo.Select(x => x.PendienteSinFactura).Sum(),
                                             MontoVencido = grupo.Select(x => x.MontoVencido).Sum(),
                                             MontoPorVencer = grupo.Select(x => x.MontoPorVencer).Sum(),
                                             PagoPrevio = grupo.Select(x => x.PagoPrevio).Sum(),
                                             PagoDentroMes = grupo.Select(x => x.PagoDentroMes).Sum(),
                                         });

            var entitiesCierre = agrupadoGeneralCierre.ToList().OrderBy(x => x.Coordinador);
            var entitiesMontos = agrupadoGeneral.ToList().OrderBy(x => x.Coordinador);

            var unionGeneralCierre = (from montos in agrupadoGeneral
                                      join cierre in agrupadoGeneralCierre on montos.Coordinador equals cierre.Coordinador
                                       into gj
                                      from subcierre in gj.DefaultIfEmpty()
                                      select new ReportePendientePeriodoCierreDTO
                                      {
                                          Coordinador = montos.Coordinador,
                                          Proyectado = montos.Proyectado,
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
                                          PendientePorFactura = montos.PendientePorFactura,
                                          PendienteSinFactura = montos.PendienteSinFactura,
                                          MontoVencido = montos.MontoVencido,
                                          MontoPorVencer = montos.MontoPorVencer,
                                          PagoPrevio = montos.PagoPrevio,
                                          PagoDentroMes = montos.PagoDentroMes,
                                          ProyectadoInicial = montos.ProyectadoInicial,
                                          ProyectadoCierre = subcierre == null ? 0 : subcierre.Proyectado,
                                          ActualCierre = subcierre == null ? 0 : subcierre.Actual,
                                          MontoPagadoCierre = subcierre == null ? 0 : subcierre.MontoPagado,
                                          IngresoVentasCierre = subcierre == null ? 0 : subcierre.IngresoVentas,
                                          MontoVencidoCierre = subcierre == null ? 0 : subcierre.MontoVencido,
                                          PagoPrevioCierre = subcierre == null ? 0 : subcierre.PagoPrevio,
                                          PagoDentroMesCierre = subcierre == null ? 0 : subcierre.PagoDentroMes,
                                          Modificacion = montos.Modificacion,
                                          RetiradosInicial = montos.RetiradosInicial,
                                      });

            var entities = unionGeneralCierre.ToList().OrderBy(x => x.Coordinador);


            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReportePendienteDetalleDTO> detalles = new List<ReportePendienteDetalleDTO>();

            ReportePendienteDetalleDTO detalle1 = new ReportePendienteDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReportePendienteDetalleDTO detalle2 = new ReportePendienteDetalleDTO();
            detalle2.Tipo = "Retirados";
            detalle2.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReportePendienteDetalleDTO detalle5 = new ReportePendienteDetalleDTO();
            detalle5.Tipo = "Modificacion($)";
            detalle5.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReportePendienteDetalleDTO detalle44 = new ReportePendienteDetalleDTO();
            detalle44.Tipo = "Modificaciones mes actual($)";
            detalle44.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle44);

            ReportePendienteDetalleDTO detalle45 = new ReportePendienteDetalleDTO();
            detalle45.Tipo = "Modificaciones mes anterior($)";
            detalle45.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle45);

            ReportePendienteDetalleDTO detalle22 = new ReportePendienteDetalleDTO();
            detalle22.Tipo = "Proyectado Actual($)";
            detalle22.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle22);

            ReportePendienteDetalleDTO detalle40 = new ReportePendienteDetalleDTO();
            detalle40.Tipo = "Ingreso Ventas($)";
            detalle40.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle40);

            ReportePendienteDetalleDTO detalle21 = new ReportePendienteDetalleDTO();
            detalle21.Tipo = "Proyectado Inicial menos Ventas($)";
            detalle21.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle21);

            ReportePendienteDetalleDTO detalle31 = new ReportePendienteDetalleDTO();
            detalle31.Tipo = "Proyectado Actual menos Ventas($)";
            detalle31.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle31);

            ReportePendienteDetalleDTO detalle19 = new ReportePendienteDetalleDTO();
            detalle19.Tipo = "Proyectado por Cobrar($)";
            detalle19.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle19);

            ReportePendienteDetalleDTO detalle8 = new ReportePendienteDetalleDTO();
            detalle8.Tipo = "Vencido($)";
            detalle8.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReportePendienteDetalleDTO detalle12 = new ReportePendienteDetalleDTO();
            detalle12.Tipo = "Por Vencer($)";
            detalle12.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle12);

            ReportePendienteDetalleDTO detalle9 = new ReportePendienteDetalleDTO();
            detalle9.Tipo = "Ingreso Real al " + respuestaGeneral.FechaCierreActual + " ($)";
            detalle9.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReportePendienteDetalleDTO detalle14 = new ReportePendienteDetalleDTO();
            detalle14.Tipo = "Ingreso Real Previo al " + respuestaGeneral.FechaCierreActual + " ($)";
            detalle14.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle14);

            ReportePendienteDetalleDTO detalle18 = new ReportePendienteDetalleDTO();
            detalle18.Tipo = "Ingreso Real Propio al " + respuestaGeneral.FechaCierreActual + " ($)";
            detalle18.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle18);

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

            ReportePendienteDetalleDTO detalle20 = new ReportePendienteDetalleDTO();
            detalle20.Tipo = "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierreActual;
            detalle20.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle20);

            ReportePendienteDetalleDTO detalle30 = new ReportePendienteDetalleDTO();
            detalle30.Tipo = "% Sobre Proyectado por Cobrar al " + respuestaGeneral.FechaCierreActual;
            detalle30.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle30);

            ReportePendienteDetalleDTO detalle11 = new ReportePendienteDetalleDTO();
            detalle11.Tipo = "% Sobre el Vencido al " + respuestaGeneral.FechaCierreActual;
            detalle11.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle11);


            //Corte de Fecha Corte Previo

            ReportePendienteDetalleDTO detalle32 = new ReportePendienteDetalleDTO();
            detalle32.Tipo = "Proyectado Inicial menos Ventas Cierre($)";
            detalle32.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle32);

            ReportePendienteDetalleDTO detalle33 = new ReportePendienteDetalleDTO();
            detalle33.Tipo = "Proyectado Actual menos Ventas Cierre($)";
            detalle33.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle33);

            ReportePendienteDetalleDTO detalle35 = new ReportePendienteDetalleDTO();
            detalle35.Tipo = "Proyectado por Cobrar Cierre($)";
            detalle35.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle35);

            ReportePendienteDetalleDTO detalle23 = new ReportePendienteDetalleDTO();
            detalle23.Tipo = "Ingreso Real al " + respuestaGeneral.FechaCierrePrevio + " ($)";
            detalle23.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle23);

            ReportePendienteDetalleDTO detalle24 = new ReportePendienteDetalleDTO();
            detalle24.Tipo = "Ingreso Real Previo al " + respuestaGeneral.FechaCierrePrevio + " ($)";
            detalle24.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle24);

            ReportePendienteDetalleDTO detalle25 = new ReportePendienteDetalleDTO();
            detalle25.Tipo = "Ingreso Real Propio al " + respuestaGeneral.FechaCierrePrevio + " ($)";
            detalle25.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle25);

            ReportePendienteDetalleDTO detalle26 = new ReportePendienteDetalleDTO();
            detalle26.Tipo = "% Sobre Proyectado Inicial al " + respuestaGeneral.FechaCierrePrevio;
            detalle26.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle26);

            ReportePendienteDetalleDTO detalle27 = new ReportePendienteDetalleDTO();
            detalle27.Tipo = "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierrePrevio;
            detalle27.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle27);

            ReportePendienteDetalleDTO detalle34 = new ReportePendienteDetalleDTO();
            detalle34.Tipo = "% Sobre Proyectado por Cobrar al " + respuestaGeneral.FechaCierrePrevio;
            detalle34.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle34);

            ReportePendienteDetalleDTO detalle28 = new ReportePendienteDetalleDTO();
            detalle28.Tipo = "% Sobre el Vencido al " + respuestaGeneral.FechaCierrePrevio;
            detalle28.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle28);

            ReportePendienteDetallesMesesDTO detallemes1;
            ReportePendienteDetallesMesesDTO detallemes2;
            ReportePendienteDetallesMesesDTO detallemes5;
            ReportePendienteDetallesMesesDTO detallemes22;
            ReportePendienteDetallesMesesDTO detallemes21;
            ReportePendienteDetallesMesesDTO detallemes31;
            ReportePendienteDetallesMesesDTO detallemes19;
            ReportePendienteDetallesMesesDTO detallemes8;
            ReportePendienteDetallesMesesDTO detallemes12;
            ReportePendienteDetallesMesesDTO detallemes9;
            ReportePendienteDetallesMesesDTO detallemes14;
            ReportePendienteDetallesMesesDTO detallemes18;
            ReportePendienteDetallesMesesDTO detallemes15;
            ReportePendienteDetallesMesesDTO detallemes16;
            ReportePendienteDetallesMesesDTO detallemes17;
            ReportePendienteDetallesMesesDTO detallemes10;
            ReportePendienteDetallesMesesDTO detallemes20;
            ReportePendienteDetallesMesesDTO detallemes11;
            ReportePendienteDetallesMesesDTO detallemes30;
            ReportePendienteDetallesMesesDTO detallemes32;
            ReportePendienteDetallesMesesDTO detallemes24;
            ReportePendienteDetallesMesesDTO detallemes25;
            ReportePendienteDetallesMesesDTO detallemes26;
            ReportePendienteDetallesMesesDTO detallemes27;
            ReportePendienteDetallesMesesDTO detallemes28;
            ReportePendienteDetallesMesesDTO detallemes29;
            ReportePendienteDetallesMesesDTO detallemes33;
            ReportePendienteDetallesMesesDTO detallemes34;
            ReportePendienteDetallesMesesDTO detallemes35;
            ReportePendienteDetallesMesesDTO detallemes40;
            ReportePendienteDetallesMesesDTO detallemes44;
            ReportePendienteDetallesMesesDTO detallemes45;

            foreach (var item in entities)
            {
                //Proyectado Inicial
                detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = item.Coordinador;
                detallemes1.Monto = item.ProyectadoInicial.ToString("0");
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //Retirados
                detallemes2 = new ReportePendienteDetallesMesesDTO();
                detallemes2.Mes = item.Coordinador;
                detallemes2.Monto = item.RetiradosInicial.ToString("0");
                detalles.Where(w => w.Tipo == "Retirados").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //Proyectado Actual
                detallemes22 = new ReportePendienteDetallesMesesDTO();
                detallemes22.Mes = item.Coordinador;
                detallemes22.Monto = (item.Actual + item.IngresoVentas).ToString("0");
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes22);
                //Modificacion
                detallemes5 = new ReportePendienteDetallesMesesDTO();
                detallemes5.Mes = item.Coordinador;
                detallemes5.Monto = (item.Actual - item.ProyectadoInicial).ToString("0");
                detalles.Where(w => w.Tipo == "Modificacion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //DiferenciaModificacionNroCuotas
                detallemes45 = new ReportePendienteDetallesMesesDTO();
                detallemes45.Mes = item.Coordinador;
                detallemes45.Monto = (item.Actual - item.ProyectadoInicial - item.Modificacion).ToString("0");
                detalles.Where(w => w.Tipo == "Modificaciones mes anterior($)").FirstOrDefault().ListaMontosMeses.Add(detallemes45);

                //DiferenciaModificacionNroCuotas
                detallemes44 = new ReportePendienteDetallesMesesDTO();
                detallemes44.Mes = item.Coordinador;
                detallemes44.Monto = (item.Modificacion).ToString("0");
                detalles.Where(w => w.Tipo == "Modificaciones mes actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes44);
                //Ingreso ventas
                detallemes40 = new ReportePendienteDetallesMesesDTO();
                detallemes40.Mes = item.Coordinador;
                detallemes40.Monto = (item.IngresoVentas).ToString("0");
                detalles.Where(w => w.Tipo == "Ingreso Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes40);
                /////////////////////fin
                //Proyectado con Cambios Inicial
                detallemes21 = new ReportePendienteDetallesMesesDTO();
                detallemes21.Mes = item.Coordinador;
                detallemes21.Monto = (item.ProyectadoInicial - item.IngresoVentas).ToString("0");
                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes21);

                //Proyectado con Cambios Actual
                detallemes31 = new ReportePendienteDetallesMesesDTO();
                detallemes31.Mes = item.Coordinador;
                detallemes31.Monto = (item.Actual).ToString("0");
                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes31);

                //Vencido
                detallemes8 = new ReportePendienteDetallesMesesDTO();
                detallemes8.Mes = item.Coordinador;
                detallemes8.Monto = item.MontoVencido.ToString("0");
                detalles.Where(w => w.Tipo == "Vencido($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Por Vencer
                detallemes12 = new ReportePendienteDetallesMesesDTO();
                detallemes12.Mes = item.Coordinador;
                detallemes12.Monto = item.MontoPorVencer.ToString("0");
                detalles.Where(w => w.Tipo == "Por Vencer($)").FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //Proyectado por Cobrar
                detallemes19 = new ReportePendienteDetallesMesesDTO();
                detallemes19.Mes = item.Coordinador;
                detallemes19.Monto = (item.MontoVencido + item.MontoPorVencer).ToString("0");
                detalles.Where(w => w.Tipo == "Proyectado por Cobrar($)").FirstOrDefault().ListaMontosMeses.Add(detallemes19);

                //Real Ingreso Previo
                detallemes14 = new ReportePendienteDetallesMesesDTO();
                detallemes14.Mes = item.Coordinador;
                detallemes14.Monto = item.PagoPrevio.ToString("0");
                detalles.Where(w => w.Tipo == "Ingreso Real Previo al " + respuestaGeneral.FechaCierreActual + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes14);

                //Real Ingreso
                detallemes18 = new ReportePendienteDetallesMesesDTO();
                detallemes18.Mes = item.Coordinador;
                detallemes18.Monto = item.PagoDentroMes.ToString("0");
                detalles.Where(w => w.Tipo == "Ingreso Real Propio al " + respuestaGeneral.FechaCierreActual + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes18);

                //INGRESO REAL
                detallemes9 = new ReportePendienteDetallesMesesDTO();
                detallemes9.Mes = item.Coordinador;
                detallemes9.Monto = (item.PagoPrevio + item.PagoDentroMes).ToString("0");
                detalles.Where(w => w.Tipo == "Ingreso Real al " + respuestaGeneral.FechaCierreActual + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente por factura
                detallemes16 = new ReportePendienteDetallesMesesDTO();
                detallemes16.Mes = item.Coordinador;
                detallemes16.Monto = item.PendientePorFactura.ToString("0");
                detalles.Where(w => w.Tipo == "Pendiente por Factura al " + respuestaGeneral.FechaCierreActual + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes16);

                //Pendiente sin factura
                detallemes17 = new ReportePendienteDetallesMesesDTO();
                detallemes17.Mes = item.Coordinador;
                detallemes17.Monto = item.PendienteSinFactura.ToString("0");
                detalles.Where(w => w.Tipo == "Pendiente sin Factura al " + respuestaGeneral.FechaCierreActual + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes17);

                //Pendiente
                detallemes15 = new ReportePendienteDetallesMesesDTO();
                detallemes15.Mes = item.Coordinador;
                detallemes15.Monto = (item.PendientePorFactura + item.PendienteSinFactura).ToString("0");
                detalles.Where(w => w.Tipo == "Pendiente al " + respuestaGeneral.FechaCierreActual + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                //%CobradoInicial
                detallemes10 = new ReportePendienteDetallesMesesDTO();
                detallemes10.Mes = item.Coordinador;
                if (item.ProyectadoInicial - item.IngresoVentas == 0)
                {
                    detallemes10.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes10.Monto = "% " + (((item.PagoPrevio + item.PagoDentroMes) / (item.ProyectadoInicial - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Inicial al " + respuestaGeneral.FechaCierreActual).FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //%CobradoActual
                detallemes20 = new ReportePendienteDetallesMesesDTO();
                detallemes20.Mes = item.Coordinador;
                if (item.Actual - item.IngresoVentas == 0)
                {
                    detallemes20.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes20.Monto = "% " + (((item.PagoPrevio + item.PagoDentroMes) / (item.Actual - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierreActual).FirstOrDefault().ListaMontosMeses.Add(detallemes20);

                //%CobradoVencido
                detallemes11 = new ReportePendienteDetallesMesesDTO();
                detallemes11.Mes = item.Coordinador;
                if (item.MontoVencido == 0.00m)
                {
                    detallemes11.Monto = "% " + (0.00m * 100).ToString("0.00");
                }
                else
                {
                    detallemes11.Monto = "% " + (((item.PagoDentroMes) / item.MontoVencido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre el Vencido al " + respuestaGeneral.FechaCierreActual).FirstOrDefault().ListaMontosMeses.Add(detallemes11);

                //%CobradoPorCobrar
                detallemes30 = new ReportePendienteDetallesMesesDTO();
                detallemes30.Mes = item.Coordinador;
                if ((item.MontoVencido + item.MontoPorVencer) == 0)
                {
                    detallemes30.Monto = "% " + (0.00m * 100).ToString("0.00");
                }
                else
                {
                    detallemes30.Monto = "% " + ((item.PagoDentroMes) / (item.MontoVencido + item.MontoPorVencer) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado por Cobrar al " + respuestaGeneral.FechaCierreActual).FirstOrDefault().ListaMontosMeses.Add(detallemes30);

                // FECHA CORTE PREVIO ///////////////////////////////////////////////////////////////////////////////////////////////////

                //Proyectado con Cambios Inicial Fecha Corte Previo
                detallemes32 = new ReportePendienteDetallesMesesDTO();
                detallemes32.Mes = item.Coordinador;
                if (item.ProyectadoCierre.HasValue && item.IngresoVentasCierre.HasValue)
                {
                    detallemes32.Monto = (item.ProyectadoCierre.Value - item.IngresoVentasCierre.Value).ToString("0");
                }
                else
                {
                    detallemes32.Monto = "0";
                }

                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas Cierre($)").FirstOrDefault().ListaMontosMeses.Add(detallemes32);

                //Proyectado con Cambios Actual Fecha Corte Previo
                detallemes33 = new ReportePendienteDetallesMesesDTO();
                detallemes33.Mes = item.Coordinador;
                if (item.ActualCierre.HasValue && item.IngresoVentasCierre.HasValue)
                {
                    detallemes33.Monto = (item.ActualCierre.Value - item.IngresoVentasCierre.Value).ToString("0");
                }
                else
                {
                    detallemes33.Monto = "0";
                }


                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas Cierre($)").FirstOrDefault().ListaMontosMeses.Add(detallemes33);

                //Proyectado por Cobrar Fecha Corte Previo
                detallemes35 = new ReportePendienteDetallesMesesDTO();
                detallemes35.Mes = item.Coordinador;
                if (item.ActualCierre.HasValue && item.PagoPrevioCierre.HasValue)
                {
                    detallemes35.Monto = (item.ActualCierre.Value - item.PagoPrevioCierre.Value).ToString("0");
                }
                else
                {
                    detallemes35.Monto = "0";
                }
                detalles.Where(w => w.Tipo == "Proyectado por Cobrar Cierre($)").FirstOrDefault().ListaMontosMeses.Add(detallemes35);

                //MontoPagado Fecha Corte Previo
                detallemes29 = new ReportePendienteDetallesMesesDTO();
                detallemes29.Mes = item.Coordinador;
                if (item.PagoPrevioCierre.HasValue && item.PagoDentroMesCierre.HasValue)
                {
                    detallemes29.Monto = (item.PagoPrevioCierre.Value - item.PagoDentroMesCierre.Value).ToString("0");
                }
                else
                {
                    detallemes29.Monto = "0";
                }
                detalles.Where(w => w.Tipo == "Ingreso Real al " + respuestaGeneral.FechaCierrePrevio + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes29);

                //Real Ingreso Previo Fecha Corte Previo
                detallemes24 = new ReportePendienteDetallesMesesDTO();
                detallemes24.Mes = item.Coordinador;
                if (item.PagoPrevioCierre.HasValue)
                {
                    detallemes24.Monto = item.PagoPrevioCierre.Value.ToString("0");
                }
                else
                {
                    detallemes24.Monto = "0";
                }
                detalles.Where(w => w.Tipo == "Ingreso Real Previo al " + respuestaGeneral.FechaCierrePrevio + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes24);


                //Real Ingreso Fecha Corte Previo
                detallemes25 = new ReportePendienteDetallesMesesDTO();
                detallemes25.Mes = item.Coordinador;
                if (item.PagoDentroMesCierre.HasValue)
                {
                    detallemes25.Monto = item.PagoDentroMesCierre.Value.ToString("0");
                }
                else
                {
                    detallemes25.Monto = "0";
                }
                detalles.Where(w => w.Tipo == "Ingreso Real Propio al " + respuestaGeneral.FechaCierrePrevio + " ($)").FirstOrDefault().ListaMontosMeses.Add(detallemes25);

                //%CobradoInicial Fecha Corte Previo
                detallemes26 = new ReportePendienteDetallesMesesDTO();
                detallemes26.Mes = item.Coordinador;
                if (item.ProyectadoCierre - item.IngresoVentasCierre == 0)
                {
                    detallemes26.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes26.Monto = "% " + (((item.MontoPagadoCierre.Value - item.IngresoVentasCierre.Value) / (item.ProyectadoCierre.Value - item.IngresoVentasCierre.Value)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Inicial al " + respuestaGeneral.FechaCierrePrevio).FirstOrDefault().ListaMontosMeses.Add(detallemes26);

                //%CobradoActual Fecha Corte Previo
                detallemes27 = new ReportePendienteDetallesMesesDTO();
                detallemes27.Mes = item.Coordinador;
                if (item.ActualCierre - item.IngresoVentasCierre == 0)
                {
                    detallemes27.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes27.Monto = "% " + (((item.MontoPagadoCierre.Value - item.IngresoVentasCierre.Value) / (item.ActualCierre.Value - item.IngresoVentasCierre.Value)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado Actual al " + respuestaGeneral.FechaCierrePrevio).FirstOrDefault().ListaMontosMeses.Add(detallemes27);

                //%CobradoVencido Fecha Corte Previo
                detallemes28 = new ReportePendienteDetallesMesesDTO();
                detallemes28.Mes = item.Coordinador;
                if (item.MontoVencidoCierre == 0.00m)
                {
                    detallemes28.Monto = "% " + (0.00m * 100).ToString("0.00");
                }
                else
                {
                    detallemes28.Monto = "% " + (((item.PagoDentroMesCierre.Value) / item.MontoVencidoCierre.Value) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre el Vencido al " + respuestaGeneral.FechaCierrePrevio).FirstOrDefault().ListaMontosMeses.Add(detallemes28);

                //%CobradoPorCobrar Fecha Corte Previo
                detallemes34 = new ReportePendienteDetallesMesesDTO();
                detallemes34.Mes = item.Coordinador;
                if ((item.ActualCierre - item.IngresoVentasCierre) - item.PagoPrevioCierre == 0)
                {
                    detallemes34.Monto = "% " + (0.00m * 100).ToString("0.00");
                }
                else
                {
                    detallemes34.Monto = "% " + (((item.PagoDentroMesCierre.Value) / ((item.ActualCierre.Value - item.IngresoVentasCierre.Value) - item.PagoPrevioCierre.Value)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Sobre Proyectado por Cobrar al " + respuestaGeneral.FechaCierrePrevio).FirstOrDefault().ListaMontosMeses.Add(detallemes34);
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


        public ReportePendienteCompuestoDTO GenerarReportePeriodo(ReportePendientePeriodoFiltroDTO FiltroPendiente)
        {
            try
            {
                var _repModalidadCurso = _unitOfWork.ModalidadCursoRepository;

                if (FiltroPendiente.Modalidad.Count() == 0)
                {
                    FiltroPendiente.Modalidad = _repModalidadCurso.ObtenerCombo().Select(w => w.Id).ToList();
                }

                var respuestaGeneral = FiltroPendiente.datosPrueba;

                var listRpta2 = GenerarReportePendienteIngresoVentasPorPeriodoOperaciones(respuestaGeneral);
                var agrupado2 = (from p in listRpta2
                                 group p by p.Periodo into grupo
                                 select new ReportePendienteDTO { g = grupo.Key, l = grupo.ToList() }).ToList();

                var listRpta3 = GenerarReportePendientePorCoordinadoraOperacionesPorPeriodo(respuestaGeneral);
                var agrupado3 = (from p in listRpta3
                                 group p by p.Periodo into grupo
                                 select new ReportePendienteDTO { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReportePendienteCompuestoDTO reporte = new ReportePendienteCompuestoDTO();
                reporte.ReportePendienteIngresoVentasPorPeriodo = agrupado2.ToList();
                reporte.ReportePendientePorCoordinador = agrupado3.ToList();

                return reporte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendienteCierrePorPeriodoPrueba(StringDTO valor)
        {
            var _reporteRepositorio = _unitOfWork.ReportePendienteV2Repository;
            var entities = _reporteRepositorio.ObtenerReportePendienteCierrePorPeriodoPrueba(valor);

            return entities;
        }

    }
}
