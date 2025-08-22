using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ITableroComercialUnidadService
    {
        #region Metodos Base
        TableroComercialUnidad Add(TableroComercialUnidad entidad);
        TableroComercialUnidad Update(TableroComercialUnidad entidad);
        bool Delete(int id, string usuario);

        List<TableroComercialUnidad> Add(List<TableroComercialUnidad> listadoEntidad);
        List<TableroComercialUnidad> Update(List<TableroComercialUnidad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TableroComercialUnidadDTO> ObtenerTableroComercialUnidad();
        IEnumerable<TableroComercialUnidadComboDTO> ObtenerCombo();
        IEnumerable<TableroComercialUnidadSinAuditoriaDTO> ObtenerTableroComercialUnidadSinAuditoria();
    }
}
