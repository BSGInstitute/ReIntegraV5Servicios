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
    /// Repositorio para Mensajes Exactos (catálogo global) usando Dapper
    /// </summary>
    public class MensajeExactoRepository : IMensajeExactoRepository
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly IConnectionFactory _connectionFactory;

        public MensajeExactoRepository(IDapperRepository dapperRepository, IConnectionFactory connectionFactory)
        {
            _dapperRepository = dapperRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InsertarAsync(MensajeExactoRequestDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionMensajeExacto_Insertar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar mensaje exacto: {ex.Message}", ex);
            }
        }

        public async Task<int> ActualizarAsync(MensajeExactoRequestDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacionMensajeExacto", entidad.Id, DbType.Int32);
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionMensajeExacto_Actualizar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar mensaje exacto: {ex.Message}", ex);
            }
        }

        public async Task<int> EliminarAsync(int id, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacionMensajeExacto", id, DbType.Int32);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionMensajeExacto_Eliminar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar mensaje exacto: {ex.Message}", ex);
            }
        }

        public async Task<List<MensajeExactoDTO>> ListarAsync()
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "[mkt].[SP_TEsquemaWhatsAppAsignacionMensajeExacto_Listar]",
                    null
                );

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<MensajeExactoDTO>>(resultado)!;
                }

                return new List<MensajeExactoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar mensajes exactos: {ex.Message}", ex);
            }
        }
    }
}
