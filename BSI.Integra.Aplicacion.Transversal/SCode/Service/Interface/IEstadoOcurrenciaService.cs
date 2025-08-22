using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IEstadoOcurrenciaService
    {

        #region Metodos Base
        EstadoOcurrencia Add(EstadoOcurrencia entidad);
        EstadoOcurrencia Update(EstadoOcurrencia entidad);
        bool Delete(int id, string usuario);

        List<EstadoOcurrencia> Add(List<EstadoOcurrencia> listadoEntidad);
        List<EstadoOcurrencia> Update(List<EstadoOcurrencia> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<EstadoOcurrenciaDTO> ObtenerEstadoOcurrencia();
    }
}
