using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface IDetalleFraseReconocidaService
    {
        #region Metodos Base
        DetalleFraseReconocida Add(DetalleFraseReconocida entidad);
        DetalleFraseReconocida Update(DetalleFraseReconocida entidad);
        bool Delete(int id, string usuario);
        List<DetalleFraseReconocida> Add(List<DetalleFraseReconocida> listadoEntidad);
        List<DetalleFraseReconocida> Update(List<DetalleFraseReconocida> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
