using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IIvrEjecucionService
    {
        #region Metodos Base
        IvrEjecucion Add(IvrEjecucionDTO data, string Usuario);
        IvrEjecucion Update(IvrEjecucionDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<IvrEjecucion> Add(List<IvrEjecucion> listadoEntidad);
        List<IvrEjecucion> Update(List<IvrEjecucion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<IvrEjecucionDTO> ObtenerIvrEjecucion();
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
