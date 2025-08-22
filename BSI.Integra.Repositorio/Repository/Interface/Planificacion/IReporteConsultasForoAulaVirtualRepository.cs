using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IReporteConsultasForoAulaVirtualRepository
    {
        bool ActualizarEstadoAtencionForo(int idForo, bool estadoAtendido, string usuarioModificacion);
        bool EliminarForo(int idForo, string usuarioModificacion);
        bool ActualizarAperturaForo(int idForo, bool estadoForo, string usuarioModificacion);
        List<ReporteConsultasForoAulaVirtualDTO> GenerarReporteConsultasForoAulaVirtual(ReporteConsultasForoFiltroDTO filtroReporte);
        public bool ActualizarEncargadoForo(int Id, int IdProveedor);
        public List<ReporteConsultasForoDetalleAulaVirtualDTO> ObtenerDetalleForo(int IdForoCurso);
        public bool EliminarForoRespuesta(int IdForoRespuesta, string UsuarioModificacion);
    }
}
