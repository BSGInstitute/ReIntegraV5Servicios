using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralCertificacionRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 22/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralCertificacion
    /// </summary>
    public class ProgramaGeneralCertificacionRepository : GenericRepository<TProgramaGeneralCertificacion>, IProgramaGeneralCertificacionRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralCertificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralCertificacion, ProgramaGeneralCertificacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralCertificacionModalidad, ProgramaGeneralCertificacionModalidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralCertificacionArgumento, ProgramaGeneralCertificacionArgumento>(MemberList.None).ReverseMap();

                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

    #region Metodos Base
    private TProgramaGeneralCertificacion MapeoEntidad(ProgramaGeneralCertificacion entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralCertificacion modelo = _mapper.Map<TProgramaGeneralCertificacion>(entidad);


                if (entidad.ProgramaGeneralCertificacionArgumentos != null && entidad.ProgramaGeneralCertificacionArgumentos.Count > 0)
                {
                    modelo.TProgramaGeneralCertificacionArgumentos = _mapper.Map<ICollection<TProgramaGeneralCertificacionArgumento>>(entidad.ProgramaGeneralCertificacionArgumentos);
                }
                if (entidad.ProgramaGeneralCertificacionModalidads != null && entidad.ProgramaGeneralCertificacionModalidads.Count > 0)
                {
                    modelo.TProgramaGeneralCertificacionModalidads = _mapper.Map<ICollection<TProgramaGeneralCertificacionModalidad>>(entidad.ProgramaGeneralCertificacionModalidads);
                }


                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralCertificacion Add(ProgramaGeneralCertificacion entidad)
        {
            try
            {
                var ProgramaGeneralCertificacion = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralCertificacion);
                return ProgramaGeneralCertificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralCertificacion Update(ProgramaGeneralCertificacion entidad)
        {
            try
            {
                var ProgramaGeneralCertificacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralCertificacion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralCertificacion);
                return ProgramaGeneralCertificacion;
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


        public IEnumerable<TProgramaGeneralCertificacion> Add(IEnumerable<ProgramaGeneralCertificacion> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralCertificacion> listado = new List<TProgramaGeneralCertificacion>();
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

        public IEnumerable<TProgramaGeneralCertificacion> Update(IEnumerable<ProgramaGeneralCertificacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralCertificacion> listado = new List<TProgramaGeneralCertificacion>();
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
        /// Fecha: 27/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> ProgramaGeneralCertificacion </returns>
        public ProgramaGeneralCertificacion? ObtenerPorId(int id)
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
	                FROM pla.T_ProgramaGeneralCertificacion
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralCertificacion>(resultado)!;
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
        /// Obtiene Certificaciones asociadas a un Id Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralCertificacionAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralCertificacionAgendaDTO> ObtenerCertificacionesParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralCertificacionAgendaDTO> certificaciones = new List<ProgramaGeneralCertificacionAgendaDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("com.SP_ObtenerRequisitosCertificacionProgramaGeneral", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    certificaciones = JsonConvert.DeserializeObject<List<ProgramaGeneralCertificacionAgendaDTO>>(resultadoStoreProcedure);
                }
                return certificaciones;
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
        /// Obtiene una lista de beneficios y argumentos  por programa pertecientes a la modalidades
        /// </summary>
        /// <returns></returns>
        public List<CompuestoCertificacionModalidadDTO> ObteneCertificacionesPorModalidades(int idPGeneral)
        {
            try
            {
                List<CertificacionModalidadDTO> motivaciones = new List<CertificacionModalidadDTO>();
                List<CompuestoCertificacionModalidadDTO> motivacionesModalidades = new();
                var query = "SELECT IdCertificacion,IdPGeneral,NombreCertificacion,IdModalidadCurso,NombreModalidad,IdArgumentoCertificacion,NombreArgumentoCertificacion, IdModalidadCertificacion FROM pla.  V_TProgramaGeneralCertificacion_Certificaciones " +
                    "WHERE EstadoModalidad = 1 and EstadoCertificacion = 1 and IdPGeneral = @idPGeneral ORDER BY IdCertificacion";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    motivaciones = JsonConvert.DeserializeObject<List<CertificacionModalidadDTO>>(resultado);
                    motivacionesModalidades = (from p in motivaciones
                                               group p by new
                                               {
                                                   p.IdPGeneral,
                                                   p.IdCertificacion,
                                                   p.NombreCertificacion
                                               } into g
                                               select new CompuestoCertificacionModalidadDTO
                                               {
                                                   IdCertificacion = g.Key.IdCertificacion,
                                                   IdPGeneral = g.Key.IdPGeneral,
                                                   NombreCertificacion = g.Key.NombreCertificacion,

                                                   Modalidades = g.Select(o => new ModalidadCursoAlternoDTO
                                                   {
                                                       Id = o.IdModalidadCertificacion,
                                                       Nombre = o.NombreModalidad,
                                                       IdModalidadCurso = o.IdModalidadCurso
                                                   }).GroupBy(i => i.Id).Select(i => i.FirstOrDefault()).ToList(),
                                                   CertificacionesArgumentos = g.Select(o => new ComboDTO
                                                   {
                                                       Id = o.IdArgumentoCertificacion.GetValueOrDefault(),
                                                       Nombre = o.NombreArgumentoCertificacion
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
