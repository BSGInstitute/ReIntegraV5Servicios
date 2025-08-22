using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IAreaFormacionService
    {
        #region Metodos Base
        AreaFormacion Add(AreaFormacion dto , string usuario);
        AreaFormacion Update(AreaFormacion dto, string usuario);
        bool Delete(int id, string usuario);

        List<AreaFormacion> Add(List<AreaFormacion> listadoEntidad);
        List<AreaFormacion> Update(List<AreaFormacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<AreaFormacionDTO> ObtenerAreaFormacion();
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
