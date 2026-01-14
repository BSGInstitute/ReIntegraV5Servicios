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
    /// Repositorio para Perfiles (catálogo global) usando Dapper
    /// </summary>
    public class PerfilRepository : IPerfilRepository
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly IConnectionFactory _connectionFactory;

        public PerfilRepository(IDapperRepository dapperRepository, IConnectionFactory connectionFactory)
        {
            _dapperRepository = dapperRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InsertarAsync(PerfilRequestDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionPerfil_Insertar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar perfil: {ex.Message}", ex);
            }
        }

        public async Task<int> ActualizarAsync(PerfilRequestDTO entidad, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacionPerfil", entidad.Id, DbType.Int32);
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionPerfil_Actualizar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar perfil: {ex.Message}", ex);
            }
        }

        public async Task<int> EliminarAsync(int id, string usuario)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacionPerfil", id, DbType.Int32);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionPerfil_Eliminar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar perfil: {ex.Message}", ex);
            }
        }

        public async Task<List<PerfilDTO>> ListarAsync()
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "[mkt].[SP_TEsquemaWhatsAppAsignacionPerfil_Listar]",
                    null
                );

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<PerfilDTO>>(resultado)!;
                }

                return new List<PerfilDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar perfiles: {ex.Message}", ex);
            }
        }
    }
}
