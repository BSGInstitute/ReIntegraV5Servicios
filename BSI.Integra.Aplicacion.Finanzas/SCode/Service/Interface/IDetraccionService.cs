using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IDetraccionService
    {
        #region Metodos Base
        Detraccion Add(Detraccion entidad);
        Detraccion Update(Detraccion entidad);
        bool Delete(int id, string usuario);

        List<Detraccion> Add(List<Detraccion> listadoEntidad);
        List<Detraccion> Update(List<Detraccion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ReporteDetraccionDTO> ObtenerReporteDetraccion(ReporteDetraccionFiltroDTO filtro);
        IEnumerable<DetraccionComboDTO> ObtenerCombo();
        IEnumerable<DetraccionComboDTO> ObtenerValorDetraccionPorPais(int IdPais);
    }
}
