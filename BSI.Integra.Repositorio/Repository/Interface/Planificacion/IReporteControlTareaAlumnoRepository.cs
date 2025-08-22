using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IReporteControlTareaAlumnoRepository
    {
        IEnumerable<ReporteControlTareaAlumnoDTO> GenerarReporteControlTareaAlumno(ReporteControlTareaAlumnoFiltroDTO filtroReporte);
        int ActualizarPersonaCalificacionControlTareaAlumno(int id, int idProveedor);
        bool ActualizarNota(NotaDTO dto, string usuario);
    }
}
