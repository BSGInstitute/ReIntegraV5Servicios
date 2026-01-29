using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IRemarketingEmbudoNivelService
    {
        #region Metodos Base
        RemarketingEmbudoNivel Add(RemarketingEmbudoNivel entidad);
        RemarketingEmbudoNivel Update(RemarketingEmbudoNivel entidad);
        bool Delete(int id, string usuario);

        List<RemarketingEmbudoNivel> Add(List<RemarketingEmbudoNivel> listadoEntidad);
        List<RemarketingEmbudoNivel> Update(List<RemarketingEmbudoNivel> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
