using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoContribuyenteRepository : IGenericRepository<TTipoContribuyente>
    {
        #region Metodos Base
        TTipoContribuyente Add(TipoContribuyente entidad);
        TTipoContribuyente Update(TipoContribuyente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoContribuyente> Add(IEnumerable<TipoContribuyente> listadoEntidad);
        IEnumerable<TTipoContribuyente> Update(IEnumerable<TipoContribuyente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<ComboDTO> ObtenerTipoContribuyente();
    }
}
