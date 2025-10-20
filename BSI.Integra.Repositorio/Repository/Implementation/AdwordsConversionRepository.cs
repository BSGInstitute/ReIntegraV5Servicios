using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// <summary>
    /// Repositorio: AdwordsConversionRepository
    /// Autor: Miguel Valdivia
    /// Fecha: 2025-10-04
    /// Descripción: Gestión de conversiones offline de Google Ads
    /// </summary>
    public class AdwordsConversionRepository : IAdwordsConversionRepository
    {
        private readonly IntegraDBContext _context;
        private readonly IConnectionFactory _connectionFactory;

        public AdwordsConversionRepository(IntegraDBContext context, IConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public async Task<AdwordsCredencialesDTO?> ObtenerCredenciales()
        {
            using (var conn = (SqlConnection)_connectionFactory.GetConnection)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var cmd = new SqlCommand(@"
                    SELECT TOP 1
                        DeveloperToken,
                        ClientCustomerId,
                        Oauth2ClientId,
                        Oauth2ClientSecret,
                        Oauth2RefreshToken,
                        ConversionActionIdIT,
                        ConversionActionIdIPPF,
                        ConversionActionIdICISM,
                        ProcesoConversionesActivo,
                        ISNULL(ApiVersion, 'v20') AS ApiVersion, 
                        ManagerAccountId
                    FROM mkt.T_AdworkCredencialApi
                    WHERE Estado = 1
                    ORDER BY FechaCreacion DESC
                ", conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new AdwordsCredencialesDTO
                        {
                            DeveloperToken = reader.GetString(0),
                            CustomerId = reader.GetString(1),
                            ClientId = reader.GetString(2),
                            ClientSecret = reader.GetString(3),
                            RefreshToken = reader.GetString(4),
                            ConversionActionIdIT = reader.IsDBNull(5) ? null : reader.GetString(5),
                            ConversionActionIdIPPF = reader.IsDBNull(6) ? null : reader.GetString(6),
                            ConversionActionIdICISM = reader.IsDBNull(7) ? null : reader.GetString(7),
                            ProcesoActivo = reader.GetBoolean(8),
                            ApiVersion = reader.GetString(9),
                            ManagerAccountId = reader.IsDBNull(10) ? null : reader.GetString(10)
                        };
                    }
                }
            }

            return null;
        }

        public async Task<List<ConversionQueueDTO>> ObtenerConversionesPendientes(int limite)
        {
            var conversiones = new List<ConversionQueueDTO>();

            // Primero obtener credenciales para los ConversionActionIds
            var credenciales = await ObtenerCredenciales();
            if (credenciales == null) return conversiones;

            using (var conn = (SqlConnection)_connectionFactory.GetConnection)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var cmd = new SqlCommand(@"
                    SELECT TOP (@Limite)
                        c.Id,
                        c.IdOportunidad,
                        c.Gclid,
                        c.TipoConversion,
                        c.EmailHasheado,
                        c.CelularHasheado,
                        c.FechaHoraConversion,
                        c.FechaHoraConversionFormatoGoogle,
                        c.ValorConversion,
                        CASE c.TipoConversion
                            WHEN 'Conversion IT' THEN @ActionIdIT
                            WHEN 'Conversion IP, PF' THEN @ActionIdIPPF
                            WHEN 'Conversion IC, IS y M' THEN @ActionIdICISM
                        END AS ConversionActionId
                    FROM mkt.T_GoogleAdsConversionQueue c
                    WHERE c.EstadoEnvio = 'Pendiente'
                        AND c.EsValidoParaEnvio = 1
                        AND c.IntentosEnvio < 3
                        AND c.Estado = 1
                    ORDER BY c.FechaCreacion ASC
                ", conn);

                cmd.Parameters.AddWithValue("@Limite", limite);
                cmd.Parameters.AddWithValue("@ActionIdIT", credenciales.ConversionActionIdIT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ActionIdIPPF", credenciales.ConversionActionIdIPPF ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ActionIdICISM", credenciales.ConversionActionIdICISM ?? (object)DBNull.Value);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        conversiones.Add(new ConversionQueueDTO
                        {
                            Id = reader.GetInt32(0),
                            IdOportunidad = reader.GetInt32(1),
                            Gclid = reader.GetString(2),
                            TipoConversion = reader.GetString(3),
                            EmailHasheado = reader.IsDBNull(4) ? null : reader.GetString(4),
                            CelularHasheado = reader.IsDBNull(5) ? null : reader.GetString(5),
                            FechaHoraConversion = reader.GetDateTime(6),
                            FechaHoraConversionFormatoGoogle = reader.GetString(7),
                            ValorConversion = reader.IsDBNull(8) ? (decimal?)null : reader.GetDecimal(8),
                            ConversionActionId = reader.IsDBNull(9) ? string.Empty : reader.GetString(9)
                        });
                    }
                }
            }

            return conversiones;
        }

        public async Task ActualizarEstadoConversion(int id, string estado, string? respuesta, string? error)
        {
            using (var conn = (SqlConnection)_connectionFactory.GetConnection)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var cmd = new SqlCommand(@"
                    UPDATE mkt.T_GoogleAdsConversionQueue
                    SET EstadoEnvio = @Estado,
                        IntentosEnvio = IntentosEnvio + 1,
                        FechaEnvio = GETDATE(),
                        RespuestaGoogleAds = @Respuesta,
                        MensajeError = @Error,
                        FechaModificacion = GETDATE(),
                        UsuarioModificacion = 'AdwordsService'
                    WHERE Id = @Id
                ", conn);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Estado", estado);
                cmd.Parameters.AddWithValue("@Respuesta", respuesta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Error", error ?? (object)DBNull.Value);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task RegistrarLog(string mensaje, bool esExito)
        {
            using (var conn = (SqlConnection)_connectionFactory.GetConnection)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var cmd = new SqlCommand(@"
                    INSERT INTO mkt.T_AdwordsLog
                        (Mensaje, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion)
                    VALUES
                        (@Mensaje, @Estado, 'AdwordsService', 'AdwordsService', GETDATE(), GETDATE())
                ", conn);

                cmd.Parameters.AddWithValue("@Mensaje", mensaje.Length > 8000 ? mensaje.Substring(0, 8000) : mensaje);
                cmd.Parameters.AddWithValue("@Estado", esExito ? 1 : 0);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<ConversionEstadoDTO>> ObtenerEstadoConversiones()
        {
            var estados = new List<ConversionEstadoDTO>();

            using (var conn = (SqlConnection)_connectionFactory.GetConnection)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var cmd = new SqlCommand(@"
                    SELECT
                        EstadoEnvio,
                        COUNT(*) AS Cantidad,
                        MIN(FechaCreacion) AS MasAntigua,
                        MAX(FechaCreacion) AS MasReciente
                    FROM mkt.T_GoogleAdsConversionQueue
                    WHERE Estado = 1
                    GROUP BY EstadoEnvio
                ", conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        estados.Add(new ConversionEstadoDTO
                        {
                            Estado = reader.GetString(0),
                            Cantidad = reader.GetInt32(1),
                            MasAntigua = reader.GetDateTime(2),
                            MasReciente = reader.GetDateTime(3)
                        });
                    }
                }
            }

            return estados;
        }
    }
}
