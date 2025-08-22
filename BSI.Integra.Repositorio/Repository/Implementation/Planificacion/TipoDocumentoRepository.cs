using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: TipoDocumentoRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_TipoDocumento
    /// </summary>
    public class TipoDocumentoRepository : GenericRepository<TTipoDocumento>, ITipoDocumentoRepository
    {
        private Mapper _mapper;

        public TipoDocumentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDocumento, TipoDocumento>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TTipoDocumento MapeoEntidad(TipoDocumento entidad)
        {
            try
            {
                TTipoDocumento modelo = _mapper.Map<TTipoDocumento>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoDocumento Add(TipoDocumento entidad)
        {
            try
            {
                var TipoDocumento = MapeoEntidad(entidad);
                base.Insert(TipoDocumento);
                return TipoDocumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoDocumento Update(TipoDocumento entidad)
        {
            try
            {
                var TipoDocumento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoDocumento.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoDocumento);
                return TipoDocumento;
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
        public IEnumerable<TTipoDocumento> Add(IEnumerable<TipoDocumento> listadoEntidad)
        {
            try
            {
                List<TTipoDocumento> listado = new List<TTipoDocumento>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TTipoDocumento> Update(IEnumerable<TipoDocumento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoDocumento> listado = new List<TTipoDocumento>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene TipoDocumento por id.
        /// </summary>
        /// <returns>TipoDocumento</returns>
        public TipoDocumento? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Clave,
	                    Descripcion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM	pla.T_TipoDocumento
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TipoDocumento>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#TDR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene TipoDocumento
        /// </summary>
        /// <returns>Lista TipoDocumentoDTO</returns>
        public IEnumerable<TipoDocumentoDTO> Obtener()
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Clave,
	                    Descripcion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_TipoDocumento
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<TipoDocumentoDTO>>(resultado)!;
                }
                return new List<TipoDocumentoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#TDR-OP-001@Error en Obtener(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene TipoDocumento
        /// </summary>
        /// <returns>Lista TipoDocumentoDTO</returns>
        public async Task<IEnumerable<TipoDocumentoDTO>> ObtenerAsync()
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Clave,
	                    Descripcion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_TipoDocumento
                    WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<TipoDocumentoDTO>>(resultado)!;
                }
                return new List<TipoDocumentoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#TDR-OP-001@Error en Obtener(), {ex.Message}");
            }
        }
    }
}



