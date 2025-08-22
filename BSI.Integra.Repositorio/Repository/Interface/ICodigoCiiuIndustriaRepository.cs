using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICodigoCiiuIndustriaRepository : IGenericRepository<TCodigoCiiuIndustrium>
    {
        CodigoCiiuIndustria ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerPorNombre(string filtro);
    }
}
