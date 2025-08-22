using BSI.Integra.Persistencia.Entidades.IntegraDB.Operaciones;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Operaciones
{
    public interface IProcesoPagoIvrRepository : IGenericRepository<TProcesoPagoIvr>
    {
        #region Metodos Base
        TProcesoPagoIvr Add(ProcesoPagoIvr entidad);
        TProcesoPagoIvr Update(ProcesoPagoIvr entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TProcesoPagoIvr> Add(IEnumerable<ProcesoPagoIvr> listadoEntidad);
        IEnumerable<TProcesoPagoIvr> Update(IEnumerable<ProcesoPagoIvr> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
