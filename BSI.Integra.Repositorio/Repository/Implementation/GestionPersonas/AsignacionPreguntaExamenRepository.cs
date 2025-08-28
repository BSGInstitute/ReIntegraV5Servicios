using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;
using System.Linq.Expressions;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class AsignacionPreguntaExamenRepository : GenericRepository<TAsignacionPreguntaExaman>, IAsignacionPreguntaExamenRepository
    {
        private Mapper _mapper;
        public AsignacionPreguntaExamenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionPreguntaExaman, AsignacionPreguntaExamen>(MemberList.None).ReverseMap();
                cfg.CreateMap<AsignacionPreguntaExamen, AsignacionPreguntaExamenDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<AsignacionPreguntaExamen, TAsignacionPreguntaExaman>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        /*private TCentil MapeoEntidad(AsignacionPreguntaExamen entidad)
        {
            try
            {
                TCentil modelo = _mapper.Map<TCentil>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCentil Add(AsignacionPreguntaExamen entidad)
        {
            try
            {
                var AsignacionPreguntaExamen = MapeoEntidad(entidad);
                Insert(AsignacionPreguntaExamen);
                return AsignacionPreguntaExamen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public TCentil Update(AsignacionPreguntaExamen entidad)
        {
            try
            {
                var AsignacionPreguntaExamen = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsignacionPreguntaExamen.RowVersion = entidadExistente.RowVersion;

                Update(AsignacionPreguntaExamen);
                return AsignacionPreguntaExamen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public IEnumerable<TCentil> Add(IEnumerable<AsignacionPreguntaExamen> listadoEntidad)
        {
            try
            {
                List<TCentil> listado = new List<TCentil>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TCentil> Update(IEnumerable<AsignacionPreguntaExamen> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCentil> listado = new List<TCentil>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        private TAsignacionPreguntaExaman MapeoEntidad(AsignacionPreguntaExamen entidad)
        {
            try
            {
                TAsignacionPreguntaExaman modelo = _mapper.Map<TAsignacionPreguntaExaman>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TAsignacionPreguntaExaman Add(AsignacionPreguntaExamen entidad)
        {
            try
            {
                var AsignacionPreguntaExamen = MapeoEntidad(entidad);
                Insert(AsignacionPreguntaExamen);
                return AsignacionPreguntaExamen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsignacionPreguntaExaman Update(AsignacionPreguntaExamen entidad)
        {
            try
            {
                var AsignacionPreguntaExamen = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsignacionPreguntaExamen.RowVersion = entidadExistente.RowVersion;

                Update(AsignacionPreguntaExamen);
                return AsignacionPreguntaExamen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TAsignacionPreguntaExaman> Add(IEnumerable<AsignacionPreguntaExamen> listadoEntidad)
        {
            try
            {
                List<TAsignacionPreguntaExaman> listado = new List<TAsignacionPreguntaExaman>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TAsignacionPreguntaExaman> Update(IEnumerable<AsignacionPreguntaExamen> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAsignacionPreguntaExaman> listado = new List<TAsignacionPreguntaExaman>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
                return listado;
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

        public AsignacionPreguntaExamen ObtenerExamenPregunta(Expression<Func<TAsignacionPreguntaExaman, bool>> filter)
        {
            try
            {
                TAsignacionPreguntaExaman entidad = base.FirstBy(filter);
                AsignacionPreguntaExamen Rp = _mapper.Map<AsignacionPreguntaExamen>(entidad);
                return Rp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene centiles asignados a un grupo de evaluacion
        /// </summary>
        /// <param name="idExamen"></param>
        /// <returns></returns>
        public int ObtenerCantidadPreguntaExamen(int idExamen)
        {
            try
            {
                var query = @"SELECT COUNT(*) AS Valor FROM gp.T_AsignacionPreguntaExamen WHERE Estado = 1 AND IdExamen = @IdExamen";

                var resultado = _dapperRepository.FirstOrDefault(query, new { IdExamen = idExamen });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    var rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                    return rpta.Valor.GetValueOrDefault();
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<AsignacionPreguntaExamen> ObtenerAsignacionesPreguntaExamenbyId(int IdPregunta)
        {
            try {
                var query = @"SELECT 
                                     Id,
                                     IdExamen,
                                     IdPregunta,
                                     NroOrden,
                                     Puntaje 
                             FROM gp.T_AsignacionPreguntaExamen WHERE Estado = 1 AND IdPregunta = @IdPregunta";

                var respuesta = _dapperRepository.QueryDapper(query, new { IdPregunta = IdPregunta });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<AsignacionPreguntaExamen>>(respuesta)!;
                }
                return new List<AsignacionPreguntaExamen>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsignacionPreguntaExamen ObtenerPorId(int idExamen) {
            try
            {
                var query = @"SELECT idExamen,idPregunta,NroOrden,Puntaje FROM gp.T_AsignacionPreguntaExamen WHERE Estado = 1 AND IdExamen = @idExamen";

                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = idExamen });
                if (!string.IsNullOrEmpty(resultado) && resultado != null)
                {
                    return JsonConvert.DeserializeObject<AsignacionPreguntaExamen>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CPCDR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        public List<AsignacionPreguntaExamen> ObtenerPorIdPregunta(int idPregunta)
        {
            try
            {
                var query = @"SELECT Id,IdExamen,IdPregunta,NroOrden,Puntaje 
                            FROM gp.T_AsignacionPreguntaExamen WHERE Estado = 1 AND IdPregunta = @idPregunta";

                var respuesta = _dapperRepository.QueryDapper(query, new { IdPregunta = idPregunta });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<AsignacionPreguntaExamen>>(respuesta)!;
                }
                return new List<AsignacionPreguntaExamen>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CPCDR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
    }
}
