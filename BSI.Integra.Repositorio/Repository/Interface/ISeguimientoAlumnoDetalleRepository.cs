using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISeguimientoAlumnoDetalleRepository : IGenericRepository<TSeguimientoAlumnoDetalle>
    {

        #region
        TSeguimientoAlumnoDetalle Add(SeguimientoAlumnoDetalle entidad);
        TSeguimientoAlumnoDetalle Update(SeguimientoAlumnoDetalle entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSeguimientoAlumnoDetalle> Add(IEnumerable<SeguimientoAlumnoDetalle> listadoEntidad);
        IEnumerable<TSeguimientoAlumnoDetalle> Update(IEnumerable<SeguimientoAlumnoDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<SeguimientoAlumnoDetalle> ObtenerPorIdSeguimientoAlumnoCategoria(int idSeguimientoAlumnoCategoria,int idEstadoMatricula);
        List<SeguimientoAlumnoDetalle> ObtenerPorIdSeguimientoAlumnoCategoria(int idSeguimientoAlumnoCategoria);

    }
}
