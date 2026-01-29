using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Repositorio.Repository.Interface;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// <summary>
    /// Repositorio para Respuestas (matriz de parámetros) usando Dapper
    /// </summary>
    public class EsquemaRespuestaRepository : IEsquemaRespuestaRepository
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly IConnectionFactory _connectionFactory;

        public EsquemaRespuestaRepository(IDapperRepository dapperRepository, IConnectionFactory connectionFactory)
        {
            _dapperRepository = dapperRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InsertarAsync(EsquemaRespuestaRequestDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacionLecturaMensaje", entidad.IdEsquemaWhatsAppAsignacionLecturaMensaje, DbType.Int32);
                    parametros.Add("@IdEsquemaWhatsAppAsignacionSubcategoria", entidad.IdEsquemaWhatsAppAsignacionSubcategoria, DbType.Int32);
                    parametros.Add("@ParametrosRespuesta", entidad.ParametrosRespuesta, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionRespuesta_Insertar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar respuesta: {ex.Message}", ex);
            }
        }

        public async Task<int> ActualizarAsync(EsquemaRespuestaActualizarDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Id", entidad.Id, DbType.Int32);
                    parametros.Add("@ParametrosRespuesta", entidad.ParametrosRespuesta, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionRespuesta_Actualizar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar respuesta: {ex.Message}", ex);
            }
        }

        public async Task<List<EsquemaRespuestaDTO>> ListarPorEsquemaAsync(int idEsquemaWhatsAppAsignacion)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "[mkt].[SP_TEsquemaWhatsAppAsignacionRespuesta_ListarPorEsquema]",
                    new { IdEsquemaWhatsAppAsignacion = idEsquemaWhatsAppAsignacion }
                );

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<EsquemaRespuestaDTO>>(resultado)!;
                }

                return new List<EsquemaRespuestaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar respuestas por esquema: {ex.Message}", ex);
            }
        }
    }
}
