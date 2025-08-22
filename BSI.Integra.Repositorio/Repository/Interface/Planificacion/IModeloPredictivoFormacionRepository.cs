using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IModeloPredictivoFormacionRepository : IGenericRepository<TModeloPredictivoFormacion>
    {
        #region Metodos Base
        TModeloPredictivoFormacion Add(ModeloPredictivoFormacion entidad);
        TModeloPredictivoFormacion Update(ModeloPredictivoFormacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloPredictivoFormacion> Add(IEnumerable<ModeloPredictivoFormacion> listadoEntidad);
        IEnumerable<TModeloPredictivoFormacion> Update(IEnumerable<ModeloPredictivoFormacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ModeloPredictivoFormacion? ObtenerPorId(int id);
        IEnumerable<ModeloPredictivoFormacion> ObtenerPorIdPGeneral(int idPGeneral);
        List<ModeloPredictivoFormacionDTO> ObtenerAreaFormacionPorPrograma(int idPGeneral);
    }
}
