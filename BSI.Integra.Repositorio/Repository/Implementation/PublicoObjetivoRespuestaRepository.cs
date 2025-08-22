using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PublicoObjetivoRespuestaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_PublicoObjetivoRespuesta
    /// </summary>
    public class PublicoObjetivoRespuestaRepository : GenericRepository<TPublicoObjetivoRespuestum>, IPublicoObjetivoRespuestaRepository
    {
        private Mapper _mapper;

        public PublicoObjetivoRespuestaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPublicoObjetivoRespuestum, PublicoObjetivoRespuesta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPublicoObjetivoRespuestum MapeoEntidad(PublicoObjetivoRespuesta entidad)
        {
            try
            {
                //crea la entidad padre
                TPublicoObjetivoRespuestum modelo = _mapper.Map<TPublicoObjetivoRespuestum>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPublicoObjetivoRespuestum Add(PublicoObjetivoRespuesta entidad)
        {
            try
            {
                var PublicoObjetivoRespuesta = MapeoEntidad(entidad);
                base.Insert(PublicoObjetivoRespuesta);
                return PublicoObjetivoRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPublicoObjetivoRespuestum Update(PublicoObjetivoRespuesta entidad)
        {
            try
            {
                var PublicoObjetivoRespuesta = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PublicoObjetivoRespuesta.RowVersion = entidadExistente.RowVersion;

                base.Update(PublicoObjetivoRespuesta);
                return PublicoObjetivoRespuesta;
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


        public IEnumerable<TPublicoObjetivoRespuestum> Add(IEnumerable<PublicoObjetivoRespuesta> listadoEntidad)
        {
            try
            {
                List<TPublicoObjetivoRespuestum> listado = new List<TPublicoObjetivoRespuestum>();
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

        public IEnumerable<TPublicoObjetivoRespuestum> Update(IEnumerable<PublicoObjetivoRespuesta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPublicoObjetivoRespuestum> listado = new List<TPublicoObjetivoRespuestum>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PublicoObjetivoRespuesta.
        /// </summary>
        /// <returns> List<PublicoObjetivoRespuestaDTO> </returns>
        public IEnumerable<PublicoObjetivoRespuesta> ObtenerPublicoObjetivoRespuesta()
        {
            try
            {
                List<PublicoObjetivoRespuesta> rpta = new List<PublicoObjetivoRespuesta>();
                var query = @"
                        SELECT
	                        Id,
						    IdOportunidad,
						    IdDocumentoSeccion_PW  AS IdDocumentoSeccionPw,
						    NivelCumplimiento,
						    Estado,
						    UsuarioCreacion,
						    UsuarioModificacion,
						    FechaCreacion,
						    FechaModificacion,
						    RowVersion,
                        FROM pla.T_PublicoObjetivoRespuesta
                        WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PublicoObjetivoRespuesta>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PublicoObjetivoRespuesta ObtenerPorIdOportunidadIdDocumentoSeccion(int idOportunidad, int idDocumentoSeccion)
        {
            try
            {
                PublicoObjetivoRespuesta rpta = new PublicoObjetivoRespuesta();
                var query = @"
                        SELECT
	                        Id,
	                        IdOportunidad,
	                        IdDocumentoSeccion_PW AS IdDocumentoSeccionPw,
	                        NivelCumplimiento,
	                        Estado,
	                        UsuarioCreacion,
	                        UsuarioModificacion,
	                        FechaCreacion,
	                        FechaModificacion,
	                        RowVersion,
	                        IdMigracion
                        FROM pla.T_PublicoObjetivoRespuesta
                        WHERE Estado = 1
                            AND IdOportunidad = @idOportunidad
                            AND IdDocumentoSeccion_PW = @idDocumentoSeccion";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad, idDocumentoSeccion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<PublicoObjetivoRespuesta>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
