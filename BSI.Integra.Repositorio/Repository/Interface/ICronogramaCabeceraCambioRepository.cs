using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICronogramaCabeceraCambioRepository : IGenericRepository<TCronogramaCabeceraCambio>
    {
        #region Metodos Base
        TCronogramaCabeceraCambio Add(CronogramaCabeceraCambio entidad);
        TCronogramaCabeceraCambio Update(CronogramaCabeceraCambio entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCronogramaCabeceraCambio> Add(IEnumerable<CronogramaCabeceraCambio> listadoEntidad);
        IEnumerable<TCronogramaCabeceraCambio> Update(IEnumerable<CronogramaCabeceraCambio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


         CambiosCronogramaCabeceraDTO AprobarRechazarCambios(int idMatriculaCabecera, int idCambio, int version, bool aprobado, bool cancelado);
    }
}
