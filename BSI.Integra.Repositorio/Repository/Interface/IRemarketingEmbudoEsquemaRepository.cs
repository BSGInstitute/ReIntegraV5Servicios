using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRemarketingEmbudoEsquemaRepository : IGenericRepository<TRemarketingEmbudoEsquema>
    {
        #region Metodos Base
        TRemarketingEmbudoEsquema Add(RemarketingEmbudoEsquema entidad);
        TRemarketingEmbudoEsquema Update(RemarketingEmbudoEsquema entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TRemarketingEmbudoEsquema> Add(IEnumerable<RemarketingEmbudoEsquema> listadoEntidad);
        IEnumerable<TRemarketingEmbudoEsquema> Update(IEnumerable<RemarketingEmbudoEsquema> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
