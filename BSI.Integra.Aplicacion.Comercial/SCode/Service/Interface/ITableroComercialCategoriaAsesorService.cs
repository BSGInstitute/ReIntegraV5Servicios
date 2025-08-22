using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ITableroComercialCategoriaAsesorService
    {
        #region Metodos Base
        TableroComercialCategoriaAsesor Add(TableroComercialCategoriaAsesor entidad);
        TableroComercialCategoriaAsesor Update(TableroComercialCategoriaAsesor entidad);
        bool Delete(int id, string usuario);

        List<TableroComercialCategoriaAsesor> Add(List<TableroComercialCategoriaAsesor> listadoEntidad);
        List<TableroComercialCategoriaAsesor> Update(List<TableroComercialCategoriaAsesor> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TableroComercialCategoriaAsesorDTO> ObtenerTableroComercialCategoriaAsesor();
        IEnumerable<TableroComercialCategoriaAsesorComboDTO> ObtenerCombo();
        IEnumerable<TableroComercialCategoriaAsesorDatosTableroDTO> ObtenerDatosTablero();
    }
}
