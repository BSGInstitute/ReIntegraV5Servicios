using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;

// =============================================
// Interfaz: IMatriculaConfiguracionComunicacionAsesorRepository
// Ubicación: BSI.Integra.Repositorio\Repository\Interface\
// Autor: Miguel Valdivia
// Fecha: 20/11/2025
// =============================================

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz del repositorio para gestionar configuración de comunicación académica desde sistema asesor
    /// </summary>
    public interface IMatriculaConfiguracionComunicacionAsesorRepository
    {
        /// <summary>
        /// Obtiene todas las matrículas activas de un alumno
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>Lista de matrículas activas</returns>
        IEnumerable<MatriculaActivaAsesorDTO> ObtenerMatriculasActivasPorAlumno(int idAlumno);

        /// <summary>
        /// Obtiene el catálogo de horarios de contacto disponibles
        /// </summary>
        /// <returns>Lista de horarios</returns>
        IEnumerable<CatalogoHorarioContactoAsesorDTO> ObtenerCatalogoHorariosContacto();

        /// <summary>
        /// Obtiene la configuración de comunicación de una matrícula específica
        /// </summary>
        /// <param name="idMatriculaCabecera">ID de la matrícula</param>
        /// <param name="idPGeneral">ID del programa general</param>
        /// <returns>Lista de configuraciones</returns>
        IEnumerable<ConfiguracionComunicacionAsesorDTO> ObtenerConfiguracionComunicacion(
            int idMatriculaCabecera,
            int idPGeneral);

        /// <summary>
        /// Inserta configuración de comunicación para una matrícula
        /// </summary>
        /// <param name="idMatriculaCabecera">ID de la matrícula</param>
        /// <param name="idPGeneral">ID del programa</param>
        /// <param name="listaDatos">Configuraciones a insertar</param>
        /// <param name="usuario">Usuario que realiza la acción</param>
        /// <returns>True si fue exitoso</returns>
        bool InsertarConfiguracionComunicacion(
            int idMatriculaCabecera,
            int idPGeneral,
            List<GuardarConfiguracionComunicacionAsesorDTO> listaDatos,
            string usuario);

        /// <summary>
        /// Actualiza configuración de comunicación existente
        /// </summary>
        /// <param name="listaDatos">Configuraciones a actualizar</param>
        /// <param name="usuario">Usuario que realiza la acción</param>
        /// <returns>True si fue exitoso</returns>
        bool ActualizarConfiguracionComunicacion(
            List<ConfiguracionComunicacionAsesorDTO> listaDatos,
            string usuario);
    }
}
