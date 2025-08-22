using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProcedenciaVentaCruzadumRepository : IGenericRepository<TProcedenciaVentaCruzadum>
    {
        bool InsertarProcedenciaVentaCruzada(int IdOportunidadActual, int IdOportunidadNueva);
    }
}
