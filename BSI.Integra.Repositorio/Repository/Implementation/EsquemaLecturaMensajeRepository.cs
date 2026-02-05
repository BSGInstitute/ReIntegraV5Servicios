using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Repositorio.Repository.Interface;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// <summary>
    /// Repositorio para Lectura de Mensajes (Clasificaciones) usando Dapper
    /// </summary>
    public class EsquemaLecturaMensajeRepository : IEsquemaLecturaMensajeRepository
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly IConnectionFactory _connectionFactory;

        public EsquemaLecturaMensajeRepository(IDapperRepository dapperRepository, IConnectionFactory connectionFactory)
        {
            _dapperRepository = dapperRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InsertarAsync(EsquemaLecturaMensajeRequestDTO entidad, string usuario)
        {
            try
            {
                // Armar string de IDs de mensajes exactos separados por coma
                var mensajesExactosIds = entidad.MensajesExactosIds != null && entidad.MensajesExactosIds.Any()
                    ? string.Join(",", entidad.MensajesExactosIds)
                    : null;

                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacion", entidad.IdEsquemaWhatsAppAsignacion, DbType.Int32);
                    parametros.Add("@ClasificacionTipoMensaje", entidad.ClasificacionTipoMensaje, DbType.String);
                    parametros.Add("@PromptLectura", entidad.PromptLectura, DbType.String);
                    parametros.Add("@MensajesExactosIds", mensajesExactosIds, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionLecturaMensaje_Insertar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar lectura de mensaje: {ex.Message}", ex);
            }
        }

        public async Task<int> ActualizarAsync(EsquemaLecturaMensajeRequestDTO entidad, string usuario)
        {
            try
            {
                // Armar string de IDs de mensajes exactos separados por coma
                var mensajesExactosIds = entidad.MensajesExactosIds != null && entidad.MensajesExactosIds.Any()
                    ? string.Join(",", entidad.MensajesExactosIds)
                    : null;

                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacionLecturaMensaje", entidad.Id, DbType.Int32);
                    parametros.Add("@ClasificacionTipoMensaje", entidad.ClasificacionTipoMensaje, DbType.String);
                    parametros.Add("@PromptLectura", entidad.PromptLectura, DbType.String);
                    parametros.Add("@MensajesExactosIds", mensajesExactosIds, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionLecturaMensaje_Actualizar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar lectura de mensaje: {ex.Message}", ex);
            }
        }

        public async Task<int> EliminarAsync(int id, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacionLecturaMensaje", id, DbType.Int32);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionLecturaMensaje_Eliminar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar lectura de mensaje: {ex.Message}", ex);
            }
        }

        public async Task<EsquemaLecturaMensajeDetalleDTO> ObtenerPorIdAsync(int id)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacionLecturaMensaje", id, DbType.Int32);

                    // Este SP retorna 2 result sets: lectura de mensaje + mensajes exactos
                    using (var multi = await conn.QueryMultipleAsync(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionLecturaMensaje_ObtenerPorId]",
                        parametros,
                        commandType: CommandType.StoredProcedure))
                    {
                        var lecturaMensaje = await multi.ReadFirstOrDefaultAsync<EsquemaLecturaMensajeDetalleDTO>();

                        if (lecturaMensaje != null)
                        {
                            lecturaMensaje.MensajesExactos = (await multi.ReadAsync<MensajeExactoAsociadoDTO>()).ToList();
                        }

                        return lecturaMensaje!;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener lectura de mensaje por Id: {ex.Message}", ex);
            }
        }

        public async Task<List<EsquemaLecturaMensajeDTO>> ListarPorEsquemaAsync(int idEsquemaWhatsAppAsignacion)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "[mkt].[SP_TEsquemaWhatsAppAsignacionLecturaMensaje_ListarPorEsquema]",
                    new { IdEsquemaWhatsAppAsignacion = idEsquemaWhatsAppAsignacion }
                );

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<EsquemaLecturaMensajeDTO>>(resultado)!;
                }

                return new List<EsquemaLecturaMensajeDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar lecturas de mensaje por esquema: {ex.Message}", ex);
            }
        }
    }
}
