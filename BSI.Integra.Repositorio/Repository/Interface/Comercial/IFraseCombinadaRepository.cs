using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface IFraseCombinadaRepository  :IGenericRepository<TFraseCombinadum>
    {
        #region Metodos Base
        TFraseCombinadum Add(FraseCombinada entidad);
        TFraseCombinadum Update(FraseCombinada entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFraseCombinadum> Add(IEnumerable<FraseCombinada> listadoEntidad);
        IEnumerable<TFraseCombinadum> Update(IEnumerable<FraseCombinada> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }   
  
}
