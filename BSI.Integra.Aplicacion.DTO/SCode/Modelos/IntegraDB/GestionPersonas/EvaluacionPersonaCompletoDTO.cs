using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class EvaluacionPersonaCompletoDTO
    {
        public int IdExamen { get; set; }
        public string NombreEvaluacion { get; set; }
        public string TituloEvaluacion { get; set; }
        public string Instrucciones { get; set; }
        public bool CronometrarExamen { get; set; }
        public int TiempoLimiteExamen { get; set; }
        public int? IdExamenTest { get; set; }
        public string ExamenTest { get; set; }
        public int? IdCriterioEvaluacionProceso { get; set; }
        public int? IdExamenConfiguracionFormato { get; set; }
        public string ColorTextoTitulo { get; set; }
        public int? TamanioTextoTitulo { get; set; }
        public string ColorFondoTitulo { get; set; }
        public string TipoLetraTitulo { get; set; }
        public string ColorTextoEnunciado { get; set; }
        public int? TamanioTextoEnunciado { get; set; }
        public string ColorFondoEnunciado { get; set; }
        public string TipoLetraEnunciado { get; set; }
        public string ColorTextoRespuesta { get; set; }
        public int? TamanioTextoRespuesta { get; set; }
        public string ColorFondoRespuesta { get; set; }
        public string TipoLetraRespuesta { get; set; }
        public int? IdExamenComportamiento { get; set; }
        public bool? ResponderTodasLasPreguntas { get; set; }
        public int? IdEvaluacionFeedBackAprobado { get; set; }
        public int? IdEvaluacionFeedBackDesaprobado { get; set; }
        public int? IdEvaluacionFeedBackCancelado { get; set; }
        public int? IdExamenConfigurarResultado { get; set; }
        public bool? CalificarExamen { get; set; }
        public decimal? PuntajeExamen { get; set; }
        public decimal? PuntajeAprobacion { get; set; }
        public bool? MostrarResultado { get; set; }
        public bool? MostrarPuntajeTotal { get; set; }
        public bool? MostrarPuntajePregunta { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool? RequiereCentil { get; set; }
        public int? IdFormulaPuntaje { get; set; }
        public List<CentilDTO> ListaCentiles { get; set; }
        public decimal? Factor { get; set; }

    }

    public class PreguntaAsociadaExamenDTO
    {
        public int Id { get; set; }
        public int IdExamen { get; set; }
        public int IdPregunta { get; set; }
        public int NroOrden { get; set; }
        public int Puntaje { get; set; }
        public string EnunciadoPregunta { get; set; }
    }
    public class PreguntaNoAsociadaExamenDTO
    {
        public int Id { get; set; }
        public string EnunciadoPregunta { get; set; }
    }

    public class ConsolidadoPreguntaAsociado
    {
        public int IdExamen { get; set; }
        public List<PreguntaAsociadaExamenDTO> ListaPreguntasAsignadas { get; set; }
        public string Usuario { get; set; }
    }


    public class EliminacionExamenCompletoDTO
    {
        public int IdExamen { get; set; }
        public string NombreEvaluacion { get; set; }
        public string TituloEvaluacion { get; set; }
        public string Instrucciones { get; set; }
        public bool CronometrarExamen { get; set; }
        public int TiempoLimiteExamen { get; set; }
        public int? IdExamenTest { get; set; }
        public string ExamenTest { get; set; }
        public int? IdCriterioEvaluacionProceso { get; set; }
        public int? IdExamenConfiguracionFormato { get; set; }
        public string ColorTextoTitulo { get; set; }
        public int? TamanioTextoTitulo { get; set; }
        public string ColorFondoTitulo { get; set; }
        public string TipoLetraTitulo { get; set; }
        public string ColorTextoEnunciado { get; set; }
        public int? TamanioTextoEnunciado { get; set; }
        public string ColorFondoEnunciado { get; set; }
        public string TipoLetraEnunciado { get; set; }
        public string ColorTextoRespuesta { get; set; }
        public int? TamanioTextoRespuesta { get; set; }
        public string ColorFondoRespuesta { get; set; }
        public string TipoLetraRespuesta { get; set; }
        public int? IdExamenComportamiento { get; set; }
        public bool? ResponderTodasLasPreguntas { get; set; }
        public int? IdEvaluacionFeedBackAprobado { get; set; }
        public int? IdEvaluacionFeedBackDesaprobado { get; set; }
        public int? IdEvaluacionFeedBackCancelado { get; set; }
        public int? IdExamenConfigurarResultado { get; set; }
        public bool? CalificarExamen { get; set; }
        public decimal? PuntajeExamen { get; set; }
        public decimal? PuntajeAprobacion { get; set; }
        public bool? MostrarResultado { get; set; }
        public bool? MostrarPuntajeTotal { get; set; }
        public bool? MostrarPuntajePregunta { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool? RequiereCentil { get; set; }
        public int? IdFormulaPuntaje { get; set; }
        public List<CentilDTO> ListaCentiles { get; set; }
        public int Id { get; set; }

    }
    public class PuntajeCalificacionComponenteDTO
    {
        public decimal? PuntajeTipoRespuesta { get; set; }
        public decimal? SumaPuntaje { get; set; }
        public int? CantidadPreguntas { get; set; }
    }
}
