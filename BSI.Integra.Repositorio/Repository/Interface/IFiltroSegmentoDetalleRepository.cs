using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFiltroSegmentoDetalleRepository : IGenericRepository<TFiltroSegmentoDetalle>
    {
        #region Metodos Base
        TFiltroSegmentoDetalle Add(FiltroSegmentoDetalle entidad);
        TFiltroSegmentoDetalle Update(FiltroSegmentoDetalle entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFiltroSegmentoDetalle> Add(IEnumerable<FiltroSegmentoDetalle> listadoEntidad);
        IEnumerable<TFiltroSegmentoDetalle> Update(IEnumerable<FiltroSegmentoDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
   

    }
}