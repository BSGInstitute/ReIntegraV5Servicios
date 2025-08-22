using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    /// Service: ReporteCambiodeFaseService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de Informacion de Oportunidades
    /// </summary>
    public class ReporteCambiodeFaseService : IReporteCambiodeFaseService
    {
        private IUnitOfWork _unitOfWork;

        public AgendaDTO agendaBo = new AgendaDTO();
        public ReporteCambiodeFaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase consultando a IntegraDB
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> ObjetoDTO: ReporteCambioDeFaseDataDTO </returns>
        public async Task<ReporteCambioDeFaseDataDTO> ReporteCambioDeFaseV2IntegraAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                filtros.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtros.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                //var tarea1 = _unitOfWork.ReportesRepository.ObtenerReporteEjecutadasSinCambiodeFaseAsync(filtros);
                var tarea1 = _unitOfWork.ReportesRepository.ObtenerActividadesSinCambiodeFaseAlternoAsync(filtros);
                var tarea2 = _unitOfWork.ReportesRepository.ObtenerReporteActividadesVencidasporTabAsync(filtros);
                var tarea3 = _unitOfWork.ReportesRepository.ObtenerReporteTasaDeConversionAsync(filtros);

                var reporteCambioDeFaseDataDTO = new ReporteCambioDeFaseDataDTO();
                reporteCambioDeFaseDataDTO.EjecutadasSinCambiodeFase = await tarea1;
                reporteCambioDeFaseDataDTO.ActividadVencidaporTab = await tarea2;
                reporteCambioDeFaseDataDTO.ReporteTasaDeCambio = await tarea3;
                return reporteCambioDeFaseDataDTO;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por asesor
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<LlamadaObservadaDTO> ObtenerReporteActividadEjecutadaLlamadaObservada(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var filtroSP = new ReporteCambioFaseSPFiltrosDTO();
                filtroSP.FechaInicio = Convert.ToDateTime(filtros.FechaInicio).Date;
                filtroSP.FechaFin = Convert.ToDateTime(filtros.FechaFin).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                if (filtros.Asesores != null && filtros.Asesores.Count() > 0)
                {
                    filtroSP.Asesores = string.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos != null && filtros.CentroCostos.Count() > 0)
                {
                    filtroSP.CentroCostos = string.Join(",", filtros.CentroCostos);
                }
                var fechaActual = DateTime.Now;
                fechaActual = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 0, 0, 0);
                List<LlamadaObservadaDTO> reporteActividadEjecutadaLlamadaObservada;
                var esHoy = (DateTime.Compare(filtroSP.FechaInicio, fechaActual) == 0);
                reporteActividadEjecutadaLlamadaObservada = _unitOfWork.ReportesRepository.ObtenerReporteActividadEjecutadaLlamadaObservada(filtroSP, esHoy);
                return reporteActividadEjecutadaLlamadaObservada;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por asesor
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<AcumuladoTiempoContactoEfectivoDTO> ObtenerAcumuladoTiempoContactoEfectivo(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var filtroSP = new ReporteCambioFaseSPFiltrosDTO();
                var filtroSP2 = new ReporteCambioFaseSPFiltrosDTO();
                filtroSP.FechaInicio = Convert.ToDateTime(filtros.FechaInicio).Date;
                filtroSP.FechaFin = Convert.ToDateTime(filtros.FechaFin).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                filtroSP2.FechaInicio = Convert.ToDateTime(DateTime.Now).Date;
                filtroSP2.FechaFin = Convert.ToDateTime(DateTime.Now).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                if (filtros.Asesores != null && filtros.Asesores.Count() > 0)
                {
                    filtroSP.Asesores = string.Join(",", filtros.Asesores);
                    filtroSP2.Asesores = string.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos != null && filtros.CentroCostos.Count() > 0)
                {
                    filtroSP.CentroCostos = string.Join(",", filtros.CentroCostos);
                    filtroSP2.CentroCostos = string.Join(",", filtros.CentroCostos);
                }
                var fechaActual = DateTime.Now;
                fechaActual = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 0, 0, 0);
                List<AcumuladoTiempoContactoEfectivoDTO> acumuladoTiempoContactoEfectivo = new();

                var acumuladoTiempoContactoEfectivoAbiertas = _unitOfWork.ReportesRepository.ObtenerAcumuladoTiempoContactoEfectivoOportunidadAbiertas(filtroSP2);
                var acumuladoTiempoContactoEfectivoCerradas = _unitOfWork.ReportesRepository.ObtenerAcumuladoTiempoContactoEfectivoOportunidadCerradas(filtroSP);

                acumuladoTiempoContactoEfectivo.AddRange(acumuladoTiempoContactoEfectivoAbiertas);
                acumuladoTiempoContactoEfectivo.AddRange(acumuladoTiempoContactoEfectivoCerradas);

                return acumuladoTiempoContactoEfectivo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por asesor
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<LlamadaObservadaDTO> ObtenerAcumuladoLlamadasReprogramadasManualmente(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var filtroSP2 = new ReporteCambioFaseSPFiltrosDTO();
                filtroSP2.FechaInicio = Convert.ToDateTime(DateTime.Now).Date;
                filtroSP2.FechaFin = Convert.ToDateTime(DateTime.Now).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                if (filtros.Asesores != null && filtros.Asesores.Count() > 0)
                {
                    filtroSP2.Asesores = string.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos != null && filtros.CentroCostos.Count() > 0)
                {
                    filtroSP2.CentroCostos = string.Join(",", filtros.CentroCostos);
                }
                List<LlamadaObservadaDTO> acumuladoLlamadasReprogramadasManualmente = _unitOfWork.ReportesRepository.ObtenerAcumuladoLlamadasReprogramadasManualmente(filtroSP2);

                return acumuladoLlamadasReprogramadasManualmente;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por asesor
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ActividadEjecutadaFaseActualDTO> ObtenerActividadEjecutadaFaseActual(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var filtroSP2 = new ReporteCambioFaseSPFiltrosDTO();

                filtroSP2.FechaInicio = Convert.ToDateTime(DateTime.Now).Date;
                filtroSP2.FechaFin = Convert.ToDateTime(DateTime.Now).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                if (filtros.Asesores != null && filtros.Asesores.Count() > 0)
                {
                    filtroSP2.Asesores = string.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos != null && filtros.CentroCostos.Count() > 0)
                {
                    filtroSP2.CentroCostos = string.Join(",", filtros.CentroCostos);
                }

                List<ActividadEjecutadaFaseActualDTO> actividadEjecutadaFaseActual = _unitOfWork.ReportesRepository.ObtenerActividadEjecutadaFaseActual(filtroSP2);

                return actividadEjecutadaFaseActual;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/12/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de cambios de fase
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public async Task<ReporteCambioDeFaseDataV2DTO> ReporteCambioDeFaseV2Async(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var nuevoFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                var nuevoFiltroProcedimiento = new ReporteCambioFaseFiltroProcedimientoDTO();
                nuevoFiltro.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                nuevoFiltro.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                filtros.FechaFin = nuevoFiltro.FechaFin;
                filtros.FechaInicio = nuevoFiltro.FechaInicio;

                nuevoFiltroProcedimiento.FechaFin = nuevoFiltro.FechaFin;
                nuevoFiltroProcedimiento.FechaInicio = nuevoFiltro.FechaInicio;

                var queryFiltro = "";

                if (filtros.Asesores.Count() > 0)
                {
                    queryFiltro = queryFiltro + " and ";
                    queryFiltro = queryFiltro + "IdPersonalAsignado in (" + string.Join(",", filtros.Asesores) + ")";
                    nuevoFiltroProcedimiento.IdPersonal = string.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    queryFiltro = queryFiltro + " and ";
                    queryFiltro = queryFiltro + "IdCentroCosto in (" + string.Join(",", filtros.CentroCostos) + ")";
                    nuevoFiltroProcedimiento.IdCentroCosto = string.Join(",", filtros.CentroCostos);
                }
                nuevoFiltro.Filtro = queryFiltro;
                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);
                Task<int> tarea1;
                Task<List<ControlCambioDeFaseV2DTO>> tarea3;

                tarea1 = _unitOfWork.ReportesRepository.ObtenerReporteMetasObtenerTotalISAsync(filtros);
                if (DateTime.Compare(nuevoFiltro.FechaInicio, fechaActual) == 0)
                {
                    tarea3 = _unitOfWork.ReportesRepository.ObtenerReporteControlCambiodeFaseAsync(filtros);
                }
                else
                {
                    tarea3 = _unitOfWork.ReportesRepository.ObtenerReporteControlCambiodeFaseCongeladoAsync(filtros);
                }

                Task<List<ReporteCambiosDeFaseOportunidadDTO>> tarea4;
                Task<List<ReporteCambiosDeFaseOportunidadDTO>> tarea8;
                Task<List<ReporteCambiosDeFaseOportunidadDTO>> tarea5;
                Task<List<ReporteCambiosDeFaseOportunidadDTO>> tarea6;
                Task<List<ReporteCambiosDeFaseOportunidadDTO>> tarea7;
                if (filtros.Acumulado)
                {
                    tarea4 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadAcumuladoV2Async(nuevoFiltro);
                    tarea8 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadAcumuladoPredictivoAsync(nuevoFiltro);
                    tarea5 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadAcumuladoConLlamadaAsync(nuevoFiltro);
                    tarea6 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadAcumuladoSinLlamadaAsync(nuevoFiltro);
                    tarea7 = _unitOfWork.ReportesRepository.ObtenerReporteControlAcumuladoRN1yRNAsync(nuevoFiltro);
                }
                else
                {
                    tarea4 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoV2Async(nuevoFiltro);
                    tarea8 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoPredictivoAsync(nuevoFiltro);
                    tarea5 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoConLlamadaAsync(nuevoFiltro);
                    tarea6 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamadaAsync(nuevoFiltro);
                    tarea7 = _unitOfWork.ReportesRepository.ObtenerReporteControlNoAcumuladoRN1yRNAsync(nuevoFiltro);
                }
                ReporteCambioDeFaseDataV2DTO data = new ReporteCambioDeFaseDataV2DTO();
                data.ReporteMetasObtenerTotalIS = await tarea1;
                data.ControlCambiodeFase = await tarea3;
                data.ReporteCambiosDeFaseOportunidad = await tarea4;
                data.ReporteCambiosDeFaseOportunidadPredictivo = await tarea8;
                data.ReporteCambiosDeFaseOportunidadConLlamada = await tarea5;
                data.ReporteCambiosDeFaseOportunidadSinLlamada = await tarea6;
                data.ReporteControlRN1yRN2 = await tarea7;
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase consultando a Replica
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteTasaContactoConySinLlamadaDTO </returns>
        public async Task<ReporteCambioDeFaseTasaContactoDTO> GenerarReporteV2TasaContactoAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var nuevoFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                nuevoFiltro.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                nuevoFiltro.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var queryFiltro = "";
                if (filtros.Asesores.Count() > 0)
                    queryFiltro += " AND IdPersonalAsignado IN (" + string.Join(",", filtros.Asesores) + ")";
                if (filtros.CentroCostos.Count() > 0)
                    queryFiltro += " AND IdCentroCosto IN (" + string.Join(",", filtros.CentroCostos) + ")";
                nuevoFiltro.Filtro = queryFiltro;

                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);
                Task<ReporteTasaContactoDTO> task1;
                var task2 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoConySinLlamadaAsync(nuevoFiltro);
                if (DateTime.Compare(nuevoFiltro.FechaInicio, fechaActual) == 0)
                    task1 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoAsync(nuevoFiltro);
                else
                    task1 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoCongeladoAsync(nuevoFiltro);
                ReporteCambioDeFaseTasaContactoDTO data = new();
                data.ReporteTasaContacto = await task1;
                data.ReporteTasaContactoConySinLlamada = await task2;
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase consultando a Replica
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteCambioDeFaseDataV2DTO </returns>
        public IEnumerable<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteCambiosDeFaseControlBICYEAcumulado(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var nuevoFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                nuevoFiltro.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                nuevoFiltro.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var queryFiltro = "";
                if (filtros.Asesores.Count() > 0)
                {
                    queryFiltro += " AND IdPersonalAsignado IN (" + string.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    queryFiltro += " AND IdCentroCosto IN (" + string.Join(",", filtros.CentroCostos) + ")";
                }
                nuevoFiltro.Filtro = queryFiltro;
                var resultado = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseControlBICYEAcumulado(nuevoFiltro);
                return resultado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase consultando a Replica
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteCambioDeFaseDataV2DTO </returns>
        public async Task<IEnumerable<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseControlBICYEAcumuladoAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var nuevoFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                nuevoFiltro.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                nuevoFiltro.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var queryFiltro = "";
                if (filtros.Asesores.Count() > 0)
                {
                    queryFiltro += " AND IdPersonalAsignado IN (" + string.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    queryFiltro += " AND IdCentroCosto IN (" + string.Join(",", filtros.CentroCostos) + ")";
                }
                nuevoFiltro.Filtro = queryFiltro;

                var resultado = await _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseControlBICYEAcumuladoAsync(nuevoFiltro);
                return resultado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Calidad Consultando a "Réplica"
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteCalidadCambioDeFaseDTO </returns>
        public ReporteConteoDatosFaseDTO ObtenerReporteConteoDatosFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var nuevoFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                var nuevoFiltroProcedimiento = new ReporteCambioFaseFiltroProcedimientoDTO();
                var reporteCalidadCambioDeFaseDataDTO = new ReporteConteoDatosFaseDTO();

                filtros.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtros.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                var resultado = _unitOfWork.ReportesRepository.ObtenerReporteConteoDatosFase(filtros);
                reporteCalidadCambioDeFaseDataDTO.ConteoDatosFase = resultado.ConteoDatosFase;
                reporteCalidadCambioDeFaseDataDTO.FechaConteoInicio = resultado.FechaInicio.Valor;
                reporteCalidadCambioDeFaseDataDTO.FechaConteoMomento = resultado.FechaMomento.Valor;
                return reporteCalidadCambioDeFaseDataDTO;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 11/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Conteo datos fase
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteCalidadCambioDeFaseDTO </returns>
        public List<ConteoDatosFaseAlternoDTO> ObtenerReporteConteoDatosFaseAlterno(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                filtros.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtros.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                var resultado = _unitOfWork.ReportesRepository.ObtenerReporteConteoDatosFaseAlterno(filtros);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
