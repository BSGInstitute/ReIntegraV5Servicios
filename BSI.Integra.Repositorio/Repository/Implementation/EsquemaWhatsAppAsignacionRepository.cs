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
    /// Repositorio para Esquema Principal usando Dapper
    /// </summary>
    public class EsquemaWhatsAppAsignacionRepository : IEsquemaWhatsAppAsignacionRepository
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly IConnectionFactory _connectionFactory;

        public EsquemaWhatsAppAsignacionRepository(IDapperRepository dapperRepository, IConnectionFactory connectionFactory)
        {
            _dapperRepository = dapperRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InsertarAsync(EsquemaWhatsAppAsignacionRequestDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@Restricciones", entidad.Restricciones, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacion_Insertar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar esquema: {ex.Message}", ex);
            }
        }

        public async Task<int> ActualizarAsync(EsquemaWhatsAppAsignacionRequestDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacion", entidad.Id, DbType.Int32);
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@Restricciones", entidad.Restricciones, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacion_Actualizar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar esquema: {ex.Message}", ex);
            }
        }

        public async Task<int> EliminarAsync(int id, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacion", id, DbType.Int32);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacion_Eliminar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar esquema: {ex.Message}", ex);
            }
        }

        public async Task<EsquemaWhatsAppAsignacionDTO> ObtenerPorIdAsync(int id)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "[mkt].[SP_TEsquemaWhatsAppAsignacion_ObtenerPorId]",
                    new { IdEsquemaWhatsAppAsignacion = id }
                );

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    var lista = JsonConvert.DeserializeObject<List<EsquemaWhatsAppAsignacionDTO>>(resultado)!;
                    return lista.FirstOrDefault()!;
                }

                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener esquema por Id: {ex.Message}", ex);
            }
        }

        public async Task<List<EsquemaWhatsAppAsignacionDTO>> ListarAsync()
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "[mkt].[SP_TEsquemaWhatsAppAsignacion_Listar]",
                    null
                );

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<EsquemaWhatsAppAsignacionDTO>>(resultado)!;
                }

                return new List<EsquemaWhatsAppAsignacionDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar esquemas: {ex.Message}", ex);
            }
        }
    }
}
