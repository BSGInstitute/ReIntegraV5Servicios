using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PostulanteExperienciaLogRepository : GenericRepository<TPostulanteExperienciaLog>, IPostulanteExperienciaLogRepository
    {
        private Mapper _mapper;
        public PostulanteExperienciaLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulanteExperienciaLog, PostulanteExperienciaLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteExperienciaLog, PostulanteExperienciaLogDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteExperienciaLog, TPostulanteExperienciaLog>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPostulanteExperienciaLog MapeoEntidad(PostulanteExperienciaLog entidad)
        {
            try
            {
                TPostulanteExperienciaLog modelo = _mapper.Map<TPostulanteExperienciaLog>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteExperienciaLog Add(PostulanteExperienciaLog entidad)
        {
            try
            {
                var PostulanteExperienciaLog = MapeoEntidad(entidad);
                base.Insert(PostulanteExperienciaLog);
                return PostulanteExperienciaLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteExperienciaLog Update(PostulanteExperienciaLog entidad)
        {
            try
            {
                var PostulanteExperienciaLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PostulanteExperienciaLog.RowVersion = entidadExistente.RowVersion;

                base.Update(PostulanteExperienciaLog);
                return PostulanteExperienciaLog;
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
        public IEnumerable<TPostulanteExperienciaLog> Add(IEnumerable<PostulanteExperienciaLog> listadoEntidad)
        {
            try
            {
                List<TPostulanteExperienciaLog> listado = new List<TPostulanteExperienciaLog>();
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
        public IEnumerable<TPostulanteExperienciaLog> Update(IEnumerable<PostulanteExperienciaLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPostulanteExperienciaLog> listado = new List<TPostulanteExperienciaLog>();
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
        /// Obtiene un registro de T_PostulanteExperienciaLog por el Primary Key
        /// </summary>
        /// <returns>PostulanteExperienciaLog o Nulo</returns>
        public PostulanteExperienciaLog? ObtenerPorId(int id)
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
                    FROM gp.T_PostulanteExperienciaLog
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteExperienciaLog>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 08/11/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene una lista de registro de T_PostulanteExperienciaLog por el Primary Key
        /// </summary>
        /// <returns>Lista de PostulanteExperienciaLog o Nulo</returns>
        public IEnumerable<PostulanteExperienciaLogV2DTO> ObtenerHistorialPostulanteExperiencia(int idPostulante)
        {
            try
            {
                List<PostulanteExperienciaLogV2DTO> respuesta = new List<PostulanteExperienciaLogV2DTO>();
                var query = @"gp.SP_ObtenerHistorialExperienciaPostulante";
                var resultado = _dapperRepository.QuerySPDapper(query, new { idPostulante = idPostulante });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<PostulanteExperienciaLogV2DTO>>(resultado)!;
                    return respuesta;
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
