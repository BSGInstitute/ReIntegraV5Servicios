using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IRemitenteMailingService
    {
        #region Metodos Base
        RemitenteMailing Add(RemitenteMailing entidad);
        RemitenteMailing Update(RemitenteMailing entidad);
        bool Delete(int id, string usuario);

        List<RemitenteMailing> Add(List<RemitenteMailing> listadoEntidad);
        List<RemitenteMailing> Update(List<RemitenteMailing> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<RemitenteMailingDTO> ObtenerTodosRemitenteMailing();
        List<RemitenteMailingAsesorDTO> ObtenerListaRemitenteMailingAsesor(int IdRemitenteMailing);
    }
}
