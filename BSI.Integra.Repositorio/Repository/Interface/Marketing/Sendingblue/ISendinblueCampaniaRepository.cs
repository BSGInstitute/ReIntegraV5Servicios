using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCamapaniaDTO;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue
{
    public interface ISendinblueCampaniaRepository
    {
        #region Metodos Base
        TSendinblueCampanium Add(CrearSendinblueCamapaniaDTO entidad);
        TSendinblueCampanium Update(CrearSendinblueCamapaniaDTO entidad);
        IEnumerable<TSendinblueCampanium> UpdateLis(List<TSendinblueCampanium> listadoEntidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSendinblueCampanium> Add(IEnumerable<CrearSendinblueCamapaniaDTO> listadoEntidad);
        IEnumerable<TSendinblueCampanium> Update(IEnumerable<CrearSendinblueCamapaniaDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TSendinblueCampanium> ObtenerTodaslasCampanias();
        public TSendinblueCampanium ObtenerCampaniaPorId(int id);
    }
}
