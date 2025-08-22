using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class EsquemaEvaluacion_ItemEvaluableAlumnoDTO
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
    public class CriterioEvaluacionCursoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int? Grupo { get; set; }
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

    public class EscalaCalificacionCriterioDTO
    {

        public int IdCriterioEvaluacion { get; set; }
        public string CriterioEvaluacion { get; set; }
        public int IdEscalaCalificacion { get; set; }
        public string EscalaCalificacion { get; set; }
    }
    public class EsquemaPGneralDetalleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
