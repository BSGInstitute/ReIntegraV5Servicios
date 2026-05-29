using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SubTipoEncuestaRepository
    /// Autor:  Junior Llerena
    /// Fecha: 2026-05-28
    /// <summary>
    /// Gestión general de T_SubTipoEncuesta
    /// </summary>
    public class SubTipoEncuestaRepository : GenericRepository<TSubTipoEncuesta>, ISubTipoEncuestaRepository
    {
        private Mapper _mapper;

        public SubTipoEncuestaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSubTipoEncuesta, SubTipoEncuesta>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSubTipoEncuesta MapeoEntidad(SubTipoEncuesta entidad)
        {
            try
            {
                return _mapper.Map<TSubTipoEncuesta>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSubTipoEncuesta Add(SubTipoEncuesta entidad)
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

        public TSubTipoEncuesta Update(SubTipoEncuesta entidad)
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
                var queryAsociaciones = @"
                    UPDATE pla.T_TipoSubTipoEncuesta
                    SET Estado              = 0,
                        UsuarioModificacion = @Usuario,
                        FechaModificacion   = GETDATE()
                    WHERE IdSubTipoEncuesta = @Id
                      AND Estado = 1";
                _dapperRepository.QueryDapper(queryAsociaciones, new { Id = id, Usuario = usuario });

                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor:  Junior Llerena
        /// Fecha: 2026-05-28
        /// Version: 1.0
        /// <summary>
        /// Obtiene un SubTipoEncuesta por su Id
        /// </summary>
        public SubTipoEncuesta ObtenerPorId(int id)
        {
            try
            {
                var rpta = new SubTipoEncuesta();
                var query = @"SELECT Id, Nombre, Estado, UsuarioCreacion, UsuarioModificacion,
                                     FechaCreacion, FechaModificacion, RowVersion
                              FROM pla.T_SubTipoEncuesta
                              WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    rpta = JsonConvert.DeserializeObject<SubTipoEncuesta>(resultado);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:  Junior Llerena
        /// Fecha: 2026-05-28
        /// Version: 1.0
        /// <summary>
        /// Obtiene combo Id/Nombre de SubTipoEncuesta activos
        /// </summary>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre FROM pla.T_SubTipoEncuesta WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                return comboDTOs;
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
        /// Obtiene todos los registros activos de T_SubTipoEncuesta
        /// </summary>
        public List<SubTipoEncuestaDTO> ObtenerTodo()
        {
            try
            {
                var rpta = new List<SubTipoEncuestaDTO>();
                var query = @"SELECT Id, Nombre, Estado, UsuarioCreacion, UsuarioModificacion,
                                     FechaCreacion, FechaModificacion
                              FROM pla.T_SubTipoEncuesta
                              WHERE Estado = 1
                              ORDER BY Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null" && resultado != "[]")
                    rpta = JsonConvert.DeserializeObject<List<SubTipoEncuestaDTO>>(resultado);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
