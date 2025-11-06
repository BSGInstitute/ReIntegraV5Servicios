using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using iText.Kernel.Geom;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReportesRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 22/09/2022
    /// <summary>
    /// Gestión general de Reportes
    /// </summary>
    public class ReporteRepository : IReporteRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 22/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ObtenerReporteTasaContacto(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<PreReporteTasaContactoDTO> datos = new List<PreReporteTasaContactoDTO>();
                ReporteTasaContactoDTO item = new ReporteTasaContactoDTO();
                var queryEjecutadasConLlamadas = string.Empty;
                var queryTotal_TotalEjecutadas = string.Empty;

                queryTotal_TotalEjecutadas = $@"
                    SELECT COUNT(*) AS CantidadTotal, 
                           ISNULL(SUM(CASE
                                   WHEN IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                                   THEN 1
                                   ELSE 0
                               END), 0) AS CantidadTotalEjecutada
                    FROM com.V_ReporteTasaContacto
                    WHERE EstadoOcurrencia = 1
                          AND EstadoOportunidad = 1
                          AND EstadoActividad = 1
                          AND (IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                               OR IdEstadoOcurrencia = @idEstadoOcurrenciaNoEjecutado)
                          AND IdFaseOportunidad != @idFaseOportunidadE
                          AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                          AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                          AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC','AutomatizacionRN2') {filtros.Filtro}
                  ";

                queryEjecutadasConLlamadas = $@"
                    SELECT COUNT(*) AS Valor
                    FROM com.V_ReporteTasaContactoLlamada
                    WHERE EstadoOcurrencia = 1
                          AND EstadoOportunidad = 1
                          AND EstadoActividad = 1
                          AND IdFaseOportunidad NOT IN (4, 11,27)
                          AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                          AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                          AND IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                          AND IdLlamada = 1
                          AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC', 'AutomatizacionRN2') {filtros.Filtro}
                ";

                const int idEstadoOcurrenciaEjecutado = 1;
                const int idEstadoOcurrenciaNoEjecutado = 2;
                const int idFaseOportunidadE = 11;
                var respuestaDapperTotal_TotalEjecutadas = _dapperRepository.FirstOrDefault(queryTotal_TotalEjecutadas,
                    new
                    {
                        idEstadoOcurrenciaEjecutado,
                        idEstadoOcurrenciaNoEjecutado,
                        idFaseOportunidadE,
                        filtros.FechaInicio,
                        filtros.FechaFin
                    });
                var respuestaDapperEjecutadasLlamada = _dapperRepository.FirstOrDefault(queryEjecutadasConLlamadas, new
                {
                    idEstadoOcurrenciaEjecutado,
                    idFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(respuestaDapperTotal_TotalEjecutadas) && !respuestaDapperTotal_TotalEjecutadas.Contains("[]"))
                {
                    var datosTotal = JsonConvert.DeserializeObject<TasaContactoEjecutadoDTO>(respuestaDapperTotal_TotalEjecutadas)!;
                    var datosEjecutadasLlamada = JsonConvert.DeserializeObject<IntDTO>(respuestaDapperEjecutadasLlamada)!;

                    item.TotalLlamadas = datosTotal.CantidadTotal;
                    item.TotalLlamadasEjecutadas = datosTotal.CantidadTotalEjecutada;
                    item.TotalLlamadasEjecutadasConLlamada = datosEjecutadasLlamada.Valor.GetValueOrDefault();
                }
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 22/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public async Task<ReporteTasaContactoDTO> ObtenerReporteTasaContactoAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                const int idEstadoOcurrenciaEjecutado = EstadoOcurrencia.Ejecutado;
                const int idEstadoOcurrenciaNoEjecutado = EstadoOcurrencia.NoEjecutado;
                const int idFaseOportunidadE = FaseOportunidad.E;
                var queryTotalEjecutadas = $@"
                    SELECT COUNT(*) AS CantidadTotal, 
                           ISNULL(SUM(CASE
                                   WHEN IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                                   THEN 1
                                   ELSE 0
                               END), 0) AS CantidadTotalEjecutada
                    FROM com.V_ReporteTasaContacto
                    WHERE EstadoOcurrencia = 1
                          AND EstadoOportunidad = 1
                          AND EstadoActividad = 1
                          AND (IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                               OR IdEstadoOcurrencia = @idEstadoOcurrenciaNoEjecutado)
                          AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                          AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                          AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC','AutomatizacionRN2') {filtros.Filtro}
                  ";
                //AND IdFaseOportunidad != @idFaseOportunidadE
                var task1 = _dapperRepository.FirstOrDefaultAsync(queryTotalEjecutadas,
                new
                {
                    idEstadoOcurrenciaEjecutado,
                    idEstadoOcurrenciaNoEjecutado,
                    idFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                var queryEjecutadasConLlamadas = $@"
                    SELECT COUNT(*) AS Valor
                    FROM com.V_ReporteTasaContactoLlamada
                    WHERE EstadoOcurrencia = 1
                          AND EstadoOportunidad = 1
                          AND EstadoActividad = 1
                          AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                          AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                          AND IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                          AND IdLlamada = 1
                          AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC', 'AutomatizacionRN2') {filtros.Filtro}
                ";
                //AND IdFaseOportunidad NOT IN (4, 11,27)
                var task2 = _dapperRepository.FirstOrDefaultAsync(queryEjecutadasConLlamadas, new
                {
                    idEstadoOcurrenciaEjecutado,
                    idFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                var respuestaDapperTotal_TotalEjecutadas = await task1;
                var respuestaDapperEjecutadasLlamada = await task2;

                ReporteTasaContactoDTO rpta = new ReporteTasaContactoDTO();

                if (!string.IsNullOrEmpty(respuestaDapperTotal_TotalEjecutadas) && !respuestaDapperTotal_TotalEjecutadas.Contains("[]"))
                {
                    var datosTotal = JsonConvert.DeserializeObject<TasaContactoEjecutadoDTO>(respuestaDapperTotal_TotalEjecutadas)!;
                    var datosEjecutadasLlamada = JsonConvert.DeserializeObject<IntDTO>(respuestaDapperEjecutadasLlamada)!;

                    rpta.TotalLlamadas = datosTotal.CantidadTotal;
                    rpta.TotalLlamadasEjecutadas = datosTotal.CantidadTotalEjecutada;
                    rpta.TotalLlamadasEjecutadasConLlamada = datosEjecutadasLlamada.Valor.GetValueOrDefault();
                }
                return rpta;
            }
            catch (Exception e)

            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para el reporte de tasa de contacto
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public async Task<ReporteTasaContactoDTO> ObtenerReporteTasaContactoTresCxAsync(ReporteCambioFaseFiltroProcesadoDTO filtros, bool esHoy)
        {
            try
            {
                string vista = esHoy ? "com.V_ReporteTasaContacto" : "com.V_ReporteTasaContactoCongelado";
                string vistaEjecutadas = esHoy ? "com.V_ReporteTasaContactoLlamadaTresCx" : "com.V_ReporteTasaContactoLlamadaCongeladoTresCx";
                const int idEstadoOcurrenciaEjecutado = EstadoOcurrencia.Ejecutado;
                const int idEstadoOcurrenciaNoEjecutado = EstadoOcurrencia.NoEjecutado;
                const int idFaseOportunidadE = FaseOportunidad.E;
                var queryTotalEjecutadas = $@"
                    SELECT COUNT(*) AS CantidadTotal, 
                           ISNULL(SUM(CASE
                                   WHEN IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                                   THEN 1
                                   ELSE 0
                               END), 0) AS CantidadTotalEjecutada
                    FROM {vista} 
                    WHERE EstadoOcurrencia = 1
                        AND EstadoOportunidad = 1
                        AND EstadoActividad = 1
                        AND (IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                            OR IdEstadoOcurrencia = @idEstadoOcurrenciaNoEjecutado)
                        AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                        AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                        AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC','AutomatizacionRN2') 
                        {filtros.Filtro}
                  ";
                //AND IdFaseOportunidad != @idFaseOportunidadE
                var task1 = _dapperRepository.FirstOrDefaultAsync(queryTotalEjecutadas,
                new
                {
                    idEstadoOcurrenciaEjecutado,
                    idEstadoOcurrenciaNoEjecutado,
                    idFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                var queryEjecutadasConLlamadas = $@"
                    SELECT COUNT(*) AS Valor
                    FROM {vistaEjecutadas} 
                    WHERE EstadoOcurrencia = 1
                        AND EstadoOportunidad = 1
                        AND EstadoActividad = 1
                        AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                        AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                        AND IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                        AND IdLlamada = 1
                        AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC', 'AutomatizacionRN2') 
                        {filtros.Filtro}
                ";
                //AND IdFaseOportunidad NOT IN (4, 11,27)
                var task2 = _dapperRepository.FirstOrDefaultAsync(queryEjecutadasConLlamadas, new
                {
                    idEstadoOcurrenciaEjecutado,
                    idFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                var respuestaDapperTotal_TotalEjecutadas = await task1;
                var respuestaDapperEjecutadasLlamada = await task2;

                ReporteTasaContactoDTO rpta = new ReporteTasaContactoDTO();

                if (!string.IsNullOrEmpty(respuestaDapperTotal_TotalEjecutadas) && respuestaDapperTotal_TotalEjecutadas != "null")
                {
                    var datosTotal = JsonConvert.DeserializeObject<TasaContactoEjecutadoDTO>(respuestaDapperTotal_TotalEjecutadas)!;
                    var datosEjecutadasLlamada = JsonConvert.DeserializeObject<IntDTO>(respuestaDapperEjecutadasLlamada)!;

                    rpta.TotalLlamadas = datosTotal.CantidadTotal;
                    rpta.TotalLlamadasEjecutadas = datosTotal.CantidadTotalEjecutada;
                    rpta.TotalLlamadasEjecutadasConLlamada = datosEjecutadasLlamada.Valor.GetValueOrDefault();
                }
                return rpta;
            }
            catch (Exception e)

            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para el reporte de tasa de contacto
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public async Task<ReporteTasaContactoDTO> ObtenerReporteTasaContactoTresCxV2Async(ReporteCambioFaseFiltroProcesadoDTO filtros, bool esHoy)
        {
            try
            {
                //string vista = esHoy ? "com.V_ReporteTasaContacto" : "com.V_ReporteTasaContactoCongelado";
                //string vistaEjecutadas = esHoy ? "com.V_ReporteTasaContactoLlamadaTresCx" : "com.V_ReporteTasaContactoLlamadaCongeladoTresCx";

                string vista = esHoy ? "com.V_ReporteTasaContactoV2_Resumido" : "com.V_ReporteTasaContactoCongeladoV2_Resumido";
                string vistaEjecutadas = esHoy ? "com.V_ReporteTasaContactoLlamadaTresCxV2_Resumido" : "com.V_ReporteTasaContactoLlamadaCongeladoTresCxV2_Resumido";
                const int idEstadoOcurrenciaEjecutado = EstadoOcurrencia.Ejecutado;
                const int idEstadoOcurrenciaNoEjecutado = EstadoOcurrencia.NoEjecutado;
                const int idFaseOportunidadE = FaseOportunidad.E;
                var queryTotalEjecutadas = $@"
                    SELECT COUNT(*) AS CantidadTotal, 
                           ISNULL(SUM(CASE
                                   WHEN IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado and IdOcurrencia not in (431)
                                   THEN 1
                                   ELSE 0
                               END), 0) AS CantidadTotalEjecutada,
                            ISNULL(SUM(CASE
                                    WHEN IdOcurrencia in (149, 162, 163, 164, 165, 168, 207, 209) THEN 1 
                                    ELSE 0
                                END),0) AS CantidadTotalManual,
                            ISNULL(SUM(CASE WHEN IdOcurrencia in(431) THEN 1 ELSE 0 END),0) AS CantidadTotalContestaCorta
                    FROM {vista} 
                    WHERE EstadoOcurrencia = 1
                        AND EstadoOportunidad = 1
                        AND EstadoActividad = 1
                        AND OtroMedio = 0
                        AND (IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                            OR IdEstadoOcurrencia = @idEstadoOcurrenciaNoEjecutado)
                        AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                        AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                        AND Llamada != 0
                        AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC','AutomatizacionRN2') {filtros.Filtro}
                  ";
                //AND IdFaseOportunidad != @idFaseOportunidadE
                var task1 = _dapperRepository.FirstOrDefaultAsync(queryTotalEjecutadas,
                new
                {
                    idEstadoOcurrenciaEjecutado,
                    idEstadoOcurrenciaNoEjecutado,
                    idFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                var queryEjecutadasConLlamadas = $@"
                    SELECT COUNT(*) AS Valor
                    FROM {vistaEjecutadas} 
                    WHERE EstadoOcurrencia = 1
                        AND EstadoOportunidad = 1
                        AND EstadoActividad = 1
                        AND OtroMedio = 0
                        AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                        AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                        AND IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                        AND Llamada != 0
                        AND IdOcurrencia not in (431)
                        AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC', 'AutomatizacionRN2') {filtros.Filtro}
                ";
                //AND IdFaseOportunidad NOT IN (4, 11,27)
                var task2 = _dapperRepository.FirstOrDefaultAsync(queryEjecutadasConLlamadas, new
                {
                    idEstadoOcurrenciaEjecutado,
                    idFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                var respuestaDapperTotal_TotalEjecutadas = await task1;
                var respuestaDapperEjecutadasLlamada = await task2;

                ReporteTasaContactoDTO rpta = new ReporteTasaContactoDTO();

                if (!string.IsNullOrEmpty(respuestaDapperTotal_TotalEjecutadas) && !respuestaDapperTotal_TotalEjecutadas.Contains("[]"))
                {
                    var datosTotal = JsonConvert.DeserializeObject<TasaContactoEjecutadoDTO>(respuestaDapperTotal_TotalEjecutadas)!;
                    var datosEjecutadasLlamada = JsonConvert.DeserializeObject<IntDTO>(respuestaDapperEjecutadasLlamada)!;

                    rpta.TotalLlamadas = datosTotal.CantidadTotal;
                    rpta.TotalLlamadasEjecutadas = datosTotal.CantidadTotalEjecutada;
                    rpta.TotalLlamadasManual = datosTotal.CantidadTotalManual;
                    rpta.TotalLlamadasContestaCorta = datosTotal.CantidadTotalContestaCorta;
                    rpta.TotalLlamadasEjecutadasConLlamada = datosEjecutadasLlamada.Valor.GetValueOrDefault();
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para el reporte de tasa de contacto
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public async Task<ReporteTasaContactoDTO> ObtenerReporteTasaContactoTresCxOtroMedioAsync(ReporteCambioFaseFiltroProcesadoDTO filtros, bool esHoy)
        {
            try
            {
                //string vista = esHoy ? "com.V_ReporteTasaContacto" : "com.V_ReporteTasaContactoCongelado";
                //string vistaEjecutadas = esHoy ? "com.V_ReporteTasaContactoLlamadaTresCx" : "com.V_ReporteTasaContactoLlamadaCongeladoTresCx";
                string vista = esHoy ? "com.V_ReporteTasaContactoV2_V2" : "com.V_ReporteTasaContactoCongeladoV2_V2";
                string vistaEjecutadas = esHoy ? "com.V_ReporteTasaContactoLlamadaTresCxV2_V2" : "com.V_ReporteTasaContactoLlamadaCongeladoTresCxV2_V2";
                const int idEstadoOcurrenciaEjecutado = EstadoOcurrencia.Ejecutado;
                const int idEstadoOcurrenciaNoEjecutado = EstadoOcurrencia.NoEjecutado;
                const int idFaseOportunidadE = FaseOportunidad.E;
                var queryTotalEjecutadas = $@"
                    SELECT COUNT(*) AS CantidadTotal, 
                           ISNULL(SUM(CASE
                                   WHEN IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                                   THEN 1
                                   ELSE 0
                               END), 0) AS CantidadTotalEjecutada,
                            ISNULL(SUM(CASE
                                    WHEN IdOcurrencia in (149, 162, 163, 164, 165, 168, 207, 209) THEN 1 
                                    ELSE 0
                                END),0) AS CantidadTotalManual
                    FROM {vista} 
                    WHERE EstadoOcurrencia = 1
                        AND EstadoOportunidad = 1
                        AND EstadoActividad = 1
                        AND OtroMedio = 1
                        AND (IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                            OR IdEstadoOcurrencia = @idEstadoOcurrenciaNoEjecutado)
                        AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                        AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                        AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC','AutomatizacionRN2') {filtros.Filtro}
                  ";
                //AND IdFaseOportunidad != @idFaseOportunidadE
                var task1 = _dapperRepository.FirstOrDefaultAsync(queryTotalEjecutadas,
                new
                {
                    idEstadoOcurrenciaEjecutado,
                    idEstadoOcurrenciaNoEjecutado,
                    idFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                var queryEjecutadasConLlamadas = $@"
                    SELECT COUNT(*) AS Valor
                    FROM {vistaEjecutadas} 
                    WHERE EstadoOcurrencia = 1
                        AND EstadoOportunidad = 1
                        AND EstadoActividad = 1
                        AND OtroMedio = 1
                        AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                        AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                        AND IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                        AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC', 'AutomatizacionRN2') {filtros.Filtro}
                ";
                //AND IdFaseOportunidad NOT IN (4, 11,27)
                var task2 = _dapperRepository.FirstOrDefaultAsync(queryEjecutadasConLlamadas, new
                {
                    idEstadoOcurrenciaEjecutado,
                    idFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                var respuestaDapperTotal_TotalEjecutadas = await task1;
                var respuestaDapperEjecutadasLlamada = await task2;

                ReporteTasaContactoDTO rpta = new ReporteTasaContactoDTO();

                if (!string.IsNullOrEmpty(respuestaDapperTotal_TotalEjecutadas) && !respuestaDapperTotal_TotalEjecutadas.Contains("[]"))
                {
                    var datosTotal = JsonConvert.DeserializeObject<TasaContactoEjecutadoDTO>(respuestaDapperTotal_TotalEjecutadas)!;
                    var datosEjecutadasLlamada = JsonConvert.DeserializeObject<IntDTO>(respuestaDapperEjecutadasLlamada)!;

                    rpta.TotalLlamadas = datosTotal.CantidadTotal;
                    rpta.TotalLlamadasEjecutadas = datosTotal.CantidadTotalEjecutada;
                    rpta.TotalLlamadasManual = datosTotal.CantidadTotalManual;
                    rpta.TotalLlamadasEjecutadasConLlamada = datosEjecutadasLlamada.Valor.GetValueOrDefault();
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de cambio de fase BIC y acumulado
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public IEnumerable<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteCambiosDeFaseControlBICYEAcumulado(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                IEnumerable<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                const int idFaseOportunidadBIC = 4;
                const int idFaseOportunidadE = 11;
                var listaFaseOportunidadExcluir = new List<int>(){
                     idFaseOportunidadBIC,
                     idFaseOportunidadE,
                    36
                    };
                var query1 = $@"
                        SELECT COUNT(Cambio) AS NumeroRegistros, 
                               FaseOrigen, 
                               FaseDestino, 
                               0.0 MetaLanzamiento, 
                               0 IndicadorLanzamiento, 
                               '' TipoDato
                        FROM com.V_ReporteCambiosDeFaseOportunidad3
                        WHERE Estado = 1
                              AND FechaLog BETWEEN @FechaInicio AND @FechaFin {filtros.Filtro} 
                              AND IdFaseOrigen != IdFaseDestino
                              AND IdFaseOportunidadLog IN @listaFaseOportunidadExcluir
                        GROUP BY FaseOrigen, 
                                 FaseDestino";

                var query2 = $@"
                        SELECT Cambio, 
                               FaseOrigen
                        FROM com.V_ReporteCambiosDeFaseOportunidad3
                        WHERE Estado = 1
                              AND FechaLog BETWEEN @FechaInicio AND @FechaFin {filtros.Filtro}
                              AND IdFaseOrigen != IdFaseDestino
                              AND IdFaseOportunidadLog IN @listaFaseOportunidadExcluir
                        ";
                var queryRespuesta1 = _dapperRepository.QueryDapper(query1, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    listaFaseOportunidadExcluir
                });
                var queryRespuesta2 = _dapperRepository.QueryDapper(query2, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    listaFaseOportunidadExcluir
                });
                if (!string.IsNullOrEmpty(queryRespuesta1) && !queryRespuesta1.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<IEnumerable<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta1)!;
                    var lista = JsonConvert.DeserializeObject<IEnumerable<CambiosDeFaseControlBICYEDTO>>(queryRespuesta2)!;
                    foreach (var item in items)
                    {
                        var indicadorLanzamiento = lista.Where(x => x.FaseOrigen == item.FaseOrigen).Count();
                        item.MetaLanzamiento = (item.NumeroRegistros * 100) / indicadorLanzamiento;
                    }
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de cambio de fase BIC y acumulado
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public async Task<IEnumerable<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseControlBICYEAcumuladoAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                IEnumerable<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                const int idFaseOportunidadBIC = 4;
                const int idFaseOportunidadE = 11;
                var listaFaseOportunidadExcluir = new List<int>(){
                     idFaseOportunidadBIC,
                     idFaseOportunidadE,
                    36 //Fase ISM
                    };
                var query1 = $@"
                        SELECT COUNT(Cambio) AS NumeroRegistros, 
                               FaseOrigen, 
                               FaseDestino, 
                               0.0 MetaLanzamiento, 
                               0 IndicadorLanzamiento, 
                               '' TipoDato
                        FROM com.V_ReporteCambiosDeFaseOportunidad3
                        WHERE Estado = 1
                              AND FechaLog BETWEEN @FechaInicio AND @FechaFin {filtros.Filtro} 
                              AND IdFaseOrigen != IdFaseDestino
                              AND IdFaseOportunidadLog IN @listaFaseOportunidadExcluir
                        GROUP BY FaseOrigen, 
                                 FaseDestino";

                var query2 = $@"
                        SELECT Cambio, 
                               FaseOrigen
                        FROM com.V_ReporteCambiosDeFaseOportunidad3
                        WHERE Estado = 1
                              AND FechaLog BETWEEN @FechaInicio AND @FechaFin {filtros.Filtro}
                              AND IdFaseOrigen != IdFaseDestino
                              AND IdFaseOportunidadLog IN @listaFaseOportunidadExcluir
                        ";
                var tarea1 = _dapperRepository.QueryDapperAsync(query1, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    listaFaseOportunidadExcluir
                });
                var tarea2 = _dapperRepository.QueryDapperAsync(query2, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    listaFaseOportunidadExcluir
                });
                var queryRespuesta1 = await tarea1;
                var queryRespuesta2 = await tarea2;
                if (!string.IsNullOrEmpty(queryRespuesta1) && !queryRespuesta1.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<IEnumerable<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta1)!;
                    var lista = JsonConvert.DeserializeObject<IEnumerable<CambiosDeFaseControlBICYEDTO>>(queryRespuesta2);
                    foreach (var item in items)
                    {
                        var indicadorLanzamiento = lista.Where(x => x.FaseOrigen == item.FaseOrigen).Count();
                        item.MetaLanzamiento = (item.NumeroRegistros * 100) / indicadorLanzamiento;
                    }
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de calidad procesamiento
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objeto DTO : List<ReporteCalidadProcesamientoDTO> </returns>
        public List<ReporteCalidadProcesamientoDTO> ObtenerReporteCalidadProcesamiento(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string filtro = "";
                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdPersonal in (" + String.Join(",", filtros.Asesores) + ")";
                }
                List<ReporteCalidadProcesamientoDTO> items = new List<ReporteCalidadProcesamientoDTO>();
                var query = $@"
                    SELECT NombreFase AS DatosAsesor,
                           AVG(PromedioBeneficios) AS PromedioBeneficios, 
                           AVG(PromedioCompetidores) AS PromedioCompetidores, 
                           AVG(PromedioHistorialFinanciero) AS PromedioHistorialFinanciero, 
                           AVG(PromedioPerfil) AS PromedioPerfil, 
                           AVG(PromedioPEspecifico) AS PromedioPEspecifico, 
                           AVG(PromedioPGeneral) AS PromedioPGeneral, 
                           AVG(PromedioProblemaSeleccionados) AS PromedioProblemaSeleccionados, 
                           AVG(PromedioProblemaSolucionados) AS PromedioProblemaSolucionados
                    FROM com.V_ReporteCalidadProcesamiento
                    WHERE Fecha BETWEEN @FechaInicio AND @FechaFin  {filtro} 
                    GROUP BY NombreFase,
                             DatosAsesor,
                             IdFaseOportunidad,
                             IdPersonal";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCalidadProcesamientoDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el total de IS
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> int </returns>
        public int ObtenerReporteMetasObtenerTotalIS(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string filtro = "";
                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdPersonalAsignado in (" + String.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdCentroCosto in (" + String.Join(",", filtros.Asesores) + ")";
                }
                int total = 0;
                var query = @"
                    SELECT COUNT(Id) AS Cantidad
                    FROM com.V_GenerarReporteMetasGetTotalIS
                    WHERE FechaLog BETWEEN @FechaInicio AND @FechaFin
                            AND Estado = 1
                            AND IdFaseOportunidad = @IdFaseOportunidadIS
                            AND IdFaseOportunidad <> IdFaseOportunidadAnt 
                    " + filtro;
                int idFaseOportunidadIS = 5;
                var queryRespuesta = _dapperRepository.FirstOrDefault(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    idFaseOportunidadIS //ValorEstatico.IdFaseOportunidadIS
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    var result = JsonConvert.DeserializeObject<ReporteMetasTotalISDTO>(queryRespuesta);
                    total = result.Cantidad;
                }
                return total;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el total de IS
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> int </returns>
        public async Task<int> ObtenerReporteMetasObtenerTotalISAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string filtro = "";
                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdPersonalAsignado in (" + String.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdCentroCosto in (" + String.Join(",", filtros.Asesores) + ")";
                }
                int total = 0;
                var query = @"
                    SELECT COUNT(Id) AS Cantidad
                    FROM com.V_GenerarReporteMetasGetTotalIS
                    WHERE FechaLog BETWEEN @FechaInicio AND @FechaFin
                            AND Estado = 1
                            AND IdFaseOportunidad = @IdFaseOportunidadIS
                            AND IdFaseOportunidad <> IdFaseOportunidadAnt 
                    " + filtro;
                int idFaseOportunidadIS = 5;
                var queryRespuesta = await _dapperRepository.FirstOrDefaultAsync(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    idFaseOportunidadIS //ValorEstatico.IdFaseOportunidadIS
                });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    var result = JsonConvert.DeserializeObject<ReporteMetasTotalISDTO>(queryRespuesta)!;
                    total = result.Cantidad;
                }
                return total;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las transiciones de una fase a otra en general de las oportunidades 
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objetos DTO : List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteCambiosDeFaseOportunidadAcumulado(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                            Count(Cambio) AS NumeroRegistros,
                            FaseOrigen,
                            FaseDestino,
                            0.0 MetaLanzamiento,
                            0 IndicadorLanzamiento, 
                            '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidad WHERE Estado = 1 and 
                            FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro +
                            " and   IdFaseOrigen != IdFaseDestino  GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene todas las transiciones de una fase a otra de las oportunidades segun el ultimo log
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objetos DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteCambiosDeFaseOportunidadNoAcumulado(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                            Count(Cambio) AS NumeroRegistros,
                            FaseOrigen,
                            FaseDestino,
                            0.0 MetaLanzamiento,
                            0 IndicadorLanzamiento, 
                            '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidad WHERE Numero = 1 and Estado = 1 and 
                            FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                            IdFaseOrigen != IdFaseDestino 
                            GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene los datos para generar el reporte de Cambios de fase Rn2
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ObtenerReporteTasaContactoRn2(ReporteCambioFaseFiltroProcedimientoDTO filtros)
        {
            try
            {
                ReporteTasaContactoDTO item = new ReporteTasaContactoDTO();
                string queryTasaContactoRn2 = _dapperRepository.QuerySPFirstOrDefault("com.SP_ObtenertasaContactoRn2", filtros);
                if (!string.IsNullOrEmpty(queryTasaContactoRn2))
                {
                    item = JsonConvert.DeserializeObject<ReporteTasaContactoDTO>(queryTasaContactoRn2);
                }
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para generar el reporte de Cambios de fase Rn2
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public async Task<ReporteTasaContactoDTO> ObtenerReporteTasaContactoRn2Async(ReporteCambioFaseFiltroProcedimientoDTO filtros)
        {
            try
            {
                ReporteTasaContactoDTO item = new ReporteTasaContactoDTO();
                var queryTasaContactoRn2 = await _dapperRepository.QuerySPFirstOrDefaultAsync("com.SP_ObtenertasaContactoRn2", filtros);
                if (!string.IsNullOrEmpty(queryTasaContactoRn2))
                {
                    item = JsonConvert.DeserializeObject<ReporteTasaContactoDTO>(queryTasaContactoRn2);
                }
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de control de cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtros de Búsqueda </param>
        /// <returns> Lista de objeto DTO: List<ControlCambiodeFaseV2DTO> </returns>
        public List<ControlCambioDeFaseV2DTO> ObtenerReporteControlCambiodeFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var resultado = new List<ControlCambioDeFaseV2DTO>();

                string asesores = null;
                string centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                {
                    asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    centroCostos = String.Join(",", filtros.Asesores);
                }

                var query = "com.SP_ControldeActividades";
                var queryRespuesta = _dapperRepository.QuerySPDapper(query, new
                {
                    asesores,
                    centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ControlCambioDeFaseV2DTO>>(queryRespuesta);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de control de cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtros de Búsqueda </param>
        /// <returns> Lista de objeto DTO: List<ControlCambiodeFaseV2DTO> </returns>
        public async Task<List<ControlCambioDeFaseV2DTO>> ObtenerReporteControlCambiodeFaseAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var resultado = new List<ControlCambioDeFaseV2DTO>();

                string asesores = null;
                string centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                {
                    asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    centroCostos = String.Join(",", filtros.Asesores);
                }

                var query = "com.SP_ControldeActividades";
                var queryRespuesta = await _dapperRepository.QuerySPDapperAsync(query, new
                {
                    asesores,
                    centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ControlCambioDeFaseV2DTO>>(queryRespuesta);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de control de cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtros de Búsqueda </param>
        /// <returns> Lista de objeto DTO: List<ControlCambiodeFaseV2DTO> </returns>
        public async Task<List<ControlCambioDeFaseV2DTO>> ObtenerReporteControlCambiodeFaseTresCxAsync(ReporteCambioFaseFiltrosDTO filtros, bool esHoy)
        {
            try
            {
                var resultado = new List<ControlCambioDeFaseV2DTO>();

                string? asesores = null;
                string? centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                    asesores = string.Join(",", filtros.Asesores);
                if (filtros.CentroCostos.Count() > 0)
                    centroCostos = string.Join(",", filtros.Asesores);

                var query = esHoy ? "com.SP_ControldeActividadesTresCx" : "com.SP_ControldeActividadesCongeladoTresCx";
                var queryRespuesta = await _dapperRepository.QuerySPDapperAsync(query, new
                {
                    asesores,
                    centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ControlCambioDeFaseV2DTO>>(queryRespuesta)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de control de cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtros de Búsqueda </param>
        /// <returns> Lista de objeto DTO: List<ControlCambiodeFaseV2DTO> </returns>
        public async Task<List<ControlCambioDeFaseV2DTO>> ObtenerReporteControlCambiodeFaseTresCxV2Async(ReporteCambioFaseFiltrosDTO filtros, bool esHoy)
        {
            try
            {
                var resultado = new List<ControlCambioDeFaseV2DTO>();

                string? asesores = null;
                string? centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                    asesores = string.Join(",", filtros.Asesores);
                if (filtros.CentroCostos.Count() > 0)
                    centroCostos = string.Join(",", filtros.Asesores);

                var query = esHoy ? "com.SP_ControldeActividadesTresCxV2_Resumido" : "com.SP_ControldeActividadesCongeladoTresCxV2_Resumido";
                var queryRespuesta = await _dapperRepository.QuerySPDapperAsync(query, new
                {
                    asesores,
                    centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ControlCambioDeFaseV2DTO>>(queryRespuesta)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Carlos Crispin R.
        /// Fecha: 25/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de control de cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtros de Búsqueda </param>
        /// <returns> Lista de objeto DTO: List<ControlOtroMedioDTO> </returns>
        public async Task<List<ControlOtroMedioDTO>> ObtenerReporteControlCambiodeFaseOtroMedioTresCxV2Async(ReporteCambioFaseFiltrosDTO filtros, bool esHoy)
        {
            try
            {
                var resultado = new List<ControlOtroMedioDTO>();

                string? asesores = null;
                string? centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                    asesores = string.Join(",", filtros.Asesores);
                if (filtros.CentroCostos.Count() > 0)
                    centroCostos = string.Join(",", filtros.Asesores);

                var query = esHoy ? "com.SP_ControldeActividadesTresCxV2_Resumido_OtroMedio" : "com.SP_ControldeActividadesCongeladoTresCxV2_Resumido_OtroMedio";
                var queryRespuesta = await _dapperRepository.QuerySPDapperAsync(query, new
                {
                    asesores,
                    centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ControlOtroMedioDTO>>(queryRespuesta)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public async Task<ReporteTasaContactoDTO> ObtenerReporteTasaContactoCongeladoAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var reporteTasaContacto = new ReporteTasaContactoDTO();

                var queryTotal_TotalEjecutadas = $@"
                    SELECT COUNT(*) AS CantidadTotal, 
                           ISNULL(SUM(CASE
                                   WHEN IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                                   THEN 1
                                   ELSE 0
                               END), 0) AS CantidadTotalEjecutada
                    FROM com.V_ReporteTasaContactoCongelado
                    WHERE EstadoOcurrencia = 1
                          AND EstadoOportunidad = 1
                          AND EstadoActividad = 1
                          AND (IdEstadoOcurrencia = @idEstadoOcurrenciaEjecutado
                               OR IdEstadoOcurrencia = @idEstadoOcurrenciaNoEjecutado)
                          AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                          AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                          AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC','AutomatizacionRN2') {filtros.Filtro}
                  ";
                //AND IdFaseOportunidad != @IdFaseOportunidadE
                var queryEjecutadasConLlamadas = $@"
                    SELECT COUNT(*) AS Valor
                    FROM com.V_ReporteTasaContactoLlamadaCongelado
                    WHERE EstadoOcurrencia = 1
                          AND EstadoOportunidad = 1
                          AND EstadoActividad = 1
                          AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                          AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                          AND IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                          AND IdLlamada = 1
                          AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC', 'AutomatizacionRN2') {filtros.Filtro}
                ";
                const int idEstadoOcurrenciaEjecutado = EstadoOcurrencia.Ejecutado;
                const int idEstadoOcurrenciaNoEjecutado = EstadoOcurrencia.NoEjecutado;

                var tarea1 = _dapperRepository.FirstOrDefaultAsync(queryTotal_TotalEjecutadas, new
                {
                    idEstadoOcurrenciaEjecutado,
                    idEstadoOcurrenciaNoEjecutado,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                var tarea2 = _dapperRepository.FirstOrDefaultAsync(queryEjecutadasConLlamadas, new
                {
                    idEstadoOcurrenciaEjecutado,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                var respuestaDapperTotal_TotalEjecutadas = await tarea1;
                var respuestaDapperEjecutadasLlamada = await tarea2;
                if (!string.IsNullOrEmpty(respuestaDapperTotal_TotalEjecutadas) && respuestaDapperTotal_TotalEjecutadas != "null")
                {
                    var datosTotal = JsonConvert.DeserializeObject<TasaContactoEjecutadoDTO>(respuestaDapperTotal_TotalEjecutadas)!;
                    reporteTasaContacto.TotalLlamadas = datosTotal.CantidadTotal;
                    reporteTasaContacto.TotalLlamadasEjecutadas = datosTotal.CantidadTotalEjecutada;
                }
                if (!string.IsNullOrEmpty(respuestaDapperEjecutadasLlamada) && respuestaDapperEjecutadasLlamada != "null")
                {
                    var datosEjecutadasLlamada = JsonConvert.DeserializeObject<IntDTO>(respuestaDapperEjecutadasLlamada)!;
                    reporteTasaContacto.TotalLlamadasEjecutadasConLlamada = datosEjecutadasLlamada.Valor.GetValueOrDefault();
                }
                return reporteTasaContacto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para generar el reporte de Cambios de fase Rn2
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ObtenerReporteTasaContactoRn2Congelado(ReporteCambioFaseFiltroProcedimientoDTO filtros)
        {
            try
            {
                var item = new ReporteTasaContactoDTO();
                string queryTasaContactoRn2 = _dapperRepository.QuerySPFirstOrDefault("com.SP_ObtenertasaContactoRn2Congelado", filtros);
                if (!string.IsNullOrEmpty(queryTasaContactoRn2))
                {
                    item = JsonConvert.DeserializeObject<ReporteTasaContactoDTO>(queryTasaContactoRn2);
                }
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para generar el reporte de Cambios de fase Rn2
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public async Task<ReporteTasaContactoDTO> ObtenerReporteTasaContactoRn2CongeladoAsync(ReporteCambioFaseFiltroProcedimientoDTO filtros)
        {
            try
            {
                var item = new ReporteTasaContactoDTO();
                var queryTasaContactoRn2 = await _dapperRepository.QuerySPFirstOrDefaultAsync("com.SP_ObtenertasaContactoRn2Congelado", filtros);
                if (!string.IsNullOrEmpty(queryTasaContactoRn2))
                {
                    item = JsonConvert.DeserializeObject<ReporteTasaContactoDTO>(queryTasaContactoRn2);
                }
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de control de cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtros de Búsqueda </param>
        /// <returns> Lista de objeto DTO: List<ControlCambiodeFaseV2DTO> </returns>
        public List<ControlCambioDeFaseV2DTO> ObtenerReporteControlCambiodeFaseCongelado(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var resultado = new List<ControlCambioDeFaseV2DTO>();

                string asesores = null;
                string centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                {
                    asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    centroCostos = String.Join(",", filtros.Asesores);
                }

                var query = "com.SP_ControldeActividadesCongelado";
                var queryRespuesta = _dapperRepository.QuerySPDapper(query, new
                {
                    asesores,
                    centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ControlCambioDeFaseV2DTO>>(queryRespuesta);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de control de cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtros de Búsqueda </param>
        /// <returns> Lista de objeto DTO: List<ControlCambiodeFaseV2DTO> </returns>
        public async Task<List<ControlCambioDeFaseV2DTO>> ObtenerReporteControlCambiodeFaseCongeladoAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var resultado = new List<ControlCambioDeFaseV2DTO>();

                string asesores = null;
                string centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                {
                    asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    centroCostos = String.Join(",", filtros.Asesores);
                }

                var query = "com.SP_ControldeActividadesCongelado";
                var queryRespuesta = await _dapperRepository.QuerySPDapperAsync(query, new
                {
                    asesores,
                    centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ControlCambioDeFaseV2DTO>>(queryRespuesta);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> ObjetoDTO: ReporteTasaContactoConySinLlamadaDTO </returns>
        public async Task<ReporteTasaContactoConySinLlamadaDTO> ObtenerReporteTasaContactoConySinLlamadaAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var rpta = new ReporteTasaContactoConySinLlamadaDTO();
                var rpta2 = new ReporteTasaContactoConySinLlamadaDTO();
                var queryOptimizado = string.Empty;
                queryOptimizado = $@"
                    SELECT 
                        COUNT(*) AS CambiosFaseTotal,
                        ISNULL(SUM(IIF(EstadoLlamada = 1, 1, 0)), 0) AS CambiosFaseConLlamada, 
                        ISNULL(SUM(IIF(EstadoLlamada = 0, 1, 0)), 0) AS CambiosFaseSinLlamada, 
                        0 AS CambiosFaseOCconLlamada, 
                        0 AS CambiosFaseOCsinLlamada 
                    FROM com.V_ReporteTasaContactoConySinLlamada2 
                    WHERE FechaLog BETWEEN @FechaInicio AND @FechaFin 
                        {filtros.Filtro} 
                        AND IdFaseOrigen != IdFaseDestino
                        AND Estado = 1
                        AND Cambio IS NOT NULL 
                        AND IdFaseDestinoCalculado NOT IN (@BIC, @RN, @RN1, @E, @RN5, @BNC1, @OD, @OM, @RN8, @BRM1)";
                //(4, 7, 9, 11, 27, 28, 32, 33, 34,29)

                var task1 = _dapperRepository.FirstOrDefaultAsync(queryOptimizado, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    FaseOportunidad.BIC,
                    FaseOportunidad.RN,
                    FaseOportunidad.RN1,
                    FaseOportunidad.E,
                    FaseOportunidad.RN5,
                    FaseOportunidad.BNC1,
                    FaseOportunidad.OD,
                    FaseOportunidad.OM,
                    FaseOportunidad.RN8,
                    FaseOportunidad.BRM1,
                });

                var queryOptimizadoOC = string.Empty;
                queryOptimizadoOC = $@"
                    SELECT 
                        0 AS CambiosFaseTotal, 
                        0 AS CambiosFaseConLlamada, 
                        0 AS CambiosFaseSinLlamada, 
                        ISNULL(SUM(IIF(EstadoLlamada = 1, 1, 0)), 0) AS CambiosFaseOCconLlamada, 
                        ISNULL(SUM(IIF(EstadoLlamada = 0, 1, 0)), 0) AS CambiosFaseOCsinLlamada 
                    FROM com.V_ReporteTasaContactoConySinLlamada2
                    WHERE FechaLog 
                        BETWEEN @FechaInicio 
                        AND @FechaFin {filtros.Filtro} 
                        AND IdFaseOrigen != IdFaseDestino
                        AND Estado=1 
                        AND IdFaseDestinoCalculado IN (@NI, @IS, @RN3, @RN2_B, @RN2_A, @M, @D, @RN4)";
                //(3, 5, 6, 10, 41, 23, 25, 26)
                var task2 = _dapperRepository.FirstOrDefaultAsync(queryOptimizadoOC, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    FaseOportunidad.NI,
                    FaseOportunidad.IS,
                    FaseOportunidad.RN3,
                    FaseOportunidad.RN2_B,
                    FaseOportunidad.RN2_A,
                    FaseOportunidad.M,
                    FaseOportunidad.D,
                    FaseOportunidad.RN4,
                });

                var resultado1 = await task1;
                if (!string.IsNullOrEmpty(resultado1) && !resultado1.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ReporteTasaContactoConySinLlamadaDTO>(resultado1)!;
                }
                var resultado2 = await task2;
                if (!string.IsNullOrEmpty(resultado2) && !resultado2.Contains("[]"))
                {
                    rpta2 = JsonConvert.DeserializeObject<ReporteTasaContactoConySinLlamadaDTO>(resultado2)!;
                }
                rpta.CambiosFaseOCconLlamada = rpta2.CambiosFaseOCconLlamada;
                rpta.CambiosFaseOCsinLlamada = rpta2.CambiosFaseOCsinLlamada;
                return rpta;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para el reporte de tasa de contacto
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> ReporteTasaContactoConySinLlamadaDTO </returns>
        public async Task<ReporteTasaContactoConySinLlamadaDTO> ObtenerReporteTasaContactoConySinLlamadaTresCxAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var rpta = new ReporteTasaContactoConySinLlamadaDTO();
                var rpta2 = new ReporteTasaContactoConySinLlamadaDTO();
                var queryOptimizado = string.Empty;
                queryOptimizado = $@"
                    SELECT 
                        COUNT(*) AS CambiosFaseTotal,
                        ISNULL(SUM(IIF(EstadoLlamada = 1, 1, 0)), 0) AS CambiosFaseConLlamada, 
                        ISNULL(SUM(IIF(EstadoLlamada = 0, 1, 0)), 0) AS CambiosFaseSinLlamada, 
                        0 AS CambiosFaseOCconLlamada, 
                        0 AS CambiosFaseOCsinLlamada 
                    FROM com.V_ReporteTasaContactoConySinLlamada2TresCx 
                    WHERE FechaLog BETWEEN @FechaInicio AND @FechaFin 
                        {filtros.Filtro} 
                        AND IdFaseOrigen != IdFaseDestino
                        AND Estado = 1
                        AND Cambio IS NOT NULL 
                        AND IdFaseDestinoCalculado NOT IN (@BIC, @RN, @RN1, @E, @RN5, @BNC1, @OD, @OM, @RN8, @BRM1)";
                var task1 = _dapperRepository.FirstOrDefaultAsync(queryOptimizado, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    FaseOportunidad.BIC,
                    FaseOportunidad.RN,
                    FaseOportunidad.RN1,
                    FaseOportunidad.E,
                    FaseOportunidad.RN5,
                    FaseOportunidad.BNC1,
                    FaseOportunidad.OD,
                    FaseOportunidad.OM,
                    FaseOportunidad.RN8,
                    FaseOportunidad.BRM1,
                });

                var queryOptimizadoOC = string.Empty;
                queryOptimizadoOC = $@"
                    SELECT 
                        0 AS CambiosFaseTotal, 
                        0 AS CambiosFaseConLlamada, 
                        0 AS CambiosFaseSinLlamada, 
                        ISNULL(SUM(IIF(EstadoLlamada = 1, 1, 0)), 0) AS CambiosFaseOCconLlamada, 
                        ISNULL(SUM(IIF(EstadoLlamada = 0, 1, 0)), 0) AS CambiosFaseOCsinLlamada 
                    FROM com.V_ReporteTasaContactoConySinLlamada2TresCx
                    WHERE FechaLog 
                        BETWEEN @FechaInicio 
                        AND @FechaFin {filtros.Filtro} 
                        AND IdFaseOrigen != IdFaseDestino
                        AND Estado=1 
                        AND IdFaseDestinoCalculado IN (@NI, @IS, @RN3, @RN2_B, @RN2_A, @M, @D, @RN4)";
                var task2 = _dapperRepository.FirstOrDefaultAsync(queryOptimizadoOC, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    FaseOportunidad.NI,
                    FaseOportunidad.IS,
                    FaseOportunidad.RN3,
                    FaseOportunidad.RN2_B,
                    FaseOportunidad.RN2_A,
                    FaseOportunidad.M,
                    FaseOportunidad.D,
                    FaseOportunidad.RN4,
                });

                var resultado1 = await task1;
                if (!string.IsNullOrEmpty(resultado1) && !resultado1.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ReporteTasaContactoConySinLlamadaDTO>(resultado1)!;
                }
                var resultado2 = await task2;
                if (!string.IsNullOrEmpty(resultado2) && !resultado2.Contains("[]"))
                {
                    rpta2 = JsonConvert.DeserializeObject<ReporteTasaContactoConySinLlamadaDTO>(resultado2)!;
                }
                rpta.CambiosFaseOCconLlamada = rpta2.CambiosFaseOCconLlamada;
                rpta.CambiosFaseOCsinLlamada = rpta2.CambiosFaseOCsinLlamada;
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#RR-ORTCS3cx-001@Error en ObtenerReporteTasaContactoConySinLlamadaTresCxAsync, {ex.Message}");
            }
        }

        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 06/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para el reporte de tasa de contacto
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> ReporteTasaContactoConySinLlamadaDTO </returns>
        public async Task<ReporteTasaContactoConySinLlamadaDTO> ObtenerReporteTasaContactoConySinLlamadaTresCxTotalAsync(ReporteCambioFaseFiltroProcesadoDTO filtros, int estadoSeguimientoWhatsapp)
        {
            try
            {
                var rpta = new ReporteTasaContactoConySinLlamadaDTO();
                var rpta2 = new ReporteTasaContactoConySinLlamadaDTO();
                var queryOptimizado = string.Empty;
                queryOptimizado = $@"
                    SELECT 
                        COUNT(*) AS CambiosFaseTotal,
                        ISNULL(SUM(IIF(EstadoLlamada = 1, 1, 0)), 0) AS CambiosFaseConLlamada, 
                        ISNULL(SUM(IIF(EstadoLlamada = 0, 1, 0)), 0) AS CambiosFaseSinLlamada, 
                        0 AS CambiosFaseOCconLlamada, 
                        0 AS CambiosFaseOCsinLlamada 
                    FROM com.V_ReporteTasaContactoConySinLlamadaTresCxV2_V2 
                    WHERE FechaLog BETWEEN @FechaInicio AND @FechaFin 
                        {filtros.Filtro} 
                        AND IdFaseOrigen != IdFaseDestino
                        AND Estado = 1
                        AND OtroMedio = {estadoSeguimientoWhatsapp}
                        AND Cambio IS NOT NULL 
                        AND IdFaseDestinoCalculado NOT IN (@BIC, @RN, @RN1, @E, @RN5, @BNC1, @OD, @OM, @RN8, @BRM1)";
                var task1 = _dapperRepository.FirstOrDefaultAsync(queryOptimizado, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    FaseOportunidad.BIC,
                    FaseOportunidad.RN,
                    FaseOportunidad.RN1,
                    FaseOportunidad.E,
                    FaseOportunidad.RN5,
                    FaseOportunidad.BNC1,
                    FaseOportunidad.OD,
                    FaseOportunidad.OM,
                    FaseOportunidad.RN8,
                    FaseOportunidad.BRM1,
                });

                var queryOptimizadoOC = string.Empty;
                queryOptimizadoOC = $@"
                    SELECT 
                        0 AS CambiosFaseTotal, 
                        0 AS CambiosFaseConLlamada, 
                        0 AS CambiosFaseSinLlamada, 
                        ISNULL(SUM(IIF(OtroMedio = 1, 1, 0)), 0) AS CambiosFaseOCotroMedio, 
                        ISNULL(SUM(IIF(OtroMedio=0 AND EstadoLlamada = 1, 1, 0)), 0) AS CambiosFaseOCconLlamada, 
                        ISNULL(SUM(IIF(OtroMedio=0 AND EstadoLlamada = 0, 1, 0)), 0) AS CambiosFaseOCsinLlamada  
                    FROM com.V_ReporteTasaContactoConySinLlamadaTresCxV2_V2
                    WHERE FechaLog 
                        BETWEEN @FechaInicio 
                        AND @FechaFin {filtros.Filtro} 
                        AND IdFaseOrigen != IdFaseDestino
                        AND Estado=1 
                        AND IdFaseDestinoCalculado IN (@NI, @IS, @RN3, @RN2_B, @RN2_A, @M, @D, @RN4)";
                var task2 = _dapperRepository.FirstOrDefaultAsync(queryOptimizadoOC, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    FaseOportunidad.NI,
                    FaseOportunidad.IS,
                    FaseOportunidad.RN3,
                    FaseOportunidad.RN2_B,
                    FaseOportunidad.RN2_A,
                    FaseOportunidad.M,
                    FaseOportunidad.D,
                    FaseOportunidad.RN4,
                });

                var resultado1 = await task1;
                if (!string.IsNullOrEmpty(resultado1) && !resultado1.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ReporteTasaContactoConySinLlamadaDTO>(resultado1)!;
                }
                var resultado2 = await task2;
                if (!string.IsNullOrEmpty(resultado2) && !resultado2.Contains("[]"))
                {
                    rpta2 = JsonConvert.DeserializeObject<ReporteTasaContactoConySinLlamadaDTO>(resultado2)!;
                }
                rpta.CambiosFaseOCconLlamada = rpta2.CambiosFaseOCconLlamada;
                rpta.CambiosFaseOCsinLlamada = rpta2.CambiosFaseOCsinLlamada;
                rpta.CambiosFaseOCotroMedio = rpta2.CambiosFaseOCotroMedio;
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#RR-ORTCS3cx-001@Error en ObtenerReporteTasaContactoConySinLlamadaTresCxV2Async, {ex.Message}");
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para el reporte de tasa de contacto
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> ReporteTasaContactoConySinLlamadaDTO </returns>
        public async Task<ReporteTasaContactoConySinLlamadaDTO> ObtenerReporteTasaContactoConySinLlamadaTresCxV2Async(ReporteCambioFaseFiltroProcesadoDTO filtros, int estadoSeguimientoWhatsapp)
        {
            try
            {
                var rpta = new ReporteTasaContactoConySinLlamadaDTO();
                var rpta2 = new ReporteTasaContactoConySinLlamadaDTO();
                var queryOptimizado = string.Empty;
                queryOptimizado = $@"
                    SELECT 
                        COUNT(*) AS CambiosFaseTotal,
                        ISNULL(SUM(IIF(EstadoLlamada = 1, 1, 0)), 0) AS CambiosFaseConLlamada, 
                        ISNULL(SUM(IIF(EstadoLlamada = 0, 1, 0)), 0) AS CambiosFaseSinLlamada, 
                        0 AS CambiosFaseOCconLlamada, 
                        0 AS CambiosFaseOCsinLlamada 
                    FROM com.V_ReporteTasaContactoConySinLlamadaTresCxV2_V2 
                    WHERE FechaLog BETWEEN @FechaInicio AND @FechaFin 
                        {filtros.Filtro} 
                        AND IdFaseOrigen != IdFaseDestino
                        AND Estado = 1
                        AND OtroMedio = {estadoSeguimientoWhatsapp}
                        AND Cambio IS NOT NULL 
                        AND IdFaseDestinoCalculado NOT IN (@BIC, @RN, @RN1, @E, @RN5, @BNC1, @OD, @OM, @RN8, @BRM1)";
                var task1 = _dapperRepository.FirstOrDefaultAsync(queryOptimizado, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    FaseOportunidad.BIC,
                    FaseOportunidad.RN,
                    FaseOportunidad.RN1,
                    FaseOportunidad.E,
                    FaseOportunidad.RN5,
                    FaseOportunidad.BNC1,
                    FaseOportunidad.OD,
                    FaseOportunidad.OM,
                    FaseOportunidad.RN8,
                    FaseOportunidad.BRM1,
                });

                var queryOptimizadoOC = string.Empty;
                queryOptimizadoOC = $@"
                    SELECT 
                        0 AS CambiosFaseTotal, 
                        0 AS CambiosFaseConLlamada, 
                        0 AS CambiosFaseSinLlamada, 
                        ISNULL(SUM(IIF(EstadoLlamada = 1, 1, 0)), 0) AS CambiosFaseOCconLlamada, 
                        ISNULL(SUM(IIF(EstadoLlamada = 0, 1, 0)), 0) AS CambiosFaseOCsinLlamada 
                    FROM com.V_ReporteTasaContactoConySinLlamadaTresCxV2_V2
                    WHERE FechaLog 
                        BETWEEN @FechaInicio 
                        AND @FechaFin {filtros.Filtro} 
                        AND IdFaseOrigen != IdFaseDestino
                        AND Estado=1 
                        AND OtroMedio = {estadoSeguimientoWhatsapp}
                        AND IdFaseDestinoCalculado IN (@NI, @IS, @RN3, @RN2_B, @RN2_A, @M, @D, @RN4)";
                var task2 = _dapperRepository.FirstOrDefaultAsync(queryOptimizadoOC, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    FaseOportunidad.NI,
                    FaseOportunidad.IS,
                    FaseOportunidad.RN3,
                    FaseOportunidad.RN2_B,
                    FaseOportunidad.RN2_A,
                    FaseOportunidad.M,
                    FaseOportunidad.D,
                    FaseOportunidad.RN4,
                });

                var resultado1 = await task1;
                if (!string.IsNullOrEmpty(resultado1) && !resultado1.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ReporteTasaContactoConySinLlamadaDTO>(resultado1)!;
                }
                var resultado2 = await task2;
                if (!string.IsNullOrEmpty(resultado2) && !resultado2.Contains("[]"))
                {
                    rpta2 = JsonConvert.DeserializeObject<ReporteTasaContactoConySinLlamadaDTO>(resultado2)!;
                }
                rpta.CambiosFaseOCconLlamada = rpta2.CambiosFaseOCconLlamada;
                rpta.CambiosFaseOCsinLlamada = rpta2.CambiosFaseOCsinLlamada;
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#RR-ORTCS3cx-001@Error en ObtenerReporteTasaContactoConySinLlamadaTresCxV2Async, {ex.Message}");
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las transiciones de una fase a otra en general de las oportunidades 
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de objetos DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteCambiosDeFaseOportunidadAcumuladoV2(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();

                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidad2 WHERE Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +

                            "IdFaseOrigen != IdFaseDestino and " +
                            "IdFaseDestinoCalculado Not In (4, 7, 9, 11, 28, 32, 33, 34,29)" +
                            "GROUP BY Cambio,FaseOrigen, FaseDestino";


                var queryRespuesta = _dapperRepository.QueryDapper(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene todas las transiciones de una fase a otra en general de las oportunidades 
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de objetos DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadAcumuladoV2Async(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();

                var query = $@"SELECT 
                                Count(Cambio) AS NumeroRegistros, 
                                FaseOrigen,
                                FaseDestino,
                                0.0 MetaLanzamiento,
                                0 IndicadorLanzamiento,
                                '' TipoDato
                            FROM com.V_ReporteCambiosDeFaseOportunidad2 
                            WHERE Estado = 1 
                                AND FechaLog between @FechaInicio  and @FechaFin {filtros.Filtro}
                                AND IdFaseOrigen != IdFaseDestino 
                                AND IdFaseDestinoCalculado Not In (4, 7, 9, 11, 28, 32, 33, 34,29)
                            GROUP BY Cambio,FaseOrigen, FaseDestino";

                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception($"Error en ObtenerReporteCambiosDeFaseOportunidadAcumuladoV2Async, {Ex.Message}");
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las transiciones de una fase a otra en general de las oportunidades 
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista ReporteCambiosDeFaseOportunidadDTO </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadAcumuladoPredictivoAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();

                var query = $@"SELECT 
                                Count(Cambio) AS NumeroRegistros, 
                                FaseOrigen,
                                FaseDestino,
                                0.0 MetaLanzamiento,
                                0 IndicadorLanzamiento,
                                '' TipoDato
                            FROM com.V_ReporteCambiosDeFaseOportunidadPredictiva 
                            WHERE Estado = 1 
                                AND FechaLog between @FechaInicio  and @FechaFin {filtros.Filtro}
                                AND IdFaseOrigen != IdFaseDestino 
                                AND IdFaseDestinoCalculado NOT IN (4, 7, 9, 11, 28, 32, 33, 34,29)
                            GROUP BY Cambio,FaseOrigen, FaseDestino";

                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception($"Error en ObtenerReporteCambiosDeFaseOportunidadAcumuladoPredictivoAsync, {Ex.Message}");
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Todo los cambios de fase realizados con Llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns>List<ReporteCambiosDeFaseOportunidadDTO></returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteCambiosDeFaseOportunidadAcumuladoConLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamada2 WHERE Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                            "IdFaseOrigen != IdFaseDestino and " +
                            "EstadoLlamada = 1 and " +
                            "IdFaseDestinoCalculado Not In (4, 7, 9, 11, 27, 28, 32, 33, 34,29)" +
                            "GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene Todo los cambios de fase realizados con Llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns>List<ReporteCambiosDeFaseOportunidadDTO></returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadAcumuladoConLlamadaAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamada2 WHERE Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                            "IdFaseOrigen != IdFaseDestino and " +
                            "EstadoLlamada = 1 and " +
                            "IdFaseDestinoCalculado Not In (4, 7, 9, 11, 27, 28, 32, 33, 34,29)" +
                            "GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener Reporte Cambios De Fase Oportunidad Acumulado ConLlamada TresCxAsync
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns>Lista ReporteCambiosDeFaseOportunidadDTO</returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadAcumuladoConLlamadaTresCxAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                //com.V_ReporteCambiosDeFaseOportunidadConySinLlamada2TresCx
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = $@"SELECT 
                                COUNT(Cambio) AS NumeroRegistros,
                                FaseOrigen,
                                FaseDestino,
                                0.0 MetaLanzamiento,
                                0 IndicadorLanzamiento,
                                '' TipoDato 
                            FROM [com].[V_ReporteCambiosDeFaseOportunidadConySinLlamadaTresCxV2] WHERE Estado = 1 
                                AND FechaLog between @FechaInicio  and @FechaFin {filtros.Filtro}
                                AND IdFaseOrigen != IdFaseDestino
                                AND EstadoLlamada = 1 
                                AND IdFaseDestinoCalculado NOT IN (4, 7, 9, 11, 27, 28, 32, 33, 34,29)
                            GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtener Reporte Cambios De Fase Oportunidad Acumulado ConLlamada TresCxAsync
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns>Lista ReporteCambiosDeFaseOportunidadDTO</returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadAcumuladoConLlamadaTresCxV2Async(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = $@"SELECT 
                                COUNT(Cambio) AS NumeroRegistros,
                                FaseOrigen,
                                FaseDestino,
                                0.0 MetaLanzamiento,
                                0 IndicadorLanzamiento,
                                '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamadaTresCxV2 WHERE Estado = 1 
                                AND FechaLog between @FechaInicio  and @FechaFin {filtros.Filtro}
                                AND IdFaseOrigen != IdFaseDestino
                                AND EstadoLlamada = 1 
                                AND IdFaseDestinoCalculado NOT IN (4, 7, 9, 11, 27, 28, 32, 33, 34,29)
                            GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta)!;
                }
                return items;
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
        /// Obtiene Todo los cambios de fase realizados sin Llamada
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de Objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteCambiosDeFaseOportunidadAcumuladoSinLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                                    Count(Cambio) AS NumeroRegistros,
                                    FaseOrigen,
                                    FaseDestino,
                                    0.0 MetaLanzamiento,
                                    0 IndicadorLanzamiento, 
                                    '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamada2 
                            WHERE Estado = 1 and 
                                    FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                                    IdFaseOrigen != IdFaseDestino and 
                                    EstadoLlamada =0 and 
                                    IdFaseDestinoCalculado Not IN(4, 7, 9, 11, 28, 34,29)
                            GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene Todo los cambios de fase realizados sin Llamada
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de Objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadAcumuladoSinLlamadaAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                                    Count(Cambio) AS NumeroRegistros,
                                    FaseOrigen,
                                    FaseDestino,
                                    0.0 MetaLanzamiento,
                                    0 IndicadorLanzamiento, 
                                    '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamada2 
                            WHERE Estado = 1 and 
                                    FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                                    IdFaseOrigen != IdFaseDestino and 
                                    EstadoLlamada =0 and 
                                    IdFaseDestinoCalculado Not IN(4, 7, 9, 11, 28, 34,29)
                            GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Todo los cambios de fase realizados sin Llamada
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de Objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadAcumuladoSinLlamadaTresCxAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                //com.V_ReporteCambiosDeFaseOportunidadConySinLlamada2TresCx 
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = $@"SELECT 
                                    Count(Cambio) AS NumeroRegistros,
                                    FaseOrigen,
                                    FaseDestino,
                                    0.0 MetaLanzamiento,
                                    0 IndicadorLanzamiento, 
                                    '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamadaTresCxV2 
                            WHERE Estado = 1 and 
                                    FechaLog between @FechaInicio  and @FechaFin {filtros.Filtro} and 
                                    IdFaseOrigen != IdFaseDestino and 
                                    EstadoLlamada = 0 AND 
                                    IdFaseDestinoCalculado Not IN(4, 7, 9, 11, 28, 34,29)
                            GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene reporte acumulado de fase RN1 Y RN
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de objetos DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteControlAcumuladoRN1yRN(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();

                var query = @"SELECT 
                            COUNT(*) AS NumeroRegistros, 
                            FaseOrigen, 
                            FaseDestino, 
                            'td' as TipoDato, 
                            0.0 as MetaLanzamiento, 
                            0 as IndicadorLanzamiento 
                            FROM com.V_ReporteCambiosDeFaseOportunidadRN1 AS t WHERE Estado = 1 AND 
                            FechaLog between @FechaInicio AND @FechaFin " + filtros.Filtro + @" AND 
                            IdFaseOrigen != IdFaseDestino AND 
                            IdFaseDestino IN (7,9) 
                            GROUP BY FaseOrigen, FaseDestino";

                var queryRespuesta = _dapperRepository.QueryDapper(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene reporte acumulado de fase RN1 Y RN
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de objetos DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteControlAcumuladoRN1yRNAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();

                var query = @"SELECT 
                            COUNT(*) AS NumeroRegistros, 
                            FaseOrigen, 
                            FaseDestino, 
                            'td' as TipoDato, 
                            0.0 as MetaLanzamiento, 
                            0 as IndicadorLanzamiento 
                            FROM com.V_ReporteCambiosDeFaseOportunidadRN1 AS t WHERE Estado = 1 AND 
                            FechaLog between @FechaInicio AND @FechaFin " + filtros.Filtro + @" AND 
                            IdFaseOrigen != IdFaseDestino AND 
                            IdFaseDestino IN (7,9) 
                            GROUP BY FaseOrigen, FaseDestino";

                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene todas las transiciones de una fase a otra de las oportunidades según el ultimo log
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns> Lista de objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoV2(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                            Count(Cambio) AS NumeroRegistros,
                            Numero, 
                            FaseOrigen,
                            FaseDestino,
                            0.0 MetaLanzamiento,
                            0 IndicadorLanzamiento, 
                            '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidad WHERE Estado = 1 and 
                            FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                            IdFaseOrigen != IdFaseDestino and 
                            IdFaseDestino Not In (4, 7, 9, 11, 28, 32, 33)
                            GROUP BY Numero,Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }

                items = items.Where(w => w.Numero == 1).ToList();
                return items;
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
        /// Obtiene todas las transiciones de una fase a otra de las oportunidades según el ultimo log
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns> Lista de objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoV2Async(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                            Count(Cambio) AS NumeroRegistros,
                            Numero, 
                            FaseOrigen,
                            FaseDestino,
                            0.0 MetaLanzamiento,
                            0 IndicadorLanzamiento, 
                            '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidad WHERE Estado = 1 and 
                            FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                            IdFaseOrigen != IdFaseDestino and 
                            IdFaseDestino Not In (4, 7, 9, 11, 28, 32, 33)
                            GROUP BY Numero,Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }

                items = items.Where(w => w.Numero == 1).ToList();
                return items;
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
        /// Obtiene todas las transiciones de una fase a otra de las oportunidades según el ultimo log
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns> Lista de objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoPredictivoAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                            Count(Cambio) AS NumeroRegistros,
                            Numero, 
                            FaseOrigen,
                            FaseDestino,
                            0.0 MetaLanzamiento,
                            0 IndicadorLanzamiento, 
                            '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidadPredictiva2 WHERE Estado = 1 and 
                            FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                            IdFaseOrigen != IdFaseDestino and 
                            IdFaseDestino Not In (4, 7, 9, 11, 28, 32, 33)
                            GROUP BY Numero,Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }

                items = items.Where(w => w.Numero == 1).ToList();
                return items;
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
        /// Obtiene Reporte de cambio de fase no acumulado con llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto de DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoConLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                            Count(Cambio) AS NumeroRegistros,
                            FaseOrigen,
                            FaseDestino,
                            0.0 MetaLanzamiento,
                            0 IndicadorLanzamiento, 
                            '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamadaNoAcumulado2 WHERE  Estado = 1 and 
                            FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                            IdFaseOrigen != IdFaseDestino and 
                            EstadoLlamada = 1 and 
                            IdEstadoOcurrencia = 1 and 
                            FaseDestino Not In ('RN', 'RN1','BNC1','E','BIC','OD','OM','RN5')
                            GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene Reporte de cambio de fase no acumulado con llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto de DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoConLlamadaAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                            Count(Cambio) AS NumeroRegistros,
                            FaseOrigen,
                            FaseDestino,
                            0.0 MetaLanzamiento,
                            0 IndicadorLanzamiento, 
                            '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamadaNoAcumulado2 WHERE  Estado = 1 and 
                            FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                            IdFaseOrigen != IdFaseDestino and 
                            EstadoLlamada = 1 and 
                            IdEstadoOcurrencia = 1 and 
                            FaseDestino Not In ('RN', 'RN1','BNC1','E','BIC','OD','OM','RN5')
                            GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Datos de Cambio de Fase Anterior y Actual  con Llamada y sin Llamada No acumulado
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista ReporteCambiosDeFaseOportunidadDTO </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoConLlamadaTresCxAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var rpta = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = $@"SELECT 
                                Count(Cambio) AS NumeroRegistros,
                                FaseOrigen,
                                FaseDestino,
                                0.0 MetaLanzamiento,
                                0 IndicadorLanzamiento, 
                                '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamadaNoAcumulado2TresCx
                            WHERE  Estado = 1
                                AND FechaLog BETWEEN @FechaInicio AND @FechaFin {filtros.Filtro}
                                AND IdFaseOrigen != IdFaseDestino 
                                AND EstadoLlamada = 1 
                                AND IdEstadoOcurrencia = 1
                                AND FaseDestino NOT IN ('RN', 'RN1','BNC1','E','BIC','OD','OM','RN5')
                            GROUP BY Cambio,FaseOrigen, FaseDestino";
                var resultado = await _dapperRepository.QueryDapperAsync(query, new { filtros.FechaInicio, filtros.FechaFin });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de cambio de fase no acumulado sin llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                                    Count(Cambio) AS NumeroRegistros,
                                    FaseOrigen,
                                    FaseDestino,
                                    0.0 MetaLanzamiento,
                                    0 IndicadorLanzamiento, 
                                    '' TipoDato 
                                    FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamadaNoAcumulado2 WHERE  Estado = 1 and 
                                    FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                                    IdFaseOrigen != IdFaseDestino and 
                                    EstadoLlamada =0 and 
                                    IdFaseDestinoCalculado Not In (4, 7, 9, 11, 27, 28, 32, 33)
                                    GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene reporte de cambio de fase no acumulado sin llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamadaAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                                    Count(Cambio) AS NumeroRegistros,
                                    FaseOrigen,
                                    FaseDestino,
                                    0.0 MetaLanzamiento,
                                    0 IndicadorLanzamiento, 
                                    '' TipoDato 
                                    FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamadaNoAcumulado2 WHERE  Estado = 1 and 
                                    FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                                    IdFaseOrigen != IdFaseDestino and 
                                    EstadoLlamada =0 and 
                                    IdFaseDestinoCalculado Not In (4, 7, 9, 11, 27, 28, 32, 33)
                                    GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamadaTresCxAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                                    Count(Cambio) AS NumeroRegistros,
                                    FaseOrigen,
                                    FaseDestino,
                                    0.0 MetaLanzamiento,
                                    0 IndicadorLanzamiento, 
                                    '' TipoDato 
                                    FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamadaNoAcumulado2TresCx WHERE  Estado = 1 and 
                                    FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                                    IdFaseOrigen != IdFaseDestino and 
                                    EstadoLlamada =0 and 
                                    IdFaseDestinoCalculado Not In (4, 7, 9, 11, 27, 28, 32, 33)
                                    GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene Reporte No Acumulado de Fase RN1 y RN2
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de objetos DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteControlNoAcumuladoRN1yRN(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                            Count(Cambio) AS NumeroRegistros,
                            FaseOrigen,
                            FaseDestino,
                            0.0 MetaLanzamiento,
                            0 IndicadorLanzamiento, 
                            '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidad WHERE Estado = 1 and 
                            FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                            IdFaseOrigen != IdFaseDestino and 
                            IdFaseDestinoCalculado In (7,9)
                            GROUP BY Cambio,FaseOrigen, FaseDestino";

                var queryRespuesta = _dapperRepository.QueryDapper(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene Reporte No Acumulado de Fase RN1 y RN2
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de objetos DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteControlNoAcumuladoRN1yRNAsync(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SELECT 
                            Count(Cambio) AS NumeroRegistros,
                            FaseOrigen,
                            FaseDestino,
                            0.0 MetaLanzamiento,
                            0 IndicadorLanzamiento, 
                            '' TipoDato 
                            FROM com.V_ReporteCambiosDeFaseOportunidad WHERE Estado = 1 and 
                            FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                            IdFaseOrigen != IdFaseDestino and 
                            IdFaseDestinoCalculado In (7,9)
                            GROUP BY Cambio,FaseOrigen, FaseDestino";

                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene Reporte de Llamadas ejecutadas sin cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns> Lista de objeto DTO : List<EjecutadasSinCambiodeFaseDTO> </returns>
        public IEnumerable<EjecutadasSinCambiodeFaseDTO> ObtenerReporteEjecutadasSinCambiodeFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string? asesores = null;
                string? centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                    asesores = string.Join(",", filtros.Asesores);
                if (filtros.CentroCostos.Count() > 0)
                    centroCostos = string.Join(",", filtros.Asesores);

                var query = "com.SP_ActividadesSinCambiodeFase";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    Asesores = asesores,
                    CentroCostos = centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<EjecutadasSinCambiodeFaseDTO>>(resultado)!;
                }
                return new List<EjecutadasSinCambiodeFaseDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Llamadas ejecutadas sin cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns> Lista de objeto DTO : List<EjecutadasSinCambiodeFaseDTO> </returns>
        public async Task<IEnumerable<EjecutadasSinCambiodeFaseDTO>> ObtenerReporteEjecutadasSinCambiodeFaseAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string? asesores = null;
                string? centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                    asesores = string.Join(",", filtros.Asesores);
                if (filtros.CentroCostos.Count() > 0)
                    centroCostos = string.Join(",", filtros.Asesores);

                var query = "com.SP_ActividadesSinCambiodeFase";
                var resultado = await _dapperRepository.QuerySPDapperAsync(query, new
                {
                    Asesores = asesores,
                    CentroCostos = centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<EjecutadasSinCambiodeFaseDTO>>(resultado)!;
                }
                return new List<EjecutadasSinCambiodeFaseDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 11/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Llamadas ejecutadas sin cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns> Lista de objeto DTO : List<EjecutadasSinCambiodeFaseDTO> </returns>
        public async Task<IEnumerable<EjecutadasSinCambiodeFaseAlternoDTO>> ObtenerActividadesSinCambiodeFaseAlternoAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string? asesores = null;
                string? centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                    asesores = string.Join(",", filtros.Asesores);
                if (filtros.CentroCostos.Count() > 0)
                    centroCostos = string.Join(",", filtros.Asesores);

                var query = "com.SP_ActividadesSinCambiodeFaseAlterno";
                var resultado = await _dapperRepository.QuerySPDapperAsync(query, new
                {
                    Asesores = asesores,
                    CentroCostos = centroCostos
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<EjecutadasSinCambiodeFaseAlternoDTO>>(resultado)!;
                }
                return new List<EjecutadasSinCambiodeFaseAlternoDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Actividades sin cambio de fase tres cx
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns> Lista de IEnumerable : IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> </returns>
        public async Task<IEnumerable<EjecutadasSinCambiodeFaseAlternoDTO>> ObtenerActividadesSinCambiodeFaseAlternoTresCxAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string? asesores = null;
                string? centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                    asesores = string.Join(",", filtros.Asesores);
                if (filtros.CentroCostos.Count() > 0)
                    centroCostos = string.Join(",", filtros.Asesores);

                //var query = "com.SP_ActividadesSinCambiodeFaseAlternoTresCx";
                var query = "com.SP_ActividadesSinCambiodeFaseAlternoTresCxV2_V2";
                var resultado = await _dapperRepository.QuerySPDapperAsync(query, new
                {
                    Asesores = asesores,
                    CentroCostos = centroCostos
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<EjecutadasSinCambiodeFaseAlternoDTO>>(resultado)!;
                }
                return new List<EjecutadasSinCambiodeFaseAlternoDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Actividades vencidas
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns> Lista de IEnumerable : IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> </returns>
        public IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> ObtenerReporteActividadesVencidasporTab(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var resultado = new List<ActividadVencidaPorTabDTO>();
                IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> agrupado = null;

                string Asesores = null;
                string CentroCostos = null;

                if (filtros.Asesores.Count() > 0)
                {
                    Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    CentroCostos = String.Join(",", filtros.Asesores);
                }

                var query = "com.SP_ActividadesVencidasPorAgendaTabNuevoModelo";
                var queryRespuesta = _dapperRepository.QuerySPDapper(query, new
                {
                    Asesores,
                    CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ActividadVencidaPorTabDTO>>(queryRespuesta);
                    agrupado = resultado.GroupBy(x => x.Dia)
                    .Select(g => new ActividadVencidaporTabPorDiaAgrupadoDTO
                    {
                        Dia = g.Key,
                        Detalle = g.Select(y => new ActividadVencidaPorTabDTO
                        {
                            Dia = y.Dia,
                            Estado = y.Estado,
                            Total = y.Total
                        }).ToList()
                    });
                }
                return agrupado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Reporte de Actividades vencidas
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns> Lista de IEnumerable : IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> </returns>
        public async Task<List<ActividadVencidaporTabPorDiaAgrupadoDTO>> ObtenerReporteActividadesVencidasporTabAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var rpta = new List<ActividadVencidaPorTabDTO>();
                IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> agrupado = new List<ActividadVencidaporTabPorDiaAgrupadoDTO>();

                string? Asesores = null;
                string? CentroCostos = null;

                if (filtros.Asesores.Count() > 0)
                {
                    Asesores = string.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    CentroCostos = string.Join(",", filtros.Asesores);
                }

                var query = "com.SP_ActividadesVencidasPorAgendaTabNuevoModelo";
                var resultado = await _dapperRepository.QuerySPDapperAsync(query, new
                {
                    Asesores,
                    CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadVencidaPorTabDTO>>(resultado)!;
                    agrupado = rpta.GroupBy(x => x.Dia)
                    .Select(g => new ActividadVencidaporTabPorDiaAgrupadoDTO
                    {
                        Dia = g.Key,
                        Detalle = g.Select(y => new ActividadVencidaPorTabDTO
                        {
                            Dia = y.Dia,
                            Estado = y.Estado,
                            Total = y.Total
                        }).ToList()
                    });
                }
                return agrupado.ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"#RR-ORAVTa-001@Error en ObtenerReporteActividadesVencidasporTabAsync,{e.Message}");
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa de conversion por filtro
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns>  objeto DTO : ReporteTasaDeCambioDTO </returns>
        public async Task<ReporteTasaDeCambioDTO> ObtenerReporteTasaDeConversionAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var respuesta = new ReporteTasaDeCambioDTO();
                var itemSemanal = new List<TCRM_CambioDeFaseDTO>();
                var itemMensual = new List<TCRM_CambioDeFaseDTO>();
                string? asesores = null;

                if (filtros.Asesores.Count() > 0)
                {
                    asesores = string.Join(",", filtros.Asesores);
                }
                var tast_semanal = _dapperRepository.QuerySPDapperAsync("com.SP_ObtenerTasaConsolidadaParaCambioDeFase", new { asesoresTCAP = asesores, fechaFinTCAP = filtros.FechaFin, tipo = 1 });/*Semanal*/
                var tast_mensual = _dapperRepository.QuerySPDapperAsync("com.SP_ObtenerTasaConsolidadaParaCambioDeFase", new { asesoresTCAP = asesores, fechaFinTCAP = filtros.FechaFin, tipo = 0 });/*Mensual*/

                var querySemanal = await tast_semanal;
                var queryMensual = await tast_mensual;
                if (!string.IsNullOrEmpty(querySemanal))
                {
                    itemSemanal = JsonConvert.DeserializeObject<List<TCRM_CambioDeFaseDTO>>(querySemanal)!;
                }
                if (!string.IsNullOrEmpty(queryMensual))
                {
                    itemMensual = JsonConvert.DeserializeObject<List<TCRM_CambioDeFaseDTO>>(queryMensual)!;
                }
                respuesta.ReporteTasaDeCambioSemanal = itemSemanal;
                respuesta.ReporteTasaDeCambioMensual = itemMensual;
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#@Error en ObtenerReporteTasaDeConversionAsync, {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 26/02/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa de conversion predictivo por filtro
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns>  objeto DTO : ReporteTasaDeCambioDTO </returns>
        public async Task<ReporteTasaDeCambioPredictivoDTO> ObtenerReporteTasaDeConversionPredictivaAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var respuesta = new ReporteTasaDeCambioPredictivoDTO();
                var itemSemanal = new List<TCRM_CambioDeFasePredictivoDTO>();
                var itemMensual = new List<TCRM_CambioDeFasePredictivoDTO>();
                string? asesores = null;

                if (filtros.Asesores.Count() > 0)
                {
                    asesores = string.Join(",", filtros.Asesores);
                }
                var tast_semanal = _dapperRepository.QuerySPDapperAsync("com.SP_ObtenerTasaConsolidadaParaCambioDeFasePredictiva", new { asesoresTCAP = asesores, fechaFinTCAP = filtros.FechaFin, tipo = 1 });/*Semanal*/
                var tast_mensual = _dapperRepository.QuerySPDapperAsync("com.SP_ObtenerTasaConsolidadaParaCambioDeFasePredictiva", new { asesoresTCAP = asesores, fechaFinTCAP = filtros.FechaFin, tipo = 0 });/*Mensual*/

                var querySemanal = await tast_semanal;
                var queryMensual = await tast_mensual;
                if (!string.IsNullOrEmpty(querySemanal))
                {
                    itemSemanal = JsonConvert.DeserializeObject<List<TCRM_CambioDeFasePredictivoDTO>>(querySemanal)!;
                }
                if (!string.IsNullOrEmpty(queryMensual))
                {
                    itemMensual = JsonConvert.DeserializeObject<List<TCRM_CambioDeFasePredictivoDTO>>(queryMensual)!;
                }
                respuesta.ReporteTasaDeCambioSemanal = itemSemanal;
                respuesta.ReporteTasaDeCambioMensual = itemMensual;
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#@Error en ObtenerReporteTasaDeConversionPredictivaAsync, {ex.Message}");
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
        public List<ReporteCalidadProcesamientoDTO> ObtenerReporteCalidadProcesamientoV2(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {

                var items = new List<ReporteCalidadProcesamientoDTO>();
                string filtro = "";

                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdPersonal in (" + String.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";
                }

                var query = $@"
                    SELECT 
                            DatosAsesor AS DatosAsesor,
                            CodigoFaseOportunidad AS NombreFase,
                            COUNT(CodigoFaseOportunidad) AS Registros,
                            CAST(ROUND(CONVERT(decimal(12,2),(Sum(PerfilCamposLlenos)*100 /Sum(PerfilCamposTotal))),3)/100 AS numeric(36,2)) AS PromedioPerfil,
                            CAST(ROUND(CONVERT(decimal(12,2),(Sum(PGeneralValidados)*100 /Sum(PGeneralTotal))),3)/100 AS numeric(36,2)) AS PromedioPGeneral,
	                        CAST(ROUND(CONVERT(decimal(12,2),(Sum(PEspecificoValidados)*100 /Sum(PEspecificoTotal))),3)/100 AS numeric(36,2)) AS PromedioPEspecifico,
                            CAST(ROUND(CONVERT(decimal(12,2),(Sum(BeneficiosValidados)*100 /Sum(BeneficiosTotales))),3)/100 AS numeric(36,2)) AS PromedioBeneficios,
	                        CAST(ROUND(AVG(CONVERT(decimal(12,2),CompetidoresVerificacion)),2) AS numeric(36,2)) AS PromedioCompetidores, 
	                        CAST(ROUND((AVG(CONVERT(decimal(12,2),ProblemaSeleccionados))/18),2) AS numeric(36,2)) AS PromedioProblemaSeleccionados,
	                        CAST(ROUND((AVG(CONVERT(decimal(12,2),ProblemaSolucionados))/18),2) AS numeric(36,2)) AS PromedioProblemaSolucionados,
                            CAST(ROUND(CONVERT(decimal(12,2),(
	                        SUM(    CASE
			                        WHEN UltimaFechaConsultaSentinel IS NOT NULL AND UltimaFechaConsultaSentinel BETWEEN @FechaInicio and @FechaFin
				                        THEN 1
			                        ELSE 0
			                        END))),2) AS numeric(36,2))/ 
		                    COUNT(  CASE
			                        WHEN UltimaFechaConsultaSentinel IS NULL
			                            THEN 1
			                        ELSE 1 END) AS PromedioHistorialFinanciero
                    FROM com.V_ObtenerReporteCalidadDeProcesamiento
                    WHERE Fecha BETWEEN @FechaInicio AND @FechaFin  {filtro} 
                    GROUP BY DatosAsesor,
                             CodigoFaseOportunidad
                    ORDER BY DatosAsesor, CodigoFaseOportunidad";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCalidadProcesamientoDTO>>(queryRespuesta);
                }
                return items;
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
        /// Obtiene el reporte de calidad procesamiento Version alterna
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns> Lista de objeto DTO : List<ReporteCalidadProcesamientoDTO> </returns>
        public List<ReporteCalidadProcesamientoAlternoDTO> ObtenerReporteCalidadProcesamientoAlterno(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string? asesores = null;
                string? centroCostos = null;
                DateTime fechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                DateTime fechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                if (filtros.Asesores.Count() > 0)
                {
                    asesores = string.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    centroCostos = string.Join(",", filtros.CentroCostos);
                }
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ReporteCalidadProcesamientoAlterno", new
                {
                    Asesores = asesores,
                    CentroCostos = centroCostos,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ReporteCalidadProcesamientoAlternoDTO>>(resultado)!;
                }
                return new List<ReporteCalidadProcesamientoAlternoDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de Diferencia de Llamadas por Bloque
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objeto DTO : List<DiferenciaLlamadasBloqueDTO> </returns>
        public List<DiferenciaLlamadasBloqueDTO> ObtenerReporteDiferenciaLlamadasBloque(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var resultado = new List<ResultadoDiferenciaLlamadasBloqueDTO>();
                var campoTotal = new DiferenciaLlamadasBloqueDTO();
                var resultadoReporte = new List<DiferenciaLlamadasBloqueDTO>();
                string filtro = "";

                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdPersonal in (" + String.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";
                }

                string queryRespuesta;
                DateTime hoy = DateTime.Now.Date;
                if (filtros.FechaFin.Date == hoy)
                {
                    var query = $@"
                        SELECT
                            SUM(CASE WHEN Diferencia = 0 THEN 1 ELSE 0 END) AS Cero,
	                        SUM(CASE WHEN Diferencia = 1 THEN 1 ELSE 0 END) AS MasCero,
	                        SUM(CASE WHEN Diferencia = 2 THEN 1 ELSE 0 END) AS MasUno,
	                        SUM(CASE WHEN Diferencia = 3 THEN 1 ELSE 0 END) AS MasDos,
	                        SUM(CASE WHEN Diferencia = 4 THEN 1 ELSE 0 END) AS MasTres,
	                        SUM(CASE WHEN Diferencia = 5 THEN 1 ELSE 0 END) AS MasCuatro,
	                        SUM(CASE WHEN Diferencia = 6 THEN 1 ELSE 0 END) AS MasCinco,
	                        SUM(CASE WHEN Diferencia > 6 THEN 1 ELSE 0 END) AS MasSeis
                        FROM com.V_ObtenerDiferenciaOportunidadesContadorBIC
                        WHERE 1=1 {filtro} 
                        GROUP BY Diferencia";
                    queryRespuesta = _dapperRepository.QueryDapper(query, null);
                }
                else
                {
                    var query = $@"
                        SELECT
                            SUM(CASE WHEN Diferencia = 0 THEN 1 ELSE 0 END) AS Cero,
	                        SUM(CASE WHEN Diferencia = 1 THEN 1 ELSE 0 END) AS MasCero,
	                        SUM(CASE WHEN Diferencia = 2 THEN 1 ELSE 0 END) AS MasUno,
	                        SUM(CASE WHEN Diferencia = 3 THEN 1 ELSE 0 END) AS MasDos,
	                        SUM(CASE WHEN Diferencia = 4 THEN 1 ELSE 0 END) AS MasTres,
	                        SUM(CASE WHEN Diferencia = 5 THEN 1 ELSE 0 END) AS MasCuatro,
	                        SUM(CASE WHEN Diferencia = 6 THEN 1 ELSE 0 END) AS MasCinco,
	                        SUM(CASE WHEN Diferencia > 6 THEN 1 ELSE 0 END) AS MasSeis
                        FROM com.V_ObtenerDiferenciaOportunidadesContadorBICHistorico
                        WHERE FechaCongelado BETWEEN @FechaInicio AND @FechaFin  {filtro} 
                        GROUP BY Diferencia";
                    queryRespuesta = _dapperRepository.QueryDapper(query, new
                    {
                        FechaInicio = filtros.FechaFin.Date,
                        FechaFin = filtros.FechaFin.Date.AddDays(1)
                    });
                }

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ResultadoDiferenciaLlamadasBloqueDTO>>(queryRespuesta);
                }

                DiferenciaLlamadasBloqueDTO nuevo;
                foreach (var registro in resultado)
                {
                    nuevo = new DiferenciaLlamadasBloqueDTO();
                    if (registro.Cero != 0)
                    {
                        nuevo.Descripcion = "0 días";
                        nuevo.Cantidad = registro.Cero.GetValueOrDefault();
                    }
                    else if (registro.MasCero != 0)
                    {
                        nuevo.Descripcion = "1 día";
                        nuevo.Cantidad = registro.MasCero.GetValueOrDefault();
                    }
                    else if (registro.MasUno != 0)
                    {
                        nuevo.Descripcion = "2 días";
                        nuevo.Cantidad = registro.MasUno.GetValueOrDefault();
                    }
                    else if (registro.MasDos != 0)
                    {
                        nuevo.Descripcion = "3 días";
                        nuevo.Cantidad = registro.MasDos.GetValueOrDefault();
                    }
                    else if (registro.MasTres != 0)
                    {
                        nuevo.Descripcion = "4 días";
                        nuevo.Cantidad = registro.MasTres.GetValueOrDefault();
                    }
                    else if (registro.MasCuatro != 0)
                    {
                        nuevo.Descripcion = "5 días";
                        nuevo.Cantidad = registro.MasCuatro.GetValueOrDefault();
                    }
                    else if (registro.MasCinco != 0)
                    {
                        nuevo.Descripcion = "Mas de 5 días";
                        nuevo.Cantidad = registro.MasCinco.GetValueOrDefault();
                    }
                    resultadoReporte.Add(nuevo);
                }

                campoTotal.Cantidad = resultadoReporte.Aggregate(0, (total, current) => total + current.Cantidad);
                campoTotal.Descripcion = "Total";
                resultadoReporte.Add(campoTotal);

                return resultadoReporte;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte Conteo de Datos por Fase
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objeto DTO : List<ConteoDatosFaseDTO> </returns>
        public (List<ConteoDatosFaseDTO> ConteoDatosFase, DateTimeDTO FechaInicio, DateTimeDTO FechaMomento) ObtenerReporteConteoDatosFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var resultAnterior = new List<ResultadoConteoDatosFaseDTO>();
                var resultActual = new List<ResultadoConteoDatosFaseDTO>();
                var resultado = new List<ConteoDatosFaseDTO>();
                bool banderaConsultaActual = false;
                DateTime fechaSeguimiento;
                fechaSeguimiento = DateTime.Now.Date;

                if (filtros.FechaFin.Date == fechaSeguimiento)
                {
                    banderaConsultaActual = true;
                }

                string filtro = "";
                string filtroConsultaActual = "";

                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdPersonal in (" + String.Join(",", filtros.Asesores) + ")";

                    filtroConsultaActual += " and ";
                    filtroConsultaActual += "IdPersonal_Asignado in (" + String.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";

                    filtroConsultaActual += " and ";
                    filtroConsultaActual += "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";
                }

                var query = $@"
                    SELECT
	                    FO.Codigo AS FaseOportunidad,
	                    COUNT(IdFaseOportunidad) AS Total
	                FROM [com].[T_DatoOportunidadAreaVenta] AS DOV
	                    INNER JOIN pla.T_FaseOportunidad AS FO ON DOV.IdFaseOportunidad = FO.Id
                        INNER JOIN pla.T_CentroCosto AS CC ON DOV.IdCentroCosto = CC.id AND CC.Estado = 1
                        INNER JOIN mkt.T_Alumno AS AL ON DOV.IdAlumno = AL.Id AND AL.Estado = 1
                    WHERE DOV.IdSesionGuardado = 1 AND DOV.FechaCreacion BETWEEN @FechaInicioDia AND @FechaInicioFinDia  {filtro} 
	                GROUP BY DOV.IdFaseOportunidad,FO.Codigo";

                var queryRespuesta = _dapperRepository.QueryDapper(query, new
                {
                    FechaInicioDia = filtros.FechaInicio.Date,
                    FechaInicioFinDia = filtros.FechaInicio.Date.AddDays(1),
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultAnterior = JsonConvert.DeserializeObject<List<ResultadoConteoDatosFaseDTO>>(queryRespuesta);
                }

                var queryFechaInicio = $@"
                    SELECT
	                    DISTINCT MIN(DOV.FechaCreacion) AS Valor
	                FROM [com].[T_DatoOportunidadAreaVenta] AS DOV
	                    INNER JOIN pla.T_FaseOportunidad AS FO ON DOV.IdFaseOportunidad = FO.Id
                        INNER JOIN pla.T_CentroCosto AS CC ON DOV.IdCentroCosto = CC.id AND CC.Estado = 1
                        INNER JOIN mkt.T_Alumno AS AL ON DOV.IdAlumno = AL.Id AND AL.Estado = 1
                    WHERE DOV.IdSesionGuardado = 1 AND DOV.FechaCreacion BETWEEN @FechaInicioDia AND @FechaInicioFinDia  {filtro}";

                var queryRespuestaFechaInicio = _dapperRepository.FirstOrDefault(queryFechaInicio, new
                {
                    FechaInicioDia = filtros.FechaInicio.Date,
                    FechaInicioFinDia = filtros.FechaInicio.Date.AddDays(1),
                });

                DateTimeDTO? fechaReporteInicio = new();
                DateTimeDTO? fechaReporteMomento = new();

                if (!string.IsNullOrEmpty(queryRespuestaFechaInicio) && queryRespuestaFechaInicio != "null")
                {
                    fechaReporteInicio = JsonConvert.DeserializeObject<DateTimeDTO>(queryRespuestaFechaInicio);
                }

                if (banderaConsultaActual)
                {
                    var queryActual = $@"
                    SELECT
	                    FO.Codigo AS FaseOportunidad,
	                    COUNT(IdFaseOportunidad) AS Total
	                FROM com.T_Oportunidad AS OP
	                    INNER JOIN pla.T_FaseOportunidad AS FO ON OP.IdFaseOportunidad = FO.Id AND FO.Estado = 1 AND FO.Id IN (2,17,7,8,12,13,22)
                        INNER JOIN pla.T_CentroCosto AS CC ON OP.IdCentroCosto = CC.id AND CC.Estado = 1
                        INNER JOIN mkt.T_Alumno AS AL ON OP.IdAlumno = AL.Id AND AL.Estado = 1
                    WHERE OP.Estado = 1 {filtroConsultaActual} 
	                GROUP BY OP.IdFaseOportunidad,FO.Codigo";
                    var queryRespuestaActual = _dapperRepository.QueryDapper(queryActual, null);

                    if (!string.IsNullOrEmpty(queryRespuestaActual) && !queryRespuestaActual.Contains("[]"))
                    {
                        resultActual = JsonConvert.DeserializeObject<List<ResultadoConteoDatosFaseDTO>>(queryRespuestaActual);
                    }

                    var queryFechaMomento = $@"
                    SELECT
	                    DISTINCT GETDATE() AS Valor
	                FROM com.T_Oportunidad AS OP
	                    INNER JOIN pla.T_FaseOportunidad AS FO ON OP.IdFaseOportunidad = FO.Id AND FO.Estado = 1 AND FO.Id IN (2,17,7,8,12,13,22)
                        INNER JOIN pla.T_CentroCosto AS CC ON OP.IdCentroCosto = CC.id AND CC.Estado = 1
                        INNER JOIN mkt.T_Alumno AS AL ON OP.IdAlumno = AL.Id AND AL.Estado = 1
                    WHERE OP.Estado = 1 {filtroConsultaActual}";
                    var queryRespuestaFechaMomento = _dapperRepository.FirstOrDefault(queryFechaMomento, null);

                    if (!string.IsNullOrEmpty(queryRespuestaFechaMomento) && queryRespuestaFechaMomento != "null")
                    {
                        fechaReporteMomento = JsonConvert.DeserializeObject<DateTimeDTO>(queryRespuestaFechaMomento);
                    }
                }
                else
                {
                    var queryActual = $@"
                    SELECT
	                    FO.Codigo AS FaseOportunidad,
	                    COUNT(IdFaseOportunidad) AS Total
	                FROM [com].[T_DatoOportunidadAreaVenta] AS DOV
	                    INNER JOIN pla.T_FaseOportunidad AS FO ON DOV.IdFaseOportunidad = FO.Id
                        INNER JOIN pla.T_CentroCosto AS CC ON DOV.IdCentroCosto = CC.id AND CC.Estado = 1
                        INNER JOIN mkt.T_Alumno AS AL ON DOV.IdAlumno = AL.Id AND AL.Estado = 1
                    WHERE DOV.IdSesionGuardado = 2 AND DOV.FechaCreacion BETWEEN @FechaInicioTarde AND @FechaInicioFinTarde  {filtro} 
	                GROUP BY DOV.IdFaseOportunidad,FO.Codigo";
                    var queryRespuestaActual = _dapperRepository.QueryDapper(queryActual, new
                    {
                        FechaInicioTarde = filtros.FechaFin.Date,
                        FechaInicioFinTarde = filtros.FechaFin.Date.AddDays(1)
                    });

                    if (!string.IsNullOrEmpty(queryRespuestaActual) && !queryRespuestaActual.Contains("[]"))
                    {
                        resultActual = JsonConvert.DeserializeObject<List<ResultadoConteoDatosFaseDTO>>(queryRespuestaActual);
                    }

                    var queryFechaMomento = $@"
                        SELECT
	                        DISTINCT MAX(DOV.FechaCreacion) AS Valor
	                    FROM [com].[T_DatoOportunidadAreaVenta] AS DOV
	                        INNER JOIN pla.T_FaseOportunidad AS FO ON DOV.IdFaseOportunidad = FO.Id
                            INNER JOIN pla.T_CentroCosto AS CC ON DOV.IdCentroCosto = CC.id AND CC.Estado = 1
                            INNER JOIN mkt.T_Alumno AS AL ON DOV.IdAlumno = AL.Id AND AL.Estado = 1
                        WHERE DOV.IdSesionGuardado = 2 AND DOV.FechaCreacion BETWEEN @FechaInicioTarde AND @FechaInicioFinTarde  {filtro}";

                    var queryRespuestaFechaMomento = _dapperRepository.FirstOrDefault(queryFechaMomento, new
                    {
                        FechaInicioTarde = filtros.FechaFin.Date,
                        FechaInicioFinTarde = filtros.FechaFin.Date.AddDays(1)
                    });

                    if (!string.IsNullOrEmpty(queryRespuestaFechaMomento) && queryRespuestaFechaMomento != "null")
                    {
                        fechaReporteMomento = JsonConvert.DeserializeObject<DateTimeDTO>(queryRespuestaFechaMomento);
                    }
                }

                ConteoDatosFaseDTO nuevoActual;
                foreach (var fase in resultActual)
                {
                    if (fase.FaseOportunidad != null)
                    {
                        nuevoActual = new ConteoDatosFaseDTO();
                        nuevoActual.Fase = fase.FaseOportunidad;
                        nuevoActual.Momento = fase.Total;
                        resultado.Add(nuevoActual);
                    }
                }

                foreach (var fase in resultAnterior)
                {
                    if (fase.FaseOportunidad != null)
                    {
                        var faseAnterior = resultado.Where(x => x.Fase == fase.FaseOportunidad).FirstOrDefault();
                        if (faseAnterior == null)
                        {
                            nuevoActual = new ConteoDatosFaseDTO();
                            nuevoActual.Fase = fase.FaseOportunidad;
                            nuevoActual.Momento = 0;
                            nuevoActual.Inicio = fase.Total;
                            resultado.Add(nuevoActual);
                        }
                        else
                        {
                            faseAnterior.Inicio = fase.Total;
                        }
                    }
                }

                nuevoActual = new ConteoDatosFaseDTO();
                var totalInicio = 0;
                var totalMomento = 0;
                foreach (var dato in resultado)
                {
                    totalInicio = totalInicio + dato.Inicio;
                    totalMomento = totalMomento + dato.Momento;
                }
                nuevoActual.Fase = "Total";
                nuevoActual.Inicio = totalInicio;
                nuevoActual.Momento = totalMomento;
                resultado.Add(nuevoActual);

                return (resultado, fechaReporteInicio, fechaReporteMomento);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 11/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte Conteo de Datos por Fase
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objeto DTO : List<ConteoDatosFaseDTO> </returns>
        public List<ConteoDatosFaseAlternoDTO> ObtenerReporteConteoDatosFaseAlterno(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                bool banderaConsultaActual = false;
                DateTime fechaSeguimiento;
                fechaSeguimiento = DateTime.Now.Date;

                if (filtros.FechaFin.Date == fechaSeguimiento)
                {
                    banderaConsultaActual = true;
                }

                string filtro = string.Empty;
                string filtroConsultaActual = string.Empty;

                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " AND IdPersonal IN (" + string.Join(",", filtros.Asesores) + ")";
                    filtroConsultaActual += " AND IdPersonal_Asignado IN (" + string.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtro += " AND IdCentroCosto IN (" + string.Join(",", filtros.CentroCostos) + ")";
                    filtroConsultaActual += " AND IdCentroCosto IN (" + string.Join(",", filtros.CentroCostos) + ")";
                }

                var query = $@"
                    SELECT
	                    FO.Codigo AS FaseOportunidad,
	                    CASE WHEN DOV.IdPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
		                    THEN DOV.IdPais
		                    ELSE 0
	                    END AS IdPais,
	                    CASE WHEN DOV.IdPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
		                    THEN pais.NombrePais
		                    ELSE 'Otros'
	                    END AS Pais,
	                    COUNT(DISTINCT DOV.IdOportunidad) AS Total,
                        MIN(DOV.FechaCreacion) AS FechaInicio
                    FROM [com].[T_DatoOportunidadAreaVenta] AS DOV
                    INNER JOIN pla.T_FaseOportunidad AS FO ON DOV.IdFaseOportunidad = FO.Id
                    INNER JOIN pla.T_CentroCosto AS CC ON DOV.IdCentroCosto = CC.id AND CC.Estado = 1
                    INNER JOIN mkt.T_Alumno AS AL ON DOV.IdAlumno = AL.Id AND AL.Estado = 1
                    LEFT JOIN conf.T_Pais AS pais ON pais.Id = DOV.IdPais
                    WHERE DOV.IdSesionGuardado = 1 AND FO.Id NOT IN (17) AND DOV.FechaCreacion BETWEEN @FechaInicioDia AND @FechaInicioFinDia {filtro} 
                    GROUP BY FO.Codigo, CASE WHEN DOV.IdPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
	                    THEN DOV.IdPais
	                    ELSE 0
                    END ,
                    CASE WHEN DOV.IdPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
	                    THEN pais.NombrePais
	                    ELSE 'Otros'
                    END
                    UNION 

                    SELECT 
	                    FO.Codigo AS FaseOportunidad, 
	                    -1 AS IdPais, 
	                    CASE 
		                    WHEN DOV.EstadoSeguimientoWhatsapp = 1 THEN 'Whatsapp' 
		                    ELSE 'Otros' 
	                    END AS Pais, 
	                    COUNT(DISTINCT DOV.IdOportunidad) AS Total, 
	                    MIN(DOV.FechaCreacion) AS FechaInicio 
                    FROM 
	                    [com].[T_DatoOportunidadAreaVenta] AS DOV 
	                    INNER JOIN pla.T_FaseOportunidad AS FO ON DOV.IdFaseOportunidad = FO.Id 
	                    INNER JOIN pla.T_CentroCosto AS CC ON DOV.IdCentroCosto = CC.id AND CC.Estado = 1 
	                    INNER JOIN mkt.T_Alumno AS AL ON DOV.IdAlumno = AL.Id AND AL.Estado = 1 
	                    LEFT JOIN conf.T_Pais AS pais ON pais.Id = DOV.IdPais 
                    WHERE 
	                    DOV.IdSesionGuardado = 1 AND DOV.EstadoSeguimientoWhatsapp = 1 AND FO.Id NOT IN (17) AND DOV.FechaCreacion BETWEEN @FechaInicioDia AND @FechaInicioFinDia {filtro}
                    GROUP BY 
	                    FO.Codigo,  
	                    CASE 
		                    WHEN DOV.EstadoSeguimientoWhatsapp = 1 THEN 'Whatsapp' 
		                    ELSE 'Otros' 
	                    END

                ";

                var queryRespuesta = _dapperRepository.QueryDapper(query, new
                {
                    FechaInicioDia = filtros.FechaInicio.Date,
                    FechaInicioFinDia = filtros.FechaInicio.Date.AddDays(1),
                });
                var resultAnterior = new List<ResultadoConteoDatosFaseAlternoDTO>();
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultAnterior = JsonConvert.DeserializeObject<List<ResultadoConteoDatosFaseAlternoDTO>>(queryRespuesta)!;
                }

                var resultActual = new List<ResultadoConteoDatosFaseAlternoDTO>();

                if (banderaConsultaActual)
                {
                    var queryActual = $@"
                    SELECT
	                    FO.Codigo AS FaseOportunidad,
	                    CASE WHEN AL.IdCodigoPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
		                    THEN AL.IdCodigoPais
		                    ELSE 0
	                    END AS IdPais,
	                    CASE WHEN AL.IdCodigoPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
		                    THEN pais.NombrePais
		                    ELSE 'Otros'
	                    END AS Pais,
	                    COUNT(DISTINCT OP.Id) AS Total,
	                    GETDATE() AS FechaMomento
                    FROM com.T_Oportunidad AS OP WITH (NOLOCK) 
                    INNER JOIN pla.T_FaseOportunidad AS FO ON OP.IdFaseOportunidad = FO.Id AND FO.Estado = 1 AND FO.Id IN (2,7,8,12,13,22)
                    INNER JOIN pla.T_CentroCosto AS CC ON OP.IdCentroCosto = CC.id AND CC.Estado = 1
                    INNER JOIN mkt.T_Alumno AS AL  WITH (NOLOCK) ON OP.IdAlumno = AL.Id AND AL.Estado = 1
                    LEFT JOIN conf.T_Pais AS Pais ON Pais.CodigoPais = AL.IdCodigoPais
                    WHERE OP.Estado = 1 {filtroConsultaActual} 
                    GROUP BY FO.Codigo,
	                    CASE WHEN AL.IdCodigoPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
		                    THEN AL.IdCodigoPais
		                    ELSE 0
	                    END,
	                    CASE WHEN AL.IdCodigoPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
		                    THEN pais.NombrePais
		                    ELSE 'Otros'
	                    END
                    UNION

                    SELECT 
                        FO.Codigo AS FaseOportunidad, 
                        -1 AS IdPais, 
                        CASE 
	                        WHEN OP.EstadoSeguimientoWhatsapp = 1 THEN 'Whatsapp' 
	                        ELSE 'Otros' 
                        END AS Pais, 
                        COUNT(DISTINCT OP.Id) AS Total, 
                        GETDATE() AS FechaMomento 
                    FROM com.T_Oportunidad AS OP WITH (NOLOCK) 
                    INNER JOIN pla.T_FaseOportunidad AS FO ON OP.IdFaseOportunidad = FO.Id AND FO.Estado = 1 AND FO.Id IN (2,7,8,12,13,22) 
                    INNER JOIN pla.T_CentroCosto AS CC ON OP.IdCentroCosto = CC.id AND CC.Estado = 1 
                    INNER JOIN mkt.T_Alumno AS AL WITH (NOLOCK) ON OP.IdAlumno = AL.Id AND AL.Estado = 1 
                    LEFT JOIN conf.T_Pais AS Pais ON Pais.CodigoPais = AL.IdCodigoPais 
                    WHERE OP.Estado = 1 AND OP.EstadoSeguimientoWhatsapp = 1 {filtroConsultaActual} 
                    GROUP BY FO.Codigo,  
                    CASE 
	                    WHEN OP.EstadoSeguimientoWhatsapp = 1 THEN 'Whatsapp' 
	                    ELSE 'Otros' 
                    END

                    ";
                    var queryRespuestaActual = _dapperRepository.QueryDapper(queryActual, null);

                    if (!string.IsNullOrEmpty(queryRespuestaActual) && !queryRespuestaActual.Contains("[]"))
                    {
                        resultActual = JsonConvert.DeserializeObject<List<ResultadoConteoDatosFaseAlternoDTO>>(queryRespuestaActual)!;
                    }
                }
                else
                {
                    var queryActual = $@"
                        SELECT
	                        FO.Codigo AS FaseOportunidad,
	                        CASE WHEN DOV.IdPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
		                        THEN DOV.IdPais
		                        ELSE 0
	                        END AS IdPais,
	                        CASE WHEN DOV.IdPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
		                        THEN pais.NombrePais
		                        ELSE 'Otros'
	                        END AS Pais,
	                        COUNT(DISTINCT DOV.IdOportunidad) AS Total,
	                        MAX(DOV.FechaCreacion) AS FechaMomento
                        FROM [com].[T_DatoOportunidadAreaVenta] AS DOV
	                    INNER JOIN pla.T_FaseOportunidad AS FO ON DOV.IdFaseOportunidad = FO.Id
                        INNER JOIN pla.T_CentroCosto AS CC ON DOV.IdCentroCosto = CC.id AND CC.Estado = 1
                        INNER JOIN mkt.T_Alumno AS AL ON DOV.IdAlumno = AL.Id AND AL.Estado = 1
                        LEFT JOIN conf.T_Pais AS Pais ON Pais.CodigoPais = DOV.IdPais
                        WHERE DOV.IdSesionGuardado = 2 
                            AND FO.Id NOT IN (17)
	                        AND DOV.FechaCreacion BETWEEN @FechaInicioTarde AND @FechaFinTarde {filtro} 
                        GROUP BY 
	                        FO.Codigo,
	                        CASE WHEN DOV.IdPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
		                        THEN DOV.IdPais 
		                        ELSE 0
	                        END,
	                        CASE WHEN DOV.IdPais IN (51,56,57,52) /*Peru,Chile,Colombia,Mexico*/
		                        THEN pais.NombrePais
		                        ELSE 'Otros'
	                        END

                        UNION

                        SELECT
                            FO.Codigo AS FaseOportunidad,
                            -1 AS IdPais, 
                            CASE 
		                        WHEN DOV.EstadoSeguimientoWhatsapp = 1 THEN 'Whatsapp' 
		                        ELSE 'Otros' 
	                        END AS Pais,
                            COUNT(DISTINCT DOV.IdOportunidad) AS Total,
                            MAX(DOV.FechaCreacion) AS FechaMomento
                        FROM [com].[T_DatoOportunidadAreaVenta] AS DOV
                        INNER JOIN pla.T_FaseOportunidad AS FO ON DOV.IdFaseOportunidad = FO.Id
                        INNER JOIN pla.T_CentroCosto AS CC ON DOV.IdCentroCosto = CC.id AND CC.Estado = 1
                        INNER JOIN mkt.T_Alumno AS AL ON DOV.IdAlumno = AL.Id AND AL.Estado = 1
                        LEFT JOIN conf.T_Pais AS Pais ON Pais.CodigoPais = DOV.IdPais
                        WHERE DOV.IdSesionGuardado = 2 AND DOV.EstadoSeguimientoWhatsapp = 1
                            AND FO.Id NOT IN (17)
                            AND DOV.FechaCreacion BETWEEN @FechaInicioTarde AND @FechaFinTarde {filtro} 
                        GROUP BY 
                            FO.Codigo,
                            CASE 
		                        WHEN DOV.EstadoSeguimientoWhatsapp = 1 THEN 'Whatsapp' 
		                        ELSE 'Otros'
                        END
                    ";
                    var queryRespuestaActual = _dapperRepository.QueryDapper(queryActual, new
                    {
                        FechaInicioTarde = filtros.FechaFin.Date,
                        FechaFinTarde = filtros.FechaFin.Date.AddDays(1)
                    });

                    if (!string.IsNullOrEmpty(queryRespuestaActual) && !queryRespuestaActual.Contains("[]"))
                    {
                        resultActual = JsonConvert.DeserializeObject<List<ResultadoConteoDatosFaseAlternoDTO>>(queryRespuestaActual)!;
                    }
                }
                var resultado = new List<ConteoDatosFaseAlternoDTO>();

                ConteoDatosFaseAlternoDTO nuevoActual;

                resultado = resultActual.Where(s => s.FaseOportunidad != null).GroupBy(x => new { x.IdPais, x.Pais }).Select(x => new ConteoDatosFaseAlternoDTO
                {
                    IdPais = x.Key.IdPais,
                    Pais = x.Key.Pais,
                    FechaInicio = null,
                    FechaMomento = x.OrderByDescending(z => z.FechaMomento).FirstOrDefault()!.FechaMomento,
                    ConteoDatosFase = x.Select(n => new ConteoDatosFaseDTO
                    {
                        Fase = n.FaseOportunidad,
                        Inicio = 0,
                        Momento = n.Total
                    }).ToList()
                }).ToList();

                foreach (var fase in resultAnterior)
                {
                    if (fase.FaseOportunidad != null)
                    {
                        var faseAnterior = resultado.Where(x => x.IdPais == fase.IdPais).FirstOrDefault();
                        //var faseAnterior = resultado.Where(x => x.Fase == fase.FaseOportunidad).FirstOrDefault();
                        if (faseAnterior == null)
                        {
                            nuevoActual = new ConteoDatosFaseAlternoDTO();
                            //nuevoActual.f = fase.FaseOportunidad;
                            //nuevoActual.Momento = 0;
                            //nuevoActual.Inicio = fase.Total;
                            nuevoActual.IdPais = fase.IdPais;
                            nuevoActual.Pais = fase.Pais;
                            nuevoActual.FechaInicio = resultAnterior.Where(x => x.IdPais == fase.IdPais).OrderByDescending(s => s.FechaInicio).FirstOrDefault()!.FechaInicio;
                            nuevoActual.FechaMomento = null;
                            nuevoActual.ConteoDatosFase = resultAnterior.Where(x => x.IdPais == fase.IdPais).Select(n => new ConteoDatosFaseDTO
                            {
                                Fase = n.FaseOportunidad,
                                Inicio = n.Total,
                                Momento = 0
                            }).ToList();
                            resultado.Add(nuevoActual);
                        }
                        else
                        {
                            //faseAnterior.Inicio = fase.Total;
                            faseAnterior.FechaInicio = resultAnterior.Where(x => x.IdPais == fase.IdPais).OrderByDescending(s => s.FechaInicio).FirstOrDefault()!.FechaInicio;
                            var conteoFase = faseAnterior.ConteoDatosFase.Where(x => x.Fase == fase.FaseOportunidad).FirstOrDefault();
                            if (conteoFase != null)
                            {
                                conteoFase.Inicio = fase.Total;
                            }
                            else
                            {
                                conteoFase = new ConteoDatosFaseDTO()
                                {
                                    Fase = fase.FaseOportunidad,
                                    Inicio = fase.Total,
                                    Momento = 0
                                };
                                conteoFase.Inicio = fase.Total;
                                faseAnterior.ConteoDatosFase.Add(conteoFase);
                            }
                        }
                    }
                }

                foreach (var dato in resultado)
                {
                    var totalInicio = 0;
                    var totalMomento = 0;
                    foreach (var item in dato.ConteoDatosFase)
                    {
                        totalInicio += item.Inicio;
                        totalMomento += item.Momento;
                    }
                    var conteoTotal = new ConteoDatosFaseDTO()
                    {
                        Fase = "Total",
                        Inicio = totalInicio,
                        Momento = totalMomento,
                    };
                    dato.ConteoDatosFase.Add(conteoTotal);
                }
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
        /// Obtiene Los Valores para el reporte de contactabilidad
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns></returns>
        public List<ReporteContactabilidadDTO> ObtenerReporteContactabilidad(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteContactabilidadDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteContactabilidadOportunidadLog", new
                {
                    Asesores = filtro.Asesores,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    Tipo = filtro.Tipo
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadDTO>>(query);
                }
                return items;
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
        /// Obtiene Los Valores para el reporte de contactabilidad por Asesor
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns></returns>
        public List<ReporteContactabilidadAsesorDTO> ObtenerReporteContactabilidadAsesorComparativo(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteContactabilidadAsesorDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteContactabilidadAsesor", new
                {
                    asesores = filtro.AsesoresComparativo,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    tipo = filtro.Tipo
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadAsesorDTO>>(query);
                }
                return items;
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
        /// Obtiene Los Valores para el reporte de contactabilidad con tiempos de contestación por asesor
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns>List<ReporteContactabilidadDTO></returns>
        public List<ReporteContactabilidadDTO> ObtenerReporteContactabilidadV2(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteContactabilidadDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteContactabilidadDetalleOportunidadLog", new
                {
                    filtro.Asesores,
                    filtro.FechaInicio,
                    filtro.FechaFin,
                    filtro.Tipo
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadDTO>>(query)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<ReporteContactabilidadDTO> ObtenerReporteContactabilidadV2TresCx(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                //com.SP_ReporteContactabilidadDetalleOportunidadLogTresCx
                var items = new List<ReporteContactabilidadDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteContactabilidadDetalleOportunidadLogTresCxV2", new
                {
                    filtro.Asesores,
                    filtro.FechaInicio,
                    filtro.FechaFin,
                    filtro.Tipo
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadDTO>>(query)!;
                }
                return items;
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
        /// Reporte contactabilidad comercial 3cx
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns> List<ReporteContactabilidadDTO> </returns>
        public List<ReporteContactabilidad3cxAlternoDTO> ObtenerReporteContactabilidadV2TresCxAlterno(ReporteContactabilidadFiltroFinalDTO filtro, bool esHoy)
        {
            try
            {
                var items = new List<ReporteContactabilidad3cxAlternoDTO>();
                var query = "[com].[SP_ReporteContactabilidadTresCxAlterno]";
                if (!esHoy)
                    query = "[com].[SP_ReporteContactabilidadCongeladoTresCxAlterno]";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    filtro.Asesores,
                    filtro.FechaInicio,
                    filtro.FechaFin,
                    filtro.Tipo
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidad3cxAlternoDTO>>(resultado)!;
                }
                return items;
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
        /// Obtiene Los Valores para el reporte de contactabilidad con tiempos de contesto por asesor de la tabla congelada diaria
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns> List<ReporteContactabilidadDTO> </returns>
        public List<ReporteContactabilidadDTO> ObtenerReporteContactabilidadCongelado(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteContactabilidadDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteContactabilidadCongelado", new
                {
                    filtro.Asesores,
                    filtro.FechaInicio,
                    filtro.FechaFin,
                    filtro.Tipo
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadDTO>>(query)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<ReporteContactabilidadDTO> ObtenerReporteContactabilidadCongeladoTresCx(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                //com.SP_ReporteContactabilidadCongeladoTresCx
                var items = new List<ReporteContactabilidadDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteContactabilidadCongeladoTresCxV2", new
                {
                    filtro.Asesores,
                    filtro.FechaInicio,
                    filtro.FechaFin,
                    filtro.Tipo
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadDTO>>(query)!;
                }
                return items;
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
        /// Obtiene Los Valores para el reporte de contactabilidad por Asesor
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns> List<ReporteContactabilidadAsesorIndicadoresDTO> </returns>
        public List<ReporteContactabilidadAsesorIndicadoresDTO> ObtenerReporteContactabilidadAsesorComparativoV2(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteContactabilidadAsesorIndicadoresDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteContactabilidadAsesorV2", new
                {
                    asesores = filtro.AsesoresComparativo,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    tipo = filtro.Tipo
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadAsesorIndicadoresDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<ReporteContactabilidadAsesorIndicadoresDTO> ObtenerReporteContactabilidadAsesorComparativoV2TresCx(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteContactabilidadAsesorIndicadoresDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteContactabilidadAsesorV2TresCx", new
                {
                    asesores = filtro.AsesoresComparativo,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    tipo = filtro.Tipo
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadAsesorIndicadoresDTO>>(query);
                }
                return items;
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
        /// Obtiene Los Valores para el reporte de contactabilidad con tiempos de contesto por asesor
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns> List<ReporteContactabilidadMinutosDTO> </returns>
        public List<ReporteContactabilidadMinutosDTO> ObtenerReporteContactabilidadMinutos(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteContactabilidadMinutosDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteMinutosContactabilidad", new
                {
                    asesores = filtro.Asesores,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    tipo = filtro.Tipo
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadMinutosDTO>>(query)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<ReporteContactabilidadMinutosDTO> ObtenerReporteContactabilidadMinutosTresCx(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteContactabilidadMinutosDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteMinutosContactabilidadTresCx", new
                {
                    asesores = filtro.Asesores,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    tipo = filtro.Tipo
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadMinutosDTO>>(query)!;
                }
                return items;
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
        /// Obtiene el reporte de contactabilidad Desagregado
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns> List<ReporteContactabilidadDTO> </returns>
        public List<ReporteContactabilidadDTO> ObtenerReporteContactabilidadDesagregado(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteContactabilidadDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteContactabilidadDesagregadoOportunidadLog", new
                {
                    Asesores = filtro.Asesores,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    Tipo = filtro.Tipo
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadDTO>>(query);
                }
                return items;
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
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns> List<ReporteSeguimientoOportunidadDTO> </returns>
        public List<ReporteSeguimientoOportunidadDTO> ObtenerReporteSeguimiento(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteSeguimientoOportunidadDTO>();

                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteSeguimientoOportunidadNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    opcionFase = filtro.OpcionFase,
                    faseOportunidadOrigen = filtro.FasesOportunidadOrigen,
                    faseOportunidadDestino = filtro.FasesOportunidadDestino
                });
                items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadDTO>>(query);
                return items;
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
        /// Obtiene el reporte de los solicitudes de visualizacion de informacion
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns> List<ReporteSeguimientoOportunidadDTO> </returns>
        public List<ReporteSeguimientoOportunidadDTO> ObtenerReporteSolicitudesVisualizacion(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteSeguimientoOportunidadDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteSeguimientoVisualizarInformacionNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos
                });
                items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadDTO>>(query);
                return items;
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
        /// Obtiene el reporte de los solicitudes de visualizacion de informacion de operaciones
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns> List<ReporteSeguimientoOportunidadDTO> </returns>
        public List<ReporteSeguimientoOportunidadDTO> ObtenerReporteSolicitudesVisualizacionOperaciones(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteSeguimientoOportunidadDTO>();

                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteSeguimientoVisualizarInformacionOperacionesNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos
                });
                items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadDTO>>(query);
                return items;
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
        public ValorIntDTO ObtenerAprobacionSolicitudVisualizacion(AprobacionSolicitudesVisualizacionFiltroDTO aprobacionFiltro)
        {
            try
            {
                var items = new ValorIntDTO();
                var query = _dapperRepository.QuerySPFirstOrDefault("com.SP_AprobarSolicitudVisualizacion", new
                {
                    IdOportunidad = aprobacionFiltro.IdOportunidad,
                    IdPersonalSolicitante = aprobacionFiltro.IdPersonalSolicitante,
                    Usuario = aprobacionFiltro.Usuario,
                    IdSolicitudVisualizacion = aprobacionFiltro.IdSolicitudVisualizacion
                });
                items = JsonConvert.DeserializeObject<ValorIntDTO>(query);
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        ///Repositorio: ReportesRepositorio
        ///Autor: Jonathan Caipo
        ///Fecha: 30/09/2022
        /// <summary>
        /// Obtiene las actividades realizadas por un asesor en un determinado dia
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda por Id</param>
        /// <param name="fechaInicio"> Filtro de Fecha de Inicio</param>
        /// <param name="fechaFin"> Filtro de Fecha de Fin </param>
        /// <returns> Lista de Objeto DTO </returns>
        public List<ReporteRealizadaDataDTO> ObtenerReporteActividadesRealizadas(ReporteActividadesRealizadasFiltrosDTO filtro, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteRealizadaDataDTO> items = new List<ReporteRealizadaDataDTO>();
                string fasesOrigen = null;
                string fasesDestino = null;

                if (filtro.IdFasesOportunidadOrigen.Count() > 0)
                {
                    fasesOrigen = String.Join(",", filtro.IdFasesOportunidadOrigen);
                }
                if (filtro.IdFasesOportunidadDestino.Count() > 0)
                {
                    fasesDestino = String.Join(",", filtro.IdFasesOportunidadDestino);
                }

                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteActividadesRealizadasNWNuevoModelo", new
                {
                    IdAsesor = filtro.IdAsesor,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    IdCentroCosto = filtro.IdCentroCosto,
                    IdAlumno = filtro.IdAlumno,
                    IdTipoDato = filtro.IdTipoDato,
                    IdCategoriaOrigen = filtro.IdTipoCategoriaOrigen,
                    IdProbabilidadActual = filtro.IdProbabilidadActual,
                    IdEstadoOcurrencia = filtro.IdEstadoOcurrencia,
                    FasesOrigen = fasesOrigen,
                    FasesDestino = fasesDestino
                });

                if (!string.IsNullOrEmpty(query))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteRealizadaDataDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        ///Autor: Jonathan Caipo
        ///Fecha: 30/09/2022
        /// <summary>
        /// Obtiene las actividades realizadas por un asesor en un determinado dia
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda por Id</param>
        /// <param name="fechaInicio"> Filtro de Fecha de Inicio</param>
        /// <param name="fechaFin"> Filtro de Fecha de Fin </param>
        /// <returns> Lista de Objeto DTO </returns>
        public List<ReporteRealizadaDataDTO> ObtenerReporteActividadesRealizadasCongelado(ReporteActividadesRealizadasFiltrosDTO filtro, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteRealizadaDataDTO> elemento = new List<ReporteRealizadaDataDTO>();
                string fasesOrigen = null;
                string fasesDestino = null;

                if (filtro.IdFasesOportunidadOrigen.Count() > 0)
                {
                    fasesOrigen = String.Join(",", filtro.IdFasesOportunidadOrigen);
                }
                if (filtro.IdFasesOportunidadDestino.Count() > 0)
                {
                    fasesDestino = String.Join(",", filtro.IdFasesOportunidadDestino);
                }

                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteActividadesRealizadasNWNuevoModeloCongelado", new
                {
                    IdAsesor = filtro.IdAsesor,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    IdCentroCosto = filtro.IdCentroCosto,
                    IdAlumno = filtro.IdAlumno,
                    IdTipoDato = filtro.IdTipoDato,
                    IdCategoriaOrigen = filtro.IdTipoCategoriaOrigen,
                    IdProbabilidadActual = filtro.IdProbabilidadActual,
                    IdEstadoOcurrencia = filtro.IdEstadoOcurrencia,
                    FasesOrigen = fasesOrigen,
                    FasesDestino = fasesDestino
                });

                if (!string.IsNullOrEmpty(query))
                {
                    elemento = JsonConvert.DeserializeObject<List<ReporteRealizadaDataDTO>>(query)!;
                }
                return elemento;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        ///Autor: Jonathan Caipo
        ///Fecha: 03/10/2022
        /// <summary>
        /// Obtiene Reporte de Actividades Realizadas en el Área de Operaciones
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <param name="fechaInicio"> Filtro de Fecha de Inicio </param>
        /// <param name="fechaFin"> Filtro de Fecha Fin </param>
        /// <returns> lista de objetos DTO: List<ReporteRealizadaDataDTO> </returns>
        public List<ReporteRealizadaDataDTO> ObtenerReporteActividadesRealizadasOperaciones(ReporteActividadesRealizadasFiltrosDTO filtro, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteRealizadaDataDTO> items = new List<ReporteRealizadaDataDTO>();
                string fasesOrigen = null;
                string fasesDestino = null;
                string estadoMatricula = null;
                string subestadoMatricula = null;
                if (filtro.IdFasesOportunidadOrigen.Count() > 0)
                {
                    fasesOrigen = String.Join(",", filtro.IdFasesOportunidadOrigen);
                }
                if (filtro.IdFasesOportunidadDestino.Count() > 0)
                {
                    fasesDestino = String.Join(",", filtro.IdFasesOportunidadDestino);
                }
                if (filtro.IdEstadoMatricula != null)
                {
                    if (filtro.IdEstadoMatricula.Count() > 0)
                    {
                        estadoMatricula = String.Join(",", filtro.IdEstadoMatricula);
                    }
                    else
                    {
                        estadoMatricula = null;
                    }
                }
                if (filtro.IdSubestadoMatricula != null)
                {
                    if (filtro.IdSubestadoMatricula.Count() > 0)
                    {
                        subestadoMatricula = String.Join(",", filtro.IdSubestadoMatricula);
                    }
                    else
                    {
                        subestadoMatricula = null;
                    }
                }
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteActividadesRealizadasOperacionesConFiltroNuevoV5_PRUEBA24012025", new
                {
                    IdAsesor = filtro.IdAsesor,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    IdCentroCosto = filtro.IdCentroCosto,
                    IdAlumno = filtro.IdAlumno,
                    IdTipoDato = filtro.IdTipoDato,
                    IdCategoriaOrigen = filtro.IdTipoCategoriaOrigen,
                    IdProbabilidadActual = filtro.IdProbabilidadActual,
                    IdEstadoOcurrencia = filtro.IdEstadoOcurrencia,
                    FasesOrigen = fasesOrigen,
                    FasesDestino = fasesDestino,
                    IdEstadoMatricula = estadoMatricula,
                    IdSubEstadoMatricula = subestadoMatricula
                    //Activo = filtro.EstadoPersonal
                });

                if (!string.IsNullOrEmpty(query))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteRealizadaDataDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por Periodo Mensual
        /// </summary>
        /// <param name="coordinadores"> Id de Coordinadores </param>
        /// <param name="asesores"> Id de Asesores </param>
        /// <param name="periodoInicio"> Id de periodo Inicio </param>
        /// <param name="periodoFin"> Id de periodo Fin </param>
        /// <returns> ReporteTasaConversionConsolidadaMensualGraficasVistaDTO </returns>
        public ReporteTasaConversionConsolidadaMensualGraficasVistaDTO ReporteTasaConversionConsolidadoAsesoresGraficasMensual(string coordinadores, string asesores, string periodoInicio, string periodoFin)
        {
            try
            {
                var rpta = new ReporteTasaConversionConsolidadaMensualGraficasVistaDTO();
                rpta.Consolidado = new List<TCRM_ConsolidadTCAsesoresMensualGraficas>();

                if (string.IsNullOrEmpty(coordinadores)) { coordinadores = "_"; }
                if (string.IsNullOrEmpty(asesores)) { asesores = "_"; }
                var query = _dapperRepository.QuerySPDapper("com.SP_GenerarReportePorPeriodoMensual_Graficas", new
                {
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    startPeriod = periodoInicio,
                    endPeriod = periodoFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta.Consolidado = JsonConvert.DeserializeObject<List<TCRM_ConsolidadTCAsesoresMensualGraficas>>(query)!;
                    rpta.Consolidado = rpta.Consolidado.OrderBy(w => w.Ano).ThenBy(w => w.Mes).ToList();
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa conversión consolidadas por Periodo Semanal
        /// </summary>
        /// <param name="coordinadores"> Id de Coordinadores </param>
        /// <param name="asesores"> Id de Asesores </param>
        /// <param name="periodoInicio"> Id de periodo Inicio </param>
        /// <param name="periodoFin"> Id de periodo Fin </param>
        /// <returns> ReporteTasaConversionConsolidadaGraficasVistaDTO </returns>
        public ReporteTasaConversionConsolidadaGraficasVistaDTO ReporteTasaConversionConsolidadoAsesoresGraficas(string coordinadores, string asesores, string periodoInicio, string periodoFin)
        {
            try
            {
                var rpta = new ReporteTasaConversionConsolidadaGraficasVistaDTO();
                rpta.Consolidado = new List<TCRM_ConsolidadTCAsesoresGraficas>();

                if (string.IsNullOrEmpty(coordinadores)) { coordinadores = "_"; }
                if (string.IsNullOrEmpty(asesores)) { asesores = "_"; }
                var query = _dapperRepository.QuerySPDapper("com.SP_GenerarReportePorPeriodo_Graficas", new
                {
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    startPeriod = periodoInicio,
                    endPeriod = periodoFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta.Consolidado = JsonConvert.DeserializeObject<List<TCRM_ConsolidadTCAsesoresGraficas>>(query);
                    rpta.Consolidado = rpta.Consolidado.OrderBy(w => w.Ano).ThenBy(w => w.NroSemana).ToList();
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns>ReporteTasaConversionConsolidadaAsesoresDTO</returns>
        public List<TCRM_ConsolidadTCAsesores> ReporteTasaConversionConsolidadoAsesores(CentroCostoPorAsesorDTO param)
        {

            var consolidado = new List<TCRM_ConsolidadTCAsesores>();
            string sp;
            if (param.Fecha)
                sp = "com.SP_GenerarReporteTCAsesoresPais";

            else
                sp = "com.SP_GenerarReporteTCAsesoresPaisCierre";

            var query = _dapperRepository.QuerySPDapper(sp, new
            {
                areaTCAP = param.Area,
                subAreaTCAP = param.SubArea,
                proGeneralTCAP = param.PGeneral,
                pEspecificoTCAP = param.PEspecifico,
                modalidadesTCAP = param.Modalidades,
                ciudadesTCAP = param.Ciudades,
                coordinadoresTCAP = param.Coordinadores,
                asesoresTCAP = param.Asesores,
                fechaInicioTCAP = param.FechaInicio,
                fechaFinTCAP = param.FechaFin
            });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                consolidado = JsonConvert.DeserializeObject<List<TCRM_ConsolidadTCAsesores>>(query)!;
            return consolidado;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns>ReporteTasaConversionConsolidadaAsesoresDTO</returns>
        public ReporteTasaConversionConsolidadaAsesoresAlternoDTO ReporteTasaConversionConsolidadoAsesoresAlterno(string area, string subarea, string pgeneral, string pespecifico, string modalidades, string ciudades, bool fecha, string coordinadores, string asesores, DateTime fechaInicio, DateTime fechaFin)
        {
            ReporteTasaConversionConsolidadaAsesoresAlternoDTO rpta = new ReporteTasaConversionConsolidadaAsesoresAlternoDTO();
            rpta.Consolidado = new List<TCRM_ConsolidadTCAsesores>();
            rpta.Desagregado = new List<TCRM_TasaConversionPorCategoriaDatoPaisAlternoDTO>();

            if (string.IsNullOrEmpty(area)) { area = "_"; }
            if (string.IsNullOrEmpty(subarea)) { subarea = "_"; }
            if (string.IsNullOrEmpty(pgeneral)) { pgeneral = "_"; }
            if (string.IsNullOrEmpty(pespecifico)) { pespecifico = "_"; }
            if (string.IsNullOrEmpty(modalidades)) { modalidades = "_"; }
            if (string.IsNullOrEmpty(ciudades)) { ciudades = "_"; }
            if (string.IsNullOrEmpty(coordinadores)) { coordinadores = "_"; }
            if (string.IsNullOrEmpty(asesores)) { asesores = "_"; }
            if (fecha)
            {
                var query = _dapperRepository.QuerySPDapper("com.SP_GenerarReporteTCAsesoresPais", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta.Consolidado = JsonConvert.DeserializeObject<List<TCRM_ConsolidadTCAsesores>>(query)!;
                }
            }
            else
            {
                var query = _dapperRepository.QuerySPDapper("com.SP_GenerarReporteTCAsesoresPaisCierre", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta.Consolidado = JsonConvert.DeserializeObject<List<TCRM_ConsolidadTCAsesores>>(query)!;

                }
            }

            var result = (from p in rpta.Consolidado
                          group p by new
                          {
                              p.probabilidadDesc,
                              p.ordenp,
                              p.grupo,
                              p.nombreGrupo,
                              p.tcMeta
                          } into g
                          select new TCRM_TasaConversionPorCategoriaDatoPaisAlternoDTO
                          {
                              probabilidadDesc = g.Key.probabilidadDesc,
                              orden = g.Key.ordenp,
                              grupo = g.Key.grupo.ToString(),
                              tcMeta = g.Key.tcMeta,
                              nombreGrupo = g.Key.probabilidadDesc + " " + g.Key.nombreGrupo,
                              pais = Guid.Empty.ToString(),
                              listaMuyAlta = g.Select(o => new TCRM_ConsolidadTCAsesores
                              {
                                  orden = o.orden,
                                  probabilidadDesc = o.probabilidadDesc,
                                  pais = o.pais,
                                  idCoordinador = o.idCoordinador,
                                  nombreCoordinador = o.nombreCoordinador,
                                  idasesor = o.idasesor,
                                  nombre = o.nombre,
                                  idcategoriaOrigen = o.idcategoriaOrigen,
                                  ca_nombre = o.ca_nombre,
                                  inscritosMatriculados = o.inscritosMatriculados,
                                  oportunidadesCerradas = o.oportunidadesCerradas,
                                  idSub = o.idSub,
                                  nombreSub = o.nombreSub,
                                  tcMeta = o.tcMeta
                              }).ToList()

                          });
            var result2 = (from p in rpta.Consolidado
                           group p by new
                           {
                               p.probabilidadDesc,
                               p.ordenp,
                               p.grupo,
                               p.nombreGrupo,
                               p.pais,
                               p.nombrePais,
                               p.tcMeta,
                           } into g
                           select new TCRM_TasaConversionPorCategoriaDatoPaisAlternoDTO
                           {
                               probabilidadDesc = g.Key.probabilidadDesc,
                               orden = g.Key.ordenp,
                               grupo = g.Key.grupo.ToString(),
                               tcMeta = g.Key.tcMeta,
                               nombreGrupo = g.Key.probabilidadDesc + " " + g.Key.nombreGrupo + " " + g.Key.nombrePais,
                               pais = g.Key.pais.ToString(),
                               nombrePais = g.Key.nombrePais,
                               listaMuyAlta = g.Select(o => new TCRM_ConsolidadTCAsesores
                               {
                                   orden = o.orden,
                                   probabilidad = o.probabilidadDesc,
                                   pais = o.pais,
                                   idasesor = o.idasesor,
                                   nombre = o.nombre,
                                   idCoordinador = o.idCoordinador,
                                   nombreCoordinador = o.nombreCoordinador,
                                   idcategoriaOrigen = o.idcategoriaOrigen,
                                   ca_nombre = o.ca_nombre,
                                   inscritosMatriculados = o.inscritosMatriculados,
                                   oportunidadesCerradas = o.oportunidadesCerradas,
                                   idSub = o.idSub,
                                   nombreSub = o.nombreSub,
                                   tcMeta = o.tcMeta
                               }).ToList()

                           });

            rpta.Desagregado = result2.ToList();

            rpta.Desagregado.AddRange(result.ToList());
            rpta.Desagregado = rpta.Desagregado.OrderBy(x => x.orden).ThenByDescending(x => x.tcMeta).ThenBy(w => w.grupo).ThenBy(w => w.nombreGrupo).ToList();
            return rpta;

        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por asesor
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns>List<TCRM_CentroCostoPorAsesorDTO></returns>
        public List<TCRM_CentroCostoPorAsesorDTO> ObtenerCentroCostoPorAsesor(CentroCostoPorAsesorDTO param)
        {
            var respuesta = new List<TCRM_CentroCostoPorAsesorDTO>();
            string sp;
            if (param.Fecha)
                sp = "com.SP_GetOportunidadesCentroCostoTCAsesoresPais";
            else
                sp = "com.SP_GetOportunidadesCentroCostoTCAsesoresPaisCierre";

            var query = _dapperRepository.QuerySPDapper(sp, new
            {
                areaTCAP = param.Area,
                subAreaTCAP = param.SubArea,
                proGeneralTCAP = param.PGeneral,
                pEspecificoTCAP = param.PEspecifico,
                modalidadesTCAP = param.Modalidades,
                ciudadesTCAP = param.Ciudades,
                coordinadoresTCAP = param.Coordinadores,
                asesoresTCAP = param.Asesores,
                fechaInicioTCAP = param.FechaInicio,
                fechaFinTCAP = param.FechaFin
            });
            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                respuesta = JsonConvert.DeserializeObject<List<TCRM_CentroCostoPorAsesorDTO>>(query)!;
            return respuesta;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por asesor
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns>List<TCRM_CentroCostoPorAsesorDTO></returns>
        public List<TCRM_CentroCostoPorAsesorDTO> ObtenerCentroCostoPorAsesorAlterno(string area, string subarea, string pgeneral, string pespecifico, string modalidades, string ciudades, bool fecha, string coordinadores, string asesores, DateTime fechaInicio, DateTime fechaFin)
        {
            List<TCRM_CentroCostoPorAsesorDTO> rpta = new List<TCRM_CentroCostoPorAsesorDTO>();

            if (string.IsNullOrEmpty(area)) { area = "_"; }
            if (string.IsNullOrEmpty(subarea)) { subarea = "_"; }
            if (string.IsNullOrEmpty(pgeneral)) { pgeneral = "_"; }
            if (string.IsNullOrEmpty(pespecifico)) { pespecifico = "_"; }
            if (string.IsNullOrEmpty(modalidades)) { modalidades = "_"; }
            if (string.IsNullOrEmpty(ciudades)) { ciudades = "_"; }
            if (string.IsNullOrEmpty(coordinadores)) { coordinadores = "_"; }
            if (string.IsNullOrEmpty(asesores)) { asesores = "_"; }
            if (fecha)
            {
                var query = _dapperRepository.QuerySPDapper("com.SP_GetOportunidadesCentroCostoTCAsesoresPais", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TCRM_CentroCostoPorAsesorDTO>>(query)!;
                }
            }
            else
            {
                var query = _dapperRepository.QuerySPDapper("com.SP_GetOportunidadesCentroCostoTCAsesoresPaisCierre", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TCRM_CentroCostoPorAsesorDTO>>(query)!;

                }
            }
            return rpta;
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
                var actividades = new List<DateTime>();
                var actividades2 = new List<OportunidadesNoEjecutadasDTO>();
                var registrosBO = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadNoEjecutadaPorId", new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    actividades2 = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(registrosBO);
                    foreach (var item in actividades2)
                    {
                        actividades.Add(item.FechaProgramada);
                    }
                }
                return actividades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registro de OportundiadLog de una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad</param>
        /// <returns>List<ReporteSeguimientoOportunidadLogDTO></returns>
        public List<ReporteSeguimientoOportunidadLogDTO> ObtenerListaOportunidadLog(int idOportunidad)
        {
            try
            {
                var oportunidadesLog = new List<ReporteSeguimientoOportunidadLogDTO>();
                var query = @"SELECT
	                            IdActividadDetalle,
	                            FaseInicio,
	                            FaseDestino,
	                            FechaModificacion,
	                            FechaSiguienteLlamada,
	                            IdFaseOportunidad,
	                            IdFaseOportunidadIP,
	                            IdFaseOportunidadPF,
	                            IdFaseOportunidadIC,
	                            FechaEnvioFaseOportunidadPF,
	                            FechaPagoFaseOportunidadPF,
	                            FechaPagoFaseOportunidadIC,
	                            IdOcurrencia,
	                            IdEstadoOcurrencia,
	                            TiempoDuracion,
	                            TiempoDuracionMinutos,
	                            TiempoDuracion3CX,
	                            IdCentralLLamada,
	                            IdTresCX,
	                            IdOportunidadLog,
	                            FechaIncioLlamadaIntegra,
	                            EstadoLlamadaIntegra,
	                            EstadoLlamadaTresCX,
	                            FechaIncioLlamadaTresCX,
	                            NombreActividad,
	                            NombreOcurrencia,
	                            ComentarioActividad,
	                            FechaFinLlamadaIntegra,
	                            FechaFinLlamadaTresCX,
	                            SubEstadoLlamadaTresCX,
	                            SubEstadoLlamadaIntegra,
	                            IdFaseOportunidadInicial,
	                            NombreGrabacionIntegra,
	                            NombreGrabacionTresCX,
	                            Webphone
                            FROM	com.V_ObtenerOportunidadLogReporteSeguimientoNW
                            WHERE
	                            IdOportunidad = @idOportunidad
	                            AND EstadoOportunidadLog = 1
	                            AND (
		                            ComentarioActividad <> 'Asignacion Manual'
		                            OR ComentarioActividad IS NULL
	                            )
	                            AND IdActividadDetalle IS NOT NULL
                            ORDER BY FechaModificacion;";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new { idOportunidad });
                var oportunidades = new List<ReporteSeguimientoOportunidadLogDTO>();
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    oportunidadesLog = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadLogDTO>>(queryRespuesta);
                    oportunidades = (from p in oportunidadesLog
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
                                         p.IdFaseOportunidadInicial
                                     } into g
                                     select new ReporteSeguimientoOportunidadLogDTO
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
                                         LlamadaIntegra = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdCentralLLamada,
                                             TiempoDuracion = o.TiempoDuracion,
                                             TiempoDuracionMinutos = o.TiempoDuracionMinutos,
                                             FechaInicioLlamada = o.FechaIncioLlamadaIntegra,
                                             EstadoLlamada = o.EstadoLlamadaIntegra,
                                             FechaFinLlamada = o.FechaFinLlamadaIntegra,
                                             SubEstadoLlamada = o.SubEstadoLlamadaIntegra,
                                             NombreGrabacion = o.NombreGrabacionIntegra,
                                             Webphone = o.Webphone
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                         LlamadaTresCX = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdTresCX,
                                             TiempoDuracion = o.TiempoDuracion3CX,
                                             FechaInicioLlamada = o.FechaIncioLlamadaTresCX,
                                             EstadoLlamada = o.EstadoLlamadaTresCX,
                                             FechaFinLlamada = o.FechaFinLlamadaTresCX,
                                             SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                             NombreGrabacion = o.NombreGrabacionTresCX
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                     }).ToList();
                }
                return oportunidades;
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
        /// Obtiene los registro de OportundiadLog de una Oportunidad
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idOportunidad"></param>
        /// <param name="idPadre"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadLogDTO> ObtenerListaOportunidadLogPorIdAlumno(int idAlumno, int idOportunidad, int idPadre)
        {
            try
            {
                List<ReporteSeguimientoOportunidadLogDTO> oportunidadesLog = new List<ReporteSeguimientoOportunidadLogDTO>();
                var query = "SELECT FaseInicio, FaseDestino, FechaModificacion, FechaSiguienteLlamada, IdFaseOportunidad, IdFaseOportunidadIP, IdFaseOportunidadPF, IdFaseOportunidadIC," +
                    "FechaEnvioFaseOportunidadPF, FechaPagoFaseOportunidadPF, FechaPagoFaseOportunidadIC, IdOcurrencia, IdEstadoOcurrencia, TiempoDuracion, TiempoDuracion3CX, IdCentralLLamada," +
                    "IdTresCX, IdOportunidadLog, FechaIncioLlamadaIntegra,EstadoLlamadaIntegra, EstadoLlamadaTresCX, FechaIncioLlamadaTresCX, NombreActividad, NombreOcurrencia, ComentarioActividad, " +
                    "FechaFinLlamadaIntegra, FechaFinLlamadaTresCX, SubEstadoLlamadaTresCX, SubEstadoLlamadaIntegra, IdFaseOportunidadInicial, NombreGrabacionIntegra, NombreGrabacionTresCX,Webphone " +
                    "FROM com.V_ObtenerOportunidadLogReporteSeguimientoNW WHERE IdContacto = @IdAlumno AND IdOportunidad in (@IdOportunidad, @IdPadre) AND EstadoOportunidadLog=1" +
                    "ORDER BY FechaModificacion";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new { IdAlumno = idAlumno, IdOportunidad = idOportunidad, IdPadre = idPadre });
                var oportunidades = new List<ReporteSeguimientoOportunidadLogDTO>();
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    oportunidadesLog = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadLogDTO>>(queryRespuesta)!;

                    oportunidades = (from p in oportunidadesLog
                                     group p by new
                                     {
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
                                         p.IdFaseOportunidadInicial
                                     } into g
                                     select new ReporteSeguimientoOportunidadLogDTO
                                     {
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
                                         LlamadaIntegra = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdCentralLLamada,
                                             TiempoDuracion = o.TiempoDuracion,
                                             FechaInicioLlamada = o.FechaIncioLlamadaIntegra,
                                             EstadoLlamada = o.EstadoLlamadaIntegra,
                                             FechaFinLlamada = o.FechaFinLlamadaIntegra,
                                             SubEstadoLlamada = o.SubEstadoLlamadaIntegra,
                                             NombreGrabacion = o.NombreGrabacionIntegra,
                                             Webphone = o.Webphone
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                         LlamadaTresCX = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdTresCX,
                                             TiempoDuracion = o.TiempoDuracion3CX,
                                             FechaInicioLlamada = o.FechaIncioLlamadaTresCX,
                                             EstadoLlamada = o.EstadoLlamadaTresCX,
                                             FechaFinLlamada = o.FechaFinLlamadaTresCX,
                                             SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                             NombreGrabacion = o.NombreGrabacionTresCX
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                     }).ToList();
                }
                return oportunidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Detalle del Log asociado a una Oportunidad para Reporte de Seguimiento (3CX)
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Lista de OportunidadLogReporteDTO </returns>
        public IEnumerable<OportunidadLogReporteATCDTO> ObtenerOportunidadLogReporteSeguimientoV5ATC(int idAlumno,int idOportunidad,int idPadre, int diferenciaHoraria)
        {
            try
            {
                List<OportunidadLogReporteATCDTO> oportunidadLog = new List<OportunidadLogReporteATCDTO>();
                var query = @"
                    	SELECT 
                            FaseInicio,
	                        FaseDestino,
	                        IdFaseOportunidadInicial,
	                        IdActividadDetalle,
	                        FechaModificacion,
	                        FechaSiguienteLlamada,
	                        IdFaseOportunidad,
	                        IdFaseOportunidadIP,
	                        IdFaseOportunidadPF,
	                        IdFaseOportunidadIC,
	                        FechaEnvioFaseOportunidadPF,
	                        FechaPagoFaseOportunidadPF,
	                        FechaPagoFaseOportunidadIC,
	                        IdOcurrencia,
	                        IdEstadoOcurrencia,
	                        IdOportunidadLog,
	                        IdOportunidad,
	                        EstadoOportunidadLog,
	                        NombreActividad,
	                        NombreOcurrencia,
	                        ComentarioActividad,
	                        IdContacto,
	                        OtroMedio,
	                        EstadoSeguimientoWhatsApp,
	                        IdRegistroLlamada,
                            DATEADD(HOUR, @diferenciaHoraria,FechaInicioLlamada) FechaInicioLlamada,
                            DATEADD(HOUR, @diferenciaHoraria, FechaFinLlamada) FechaFinLlamada,
	                        DuracionTimbrado,
	                        DuracionContesto,
	                        EstadoLlamada,
	                        SubEstadoLlamada,
	                        NombreGrabacion,
	                        UrlGrabacion,
	                        WebphoneGrabacion,
	                        TelefonoDestinoReal,
	                        TelefonoDestino,
	                        OrigenLlamada,
	                        AnexoCentral 
	                    FROM ope.V_ObtenerOportunidadLogReporteSeguimientoOperaciones
	                    WHERE 
                             IdContacto = @IdAlumno AND IdOportunidad in (@IdOportunidad, @IdPadre)
		                    AND EstadoOportunidadLog = 1
		                    AND ((ComentarioActividad <> 'Asignacion Manual'  AND ComentarioActividad <> 'Cambio de Centro Costo') OR ComentarioActividad IS NULL)
		                    AND IdActividadDetalle IS NOT NULL
	                    ORDER BY FechaModificacion";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { IdAlumno = idAlumno, IdOportunidad = idOportunidad, IdPadre = idPadre, diferenciaHoraria });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "[]")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<List<OportunidadLogReporteATCDTO>>(resultadoQuery)!;
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw new Exception($"#OLR-OOLRSA3cx-001@Error en ObtenerOportunidadLogReporteSeguimientoV5, {ex.Message}");
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Detalle del Log asociado a una Oportunidad para Reporte de Seguimiento (3CX)
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Lista de OportunidadLogReporteDTO </returns>
        public OportunidadLogReporteOperacionesDTO ObtenerOportunidadLogReporteSeguimientoV5ATC(int idAlumno, int idOportunidad, int idPadre, int diferenciaHoraria, int pageNumber, int pageSize)
        {
            try
            {
                OportunidadLogReporteOperacionesDTO oportunidadLog = new OportunidadLogReporteOperacionesDTO();
                var query = @"ope.SP_ObtenerOportunidadLogReporteSeguimientoOperaciones";
                var resultadoQuery = _dapperRepository.QuerySPDapper(query, new { IdAlumno = idAlumno, IdOportunidad = idOportunidad, IdPadre = idPadre, DiferenciaHoraria = diferenciaHoraria, pageNumber = pageNumber, pageSize = pageSize });
                var jsonResult = ((dynamic)JsonConvert.DeserializeObject(resultadoQuery)!)[0].JsonResult.ToString();

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "[]")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<OportunidadLogReporteOperacionesDTO>(jsonResult)!;
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw new Exception($"#OLR-OOLRSA3cx-001@Error en ObtenerOportunidadLogReporteSeguimientoV5, {ex.Message}");
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 03/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte pendiente por periodo y coordinador
        /// </summary>
        /// <param name="filtroPendiente"></param>
        /// <returns> Lista de DTO: List<ReportePendientePeriodoyCoordinadorDTO> </returns>
        public IEnumerable<OportunidadLogReporteATCDTO> ObtenerOportunidadLogReporteSeguimientoV5Operaciones(int idAlumno, int idOportunidad, int idPadre, int diferenciaHoraria, int PageNumber, int PageSize)
        {
            try
            {
                List<OportunidadLogReporteATCDTO> oportunidadLog = new List<OportunidadLogReporteATCDTO>();
                var query = "ope.SP_ObtenerOportunidadLogReporteSeguimientoV5ATC";
                var res = _dapperRepository.QuerySPDapper(query, new { idAlumno, idOportunidad, idPadre, diferenciaHoraria, PageNumber, PageSize });
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<List<OportunidadLogReporteATCDTO>>(res)!;
                }
                return oportunidadLog;

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
        /// Obtiene la lista de actividades
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns></returns>
        public List<ReporteActividadOcurrenciaDTO> ReporteActividadOcurrenciaPorIdAlumno(int idAlumno)
        {
            try
            {
                List<ReporteActividadOcurrenciaDTO> items = new List<ReporteActividadOcurrenciaDTO>();
                var query1 = "SELECT " +
                            "IdOportunidad," +
                            "IdEstadoOcurrencia," +
                            "IdFaseOportunidadAnterior," +
                            "IdFaseActual," +
                            "FechaReal " +
                            "FROM com.V_NumeroActividadesEstadoOcurrencia where IdContacto = @IdAlumno";

                var queryRespuesta1 = _dapperRepository.QueryDapper(query1, new { IdAlumno = idAlumno });
                if (!queryRespuesta1.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteActividadOcurrenciaDTO>>(queryRespuesta1)!;
                }
                return items;
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
        /// Obtiene la oportunidad con Codigo de Fase de acuerdo a un IdAlumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public ReporteSeguimientoOportunidadLogGridDTO ObtenerOportunidadCodigoFaseporIdAlumno(int idAlumno, int idOportunidad)
        {
            try
            {
                ReporteSeguimientoOportunidadLogGridDTO item = new ReporteSeguimientoOportunidadLogGridDTO();
                var query = @"SELECT 
                                FaseInicio, FechaSiguienteLlamada, IdFaseOportunidad 
                            FROM 
                                com.V_ObtenerOportunidadCodigoFase 
                            WHERE 
                                IdAlumno = @IdAlumno AND IdOportunidad = @IdOportunidad";
                var queryRespuesta = _dapperRepository.FirstOrDefault(query, new { IdAlumno = idAlumno, IdOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    item = JsonConvert.DeserializeObject<ReporteSeguimientoOportunidadLogGridDTO>(queryRespuesta)!;
                }
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
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
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.PeriodoInicio.Year, filtroPendiente.PeriodoInicio.Month, filtroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.PeriodoFin.Year, filtroPendiente.PeriodoFin.Month, filtroPendiente.PeriodoFin.Day, 23, 59, 59);

                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoyCoordinador]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
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
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.PeriodoInicio.Year, filtroPendiente.PeriodoInicio.Month, filtroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.PeriodoFin.Year, filtroPendiente.PeriodoFin.Month, filtroPendiente.PeriodoFin.Day, 23, 59, 59);

                List<ReportePendientesCambiosPorCoordinadorDTO> items = new List<ReportePendientesCambiosPorCoordinadorDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePendientesCambiosPorCoordinador]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientesCambiosPorCoordinadorDTO>>(query)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
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
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.PeriodoInicio.Year, filtroPendiente.PeriodoInicio.Month, filtroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.PeriodoFin.Year, filtroPendiente.PeriodoFin.Month, filtroPendiente.PeriodoFin.Day, 23, 59, 59);

                List<ReportePendientesDiferenciasDTO> items = new List<ReportePendientesDiferenciasDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePendientesDiferencias]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientesDiferenciasDTO>>(query)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
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
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.PeriodoInicio.Year, filtroPendiente.PeriodoInicio.Month, filtroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.PeriodoFin.Year, filtroPendiente.PeriodoFin.Month, filtroPendiente.PeriodoFin.Day, 23, 59, 59);

                List<ReportePendienteDetallesDTO> items = new List<ReportePendienteDetallesDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoyCoordinadorDetalles]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendienteDetallesDTO>>(query)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
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
        public List<ReportePagosDiaPeriodoDTO> ObtenerReportePagosDia(ReportePagosDiaPeriodoFiltroDTO filtroReportePagosDiaPeriodo, DateTime fechainicio, DateTime fechafin)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroReportePagosDiaPeriodo.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroReportePagosDiaPeriodo.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                List<ReportePagosDiaPeriodoDTO> items = new List<ReportePagosDiaPeriodoDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePagosDiaPeriodo]", new { fechainicio, fechafin, coordinadoras = coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePagosDiaPeriodoDTO>>(query)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista por 12 meses antes del periodo seleccionado para saber los pagos realizados
        /// </summary>
        /// <returns> Lista DTO: List<ReportePagosDiaPeriodoDTO> </returns>
        public List<ReportePagosDiaPeriodoDTO> ObtenerReportePagosPeriodo(ReportePagosDiaPeriodoFiltroDTO filtroReportePagosDiaPeriodo, DateTime fechainicio, DateTime fechafin)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroReportePagosDiaPeriodo.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroReportePagosDiaPeriodo.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                List<ReportePagosDiaPeriodoDTO> items = new List<ReportePagosDiaPeriodoDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePagosDiaPeriodo_Periodo]", new { fechainicio, fechafin, coordinadoras = coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePagosDiaPeriodoDTO>>(query)!;
                }
                return items;
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
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<ReporteSeguimientoOportunidadesOperacionesDTO> ObtenerReporteSeguimientoOperaciones(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadesOperacionesDTO> items = new List<ReporteSeguimientoOportunidadesOperacionesDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteSeguimientoOportunidadOperacionesNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    opcionFase = filtro.OpcionFase,
                    faseOportunidadOrigen = filtro.FasesOportunidadOrigen,
                    faseOportunidadDestino = filtro.FasesOportunidadDestino,
                    codigomatricula = filtro.CodigoMatricula,
                    documentoidentidad = filtro.DocumentoIdentidad,
                    estadosmatricula = filtro.EstadosMatricula,
                    controlFiltroFecha = filtro.ControlFiltroFecha
                });
                if (query != null)
                {
                    items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesOperacionesDTO>>(query)!;
                }
                //if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                //{
                //    items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesOperacionesDTO>>(query)!;
                //}
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de inscritos por carrera
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<ReporteInscritosCarreraOperacionesDTO> ObtenerReporteSeguimientoInscritosCarreraOperaciones(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteInscritosCarreraOperacionesDTO> items = new List<ReporteInscritosCarreraOperacionesDTO>();
                var query = _dapperRepository.QuerySPDapper("ope.SP_ReporteSeguimientoInscritosCarrera", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    opcionFase = filtro.OpcionFase,
                    faseOportunidadOrigen = filtro.FasesOportunidadOrigen,
                    faseOportunidadDestino = filtro.FasesOportunidadDestino,
                    codigomatricula = filtro.CodigoMatricula,
                    documentoidentidad = filtro.DocumentoIdentidad,
                    estadosmatricula = filtro.EstadosMatricula,
                    controlFiltroFecha = filtro.ControlFiltroFecha
                });
                if (query != null)
                {
                    items = JsonConvert.DeserializeObject<List<ReporteInscritosCarreraOperacionesDTO>>(query)!;
                }
                //if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                //{
                //    items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesOperacionesDTO>>(query)!;
                //}
                return items;
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
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesModeloDTO> - listaRespuesta </returns>
        public List<ReporteSeguimientoOportunidadesModeloDTO> ObtenerReporteSeguimientoProbabilidad(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadesModeloDTO> listaRespuesta = new List<ReporteSeguimientoOportunidadesModeloDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteSeguimientoOportunidadModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    opcionFase = filtro.OpcionFase,
                    faseOportunidadOrigen = filtro.FasesOportunidadOrigen,
                    faseOportunidadDestino = filtro.FasesOportunidadDestino
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    listaRespuesta = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesModeloDTO>>(query)!;
                }
                return listaRespuesta;
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
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades por fecha de creacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista de objetos de clase ReporteSeguimientoOportunidadesDTO </returns>
        public List<ReporteSeguimientoOportunidadDTO> ObtenerReporteSeguimientoFC(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadDTO> items = new List<ReporteSeguimientoOportunidadDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteSeguimientoOportunidadFCNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadDTO>>(query)!;
                }
                return items;
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
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades por fecha de registrocampania
        /// </summary>
        /// <param name="filtro">Objeto de clase SeguimientoFiltroFinalDTO</param>
        /// <returns>Lista de objetos de clase ReporteSeguimientoOportunidadesDTO</returns>
        public List<ReporteSeguimientoOportunidadDTO> ObtenerReporteSeguimientoFRC(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadDTO> items = new List<ReporteSeguimientoOportunidadDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteSeguimientoOportunidadFRCNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadDTO>>(query)!;
                }
                return items;
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
        /// Genera reportes del estado del pago de los alumnos.
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtros2"></param>
        /// <returns> Lista DTO: List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> </returns>
        public List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> GenerarReporteEstadoAlumnosPagos(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> items = new List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO>();
                var query = _dapperRepository.QuerySPDapper("ope.SP_ReporteAvanceAcademicoPagos", new
                {
                    valor = filtro,
                    fechainiciomatricula = filtros2.FechaInicioMatricula,
                    fechafinmatricula = filtros2.FechaFinMatricula,
                    fechainicioasignacion = filtros2.FechaInicioAsignacion,
                    fechafinasignacion = filtros2.FechaFinAsignacion,
                    tipofiltrofecha = filtros2.CheckFecha
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO>>(query)!;
                }
                return items;
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
        /// 
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtros2"></param>
        /// <returns> Lista DTO: List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> </returns>
        public List<ReporteIndicadoresOperativosDTO> GenerarReporteIndicadoresOperativos(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {

                List<ReporteIndicadoresOperativosDTO> items = new List<ReporteIndicadoresOperativosDTO>();

                //var query = _dapperRepository.QuerySPDapper("ope.SP_ReporteIndicadoresOperativosAtencionClienteOportunidadLog_26072025", new
                var query = _dapperRepository.QuerySPDapper("ope.SP_ReporteIndicadoresOperativosAtencionClienteOportunidadLog", new
                {
                    Valor = filtro,
                    FechaInicio = filtros2.FechaInicio,
                    FechaFin = filtros2.FechaFin
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteIndicadoresOperativosDTO>>(query);
                }
                return items;
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
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtros2"></param>
        /// <returns> Lista DTO:  List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO>  </returns>
        public List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> GenerarReporteIndicadoresOperativosPorDiaCoordinadora(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> items = new List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO>();
                var query = _dapperRepository.QuerySPDapper("ope.SP_ReporteIndicadoresOperativosPorCoordinadoraAtencionClienteOportunidadLog", new
                //var query = _dapperRepository.QuerySPDapper("ope.SP_ReporteIndicadoresOperativosPorCoordinadoraAtencionClienteOportunidadLog_26072025", new
                {
                    Valor = filtro,
                    FechaInicio = filtros2.FechaInicio,
                    FechaFin = filtros2.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO>>(query);
                }
                return items;
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
        /// Genera reporte del estado de los alumnos.
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtro2"></param>
        /// <returns> Lista DTO: List<ReporteAvanceAcademicoPresencialOnlineDTO> - items </returns>
        public List<ReporteAvanceAcademicoPresencialOnlineDTO> GenerarReporteEstadoAlumnos2(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtro2)
        {
            try
            {
                List<ReporteAvanceAcademicoPresencialOnlineDTO> items = new List<ReporteAvanceAcademicoPresencialOnlineDTO>();
                var query = _dapperRepository.QuerySPDapper("ope.SP_ReporteAvanceAcademicoAonline", new
                {
                    valor = filtro,
                    fechainiciomatricula = filtro2.FechaInicioMatricula,
                    fechafinmatricula = filtro2.FechaFinMatricula,
                    fechainicioasignacion = filtro2.FechaInicioAsignacion,
                    fechafinasignacion = filtro2.FechaFinAsignacion,
                    tipofiltrofecha = filtro2.CheckFecha
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteAvanceAcademicoPresencialOnlineDTO>>(query)!;
                }
                return items;
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
        /// Genera reportes de pago del estado de avance académico de los alumnos.
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtro2"></param>
        /// <returns> Lista DTO: List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> - items </returns>
        public List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> GenerarReporteEstadoAlumnosAvanceAcademicoVSPagos(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtro2)
        {
            try
            {
                List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> items = new List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO>();
                var query = _dapperRepository.QuerySPDapper("ope.SP_ReporteAvanceAcademicoVSPagosAonline", new
                {
                    valor = filtro,
                    fechainiciomatricula = filtro2.FechaInicioMatricula,
                    fechafinmatricula = filtro2.FechaFinMatricula,
                    fechainicioasignacion = filtro2.FechaInicioAsignacion,
                    fechafinasignacion = filtro2.FechaFinAsignacion,
                    tipofiltrofecha = filtro2.CheckFecha
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO>>(query)!;
                }
                return items;
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
        /// 
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtro2"></param>
        /// <returns> Lista DTO: List<ReporteAvanceAcademicoAlumnosPagosAtrasados> - items </returns>
        public List<ReporteAvanceAcademicoAlumnosPagosAtrasados> GenerarReporteEstadoAlumnosPagosAtrasados(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtro2)
        {
            try
            {
                List<ReporteAvanceAcademicoAlumnosPagosAtrasados> items = new List<ReporteAvanceAcademicoAlumnosPagosAtrasados>();
                var query = _dapperRepository.QuerySPDapper("ope.SP_ReporteAlumnosConPagosAtrasados", new
                {
                    valor = filtro,
                    fechainiciomatricula = filtro2.FechaInicioMatricula,
                    fechafinmatricula = filtro2.FechaFinMatricula,
                    fechainicioasignacion = filtro2.FechaInicioAsignacion,
                    fechafinasignacion = filtro2.FechaFinAsignacion,
                    tipofiltrofecha = filtro2.CheckFecha
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteAvanceAcademicoAlumnosPagosAtrasados>>(query)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos para el reporte Pago ALumnos
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="filtro2"></param>
        /// <returns> Lista DTO: List<ReporteAvanceAcademicoAlumnosPagosAtrasados> - items </returns>
        public List<PagoAlumnoDTO> ObtenerReportePagoAlumnos(FiltroReportePagoDTO Filtro)
        {
            try
            {
                DateTime FechaIni = new DateTime(Filtro.FechaInicio.Year, Filtro.FechaInicio.Month, Filtro.FechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(Filtro.FechaFin.Year, Filtro.FechaFin.Month, Filtro.FechaFin.Day, 23, 59, 59);

                List<PagoAlumnoDTO> items = new List<PagoAlumnoDTO>();
                var _query = @" 
                    select 
                        IdCronogramaPagoDetalleFinal,Alumno,CodigoAlumno,FechaPagoOriginal,FechaPago,
                        DiaPago,FechaPagoReal,DiasDeposito,DiasDisponible,CuentaFeriados,
                        ConsideraVSD,ConsiderarDiasHabilesLV,ConsiderarDiasHabilesLS,FechaDepositaron,
                        FechaDisponible,EstadoEfectivo,Cuota_SubCuota,FechaCuota,FormaPago,IdMatriculaCabecera,
                        FechaProcesoPago,FechaProcesoPagoReal,FechaMatricula,IdCiudad,Cuota,MonedaCuota 
                    from [fin].[V_ObtenerPagos] 
                    where  CAST(FechaProcesoPago as date) between CAST(@FechaInicio as date) and CAST(@FechaFin as DATE) ";

                if (Filtro.IdCambio == 1) _query += " and FechaProcesoPagoReal is null and FechaDepositaron is null and FechaDisponible is null ";
                else if (Filtro.IdCambio == 2) _query += " and FechaProcesoPagoReal is not null ";
                else if (Filtro.IdCambio == 3) _query += " and FechaDepositaron is not null  ";
                else if (Filtro.IdCambio == 4) _query += " and FechaDisponible is not null ";

                if (Filtro.CodigoMatricula.Length > 1) _query += " and CodigoAlumno=@CodigoMatricula ";

                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { FechaInicio = FechaIni, FechaFin = FechaFin, CodigoMatricula = Filtro.CodigoMatricula });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagoAlumnoDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por asesor
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<TCRM_CentroCostoByAsesorDetallesDTO> ObtenerCentroCostoPorAsesorDetalles(string area, string subarea, string pgeneral, string pespecifico, string modalidades, string ciudades, bool fecha, string coordinadores, string asesores, DateTime fechaInicio, DateTime fechaFin)
        {
            List<TCRM_CentroCostoByAsesorDetallesDTO> rpta = new List<TCRM_CentroCostoByAsesorDetallesDTO>();

            if (string.IsNullOrEmpty(area))
            {
                area = "_";
            }
            if (string.IsNullOrEmpty(subarea))
            {
                subarea = "_";
            }
            if (string.IsNullOrEmpty(pgeneral))
            {
                pgeneral = "_";
            }
            if (string.IsNullOrEmpty(pespecifico))
            {
                pespecifico = "_";
            }
            if (string.IsNullOrEmpty(modalidades))
            {
                modalidades = "_";
            }
            if (string.IsNullOrEmpty(ciudades))
            {
                ciudades = "_";
            }
            if (string.IsNullOrEmpty(coordinadores))
            {
                coordinadores = "_";
            }
            if (string.IsNullOrEmpty(asesores))
            {
                asesores = "_";
            }

            if (fecha)
            {
                var query = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadesCentroCostoTCAsesoresPaisCierreDetallesV5", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TCRM_CentroCostoByAsesorDetallesDTO>>(query);
                }
            }
            else
            {
                var query = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadesCentroCostoTCAsesoresPaisCierreDetallesV5", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TCRM_CentroCostoByAsesorDetallesDTO>>(query);

                }
            }
            return rpta;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Conteo de acctividad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<LlamadaObservadaDTO> ObtenerReporteActividadEjecutadaLlamadaObservada(ReporteCambioFaseSPFiltrosDTO filtros, bool esHoy)
        {
            try
            {
                var query = esHoy ? "com.SP_ReporteActividadEjecutadaLlamadaObservada" : "com.SP_ReporteActividadEjecutadaLlamadaObservadaCongelado";
                List<LlamadaObservadaDTO> items = new List<LlamadaObservadaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ReporteActividadEjecutadaLlamadaObservada", new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    items = JsonConvert.DeserializeObject<List<LlamadaObservadaDTO>>(resultado)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/12/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Reporte actividades Ejecutada llamada observada
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<LlamadaObservadaDTO> ObtenerReporteActividadEjecutadaLlamadaObservadaTresCx(ReporteCambioFaseSPFiltrosDTO filtros, bool esHoy)
        {
            try
            {
                var query = esHoy ? "com.SP_ReporteActividadEjecutadaLlamadaObservadaTresCx" : "com.SP_ReporteActividadEjecutadaLlamadaObservadaCongeladoTresCx";
                List<LlamadaObservadaDTO> items = new List<LlamadaObservadaDTO>();
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    items = JsonConvert.DeserializeObject<List<LlamadaObservadaDTO>>(resultado)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Reporte actividades Ejecutada llamada observada alterno
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<LlamadaObservadaDTO> ObtenerReporteActividadEjecutadaLlamadaObservadaTresCxV2(ReporteCambioFaseSPFiltrosDTO filtros, bool esHoy)
        {
            try
            {
                var query = esHoy ? "com.SP_ReporteActividadEjecutadaLlamadaObservadaTresCxV2_Resumido" : "com.SP_ReporteActividadEjecutadaLlamadaObservadaTresCxCongeladoV2_Resumido";
                List<LlamadaObservadaDTO> items = new List<LlamadaObservadaDTO>();
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    items = JsonConvert.DeserializeObject<List<LlamadaObservadaDTO>>(resultado)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Conteo de acctividad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<ActividadEjecutadaFaseActualDTO> ObtenerActividadEjecutadaFaseActual(ReporteCambioFaseSPFiltrosDTO filtros)
        {
            try
            {
                List<ActividadEjecutadaFaseActualDTO> items = new List<ActividadEjecutadaFaseActualDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ActividadEjecutadaFaseActual", new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ActividadEjecutadaFaseActualDTO>>(query)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Conteo de acctividad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<ActividadEjecutadaFaseActualDTO> ObtenerActividadEjecutadaFaseActualTresCx(ReporteCambioFaseSPFiltrosDTO filtros)
        {
            try
            {
                //com.SP_ActividadEjecutadaFaseActualTresCx
                List<ActividadEjecutadaFaseActualDTO> items = new List<ActividadEjecutadaFaseActualDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ActividadEjecutadaFaseActualTresCxV2", new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ActividadEjecutadaFaseActualDTO>>(query)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Conteo de acctividad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<LlamadaObservadaDTO> ObtenerAcumuladoLlamadasReprogramadasManualmente(ReporteCambioFaseSPFiltrosDTO filtros)
        {
            try
            {
                List<LlamadaObservadaDTO> items = new List<LlamadaObservadaDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_AcumuladoLlamadasReprogramadasManualmente_V2", new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<LlamadaObservadaDTO>>(query)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Conteo de acctividad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<AcumuladoTiempoContactoEfectivoDTO> ObtenerAcumuladoTiempoContactoEfectivoOportunidadAbiertas(ReporteCambioFaseSPFiltrosDTO filtros)
        {
            try
            {
                List<AcumuladoTiempoContactoEfectivoDTO> items = new List<AcumuladoTiempoContactoEfectivoDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_AcumuladoTiempoContactoEfectivoOportunidadAbiertas", new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin,
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<AcumuladoTiempoContactoEfectivoDTO>>(query)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AcumuladoTiempoContactoEfectivoDTO> ObtenerAcumuladoTiempoContactoEfectivoOportunidadAbiertasTresCx(ReporteCambioFaseSPFiltrosDTO filtros)
        {
            try
            {
                List<AcumuladoTiempoContactoEfectivoDTO> items = new List<AcumuladoTiempoContactoEfectivoDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_AcumuladoTiempoContactoEfectivoOportunidadAbiertasTresCx_Resumido", new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin,
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<AcumuladoTiempoContactoEfectivoDTO>>(query)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Conteo de acctividad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<AcumuladoTiempoContactoEfectivoDTO> ObtenerAcumuladoTiempoContactoEfectivoOportunidadCerradas(ReporteCambioFaseSPFiltrosDTO filtros)
        {
            try
            {
                List<AcumuladoTiempoContactoEfectivoDTO> items = new List<AcumuladoTiempoContactoEfectivoDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_AcumuladoTiempoContactoEfectivoOportunidadCerradas", new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin,
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<AcumuladoTiempoContactoEfectivoDTO>>(query)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene tiempo de contacto acumulado de oportunidades cerradas
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<AcumuladoTiempoContactoEfectivoDTO> ObtenerAcumuladoTiempoContactoEfectivoOportunidadCerradasTresCx(ReporteCambioFaseSPFiltrosDTO filtros)
        {
            try
            {
                List<AcumuladoTiempoContactoEfectivoDTO> items = new List<AcumuladoTiempoContactoEfectivoDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_AcumuladoTiempoContactoEfectivoOportunidadCerradasTresCx_Resumido", new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin,
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<AcumuladoTiempoContactoEfectivoDTO>>(query)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 05/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Conteo de acctividad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO: List<ReporteSeguimientoOportunidadesOperacionesDTO> </returns>
        public List<AcumuladoTiempoContactoEfectivoDTO> ObtenerAcumuladoTiempoContactoEfectivoOportunidadCerradasCongelado(ReporteCambioFaseSPFiltrosDTO filtros)
        {
            try
            {
                List<AcumuladoTiempoContactoEfectivoDTO> items = new List<AcumuladoTiempoContactoEfectivoDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_AcumuladoTiempoContactoEfectivoOportunidadCerradasCongelado", new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin,
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<AcumuladoTiempoContactoEfectivoDTO>>(query)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Reporte completo de Problemas Aula Virtual
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO - List<ReporteProblemasAulaVirtualResultadoDTO> - reporte </returns>
        public List<ReporteProblemasAulaVirtualResultadoDTO> ObtenerReporteProblemasAulaVirtual(ReporteProblemasAulaVirtualFiltroDTO filtro)
        {
            try
            {
                List<ReporteProblemasAulaVirtualResultadoDTO> reporte = new List<ReporteProblemasAulaVirtualResultadoDTO>();
                string? coordinadores = null;
                string? centroCosto = null;
                string? tipoCategoriaError = null;
                string? matriculaCabecera = null;

                if (filtro.IdsCoordinadores != null && filtro.IdsCoordinadores.Count() > 0)
                {
                    coordinadores = string.Join(",", filtro.IdsCoordinadores);
                }
                if (filtro.IdsCentrosCosto != null && filtro.IdsCentrosCosto.Count() > 0)
                {
                    centroCosto = string.Join(",", filtro.IdsCentrosCosto);
                }
                if (filtro.IdsTiposCategoriaError != null && filtro.IdsTiposCategoriaError.Count() > 0)
                {
                    tipoCategoriaError = string.Join(",", filtro.IdsTiposCategoriaError);
                }
                if (filtro.IdsMatriculasCabecera != null && filtro.IdsMatriculasCabecera.Count() > 0)
                {
                    matriculaCabecera = string.Join(",", filtro.IdsMatriculasCabecera);
                }

                var query = _dapperRepository.QuerySPDapper("pla.SP_ReporteProblemasAulaVirtual", new
                {
                    Coordinador = coordinadores,
                    CentroCosto = centroCosto,
                    MatriculaCabecera = matriculaCabecera,
                    TipoCategoriaError = tipoCategoriaError,
                    filtro.FechaInicio,
                    filtro.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporte = JsonConvert.DeserializeObject<List<ReporteProblemasAulaVirtualResultadoDTO>>(query)!;
                }
                return reporte;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerReporteProblemasAulaVirtual()", ex);
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
        public List<ReporteDevolucionDTO> ObtenerReporteDevoluciones(ReporteDevolucionesFiltroDTO FiltroDevoluciones)
        {
            try
            {
                List<ReporteDevolucionDTO> resultado = new List<ReporteDevolucionDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReporteDevolucionesV5]", new
                {
                    FechaIni = FiltroDevoluciones.FechaInicio,
                    FechaFin = FiltroDevoluciones.FechaFin,
                    FechaIniCronograma = FiltroDevoluciones.FechaInicioCronograma,
                    FechaFinCronograma = FiltroDevoluciones.FechaFinCronograma,
                    IdAlumno = FiltroDevoluciones.IdAlumno,
                    IdCentroCosto = FiltroDevoluciones.IdCentroCosto,
                    IdMatricula = FiltroDevoluciones.IdMatricula,
                    EstadoPago = FiltroDevoluciones.EstadoPago,
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ReporteDevolucionDTO>>(query);
                }

                return resultado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        ///Autor: Griselberto Huaman
        ///Fecha: 27/07/2021
        /// <summary>
        /// Congela los datos del reporte de devoluciones en base a una fecha  
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDeDevoluciones(DateTime FechaCongelamiento, string Usuario)
        {
            try
            {
                ResultadoDTO valor = new ResultadoDTO();
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_CongelarReporteDevolucion", new { Usuario, FechaCongelamiento });
                if (!string.IsNullOrEmpty(registroDB) && !registroDB.Contains("[]"))
                {
                    valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                }
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de flujos 
        /// </summary>
        /// <returns></returns>
        public List<ReporteFlujoDTO> ObtenerReporteFlujos(FiltroFechaDTO filtro)
        {
            try
            {

                List<ReporteFlujoDTO> items = new List<ReporteFlujoDTO>();
                var query = _dapperRepository.QuerySPDapper("fin.SP_ReporteFlujo", new
                {
                    _FechaIni = filtro.FechaInicio,
                    _FechaFin = filtro.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteFlujoDTO>>(query);
                }

                return items;
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
                List<ReportePagosDTO> items = new List<ReportePagosDTO>();
                var _query = string.Empty;
                _query = @"   Select 
                            Id, Empresa,EmpresaFur, Sede, Area, TipoPedido, CodigoFur, 
                            ProductoServicio, Cantidad, Unidades, PrecioUnitario, 
                            Moneda, Descripcion, CentroCosto, Curso, Programa, 
                            Rubro, NroCuenta, Cuenta, UsuarioCreacion, UsuarioModificacion, 
                            TipoDocumento, NroDoc, Proveedor, TipoComprobante, NumComprobante,
                            MonedaPago, MontoPagado, TotalDolares,NumCuenta, NumRecibo, 
                            TipoPago, FechaEmision, FechaVencimiento, FechaPagoBanco, 
                            MesPagoBanco, Anterior, MontoProgramado, MontoNoProgramado,
							UsuarioAprobacion, MesPresupuesto, AnioPresupuesto, MesVcto, 
							AnioVcto, Observaciones
                        from fin.V_ReporteFurPago 
                        where 
                            FechaBanco BETWEEN @FechaInicio AND @FechaFin 
                            AND FurCodigo <> '' 
                            AND (FurAntiguo = '0' OR FurAntiguo IS NULL) 
                            AND Estado = 1 
                            AND EstadoPago = 1 
                        ORDER BY FechaBanco DESC";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { FechaInicio = filtro.FechaInicio, FechaFin = filtro.FechaFin });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePagosDTO>>(respuestaDapper);
                }

                return items;
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
                List<ReporteDocumentosPendientesPagoDTO> items = new List<ReporteDocumentosPendientesPagoDTO>();
                var _query = string.Empty;
                _query = @"
                       SELECT Id,
                           Empresa,
                           EmpresaFur,
                           Sede,
                           Area,
                           TipoPedido,
                           CodigoFur,
                           ProductoServicio,
                           Cantidad,
                           Unidades,
                           PrecioUnitario,
                           Moneda,
                           Descripcion,
                           CentroCosto,
                           Curso,
                           Programa,
                           Atipico,
                           Rubro,
                           NroCuenta,
                           Cuenta,
                           UsuarioCreacion,
                           UsuarioModificacion,
                           TipoDocumento,
                           NroDoc,
                           Proveedor,
                           TipoComprobante,
                           NumComprobante,
                           MonedaComprobante,
                           MontoaPagar,
                           TotalDolaresAsociado,
                           MonedaPago,
                           ACuenta,
                           TotalDolaresACuenta,
                           TotalDolaresPendiente,
                           FechaEmisionComprobante,
                           MesdeEmision,
                           FechaVencimientoComprobante,
                           MesdeVcto,
                           Estado,
                           Anterior,
	                       UsuarioAprobacion,
	                       MesPresupuesto,
	                       AnioPresupuesto,
	                       AnioVcto,
	                       Observaciones
                    FROM fin.V_ReporteDocumentosPendientesPagos
                    ORDER BY FurFechaEmision DESC;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteDocumentosPendientesPagoDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        ///Autor: Griselberto Huaman
        ///Fecha: 19/05/2021
        /// <summary>
        /// Congela los datos del reporte de flujo y guarda la fecha
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDeFlujoPorDia(DateTime FechaCongelamiento, string Usuario)
        {
            try
            {
                RespuestaDTO valor = new RespuestaDTO();
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_GenerarCongelamientoReporteFlujoPorDia", new { Usuario, FechaCongelamiento });
                if (!string.IsNullOrEmpty(registroDB) && !registroDB.Contains("[]"))
                {
                    valor = JsonConvert.DeserializeObject<RespuestaDTO>(registroDB);
                    if (valor.Respuesta == 0) throw new Exception("Ya existe un congelamiento para esta fecha " + FechaCongelamiento.ToString("dd-MM-yyyy"));
                }
                return valor.Respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Autor: Griselberto Huaman
        ///Fecha: 19/05/2021
        /// <summary>
        /// Congela los datos del reporte de flujo y guarda el periodo  
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="IdPeriod"> periodo</param>
        public int CongelarReporteDeFlujoPorPeriodo(string Usuario, int IdPeriodo)
        {
            try
            {
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_GenerarCongelamientoReporteFlujoPorPeriodo", new { Usuario, IdPeriodo });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Autor: Griselberto Huaman
        ///Fecha: 07/02/2022
        /// <summary>
        /// Congela los datos del reporte de flujo y guarda la fecha
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteOriginalesPorDia(DateTime FechaCongelamiento, string Usuario)
        {
            try
            {
                ResultadoDTO valor = new ResultadoDTO();
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_CongelamientoCronogramasOriginales", new { Usuario, FechaCongelamiento });
                if (!string.IsNullOrEmpty(registroDB) && !registroDB.Contains("[]"))
                {
                    valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                    if (valor.Resultado == 0) throw new Exception("Ya existe un congelamiento para esta fecha " + FechaCongelamiento.ToString("dd-MM-yyyy"));
                }
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: ReporteRepositorio
        ///Autor: Miguel Mora
        ///Fecha: 07/05/2021
        /// <summary>
        /// Congela los datos del reporte de pagos en base a una fecha  
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDePagosPorDia(DateTime FechaCongelamiento, string Usuario)
        {
            try
            {
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_CongelarReportePagoPorDia", new { Usuario, FechaCongelamiento });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: ReporteRepositorio
        ///Autor: Miguel Mora
        ///Fecha: 07/05/2021
        /// <summary>
        /// Congela los datos del reporte de pagos en base al dia fiunal de un periodo 
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="IdPeriod"> periodo</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDePagosPorPeriodo(DateTime FechaCongelamiento, int IdPeriodo, string Usuario)
        {
            try
            {
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_CongelarReportePagoPorPeriodo", new { Usuario, FechaCongelamiento, IdPeriodo });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Autor: Griselberto Huaman
        ///Fecha: 07/02/2022
        /// <summary>
        /// Obtiene el reporte de control documentos
        /// </summary>
        /// <returns></returns>
        public List<ReporteControlDocumentosDTO> ObtenerReporteControlDocumentos(ReporteControlDocumentosFiltroDTO filtroControlDocumentos)
        {
            try
            {

                List<ReporteControlDocumentosDTO> items = new List<ReporteControlDocumentosDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReporteControlDocumentosV5]", new
                {
                    FechaInicio = filtroControlDocumentos.FechaInicio,
                    FechaFin = filtroControlDocumentos.FechaFin,
                    IdCoordinador = filtroControlDocumentos.IdCoordinador,
                    IdAsesor = filtroControlDocumentos.IdAsesor,
                    IdCentroCosto = filtroControlDocumentos.IdCentroCosto,
                    IdAlumno = filtroControlDocumentos.IdAlumno,
                    IdMatricula = filtroControlDocumentos.IdMatricula,
                    IdEstado = filtroControlDocumentos.IdEstadoPagoMatricula
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteControlDocumentosDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        ///Autor: Griselberto Huaman
        ///Fecha: 07/02/2022
        /// <summary>
        /// Obtiene el reporte de pagos por asistente
        /// </summary>
        /// <returns></returns>
        public List<ReportePagoPorAsistenteDTO> ObtenerReportePagoPorAsistente(filtroReportePagoPorAsistenteDTO filtro)
        {
            try
            {

                List<ReportePagoPorAsistenteDTO> items = new List<ReportePagoPorAsistenteDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ObtenerReportePagosPorAsistente]", new
                {
                    FechaInicioPago = filtro.FechaInicio,
                    FechaFinPago = filtro.FechaFin,
                    IdsConfiguracionPeriodo = filtro.IdsConfiguracion
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePagoPorAsistenteDTO>>(query);
                }

                return items;
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
                List<ReporteComisionesDTO> comisiones = new List<ReporteComisionesDTO>();
                var comisionesDB = _dapperRepository.QuerySPDapper("[fin].[SP_GenerarReporteComisionPersonalv5]", new
                {
                    IdsAsesores = filtro.IdsAsesores,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    IdSubEstado = filtro.IdSubEstado
                });
                if (!string.IsNullOrEmpty(comisionesDB) && !comisionesDB.Contains("[]"))
                {
                    comisiones = JsonConvert.DeserializeObject<List<ReporteComisionesDTO>>(comisionesDB);
                }
                return comisiones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Autor: Griselberto Huaman
        ///Fecha: 07/02/2022
        /// <summary>
        /// Obtiene el Reporte de Ingresos Por Periodos
        /// </summary>
        /// <param name="filtro"> filtro </param>
        /// <returns>List<ReporteComisionesDTO></returns>
        public List<PagosIngresosDTO> ObtenerReportePagosIngresos(ReportePagosPorPeriodoFiltroDTO filtro)
        {
            try
            {

                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePagosIngresosV5]", new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    IdCentroCosto = filtro.IdCentroCosto,
                    IdAlumno = filtro.IdAlumno,
                    IdMatriculaCabecera = filtro.IdMatricula,
                    IdFormaPago = filtro.IdFormaPago,
                    IdCiudad = filtro.IdCiudad,
                    IdModalidad = filtro.IdModalidad
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(query);
                }

                return items;
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
        public bool ActualizarReporteComisiones(DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                var comisionesDB = _dapperRepository.QuerySPDapper("[fin].[SP_ActualizarReporteComisionPersonalV5]", new
                {
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin,
                });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

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
                List<ReportePagosACuentaDTO> pagos = new List<ReportePagosACuentaDTO>();
                var pagosResult = _dapperRepository.QuerySPDapper("fin.SP_ObtenerPagosACuenta", new
                {
                    Anios = Anios
                });
                if (!string.IsNullOrEmpty(pagosResult) && !pagosResult.Contains("[]"))
                {
                    pagos = JsonConvert.DeserializeObject<List<ReportePagosACuentaDTO>>(pagosResult);
                }
                return pagos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }


        ///Autor: Griselberto Huaman
        ///Fecha: 07/02/2022
        /// <summary>
        /// Obtiene las tasas de cambio para el  reporte de Pagos 
        /// </summary>
        /// <returns>Arreglo de ReportePagosACuentaDTO</returns>
        public List<TasaCambioReportePagosDTO> ObtenerTasaCambioReportePagoACuenta(string Anios)
        {
            try
            {
                List<TasaCambioReportePagosDTO> pagos = new List<TasaCambioReportePagosDTO>();
                var pagosResult = _dapperRepository.QuerySPDapper("fin.SP_ObtenerTasaCambioReportePagoACuenta", new
                {
                    Anios = Anios
                });
                if (!string.IsNullOrEmpty(pagosResult) && !pagosResult.Contains("[]"))
                {
                    pagos = JsonConvert.DeserializeObject<List<TasaCambioReportePagosDTO>>(pagosResult);
                }
                return pagos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }


        ///Autor: Flavio R. Mamani Fabian
        ///Fecha: 08/08
        /// <summary>
        /// Metodo para actualizar CronogramaVersionFinal del dia actual
        /// </summary>
        /// <returns></returns>
        public bool ActualizarCronogramaVersionFinal()
        {
            try
            {
                //Se comenta codigo para ejecutar el SP dentro de un job
                var query = _dapperRepository.QuerySPDapper("fin.SP_InsertarTablaCronogramaVersionFinal", null);
                return true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Marco Villanueva.
        /// Fecha: 02/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades de TresCx
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns> List<ReporteSeguimientoOportunidadDTO> </returns>
        public List<ReporteSeguimientoOportunidadDTO> ObtenerReporteSeguimientoTresCx(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                var items = new List<ReporteSeguimientoOportunidadDTO>();

                var query = _dapperRepository.QuerySPDapper("[com].[SP_ReporteSeguimientoOportunidadNuevoModeloTresCx]", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    opcionFase = filtro.OpcionFase,
                    faseOportunidadOrigen = filtro.FasesOportunidadOrigen,
                    faseOportunidadDestino = filtro.FasesOportunidadDestino
                });
                items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadDTO>>(query);
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Marco Villanueva.
        /// Fecha: 02/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta el filtro segmento para conjunto lista de TresCx
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns> Lista de objetos de clase DateTime </returns>
        public List<DateTime> ObtenerActividadesNoEjecutadasTresCx(int idOportunidad)
        {
            try
            {
                var actividades = new List<DateTime>();
                var actividades2 = new List<OportunidadesNoEjecutadasDTO>();
                var registrosBO = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadNoEjecutadaPorId", new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    actividades2 = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(registrosBO);
                    foreach (var item in actividades2)
                    {
                        actividades.Add(item.FechaProgramada);
                    }
                }
                return actividades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Marco Villanueva.
        /// Fecha: 02/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registro de OportundiadLog de una Oportunidad TresCx
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad</param>
        /// <returns>List<ReporteSeguimientoOportunidadLogDTO></returns>
        public List<ReporteSeguimientoOportunidadLogTresCxDTO> ObtenerListaOportunidadLogTresCx(int idOportunidad)
        {
            try
            {
                var oportunidadesLog = new List<ReporteSeguimientoOportunidadLogTresCxDTO>();
                var query = @"SELECT
	                            IdActividadDetalle,
	                            FaseInicio,
	                            FaseDestino,
	                            FechaModificacion,
	                            FechaSiguienteLlamada,
	                            IdFaseOportunidad,
	                            IdFaseOportunidadIP,
	                            IdFaseOportunidadPF,
	                            IdFaseOportunidadIC,
	                            FechaEnvioFaseOportunidadPF,
	                            FechaPagoFaseOportunidadPF,
	                            FechaPagoFaseOportunidadIC,
	                            IdOcurrencia,
	                            IdEstadoOcurrencia,
                                TiempoDuracionTresCx,
	                            TiempoDuracionMinutosTresCx,
	                            IdTresCX,
	                            IdOportunidadLog,
	                            EstadoLlamadaTresCX,
	                            FechaIncioLlamadaTresCX,
	                            NombreActividad,
	                            NombreOcurrencia,
	                            ComentarioActividad,
	                            FechaFinLlamadaTresCX,
	                            SubEstadoLlamadaTresCX,
	                            IdFaseOportunidadInicial,
	                            UrlGrabacionTresCx,
	                            Webphone
                            FROM	com.V_ObtenerOportunidadLogReporteSeguimientoNWTresCx
                            WHERE
	                            IdOportunidad = @idOportunidad
	                            AND EstadoOportunidadLog = 1
	                            AND (
		                            ComentarioActividad <> 'Asignacion Manual'
		                            OR ComentarioActividad IS NULL
	                            )
	                            AND IdActividadDetalle IS NOT NULL
                            ORDER BY FechaModificacion;";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new { idOportunidad });
                var oportunidades = new List<ReporteSeguimientoOportunidadLogTresCxDTO>();
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    oportunidadesLog = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadLogTresCxDTO>>(queryRespuesta);
                    oportunidades = (from p in oportunidadesLog
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
                                         p.IdFaseOportunidadInicial
                                     } into g
                                     select new ReporteSeguimientoOportunidadLogTresCxDTO
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
                                         /*
                                         LlamadaIntegra = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdCentralLLamada,
                                             TiempoDuracion = o.TiempoDuracion,
                                             TiempoDuracionMinutos = o.TiempoDuracionMinutos,
                                             //FechaInicioLlamada = o.FechaIncioLlamadaIntegra,
                                             //EstadoLlamada = o.EstadoLlamadaIntegra,
                                             //FechaFinLlamada = o.FechaFinLlamadaIntegra,
                                             //SubEstadoLlamada = o.SubEstadoLlamadaIntegra,
                                             //NombreGrabacion = o.NombreGrabacionIntegra,
                                             Webphone = o.Webphone
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                         */
                                         LlamadaTresCX = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdTresCX,
                                             TiempoDuracion = o.TiempoDuracionMinutosTresCx,

                                             FechaInicioLlamada = o.FechaIncioLlamadaTresCX,
                                             EstadoLlamada = o.EstadoLlamadaTresCX,
                                             FechaFinLlamada = o.FechaFinLlamadaTresCX,
                                             SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                             NombreGrabacion = o.UrlGrabacionTresCx,
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                     }).ToList();
                }
                return oportunidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 26/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Conteo de Oportunidades predictivas
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista ControlOportunidadPredictivaDTO </returns>
        public List<ControlOportunidadPredictivaDTO> ObtenerControlOportunidadPredictiva(ReporteCambioFaseSPFiltrosDTO filtros)
        {
            try
            {
                List<ControlOportunidadPredictivaDTO> items = new List<ControlOportunidadPredictivaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ControlOportunidadPredictiva", new
                {
                    filtros.Asesores,
                    filtros.CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    items = JsonConvert.DeserializeObject<List<ControlOportunidadPredictivaDTO>>(resultado)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Reporte contactabilidad comercial 3cx
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <returns> List<ReporteContactabilidadDTO> </returns>
        public List<ReporteLlamadaEntranteDTO> ObtenerReporteLlamadaEntrante(FiltroReporteLlamadaEntranteDTO filtro)
        {
            try
            {
                var items = new List<ReporteLlamadaEntranteDTO>();
                var query = "[com].[SP_ReporteLlamadaEntrante]";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    filtro.Asesores,
                    filtro.FechaInicio,
                    filtro.FechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    items = JsonConvert.DeserializeObject<List<ReporteLlamadaEntranteDTO>>(resultado)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Gilmer Qm
        /// Fecha: 2024/11/11
        /// Versión: 1.0
        /// <summary>
        /// Obtener Reporte Cambios De Fase Oportunidad Acumulado ConLlamada TresCxAsync
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns>Lista ReporteCambiosDeFaseOportunidadDTO</returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadAcumuladoConLlamadaAlternoAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                //com.V_ReporteCambiosDeFaseOportunidadConySinLlamada2TresCx
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = $@"com.SP_ReporteCambiosDeFaseOportunidadConLlamada";
                string? asesores = null;
                string? centroCostos = null;
                if (filtros.Asesores.Count() > 0)
                    asesores = string.Join(",", filtros.Asesores);
                if (filtros.CentroCostos.Count() > 0)
                    centroCostos = string.Join(",", filtros.Asesores);

                var queryRespuesta = await _dapperRepository.QuerySPDapperAsync(query, new
                {
                    asesores,
                    centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta)!;
                }
                return items;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// Autor: Gilmer Qm
        /// Fecha: 2024/11/11
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Todo los cambios de fase realizados sin Llamada
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de Objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadAcumuladoSinLlamadaAlternoAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = $@"com.SP_ReporteCambiosDeFaseOportunidadSinLlamada";
                string? asesores = null;
                string? centroCostos = null;
                if (filtros.Asesores.Count() > 0)
                    asesores = string.Join(",", filtros.Asesores);
                if (filtros.CentroCostos.Count() > 0)
                    centroCostos = string.Join(",", filtros.Asesores);

                var queryRespuesta = await _dapperRepository.QuerySPDapperAsync(query, new
                {
                    asesores,
                    centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;


            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Datos de Cambio de Fase Anterior y Actual  con Llamada y sin Llamada No acumulado
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista ReporteCambiosDeFaseOportunidadDTO </returns>
        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoConLlamadaAlternoAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var rpta = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = $@"com.SP_ReporteCambiosDeFaseOportunidadConLlamadaNoAcumulado";
                string? asesores = null;
                string? centroCostos = null;
                if (filtros.Asesores.Count() > 0)
                    asesores = string.Join(",", filtros.Asesores);
                if (filtros.CentroCostos.Count() > 0)
                    centroCostos = string.Join(",", filtros.Asesores);

                var resultado = await _dapperRepository.QueryDapperAsync(query, new
                {
                    asesores,
                    centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(resultado)!;
                }
                return rpta;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamadaAlternoAsync(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                var items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = @"SP_ReporteCambiosDeFaseOportunidadSinLlamadaNoAcumulado";
                string? asesores = null;
                string? centroCostos = null;
                if (filtros.Asesores.Count() > 0)
                    asesores = string.Join(",", filtros.Asesores);
                if (filtros.CentroCostos.Count() > 0)
                    centroCostos = string.Join(",", filtros.Asesores);

                var queryRespuesta = await _dapperRepository.QueryDapperAsync(query, new
                {
                    asesores,
                    centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
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