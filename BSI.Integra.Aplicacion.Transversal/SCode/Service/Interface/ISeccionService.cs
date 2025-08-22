using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ISeccionService
    {

        #region Metodos Base
        Seccion Add(SeccionEnvio entidad);
        Seccion Update(SeccionEnvio entidad);
        bool Delete(int id, string usuario);

        List<Seccion> Add(List<Seccion> listadoEntidad);
        List<Seccion> Update(List<Seccion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SeccionCombo> ObtenerCombo();
        IEnumerable<Seccion> ObtenerSeccion();



    }
}
