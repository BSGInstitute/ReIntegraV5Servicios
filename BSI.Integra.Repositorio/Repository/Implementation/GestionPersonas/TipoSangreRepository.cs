using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class TipoSangreRepository : GenericRepository<TTipoSangre>, ITipoSangreRepository
    {
        private Mapper _mapper;
        public TipoSangreRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoSangre, TipoSangre>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoSangre, TipoSangreDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoSangre, TTipoSangre>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoSangre MapeoEntidad(TipoSangre entidad)
        {
            try
            {
                TTipoSangre modelo = _mapper.Map<TTipoSangre>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoSangre Add(TipoSangre entidad)
        {
            try
            {
                var TipoSangre = MapeoEntidad(entidad);
                base.Insert(TipoSangre);
                return TipoSangre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoSangre Update(TipoSangre entidad)
        {
            try
            {
                var TipoSangre = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoSangre.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoSangre);
                return TipoSangre;
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
        public IEnumerable<TTipoSangre> Add(IEnumerable<TipoSangre> listadoEntidad)
        {
            try
            {
                List<TTipoSangre> listado = new List<TTipoSangre>();
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
        public IEnumerable<TTipoSangre> Update(IEnumerable<TipoSangre> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoSangre> listado = new List<TTipoSangre>();
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
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 07/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public IEnumerable<TipoSangreDTO> Obtener()
        {
            try
            {
                List<TipoSangreDTO> rpta = new List<TipoSangreDTO>();
                var query = @"
                    SELECT
	                    Id,TipoSangre
                    FROM gp.T_TipoSangre
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoSangreDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Marco Jose Villanueva Torres
        /// Fecha: 07/05/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns>TipoSangre || null</returns>
        public TipoSangre? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_TipoSangre
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TipoSangre>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EPS-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
