using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PostulanteEquipoComputoRepository : GenericRepository<TCriterioEvaluacionProceso>, IPostulanteEquipoComputoRepository
    {
        private Mapper _mapper;
        public PostulanteEquipoComputoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionProceso, PostulanteEquipoComputo>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteEquipoComputo, PostulanteEquipoComputoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteEquipoComputo, TCriterioEvaluacionProceso>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCriterioEvaluacionProceso MapeoEntidad(PostulanteEquipoComputo entidad)
        {
            try
            {
                TCriterioEvaluacionProceso modelo = _mapper.Map<TCriterioEvaluacionProceso>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCriterioEvaluacionProceso Add(PostulanteEquipoComputo entidad)
        {
            try
            {
                var PostulanteEquipoComputo = MapeoEntidad(entidad);
                base.Insert(PostulanteEquipoComputo);
                return PostulanteEquipoComputo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCriterioEvaluacionProceso Update(PostulanteEquipoComputo entidad)
        {
            try
            {
                var PostulanteEquipoComputo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PostulanteEquipoComputo.RowVersion = entidadExistente.RowVersion;

                base.Update(PostulanteEquipoComputo);
                return PostulanteEquipoComputo;
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
        public IEnumerable<TCriterioEvaluacionProceso> Add(IEnumerable<PostulanteEquipoComputo> listadoEntidad)
        {
            try
            {
                List<TCriterioEvaluacionProceso> listado = new List<TCriterioEvaluacionProceso>();
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
        public IEnumerable<TCriterioEvaluacionProceso> Update(IEnumerable<PostulanteEquipoComputo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCriterioEvaluacionProceso> listado = new List<TCriterioEvaluacionProceso>();
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
        /// Obtiene un registro de T_PostulanteEquipoComputo por el Primary Key
        /// </summary>
        /// <returns>PostulanteEquipoComputo o Nulo</returns>
        public PostulanteEquipoComputo? ObtenerPorId(int id)
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
                    FROM gp.T_PostulanteEquipoComputo
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteEquipoComputo>(resultado)!;
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
