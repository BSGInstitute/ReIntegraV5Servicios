using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITableroComercialUnidadRepository : IGenericRepository<TTableroComercialUnidad>
    {
        #region Metodos Base
        TTableroComercialUnidad Add(TableroComercialUnidad entidad);
        TTableroComercialUnidad Update(TableroComercialUnidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTableroComercialUnidad> Add(IEnumerable<TableroComercialUnidad> listadoEntidad);
        IEnumerable<TTableroComercialUnidad> Update(IEnumerable<TableroComercialUnidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TableroComercialUnidadDTO> ObtenerTableroComercialUnidad();
        IEnumerable<TableroComercialUnidadComboDTO> ObtenerCombo();
        IEnumerable<TableroComercialUnidadSinAuditoriaDTO> ObtenerTableroComercialUnidadSinAuditoria();
    }
}