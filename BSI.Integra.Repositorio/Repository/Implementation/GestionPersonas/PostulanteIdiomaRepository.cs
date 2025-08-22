using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PostulanteIdiomaRepository : GenericRepository<TPostulanteIdioma>, IPostulanteIdiomaRepository
    {
        private Mapper _mapper;
        public PostulanteIdiomaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulanteIdioma, PostulanteIdioma>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteIdioma, PostulanteIdiomaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteIdioma, TPostulanteIdioma>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPostulanteIdioma MapeoEntidad(PostulanteIdioma entidad)
        {
            try
            {
                TPostulanteIdioma modelo = _mapper.Map<TPostulanteIdioma>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteIdioma Add(PostulanteIdioma entidad)
        {
            try
            {
                var PostulanteIdioma = MapeoEntidad(entidad);
                base.Insert(PostulanteIdioma);
                return PostulanteIdioma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteIdioma Update(PostulanteIdioma entidad)
        {
            try
            {
                var PostulanteIdioma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PostulanteIdioma.RowVersion = entidadExistente.RowVersion;

                base.Update(PostulanteIdioma);
                return PostulanteIdioma;
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
        public IEnumerable<TPostulanteIdioma> Add(IEnumerable<PostulanteIdioma> listadoEntidad)
        {
            try
            {
                List<TPostulanteIdioma> listado = new List<TPostulanteIdioma>();
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
        public IEnumerable<TPostulanteIdioma> Update(IEnumerable<PostulanteIdioma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPostulanteIdioma> listado = new List<TPostulanteIdioma>();
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

        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_PostulanteIdioma por el Primary Key
        /// </summary>
        /// <returns>PostulanteIdioma o Nulo</returns>
        public PostulanteIdioma? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
	                    IdPostulante,
	                    IdIdioma,
	                    IdNivelIdioma,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_PostulanteIdioma
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteIdioma>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
