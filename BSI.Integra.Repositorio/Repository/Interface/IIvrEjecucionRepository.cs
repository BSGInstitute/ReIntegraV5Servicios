using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IIvrEjecucionRepository : IGenericRepository<TIvrEjecucion>
    {
        #region Metodos Base
        TIvrEjecucion Add(IvrEjecucion entidad);
        TIvrEjecucion Update(IvrEjecucion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TIvrEjecucion> Add(IEnumerable<IvrEjecucion> listadoEntidad);
        IEnumerable<TIvrEjecucion> Update(IEnumerable<IvrEjecucion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<IvrEjecucionDTO> ObtenerIvrEjecucion();
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
