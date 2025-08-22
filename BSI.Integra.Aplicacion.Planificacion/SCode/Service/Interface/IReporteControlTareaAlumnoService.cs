using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IReporteControlTareaAlumnoService
    {
        IEnumerable<ReporteControlTareaAlumnoDTO> GenerarReporteControlTareaAlumno(ReporteControlTareaAlumnoFiltroDTO filtroReporte);
        string ActualizarPersonaCalificacionControlTareaAlumno(ReporteControlTareaAlumnoActualizacionDTO datos);
        bool ActualizarNota(NotaDTO dto, string usuario);
    }
}
