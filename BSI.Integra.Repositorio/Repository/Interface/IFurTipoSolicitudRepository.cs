using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFurTipoSolicitudRepository
    {
        #region Metodos Base
         TFurTipoSolicitud Add(TFurTipoSolicitudDTO entidad);
         TFurTipoSolicitud Update(TFurTipoSolicitudDTO entidad);
         bool Delete(int id, string usuario);
         IEnumerable<TFurTipoSolicitud> Add(IEnumerable<TFurTipoSolicitudDTO> listadoEntidad);
         IEnumerable<TFurTipoSolicitud> Update(IEnumerable<TFurTipoSolicitudDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TFurTipoSolicitud ObtenerPorId(int id);
        List<TFurTipoSolicitud> ObtenerTodos();
        List<TFurTipoSolicitud> ObtenerPorTexto(string texto);
    }
}
