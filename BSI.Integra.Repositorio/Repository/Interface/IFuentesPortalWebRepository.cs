using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFuentesPortalWebRepository : IGenericRepository<TFuentesPortalWeb>
    {
        #region Metodos Base
        TFuentesPortalWeb Add(FuentesPortalWeb entidad);
        TFuentesPortalWeb Update(FuentesPortalWeb entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFuentesPortalWeb> Add(IEnumerable<FuentesPortalWeb> listadoEntidad);
        IEnumerable<TFuentesPortalWeb> Update(IEnumerable<FuentesPortalWeb> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<comboFuentes> ObtenerCombo();
        IEnumerable<FuentesPortalWeb> ObtenerFuentesPortalWeb();

    }
}
