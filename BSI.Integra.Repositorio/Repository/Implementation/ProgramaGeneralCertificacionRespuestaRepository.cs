using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralCertificacionRespuestaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralCertificacionRespuesta
    /// </summary>
    public class ProgramaGeneralCertificacionRespuestaRepository : GenericRepository<TProgramaGeneralCertificacionRespuestum>, IProgramaGeneralCertificacionRespuestaRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralCertificacionRespuestaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralCertificacionRespuestum, ProgramaGeneralCertificacionRespuesta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralCertificacionRespuestum MapeoEntidad(ProgramaGeneralCertificacionRespuesta entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralCertificacionRespuestum modelo = _mapper.Map<TProgramaGeneralCertificacionRespuestum>(entidad);

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

        public TProgramaGeneralCertificacionRespuestum Add(ProgramaGeneralCertificacionRespuesta entidad)
        {
            try
            {
                var ProgramaGeneralCertificacionRespuesta = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralCertificacionRespuesta);
                return ProgramaGeneralCertificacionRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralCertificacionRespuestum Update(ProgramaGeneralCertificacionRespuesta entidad)
        {
            try
            {
                var ProgramaGeneralCertificacionRespuesta = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralCertificacionRespuesta.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralCertificacionRespuesta);
                return ProgramaGeneralCertificacionRespuesta;
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


        public IEnumerable<TProgramaGeneralCertificacionRespuestum> Add(IEnumerable<ProgramaGeneralCertificacionRespuesta> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralCertificacionRespuestum> listado = new List<TProgramaGeneralCertificacionRespuestum>();
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

        public IEnumerable<TProgramaGeneralCertificacionRespuestum> Update(IEnumerable<ProgramaGeneralCertificacionRespuesta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralCertificacionRespuestum> listado = new List<TProgramaGeneralCertificacionRespuestum>();
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
        /// Obtiene todos los registros de T_ProgramaGeneralCertificacionRespuesta.
        /// </summary>
        /// <returns> List<ProgramaGeneralCertificacionRespuestaDTO> </returns>
        public IEnumerable<ProgramaGeneralCertificacionRespuestaDTO> ObtenerProgramaGeneralCertificacionRespuesta()
        {
            try
            {
                List<ProgramaGeneralCertificacionRespuestaDTO> rpta = new List<ProgramaGeneralCertificacionRespuestaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidad,
	                    IdProgramaGeneralCertificacion,
	                    Respuesta,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralCertificacionRespuesta
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralCertificacionRespuestaDTO>>(resultado);
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
        /// Obtiene el registro de T_ProgramaGeneralCertificacionRespuesta asociado a una Oportunidad y una Certificacion.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idCertificacion">Id de la Certificacion</param>
        /// <returns> ProgramaGeneralCertificacionRespuestaDTO </returns>
        public ProgramaGeneralCertificacionRespuestaDTO ObtenerCertificacionRespuesta(int idOportunidad, int idCertificacion)
        {
            try
            {
                ProgramaGeneralCertificacionRespuestaDTO rpta = new ProgramaGeneralCertificacionRespuestaDTO();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidad,
	                    IdProgramaGeneralCertificacion,
	                    Respuesta,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralCertificacionRespuesta
                    WHERE Estado = 1
                        AND IdOportunidad = @idOportunidad
                        AND IdProgramaGeneralCertificacion = @idCertificacion";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad, idCertificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ProgramaGeneralCertificacionRespuestaDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralCertificacionRespuesta ObtenerPorIdOportunidadIdCertificacion(int idOportunidad, int idCertificacion)
        {
            try
            {
                ProgramaGeneralCertificacionRespuesta rpta = new ProgramaGeneralCertificacionRespuesta();
                var query = @"
                        SELECT
	                        Id,
                            IdOportunidad,
                            IdProgramaGeneralCertificacion,
                            Respuesta,
                            Estado,
                            UsuarioCreacion,
                            UsuarioModificacion,
                            FechaCreacion,
                            FechaModificacion,
                            RowVersion,
                            IdMigracion
                        FROM pla.T_ProgramaGeneralCertificacionRespuesta
                        WHERE Estado = 1
                            AND IdOportunidad = @idOportunidad
                            AND IdProgramaGeneralCertificacion = @idCertificacion";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad, idCertificacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ProgramaGeneralCertificacionRespuesta>(resultado)!;
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
