using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface IFraseCombinadaService
    {
        #region Metodos Base
        FraseCombinada Add(FraseCombinada entidad);
        FraseCombinada Update(FraseCombinada entidad);
        bool Delete(int id, string usuario);
        List<FraseCombinada> Add(List<FraseCombinada> listadoEntidad);
        List<FraseCombinada> Update(List<FraseCombinada> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
