using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISubAreaRepository : IGenericRepository<TSubArea>
    {

        Task<IEnumerable<SubAreaDTO>> ObtenerSubAreaAsync();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        Task<IEnumerable<ComboDTO>> ObtenerSubAreaPorIdAreaAsync(int idArea);
    }
}