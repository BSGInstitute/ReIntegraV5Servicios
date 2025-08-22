using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IModeloDataMiningService
    {
        #region Metodos Base
        ModeloDataMining Add(ModeloDataMining entidad);
        ModeloDataMining Update(ModeloDataMining entidad);
        bool Delete(int id, string usuario);

        List<ModeloDataMining> Add(List<ModeloDataMining> listadoEntidad);
        List<ModeloDataMining> Update(List<ModeloDataMining> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<ValorIntDTO> ListaPorOportunidad(int idOportunidad);
        ModeloDataMining ObtenerPorOportunidad(int idOportunidad);
        void ObtenerProbabilidad(int idOportunidad, ref ModeloDataMining modeloDataMining);
        ModeloDataMining ObtenerPorId(int id);
    }
}
