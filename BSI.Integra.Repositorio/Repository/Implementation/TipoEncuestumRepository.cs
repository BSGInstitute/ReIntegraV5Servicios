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
    /// Repositorio: TipoEncuestumRepository
    /// Autor: Jorge Gamero
    /// Fecha: 26/13/2025
    /// <summary>
    /// Gestión general de T_TipoEncuesta
    /// </summary>
    public class TipoEncuestumRepository : GenericRepository<TTipoEncuestum>, ITipoEncuestumRepository
    {
        private Mapper _mapper;

        public TipoEncuestumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoEncuestum, TipoEncuesta>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoEncuestum MapeoEntidad(TipoEncuesta entidad)
        {
            try { return _mapper.Map<TTipoEncuestum>(entidad); }
            catch (Exception ex) { throw ex; }
        }

        public TTipoEncuestum Add(TipoEncuesta entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                base.Insert(modelo);
                return modelo;
            }
            catch (Exception ex) { throw ex; }
        }

        public TTipoEncuestum Update(TipoEncuesta entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                var existente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                modelo.RowVersion = existente.RowVersion;
                base.Update(modelo);
                return modelo;
            }
            catch (Exception ex) { throw ex; }
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
                    WHERE IdTipoEncuesta = @Id
                      AND Estado = 1";
                _dapperRepository.QueryDapper(queryAsociaciones, new { Id = id, Usuario = usuario });

                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        /// Autor: Jorge Gamero
        /// Fecha: 26/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene un TipoEncuesta por su Id
        /// </summary>
        public TipoEncuesta ObtenerPorId(int id)
        {
            try
            {
                var rpta = new TipoEncuesta();
                var query = @"SELECT Id, Nombre, Estado, UsuarioCreacion, UsuarioModificacion,
                                     FechaCreacion, FechaModificacion, RowVersion
                              FROM pla.T_TipoEncuesta WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    rpta = JsonConvert.DeserializeObject<TipoEncuesta>(resultado);
                return rpta;
            }
            catch (Exception ex) { throw ex; }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 26/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros activos de T_TipoEncuesta
        /// </summary>
        public List<TipoEncuestaDTO> ObtenerTodo()
        {
            try
            {
                var rpta = new List<TipoEncuestaDTO>();
                var query = @"SELECT Id, Nombre, Estado, UsuarioCreacion, UsuarioModificacion,
                                     FechaCreacion, FechaModificacion
                              FROM pla.T_TipoEncuesta
                              WHERE Estado = 1
                              ORDER BY Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null" && resultado != "[]")
                    rpta = JsonConvert.DeserializeObject<List<TipoEncuestaDTO>>(resultado);
                return rpta;
            }
            catch (Exception ex) { throw ex; }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 26/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla T_TipoEncuesta
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"	SELECT Id, Nombre FROM pla.T_TipoEncuesta WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 26/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la vista V_TModalidadCurso_Filtro
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerComboTipoModalidad()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"	SELECT Id, Nombre FROM pla.V_TModalidadCurso_Filtro WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
