using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System.Data;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralProblemaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralProblema
    /// </summary>
    public class ProgramaGeneralProblemaRepository : GenericRepository<TProgramaGeneralProblema>, IProgramaGeneralProblemaRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralProblemaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblema, ProgramaGeneralProblema>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralProblemaModalidad, ProgramaGeneralProblemaModalidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralProblemaDetalleSolucion, ProgramaGeneralProblemaDetalleSolucion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralProblema MapeoEntidad(ProgramaGeneralProblema entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblema modelo = _mapper.Map<TProgramaGeneralProblema>(entidad);

                if (entidad.ProgramaGeneralProblemaModalidad != null && entidad.ProgramaGeneralProblemaModalidad.Count > 0)
                {
                    foreach (var hijo in entidad.ProgramaGeneralProblemaModalidad)
                    {
                        var entidadHijo = _mapper.Map<TProgramaGeneralProblemaModalidad>(hijo);
                        modelo.TProgramaGeneralProblemaModalidads.Add(entidadHijo);
                    }
                }
                if (entidad.ProgramaGeneralProblemaDetalleSolucion != null && entidad.ProgramaGeneralProblemaDetalleSolucion.Count > 0)
                {
                    foreach (var hijo in entidad.ProgramaGeneralProblemaDetalleSolucion)
                    {
                        var entidadHijo = _mapper.Map<TProgramaGeneralProblemaDetalleSolucion>(hijo);
                        modelo.TProgramaGeneralProblemaDetalleSolucions.Add(entidadHijo);
                    }
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblema Add(ProgramaGeneralProblema entidad)
        {
            try
            {
                var ProgramaGeneralProblema = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralProblema);
                return ProgramaGeneralProblema;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblema Update(ProgramaGeneralProblema entidad)
        {
            try
            {
                var ProgramaGeneralProblema = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralProblema.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralProblema);
                return ProgramaGeneralProblema;
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


        public IEnumerable<TProgramaGeneralProblema> Add(IEnumerable<ProgramaGeneralProblema> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralProblema> listado = new List<TProgramaGeneralProblema>();
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

        public IEnumerable<TProgramaGeneralProblema> Update(IEnumerable<ProgramaGeneralProblema> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralProblema> listado = new List<TProgramaGeneralProblema>();
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
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralProblema para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaComboDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaComboDTO> ObtenerCombo()
        {
            try
            {
                List<ProgramaGeneralProblemaComboDTO> rpta = new List<ProgramaGeneralProblemaComboDTO>();
                var query = "SELECT Id, Nombre FROM pla.T_ProgramaGeneralProblema WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralProblema.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDTO> ObtenerProgramaGeneralProblema()
        {
            try
            {
                List<ProgramaGeneralProblemaDTO> rpta = new List<ProgramaGeneralProblemaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPGeneral,
	                    Nombre,
	                    EsVisibleAgenda,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion
                    FROM pla.T_ProgramaGeneralProblema
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralProblema.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaDTO> </returns>
        public ProgramaGeneralProblema ObtenerPorId(int id)
        {
            try
            {
                List<ProgramaGeneralProblemaDTO> rpta = new List<ProgramaGeneralProblemaDTO>();
                var query = @"
                    SELECT
                        Id	,
                        IdPGeneral AS IdPgeneral,
                        Nombre,
                        EsVisibleAgenda,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion
                        FROM pla.T_ProgramaGeneralProblema
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralProblema>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problemas de Programa General asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaAgendaDTO> ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralProblemaAgendaDTO> problemas = new List<ProgramaGeneralProblemaAgendaDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("com.SP_ObtenerProblemaProgramaGeneral", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    problemas = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaAgendaDTO>>(resultadoStoreProcedure);
                }
                return problemas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problemas de Programa General asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaAgendaDTO> ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidadNuevaAgenda(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralProblemaAgendaDTO> problemas = new List<ProgramaGeneralProblemaAgendaDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("com.SP_ObtenerProblemaProgramaGeneral", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    problemas = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaAgendaDTO>>(resultadoStoreProcedure);
                }
                return problemas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problemas de Programa General junto con sus Argumentos y Modalidades.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaArgumentoModalidadDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaArgumentoModalidadDTO> ObtenerProblemaArgumentoModalidad()
        {
            try
            {
                List<ProgramaGeneralProblemaArgumentoModalidadDTO> problemas = new List<ProgramaGeneralProblemaArgumentoModalidadDTO>();
                var query = @"
                    SELECT IdProblema,IdPGeneral,NombreProblema,IdModalidadCurso,NombreModalidad,IdArgumentoProblema,DetalleArgumentoProblema,
	                    SolucionArgumentoProblema, IdModalidadProblema
                    FROM pla.V_TProgramaGeneralProblema_Problemas
                    WHERE (EstadoArgumento IS NULL OR EstadoArgumento = 1)
	                    AND EstadoModalidad = 1
	                    AND EstadoProblema = 1";
                var resultadoStoreProcedure = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    problemas = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaArgumentoModalidadDTO>>(resultadoStoreProcedure);
                }
                return problemas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de beneficios y argumentos  por programa pertecientes a la modalidades
        /// </summary>
        /// <returns></returns>
        public List<CompuestoProblemaModalidadAlternoDTO> ObteneProblemasPorModalidades(int idPGeneral)
        {
            try
            {
                List<ProblemaModalidadAlternoDTO> motivaciones = new List<ProblemaModalidadAlternoDTO>();
                List<CompuestoProblemaModalidadAlternoDTO> motivacionesModalidades = new List<CompuestoProblemaModalidadAlternoDTO>();
                var query = "SELECT IdProblema,IdPGeneral,NombreProblema,IdModalidadCurso,NombreModalidad,IdArgumentoProblema,DetalleArgumentoProblema,SolucionArgumentoProblema, IdModalidadProblema, EsVisibleAgenda FROM pla.V_TProgramaGeneralProblema_Problemas " +
                    "WHERE EstadoModalidad = 1 and EstadoProblema = 1 and IdPGeneral = @idPGeneral ORDER BY IdProblema";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    motivaciones = JsonConvert.DeserializeObject<List<ProblemaModalidadAlternoDTO>>(resultado);
                    motivacionesModalidades = (from p in motivaciones
                                               group p by new
                                               {
                                                   p.IdPGeneral,
                                                   p.IdProblema,
                                                   p.NombreProblema,
                                                   p.EsVisibleAgenda
                                               } into g
                                               select new CompuestoProblemaModalidadAlternoDTO
                                               {
                                                   IdProblema = g.Key.IdProblema,
                                                   IdPGeneral = g.Key.IdPGeneral,
                                                   NombreProblema = g.Key.NombreProblema,
                                                   EsVisibleAgenda = g.Key.EsVisibleAgenda,
                                                   Modalidades = g.Select(o => new ModalidadCursoAlternoDTO
                                                   {
                                                       Id = o.IdModalidadProblema,
                                                       Nombre = o.NombreModalidad,
                                                       IdModalidadCurso = o.IdModalidadCurso
                                                   }).GroupBy(i => i.Id).Select(i => i.FirstOrDefault()!).ToList(),
                                                   ProblemasArgumentos = g.Select(o => new ProblemaDetalleSolucionAlternoDTO
                                                   {
                                                       Id = o.IdArgumentoProblema,
                                                       Detalle = o.DetalleArgumentoProblema,
                                                       Solucion = o.SolucionArgumentoProblema
                                                   }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                               }).ToList();

                }
                return motivacionesModalidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
