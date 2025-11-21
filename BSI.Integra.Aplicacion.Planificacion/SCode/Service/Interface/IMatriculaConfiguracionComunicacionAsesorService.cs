using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;

// =============================================
// Interfaz: IMatriculaConfiguracionComunicacionAsesorService
// Ubicación: BSI.Integra.Aplicacion.Planificacion\SCode\Service\Interface\
// Autor: Miguel Valdivia
// Fecha: 20/11/2025
// =============================================

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    /// <summary>
    /// Interfaz del servicio para gestionar configuración de comunicación académica desde sistema asesor
    /// </summary>
    public interface IMatriculaConfiguracionComunicacionAsesorService
    {
        /// <summary>
        /// Obtiene el catálogo de horarios de contacto disponibles
        /// </summary>
        /// <returns>Lista de horarios</returns>
        IEnumerable<CatalogoHorarioContactoAsesorDTO> ObtenerCatalogoHorariosContacto();

        /// <summary>
        /// Obtiene la configuración de comunicación de un alumno usando su IdAlumno
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>ResponseConfiguracionComunicacionAsesorDTO con los datos o error</returns>
        ResponseConfiguracionComunicacionAsesorDTO ObtenerConfiguracionPorAlumno(int idAlumno);

        /// <summary>
        /// Guarda la configuración de comunicación de un alumno en todas sus matrículas activas
        /// Inserta o actualiza según corresponda
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <param name="listaDatos">Lista de configuraciones</param>
        /// <param name="usuario">Usuario asesor que realiza la acción</param>
        /// <returns>ResponseConfiguracionComunicacionAsesorDTO con resultado</returns>
        ResponseConfiguracionComunicacionAsesorDTO GuardarConfiguracionPorAlumno(
            int idAlumno,
            List<GuardarConfiguracionComunicacionAsesorDTO> listaDatos,
            string usuario);
    }
}
