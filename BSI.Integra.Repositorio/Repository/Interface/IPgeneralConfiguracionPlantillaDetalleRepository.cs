using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPgeneralConfiguracionPlantillaDetalleRepository : IGenericRepository<TPgeneralConfiguracionPlantillaDetalle>
    {
        #region Metodos Base
        TPgeneralConfiguracionPlantillaDetalle Add(PGeneralConfiguracionPlantillaDetalle entidad);
        TPgeneralConfiguracionPlantillaDetalle Update(PGeneralConfiguracionPlantillaDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralConfiguracionPlantillaDetalle> Add(IEnumerable<PGeneralConfiguracionPlantillaDetalle> listadoEntidad);
        IEnumerable<TPgeneralConfiguracionPlantillaDetalle> Update(IEnumerable<PGeneralConfiguracionPlantillaDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PGeneralConfiguracionPlantillaDetalle> ObtenerPorIdPgeneralConfiguracionPlantilla(int idPgeneralConfiguracionPlantilla);
    }
}
