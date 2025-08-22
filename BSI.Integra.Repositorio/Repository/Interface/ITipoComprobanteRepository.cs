using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoComprobanteRepository : IGenericRepository<TTipoComprobante>
    {
        #region Metodos Base
        TTipoComprobante Add(TipoComprobante entidad);
        TTipoComprobante Update(TipoComprobante entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoComprobante> Add(IEnumerable<TipoComprobante> listadoEntidad);
        IEnumerable<TTipoComprobante> Update(IEnumerable<TipoComprobante> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoComprobanteDTO> ObtenerListaTipoComprobante();
    }
}
