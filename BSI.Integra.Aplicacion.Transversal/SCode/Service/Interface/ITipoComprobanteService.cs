using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITipoComprobanteService
    {
        #region Metodos Base
        TipoComprobante Add(TipoComprobanteDTO data);
        TipoComprobante Update(TipoComprobanteDTO data);
        bool Delete(int id, string usuario);

        List<TipoComprobante> Add(List<TipoComprobante> listadoEntidad);
        List<TipoComprobante> Update(List<TipoComprobante> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TipoComprobanteDTO> ObtenerListaTipoComprobante();
    }
}
