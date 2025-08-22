using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IPaisConfiguracionAsignacionRegularService
    {
        #region Metodos Base
        PaisConfiguracionAsignacionRegular Add(PaisConfiguracionAsignacionRegular entidad);
        PaisConfiguracionAsignacionRegular Update(PaisConfiguracionAsignacionRegular entidad);
        bool Delete(int id, string usuario);

        List<PaisConfiguracionAsignacionRegular> Add(List<PaisConfiguracionAsignacionRegular> listadoEntidad);
        List<PaisConfiguracionAsignacionRegular> Update(List<PaisConfiguracionAsignacionRegular> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
