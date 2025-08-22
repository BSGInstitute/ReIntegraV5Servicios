using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoImpuestoRepository : IGenericRepository<TTipoImpuesto>
    {
        #region Metodos Base
        TTipoImpuesto Add(TipoImpuesto entidad);
        TTipoImpuesto Update(TipoImpuesto entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoImpuesto> Add(IEnumerable<TipoImpuesto> listadoEntidad);
        IEnumerable<TTipoImpuesto> Update(IEnumerable<TipoImpuesto> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoImpuestoDTO> ObtenerTipoImpuesto();
        IEnumerable<TipoImpuestoComboDTO> ObtenerCombo();
    }
}
