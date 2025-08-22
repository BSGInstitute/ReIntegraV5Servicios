using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    public class EsquemaEvaluacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdFormaCalculoEvaluacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public List<EsquemaEvaluacionDetalleDTO> EsquemaEvaluacionDetalles { get; set; }
    }
    public class EsquemaEvaluacionCongeladoListadoDTO
    {
        public int Id { get; set; }
        public int? IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno { get; set; }
        public int IdEsquemaEvaluacionPGeneral { get; set; }
        public string NombreEsquema { get; set; }
        public int IdFormaCalculoEvaluacion { get; set; }
        public string FormaCalculoEvaluacion { get; set; }
        public List<EsquemaEvaluacionDetalleCongeladoDTO> EsquemasEvaluacionDetalle { get; set; }
    }
    public class EsquemaEvaluacionDetalleCongeladoDTO
    {
        public int Id { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public string NombreEsquemaDetalle { get; set; }
        public int Ponderacion { get; set; }
    }
    public class EsquemaEvaluacionNotaCursoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int Grupo { get; set; }
        public List<EsquemaEvaluacionDetalleCalificacionDTO> DetalleCalificacion { get; set; }
        public decimal? NotaCurso { get; set; }
    }
    public class EsquemaEvaluacionDetalleCalificacionDTO
    {
        public int IdCriterioEvaluacion { get; set; }
        public string CriterioEvaluacion { get; set; }
        public decimal Valor { get; set; }
        public int Ponderacion { get; set; }
    }
    public class EsquemaEvaluacionItemEvaluableAlumnoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int Grupo { get; set; }

        public int IdCriterioEvaluacion { get; set; }
        public string CriterioEvaluacion { get; set; }

        public int IdEsquemaEvaluacionPGeneralDetalle { get; set; }
        public int IdEsquemaEvaluacion { get; set; }

        public int IdParametroEvaluacion { get; set; }
        public int? IdEscalaCalificacionDetalle { get; set; }

        public decimal? ValorEscala { get; set; }

        public int? IdFormaCalificacionCriterio { get; set; }
        public int? IdFormaCalculoEvaluacion_Parametro { get; set; }
        public int? IdFormaCalculoEvaluacion_Criterio { get; set; }
        public int Ponderacion_Parametro { get; set; }
        public int IdFormaCalculoEvaluacion_Esquema { get; set; }
        public int Ponderacion_Criterio { get; set; }
    }
    public class EsquemaEvaluacionComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class EsquemaCriterioEvaluacionDTO
    {
        public int IdEsquemaEvaluacion { get; set; }
        public string EsquemaEvaluacion { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public string CriterioEvaluacion { get; set; }
        public int Ponderacion { get; set; }

    }
    public class EsquemaeEvaluacionProgramaEspecificoCreacionDTO
    {
        public PEspecificoEsquemaDTO asociacionEsquema { get; set; }
        public List<CriterioEvaluacionEsquemaDTO> criterios { get; set; }
    }
    public class PEspecificoEsquemaDTO
    {
        public int Id { get; set; }
        public int IdProgramaEspecifico { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
    }
    public class CriterioEvaluacionEsquemaDTO
    {
        public int IdCriterioEvaluacion { get; set; }
        public int Ponderacion { get; set; }
    }
    public class EsquemaEvaluacionPGeneralRespuestaDTO
    {
        public int Id { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public int IdPgeneral { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? EsquemaPredeterminado { get; set; }
        public string Esquema { get; set; }
        public List<int> ListadoModalidad { get; set; }
        public string ModalidadMostrar { get; set; }
        public List<int> ListadoProveedor { get; set; }
    }
    public class EsquemaEvaluacionRegistrarAsignacionDTO
    {
        public int Id { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public List<int> IdModalidad { get; set; }
        public List<int> IdProveedor { get; set; }
        public int IdPGeneral { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool EsquemaPredeterminado { get; set; }
        public List<EsquemaEvaluacion_RegistrarDetalleAsignacionDTO> ListadoDetalleAsignacion { get; set; }
    }
    public class EsquemaEvaluacion_RegistrarDetalleAsignacionDTO
    {
        public int Id { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int? IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string? UrlArchivoInstrucciones { get; set; }
    }
}
