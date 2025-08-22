using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IIvrTipoConfiguracionRepository : IGenericRepository<TIvrTipoConfiguracion>
    {
        #region Metodos Base
        TIvrTipoConfiguracion Add(IvrTipoConfiguracion entidad);
        TIvrTipoConfiguracion Update(IvrTipoConfiguracion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TIvrTipoConfiguracion> Add(IEnumerable<IvrTipoConfiguracion> listadoEntidad);
        IEnumerable<TIvrTipoConfiguracion> Update(IEnumerable<IvrTipoConfiguracion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<IvrTipoConfiguracionDTO> ObtenerIvrTipoConfiguracion();
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
