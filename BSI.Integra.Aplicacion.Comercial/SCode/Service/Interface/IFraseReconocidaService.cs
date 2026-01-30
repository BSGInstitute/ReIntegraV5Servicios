using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface IFraseReconocidaService
    {
        #region Metodos Base
        FraseReconocida Add(FraseReconocida entidad);
        FraseReconocida Update(FraseReconocida entidad);
        bool Delete(int id, string usuario);
        List<FraseReconocida> Add(List<FraseReconocida> listadoEntidad);
        List<FraseReconocida> Update(List<FraseReconocida> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
