using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaAsociacionModuloSistemaRepository : IGenericRepository<TPlantillaAsociacionModuloSistema>
    {
        void EliminacionLogicoPorPlantilla(int idPlantilla, string usuario, List<int> nuevos);

        IEnumerable<PlantillaAsociacionModuloSistema> ObtenerPlantillaAsociacionModuloSistemaPorIdPlantilla(int idPlantilla);
        PlantillaAsociacionModuloSistema ObtenerPorIdModuloSistemaYPorIdPlantilla(int idModuloSistema, int idPlantilla);

    }
}