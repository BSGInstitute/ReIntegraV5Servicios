using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// Interface: IGooglePlacesConfiguracionService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del servicio de dominio para la gestión de sedes de Google Places.
    /// CRUD de la configuración (nombre, identificador, valoración, total reseñas).
    /// </summary>
    public interface IGooglePlacesConfiguracionService
    {
        /// <summary>Obtiene todas las configuraciones activas de sedes de Google Places.</summary>
        List<GooglePlacesConfiguracion> ObtenerTodos();
        /// <summary>Obtiene las sedes para el combo de selección del frontend.</summary>
        List<GooglePlacesConfiguracionComboDTO> ObtenerCombo();
        /// <summary>Inserta una configuración de sede y persiste los cambios.</summary>
        GooglePlacesConfiguracion Add(GooglePlacesConfiguracion entidad);
        /// <summary>Actualiza una configuración de sede y persiste los cambios.</summary>
        GooglePlacesConfiguracion Update(GooglePlacesConfiguracion entidad);
        /// <summary>Elimina lógicamente una configuración de sede por su Id.</summary>
        bool Delete(int id, string usuario);
    }
}
