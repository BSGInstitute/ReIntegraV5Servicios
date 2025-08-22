using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITarifarioRepository : IGenericRepository<TTarifario>
    {
        bool ActualizarGestionadoCronogramaPagoTarifario(int id);

        List<FiltroDTO> ObtenerTodoFiltro();
    }
}
