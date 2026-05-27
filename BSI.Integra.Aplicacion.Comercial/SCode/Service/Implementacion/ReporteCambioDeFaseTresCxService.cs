using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ReporteCambioDeFaseTresCxService
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de reporte de cambios de fase 3cx
    /// </summary>
    public class ReporteCambioDeFaseTresCxService : IReporteCambioDeFaseTresCxService
    {

        private IUnitOfWork _unitOfWork;

        public AgendaDTO agendaBo = new AgendaDTO();
        public ReporteCambioDeFaseTresCxService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase consultando a IntegraDB
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> ObjetoDTO: ReporteCambioDeFaseDataDTO </returns>
        public async Task<ReporteCambioDeFaseTasaContactoDTO> GenerarReporteTasaContactoTresCxAsync(ReporteCambioFaseFiltrosDTO filtros)
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
                var task2 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoConySinLlamadaTresCxAsync(nuevoFiltro);
                var esHoy = (DateTime.Compare(nuevoFiltro.FechaInicio, fechaActual) == 0);
                Task<ReporteTasaContactoDTO> task1 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoTresCxAsync(nuevoFiltro, esHoy);
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

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase consultando a IntegraDB
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> ObjetoDTO: ReporteCambioDeFaseDataDTO </returns>
        public async Task<ReporteCambioDeFaseTasaContactoDTO> GenerarReporteTasaContactoTresCxTotalAsync(ReporteCambioFaseFiltrosDTO filtros)
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
                var task2 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoConySinLlamadaTresCxTotalAsync(nuevoFiltro, 0);
                var esHoy = (DateTime.Compare(nuevoFiltro.FechaInicio, fechaActual) == 0);
                Task<ReporteTasaContactoDTO> task1 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoTresCxV2Async(nuevoFiltro, esHoy);
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
        /// Autor: Carlos Crispin Riquelme
        /// Fecha: 27/05/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase de Llamadas por Whatsapp consultando a IntegraDB
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> ObjetoDTO: ReporteCambioDeFaseDataDTO </returns>
        public async Task<ReporteCambioDeFaseTasaContactoWhatsappDTO> GenerarReporteTasaContactoWhatsappAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var nuevoFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                nuevoFiltro.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                nuevoFiltro.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var nuevoFiltroConsentimiento = new ReporteCambioFaseFiltroProcesadoDTO();
                nuevoFiltroConsentimiento.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                nuevoFiltroConsentimiento.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var queryFiltro = "";
                var queryFiltroConsentimeinto = "";
                if (filtros.Asesores.Count() > 0)
                {
                    queryFiltro += " AND IdPersonalAsignado IN (" + string.Join(",", filtros.Asesores) + ")";
                    queryFiltroConsentimeinto += " AND WL.IdPersonal IN (" + string.Join(",", filtros.Asesores) + ")";
                } 
                if (filtros.CentroCostos.Count() > 0)
                    queryFiltro += " AND IdCentroCosto IN (" + string.Join(",", filtros.CentroCostos) + ")";
                nuevoFiltro.Filtro = queryFiltro;
                nuevoFiltroConsentimiento.Filtro = queryFiltroConsentimeinto;

                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);
                var task2 = _unitOfWork.ReportesRepository.ObtenerReporteConsentimientoWhatsappV2Async(nuevoFiltroConsentimiento);
                var esHoy = (DateTime.Compare(nuevoFiltro.FechaInicio, fechaActual) == 0);
                Task<ReporteTasaContactoDTO> task1 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoWhatsappV2Async(nuevoFiltro, esHoy);
                ReporteCambioDeFaseTasaContactoWhatsappDTO data = new();
                data.ReporteTasaContactoLlamadasWhatsapp = await task1;
                data.ReporteConsentimientosWhatsapp = await task2;
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase consultando a IntegraDB
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> ObjetoDTO: ReporteCambioDeFaseDataDTO </returns>
        public async Task<ReporteCambioDeFaseTasaContactoDTO> GenerarReporteTasaContactoTresCxV2Async(ReporteCambioFaseFiltrosDTO filtros)
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
                var task2 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoConySinLlamadaTresCxTotalAsync(nuevoFiltro, 0);
                var esHoy = (DateTime.Compare(nuevoFiltro.FechaInicio, fechaActual) == 0);
                Task<ReporteTasaContactoDTO> task1 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoTresCxV2Async(nuevoFiltro, esHoy);
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase consultando a IntegraDB
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> ObjetoDTO: ReporteCambioDeFaseDataDTO </returns>
        public async Task<ReporteCambioDeFaseTasaContactoDTO> GenerarReporteTasaContactoTresCxOtroMedioAsync(ReporteCambioFaseFiltrosDTO filtros)
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
                var esHoy = (DateTime.Compare(nuevoFiltro.FechaInicio, fechaActual) == 0);
                Task<ReporteTasaContactoDTO> task1 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoTresCxOtroMedioAsync(nuevoFiltro, esHoy);
                var task2 = _unitOfWork.ReportesRepository.ObtenerReporteTasaContactoConySinLlamadaTresCxV2Async(nuevoFiltro, 1);
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase para control de cambios de fase
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> ReporteCambioDeFaseDataV2DTO </returns>
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

                tarea1 = _unitOfWork.ReportesRepository.ObtenerReporteMetasObtenerTotalISAsync(filtros);/*No usa ninguna tabla de llamadas*/
                var esHoy = (DateTime.Compare(nuevoFiltro.FechaInicio, fechaActual) == 0);

                //Task<List<ControlCambioDeFaseV2DTO>> tarea3 = _unitOfWork.ReportesRepository.ObtenerReporteControlCambiodeFaseTresCxAsync(filtros, esHoy);
                Task<List<ControlCambioDeFaseV2DTO>> tarea3 = _unitOfWork.ReportesRepository.ObtenerReporteControlCambiodeFaseTresCxV2Async(filtros, esHoy);
                Task<List<ControlOtroMedioDTO>> tarea9 = _unitOfWork.ReportesRepository.ObtenerReporteControlCambiodeFaseOtroMedioTresCxV2Async(filtros, esHoy);

                Task<List<ReporteCambiosDeFaseOportunidadDTO>> tarea4;
                Task<List<ReporteCambiosDeFaseOportunidadDTO>> tarea8;
                Task<List<ReporteCambiosDeFaseOportunidadDTO>> tarea5;
                Task<List<ReporteCambiosDeFaseOportunidadDTO>> tarea6;
                Task<List<ReporteCambiosDeFaseOportunidadDTO>> tarea7;


                if (filtros.Acumulado)
                {
                    tarea4 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadAcumuladoV2Async(nuevoFiltro);/*No usa ninguna tabla de llamadas*/
                    tarea8 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadAcumuladoPredictivoAsync(nuevoFiltro);/*No usa ninguna tabla de llamadas*/
                    //tarea5 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadAcumuladoConLlamadaTresCxAsync(nuevoFiltro);
                    tarea5 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadAcumuladoConLlamadaAlternoAsync(filtros);
                    //tarea6 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadAcumuladoSinLlamadaTresCxAsync(nuevoFiltro);
                    tarea6 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadAcumuladoSinLlamadaAlternoAsync(filtros);
                    tarea7 = _unitOfWork.ReportesRepository.ObtenerReporteControlAcumuladoRN1yRNAsync(nuevoFiltro);/*No usa ninguna tabla de llamadas*/
                }
                else
                {
                    tarea4 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoV2Async(nuevoFiltro);/*No usa ninguna tabla de llamadas*/
                    tarea8 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoPredictivoAsync(nuevoFiltro);/*No usa ninguna tabla de llamadas*/
                    //tarea5 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoConLlamadaTresCxAsync(nuevoFiltro);
                    tarea5 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoConLlamadaAlternoAsync(filtros);
                    //tarea6 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamadaTresCxAsync(nuevoFiltro);
                    tarea6 = _unitOfWork.ReportesRepository.ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamadaAlternoAsync(filtros);
                    tarea7 = _unitOfWork.ReportesRepository.ObtenerReporteControlNoAcumuladoRN1yRNAsync(nuevoFiltro);
                }

                ReporteCambioDeFaseDataV2DTO data = new ReporteCambioDeFaseDataV2DTO();
                data.ReporteMetasObtenerTotalIS = await tarea1;
                data.ControlCambiodeFase = await tarea3;
                data.ReporteOtroMedio = await tarea9;
                data.ReporteCambiosDeFaseOportunidad = await tarea4;
                data.ReporteCambiosDeFaseOportunidadPredictivo = await tarea8;
                data.ReporteCambiosDeFaseOportunidadConLlamada = await tarea5;
                data.ReporteCambiosDeFaseOportunidadSinLlamada = await tarea6;
                data.ReporteControlRN1yRN2 = await tarea7;
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase para control de cambios de fase
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> ReporteCambioDeFaseDataDTO </returns>
        public async Task<ReporteCambioDeFaseDataDTO> ReporteCambioDeFaseV2IntegraAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                filtros.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtros.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var tarea1 = _unitOfWork.ReportesRepository.ObtenerActividadesSinCambiodeFaseAlternoTresCxAsync(filtros);
                var tarea2 = _unitOfWork.ReportesRepository.ObtenerReporteActividadesVencidasporTabAsync(filtros);
                var tarea3 = _unitOfWork.ReportesRepository.ObtenerReporteTasaDeConversionAsync(filtros);
                var tarea4 = _unitOfWork.ReportesRepository.ObtenerReporteTasaDeConversionPredictivaAsync(filtros);

                var reporteCambioDeFaseDataDTO = new ReporteCambioDeFaseDataDTO();
                reporteCambioDeFaseDataDTO.EjecutadasSinCambiodeFase = await tarea1;
                reporteCambioDeFaseDataDTO.ActividadVencidaporTab = await tarea2;
                reporteCambioDeFaseDataDTO.ReporteTasaDeCambio = await tarea3;
                reporteCambioDeFaseDataDTO.ReporteTasaDeCambioPredictivo = await tarea4;
                return reporteCambioDeFaseDataDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase para control de cambios de fase
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> ReporteCambioDeFaseDataDTO </returns>
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
                reporteActividadEjecutadaLlamadaObservada = _unitOfWork.ReportesRepository.ObtenerReporteActividadEjecutadaLlamadaObservadaTresCx(filtroSP, esHoy);
                return reporteActividadEjecutadaLlamadaObservada;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Cambio de Fase para control de cambios de fase
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> ReporteCambioDeFaseDataDTO </returns>
        public List<LlamadaObservadaDTO> ObtenerReporteActividadEjecutadaLlamadaObservadaV2(ReporteCambioFaseFiltrosDTO filtros)
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
                reporteActividadEjecutadaLlamadaObservada = _unitOfWork.ReportesRepository.ObtenerReporteActividadEjecutadaLlamadaObservadaTresCxV2(filtroSP, esHoy);
                return reporteActividadEjecutadaLlamadaObservada;
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
        /// Obtiene el reporte de llamadas acumuladas reprogramadas manualmente
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

                var acumuladoTiempoContactoEfectivoAbiertas = _unitOfWork.ReportesRepository.ObtenerAcumuladoTiempoContactoEfectivoOportunidadAbiertasTresCx(filtroSP2);
                //var acumuladoTiempoContactoEfectivoCerradas = _unitOfWork.ReportesRepository.ObtenerAcumuladoTiempoContactoEfectivoOportunidadCerradasTresCx(filtroSP);

                acumuladoTiempoContactoEfectivo.AddRange(acumuladoTiempoContactoEfectivoAbiertas);
                //acumuladoTiempoContactoEfectivo.AddRange(acumuladoTiempoContactoEfectivoCerradas);

                return acumuladoTiempoContactoEfectivo;
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
        /// Obtiene el reporte de llamadas acumuladas reprogramadas manualmente
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<LlamadaObservadaDTO> ObtenerAcumuladoLlamadasReprogramadasManualmente(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var filtroSP2 = ObtenerFiltroBase(filtros);
                List<LlamadaObservadaDTO> acumuladoLlamadasReprogramadasManualmente = _unitOfWork.ReportesRepository.ObtenerAcumuladoLlamadasReprogramadasManualmente(filtroSP2);
                return acumuladoLlamadasReprogramadasManualmente;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 06/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de actividades ejecutadas en la fase actual
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ActividadEjecutadaFaseActualDTO> ObtenerActividadEjecutadaFaseActualTresCx(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var filtroSP2 = ObtenerFiltroBase(filtros);
                List<ActividadEjecutadaFaseActualDTO> actividadEjecutadaFaseActual = _unitOfWork.ReportesRepository.ObtenerActividadEjecutadaFaseActualTresCx(filtroSP2);
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
        /// Obtiene el filtro para los reportes
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        private ReporteCambioFaseSPFiltrosDTO ObtenerFiltroBase(ReporteCambioFaseFiltrosDTO filtros)
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
            return filtroSP2;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 26/2/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de control de oportunidades predictivas
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ControlOportunidadPredictivaDTO> ObtenerControlOportunidadPredictiva(ReporteCambioFaseFiltrosDTO filtros)
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
                var resultado = _unitOfWork.ReportesRepository.ObtenerControlOportunidadPredictiva(filtroSP);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
