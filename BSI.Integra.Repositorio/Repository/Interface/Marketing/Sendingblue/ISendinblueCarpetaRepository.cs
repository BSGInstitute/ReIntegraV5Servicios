using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.IntegracionConIntegraDB.RelacionFolderConListaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCarpetaDTO;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue
{
    public interface ISendinblueCarpetaRepository
    {
        #region Metodos Base
        TSendinblueCarpetum Add(CrearSendinblueCarpeta entidad);
        TSendinblueCarpetum Update(CrearSendinblueCarpeta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSendinblueCarpetum> Add(IEnumerable<CrearSendinblueCarpeta> listadoEntidad);
        IEnumerable<TSendinblueCarpetum> Update(IEnumerable<CrearSendinblueCarpeta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TSendinblueCarpetum> ObtenerTodaslasCampanias();
        TSendinblueCarpetum ObtenerCampaniaPorId(int id);
        RelacionFolderConLista ObtenerFolderMasListas(int idF);
    }
}
