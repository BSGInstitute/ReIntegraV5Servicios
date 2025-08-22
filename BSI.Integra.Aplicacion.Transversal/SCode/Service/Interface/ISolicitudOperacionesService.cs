using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ISolicitudOperacionesService
    {
        SolicitudOperaciones ObtenerValorNuevo(bool aprobado, bool realizado, int idOportunidad, int iPlantillaBaseWhatsAppFacebook);
        SolicitudOperaciones ObtenerPorIdSolicitudOperaciones(int idSolicitudOperaciones);
        SolicitudOperacionesRealizadoDTO RealizadoSolicitudOperaciones(int idSolicitudOperaciones, string usuario, string observacion);
        List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperaciones(int idOportunidad);
        SolicitudOperaciones CancelarSolicitudOperaciones(int idSolicitudOperaciones, string usuario, string observacion);
        List<TodoSolicitudOperacionesDTO> ObtenerTodoSolicitudOperaciones();
        List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperacionesRealizadas(int idOportunidad);
        List<DatosSolicitudOperacionesDTO> ObtenerHistorialAccesoTemporal(int idOportunidad);
        IntDTO ValidarCambioSubEstado(int idOportunidad, string valorNuevo);
        IntDTO ActualizarTerminosPortalWeb(int idOportunidad);
        void AprobarCambioCategoriaAlumno(int idOportunidad, String categoria);
        bool ExisteTotal(int idOportunidad, int idTipoSolicitudOperaciones);
        SolicitudOperaciones InsertarSolicitudOperaciones(SolicitudOperacionesDTO obj);
        int ObtenerMatriculaPorOportunidad(int idOportunidad);
        void RegistrarCursoPrueba(int idSolicitudOperaciones);
        void AmpliacionAccesosTemporales(int idAlumno, string fechaExpiracion, string idPEspecifico);
    }
}
