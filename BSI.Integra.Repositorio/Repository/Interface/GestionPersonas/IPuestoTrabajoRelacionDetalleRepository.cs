using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public  interface IPuestoTrabajoRelacionDetalleRepository : IGenericRepository<TPuestoTrabajoRelacionDetalle>
    {
        #region Metodos Base
        TPuestoTrabajoRelacionDetalle Add(PuestoTrabajoRelacionDetalle entidad);

        TPuestoTrabajoRelacionDetalle Update(PuestoTrabajoRelacionDetalle entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPuestoTrabajoRelacionDetalle> Add(IEnumerable<PuestoTrabajoRelacionDetalle> listadoEntidad);
        IEnumerable<TPuestoTrabajoRelacionDetalle> Update(IEnumerable<PuestoTrabajoRelacionDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
