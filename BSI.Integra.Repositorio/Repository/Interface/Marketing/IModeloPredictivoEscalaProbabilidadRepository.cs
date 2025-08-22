using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface IModeloPredictivoEscalaProbabilidadRepository : IGenericRepository<TModeloPredictivoEscalaProbabilidad>
    {
        #region Metodos Base
        TModeloPredictivoEscalaProbabilidad Add(ModeloPredictivoEscalaProbabilidad entidad);
        TModeloPredictivoEscalaProbabilidad Update(ModeloPredictivoEscalaProbabilidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloPredictivoEscalaProbabilidad> Add(IEnumerable<ModeloPredictivoEscalaProbabilidad> listadoEntidad);
        IEnumerable<TModeloPredictivoEscalaProbabilidad> Update(IEnumerable<ModeloPredictivoEscalaProbabilidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ModeloPredictivoEscalaProbabilidad? ObtenerPorId(int id);
        IEnumerable<ModeloPredictivoEscalaProbabilidad> ObtenerPorIdPGeneral(int idPGeneral);
        List<ModeloPredictivoEscalaDTO> ObtenerEscalaPorPrograma(int idPGeneral);
    }
}
