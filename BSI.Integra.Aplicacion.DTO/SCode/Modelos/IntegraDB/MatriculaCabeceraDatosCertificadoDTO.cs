using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MatriculaCabeceraDatosCertificadoDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdCertificadoGeneradoAutomatico { get; set; }
        public string Duracion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string NombreCurso { get; set; }
        public bool EstadoCambioDatos { get; set; }
        public string Usuario { get; set; }
        public string Mensaje { get; set; }

    }
}
