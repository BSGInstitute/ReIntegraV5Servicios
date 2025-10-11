using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IFacebookFormularioLeadgenLogService
    {
        #region Metodos Base
        FacebookFormularioLeadgenLog Add(FacebookFormularioLeadgenLog entidad);
        FacebookFormularioLeadgenLog Update(FacebookFormularioLeadgenLog entidad);
        bool Delete(int id, string usuario);

        List<FacebookFormularioLeadgenLog> Add(List<FacebookFormularioLeadgenLog> listadoEntidad);
        List<FacebookFormularioLeadgenLog> Update(List<FacebookFormularioLeadgenLog> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public void EvaluarConversionFacebook(int idOportunidad);
    }
}
