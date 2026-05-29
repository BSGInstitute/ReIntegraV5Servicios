using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoSubTipoEncuestaRepository
    /// Autor: Junior Llerena
    /// Fecha: 2026-05-28
    /// <summary>
    /// Gestión general de T_TipoSubTipoEncuesta
    /// </summary>
    public class TipoSubTipoEncuestaRepository : GenericRepository<TTipoSubTipoEncuesta>, ITipoSubTipoEncuestaRepository
    {
        private Mapper _mapper;

        public TipoSubTipoEncuestaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoSubTipoEncuesta, TipoSubTipoEncuesta>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoSubTipoEncuesta MapeoEntidad(TipoSubTipoEncuesta entidad)
        {
            try
            {
                return _mapper.Map<TTipoSubTipoEncuesta>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoSubTipoEncuesta Add(TipoSubTipoEncuesta entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                base.Insert(modelo);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoSubTipoEncuesta Update(TipoSubTipoEncuesta entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                modelo.RowVersion = entidadExistente.RowVersion;
                base.Update(modelo);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros activos con nombres de tipo y subtipo
        /// </summary>
        public List<TipoSubTipoEncuestaDTO> ObtenerTodo()
        {
            try
            {
                var rpta = new List<TipoSubTipoEncuestaDTO>();
                var query = @"SELECT
                                  TST.Id,
                                  TST.IdTipoEncuesta,
                                  TE.Nombre  AS NombreTipoEncuesta,
                                  TST.IdSubTipoEncuesta,
                                  ST.Nombre  AS NombreSubTipoEncuesta,
                                  TST.Estado,
                                  TST.UsuarioCreacion,
                                  TST.UsuarioModificacion,
                                  TST.FechaCreacion,
                                  TST.FechaModificacion
                              FROM pla.T_TipoSubTipoEncuesta TST
                              INNER JOIN pla.T_TipoEncuesta    TE  ON TE.Id  = TST.IdTipoEncuesta
                              INNER JOIN pla.T_SubTipoEncuesta ST  ON ST.Id  = TST.IdSubTipoEncuesta
                              WHERE TST.Estado = 1
                              ORDER BY TE.Nombre, ST.Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null" && resultado != "[]")
                    rpta = JsonConvert.DeserializeObject<List<TipoSubTipoEncuestaDTO>>(resultado);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Version: 1.0
        /// <summary>
        /// Obtiene los subtipos asociados a un tipo de encuesta especifico
        /// </summary>
        public List<TipoSubTipoEncuestaDTO> ObtenerPorTipoEncuesta(int idTipoEncuesta)
        {
            try
            {
                var rpta = new List<TipoSubTipoEncuestaDTO>();
                var query = @"SELECT
                                  TST.Id,
                                  TST.IdTipoEncuesta,
                                  TE.Nombre  AS NombreTipoEncuesta,
                                  TST.IdSubTipoEncuesta,
                                  ST.Nombre  AS NombreSubTipoEncuesta,
                                  TST.Estado,
                                  TST.UsuarioCreacion,
                                  TST.UsuarioModificacion,
                                  TST.FechaCreacion,
                                  TST.FechaModificacion
                              FROM pla.T_TipoSubTipoEncuesta TST
                              INNER JOIN pla.T_TipoEncuesta    TE  ON TE.Id  = TST.IdTipoEncuesta
                              INNER JOIN pla.T_SubTipoEncuesta ST  ON ST.Id  = TST.IdSubTipoEncuesta
                              WHERE TST.Estado = 1
                                AND TST.IdTipoEncuesta = @IdTipoEncuesta
                              ORDER BY ST.Nombre";
                var resultado = _dapperRepository.QueryDapper(query, new { IdTipoEncuesta = idTipoEncuesta });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null" && resultado != "[]")
                    rpta = JsonConvert.DeserializeObject<List<TipoSubTipoEncuestaDTO>>(resultado);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Junior Llerena
        /// Fecha: 2026-05-28
        /// Version: 1.0
        /// <summary>
        /// Verifica si ya existe una asociacion activa entre tipo y subtipo
        /// </summary>
        public bool ExisteAsociacion(int idTipoEncuesta, int idSubTipoEncuesta)
        {
            try
            {
                var query = @"SELECT Id FROM pla.T_TipoSubTipoEncuesta
                              WHERE IdTipoEncuesta    = @IdTipoEncuesta
                                AND IdSubTipoEncuesta = @IdSubTipoEncuesta
                                AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdTipoEncuesta = idTipoEncuesta, IdSubTipoEncuesta = idSubTipoEncuesta });
                return !string.IsNullOrEmpty(resultado) && resultado != "null" && resultado != "[]";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
