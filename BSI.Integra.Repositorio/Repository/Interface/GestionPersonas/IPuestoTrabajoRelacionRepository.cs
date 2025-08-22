using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPuestoTrabajoRelacionRepository : IGenericRepository<TPuestoTrabajoRelacion>
    {
        #region Metodos Base
        TPuestoTrabajoRelacion Add(PuestoTrabajoRelacion entidad);

        TPuestoTrabajoRelacion Update(PuestoTrabajoRelacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPuestoTrabajoRelacion> Add(IEnumerable<PuestoTrabajoRelacion> listadoEntidad);
        IEnumerable<TPuestoTrabajoRelacion> Update(IEnumerable<PuestoTrabajoRelacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
