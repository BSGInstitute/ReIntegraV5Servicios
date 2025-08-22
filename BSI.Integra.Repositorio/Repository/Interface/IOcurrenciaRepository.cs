using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOcurrenciaRepository : IGenericRepository<TOcurrencium>
    {
        #region Metodos Base
        TOcurrencium Add(Ocurrencia entidad);
        TOcurrencium Update(Ocurrencia entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOcurrencium> Add(IEnumerable<Ocurrencia> listadoEntidad);
        IEnumerable<TOcurrencium> Update(IEnumerable<Ocurrencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OcurrenciaDTO> ObtenerOcurrencia();
        IEnumerable<OcurrenciaComboDTO> ObtenerCombo();
        List<HojaActividadesDTO> ObtenerHojaActividadesPorIdOcurrenciaAlterno(int idOcurrencia);
        bool ValidarEstadoOcurrencia(int idOcurrencia);
        Task<bool> ValidarEstadoOcurrenciaAsync(int idOcurrencia);
        int ValidarGrupoPreLanzamiento(int idCategoria);
        Task<int> ValidarGrupoPreLanzamientoAsync(int idCategoria);
        OcurrenciaDTO ObtenerPorId(int idOcurrencia);
        Ocurrencia ObtenerOcurrenciaPorActividad(int idOcurrencia);
        int ObtenerOcurrenciaPorNombre(string nombre);
    }
}