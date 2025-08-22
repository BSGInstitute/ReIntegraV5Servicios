using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPEspecificoConsumoRepository : IGenericRepository<TPespecificoConsumo>
    {
        #region Metodos Base
        TPespecificoConsumo Add(PEspecificoConsumo entidad);
        TPespecificoConsumo Update(PEspecificoConsumo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPespecificoConsumo> Add(IEnumerable<PEspecificoConsumo> listadoEntidad);
        IEnumerable<TPespecificoConsumo> Update(IEnumerable<PEspecificoConsumo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
