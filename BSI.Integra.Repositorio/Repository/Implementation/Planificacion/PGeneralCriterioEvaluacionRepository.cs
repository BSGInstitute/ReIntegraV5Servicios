using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PGeneralCriterioEvaluacionRepository
    /// Autor: Gilmer Qm
    /// Fecha: 16/06/2023
    /// <summary>
    /// Gestión general de T_PGeneralCriterioEvaluacion
    /// </summary>
    public class PGeneralCriterioEvaluacionRepository : GenericRepository<TPgeneralCriterioEvaluacion>, IPGeneralCriterioEvaluacionRepository
    {
        private Mapper _mapper;
        public PGeneralCriterioEvaluacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralCriterioEvaluacion, PgeneralCriterioEvaluacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPgeneralCriterioEvaluacion MapeoEntidad(PgeneralCriterioEvaluacion entidad)
        {
            try
            {
                TPgeneralCriterioEvaluacion modelo = _mapper.Map<TPgeneralCriterioEvaluacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPgeneralCriterioEvaluacion Add(PgeneralCriterioEvaluacion entidad)
        {
            try
            {
                var PgeneralCriterioEvaluacion = MapeoEntidad(entidad);
                base.Insert(PgeneralCriterioEvaluacion);
                return PgeneralCriterioEvaluacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPgeneralCriterioEvaluacion Update(PgeneralCriterioEvaluacion entidad)
        {
            try
            {
                var PgeneralCriterioEvaluacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralCriterioEvaluacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralCriterioEvaluacion);
                return PgeneralCriterioEvaluacion;
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
        public IEnumerable<TPgeneralCriterioEvaluacion> Add(IEnumerable<PgeneralCriterioEvaluacion> listadoEntidad)
        {
            try
            {
                List<TPgeneralCriterioEvaluacion> listado = new List<TPgeneralCriterioEvaluacion>();
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
        public IEnumerable<TPgeneralCriterioEvaluacion> Update(IEnumerable<PgeneralCriterioEvaluacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralCriterioEvaluacion> listado = new List<TPgeneralCriterioEvaluacion>();
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
        /// Version:/ 1.0
        /// <summary>
        /// </summary>
        /// <returns> ProgramaGeneralBeneficio </returns>
        public PgeneralCriterioEvaluacion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id	,
		                IdPGeneral AS IdPgeneral,
		                IdModalidadCurso,
		                Nombre,
		                Porcentaje,
		                IdCriterioEvaluacion,
		                IdTipoPromedio,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
                    FROM pla.T_PGeneralCriterioEvaluacion
                    WHERE Id=@id AND Estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralCriterioEvaluacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 16/06/2023
        /// <summary>
        /// Obtiene la  lista de  T_PGeneralCriterioEvaluacion por el (FK) IdPGeneral
        /// </summary>
        /// <param name="idPgeneral"></param>
        /// <returns> Lista PGeneralCriterioEvaluacionDTO </returns>
        public List<PGeneralCriterioEvaluacionDTO> ObtenerPGcriteriosEvaluacionAOnline(int idPgeneral)
        {
            try
            {
                var query = @"SELECT IdPGeneral AS IdPgeneral,
                                   IdModalidadCurso,
                                   Nombre,
                                   Porcentaje,
                                   IdCriterioEvaluacion,
                                   IdTipoPromedio
                            FROM pla.T_PGeneralCriterioEvaluacion
                            WHERE IdModalidadCurso = 1
                                  AND IdPGeneral = @IdPgeneral
                                  AND Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<PGeneralCriterioEvaluacionDTO>>(resultado)!;
                }
                return new List<PGeneralCriterioEvaluacionDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// <summary>
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista PGeneralCriterioEvaluacionDTO </returns>
        public List<PGeneralCriterioEvaluacionDTO> ObtenerPGcriteriosEvaluacionOnline(int idPgeneral)
        {
            try
            {
                List<PGeneralCriterioEvaluacionDTO> rpta = new();
                var query = "SELECT Id, IdPgeneral, IdModalidadCurso, Nombre, Porcentaje,IdCriterioEvaluacion,IdTipoPromedio FROM pla.T_PgeneralCriterioEvaluacion WHERE IdModalidadCurso = 2 and IdPgeneral = @idPgeneral and Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PGeneralCriterioEvaluacionDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// <summary>
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista PGeneralCriterioEvaluacionDTO </returns>
        public List<PGeneralCriterioEvaluacionDTO> ObtenerPGcriteriosEvaluacionPresencial(int idPgeneral)
        {
            try
            {
                var query = "SELECT Id, IdPgeneral, IdModalidadCurso, Nombre, Porcentaje,IdCriterioEvaluacion,IdTipoPromedio FROM pla.T_PgeneralCriterioEvaluacion WHERE IdModalidadCurso = 0 and IdPgeneral = @idPgeneral and Estado=1";
                var resutlado = _dapperRepository.QueryDapper(query, new { IdPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(resutlado) && !resutlado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<PGeneralCriterioEvaluacionDTO>>(resutlado)!;
                }
                return new List<PGeneralCriterioEvaluacionDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 28/06/2023
        /// <summary>
        /// Obtiene las distintas modalidades por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> IEnumerable<PGeneralModalidadDTO> </returns> 
        public IEnumerable<PGeneralModalidadDTO> ObtenerModalidadesPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = "SELECT DISTINCT IdModalidadCurso,IdPgeneral FROM pla.T_PgeneralCriterioEvaluacion WHERE IdPgeneral= @IdPgeneral and Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralModalidadDTO>>(resultado)!;
                return new List<PGeneralModalidadDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary> 
        /// Realiza una eliminacion logica a la tabla [T_PGeneralCriterioEvaluacion] por el IdPGeneral y IdModalidadCurso
        /// eliminamos criterios de manera logica cuando la modalidad de un curso padre cambie  
        /// </summary>   
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <param name="idModalidadCurso"> (PK) de T_ModalidadCurso </param>
        /// <returns> </returns> 
        public void EliminarPorIdPGeneralIdModalidad(int idPGeneral, int idModalidadCurso, string usuario)
        {
            try
            {
                var query = _dapperRepository.QuerySPDapper("[pla].[SP_EliminacionCriterioEvaluacionV5]", new
                {
                    IdPGeneral = idPGeneral,
                    IdModalidadCurso = idModalidadCurso,
                    Usuario = usuario
                });
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo de PgeneralCriterioEvaluacion por medio del IdModadlidadCurso
        /// </summary>
        /// <param name="idModalidadCurso"></param>
        /// <returns> Lista DTO - List<PgeneralCriterioEvaluacion> </returns>
        public IEnumerable<PgeneralCriterioEvaluacion> ObtenerPorIdModalidadCurso(int idModalidadCurso)
        {
            try
            {
                var query = @"SELECT 
                                Id,
                                IdPGeneral,
                                IdModalidadCurso,
                                Nombre,
                                Porcentaje,
                                IdCriterioEvaluacion,
                                IdTipoPromedio,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion 
                            FROM 
                                pla.T_PGeneralCriterioEvaluacion 
                            WHERE 
                                IdModalidadCurso = @IdModalidadCurso AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdModalidadCurso = idModalidadCurso });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralCriterioEvaluacion>>(resultado)!;
                }
                return new List<PgeneralCriterioEvaluacion>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGCER-OPIMC-001@Error en ObtenerPorIdModalidadCurso() {ex.Message}", ex);
            }
        }
    }
}
