using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IPaisAsignacionRegularService
    {
        #region Metodos Base
        PaisAsignacionRegular Add(PaisAsignacionRegular entidad);
        PaisAsignacionRegular Update(PaisAsignacionRegular entidad);
        bool Delete(int id, string usuario);

        List<PaisAsignacionRegular> Add(List<PaisAsignacionRegular> listadoEntidad);
        List<PaisAsignacionRegular> Update(List<PaisAsignacionRegular> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion


    }
}
