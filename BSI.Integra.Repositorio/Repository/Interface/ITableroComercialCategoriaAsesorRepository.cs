using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITableroComercialCategoriaAsesorRepository : IGenericRepository<TTableroComercialCategoriaAsesor>
    {
        #region Metodos Base
        TTableroComercialCategoriaAsesor Add(TableroComercialCategoriaAsesor entidad);
        TTableroComercialCategoriaAsesor Update(TableroComercialCategoriaAsesor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTableroComercialCategoriaAsesor> Add(IEnumerable<TableroComercialCategoriaAsesor> listadoEntidad);
        IEnumerable<TTableroComercialCategoriaAsesor> Update(IEnumerable<TableroComercialCategoriaAsesor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TableroComercialCategoriaAsesorDTO> ObtenerTableroComercialCategoriaAsesor();
        IEnumerable<TableroComercialCategoriaAsesorComboDTO> ObtenerCombo();
        IEnumerable<TableroComercialCategoriaAsesorDatosTableroDTO> ObtenerDatosTablero();
    }
}
