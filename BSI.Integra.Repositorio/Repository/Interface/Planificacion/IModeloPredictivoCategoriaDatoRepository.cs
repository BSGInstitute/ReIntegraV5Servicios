using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IModeloPredictivoCategoriaDatoRepository : IGenericRepository<TModeloPredictivoCategoriaDato>
    {
        #region Metodos Base
        TModeloPredictivoCategoriaDato Add(ModeloPredictivoCategoriaDato entidad);
        TModeloPredictivoCategoriaDato Update(ModeloPredictivoCategoriaDato entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloPredictivoCategoriaDato> Add(IEnumerable<ModeloPredictivoCategoriaDato> listadoEntidad);
        IEnumerable<TModeloPredictivoCategoriaDato> Update(IEnumerable<ModeloPredictivoCategoriaDato> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ModeloPredictivoCategoriaDato? ObtenerPorId(int id);
        IEnumerable<ModeloPredictivoCategoriaDato> ObtenerPorIdPGeneral(int idPGeneral);
        List<ModeloPredictivoCategoriaDatoDTO> ObtenerCategoriaDatoPorPrograma(int idPGeneral);
    }
}
