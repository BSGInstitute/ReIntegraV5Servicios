using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralMotivacionRespuestaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralMotivacionRespuesta
    /// </summary>
    public class ProgramaGeneralMotivacionRespuestaRepository : GenericRepository<TProgramaGeneralMotivacionRespuestum>, IProgramaGeneralMotivacionRespuestaRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralMotivacionRespuestaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralMotivacionRespuestum, ProgramaGeneralMotivacionRespuesta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralMotivacionRespuestum MapeoEntidad(ProgramaGeneralMotivacionRespuesta entidad)
        {
            try
            {
                return _mapper.Map<TProgramaGeneralMotivacionRespuestum>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralMotivacionRespuestum Add(ProgramaGeneralMotivacionRespuesta entidad)
        {
            try
            {
                var ProgramaGeneralMotivacionRespuesta = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralMotivacionRespuesta);
                return ProgramaGeneralMotivacionRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralMotivacionRespuestum Update(ProgramaGeneralMotivacionRespuesta entidad)
        {
            try
            {
                var ProgramaGeneralMotivacionRespuesta = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralMotivacionRespuesta.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralMotivacionRespuesta);
                return ProgramaGeneralMotivacionRespuesta;
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


        public IEnumerable<TProgramaGeneralMotivacionRespuestum> Add(IEnumerable<ProgramaGeneralMotivacionRespuesta> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralMotivacionRespuestum> listado = new List<TProgramaGeneralMotivacionRespuestum>();
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

        public IEnumerable<TProgramaGeneralMotivacionRespuestum> Update(IEnumerable<ProgramaGeneralMotivacionRespuesta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralMotivacionRespuestum> listado = new List<TProgramaGeneralMotivacionRespuestum>();
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
        /// Obtiene todos los registros de T_ProgramaGeneralMotivacionRespuesta.
        /// </summary>
        /// <returns> List<ProgramaGeneralMotivacionRespuestaDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionRespuestaDTO> ObtenerProgramaGeneralMotivacionRespuesta()
        {
            try
            {
                List<ProgramaGeneralMotivacionRespuestaDTO> rpta = new List<ProgramaGeneralMotivacionRespuestaDTO>();
                var query = @"
                    SELECT
	                    Estado,
				        FechaCreacion,
				        FechaModificacion,
				        Id,
				        IdMigracion,
				        IdOportunidad,
				        IdProgramaGeneralMotivacion,
				        Respuesta,
				        RowVersion,
				        UsuarioCreacion,
				        UsuarioModificacion
                    FROM pla.T_ProgramaGeneralMotivacionRespuesta
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralMotivacionRespuestaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_ProgramaGeneralMotivacionRespuesta asociado a una Oportunidad y una Motivacion.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idMotivacion">Id de la Motivacion asociada a un Programa General</param>
        /// <returns> ProgramaGeneralMotivacionRespuestaDTO </returns>
        public ProgramaGeneralMotivacionRespuesta ObtenerPorIdOportunidadIdMotivacion(int idOportunidad, int idMotivacion)
        {
            try
            {
                ProgramaGeneralMotivacionRespuesta rpta = new ProgramaGeneralMotivacionRespuesta();
                var query = @"
                            SELECT
	                            Estado,
				                FechaCreacion,
				                FechaModificacion,
				                Id,
				                IdMigracion,
				                IdOportunidad,
				                IdProgramaGeneralMotivacion,
				                Respuesta,
				                RowVersion,
				                UsuarioCreacion,
				                UsuarioModificacion
                            FROM pla.T_ProgramaGeneralMotivacionRespuesta
                            WHERE Estado = 1
                                AND IdOportunidad = @idOportunidad
                                AND IdProgramaGeneralMotivacion = @idMotivacion";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad, idMotivacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ProgramaGeneralMotivacionRespuesta>(resultado);
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
