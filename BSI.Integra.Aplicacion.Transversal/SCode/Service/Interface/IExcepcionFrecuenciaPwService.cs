using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IExcepcionFrecuenciaPwService
    {
        #region Metodos Base
        ExcepcionFrecuenciaPw Add(ExcepcionFrecuenciaPw entidad);
        ExcepcionFrecuenciaPw Update(ExcepcionFrecuenciaPw entidad);
        bool Delete(int id, string usuario);

        List<ExcepcionFrecuenciaPw> Add(List<ExcepcionFrecuenciaPw> listadoEntidad);
        List<ExcepcionFrecuenciaPw> Update(List<ExcepcionFrecuenciaPw> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ExcepcionFrecuenciaPwDTO> ObtenerExcepcionFrecuenciaPw();
        IEnumerable<ExcepcionFrecuenciaPGeneralDTO> ObtenerTodoProgramaGeneral();
    }
}
