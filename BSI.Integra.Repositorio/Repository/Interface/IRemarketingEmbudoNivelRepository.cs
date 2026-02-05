using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRemarketingEmbudoNivelRepository : IGenericRepository<TRemarketingEmbudoNivel>
    {
        #region Metodos Base
        TRemarketingEmbudoNivel Add(RemarketingEmbudoNivel entidad);
        TRemarketingEmbudoNivel Update(RemarketingEmbudoNivel entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TRemarketingEmbudoNivel> Add(IEnumerable<RemarketingEmbudoNivel> listadoEntidad);
        IEnumerable<TRemarketingEmbudoNivel> Update(IEnumerable<RemarketingEmbudoNivel> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}

