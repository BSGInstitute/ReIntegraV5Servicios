using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface ISolicitudAlumnoService
    {
        #region Metodos Base
        SolicitudAlumno Add(SolicitudAlumno entidad);
        SolicitudAlumno Update(SolicitudAlumno entidad);
        bool Delete(int id, string usuario);
        List<SolicitudAlumno> Add(List<SolicitudAlumno> listadoEntidad);
        List<SolicitudAlumno> Update(List<SolicitudAlumno> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        SolicitudAlumno ObtenerPorId(int id);
        IEnumerable<SolicitudPersonalAlumnoDTO> ObtenerPersonalSolicitanteAlumno();
        IEnumerable<SolicitudPersonalSolucionAlumnoDTO> ObtenerPersonalSolucionSolicitudAlumno(
            List<int> IdPersonal
        );
        IEnumerable<ReporteSolicitudAlumnoDTO> ObtenerReporteSolicitudesPorFiltroAlumno(
            FiltroReporteSolicitudAlumnoDTO FiltroReporteSolicitud
        );
        TiposSolicitudAlumnosCompletoDTO ObtenerTiposSolicitudCompleto();
        RespuestaVerificacionSolicitudDTO VerificarSolicitudActivaAlumno(
            VerificarSolicitudAlumnoDTO filtro
        );
        RespuestaRegistroSolicitudDTO RegistrarSolicitudAlumno(
            RegistrarSolicitudAlumnoDTO solicitud
        );
    }
}
