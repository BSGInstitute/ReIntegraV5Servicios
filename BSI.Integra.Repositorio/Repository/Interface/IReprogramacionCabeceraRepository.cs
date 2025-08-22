using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReprogramacionCabeceraRepository : IGenericRepository<TReprogramacionCabecera>
    {
        #region Metodos Base
        TReprogramacionCabecera Add(ReprogramacionCabecera entidad);
        TReprogramacionCabecera Update(ReprogramacionCabecera entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TReprogramacionCabecera> Add(IEnumerable<ReprogramacionCabecera> listadoEntidad);
        IEnumerable<TReprogramacionCabecera> Update(IEnumerable<ReprogramacionCabecera> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ReprogramacionCabeceraDTO> ObtenerReprogramacionCabecera();
        ReprogramacionCabeceraRADTO ObtenerCantidadIntervaloYReprogramacionPorDiaPermitida(int idActividadCabecera, int idCategoria);
        ReprogramacionCabeceraPersonalRADTO ObtenerCantidadReprogramacionDelDiaPorAsesor(int idActividadCabecera, int idCategoria, int idPersonal);
        ReprogramacionCabecera ObtenerPorIdCabeceraIdCategoriaOrigen(int idActividadCabecera, int idCategoriaOrigen);
        Task<ReprogramacionCabecera> ObtenerPorIdCabeceraIdCategoriaOrigenAsync(int idActividadCabecera, int idCategoriaOrigen);
        List<ReprogramacionCabeceraDTO> ObtenerReprogramacionCabPorActividadCab(int IdActividadCabecera);

    }
}