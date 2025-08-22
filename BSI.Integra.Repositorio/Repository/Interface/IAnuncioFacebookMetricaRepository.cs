using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAnuncioFacebookMetricaRepository : IGenericRepository<TAnuncioFacebookMetrica>
    {
        #region Metodos Base
        TAnuncioFacebookMetrica Add(AnuncioFacebookMetrica entidad);
        TAnuncioFacebookMetrica Update(AnuncioFacebookMetrica entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAnuncioFacebookMetrica> Add(IEnumerable<AnuncioFacebookMetrica> listadoEntidad);
        IEnumerable<TAnuncioFacebookMetrica> Update(IEnumerable<AnuncioFacebookMetrica> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

     


        List<ReporteAnuncioFacebookMetricaDTO> ObtenerReporteAnuncioFacebookMetrica(int? idAreaCapacitacion);
        public string ObtenerUltimaModificacion();
        List<AreaAnuncioFacebookMetricaDTO> ObtenerComboAreaAnuncioFacebookMetrica();
        bool EliminarDatosPorFecha(DateTime fechaConsulta, string usuario);

    }
}
