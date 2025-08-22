using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;


namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: FeedbackGrupoPreguntaProgramaEspecificoRepository
    /// Autor: Klebert Layme.
    /// Fecha: 27/05/2023
    /// <summary>
    /// Gestión general de TFeedbackGrupoPreguntaProgramaEspecifico
    /// </summary>
    public class FeedbackGrupoPreguntaProgramaEspecificoRepository : GenericRepository<TFeedbackGrupoPreguntaProgramaEspecifico>, IFeedbackGrupoPreguntaProgramaEspecificoRepository
    {
        private Mapper _mapper;

        public FeedbackGrupoPreguntaProgramaEspecificoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFeedbackGrupoPreguntaProgramaEspecifico, FeedbackGrupoPreguntaProgramaEspecifico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFeedbackGrupoPreguntaProgramaEspecifico MapeoEntidad(FeedbackGrupoPreguntaProgramaEspecifico entidad)
        {
            try
            {
                //crea la entidad padre
                TFeedbackGrupoPreguntaProgramaEspecifico modelo = _mapper.Map<TFeedbackGrupoPreguntaProgramaEspecifico>(entidad);

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

        public TFeedbackGrupoPreguntaProgramaEspecifico Add(FeedbackGrupoPreguntaProgramaEspecifico entidad)
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

        public TFeedbackGrupoPreguntaProgramaEspecifico Update(FeedbackGrupoPreguntaProgramaEspecifico entidad)
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


        public IEnumerable<TFeedbackGrupoPreguntaProgramaEspecifico> Add(IEnumerable<FeedbackGrupoPreguntaProgramaEspecifico> listadoEntidad)
        {
            try
            {
                List<TFeedbackGrupoPreguntaProgramaEspecifico> listado = new List<TFeedbackGrupoPreguntaProgramaEspecifico>();
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

        public IEnumerable<TFeedbackGrupoPreguntaProgramaEspecifico> Update(IEnumerable<FeedbackGrupoPreguntaProgramaEspecifico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFeedbackGrupoPreguntaProgramaEspecifico> listado = new List<TFeedbackGrupoPreguntaProgramaEspecifico>();
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
        /// Obtiene todos los registros de T_FeedbackGrupoPreguntaProgramaEspecifico por id.
        /// </summary>
        /// <returns> FeedbackGrupoPreguntaProgramaEspecifico </returns>
        public IEnumerable<FeedbackGrupoPreguntaProgramaEspecifico> ObtenerPorIdFeedbackConfigurar(int IdFeedbackConfigurarGrupoPregunta)
        {
            try
            {
                IEnumerable<FeedbackGrupoPreguntaProgramaEspecifico> rpta = new List<FeedbackGrupoPreguntaProgramaEspecifico>();
                var query = @"
                    SELECT
	                    Id,IdFeedbackConfigurarGrupoPregunta,IdPEspecifico,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion
                    FROM pla.T_FeedbackGrupoPreguntaProgramaEspecifico
                    WHERE Estado = 1 AND IdFeedbackConfigurarGrupoPregunta=@IdFeedbackConfigurarGrupoPregunta";
                var resultado = _dapperRepository.QueryDapper(query, new { IdFeedbackConfigurarGrupoPregunta });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<FeedbackGrupoPreguntaProgramaEspecifico>>(resultado)!;
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
