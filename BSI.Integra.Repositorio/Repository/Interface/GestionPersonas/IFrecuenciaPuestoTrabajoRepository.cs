using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IFrecuenciaPuestoTrabajoRepository : IGenericRepository<TFrecuenciaPuestoTrabajo>
    {
        #region Metodos Base
        TFrecuenciaPuestoTrabajo Add(FrecuenciaPuestoTrabajo entidad);
        TFrecuenciaPuestoTrabajo Update(FrecuenciaPuestoTrabajo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFrecuenciaPuestoTrabajo> Add(IEnumerable<FrecuenciaPuestoTrabajo> listadoEntidad);
        IEnumerable<TFrecuenciaPuestoTrabajo> Update(IEnumerable<FrecuenciaPuestoTrabajo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<FrecuenciaPuestoTrabajoDTO> Obtener();
        FrecuenciaPuestoTrabajo? ObtenerPorId(int id);
    }
}
