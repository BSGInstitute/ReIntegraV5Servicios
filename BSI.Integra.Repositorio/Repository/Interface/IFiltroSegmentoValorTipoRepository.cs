using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFiltroSegmentoValorTipoRepository : IGenericRepository<TFiltroSegmentoValorTipo>
    {
        #region Metodos Base
        TFiltroSegmentoValorTipo Add(FiltroSegmentoValorTipo entidad);
        TFiltroSegmentoValorTipo Update(FiltroSegmentoValorTipo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFiltroSegmentoValorTipo> Add(IEnumerable<FiltroSegmentoValorTipo> listadoEntidad);
        IEnumerable<TFiltroSegmentoValorTipo> Update(IEnumerable<FiltroSegmentoValorTipo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}