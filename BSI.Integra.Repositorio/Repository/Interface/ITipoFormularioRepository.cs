using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoFormularioRepository : IGenericRepository<TTipoInteracccion>
    {
        #region Metodos Base
        TTipoInteracccion Add(TipoFormulario entidad);
        TTipoInteracccion Update(TipoFormulario entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoInteracccion> Add(IEnumerable<TipoFormulario> listadoEntidad);
        IEnumerable<TTipoInteracccion> Update(IEnumerable<TipoFormulario> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<ComboDTO> ObtenerListaTipoFormulario();
    }
}
