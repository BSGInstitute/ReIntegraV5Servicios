    using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IReporteConsultasForoAulaVirtualService
    {
        public bool ActualizarEstadoAtencionForo(int idForo, bool estadoAtendido, string usuarioModificacion);
        bool EliminarForo(int idForo, string usuarioModificacion);
        bool ActualizarAperturaForo(int idForo, bool estadoForo, string usuarioModificacion);
        public ComboReporteForoDTO ObtenerCombosModulo();
        public List<ReporteConsultasForoAulaVirtualDTO> GenerarReporteConsultasForoAulaVirtual(ReporteConsultasForoFiltroDTO filtroReporte);
        public PersonalAsignadoDocenteDTO ActualizarPersonaRevisionForo(int idForo, int idProveedor);
        public List<ReporteConsultasForoDetalleAulaVirtualDTO> ObtenerDetalleForo(int idForoCurso);
        public bool EliminarForoRespuesta(int idForoRespuesta, string usuarioModificacion);
        public bool EnvioCorreoAsignacionForoDocente(ForosCorreoDTO datos, int idPer);
    }
}
