

using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial
{
    public class LineamientoCalificacionDTO
    {
        public int Id { get; set; }
        public int IdCriterioCalificacionLlamada { get; set; }
        public int IdCriticidadCalificacion { get; set; }
        public string NombreLineamiento { get; set; } = null!;
        public int? Orden { get; set; }
        public string? Descripcion { get; set; }
        public string? HerramientaAnalisis { get; set; }
        public int? Version { get; set; }
        public bool? EsVigente { get; set; }
        public DateTime? FechaVigenciaInicio { get; set; }
        public DateTime? FechaVigenciaFin { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }


    public class LineamientoCalificacionEntradaDTO
    {
        public int? Id { get; set; }
        public int IdCriterioCalificacionLlamada { get; set; }
        public int IdCriticidadCalificacion { get; set; }
        public string NombreLineamiento { get; set; } = null!;
        public int? Orden { get; set; }
        public string? Descripcion { get; set; }
        public string? HerramientaAnalisis { get; set; }
        public int? Version { get; set; }
        public bool? EsVigente { get; set; }
        public DateTime? FechaVigenciaInicio { get; set; }
        public DateTime? FechaVigenciaFin { get; set; }
        public string Usuario { get; set; }
    }
    public class ConfiguracionEsquemaCalificacionDTO
    {
        public int IdFase { get; set; }
        public int IdCriterio { get; set; }
        public int IdCriticidad { get; set; }
        public int IdLineamiento { get; set; }
        public string Fase { get; set; }
        public int OrdenFase { get; set; }
        public string Criterio { get; set; }
        public string DescripcionCriterio { get; set; }
        public int? OrdenCriterio { get; set; }
        public string Criticidad { get; set; }
        public string DescripcionCriticidad { get; set; }
        public string NombreLineamiento { get; set; }
        public string DescripcionLineamiento { get; set; }
        public int? OrdenLineamiento { get; set; }
        public string HerramientaAnalisis { get; set; }
        public int Version { get; set; }
        public bool EsVigente { get; set; }
        public DateTime? FechaVigenciaInicio { get; set; }
        public DateTime? FechaVigenciaFin { get; set; }
        public bool EstadoLineamiento { get; set; }
        public bool EstadoCriticidad { get; set; }
        public bool EstadoCriterio { get; set; }
        public bool EstadoFase { get; set; }
    }
    public class CongelamientoConfiguracionDTO
    {
        public bool EsVigente { get; set; }
        public string DescripcionVersion { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
    }
    public class CongelamientoConfiguracionActivaDTO
    {
        public int IdVersion { get; set; }
        public string Usuario { get; set; }
    }
    public class ConfiguracionEsquemaCalificacionLlamdaDTO
    {
        public int Id { get; set; }
        public DateTime FechaVersion { get; set; }
        public string DescripcionVersion { get; set; }
        public string ConfiguracionJSON { get; set; }
        public bool EsVigente { get; set; }
        public string Comentario { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }




    public class CalificacionLlamadaDTO
    {
        public int Id { get; set; }
        public int IdVersionConfiguracionCalificacioLlamada { get; set; }
        public int IdLlamadaWebphoneCruceCentralTresCx { get; set; }
        public int? IdCriterioCalificacionLlamada { get; set; }
        public int? IdCalificacionPuntoGeneral { get; set; }
        public decimal? Nota { get; set; }
        public string Comentario { get; set; }
        public string Brecha { get; set; }
        public bool TipoCalificacion { get; set; }
    }
    public class HistoricoCalificacionDTO
    {
        public int IdCalificacionLlamada { get; set; }
        public int IdVersionConfiguracionCalificacioLlamada { get; set; }
        public int IdLlamadaWebphoneCruceCentralTresCx { get; set; }
        public DateTime? FechaInicioLlamadaCentral { get; set; }
        public int? IdCriterioCalificacionLlamada { get; set; }
        public int? IdCalificacionPuntoGeneral { get; set; }
        public decimal? Nota { get; set; }
        public string Comentario { get; set; }
        public string Brecha { get; set; }
        public bool? TipoCalificacion { get; set; }
    }

    public class CalificacionLlamadaManualDTO
    {
        public int IdLlamada { get; set; }
        public int IdVersion { get; set; }
        public List<DetalleCalificacionMualDTO> Calificaciones { get; set; }
        public List<DetalleCalificacionPuntoGeneralDTO> CalificacionesPuntosGenerales { get; set; }
        public string Usuario { get; set; }
    }
    public class CalificacionLlamadaAutomaticaDTO
    {
        public int IdLlamada { get; set; }
        public int IdVersion { get; set; }
        public List<DetalleCalificacionMualDTO> Calificaciones { get; set; }
        public List<DetalleCalificacionPuntoGeneralDTO> CalificacionesPuntosGenerales { get; set; }
        public List<DetallePuntosCriticosDTO> CalificacionesPuntosCriticos { get; set; }
        public List<DetalleCalificacionFaseDTO> CalificacionesFase { get; set; }
        public string? InterrupcionLlamada { get; set; }
        public string Usuario { get; set; }
    }
    public class DetalleCalificacionFaseDTO
    {
        public int IdFase { get; set; }
        public string JustificacionGeneral { get; set; }
        public string BrechaGeneral { get; set; }
    }
    public class DetalleCalificacionMualDTO
    {
        public int IdCriterioCalificacionLlamada { get; set; }
        public decimal Nota { get; set; }
        public string? Comentario { get; set; }
        public string? Brecha { get; set; }
    }
    public class DetalleCalificacionPuntoGeneralDTO
    {
        public int idCalificacionPuntoGeneral { get; set; }
        public decimal Nota { get; set; }
        public string? Comentario { get; set; }
        public string? Brecha { get; set; }
    }
    public class DetallePuntosCriticosDTO
    {
        public string? Criterio { get; set; }
        public decimal? Nota { get; set; }
        public string? Comentario { get; set; }
        public string? Brecha { get; set; }
    }
    public class EstadoLlamadaCalificadaDTO
    {
        public int IdLlamada { get; set; }
        public int estadoCalificacion { get; set; }
        public string Usuario { get; set; }
    }

    //public class ConfiguracionTranscripcionDTO
    //{
    //    public TimeSpan HoraEjecucion { get; set; }
    //    public DateTime FechaInicio { get; set; }
    //    public DateTime FechaFin { get; set; }

    //    public List<int> Dias { get; set; }
    //    public List<int> Asesores { get; set; }

    //    public List<int>? FasesEspecificas { get; set; }
    //    public FaseRangoDTO? FasesRango { get; set; }
    //    public string UsuarioCreacion { get; set; }
    //}
    public class ConfiguracionTranscripcionDTO
    {
        public TimeSpan HoraEjecucion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string UsuarioCreacion { get; set; }
        public List<int> Dias { get; set; }
        public List<AsesorTranscripcionDTO> Asesores { get; set; }
    }

    public class AsesorTranscripcionDTO
    {
        public int AsesorId { get; set; }
        public List<FaseRangoDTO> Mapeos { get; set; }
    }

    public class FaseRangoDTO
    {
        public int Origen { get; set; }     // origen
        public int Destino { get; set; } // destino
    }



    /// <summary>
    /// DTO que representa la configuración activa para procesos de transcripción y calificación.
    /// Fuente: [com].[V_ObtenerConfigurarionMasivaTranscripcionCalificacion]
    /// </summary>
    public class ConfiguracionMasivaTranscripcionCalificacionDTO
    {
        /// <summary>
        /// Identificador del proceso programado.
        /// </summary>
        public int IdProcesoProgramado { get; set; }

        /// <summary>
        /// Identificador del tipo de proceso programado (Transcripción o Calificación).
        /// </summary>
        public int IdTipoProcesoProgramado { get; set; }

        /// <summary>
        /// Hora exacta de ejecución del proceso.
        /// </summary>
        public TimeSpan HoraEjecucion { get; set; }

        /// <summary>
        /// Día de la semana en que se ejecuta el proceso (1 = lunes, ..., 7 = domingo).
        /// </summary>
        public byte DiaSemana { get; set; }

        /// <summary>
        /// Identificador del asesor relacionado al proceso.
        /// </summary>
        public int IdPersonal { get; set; }

        /// <summary>
        /// Fecha de inicio del filtro de llamadas que aplican al proceso.
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha de fin del filtro de llamadas que aplican al proceso.
        /// </summary>
        public DateTime FechaFin { get; set; }


        /// <summary>
        /// Fase de origen en caso de filtro por rango.
        /// </summary>
        public int? IdFaseOportunidad_Origen { get; set; }

        /// <summary>
        /// Fase de destino en caso de filtro por rango.
        /// </summary>
        public int? IdFaseOportunidad_Destino { get; set; }
        /// <summary>
        /// Configuracion ACtiva ?
        /// </summary>
        public bool Activo { get; set; }
    }

    public class ConfiguracionActivoProcesoDTO
    {
        public string Usuario { get; set; }
        public bool Activo { get; set; }
    }


    public class TranscripcionPayloadDTO
    {
        public string idLlamada { get; set; }
        public string idActividadDetalle { get; set; }
        public int idPersonal { get; set; }
        public string userName { get; set; }
        public string celular { get; set; }
        public string audioUrl { get; set; }
        public string locale { get; set; }
        public string ocurrencia { get; set; }
    }
    public class LlamadaProcesoAutoDTO
    {
        public int IdActividadDetalle { get; set; }
        public int IdLlamada { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPersonal_Asignado { get; set; }
        public string Comentario { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public string Ocurrencia { get; set; }
        public int IdOportunidadLog { get; set; }
        public int IdAlumno { get; set; }
        public int IdCodigoPais { get; set; }
        public int IdCentroCosto { get; set; }

        public int IdClasificacionPersona { get; set; }
        public int IdFaseOportunidadAnt { get; set; }
        public int IdFaseOportunidad { get; set; }
        public string UrlAudioProcesado { get; set; }
        public string Origen { get; set; }
        public int? DuracionContestoCentral { get; set; }
        public bool? EsLlamadaTranscrita { get; set; } = false;
        public bool? EsLlamadaCalificada { get; set; }
        public DateTime? FechaLlamada { get; set; }
    }




    public class FaseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
    }

    public class CriterioDTO
    {
        public int Id { get; set; }
        public int IdFaseCalificacion { get; set; }
        public string NombreCriterio { get; set; }
        public int Orden { get; set; }
    }

    public class LineamientoDTO
    {
        public int Id { get; set; }
        public int IdCriterioCalificacionLlamada { get; set; }
        public int IdCriticidadCalificacion { get; set; }
        public string NombreLineamiento { get; set; }
        public int Orden { get; set; }
    }

    public class CriticidadDTO
    {
        public int Id { get; set; }
        public string NombreCriticidad { get; set; }
    }

    public class PuntoGeneralDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }
    public class ConfiguracionLineamientoDTO
    {
        public List<FaseDTO> FasesCalificacion { get; set; }
        public List<CriterioDTO> CriteriosCalificacion { get; set; }
        public List<LineamientoDTO> LineamientosCalificacion { get; set; }
        public List<CriticidadDTO> CriticidadCalificacion { get; set; }
        public List<PuntoGeneralDTO> PuntosGeneralesCalificacion { get; set; }
    }
    public class ResultadoEvaluacionBatch
    {
        [JsonPropertyName("graded_result")]
        public GradedResult GradedResult { get; set; }

        [JsonPropertyName("idLlamada")]
        public int IdLlamada { get; set; }
        [JsonPropertyName("idPersonal")]
        public int? IdPersonal { get; set; }
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }
        [JsonPropertyName("contacto")]
        public string? contacto { get; set; }

    }

    public class ResultadoEvaluacion
    {
        [JsonPropertyName("graded_result")]
        public GradedResult GradedResult { get; set; }
    }

    public class GradedResult
    {
        [JsonPropertyName("fases")]
        public List<FaseEvaluacion> Fases { get; set; }

        [JsonPropertyName("puntosgenerales")]
        public List<PuntoGeneral> Puntosgenerales { get; set; }

        [JsonPropertyName("puntoscriticos")]
        public List<PuntoCritico>? Puntoscriticos { get; set; }
        [JsonPropertyName("interrupcionllamada")]
        public string? InterrupcionLlamada { get; set; }



    }

    public class FaseEvaluacion
    {
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("criterios")]
        public List<CriterioEvaluacion> Criterios { get; set; }

        [JsonPropertyName("justificaciongeneral")]
        public string JustificacionGeneral { get; set; }

        [JsonPropertyName("brechageneral")]
        public string BrechaGeneral { get; set; }
    }

    public class CriterioEvaluacion
    {
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("lineamientos")]
        public List<LineamientoEvaluacion> Lineamientos { get; set; }

        [JsonPropertyName("calificacion")]
        public int Calificacion { get; set; }

        [JsonPropertyName("justificacion")]
        public string Justificacion { get; set; }
        [JsonPropertyName("brecha")]
        public string Brecha { get; set; }
    }
    public class LineamientoEvaluacion
    {
        [JsonPropertyName("lineamiento")]
        public string Lineamiento { get; set; }

        [JsonPropertyName("importancia")]
        public string Importancia { get; set; }
    }

    public class PuntoGeneral
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("calificacion")]
        public decimal Calificacion { get; set; }

        [JsonPropertyName("justificacion")]
        public string Justificacion { get; set; }
    }

    public class PuntoCritico
    {
        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; }

        [JsonPropertyName("calificacion")]
        public decimal? Calificacion { get; set; }

        [JsonPropertyName("feedback")]
        public string? Feedback { get; set; }
    }

    public class RecomendacionLlamadaDTO
    {
        [JsonProperty("idLlamada")]
        public string IdLlamada { get; set; }

        [JsonProperty("recomendaciones")]
        public List<string> Recomendaciones { get; set; }
    }

    public class InsertRecomendacionResultDTO
    {
        public int IdTranscripcionLlamada { get; set; }
        public int RecomendacionesInsertadas { get; set; }
    }



    /*IINICIO DTOS REPÔRTES*/

    public class ReporteCalificacionRequest
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<int>? IdsAsesores { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdFaseI { get; set; }
        public int? IdFaseD { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanioPagina { get; set; } = 10;
    }

    public class ReporteJsonWrapper
    {
        public List<LlamadaCalificadaRawDTO> Items { get; set; }
        public int TotalRegistros { get; set; }
    }

    public class LlamadaCalificadaRawDTO
    {
        public int IdLlamada { get; set; }
        public int IdOportunidad { get; set; }
        public DateTime FechaInicioLlamadaCentral { get; set; }
        public int DuracionContestoCentral { get; set; }
        public int IdAlumno { get; set; }
        public string NombreCliente { get; set; }
        public int IdAsesor { get; set; }
        public string NombreAsesor { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreFaseI { get; set; }
        public string CodigoFaseI { get; set; }
        public string NombreFaseD { get; set; }
        public string CodigoFaseD { get; set; }
        public decimal PuntajePromedio { get; set; }
        public bool TipoCalificacion { get; set; }
        public int IdCalificacionLlamada { get; set; }
        public int IdCriterioCalificacion { get; set; }
        public string Comentario { get; set; }
        public int? IdOcurrenciaPadreAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdVersionConfiguracionCalificacioLlamada { get; set; }

        public string? OcurrenciaPadreAlterno { get; set; }
        public string? OcurrenciaAlterno { get; set; }
        public string? EstadoOcurrenciaAlterno { get; set; }
        public string? ComentarioLlamadaNoCalificada { get; set; }
        public bool? OcurrenciaConsistente { get; set; }

        public string? ComentarioConsistenciaOcurrencia { get; set; }





    }

    public class LlamadaCalificadaDTO
    {
        public int IdLlamada { get; set; }
        public int IdOportunidad { get; set; }
        public DateTime FechaInicioLlamadaCentral { get; set; }
        public int? DuracionContestoCentral { get; set; }

        public int IdAlumno { get; set; }
        public string NombreCliente { get; set; }

        public int IdAsesor { get; set; }
        public string NombreAsesor { get; set; }

        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }

        public string NombreFaseI { get; set; }
        public string CodigoFaseI { get; set; }
        public string NombreFaseD { get; set; }
        public string CodigoFaseD { get; set; }



        // Promedio calculado en service EXCLUYENDO -1
        public decimal? Promedio { get; set; }

        public int? IdOcurrenciaPadreAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdVersionConfiguracionCalificacioLlamada { get; set; }

        public string? OcurrenciaPadreAlterno { get; set; }
        public string? OcurrenciaAlterno { get; set; }
        public string? EstadoOcurrenciaAlterno { get; set; }
        public string? ComentarioLlamadaNoCalificada { get; set; }
        public bool? OcurrenciaConsistente { get; set; }

        public string? ComentarioConsistenciaOcurrencia { get; set; }


        public List<PuntoCriticoDTO> PuntosCriticos { get; set; }
    }
    public class PuntoCriticoDTO
    {
        public decimal Nota { get; set; }
        public bool TipoCalificacion { get; set; }
        public string Comentario { get; set; }
    }
    public class ReporteCalificacionResponse
    {
        public int TotalRegistros { get; set; }
        public IEnumerable<LlamadaCalificadaDTO> Data { get; set; }
    }


    public class ReporteCalificacionGlobalRequest
    {
        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        public List<int>? IdsAsesores { get; set; }

        public int? IdCentroCosto { get; set; }

        public int? IdFaseI { get; set; }

        public int? IdFaseD { get; set; }
    }
    public class ReporteCalificacionGlobalResponse
    {
        public int TotalLlamadas { get; set; }
        public decimal PromedioGlobal { get; set; }
        public int TotalCalificaciones { get; set; }
        public DateTime FechaCalculo { get; set; }
    }
    public class ReporteCalificacionGlobalWrapper
    {
        public int TotalLlamadas { get; set; }
        public decimal PromedioGlobal { get; set; }
        public int TotalCalificaciones { get; set; }
    }
    /// <summary>
    /// DTO para calificaciones por fase
    /// </summary>
    public class CalificacionFaseDTO
    {
        public int Id { get; set; }
        public int IdLlamadaWebphoneCruceCentralTresCx { get; set; }
        public int IdVersionConfiguracionCalificacioLlamada { get; set; }
        public int IdFaseCalificacion { get; set; }
        public string Comentario { get; set; }
        public string Brecha { get; set; }
        public bool TipoCalificacion { get; set; }
    }
    /*FIN DE DTOS REPORTES CALIFICACION*/


    /// <summary>
    /// DTO para información de llamadas (vista com.V_ObtenerInformacionLlamadaCruceCentralTresCx)
    /// </summary>
    public class InformacionLlamadaTresCxDTO
    {
        public int IdActividadDetalle { get; set; }
        public int IdRegistroLlamada { get; set; }

        public System.DateTime? FechaInicioLlamada { get; set; }
        public System.DateTime? FechaFinLlamada { get; set; }

        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }

        public string EstadoLlamada { get; set; }
        public string SubEstadoLlamada { get; set; }
        public string NombreGrabacion { get; set; }
        public string UrlGrabacion { get; set; }
        public string UrlGrabacion2 { get; set; }
        public string WebphoneGrabacion { get; set; }
        public string TelefonoDestinoReal { get; set; }
        public string TelefonoDestino { get; set; }
        public string OrigenLlamada { get; set; }
        public string AnexoCentral { get; set; }

        public int Tipo { get; set; }

        public string OrigenReal { get; set; }

        public bool EsLlamadaCalificada { get; set; }
        public bool EsLlamadaTranscrita { get; set; }
    }
}
