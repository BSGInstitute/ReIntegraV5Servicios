using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    /// <summary>
    /// Interfaz del repositorio para el manejo de docentes postulantes
    /// </summary>
    public interface IDocentePostulanteRepository : IGenericRepository<TDocentePostulante>
    {
        #region Metodos Base
        /// <summary>
        /// Agrega un nuevo docente postulante
        /// </summary>
        TDocentePostulante Add(DocentePostulante entidad);

        /// <summary>
        /// Actualiza un docente postulante existente
        /// </summary>
        TDocentePostulante Update(DocentePostulante entidad);

        /// <summary>
        /// Elimina lógicamente un docente postulante
        /// </summary>
        bool Delete(int id, string usuario);

        /// <summary>
        /// Agrega múltiples docentes postulantes
        /// </summary>
        IEnumerable<TDocentePostulante> Add(IEnumerable<DocentePostulante> listadoEntidad);

        /// <summary>
        /// Actualiza múltiples docentes postulantes
        /// </summary>
        IEnumerable<TDocentePostulante> Update(IEnumerable<DocentePostulante> listadoEntidad);

        /// <summary>
        /// Elimina lógicamente múltiples docentes postulantes
        /// </summary>
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        /// <summary>
        /// Obtiene todos los docentes postulantes activos con información adicional
        /// </summary>
        List<DocentePostulanteDTO> ObtenerDocentePostulante();

        /// <summary>
        /// Obtiene un docente postulante por su ID
        /// </summary>
        DocentePostulante ObtenerPorId(int id);

        /// <summary>
        /// Obtiene múltiples docentes postulantes por sus IDs
        /// </summary>
        List<DocentePostulante> ObtenerPorIds(List<int> ids);
    }
}
