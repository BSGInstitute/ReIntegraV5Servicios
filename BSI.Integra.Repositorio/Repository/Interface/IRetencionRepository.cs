using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRetencionRepository : IGenericRepository<TRetencion>
    {
        #region Metodos Base
        TRetencion Add(Retencion entidad);
        TRetencion Update(Retencion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TRetencion> Add(IEnumerable<Retencion> listadoEntidad);
        IEnumerable<TRetencion> Update(IEnumerable<Retencion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<RetencionDTO> ObtenerRetencion();
        IEnumerable<RetencionComboDTO> ObtenerCombo();
    }
}
