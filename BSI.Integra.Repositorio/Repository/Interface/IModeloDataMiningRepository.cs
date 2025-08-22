using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IModeloDataMiningRepository : IGenericRepository<TModeloDataMining>
    {
        #region Metodos Base
        TModeloDataMining Add(ModeloDataMining entidad);
        TModeloDataMining Update(ModeloDataMining entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloDataMining> Add(IEnumerable<ModeloDataMining> listadoEntidad);
        IEnumerable<TModeloDataMining> Update(IEnumerable<ModeloDataMining> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<ValorIntDTO> ObtenerListaPorOportunidad(int idOportunidad);
        ModeloDataMining ObtenerPorOportunidad(int idOportunidad);
        ProbabilidadModeloDataMiningDTO ObtenerProbabilidad(int idOportunidad);
        ModeloDataMining ObtenerPorId(int id);
    }
}
