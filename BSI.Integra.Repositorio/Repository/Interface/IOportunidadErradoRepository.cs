using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadErradoRepository : IGenericRepository<TOportunidadErrado>
    {
        #region Metodos Base
        TOportunidadErrado Add(OportunidadErrado entidad);
        TOportunidadErrado Update(OportunidadErrado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidadErrado> Add(IEnumerable<OportunidadErrado> listadoEntidad);
        IEnumerable<TOportunidadErrado> Update(IEnumerable<OportunidadErrado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}