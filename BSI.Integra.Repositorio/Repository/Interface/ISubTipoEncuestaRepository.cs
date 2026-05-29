using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISubTipoEncuestaRepository : IGenericRepository<TSubTipoEncuesta>
    {
        #region Metodos Base
        TSubTipoEncuesta Add(SubTipoEncuesta entidad);
        TSubTipoEncuesta Update(SubTipoEncuesta entidad);
        bool Delete(int id, string usuario);
        #endregion

        SubTipoEncuesta ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        List<SubTipoEncuestaDTO> ObtenerTodo();
    }
}
