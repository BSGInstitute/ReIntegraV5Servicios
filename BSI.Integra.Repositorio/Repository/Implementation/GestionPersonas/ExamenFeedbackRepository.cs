using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ExamenFeedbackRepository : GenericRepository<TExamenFeedback>, IExamenFeedbackRepository
    {
        private Mapper _mapper;
        public ExamenFeedbackRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExamenFeedback, ExamenFeedback>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenFeedback, ExamenFeedbackDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenFeedback, TExamenFeedback>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TExamenFeedback MapeoEntidad(ExamenFeedback entidad)
        {
            try
            {
                TExamenFeedback modelo = _mapper.Map<TExamenFeedback>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TExamenFeedback Add(ExamenFeedback entidad)
        {
            try
            {
                var ExamenFeedback = MapeoEntidad(entidad);
                base.Insert(ExamenFeedback);
                return ExamenFeedback;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TExamenFeedback Update(ExamenFeedback entidad)
        {
            try
            {
                var ExamenFeedback = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ExamenFeedback.RowVersion = entidadExistente.RowVersion;

                base.Update(ExamenFeedback);
                return ExamenFeedback;
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
        public IEnumerable<TExamenFeedback> Add(IEnumerable<ExamenFeedback> listadoEntidad)
        {
            try
            {
                List<TExamenFeedback> listado = new List<TExamenFeedback>();
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
        public IEnumerable<TExamenFeedback> Update(IEnumerable<ExamenFeedback> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TExamenFeedback> listado = new List<TExamenFeedback>();
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
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public IEnumerable<ExamenFeedbackDTO> Obtener()
        {
            try
            {
                List<ExamenFeedbackDTO> rpta = new List<ExamenFeedbackDTO>();
                var query = @"
                    SELECT
	                    Id, Nombre, Url
                    FROM gp.T_ExamenFeedback
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ExamenFeedbackDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns>ExamenFeedback || null</returns>
        public ExamenFeedback? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
                        Url,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_ExamenFeedback
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ExamenFeedback>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EFS-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
