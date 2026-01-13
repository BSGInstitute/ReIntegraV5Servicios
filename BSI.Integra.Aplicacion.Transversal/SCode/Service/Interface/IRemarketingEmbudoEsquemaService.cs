using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IRemarketingEmbudoEsquemaService
    {
        #region Metodos Base
        RemarketingEmbudoEsquema Add(RemarketingEmbudoEsquema entidad);
        RemarketingEmbudoEsquema Update(RemarketingEmbudoEsquema entidad);
        bool Delete(int id, string usuario);

        List<RemarketingEmbudoEsquema> Add(List<RemarketingEmbudoEsquema> listadoEntidad);
        List<RemarketingEmbudoEsquema> Update(List<RemarketingEmbudoEsquema> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
