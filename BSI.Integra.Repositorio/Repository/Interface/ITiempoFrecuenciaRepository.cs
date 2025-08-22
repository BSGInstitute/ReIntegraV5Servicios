using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITiempoFrecuenciaRepository : IGenericRepository<TTiempoFrecuencium>
    {
        #region Metodos Base
        TTiempoFrecuencium Add(TiempoFrecuencia entidad);
        TTiempoFrecuencium Update(TiempoFrecuencia entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTiempoFrecuencium> Add(IEnumerable<TiempoFrecuencia> listadoEntidad);
        IEnumerable<TTiempoFrecuencium> Update(IEnumerable<TiempoFrecuencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<ComboDTO> ObtenerListaTiempoFrecuencia();
        List<ComboDTO> ObtenerListaParaFiltroSegmento();
        IEnumerable<TiempoFrecuenciaDTO> ObtenerComboPorIds(int[] ids);
        Task<IEnumerable<TiempoFrecuenciaDTO>> ObtenerComboPorIdsAsync(int[] ids);
    }
}
