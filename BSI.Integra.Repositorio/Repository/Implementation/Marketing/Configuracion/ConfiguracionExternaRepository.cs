using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Configuracion;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Configuracion
{
    /// Autor: Miguel Valdivia
    /// Fecha: 18/03/2026
    /// Version: 1.0
    /// <summary>
    /// Consultas a BD para construir el JSON de interaccion del asistente WhatsApp.
    /// </summary>
    public class ConfiguracionExternaRepository : IConfiguracionExternaRepository
    {
        private readonly string _connectionString;

        public ConfiguracionExternaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("IntegraDB");
        }

        public async Task<InteraccionPatchDTO> ObtenerEsquemaInteraccionAsync(int idChatbotEsquema)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            // 1. Esquema maestro
            var esquema = await conn.QueryFirstOrDefaultAsync<dynamic>(
                @"SELECT Id, Nombre, Restricciones
                  FROM [mkt].[T_ChatbotEsquema]
                  WHERE Id = @Id AND Estado = 1",
                new { Id = idChatbotEsquema });

            if (esquema == null)
                throw new Exception($"No se encontro el esquema con Id {idChatbotEsquema}.");

            // 2. Lecturas de mensaje
            var lecturas = (await conn.QueryAsync<dynamic>(
                @"SELECT Id AS IdLectura,
                         ClasificacionTipoMensaje,
                         PromptLectura
                  FROM [mkt].[T_ChatbotEsquemaLecturaMensaje]
                  WHERE IdChatbotEsquema = @Id AND Estado = 1
                  ORDER BY Orden",
                new { Id = idChatbotEsquema })).ToList();

            // 3. Mensajes exactos
            var mensajesExactos = (await conn.QueryAsync<dynamic>(
                @"SELECT CELME.IdChatbotEsquemaLecturaMensaje AS IdLectura,
                         AME.Id                              AS IdMensajeExacto,
                         AME.Nombre                          AS Texto
                  FROM [mkt].[T_ChatbotEsquemaLecturaMensajeExacto] CELME
                  INNER JOIN [mkt].[T_ChatbotEsquemaLecturaMensaje]          CLM ON CLM.Id = CELME.IdChatbotEsquemaLecturaMensaje
                  INNER JOIN [mkt].[T_ChatbotEsquemaAsignacionMensajeExacto] AME ON AME.Id = CELME.IdChatbotEsquemaAsignacionMensajeExacto
                  WHERE CLM.IdChatbotEsquema = @Id AND CLM.Estado = 1 AND AME.Estado = 1",
                new { Id = idChatbotEsquema })).ToList();

            // 4. Subcategorias (perfiles)
            var subcategorias = (await conn.QueryAsync<dynamic>(
                @"SELECT SC.Id AS IdSubcategoria,
                         SC.Nombre
                  FROM [mkt].[T_ChatbotEsquemaSubcategoria] SC
                  INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] CII ON CII.Id = SC.IdChatbotEsquemaInterpretarInformacion
                  WHERE CII.IdChatbotEsquema = @Id AND CII.Estado = 1 AND SC.Estado = 1
                  ORDER BY SC.Orden",
                new { Id = idChatbotEsquema })).ToList();

            // 5. Fases por subcategoria
            var fases = (await conn.QueryAsync<dynamic>(
                @"SELECT CSF.IdChatbotEsquemaSubcategoria AS IdSubcategoria,
                         AF.Nombre                       AS NombreFase
                  FROM [mkt].[T_ChatbotEsquemaSubcategoriaFase] CSF
                  INNER JOIN [mkt].[T_ChatbotEsquemaSubcategoria]            SC  ON SC.Id  = CSF.IdChatbotEsquemaSubcategoria
                  INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion]  CII ON CII.Id = SC.IdChatbotEsquemaInterpretarInformacion
                  INNER JOIN [mkt].[T_ChatbotEsquemaAsignacionFase]          AF  ON AF.Id  = CSF.IdChatbotEsquemaAsignacionFase
                  WHERE CII.IdChatbotEsquema = @Id AND CII.Estado = 1 AND SC.Estado = 1",
                new { Id = idChatbotEsquema })).ToList();

            // 6. Respuestas
            var respuestas = (await conn.QueryAsync<dynamic>(
                @"SELECT Id                             AS IdRespuesta,
                         IdChatbotEsquemaLecturaMensaje AS IdEtiqueta,
                         ParametrosRespuesta
                  FROM [mkt].[T_ChatbotEsquemaRespuesta]
                  WHERE IdChatbotEsquema = @Id AND Estado = 1
                  ORDER BY Orden",
                new { Id = idChatbotEsquema })).ToList();

            // ── Construir DTO ──────────────────────────────────────────────────

            return new InteraccionPatchDTO
            {
                Esquemas = new List<EsquemaPatchDTO>
                {
                    new EsquemaPatchDTO
                    {
                        IdEsquema     = (int)esquema.Id,
                        NombreEsquema = (string)esquema.Nombre,
                        Restricciones = (string)(esquema.Restricciones ?? ""),

                        Mensajes = lecturas.Select(l => new MensajePatchDTO
                        {
                            IdEtiqueta           = (int)l.IdLectura,
                            NombreEtiqueta       = (string)l.ClasificacionTipoMensaje,
                            InterpretacionPrompt = (string)(l.PromptLectura ?? ""),
                            MensajeExacto = mensajesExactos
                                .Where(m => (int)m.IdLectura == (int)l.IdLectura)
                                .Select(m => new MensajeExactoPatchDTO
                                {
                                    Id    = (int)m.IdMensajeExacto,
                                    Texto = (string)m.Texto
                                }).ToList()
                        }).ToList(),

                        Perfiles = subcategorias.Select(s => new PerfilPatchDTO
                        {
                            IdPerfil     = (int)s.IdSubcategoria,
                            NombrePerfil = (string)s.Nombre,
                            Fases = fases
                                .Where(f => (int)f.IdSubcategoria == (int)s.IdSubcategoria)
                                .Select(f => (string)f.NombreFase)
                                .ToList()
                        }).ToList(),

                        Respuestas = respuestas.Select(r => new RespuestaPatchDTO
                        {
                            IdRespuesta           = (int)r.IdRespuesta,
                            IdEtiqueta            = (int)r.IdEtiqueta,
                            InstruccionesEtiqueta = (string)(r.ParametrosRespuesta ?? "")
                        }).ToList()
                    }
                }
            };
        }
    }
}
