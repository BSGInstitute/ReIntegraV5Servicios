using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    /// Interface: IGooglePlacesConfiguracionRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del repositorio de sedes de Google Places.
    /// CRUD básico para gestionar la configuración de las sedes (nombre, identificador, valoración).
    /// </summary>
    public interface IGooglePlacesConfiguracionRepository : IGenericRepository<TGooglePlacesConfiguracion>
    {
        /// <summary>Inserta una configuración de sede y retorna el modelo persistido.</summary>
        TGooglePlacesConfiguracion Add(GooglePlacesConfiguracion entidad);
        /// <summary>Actualiza una configuración de sede con control de concurrencia.</summary>
        TGooglePlacesConfiguracion Update(GooglePlacesConfiguracion entidad);
        /// <summary>Elimina lógicamente una configuración de sede.</summary>
        bool Delete(int id, string usuario);
    }
}
