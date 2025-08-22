using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: FeedbackGrupoPreguntaProgramaGeneralRepository
    /// Autor: Klebert Layme.
    /// Fecha: 27/05/2023
    /// <summary>
    /// Gestión general de TFeedbackGrupoPreguntaProgramaGeneral
    /// </summary>
    public class FeedbackGrupoPreguntaProgramaGeneralRepository : GenericRepository<TFeedbackGrupoPreguntaProgramaGeneral>, IFeedbackGrupoPreguntaProgramaGeneralRepository
    {
        private Mapper _mapper;

        public FeedbackGrupoPreguntaProgramaGeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFeedbackGrupoPreguntaProgramaGeneral, FeedbackGrupoPreguntaProgramaGeneral>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFeedbackGrupoPreguntaProgramaGeneral MapeoEntidad(FeedbackGrupoPreguntaProgramaGeneral entidad)
        {
            try
            {
                //crea la entidad padre
                TFeedbackGrupoPreguntaProgramaGeneral modelo = _mapper.Map<TFeedbackGrupoPreguntaProgramaGeneral>(entidad);

                //mapea los hijos
                //if (entidad.AreaParametroSeoPw != null && entidad.AreaParametroSeoPw.Count > 0)
                //{
                //    modelo.TAreaParametroSeoPws = _mapper.Map<List<TAreaParametroSeoPw>>(entidad.AreaParametroSeoPw);
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFeedbackGrupoPreguntaProgramaGeneral Add(FeedbackGrupoPreguntaProgramaGeneral entidad)
        {
            try
            {
                var AreaCapacitacion = MapeoEntidad(entidad);
                base.Insert(AreaCapacitacion);
                return AreaCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFeedbackGrupoPreguntaProgramaGeneral Update(FeedbackGrupoPreguntaProgramaGeneral entidad)
        {
            try
            {
                var AreaCapacitacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AreaCapacitacion.RowVersion = entidadExistente.RowVersion;

                base.Update(AreaCapacitacion);
                return AreaCapacitacion;
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


        public IEnumerable<TFeedbackGrupoPreguntaProgramaGeneral> Add(IEnumerable<FeedbackGrupoPreguntaProgramaGeneral> listadoEntidad)
        {
            try
            {
                List<TFeedbackGrupoPreguntaProgramaGeneral> listado = new List<TFeedbackGrupoPreguntaProgramaGeneral>();
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

        public IEnumerable<TFeedbackGrupoPreguntaProgramaGeneral> Update(IEnumerable<FeedbackGrupoPreguntaProgramaGeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFeedbackGrupoPreguntaProgramaGeneral> listado = new List<TFeedbackGrupoPreguntaProgramaGeneral>();
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


        /// Autor: Klebert Layme.
        /// Fecha: 27/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FeedbackGrupoPreguntaProgramaGeneral por id.
        /// </summary>
        /// <returns> List<FeedbackConfigurarGrupoPreguntaDTO> </returns>
        public IEnumerable<FeedbackGrupoPreguntaProgramaGeneral> ObtenerPorIdFeedbackConfigurar(int IdFeedbackConfigurarGrupoPregunta)
        {
            try
            {
                IEnumerable<FeedbackGrupoPreguntaProgramaGeneral> rpta = new List<FeedbackGrupoPreguntaProgramaGeneral>();
                var query = @"
                    SELECT
	                    Id,IdFeedbackConfigurarGrupoPregunta,IdPGeneral,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion
                    FROM pla.T_FeedbackGrupoPreguntaProgramaGeneral
                    WHERE Estado = 1 AND IdFeedbackConfigurarGrupoPregunta=@IdFeedbackConfigurarGrupoPregunta";
                var resultado = _dapperRepository.QueryDapper(query, new { IdFeedbackConfigurarGrupoPregunta });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<FeedbackGrupoPreguntaProgramaGeneral>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
