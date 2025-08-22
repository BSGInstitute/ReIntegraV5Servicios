using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ISolucionClienteByActividadService
    {
        #region Metodos Base
        SolucionClienteByActividad Add(SolucionClienteByActividad entidad);
        SolucionClienteByActividad Update(SolucionClienteByActividad entidad);
        bool Delete(int id, string usuario);

        List<SolucionClienteByActividad> Add(List<SolucionClienteByActividad> listadoEntidad);
        List<SolucionClienteByActividad> Update(List<SolucionClienteByActividad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
