using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReporteIngresoRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteIngreso
    /// </summary>
    public class ReporteIngresoRepository : IReporteIngresoRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteIngresoRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosVentas(FiltroFechaDTO Filtro)
        {
            try
            {
                 List<PagoAlumnoIngresosDTO> items = new List<PagoAlumnoIngresosDTO>();
                var CamposTabla = "CodigoMatricula,IdCronogramaPagoDetalleFinal,IdMatriculaCabecera,nrocuota,nrosubcuota,cuotadolares,montopagado,MontoPagadoTipoCambioFechaPago" +
                    ", periodoporfechavencimiento, periodofechapago, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, FechaIngresoEnCuenta" +
                    ", EstadoEfectivo, FechaCuota, IdCiudad, IdCategoriaOrigen, fechapagoOriginal,FechaMatricula, PorcentajeComision, CobroComisionMontoPagado";
                var _query = string.Empty;

                _query = "select " + CamposTabla + " from [fin].[V_ReporteIngresosPagoAlumnosVentas] where CAST(FechaPago as date) between CAST(@FechaInicio as date) and CAST(@FechaFin as DATE) ";

                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { FechaInicio = Filtro.FechaInicio, FechaFin = Filtro.FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagoAlumnoIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosOperaciones(FiltroFechaDTO Filtro)
        {
            try
            {

                List<PagoAlumnoIngresosDTO> items = new List<PagoAlumnoIngresosDTO>();
                var CamposTabla = "CodigoMatricula,IdCronogramaPagoDetalleFinal,IdMatriculaCabecera,nrocuota,nrosubcuota,cuotadolares,montopagado,MontoPagadoTipoCambioFechaPago" +
                    ", periodoporfechavencimiento, periodofechapago, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, FechaIngresoEnCuenta" +
                    ", EstadoEfectivo, FechaCuota, IdCiudad, IdCategoriaOrigen, fechapagoOriginal,FechaMatricula, PorcentajeComision, CobroComisionMontoPagado";
                var _query = string.Empty;

                _query = "select " + CamposTabla + " from [fin].[V_ReporteIngresosPagoAlumnosOperaciones] where CAST(FechaPago as date) between CAST(@FechaInicio as date) and CAST(@FechaFin as DATE) ";

                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { FechaInicio = Filtro.FechaInicio, FechaFin = Filtro.FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagoAlumnoIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerReporteIngresosOperacionesTipoCambio(FiltroFechaDTO Filtro)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();
                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                            ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                            ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReporteIngresosPagoAlumnosOperacionesTipoCambio where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { fechaInicio = Filtro.FechaInicio, fechaFin = Filtro.FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosOtrosIngresos(FiltroFechaDTO Filtro)
        {
            try
            {

                List<PagoAlumnoIngresosDTO> items = new List<PagoAlumnoIngresosDTO>();
                var CamposTabla = "IdCronogramaPagoDetalleFinal,IdMatriculaCabecera,nrocuota,nrosubcuota,cuotadolares,montopagado,MontoPagadoTipoCambioFechaPago" +
                    ", periodoporfechavencimiento, periodofechapago, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, FechaIngresoEnCuenta" +
                    ", EstadoEfectivo, FechaCuota, IdCiudad, IdCategoriaOrigen, fechapagoOriginal, PorcentajeComision, CobroComisionMontoPagado, IdTipoMovimientoCaja";
                var _query = string.Empty;

                _query = "select " + CamposTabla + " from [fin].[V_ReporteIngresosOtrosIngresosEgresos] where FechaPago between @FechaInicio  and @FechaFin ";

                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { FechaInicio = Filtro.FechaInicio, FechaFin = Filtro.FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagoAlumnoIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresos(FiltroFechaDTO Filtro)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                            ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                            ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosIngresos where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { fechaInicio = Filtro.FechaInicio, fechaFin = Filtro.FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosPosterior(FiltroFechaDTO Filtro)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                             ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsideraDiasHabilesLV, ConsideraDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                             ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosIngresosSinDepositos where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { fechaInicio = Filtro.FechaInicio, fechaFin = Filtro.FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosAnterior(FiltroFechaDTO Filtro)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                             ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsideraDiasHabilesLV, ConsideraDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                             ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosIngresosSinDepositos where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { fechaInicio = Filtro.FechaInicio, fechaFin = Filtro.FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosGestionCobranza(FiltroFechaDTO Filtro)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                             ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                             ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosGestionCobranza where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { fechaInicio = Filtro.FechaInicio, fechaFin = Filtro.FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosTasasAcademicas(FiltroFechaDTO Filtro)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                            ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                            ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosTasasAcademicas where CAST(FechaPagoOriginal as date)>=CAST(@FechaInicio as date) and CAST(FechaPagoOriginal as date)<=CAST(@FechaFin as date) ";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { FechaInicio = Filtro.FechaInicio, FechaFin = Filtro.FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosAnteriorConDeposito(FiltroFechaDTO Filtro)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                          ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro,FechaDepositaron, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                          ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosIngresosConDepositos where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { FechaInicio = Filtro.FechaInicio, FechaFin = Filtro.FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosPosteriorConDeposito(FiltroFechaDTO Filtro)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                               ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro,FechaDepositaron, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                               ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosIngresosConDepositos where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { FechaInicio = Filtro.FechaInicio, FechaFin = Filtro.FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
