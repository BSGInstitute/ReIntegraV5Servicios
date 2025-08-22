using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadPrerequisitoEspecificoRepository : IGenericRepository<TOportunidadPrerequisitoEspecifico>
    {
        #region Metodos Base
        TOportunidadPrerequisitoEspecifico Add(OportunidadPrerequisitoEspecifico entidad);
        TOportunidadPrerequisitoEspecifico Update(OportunidadPrerequisitoEspecifico entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidadPrerequisitoEspecifico> Add(IEnumerable<OportunidadPrerequisitoEspecifico> listadoEntidad);
        IEnumerable<TOportunidadPrerequisitoEspecifico> Update(IEnumerable<OportunidadPrerequisitoEspecifico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
