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
    /// Repositorio para Interpretar Información del Contacto usando Dapper
    /// </summary>
    public class EsquemaInterpretarInformacionRepository : IEsquemaInterpretarInformacionRepository
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly IConnectionFactory _connectionFactory;

        public EsquemaInterpretarInformacionRepository(IDapperRepository dapperRepository, IConnectionFactory connectionFactory)
        {
            _dapperRepository = dapperRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InsertarAsync(EsquemaInterpretarInformacionRequestDTO entidad, string usuario)
        {
            try
            {
                // Armar string de IDs de clasificaciones separados por coma
                var clasificacionesIds = entidad.ClasificacionesIds != null && entidad.ClasificacionesIds.Any()
                    ? string.Join(",", entidad.ClasificacionesIds)
                    : null;

                // Armar string de subcategorías con formato especial
                // Formato: NombreSubcat|TieneFaseMaxima|TienePerfil|FasesIds|PerfilesIds;siguiente...
                var subcategoriasString = entidad.Subcategorias != null && entidad.Subcategorias.Any()
                    ? string.Join(";", entidad.Subcategorias.Select(s =>
                        {
                            var fasesIds = s.FasesIds != null && s.FasesIds.Any()
                                ? string.Join(",", s.FasesIds)
                                : string.Empty;

                            var perfilesIds = s.PerfilesIds != null && s.PerfilesIds.Any()
                                ? string.Join(",", s.PerfilesIds)
                                : string.Empty;

                            return $"{s.Nombre}|{(s.TieneFaseMaxima ? 1 : 0)}|{(s.TienePerfil ? 1 : 0)}|{fasesIds}|{perfilesIds}";
                        }))
                    : null;

                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdEsquemaWhatsAppAsignacion", entidad.IdEsquemaWhatsAppAsignacion, DbType.Int32);
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@ClasificacionesIds", clasificacionesIds, DbType.String);
                    parametros.Add("@Subcategorias", subcategoriasString, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionInterpretarInformacion_Insertar]",
                        parametros,
                        commandType: CommandType.StoredProcedure,
                        commandTimeout: 1200 // 20 minutos para transacciones complejas
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar interpretación de información: {ex.Message}", ex);
            }
        }

        public async Task<int> ActualizarAsync(EsquemaInterpretarInformacionRequestDTO entidad, string usuario)
        {
            try
            {
                // Armar string de IDs de clasificaciones separados por coma
                var clasificacionesIds = entidad.ClasificacionesIds != null && entidad.ClasificacionesIds.Any()
                    ? string.Join(",", entidad.ClasificacionesIds)
                    : null;

                // Armar string de subcategorías con formato especial
                var subcategoriasString = entidad.Subcategorias != null && entidad.Subcategorias.Any()
                    ? string.Join(";", entidad.Subcategorias.Select(s =>
                        {
                            var fasesIds = s.FasesIds != null && s.FasesIds.Any()
                                ? string.Join(",", s.FasesIds)
                                : string.Empty;

                            var perfilesIds = s.PerfilesIds != null && s.PerfilesIds.Any()
                                ? string.Join(",", s.PerfilesIds)
                                : string.Empty;

                            return $"{s.Nombre}|{(s.TieneFaseMaxima ? 1 : 0)}|{(s.TienePerfil ? 1 : 0)}|{fasesIds}|{perfilesIds}";
                        }))
                    : null;

                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Id", entidad.Id, DbType.Int32);
                    parametros.Add("@Nombre", entidad.Nombre, DbType.String);
                    parametros.Add("@ClasificacionesIds", clasificacionesIds, DbType.String);
                    parametros.Add("@Subcategorias", subcategoriasString, DbType.String);
                    parametros.Add("@Usuario", usuario, DbType.String);

                    var resultado = await conn.QueryFirstOrDefaultAsync<int>(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionInterpretarInformacion_Actualizar]",
                        parametros,
                        commandType: CommandType.StoredProcedure,
                        commandTimeout: 1200
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar interpretación de información: {ex.Message}", ex);
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
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionInterpretarInformacion_Eliminar]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar interpretación de información: {ex.Message}", ex);
            }
        }

        public async Task<EsquemaInterpretarInformacionDetalleDTO> ObtenerPorIdAsync(int id)
        {
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Id", id, DbType.Int32);

                    // Este SP retorna 5 result sets:
                    // 1. Interpretación de información
                    // 2. Clasificaciones asociadas
                    // 3. Subcategorías
                    // 4. Fases de subcategorías
                    // 5. Perfiles de subcategorías
                    using (var multi = await conn.QueryMultipleAsync(
                        "[mkt].[SP_TEsquemaWhatsAppAsignacionInterpretarInformacion_ObtenerPorId]",
                        parametros,
                        commandType: CommandType.StoredProcedure))
                    {
                        var interpretacion = await multi.ReadFirstOrDefaultAsync<EsquemaInterpretarInformacionDetalleDTO>();

                        if (interpretacion != null)
                        {
                            interpretacion.Clasificaciones = (await multi.ReadAsync<ClasificacionAsociadaDTO>()).ToList();
                            var subcategorias = (await multi.ReadAsync<SubcategoriaDetalleDTO>()).ToList();
                            var fases = (await multi.ReadAsync<FaseAsociadaDTO>()).ToList();
                            var perfiles = (await multi.ReadAsync<PerfilAsociadoDTO>()).ToList();

                            // Asociar fases y perfiles a cada subcategoría
                            foreach (var subcategoria in subcategorias)
                            {
                                subcategoria.Fases = fases.Where(f => f.IdEsquemaWhatsAppAsignacionSubcategoria == subcategoria.Id).ToList();
                                subcategoria.Perfiles = perfiles.Where(p => p.IdEsquemaWhatsAppAsignacionSubcategoria == subcategoria.Id).ToList();
                            }

                            interpretacion.Subcategorias = subcategorias;
                        }

                        return interpretacion!;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener interpretación de información por Id: {ex.Message}", ex);
            }
        }

        public async Task<List<EsquemaInterpretarInformacionDTO>> ListarPorEsquemaAsync(int idEsquemaWhatsAppAsignacion)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "[mkt].[SP_TEsquemaWhatsAppAsignacionInterpretarInformacion_ListarPorEsquema]",
                    new { IdEsquemaWhatsAppAsignacion = idEsquemaWhatsAppAsignacion }
                );

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<EsquemaInterpretarInformacionDTO>>(resultado)!;
                }

                return new List<EsquemaInterpretarInformacionDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar interpretaciones de información por esquema: {ex.Message}", ex);
            }
        }
    }
}
