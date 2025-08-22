using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReporteResumenMontoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteResumenMonto
    /// </summary>
    public class ReporteResumenMontoService : IReporteResumenMontoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReporteResumenMontoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<TReporteResumenMonto, ReporteResumenMonto>(MemberList.None).ReverseMap();
            //    cfg.CreateMap<ReporteResumenMontoRecibidoDTO, ReporteResumenMonto>(MemberList.None).ReverseMap();
            //});
            //_mapper = new Mapper(config);
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte ResumenMontos_Cierre.
        /// </summary>
        /// <returns> List<ReporteResumenMontoDTO> </returns>
        public List<ReporteResumenMontosCierreDTO> ObtenerReporteResumenMontosCierre(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                return _unitOfWork.ReporteResumenMontoRepository.ObtenerReporteResumenMontosCierre(FiltroPendiente);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte ResumenMontos_NuevosMatriculados.
        /// </summary>
        /// <returns> List<ReporteResumenMontoDTO> </returns>
        public List<ReporteResumenMontosNuevosMatriculadosDTO> ObtenerReporteResumenMontosNuevosMatriculados(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                return _unitOfWork.ReporteResumenMontoRepository.ObtenerReporteResumenMontosNuevosMatriculados(FiltroPendiente);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte ObtenerReporteResumenMontosDiferencias.
        /// </summary>
        /// <returns> List<ReporteResumenMontoDTO> </returns>
        public List<ReporteResumenMontosDiferenciasDTO> ObtenerReporteResumenMontosDiferencias(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                return _unitOfWork.ReporteResumenMontoRepository.ObtenerReporteResumenMontosDiferencias(FiltroPendiente);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte ObtenerReporteResumenMontos.
        /// </summary>
        /// <returns> List<ReporteResumenMontoDTO> </returns>
        public List<ReporteResumenMontosDTO> ObtenerReporteResumenMontos(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                return _unitOfWork.ReporteResumenMontoRepository.ObtenerReporteResumenMontos(FiltroPendiente);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte ObtenerReporteResumenMontosCambios.
        /// </summary>
        /// <returns> List<ReporteResumenMontoDTO> </returns>
        public List<ReporteResumenMontosCambiosDTO> ObtenerReporteResumenMontosCambios(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                return _unitOfWork.ReporteResumenMontoRepository.ObtenerReporteResumenMontosCambios(FiltroPendiente);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoPeriodoActual(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReporteResumenMontosDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       PyActualInHouse = grupo.Select(x => x.PyActualInHouse).Sum(),
                                       ActualRealInHouse = grupo.Select(x => x.ActualRealInHouse).Sum(),
                                       //DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       //DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            //var cambios = respuestaGeneral.Cambios;
            //var modificaciones = respuestaGeneral.Diferencias;
            //var cantidad = cambios.Count();
            //var agrupadomatricula = (from p in modificaciones
            //                         group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
            //                         select new { g = grupo.Key, l = grupo }).ToList();



            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Retiros Con Devolucion($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Retiros Sin Devolucion($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Proyectado Actual($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Real($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Pendiente($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Proyectado Actual InHouse($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Real InHouse($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            ReporteResumenMontosDetalleDTO detalle11 = new ReporteResumenMontosDetalleDTO();
            detalle11.Tipo = "Pendiente InHouse($)";
            detalle11.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle11);

            ReporteResumenMontosDetalleDTO detalle12 = new ReporteResumenMontosDetalleDTO();
            detalle12.Tipo = "Proyectado Actual TOTAL($)";
            detalle12.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle12);

            ReporteResumenMontosDetalleDTO detalle13 = new ReporteResumenMontosDetalleDTO();
            detalle13.Tipo = "Real TOTAL($)";
            detalle13.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle13);

            ReporteResumenMontosDetalleDTO detalle14 = new ReporteResumenMontosDetalleDTO();
            detalle14.Tipo = "Pendiente TOTAL($)";
            detalle14.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle14);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = ((int)Math.Round(item.Proyectado)).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = ((int)Math.Round(item.DiferenciaCambioFecha)).ToString();
                detalles.Where(w => w.Tipo == "Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = ((int)Math.Round(item.DiferenciaCambioMonto)).ToString();
                detalles.Where(w => w.Tipo == "Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);


                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = ((int)Math.Round((item.Retiro_CD * -1))).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = ((int)Math.Round((item.Retiro_SD * -1))).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = ((int)Math.Round(item.Actual)).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = ((int)Math.Round(item.MontoPagado)).ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = ((int)Math.Round((item.Actual - item.MontoPagado))).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Proyectado Actual Inhouse
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = ((int)Math.Round((item.PyActualInHouse))).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual InHouse($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Real InHouse
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = ((int)Math.Round(item.ActualRealInHouse)).ToString();
                detalles.Where(w => w.Tipo == "Real InHouse($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //Real InHouse
                ReporteResumenMontosDetallesMesesDTO detallemes11 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes11.Mes = item.PeriodoPorFechaVencimiento;
                detallemes11.Monto = ((int)Math.Round((item.PyActualInHouse - item.ActualRealInHouse))).ToString();
                detalles.Where(w => w.Tipo == "Pendiente InHouse($)").FirstOrDefault().ListaMontosMeses.Add(detallemes11);

                //Proyectado Actual total($)
                ReporteResumenMontosDetallesMesesDTO detallemes12 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes12.Mes = item.PeriodoPorFechaVencimiento;
                detallemes12.Monto = ((int)Math.Round((item.Actual + item.PyActualInHouse))).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual TOTAL($)").FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //Real total
                ReporteResumenMontosDetallesMesesDTO detallemes13 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes13.Mes = item.PeriodoPorFechaVencimiento;
                detallemes13.Monto = ((int)Math.Round((item.MontoPagado + item.ActualRealInHouse))).ToString();
                detalles.Where(w => w.Tipo == "Real TOTAL($)").FirstOrDefault().ListaMontosMeses.Add(detallemes13);

                //Pendiente Real
                ReporteResumenMontosDetallesMesesDTO detallemes14 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes14.Mes = item.PeriodoPorFechaVencimiento;
                detallemes14.Monto = ((int)Math.Round(((item.Actual - item.MontoPagado) + (item.PyActualInHouse - item.ActualRealInHouse)))).ToString();
                detalles.Where(w => w.Tipo == "Pendiente TOTAL($)").FirstOrDefault().ListaMontosMeses.Add(detallemes14);
            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }
            var agrupado3 = (from p in finales
                             group p by p.Periodo into grupo
                             select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

            return agrupado3;

        }

        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoPeriodoCierre(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {

            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontosCierre
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReporteResumenMontosCierrePeriodoDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Real($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = ((int)Math.Round(item.Actual)).ToString();
                detalles.Where(w => w.Tipo == "Proyectado($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = ((int)Math.Round(item.MontoPagado)).ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            var agrupado4 = (from p in finales
                             group p by p.Periodo into grupo
                             select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

            return agrupado4;

        }

        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosVariacionMensual(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneralMontos = (from r in respuestaGeneral.ResumenMontos
                                         group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                         select new ReporteResumenMontosVariacionesDTO
                                         {
                                             PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                             ActualMontos = grupo.Select(x => x.Actual).Sum(),
                                             MontoPagadoMontos = grupo.Select(x => x.MontoPagado).Sum(),
                                             DiferenciaPorModificacion = grupo.Select(x => x.DiferenciaPorModificacion).Sum(),
                                             NuevaConsultoria = grupo.Select(x => x.NuevaConsultoria).Sum(),
                                             NuevasMatriculas = grupo.Select(x => x.NuevasMatriculas).Sum(),
                                             IngresoRealNuevasMatriculas = grupo.Select(x => x.IngresoRealNuevasMatriculas).Sum(),
                                             PendientMesOrdenServicio = grupo.Select(x => x.PendientMesOrdenServicio).Sum(),
                                             PendientMesSinOrdenServicio = grupo.Select(x => x.PendientMesSinOrdenServicio).Sum(),
                                             RetirosCD_Mes = grupo.Select(x => x.RetirosCD_Mes).Sum(),
                                             RetirosSD_Mes = grupo.Select(x => x.RetirosSD_Mes).Sum(),
                                             IncrementosDisminucionesCronograma = grupo.Select(x => x.IncrementosDisminucionesCronograma).Sum(),
                                             ModificacionInhouse = grupo.Select(x => x.ModificacionInhouse).Sum(),
                                         });

            var agrupadoGeneralCierre = (from r in respuestaGeneral.ResumenMontosCierre
                                         group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                         select new ReporteResumenMontosUnionCierreDTO
                                         {
                                             PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                             Actual = grupo.Select(x => x.Actual).Sum(),
                                             MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                         });

            var entitiesCierre = agrupadoGeneralCierre.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var entitiesMontos = agrupadoGeneralMontos.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);

            var unionMontosCierre = (from montos in agrupadoGeneralMontos
                                     join cierre in agrupadoGeneralCierre on montos.PeriodoPorFechaVencimiento equals cierre.PeriodoPorFechaVencimiento
                                     select new ReporteResumenMontosVariacionesDTO
                                     {
                                         PeriodoPorFechaVencimiento = montos.PeriodoPorFechaVencimiento,
                                         ActualMontos = montos.ActualMontos,
                                         ActualCierre = cierre.Actual,
                                         MontoPagadoMontos = montos.MontoPagadoMontos,
                                         MontoPagadoCierre = cierre.MontoPagado,
                                         DiferenciaPorModificacion = montos.DiferenciaPorModificacion,
                                         NuevaConsultoria = montos.NuevaConsultoria,
                                         NuevasMatriculas = montos.NuevasMatriculas,
                                         IngresoRealNuevasMatriculas = montos.IngresoRealNuevasMatriculas,
                                         PendientMesOrdenServicio = montos.PendientMesOrdenServicio,
                                         PendientMesSinOrdenServicio = montos.PendientMesSinOrdenServicio,
                                         RetirosCD_Mes = montos.RetirosCD_Mes,
                                         RetirosSD_Mes = montos.RetirosSD_Mes,
                                         IncrementosDisminucionesCronograma = montos.IncrementosDisminucionesCronograma,
                                         ModificacionInhouse = montos.ModificacionInhouse,
                                     });

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "% DIFERENCIA PROYECCION";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "% DIFERENCIA PAGO REAL";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "DIFERENCIA PROYECCION($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Diferencia por modificaciones($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Nuevos Consultorias($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Nuevas matriculas proyectado($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Nuevas matriculas ingreso real del mes($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Ordenes de servicio($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Resto de cta 1 pago periodo actual($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Devolucion de Dinero Alumnos Inscritos($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            ReporteResumenMontosDetalleDTO detalle11 = new ReporteResumenMontosDetalleDTO();
            detalle11.Tipo = "Retiro de Alumnos Inscritos Autorizados($)";
            detalle11.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle11);

            ReporteResumenMontosDetalleDTO detalle12 = new ReporteResumenMontosDetalleDTO();
            detalle12.Tipo = "Cancelación de curso($)";
            detalle12.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle12);

            ReporteResumenMontosDetalleDTO detalle13 = new ReporteResumenMontosDetalleDTO();
            detalle13.Tipo = "Incrementos y disminuciones de Cronogramas($)";
            detalle13.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle13);

            ReporteResumenMontosDetalleDTO detalle14 = new ReporteResumenMontosDetalleDTO();
            detalle14.Tipo = "Modificaciones Inhouse($)";
            detalle14.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle14);

            ReporteResumenMontosDetalleDTO detalle15 = new ReporteResumenMontosDetalleDTO();
            detalle15.Tipo = "DIFERENCIA DE PROYECCION NETA($)";
            detalle15.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle15);

            ReporteResumenMontosDetalleDTO detalle16 = new ReporteResumenMontosDetalleDTO();
            detalle16.Tipo = "DIFERENCIA PAGO REAL($)";
            detalle16.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle16);

            foreach (var item2 in unionMontosCierre)
            {
                var Valor = 0.00m;
                //% DIFERENCIA PROYECCION
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item2.PeriodoPorFechaVencimiento;
                if (item2.ActualCierre == 0)
                {
                    detallemes1.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes1.Monto = "% " + (((item2.ActualMontos / item2.ActualCierre) - 1) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% DIFERENCIA PROYECCION").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //% DIFERENCIA PAGO REAL
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item2.PeriodoPorFechaVencimiento;
                if (item2.MontoPagadoCierre == 0)
                {
                    detallemes2.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes2.Monto = "% " + (((item2.MontoPagadoMontos / item2.MontoPagadoCierre) - 1) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% DIFERENCIA PAGO REAL").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DIFERENCIA PROYECCION($)
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes3.Monto = (item2.ActualMontos - item2.ActualCierre).ToString();
                detalles.Where(w => w.Tipo == "DIFERENCIA PROYECCION($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //Diferencia por modificaciones($)
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item2.DiferenciaPorModificacion.ToString();
                detalles.Where(w => w.Tipo == "Diferencia por modificaciones($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //Nuevos Consultorias($)
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item2.NuevaConsultoria.ToString();
                //detallemes5.Monto = item.NuevasMatriculas.ToString();
                detalles.Where(w => w.Tipo == "Nuevos Consultorias($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Nuevas matriculas proyectado($)
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes6.Monto = item2.NuevasMatriculas.ToString();
                detalles.Where(w => w.Tipo == "Nuevas matriculas proyectado($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Nuevas matriculas ingreso real del mes($)
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes7.Monto = item2.IngresoRealNuevasMatriculas.ToString();
                detalles.Where(w => w.Tipo == "Nuevas matriculas ingreso real del mes($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Ordenes de servicio($)
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item2.PendientMesOrdenServicio.ToString();
                detalles.Where(w => w.Tipo == "Ordenes de servicio($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Resto de cta 1 pago periodo actual($)
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item2.PendientMesSinOrdenServicio.ToString();
                detalles.Where(w => w.Tipo == "Resto de cta 1 pago periodo actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Devolucion de Dinero Alumnos Inscritos($)
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item2.RetirosCD_Mes * -1).ToString();
                detalles.Where(w => w.Tipo == "Devolucion de Dinero Alumnos Inscritos($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //Retiro de Alumnos Inscritos Autorizados($)
                ReporteResumenMontosDetallesMesesDTO detallemes11 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes11.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes11.Monto = (item2.RetirosSD_Mes * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiro de Alumnos Inscritos Autorizados($)").FirstOrDefault().ListaMontosMeses.Add(detallemes11);

                //Cancelación de curso($)
                ReporteResumenMontosDetallesMesesDTO detallemes12 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes12.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes12.Monto = Valor.ToString();
                detalles.Where(w => w.Tipo == "Cancelación de curso($)").FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //Incrementos y disminuciones de Cronogramas($)
                ReporteResumenMontosDetallesMesesDTO detallemes13 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes13.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes13.Monto = item2.IncrementosDisminucionesCronograma.ToString();
                detalles.Where(w => w.Tipo == "Incrementos y disminuciones de Cronogramas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes13);

                //Modificaciones Inhouse($)
                ReporteResumenMontosDetallesMesesDTO detallemes14 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes14.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes14.Monto = item2.ModificacionInhouse.ToString();
                detalles.Where(w => w.Tipo == "Modificaciones Inhouse($)").FirstOrDefault().ListaMontosMeses.Add(detallemes14);

                //DIFERENCIA DE PROYECCION NETA($)
                ReporteResumenMontosDetallesMesesDTO detallemes15 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes15.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes15.Monto = ((item2.ActualMontos - item2.ActualCierre) - (item2.DiferenciaPorModificacion + item2.NuevaConsultoria + item2.NuevasMatriculas +
                (item2.RetirosCD_Mes * -1) + (item2.RetirosSD_Mes * -1) + item2.IncrementosDisminucionesCronograma + item2.ModificacionInhouse)).ToString();
                detalles.Where(w => w.Tipo == "DIFERENCIA DE PROYECCION NETA($)").FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                //%DIFERENCIA PAGO REAL($)
                ReporteResumenMontosDetallesMesesDTO detallemes16 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes16.Mes = item2.PeriodoPorFechaVencimiento;
                detallemes16.Monto = (item2.MontoPagadoMontos - item2.MontoPagadoCierre).ToString();
                detalles.Where(w => w.Tipo == "DIFERENCIA PAGO REAL($)").FirstOrDefault().ListaMontosMeses.Add(detallemes16);

            }

            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            var agrupado25 = (from p in finales
                              group p by p.Periodo into grupo
                              select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

            return agrupado25;

        }

        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosNuevosMatriculados(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenNuevosMatriculados
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReporteResumenMontosPagosDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       IngresoRealNuevasMatriculas = grupo.Select(x => x.TipoFecha== 0 ? x.PagadoD : 0 ).Sum(), // 0 vencimiento 
                                       IngresoRealNuevasMatriculasFechaPago = grupo.Select(x => x.TipoFecha == 1 ? x.PagadoD : 0).Sum(),// 1 pago
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);

            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Ingreso matriculas según fecha de cronograma($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ingreso según fecha de pago($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            foreach (var item in entities)
            {
                //Ingreso matriculas según fecha de cronograma($)
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = ((int)Math.Round(item.IngresoRealNuevasMatriculas)).ToString();
                detalles.Where(w => w.Tipo == "Ingreso matriculas según fecha de cronograma($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //Ingreso según fecha de pago($)
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = ((int)Math.Round(item.IngresoRealNuevasMatriculasFechaPago)).ToString();
                detalles.Where(w => w.Tipo == "Ingreso según fecha de pago($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            var agrupado26 = (from p in finales
                              group p by p.Periodo into grupo
                              select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


            return agrupado26;

        }


        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoPais(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReporteResumenMontosDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = respuestaGeneral.Cambios;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasDTO temp = new ReporteResumenMontosDiferenciasDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();

                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual  ).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual  ).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual  ).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual  ).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado  ).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado  ).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado  ).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado  ).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado  ).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado  ).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado  ).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado  ).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }

            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            var agrupado6 = (from p in finales
                             group p by p.Periodo into grupo
                             select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

            return agrupado6;

        }

        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoModalidadPresencialPais(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where  r.IdTipoModalidad == 0
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTipoModalidad == 0 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasDTO temp = new ReporteResumenMontosDiferenciasDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            var agrupado12 = (from p in finales
                              group p by p.Periodo into grupo
                              select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

            return agrupado12;

        }
        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoModalidadOnlinePais(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTipoModalidad == 2
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTipoModalidad == 2 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasDTO temp = new ReporteResumenMontosDiferenciasDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            var agrupado12 = (from p in finales
                              group p by p.Periodo into grupo
                              select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

            return agrupado12;

        }
        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoModalidadAonlinePais(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where  r.IdTipoModalidad == 1
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTipoModalidad == 1 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasDTO temp = new ReporteResumenMontosDiferenciasDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado   && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }
            var agrupado12 = (from p in finales
                              group p by p.Periodo into grupo
                              select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

            return agrupado12;

        }

        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoModalidadInHousePais(ReporteResumenMontosGeneralTotalDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.ResumenMontos
                                   where r.IdTipoModalidad == -2
                                   group r by new { r.PeriodoPorFechaVencimiento, r.IdTipoModalidad } into grupo
                                   select new ReporteResumenMontosModalidadesDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,
                                       IdTipoModalidad = grupo.Key.IdTipoModalidad,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       Retiro_CD = grupo.Select(x => x.Retiro_CD).Sum(),
                                       Retiro_SD = grupo.Select(x => x.Retiro_SD).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = from r in respuestaGeneral.Cambios where r.IdTipoModalidad == -2 select r;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReporteResumenMontosDiferenciasDTO temp = new ReporteResumenMontosDiferenciasDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        // && w.Coordinador == item.Coordinador
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.IdTipoModalidad == item.IdTipoModalidad).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteResumenMontosDetalleDTO> detalles = new List<ReporteResumenMontosDetalleDTO>();

            ReporteResumenMontosDetalleDTO detalle1 = new ReporteResumenMontosDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteResumenMontosDetalleDTO detalle2 = new ReporteResumenMontosDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteResumenMontosDetalleDTO detalle3 = new ReporteResumenMontosDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteResumenMontosDetalleDTO detalle4 = new ReporteResumenMontosDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteResumenMontosDetalleDTO detalle5 = new ReporteResumenMontosDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteResumenMontosDetalleDTO detalle6 = new ReporteResumenMontosDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteResumenMontosDetalleDTO detalle7 = new ReporteResumenMontosDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteResumenMontosDetalleDTO detalle8 = new ReporteResumenMontosDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteResumenMontosDetalleDTO detalle9 = new ReporteResumenMontosDetalleDTO();
            detalle9.Tipo = "Real($)";
            detalle9.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteResumenMontosDetalleDTO detalle10 = new ReporteResumenMontosDetalleDTO();
            detalle10.Tipo = "Pendiente($)";
            detalle10.ListaMontosMeses = new List<ReporteResumenMontosDetallesMesesDTO>();
            detalles.Add(detalle10);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes1 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReporteResumenMontosDetallesMesesDTO detallemes2 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReporteResumenMontosDetallesMesesDTO detallemes3 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReporteResumenMontosDetallesMesesDTO detallemes4 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReporteResumenMontosDetallesMesesDTO detallemes5 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Retiros Con Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes6 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.Retiro_CD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReporteResumenMontosDetallesMesesDTO detallemes7 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.Retiro_SD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Inicial
                ReporteResumenMontosDetallesMesesDTO detallemes8 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Real
                ReporteResumenMontosDetallesMesesDTO detallemes9 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = item.MontoPagado.ToString();
                detalles.Where(w => w.Tipo == "Real($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Pendiente
                ReporteResumenMontosDetallesMesesDTO detallemes10 = new ReporteResumenMontosDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                detallemes10.Monto = (item.Actual - item.MontoPagado).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

            }
            List<ReporteResumenMontosDetalleFinalDTO> finales = new List<ReporteResumenMontosDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteResumenMontosDetalleFinalDTO item = new ReporteResumenMontosDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }

            var agrupado23 = (from p in finales
                              group p by p.Periodo into grupo
                              select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

            return agrupado23;

        }

    }

}
