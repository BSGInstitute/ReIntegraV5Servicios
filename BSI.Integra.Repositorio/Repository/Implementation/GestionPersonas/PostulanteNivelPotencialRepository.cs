using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PostulanteNivelPotencialRepository : GenericRepository<TPostulanteNivelPotencial>, IPostulanteNivelPotencialRepository
    {
        private Mapper _mapper;
        public PostulanteNivelPotencialRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulanteNivelPotencial, PostulanteNivelPotencial>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteNivelPotencial, PostulanteNivelPotencialDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteNivelPotencial, TPostulanteNivelPotencial>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPostulanteNivelPotencial MapeoEntidad(PostulanteNivelPotencial entidad)
        {
            try
            {
                TPostulanteNivelPotencial modelo = _mapper.Map<TPostulanteNivelPotencial>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteNivelPotencial Add(PostulanteNivelPotencial entidad)
        {
            try
            {
                var PostulanteNivelPotencial = MapeoEntidad(entidad);
                base.Insert(PostulanteNivelPotencial);
                return PostulanteNivelPotencial;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteNivelPotencial Update(PostulanteNivelPotencial entidad)
        {
            try
            {
                var PostulanteNivelPotencial = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PostulanteNivelPotencial.RowVersion = entidadExistente.RowVersion;

                base.Update(PostulanteNivelPotencial);
                return PostulanteNivelPotencial;
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
        public IEnumerable<TPostulanteNivelPotencial> Add(IEnumerable<PostulanteNivelPotencial> listadoEntidad)
        {
            try
            {
                List<TPostulanteNivelPotencial> listado = new List<TPostulanteNivelPotencial>();
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
        public IEnumerable<TPostulanteNivelPotencial> Update(IEnumerable<PostulanteNivelPotencial> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPostulanteNivelPotencial> listado = new List<TPostulanteNivelPotencial>();
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
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de .T_PostulanteNivelPotencial.
        /// </summary>
        /// <returns> List<PostulanteNivelPotencial> </returns>
        public IEnumerable<PostulanteNivelPotencialDTO> Obtener()
        {
            try
            {
                List<PostulanteNivelPotencialDTO> rpta = new List<PostulanteNivelPotencialDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_PostulanteNivelPotencial
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PostulanteNivelPotencialDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Marco Jose Villanueva Torres
        /// Fecha: 30/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns>PostulanteNivelPotencial || null</returns>
        public PostulanteNivelPotencial? ObtenerPorId(int id)
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
                    FROM gp.T_PostulanteNivelPotencial
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteNivelPotencial>(resultado)!;
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
