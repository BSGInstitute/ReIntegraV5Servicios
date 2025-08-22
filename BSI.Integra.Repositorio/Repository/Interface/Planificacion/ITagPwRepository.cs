using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ITagPwRepository : IGenericRepository<TTagPw>
    {
        #region Metodos Base
        TTagPw Add(TagPw entidad);
        TTagPw Update(TagPw entidad);
        bool Delete(int id, string usuario);
        #endregion
        TagPw? ObtenerPorId(int id);
        IEnumerable<TagEntidadPwDTO> Obtener();
        IEnumerable<ParametroSeoPortalWebDTO> ObtenerParametroPorIdTag(int idTag);
        IEnumerable<ParametroSeoPw> ObtenerParametroSeoPwPorIdTag(int id);
        IEnumerable<DatosTagPwDTO> ObtenerTagAsociados(List<int> id);
        IEnumerable<ComboDTO> ObtenerTagSinAsociar(List<int> ids);
    }
}
