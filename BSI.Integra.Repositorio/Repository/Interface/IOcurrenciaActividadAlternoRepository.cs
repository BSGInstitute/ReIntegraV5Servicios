using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOcurrenciaActividadAlternoRepository : IGenericRepository<TOcurrenciaActividadAlterno>
    {
        #region Metodos Base
        TOcurrenciaActividadAlterno Add(OcurrenciaActividadAlterno entidad);
        TOcurrenciaActividadAlterno Update(OcurrenciaActividadAlterno entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOcurrenciaActividadAlterno> Add(IEnumerable<OcurrenciaActividadAlterno> listadoEntidad);
        IEnumerable<TOcurrenciaActividadAlterno> Update(IEnumerable<OcurrenciaActividadAlterno> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OcurrenciaActividadAlternoDTO> ObtenerOcurrenciaActividadAlterno();
        IEnumerable<OcurrenciaActividadAlternoComboDTO> ObtenerCombo();
        IEnumerable<ArbolOcurenciaAlternoDTO> ObtenerArbolOcurrenciaAlterno(int idActividadCabecera, int idOcurrenciaPadre);
        ArbolOcurenciaAlternoDTO? ObtenerArbolOcurrenciaAlternoV2(int idActividadCabecera, int idOcurrenciaPadre, int idOcurrenciaReporte);
        OcurenciaActividadCompletoDTO ObtenerOcurrenciaActividadPorId(int? idOcurrenciaActividad);
        Task<OcurenciaActividadCompletoDTO> ObtenerOcurrenciaActividadPorIdAsync(int? idOcurrenciaActividad);
    }
}