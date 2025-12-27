using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    /// <summary>
    /// Interfaz del servicio para el manejo de fases de gestión de contactos
    /// </summary>
    public interface IFaseGestionContactoService
    {
        /// <summary>
        /// Obtiene todas las fases de gestión de contacto activas
        /// </summary>
        List<FaseGestionContactoDTO> Obtener();

        /// <summary>
        /// Obtiene una fase de gestión de contacto por su ID
        /// </summary>
        FaseGestionContactoDTO ObtenerPorId(int id);

        /// <summary>
        /// Inserta una nueva fase de gestión de contacto
        /// </summary>
        FaseGestionContactoDTO Insertar(FaseGestionContactoDTO dto, string usuario);

        /// <summary>
        /// Actualiza una fase de gestión de contacto existente
        /// </summary>
        FaseGestionContactoDTO Actualizar(FaseGestionContactoDTO dto, string usuario);

        /// <summary>
        /// Elimina lógicamente una fase de gestión de contacto
        /// </summary>
        bool Eliminar(int id, string usuario);
    }
}
