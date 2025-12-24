
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IComunicacionAcademicaService
    {
        ConfigurarPreferenciaDTO ObtenerOpcionesPreferenciaComunicacion();
        object ActualizarPreferenciaComunicacionAlumno(PreferenciaConfiguracionDTO preferencia);
        PreferenciaConfiguracionDTO ObtenerPreferenciaComunicacionAlumno(int IdAlumno);
    }
}
