using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISeccionRepository : IGenericRepository<TSeccion>
    {
        #region Metodos Base
        TSeccion Add(Seccion entidad);
        TSeccion Update(Seccion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSeccion> Add(IEnumerable<Seccion> listadoEntidad);
        IEnumerable<TSeccion> Update(IEnumerable<Seccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SeccionCombo> ObtenerCombo();
        IEnumerable<Seccion> ObtenerSeccion();

    }
}
