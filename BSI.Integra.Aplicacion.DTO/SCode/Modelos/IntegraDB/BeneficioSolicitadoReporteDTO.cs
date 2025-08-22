using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class BeneficioSolicitadoReporteDTO
    {
        public int IdMatriculaCabeceraBeneficios { get; set; }
        public string Alumno { get; set; }
        public string CodigoMatricula { get; set; }
        public string Estado_Matricula { get; set; }
        public string SubEstado_Matricula { get; set; }
        public string VersionProgram { get; set; }
        public string Beneficio { get; set; }
        public string Programa { get; set; }
        public string CentroCosto { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Coordinador { get; set; }
        public int IdEstadoSolicitudBeneficio { get; set; }
        public string EstadoSolicitud { get; set; }
        public string UsuarioAprobacion { get; set; }
        public string UsuarioEntregoBeneficio { get; set; }
        public DateTime FechaMatricula { get; set; }
        public DateTime FechaCongelamiento { get; set; }
    }
}
