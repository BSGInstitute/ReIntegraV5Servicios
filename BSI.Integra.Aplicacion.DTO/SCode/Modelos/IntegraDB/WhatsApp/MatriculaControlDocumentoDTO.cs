using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class MatriculaControlDocumentoDTO
    {
        public string EstadoMatricula { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public string Mes { get; set; }
        public decimal? PagoAcumuladoCronogramaFinal { get; set; }
        public int? IdCriterioCalificacion { get; set; }
        public string QuienEntrego { get; set; }
        public DateTime? FechaEntregaDocumento { get; set; }
        public string Observaciones { get; set; }
        public string NombreCoordinador { get; set; }
        public string NombreAsesor { get; set; }
        public DateTime? FechaPrimerPagoCronogramaFinal { get; set; }
        public int IdControlDocAlumno { get; set; }


    }
}
