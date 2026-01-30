using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISolicitudAlumnoRepository : IGenericRepository<TSolicitudAlumno>
    {
        #region Metodos Base
        TSolicitudAlumno Add(SolicitudAlumno entidad);
        TSolicitudAlumno Update(SolicitudAlumno entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSolicitudAlumno> Add(IEnumerable<SolicitudAlumno> listadoEntidad);
        IEnumerable<TSolicitudAlumno> Update(IEnumerable<SolicitudAlumno> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorArea(int idPersonal);
        IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorPersonal(int idPersonal);
        IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltro(
            FiltroSolicitudesDTO FiltroSolicitud
        );
        IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltroAlumno(
            FiltroSolicitudAlumnoDTO FiltroSolicitud
        );
        IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltroAlumnoRevision(
            FiltroSolicitudAlumnoDTO FiltroSolicitud
        );
        IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltroAlumnoGestion(
            FiltroSolicitudAlumnoDTO FiltroSolicitud
        );
        IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesAlumnoPorFiltroReporte(
            FiltroSolicitudesDTO FiltroSolicitud
        );
        IEnumerable<SolicitudAlumnoFiltradaDTO> obtenerSolicitudesAlumno();
        IEnumerable<SolicitudLogDTO> obtenerLogSolicitudes(int idSolicitud);
        IEnumerable<SolicitudPersonalAlumnoDTO> ObtenerPersonalSolicitanteAlumno();
        IEnumerable<SolicitudPersonalSolucionAlumnoDTO> ObtenerPersonalSolucionSolicitudAlumno(
            List<int> IdPersonal
        );
        SolicitudAlumno ObtenerPorId(int id);
        IEnumerable<ReporteSolicitudAlumnoDTO> ObtenerReporteSolicitudesPorFiltroAlumno(
            FiltroReporteSolicitudAlumnoDTO FiltroReporteSolicitud
        );
        List<TipoSolicitudEstructuraDTO> ObtenerEstructuraSolicitudesPlana();
        RespuestaVerificacionSolicitudDTO VerificarSolicitudActivaAlumno(
            VerificarSolicitudAlumnoDTO filtro
        );
        (
            int? IdMatriculaCabecera,
            int? IdPersonal,
            string ErrorDescripcion,
            string ErrorException
        ) ObtenerDatosParaSolicitudAlumno(int idAlumno, int idPEspecifico);
        IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltroAsesor(
            FiltroSolicitudAlumnoPorAsesorDTO FiltroSolicitud
        );
        RespuestaSolicitudesAlumnoDTO ObtenerSolicitudesAgrupadasPorAsesor(
            FiltroSolicitudAlumnoPorAsesorDTO FiltroSolicitud
        );
        bool ActualizarEstadoSolicitud(int idSolicitud);
    }
}
