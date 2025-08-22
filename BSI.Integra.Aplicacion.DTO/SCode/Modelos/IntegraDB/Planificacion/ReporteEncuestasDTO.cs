using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Protobuf.WellKnownTypes.Field.Types;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ReporteEncuestasDTO
    {
        public int IdAlumno { get; set; }
        public string Alumno { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdPEspecificoExamen { get; set; }
        public string PEspecificoExamen { get; set; }
        public int IdPGeneralExamen { get; set; }
        public string PGeneralExamen { get; set; }
        public string CentroCostoExamen { get; set; }
        public int IdCentroCostoMatricula { get; set; }
        public string CentroCostoMatricula { get; set; }
        public int? IdPersonal { get; set; }
        public string? NombrePersonal { get; set; }
        public string Encuesta { get; set; }
        public int NroOrden { get; set; }
        public int IdTipoPregunta { get; set; }
        public int? IdPregunta { get; set; }
        public int? IdTipoRespuesta { get; set; }
        public string? NombreTipoRespuesta { get; set; }
        public string? EnunciadoPregunta { get; set; }
        public string? TextoRespuesta { get; set; }
        public int? IdRespuesta { get; set; }
        public int? OrdenRespuesta { get; set; }
        public string? EnunciadoRespuesta { get; set; }
        public string? ValorRespuesta { get; set; }
        public int IdPGeneralMatricula { get; set; }
        public string PGeneralMatricula { get; set; }
        public string FechaCreacion { get; set; }
        public string NombreDocente { get; set; }
    }
    public class ReporteEncuestasFiltroDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<int>? IdsProgramasGenerales { get; set; }
        public List<int>? IdsProgramasEspecificos { get; set; }
        public List<int>? IdsDocentes { get; set; }
        public int Version { get; set; }
        
    }

    public class ObtenerPreguntasExamenDTO
    {
        public int NroOrden { get; set; }
        public int IdPregunta { get; set; }
        public int IdRespuesta { get; set; }
        public int IdExamen { get; set; }
        public int IdTipoPregunta { get; set; }
        public string? TipoPregunta { get; set; }
        public string? EnunciadoPregunta { get; set; }
        public string? NombreTipoRespuesta { get; set; }
        public string? EnunciadoRespuesta { get; set; }
        public int IdTipoRespuesta { get; set; }
        public int NroOrdenRespuesta { get; set; }
        public bool Estado { get; set; }
    }
    public class AgrupadoEncuestasDTO
    {
        public int IdAlumno { get; set; }
        public string Alumno { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdPEspecificoExamen { get; set; }
        public string PEspecificoExamen { get; set; }
        public int IdPGeneralExamen { get; set; }
        public string PGeneralExamen { get; set; }
        public string CentroCostoExamen { get; set; }
        public int IdCentroCostoMatricula { get; set; }
        public string CentroCostoMatricula { get; set; }
        public int? IdCoordinador { get; set; }
        public string? CoordinadorAcademico { get; set; }
        public int IdPGeneralMatricula { get; set; }
        public string PGeneralMatricula { get; set; }
        public string Encuesta { get; set; }
        public string FechaCreacion { get; set; }
        public List<AgrupadoPreguntasEncuestasDTO> Preguntas { get; set; }
        public string NombreDocente { get; set; }
    }
    public class AgrupadoPreguntasEncuestasDTO
    {
        public int IdPregunta { get; set; }
        public string? EnunciadoPregunta { get; set; }
        public int? NroOrden { get; set; }
        public bool Validado { get; set; }
        public int IdTipoPregunta { get; set; }
        public int? IdTipoRespuesta { get; set; }
        public string? NombreTipoRespuesta { get; set; }
        public List<AgrupadoRespuestasDTO> Respuestas { get; set; }
    }
    public class AgrupadoRespuestasDTO
    {
        public int IdRespuesta { get; set; }
        public string? EnunciadoRespuesta { get; set; }
        public int? OrdenRespuesta { get; set; }
        public string? TextoRespuesta { get; set; }
        public bool Validado { get; set; }
    }
    public class AgrupadoPreguntasDTO
    {
        public int IdExamen { get; set; }
        public int IdPregunta { get; set; }
        public string EnunciadoPregunta { get; set; }
        public int NroOrden { get; set; }
        public string NombreTipoRespuesta { get; set; }
        public int IdTipoRespuesta { get; set; }
        public int IdTipoPregunta { get; set; }
        public string TipoPregunta { get; set; }
        public bool Estado { get; set; }
        public List<RespuestasAgrupadoPreguntasDTO> Respuestas { get; set; }
    }
    public class RespuestasAgrupadoPreguntasDTO
    {
        public int IdRespuesta { get; set; }
        public string EnunciadoRespuesta { get; set; }
        public int NroOrdenRespuesta { get; set; }
    }

    public class VersionEncuestaDTO
    {
        public int Version { get; set; }
    }

}
