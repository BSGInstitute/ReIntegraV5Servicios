using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueAtributoDTO;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue
{
    public interface ISendinblueAtributoRepository
    {
        #region Metodos Base
        TSendinblueAtributo Add(CrearSendinblueAtributo entidad);
        TSendinblueAtributo Update(CrearSendinblueAtributo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSendinblueAtributo> Add(IEnumerable<CrearSendinblueAtributo> listadoEntidad);
        IEnumerable<TSendinblueAtributo> Update(IEnumerable<CrearSendinblueAtributo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TSendinblueAtributo ObtenerCampaniaPorId(int id);
        IEnumerable<TSendinblueAtributo> ObtenerTodaslasCampanias();
    }
}
