using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoServicioRepository : IGenericRepository<TTipoServicio>
    {
        #region Metodos Base
        TTipoServicio Add(TipoServicio entidad);
        TTipoServicio Update(TipoServicio entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoServicio> Add(IEnumerable<TipoServicio> listadoEntidad);
        IEnumerable<TTipoServicio> Update(IEnumerable<TipoServicio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoServicioDTO> ObtenerTipoServicio();
        IEnumerable<TipoServicioComboDTO> ObtenerCombo();
    }
}
