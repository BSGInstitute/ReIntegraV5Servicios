using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PostulanteComparacionRepository : GenericRepository<TPostulanteComparacion>, IPostulanteComparacionRepository
    {
        private Mapper _mapper;
        public PostulanteComparacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulanteComparacion, PostulanteComparacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteComparacion, TPostulanteComparacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPostulanteComparacion MapeoEntidad(PostulanteComparacion entidad)
        {
            try
            {
                TPostulanteComparacion modelo = _mapper.Map<TPostulanteComparacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteComparacion Add(PostulanteComparacion entidad)
        {
            try
            {
                var PostulanteComparacion = MapeoEntidad(entidad);
                base.Insert(PostulanteComparacion);
                return PostulanteComparacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteComparacion Update(PostulanteComparacion entidad)
        {
            try
            {
                var PostulanteComparacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PostulanteComparacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PostulanteComparacion);
                return PostulanteComparacion;
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
        public IEnumerable<TPostulanteComparacion> Add(IEnumerable<PostulanteComparacion> listadoEntidad)
        {
            try
            {
                List<TPostulanteComparacion> listado = new List<TPostulanteComparacion>();
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
        public IEnumerable<TPostulanteComparacion> Update(IEnumerable<PostulanteComparacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPostulanteComparacion> listado = new List<TPostulanteComparacion>();
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

        /// Autor:Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns>PostulanteComparacion || null</returns>
        public PostulanteComparacion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
		                IdPostulante,
		                IdGrupoComparacionProcesoSeleccion,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM gp.T_PostulanteComparacion
                    WHERE Id=@id AND Estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteComparacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PCR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor:Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <param name="idGrupoComparacion"> (PK) </param> 
        /// <summary>
        /// Obtiene los idsPostulantes por id grupo comparacion
        /// </summary>
        /// <returns>Ids postulantes</returns>
        public List<int> ObtenerIdsPostulantesPorIdGrupoComparacion(int idGrupoComparacion)
        {
            try
            {
                var query = @"
                    SELECT
		                IdPostulante AS Valor
                    FROM gp.T_PostulanteComparacion
                    WHERE Estado=1 AND IdGrupoComparacionProcesoSeleccion = @IdGrupoComparacion
                    GROUP BY IdPostulante";
                var resultado = _dapperRepository.QueryDapper(query, new { IdGrupoComparacion = idGrupoComparacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    var rpta = JsonConvert.DeserializeObject<List<IntDTO>>(resultado)!;
                    return rpta.Select(x => x.Valor!.Value).ToList();
                }
                return new List<int>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerIdsPostulantesPorIdGrupoComparacion(), {ex.Message}");
            }
        }
    }
}
