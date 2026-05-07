using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    /// Interface: IGooglePlacesCredencialApiRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del repositorio de credenciales de Google Places API.
    /// Almacena las API Keys para consumir la Google Places API.
    /// </summary>
    public interface IGooglePlacesCredencialApiRepository : IGenericRepository<TGooglePlacesCredencialApi>
    {
    }
}
