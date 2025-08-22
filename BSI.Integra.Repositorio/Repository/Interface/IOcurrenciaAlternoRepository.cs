using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOcurrenciaAlternoRepository : IGenericRepository<TOcurrenciaAlterno>
    {
        #region Metodos Base
        TOcurrenciaAlterno Add(OcurrenciaAlterno entidad);
        TOcurrenciaAlterno Update(OcurrenciaAlterno entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOcurrenciaAlterno> Add(IEnumerable<OcurrenciaAlterno> listadoEntidad);
        IEnumerable<TOcurrenciaAlterno> Update(IEnumerable<OcurrenciaAlterno> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OcurrenciaAlternoDTO> ObtenerOcurrenciaAlterno();
        IEnumerable<OcurrenciaAlternoComboDTO> ObtenerCombo();
        OcurrenciaAlternoDTO ObtenerOcurrenciaPorActividad(int idOcurrencia);
        OcurrenciaAlterno ObtenerPorId(int idOcurrencia);
        Task<OcurrenciaAlterno> ObtenerPorIdAsync(int idOcurrencia);
    }
}