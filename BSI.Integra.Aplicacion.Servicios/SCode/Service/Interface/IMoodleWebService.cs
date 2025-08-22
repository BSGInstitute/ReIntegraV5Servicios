using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Servicios.Service.Interface
{
    public interface IMoodleWebService
    {
        public MoodleWebServiceRespuestaDTO CrearUsuario(MoodleWebServiceCrearUsuarioDTO usuario);
        public MoodleWebServiceRespuestaDTO ActualizarClaveMoodle(MoodleWebServiceActualizarClaveDTO accesos);
        public MoodleWebServiceRespuestaDTO RegistrarMatricula(MoodleWebServiceRegistrarMatriculaDTO matricula);
        public string Patron_CrearUsuario(MoodleWebServiceCrearUsuarioDTO usuario);

    }
}
