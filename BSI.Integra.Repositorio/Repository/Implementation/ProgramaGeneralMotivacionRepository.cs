using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralMotivacionRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 22/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralMotivacion
    /// </summary>
    public class ProgramaGeneralMotivacionRepository : GenericRepository<TProgramaGeneralMotivacion>, IProgramaGeneralMotivacionRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralMotivacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralMotivacion, ProgramaGeneralMotivacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralMotivacionModalidad, ProgramaGeneralMotivacionModalidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralMotivacionArgumento, ProgramaGeneralMotivacionArgumento>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralMotivacion MapeoEntidad(ProgramaGeneralMotivacion entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralMotivacion modelo = _mapper.Map<TProgramaGeneralMotivacion>(entidad);

                if (entidad.ProgramaGeneralMotivacionArgumentos != null && entidad.ProgramaGeneralMotivacionArgumentos.Count > 0)
                {
                    modelo.TProgramaGeneralMotivacionArgumentos = _mapper.Map<ICollection<TProgramaGeneralMotivacionArgumento>> (entidad.ProgramaGeneralMotivacionArgumentos);
                }
                if (entidad.ProgramaGeneralMotivacionModalidads != null && entidad.ProgramaGeneralMotivacionModalidads.Count > 0)
                {
                    modelo.TProgramaGeneralMotivacionModalidads= _mapper.Map<ICollection<TProgramaGeneralMotivacionModalidad>>(entidad.ProgramaGeneralMotivacionModalidads);
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralMotivacion Add(ProgramaGeneralMotivacion entidad)
        {
            try
            {
                var ProgramaGeneralMotivacion = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralMotivacion);
                return ProgramaGeneralMotivacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralMotivacion Update(ProgramaGeneralMotivacion entidad)
        {
            try
            {
                var ProgramaGeneralMotivacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralMotivacion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralMotivacion);
                return ProgramaGeneralMotivacion;
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


        public IEnumerable<TProgramaGeneralMotivacion> Add(IEnumerable<ProgramaGeneralMotivacion> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralMotivacion> listado = new List<TProgramaGeneralMotivacion>();
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

        public IEnumerable<TProgramaGeneralMotivacion> Update(IEnumerable<ProgramaGeneralMotivacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralMotivacion> listado = new List<TProgramaGeneralMotivacion>();
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
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ProgramaGeneralMotivacion? ObtenerPorId(int id)
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
	                FROM pla.T_ProgramaGeneralMotivacion
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralMotivacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralMotivacion.
        /// </summary>
        /// <returns> List<ProgramaGeneralMotivacionDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionDTO> ObtenerProgramaGeneralMotivacion()
        {
            try
            {
                List<ProgramaGeneralMotivacionDTO> rpta = new List<ProgramaGeneralMotivacionDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPGeneral,
	                    Nombre,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralMotivacion
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralMotivacionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralMotivacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralMotivacionComboDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionComboDTO> ObtenerCombo()
        {
            try
            {
                List<ProgramaGeneralMotivacionComboDTO> rpta = new List<ProgramaGeneralMotivacionComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.T_ProgramaGeneralMotivacion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralMotivacionComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Motivaciones asociadas a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralMotivacionAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionAgendaDTO> ObtenerMotivacionesParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralMotivacionAgendaDTO> motivaciones = new List<ProgramaGeneralMotivacionAgendaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerFactorMotivacionProgramaGeneral", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    motivaciones = JsonConvert.DeserializeObject<List<ProgramaGeneralMotivacionAgendaDTO>>(resultado);
                }
                return motivaciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio 
        /// Fecha: 06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de beneficios y argumentos  por programa pertecientes a la modalidades
        /// </summary>
        /// <returns></returns>
        public List<CompuestoMotivacionModalidadAlternoDTO> ObteneMotivacionesPorModalidades(int idPGeneral)
        {
            try
            {
                List<MotivacionModalidadDTO> motivaciones = new List<MotivacionModalidadDTO>();
                List<CompuestoMotivacionModalidadAlternoDTO> motivacionesModalidades = new List<CompuestoMotivacionModalidadAlternoDTO>();
                var query = "SELECT IdMotivacion,IdPGeneral,NombreMotivacion,IdModalidadCurso,NombreModalidad,IdArgumentoMotivacion,NombreArgumentoMotivacion, IdModalidadMotivacion FROM pla.  V_TProgramaGeneralMotivacion_Motivaciones " +
                    "WHERE EstadoModalidad = 1 and EstadoMotivacion = 1 and IdPGeneral = @idPGeneral ORDER BY IdMotivacion";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    motivaciones = JsonConvert.DeserializeObject<List<MotivacionModalidadDTO>>(resultado);
                    motivacionesModalidades = (from p in motivaciones
                                               group p by new
                                               {
                                                   p.IdPGeneral,
                                                   p.IdMotivacion,
                                                   p.NombreMotivacion
                                               } into g
                                               select new CompuestoMotivacionModalidadAlternoDTO
                                               {
                                                   IdMotivacion = g.Key.IdMotivacion,
                                                   IdPGeneral = g.Key.IdPGeneral,
                                                   NombreMotivacion = g.Key.NombreMotivacion,
                                                   Modalidades = g.Select(o => new ModalidadCursoAlternoDTO
                                                   {
                                                       Id = o.IdModalidadMotivacion,
                                                       Nombre = o.NombreModalidad,
                                                       IdModalidadCurso = o.IdModalidadCurso
                                                   }).GroupBy(i => i.Id).Select(i => i.FirstOrDefault()!).ToList(),
                                                   MotivacionesArgumentos = g.Select(o => new ComboDTO
                                                   {
                                                       Id = o.IdArgumentoMotivacion ?? 0,
                                                       Nombre = o.NombreArgumentoMotivacion
                                                   }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != 0).ToList(),

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
