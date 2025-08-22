using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PostulanteFormacionLogRepository : GenericRepository<TPostulanteFormacionLog>, IPostulanteFormacionLogRepository
    {
        private Mapper _mapper;
        public PostulanteFormacionLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulanteFormacionLog, PostulanteFormacionLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteFormacionLog, PostulanteFormacionLogDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteFormacionLog, TPostulanteFormacionLog>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPostulanteFormacionLog MapeoEntidad(PostulanteFormacionLog entidad)
        {
            try
            {
                TPostulanteFormacionLog modelo = _mapper.Map<TPostulanteFormacionLog>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteFormacionLog Add(PostulanteFormacionLog entidad)
        {
            try
            {
                var PostulanteFormacionLog = MapeoEntidad(entidad);
                base.Insert(PostulanteFormacionLog);
                return PostulanteFormacionLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteFormacionLog Update(PostulanteFormacionLog entidad)
        {
            try
            {
                var PostulanteFormacionLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PostulanteFormacionLog.RowVersion = entidadExistente.RowVersion;

                base.Update(PostulanteFormacionLog);
                return PostulanteFormacionLog;
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
        public IEnumerable<TPostulanteFormacionLog> Add(IEnumerable<PostulanteFormacionLog> listadoEntidad)
        {
            try
            {
                List<TPostulanteFormacionLog> listado = new List<TPostulanteFormacionLog>();
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
        public IEnumerable<TPostulanteFormacionLog> Update(IEnumerable<PostulanteFormacionLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPostulanteFormacionLog> listado = new List<TPostulanteFormacionLog>();
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
        /// Obtiene un registro de T_PostulanteFormacionLog por el Primary Key
        /// </summary>
        /// <returns>PostulanteFormacionLog o Nulo</returns>
        public PostulanteFormacionLog? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdPostulante,
		                IdPostulanteFormacion,
		                IdCentroEstudio,
		                IdTipoEstudio,
		                IdAreaFormacion,
		                IdEstadoEstudio,
		                FechaInicio,
		                FechaFin,
		                OtraInstitucion,
		                OtraCarrera,
		                AlaActualidad,
		                TurnoEstudio,
		                IdPais,
		                TipoActualizacion,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM gp.T_PostulanteFormacionLog
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteFormacionLog>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }

        /// Autor: Eliot Arias F.
		/// Fecha: 09/11/2024
		/// Version: 1.0
		/// <summary>
		/// Obtiene lista de historial formacion academica del postulante para el historial
		/// </summary>
		/// <param name="idPostulante"> id del postulante</param>
		/// <returns> IEnumerable<PostulanteFormacionLogDTO>  </returns>
		public IEnumerable<PostulanteFormacionLogDTO> ObtenerHistorialPostulanteFormacion(int idPostulante)
        {
            try
            {
                List<PostulanteFormacionLogDTO> lista = new List<PostulanteFormacionLogDTO>();
                string query = "gp.SP_ObtenerHistorialFormacionPostulante";
                var res = _dapperRepository.QuerySPDapper(query, new { IdPostulante = idPostulante });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PostulanteFormacionLogDTO>>(res)!;
                    return lista;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
