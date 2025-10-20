

using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFacebookFormularioLeadgenLogRepository : IGenericRepository<TFacebookFormularioLeadgenLog>
    {
        #region Metodos Base
        TFacebookFormularioLeadgenLog Add(FacebookFormularioLeadgenLog entidad);
        TFacebookFormularioLeadgenLog Update(FacebookFormularioLeadgenLog entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFacebookFormularioLeadgenLog> Add(IEnumerable<FacebookFormularioLeadgenLog> listadoEntidad);
        IEnumerable<TFacebookFormularioLeadgenLog> Update(IEnumerable<FacebookFormularioLeadgenLog> listadoEntidad);

        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
