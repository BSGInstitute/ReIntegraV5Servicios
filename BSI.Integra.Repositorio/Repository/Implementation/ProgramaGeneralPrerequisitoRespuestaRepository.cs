using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralPrerequisitoRespuestaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPrerequisitoRespuesta
    /// </summary>
    public class ProgramaGeneralPrerequisitoRespuestaRepository : GenericRepository<TProgramaGeneralPrerequisitoRespuestum>, IProgramaGeneralPrerequisitoRespuestaRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPrerequisitoRespuestaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPrerequisitoRespuestum, ProgramaGeneralPrerequisitoRespuesta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralPrerequisitoRespuestum MapeoEntidad(ProgramaGeneralPrerequisitoRespuesta entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPrerequisitoRespuestum modelo = _mapper.Map<TProgramaGeneralPrerequisitoRespuestum>(entidad);

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

        public TProgramaGeneralPrerequisitoRespuestum Add(ProgramaGeneralPrerequisitoRespuesta entidad)
        {
            try
            {
                var ProgramaGeneralPrerequisitoRespuesta = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralPrerequisitoRespuesta);
                return ProgramaGeneralPrerequisitoRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPrerequisitoRespuestum Update(ProgramaGeneralPrerequisitoRespuesta entidad)
        {
            try
            {
                var ProgramaGeneralPrerequisitoRespuesta = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralPrerequisitoRespuesta.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralPrerequisitoRespuesta);
                return ProgramaGeneralPrerequisitoRespuesta;
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


        public IEnumerable<TProgramaGeneralPrerequisitoRespuestum> Add(IEnumerable<ProgramaGeneralPrerequisitoRespuesta> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPrerequisitoRespuestum> listado = new List<TProgramaGeneralPrerequisitoRespuestum>();
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

        public IEnumerable<TProgramaGeneralPrerequisitoRespuestum> Update(IEnumerable<ProgramaGeneralPrerequisitoRespuesta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPrerequisitoRespuestum> listado = new List<TProgramaGeneralPrerequisitoRespuestum>();
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
        /// Obtiene todos los registros de T_ProgramaGeneralPrerequisitoRespuesta.
        /// </summary>
        /// <returns> List<ProgramaGeneralPrerequisitoRespuestaDTO> </returns>
        public IEnumerable<ProgramaGeneralPrerequisitoRespuestaDTO> ObtenerProgramaGeneralPrerequisitoRespuesta()
        {
            try
            {
                List<ProgramaGeneralPrerequisitoRespuestaDTO> rpta = new List<ProgramaGeneralPrerequisitoRespuestaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidad,
	                    IdProgramaGeneralPrerequisito,
	                    Respuesta,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralPrerequisitoRespuesta
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralPrerequisitoRespuestaDTO>>(resultado);
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
        /// Obtiene el registro de T_ProgramaGeneralPrerequisitoRespuesta asociado a una Oportunidad y un Prerequisito.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPrerequisito">Id del Prerequisito asociado a un Programa General</param>
        /// <returns> ProgramaGeneralPrerequisitoRespuestaDTO </returns>
        public ProgramaGeneralPrerequisitoRespuestaDTO ObtenerPrerequisitoRespuesta(int idOportunidad, int idPrerequisito)
        {
            try
            {
                ProgramaGeneralPrerequisitoRespuestaDTO rpta = new ProgramaGeneralPrerequisitoRespuestaDTO();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidad,
	                    IdProgramaGeneralPrerequisito,
	                    Respuesta,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralPrerequisitoRespuesta
                    WHERE Estado = 1
                        AND IdOportunidad = @idOportunidad
                        AND IdProgramaGeneralPrerequisito = @idPrerequisito";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad, idPrerequisito });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ProgramaGeneralPrerequisitoRespuestaDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio Rodrigo Mamani Fabian
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_ProgramaGeneralPrerequisitoRespuesta asociado a una Oportunidad y un Prerequisito.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPrerequisito">Id del Prerequisito asociado a un Programa General</param>
        /// <returns> ProgramaGeneralPrerequisitoRespuestaDTO </returns>
        public ProgramaGeneralPrerequisitoRespuesta ObtenerPorIdOportunidadIdPrerequisito(int idOportunidad, int idPrerequisito)
        {
            try
            {
                ProgramaGeneralPrerequisitoRespuesta rpta = new ProgramaGeneralPrerequisitoRespuesta();
                var query = @"
                        SELECT
	                        Id,
	                        IdOportunidad,
	                        IdProgramaGeneralPrerequisito,
	                        Respuesta,
	                        Estado,
	                        UsuarioCreacion,
	                        UsuarioModificacion,
	                        FechaCreacion,
	                        FechaModificacion,
	                        RowVersion,
	                        IdMigracion
                        FROM pla.T_ProgramaGeneralPrerequisitoRespuesta
                        WHERE Estado = 1
                            AND IdOportunidad = @idOportunidad
                            AND IdProgramaGeneralPrerequisito = @idPrerequisito";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad, idPrerequisito });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ProgramaGeneralPrerequisitoRespuesta>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
