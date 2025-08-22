using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralBeneficioRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 26/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralBeneficio
    /// </summary>
    public class ProgramaGeneralBeneficioRepository : GenericRepository<TProgramaGeneralBeneficio>, IProgramaGeneralBeneficioRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralBeneficioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralBeneficio, ProgramaGeneralBeneficio>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralBeneficioArgumento, ProgramaGeneralBeneficioArgumento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralBeneficioModalidad, ProgramaGeneralBeneficioModalidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralBeneficio MapeoEntidad(ProgramaGeneralBeneficio entidad)
        {
            try
            {
                TProgramaGeneralBeneficio modelo = _mapper.Map<TProgramaGeneralBeneficio>(entidad);
                if (entidad.ProgramaGeneralBeneficioArgumentos != null && entidad.ProgramaGeneralBeneficioArgumentos.Count() > 0)
                {
                    modelo.TProgramaGeneralBeneficioArgumentos = _mapper.Map<ICollection<TProgramaGeneralBeneficioArgumento>>(entidad.ProgramaGeneralBeneficioArgumentos);
                }
                if (entidad.ProgramaGeneralBeneficioModalidads != null && entidad.ProgramaGeneralBeneficioModalidads.Count() > 0)
                {
                    modelo.TProgramaGeneralBeneficioModalidads = _mapper.Map<ICollection<TProgramaGeneralBeneficioModalidad>>(entidad.ProgramaGeneralBeneficioModalidads);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralBeneficio Add(ProgramaGeneralBeneficio entidad)
        {
            try
            {
                var ProgramaGeneralBeneficio = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralBeneficio);
                return ProgramaGeneralBeneficio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralBeneficio Update(ProgramaGeneralBeneficio entidad)
        {
            try
            {
                var ProgramaGeneralBeneficio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralBeneficio.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralBeneficio);
                return ProgramaGeneralBeneficio;
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


        public IEnumerable<TProgramaGeneralBeneficio> Add(IEnumerable<ProgramaGeneralBeneficio> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralBeneficio> listado = new List<TProgramaGeneralBeneficio>();
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

        public IEnumerable<TProgramaGeneralBeneficio> Update(IEnumerable<ProgramaGeneralBeneficio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralBeneficio> listado = new List<TProgramaGeneralBeneficio>();
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
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralBeneficio por id
        /// </summary>
        /// <returns> ProgramaGeneralBeneficio </returns>
        public ProgramaGeneralBeneficio? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdPGeneral AS IdPgeneral,
		                Nombre,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
                    FROM pla.T_ProgramaGeneralBeneficio
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralBeneficio>(resultado);
                }
                return null;
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
        /// Obtiene todos los registros de T_ProgramaGeneralBeneficio.
        /// </summary>
        /// <returns> List<ProgramaGeneralBeneficioDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioDTO> ObtenerProgramaGeneralBeneficio()
        {
            try
            {
                List<ProgramaGeneralBeneficioDTO> rpta = new List<ProgramaGeneralBeneficioDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPGeneral,
	                    Nombre,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralBeneficio
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralBeneficioDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene registros de T_ProgramaGeneralBeneficio para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralBeneficioComboDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioComboDTO> ObtenerCombo()
        {
            try
            {
                List<ProgramaGeneralBeneficioComboDTO> rpta = new List<ProgramaGeneralBeneficioComboDTO>();
                var query = @"SELECT Id,IdPGeneral,Nombre FROM pla.T_ProgramaGeneralBeneficio WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralBeneficioComboDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene Beneficios asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralPrerequisitoOportunidadDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioOportunidadDTO> ObtenerProgramaGeneralBeneficioPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralBeneficioOportunidadDTO> beneficios = new List<ProgramaGeneralBeneficioOportunidadDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("pla.SP_ObtenerBeneficiosPorOportunidad", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    beneficios = JsonConvert.DeserializeObject<List<ProgramaGeneralBeneficioOportunidadDTO>>(resultadoStoreProcedure);
                }
                return beneficios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CompuestoBeneficioModalidadAlternoDTO> ObteneBeneficiosPorModalidades(int idPGeneral)
        {
            try
            {
                List<BeneficioModalidadDTO> beneficios = new List<BeneficioModalidadDTO>();
                List<CompuestoBeneficioModalidadAlternoDTO> beneficiosModalidades = new List<CompuestoBeneficioModalidadAlternoDTO>();
                var query = "SELECT IdBeneficio,IdPGeneral,NombreBeneficio,IdModalidadCurso,NombreModalidad,IdArgumentoBeneficio,NombreArgumentoBeneficio, IdModalidadBeneficio FROM pla.V_TProgramaGeneralBeneficio_Beneficios " +
                    "WHERE EstadoModalidad = 1 and EstadoBeneficio = 1 and IdPGeneral = @idPGeneral ORDER BY IdBeneficio";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    beneficios = JsonConvert.DeserializeObject<List<BeneficioModalidadDTO>>(resultado);
                    beneficiosModalidades = (from p in beneficios
                                             group p by new
                                             {
                                                 p.IdPGeneral,
                                                 p.IdBeneficio,
                                                 p.NombreBeneficio
                                             } into g
                                             select new CompuestoBeneficioModalidadAlternoDTO
                                             {
                                                 IdBeneficio = g.Key.IdBeneficio,
                                                 IdPGeneral = g.Key.IdPGeneral,
                                                 NombreBeneficio = g.Key.NombreBeneficio,
                                                 Modalidades = g.Select(o => new ModalidadCursoAlternoDTO
                                                 {
                                                     Id = o.IdModalidadBeneficio,
                                                     Nombre = o.NombreModalidad,
                                                     IdModalidadCurso = o.IdModalidadCurso
                                                 }).GroupBy(i => i.Id).Select(i => i.FirstOrDefault()!).ToList(),
                                                 BeneficiosArgumentos = g.Select(o => new ComboDTO
                                                 {
                                                     Id = o.IdArgumentoBeneficio ?? 0,
                                                     Nombre = o.NombreArgumentoBeneficio
                                                 }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != 0).ToList(),

                                             }).ToList();

                }
                return beneficiosModalidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
