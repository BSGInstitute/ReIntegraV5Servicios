using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITiempoFrecuenciaService
    {
        #region Metodos Base
        TiempoFrecuencia Add(TiempoFrecuencia entidad);
        TiempoFrecuencia Update(TiempoFrecuencia entidad);
        bool Delete(int id, string usuario);

        List<TiempoFrecuencia> Add(List<TiempoFrecuencia> listadoEntidad);
        List<TiempoFrecuencia> Update(List<TiempoFrecuencia> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion


        List<DTO.ComboDTO> ObtenerListaParaFiltroSegmento();
        }
}
