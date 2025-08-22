using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICajaEgresoAprobadoService
    {
        #region Metodos Base
        CajaEgresoAprobado Add(CajaEgresoAprobado entidad);
        CajaEgresoAprobado Update(CajaEgresoAprobado entidad);
        bool Delete(int id, string usuario);

        List<CajaEgresoAprobado> Add(List<CajaEgresoAprobado> listadoEntidad);
        List<CajaEgresoAprobado> Update(List<CajaEgresoAprobado> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion


    }
}
