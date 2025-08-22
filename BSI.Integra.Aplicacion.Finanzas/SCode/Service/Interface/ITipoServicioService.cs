using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ITipoServicioService
    {
        #region Metodos Base
        TipoServicio Add(TipoServiciosDTO entidad, string Usuario);
        TipoServicio Update(TipoServiciosDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<TipoServicio> Add(List<TipoServicio> listadoEntidad);
        List<TipoServicio> Update(List<TipoServicio> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TipoServicioDTO> ObtenerTipoServicio();
        IEnumerable<TipoServicioComboDTO> ObtenerCombo();
    }
}
