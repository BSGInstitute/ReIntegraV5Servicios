using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadBeneficioRepository : IGenericRepository<TOportunidadBeneficio>
    {
        #region Metodos Base
        TOportunidadBeneficio Add(OportunidadBeneficio entidad);
        TOportunidadBeneficio Update(OportunidadBeneficio entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidadBeneficio> Add(IEnumerable<OportunidadBeneficio> listadoEntidad);
        IEnumerable<TOportunidadBeneficio> Update(IEnumerable<OportunidadBeneficio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
