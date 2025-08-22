using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadPrerequisitoGeneralRepository : IGenericRepository<TOportunidadPrerequisitoGeneral>
    {
        #region Metodos Base
        TOportunidadPrerequisitoGeneral Add(OportunidadPrerequisitoGeneral entidad);
        TOportunidadPrerequisitoGeneral Update(OportunidadPrerequisitoGeneral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidadPrerequisitoGeneral> Add(IEnumerable<OportunidadPrerequisitoGeneral> listadoEntidad);
        IEnumerable<TOportunidadPrerequisitoGeneral> Update(IEnumerable<OportunidadPrerequisitoGeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
