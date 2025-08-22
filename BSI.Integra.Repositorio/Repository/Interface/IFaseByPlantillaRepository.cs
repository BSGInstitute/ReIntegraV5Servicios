using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFaseByPlantillaRepository : IGenericRepository<TFaseByPlantilla>
    {
        #region Metodos Base
        TFaseByPlantilla Add(FaseByPlantilla entidad);
        TFaseByPlantilla Update(FaseByPlantilla entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFaseByPlantilla> Add(IEnumerable<FaseByPlantilla> listadoEntidad);
        IEnumerable<TFaseByPlantilla> Update(IEnumerable<FaseByPlantilla> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        void EliminacionLogicoPorPlantilla(int idPlantilla, string usuario, List<int> nuevos);
        FaseByPlantilla ObtenerPorIdOrigenYPorIdPlantilla(int idFaseOrigen, int idPlantilla);

    }
}