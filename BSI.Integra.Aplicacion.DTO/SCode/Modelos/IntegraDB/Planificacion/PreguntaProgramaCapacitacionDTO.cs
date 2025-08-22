using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PreguntaProgramaCapacitacionDTO
    {
        public int? Id { get; set; }
        public int IdPgeneral { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public int? OrdenFilaSesion { get; set; }
        public int? IdTipoRespuesta { get; set; }
        public int? IdPreguntaEscalaValor { get; set; }
        public string? EnunciadoPregunta { get; set; }
        public bool? RequiereTiempo { get; set; }
        public int? MinutosPorPregunta { get; set; }
        public bool? RespuestaAleatoria { get; set; }
        public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
        public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
        public bool? MostrarFeedbackInmediato { get; set; }
        public bool? MostrarFeedbackPorPregunta { get; set; }
        public int? IdPreguntaIntento { get; set; }
        public int? IdPreguntaTipo { get; set; }
        public int? IdTipoRespuestaCalificacion { get; set; }
        public int? FactorRespuesta { get; set; }
        public string? GrupoPregunta { get; set; }
        public int? IdTipoMarcador { get; set; }
        public decimal? ValorMarcador { get; set; }
        public int? OrdenPreguntaGrupo { get; set; }
        public int? IdPespecifico { get; set; }
    }
    public class PreguntaPorProgramaDTO
    {
        public int? Id { get; set; }
        public string? EnunciadoPregunta { get; set; }
    }
    public class GrupoPreguntaProgramaCapacitacionDTO
    {
        public int IdPgeneral { get; set; }
        public string? GrupoPregunta { get; set; }
        public int? IdTipoVista { get; set; }
        public int Segundos { get; set; }
    }
    public class ReporteExcelPreguntasInteractivasDTO
    {
        public int? Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public int OrdenFilaCapitulo { get; set; }
        public string Sesion { get; set; }
        public string SubSesion { get; set; }
        public string GrupoPregunta { get; set; }
        public int IdTipoMarcador { get; set; }
        public int ValorMarcador { get; set; }
        public int OrdenPreguntaGrupo { get; set; }
        public int IdTipoRespuesta { get; set; }
        public int IdPreguntaTipo { get; set; }
        public string EnunciadoPregunta { get; set; }
        public int MinutosPorPregunta { get; set; }
        public bool RespuestaAleatoria { get; set; }
        public bool ActivarFeedBackRespuestaCorrecta { get; set; }
        public bool ActivarFeedBackRespuestaIncorrecta { get; set; }
        public bool MostrarFeedbackInmediato { get; set; }
        public bool MostrarFeedbackPorPregunta { get; set; }
        public int NumeroMaximoIntento { get; set; }
        public bool ActivarFeedbackMaximoIntento { get; set; }
        public string MensajeFeedback { get; set; }
        public int IdTipoRespuestaCalificacion { get; set; }
        public int FactorRespuesta { get; set; }
        public bool RespuestaCorrecta { get; set; }
        public int NroOrden { get; set; }
        public string EnunciadoRespuesta { get; set; }
        public int Puntaje { get; set; }
        public string FeedbackPositivo { get; set; }
        public string FeedbackNegativo { get; set; }
        public int PorcentajeCalificacion { get; set; }
    }
    public class ReporteExcelPreguntasInteractivasPrevioDTO
    {
        public int? Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public int OrdenFilaCapitulo { get; set; }
        public string GrupoPregunta { get; set; }
        public int IdTipoMarcador { get; set; }
        public int ValorMarcador { get; set; }
        public int OrdenPreguntaGrupo { get; set; }
        public int IdTipoRespuesta { get; set; }
        public int? IdPreguntaTipo { get; set; }
        public string EnunciadoPregunta { get; set; }
        public int MinutosPorPregunta { get; set; }
        public bool RespuestaAleatoria { get; set; }
        public bool ActivarFeedBackRespuestaCorrecta { get; set; }
        public bool ActivarFeedBackRespuestaIncorrecta { get; set; }
        public bool MostrarFeedbackInmediato { get; set; }
        public bool MostrarFeedbackPorPregunta { get; set; }
        public int NumeroMaximoIntento { get; set; }
        public bool ActivarFeedbackMaximoIntento { get; set; }
        public string MensajeFeedback { get; set; }
        public int IdTipoRespuestaCalificacion { get; set; }
        public int FactorRespuesta { get; set; }
        public bool RespuestaCorrecta { get; set; }
        public int NroOrden { get; set; }
        public string EnunciadoRespuesta { get; set; }
        public int Puntaje { get; set; }
        public string FeedbackPositivo { get; set; }
        public string FeedbackNegativo { get; set; }
        public int IdPreguntaIntento { get; set; }
        public int OrdenFilaSesion { get; set; }
    }
    public class CompuestoPreguntaProgramaCapacitacionDTO
    {
        public PreguntaProgramaCapacitacionDTO PreguntaProgramaCapacitacion { get; set; }
        public PreguntaIntentoDTO PreguntaIntento { get; set; }
        public List<RespuestaPreguntaProgramaCapacitacionDTO> RespuestaPreguntaProgramaCapacitacions { get; set; }
    }
    public class PreguntaProgramaCapacitacionCombosModuloDTO
    {
        public List<PreguntaTipoRespuestaDTO> PreguntaTipoRespuestas { get; set; }
        public List<ComboDTO> PGenerals { get; set; }
        public List<ComboDTO> TipoRespuestaCalificacions { get; set; }
        public List<ComboDTO> TipoMarcadors { get; set; }
        public List<PEspecificoPGeneralFiltroDTO> PEspecificos { get; set; }
    }
    public class ListadoPreguntaPorEstructuraDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int OrdenFilaCapitulo { get; set; }
        public int OrdenFilaSesion { get; set; }
        public string GrupoPregunta { get; set; }
        public int IdTipoVista { get; set; }
        public int Segundos { get; set; }
        public int OrdenPreguntaGrupo { get; set; }
        public string EnunciadoPregunta { get; set; }
        public bool RespuestaAleatoria { get; set; }
        public bool MostrarFeedbackInmediato { get; set; }
        public bool MostrarFeedbackPorPregunta { get; set; }
        public int NumeroMaximoIntento { get; set; }
        public int TipoRespuesta { get; set; }
    }
    public class ImportacionPreguntaRespuestaProgramaCapacitacionDTO
    {
        public int? NumeroMaximoIntento { get; set; }
        public bool? ActivarFeedbackMaximoIntento { get; set; }
        public string MensajeFeedback { get; set; }
        public int? IdTipoRespuesta { get; set; }
        public string EnunciadoPregunta { get; set; }
        public int? MinutosPorPregunta { get; set; }
        public bool? RespuestaAleatoria { get; set; }
        public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
        public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
        public bool? MostrarFeedbackInmediato { get; set; }
        public bool? MostrarFeedbackPorPregunta { get; set; }
        public int? IdPreguntaTipo { get; set; }
        public int? IdTipoRespuestaCalificacion { get; set; }
        public int? FactorRespuesta { get; set; }
        public int IdPgeneral { get; set; }
        public int? IdPEspecifico { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public string Sesion { get; set; }
        public string SubSeccion { get; set; }
        public string GrupoPregunta { get; set; }
        public int? IdTipoMarcador { get; set; }
        public decimal? ValorMarcador { get; set; }
        public int? OrdenPreguntaGrupo { get; set; }
        //============RESPUESTAS=============================

        public bool? RespuestaCorrecta { get; set; }
        public int NroOrden { get; set; }
        public string EnunciadoRespuesta { get; set; }
        public int? NroOrdenRespuesta { get; set; }
        public int? Puntaje { get; set; }
        public string FeedbackPositivo { get; set; }
        public string FeedbackNegativo { get; set; }

        //====================PREGUNTAINTENTO=============
        public int? PorcentajeCalificacion { get; set; }
    }
    public class RespuestaPreguntaProgramaCapacitacionAgrupadoDTO
    {
        public bool? RespuestaCorrecta { get; set; }
        public int NroOrden { get; set; }
        public string EnunciadoRespuesta { get; set; }
        public int? NroOrdenRespuesta { get; set; }
        public int? Puntaje { get; set; }
        public string FeedbackPositivo { get; set; }
        public string FeedbackNegativo { get; set; }
    }

    public class PreguntaProgramaCapacitacionExcelCompuestoDTO
    {
        public PreguntaProgramaCapacitacionAgrupadoDTO PreguntaProgramaCapacitacion { get; set; }
    }
    public class PreguntaProgramaCapacitacionAgrupadoDTO
    {
        public int? IdTipoRespuesta { get; set; }
        public string EnunciadoPregunta { get; set; }
        public int? MinutosPorPregunta { get; set; }
        public bool? RespuestaAleatoria { get; set; }
        public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
        public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
        public bool? MostrarFeedbackInmediato { get; set; }
        public bool? MostrarFeedbackPorPregunta { get; set; }
        public int? IdPreguntaTipo { get; set; }
        public int? IdTipoRespuestaCalificacion { get; set; }
        public int? FactorRespuesta { get; set; }
        public int IdPgeneral { get; set; }
        public int? IdPEspecifico { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public string Sesion { get; set; }
        public string SubSeccion { get; set; }
        public string GrupoPregunta { get; set; }
        public int? IdTipoMarcador { get; set; }
        public decimal? ValorMarcador { get; set; }
        public int? OrdenPreguntaGrupo { get; set; }
        public PreguntaIntentoAgrupadoDTO PreguntaIntento { get; set; }
        public List<RespuestaPreguntaProgramaCapacitacionAgrupadoDTO> RespuestaPreguntaProgramaCapacitacion { get; set; }
    }
    public class PreguntaIntentoAgrupadoDTO
    {
        public int? NumeroMaximoIntento { get; set; }
        public bool? ActivarFeedbackMaximoIntento { get; set; }
        public string MensajeFeedback { get; set; }
        public List<PreguntaIntentoDetalleAgrupadoDTO> PreguntaIntentoDetalle { get; set; }
    }
    public class PreguntaIntentoDetalleAgrupadoDTO
    {
        public int? PorcentajeCalificacion { get; set; }
    }
    public class ImportarExcelRespuestaDTO
    {
        public int Total { get; set; }
        public int Correcto { get; set; }
        public int Error { get; set; }
        public List<string> Errores { get; set; }
    }
    public class ImportarRespuestasExcelDTO
    {
        public IFormFile File { get; set; }
        public int IdPregunta { get; set; } 
    }
}
