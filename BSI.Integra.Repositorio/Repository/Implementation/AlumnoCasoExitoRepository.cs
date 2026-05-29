using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class AlumnoCasoExitoRepository : IAlumnoCasoExitoRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public AlumnoCasoExitoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public IEnumerable<AlumnoCasoExitoDTO> Obtener()
        {
            try
            {
                var rpta = new List<AlumnoCasoExitoDTO>();
                var query = @"
                    SELECT
                        ACE.Id,
                        ACE.Nombre,
                        ACE.TituloTestimonio,
                        ACE.FotoPerfil,
                        ACE.Testimonio,
                        ACE.IdPais,
                        P.NombrePais,
                        ACE.Posicion,
                        ACE.Visibilidad,
                        ACE.Estado,
                        ACE.UsuarioCreacion,
                        ACE.UsuarioModificacion,
                        ACE.FechaCreacion,
                        ACE.FechaModificacion,
                        ACE.IdMigracion
                    FROM [mkt].[T_AlumnoCasoExito] ACE
                    INNER JOIN [conf].[T_Pais] P ON P.Id = ACE.IdPais
                    WHERE ACE.Estado = 1
                    ORDER BY ACE.Posicion ASC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    rpta = JsonConvert.DeserializeObject<List<AlumnoCasoExitoDTO>>(resultado)!;
                return rpta;
            }
            catch (Exception ex) { throw ex; }
        }

        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var rpta = new List<ComboDTO>();
                var query = @"
                    SELECT ACE.Id, ACE.Nombre
                    FROM [mkt].[T_AlumnoCasoExito] ACE
                    WHERE ACE.Estado = 1 AND ACE.Visibilidad = 1
                    ORDER BY ACE.Posicion ASC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                return rpta;
            }
            catch (Exception ex) { throw ex; }
        }

        public AlumnoCasoExitoDTO? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
                        ACE.Id,
                        ACE.Nombre,
                        ACE.TituloTestimonio,
                        ACE.FotoPerfil,
                        ACE.Testimonio,
                        ACE.IdPais,
                        P.NombrePais,
                        ACE.Posicion,
                        ACE.Visibilidad,
                        ACE.Estado,
                        ACE.UsuarioCreacion,
                        ACE.UsuarioModificacion,
                        ACE.FechaCreacion,
                        ACE.FechaModificacion,
                        ACE.IdMigracion
                    FROM [mkt].[T_AlumnoCasoExito] ACE
                    INNER JOIN [conf].[T_Pais] P ON P.Id = ACE.IdPais
                    WHERE ACE.Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<AlumnoCasoExitoDTO>(resultado)!;
                return null;
            }
            catch (Exception ex) { throw new Exception("Error en ObtenerPorId()", ex); }
        }

        public int Insertar(AlumnoCasoExitoDTO dto, string usuario)
        {
            try
            {
                var query = @"
                    EXEC mkt.SP_TAlumnoCasoExito_Insertar
                        @Nombre              = @Nombre,
                        @TituloTestimonio    = @TituloTestimonio,
                        @FotoPerfil          = @FotoPerfil,
                        @Testimonio          = @Testimonio,
                        @IdPais              = @IdPais,
                        @Posicion            = @Posicion,
                        @Visibilidad         = @Visibilidad,
                        @UsuarioCreacion     = @UsuarioCreacion,
                        @UsuarioModificacion = @UsuarioModificacion,
                        @IdMigracion         = @IdMigracion";
                var resultado = _dapperRepository.FirstOrDefault(query, new
                {
                    dto.Nombre,
                    dto.TituloTestimonio,
                    dto.FotoPerfil,
                    dto.Testimonio,
                    dto.IdPais,
                    dto.Posicion,
                    dto.Visibilidad,
                    UsuarioCreacion     = usuario,
                    UsuarioModificacion = usuario,
                    IdMigracion         = (Guid?)null
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(resultado);
                    return (int)(data?.Id ?? 0);
                }
                return 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool Actualizar(AlumnoCasoExitoDTO dto, string usuario)
        {
            try
            {
                var query = @"
                    EXEC mkt.SP_TAlumnoCasoExito_Actualizar
                        @IdAlumnoCasoExito   = @IdAlumnoCasoExito,
                        @Nombre              = @Nombre,
                        @TituloTestimonio    = @TituloTestimonio,
                        @FotoPerfil          = @FotoPerfil,
                        @Testimonio          = @Testimonio,
                        @IdPais              = @IdPais,
                        @Posicion            = @Posicion,
                        @Visibilidad         = @Visibilidad,
                        @UsuarioModificacion = @UsuarioModificacion";
                _dapperRepository.QueryDapper(query, new
                {
                    IdAlumnoCasoExito   = dto.Id,
                    dto.Nombre,
                    dto.TituloTestimonio,
                    dto.FotoPerfil,
                    dto.Testimonio,
                    dto.IdPais,
                    dto.Posicion,
                    dto.Visibilidad,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var query = @"
                    EXEC mkt.SP_TAlumnoCasoExito_Eliminar
                        @IdAlumnoCasoExito   = @IdAlumnoCasoExito,
                        @UsuarioModificacion = @UsuarioModificacion";
                _dapperRepository.QueryDapper(query, new
                {
                    IdAlumnoCasoExito   = id,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool ActualizarVisibilidad(int id, bool estadoVisibilidad, string usuario)
        {
            try
            {
                var query = @"
                    EXEC mkt.SP_TAlumnoCasoExito_ActualizarVisibilidad
                        @IdAlumnoCasoExito   = @IdAlumnoCasoExito,
                        @Visibilidad         = @Visibilidad,
                        @UsuarioModificacion = @UsuarioModificacion";
                _dapperRepository.QueryDapper(query, new
                {
                    IdAlumnoCasoExito   = id,
                    Visibilidad         = estadoVisibilidad,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool ActualizarPosiciones(string jsonPosiciones, string usuario)
        {
            try
            {
                var query = @"
                    EXEC mkt.SP_TAlumnoCasoExito_ActualizarPosiciones
                        @JsonPosiciones      = @JsonPosiciones,
                        @UsuarioModificacion = @UsuarioModificacion";
                _dapperRepository.QueryDapper(query, new
                {
                    JsonPosiciones      = jsonPosiciones,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
