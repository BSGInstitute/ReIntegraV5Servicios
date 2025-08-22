using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ExamenRealizadoRespuestaEvaluadorRepository : GenericRepository<TExamenRealizadoRespuestaEvaluador>, IExamenRealizadoRespuestaEvaluadorRepository
    {
        private Mapper _mapper;
        public ExamenRealizadoRespuestaEvaluadorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExamenRealizadoRespuestaEvaluador, ExamenRealizadoRespuestaEvaluador>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenRealizadoRespuestaEvaluador, ExamenRealizadoRespuestaEvaluadorDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenRealizadoRespuestaEvaluador, TExamenRealizadoRespuestaEvaluador>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TExamenRealizadoRespuestaEvaluador MapeoEntidad(ExamenRealizadoRespuestaEvaluador entidad)
        {
            try
            {
                TExamenRealizadoRespuestaEvaluador modelo = _mapper.Map<TExamenRealizadoRespuestaEvaluador>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TExamenRealizadoRespuestaEvaluador Add(ExamenRealizadoRespuestaEvaluador entidad)
        {
            try
            {
                var ExamenRealizadoRespuestaEvaluador = MapeoEntidad(entidad);
                base.Insert(ExamenRealizadoRespuestaEvaluador);
                return ExamenRealizadoRespuestaEvaluador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TExamenRealizadoRespuestaEvaluador Update(ExamenRealizadoRespuestaEvaluador entidad)
        {
            try
            {
                var ExamenRealizadoRespuestaEvaluador = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ExamenRealizadoRespuestaEvaluador.RowVersion = entidadExistente.RowVersion;

                base.Update(ExamenRealizadoRespuestaEvaluador);
                return ExamenRealizadoRespuestaEvaluador;
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
        public IEnumerable<TExamenRealizadoRespuestaEvaluador> Add(IEnumerable<ExamenRealizadoRespuestaEvaluador> listadoEntidad)
        {
            try
            {
                List<TExamenRealizadoRespuestaEvaluador> listado = new List<TExamenRealizadoRespuestaEvaluador>();
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
        public IEnumerable<TExamenRealizadoRespuestaEvaluador> Update(IEnumerable<ExamenRealizadoRespuestaEvaluador> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TExamenRealizadoRespuestaEvaluador> listado = new List<TExamenRealizadoRespuestaEvaluador>();
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
        /// Obtiene un registro de T_ExamenRealizadoRespuestaEvaluador por el Primary Key
        /// </summary>
        /// <returns>ExamenRealizadoRespuestaEvaluador o Nulo</returns>
        public ExamenRealizadoRespuestaEvaluador? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdExamenAsignadoEvaluador,
		                IdPregunta,
		                IdRespuestaPregunta,
		                TextoRespuesta,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM gp.T_ExamenRealizadoRespuestaEvaluador
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ExamenRealizadoRespuestaEvaluador>(resultado)!;
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
