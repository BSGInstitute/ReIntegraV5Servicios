using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IFiltroSegmentoDetalleService
    {
        #region Metodos Base
        FiltroSegmentoDetalle Add(FiltroSegmentoDetalle entidad);
        FiltroSegmentoDetalle Update(FiltroSegmentoDetalle entidad);
        bool Delete(int id, string usuario);

        List<FiltroSegmentoDetalle> Add(List<FiltroSegmentoDetalle> listadoEntidad);
        List<FiltroSegmentoDetalle> Update(List<FiltroSegmentoDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        
    }
}
