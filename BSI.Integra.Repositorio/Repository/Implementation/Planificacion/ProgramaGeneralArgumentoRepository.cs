using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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

        public ComboDTO ObtenerMotivacionesByDiccionario(string motivacion)
        {
            try
            {
                var query = @"
                    SELECT IdProgramaMotivacion AS Id, DescripcionProgramaMotivacion AS Nombre
                    FROM pla.V_ProgramaMotivacionDiccionario_ProgramaMotivacion
                    WHERE NombreMotivacionAlterno = @motivacion
                ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { motivacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ComboDTO>(resultado);
                }
                return null;
                            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MotivacionDiccionarioViewDTO> ObtenerMotivacionesTodoDiccionario()
        {
            try
            {
                List<MotivacionDiccionarioViewDTO> rpta = new List<MotivacionDiccionarioViewDTO>();
                var query = @"
                    SELECT IdProgramaMotivacion, DescripcionProgramaMotivacion, NombreMotivacionAlterno
                    FROM pla.V_ProgramaMotivacionDiccionario_ProgramaMotivacion
                ";
                var resultado = _dapperRepository.QueryDapper(query, new {});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MotivacionDiccionarioViewDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralArgumentoDetalleMotivacionNombreDTO ObtenerProgramaGeneralArgumentoDetalleMotivacionNombre(int IdProgramaGeneralArgumentoDetalle)
        {
            try
            {
                ProgramaGeneralArgumentoDetalleMotivacionNombreDTO rpta = new ProgramaGeneralArgumentoDetalleMotivacionNombreDTO();
                var query = @"
                    SELECT 
	                    IdProgramaGeneralArgumentoDetalleMotivacion,
	                    IdProgramaGeneralArgumentoDetalle,
	                    IdProgramaMotivacion,
	                    NombreMotivacion
                    FROM pla.V_ProgramaGeneralArgumentoDetalleMotivacion
                    WHERE IdProgramaGeneralArgumentoDetalle = @IdProgramaGeneralArgumentoDetalle  
                ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdProgramaGeneralArgumentoDetalle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ProgramaGeneralArgumentoDetalleMotivacionNombreDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProgramaGeneralArgumentoDTO>> ObtenerArgumentosAsync(int idOportunidad)
        {
            var spArgumentos = "EXEC pla.SP_ProgramaGeneralArgumento_ObtenerPorOportunidad @IdOportunidad";
            var resultado = await _dapperRepository.QueryDapperAsync(
                spArgumentos,
                new { IdOportunidad = idOportunidad }
            ).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                return JsonConvert.DeserializeObject<List<ProgramaGeneralArgumentoDTO>>(resultado);
            }
            return new List<ProgramaGeneralArgumentoDTO>();
        }

        public async Task<List<PrioridadRepoDTO>> ObtenerPrioridadesAsync(int idOportunidad)
        {
            var query = @"
                            SELECT OPMS.IdProgramaMotivacion, OPMS.Prioridad
                            FROM pla.T_OportunidadProgramaMotivacionSeleccion AS OPMS
                            WHERE OPMS.IdOportunidad = @IdOportunidad AND OPMS.Estado = 1;";
            var resultado = await _dapperRepository.QueryDapperAsync(query, new { IdOportunidad = idOportunidad }).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                return JsonConvert.DeserializeObject<List<PrioridadRepoDTO>>(resultado);
            }
            return new List<PrioridadRepoDTO>();
        }

        public async Task<List<MotivacionRepoDTO>> ObtenerMotivacionesAsync(List<int> idsMotivacion)
        {
            var query = @"
                            SELECT
                                PM.Id,
                                PM.Descripcion AS Nombre
                            FROM pla.T_ProgramaMotivacion AS PM
                            WHERE PM.Id IN @IdsMotivacion AND PM.Estado = 1;";

            var resultado = await _dapperRepository.QueryDapperAsync(query, new { IdsMotivacion = idsMotivacion }).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                return JsonConvert.DeserializeObject<List<MotivacionRepoDTO>>(resultado);
            }
            return new List<MotivacionRepoDTO>();
        }

        public async Task<List<DescripcionRepoDTO>> ObtenerDescripcionesMotivacionAsync(int idPGeneral) { var query = @"                                 SELECT                                     PGM.Id AS IdEspecifico,                                     PGM.Nombre AS NombreMotivacion,                                     STRING_AGG(CONVERT(NVARCHAR(MAX), PGMA.Nombre), ' ') AS Descripcion                                 FROM pla.T_ProgramaGeneralMotivacionArgumento AS PGMA                                 INNER JOIN pla.T_ProgramaGeneralMotivacion AS PGM                                     ON PGMA.IdProgramaGeneralMotivacion = PGM.Id                                 WHERE                                     PGMA.IdPGeneral = @IdPGeneral                                     AND PGMA.Estado = 1                                     AND PGM.Estado = 1                                 GROUP BY                                     PGM.Id, PGM.Nombre"; var resultado = await _dapperRepository.QueryDapperAsync(query, new { IdPGeneral = idPGeneral }).ConfigureAwait(false); if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]")) { return JsonConvert.DeserializeObject<List<DescripcionRepoDTO>>(resultado); } return new List<DescripcionRepoDTO>(); }

        public async Task<List<DetalleRepoDTO>> ObtenerDetallesAsync(int idPGeneral) { var query = @"                                 SELECT PGAD.Id, PGAD.IdProgramaGeneralArgumento, PGAD.Detalle                                 FROM pla.T_ProgramaGeneralArgumentoDetalle AS PGAD                                 INNER JOIN pla.T_ProgramaGeneralArgumento AS PGA ON PGAD.IdProgramaGeneralArgumento = PGA.Id                                 WHERE PGA.IdPGeneral = @IdPGeneral AND PGAD.Estado = 1;"; var resultado = await _dapperRepository.QueryDapperAsync(query, new { IdPGeneral = idPGeneral }).ConfigureAwait(false); if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]")) { return JsonConvert.DeserializeObject<List<DetalleRepoDTO>>(resultado); } return new List<DetalleRepoDTO>(); }

        public async Task<List<ArgumentoDetalleMotivacionRepoDTO>> ObtenerArgumentoDetalleAsync(int idPGeneral) { var query = @"                             SELECT                                 PGADM.IdProgramaGeneralArgumentoDetalle,                                 PGADM.IdProgramaMotivacion                             FROM pla.T_ProgramaGeneralArgumentoDetalleMotivacion AS PGADM                             INNER JOIN pla.T_ProgramaGeneralArgumentoDetalle AS PGAD                                 ON PGADM.IdProgramaGeneralArgumentoDetalle = PGAD.Id                             INNER JOIN pla.T_ProgramaGeneralArgumento AS PGA                                 ON PGAD.IdProgramaGeneralArgumento = PGA.Id                             WHERE PGA.IdPGeneral = @IdPGeneral AND PGADM.Estado = 1;"; var resultado = await _dapperRepository.QueryDapperAsync(query, new { IdPGeneral = idPGeneral }).ConfigureAwait(false); if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]")) { return JsonConvert.DeserializeObject<List<ArgumentoDetalleMotivacionRepoDTO>>(resultado); } return new List<ArgumentoDetalleMotivacionRepoDTO>(); }

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
                    SELECT Id, IdProgramaGeneralArgumentoDetalle, IdProgramaMotivacion, Estado,
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

    }
}
