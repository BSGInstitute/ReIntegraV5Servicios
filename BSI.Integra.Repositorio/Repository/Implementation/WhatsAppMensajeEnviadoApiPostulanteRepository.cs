using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Interface;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppMensajeEnviadoApiPostulanteRepository
    /// Autor: Claude (sesion sdd-apply gp-whatsapp-endpoints)
    /// Fecha: 2026-05-13
    /// Version: 1.0
    /// <summary>
    /// Acceso a datos del chat WhatsApp GP (postulantes). Usa Dapper via IDapperRepository
    /// porque los SPs viven en esquema gp y no tienen modelo EF directo. El patron es identico
    /// a la mayoria de repos del repo (1200+ usos): SP devuelve JSON via QuerySPDapperAsync y
    /// se deserializa con Newtonsoft.Json.
    /// </summary>
    public class WhatsAppMensajeEnviadoApiPostulanteRepository : IWhatsAppMensajeEnviadoApiPostulanteRepository
    {
        private const string SpPendientes      = "gp.SP_WhatsAppObtenerMensajesPendientesPostulante";
        private const string SpConversaciones  = "gp.SP_WhatsAppObtenerConversacionesPostulante";
        private const string SpHistorial       = "gp.SP_WhatsAppObtenerHistorialChatPostulante";
        private const string SpValidarVentana  = "gp.SP_WhatsAppValidarMensajesRecibidosEn24HorasPostulante";

        private readonly IDapperRepository _dapperRepository;

        public WhatsAppMensajeEnviadoApiPostulanteRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        /// <summary>FR-1 / FR-5: pendientes del asesor logueado.</summary>
        public async Task<IEnumerable<PendienteWhatsAppPostulanteDTO>> ObtenerPendientesAsync(int idPersonal)
        {
            var json = await _dapperRepository.QuerySPDapperAsync(SpPendientes, new { IdPersonal = idPersonal });
            var lista = JsonConvert.DeserializeObject<List<PendienteWhatsAppPostulanteDTO>>(json);
            return lista ?? new List<PendienteWhatsAppPostulanteDTO>();
        }

        /// <summary>FR-2: ultimo mensaje por postulante asignado al asesor.</summary>
        public async Task<IEnumerable<ConversacionWhatsAppPostulanteDTO>> ObtenerConversacionesAsync(int idPersonal)
        {
            var json = await _dapperRepository.QuerySPDapperAsync(SpConversaciones, new { IdPersonal = idPersonal });
            var lista = JsonConvert.DeserializeObject<List<ConversacionWhatsAppPostulanteDTO>>(json);
            return lista ?? new List<ConversacionWhatsAppPostulanteDTO>();
        }

        /// <summary>
        /// FR-3: arma el HistorialChatPostulanteDTO agrupando filas planas del SP.
        /// La primera fila define IdPostulante / NombreCompleto / WaNumero;
        /// las demas se mapean al List Mensajes.
        ///
        /// Hotfix 404: distingue dos casos cuando el SP no devuelve filas:
        ///   - Postulante existe en gp.T_Postulante (Estado=1) pero sin mensajes
        ///       => devuelve DTO con Mensajes vacio (el service responde 200).
        ///   - Postulante NO existe en gp.T_Postulante (Id invalido o Estado=0)
        ///       => devuelve null (el service mapea a NotFoundException => 404).
        ///
        /// Devuelve null SI Y SOLO SI el postulante no existe en gp.T_Postulante con Estado=1.
        /// </summary>
        public async Task<HistorialChatPostulanteDTO?> ObtenerHistorialAsync(int idPostulante, int? idPais)
        {
            var json = await _dapperRepository.QuerySPDapperAsync(SpHistorial, new { IdPostulante = idPostulante, IdPais = idPais });
            var filas = JsonConvert.DeserializeObject<List<HistorialFilaPlana>>(json);

            if (filas is null || filas.Count == 0)
            {
                // El SP no devolvio filas. Verificar si el postulante existe para distinguir
                // "sin mensajes" (200 con DTO vacio) vs "postulante inexistente" (404).
                var postulante = await VerificarExistenciaPostulanteAsync(idPostulante);
                if (postulante is null)
                {
                    return null;
                }

                return new HistorialChatPostulanteDTO
                {
                    IdPostulante   = postulante.Id,
                    NombreCompleto = postulante.NombreCompleto,
                    WaNumero       = string.Empty,
                    Mensajes       = new List<MensajeChatPostulanteDTO>()
                };
            }

            var cabecera = filas[0];
            return new HistorialChatPostulanteDTO
            {
                IdPostulante   = cabecera.IdPostulante,
                NombreCompleto = cabecera.NombreCompleto,
                WaNumero       = cabecera.WaNumero ?? string.Empty,
                Mensajes       = filas.Select(MapearAMensaje).ToList()
            };
        }

        /// <summary>
        /// Verifica si el postulante existe en gp.T_Postulante con Estado=1 y devuelve
        /// su Id + NombreCompleto. Devuelve null si no existe o esta deshabilitado.
        ///
        /// Usado por ObtenerHistorialAsync para distinguir 200 (existe, sin mensajes) vs 404
        /// (no existe). Patron Dapper inline + JSON deserialize, igual que el resto del repo.
        /// </summary>
        private async Task<PostulanteSimpleDTO?> VerificarExistenciaPostulanteAsync(int idPostulante)
        {
            const string query = @"
                SELECT
                    Id,
                    LTRIM(RTRIM(CONCAT(Nombre, ' ', ApellidoPaterno, ' ', ISNULL(ApellidoMaterno, '')))) AS NombreCompleto
                FROM gp.T_Postulante
                WHERE Id = @IdPostulante AND Estado = 1";

            var json = await _dapperRepository.QueryDapperAsync(query, new { IdPostulante = idPostulante });
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            var lista = JsonConvert.DeserializeObject<List<PostulanteSimpleDTO>>(json);
            return lista?.FirstOrDefault();
        }

        /// <summary>
        /// DTO interno del repo para verificacion de existencia.
        /// NO expuesto en la capa publica de DTOs (HistorialChatPostulanteDTO ya lleva
        /// IdPostulante y NombreCompleto en su propia shape).
        /// </summary>
        private sealed class PostulanteSimpleDTO
        {
            public int    Id             { get; set; }
            public string NombreCompleto { get; set; } = string.Empty;
        }

        /// <summary>
        /// FR-9: ventana Meta 24h para texto libre. Espejo de
        /// `WhatsAppMensajeEnviadoRepository.ValidarMesajesEnviadosEn24Horas` (ATC):
        /// devuelve TRUE si NO hubo mensaje recibido del postulante en las ultimas 24h
        /// (=> requiere plantilla), FALSE si si lo hubo (=> texto libre permitido).
        ///
        /// Fail-safe: si el SP no devuelve filas (JSON vacio / null), retorna TRUE
        /// para forzar plantilla — paridad con ATC, que en la misma condicion devolvia true.
        /// </summary>
        public async Task<bool> ValidarVentana24HorasAsync(string numero)
        {
            var json = await _dapperRepository.QuerySPDapperAsync(SpValidarVentana, new { WaFrom = numero });
            var filas = JsonConvert.DeserializeObject<List<VentanaResultado>>(json);

            if (filas is null || filas.Count == 0)
            {
                return true;
            }

            return filas[0].RequierePlantilla;
        }

        /// <summary>
        /// Forma plana del retorno del SP gp.SP_ValidarMensajesRecibidosEn24HorasPostulante:
        /// una unica fila con un solo campo bool RequierePlantilla.
        /// Privada — solo para deserializacion JSON.
        /// </summary>
        private sealed class VentanaResultado
        {
            public bool RequierePlantilla { get; set; }
        }

        private static MensajeChatPostulanteDTO MapearAMensaje(HistorialFilaPlana fila)
        {
            return new MensajeChatPostulanteDTO
            {
                Id             = fila.IdWhatsAppMensajeERPostulante,
                Tipo           = fila.Tipo,
                WaType         = fila.WaType,
                WaBody         = fila.WaBody,
                WaFile         = fila.WaFile,
                WaCaption      = fila.WaCaption,
                WaFileName     = fila.WaFileName,
                WaMimeType     = fila.WaMimeType,
                FechaCreacion  = fila.FechaCreacion,
                IdPersonal     = fila.IdPersonal,
                NombrePersonal = fila.NombrePersonal,
                IdPostulante   = fila.IdPostulante,
                WaFrom         = fila.WaFrom,
                WaTo           = fila.WaTo,
                IdPais         = fila.IdPais,
                WaId           = fila.WaId,
                WaStatus = fila.WaStatus,
            };
        }

        /// <summary>
        /// Forma plana que devuelve gp.SP_ObtenerHistorialChatPostulante:
        /// columnas crudas + cabecera repetida (NombreCompleto, WaNumero).
        /// Privada — solo para deserializacion JSON.
        /// </summary>
        private sealed class HistorialFilaPlana
        {
            public int      IdWhatsAppMensajeERPostulante            { get; set; }
            public int      Tipo           { get; set; }
            public string?  WaType         { get; set; }
            public string?  WaBody         { get; set; }
            public string?  WaFile         { get; set; }
            public string?  WaCaption      { get; set; }
            public string?  WaFileName     { get; set; }
            public string?  WaMimeType     { get; set; }
            public DateTime FechaCreacion  { get; set; }
            public int?     IdPersonal     { get; set; }
            public string?  NombrePersonal { get; set; }
            public int?     IdPostulante   { get; set; }
            public string?  WaFrom         { get; set; }
            public string?  WaTo           { get; set; }
            public int      IdPais         { get; set; }
            public string?  WaId           { get; set; }
            public string?  NombreCompleto { get; set; }
            public string?  WaNumero       { get; set; }
            public string? WaStatus { get; set; }
        }
    }
}
