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
    }
}
