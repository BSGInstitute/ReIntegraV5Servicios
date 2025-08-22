using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISolicitudOperacionesAccesoTemporalDetalleRepository : IGenericRepository<TSolicitudOperacionesAccesoTemporalDetalle>
    {
        #region Metodos Base
        TSolicitudOperacionesAccesoTemporalDetalle Add(SolicitudOperacionesAccesoTemporalDetalle entidad);
        TSolicitudOperacionesAccesoTemporalDetalle Update(SolicitudOperacionesAccesoTemporalDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSolicitudOperacionesAccesoTemporalDetalle> Add(IEnumerable<SolicitudOperacionesAccesoTemporalDetalle> listadoEntidad);
        IEnumerable<TSolicitudOperacionesAccesoTemporalDetalle> Update(IEnumerable<SolicitudOperacionesAccesoTemporalDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
