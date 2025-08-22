using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOportunidadInformacionService
    {
        OportunidadInformacionDTO ObtenerOportunidadInformacion(int idClasificacionPersona, int idAlumno);
        InformacionOportunidadDTO ObtenerInformacionOportunidad(int idAlumno, int idClasificacionPersona);
    }
}
