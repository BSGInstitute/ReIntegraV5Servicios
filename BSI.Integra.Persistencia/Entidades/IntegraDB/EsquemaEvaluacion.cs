using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EsquemaEvaluacion : BaseIntegraEntity
    {
        [MaxLength(150)]
        public string Nombre { get; set; }
        public int IdFormaCalculoEvaluacion { get; set; }
        public int IdModalidadCurso { get; set; }

        public List<EsquemaEvaluacionDetalle> EsquemaEvaluacionDetalles { get; set; }
    }
}
