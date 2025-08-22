using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISolicitudOperacionesRepository : IGenericRepository<TSolicitudOperacione>
    {
        #region Metodos Base
        TSolicitudOperacione Add(SolicitudOperaciones entidad);
        TSolicitudOperacione Update(SolicitudOperaciones entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSolicitudOperacione> Add(IEnumerable<SolicitudOperaciones> listadoEntidad);
        IEnumerable<TSolicitudOperacione> Update(IEnumerable<SolicitudOperaciones> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        SolicitudOperaciones ObtenerValorNuevo(bool aprobado, bool realizado, int idOportunidad, int iPlantillaBaseWhatsAppFacebook);
        SolicitudOperaciones ObtenerPorIdSolicitudOperaciones(int idSolicitudOperaciones); 
        SolicitudOperaciones ObtenerPorIdAprobadoSolicitudOperaciones(int idSolicitudOperaciones);
        List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperacionesEnBloque(int idSolicitudOperaciones);
        List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperaciones(int idOportunidad);
        List<TodoSolicitudOperacionesDTO> ObtenerTodoSolicitudOperaciones();

        List<TodoSolicitudOperacionesDTO> ObtenerTodoFiltroOperaciones(filtroReportetipo4DTO filtroSolicitudReporte);
        List<TodoSolicitudOperacionesDTO> ObtenerTodoFiltroOperacionesCompleto(filtroReporteDTO filtroSolicitudReporte);
        List<TipoSolicitudDTO> ObtenerTipoSolicitud();
        List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperacionesRealizadas(int idOportunidad);
        List<HistorialAsesoraDTO> ObtenerHistorialAsesora(int idMatriculaCabecera);
        List<DatosSolicitudOperacionesDTO> ObtenerHistorialAccesoTemporal(int idOportunidad);
        IntDTO ValidarCambioSubEstado(int idOportunidad, string valorNuevo);
        IntDTO ActualizarTerminosPortalWeb(int idOportunidad);
        void AprobarCambioCategoriaAlumno(int idOportunidad, String categoria);
        bool ExisteTotal(int idOportunidad, int idTipoSolicitudOperaciones);
        int ObtenerMatriculaPorOportunidad(int idOportunidad);
        void RegistrarCursoPrueba(int idSolicitudOperaciones);
        void AmpliacionAccesosTemporales(int idAlumno, string fechaExpiracion, string idPEspecifico);
    }
}
