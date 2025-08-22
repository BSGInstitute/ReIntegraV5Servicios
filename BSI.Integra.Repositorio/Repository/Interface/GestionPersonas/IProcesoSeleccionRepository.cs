using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IProcesoSeleccionRepository : IGenericRepository<TProcesoSeleccion>
    {
        #region Metodos Base
        TProcesoSeleccion Add(ProcesoSeleccion entidad);
        TProcesoSeleccion Update(ProcesoSeleccion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProcesoSeleccion> Add(IEnumerable<ProcesoSeleccion> listadoEntidad);
        IEnumerable<TProcesoSeleccion> Update(IEnumerable<ProcesoSeleccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public List<ProcesoSeleccionConvocatoriaDTO> ObtenerProcesosSeleccionConvocatoria();
        IEnumerable<ComboDTO> ObtenerCodigoNombre();
        Task<IEnumerable<ComboDTO>> ObtenerCodigoNombreAsync();
        public List<ProcesoSeleccionDTO> ObtenerProcesoSeleccionTotal();
        ProcesoSeleccion? ObtenerPorId(int id);
        public List<ProcesoSeleccionDTO> ObtenerProcesosSeleccion();
        List<ConfigurarProcesoSeleccionDTO> ObtenerConfiguracionProcesoSeleccion();
        IEnumerable<ProcesoSeleccionEstadoFiltroDTO> ObtenerEstadoProcesoSeleccion();
        ProcesoSeleccionEstadoFiltroDTO ObtenerEstadoProcesoSeleccionPorId(int id);
        List<ReporteAnalisisProcesoSeleccionDTO> ObtenerReporteAnalisisProcesoSeleccion(FiltroAnalisisProcesoSeleccionDTO filtro);
        List<ReporteAnalisisProcesoSeleccionDTO> ObtenerReporteAnalisisProcesoSeleccion_V2(FiltroAnalisisProcesoSeleccionDTO filtro);
        IEnumerable<ProcesoSeleccionComboReporteDTO> ObtenerCombo();
        
    }
}
