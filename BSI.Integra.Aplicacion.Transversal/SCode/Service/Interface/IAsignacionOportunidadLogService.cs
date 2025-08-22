using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAsignacionOportunidadLogService
    {
        #region Metodos Base
        AsignacionOportunidadLog Add(AsignacionOportunidadLog entidad);
        AsignacionOportunidadLog Update(AsignacionOportunidadLog entidad);
        bool Delete(int id, string usuario);
        List<AsignacionOportunidadLog> Add(List<AsignacionOportunidadLog> listadoEntidad);
        List<AsignacionOportunidadLog> Update(List<AsignacionOportunidadLog> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
