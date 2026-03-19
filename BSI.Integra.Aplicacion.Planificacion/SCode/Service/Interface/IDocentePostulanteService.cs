using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    /// <summary>
    /// Interfaz del servicio para el manejo de docentes postulantes
    /// </summary>
    public interface IDocentePostulanteService
    {
        /// <summary>
        /// Obtiene todos los docentes postulantes activos
        /// </summary>
        List<DocentePostulanteDTO> Obtener();

        /// <summary>
        /// Obtiene un docente postulante por su ID
        /// </summary>
        DocentePostulanteDTO ObtenerPorId(int id);

        /// <summary>
        /// Inserta un nuevo docente postulante y crea automáticamente Persona, ClasificacionPersona y GestionContacto
        /// </summary>
        Task<DocentePostulanteDTO> InsertarAsync(DocentePostulanteDTO dto, string usuario);

        /// <summary>
        /// Actualiza un docente postulante existente
        /// </summary>
        DocentePostulanteDTO Actualizar(DocentePostulanteDTO dto, string usuario);

        /// <summary>
        /// Elimina lógicamente un docente postulante
        /// </summary>
        bool Eliminar(int id, string usuario);
    }
}
