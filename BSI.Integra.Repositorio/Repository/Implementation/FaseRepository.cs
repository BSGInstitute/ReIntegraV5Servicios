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
    /// Repositorio para Fases (catálogo global) usando Dapper
    /// </summary>
    public class FaseRepository : IFaseRepository
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly IConnectionFactory _connectionFactory;

        public FaseRepository(IDapperRepository dapperRepository, IConnectionFactory connectionFactory)
        {
            _dapperRepository = dapperRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InsertarAsync(FaseRequestDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionFase_Insertar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar fase: {ex.Message}", ex);
            }
        }

        public async Task<int> ActualizarAsync(FaseRequestDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacionFase", entidad.Id, DbType.Int32);
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionFase_Actualizar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar fase: {ex.Message}", ex);
            }
        }

        public async Task<int> EliminarAsync(int id, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacionFase", id, DbType.Int32);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionFase_Eliminar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar fase: {ex.Message}", ex);
            }
        }

        public async Task<List<FaseDTO>> ListarAsync()
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "[mkt].[SP_TEsquemaWhatsAppAsignacionFase_Listar]",
                    null
                );

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<FaseDTO>>(resultado)!;
                }

                return new List<FaseDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar fases: {ex.Message}", ex);
            }
        }
    }
}
