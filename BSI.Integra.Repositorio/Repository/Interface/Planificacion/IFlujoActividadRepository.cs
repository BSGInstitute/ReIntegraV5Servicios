using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IFlujoActividadRepository : IGenericRepository<TFlujoActividad>
    {
        #region Metodos Base
        TFlujoActividad Add(FlujoActividad entidad);
        TFlujoActividad Update(FlujoActividad entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFlujoActividad> Add(IEnumerable<FlujoActividad> listadoEntidad);
        IEnumerable<TFlujoActividad> Update(IEnumerable<FlujoActividad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        FlujoActividad? ObtenerPorId(int id);
        IEnumerable<FlujoActividadDTO>? ObtenerPorIdFlujoFase(int idFlujoFase);
    }
}
