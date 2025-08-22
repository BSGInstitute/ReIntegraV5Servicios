using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IFlujoFaseRepository : IGenericRepository<TFlujoFase>
    {
        #region Metodos Base
        TFlujoFase Add(FlujoFase entidad);
        TFlujoFase Update(FlujoFase entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFlujoFase> Add(IEnumerable<FlujoFase> listadoEntidad);
        IEnumerable<TFlujoFase> Update(IEnumerable<FlujoFase> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        FlujoFase? ObtenerPorId(int id);
        IEnumerable<FlujoFaseDTO>? ObtenerPorIdFlujo(int idFlujo);
    }
}
