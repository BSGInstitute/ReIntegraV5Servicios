using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralPrerequisitoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 26/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPrerequisito
    /// </summary>
    public class ProgramaGeneralPrerequisitoRepository : GenericRepository<TProgramaGeneralPrerequisito>, IProgramaGeneralPrerequisitoRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPrerequisitoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPrerequisito, ProgramaGeneralPrerequisito>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralPrerequisitoModalidad, ProgramaGeneralPrerequisitoModalidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralPrerequisito MapeoEntidad(ProgramaGeneralPrerequisito entidad)
        {
            try
            {
                TProgramaGeneralPrerequisito modelo = _mapper.Map<TProgramaGeneralPrerequisito>(entidad);
                if (entidad.ProgramaGeneralPrerequisitoModalidads != null && entidad.ProgramaGeneralPrerequisitoModalidads.Count() > 0)
                {
                    modelo.TProgramaGeneralPrerequisitoModalidads = _mapper.Map<ICollection<TProgramaGeneralPrerequisitoModalidad>>(entidad.ProgramaGeneralPrerequisitoModalidads);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProgramaGeneralPrerequisito Add(ProgramaGeneralPrerequisito entidad)
        {
            try
            {
                var ProgramaGeneralPrerequisito = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralPrerequisito);
                return ProgramaGeneralPrerequisito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProgramaGeneralPrerequisito Update(ProgramaGeneralPrerequisito entidad)
        {
            try
            {
                var ProgramaGeneralPrerequisito = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralPrerequisito.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralPrerequisito);
                return ProgramaGeneralPrerequisito;
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
        public IEnumerable<TProgramaGeneralPrerequisito> Add(IEnumerable<ProgramaGeneralPrerequisito> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPrerequisito> listado = new List<TProgramaGeneralPrerequisito>();
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
        public IEnumerable<TProgramaGeneralPrerequisito> Update(IEnumerable<ProgramaGeneralPrerequisito> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPrerequisito> listado = new List<TProgramaGeneralPrerequisito>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda información de T_ProgramaGeneralPrerequisito por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ProgramaGeneralPrerequisito </returns>
        public ProgramaGeneralPrerequisito? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral AS IdPgeneral,
                        Nombre,
                        Tipo,
                        Orden,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_ProgramaGeneralPrerequisito
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPrerequisito>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPrR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Prerequisitos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralPrerequisitoOportunidadDTO> </returns>
        public IEnumerable<ProgramaGeneralPrerequisitoOportunidadDTO> ObtenerProgramaGeneralPrerequisitoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralPrerequisitoOportunidadDTO> prerequisitos = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("pla.SP_ObtenerPrerequisitosPorOportunidad", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    prerequisitos = JsonConvert.DeserializeObject<List<ProgramaGeneralPrerequisitoOportunidadDTO>>(resultadoStoreProcedure)!;
                }
                return prerequisitos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Prerequisitos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralPrerequisitoOportunidadDTO> </returns>
        public IEnumerable<ProgramaGeneralPrerequisitoOportunidadDTO> ObtenerProgramaGeneralPrerequisitoEspecificoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralPrerequisitoOportunidadDTO> prerequisitos = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("pla.SP_ObtenerPrerequisitoEspecificoPorOportunidad", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    prerequisitos = JsonConvert.DeserializeObject<List<ProgramaGeneralPrerequisitoOportunidadDTO>>(resultadoStoreProcedure)!;
                }
                return prerequisitos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de prerequisitos por programa pertecientes a la modalidades
        /// </summary>
        /// <returns></returns>
        public List<CompuestoPreRequisitoModalidadDTO> ObtenerPreRequisitosPorModalidades(int idPGeneral)
        {
            try
            {
                List<PreRequisitoModalidadDTO> preRequisitos = new List<PreRequisitoModalidadDTO>();
                List<CompuestoPreRequisitoModalidadDTO> preRequisitosModalidades = new List<CompuestoPreRequisitoModalidadDTO>();
                var query = @"SELECT IdPreRequisito,IdPGeneral, NombrePreRequisito,Orden,Tipo,IdModalidadCurso,NombreModalidad,IdModalidadPreRequisito
                    FROM pla.V_TProgramaGeneralPrerequisito_PreRequisitos 
                    WHERE EstadoModalidad = 1 and EstadoPreRequisito = 1 and IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    preRequisitos = JsonConvert.DeserializeObject<List<PreRequisitoModalidadDTO>>(resultado)!;
                    preRequisitosModalidades = (from p in preRequisitos
                                                group p by new
                                                {
                                                    p.IdPGeneral,
                                                    p.IdPreRequisito,
                                                    p.NombrePreRequisito,
                                                    p.Orden,
                                                    p.Tipo
                                                } into g
                                                select new CompuestoPreRequisitoModalidadDTO
                                                {
                                                    IdPreRequisito = g.Key.IdPreRequisito,
                                                    IdPGeneral = g.Key.IdPGeneral,
                                                    NombrePreRequisito = g.Key.NombrePreRequisito,
                                                    Orden = g.Key.Orden,
                                                    Tipo = g.Key.Tipo,

                                                    Modalidades = g.Select(o => new ModalidadCursoProblemaDTO
                                                    {
                                                        Id = o.IdModalidadPreRequisito,
                                                        Nombre = o.NombreModalidad,
                                                        IdModalidad = o.IdModalidadCurso
                                                    }).ToList()
                                                }).OrderBy(x => x.Orden).ToList();
                }
                return preRequisitosModalidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
