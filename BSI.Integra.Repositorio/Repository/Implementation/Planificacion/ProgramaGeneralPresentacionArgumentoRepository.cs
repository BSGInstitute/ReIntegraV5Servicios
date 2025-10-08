using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ProgramaGeneralPresentacionArgumentoRepository : GenericRepository<TProgramaGeneralPresentacionArgumento>, IProgramaGeneralPresentacionArgumentoRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPresentacionArgumentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPresentacionArgumento, ProgramaGeneralPresentacionArgumento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralPresentacionArgumentoDetalleSolucion, ProgramaGeneralPresentacionArgumentoDetalleSolucion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralPresentacionArgumentoModalidad, ProgramaGeneralPresentacionArgumentoModalidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralPresentacionArgumentoModalidad, ProgramaGeneralPresentacionArgumento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralPresentacionArgumentoDetalleSolucion, ProgramaGeneralPresentacionArgumento>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralPresentacionArgumento MapeoEntidad(ProgramaGeneralPresentacionArgumento entidad)
        {
            try
            {

                TProgramaGeneralPresentacionArgumento modelo = _mapper.Map<TProgramaGeneralPresentacionArgumento>(entidad);

                if (entidad.ProgramaGeneralPresentacionArgumentoModalidad != null && entidad.ProgramaGeneralPresentacionArgumentoModalidad.Count > 0)
                {
                    foreach (var hijo in entidad.ProgramaGeneralPresentacionArgumentoModalidad)
                    {
                        var entidadHijo = _mapper.Map<TProgramaGeneralPresentacionArgumentoModalidad>(hijo);
                        modelo.TProgramaGeneralPresentacionArgumentoModalidads.Add(entidadHijo);
                    }
                }
                if (entidad.ProgramaGeneralPresentacionArgumentoDetalleSolucion != null && entidad.ProgramaGeneralPresentacionArgumentoDetalleSolucion.Count > 0)
                {
                    foreach (var hijo in entidad.ProgramaGeneralPresentacionArgumentoDetalleSolucion)
                    {
                        var entidadHijo = _mapper.Map<TProgramaGeneralPresentacionArgumentoDetalleSolucion>(hijo);
                        modelo.TProgramaGeneralPresentacionArgumentoDetalleSolucions.Add(entidadHijo);
                    }
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPresentacionArgumento Add(ProgramaGeneralPresentacionArgumento entidad)
        {
            try
            {
                var ProgramaGeneralPresentacionArgumento = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralPresentacionArgumento);
                return ProgramaGeneralPresentacionArgumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPresentacionArgumento Update(ProgramaGeneralPresentacionArgumento entidad)
        {
            try
            {
                var ProgramaGeneralPresentacionArgumento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralPresentacionArgumento.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralPresentacionArgumento);
                return ProgramaGeneralPresentacionArgumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Exist(int id)
        {
            try
            {
                return _entities.Any(w => w.Id == id && w.Estado == true);
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


        public IEnumerable<TProgramaGeneralPresentacionArgumento> Add(IEnumerable<ProgramaGeneralPresentacionArgumento> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPresentacionArgumento> listado = new List<TProgramaGeneralPresentacionArgumento>();
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

        public IEnumerable<TProgramaGeneralPresentacionArgumento> Update(IEnumerable<ProgramaGeneralPresentacionArgumento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPresentacionArgumento> listado = new List<TProgramaGeneralPresentacionArgumento>();
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


        public IEnumerable<ProgramaGeneralPresentacionArgumentoDTO> Obtener()
        {
            try
            {
                List<ProgramaGeneralPresentacionArgumentoDTO> rpta = new List<ProgramaGeneralPresentacionArgumentoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPGeneral,
	                    Nombre,
                        Descripcion,
	                    EsVisibleAgenda,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,RowVersion
                    FROM pla.T_ProgramaGeneralPresentacionArgumento
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralPresentacionArgumentoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"
                    SELECT Id, Nombre FROM pla.T_ProgramaGeneralPresentacionArgumento WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 15/09/2023
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >CertificadoPartnerComplemento || null</returns>
        public ProgramaGeneralPresentacionArgumento? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
                        IdPGeneral,
	                    Nombre,
                        Descripcion,
	                    EsVisibleAgenda,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,RowVersion
                    FROM pla.T_ProgramaGeneralPresentacionArgumento
                    WHERE Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPresentacionArgumento>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 01-10-2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problemas de Programa General asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaAgendaDTO> </returns>
        public List<ProgramaGeneralPresentacionArgumentoAgendaDTO> ObtenerProgramaGeneralPresentacionArgumentoParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralPresentacionArgumentoAgendaDTO> presentacionArgumento = new List<ProgramaGeneralPresentacionArgumentoAgendaDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("com.SP_ObtenerPresentacionArgumentoProgramaGeneral", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    presentacionArgumento = JsonConvert.DeserializeObject<List<ProgramaGeneralPresentacionArgumentoAgendaDTO>>(resultadoStoreProcedure);
                }
                return presentacionArgumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Marco Jose Villanueva Torres
        /// Fecha: 06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de beneficios y argumentos  por programa pertecientes a la modalidades
        /// </summary>
        /// <returns></returns>
        public List<CompuestoPresentacionArgumentoModalidadAlternoDTO> ObtenePresentacionArgumentoPorModalidades(int idPGeneral)
        {
            try
            {
                List<PresentacionArgumentoModalidadAlternoDTO> motivaciones = new List<PresentacionArgumentoModalidadAlternoDTO>();
                List<CompuestoPresentacionArgumentoModalidadAlternoDTO> motivacionesModalidades = new List<CompuestoPresentacionArgumentoModalidadAlternoDTO>();
                var query = "SELECT IdPresentacionArgumento,IdPGeneral,NombrePresentacionArgumento,DescripcionPresentacionArgumento,IdModalidadPresentacionArgumento,IdModalidadCurso,NombreModalidad,IdArgumentoPresentacionArgumento,DetalleArgumentoPresentacionArgumento,SolucionArgumentoPresentacionArgumento, EstadoPresentacionArgumento, EsVisibleAgenda ,EstadoArgumento FROM pla.V_ObtenerProgramaGeneralPresentacionArgumentos " +
                    "WHERE EstadoModalidad = 1 and EstadoPresentacionArgumento = 1 and IdPGeneral = @idPGeneral ORDER BY IdPresentacionArgumento";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    motivaciones = JsonConvert.DeserializeObject<List<PresentacionArgumentoModalidadAlternoDTO>>(resultado);
                    motivacionesModalidades = (from p in motivaciones
                                               group p by new
                                               {
                                                   p.IdPGeneral,
                                                   p.IdPresentacionArgumento,
                                                   p.NombrePresentacionArgumento,
                                                   p.DescripcionPresentacionArgumento,
                                                   p.EsVisibleAgenda
                                               } into g
                                               select new CompuestoPresentacionArgumentoModalidadAlternoDTO
                                               {
                                                   IdPresentacionArgumento = g.Key.IdPresentacionArgumento,
                                                   IdPGeneral = g.Key.IdPGeneral,
                                                   NombrePresentacionArgumento = g.Key.NombrePresentacionArgumento,
                                                   DescripcionPresentacionArgumento = g.Key.DescripcionPresentacionArgumento,
                                                   EsVisibleAgenda = g.Key.EsVisibleAgenda,
                                                   Modalidades = g.Select(o => new ModalidadCursoAlternoDTO
                                                   {
                                                       Id = o.IdModalidadPresentacionArgumento,
                                                       Nombre = o.NombreModalidad,
                                                       IdModalidadCurso = o.IdModalidadCurso
                                                   }).GroupBy(i => i.Id).Select(i => i.FirstOrDefault()!).ToList(),
                                                   PresentacionArgumento = g.Select(o => new PresentacionArgumentoDetalleSolucionAlternoDTO
                                                   {
                                                       Id = o.IdArgumentoPresentacionArgumento,
                                                       Detalle = o.DetalleArgumentoPresentacionArgumento,
                                                       Solucion = o.SolucionArgumentoPresentacionArgumento
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
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerProgramaGeneralPresentacionArgumentoHtml(int idPGeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> presentacionArgumento = new List<RegistroListaSeccionesDocumentoDTO>();
                var resultadoStoreProcedure = _dapperRepository.QueryDapper("SELECT Titulo,Contenido,IdPGeneral,Cabecera FROM com.V_ObtenerProgramaGeneralPresentacionArgumentoHtml WHERE IdPGeneral=@IdPGeneral", new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    presentacionArgumento = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(resultadoStoreProcedure);
                }
                return presentacionArgumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<PresentacionProgramadto> ObtenerProgramaGeneralPresentacionArgumento(int idPGeneral)
        {
            try
            {
                List<PresentacionProgramadto> presentacionArgumento = new List<PresentacionProgramadto>();
                var resultadoStoreProcedure = _dapperRepository.QueryDapper("SELECT IdPGeneral,Titulo,IdPGeneral,Cabecera ,Solucion FROM com.V_ObtenerPresentacionPrograma WHERE IdPGeneral=@IdPGeneral", new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    presentacionArgumento = JsonConvert.DeserializeObject<List<PresentacionProgramadto>>(resultadoStoreProcedure);
                }
                return presentacionArgumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
