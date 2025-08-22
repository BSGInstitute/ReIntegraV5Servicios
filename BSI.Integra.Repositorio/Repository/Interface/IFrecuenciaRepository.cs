using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFrecuenciaRepository : IGenericRepository<TFrecuencium>
    {
        #region Metodos Base
        TFrecuencium Add(Frecuencia entidad);
        TFrecuencium Update(Frecuencia entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFrecuencium> Add(IEnumerable<Frecuencia> listadoEntidad);
        IEnumerable<TFrecuencium> Update(IEnumerable<Frecuencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<FrecuenciaDTO> ObtenerFrecuencia();
        Frecuencia? ObtenerPorId(int id);
        FrecuenciaDTO? ObtenerFrecuenciaPorId(int idFrecuencia);
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        public List<DatosFrecuenciaGeneralDTO> ObtenerListaFrecuenciaActividad();
        public List<DatosFrecuenciaGeneralDTO> ObtenerFrecuenciaReporteDocumentos();

    }
}
