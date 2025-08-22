using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IIvrTipoConfiguracionService
    {
        #region Metodos Base
        IvrTipoConfiguracion Add(IvrTipoConfiguracionDTO data, string Usuario);
        IvrTipoConfiguracion Update(IvrTipoConfiguracionDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<IvrTipoConfiguracion> Add(List<IvrTipoConfiguracion> listadoEntidad);
        List<IvrTipoConfiguracion> Update(List<IvrTipoConfiguracion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<IvrTipoConfiguracionDTO> ObtenerIvrTipoConfiguracion();
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
