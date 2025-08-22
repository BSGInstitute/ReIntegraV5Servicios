using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoInteraccionRepository : IGenericRepository<TTipoInteracccion>
    {
        #region Metodos Base
        TTipoInteracccion Add(TipoInteraccion entidad);
        TTipoInteracccion Update(TipoInteraccion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoInteracccion> Add(IEnumerable<TipoInteraccion> listadoEntidad);
        IEnumerable<TTipoInteracccion> Update(IEnumerable<TipoInteraccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<TipoInteraccionDTO> ObtenerTipoInteraccion();
        IEnumerable<TipoInteraccionCanalDTO> ObtenerTipoInteraccionCanalCombo();
        List<FiltroDTO> ObtenerPorTipoInteraccionGeneralFormulario();
    }
}
