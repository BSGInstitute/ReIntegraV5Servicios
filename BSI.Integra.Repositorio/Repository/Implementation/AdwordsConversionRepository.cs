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
                        c.IdSubcuentaGoogle,
                        s.CustomerId,
                        s.NombreSubcuenta,
                        CASE c.TipoConversion
                            WHEN 'Conversion IT' THEN s.ConversionActionIdIT
                            WHEN 'Conversion IP, PF' THEN s.ConversionActionIdIPPF
                            WHEN 'Conversion IC, IS y M' THEN s.ConversionActionIdICISM
                        END AS ConversionActionId
                    FROM mkt.T_GoogleAdsConversionQueue c
                    LEFT JOIN mkt.T_GoogleAdsSubcuenta s ON c.IdSubcuentaGoogle = s.Id AND s.Estado = 1 AND s.Activo = 1
                    WHERE c.EstadoEnvio = 'Pendiente'
                        AND c.EsValidoParaEnvio = 1
                        AND c.IntentosEnvio < 3
                        AND c.Estado = 1
                        AND c.FechaHoraConversion >= DATEADD(DAY, -90, GETDATE())
                    ORDER BY c.FechaCreacion ASC
                ", conn);

                cmd.Parameters.AddWithValue("@Limite", limite);

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
                            IdSubcuentaGoogle = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9),
                            CustomerId = reader.IsDBNull(10) ? null : reader.GetString(10),
                            NombreSubcuenta = reader.IsDBNull(11) ? null : reader.GetString(11),
                            ConversionActionId = reader.IsDBNull(12) ? string.Empty : reader.GetString(12)
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

        public async Task<GoogleAdsSubcuentaDTO?> ObtenerSubcuentaPorCustomerId(string customerId)
        {
            using (var conn = (SqlConnection)_connectionFactory.GetConnection)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var cmd = new SqlCommand(@"
                    SELECT Id, CustomerId, NombreSubcuenta, ConversionActionIdIT, ConversionActionIdIPPF, ConversionActionIdICISM, Activo
                    FROM mkt.T_GoogleAdsSubcuenta
                    WHERE CustomerId = @CustomerId AND Estado = 1 AND Activo = 1
                ", conn);

                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new GoogleAdsSubcuentaDTO
                        {
                            Id = reader.GetInt32(0),
                            CustomerId = reader.GetString(1),
                            NombreSubcuenta = reader.GetString(2),
                            ConversionActionIdIT = reader.IsDBNull(3) ? null : reader.GetString(3),
                            ConversionActionIdIPPF = reader.IsDBNull(4) ? null : reader.GetString(4),
                            ConversionActionIdICISM = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Activo = reader.GetBoolean(6)
                        };
                    }
                }
            }
            return null;
        }

        public async Task<GoogleAdsSubcuentaDTO?> ObtenerSubcuentaPorId(int id)
        {
            using (var conn = (SqlConnection)_connectionFactory.GetConnection)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var cmd = new SqlCommand(@"
                    SELECT Id, CustomerId, NombreSubcuenta, ConversionActionIdIT, ConversionActionIdIPPF, ConversionActionIdICISM, Activo
                    FROM mkt.T_GoogleAdsSubcuenta
                    WHERE Id = @Id AND Estado = 1 AND Activo = 1
                ", conn);

                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new GoogleAdsSubcuentaDTO
                        {
                            Id = reader.GetInt32(0),
                            CustomerId = reader.GetString(1),
                            NombreSubcuenta = reader.GetString(2),
                            ConversionActionIdIT = reader.IsDBNull(3) ? null : reader.GetString(3),
                            ConversionActionIdIPPF = reader.IsDBNull(4) ? null : reader.GetString(4),
                            ConversionActionIdICISM = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Activo = reader.GetBoolean(6)
                        };
                    }
                }
            }
            return null;
        }

        public async Task<List<GoogleAdsSubcuentaDTO>> ObtenerSubcuentasActivas()
        {
            var subcuentas = new List<GoogleAdsSubcuentaDTO>();

            using (var conn = (SqlConnection)_connectionFactory.GetConnection)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var cmd = new SqlCommand(@"
                    SELECT Id, CustomerId, NombreSubcuenta, ConversionActionIdIT, ConversionActionIdIPPF, ConversionActionIdICISM, Activo
                    FROM mkt.T_GoogleAdsSubcuenta
                    WHERE Estado = 1 AND Activo = 1
                    ORDER BY NombreSubcuenta
                ", conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        subcuentas.Add(new GoogleAdsSubcuentaDTO
                        {
                            Id = reader.GetInt32(0),
                            CustomerId = reader.GetString(1),
                            NombreSubcuenta = reader.GetString(2),
                            ConversionActionIdIT = reader.IsDBNull(3) ? null : reader.GetString(3),
                            ConversionActionIdIPPF = reader.IsDBNull(4) ? null : reader.GetString(4),
                            ConversionActionIdICISM = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Activo = reader.GetBoolean(6)
                        });
                    }
                }
            }
            return subcuentas;
        }

        public async Task<List<GoogleFormularioLeadgenDTO>> ObtenerLeadsSinSubcuentaAsignada(int limite)
        {
            var leads = new List<GoogleFormularioLeadgenDTO>();

            using (var conn = (SqlConnection)_connectionFactory.GetConnection)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var cmd = new SqlCommand(@"
                    SELECT TOP (@Limite) Id, CampaniaGoogle, FormularioGoogle, Gcl
                    FROM mkt.T_GoogleFormularioLeadgen
                    WHERE IdSubcuentaGoogle IS NULL
                        AND CampaniaGoogle IS NOT NULL
                        AND CampaniaGoogle <> ''
                        AND Estado = 1
                        AND FechaCreacion >= DATEADD(DAY, -90, GETDATE())
                    ORDER BY FechaCreacion ASC
                ", conn);

                cmd.Parameters.AddWithValue("@Limite", limite);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        leads.Add(new GoogleFormularioLeadgenDTO
                        {
                            Id = reader.GetInt32(0),
                            CampaniaGoogle = reader.GetString(1),
                            FormularioGoogle = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Gclid = reader.IsDBNull(3) ? null : reader.GetString(3)
                        });
                    }
                }
            }
            return leads;
        }

        public async Task ActualizarSubcuentaLead(int id, int idSubcuentaGoogle)
        {
            using (var conn = (SqlConnection)_connectionFactory.GetConnection)
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var cmd = new SqlCommand(@"
                    UPDATE mkt.T_GoogleFormularioLeadgen
                    SET IdSubcuentaGoogle = @IdSubcuentaGoogle,
                        FechaEnriquecimiento = GETDATE(),
                        UsuarioModificacion = 'GoogleAdsAPI',
                        FechaModificacion = GETDATE()
                    WHERE Id = @Id
                ", conn);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@IdSubcuentaGoogle", idSubcuentaGoogle);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
