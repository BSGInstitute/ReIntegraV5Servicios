using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class FeedbackConfigurarDetalleRepository: GenericRepository<TFeedbackConfigurarDetalle>, IFeedbackConfigurarDetalleRepository
    {
        private Mapper _mapper;



        public FeedbackConfigurarDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFeedbackConfigurarDetalle, FeedbackConfigurarDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFeedbackConfigurarDetalle, FeedbackConfigurarDetalleDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }



        #region Metodos Base
        private TFeedbackConfigurarDetalle MapeoEntidad(FeedbackConfigurarDetalle entidad)
        {
            try
            {
     
                TFeedbackConfigurarDetalle modelo = _mapper.Map<TFeedbackConfigurarDetalle>(entidad);

            

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFeedbackConfigurarDetalle Add(FeedbackConfigurarDetalle entidad)
        {
            try
            {
                var feedbackConfigurarDetalle = MapeoEntidad(entidad);
                base.Insert(feedbackConfigurarDetalle);
                return feedbackConfigurarDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFeedbackConfigurarDetalle Update(FeedbackConfigurarDetalle entidad)
        {
            try
            {
                var feedbackConfigurarDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                feedbackConfigurarDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(feedbackConfigurarDetalle);
                return feedbackConfigurarDetalle;
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


        public IEnumerable<TFeedbackConfigurarDetalle> Add(IEnumerable<FeedbackConfigurarDetalle> listadoEntidad)
        {
            try
            {
                List<TFeedbackConfigurarDetalle> listado = new List<TFeedbackConfigurarDetalle>();
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

        public IEnumerable<TFeedbackConfigurarDetalle> Update(IEnumerable<FeedbackConfigurarDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFeedbackConfigurarDetalle> listado = new List<TFeedbackConfigurarDetalle>();
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



        public IEnumerable<FeedbackConfigurarDetalle> ObtenerDetallePorIdFeedbackConfigurar(int idFeedbackConfigurar)
        {
            try
            {
                var query = @"
                         SELECT 
                        Id,
                        IdFeedbackConfigurar,
                        IdSexo,
                        Puntaje,
                        NombreVideo,
                        OrdenVideo,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion
                    FROM 
                        pla.T_FeedbackConfigurarDetalle
                        WHERE Estado = 1 AND IdFeedbackConfigurar=@idFeedbackConfigurar";
                var resultado = _dapperRepository.QueryDapper(query, new { idFeedbackConfigurar });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<FeedbackConfigurarDetalle>>(resultado)!;
                }
                return new List<FeedbackConfigurarDetalle>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PCPwR-OPIP-001@Error en ObtenerDetallePorIdFeedbackConfigurar(), {ex.Message}");
            }
        }


        public FeedbackConfigurarDetalle? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
		                IdFeedbackConfigurar,
		                IdSexo,
		                Puntaje,
                        NombreVideo,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,OrdenVideo
                    FROM 
                        pla.T_FeedbackConfigurarDetalle
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<FeedbackConfigurarDetalle>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FCR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }



    }
}
