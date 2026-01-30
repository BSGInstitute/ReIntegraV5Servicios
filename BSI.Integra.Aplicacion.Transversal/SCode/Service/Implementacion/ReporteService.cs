using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using ClosedXML.Excel;
using System.Text.RegularExpressions;


namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ReportesService
    /// Autor: Gilmer Quispe.
    /// Fecha: 22/09/2022
    /// <summary>
    /// Gestión general de ReportesfObtenerCentroCostoPorAsesorDetalles
    /// </summary>
    public class ReporteService : IReporteService
    {
        private IUnitOfWork _unitOfWork;

        public ReporteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Calidad Consultando a "Réplica"
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteCalidadCambioDeFaseDTO </returns>
        public ReporteCalidadCambioDeFaseDTO ReporteCalidadCambioDeFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var nuevoFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                var nuevoFiltroProcedimiento = new ReporteCambioFaseFiltroProcedimientoDTO();
                var reporteCalidadCambioDeFaseDataDTO = new ReporteCalidadCambioDeFaseDTO();

                filtros.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtros.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                reporteCalidadCambioDeFaseDataDTO.ReporteCalidadProcesamiento = ReporteCalidadProcesamientoV2(filtros);
                reporteCalidadCambioDeFaseDataDTO.DiferenciaLlamadasBloque = ReporteDiferenciaLlamadasBloqueV2(filtros);
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Calidad Consultando a "Réplica"
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteCalidadCambioDeFaseDTO </returns>
        public ReporteCalidadCambioDeFaseAlternoDTO ReporteCalidadCambioDeFaseAlterno(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var nuevoFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                var nuevoFiltroProcedimiento = new ReporteCambioFaseFiltroProcedimientoDTO();
                var reporteCalidadCambioDeFaseDataDTO = new ReporteCalidadCambioDeFaseAlternoDTO();

                filtros.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtros.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                reporteCalidadCambioDeFaseDataDTO.ReporteCalidadProcesamiento = _unitOfWork.ReportesRepository.ObtenerReporteCalidadProcesamientoAlterno(filtros);
                reporteCalidadCambioDeFaseDataDTO.DiferenciaLlamadasBloque = ReporteDiferenciaLlamadasBloque(filtros);
                return reporteCalidadCambioDeFaseDataDTO;
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
        /// Obtiene el reporte de calidad procesamiento Version 2
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns> Lista de objeto DTO : List<ReporteCalidadProcesamientoDTO> </returns>
        public List<ReporteCalidadProcesamientoDTO> ReporteCalidadProcesamientoV2(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var resultado = _unitOfWork.ReportesRepository.ObtenerReporteCalidadProcesamientoV2(filtros);
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de Diferencia de Llamadas por Bloque
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objeto DTO : List<DiferenciaLlamadasBloqueDTO> </returns>
        public List<DiferenciaLlamadasBloqueDTO> ReporteDiferenciaLlamadasBloqueV2(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var resultado = _unitOfWork.ReportesRepository.ObtenerReporteDiferenciaLlamadasBloque(filtros);
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de Diferencia de Llamadas por Bloque
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objeto DTO : List<DiferenciaLlamadasBloqueDTO> </returns>
        public List<DiferenciaLlamadasBloqueDTO> ReporteDiferenciaLlamadasBloque(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var resultado = _unitOfWork.ContadorBicLogRepository.ObtenerReporteContadorBicLog(filtros)!;
                var agrupado = resultado.GroupBy(x => x.IdOportunidad).Select(s => new
                {
                    IdOportunidad = s.Key,
                    Detalle = s.Select(n => new
                    {
                        n.EstadoContactoManhana,
                        n.EstadoContactoTarde,
                        n.FechaLogContacto
                    }).ToList()
                }).ToList();
                int total0dias = 0;
                int total1dias = 0;
                int total2dias = 0;
                //int total3dias = 0;
                //int total4dias = 0;
                //int total5dias = 0;
                int totalMasDe2dias = 0;
                int total = 0;
                foreach (var item in agrupado)
                {
                    var ordenado = item.Detalle.OrderBy(x => x.FechaLogContacto).ToList();
                    if (ordenado.Count() == 1)
                    {
                        var elemento = ordenado.ElementAt(0);
                        if (elemento.EstadoContactoManhana == true && elemento.EstadoContactoTarde == true)
                        {
                            total0dias++;
                        }
                        else
                        {
                            total1dias++;
                        }
                    }
                    else if (ordenado.Count() > 1)
                    {
                        var primerContacto = ordenado.FirstOrDefault()!.FechaLogContacto.Date;
                        var ultimoContacto = ordenado.LastOrDefault()!.FechaLogContacto.Date;
                        var contadorDiasSinContacto = 0;
                        for (DateTime fecha = primerContacto; fecha <= ultimoContacto; fecha = fecha.AddDays(1))
                        {
                            if (fecha.DayOfWeek == DayOfWeek.Sunday)
                            {
                                break;
                            }
                            var contacto = ordenado.FirstOrDefault(x => x.FechaLogContacto.Date == fecha.Date);
                            if (contacto == null)
                            {
                                contadorDiasSinContacto++;
                            }
                            else if (contacto.EstadoContactoManhana == false || contacto.EstadoContactoTarde == false)
                            {
                                if (fecha.DayOfWeek == DayOfWeek.Saturday && contacto.EstadoContactoManhana == false && contacto.EstadoContactoTarde == false)
                                {
                                    contadorDiasSinContacto++;
                                }
                                else
                                {
                                    contadorDiasSinContacto++;
                                }
                            }
                        }
                        switch (contadorDiasSinContacto)
                        {
                            case 0:
                                total0dias++;
                                break;
                            case 1:
                                total1dias++;
                                break;
                            case 2:
                                total2dias++;
                                break;
                            //case 3:
                            //    total3dias++;
                            //    break;
                            //case 4:
                            //    total4dias++;
                            //    break;
                            //case 5:
                            //    total5dias++;
                            //    break;
                            default:
                                totalMasDe2dias++;
                                break;
                        }
                    }
                }
                var rpta = new List<DiferenciaLlamadasBloqueDTO>(){
                    new DiferenciaLlamadasBloqueDTO()
                    {
                        Descripcion = "0 días",
                        Cantidad = total0dias
                    },
                    new DiferenciaLlamadasBloqueDTO()
                    {
                        Descripcion = "1 día",
                        Cantidad = total1dias
                    },
                    new DiferenciaLlamadasBloqueDTO()
                    {
                        Descripcion = "2 días",
                        Cantidad = total2dias
                    },
                    //new DiferenciaLlamadasBloqueDTO()
                    //{
                    //    Descripcion = "3 días",
                    //    Cantidad = total3dias
                    //},
                    //new DiferenciaLlamadasBloqueDTO()
                    //{
                    //    Descripcion = "4 días",
                    //    Cantidad = total4dias
                    //},
                    //new DiferenciaLlamadasBloqueDTO()
                    //{
                    //    Descripcion = "5 días",
                    //    Cantidad = total5dias
                    //},
                    new DiferenciaLlamadasBloqueDTO()
                    {
                        Descripcion = "Mas de 2 días",
                        Cantidad = totalMasDe2dias
                    },
                    new DiferenciaLlamadasBloqueDTO()
                    {
                        Descripcion = "Total",
                        Cantidad = totalMasDe2dias + total0dias + total1dias + total2dias
                    },
                };
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos de asesores para Módulo de Reporte de Contactabilidad para Operaciones
        /// </summary>
        /// <param name="idPersonal"> Id del personal </param>
        /// <returns> objeto DTO : ReporteContactabilidadCombosDTO </returns>
        public ReporteContactabilidadCombosDTO ObtenerCombosReporteOperaciones(int idPersonal)
        {
            try
            {
                var resultado = new ReporteContactabilidadCombosDTO();
                var asistentes = _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoOperacionesTotal(idPersonal);
                //activos
                resultado.AsistentesActivos = asistentes.Where(w => w.Activo == true).ToList();
                //todos
                resultado.AsistentesTotales = asistentes;
                //inactivo
                resultado.AsistentesInactivos = asistentes.Where(w => w.Activo == false).ToList();

                return (resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Generar Reporte de contactabilidad para Comercial
        /// </summary>
        /// <param name="filtros"> Filtros de busqueda </param>
        /// <returns> objeto : ReporteContactabilidadDataV2DTO</returns>
        public ReporteContactabilidadDataV2DTO ReporteContactabilidadV2(ReporteContactabilidadFiltroAlternoDTO filtros)
        {
            try
            {
                var filtroOrdenado = new ReporteContactabilidadFiltroFinalDTO();
                var resultado = new ReporteContactabilidadDataV2DTO();
                var asesoresFinal = new List<int>();
                var data = new List<ReporteContactabilidadDTO>();
                var data2 = new List<ReporteContactabilidadAgrupadoDTO>();
                if (filtros.Asesores.Count() > 0)
                {
                    filtroOrdenado.Asesores = string.Join(",", filtros.Asesores);
                }
                filtroOrdenado.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtroOrdenado.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                filtroOrdenado.Tipo = filtros.Tipo;
                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);

                if (DateTime.Compare(filtroOrdenado.FechaInicio, fechaActual) == 0)
                {
                    data = _unitOfWork.ReportesRepository.ObtenerReporteContactabilidadV2(filtroOrdenado);
                }
                else
                {
                    data = _unitOfWork.ReportesRepository.ObtenerReporteContactabilidadCongelado(filtroOrdenado);
                }
                var data3 = _unitOfWork.ReportesRepository.ObtenerReporteContactabilidadMinutos(filtroOrdenado);

                resultado.AsesorContactabilidad = data;
                resultado.ComparativoAsesor = data2;
                resultado.MinutosContactabilidad = data3;

                return resultado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Generar Reporte de contactabilidad para Comercial
        /// </summary>
        /// <param name="filtros"> Filtros de busqueda </param>
        /// <returns> objeto : ReporteContactabilidadDataV2DTO</returns>
        public ReporteContactabilidadAlternoDTO ReporteContactabilidadV2TresCx(ReporteContactabilidadFiltroAlternoDTO filtros)
        {
            try
            {
                var filtroOrdenado = new ReporteContactabilidadFiltroFinalDTO();
                var resultado = new ReporteContactabilidadAlternoDTO();
                var asesoresFinal = new List<int>();
                var data = new List<ReporteContactabilidadDTO>();
                var data2 = new List<ReporteContactabilidadAgrupadoDTO>();

                if (filtros.Asesores.Count() > 0)
                {
                    filtroOrdenado.Asesores = string.Join(",", filtros.Asesores);
                }

                filtroOrdenado.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtroOrdenado.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                filtroOrdenado.Tipo = filtros.Tipo;
                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);

                if (DateTime.Compare(filtroOrdenado.FechaInicio, fechaActual) == 0)
                {
                    data = _unitOfWork.ReportesRepository.ObtenerReporteContactabilidadV2TresCx(filtroOrdenado);
                }
                else
                {
                    data = _unitOfWork.ReportesRepository.ObtenerReporteContactabilidadCongeladoTresCx(filtroOrdenado);
                }

                //var data3 = _unitOfWork.ReportesRepository.ObtenerReporteContactabilidadMinutosTresCx(filtroOrdenado);

                resultado.AsesorContactabilidad = data;
                //resultado.ComparativoAsesor = data2;
                //resultado.MinutosContactabilidad = data3;

                return resultado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 14/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Generar Reporte de contactabilidad para Comercial 3cx alterno
        /// </summary>
        /// <param name="filtros"> Filtros de busqueda </param>
        /// <returns> objeto : ReporteContactabilidadDataV2DTO</returns>
        public List<ReporteContactabilidad3cxAlternoDTO> ReporteContactabilidadV2TresCxAlterno(ReporteContactabilidadFiltroAlternoDTO filtros)
        {
            try
            {
                var filtroOrdenado = new ReporteContactabilidadFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                    filtroOrdenado.Asesores = string.Join(",", filtros.Asesores);
                filtroOrdenado.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtroOrdenado.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                filtroOrdenado.Tipo = filtros.Tipo;
                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);
                var esHoy = (DateTime.Compare(filtroOrdenado.FechaInicio, fechaActual) == 0);
                var data = _unitOfWork.ReportesRepository.ObtenerReporteContactabilidadV2TresCxAlterno(filtroOrdenado, esHoy);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Generar Reporte de contactabilidad Desagregado (Deprecated)
        /// </summary>
        /// <param name="filtros"> Filtros de busqueda </param>
        /// <returns> objeto : ReporteContactabilidadDataV2DTO</returns>
        public ReporteContactabilidadDataV2DTO ReporteContactabilidadDesagregado(ReporteContactabilidadFiltroDTO filtros)
        {
            try
            {
                var filtroOrdenado = new ReporteContactabilidadFiltroFinalDTO();
                var resultado = new ReporteContactabilidadDataV2DTO();
                var data2 = new List<ReporteContactabilidadAgrupadoDTO>();
                var asesoresFinal = new List<int>();

                filtros.FechaFin = filtros.FechaFin.AddHours(-5);
                filtros.FechaInicio = filtros.FechaInicio.AddHours(-5);
                if (filtros.Asesores.Count() > 0)
                {
                    filtroOrdenado.Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.AsesoresComparativos.Count() > 0)
                {
                    filtroOrdenado.AsesoresComparativo = String.Join(",", filtros.AsesoresComparativos);
                }
                filtroOrdenado.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtroOrdenado.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                filtroOrdenado.Tipo = filtros.Tipo;

                var data = _unitOfWork.ReportesRepository.ObtenerReporteContactabilidadDesagregado(filtroOrdenado);
                if (filtros.AsesoresComparativos.Count() > 0)
                {
                    var resultado2 = _unitOfWork.ReportesRepository.ObtenerReporteContactabilidadAsesorComparativoV2(filtroOrdenado);
                    data2 = (from p in resultado2
                             group p by new
                             {
                                 p.IdAsesor,
                             } into g
                             select new ReporteContactabilidadAgrupadoDTO
                             {
                                 IdAsesor = g.Key.IdAsesor,
                                 Lista = g.Select(o => new ReporteContactabilidadAsesorIndicadoresDTO
                                 {
                                     IdAsesor = o.IdAsesor,
                                     Hora = o.Hora,
                                     Clave = o.Clave,
                                     Valor = o.Valor,
                                     Tipo = o.Tipo,
                                     TotalLlamadas = o.TotalLlamadas,
                                 }).ToList()
                             }).ToList();
                }
                resultado.AsesorContactabilidad = data;
                resultado.ComparativoAsesor = data2;

                return resultado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene le reporte de seguimiento oportunidad por filtros
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns>List<ReporteSeguimientoOportunidadesDTO> </returns>
        public List<ReporteSeguimientoOportunidadDTO> ReporteSeguimientoOportunidad(ReporteSeguimientoOportunidadesFiltrosDTO filtros)
        {
            try
            {
                var filtroOrdenado = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    filtroOrdenado.Asesores = string.Join(",", filtros.Asesores);
                }
                if (filtros.FasesOportunidad.Count() > 0)
                {
                    filtroOrdenado.FasesOportunidad = string.Join(",", filtros.FasesOportunidad);
                }
                if (filtros.FaseOportunidadOrigen.Count() > 0)
                {
                    filtroOrdenado.FasesOportunidadOrigen = string.Join(",", filtros.FaseOportunidadOrigen);
                }
                if (filtros.FaseOportunidadDestino.Count() > 0)
                {
                    filtroOrdenado.FasesOportunidadDestino = string.Join(",", filtros.FaseOportunidadDestino);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtroOrdenado.CentroCostos = string.Join(",", filtros.CentroCostos);
                }
                filtroOrdenado.OpcionFase = filtros.OpcionFase;
                filtroOrdenado.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtroOrdenado.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var data = _unitOfWork.ReportesRepository.ObtenerReporteSeguimiento(filtroOrdenado);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte solicitudes de visualizacion
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> List<ReporteSeguimientoOportunidadDTO> </returns>
        public List<ReporteSeguimientoOportunidadDTO> GenerarReporteSolicitudesVisualizacion(ReporteSolicitudesVisualizacionFiltroDTO filtros)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                var filtroOrdenado = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    filtroOrdenado.Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.FasesOportunidad.Count() > 0)
                {
                    filtroOrdenado.FasesOportunidad = String.Join(",", filtros.FasesOportunidad);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtroOrdenado.CentroCostos = String.Join(",", filtros.CentroCostos);
                }
                var data = _unitOfWork.ReportesRepository.ObtenerReporteSolicitudesVisualizacion(filtroOrdenado);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte solicitudes de visualizacion de operaciones
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> List<ReporteSeguimientoOportunidadDTO> </returns>
        public List<ReporteSeguimientoOportunidadDTO> GenerarReporteSolicitudesVisualizacionOperaciones(ReporteSolicitudesVisualizacionFiltroDTO filtros)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                var filtroOrdenado = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    filtroOrdenado.Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.FasesOportunidad.Count() > 0)
                {
                    filtroOrdenado.FasesOportunidad = String.Join(",", filtros.FasesOportunidad);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtroOrdenado.CentroCostos = String.Join(",", filtros.CentroCostos);
                }
                var data = _unitOfWork.ReportesRepository.ObtenerReporteSolicitudesVisualizacionOperaciones(filtroOrdenado);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Aprueba la solicitud de informacion
        /// </summary>
        /// <param name="aprobacionFiltro"> filtro de aprobación </param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO AprobacionSolicitudVisualizacion(AprobacionSolicitudesVisualizacionFiltroDTO aprobacionFiltro)
        {
            try
            {
                var resultado = _unitOfWork.ReportesRepository.ObtenerAprobacionSolicitudVisualizacion(aprobacionFiltro);
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/09/2022
        /// <summary>
        /// Obtiene html con valores de estilo según tiempo
        /// </summary>
        /// <param name="minutosReal"></param>
        /// <returns> Html de configuración de estilo: String </returns>
        private string ClasificarColorLlamadaRealizadasActividadesMinutos(double minutosReal)
        {
            string stiloFinal = String.Empty;
            string colorRow = String.Empty;
            string colorTexto = String.Empty;
            if (minutosReal < 2) { colorRow = "blue"; colorTexto = "white"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal >= 2 && minutosReal < 3) { colorRow = "skyblue"; colorTexto = "black"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal >= 3 && minutosReal < 5) { colorRow = "yellow"; colorTexto = "black"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal >= 5 && minutosReal <= 8) { colorRow = "orange"; colorTexto = "black"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal > 8) { colorRow = "red"; colorTexto = "white"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            return stiloFinal;
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 03/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Actividades Realizadas Operaciones
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda </param>
        /// <returns> Información de Actividades Realizadas: List<ProcesadoDataActividadesRealizadasOperacionesDTO> <returns>
        public List<ProcesadoDataActividadesRealizadasDTO> ReporteActividadesRealizadasOperaciones(ReporteActividadesRealizadasFiltrosDTO filtros)
        {
            try
            {
                filtros.Fecha = filtros.Fecha.AddHours(-5);
                DateTime fechaInicio = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, 0, 0, 0);
                DateTime fechaFin = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, 23, 59, 59);
                if (filtros.EstadoFiltroHora)
                {
                    fechaInicio = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, filtros.HoraInicio, filtros.MinutosInicio, 0);
                    fechaFin = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, filtros.HoraFin, filtros.MinutosFin, 0);

                }
                var data = _unitOfWork.ReportesRepository.ObtenerReporteActividadesRealizadasOperaciones(filtros, fechaInicio, fechaFin);

                var result = (from p in data
                              group p by new
                              {
                                  p.IdActividad,
                                  p.NombreCentroCosto,
                                  p.NombreCompletoContacto,
                                  p.CodigoFaseFinal,
                                  p.NombreTipoDato,
                                  p.NombreOrigen,
                                  p.FechaProgramada,
                                  p.FechaReal,
                                  p.NombreActividadCabecera,
                                  p.NombreOcurrencia,
                                  p.OcurrenciaPadre,
                                  p.ComentarioActividad,
                                  p.NombreCompletoAsesor,
                                  p.IdAlumno,
                                  p.IdOportunidad,
                                  p.ProbabilidadActual,
                                  p.CodigoFaseOrigen,
                                  p.IdFaseOportunidadInicial,
                                  p.FechaModificacion,
                                  p.NombreCategoriaOrigen,
                                  p.EstadoOcurrencia,
                                  p.NombreGrupo,
                              } into g
                              select new CompuestoActividadesRealizadasDTO
                              {
                                  IdActividad = g.Key.IdActividad,
                                  NombreCentroCosto = g.Key.NombreCentroCosto,
                                  NombreCompletoContacto = g.Key.NombreCompletoContacto,
                                  CodigoFaseFinal = g.Key.CodigoFaseFinal,
                                  NombreTipoDato = g.Key.NombreTipoDato,
                                  NombreOrigen = g.Key.NombreOrigen,
                                  FechaProgramada = g.Key.FechaProgramada,
                                  FechaReal = g.Key.FechaReal,
                                  NombreActividadCabecera = g.Key.NombreActividadCabecera,
                                  NombreOcurrencia = g.Key.NombreOcurrencia,
                                  OcurrenciaPadre = g.Key.OcurrenciaPadre,
                                  ComentarioActividad = g.Key.ComentarioActividad,
                                  NombreCompletoAsesor = g.Key.NombreCompletoAsesor,
                                  IdAlumno = g.Key.IdAlumno,
                                  IdOportunidad = g.Key.IdOportunidad,
                                  ProbabilidadActual = g.Key.ProbabilidadActual,
                                  CodigoFaseOrigen = g.Key.CodigoFaseOrigen,
                                  IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                                  FechaModificacion = g.Key.FechaModificacion,
                                  NombreCategoriaOrigen = g.Key.NombreCategoriaOrigen,
                                  EstadoOcurrencia = g.Key.EstadoOcurrencia,
                                  NombreGrupo = g.Key.NombreGrupo,

                                  LlamadasIntegra = g.Select(o => new InformacionLlamadaDTO
                                  {
                                      Id = o.IdLlamadaWebphone,
                                      DuracionTimbrado = o.DuracionTimbrado,
                                      DuracionContesto = o.DuracionContesto,
                                      FechaInicioLlamada = o.FechaInicioLlamada,
                                      FechaFinLlamada = o.FechaFinLlamada,
                                      EstadoLlamada = null,
                                      SubEstadoLlamada = null,
                                      NombreGrabacion = o.NombreGrabacionIntegra,
                                      Webphone = o.Webphone,
                                      OrigenLlamada = o.OrigenLlamada 

                                  }).OrderByDescending(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                  LlamadasCentral = g.Select(o => new InformacionLlamadaDTO
                                  {
                                      Id = o.IdTresCX,
                                      DuracionTimbrado = o.DuracionTimbradoTresCx,
                                      DuracionContesto = o.DuracionContestoTresCx,
                                      FechaInicioLlamada = o.FechaInicioLlamadaTresCX,
                                      FechaFinLlamada = o.FechaFinLlamadaTresCX,
                                      EstadoLlamada = o.EstadoLlamadaTresCX,
                                      SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                      NombreGrabacion = o.NombreGrabacionTresCX,

                                  }).OrderBy(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()
                              }).OrderBy(x => x.FechaReal);

                List<ProcesadoDataActividadesRealizadasDTO> final = new List<ProcesadoDataActividadesRealizadasDTO>();

                //Variables Temporales ------------
                var flag = false;
                var count = 0;
                double minutos = 0;
                double totalContesto = 0;
                double totalTimbrado = 0;
                double totalPerdido = 0;
                double mayorPerdido = 0;
                double minutosTotalLlamada = 0;
                DateTime fechaUltimaLlamadaTemp = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, 00, 00, 00);
                DateTime fechaTemp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);

                int diaFecha = filtros.Fecha.Day;
                //Fin Variables Temporales --------

                foreach (var item in result)
                {
                    ProcesadoDataActividadesRealizadasDTO itemDetalle = new ProcesadoDataActividadesRealizadasDTO()
                    {
                        IdActividad = item.IdActividad,
                        NombreCentroCosto = item.NombreCentroCosto,
                        NombreCompletoContacto = item.NombreCompletoContacto,
                        CodigoFaseFinal = item.CodigoFaseFinal,
                        NombreTipoDato = item.NombreTipoDato,
                        NombreOrigen = item.NombreOrigen,
                        FechaProgramada = item.FechaProgramada,
                        FechaReal = item.FechaReal,
                        NombreActividadCabecera = item.NombreActividadCabecera,
                        NombreOcurrencia = item.NombreOcurrencia,
                        OcurrenciaPadre = item.OcurrenciaPadre,
                        ComentarioActividad = item.ComentarioActividad,
                        NombreCompletoAsesor = item.NombreCompletoAsesor,
                        IdAlumno = item.IdAlumno,
                        IdOportunidad = item.IdOportunidad,
                        ProbabilidadActual = item.ProbabilidadActual,
                        CodigoFaseOrigen = item.CodigoFaseOrigen,
                        NombreCategoriaOrigen = item.NombreCategoriaOrigen,
                        EstadoOcurrencia = item.EstadoOcurrencia,
                        NombreGrupo = item.NombreGrupo,
                    };

                    if (item.LlamadasIntegra.Count() > 0)
                    {
                        var ordenLlamadas = item.LlamadasIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();
                        var fechaUltima = ordenLlamadas.Select(s => s.FechaInicioLlamada).FirstOrDefault();
                        var primeraLlamda = item.LlamadasIntegra.FirstOrDefault();
                        var ultimaLlamada = item.LlamadasIntegra.LastOrDefault()!;

                        if (count > 0 && flag)
                        {
                            if (diaFecha == fechaTemp.Day)
                            {
                                var min = ((fechaUltima.Value - fechaTemp).TotalSeconds / 60).ToString("0.0");
                                minutos = Convert.ToDouble(min);
                            }
                            else
                            {
                                minutos = 0;
                            }
                        }
                        if (fechaUltima != null)
                        {
                            flag = true;
                            fechaTemp = item.LlamadasIntegra.Select(x => x.FechaInicioLlamada).FirstOrDefault().Value;
                            double contesto = Convert.ToDouble(primeraLlamda.DuracionContesto);
                            double timbrado = Convert.ToDouble(primeraLlamda.DuracionTimbrado);
                            fechaTemp = fechaTemp.AddSeconds(contesto + timbrado);
                        }

                        fechaUltimaLlamadaTemp = ultimaLlamada.FechaFinLlamada!.Value;
                        totalTimbrado += ((item.LlamadasIntegra.Select(s => Convert.ToDouble(s.DuracionTimbrado))).Sum());
                        totalContesto += (item.LlamadasIntegra.Select(s => Convert.ToDouble(s.DuracionContesto))).Sum();

                        if (minutos >= 0)
                        {
                            totalPerdido += minutos;
                        }
                        item.LlamadasIntegra = item.LlamadasIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();

                        var htmlGrabacionesCentral = "";
                        var htmlGrabacionesIntegra = "";

                        foreach (var llamada in item.LlamadasCentral)
                        {
                            if (llamada.NombreGrabacion != null)
                            {
                                htmlGrabacionesCentral = htmlGrabacionesCentral + "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamada3CX('" + llamada.NombreGrabacion + "')\"><b>Escuchar</b></a>";
                            }
                            htmlGrabacionesCentral = htmlGrabacionesCentral + "</br>";
                        }
                        var primeraFechaFin = DateTime.Now;
                        int ContadorLlamadas = 0;
                        double minutosLlamada = 0;

                        foreach (var llamada in item.LlamadasIntegra)
                        {
                            if (llamada.NombreGrabacion != null)
                            {
                                itemDetalle.NombreGrabacionIntegra.Add(new NombreGrabacionDTO() {  NombreGrabacion = llamada.NombreGrabacion, OrigenLlamada = llamada.OrigenLlamada, Webphone = llamada.Webphone });
                            }
                            llamada.MinutosPerdidos = "";
                            if (ContadorLlamadas > 0)
                            {

                                var min = ((llamada.FechaInicioLlamada.Value - primeraFechaFin).TotalSeconds / 60);
                                if (min < 0) min = 0;
                                var minTexto = min.ToString("0.0");
                                minutosLlamada = Convert.ToDouble(min);
                                string estilo = ClasificarColorLlamadaRealizadasActividadesMinutos(minutosLlamada);
                                llamada.MinutosPerdidos = "<div id = 'RowIntervalLlamada'" + estilo + ">" + minTexto + " min.</div>";
                            }
                            primeraFechaFin = llamada.FechaFinLlamada.Value;
                            minutosTotalLlamada += minutosLlamada;
                            ContadorLlamadas++;
                        }

                        foreach (var tiempo in item.LlamadasIntegra)
                        {
                            if (tiempo.OrigenLlamada.Contains("3cx"))
                            {
                                itemDetalle.TiemposDuracionLlamadas = itemDetalle.TiemposDuracionLlamadas + "<div style='text-decoration: underline;color: blue !important;font-weight: bold;'> <strong >TT: </strong >" + ((double)(tiempo.DuracionTimbrado == null ? 0 : tiempo.DuracionTimbrado) / 60).ToString("0.0") + " min.<strong > TC:</strong > " + ((double)(tiempo.DuracionContesto == null ? 0 : tiempo.DuracionContesto) / 60).ToString("0.0") + " min.</div> ";
                            }
                            else if (tiempo.OrigenLlamada.Contains("Ringover"))
                            {
                                itemDetalle.TiemposDuracionLlamadas = itemDetalle.TiemposDuracionLlamadas + "<div style='text-decoration: underline;color: #07ad9c !important;font-weight: bold;'> <strong >TT: </strong >" + ((double)(tiempo.DuracionTimbrado == null ? 0 : tiempo.DuracionTimbrado) / 60).ToString("0.0") + " min.<strong > TC:</strong > " + ((double)(tiempo.DuracionContesto == null ? 0 : tiempo.DuracionContesto) / 60).ToString("0.0") + " min.</div>";
                            }
                            else if (tiempo.OrigenLlamada.Contains("Wavix"))
                            {
                                itemDetalle.TiemposDuracionLlamadas = itemDetalle.TiemposDuracionLlamadas + "<div style='text-decoration: underline;color: brown !important;font-weight: bold;'> <strong >TT: </strong >" + ((double)(tiempo.DuracionTimbrado == null ? 0 : tiempo.DuracionTimbrado) / 60).ToString("0.0") + " min.<strong > TC:</strong > " + ((double)(tiempo.DuracionContesto == null ? 0 : tiempo.DuracionContesto) / 60).ToString("0.0") + " min.</div>";
                            }
                            else if (tiempo.OrigenLlamada.Contains("Corporativo"))
                            {
                                itemDetalle.TiemposDuracionLlamadas = itemDetalle.TiemposDuracionLlamadas + "<div style='text-decoration: underline;color: #ff6113 !important;font-weight: bold;'> <strong >TT: </strong >" + ((double)(tiempo.DuracionTimbrado == null ? 0 : tiempo.DuracionTimbrado) / 60).ToString("0.0") + " min.<strong > TC:</strong > " + ((double)(tiempo.DuracionContesto == null ? 0 : tiempo.DuracionContesto) / 60).ToString("0.0") + " min.</div>";
                            }
                            else if (tiempo.OrigenLlamada.Contains("Integra"))
                            {
                                itemDetalle.TiemposDuracionLlamadas = itemDetalle.TiemposDuracionLlamadas + "<div style='text-decoration: underline;color: #212121 !important;'> <strong >TT: </strong >" + ((double)(tiempo.DuracionTimbrado == null ? 0 : tiempo.DuracionTimbrado) / 60).ToString("0.0") + " min.<strong > TC:</strong > " + ((double)(tiempo.DuracionContesto == null ? 0 : tiempo.DuracionContesto) / 60).ToString("0.0") + " min.</div>";
                            }
                            else if (tiempo.OrigenLlamada.Contains("Wolkbox"))
                            {
                                itemDetalle.TiemposDuracionLlamadas = itemDetalle.TiemposDuracionLlamadas + "<div style='text-decoration: underline;color: #9a0f3d !important;font-weight: bold;'> <strong >TT: </strong >" + ((double)(tiempo.DuracionTimbrado == null ? 0 : tiempo.DuracionTimbrado) / 60).ToString("0.0") + " min.<strong > TC:</strong > " + ((double)(tiempo.DuracionContesto == null ? 0 : tiempo.DuracionContesto) / 60).ToString("0.0") + " min.</div>";
                            }
                            else
                            {
                                itemDetalle.TiemposDuracionLlamadas = itemDetalle.TiemposDuracionLlamadas + "<strong >TT: </strong >" + ((double)(tiempo.DuracionTimbrado == null ? 0 : tiempo.DuracionTimbrado) / 60).ToString("0.0") + " min.<strong > TC:</strong > " + ((double)(tiempo.DuracionContesto == null ? 0 : tiempo.DuracionContesto) / 60).ToString("0.0") + " min.";
                            }
                            
                        }

                        //itemDetalle.TiemposDuracionLlamadas = String.Concat(item.LlamadasIntegra.Select(o => " <strong >TT: </strong >" + ((double)(o.DuracionTimbrado == null ? 0 : o.DuracionTimbrado) / 60).ToString("0.0") + " min.<strong > TC:</strong > " + ((double)(o.DuracionContesto == null ? 0 : o.DuracionContesto) / 60).ToString("0.0") + " min.<br />"));
                        itemDetalle.FechaLlamada = String.Concat(item.LlamadasIntegra.Select(o => o.MinutosPerdidos + "<strong >I: </strong >" + o.FechaInicioLlamada.Value.ToString("HH:mm:ss") + "<strong> T: </strong >" + o.FechaFinLlamada.Value.ToString("HH:mm:ss") + "<br />"));
                        //itemDetalle.NombreGrabacionTresCX = htmlGrabacionesCentral;
                        //itemDetalle.NombreGrabacionIntegra = htmlGrabacionesIntegra;
                    }
                    else
                    {
                        if(count > 0 && flag)
                        {
                            var min = ((itemDetalle.FechaReal - fechaUltimaLlamadaTemp).TotalSeconds / 60).ToString("0.0");
                            minutos = Convert.ToDouble(min);
                        }

                        
                        if (minutos >= 0)
                            totalPerdido += minutos;

                        flag = true;
                        fechaUltimaLlamadaTemp = itemDetalle.FechaReal;
                        fechaTemp = itemDetalle.FechaReal;
                    }

                    itemDetalle.MinutosTotalIntervaleLlamadas = minutosTotalLlamada;
                    itemDetalle.MinutosIntervale = minutos;
                    itemDetalle.MinutosTotalContesto = totalContesto;
                    itemDetalle.MinutosTotalTimbrado = totalTimbrado;
                    itemDetalle.MinutosTotalPerdido = totalPerdido;
                    count++;
                    mayorPerdido = mayorPerdido > minutos ? mayorPerdido : minutos;
                    itemDetalle.MayorTiempo = mayorPerdido;
                    itemDetalle.TiemposTresCX = String.Concat(item.LlamadasCentral.Select(o => " <strong >TT: </strong >" + ((double)(o.DuracionTimbrado == null ? 0 : o.DuracionTimbrado) / 60).ToString("0.0") + " min. <strong >TC:</strong > " + ((double)(o.DuracionContesto == null ? 0 : o.DuracionContesto) / 60).ToString("0.0") + " min. <br />"));
                    itemDetalle.EstadosTresCX = String.Concat(item.LlamadasCentral.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
                    itemDetalle.ExisteLlamadaExitosa = itemDetalle.EstadosTresCX.Contains("Llamada Exitosa");
                    itemDetalle.TotalEjecutadas = 0;
                    itemDetalle.TotalNoEjecutadas = 0;
                    itemDetalle.TotalAsignacionManual = 0;
                    itemDetalle.TiemposDuracionLlamadas = itemDetalle.TiemposDuracionLlamadas == null ? "vacio" : itemDetalle.TiemposDuracionLlamadas;
                    itemDetalle.FechaLlamada = itemDetalle.FechaLlamada == null ? "vacio" : itemDetalle.FechaLlamada;

                    final.Add(itemDetalle);
                }
                if (filtros.EstadoLlamada != null)
                {
                    if (filtros.EstadoLlamada == 1)
                    {
                        final = final.Where(x => x.ExisteLlamadaExitosa == true).ToList();
                    }
                    else
                    {
                        final = final.Where(x => x.ExisteLlamadaExitosa == false).ToList();
                    }
                }
                var dataFinal = final.OrderByDescending(s => s.FechaReal).ToList();
                return dataFinal;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Tasa de Conversión Consolidado por Asesor según periodo Mensual
        /// </summary>
        /// <param name="filtro"> filtro de reporte </param>
        /// <returns> ReporteTasaConversionConsolidadaMensualGraficasVistaDTO <returns>
        public ReporteTasaConversionConsolidadaMensualGraficasVistaDTO ReporteTasaConversionConsolidadoAsesoresGraficaMensual(ReporteTasaConversionConsolidadaGraficaFiltroDTO filtro)
        {
            string coordinadores = ListIntToString(filtro.Coordinadores);
            string asesores = ListIntToString(filtro.Asesores);
            string periodoInicio = filtro.PeriodoInicio;
            string periodoFin = filtro.PeriodoFin;
            try
            {
                var listRpta = _unitOfWork.ReportesRepository.ReporteTasaConversionConsolidadoAsesoresGraficasMensual(coordinadores, asesores, periodoInicio, periodoFin);
                return listRpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Tasa de Conversión Consolidado por Asesor según periodo Mensual
        /// </summary>
        /// <param name="filtros"> filtro de reporte </param>
        /// <returns> ReporteTasaConversionConsolidadaMensualGraficasVistaDTO <returns>
        public ReporteTasaConversionConsolidadaGraficasVistaDTO ReporteTasaConversionConsolidadoAsesoresGrafica(ReporteTasaConversionConsolidadaGraficaFiltroDTO filtros)
        {
            string coordinadores = ListIntToString(filtros.Coordinadores);
            string asesores = ListIntToString(filtros.Asesores);
            string periodoInicio = filtros.PeriodoInicio;
            string periodoFin = filtros.PeriodoFin;
            try
            {
                var listRpta = _unitOfWork.ReportesRepository.ReporteTasaConversionConsolidadoAsesoresGraficas(coordinadores, asesores, periodoInicio, periodoFin);
                return listRpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Convierte lista de int en string
        /// </summary>
        /// <returns> string </returns>
        private string ListIntToString(IList<int> datos)
        {
            if (datos == null)
                datos = new List<int>();
            int numeroElementos = datos.Count;
            string rptaCadena = string.Empty;
            for (int i = 0; i < numeroElementos - 1; i++)
                rptaCadena += Convert.ToString(datos[i]) + ",";
            if (numeroElementos > 0)
                rptaCadena += Convert.ToString(datos[numeroElementos - 1]);
            return rptaCadena;
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Convierte lista de Id string en una sola cadena string
        /// </summary>
        /// <returns> string </returns>
        private string ListStringToString(IList<string> datos)
        {
            if (datos == null)
                datos = new List<string>();
            int NumberElements = datos.Count;
            string rptaCadena = string.Empty;
            for (int i = 0; i < NumberElements - 1; i++)
                rptaCadena += Convert.ToString(datos[i]) + ",";
            if (NumberElements > 0)
                rptaCadena += Convert.ToString(datos[NumberElements - 1]);
            return rptaCadena;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de la Tasa Conversión de cada Asesor
        /// </summary>
        /// <param filtros></param>
        /// <returns>ReporteTasaConversionConsolidadaAsesoresDTO</returns>
        /// <exception cref="Exception"></exception>
        public ReporteTasaConversionConsolidadaAsesoresAlternoDTO ReporteTasaConversionConsolidadoAsesores(ReporteTasaConversionConsolidadaFiltroDTO filtros)
        {
            string area = ListIntToString(filtros.Areas);
            string subarea = ListIntToString(filtros.SubAreas);
            string PGeneral = ListIntToString(filtros.PGeneral);
            string PEspecifico = ListIntToString(filtros.PEspecifico);
            string modalidades = ListStringToString(filtros.Modalidades);
            string ciudades = ListStringToString(filtros.Ciudades);
            string coordinadores = ListIntToString(filtros.Coordinadores);
            string asesores = ListIntToString(filtros.Asesores);
            filtros.FechaInicio = Convert.ToDateTime(filtros.FechaInicio).Date;
            filtros.FechaFin = Convert.ToDateTime(filtros.FechaFin).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            try
            {
                ReporteTasaConversionConsolidadaAsesoresAlternoDTO item = new ReporteTasaConversionConsolidadaAsesoresAlternoDTO();
                item = _unitOfWork.ReportesRepository.ReporteTasaConversionConsolidadoAsesoresAlterno(area, subarea, PGeneral, PEspecifico, modalidades, ciudades, filtros.Fecha, coordinadores, asesores, filtros.FechaInicio.Value, filtros.FechaFin.Value);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de Centro Costo de cada Asesor
        /// </summary>
        /// <param filtros></param>
        /// <returns>List<TCRM_CentroCostoPorAsesorDTO></returns>
        /// <exception cref="Exception"></exception>
        public List<TCRM_CentroCostoPorAsesorDTO> ObtenerCentroCostoPorAsesor(ReporteTasaConversionConsolidadaFiltroDTO filtros)
        {
            string area = ListIntToString(filtros.Areas);
            string subarea = ListIntToString(filtros.SubAreas);
            string PGeneral = ListIntToString(filtros.PGeneral);
            string PEspecifico = ListIntToString(filtros.PEspecifico);
            string modalidades = ListStringToString(filtros.Modalidades);
            string ciudades = ListStringToString(filtros.Ciudades);
            string coordinadores = ListIntToString(filtros.Coordinadores);
            string asesores = ListIntToString(filtros.Asesores);
            filtros.FechaInicio = Convert.ToDateTime(filtros.FechaInicio).Date;
            filtros.FechaFin = Convert.ToDateTime(filtros.FechaFin).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            try
            {
                List<TCRM_CentroCostoPorAsesorDTO> item = new List<TCRM_CentroCostoPorAsesorDTO>();
                return _unitOfWork.ReportesRepository.ObtenerCentroCostoPorAsesorAlterno(area, subarea, PGeneral, PEspecifico, modalidades, ciudades, filtros.Fecha, coordinadores, asesores, filtros.FechaInicio.Value, filtros.FechaFin.Value);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta el filtro segmento para conjunto lista
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns> Lista de objetos de clase DateTime </returns>
        public List<DateTime> ObtenerActividadesNoEjecutadas(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ReportesRepository.ObtenerActividadesNoEjecutadas(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtiene el log de las oportunidades
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Lista de objetos de clase ReporteSeguimientoOportunidadLogGridDTO</returns>
        public List<ReporteSeguimientoOportunidadLogGridDTO> ObtenerOportunidadesLog(int idOportunidad)
        {
            try
            {
                List<ReporteSeguimientoOportunidadLogDTO> oportunidadesLog = _unitOfWork.ReportesRepository.ObtenerListaOportunidadLog(idOportunidad);

                List<ReporteSeguimientoOportunidadLogGridDTO> listaSeguientoOportunidad = new List<ReporteSeguimientoOportunidadLogGridDTO>();
                List<ReporteActividadOcurrenciaDTO> listaActividades = _unitOfWork.OportunidadLogRepository.ObtenerReporteActividadOcurrenciaPorIdOportunidad(idOportunidad);
                foreach (var oportunidad in oportunidadesLog)
                {

                    ReporteSeguimientoOportunidadLogGridDTO auxOportunidadLog = new ReporteSeguimientoOportunidadLogGridDTO();
                    auxOportunidadLog.IdActividadDetalle = oportunidad.IdActividadDetalle.Value;
                    auxOportunidadLog.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == EstadoOcurrencia.Ejecutado
                        && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial
                        && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == EstadoOcurrencia.NoEjecutado
                        && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial
                        && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == EstadoOcurrencia.AsignacionManual
                        && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial
                        && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.FaseInicio = oportunidad.FaseInicio;
                    auxOportunidadLog.FaseDestino = oportunidad.FaseDestino;
                    auxOportunidadLog.FechaModificacion = oportunidad.FechaModificacion;
                    auxOportunidadLog.FechaSiguienteLlamada = oportunidad.FechaSiguienteLlamada;
                    auxOportunidadLog.LlamadaIntegra = oportunidad.LlamadaIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();
                    auxOportunidadLog.LlamadaTresCX = oportunidad.LlamadaTresCX.OrderBy(x => x.FechaInicioLlamada).ToList();
                    auxOportunidadLog.NombreActividad = oportunidad.NombreActividad;
                    auxOportunidadLog.NombreOcurrencia = oportunidad.NombreOcurrencia;
                    auxOportunidadLog.ComentarioActividad = oportunidad.ComentarioActividad;

                    if (oportunidad.IdFaseOportunidad == 8)
                    {
                        if (oportunidad.IdFaseOportunidadIP == 5)
                            auxOportunidadLog.EstadoFase = "Solido";
                        else
                            auxOportunidadLog.EstadoFase = "-";
                    }
                    else if (oportunidad.IdFaseOportunidad == 22)
                    {
                        if (oportunidad.IdFaseOportunidadPF == 5)
                            auxOportunidadLog.EstadoFase = "Solido";
                        else
                            auxOportunidadLog.EstadoFase = "-";
                        auxOportunidadLog.FechaEnvio = oportunidad.FechaEnvioFaseOportunidadPF;
                    }
                    else if (oportunidad.IdFaseOportunidad == 12)
                    {
                        if (oportunidad.IdFaseOportunidadIC == 5)
                            auxOportunidadLog.EstadoFase = "Solido";
                        else
                            auxOportunidadLog.EstadoFase = "-";
                    }

                    if (oportunidad.IdFaseOportunidad == 12 || oportunidad.IdFaseOportunidad == 22)
                    {
                        if (oportunidad.FechaPagoFaseOportunidadPF != null)
                            auxOportunidadLog.FechaPago = oportunidad.FechaPagoFaseOportunidadPF;
                        else
                            auxOportunidadLog.FechaPago = oportunidad.FechaPagoFaseOportunidadIC;
                    }

                    if (oportunidad.IdEstadoOcurrencia == 2)    //ValorEstatico.IdEstadoOcurrenciaNoEjecutado)
                    {
                        List<int> idOcurrencias = new List<int>() { 149, 162, 163, 164, 165, 168, 207, 209 };
                        if (idOcurrencias.Contains(oportunidad.IdOcurrencia ?? 0))
                            auxOportunidadLog.Estado = "REPROGRAMADO M.";
                        else
                            auxOportunidadLog.Estado = "REPROGRAMADO AUT.";
                    }
                    else if (oportunidad.IdEstadoOcurrencia == 1)
                        auxOportunidadLog.Estado = "EJECUTADO";
                    else if (oportunidad.IdEstadoOcurrencia == 7)
                        auxOportunidadLog.Estado = "ASIGNACION";

                    auxOportunidadLog.TiempoDuracion = oportunidad.TiempoDuracion;
                    auxOportunidadLog.TiempoDuracion3CX = oportunidad.TiempoDuracion3CX;
                    listaSeguientoOportunidad.Add(auxOportunidadLog);
                }
                var codigoFaseOportunidad = _unitOfWork.OportunidadRepository.ObtenerCodigoFasePorIdOportunidad(idOportunidad);
                ReporteSeguimientoOportunidadLogGridDTO ultimaOportunidad = new ReporteSeguimientoOportunidadLogGridDTO();
                ultimaOportunidad.FaseInicio = codigoFaseOportunidad.FaseInicio;
                ultimaOportunidad.FechaSiguienteLlamada = codigoFaseOportunidad.FechaSiguienteLlamada;
                ultimaOportunidad.IdFaseOportunidad = codigoFaseOportunidad.IdFaseOportunidad;
                ultimaOportunidad.Estado = "NO EJECUTADO";
                ultimaOportunidad.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 1 && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count(); //ValorEstatico.IdEstadoOcurrenciaEjecutado
                ultimaOportunidad.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 2 && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();//ValorEstatico.IdEstadoOcurrenciaNoEjecutado
                ultimaOportunidad.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == 7 && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();

                listaSeguientoOportunidad.Add(ultimaOportunidad);

                return listaSeguientoOportunidad;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 16/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Oportunidades Log por alumno mediante idAlumno, idOportunidad, idPadre
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idOportunidad"></param>
        /// <param name="idPadre"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadLogGridDTO> ObtenerOportunidadesLogPorAlumno(int idAlumno, int idOportunidad, int idPadre)
        {
            try
            {
                List<ReporteSeguimientoOportunidadLogDTO> oportunidades = _unitOfWork.ReportesRepository.ObtenerListaOportunidadLogPorIdAlumno(idAlumno, idOportunidad, idPadre);
                List<ObtenerSeguimientoPagosAlumnoComentarioDTO> listaComentarios = _unitOfWork.OportunidadRepository.ObtenerComentariosOperacionesPagosAcademicos(idOportunidad);

                List<ReporteSeguimientoOportunidadLogGridDTO> listaSeguientoOportunidad = new List<ReporteSeguimientoOportunidadLogGridDTO>();
                List<ReporteActividadOcurrenciaDTO> listaActividades = _unitOfWork.ReportesRepository.ReporteActividadOcurrenciaPorIdAlumno(idAlumno);
                ReporteSeguimientoOportunidadLogGridDTO auxOportunidadLog;
                foreach (var oportunidad in oportunidades)
                {

                    auxOportunidadLog = new ReporteSeguimientoOportunidadLogGridDTO();
                    auxOportunidadLog.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 1 && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 2 && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == 7 && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.FaseInicio = oportunidad.FaseInicio;
                    auxOportunidadLog.FaseDestino = oportunidad.FaseDestino;
                    auxOportunidadLog.FechaModificacion = oportunidad.FechaModificacion;
                    auxOportunidadLog.ComentarioAcademico = string.Join("", (listaComentarios.Where(comentario => comentario.Fecha.Value.Date == oportunidad.FechaModificacion.Value.Date).Select(comentario => comentario.ComentariosTipoAcademico)));
                    auxOportunidadLog.ComentarioPago = string.Join("", (listaComentarios.Where(comentario => comentario.Fecha.Value.Date == oportunidad.FechaModificacion.Value.Date).Select(comentario => comentario.ComentariosTipoPago)));

                    auxOportunidadLog.FechaSiguienteLlamada = oportunidad.FechaSiguienteLlamada;
                    auxOportunidadLog.LlamadaIntegra = oportunidad.LlamadaIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();
                    auxOportunidadLog.LlamadaTresCX = oportunidad.LlamadaTresCX.OrderBy(x => x.FechaInicioLlamada).ToList();
                    auxOportunidadLog.NombreActividad = oportunidad.NombreActividad;
                    auxOportunidadLog.NombreOcurrencia = oportunidad.NombreOcurrencia;
                    auxOportunidadLog.ComentarioActividad = oportunidad.ComentarioActividad;

                    if (oportunidad.IdFaseOportunidad == 8)
                    {
                        if (oportunidad.IdFaseOportunidadIP == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                    }
                    else if (oportunidad.IdFaseOportunidad == 22)
                    {
                        if (oportunidad.IdFaseOportunidadPF == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                        auxOportunidadLog.FechaEnvio = oportunidad.FechaEnvioFaseOportunidadPF;
                    }
                    else if (oportunidad.IdFaseOportunidad == 12)
                    {
                        if (oportunidad.IdFaseOportunidadIC == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                    }

                    if (oportunidad.IdFaseOportunidad == 12 || oportunidad.IdFaseOportunidad == 22)
                    {
                        if (oportunidad.FechaPagoFaseOportunidadPF != null) auxOportunidadLog.FechaPago = oportunidad.FechaPagoFaseOportunidadPF;
                        else auxOportunidadLog.FechaPago = oportunidad.FechaPagoFaseOportunidadIC;
                    }
                    if (oportunidad.IdEstadoOcurrencia == 2) //ValorEstatico.IdEstadoOcurrenciaNoEjecutado
                    {
                        if (new int[] { 149, 162, 163, 164, 165, 168, 207, 209 }.Contains(oportunidad.IdOcurrencia ?? 0)) auxOportunidadLog.Estado = "REPROGRAMADO M.";
                        else auxOportunidadLog.Estado = "REPROGRAMADO AUT.";
                    }
                    else if (oportunidad.IdEstadoOcurrencia == 1) auxOportunidadLog.Estado = "EJECUTADO";
                    else if (oportunidad.IdEstadoOcurrencia == 7) auxOportunidadLog.Estado = "ASIGNACION";

                    auxOportunidadLog.TiempoDuracion = oportunidad.TiempoDuracion;
                    auxOportunidadLog.TiempoDuracion3CX = oportunidad.TiempoDuracion3CX;
                    listaSeguientoOportunidad.Add(auxOportunidadLog);
                }

                var ultimaOportunidad = _unitOfWork.ReportesRepository.ObtenerOportunidadCodigoFaseporIdAlumno(idAlumno, idOportunidad);
                ultimaOportunidad.Estado = "NO EJECUTADO";

                ultimaOportunidad.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 1 && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();
                ultimaOportunidad.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 2 && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();
                ultimaOportunidad.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == 7 && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();

                listaSeguientoOportunidad.Add(ultimaOportunidad);

                return listaSeguientoOportunidad;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 16/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Oportunidades Log por alumno mediante idAlumno, idOportunidad, idPadre
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idOportunidad"></param>
        /// <param name="idPadre"></param>
        /// <returns></returns>
        public ReporteSeguimientoNWActividadAlternoOperacionesDTO ObtenerOportunidadesLogPorAlumnoATC(int idAlumno, int idOportunidad, int idPadre, int pageNumber, int pageSize)
        {
            try
            {
                var seguimientoDetalle = ObtenerReporteSeguimientoDetallePorIdOportunidadATC(idAlumno, idOportunidad, idPadre, pageNumber, pageSize);
                List<ReporteActividadOcurrenciaDTO> listaActividades = _unitOfWork.ReportesRepository.ReporteActividadOcurrenciaPorIdAlumno(idAlumno);
                List<ObtenerSeguimientoPagosAlumnoComentarioDTO> listaComentarios = _unitOfWork.OportunidadRepository.ObtenerComentariosOperacionesPagosAcademicos(idOportunidad);

                var reporteActividades = new List<ReporteSeguimientoNWActividadAlternoATCDTO>();
                seguimientoDetalle.Items.ForEach(s =>
                {
                    var item = new ReporteSeguimientoNWActividadAlternoATCDTO()
                    {
                        IdActividadDetalle = s.IdActividadDetalle,
                        FaseInicio = s.FaseInicio,
                        FaseDestino = s.FaseDestino,
                        FechaModificacion = s.FechaModificacion,
                        FechaSiguienteLlamada = s.FechaSiguienteLlamada,
                        NombreActividad = s.NombreActividad,
                        NombreOcurrencia = s.NombreOcurrencia,
                        ComentarioActividad = s.ComentarioActividad,
                        ComentarioAcademico = string.Join("", (listaComentarios.Where(comentario => comentario.Fecha.Value.Date == s.FechaModificacion.Value.Date).Select(comentario => comentario.ComentariosTipoAcademico))),
                        ComentarioPago = string.Join("", (listaComentarios.Where(comentario => comentario.Fecha.Value.Date == s.FechaModificacion.Value.Date).Select(comentario => comentario.ComentariosTipoPago))),
                        OtroMedio = s.OtroMedio,
                        EstadoSeguimientoWhatsApp = s.EstadoSeguimientoWhatsApp,
                        TotalEjecutadas = listaActividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        TotalNoEjecutadas = listaActividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        TotalAsignacionManual = listaActividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        EstadoFase = ObtenerEstadoFaseOportunidadLog(
                                new EstadoFaseOportunidadLogDTO()
                                {
                                    IdFaseOportunidad = s.IdFaseOportunidad,
                                    IdFaseOportunidadIC = s.IdFaseOportunidadIC,
                                    IdFaseOportunidadIP = s.IdFaseOportunidadIP,
                                    IdFaseOportunidadPF = s.IdFaseOportunidadPF
                                }
                            ),
                        Estado = ObtenerEstadoActividadOportunidadLog(s.IdEstadoOcurrencia, s.IdOcurrencia),
                        FechaEnvio = s.IdFaseOportunidad == FaseOportunidad.PF ? s.FechaEnvioFaseOportunidadPF : null,
                        FechaPago = s.IdFaseOportunidad == FaseOportunidad.IC || s.IdFaseOportunidad == FaseOportunidad.PF ?
                            s.FechaPagoFaseOportunidadPF ?? s.FechaPagoFaseOportunidadIC : null,
                    };
                    item.LlamadasIntegra3cx = s.Llamadas.OrderBy(p => p.FechaInicioLlamada).ToList();
                    reporteActividades.Add(item);
                });

                if (pageNumber == 1)
                {
                    var codigoFase = _unitOfWork.ReportesRepository.ObtenerOportunidadCodigoFaseporIdAlumno(idAlumno, idOportunidad);
                    var actividadProgramada = new ReporteSeguimientoNWActividadAlternoATCDTO()
                    {
                        FaseInicio = codigoFase.FaseInicio,
                        FechaSiguienteLlamada = codigoFase.FechaSiguienteLlamada,
                        Estado = "NO EJECUTADO",
                        TotalEjecutadas = listaActividades
                                .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado
                                    && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                        TotalNoEjecutadas = listaActividades
                                .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado
                                    && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                        TotalAsignacionManual = listaActividades
                                .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual
                                    && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                    };
                    reporteActividades.Add(actividadProgramada);
                }

                return new ReporteSeguimientoNWActividadAlternoOperacionesDTO
                {
                    Items = reporteActividades,
                    TotalActividades = seguimientoDetalle.TotalActividades
                };
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 30/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Historial de Comentarios asociado a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadLogHistorialComentariosDTO> </returns>
        public OportunidadLogReporteSeguimientoDetalleOperacionesDTO ObtenerReporteSeguimientoDetallePorIdOportunidadATC(int idAlumno, int idOportunidad, int idPadre, int pageNumber, int pageSize)
        {
            try
            {
                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad);
                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(oportunidad.IdPersonalAsignado == null ? 0 : oportunidad.IdPersonalAsignado.Value);
                var logSeguimientoNW = _unitOfWork.ReportesRepository.ObtenerOportunidadLogReporteSeguimientoV5ATC(idAlumno, idOportunidad, idPadre, diferenciaHoraria == null ? 0 : (diferenciaHoraria.Valor == null ? 0 : diferenciaHoraria.Valor.Value), pageNumber, pageSize);
                var seguimientoNWDetalle = (
                    from p in logSeguimientoNW.Items
                    group p by new
                    {
                        p.IdActividadDetalle,
                        p.FaseInicio,
                        p.FaseDestino,
                        p.FechaModificacion,
                        p.FechaSiguienteLlamada,
                        p.IdFaseOportunidad,
                        p.IdFaseOportunidadIP,
                        p.IdFaseOportunidadPF,
                        p.IdFaseOportunidadIC,
                        p.FechaEnvioFaseOportunidadPF,
                        p.FechaPagoFaseOportunidadPF,
                        p.FechaPagoFaseOportunidadIC,
                        p.IdOcurrencia,
                        p.IdEstadoOcurrencia,
                        p.IdOportunidadLog,
                        p.NombreActividad,
                        p.NombreOcurrencia,
                        p.ComentarioActividad,
                        p.IdFaseOportunidadInicial,
                        p.EstadoSeguimientoWhatsApp,
                        p.OtroMedio
                    } into g
                    select new OportunidadLogReporteSeguimientoDetalleATCDTO
                    {
                        IdActividadDetalle = g.Key.IdActividadDetalle,
                        FaseInicio = g.Key.FaseInicio,
                        FaseDestino = g.Key.FaseDestino,
                        FechaModificacion = g.Key.FechaModificacion,
                        FechaSiguienteLlamada = g.Key.FechaSiguienteLlamada,
                        IdFaseOportunidad = g.Key.IdFaseOportunidad,
                        IdFaseOportunidadIP = g.Key.IdFaseOportunidadIP,
                        IdFaseOportunidadPF = g.Key.IdFaseOportunidadPF,
                        IdFaseOportunidadIC = g.Key.IdFaseOportunidadIC,
                        FechaEnvioFaseOportunidadPF = g.Key.FechaEnvioFaseOportunidadPF,
                        FechaPagoFaseOportunidadPF = g.Key.FechaPagoFaseOportunidadPF,
                        FechaPagoFaseOportunidadIC = g.Key.FechaPagoFaseOportunidadIC,
                        IdOcurrencia = g.Key.IdOcurrencia,
                        IdEstadoOcurrencia = g.Key.IdEstadoOcurrencia,
                        IdOportunidadLog = g.Key.IdOportunidadLog,
                        NombreActividad = g.Key.NombreActividad,
                        NombreOcurrencia = g.Key.NombreOcurrencia,
                        ComentarioActividad = g.Key.ComentarioActividad,
                        IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                        OtroMedio = g.Key.OtroMedio,
                        EstadoSeguimientoWhatsApp = g.Key.EstadoSeguimientoWhatsApp,
                        Llamadas = g.Select(o => new LlamadaIntegra3cxDTO
                        {
                            Id = o.IdRegistroLlamada,
                            DuracionTimbrado = o.DuracionTimbrado,
                            DuracionContesto = o.DuracionContesto,
                            DuracionTimbradoMinutos = ((double)(o.DuracionTimbrado.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            DuracionContestoMinutos = ((double)(o.DuracionContesto.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            FechaInicioLlamada = o.FechaInicioLlamada,
                            FechaFinLlamada = o.FechaFinLlamada,
                            EstadoLlamada = o.EstadoLlamada,
                            SubEstadoLlamada = o.SubEstadoLlamada,
                            UrlGrabacion = o.UrlGrabacion,
                            NombreGrabacion = o.UrlGrabacion,
                            Webphone = o.WebphoneGrabacion,
                            OrigenLlamada = o.OrigenLlamada
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()
                    }
                ).ToList();

                return new OportunidadLogReporteSeguimientoDetalleOperacionesDTO
                {
                    Items = seguimientoNWDetalle,
                    TotalActividades = logSeguimientoNW.TotalActividades
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 16/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Oportunidades Log por alumno mediante idAlumno, idOportunidad, idPadre
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idOportunidad"></param>
        /// <param name="idPadre"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoNWActividadAlternoATCDTO> ObtenerOportunidadesLogPorAlumnoOperaciones(int idAlumno, int idOportunidad, int idPadre,int numberPage,int pageSize)
        {
            try
            {
                var seguimientoDetalle = ObtenerReporteSeguimientoDetalleOportunidadATC(idAlumno, idOportunidad, idPadre,numberPage,pageSize);
                List<ReporteActividadOcurrenciaDTO> listaActividades = _unitOfWork.ReportesRepository.ReporteActividadOcurrenciaPorIdAlumno(idAlumno);
                List<ObtenerSeguimientoPagosAlumnoComentarioDTO> listaComentarios = _unitOfWork.OportunidadRepository.ObtenerComentariosOperacionesPagosAcademicos(idOportunidad);

                List<ReporteSeguimientoOportunidadLogGridDTO> listaSeguientoOportunidad = new List<ReporteSeguimientoOportunidadLogGridDTO>();
                ReporteSeguimientoOportunidadLogGridDTO auxOportunidadLog;
                var reporteActividades = new List<ReporteSeguimientoNWActividadAlternoATCDTO>();
                seguimientoDetalle.ForEach(s =>
                {
                    var item = new ReporteSeguimientoNWActividadAlternoATCDTO()
                    {
                        IdActividadDetalle = s.IdActividadDetalle,
                        FaseInicio = s.FaseInicio,
                        FaseDestino = s.FaseDestino,
                        FechaModificacion = s.FechaModificacion,
                        FechaSiguienteLlamada = s.FechaSiguienteLlamada,
                        NombreActividad = s.NombreActividad,
                        NombreOcurrencia = s.NombreOcurrencia,
                        ComentarioActividad = s.ComentarioActividad,
                        ComentarioAcademico = string.Join("", (listaComentarios.Where(comentario => comentario.Fecha.Value.Date == s.FechaModificacion.Value.Date).Select(comentario => comentario.ComentariosTipoAcademico))),
                        ComentarioPago = string.Join("", (listaComentarios.Where(comentario => comentario.Fecha.Value.Date == s.FechaModificacion.Value.Date).Select(comentario => comentario.ComentariosTipoPago))),
                        OtroMedio = s.OtroMedio,
                        EstadoSeguimientoWhatsApp = s.EstadoSeguimientoWhatsApp,
                        TotalEjecutadas = listaActividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        TotalNoEjecutadas = listaActividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        TotalAsignacionManual = listaActividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        EstadoFase = ObtenerEstadoFaseOportunidadLog(
                                new EstadoFaseOportunidadLogDTO()
                                {
                                    IdFaseOportunidad = s.IdFaseOportunidad,
                                    IdFaseOportunidadIC = s.IdFaseOportunidadIC,
                                    IdFaseOportunidadIP = s.IdFaseOportunidadIP,
                                    IdFaseOportunidadPF = s.IdFaseOportunidadPF
                                }
                            ),
                        Estado = ObtenerEstadoActividadOportunidadLog(s.IdEstadoOcurrencia, s.IdOcurrencia),
                        FechaEnvio = s.IdFaseOportunidad == FaseOportunidad.PF ? s.FechaEnvioFaseOportunidadPF : null,
                        FechaPago = s.IdFaseOportunidad == FaseOportunidad.IC || s.IdFaseOportunidad == FaseOportunidad.PF ?
                            s.FechaPagoFaseOportunidadPF ?? s.FechaPagoFaseOportunidadIC : null,
                    };
                    item.LlamadasIntegra3cx = s.Llamadas.OrderBy(p => p.FechaInicioLlamada).ToList();
                    reporteActividades.Add(item);
                });

                var codigoFase = _unitOfWork.ReportesRepository.ObtenerOportunidadCodigoFaseporIdAlumno(idAlumno, idOportunidad);
                var actividadProgramada = new ReporteSeguimientoNWActividadAlternoATCDTO()
                {
                    FaseInicio = codigoFase.FaseInicio,
                    FechaSiguienteLlamada = codigoFase.FechaSiguienteLlamada,
                    Estado = "NO EJECUTADO",
                    TotalEjecutadas = listaActividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                    TotalNoEjecutadas = listaActividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                    TotalAsignacionManual = listaActividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                };
                reporteActividades.Add(actividadProgramada);
                return reporteActividades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 30/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Historial de Comentarios asociado a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadLogHistorialComentariosDTO> </returns>
        public List<OportunidadLogReporteSeguimientoDetalleATCDTO> ObtenerReporteSeguimientoDetalleOportunidadATC(int idAlumno, int idOportunidad, int idPadre,int numberPage,int pageSize)
        {
            try
            {
                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad);
                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(oportunidad.IdPersonalAsignado == null ? 0 : oportunidad.IdPersonalAsignado.Value);
                var logSeguimientoNW = _unitOfWork.ReportesRepository.ObtenerOportunidadLogReporteSeguimientoV5Operaciones(idAlumno, idOportunidad, idPadre, diferenciaHoraria == null ? 0 : (diferenciaHoraria.Valor == null ? 0 : diferenciaHoraria.Valor.Value), numberPage,pageSize);
                var seguimientoNWDetalle = (
                    from p in logSeguimientoNW
                    group p by new
                    {
                        p.IdActividadDetalle,
                        p.FaseInicio,
                        p.FaseDestino,
                        p.FechaModificacion,
                        p.FechaSiguienteLlamada,
                        p.IdFaseOportunidad,
                        p.IdFaseOportunidadIP,
                        p.IdFaseOportunidadPF,
                        p.IdFaseOportunidadIC,
                        p.FechaEnvioFaseOportunidadPF,
                        p.FechaPagoFaseOportunidadPF,
                        p.FechaPagoFaseOportunidadIC,
                        p.IdOcurrencia,
                        p.IdEstadoOcurrencia,
                        p.IdOportunidadLog,
                        p.NombreActividad,
                        p.NombreOcurrencia,
                        p.ComentarioActividad,
                        p.IdFaseOportunidadInicial,
                        p.EstadoSeguimientoWhatsApp,
                        p.OtroMedio
                    } into g
                    select new OportunidadLogReporteSeguimientoDetalleATCDTO
                    {
                        IdActividadDetalle = g.Key.IdActividadDetalle,
                        FaseInicio = g.Key.FaseInicio,
                        FaseDestino = g.Key.FaseDestino,
                        FechaModificacion = g.Key.FechaModificacion,
                        FechaSiguienteLlamada = g.Key.FechaSiguienteLlamada,
                        IdFaseOportunidad = g.Key.IdFaseOportunidad,
                        IdFaseOportunidadIP = g.Key.IdFaseOportunidadIP,
                        IdFaseOportunidadPF = g.Key.IdFaseOportunidadPF,
                        IdFaseOportunidadIC = g.Key.IdFaseOportunidadIC,
                        FechaEnvioFaseOportunidadPF = g.Key.FechaEnvioFaseOportunidadPF,
                        FechaPagoFaseOportunidadPF = g.Key.FechaPagoFaseOportunidadPF,
                        FechaPagoFaseOportunidadIC = g.Key.FechaPagoFaseOportunidadIC,
                        IdOcurrencia = g.Key.IdOcurrencia,
                        IdEstadoOcurrencia = g.Key.IdEstadoOcurrencia,
                        IdOportunidadLog = g.Key.IdOportunidadLog,
                        NombreActividad = g.Key.NombreActividad,
                        NombreOcurrencia = g.Key.NombreOcurrencia,
                        ComentarioActividad = g.Key.ComentarioActividad,
                        IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                        OtroMedio = g.Key.OtroMedio,
                        EstadoSeguimientoWhatsApp = g.Key.EstadoSeguimientoWhatsApp,
                        Llamadas = g.Select(o => new LlamadaIntegra3cxDTO
                        {
                            Id = o.IdRegistroLlamada,
                            DuracionTimbrado = o.DuracionTimbrado,
                            DuracionContesto = o.DuracionContesto,
                            DuracionTimbradoMinutos = ((double)(o.DuracionTimbrado.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            DuracionContestoMinutos = ((double)(o.DuracionContesto.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            FechaInicioLlamada = o.FechaInicioLlamada,
                            FechaFinLlamada = o.FechaFinLlamada,
                            EstadoLlamada = o.EstadoLlamada,
                            SubEstadoLlamada = o.SubEstadoLlamada,
                            UrlGrabacion = o.UrlGrabacion,
                            NombreGrabacion = o.UrlGrabacion,
                            Webphone = o.WebphoneGrabacion,
                            OrigenLlamada = o.OrigenLlamada
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()
                    }
                ).ToList();
                return seguimientoNWDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Estado Fase de la Oportunidad dependiendo de los valores de IdFaseOportunidad
        /// </summary>
        /// <param name="olog">DTO que encapsula los IdFaseOportunidad</param>
        /// <returns> ValorStringDTO </returns>
        public string ObtenerEstadoFaseOportunidadLog(EstadoFaseOportunidadLogDTO olog)
        {
            string estadoFase = "-";
            const int estadoSolido = 1; //5
            const int estadoDudoso = 2;
            if (olog.IdFaseOportunidad == FaseOportunidad.IP)
            {
                if (olog.IdFaseOportunidadIP == estadoSolido)
                    estadoFase = "Solido";
                else if (olog.IdFaseOportunidadIP == estadoDudoso)
                    estadoFase = "Dudoso";
                else
                    estadoFase = "-";
            }
            else if (olog.IdFaseOportunidad == FaseOportunidad.PF)
            {
                if (olog.IdFaseOportunidadPF == estadoSolido)
                    estadoFase = "Solido";
                else if (olog.IdFaseOportunidadPF == estadoDudoso)
                    estadoFase = "Dudoso";
                else
                    estadoFase = "-";
            }
            else if (olog.IdFaseOportunidad == FaseOportunidad.IC)
            {
                if (olog.IdFaseOportunidadIC == estadoSolido)
                    estadoFase = "Solido";
                else if (olog.IdFaseOportunidadIC == estadoDudoso)
                    estadoFase = "Dudoso";
                else
                    estadoFase = "-";
            }
            return estadoFase;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Estado de la Actividad dependiendo de el EstadoOcurrencia y la Ocurrencia
        /// </summary>
        /// <param name="idEstadoOcurrencia">Id de EstadoOcurrencia</param>
        /// <param name="idOcurrencia">Id de Ocurrencia</param>
        /// <returns> ValorStringDTO </returns>
        public string? ObtenerEstadoActividadOportunidadLog(int? idEstadoOcurrencia, int? idOcurrencia)
        {
            try
            {
                string? estado = null;
                if (idEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado)
                    estado = "EJECUTADO";
                else if (idEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado)
                {
                    bool reprogramadoManual = new int[] { 149, 162, 163, 164, 165, 168, 207, 209 }.Contains(idOcurrencia ?? 0);
                    if (reprogramadoManual)
                        estado = "REPROGRAMADO M.";
                    else
                        estado = "REPROGRAMADO AUT.";
                }
                else if (idEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual)
                    estado = "ASIGNACION";

                return estado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Estado de la Actividad dependiendo de el EstadoOcurrencia y la Ocurrencia
        /// </summary>
        /// <param name="idEstadoOcurrencia">Id de EstadoOcurrencia</param>
        /// <param name="idOcurrencia">Id de Ocurrencia</param>
        /// <returns> ValorStringDTO </returns>
        public string? ObtenerEstadoActividadOportunidadLogATC(int? idEstadoOcurrencia, int? idOcurrencia)
        {
            try
            {
                string? estado = null;
                if (idEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado)
                    estado = "EJECUTADO";
                else if (idEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado)
                {
                    bool reprogramadoManual = new int[] { 149, 162, 163, 164, 165, 168, 207, 209 }.Contains(idOcurrencia ?? 0);
                    if (reprogramadoManual)
                        estado = "REPROGRAMADO M.";
                    else
                        estado = "REPROGRAMADO AUT.";
                }
                else if (idEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual)
                    estado = "ASIGNACION";

                return estado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte pendiente por periodo y coordinador
        /// </summary>
        /// <param name="filtroPendiente"></param>
        /// <returns> Lista de DTO: List<ReportePendientePeriodoyCoordinadorDTO> </returns>
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoYCoordinador(ReportePendienteFiltroDTO filtroPendiente)
        {
            try
            {
                return _unitOfWork.ReportesRepository.ObtenerReportePendientePeriodoYCoordinador(filtroPendiente);
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
        /// Obtiene el reporte pendiente de cambios por coordinador
        /// </summary>
        /// <param name="filtroPendiente"></param>
        /// <returns> Lista de DTO: List<ReportePendientesCambiosPorCoordinadorDTO> </returns>
        public List<ReportePendientesCambiosPorCoordinadorDTO> ObtenerReportePendienteCambiosPorCoordinador(ReportePendienteFiltroDTO filtroPendiente)
        {
            try
            {
                return _unitOfWork.ReportesRepository.ObtenerReportePendienteCambiosPorCoordinador(filtroPendiente);
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
        /// Obtiene las diferencias del reporte pendiente 
        /// </summary>
        /// <param name="filtroPendiente"></param>
        /// <returns> Lista de DTO: List<ReportePendientesDiferenciasDTO> </returns>
        public List<ReportePendientesDiferenciasDTO> ObtenerReportePendienteDiferencias(ReportePendienteFiltroDTO filtroPendiente)
        {
            try
            {
                return _unitOfWork.ReportesRepository.ObtenerReportePendienteDiferencias(filtroPendiente);
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
        /// Obtiene reporte pendiente detalles
        /// </summary>
        /// <param name="filtroPendiente"></param>
        /// <returns> Lista DTO: List<ReportePendienteDetallesDTO> </returns>
        public List<ReportePendienteDetallesDTO> ObtenerReportePendienteDetalles(ReportePendienteFiltroDTO filtroPendiente)
        {
            try
            {
                return _unitOfWork.ReportesRepository.ObtenerReportePendienteDetalles(filtroPendiente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene reporte de pagos por día
        /// </summary>
        /// <param name="filtroReportePagosDiaPeriodo"></param>
        /// <returns> Lista DTO: List<ReportePagosDiaPeriodoDTO> </returns>
        public List<ReportePagosDiaPeriodoDTO> ObtenerReportePagosDia(ReportePagosDiaPeriodoFiltroDTO filtroReportePagosDiaPeriodo)
        {
            try
            {
                PeriodoService periodoService = new PeriodoService(_unitOfWork);
                var XFechaInicio = periodoService.ObtenerFechaInicial(filtroReportePagosDiaPeriodo.Periodo);
                var XFechaFin = periodoService.ObtenerFechaFinal(filtroReportePagosDiaPeriodo.Periodo);

                var FechaInicio = Convert.ToDateTime(XFechaInicio.Valor);
                var FechaFin = Convert.ToDateTime(XFechaFin.Valor);

                DateTime fechainicio = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 59);

                return _unitOfWork.ReportesRepository.ObtenerReportePagosDia(filtroReportePagosDiaPeriodo, fechainicio, fechafin);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista por 12 meses antes del periodo seleccionado para saber los pagos realizados
        /// </summary>
        /// <returns> Lista DTO: List<ReportePagosDiaPeriodoDTO> </returns>
        public List<ReportePagosDiaPeriodoDTO> ObtenerReportePagosPeriodo(ReportePagosDiaPeriodoFiltroDTO filtroReportePagosDiaPeriodo)
        {
            try
            {
                PeriodoService periodoService = new PeriodoService(_unitOfWork);
                var XFechaInicio = periodoService.ObtenerFechaInicial(filtroReportePagosDiaPeriodo.Periodo);
                var XFechaFin = periodoService.ObtenerFechaFinal(filtroReportePagosDiaPeriodo.Periodo);

                var FechaFin = Convert.ToDateTime(XFechaFin.Valor);
                var FechaInicio = Convert.ToDateTime(XFechaInicio.Valor).AddMonths(-12);

                DateTime fechainicio = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 59);

                return _unitOfWork.ReportesRepository.ObtenerReportePagosPeriodo(filtroReportePagosDiaPeriodo, fechainicio, fechafin);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene reporte seguimiento de oportunidades
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadesOperacionesDTO> ObtenerReporteSeguimientoOportunidadOperaciones(ReporteSeguimientoOportunidadesFiltrosDTO filtro)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                filtro.FechaFin = filtro.FechaFin.AddHours(-5);
                filtro.FechaInicio = filtro.FechaInicio.AddHours(-5);
                SeguimientoFiltroFinalDTO seguimientoDTO = new SeguimientoFiltroFinalDTO();

                if (filtro.Asesores.Count() > 0)
                {
                    seguimientoDTO.Asesores = String.Join(",", filtro.Asesores);
                }
                if (filtro.EstadosMatricula.Count() > 0)
                {
                    seguimientoDTO.EstadosMatricula = String.Join(",", filtro.EstadosMatricula);
                }
                if (filtro.FasesOportunidad.Count() > 0)
                {
                    seguimientoDTO.FasesOportunidad = String.Join(",", filtro.FasesOportunidad);
                }
                if (filtro.FaseOportunidadOrigen.Count() > 0)
                {
                    seguimientoDTO.FasesOportunidadOrigen = String.Join(",", filtro.FaseOportunidadOrigen);
                }
                if (filtro.FaseOportunidadDestino.Count() > 0)
                {
                    seguimientoDTO.FasesOportunidadDestino = String.Join(",", filtro.FaseOportunidadDestino);
                }
                if (filtro.CentroCostos.Count() > 0)
                {
                    seguimientoDTO.CentroCostos = String.Join(",", filtro.CentroCostos);
                }
                seguimientoDTO.OpcionFase = filtro.OpcionFase;
                seguimientoDTO.ControlFiltroFecha = filtro.ControlFiltroFecha;
                seguimientoDTO.FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);
                seguimientoDTO.FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0);
                seguimientoDTO.CodigoMatricula = filtro.CodigoMatricula;
                seguimientoDTO.DocumentoIdentidad = filtro.DocumentoIdentidad;

                var data = _unitOfWork.ReportesRepository.ObtenerReporteSeguimientoOperaciones(seguimientoDTO);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Joseph LLanque
        /// Fecha: 03/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene reporte seguimiento de oportunidades
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ReporteInscritosCarreraOperacionesDTO> ObtenerReporteSeguimientoInscritosCarreraOperaciones(ReporteSeguimientoOportunidadesFiltrosDTO filtro)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                filtro.FechaFin = filtro.FechaFin.AddHours(-5);
                filtro.FechaInicio = filtro.FechaInicio.AddHours(-5);
                SeguimientoFiltroFinalDTO seguimientoDTO = new SeguimientoFiltroFinalDTO();
                var _alumnoService = new AlumnoService(_unitOfWork);

                if (filtro.Asesores.Count() > 0)
                {
                    seguimientoDTO.Asesores = String.Join(",", filtro.Asesores);
                }
                if (filtro.EstadosMatricula.Count() > 0)
                {
                    seguimientoDTO.EstadosMatricula = String.Join(",", filtro.EstadosMatricula);
                }
                if (filtro.FasesOportunidad.Count() > 0)
                {
                    seguimientoDTO.FasesOportunidad = String.Join(",", filtro.FasesOportunidad);
                }
                if (filtro.FaseOportunidadOrigen.Count() > 0)
                {
                    seguimientoDTO.FasesOportunidadOrigen = String.Join(",", filtro.FaseOportunidadOrigen);
                }
                if (filtro.FaseOportunidadDestino.Count() > 0)
                {
                    seguimientoDTO.FasesOportunidadDestino = String.Join(",", filtro.FaseOportunidadDestino);
                }
                if (filtro.CentroCostos.Count() > 0)
                {
                    seguimientoDTO.CentroCostos = String.Join(",", filtro.CentroCostos);
                }
                seguimientoDTO.OpcionFase = filtro.OpcionFase;
                seguimientoDTO.ControlFiltroFecha = filtro.ControlFiltroFecha;
                seguimientoDTO.FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);
                seguimientoDTO.FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0);
                seguimientoDTO.CodigoMatricula = filtro.CodigoMatricula;
                seguimientoDTO.DocumentoIdentidad = filtro.DocumentoIdentidad;

                var data = _unitOfWork.ReportesRepository.ObtenerReporteSeguimientoInscritosCarreraOperaciones(seguimientoDTO);

                foreach(var item in data)
                {
                    if(!string.IsNullOrWhiteSpace(item.Correo))
                        item.Correo = _alumnoService.EncriptarCorreoHash(item.Correo);
                    if (!string.IsNullOrWhiteSpace(item.Celular))
                        item.Celular = _alumnoService.EncriptarNumeroHash(Regex.Replace(item.Celular, @"[^\d]", ""));
                }



                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// Autor: Jonathan Caipo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades con las probabilidades.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesModeloDTO> - data </returns>
        public List<ReporteSeguimientoOportunidadesModeloDTO> ObtenerReporteSeguimientoOportunidadProbabilidad(ReporteSeguimientoOportunidadesFiltrosDTO filtro)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                filtro.FechaFin = filtro.FechaFin.AddHours(-5);
                filtro.FechaInicio = filtro.FechaInicio.AddHours(-5);
                SeguimientoFiltroFinalDTO seguimientoDTO = new SeguimientoFiltroFinalDTO();

                if (filtro.Asesores.Count() > 0)
                {
                    seguimientoDTO.Asesores = String.Join(",", filtro.Asesores);
                }

                if (filtro.FasesOportunidad.Count() > 0)
                {
                    seguimientoDTO.FasesOportunidad = String.Join(",", filtro.FasesOportunidad);
                }
                if (filtro.FaseOportunidadOrigen.Count() > 0)
                {
                    seguimientoDTO.FasesOportunidadOrigen = String.Join(",", filtro.FaseOportunidadOrigen);
                }
                if (filtro.FaseOportunidadDestino.Count() > 0)
                {
                    seguimientoDTO.FasesOportunidadDestino = String.Join(",", filtro.FaseOportunidadDestino);
                }
                if (filtro.CentroCostos.Count() > 0)
                {
                    seguimientoDTO.CentroCostos = String.Join(",", filtro.CentroCostos);
                }
                seguimientoDTO.OpcionFase = filtro.OpcionFase;
                seguimientoDTO.FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);
                seguimientoDTO.FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0);
                var data = _unitOfWork.ReportesRepository.ObtenerReporteSeguimientoProbabilidad(seguimientoDTO);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de seguimiento de oportunidad por fecha de registro de la campania
        /// </summary>
        /// <param name="filtro">Objeto de clase ReporteSeguimientoOportunidadesFiltrosDTO</param>
        /// <returns> Lista de objetos de clase ReporteSeguimientoOportunidadesDTO </returns>
        public List<ReporteSeguimientoOportunidadDTO> ObtenerReporteSeguimientoOportunidadFC(ReporteSeguimientoOportunidadesFiltrosDTO filtro)
        {
            try
            {
                //La vista Manda cinco horas Adelantadas
                filtro.FechaFin = filtro.FechaFin.AddHours(-5);
                filtro.FechaInicio = filtro.FechaInicio.AddHours(-5);
                SeguimientoFiltroFinalDTO filtroSeguimientoFinal = new SeguimientoFiltroFinalDTO();

                if (filtro.Asesores.Count() > 0)
                {
                    filtroSeguimientoFinal.Asesores = String.Join(",", filtro.Asesores);
                }
                if (filtro.FasesOportunidad.Count() > 0)
                {
                    filtroSeguimientoFinal.FasesOportunidad = String.Join(",", filtro.FasesOportunidad);
                }
                if (filtro.CentroCostos.Count() > 0)
                {
                    filtroSeguimientoFinal.CentroCostos = String.Join(",", filtro.CentroCostos);
                }
                filtroSeguimientoFinal.FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);
                filtroSeguimientoFinal.FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0);
                var data = _unitOfWork.ReportesRepository.ObtenerReporteSeguimientoFC(filtroSeguimientoFinal);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de seguimiento de oportunidad por fecha de registro de la campania
        /// </summary>
        /// <param name="filtro">Objeto de clase ReporteSeguimientoOportunidadesFiltrosDTO</param>
        /// <returns>Lista de objetos de clase ReporteSeguimientoOportunidadesDTO</returns>
        public List<ReporteSeguimientoOportunidadDTO> ObtenerReporteSeguimientoOportunidadFRC(ReporteSeguimientoOportunidadesFiltrosDTO filtro)
        {
            try
            {
                // La vista manda cinco horas Adelantadas
                filtro.FechaFin = filtro.FechaFin.AddHours(-5);
                filtro.FechaInicio = filtro.FechaInicio.AddHours(-5);

                SeguimientoFiltroFinalDTO filtroSeguimientoFinal = new SeguimientoFiltroFinalDTO();

                if (filtro.Asesores.Count() > 0)
                {
                    filtroSeguimientoFinal.Asesores = string.Join(",", filtro.Asesores);
                }
                if (filtro.FasesOportunidad.Count() > 0)
                {
                    filtroSeguimientoFinal.FasesOportunidad = string.Join(",", filtro.FasesOportunidad);
                }
                if (filtro.CentroCostos.Count() > 0)
                {
                    filtroSeguimientoFinal.CentroCostos = string.Join(",", filtro.CentroCostos);
                }
                filtroSeguimientoFinal.FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);
                filtroSeguimientoFinal.FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0);

                var data = _unitOfWork.ReportesRepository.ObtenerReporteSeguimientoFRC(filtroSeguimientoFinal);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtro2"></param>
        /// <returns> Lista DTO: List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> - data </returns>
        public List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> GenerarReporteEstadoAlumnosPagos(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtro2)
        {
            try
            {
                filtro2.FechaInicioMatricula = filtro2.FechaInicioMatricula.Value.AddHours(-5);
                filtro2.FechaFinMatricula = filtro2.FechaFinMatricula.Value.AddHours(-5);
                filtro2.FechaInicioAsignacion = filtro2.FechaInicioAsignacion.Value.AddHours(-5);
                filtro2.FechaFinAsignacion = filtro2.FechaFinAsignacion.Value.AddHours(-5);

                filtro2.FechaInicioMatricula = new DateTime(filtro2.FechaInicioMatricula.Value.Year, filtro2.FechaInicioMatricula.Value.Month, filtro2.FechaInicioMatricula.Value.Day, 0, 0, 0);
                filtro2.FechaFinMatricula = new DateTime(filtro2.FechaFinMatricula.Value.Year, filtro2.FechaFinMatricula.Value.Month, filtro2.FechaFinMatricula.Value.Day, 23, 59, 59);
                filtro2.FechaInicioAsignacion = new DateTime(filtro2.FechaInicioAsignacion.Value.Year, filtro2.FechaInicioAsignacion.Value.Month, filtro2.FechaInicioAsignacion.Value.Day, 0, 0, 0);
                filtro2.FechaFinAsignacion = new DateTime(filtro2.FechaFinAsignacion.Value.Year, filtro2.FechaFinAsignacion.Value.Month, filtro2.FechaFinAsignacion.Value.Day, 23, 59, 59);

                var data = _unitOfWork.ReportesRepository.GenerarReporteEstadoAlumnosPagos(filtro, filtro2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Miguel Quiñones
        /// Fecha: 28/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtro2"></param>
        /// <returns> Lista DTO: List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> - data </returns>
        public List<ReporteIndicadoresOperativosDTO> GenerarReporteIndicadoresOperativos(string filtros, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                filtros2.FechaInicio = filtros2.FechaInicio.Value.AddHours(-5);
                filtros2.FechaFin = filtros2.FechaFin.Value.AddHours(-5);


                filtros2.FechaInicio = new DateTime(filtros2.FechaInicio.Value.Year, filtros2.FechaInicio.Value.Month, filtros2.FechaInicio.Value.Day, 0, 0, 0);
                filtros2.FechaFin = new DateTime(filtros2.FechaFin.Value.Year, filtros2.FechaFin.Value.Month, filtros2.FechaFin.Value.Day, 23, 59, 59);

                var data = _unitOfWork.ReportesRepository.GenerarReporteIndicadoresOperativos(filtros, filtros2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Miguel Quiñones
        /// Fecha: 28/04/2023
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> GenerarReporteIndicadoresOperativosPorDiaCoordinadora(string filtros, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                //filtros2.FechaInicio = filtros2.FechaInicio.Value.AddHours(-5);
                //filtros2.FechaFin = filtros2.FechaFin.Value.AddHours(-5);


                filtros2.FechaInicio = new DateTime(filtros2.FechaInicio.Value.Year, filtros2.FechaInicio.Value.Month, filtros2.FechaInicio.Value.Day, 0, 0, 0);
                filtros2.FechaFin = new DateTime(filtros2.FechaFin.Value.Year, filtros2.FechaFin.Value.Month, filtros2.FechaFin.Value.Day, 23, 59, 59);

                var data = _unitOfWork.ReportesRepository.GenerarReporteIndicadoresOperativosPorDiaCoordinadora(filtros, filtros2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// Autor: Miguel Quiñones
        /// Fecha: 28/04/2023
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO> GenerarReporteIndicadoresOperativosPorDiaCoordinadoraAgrupadoVersion2(IGrouping<string, ReporteIndicadoresOperativosPorDiaCoorinadorDTO> item)
        {
            try
            {
                var temporal = item;

                var agrupado = temporal.GroupBy(x => x.Dia)
                .Select(g => new ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO
                {
                    Coordinadora = temporal.Key,
                    Dia = g.Key,
                    Detalle = g.Select(y => new ReporteIndicadoresOperativosPorDiaCoorinadorDTO
                    {
                        Dia = y.Dia,
                        Coordinadora = y.Coordinadora,
                        Estado = y.Estado,
                        Total = y.Total
                    }).ToList()
                });
                return agrupado.ToList();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// Autor: Miguel Quiñones
        /// Fecha: 28/04/2023
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public IEnumerable<ReporteIndicadoresOperativosAgrupadoDTO> GenerarReporteIndicadoresOperativosAgrupado(List<ReporteIndicadoresOperativosDTO> respuestaGeneral)
        {
            try
            {
                IEnumerable<ReporteIndicadoresOperativosAgrupadoDTO> agrupado = null;


                agrupado = respuestaGeneral.GroupBy(x => x.Coordinadora)
                .Select(g => new ReporteIndicadoresOperativosAgrupadoDTO
                {
                    Coordinadora = g.Key,
                    Detalle = g.Select(y => new ReporteIndicadoresOperativosDTO
                    {
                        Coordinadora = y.Coordinadora,
                        Estado = y.Estado,
                        Total = y.Total
                    }).ToList()
                });

                return agrupado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="respuestaGeneral"></param>
        /// <returns> DTO: IEnumerable<ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO> - agrupado </returns>
        public IEnumerable<ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoPagos(List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> respuestaGeneral)
        {
            try
            {
                IEnumerable<ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO> agrupado = null;
                agrupado = respuestaGeneral.GroupBy(x => x.Coordinadora)
                .Select(g => new ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO
                {
                    Coordinadora = g.Key,
                    Detalle = g.Select(y => new ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO
                    {
                        Coordinadora = y.Coordinadora,
                        Tipo = y.Tipo,
                        EstadoMatricula = y.EstadoMatricula,
                        SubEstadoMatricula = y.SubEstadoMatricula,
                        Total = y.Total
                    }).ToList()
                });
                return agrupado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtro2"></param>
        /// <returns> Lista DTO: List<ReporteAvanceAcademicoPresencialOnlineDTO> - data </returns>
        public List<ReporteAvanceAcademicoPresencialOnlineDTO> GenerarReporteEstadoAlumnos2(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtro2)
        {
            try
            {
                filtro2.FechaInicioMatricula = filtro2.FechaInicioMatricula.Value.AddHours(-5);
                filtro2.FechaFinMatricula = filtro2.FechaFinMatricula.Value.AddHours(-5);
                filtro2.FechaInicioAsignacion = filtro2.FechaInicioAsignacion.Value.AddHours(-5);
                filtro2.FechaFinAsignacion = filtro2.FechaFinAsignacion.Value.AddHours(-5);

                filtro2.FechaInicioMatricula = new DateTime(filtro2.FechaInicioMatricula.Value.Year, filtro2.FechaInicioMatricula.Value.Month, filtro2.FechaInicioMatricula.Value.Day, 0, 0, 0);
                filtro2.FechaFinMatricula = new DateTime(filtro2.FechaFinMatricula.Value.Year, filtro2.FechaFinMatricula.Value.Month, filtro2.FechaFinMatricula.Value.Day, 23, 59, 59);
                filtro2.FechaInicioAsignacion = new DateTime(filtro2.FechaInicioAsignacion.Value.Year, filtro2.FechaInicioAsignacion.Value.Month, filtro2.FechaInicioAsignacion.Value.Day, 0, 0, 0);
                filtro2.FechaFinAsignacion = new DateTime(filtro2.FechaFinAsignacion.Value.Year, filtro2.FechaFinAsignacion.Value.Month, filtro2.FechaFinAsignacion.Value.Day, 23, 59, 59);

                var data = _unitOfWork.ReportesRepository.GenerarReporteEstadoAlumnos2(filtro, filtro2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="respuestaGeneral"></param>
        /// <returns> DTO: IEnumerable<ReporteEstadoAlumnosAgrupadoDTO> - agrupado </returns>
        public IEnumerable<ReporteEstadoAlumnosAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoAonline(List<ReporteAvanceAcademicoPresencialOnlineDTO> respuestaGeneral)
        {
            try
            {
                IEnumerable<ReporteEstadoAlumnosAgrupadoDTO> agrupado = null;
                agrupado = respuestaGeneral.Where(w => w.Tipo == "Aonline").GroupBy(x => x.Coordinadora)
                .Select(g => new ReporteEstadoAlumnosAgrupadoDTO
                {
                    Coordinadora = g.Key,
                    Detalle = g.Select(y => new ReporteAvanceAcademicoPresencialOnlineDTO
                    {
                        Coordinadora = y.Coordinadora,
                        Tipo = y.Tipo,
                        Estado = y.Estado,
                        Total = y.Total
                    }).ToList()
                });
                return agrupado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtro2"></param>
        /// <returns> Lista DTO: List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> - data </returns>
        public List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> GenerarReporteEstadoAlumnosAvanceAcademicoVSPagos(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtro2)
        {
            try
            {
                filtro2.FechaInicioMatricula = filtro2.FechaInicioMatricula.Value.AddHours(-5);
                filtro2.FechaFinMatricula = filtro2.FechaFinMatricula.Value.AddHours(-5);
                filtro2.FechaInicioAsignacion = filtro2.FechaInicioAsignacion.Value.AddHours(-5);
                filtro2.FechaFinAsignacion = filtro2.FechaFinAsignacion.Value.AddHours(-5);

                filtro2.FechaInicioMatricula = new DateTime(filtro2.FechaInicioMatricula.Value.Year, filtro2.FechaInicioMatricula.Value.Month, filtro2.FechaInicioMatricula.Value.Day, 0, 0, 0);
                filtro2.FechaFinMatricula = new DateTime(filtro2.FechaFinMatricula.Value.Year, filtro2.FechaFinMatricula.Value.Month, filtro2.FechaFinMatricula.Value.Day, 23, 59, 59);
                filtro2.FechaInicioAsignacion = new DateTime(filtro2.FechaInicioAsignacion.Value.Year, filtro2.FechaInicioAsignacion.Value.Month, filtro2.FechaInicioAsignacion.Value.Day, 0, 0, 0);
                filtro2.FechaFinAsignacion = new DateTime(filtro2.FechaFinAsignacion.Value.Year, filtro2.FechaFinAsignacion.Value.Month, filtro2.FechaFinAsignacion.Value.Day, 23, 59, 59);

                var data = _unitOfWork.ReportesRepository.GenerarReporteEstadoAlumnosAvanceAcademicoVSPagos(filtro, filtro2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="respuestaGeneral"></param>
        /// <returns> DTO: IEnumerable<ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO> - agrupado </returns>
        public IEnumerable<ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoAonlineAvanceAcademicoVSPagos(List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> respuestaGeneral)
        {
            try
            {
                IEnumerable<ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO> agrupado = null;
                agrupado = respuestaGeneral.Where(w => w.Tipo == "Aonline").GroupBy(x => x.Coordinadora)
                .Select(g => new ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO
                {
                    Coordinadora = g.Key,
                    Detalle = g.Select(y => new ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO
                    {
                        Coordinadora = y.Coordinadora,
                        Tipo = y.Tipo,
                        EstadoAcademico = y.EstadoAcademico,
                        EstadoPagos = y.EstadoPagos,
                        Total = y.Total
                    }).ToList()
                });
                return agrupado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtro2"></param>
        /// <returns> Lista DTO: List<ReporteAvanceAcademicoAlumnosPagosAtrasados> - data </returns>
        public List<ReporteAvanceAcademicoAlumnosPagosAtrasados> GenerarReporteEstadoAlumnosPagosAtrasados(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtro2)
        {
            try
            {
                filtro2.FechaInicioMatricula = filtro2.FechaInicioMatricula.Value.AddHours(-5);
                filtro2.FechaFinMatricula = filtro2.FechaFinMatricula.Value.AddHours(-5);
                filtro2.FechaInicioAsignacion = filtro2.FechaInicioAsignacion.Value.AddHours(-5);
                filtro2.FechaFinAsignacion = filtro2.FechaFinAsignacion.Value.AddHours(-5);

                filtro2.FechaInicioMatricula = new DateTime(filtro2.FechaInicioMatricula.Value.Year, filtro2.FechaInicioMatricula.Value.Month, filtro2.FechaInicioMatricula.Value.Day, 0, 0, 0);
                filtro2.FechaFinMatricula = new DateTime(filtro2.FechaFinMatricula.Value.Year, filtro2.FechaFinMatricula.Value.Month, filtro2.FechaFinMatricula.Value.Day, 23, 59, 59);
                filtro2.FechaInicioAsignacion = new DateTime(filtro2.FechaInicioAsignacion.Value.Year, filtro2.FechaInicioAsignacion.Value.Month, filtro2.FechaInicioAsignacion.Value.Day, 0, 0, 0);
                filtro2.FechaFinAsignacion = new DateTime(filtro2.FechaFinAsignacion.Value.Year, filtro2.FechaFinAsignacion.Value.Month, filtro2.FechaFinAsignacion.Value.Day, 23, 59, 59);

                var data = _unitOfWork.ReportesRepository.GenerarReporteEstadoAlumnosPagosAtrasados(filtro, filtro2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="respuestaGeneral"></param>
        /// <returns> DTO: IEnumerable<ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO> - agrupado </returns>
        public IEnumerable<ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoAonlineAlumnosPagosAtrasados(List<ReporteAvanceAcademicoAlumnosPagosAtrasados> respuestaGeneral)
        {
            try
            {
                IEnumerable<ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO> agrupado = null;
                agrupado = respuestaGeneral.GroupBy(x => x.Coordinadora)
                .Select(g => new ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO
                {
                    Coordinadora = g.Key,
                    Detalle = g.Select(y => new ReporteAvanceAcademicoAlumnosPagosAtrasados
                    {
                        Coordinadora = y.Coordinadora,
                        Tipo = y.Tipo,
                        Estado = y.Estado,
                        NumeroAlumnos = y.NumeroAlumnos,
                        NumeroCuotasAtrasadas = y.NumeroCuotasAtrasadas,
                        MontoTotalCuotasAtrasadas = y.MontoTotalCuotasAtrasadas
                    }).ToList()
                });
                return agrupado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera reporte completo del estado de pago de los alumnos.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ReporteEstadosAlumnosDTO GenerarReporteEstadoAlumnos(ReporteTasaConversionConsolidadaFiltroDTO filtro)
        {
            try
            {
                ReporteService reporteService = new ReporteService(_unitOfWork);
                PersonalService personalService = new PersonalService(_unitOfWork);
                ReporteEstadosAlumnosDTO reporte = new ReporteEstadosAlumnosDTO();

                if (filtro.Coordinadores.Count() == 0)
                {
                    var asistentesCargo = personalService.ObtenerPersonalAsignadoOperacionesTotalV2(filtro.Personal.Value);
                    List<int> ListaCoordinadortmp = new List<int>();
                    foreach (var item in asistentesCargo)
                    {
                        ListaCoordinadortmp.Add(item.Id);
                    }
                    filtro.Coordinadores = ListaCoordinadortmp;
                    string coordinadores = ListIntToString(filtro.Coordinadores);
                    //PAGOS//PRESENCIAL-ONLINE-AONLINE
                    var RespuestaEstadoAlumnosGeneral = reporteService.GenerarReporteEstadoAlumnosPagos(coordinadores, filtro).ToList();
                    var RespuestaEstadoAlumnosAgrupado = reporteService.GenerarReporteEstadoAlumnoAgrupadoPagos(RespuestaEstadoAlumnosGeneral);

                    //AONLINE
                    var RespuestaEstadoAlumnosAonlineGeneral = reporteService.GenerarReporteEstadoAlumnos2(coordinadores, filtro).ToList();
                    var RespuestaEstadoAlumnosAonlineAgrupado = reporteService.GenerarReporteEstadoAlumnoAgrupadoAonline(RespuestaEstadoAlumnosAonlineGeneral);

                    //AONLINE//ACADEMICOVSPAGOS
                    var RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosGeneral = reporteService.GenerarReporteEstadoAlumnosAvanceAcademicoVSPagos(coordinadores, filtro).ToList();
                    var RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosAgrupado = reporteService.GenerarReporteEstadoAlumnoAgrupadoAonlineAvanceAcademicoVSPagos(RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosGeneral);

                    //PRESENCIAL-ONLINE-AONLINE//ALUMNOSPAGOSATRASADOS
                    var RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosGeneral = reporteService.GenerarReporteEstadoAlumnosPagosAtrasados(coordinadores, filtro).ToList();
                    var RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosAgrupado = reporteService.GenerarReporteEstadoAlumnoAgrupadoAonlineAlumnosPagosAtrasados(RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosGeneral);

                    reporte.ReporteAvanceAcademicoPagos = RespuestaEstadoAlumnosAgrupado;
                    reporte.ReporteAvanceAcademicoAonline = RespuestaEstadoAlumnosAonlineAgrupado;
                    reporte.ReporteAvanceAcademicoVSPagosAonline = RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosAgrupado;
                    reporte.ReporteAvanceAcademicoAlumnosPagoAtrasado = RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosAgrupado;
                }
                else
                {
                    string coordinadores = ListIntToString(filtro.Coordinadores);
                    //PAGOS//PRESENCIAL-ONLINE-AONLINE
                    var RespuestaEstadoAlumnosGeneral = reporteService.GenerarReporteEstadoAlumnosPagos(coordinadores, filtro).ToList();
                    var RespuestaEstadoAlumnosAgrupado = reporteService.GenerarReporteEstadoAlumnoAgrupadoPagos(RespuestaEstadoAlumnosGeneral);

                    //AONLINE
                    var RespuestaEstadoAlumnosAonlineGeneral = reporteService.GenerarReporteEstadoAlumnos2(coordinadores, filtro).ToList();
                    var RespuestaEstadoAlumnosAonlineAgrupado = reporteService.GenerarReporteEstadoAlumnoAgrupadoAonline(RespuestaEstadoAlumnosAonlineGeneral);

                    //AONLINE//ACADEMICOVSPAGOS
                    var RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosGeneral = reporteService.GenerarReporteEstadoAlumnosAvanceAcademicoVSPagos(coordinadores, filtro).ToList();
                    var RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosAgrupado = reporteService.GenerarReporteEstadoAlumnoAgrupadoAonlineAvanceAcademicoVSPagos(RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosGeneral);

                    //PRESENCIAL-ONLINE-AONLINE//ALUMNOSPAGOSATRASADOS
                    var RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosGeneral = reporteService.GenerarReporteEstadoAlumnosPagosAtrasados(coordinadores, filtro).ToList();
                    var RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosAgrupado = reporteService.GenerarReporteEstadoAlumnoAgrupadoAonlineAlumnosPagosAtrasados(RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosGeneral);

                    reporte.ReporteAvanceAcademicoPagos = RespuestaEstadoAlumnosAgrupado;
                    reporte.ReporteAvanceAcademicoAonline = RespuestaEstadoAlumnosAonlineAgrupado;
                    reporte.ReporteAvanceAcademicoVSPagosAonline = RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosAgrupado;
                    reporte.ReporteAvanceAcademicoAlumnosPagoAtrasado = RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosAgrupado;
                }
                return reporte;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera reporte completo del estado de pago de los alumnos.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ReporteIndicadoresOperativosFinalDTO GenerarReporteIndicadoresOperativosService(ReporteTasaConversionConsolidadaFiltroDTO filtro)
        {
            try
            {
                ReporteService reportes = new ReporteService(_unitOfWork);
                PersonalService personalService = new PersonalService(_unitOfWork);

                //Reportes reportes = new Reportes();
                ReporteIndicadoresOperativosFinalDTO reporte = new ReporteIndicadoresOperativosFinalDTO();

                if (filtro.Coordinadores.Count() == 0)
                {
                    var asistentesCargo = personalService.ObtenerPersonalAsignadoOperacionesTotal(filtro.Personal.Value);
                    List<int> ListaCoordinadortmp = new List<int>();
                    foreach (var item in asistentesCargo)
                    {
                        ListaCoordinadortmp.Add(item.Id);
                    }
                    filtro.Coordinadores = ListaCoordinadortmp;
                    string _coordinadores = ListIntToString(filtro.Coordinadores);

                    //General
                    var RespuestaIndicadoresOperativos = reportes.GenerarReporteIndicadoresOperativos(_coordinadores, filtro).ToList();
                    var RespuestaIndicadoresOperativosAgrupado = reportes.GenerarReporteIndicadoresOperativosAgrupado(RespuestaIndicadoresOperativos);

                    //Por Dia Coordinadora
                    var RespuestaIndicadoresOperativosPorDiaCoordinadora = reportes.GenerarReporteIndicadoresOperativosPorDiaCoordinadora(_coordinadores, filtro).ToList();

                    var coordinadores = RespuestaIndicadoresOperativosPorDiaCoordinadora.GroupBy(x => x.Coordinadora);
                    var RespuestaIndicadoresOperativosAgrupadoPorDia = new List<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO>();
                    var ListaCoordinadoras = new List<String>();
                    foreach (var item in coordinadores)
                    {
                        RespuestaIndicadoresOperativosAgrupadoPorDia.AddRange(reportes.GenerarReporteIndicadoresOperativosPorDiaCoordinadoraAgrupadoVersion2(item));
                        ListaCoordinadoras.Add(item.Key);
                    }
                    reporte.ReporteIndicadoresOperativos = RespuestaIndicadoresOperativosAgrupado;
                    reporte.ReporteIndicadoresOperativosAgrupadoPorDia = RespuestaIndicadoresOperativosAgrupadoPorDia;
                    reporte.Coordinadoras = ListaCoordinadoras;
                }
                else
                {
                    string _coordinadores = ListIntToString(filtro.Coordinadores);
                    //General
                    var RespuestaIndicadoresOperativos = reportes.GenerarReporteIndicadoresOperativos(_coordinadores, filtro).ToList();
                    var RespuestaIndicadoresOperativosAgrupado = reportes.GenerarReporteIndicadoresOperativosAgrupado(RespuestaIndicadoresOperativos);

                    //Por Dia Coordinadora
                    var RespuestaIndicadoresOperativosPorDiaCoordinadora = reportes.GenerarReporteIndicadoresOperativosPorDiaCoordinadora(_coordinadores, filtro).ToList();

                    var coordinadores = RespuestaIndicadoresOperativosPorDiaCoordinadora.GroupBy(x => x.Coordinadora);
                    var RespuestaIndicadoresOperativosAgrupadoPorDia = new List<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO>();
                    var ListaCoordinadoras = new List<String>();
                    foreach (var item in coordinadores)
                    {
                        RespuestaIndicadoresOperativosAgrupadoPorDia.AddRange(reportes.GenerarReporteIndicadoresOperativosPorDiaCoordinadoraAgrupadoVersion2(item));
                        ListaCoordinadoras.Add(item.Key);
                    }
                    reporte.ReporteIndicadoresOperativos = RespuestaIndicadoresOperativosAgrupado;
                    reporte.ReporteIndicadoresOperativosAgrupadoPorDia = RespuestaIndicadoresOperativosAgrupadoPorDia;
                    reporte.Coordinadoras = ListaCoordinadoras;
                }


                return reporte;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public object GenerarReporteDisponibilidadCuota(FiltroReportePagoDTO filtro)
        {
            try
            {
                var reportesRepositorio = _unitOfWork.ReportesRepository;
                var repFeriadoRep = _unitOfWork.FeriadoRepository;
                var repPeriodo = _unitOfWork.PeriodoRepository;
                var result = reportesRepositorio.ObtenerReportePagoAlumnos(filtro).ToList();
                var ListFeriados = repFeriadoRep.Obtener();

                DateTime? FechaDisponibleOriginal = null;
                DateTime? FechaIngresoEnCuentaOriginal = null;
                foreach (var item in result)
                {
                    FechaDisponibleOriginal = item.FechaDisponible;
                    FechaIngresoEnCuentaOriginal = item.FechaDepositaron;

                    if (item.CuentaFeriados == false && item.ConsiderarDiasHabilesLV == false && item.ConsiderarDiasHabilesLS == false)
                    {
                        item.FechaDepositaron = item.FechaPagoReal;
                        item.FechaDisponible = item.FechaPagoReal;
                        item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(item.DiasDeposito);
                        item.FechaDisponible = item.FechaDisponible.Value.AddDays(item.DiasDisponible);
                    }
                    else
                    {
                        //deposito
                        if (item.DiasDeposito == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaDepositaron = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && item.ConsiderarDiasHabilesLS == true) || ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && item.ConsiderarDiasHabilesLV == true))
                                    {
                                        item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaDepositaron = item.FechaPagoReal;
                            while (item.DiasDeposito > 0)
                            {
                                item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }
                                else
                                {
                                    if (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsiderarDiasHabilesLV == true && (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsiderarDiasHabilesLS == true && item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                            item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                        else
                                            item.DiasDeposito = item.DiasDeposito - 1;
                                    }
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }

                            }
                        }

                        //disponible
                        if (item.DiasDisponible == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaDisponible = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && item.ConsiderarDiasHabilesLS == true) || ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && item.ConsiderarDiasHabilesLV == true))
                                    {
                                        item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaDisponible = item.FechaPagoReal;
                            while (item.DiasDisponible > 0)
                            {
                                item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDisponible = item.DiasDisponible - 1;
                                }
                                else
                                {
                                    if (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsiderarDiasHabilesLV == true && (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsiderarDiasHabilesLS == true && item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                            item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                        else
                                            item.DiasDisponible = item.DiasDisponible - 1;
                                    }
                                    else
                                        item.DiasDisponible = item.DiasDisponible - 1;
                                }

                            }
                        }

                    }

                    if (DateTime.Now.Date >= item.FechaDisponible.Value.Date)
                        item.EstadoEfectivo = "Disponible";
                    else
                        if (DateTime.Now.Date >= item.FechaDepositaron.Value.Date)
                        item.EstadoEfectivo = "Depositado";
                    item.FechaDisponible = FechaDisponibleOriginal == null ? item.FechaDisponible : FechaDisponibleOriginal;
                    item.FechaDepositaron = FechaIngresoEnCuentaOriginal == null ? item.FechaDepositaron : FechaIngresoEnCuentaOriginal;
                }

                return result.OrderByDescending(x => x.FechaProcesoPago);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por asesor
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<TCRM_CentroCostoByAsesorDetallesDTO> ObtenerCentroCostoPorAsesorDetalles(ReporteTasaConversionConsolidadaFiltroDTO Filtros)
        {
            try
            {
                var _repReportes = _unitOfWork.ReportesRepository;

                string area = ListIntToString(Filtros.Areas);
                string subarea = ListIntToString(Filtros.SubAreas);
                string pgeneral = ListIntToString(Filtros.PGeneral);
                string pespecifico = ListIntToString(Filtros.PEspecifico);
                string modalidades = ListStringToString(Filtros.Modalidades);
                string ciudades = ListStringToString(Filtros.Ciudades);
                string coordinadores = ListIntToString(Filtros.Coordinadores);
                string asesores = ListIntToString(Filtros.Asesores);
                Filtros.FechaInicio = Convert.ToDateTime(Filtros.FechaInicio).Date;
                Filtros.FechaFin = Convert.ToDateTime(Filtros.FechaFin).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                List<TCRM_CentroCostoByAsesorDetallesDTO> item = new List<TCRM_CentroCostoByAsesorDetallesDTO>();
                return _repReportes.ObtenerCentroCostoPorAsesorDetalles(area, subarea, pgeneral, pespecifico, modalidades, ciudades, Filtros.Fecha, coordinadores, asesores, Filtros.FechaInicio.Value, Filtros.FechaFin.Value);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Reporte de Devoluciones
        /// </summary>
        /// <param name="FiltroDevoluciones"></param>
        /// <returns> Lista DTO - List<ReporteDevolucionDTO> - reporte </returns>
        public ReporteDevolucionesCompuestoDTO ObtenerReporteDevoluciones(ReporteDevolucionesFiltroDTO FiltroDevoluciones)
        {
            try
            {
                var reporteDevolucion = _unitOfWork.ReportesRepository.ObtenerReporteDevoluciones(FiltroDevoluciones);
                var agrupado = (from p in reporteDevolucion
                                group p by p.PeriodoPorFechaVencimiento into grupo
                                select new ReporteDevolucion { g = grupo.Key, l = grupo.ToList() });

                var ConMontos = FiltroDevoluciones.FechaInicioCronograma == null || FiltroDevoluciones.FechaFinCronograma == null ? false : true;

                ReporteDevolucionesCompuestoDTO reporte = new ReporteDevolucionesCompuestoDTO();
                reporte.ReporteDevolucionAgrupado = agrupado.ToList();
                reporte.Cronograma = ConMontos;
                reporte.ReporteDevoluciones = reporteDevolucion;

                return reporte;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Congela Reporte de Devoluciones
        /// </summary>
        /// <param name="FechaCongelamiento"></param>
        /// <param name="Usuario"></param>
        /// <returns> Lista DTO - List<ReporteDevolucionDTO> - reporte </returns>
        public int CongelarReporteDeDevoluciones(DateTime FechaCongelamiento, string Usuario)
        {
            try
            {
                return _unitOfWork.ReportesRepository.CongelarReporteDeDevoluciones(FechaCongelamiento, Usuario);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte FLUJO
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO - List<ReporteDevolucionDTO> - reporte </returns>
        public ReporteFlujoFinalDTO ObtenerReporteFlujos(FiltroFechaDTO filtro)
        {
            try
            {
                var reporte = _unitOfWork.ReportesRepository.ObtenerReporteFlujos(filtro);
                byte[] registroByte;
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Hoja 1");

                    string[] columnHeaders = {
                        "Código de Programa", "Estado de Programa", "Estado de Finanzas", "Estado Matricula",
                        "Subestado de Matricula", "Alumno", "Pais Alumno", "Fecha Vencimiento Original",
                        "Monto Cuota Original", "Monto Modificado", "Fecha Vencimiento Actual", "Monto Cuota Actual",
                        "Fecha Pago", "Monto Pagado", "Saldo Pendiente", "Mora", "Nro Cuota", "Moneda",
                        "Total Cuota Dolar", "Real Pago Dolar", "Saldo Pendiente Dolar", "Origen Programa",
                        "Código Matricula", "Paquete", "Email", "Tel Fijo", "Tel Cel", "DNI", "Dirección",
                        "Documento Pago", "Razón Social", "Coordinadora Academica", "Coordinadora Cobranza"
                    };

                    for (int i = 0; i < columnHeaders.Length; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = columnHeaders[i];
                    }

                    int currentRow = 2;

                    foreach (var item in reporte)
                    {
                        worksheet.Cell(currentRow, 1).Value = item.codigo;
                        worksheet.Cell(currentRow, 2).Value = item.EstadoP;
                        worksheet.Cell(currentRow, 3).Value = item.EstadoFinanzas;
                        worksheet.Cell(currentRow, 4).Value = item.EstadoMatricula;
                        worksheet.Cell(currentRow, 5).Value = item.SubEstadoMatricula;
                        worksheet.Cell(currentRow, 6).Value = item.Alumno;
                        worksheet.Cell(currentRow, 7).Value = item.PaisAlumno;
                        worksheet.Cell(currentRow, 8).Value = item.fechavencimientoOriginal?.Date;
                        worksheet.Cell(currentRow, 9).Value = item.cuotaOriginal;
                        worksheet.Cell(currentRow, 10).Value = item.Modificacion;
                        worksheet.Cell(currentRow, 11).Value = item.fechavencimiento.Date;
                        worksheet.Cell(currentRow, 12).Value = item.cuota;
                        worksheet.Cell(currentRow, 13).Value = item.FechaPago?.Date;
                        worksheet.Cell(currentRow, 14).Value = item.montopagado;
                        worksheet.Cell(currentRow, 15).Value = item.saldopendiente;
                        worksheet.Cell(currentRow, 16).Value = item.mora;
                        worksheet.Cell(currentRow, 17).Value = item.nrocuota;
                        worksheet.Cell(currentRow, 18).Value = item.moneda;
                        worksheet.Cell(currentRow, 19).Value = item.TotalCuotaD;
                        worksheet.Cell(currentRow, 20).Value = item.RealPagoD;
                        worksheet.Cell(currentRow, 21).Value = item.SaldoPendienteD;
                        worksheet.Cell(currentRow, 22).Value = item.OrigenPrograma;
                        worksheet.Cell(currentRow, 23).Value = item.CodigoMatricula;
                        worksheet.Cell(currentRow, 24).Value = item.Paquete;
                        worksheet.Cell(currentRow, 25).Value = item.Email;
                        worksheet.Cell(currentRow, 26).Value = item.TelFijo;
                        worksheet.Cell(currentRow, 27).Value = item.TelCel;
                        worksheet.Cell(currentRow, 28).Value = item.Dni;
                        worksheet.Cell(currentRow, 29).Value = item.Direccion;
                        worksheet.Cell(currentRow, 30).Value = item.DocumentoPago;
                        worksheet.Cell(currentRow, 31).Value = item.RazonSocial;
                        worksheet.Cell(currentRow, 32).Value = item.Coordinadoraacademica;
                        worksheet.Cell(currentRow, 33).Value = item.Coordinadoracobranza;
                        currentRow++;
                    }


                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        registroByte = stream.ToArray();
                    }
                }



                ReporteFlujoFinalDTO resultado = new ReporteFlujoFinalDTO();
                resultado.reporteFlujo = reporte;
                resultado.dataExport = registroByte;

                return resultado;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<ReportePagosDTO> ObtenerReportePagos(FiltroFechaDTO filtro)
        {
            try
            {
                return _unitOfWork.ReportesRepository.ObtenerReportePagos(filtro);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los pagos por asistente
        /// </summary>
        /// <returns></returns>
        public List<ReportePagoPorAsistenteDTO> ObtenerReportePagoPorAsistente(filtroReportePagoPorAsistenteDTO filtro)
        {
            try
            {
                return _unitOfWork.ReportesRepository.ObtenerReportePagoPorAsistente(filtro);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los documentos pendientes (comprobantes) de pago
        /// </summary>
        /// <returns></returns>
        public List<ReporteDocumentosPendientesPagoDTO> ObtenerReporteDocumentosPendientesPago()
        {
            try
            {
                return _unitOfWork.ReportesRepository.ObtenerReporteDocumentosPendientesPago();

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Congela el reporte FLujo por Dia
        /// </summary>
        /// <param name="FechaCongelamiento"></param>
        ///  <param name="Usuario"></param>
        /// <returns> Lista DTO - List<ReporteDevolucionDTO> - reporte </returns>
        public int CongelarReporteDeFlujoPorDia(DateTime FechaCongelamiento, string Usuario)
        {
            try
            {
                return _unitOfWork.ReportesRepository.CongelarReporteDeFlujoPorDia(FechaCongelamiento, Usuario);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Congela el reporte FLujo por periodo
        /// </summary>
        /// <param name="IdPeriodo"></param>
        ///  <param name="Usuario"></param>
        /// <returns> Lista DTO - List<ReporteDevolucionDTO> - reporte </returns>
        public int CongelarReporteDeFlujoPorPeriodo(string Usuario, int IdPeriodo)
        {
            try
            {
                return _unitOfWork.ReportesRepository.CongelarReporteDeFlujoPorPeriodo(Usuario, IdPeriodo);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Congela el reporte Originales por ida
        /// </summary>
        /// <param name="FechaCongelamiento"></param>
        ///  <param name="Usuario"></param>
        /// <returns> Lista DTO - List<ReporteDevolucionDTO> - reporte </returns>
        public int CongelarReporteOriginalesPorDia(DateTime FechaCongelamiento, string Usuario)
        {
            try
            {
                return _unitOfWork.ReportesRepository.CongelarReporteOriginalesPorDia(FechaCongelamiento, Usuario);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte Control de documentos.
        /// </summary>
        /// <param name="filtroControlDocumentos"></param>
        /// <returns> Lista DTO - List<ReporteDevolucionDTO> - reporte </returns>
        public List<ReporteControlDocumentosDTO> ObtenerReporteControlDocumentos(ReporteControlDocumentosFiltroDTO filtroControlDocumentos)
        {
            try
            {
                var reporteControlDocumentos = _unitOfWork.ReportesRepository.ObtenerReporteControlDocumentos(filtroControlDocumentos);
                foreach (var item in reporteControlDocumentos)
                {
                    var documentos = item.Documentos;
                    var ArrayDocumentos = documentos.Split(';');

                    var campoDocumentoMap = new Dictionary<string, string>
                    {
                        {"1", "Cronograma"},
                        {"2", "Convenio"},
                        {"3", "Pagare"},
                        {"4", "Carta_Autorizacion"},
                        {"5", "Hoja_Requisitos"},
                        {"6", "Orden_compra"},
                        {"7", "Carta_compromiso"},
                        {"8", "DNI"}
                    };

                    ArrayDocumentos.Select(doc =>
                    {
                        var docInfo = doc.Split(',');
                        if (docInfo.Length >= 4)
                        {
                            var IdDocumento = docInfo[1];
                            var estadoDoc = docInfo[3];

                            if (campoDocumentoMap.TryGetValue(IdDocumento, out var fieldName))
                            {
                                item.GetType().GetProperty(fieldName)?.SetValue(item, estadoDoc.Equals("1") ? 1 : 0);
                            }
                        }

                        return doc;

                        return doc;
                    }).ToList();
                }

                return reporteControlDocumentos;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        ///Autor: Griselberto Huaman
        ///Fecha: 07/02/2022
        /// <summary>
        /// Genera el reporte de seguimiento de comisiones [ejecuta el store procedure: [fin].[SP_GenerarReporteComisionPersonal]
        /// </summary>
        /// <param name="filtro"> filtro </param>
        /// <returns>List<ReporteComisionesDTO></returns>
        public List<ReporteComisionesDTO> ObtenerReporteComisiones(FiltroReporteComisionDTO filtro)
        {
            try
            {
                return _unitOfWork.ReportesRepository.ObtenerReporteComisiones(filtro);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        ///Autor: Griselberto Huaman
        ///Fecha: 07/02/2022
        /// <summary>
        /// Actualiza el estado de las comisiones a pagado
        /// </summary>
        /// <returns>Arreglo de ReporteComisionesDTO</returns>
        public bool ActualizarReporteComisiones(FiltroGenerarCierreDTO filtro)
        {
            try
            {
                return _unitOfWork.ReportesRepository.ActualizarReporteComisiones(filtro.FechaInicio, filtro.FechaFin);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        ///Autor: Griselberto Huaman
        ///Fecha: 07/02/2022
        /// <summary>
        /// Obtiene el reporte de Pagos 
        /// </summary>
        /// <returns>Arreglo de ReportePagosACuentaDTO</returns>
        public List<ReportePagosACuentaDTO> ObtenerReportePagosACuenta(string Anios)
        {
            try
            {
                return _unitOfWork.ReportesRepository.ObtenerReportePagosACuenta(Anios);

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Generar Reporte de contactabilidad para Comercial 3cx alterno
        /// </summary>
        /// <param name="filtros"> Filtros de busqueda </param>
        /// <returns> objeto : ReporteContactabilidadDataV2DTO</returns>
        public List<ReporteLlamadaEntranteDTO> ObtenerReporteLlamadaEntrante(FiltroReporteLlamadaEntranteDTO filtros)
        {
            try
            {
                var filtroOrdenado = new FiltroReporteLlamadaEntranteDTO();
                filtroOrdenado.Asesores = filtros.Asesores;
                filtroOrdenado.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtroOrdenado.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);
                var esHoy = (DateTime.Compare(filtroOrdenado.FechaInicio, fechaActual) == 0);
                var data = _unitOfWork.ReportesRepository.ObtenerReporteLlamadaEntrante(filtroOrdenado);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
