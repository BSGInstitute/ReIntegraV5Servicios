using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PgeneralCriterioEvaluacionHijoRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PgeneralCriterioEvaluacionHijo
    /// </summary>
    public class PgeneralCriterioEvaluacionHijoRepository : GenericRepository<TPgeneralCriterioEvaluacionHijo>, IPgeneralCriterioEvaluacionHijoRepository
    {
        private Mapper _mapper;

        public PgeneralCriterioEvaluacionHijoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralCriterioEvaluacionHijo, PgeneralCriterioEvaluacionHijo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPgeneralCriterioEvaluacionHijo MapeoEntidad(PgeneralCriterioEvaluacionHijo entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralCriterioEvaluacionHijo modelo = _mapper.Map<TPgeneralCriterioEvaluacionHijo>(entidad);

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

        public TPgeneralCriterioEvaluacionHijo Add(PgeneralCriterioEvaluacionHijo entidad)
        {
            try
            {
                var PgeneralCriterioEvaluacionHijo = MapeoEntidad(entidad);
                base.Insert(PgeneralCriterioEvaluacionHijo);
                return PgeneralCriterioEvaluacionHijo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralCriterioEvaluacionHijo Update(PgeneralCriterioEvaluacionHijo entidad)
        {
            try
            {
                var PgeneralCriterioEvaluacionHijo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralCriterioEvaluacionHijo.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralCriterioEvaluacionHijo);
                return PgeneralCriterioEvaluacionHijo;
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


        public IEnumerable<TPgeneralCriterioEvaluacionHijo> Add(IEnumerable<PgeneralCriterioEvaluacionHijo> listadoEntidad)
        {
            try
            {
                List<TPgeneralCriterioEvaluacionHijo> listado = new List<TPgeneralCriterioEvaluacionHijo>();
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

        public IEnumerable<TPgeneralCriterioEvaluacionHijo> Update(IEnumerable<PgeneralCriterioEvaluacionHijo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralCriterioEvaluacionHijo> listado = new List<TPgeneralCriterioEvaluacionHijo>();
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
        public PgeneralCriterioEvaluacionHijo? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT 
                        Id,
		                IdPGeneral AS IdPgeneral,
		                ConsiderarNota,
		                Porcentaje,
		                IdModalidadCurso,
		                IdTipoPromedio,
		                IdPGeneral_Hijo AS IdPgeneralHijo,
		                IdCriterioEvaluacion,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
                    FROM pla.T_PGeneralCriterioEvaluacionHijo
                    WHERE Id=@id AND Estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralCriterioEvaluacionHijo>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Inserta modalidad al curso hijo 
        public void InsertarModalidadPGHIjo(PgeneralModalidad pgeneralModalidad)
        {
            try
            {
                var query = _dapperRepository.QuerySPDapper("pla.SP_InsertarPgeneralCriterio", new
                {
                    IdPGeneral = pgeneralModalidad.IdPgeneral,
                    IdPGeneralModalidadCurso = pgeneralModalidad.IdModalidadCurso
                });
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> ProgramaGeneralBeneficio </returns>
        public List<PGeneralCursoCriterioHijoVistaDTO> ListarPadreCursosCriteriosAonline(int idPgeneral)
        {
            try
            {
                List<PGeneralCursoCriterioHijoVistaDTO> programasGenerales = new();
                var query = "SELECT Id,IdProgramaGeneralHijo, Nombre,ConsiderarNota,Porcentaje,IdModalidadCurso,EsCurso,IdCriterioEvaluacion FROM pla.V_TPgeneralCE_ObtenerHijos WHERE IdModalidadCurso=1 and IdPgeneral_Padre = @idPgeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPgeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralCursoCriterioHijoVistaDTO>>(resultado)!;
                }
                return programasGenerales;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> ProgramaGeneralBeneficio </returns>
        public List<PGeneralCursoCriterioHijoVistaDTO> ListarPadreCursosCriteriosPresencial(int idPgeneral)
        {
            try
            {
                List<PGeneralCursoCriterioHijoVistaDTO> rpta = new();
                var query = @"SELECT
	                            Id,
	                            IdProgramaGeneralHijo,
	                            Nombre,
	                            ConsiderarNota,
	                            Porcentaje,
	                            IdModalidadCurso,
	                            EsCurso,
	                            IdCriterioEvaluacion,
	                            IdPGeneral_Padre AS IdPGeneral_Padre
                            FROM pla.V_TPgeneralCE_ObtenerHijos
                            WHERE IdModalidadCurso=0 and IdPgeneral_Padre = @idPgeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPgeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PGeneralCursoCriterioHijoVistaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> ProgramaGeneralBeneficio </returns>
        public List<PGeneralCursoCriterioHijoVistaDTO> ListarPadreCursosCriteriosOnline(int idPgeneral)
        {
            try
            {
                List<PGeneralCursoCriterioHijoVistaDTO> rpta = new();
                var query = "SELECT Id,IdProgramaGeneralHijo, Nombre,ConsiderarNota,Porcentaje,IdModalidadCurso,EsCurso,IdCriterioEvaluacion FROM pla.V_TPgeneralCE_ObtenerHijos WHERE IdModalidadCurso=2 and IdPgeneral_Padre = @idPgeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPgeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PGeneralCursoCriterioHijoVistaDTO>>(resultado)!;
                }
                return rpta;
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
        /// obtiene los registros por el IdPGeneral
        /// </summary>   
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <returns> IEnumerable<PGeneralModalidadDTO> </returns>
        public IEnumerable<PGeneralModalidadDTO> ObtenerModalidadesPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _query = "SELECT DISTINCT IdModalidadCurso,IdPgeneral FROM pla.T_PgeneralCriterioEvaluacionHijo WHERE IdPgeneral= @IdPgeneral and Estado=1";
                var pgeneralDB = _dapperRepository.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralModalidadDTO>>(pgeneralDB);

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
        /// Realiza una eliminacion logica a la tabla [T_PGeneralCriterioEvaluacionHijo] por el IdPGeneral y IdModalidadCurso
        /// </summary>   
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <param name="idModalidadCurso"> (PK) de T_ModalidadCurso </param>
        /// <returns> </returns>
        public void EliminarPorIdPGeneralYIdModalidad(int idPGeneral, int idModalidadCurso, string usuario)
        {
            try
            {
                var query = _dapperRepository.QuerySPDapper("[pla].[SP_EliminacionCriterioEvaluacionHijoV5]", new
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
    }
}
