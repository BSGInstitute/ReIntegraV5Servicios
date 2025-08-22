using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface IModeloPredictivoTrabajoRepository : IGenericRepository<TModeloPredictivoTrabajo>
    {
        #region Metodos Base
        TModeloPredictivoTrabajo Add(ModeloPredictivoTrabajo entidad);
        TModeloPredictivoTrabajo Update(ModeloPredictivoTrabajo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloPredictivoTrabajo> Add(IEnumerable<ModeloPredictivoTrabajo> listadoEntidad);
        IEnumerable<TModeloPredictivoTrabajo> Update(IEnumerable<ModeloPredictivoTrabajo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ModeloPredictivoTrabajo? ObtenerPorId(int id);
        IEnumerable<ModeloPredictivoTrabajo> ObtenerPorIdPGeneral(int idPGeneral);
        List<ModeloPredictivoTrabajoDTO> ObtenerTrabajoPorPrograma(int idPGeneral);
    }
}
