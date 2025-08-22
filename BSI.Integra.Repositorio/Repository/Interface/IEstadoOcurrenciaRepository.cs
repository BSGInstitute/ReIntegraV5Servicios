using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEstadoOcurrenciaRepository : IGenericRepository<TEstadoOcurrencium>
    {
        #region Metodos Base
        TEstadoOcurrencium Add(EstadoOcurrencia entidad);
        TEstadoOcurrencium Update(EstadoOcurrencia entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEstadoOcurrencium> Add(IEnumerable<EstadoOcurrencia> listadoEntidad);
        IEnumerable<TEstadoOcurrencium> Update(IEnumerable<EstadoOcurrencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        IEnumerable<EstadoOcurrenciaDTO> ObtenerEstadoOcurrencia();
    }
}
