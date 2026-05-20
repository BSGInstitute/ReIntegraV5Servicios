using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: LlamadasWhatsAppRepository
    /// Autor: WhatsApp Business Calling API integration
    /// Fecha: 2026-05-08
    /// <summary>
    /// Repositorio de lectura para historial de llamadas de WhatsApp Business Calling.
    /// Tabla: com.T_WhatsappLlamada (modelo: TWhatsappLlamadum).
    /// </summary>
    public class LlamadasWhatsAppRepository : GenericRepository<TWhatsappLlamadum>, ILlamadasWhatsAppRepository
    {
        public LlamadasWhatsAppRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
        }

        /// Tipo Función: Lectura
        /// Autor: WhatsApp Business Calling API integration
        /// Fecha: 2026-05-08
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el historial paginado de llamadas filtrando por país, área, agente, número, tipo, estado y rango de fechas.
        /// El SP devuelve cada fila con la columna TotalRegistros para resolver la paginación en el cliente.
        /// </summary>
        public LlamadasHistorialResultadoDTO ObtenerHistorialPaginado(LlamadasHistorialFiltroDTO filtro)
        {
            try
            {
                var resultado = new LlamadasHistorialResultadoDTO
                {
                    Pagina = filtro.Pagina,
                    Llamadas = new List<WhatsAppLlamadaResumenDTO>(),
                    TotalRegistros = 0
                };

                var parametros = new
                {
                    //idPais = filtro.IdPais,
                    //idArea = filtro.IdPersonalAreaTrabajo,
                    idPersonal = filtro.IdPersonal,
                    //numero = filtro.NumeroWhatsApp,
                    //tipo = filtro.TipoLlamada,
                    //estado = filtro.EstadoLlamada,
                    //desde = filtro.FechaDesde,
                    //hasta = filtro.FechaHasta,
                    pagina = filtro.Pagina,
                    registrosPorPagina = filtro.RegistrosPorPagina
                };

                var json = _dapperRepository.QuerySPDapper("com.SP_WhatsappLlamada_ObtenerHistorial", parametros);

                if (string.IsNullOrEmpty(json) || json.Contains("[]"))
                {
                    return resultado;
                }

                var lista = JsonConvert.DeserializeObject<List<WhatsAppLlamadaResumenDTO>>(json);
                if (lista != null && lista.Count > 0)
                {
                    resultado.Llamadas = lista;
                    resultado.TotalRegistros = lista[0].TotalRegistros;
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Tipo Función: Lectura
        /// Autor: WhatsApp Business Calling API integration
        /// Fecha: 2026-05-15
        /// Versión: 1.0
        /// <summary>
        /// Busca la última solicitud de consentimiento saliente (TipoLlamada=2) para un par
        /// (numero, idPais). Se usa para que el frontend decida si mostrar "Solicitar", "Esperando",
        /// "Llamar" o "Rechazado". El nombre de la columna PK es Id (no IdWhatsappLlamada);
        /// se aliasea en el SELECT para que matchee con el DTO.
        /// </summary>
        public WhatsAppConsentimientoRawDTO? ObtenerUltimoConsentimiento(string numeroWhatsApp, int idPais)
        {
            try
            {
                const string sql = @"SELECT TOP 1 Id AS IdWhatsappLlamada, WaId,
                                            ConsentimientoEstado, ConsentimientoFecha, ConsentimientoExpira
                                     FROM com.T_WhatsappLlamada
                                     WHERE NumeroWhatsApp = @NumeroWhatsApp
                                       AND IdPais = @IdPais
                                       AND TipoLlamada = 2
                                       AND ConsentimientoEstado IS NOT NULL
                                     ORDER BY Id DESC";

                var parametros = new { NumeroWhatsApp = numeroWhatsApp, IdPais = idPais };
                var json = _dapperRepository.FirstOrDefault(sql, parametros);

                if (string.IsNullOrEmpty(json) || json == "null") return null;
                return JsonConvert.DeserializeObject<WhatsAppConsentimientoRawDTO>(json);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Tipo Función: Escritura
        /// Autor: WhatsApp Business Calling API integration
        /// Fecha: 2026-05-19
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la URL + nombre del blob de la grabación de una llamada vía
        /// SP_WhatsappLlamada_ActualizarGrabacion. El SP también actualiza UsuarioModificacion
        /// y FechaModificacion.
        /// </summary>
        public bool ActualizarGrabacion(int idWhatsappLlamada, string grabacionUrl, string grabacionBlobNombre, string usuarioModificacion)
        {
            try
            {
                var parametros = new
                {
                    Id                  = idWhatsappLlamada,
                    GrabacionUrl        = grabacionUrl,
                    GrabacionBlobNombre = grabacionBlobNombre,
                    UsuarioModificacion = usuarioModificacion
                };
                _dapperRepository.QuerySPDapper("com.SP_WhatsappLlamada_ActualizarGrabacion", parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
