using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ProgramaGeneralArgumentoRepository : GenericRepository<TProgramaGeneralArgumento>, IProgramaGeneralArgumentoRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralArgumentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralArgumento, ProgramaGeneralArgumento>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralArgumento MapeoEntidad(ProgramaGeneralArgumento entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralArgumento modelo = _mapper.Map<TProgramaGeneralArgumento>(entidad);

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

        public TProgramaGeneralArgumento Add(ProgramaGeneralArgumento entidad)
        {
            try
            {
                var ProgramaGeneralArgumento = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralArgumento);
                return ProgramaGeneralArgumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralArgumento Update(ProgramaGeneralArgumento entidad)
        {
            try
            {
                var ProgramaGeneralArgumento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralArgumento.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralArgumento);
                return ProgramaGeneralArgumento;
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


        public IEnumerable<TProgramaGeneralArgumento> Add(IEnumerable<ProgramaGeneralArgumento> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralArgumento> listado = new List<TProgramaGeneralArgumento>();
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

        public IEnumerable<TProgramaGeneralArgumento> Update(IEnumerable<ProgramaGeneralArgumento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralArgumento> listado = new List<TProgramaGeneralArgumento>();
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

        #region Obtener Relacion Hijos
        public List<ProgramaGeneralArgumentoModalidad> ObtenerProgramaGeneralArgumentoModalidad(int IdProgramaGeneralArgumento)
        {
            try
            {
                List<ProgramaGeneralArgumentoModalidad> rpta = new List<ProgramaGeneralArgumentoModalidad>();
                var query = @"
                    SELECT Id, IdProgramaGeneralArgumento, IdModalidadCurso, Nombre, Estado
                    FROM pla.T_ProgramaGeneralArgumentoModalidad
                    WHERE estado = 1 AND IdProgramaGeneralArgumento = @IdProgramaGeneralArgumento";
                var resultado = _dapperRepository.QueryDapper(query, new {IdProgramaGeneralArgumento});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoModalidad>>(resultado);
                }
                return rpta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<ProgramaGeneralArgumentoModalidad>> ObtenerProgramaGeneralArgumentoModalidadAsync(int IdProgramaGeneralArgumento)
        {
            try
            {
                var query = @"
                SELECT Id, IdProgramaGeneralArgumento, IdModalidadCurso, Nombre, Estado
                FROM pla.T_ProgramaGeneralArgumentoModalidad
                WHERE estado = 1 AND IdProgramaGeneralArgumento = @IdProgramaGeneralArgumento";

                var resultado = await _dapperRepository.QueryDapperAsync(query, new { IdProgramaGeneralArgumento }).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoModalidad>>(resultado) ?? new List<ProgramaGeneralArgumentoModalidad>();
                }

                return new List<ProgramaGeneralArgumentoModalidad>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProgramaGeneralArgumentoDetalle> ObtenerProgramaGeneralArgumentoDetalle(int IdProgramaGeneralArgumento)
        {
            try
            {
                List<ProgramaGeneralArgumentoDetalle> rpta = new List<ProgramaGeneralArgumentoDetalle>();
                var query = @"
                    SELECT Id, IdProgramaGeneralArgumento, Detalle , Estado,
                               FechaCreacion,
                               FechaModificacion,
	                           UsuarioCreacion,
                               UsuarioModificacion,
                               RowVersion
                    FROM  pla.T_ProgramaGeneralArgumentoDetalle
                    WHERE Estado = 1 AND IdProgramaGeneralArgumento = @IdProgramaGeneralArgumento
                ";
                var resultado = _dapperRepository.QueryDapper(query, new {IdProgramaGeneralArgumento});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoDetalle>>(resultado);
                }
                return rpta;
            }
            catch (Exception)
            {
                throw;
            }
        }
            // En: ProgramaGeneralArgumentoRepository.cs (o repositorios separados)

            /*
            **********************************************************
            * ADVERTENCIA (Principio 5: Transparencia):
            * Esta implementacion sigue la instruccion del usuario (Turno 73/77).
            * Genera 6 llamadas secuenciales a la DB (N+1) y 6 deserializaciones JSON.
            **********************************************************
            */

            // CONSULTA 0 (del Turno 67) - Obtener Argumentos y IdPGeneral
            public async Task<List<ProgramaGeneralArgumentoDTO>> ObtenerArgumentosAsync(int idOportunidad)
            {
                var spArgumentos = "EXEC pla.SP_ProgramaGeneralArgumento_ObtenerPorOportunidad @IdOportunidad";
                var jsonArgumentos = await _dapperRepository.QueryDapperAsync(
                    spArgumentos,
                    new { IdOportunidad = idOportunidad }
                ).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(jsonArgumentos) && !jsonArgumentos.Contains("[]"))
                {
                    // CORRECCION (Turno 72): 'ProgramaGeneralArgumentoDTO.Nombre' debe ser STRING
                    return JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoDTO>>(jsonArgumentos);
                }
                return new List<ProgramaGeneralArgumentoDTO>();
            }


            // CONSULTA 1 (del Turno 73/77) - Obtener Prioridades (SELECCIONADAS)
            public async Task<List<PrioridadRepoDTO>> ObtenerPrioridadesAsync(int idOportunidad)
            {
                var queryRS5 = @"
            SELECT OPMS.IdProgramaMotivacion, OPMS.Prioridad
            FROM pla.T_OportunidadProgramaMotivacionSeleccion AS OPMS
            WHERE OPMS.IdOportunidad = @IdOportunidad AND OPMS.Estado = 1;";
                var jsonRS5 = await _dapperRepository.QueryDapperAsync(queryRS5, new { IdOportunidad = idOportunidad }).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(jsonRS5) && !jsonRS5.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<PrioridadRepoDTO>>(jsonRS5);
                }
                return new List<PrioridadRepoDTO>();
            }

            // CONSULTA 2 (CORREGIDA - Turno 80) - Obtener Motivaciones (Genericas)
            // (Usa T_ProgramaMotivacion (Turno 77, Snippet 6))
            public async Task<List<MotivacionRepoDTO>> ObtenerMotivacionesAsync(List<int> idsMotivacion)
            {
                var queryRS1 = @"
            SELECT
                PM.Id,
                PM.Descripcion AS Nombre /* (El 'Nombre' del JSON (Turno 79) es la 'Descripcion' (Turno 77)) */
            FROM pla.T_ProgramaMotivacion AS PM
            WHERE PM.Id IN @IdsMotivacion AND PM.Estado = 1;";

                var jsonRS1 = await _dapperRepository.QueryDapperAsync(queryRS1, new { IdsMotivacion = idsMotivacion }).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(jsonRS1) && !jsonRS1.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<MotivacionRepoDTO>>(jsonRS1);
                }
                return new List<MotivacionRepoDTO>();
            }

            // CONSULTA 3 (CORREGIDA - Turno 80) - Obtener Descripciones HTML
            // (Usa T_ProgramaGeneralMotivacionArgumento (Turno 77, Snippet 12))
            public async Task<List<DescripcionRepoDTO>> ObtenerDescripcionesMotivacionAsync(int idPGeneral)
            {
                /*
                **********************************************************
                * CORRECCION (Turno 80): Resuelve el Conflicto de FK
                * Unimos PGMA -> PGM -> PM para obtener el 'Id' Generico (PM.Id)
                * (FIX (Turn 80): Resolves the FK Mismatch
                * We join PGMA -> PGM -> PM to get the Generic 'Id' (PM.Id))
                **********************************************************
                */
                var queryRS_HTML = @"
            SELECT 
                PM.Id AS IdProgramaMotivacion, /* (FK a PM (Generico)) */
                STRING_AGG(CONVERT(NVARCHAR(MAX), PGMA.Nombre), ' ') AS Descripcion
            FROM pla.T_ProgramaGeneralMotivacionArgumento AS PGMA
            /* (JOIN 1: PGMA (HTML) -> PGM (Especifico)) */
            INNER JOIN pla.T_ProgramaGeneralMotivacion AS PGM 
                ON PGMA.IdProgramaGeneralMotivacion = PGM.Id
            /* (JOIN 2: PGM (Especifico) -> PM (Generico) via NOMBRES) */
            INNER JOIN pla.T_ProgramaMotivacion AS PM
                ON PGM.Nombre = PM.Descripcion
            WHERE 
                PGMA.IdPGeneral = @IdPGeneral 
                AND PGMA.Estado = 1 
                AND PGM.Estado = 1 
                AND PM.Estado = 1
            GROUP BY 
                PM.Id"; // Group by PM.Id (Generico)

                var jsonRS_HTML = await _dapperRepository.QueryDapperAsync(queryRS_HTML, new { IdPGeneral = idPGeneral }).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(jsonRS_HTML) && !jsonRS_HTML.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<DescripcionRepoDTO>>(jsonRS_HTML);
                }
                return new List<DescripcionRepoDTO>();
            }


            // CONSULTA 4 (del Turno 73) - Obtener Detalles
            public async Task<List<DetalleRepoDTO>> ObtenerDetallesAsync(int idPGeneral)
            {
                var queryRS3 = @"
            SELECT PGAD.Id, PGAD.IdProgramaGeneralArgumento, PGAD.Detalle
            FROM pla.T_ProgramaGeneralArgumentoDetalle AS PGAD
            INNER JOIN pla.T_ProgramaGeneralArgumento AS PGA ON PGAD.IdProgramaGeneralArgumento = PGA.Id
            WHERE PGA.IdPGeneral = @IdPGeneral AND PGAD.Estado = 1;";
                var jsonRS3 = await _dapperRepository.QueryDapperAsync(queryRS3, new { IdPGeneral = idPGeneral }).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(jsonRS3) && !jsonRS3.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<DetalleRepoDTO>>(jsonRS3);
                }
                return new List<DetalleRepoDTO>();
            }

            // CONSULTA 5 (del Turno 73) - Obtener Links
            public async Task<List<DetalleMotivacionLinkRepoDTO>> ObtenerLinksAsync(int idPGeneral)
            {
                /*
                **********************************************************
                * ASUNCION CRITICA (Turno 79):
                * Asumo que 'IdProgramaMotivacion' (en T_PGADM) es el FK
                * a T_ProgramaMotivacion (PM) (Generico).
                * (CRITICAL ASSUMPTION (Turn 79):
                * Assuming 'IdProgramaMotivacion' (in T_PGADM) is the FK
                * to T_ProgramaMotivacion (PM) (Generic).)
                **********************************************************
                */
                var queryRS4 = @"
            SELECT 
                PGADM.IdProgramaGeneralArgumentoDetalle, 
                PGADM.IdProgramaMotivacion /* (ASUNCION: FK a PM (Generico)) */
            FROM pla.T_ProgramaGeneralArgumentoDetalleMotivacion AS PGADM
            INNER JOIN pla.T_ProgramaGeneralArgumentoDetalle AS PGAD 
                ON PGADM.IdProgramaGeneralArgumentoDetalle = PGAD.Id
            INNER JOIN pla.T_ProgramaGeneralArgumento AS PGA 
                ON PGAD.IdProgramaGeneralArgumento = PGA.Id
            WHERE PGA.IdPGeneral = @IdPGeneral AND PGADM.Estado = 1;";
                var jsonRS4 = await _dapperRepository.QueryDapperAsync(queryRS4, new { IdPGeneral = idPGeneral }).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(jsonRS4) && !jsonRS4.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<DetalleMotivacionLinkRepoDTO>>(jsonRS4);
                }
                return new List<DetalleMotivacionLinkRepoDTO>();
            }
        public async Task<IEnumerable<ProgramaGeneralArgumentoDetalle>> ObtenerProgramaGeneralArgumentoDetalleAsync(int IdProgramaGeneralArgumento)
        {
            try
            {
                var query = @"
                SELECT Id, IdProgramaGeneralArgumento, Detalle , Estado,
                       FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, RowVersion
                FROM pla.T_ProgramaGeneralArgumentoDetalle
                WHERE Estado = 1 AND IdProgramaGeneralArgumento = @IdProgramaGeneralArgumento";

                var resultado = await _dapperRepository.QueryDapperAsync(query, new { IdProgramaGeneralArgumento }).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoDetalle>>(resultado) ?? new List<ProgramaGeneralArgumentoDetalle>();
                }

                return new List<ProgramaGeneralArgumentoDetalle>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProgramaGeneralArgumentoDetalleMotivacion ObtenerProgramaGeneralArgumentoDetalleMotivacion(int IdProgramaGeneralArgumentoDetalle)
        {
            try
            {
                ProgramaGeneralArgumentoDetalleMotivacion rpta = new ProgramaGeneralArgumentoDetalleMotivacion();
                var query = @"
                    SELECT Id, IdProgramaGeneralArgumentoDetalle, IdProgramaGeneralMotivacion, NombreMotivacion ,   Estado,
                           FechaCreacion,
                           FechaModificacion,
	                       UsuarioCreacion,
                           UsuarioModificacion,
                           RowVersion
                    FROM pla.T_ProgramaGeneralArgumentoDetalleMotivacion
	                WHERE Estado = 1 AND IdProgramaGeneralArgumentoDetalle = @IdProgramaGeneralArgumentoDetalle
                ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdProgramaGeneralArgumentoDetalle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ProgramaGeneralArgumentoDetalleMotivacion>(resultado);
                }
                return rpta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ProgramaGeneralArgumentoDetalleMotivacion?> ObtenerProgramaGeneralArgumentoDetalleMotivacionAsync(int IdProgramaGeneralArgumentoDetalle)
        {
            try
            {
                var query = @"
                SELECT Id, IdProgramaGeneralArgumentoDetalle, IdProgramaMotivacion, Estado,
                       FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, RowVersion
                FROM pla.T_ProgramaGeneralArgumentoDetalleMotivacion
                WHERE Estado = 1 AND IdProgramaGeneralArgumentoDetalle = @IdProgramaGeneralArgumentoDetalle";

                var resultado = await _dapperRepository.QueryDapperAsync(query, new { IdProgramaGeneralArgumentoDetalle }).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(resultado) || resultado == "null" || resultado == "[]")
                {
                    return null;
                }

                var list = JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoDetalleMotivacion>>(resultado);
                return list?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public IEnumerable<ProgramaGeneralArgumentoDTO> Obtener()
        {
            try
            {
                //crear vista en la bd
                List<ProgramaGeneralArgumentoDTO> rpta = new List<ProgramaGeneralArgumentoDTO>();
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
                    FROM pla.T_ProgramaGeneralArgumento
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //realizar obtener por id vista con modaliddes y detalles motivaciones

        public ProgramaGeneralArgumento? ObtenerPorId(int id)
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
                    FROM pla.T_ProgramaGeneralArgumento
                    WHERE Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralArgumento>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

        public List<ProgramaGeneralArgumentoDTO> ObtenerTodo(int IdPGeneral)
        {
            try
            {
                List<ProgramaGeneralArgumentoDTO> rpta = new();
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
                    FROM pla.T_ProgramaGeneralArgumento WHERE Estado = 1 AND IdPGeneral = @IdPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralArgumentoDTO> ObtenerTodoProgramaGeneral(int IdPGeneral)
        {
            try
            {
                List<ProgramaGeneralArgumentoDTO> rpta = new();
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
                    FROM pla.T_ProgramaGeneralArgumento WHERE Estado = 1 AND IdPGeneral = @IdPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ProgramaGeneralArgumentoDTO>> ObtenerTodoProgramaGeneralAsync(int IdOportunidad)
        {
            try
            {
                var query = @"EXEC pla.SP_ProgramaGeneralArgumento_ObtenerPorOportunidad @IdOportunidad";

                var resultado = await _dapperRepository.QueryDapperAsync(query, new { IdOportunidad }).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoDTO>>(resultado) ?? new List<ProgramaGeneralArgumentoDTO>();
                }

                return new List<ProgramaGeneralArgumentoDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProgramaGeneralArgumentoMotivacionDTO> ObtenerMotivaciones(int IdPGeneral)
        {
            try
            {
                List<ProgramaGeneralArgumentoMotivacionDTO> rpta = new List<ProgramaGeneralArgumentoMotivacionDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPGeneral,
	                    Nombre
                    FROM pla.T_ProgramaGeneralMotivacion
                    WHERE Estado = 1 and  IdPGeneral=@IdPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = IdPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoMotivacionDTO>>(resultado);
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
