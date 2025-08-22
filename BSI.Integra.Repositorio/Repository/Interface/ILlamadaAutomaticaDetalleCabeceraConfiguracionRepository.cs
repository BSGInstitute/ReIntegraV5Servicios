using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ILlamadaAutomaticaDetalleCabeceraConfiguracionRepository : IGenericRepository<TLlamadaAutomaticaDetalleCabeceraConfiguracion>
    {
        #region Metodos Base
        TLlamadaAutomaticaDetalleCabeceraConfiguracion Add(LlamadaAutomaticaDetalleCabeceraConfiguracion entidad);
        TLlamadaAutomaticaDetalleCabeceraConfiguracion Update(LlamadaAutomaticaDetalleCabeceraConfiguracion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TLlamadaAutomaticaDetalleCabeceraConfiguracion> Add(IEnumerable<LlamadaAutomaticaDetalleCabeceraConfiguracion> listadoEntidad);
        IEnumerable<TLlamadaAutomaticaDetalleCabeceraConfiguracion> Update(IEnumerable<LlamadaAutomaticaDetalleCabeceraConfiguracion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public LlamadaAutomaticaDetalleCabeceraConfiguracion ObtenerLlamadaAutomaticaDetalleCabeceraConfiguracionPorId(int Id);
    }
}
