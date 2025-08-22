using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMandrilEnvioCorreoRepository : IGenericRepository<TMandrilEnvioCorreo>
    {
        #region Metodos Base
        TMandrilEnvioCorreo Add(MandrilEnvioCorreo entidad);
        TMandrilEnvioCorreo Update(MandrilEnvioCorreo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMandrilEnvioCorreo> Add(IEnumerable<MandrilEnvioCorreo> listadoEntidad);
        IEnumerable<TMandrilEnvioCorreo> AddSync(IEnumerable<MandrilEnvioCorreo> listadoEntidad);
        IEnumerable<TMandrilEnvioCorreo> Update(IEnumerable<MandrilEnvioCorreo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        bool ValidarEnvioCorreo(int idOportunidad, string usuario);
    }
}
