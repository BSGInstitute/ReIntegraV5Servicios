using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    /// <summary>
    /// Interfaz del repositorio para el manejo de fases de gestión de contactos
    /// </summary>
    public interface IFaseGestionContactoRepository : IGenericRepository<TFaseGestionContacto>
    {
        #region Metodos Base
        /// <summary>
        /// Agrega una nueva fase de gestión de contacto
        /// </summary>
        TFaseGestionContacto Add(FaseGestionContacto entidad);

        /// <summary>
        /// Actualiza una fase de gestión de contacto existente
        /// </summary>
        TFaseGestionContacto Update(FaseGestionContacto entidad);

        /// <summary>
        /// Elimina lógicamente una fase de gestión de contacto
        /// </summary>
        bool Delete(int id, string usuario);

        /// <summary>
        /// Agrega múltiples fases de gestión de contacto
        /// </summary>
        IEnumerable<TFaseGestionContacto> Add(IEnumerable<FaseGestionContacto> listadoEntidad);

        /// <summary>
        /// Actualiza múltiples fases de gestión de contacto
        /// </summary>
        IEnumerable<TFaseGestionContacto> Update(IEnumerable<FaseGestionContacto> listadoEntidad);

        /// <summary>
        /// Elimina lógicamente múltiples fases de gestión de contacto
        /// </summary>
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        /// <summary>
        /// Obtiene todas las fases de gestión de contacto activas
        /// </summary>
        List<FaseGestionContactoDTO> ObtenerFaseGestionContacto();

        /// <summary>
        /// Obtiene una fase de gestión de contacto por su ID
        /// </summary>
        FaseGestionContacto ObtenerPorId(int id);

        /// <summary>
        /// Obtiene múltiples fases de gestión de contacto por sus IDs
        /// </summary>
        List<FaseGestionContacto> ObtenerPorIds(List<int> ids);
    }
}
