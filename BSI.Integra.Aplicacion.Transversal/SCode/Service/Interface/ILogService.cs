using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ILogService
    {
        #region Metodos Base
        Log Add(Log entidad);
        Log Update(Log entidad);
        bool Delete(int id, string usuario);

        List<Log> Add(List<Log> listadoEntidad);
        List<Log> Update(List<Log> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
