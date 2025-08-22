using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SolicitudAlumno : BaseIntegraEntity
    {
        public int IdEstadoSolicitud { get; set; }
        public int IdPersonal { get; set; }
        public int IdSolicitud { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPespescifico { get; set; }
        public string? DetalleSolicitud { get; set; }
        public string? ContentTypeSolicitante { get; set; }
        public string? NombreArchivoSolicitante { get; set; }
        public string? ContentTypeSolucion { get; set; }
        public string? NombreArchivoSolucion { get; set; }
        public string? ComentarioSolucion { get; set; }
        public int? IdControlSolicitudOrigen { get; set; }
    }
}
