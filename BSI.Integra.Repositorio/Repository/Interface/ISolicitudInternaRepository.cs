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
    public interface ISolicitudInternaRepository : IGenericRepository<TSolicitudInterna>
    {
        #region Metodos Base
        TSolicitudInterna Add(SolicitudInterna entidad);
        TSolicitudInterna Update(SolicitudInterna entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSolicitudInterna> Add(IEnumerable<SolicitudInterna> listadoEntidad);
        IEnumerable<TSolicitudInterna> Update(IEnumerable<SolicitudInterna> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesPorArea(int idPersonal);
        IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesGestion(int idPersonal);
        IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesPorFiltro(FiltroSolicitudesInternasDTO FiltroSolicitud);
        IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesAlumnoPorFiltroReporte(FiltroSolicitudesInternasDTO FiltroSolicitud);
        IEnumerable<SolicitudInternaFiltradaDTO> obtenerSolicitudesInternas();

        SolicitudInterna ObtenerPorId(int id);





    }
}
