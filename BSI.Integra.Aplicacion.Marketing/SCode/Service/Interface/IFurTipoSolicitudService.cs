using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IFurTipoSolicitudService
    {
        #region Metodos base
        TFurTipoSolicitud Add(FurTipoSolicitudDTO entidad, string usuario);
        TFurTipoSolicitud Update(TFurTipoSolicitudDTOV2 entidad, string usuario);
        bool Delete(int id, string usuario);
        IEnumerable<TFurTipoSolicitud> Add(IEnumerable<FurTipoSolicitudDTO> listadoEntidad, string usuario);
        IEnumerable<TFurTipoSolicitud> Update(IEnumerable<TFurTipoSolicitudDTO> listadoEntidad, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TFurTipoSolicitud ObtenerPorId(int id);
        List<TFurTipoSolicitud> ObtenerTodos();
        List<TFurTipoSolicitud> ObtenerPorTexto(string texto);
    }
}
