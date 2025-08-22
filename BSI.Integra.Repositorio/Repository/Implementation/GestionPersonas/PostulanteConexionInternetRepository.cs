using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PostulanteConexionInternetRepository : GenericRepository<TPostulanteConexionInternet>, IPostulanteConexionInternetRepository
    {
        private Mapper _mapper;
        public PostulanteConexionInternetRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulanteConexionInternet, PostulanteConexionInternet>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteConexionInternet, PostulanteConexionInternetDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteConexionInternet, TPostulanteConexionInternet>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPostulanteConexionInternet MapeoEntidad(PostulanteConexionInternet entidad)
        {
            try
            {
                TPostulanteConexionInternet modelo = _mapper.Map<TPostulanteConexionInternet>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteConexionInternet Add(PostulanteConexionInternet entidad)
        {
            try
            {
                var PostulanteConexionInternet = MapeoEntidad(entidad);
                base.Insert(PostulanteConexionInternet);
                return PostulanteConexionInternet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteConexionInternet Update(PostulanteConexionInternet entidad)
        {
            try
            {
                var PostulanteConexionInternet = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PostulanteConexionInternet.RowVersion = entidadExistente.RowVersion;

                base.Update(PostulanteConexionInternet);
                return PostulanteConexionInternet;
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
        public IEnumerable<TPostulanteConexionInternet> Add(IEnumerable<PostulanteConexionInternet> listadoEntidad)
        {
            try
            {
                List<TPostulanteConexionInternet> listado = new List<TPostulanteConexionInternet>();
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
        public IEnumerable<TPostulanteConexionInternet> Update(IEnumerable<PostulanteConexionInternet> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPostulanteConexionInternet> listado = new List<TPostulanteConexionInternet>();
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
        /// Obtiene un registro de T_PostulanteConexionInternet por el Primary Key
        /// </summary>
        /// <returns>PostulanteConexionInternet o Nulo</returns>
        public PostulanteConexionInternet? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    IdPersonal,
	                    IdPostulante,
	                    IdExamen,
	                    IdProcesoSeleccion,
	                    EstadoExamen,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_PostulanteConexionInternet
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteConexionInternet>(resultado)!;
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
