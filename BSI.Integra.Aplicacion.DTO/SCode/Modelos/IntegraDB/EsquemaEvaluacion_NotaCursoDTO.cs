using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class EsquemaEvaluacion_NotaCursoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int Grupo { get; set; }
        public List<EsquemaEvaluacion_DetalleCalificacionDTO> DetalleCalificacion { get; set; }
        public decimal? NotaCurso { get; set; }
    }
    public class EsquemaEvaluacion_NotaCursosDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int Grupo { get; set; }
        public List<EsquemaEvaluacion_DetalleCalificacionDTO> DetalleCalificacion { get; set; }
        public decimal? NotaCurso { get; set; }
        public ExcepcionRegistroDTO Excepcion { get; set; }

    }
}
