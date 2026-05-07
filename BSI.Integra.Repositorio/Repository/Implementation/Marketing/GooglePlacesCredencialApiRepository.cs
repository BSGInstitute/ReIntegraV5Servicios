using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    /// Repositorio: GooglePlacesCredencialApiRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Acceso a datos de la tabla mkt.T_GooglePlacesCredencialApi.
    /// Almacena las credenciales (API Key) para consumir la Google Places API.
    /// </summary>
    public class GooglePlacesCredencialApiRepository : GenericRepository<TGooglePlacesCredencialApi>, IGooglePlacesCredencialApiRepository
    {
        public GooglePlacesCredencialApiRepository(
            IntegraDBContext context,
            IConnectionFactory connectionFactory,
            IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
        }
    }
}
