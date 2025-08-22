using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ReportePendienteService
    /// Autor: Jonathan Caipo
    /// Fecha: 13/01/2023
    /// <summary>
    /// Gestión general de Reportes.
    /// </summary>
    public class ReportePendienteService : IReportePendienteService
    {
        private IUnitOfWork _unitOfWork;

        public ReportePendienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene combo de pendientes.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> DTO: comboPendiente - ComboPendienteDTO </returns>
        public CombosPendienteDTO ObtenerCombosPendientes(int idPersonal)
        {
            try
            {
                PeriodoService periodoService = new PeriodoService(_unitOfWork);
                PersonalService personalService = new PersonalService(_unitOfWork);
                CombosPendienteDTO comboPendiente = new CombosPendienteDTO();
                List<PersonalAsignadoDTO> asistentes = personalService.ObtenerPersonalAsignadoOperacionesUsuarioTotal(idPersonal);

                //activos
                comboPendiente.ListaModalidades = _unitOfWork.ModalidadCursoRepository.ObtenerCombo().ToList();
                comboPendiente.ListaPeriodo = periodoService.ObtenerPeriodosPendiente();
                //activos
                comboPendiente.AsistentesActivos = asistentes.Where(w => w.Activo == true).ToList();
                //todos
                comboPendiente.AsistentesTotales = asistentes;
                //inactivo
                comboPendiente.AsistentesInactivos = asistentes.Where(w => w.Activo == false).ToList();

                return comboPendiente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <param name="filtroPendiente"></param>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        public ReportePendienteCompuestoDTO GenerarReporte(ReportePendienteFiltroDTO filtroPendiente)
        {
            try
            {
                if (filtroPendiente.Modalidad.Count() == 0)
                {
                    filtroPendiente.Modalidad = _unitOfWork.ModalidadCursoRepository.ObtenerCombo().Select(w => w.Id).ToList();
                }

                CronogramaPagoDetalleFinalService reporteCronogramaGeneral = new CronogramaPagoDetalleFinalService(_unitOfWork);
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReportePendienteOperacionesGeneral(filtroPendiente);

                CronogramaPagoDetalleFinalService reporteCronograma = new CronogramaPagoDetalleFinalService(_unitOfWork);
                var listRpta = reporteCronograma.GenerarReportePendientePorPeriodoOperaciones(respuestaGeneral);
                var agrupado = (from p in listRpta
                                group p by p.Periodo into grupo
                                select new ReportePendienteDTO { g = grupo.Key, l = grupo.ToList() });

                CronogramaPagoDetalleFinalService reporteCronograma2 = new CronogramaPagoDetalleFinalService(_unitOfWork);
                var listRpta2 = reporteCronograma2.GenerarReportePendienteIngresoVentasPorPeriodoOperacionesAnterior(respuestaGeneral);
                var agrupado2 = (from p in listRpta2
                                 group p by p.Periodo into grupo
                                 select new ReportePendienteDTO { g = grupo.Key, l = grupo.ToList() }).ToList();

                CronogramaPagoDetalleFinalService reporteCronograma3 = new CronogramaPagoDetalleFinalService(_unitOfWork);
                var listRpta3 = reporteCronograma3.GenerarReportePendientePorCoordinadoraOperaciones(respuestaGeneral);
                var agrupado3 = (from p in listRpta3
                                 group p by p.Periodo into grupo
                                 select new ReportePendienteDTO { g = grupo.Key, l = grupo.ToList() }).ToList();

                CronogramaPagoDetalleFinalService reporteCronograma4 = new CronogramaPagoDetalleFinalService(_unitOfWork);
                var listRpta4 = reporteCronograma4.GenerarReportePendientePeriodoYCoordinadorOperaciones(respuestaGeneral);
                var agrupado4 = (from p in listRpta4
                                 group p by p.Periodo into grupo
                                 select new ReportePendientePorCoordinadorDTO { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReportePendienteCompuestoDTO reporte = new ReportePendienteCompuestoDTO();
                reporte.ReportePendientePorPeriodo = agrupado.ToList();
                reporte.ReportePendienteIngresoVentasPorPeriodo = agrupado2.ToList();
                reporte.ReportePendientePorCoordinador = agrupado3.ToList();
                reporte.ReportePendientePeriodoYCoordinador = agrupado4.ToList();

                return reporte;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera Reporte detallado.
        /// </summary>
        /// <param name="filtroPendiente"></param>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        public ReportePendienteCompuestoDTO GenerarReporteDetalles(ReportePendienteFiltroDTO filtroPendiente)
        {
            try
            {
                PersonalService personalService = new PersonalService(_unitOfWork);

                if (filtroPendiente.Modalidad.Count() == 0)
                {
                    filtroPendiente.Modalidad = _unitOfWork.ModalidadCursoRepository.ObtenerCombo().Select(w => w.Id).ToList();
                }

                ReporteService reporteService = new ReporteService(_unitOfWork);
                var respuestaGeneral = reporteService.ObtenerReportePendienteDetalles(filtroPendiente);
                ReportePendienteCompuestoDTO reporte = new ReportePendienteCompuestoDTO();
                List<PersonalAsignadoReportePendienteDTO> coordinadoras = new List<PersonalAsignadoReportePendienteDTO>();
                foreach (var personal in filtroPendiente.Coordinadora)
                {
                    if (personal != "0")
                    {
                        var ResultadoCoordinadora = personalService.ObtenerDatosUsuariosReportePendiente(personal);
                        coordinadoras.Add(ResultadoCoordinadora);
                    }
                }

                reporte.ListaCoordinadoras = coordinadoras;
                reporte.ReportePendienteDetalles = respuestaGeneral.ToList();
                reporte.EstadoPersonal = filtroPendiente.EstadoPersonal;

                return reporte;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
