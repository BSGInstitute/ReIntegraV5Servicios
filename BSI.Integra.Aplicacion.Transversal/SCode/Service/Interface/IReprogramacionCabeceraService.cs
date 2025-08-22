using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IReprogramacionCabeceraService
    {
        #region Metodos Base
        ReprogramacionCabecera Add(ReprogramacionCabecera entidad);
        ReprogramacionCabecera Update(ReprogramacionCabecera entidad);
        bool Delete(int id, string usuario);

        List<ReprogramacionCabecera> Add(List<ReprogramacionCabecera> listadoEntidad);
        List<ReprogramacionCabecera> Update(List<ReprogramacionCabecera> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ReprogramacionCabeceraDTO> ObtenerReprogramacionCabecera();
        ReprogramacionCabeceraRADTO ObtenerCantidadIntervaloYReprogramacionPorDiaPermitida(int idActividadCabecera, int idCategoria);
        ReprogramacionCabeceraPersonalRADTO ObtenerCantidadReprogramacionDelDiaPorAsesor(int idActividadCabecera, int idCategoria, int idPersonal);
        ReprogramacionCabecera ObtenerPorIdCabeceraIdCategoriaOrigen(int idActividadCabecera, int idCategoriaOrigen);
        ReprogramacionCabecera MapeoEntidadDesdeDTOReprogramacion(ReprogramacionCabeceraSinAuditoriaDTO objetoDto);
    }
}
