using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISolicitudRepository : IGenericRepository<TSolicitud>
    {
        #region Metodos Base
        TSolicitud Add(Solicitud entidad);
        TSolicitud Update(Solicitud entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSolicitud> Add(IEnumerable<Solicitud> listadoEntidad);
        IEnumerable<TSolicitud> Update(IEnumerable<Solicitud> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Solicitud ObtenerPorId(int id);
        IEnumerable<ReporteSolicitudDTO> ObtenerSolicitudes();
        IEnumerable<HistorialSolicitudAlumnoDTO> ObtenerHistorialSolicitudAlumno(int IdMatriculaCabecera, int IdPEspecifico);
        IEnumerable<EstadoSolicitudDTO> ObtenerEstadosSolicitud();
        IEnumerable<EstadoSolicitudDTO> ObtenerEstadosSolicitudRevision();
        IEnumerable<EstadoSolicitudDTO> ObtenerEstadosSolicitudGestion();


    }
}
