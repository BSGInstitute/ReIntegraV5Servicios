using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralModeloCertificadoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 19/09/2022
    /// <summary>
    /// Gestión general de TProgramaGeneralModeloCertificado
    /// </summary>
    public class ProgramaGeneralModeloCertificadoRepository : GenericRepository<TProgramaGeneralModeloCertificado>, IProgramaGeneralModeloCertificadoRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralModeloCertificadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralModeloCertificado, ProgramaGeneralModeloCertificado>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralModeloCertificadoModalidad, ProgramaGeneralModeloCertificadoModalidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralModeloCertificado MapeoEntidad(ProgramaGeneralModeloCertificado entidad)
        {
            try
            {
                TProgramaGeneralModeloCertificado modelo = _mapper.Map<TProgramaGeneralModeloCertificado>(entidad);
                if (entidad.ProgramaGeneralModeloCertificadoModalidads != null && entidad.ProgramaGeneralModeloCertificadoModalidads.Count() > 0)
                {
                    modelo.TProgramaGeneralModeloCertificadoModalidads = _mapper.Map<ICollection<TProgramaGeneralModeloCertificadoModalidad>>(entidad.ProgramaGeneralModeloCertificadoModalidads);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProgramaGeneralModeloCertificado Add(ProgramaGeneralModeloCertificado entidad)
        {
            try
            {
                var ProgramaGeneralModeloCertificado = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralModeloCertificado);
                return ProgramaGeneralModeloCertificado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProgramaGeneralModeloCertificado Update(ProgramaGeneralModeloCertificado entidad)
        {
            try
            {
                var ProgramaGeneralModeloCertificado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralModeloCertificado.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralModeloCertificado);
                return ProgramaGeneralModeloCertificado;
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
        public IEnumerable<TProgramaGeneralModeloCertificado> Add(IEnumerable<ProgramaGeneralModeloCertificado> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralModeloCertificado> listado = new List<TProgramaGeneralModeloCertificado>();
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
        public IEnumerable<TProgramaGeneralModeloCertificado> Update(IEnumerable<ProgramaGeneralModeloCertificado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralModeloCertificado> listado = new List<TProgramaGeneralModeloCertificado>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 26/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public ProgramaGeneralModeloCertificado? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                   SELECT Id,
		                IdPGeneral AS IdPgeneral,
		                Nombre,
		                Url,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
	                FROM pla.T_ProgramaGeneralModeloCertificado 
                        WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralModeloCertificado>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 20/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de modelos de certificados
        /// </summary>
        /// <returns> List<ProgramaGeneralModeloCertificadoDTO> </returns>   
        public List<ProgramaGeneralModeloCertificadoDTO> ObtenerModeloCertificadoProgramaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralModeloCertificadoDTO> lista = new List<ProgramaGeneralModeloCertificadoDTO>();
                var query = "com.SP_ObtenerModeloCertificadoProgramaGeneral";
                var respuesta = _dapperRepository.QuerySPDapper(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ProgramaGeneralModeloCertificadoDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de modelos de certificados
        /// </summary>
        /// <returns> List<PGeneralPublicoObjetivoDTO> </returns>
        public List<PGeneralModeloCertificadoDTO> ObtenerModeloCertificadoPrograma(int idOportunidad)
        {
            try
            {
                List<PGeneralModeloCertificadoDTO> lista = new List<PGeneralModeloCertificadoDTO>();
                var query = "com.SP_ObtenerModeloCertificadoProgramaGeneral";
                var respuesta = _dapperRepository.QuerySPDapper(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PGeneralModeloCertificadoDTO>>(respuesta)!;
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 2022/08/04
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de modelos de certificados
        /// </summary>
        /// <returns></returns>
        public List<CompuestoProblemaModeloCertificadoDTO> ObteneCertificacionesPorModalidades(int idPGeneral)
        {
            try
            {
                List<ModeloModalidadDTO> modelos = new List<ModeloModalidadDTO>();
                List<CompuestoProblemaModeloCertificadoDTO> modelosModalidades = new List<CompuestoProblemaModeloCertificadoDTO>();
                var query = @"
                        SELECT
	                        IdModeloCertificado,
	                        IdPGeneral,
	                        NombreModeloCertificado,
	                        IdModalidadModelo,
	                        IdModalidadCurso,
	                        NombreModalidad,
	                        EstadoCertificacion,
	                        EstadoModalidad,
                            UrlModeloCertificado
                        FROM pla.V_TProgramaGeneralModeloCertificado_Modelos
                        WHERE EstadoModalidad = 1 AND EstadoCertificacion = 1 AND IdPGeneral = @idPGeneral ORDER BY IdModeloCertificado;
                        ";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    modelos = JsonConvert.DeserializeObject<List<ModeloModalidadDTO>>(resultado);
                    modelosModalidades = (from p in modelos
                                          group p by new
                                          {
                                              p.IdPGeneral,
                                              p.IdModeloCertificado,
                                              p.NombreModeloCertificado,
                                              p.UrlModeloCertificado
                                          } into g
                                          select new CompuestoProblemaModeloCertificadoDTO
                                          {
                                              IdModeloCertificado = g.Key.IdModeloCertificado,
                                              IdPGeneral = g.Key.IdPGeneral,
                                              NombreModeloCertificado = g.Key.NombreModeloCertificado,
                                              UrlModeloCertificado = g.Key.UrlModeloCertificado,
                                              Modalidades = g.Select(o => new ModalidadCursoAlternoDTO
                                              {
                                                  Id = o.IdModalidadModelo,
                                                  Nombre = o.NombreModalidad,
                                                  IdModalidadCurso = o.IdModalidadCurso
                                              }).GroupBy(i => i.Id).Select(i => i.FirstOrDefault()!).ToList()
                                          }).ToList();
                }
                return modelosModalidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
