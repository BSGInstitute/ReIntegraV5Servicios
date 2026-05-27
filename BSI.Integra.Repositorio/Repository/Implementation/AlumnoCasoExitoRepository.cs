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
                        ACE.NombreAlumno,
                        ACE.NombrePrograma,
                        ACE.FotoPerfil,
                        ACE.FotoPerfilAlf,
                        ACE.Testimonio,
                        ACE.IdPais,
                        P.NombrePais,
                        ACE.Posicion,
                        ACE.EstadoVisibilidad,
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
                    SELECT ACE.Id, ACE.NombreAlumno AS Nombre
                    FROM [mkt].[T_AlumnoCasoExito] ACE
                    WHERE ACE.Estado = 1 AND ACE.EstadoVisibilidad = 1
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
                        ACE.NombreAlumno,
                        ACE.NombrePrograma,
                        ACE.FotoPerfil,
                        ACE.FotoPerfilAlf,
                        ACE.Testimonio,
                        ACE.IdPais,
                        P.NombrePais,
                        ACE.Posicion,
                        ACE.EstadoVisibilidad,
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
                        @NombreAlumno        = @NombreAlumno,
                        @NombrePrograma      = @NombrePrograma,
                        @FotoPerfil          = @FotoPerfil,
                        @FotoPerfilAlf       = @FotoPerfilAlf,
                        @Testimonio          = @Testimonio,
                        @IdPais              = @IdPais,
                        @Posicion            = @Posicion,
                        @EstadoVisibilidad   = @EstadoVisibilidad,
                        @UsuarioCreacion     = @UsuarioCreacion,
                        @UsuarioModificacion = @UsuarioModificacion,
                        @IdMigracion         = @IdMigracion";
                var resultado = _dapperRepository.FirstOrDefault(query, new
                {
                    dto.NombreAlumno,
                    dto.NombrePrograma,
                    dto.FotoPerfil,
                    dto.FotoPerfilAlf,
                    dto.Testimonio,
                    dto.IdPais,
                    dto.Posicion,
                    dto.EstadoVisibilidad,
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
                        @IdCasoExitoAlumno   = @IdCasoExitoAlumno,
                        @NombreAlumno        = @NombreAlumno,
                        @NombrePrograma      = @NombrePrograma,
                        @FotoPerfil          = @FotoPerfil,
                        @FotoPerfilAlf       = @FotoPerfilAlf,
                        @Testimonio          = @Testimonio,
                        @IdPais              = @IdPais,
                        @Posicion            = @Posicion,
                        @EstadoVisibilidad   = @EstadoVisibilidad,
                        @UsuarioModificacion = @UsuarioModificacion";
                _dapperRepository.QueryDapper(query, new
                {
                    IdCasoExitoAlumno   = dto.Id,
                    dto.NombreAlumno,
                    dto.NombrePrograma,
                    dto.FotoPerfil,
                    dto.FotoPerfilAlf,
                    dto.Testimonio,
                    dto.IdPais,
                    dto.Posicion,
                    dto.EstadoVisibilidad,
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
                        @IdCasoExitoAlumno   = @IdCasoExitoAlumno,
                        @UsuarioModificacion = @UsuarioModificacion";
                _dapperRepository.QueryDapper(query, new
                {
                    IdCasoExitoAlumno   = id,
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
                        @IdCasoExitoAlumno   = @IdCasoExitoAlumno,
                        @EstadoVisibilidad   = @EstadoVisibilidad,
                        @UsuarioModificacion = @UsuarioModificacion";
                _dapperRepository.QueryDapper(query, new
                {
                    IdCasoExitoAlumno   = id,
                    EstadoVisibilidad   = estadoVisibilidad,
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
