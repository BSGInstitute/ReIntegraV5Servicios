using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadConfiguradoRepository : IGenericRepository<TOportunidadConfigurado>
    {
        #region Metodos Base
        TOportunidadConfigurado Add(OportunidadConfigurado entidad);
        TOportunidadConfigurado Update(OportunidadConfigurado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidadConfigurado> Add(IEnumerable<OportunidadConfigurado> listadoEntidad);
        IEnumerable<TOportunidadConfigurado> Update(IEnumerable<OportunidadConfigurado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}