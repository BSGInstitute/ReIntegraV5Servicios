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
    /// Repositorio para Actividad (vinculación número WhatsApp con esquema) usando Dapper
    /// </summary>
    public class EsquemaActividadRepository : IEsquemaActividadRepository
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly IConnectionFactory _connectionFactory;

        public EsquemaActividadRepository(IDapperRepository dapperRepository, IConnectionFactory connectionFactory)
        {
            _dapperRepository = dapperRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InsertarAsync(EsquemaActividadRequestDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@IdAsistenteMarketingWhatsAppAsignacion", entidad.IdAsistenteMarketingWhatsAppAsignacion, DbType.Int32);
                    parametros.Add("@IdEsquemaWhatsAppAsignacion", entidad.IdEsquemaWhatsAppAsignacion, DbType.Int32);
                    parametros.Add("@Estado", entidad.Estado ?? true, DbType.Boolean);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionActividad_Insertar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar actividad: {ex.Message}", ex);
            }
        }

        public async Task<int> ActualizarAsync(EsquemaActividadRequestDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Id", entidad.Id, DbType.Int32);
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@IdAsistenteMarketingWhatsAppAsignacion", entidad.IdAsistenteMarketingWhatsAppAsignacion, DbType.Int32);
                    parametros.Add("@IdEsquemaWhatsAppAsignacion", entidad.IdEsquemaWhatsAppAsignacion, DbType.Int32);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionActividad_Actualizar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar actividad: {ex.Message}", ex);
            }
        }

        public async Task<int> EliminarAsync(int id, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Id", id, DbType.Int32);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionActividad_Eliminar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar actividad: {ex.Message}", ex);
            }
        }

        public async Task<int> ActualizarEstadoAsync(EsquemaActividadEstadoDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Id", entidad.Id, DbType.Int32);
                    parametros.Add("@Estado", entidad.Estado, DbType.Boolean);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionActividad_ActualizarEstado]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar estado de actividad: {ex.Message}", ex);
            }
        }

        public async Task<List<EsquemaActividadDTO>> ListarAsync()
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "[mkt].[SP_TEsquemaWhatsAppAsignacionActividad_Listar]",
                    null
                );

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<EsquemaActividadDTO>>(resultado)!;
                }

                return new List<EsquemaActividadDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar actividades: {ex.Message}", ex);
            }
        }
    }
}
