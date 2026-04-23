using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.EsquemaRespuestas;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.EsquemaRespuestas
{
    public class EsquemaRespuestasRepository : IEsquemaRespuestasRepository
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly string _connectionString;

        public EsquemaRespuestasRepository(IDapperRepository dapperRepository, IConfiguration configuration)
        {
            _dapperRepository = dapperRepository;
            _connectionString = configuration.GetConnectionString("IntegraDB");
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna el detalle completo de un esquema por su Id, incluyendo lecturas, mensajes exactos,
        /// bloques de interpretacion, subcategorias, fases, perfiles y respuestas.
        /// </summary>
        public async Task<EsquemaDetalleResponseDTO> ObtenerEsquemaPorIdAsync(int idChatbotEsquema)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var esquema = await conn.QueryFirstOrDefaultAsync<dynamic>(
                @"SELECT Id, Nombre, Restricciones
                  FROM [mkt].[T_ChatbotEsquema]
                  WHERE Id = @Id AND Estado = 1",
                new { Id = idChatbotEsquema });

            if (esquema == null) return null;

            var lecturas = (await conn.QueryAsync<dynamic>(
                @"SELECT
          CLM.Id                       AS IdChatbotEsquemaLecturaMensaje,
          CLM.ClasificacionTipoMensaje AS ClasificacionTipoMensaje,
          CLM.PromptLectura            AS PromptLectura,
          CLM.Orden                    AS Orden
      FROM [mkt].[T_ChatbotEsquemaLecturaMensaje] CLM
      WHERE CLM.IdChatbotEsquema = @IdChatbotEsquema
        AND CLM.Estado           = 1",
                new { Id = idChatbotEsquema })).ToList();

            var mensajes = (await conn.QueryAsync<dynamic>(
                @"SELECT
          CELME.IdChatbotEsquemaLecturaMensaje          AS IdChatbotEsquemaLecturaMensaje,
          CEAME.Id                                       AS IdChatbotEsquemaAsignacionMensajeExacto,
          CEAME.Nombre                                   AS NombreMensajeExacto
      FROM [mkt].[T_ChatbotEsquemaLecturaMensajeExacto]          CELME
      INNER JOIN [mkt].[T_ChatbotEsquemaLecturaMensaje]           CLM
             ON  CLM.Id  = CELME.IdChatbotEsquemaLecturaMensaje
      INNER JOIN [mkt].[T_ChatbotEsquemaAsignacionMensajeExacto]  CEAME
             ON  CEAME.Id = CELME.IdChatbotEsquemaAsignacionMensajeExacto
      WHERE CLM.IdChatbotEsquema = @IdChatbotEsquema
        AND CLM.Estado           = 1
        AND CEAME.Estado         = 1",
                new { Id = idChatbotEsquema })).ToList();

            var interpretarList = (await conn.QueryAsync<dynamic>(
                @"SELECT Id AS IdChatbotEsquemaInterpretarInformacion, Nombre
                  FROM [mkt].[T_ChatbotEsquemaInterpretarInformacion]
                  WHERE IdChatbotEsquema = @Id AND Estado = 1
                  ORDER BY Orden",
                new { Id = idChatbotEsquema })).ToList();

            var clasificaciones = (await conn.QueryAsync<dynamic>(
                @"SELECT c.IdChatbotEsquemaInterpretarInformacion,
                         l.ClasificacionTipoMensaje AS Clasificacion
                  FROM [mkt].[T_ChatbotEsquemaInterpretarInformacionClasificacion] c
                  INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] ii
                         ON ii.Id = c.IdChatbotEsquemaInterpretarInformacion
                  INNER JOIN [mkt].[T_ChatbotEsquemaLecturaMensaje] l
                         ON l.Id = c.IdChatbotEsquemaLecturaMensaje
                  WHERE ii.IdChatbotEsquema = @Id AND ii.Estado = 1",
                new { Id = idChatbotEsquema })).ToList();

            var subcategorias = (await conn.QueryAsync<dynamic>(
                @"SELECT s.Id AS IdChatbotEsquemaSubcategoria, s.IdChatbotEsquemaInterpretarInformacion, s.Nombre
                  FROM [mkt].[T_ChatbotEsquemaSubcategoria] s
                  INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] ii
                         ON ii.Id = s.IdChatbotEsquemaInterpretarInformacion
                  WHERE ii.IdChatbotEsquema = @Id AND ii.Estado = 1 AND s.Estado = 1
                  ORDER BY s.Orden",
                new { Id = idChatbotEsquema })).ToList();

            var fases = (await conn.QueryAsync<dynamic>(
                @"SELECT sf.IdChatbotEsquemaSubcategoria, a.Nombre AS NombreFase
                  FROM [mkt].[T_ChatbotEsquemaSubcategoriaFase] sf
                  INNER JOIN [mkt].[T_ChatbotEsquemaSubcategoria] s
                         ON s.Id = sf.IdChatbotEsquemaSubcategoria
                  INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] ii
                         ON ii.Id = s.IdChatbotEsquemaInterpretarInformacion
                  INNER JOIN [mkt].[T_ChatbotEsquemaAsignacionFase] a
                         ON a.Id = sf.IdChatbotEsquemaAsignacionFase
                  WHERE ii.IdChatbotEsquema = @Id AND ii.Estado = 1 AND s.Estado = 1",
                new { Id = idChatbotEsquema })).ToList();

            var perfiles = (await conn.QueryAsync<dynamic>(
                @"SELECT sp2.IdChatbotEsquemaSubcategoria, a.Nombre AS NombrePerfil
                  FROM [mkt].[T_ChatbotEsquemaSubcategoriaPerfil] sp2
                  INNER JOIN [mkt].[T_ChatbotEsquemaSubcategoria] s
                         ON s.Id = sp2.IdChatbotEsquemaSubcategoria
                  INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] ii
                         ON ii.Id = s.IdChatbotEsquemaInterpretarInformacion
                  INNER JOIN [mkt].[T_ChatbotEsquemaAsignacionPerfil] a
                         ON a.Id = sp2.IdChatbotEsquemaAsignacionPerfil
                  WHERE ii.IdChatbotEsquema = @Id AND ii.Estado = 1 AND s.Estado = 1",
                new { Id = idChatbotEsquema })).ToList();

            var respuestas = (await conn.QueryAsync<dynamic>(
                @"SELECT l.ClasificacionTipoMensaje AS Clasificacion,
                         s.Nombre                  AS Subcategoria,
                         r.ParametrosRespuesta
                  FROM [mkt].[T_ChatbotEsquemaRespuesta] r
                  INNER JOIN [mkt].[T_ChatbotEsquemaLecturaMensaje] l
                         ON l.Id = r.IdChatbotEsquemaLecturaMensaje
                  LEFT  JOIN [mkt].[T_ChatbotEsquemaSubcategoria] s
                         ON s.Id = r.IdChatbotEsquemaSubcategoria
                  WHERE r.IdChatbotEsquema = @Id AND r.Estado = 1
                  ORDER BY r.Orden",
                new { Id = idChatbotEsquema })).ToList();

            return new EsquemaDetalleResponseDTO
            {
                IdChatbotEsquema = (int)esquema.IdChatbotEsquema,
                Nombre = (string)esquema.Nombre,
                Restricciones    = (string)(esquema.Restricciones ?? ""),

                LecturasMensajes = lecturas.Select(l => new LecturaMensajeDetalleDTO
                {
                    Clasificacion   = (string)l.Clasificacion,
                    PromptLectura   = (string)l.PromptLectura,
                    MensajesExactos = mensajes
                        .Where(m => m.IdChatbotEsquemaLecturaMensaje == l.IdChatbotEsquemaLecturaMensaje)
                        .Select(m => (string)m.NombreMensajeExacto)
                        .ToList()
                }).ToList(),

                InterpretarInformacion = interpretarList.Select(ii => new InterpretarInformacionDetalleDTO
                {
                    Nombre          = (string)ii.Nombre,
                    Clasificaciones = clasificaciones
                        .Where(c => c.IdChatbotEsquemaInterpretarInformacion == ii.IdChatbotEsquemaInterpretarInformacion)
                        .Select(c => (string)c.Clasificacion)
                        .ToList(),
                    Subcategorias = subcategorias
                        .Where(s => s.IdChatbotEsquemaInterpretarInformacion == ii.IdChatbotEsquemaInterpretarInformacion)
                        .Select(s => new SubcategoriaDetalleDTO
                        {
                            Nombre           = (string)s.Nombre,
                            FasMaximaValores = fases
                                .Where(f => f.IdChatbotEsquemaSubcategoria == s.IdChatbotEsquemaSubcategoria)
                                .Select(f => (string)f.NombreFase)
                                .ToList(),
                            PerfilValores = perfiles
                                .Where(p => p.IdChatbotEsquemaSubcategoria == s.IdChatbotEsquemaSubcategoria)
                                .Select(p => (string)p.NombrePerfil)
                                .ToList()
                        }).ToList()
                }).ToList(),

                EsquemasRespuesta = respuestas.Select(r => new EsquemaRespuestaDetalleDTO
                {
                    Clasificacion       = (string)r.Clasificacion,
                    Subcategoria        = (string?)r.Subcategoria,
                    ParametrosRespuesta = (string)r.ParametrosRespuesta
                }).ToList()
            };
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna todos los esquemas activos con detalle completo de lecturas, mensajes exactos,
        /// interpretaciones, subcategorias, fases, perfiles y respuestas.
        /// Ejecuta todas las consultas en una sola conexion para minimizar round-trips.
        /// </summary>
        public async Task<List<EsquemaListadoCompletoDTO>> ObtenerListadoEsquemasAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var esquemas = (await conn.QueryAsync<dynamic>(
                @"SELECT Id AS IdChatbotEsquema, Nombre, Restricciones
                  FROM [mkt].[T_ChatbotEsquema]
                  WHERE Estado = 1
                  ORDER BY Id DESC")).ToList();

            if (!esquemas.Any()) return new List<EsquemaListadoCompletoDTO>();

            var ids = esquemas.Select(e => (int)e.IdChatbotEsquema).ToList();

            var lecturas = (await conn.QueryAsync<dynamic>(
                @"SELECT CLM.Id AS IdChatbotEsquemaLecturaMensaje,
                         CLM.IdChatbotEsquema,
                         CLM.ClasificacionTipoMensaje AS Clasificacion,
                         CLM.PromptLectura
                  FROM [mkt].[T_ChatbotEsquemaLecturaMensaje] CLM
                  WHERE CLM.IdChatbotEsquema IN @Ids AND CLM.Estado = 1
                  ORDER BY CLM.Orden",
                new { Ids = ids })).ToList();

            var mensajes = (await conn.QueryAsync<dynamic>(
                @"SELECT CELME.IdChatbotEsquemaLecturaMensaje,
                         CEAME.Nombre AS NombreMensajeExacto
                  FROM [mkt].[T_ChatbotEsquemaLecturaMensajeExacto] CELME
                  INNER JOIN [mkt].[T_ChatbotEsquemaLecturaMensaje]          CLM   ON CLM.Id   = CELME.IdChatbotEsquemaLecturaMensaje
                  INNER JOIN [mkt].[T_ChatbotEsquemaAsignacionMensajeExacto] CEAME ON CEAME.Id = CELME.IdChatbotEsquemaAsignacionMensajeExacto
                  WHERE CLM.IdChatbotEsquema IN @Ids AND CLM.Estado = 1 AND CEAME.Estado = 1",
                new { Ids = ids })).ToList();

            var interpretarList = (await conn.QueryAsync<dynamic>(
                @"SELECT CII.Id AS IdChatbotEsquemaInterpretarInformacion,
                         CII.IdChatbotEsquema,
                         CII.Nombre
                  FROM [mkt].[T_ChatbotEsquemaInterpretarInformacion] CII
                  WHERE CII.IdChatbotEsquema IN @Ids AND CII.Estado = 1
                  ORDER BY CII.Orden",
                new { Ids = ids })).ToList();

            var clasificaciones = (await conn.QueryAsync<dynamic>(
                @"SELECT CIIC.IdChatbotEsquemaInterpretarInformacion,
                         CLM.ClasificacionTipoMensaje AS Clasificacion
                  FROM [mkt].[T_ChatbotEsquemaInterpretarInformacionClasificacion] CIIC
                  INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] CII ON CII.Id = CIIC.IdChatbotEsquemaInterpretarInformacion
                  INNER JOIN [mkt].[T_ChatbotEsquemaLecturaMensaje]  CLM ON CLM.Id = CIIC.IdChatbotEsquemaLecturaMensaje
                  WHERE CII.IdChatbotEsquema IN @Ids AND CII.Estado = 1",
                new { Ids = ids })).ToList();

            var subcategorias = (await conn.QueryAsync<dynamic>(
                @"SELECT SC.Id AS IdChatbotEsquemaSubcategoria,
                         SC.IdChatbotEsquemaInterpretarInformacion,
                         SC.Nombre
                  FROM [mkt].[T_ChatbotEsquemaSubcategoria] SC
                  INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] CII ON CII.Id = SC.IdChatbotEsquemaInterpretarInformacion
                  WHERE CII.IdChatbotEsquema IN @Ids AND CII.Estado = 1 AND SC.Estado = 1
                  ORDER BY SC.Orden",
                new { Ids = ids })).ToList();

            var fases = (await conn.QueryAsync<dynamic>(
                @"SELECT CESF.IdChatbotEsquemaSubcategoria,
                         CEF.Nombre AS NombreFase
                  FROM [mkt].[T_ChatbotEsquemaSubcategoriaFase] CESF
                  INNER JOIN [mkt].[T_ChatbotEsquemaSubcategoria]    SC  ON SC.Id  = CESF.IdChatbotEsquemaSubcategoria
                  INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] CII ON CII.Id = SC.IdChatbotEsquemaInterpretarInformacion
                  INNER JOIN [mkt].[T_ChatbotEsquemaAsignacionFase]  CEF ON CEF.Id = CESF.IdChatbotEsquemaAsignacionFase
                  WHERE CII.IdChatbotEsquema IN @Ids AND CII.Estado = 1 AND SC.Estado = 1",
                new { Ids = ids })).ToList();

            var perfiles = (await conn.QueryAsync<dynamic>(
                @"SELECT CESP.IdChatbotEsquemaSubcategoria,
                         CEAP.Nombre AS NombrePerfil
                  FROM [mkt].[T_ChatbotEsquemaSubcategoriaPerfil] CESP
                  INNER JOIN [mkt].[T_ChatbotEsquemaSubcategoria]    SC   ON SC.Id   = CESP.IdChatbotEsquemaSubcategoria
                  INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] CII  ON CII.Id  = SC.IdChatbotEsquemaInterpretarInformacion
                  INNER JOIN [mkt].[T_ChatbotEsquemaAsignacionPerfil] CEAP ON CEAP.Id = CESP.IdChatbotEsquemaAsignacionPerfil
                  WHERE CII.IdChatbotEsquema IN @Ids AND CII.Estado = 1 AND SC.Estado = 1",
                new { Ids = ids })).ToList();

            var respuestas = (await conn.QueryAsync<dynamic>(
                @"SELECT CER.IdChatbotEsquema,
                         CLM.ClasificacionTipoMensaje AS Clasificacion,
                         SC.Nombre                   AS Subcategoria,
                         CER.ParametrosRespuesta
                  FROM [mkt].[T_ChatbotEsquemaRespuesta] CER
                  INNER JOIN [mkt].[T_ChatbotEsquemaLecturaMensaje] CLM ON CLM.Id = CER.IdChatbotEsquemaLecturaMensaje
                  LEFT  JOIN [mkt].[T_ChatbotEsquemaSubcategoria]   SC  ON SC.Id  = CER.IdChatbotEsquemaSubcategoria
                  WHERE CER.IdChatbotEsquema IN @Ids AND CER.Estado = 1
                  ORDER BY CER.Orden",
                new { Ids = ids })).ToList();

            return esquemas.Select(e =>
            {
                int esqId           = (int)e.IdChatbotEsquema;
                var esqLecturas     = lecturas.Where(l  => (int)l.IdChatbotEsquema == esqId).ToList();
                var esqInterpretar  = interpretarList.Where(ii => (int)ii.IdChatbotEsquema == esqId).ToList();

                return new EsquemaListadoCompletoDTO
                {
                    IdChatbotEsquema = esqId,
                    Nombre   = (string)e.Nombre,
                    Restricciones    = (string)(e.Restricciones ?? ""),

                    LecturasMensajes = esqLecturas.Select(l => new LecturaMensajeDetalleDTO
                    {
                        Clasificacion   = (string)l.Clasificacion,
                        PromptLectura   = (string)l.PromptLectura,
                        MensajesExactos = mensajes
                            .Where(m => (int)m.IdChatbotEsquemaLecturaMensaje == (int)l.IdChatbotEsquemaLecturaMensaje)
                            .Select(m => (string)m.NombreMensajeExacto)
                            .ToList()
                    }).ToList(),

                    InterpretarInformacion = esqInterpretar.Select(ii => new InterpretarInformacionDetalleDTO
                    {
                        Nombre          = (string)ii.Nombre,
                        Clasificaciones = clasificaciones
                            .Where(c => (int)c.IdChatbotEsquemaInterpretarInformacion == (int)ii.IdChatbotEsquemaInterpretarInformacion)
                            .Select(c => (string)c.Clasificacion)
                            .ToList(),
                        Subcategorias = subcategorias
                            .Where(s => (int)s.IdChatbotEsquemaInterpretarInformacion == (int)ii.IdChatbotEsquemaInterpretarInformacion)
                            .Select(s => new SubcategoriaDetalleDTO
                            {
                                Nombre           = (string)s.Nombre,
                                FasMaximaValores = fases
                                    .Where(f => (int)f.IdChatbotEsquemaSubcategoria == (int)s.IdChatbotEsquemaSubcategoria)
                                    .Select(f => (string)f.NombreFase)
                                    .ToList(),
                                PerfilValores = perfiles
                                    .Where(p => (int)p.IdChatbotEsquemaSubcategoria == (int)s.IdChatbotEsquemaSubcategoria)
                                    .Select(p => (string)p.NombrePerfil)
                                    .ToList()
                            }).ToList()
                    }).ToList(),

                    EsquemasRespuesta = respuestas
                        .Where(r => (int)r.IdChatbotEsquema == esqId)
                        .Select(r => new EsquemaRespuestaDetalleDTO
                        {
                            Clasificacion       = (string)r.Clasificacion,
                            Subcategoria        = (string?)r.Subcategoria,
                            ParametrosRespuesta = (string)(r.ParametrosRespuesta ?? "")
                        }).ToList()
                };
            }).ToList();
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Borrado logico en cascada de un esquema y todos sus registros hijo.
        /// </summary>
        /// <returns>Id del esquema eliminado</returns>
        public int EliminarEsquema(int id, string usuario)
        {
            try
            {
                var SP = "[mkt].[SP_ChatbotEsquema_Eliminar]";
                _dapperRepository.QuerySPDapper(SP, new { IdChatbotEsquema = id, UsuarioModificacion = usuario });
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna todos los registros activos del catalogo de fases para asignacion a subcategorias del esquema.
        /// </summary>
        public List<ChatbotEsquemaAsignacionFaseDTO> ObtenerListadoFase()
        {
            try
            {
                var SP = "[mkt].[SP_TChatbotEsquemaAsignacionFase_Obtener]";
                var jsonResult = _dapperRepository.QuerySPDapper(SP, null);

                if (string.IsNullOrEmpty(jsonResult) || jsonResult == "null")
                    return new List<ChatbotEsquemaAsignacionFaseDTO>();

                return JsonConvert.DeserializeObject<List<ChatbotEsquemaAsignacionFaseDTO>>(jsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 18/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna el catalogo activo de numeros WhatsApp para asignacion al esquema chatbot.
        /// </summary>
        public List<AsistenteWhatsAppAsignacionNumeroDTO> ObtenerListadoNumero()
        {
            try
            {
                var SP = "[mkt].[SP_TAsistenteMarketingWhatsAppAsignacion_Obtener]";
                var jsonResult = _dapperRepository.QuerySPDapper(SP, null);

                if (string.IsNullOrEmpty(jsonResult) || jsonResult == "null")
                    return new List<AsistenteWhatsAppAsignacionNumeroDTO>();

                return JsonConvert.DeserializeObject<List<AsistenteWhatsAppAsignacionNumeroDTO>>(jsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 18/03/2026
        /// Version: 1.0
        /// <summary>
        /// Upsert de numero WhatsApp en T_AsistenteMarketingWhatsAppAsignacion.
        /// Busca el numero en conf.T_WhatsAppConfiguracionApi (con soporte dual formato Mexico).
        /// Si no existe en conf → lanza excepcion.
        /// Si ya existe en ia → UPDATE EsquemaRespuesta + Estado.
        /// Si no existe en ia → INSERT completo.
        /// </summary>
        public int UpsertAsistenteWhatsAppAsignacion(UpsertAsistenteWhatsAppAsignacionDTO request, string usuario)
        {
            try
            {
                // Generar formato alternativo para Mexico (IdPais 52)
                var numero    = request.NumeroWhatsApp.Trim();
                var numeroAlt = numero;

                if (numero.StartsWith("521") && numero.Length == 13)
                    numeroAlt = "52" + numero.Substring(3);
                else if (numero.StartsWith("52") && numero.Length == 12)
                    numeroAlt = "521" + numero.Substring(2);

                // 1. Buscar en conf.T_WhatsAppConfiguracionApi
                var queryConf =
                    @"SELECT TOP 1 NumeroIndentificador, IdPais
                      FROM [conf].[T_WhatsAppConfiguracionApi]
                      WHERE Estado = 1
                        AND (Numero = @Numero OR Numero = @NumeroAlt
                          OR NumeroWhatsApp = @Numero OR NumeroWhatsApp = @NumeroAlt)";

                var jsonConf = _dapperRepository.QueryDapper(queryConf, new { Numero = numero, NumeroAlt = numeroAlt });

                if (string.IsNullOrEmpty(jsonConf) || jsonConf == "null")
                    throw new Exception($"El numero {numero} no existe en la configuracion de WhatsApp.");

                var conf = JsonConvert.DeserializeObject<dynamic>(jsonConf);
                if (conf == null || conf.Count == 0)
                    throw new Exception($"El numero {numero} no existe en la configuracion de WhatsApp.");

                string numeroIdentificador = (string)conf[0].NumeroIndentificador;
                int    idPais              = (int)conf[0].IdPais;

                // 2. Verificar si ya existe en ia.T_AsistenteMarketingWhatsAppAsignacion
                var queryExiste =
                    @"SELECT TOP 1 Id FROM [ia].[T_AsistenteMarketingWhatsAppAsignacion]
                      WHERE NumeroWhatsApp = @Numero OR NumeroWhatsApp = @NumeroAlt";

                var jsonExiste = _dapperRepository.QueryDapper(queryExiste, new { Numero = numero, NumeroAlt = numeroAlt });
                var existe     = JsonConvert.DeserializeObject<dynamic>(jsonExiste);

                if (existe != null && existe.Count > 0)
                {
                    // 3a. UPDATE: solo EsquemaRespuesta y Estado
                    int id      = (int)existe[0].Id;
                    var queryUp =
                        @"UPDATE [ia].[T_AsistenteMarketingWhatsAppAsignacion]
                          SET EsquemaRespuesta    = @EsquemaRespuesta,
                              Estado              = @Estado,
                              UsuarioModificacion = @Usuario,
                              FechaModificacion   = GETDATE()
                          WHERE Id = @Id";

                    _dapperRepository.QueryDapper(queryUp, new
                    {
                        EsquemaRespuesta = request.EsquemaRespuesta,
                        Estado           = request.Estado,
                        Usuario          = usuario,
                        Id               = id
                    });

                    return id;
                }
                else
                {
                    // 3b. INSERT completo
                    var queryIns =
                        @"INSERT INTO [ia].[T_AsistenteMarketingWhatsAppAsignacion]
                               (NumeroWhatsApp, NumeroIdentificador, IdPais, EsquemaRespuesta,
                                Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion)
                          VALUES
                               (@NumeroWhatsApp, @NumeroIdentificador, @IdPais, @EsquemaRespuesta,
                                @Estado, @Usuario, @Usuario, GETDATE(), GETDATE());
                          SELECT CAST(SCOPE_IDENTITY() as int) as NewId;";

                    var jsonIns    = _dapperRepository.QueryDapper(queryIns, new
                    {
                        NumeroWhatsApp       = numero,
                        NumeroIdentificador  = numeroIdentificador,
                        IdPais               = idPais,
                        EsquemaRespuesta     = request.EsquemaRespuesta,
                        Estado               = request.Estado,
                        Usuario              = usuario
                    });

                    var resultIns = JsonConvert.DeserializeObject<dynamic>(jsonIns);
                    return (int)resultIns[0].NewId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna todos los registros activos del catalogo de mensajes exactos utilizados como criterio de filtrado del BOT.
        /// </summary>
        public List<ChatbotEsquemaAsignacionMensajeExactoDTO> ObtenerListadoMensajeExacto()
        {
            try
            {
                var SP = "[mkt].[SP_TChatbotEsquemaAsignacionMensajeExacto_Obtener]";
                var jsonResult = _dapperRepository.QuerySPDapper(SP, null);

                if (string.IsNullOrEmpty(jsonResult) || jsonResult == "null")
                    return new List<ChatbotEsquemaAsignacionMensajeExactoDTO>();

                return JsonConvert.DeserializeObject<List<ChatbotEsquemaAsignacionMensajeExactoDTO>>(jsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna todos los registros activos del catalogo de perfiles de contacto para asignacion a subcategorias del esquema.
        /// </summary>
        public List<ChatbotEsquemaAsignacionPerfilDTO> ObtenerListadoPerfil()
        {
            try
            {
                var SP = "[mkt].[SP_TChatbotEsquemaAsignacionPerfil_Obtener]";
                var jsonResult = _dapperRepository.QuerySPDapper(SP, null);

                if (string.IsNullOrEmpty(jsonResult) || jsonResult == "null")
                    return new List<ChatbotEsquemaAsignacionPerfilDTO>();

                return JsonConvert.DeserializeObject<List<ChatbotEsquemaAsignacionPerfilDTO>>(jsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta un esquema completo de forma atomica. Recibe el DTO deserializado desde el frontend
        /// y orquesta los inserts en cada tabla con una unica transaccion SQL.
        /// </summary>
        /// <returns>Id del esquema recien creado</returns>
        public int InsertarEsquema(CrearEsquemaRequestDTO request, string usuario)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // 1. Insertar mensajes exactos nuevos en catalogo (omite duplicados activos)
                foreach (var nombre in request.NuevosMensajesExactos)
                {
                    connection.Execute(
                        @"IF NOT EXISTS (
                            SELECT 1 FROM [mkt].[T_ChatbotEsquemaAsignacionMensajeExacto]
                            WHERE Nombre = @Nombre AND Estado = 1
                          )
                          INSERT INTO [mkt].[T_ChatbotEsquemaAsignacionMensajeExacto]
                                 (Nombre, UsuarioCreacion)
                          VALUES (@Nombre, @Usuario)",
                        new { Nombre = nombre, Usuario = usuario },
                        transaction);
                }

                // 2. Insertar esquema maestro
                var idEsquema = connection.QueryFirstOrDefault<int>(
                    @"INSERT INTO [mkt].[T_ChatbotEsquema]
                             (Nombre, Restricciones, UsuarioCreacion)
                      VALUES (@Nombre, @Restricciones, @Usuario);
                      SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new { request.Nombre, request.Restricciones, Usuario = usuario },
                    transaction);

                // Mapa clasificacion → idLectura (necesario para EsquemasRespuesta e InterpretarInformacion)
                var clasificacionToIdLectura = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                // Mapa nombreSubcategoria → idSubcategoria (evita lookup a BD en paso de respuestas)
                var subcategoriaToId = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // 3. Insertar lecturas de mensaje
                int ordenLectura = 0;
                foreach (var lectura in request.LecturasMensajes)
                {
                    var idLectura = connection.QueryFirstOrDefault<int>(
                        @"INSERT INTO [mkt].[T_ChatbotEsquemaLecturaMensaje]
                                 (IdChatbotEsquema, ClasificacionTipoMensaje, PromptLectura, Orden, UsuarioCreacion)
                          VALUES (@IdEsquema, @Clasificacion, @PromptLectura, @Orden, @Usuario);
                          SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { IdEsquema = idEsquema, lectura.Clasificacion, lectura.PromptLectura, Orden = ordenLectura, Usuario = usuario },
                        transaction);

                    clasificacionToIdLectura[lectura.Clasificacion] = idLectura;

                    // 3a. Vincular mensajes exactos a esta lectura
                    foreach (var nombreMensaje in lectura.MensajesExactos)
                    {
                        connection.Execute(
                            @"INSERT INTO [mkt].[T_ChatbotEsquemaLecturaMensajeExacto]
                                     (IdChatbotEsquemaLecturaMensaje, IdChatbotEsquemaAsignacionMensajeExacto, UsuarioCreacion)
                              SELECT @IdLectura, Id, @Usuario
                              FROM   [mkt].[T_ChatbotEsquemaAsignacionMensajeExacto]
                              WHERE  Nombre = @Nombre AND Estado = 1",
                            new { IdLectura = idLectura, Nombre = nombreMensaje, Usuario = usuario },
                            transaction);
                    }

                    ordenLectura++;
                }

                // 4. Insertar bloques InterpretarInfo
                int ordenInfo = 0;
                foreach (var info in request.InterpretarInformacion)
                {
                    var idInfo = connection.QueryFirstOrDefault<int>(
                        @"INSERT INTO [mkt].[T_ChatbotEsquemaInterpretarInformacion]
                                 (IdChatbotEsquema, Nombre, Orden, UsuarioCreacion)
                          VALUES (@IdEsquema, @Nombre, @Orden, @Usuario);
                          SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { IdEsquema = idEsquema, info.Nombre, Orden = ordenInfo, Usuario = usuario },
                        transaction);

                    // 4a. Clasificaciones: vincula este bloque a las lecturas por nombre
                    foreach (var clasif in info.Clasificaciones)
                    {
                        if (!clasificacionToIdLectura.TryGetValue(clasif, out var idLecturaClasif))
                            continue;

                        connection.Execute(
                            @"INSERT INTO [mkt].[T_ChatbotEsquemaInterpretarInformacionClasificacion]
                                     (IdChatbotEsquemaInterpretarInformacion, IdChatbotEsquemaLecturaMensaje, UsuarioCreacion)
                              VALUES (@IdInfo, @IdLectura, @Usuario)",
                            new { IdInfo = idInfo, IdLectura = idLecturaClasif, Usuario = usuario },
                            transaction);
                    }

                    // 4b. Subcategorias
                    int ordenSub = 0;
                    foreach (var sub in info.Subcategorias)
                    {
                        var idSubcategoria = connection.QueryFirstOrDefault<int>(
                            @"INSERT INTO [mkt].[T_ChatbotEsquemaSubcategoria]
                                     (IdChatbotEsquemaInterpretarInformacion, Nombre, Orden, UsuarioCreacion)
                              VALUES (@IdInfo, @Nombre, @Orden, @Usuario);
                              SELECT CAST(SCOPE_IDENTITY() AS INT);",
                            new { IdInfo = idInfo, sub.Nombre, Orden = ordenSub, Usuario = usuario },
                            transaction);

                        subcategoriaToId[sub.Nombre] = idSubcategoria;

                        foreach (var fase in sub.FasMaximaValores)
                        {
                            connection.Execute(
                                @"INSERT INTO [mkt].[T_ChatbotEsquemaSubcategoriaFase]
                                         (IdChatbotEsquemaSubcategoria, IdChatbotEsquemaAsignacionFase, UsuarioCreacion)
                                  SELECT @IdSub, Id, @Usuario
                                  FROM   [mkt].[T_ChatbotEsquemaAsignacionFase]
                                  WHERE  Nombre = @Nombre AND Estado = 1",
                                new { IdSub = idSubcategoria, Nombre = fase, Usuario = usuario },
                                transaction);
                        }

                        foreach (var perfil in sub.PerfilValores)
                        {
                            connection.Execute(
                                @"INSERT INTO [mkt].[T_ChatbotEsquemaSubcategoriaPerfil]
                                         (IdChatbotEsquemaSubcategoria, IdChatbotEsquemaAsignacionPerfil, UsuarioCreacion)
                                  SELECT @IdSub, Id, @Usuario
                                  FROM   [mkt].[T_ChatbotEsquemaAsignacionPerfil]
                                  WHERE  Nombre = @Nombre AND Estado = 1",
                                new { IdSub = idSubcategoria, Nombre = perfil, Usuario = usuario },
                                transaction);
                        }

                        ordenSub++;
                    }

                    ordenInfo++;
                }

                // 5. Insertar esquemas de respuesta
                int ordenRespuesta = 0;
                foreach (var resp in request.EsquemasRespuesta)
                {
                    if (!clasificacionToIdLectura.TryGetValue(resp.Clasificacion, out var idLecturaResp))
                        continue;

                    int? idSubcategoriaResp = null;
                    if (!string.IsNullOrWhiteSpace(resp.Subcategoria) && resp.Subcategoria != "—")
                    {
                        if (subcategoriaToId.TryGetValue(resp.Subcategoria, out var tempId))
                            idSubcategoriaResp = tempId;
                    }

                    connection.Execute(
                        @"INSERT INTO [mkt].[T_ChatbotEsquemaRespuesta]
                                 (IdChatbotEsquema, IdChatbotEsquemaLecturaMensaje,
                                  IdChatbotEsquemaSubcategoria, ParametrosRespuesta, Orden, UsuarioCreacion)
                          VALUES (@IdEsquema, @IdLectura, @IdSubcategoria, @ParametrosRespuesta, @Orden, @Usuario)",
                        new
                        {
                            IdEsquema        = idEsquema,
                            IdLectura        = idLecturaResp,
                            IdSubcategoria   = idSubcategoriaResp,
                            resp.ParametrosRespuesta,
                            Orden            = ordenRespuesta,
                            Usuario          = usuario
                        },
                        transaction);

                    ordenRespuesta++;
                }

                transaction.Commit();
                return idEsquema;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza un esquema existente de forma atomica.
        /// Estrategia: limpieza fisica de tablas N:M, borrado logico de entidades hijas
        /// y reinsercion completa en una sola transaccion.
        /// </summary>
        /// <returns>Id del esquema actualizado</returns>
        public int ActualizarEsquema(ActualizarEsquemaRequestDTO request, string usuario)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var idEsquema = request.IdChatbotEsquema;

                // 1. Actualizar esquema maestro
                connection.Execute(
                    @"UPDATE CE
                      SET    CE.Nombre     = @Nombre,
                             CE.Restricciones       = @Restricciones,
                             CE.UsuarioModificacion = @Usuario,
                             CE.FechaModificacion   = GETDATE()
                      FROM   [mkt].[T_ChatbotEsquema] CE
                      WHERE  CE.Id     = @IdEsquema
                        AND  CE.Estado = 1",
                    new { request.Nombre, request.Restricciones, Usuario = usuario, IdEsquema = idEsquema },
                    transaction);

                // 2. Insertar mensajes exactos nuevos en catalogo (omite duplicados activos)
                foreach (var nombre in request.NuevosMensajesExactos)
                {
                    connection.Execute(
                        @"IF NOT EXISTS (
                            SELECT 1 FROM [mkt].[T_ChatbotEsquemaAsignacionMensajeExacto]
                            WHERE Nombre = @Nombre AND Estado = 1
                          )
                          INSERT INTO [mkt].[T_ChatbotEsquemaAsignacionMensajeExacto]
                                 (Nombre, UsuarioCreacion)
                          VALUES (@Nombre, @Usuario)",
                        new { Nombre = nombre, Usuario = usuario },
                        transaction);
                }

                // 3. Limpieza fisica de tablas N:M (no tienen campo Estado)
                connection.Execute(
                    @"DELETE CESF
                      FROM   [mkt].[T_ChatbotEsquemaSubcategoriaFase] CESF
                      INNER JOIN [mkt].[T_ChatbotEsquemaSubcategoria]    SC  ON SC.Id  = CESF.IdChatbotEsquemaSubcategoria
                      INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] CII ON CII.Id = SC.IdChatbotEsquemaInterpretarInformacion
                      WHERE  CII.IdChatbotEsquema = @IdEsquema",
                    new { IdEsquema = idEsquema }, transaction);

                connection.Execute(
                    @"DELETE CESP
                      FROM   [mkt].[T_ChatbotEsquemaSubcategoriaPerfil] CESP
                      INNER JOIN [mkt].[T_ChatbotEsquemaSubcategoria]    SC  ON SC.Id  = CESP.IdChatbotEsquemaSubcategoria
                      INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] CII ON CII.Id = SC.IdChatbotEsquemaInterpretarInformacion
                      WHERE  CII.IdChatbotEsquema = @IdEsquema",
                    new { IdEsquema = idEsquema }, transaction);

                connection.Execute(
                    @"DELETE CIIC
                      FROM   [mkt].[T_ChatbotEsquemaInterpretarInformacionClasificacion] CIIC
                      INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] CII
                             ON CII.Id = CIIC.IdChatbotEsquemaInterpretarInformacion
                      WHERE  CII.IdChatbotEsquema = @IdEsquema",
                    new { IdEsquema = idEsquema }, transaction);

                connection.Execute(
                    @"DELETE CELME
                      FROM   [mkt].[T_ChatbotEsquemaLecturaMensajeExacto] CELME
                      INNER JOIN [mkt].[T_ChatbotEsquemaLecturaMensaje] CLM
                             ON CLM.Id = CELME.IdChatbotEsquemaLecturaMensaje
                      WHERE  CLM.IdChatbotEsquema = @IdEsquema",
                    new { IdEsquema = idEsquema }, transaction);

                // 4. Borrado logico de entidades hijas
                connection.Execute(
                    @"UPDATE SC
                      SET    SC.Estado              = 0,
                             SC.UsuarioModificacion = @Usuario,
                             SC.FechaModificacion   = GETDATE()
                      FROM   [mkt].[T_ChatbotEsquemaSubcategoria]    SC
                      INNER JOIN [mkt].[T_ChatbotEsquemaInterpretarInformacion] CII
                             ON CII.Id = SC.IdChatbotEsquemaInterpretarInformacion
                      WHERE  CII.IdChatbotEsquema = @IdEsquema AND SC.Estado = 1",
                    new { IdEsquema = idEsquema, Usuario = usuario }, transaction);

                connection.Execute(
                    @"UPDATE CII
                      SET    CII.Estado              = 0,
                             CII.UsuarioModificacion = @Usuario,
                             CII.FechaModificacion   = GETDATE()
                      FROM   [mkt].[T_ChatbotEsquemaInterpretarInformacion] CII
                      WHERE  CII.IdChatbotEsquema = @IdEsquema AND CII.Estado = 1",
                    new { IdEsquema = idEsquema, Usuario = usuario }, transaction);

                // DELETE fisico porque UIX_T_ChatbotEsquemaRespuesta_SinSubcategoria no filtra por Estado
                connection.Execute(
                    @"DELETE CER
                      FROM   [mkt].[T_ChatbotEsquemaRespuesta] CER
                      WHERE  CER.IdChatbotEsquema = @IdEsquema",
                    new { IdEsquema = idEsquema }, transaction);

                // DELETE fisico porque la tabla tiene UNIQUE KEY en (IdChatbotEsquema, ClasificacionTipoMensaje)
                // sin filtrar por Estado, por lo que el borrado logico impediria la reinsercion.
                // Los hijos N:M ya fueron eliminados fisicamente en el paso anterior.
                connection.Execute(
                    @"DELETE CLM
                      FROM   [mkt].[T_ChatbotEsquemaLecturaMensaje] CLM
                      WHERE  CLM.IdChatbotEsquema = @IdEsquema",
                    new { IdEsquema = idEsquema }, transaction);

                // 5. Reinsertar configuracion nueva (misma logica que InsertarEsquema pasos 3-5)
                var clasificacionToIdLectura = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                int ordenLectura = 0;
                foreach (var lectura in request.LecturasMensajes)
                {
                    var idLectura = connection.QueryFirstOrDefault<int>(
                        @"INSERT INTO [mkt].[T_ChatbotEsquemaLecturaMensaje]
                                 (IdChatbotEsquema, ClasificacionTipoMensaje, PromptLectura, Orden, UsuarioCreacion)
                          VALUES (@IdEsquema, @Clasificacion, @PromptLectura, @Orden, @Usuario);
                          SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { IdEsquema = idEsquema, lectura.Clasificacion, lectura.PromptLectura, Orden = ordenLectura, Usuario = usuario },
                        transaction);

                    clasificacionToIdLectura[lectura.Clasificacion] = idLectura;

                    foreach (var nombreMensaje in lectura.MensajesExactos)
                    {
                        connection.Execute(
                            @"INSERT INTO [mkt].[T_ChatbotEsquemaLecturaMensajeExacto]
                                     (IdChatbotEsquemaLecturaMensaje, IdChatbotEsquemaAsignacionMensajeExacto, UsuarioCreacion)
                              SELECT @IdLectura, Id, @Usuario
                              FROM   [mkt].[T_ChatbotEsquemaAsignacionMensajeExacto]
                              WHERE  Nombre = @Nombre AND Estado = 1",
                            new { IdLectura = idLectura, Nombre = nombreMensaje, Usuario = usuario },
                            transaction);
                    }

                    ordenLectura++;
                }

                // Mapa nombreSubcategoria → idSubcategoria (evita lookup a BD en paso de respuestas)
                var subcategoriaToId = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                int ordenInfo = 0;
                foreach (var info in request.InterpretarInformacion)
                {
                    var idInfo = connection.QueryFirstOrDefault<int>(
                        @"INSERT INTO [mkt].[T_ChatbotEsquemaInterpretarInformacion]
                                 (IdChatbotEsquema, Nombre, Orden, UsuarioCreacion)
                          VALUES (@IdEsquema, @Nombre, @Orden, @Usuario);
                          SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new { IdEsquema = idEsquema, info.Nombre, Orden = ordenInfo, Usuario = usuario },
                        transaction);

                    foreach (var clasif in info.Clasificaciones)
                    {
                        if (!clasificacionToIdLectura.TryGetValue(clasif, out var idLecturaClasif))
                            continue;

                        connection.Execute(
                            @"INSERT INTO [mkt].[T_ChatbotEsquemaInterpretarInformacionClasificacion]
                                     (IdChatbotEsquemaInterpretarInformacion, IdChatbotEsquemaLecturaMensaje, UsuarioCreacion)
                              VALUES (@IdInfo, @IdLectura, @Usuario)",
                            new { IdInfo = idInfo, IdLectura = idLecturaClasif, Usuario = usuario },
                            transaction);
                    }

                    int ordenSub = 0;
                    foreach (var sub in info.Subcategorias)
                    {
                        var idSubcategoria = connection.QueryFirstOrDefault<int>(
                            @"INSERT INTO [mkt].[T_ChatbotEsquemaSubcategoria]
                                     (IdChatbotEsquemaInterpretarInformacion, Nombre, Orden, UsuarioCreacion)
                              VALUES (@IdInfo, @Nombre, @Orden, @Usuario);
                              SELECT CAST(SCOPE_IDENTITY() AS INT);",
                            new { IdInfo = idInfo, sub.Nombre, Orden = ordenSub, Usuario = usuario },
                            transaction);

                        subcategoriaToId[sub.Nombre] = idSubcategoria;

                        foreach (var fase in sub.FasMaximaValores)
                        {
                            connection.Execute(
                                @"INSERT INTO [mkt].[T_ChatbotEsquemaSubcategoriaFase]
                                         (IdChatbotEsquemaSubcategoria, IdChatbotEsquemaAsignacionFase, UsuarioCreacion)
                                  SELECT @IdSub, Id, @Usuario
                                  FROM   [mkt].[T_ChatbotEsquemaAsignacionFase]
                                  WHERE  Nombre = @Nombre AND Estado = 1",
                                new { IdSub = idSubcategoria, Nombre = fase, Usuario = usuario },
                                transaction);
                        }

                        foreach (var perfil in sub.PerfilValores)
                        {
                            connection.Execute(
                                @"INSERT INTO [mkt].[T_ChatbotEsquemaSubcategoriaPerfil]
                                         (IdChatbotEsquemaSubcategoria, IdChatbotEsquemaAsignacionPerfil, UsuarioCreacion)
                                  SELECT @IdSub, Id, @Usuario
                                  FROM   [mkt].[T_ChatbotEsquemaAsignacionPerfil]
                                  WHERE  Nombre = @Nombre AND Estado = 1",
                                new { IdSub = idSubcategoria, Nombre = perfil, Usuario = usuario },
                                transaction);
                        }

                        ordenSub++;
                    }

                    ordenInfo++;
                }

                int ordenRespuesta = 0;
                foreach (var resp in request.EsquemasRespuesta)
                {
                    if (!clasificacionToIdLectura.TryGetValue(resp.Clasificacion, out var idLecturaResp))
                        continue;

                    int? idSubcategoriaResp = null;
                    if (!string.IsNullOrWhiteSpace(resp.Subcategoria) && resp.Subcategoria != "—")
                    {
                        if (subcategoriaToId.TryGetValue(resp.Subcategoria, out var tempId))
                            idSubcategoriaResp = tempId;
                    }

                    connection.Execute(
                        @"INSERT INTO [mkt].[T_ChatbotEsquemaRespuesta]
                                 (IdChatbotEsquema, IdChatbotEsquemaLecturaMensaje,
                                  IdChatbotEsquemaSubcategoria, ParametrosRespuesta, Orden, UsuarioCreacion)
                          VALUES (@IdEsquema, @IdLectura, @IdSubcategoria, @ParametrosRespuesta, @Orden, @Usuario)",
                        new
                        {
                            IdEsquema        = idEsquema,
                            IdLectura        = idLecturaResp,
                            IdSubcategoria   = idSubcategoriaResp,
                            resp.ParametrosRespuesta,
                            Orden            = ordenRespuesta,
                            Usuario          = usuario
                        },
                        transaction);

                    ordenRespuesta++;
                }

                transaction.Commit();
                return idEsquema;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
