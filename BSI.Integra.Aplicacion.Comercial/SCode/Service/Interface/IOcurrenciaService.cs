using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IOcurrenciaService
    {
        #region Metodos Base
        Ocurrencia Add(Ocurrencia entidad);
        Ocurrencia Update(Ocurrencia entidad);
        bool Delete(int id, string usuario);

        List<Ocurrencia> Add(List<Ocurrencia> listadoEntidad);
        List<Ocurrencia> Update(List<Ocurrencia> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OcurrenciaDTO> ObtenerOcurrencia();
        IEnumerable<OcurrenciaComboDTO> ObtenerCombo();
        List<HojaActividadesDTO> ObtenerHojaActividadesPorIdOcurrenciaAlterno(int idOcurrencia);
        bool ValidarEstadoOcurrencia(int idOcurrencia);
        int ValidarGrupoPreLanzamiento(int idCategoria);
        OcurrenciaDTO ObtenerPorId(int idOcurrencia);
    }
}
