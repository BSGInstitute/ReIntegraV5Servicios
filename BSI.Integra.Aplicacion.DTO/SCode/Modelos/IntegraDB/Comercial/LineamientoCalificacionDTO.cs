

using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Google.Api.Ads.AdWords.v201809;
using Mandrill.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;

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
        public int? IdPersonalAreaTrabajo {get; set;}
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
        public int IdVersionConfiguracionCalificacionLlamada { get; set; }
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
        public int IdVersionConfiguracionCalificacionLlamada { get; set; }
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
        public string ComentarioAvance { get; set; }
        public int PorcentajeAvance { get; set; }
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
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdPersonal_Asignado { get; set; }
        public string Comentario { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public int? IdOcurrencia { get; set; }
        public string Ocurrencia { get; set; }
        public string OcurrenciaAlterno { get; set; }
        public int IdOportunidadLog { get; set; }
        public int IdAlumno { get; set; }
        public int IdCodigoPais { get; set; }
        public int IdCentroCosto { get; set; }

        public int IdClasificacionPersona { get; set; }
        public int IdFaseOportunidad_Ant { get; set; }
        public int IdFaseOportunidad { get; set; }
        public string FaseOportunidad_Ant { get; set; }
        public string FaseOportunidad{ get; set; }

        public string UrlAudioProcesado { get; set; }
        public string Origen { get; set; }
        public int? DuracionContestoCentral { get; set; }
        public bool? EsLlamadaTranscrita { get; set; } = false;
        public bool? EsLlamadaCalificada { get; set; }
        public DateTime? FechaLlamada { get; set; }
    }
    public class LlamadaProcesoAutoAtencioClienteDTO
    {
        public int IdActividadDetalle { get; set; }
        public int IdLlamada { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdPersonalAsignado { get; set; }
        public string Comentario { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public string NombreOcurrencia { get; set; }
        public int IdOportunidadLog { get; set; }
        public int IdAlumno { get; set; }
        public int IdCodigoPais { get; set; }
        public int IdCentroCosto { get; set; }

        public int IdClasificacionPersona { get; set; }

        public int IdFaseOportunidadAnterior { get; set; }
        public int IdFaseOportunidadActual { get; set; }
        public string FaseOportunidad_Ant { get; set; }
        public string FaseOportunidad { get; set; }

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
        public string Nombre { get; set; }
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
        [JsonPropertyName("idPersonalAreaTrabajo")]
        public int IdPersonalAreaTrabajo { get; set; }


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

        [JsonPropertyName("comentario_avance_fase")]
        public string ComentarioAvance { get; set; }

        [JsonPropertyName("porcentaje_avance_fase")]
        public int PorcentajeAvance { get; set; }
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
    /*eliminar ReporteCalificacionRequest en cuanto se suba nueva version por area*/
    public class ReporteCalificacionRequest
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<int>? IdsAsesores { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdFaseI { get; set; }
        public int? IdFaseD { get; set; }
        public int? EstadoActividadCabecera { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanioPagina { get; set; } = 10;
    }
    public class ReporteCalificacionAreaRequest
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<int>? IdsAsesores { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdFaseI { get; set; }
        public int? IdFaseD { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? EstadoActividadCabecera { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanioPagina { get; set; } = 10;
    }

    public class ReporteJsonWrapper
    {
        public List<LlamadaCalificadaRawDTO> Items { get; set; }
        public int TotalRegistros { get; set; }
    }
    public class ReporteJsonWrapperAtencionCliente
    {
        public List<LlamadaCalificadaAtencionClienteRawDTO> Items { get; set; }
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
        public int? IdVersionConfiguracionCalificacionLlamada { get; set; }

        public string? OcurrenciaPadreAlterno { get; set; }
        public string? OcurrenciaAlterno { get; set; }
        public string? EstadoOcurrenciaAlterno { get; set; }
        public string? ComentarioLlamadaNoCalificada { get; set; }
        public bool? OcurrenciaConsistente { get; set; }

        public string? ComentarioConsistenciaOcurrencia { get; set; }
        public bool? CambioFaseConsistente { get; set; }

        public string? ComentarioConsistenciaCambioFase { get; set; }
        public string? InterrupcionLlamada { get; set; }
        public string? PuntoCritico { get; set; }
    }

    public class LlamadaCalificadaAtencionClienteRawDTO
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

        public decimal? PuntajePromedio { get; set; }

        public int TipoCalificacion { get; set; }

        public int IdCalificacionLlamada { get; set; }

        public string Comentario { get; set; }

        public int? IdOcurrenciaPadre { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public int? IdOcurrencia { get; set; }

        public string? OcurrenciaPadre { get; set; }
        public string? Ocurrencia { get; set; }
        public string? EstadoOcurrencia { get; set; }

        public bool? OcurrenciaConsistente { get; set; }
        public string? ComentarioConsistenciaOcurrencia { get; set; }

        public bool? CambioFaseConsistente { get; set; }
        public string? ComentarioConsistenciaCambioFase { get; set; }

        public string? InterrupcionLlamada { get; set; }
        public string? PuntoCritico { get; set; }
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
        public int? IdVersionConfiguracionCalificacionLlamada { get; set; }

        public string? OcurrenciaPadreAlterno { get; set; }
        public string? OcurrenciaAlterno { get; set; }
        public string? EstadoOcurrenciaAlterno { get; set; }
        public string? ComentarioLlamadaNoCalificada { get; set; }
        public bool? OcurrenciaConsistente { get; set; }

        public string? ComentarioConsistenciaOcurrencia { get; set; }
        public bool? CambioFaseConsistente { get; set; }

        public string? ComentarioConsistenciaCambioFase { get; set; }

        public string? InterrupcionLlamada { get; set; }


        public List<string> PuntosCriticos { get; set; }
    }
    public class LlamadaCalificadaAtencionClienteDTO
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

        public decimal? Promedio { get; set; }

        // Cambios: se reemplazan los campos *Alterno* por los nombres reales del SP
        public int? IdOcurrenciaPadre { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public int? IdOcurrencia { get; set; }

        public string? OcurrenciaPadre { get; set; }
        public string? Ocurrencia { get; set; }
        public string? EstadoOcurrencia { get; set; }

        public string? ComentarioLlamadaNoCalificada { get; set; }

        public bool? OcurrenciaConsistente { get; set; }
        public string? ComentarioConsistenciaOcurrencia { get; set; }

        public bool? CambioFaseConsistente { get; set; }
        public string? ComentarioConsistenciaCambioFase { get; set; }

        public string? InterrupcionLlamada { get; set; }

        public List<string> PuntosCriticos { get; set; }
    }

    public class PuntoCriticoDTO
    {
        public decimal? Nota { get; set; }
        public bool? TipoCalificacion { get; set; }
        public string? PuntoCritico { get; set; }
    }
    public class ReporteCalificacionResponse
    {
        public int TotalRegistros { get; set; }
        public IEnumerable<LlamadaCalificadaDTO> Data { get; set; }
    }
    public class ReporteCalificacionAtencionClienteResponse
    {
        public int TotalRegistros { get; set; }
        public IEnumerable<LlamadaCalificadaAtencionClienteDTO> Data { get; set; }
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
        public int? EstadoActividadCabecera { get; set; }
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
        public int IdVersionConfiguracionCalificacionLlamada { get; set; }
        public int IdFaseCalificacion { get; set; }
        public string Comentario { get; set; }
        public string Brecha { get; set; }
        public bool TipoEvaluacion { get; set; }
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
    /// <summary>
    /// DTO para representar la información de llamadas webphone con sus ocurrencias y estados
    /// </summary>
    public class LlamadaWebphoneOcurrenciaDTO
    {
        /// <summary>
        /// Identificador único del alumno
        /// </summary>
        public int IdAlumno { get; set; }

        /// <summary>
        /// Nombre completo del cliente (alumno)
        /// </summary>
        public string NombreCliente { get; set; }

        /// <summary>
        /// Identificador único de la llamada
        /// </summary>
        public int IdLlamada { get; set; }

        /// <summary>
        /// Identificador único de la oportunidad
        /// </summary>
        public int IdOportunidad { get; set; }

        /// <summary>
        /// Identificador de la ocurrencia padre alterno (puede ser nulo)
        /// </summary>
        public int? IdOcurrenciaPadreAlterno { get; set; }

        /// <summary>
        /// Identificador de la ocurrencia de actividad alterno (puede ser nulo)
        /// </summary>
        public int? IdOcurrenciaActividadAlterno { get; set; }

        /// <summary>
        /// Identificador de la ocurrencia alterno (puede ser nulo)
        /// </summary>
        public int? IdOcurrenciaAlterno { get; set; }

        /// <summary>
        /// Nombre de la ocurrencia padre alterno
        /// </summary>
        public string OcurrenciaPadreAlterno { get; set; }

        /// <summary>
        /// Nombre de la ocurrencia alterno
        /// </summary>
        public string OcurrenciaAlterno { get; set; }

        /// <summary>
        /// Estado de la ocurrencia alterno (con lógica de reprogramación manual/automática)
        /// </summary>
        public string EstadoOcurrenciaAlterno { get; set; }

        /// <summary>
        /// Fecha real de la actividad
        /// </summary>
        public DateTime FechaReal { get; set; }
    }

    public class HistoricoOcurrenciaAtencionClienteDto
    {
        public int IdLlamada { get; set; }
        public string EstadoOcurrencia { get; set; }
        public int IdAlumno { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public int? IdOcurrenciaPadre { get; set; }
        public int IdOportunidad { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreCliente { get; set; }
        public string? Ocurrencia { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public string? OcurrenciaPadre { get; set; }
    }



    /// <summary>
    /// DTO que representa la información de la vista de transiciones de fase de oportunidad y sus criterios/lineamientos.
    /// </summary>
    public class  TransicionCambioFaseOportunidadDTO
    {
        public int IdTransicionFaseOportunidad { get; set; }

        public int IdFaseOrigen { get; set; }
        public string NombreFaseOrigen { get; set; }
        public string CodigoFaseOrigen { get; set; }

        public int IdFaseDestino { get; set; }
        public string NombreFaseDestino { get; set; }
        public string CodigoFaseDestino { get; set; }

        public int IdCriterio { get; set; }
        public int OrdenCriterio { get; set; }
        public string NombreCriterio { get; set; }

        public int IdLineamientoCalificacionFase { get; set; }
        public int OrdenLineamiento { get; set; }
        public string NombreLineamientoCalificacionFase { get; set; }
        public int IdCriticidadCalificacion { get; set; }

        public string NombreCriticidad { get; set; }
    }
    public class TransicionFaseResponse
    {
        public List<TransicionFase> TransicionesFase { get; set; }
    }

    public class TransicionFase
    {
        public int IdTransicionFaseOportunidad { get; set; }
        public Fase FaseOrigen { get; set; }
        public Fase FaseDestino { get; set; }
        public List<Criterio> Criterios { get; set; }
    }

    public class Fase
    {
        public int IdFaseDestino { get; set; }
        public string NombreFaseDestino { get; set; }
        public string CodigoFaseDestino { get; set; }
        public int IdFaseOrigen { get; set; }
        public string NombreFaseOrigen { get; set; }
        public string CodigoFaseOrigen { get; set; }
    }

    public class Criterio
    {
        public int IdCriterio { get; set; }
        public int OrdenCriterio { get; set; }
        public string NombreCriterio { get; set; }
        public List<Lineamiento> Lineamientos { get; set; }
    }

    public class Lineamiento
    {
        public int IdLineamientoCalificacionFase { get; set; }
        public int OrdenLineamiento { get; set; }
        public string NombreLineamientoCalificacionFase { get; set; }
        public Criticidad Criticidad { get; set; }
    }

    public class Criticidad
    {
        public int IdCriticidadCalificacion { get; set; }
        public string NombreCriticidad { get; set; }
    }

    /*PUNTO CRITICOS DTO */

    public class PuntosCriticosLlamadaDiaDto
    {
        public DateTime FechaReal { get; set; }
        public int IdLlamadaWebphoneCruceCentralTresCx { get; set; }
        public int IdPersonal { get; set; }
        public string PuntoCritico { get; set; } = null!;
        public string? ResumenLlamada { get; set; }
    }

    public class RecomendacionPuntoCriticoLlamadaDTO
    {
        public List<LlamadaPuntoCriticoDTO> items { get; set; } = new();
    }
    public class ResultadoPuntoCriticoConsolidaddoDTO
    {
        public List<string> consolidadocritico { get; set; }
    }

    public class LlamadaPuntoCriticoDTO
    {
        public string idLlamada { get; set; }
        public string? summary { get; set; }
        public List<string> puntoscriticos { get; set; } = new();
    }

    public class AnalisisLlamadasRespuestaDTO
    {
        public bool Ok { get; set; }
        public string? Mensaje { get; set; }
        public object? Data { get; set; }
    }
    public class PuntoCriticoResumenDiarioDTO
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdPersonal { get; set; }
        public string PuntoCritico { get; set; }
    }
    public class PuntoCriticoResumenEntradaDTO
    {
        public int IdPersonal { get; set; }
        public DateTime FechaGeneracion { get; set; }
    }
    //DTOs Calificacion Individual
    public class CalificacionIndividualRequestDTO
    {
        public int IdPersonal { get; set; }
        public string Contacto { get; set; }
        public string UserName { get; set; }
        public string IdCodigoPais { get; set; }

        [JsonPropertyName("transcription")]
        public List<TranscriptionWebhookPayloadDTO> Transcription { get; set; }

        public LineamientoDto Lineamientos { get; set; }
        public BrochureDTO Brochure { get; set; }
    }

    public class LineamientoDto
    {
        public Dictionary<string, FaseDto> Fases { get; set; }

        [JsonPropertyName("puntosgenerales")]
        public List<PuntoGeneralCalifDTO> PuntosGenerales { get; set; }
    }

    public class FaseDto
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public Dictionary<string, CriterioCalifDTO> Criterios { get; set; }
    }

    public class CriterioCalifDTO
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public List<LineamientoCalifDTO> Lineamientos { get; set; }
    }

    public class LineamientoCalifDTO
    {
        public int Id { get; set; }
        public int Orden { get; set; }

        [JsonPropertyName("lineamiento")]
        [Required]
        public string NombreLineamiento { get; set; }

        [JsonPropertyName("importancia")]
        public string Importancia { get; set; }
    }




    public class PuntoGeneralCalifDTO
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class BrochureDTO
    {
        public int IdPGeneral { get; set; }
        public string InformacionPrograma { get; set; }
        public List<ResumenProgramaV2DTO> ResumenProgramasV2 { get; set; }
        public string EtiquetaDuracionHorarios { get; set; }

        [JsonPropertyName("etiquetaExpositores")]
        public string? EtiquetaExpositores { get; set; }

        public string EtiquetaBeneficiosInversion { get; set; }
        public string EtiquetaFormasPago { get; set; }
        public string EtiquetaTarifarios { get; set; }
        public List<BeneficioDTO> ListaBeneficios { get; set; }

        public object InformacionDatoCliente { get; set; }
        public object InformacionPerfilyFIltradoProspectos { get; set; }
        public object ArgumentosMotivacionPrograma { get; set; }
        public object InformacionOportunidad { get; set; }
        public object PublicoObjetivoDirecto { get; set; }
        public object ProblemaDetallePrograma { get; set; }
    }
    public class ResumenProgramaV2DTO
    {
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public string NombrePrograma { get; set; }
        public string Duracion { get; set; }
        public string Inversion { get; set; }
        public string Certificacion { get; set; }
    }
    public class BeneficioDTO
    {
        public int Paquete { get; set; }
        public string Titulo { get; set; }
        public int OrdenBeneficio { get; set; }
    }
    public class CalificacionIndividualResponseDTO
    {
        public int IdEvaluacion { get; set; }
        public string Estado { get; set; }
        public string Mensaje { get; set; }
    }
    public class EvaluacionLlamadaDetalleDTO
        {
    /// <summary>
    /// ID de la llamada (IdLlamadaWebphoneCruceCentralTresCx)
    /// </summary>
    public int IdLlamadaWebphoneCruceCentralTresCx { get; set; }
        /// <summary>
        /// Estado de la evaluación (1 = Activo, 0 = Inactivo)
        /// </summary>
    public int IdCriterioCalificacionLlamada { get; set; }

    /// <summary>
    /// Nota obtenida en el criterio
    /// </summary>
    public decimal Nota { get; set; }

    /// <summary>
    /// Comentario de la evaluación para el criterio
    /// </summary>
    public string? Comentario { get; set; }
 
    /// <summary>
    /// Brechas identificadas en el criterio
    /// </summary>
    public string? Brecha { get; set; }
      } 
    public class EvaluacionPuntoGeneralDetalleDTO
{
    public int IdLlamadaWebphoneCruceCentralTresCx { get; set; }
    public int IdCalificacionPuntoGeneral { get; set; }
    public decimal Nota { get; set; }
    public string? Comentario { get; set; }
    public string? Brecha { get; set; }
}
    public class ConfiguracionEsquemaCalificacionPorLlamdaDTO
    {
        public int IdEvaluacionLlamada { get; set; }
        public int IdVersionConfiguracionCalificacionLlamada { get; set; }
        public string ConfiguracionJSON { get; set; }
        
    }
    public class EvaluacionLlamadaJerarquicaDTO
    {
        public string TipoEntidad { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Orden { get; set; }
        public int? IdPadre { get; set; }
        public string NombrePadre { get; set; }
        public int? IdCriticidad { get; set; }
        public string NombreCriticidad { get; set; }
        public string Descripcion { get; set; }
    }

    public class EvaluacionLlamadaFaseDTO
    {
        public int Id { get; set; }
        [JsonPropertyName("Nombre")]
        public string NombreFase { get; set; }
        public int Orden { get; set; }
        public string Descripcion { get; set; }
    }

    public class EvaluacionLlamadaCriterioDTO
    {
        public int Id { get; set; }
        public int IdFaseCalificacion { get; set; }
        public string NombreCriterio { get; set; }
        public int Orden { get; set; }
        public string Descripcion { get; set; }
    }

    public class EvaluacionLlamadaLineamientoDTO
    {
        public int Id { get; set; }
        public int IdCriterioCalificacionLlamada { get; set; }
        public int IdCriticidadCalificacion { get; set; }
        public string NombreLineamiento { get; set; }
        public int Orden { get; set; }
        public string Descripcion { get; set; }
    }

    public class EvaluacionLlamadaCriticidadDTO
    {
        public int Id { get; set; }
        [JsonPropertyName("NombreCriticidad")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class EvaluacionLlamadaPuntoGeneralDTO
    {
        public int Id { get; set; }
        [JsonPropertyName("Nombre")]
        public string Nombre { get; set; }

        public int Orden { get; set; }
        public string Descripcion { get; set; }
    }
    public class ConfiguracionLineamientoV2DTO
    {
        public List<EvaluacionLlamadaFaseDTO> FasesCalificacion { get; set; } = new List<EvaluacionLlamadaFaseDTO>();
        public List<EvaluacionLlamadaCriterioDTO> CriteriosCalificacion { get; set; } = new List<EvaluacionLlamadaCriterioDTO>();
        public List<EvaluacionLlamadaLineamientoDTO> LineamientosCalificacion { get; set; } = new List<EvaluacionLlamadaLineamientoDTO>();
        public List<EvaluacionLlamadaCriticidadDTO> CriticidadCalificacion { get; set; } = new List<EvaluacionLlamadaCriticidadDTO>();
        public List<EvaluacionLlamadaPuntoGeneralDTO> PuntosGeneralesCalificacion { get; set; } = new List<EvaluacionLlamadaPuntoGeneralDTO>();
    }
    public class ConfiguracionVigenteJerarquiaDTO
    {
        public string TipoEntidad { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Orden { get; set; }
        public int? IdPadre { get; set; }
        public string NombrePadre { get; set; }
        public int? IdCriticidad { get; set; }
        public string NombreCriticidad { get; set; }
        public string Descripcion { get; set; }
    }
    public class VersionConfiguracionPorLlamadaDTO
    {
        public int IdVersionConfiguracionCalificacionLlamada { get; set; }
    }

    /// <summary>
    /// DTO para calificación manual en tiempo real usando tablas temporales.
    /// Utiliza IdActividadDetalle + NumeroLlamada en lugar de IdLlamada.
    /// </summary>
    public class CalificacionLlamadaManualTemporalDTO
    {
        /// <summary>
        /// ID de la actividad detalle (usado temporalmente hasta obtener IdLlamada definitivo)
        /// </summary>
        public int IdActividadDetalle { get; set; }

        /// <summary>
        /// Número secuencial de la llamada dentro de la actividad (1, 2, 3, etc.)
        /// </summary>
        public int NumeroLlamada { get; set; }

        /// <summary>
        /// ID de la versión de configuración de calificación
        /// </summary>
        public int IdVersion { get; set; }

        /// <summary>
        /// Tipo de evaluación: false=Manual, true=Automática
        /// </summary>
        public bool TipoEvaluacion { get; set; }

        /// <summary>
        /// Lista de calificaciones por criterio
        /// </summary>
        public List<DetalleCalificacionMualDTO> Calificaciones { get; set; }

        /// <summary>
        /// Lista de calificaciones de puntos generales
        /// </summary>
        public List<DetalleCalificacionPuntoGeneralDTO> CalificacionesPuntosGenerales { get; set; }

        /// <summary>
        /// Usuario que realiza la calificación
        /// </summary>
        public string Usuario { get; set; }
    }

}
