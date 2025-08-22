using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IAlumnoCuponRegistroService
    {
        #region Metodos Base
        AlumnoCuponRegistro Add(AlumnoCuponRegistro entidad);
        AlumnoCuponRegistro Update(AlumnoCuponRegistro entidad);
        bool Delete(int id, string usuario);

        List<AlumnoCuponRegistro> Add(List<AlumnoCuponRegistro> listadoEntidad);
        List<AlumnoCuponRegistro> Update(List<AlumnoCuponRegistro> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<AlumnoCuponRegistroDTO> ObtenerAlumnoCuponRegistro();
        IEnumerable<AlumnoCuponRegistroComboDTO> ObtenerCombo();
    }
}
