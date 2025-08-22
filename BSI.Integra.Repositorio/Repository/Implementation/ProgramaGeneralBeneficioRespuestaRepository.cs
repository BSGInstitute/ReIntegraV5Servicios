using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralBeneficioRespuestaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralBeneficioRespuesta
    /// </summary>
    public class ProgramaGeneralBeneficioRespuestaRepository : GenericRepository<TProgramaGeneralBeneficioRespuestum>, IProgramaGeneralBeneficioRespuestaRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralBeneficioRespuestaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralBeneficioRespuestum, ProgramaGeneralBeneficioRespuesta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralBeneficioRespuestum MapeoEntidad(ProgramaGeneralBeneficioRespuesta entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralBeneficioRespuestum modelo = _mapper.Map<TProgramaGeneralBeneficioRespuestum>(entidad);

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

        public TProgramaGeneralBeneficioRespuestum Add(ProgramaGeneralBeneficioRespuesta entidad)
        {
            try
            {
                var ProgramaGeneralBeneficioRespuesta = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralBeneficioRespuesta);
                return ProgramaGeneralBeneficioRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralBeneficioRespuestum Update(ProgramaGeneralBeneficioRespuesta entidad)
        {
            try
            {
                var ProgramaGeneralBeneficioRespuesta = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralBeneficioRespuesta.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralBeneficioRespuesta);
                return ProgramaGeneralBeneficioRespuesta;
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


        public IEnumerable<TProgramaGeneralBeneficioRespuestum> Add(IEnumerable<ProgramaGeneralBeneficioRespuesta> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralBeneficioRespuestum> listado = new List<TProgramaGeneralBeneficioRespuestum>();
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

        public IEnumerable<TProgramaGeneralBeneficioRespuestum> Update(IEnumerable<ProgramaGeneralBeneficioRespuesta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralBeneficioRespuestum> listado = new List<TProgramaGeneralBeneficioRespuestum>();
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
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralBeneficioRespuesta.
        /// </summary>
        /// <returns> List<ProgramaGeneralBeneficioRespuestaDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioRespuestaDTO> ObtenerProgramaGeneralBeneficioRespuesta()
        {
            try
            {
                List<ProgramaGeneralBeneficioRespuestaDTO> rpta = new List<ProgramaGeneralBeneficioRespuestaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidad,
	                    IdProgramaGeneralBeneficio,
	                    Respuesta,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralBeneficioRespuesta
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralBeneficioRespuestaDTO>>(resultado);
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
        /// Obtiene la informacion del registro asociado a la Oportunidad y al Beneficio.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idBeneficio">Id del Beneficio asociado a un Programa General</param>
        /// <returns> ProgramaGeneralBeneficioRespuestaDTO </returns>
        public ProgramaGeneralBeneficioRespuesta ObtenerPorIdOportunidadIdBeneficio(int idOportunidad, int idBeneficio)
        {
            try
            {
                ProgramaGeneralBeneficioRespuesta rpta = new ProgramaGeneralBeneficioRespuesta();
                var query = @"
                            SELECT
	                            Id,
	                            IdOportunidad,
	                            IdProgramaGeneralBeneficio,
	                            Respuesta,
                                Estado,
	                            UsuarioCreacion,
	                            UsuarioModificacion,
	                            FechaCreacion,
	                            FechaModificacion
                            FROM pla.T_ProgramaGeneralBeneficioRespuesta
                            WHERE Estado = 1
                                AND IdOportunidad = @idOportunidad
                                AND IdProgramaGeneralBeneficio = @idBeneficio";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad, idBeneficio });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ProgramaGeneralBeneficioRespuesta>(resultado);
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
