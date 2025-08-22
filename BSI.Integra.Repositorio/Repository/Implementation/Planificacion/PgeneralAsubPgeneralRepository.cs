using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PgeneralAsubPgeneralRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 25/07/2023
    /// <summary>
    /// Gestión general de T_PgeneralAsubPgeneral
    /// </summary>
    public class PgeneralAsubPgeneralRepository : GenericRepository<TPgeneralAsubPgeneral>, IPgeneralAsubPgeneralRepository
    {
        private Mapper _mapper;

        public PgeneralAsubPgeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralAsubPgeneral, PgeneralAsubPgeneral>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralAsubPgeneralVersionPrograma, PgeneralAsubPgeneralVersionPrograma>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPgeneralAsubPgeneral MapeoEntidad(PgeneralAsubPgeneral entidad)
        {
            try
            {
                TPgeneralAsubPgeneral modelo = _mapper.Map<TPgeneralAsubPgeneral>(entidad);
                if (entidad.PgeneralAsubPgeneralVersionProgramas != null && entidad.PgeneralAsubPgeneralVersionProgramas.Count > 0)
                    modelo.TPgeneralAsubPgeneralVersionProgramas = _mapper.Map<List<TPgeneralAsubPgeneralVersionPrograma>>(entidad.PgeneralAsubPgeneralVersionProgramas);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPgeneralAsubPgeneral Add(PgeneralAsubPgeneral entidad)
        {
            try
            {
                var PgeneralAsubPgeneral = MapeoEntidad(entidad);
                base.Insert(PgeneralAsubPgeneral);
                return PgeneralAsubPgeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPgeneralAsubPgeneral Update(PgeneralAsubPgeneral entidad)
        {
            try
            {
                var PgeneralAsubPgeneral = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralAsubPgeneral.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralAsubPgeneral);
                return PgeneralAsubPgeneral;
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
        public IEnumerable<TPgeneralAsubPgeneral> Add(IEnumerable<PgeneralAsubPgeneral> listadoEntidad)
        {
            try
            {
                List<TPgeneralAsubPgeneral> listado = new List<TPgeneralAsubPgeneral>();
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
        public IEnumerable<TPgeneralAsubPgeneral> Update(IEnumerable<PgeneralAsubPgeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralAsubPgeneral> listado = new List<TPgeneralAsubPgeneral>();
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
        /// Obtiene todos los registros de T_PgeneralAsubPgeneral.
        /// </summary>
        /// <param name="id">Id PgeneralAsubPgeneral</param>
        /// <returns> PgeneralAsubPgeneral </returns>
        public PgeneralAsubPgeneral? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                   SELECT Id,
		                IdPGeneral_Padre AS IdPgeneralPadre,
		                IdPGeneral_Hijo AS IdPgeneralHijo,
		                Orden,
                        EsVisiblePortal,
                        IdCiclo,                
                        IdModulo,                        
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
                    FROM pla.T_PGeneralASubPGeneral WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralAsubPgeneral>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PgSpg-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PgeneralAsubPgeneral.
        /// </summary>
        /// <param name="ids">Ids PgeneralAsubPgeneral</param>
        /// <returns> PgeneralAsubPgeneral </returns>
        public IEnumerable<PgeneralAsubPgeneral> ObtenerPorIds(List<int> ids)
        {
            try
            {
                var query = @"
                   SELECT Id,
		                IdPGeneral_Padre AS IdPgeneralPadre,
		                IdPGeneral_Hijo AS IdPgeneralHijo,
		                Orden,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
                    FROM pla.T_PGeneralASubPGeneral WHERE Estado=1 AND Id IN @ids";
                var resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralAsubPgeneral>>(resultado)!;
                }
                return new List<PgeneralAsubPgeneral>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PgSpg-OPIs-001@Error en ObtenerPorIds() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PgeneralAsubPgeneral.
        /// </summary>
        /// <param name="id">Id PgeneralAsubPgeneral</param>
        /// <returns> PgeneralAsubPgeneral </returns>
        public IEnumerable<PgeneralAsubPgeneral> ObtenerPorIdPgeneralPadre(int idPgeneralPadre)
        {
            try
            {
                var query = @"
                   SELECT Id,
		                IdPGeneral_Padre AS IdPgeneralPadre,
		                IdPGeneral_Hijo AS IdPgeneralHijo,
		                Orden,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
                    FROM pla.T_PGeneralASubPGeneral WHERE Estado=1 AND IdPGeneral_Padre=@idPgeneralPadre";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPgeneralPadre });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralAsubPgeneral>>(resultado)!;
                }
                return new List<PgeneralAsubPgeneral>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PgSpg-OIP-001@Error en ObtenerPorIdPgeneralPadre() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos hijos de un programa General para el CRUD
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> List<PGeneralASubPGeneralCursoHijo> </returns>
        public IEnumerable<PgeneralAsubPgeneralCursoHijoDTO> ObtenerCursosHijosPorIdPgeneral(int idPGeneral)
        {
            try
            {
                var query = @"SELECT 
                        IdTroncalGeneral,
	                    Nombre,
	                    IdCurso,
	                    IdPadre,
	                    Orden,
	                    EsVisiblePortal,
	                    IdCiclo,
	                    IdModulo
                    FROM pla.V_TPGeneralASubPGeneral_CursosHijos
                    WHERE EstadoTroncal = 1
                          AND EstadoPGeneralASubPGeneral = 1
                          AND IdPadre = @IdPgeneral
                    ORDER BY Orden;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    var rpta = JsonConvert.DeserializeObject<IEnumerable<PgeneralAsubPgeneralCursoHijoDTO>>(resultado)!;
                    foreach (var item in rpta)
                    {
                        var queryVersion = @"SELECT ISNULL(IdVersionPrograma, 4) AS Valor FROM 
                                        pla.V_TPGeneralASubPGeneral_VersionPrograma 
                                        WHERE Estado = 1 
                                                AND IdPgeneralASubPgeneral = @IdPgeneralASubPgeneral";
                        var resultadoVersion = _dapperRepository.QueryDapper(queryVersion, new { IdPgeneralASubPgeneral = item.IdCurso });
                        if (!string.IsNullOrEmpty(resultadoVersion) && resultadoVersion != "[]")
                        {
                            var versiones = JsonConvert.DeserializeObject<List<IntDTO>>(resultadoVersion)!;
                            item.Versiones = versiones.Select(x => x.Valor!.Value).ToList();
                        }
                        else
                            item.Versiones = new();
                    }
                    return rpta;
                }
                return new List<PgeneralAsubPgeneralCursoHijoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PgSpg-OCHIPg-001@Error en ObtenerCursosHijosPorIdSubPgeneral() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos hijos de un programa General para el CRUD
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> List<PGeneralASubPGeneralCursoHijo> </returns>
        public PgeneralAsubPgeneralCursoHijoDTO? ObtenerCursosHijosPorIdSubPgeneral(int idSubPgeneral)
        {
            try
            {
                var query = @"SELECT 
                        IdTroncalGeneral,
	                    Nombre,
	                    IdCurso,
	                    IdPadre,
	                    Orden,
	                    EsVisiblePortal,
	                    IdCiclo,
	                    IdModulo
                    FROM pla.V_TPGeneralASubPGeneral_CursosHijos
                    WHERE EstadoTroncal = 1
                          AND EstadoPGeneralASubPGeneral = 1
                          AND IdCurso = @idSubPgeneral
                    ORDER BY Orden;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idSubPgeneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    var rpta = JsonConvert.DeserializeObject<PgeneralAsubPgeneralCursoHijoDTO>(resultado)!;
                    var queryVersion = @"SELECT ISNULL(IdVersionPrograma, 4) AS Valor FROM 
                                    pla.V_TPGeneralASubPGeneral_VersionPrograma 
                                    WHERE Estado = 1 
                                            AND IdPgeneralASubPgeneral = @IdPgeneralASubPgeneral";
                    var resultadoVersion = _dapperRepository.QueryDapper(queryVersion, new { IdPgeneralASubPgeneral = rpta.IdCurso });
                    if (!string.IsNullOrEmpty(resultadoVersion) && resultadoVersion != "[]")
                    {
                        var versiones = JsonConvert.DeserializeObject<List<IntDTO>>(resultadoVersion)!;
                        rpta.Versiones = versiones.Select(x => x.Valor!.Value).ToList();
                    }
                    else
                        rpta.Versiones = new();
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PgSpg-OCHISpg-001@Error en ObtenerCursosHijosPorIdSubPgeneral() {ex.Message}", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PGeneralASubPGeneral.
        /// </summary>
        /// <returns> List<PGeneralASubPGeneralDTO> </returns>
        public IEnumerable<PgeneralAsubPgeneralDTO> ObtenerPGeneralASubPGeneral()
        {
            try
            {
                List<PgeneralAsubPgeneralDTO> rpta = new List<PgeneralAsubPgeneralDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPGeneral_Padre,
	                    IdPGeneral_Hijo,
	                    Orden,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_PGeneralASubPGeneral
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<PgeneralAsubPgeneralDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PGeneralASubPGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<PGeneralASubPGeneralComboDTO> </returns>
        public IEnumerable<PGeneralASubPGeneralComboDTO> ObtenerCombo()
        {
            try
            {
                List<PGeneralASubPGeneralComboDTO> rpta = new List<PGeneralASubPGeneralComboDTO>();
                var query = @"
                    SELECT
	                    PGSPG.Id,
	                    PGP.Nombre AS PGeneralPadre,
	                    PGH.Nombre AS PGeneralHijo
                    FROM pla.T_PGeneralASubPGeneral AS PGSPG
                    INNER JOIN pla.T_PGeneral AS PGP
	                    ON PGSPG.IdPGeneral_Padre = PGP.Id
	                    AND PGP.Estado = 1
                    INNER JOIN pla.T_PGeneral AS PGH
	                    ON PGSPG.IdPGeneral_Hijo = PGH.Id
	                    AND PGH.Estado = 1
                    WHERE PGH.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PGeneralASubPGeneralComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PGeneralASubPGeneral para mostrarse en combo.
        /// </summary>
        /// <param name="idPgeneral">Id de Programa General Padre</param>
        /// <returns> List<PGeneralASubPGeneralComboDTO> </returns>
        public List<PgeneralHijoDTO> ObtenerPGeneralHijos(int idPgeneral)
        {
            try
            {
                var listaprogramaGeneral = new List<PgeneralHijoDTO>();
                string query = @"SELECT Id, IdPgeneral, Nombre, Pg_titulo, pw_duracion, Orden
                                FROM pla.V_TPgeneral_ObtenerHijos
                                WHERE IdPGeneral_Padre = @IdPgeneral 
                                    AND Estado=1 ORDER BY Orden ASC";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaprogramaGeneral = JsonConvert.DeserializeObject<List<PgeneralHijoDTO>>(resultado);
                }
                return listaprogramaGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PGeneralASubPGeneral para mostrarse en combo.
        /// </summary>
        /// <param name="idPgeneral">Id de Programa General Padre</param>
        /// <returns> List<PGeneralASubPGeneralComboDTO> </returns>
        public async Task<List<PgeneralHijoDTO>> ObtenerPGeneralHijosAsync(int idPgeneral)
        {
            try
            {
                var listaprogramaGeneral = new List<PgeneralHijoDTO>();
                string query = @"SELECT Id, IdPgeneral, Nombre, Pg_titulo, pw_duracion, Orden
                                FROM pla.V_TPgeneral_ObtenerHijos
                                WHERE IdPGeneral_Padre = @IdPgeneral 
                                    AND Estado=1 ORDER BY Orden ASC";
                var resultado = await _dapperRepository.QueryDapperAsync(query, new { IdPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaprogramaGeneral = JsonConvert.DeserializeObject<List<PgeneralHijoDTO>>(resultado);
                }
                return listaprogramaGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/07/2022
        /// Version: 1.0
        /// <summary>
        /// Metodo que retorna lista de secciones de documento del programa general apuntando a la nueva version
        /// </summary>
        /// <param name="idPgeneral">Id de Programa General Padre</param>
        /// <param name="version">Version del Programa</param>
        /// <returns> List<PgeneralHijoDTO> </returns>
        public List<PgeneralHijoDTO> ObtenerPGeneralHijosVersion(int idPgeneral, string version)
        {
            try
            {
                var rpta = new List<PgeneralHijoDTO>();
                string queryListaPGeneral = "";
                if (version != null && version != "")
                {
                    queryListaPGeneral = "SELECT Id, IdPgeneral, Nombre, Pg_titulo, pw_duracion, Orden FROM pla.V_TPgeneral_ObtenerHijosVersion WHERE IdPGeneral_Padre = @IdPgeneral and IdVersionPrograma=@IdVersion and Estado = 1 ORDER BY Orden ASC";
                }
                else
                {
                    queryListaPGeneral = "SELECT Id, IdPgeneral, Nombre, Pg_titulo, pw_duracion, Orden FROM pla.V_TPgeneral_ObtenerHijosVersion WHERE IdPGeneral_Padre = @IdPgeneral and IdVersionPrograma IS NULL and Estado = 1 ORDER BY Orden ASC";
                }
                var listaprogramaGeneral = _dapperRepository.QueryDapper(queryListaPGeneral, new { IdPgeneral = idPgeneral, IdVersion = version });
                if (!string.IsNullOrEmpty(listaprogramaGeneral) && !listaprogramaGeneral.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PgeneralHijoDTO>>(listaprogramaGeneral);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos hijos de un programa General para Congelamiento EstructuraCurricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<CursoHijoIdDTO> ObtenerCursosCongelamientoEstrucuraCurricular(int idMatriculaCabecera)
        {
            List<CursoHijoIdDTO> cursos = new List<CursoHijoIdDTO>();
            var _query = string.Empty;
            _query = @"SELECT IdPGeneral AS Id,
                               GEN.Nombre,
                               PEM.Duracion,
                               PES.Id AS IdPEspecifico
                        FROM ope.T_PEspecificoMatriculaAlumno PEM
                            INNER JOIN pla.T_PEspecifico PES
                                ON PES.Id = PEM.IdPEspecifico
                            INNER JOIN pla.T_PGeneral GEN
                                ON GEN.Id = PES.IdProgramaGeneral
                        WHERE PEM.IdMatriculaCabecera = @IdMatriculaCabecera
                              AND PEM.Estado = 1
                        ORDER BY PES.Id ASC";
            var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdMatriculaCabecera = idMatriculaCabecera });
            if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
            {
                cursos = JsonConvert.DeserializeObject<List<CursoHijoIdDTO>>(respuestaDapper)!;
            }
            return cursos;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos hijos de un programa General para EstructuraCurricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<CursoHijoDuracionDTO> </returns>
        public List<CursoHijoDuracionDTO> ObtenerCursosEstrucuraCurricular(int idMatriculaCabecera)
        {

            List<CursoHijoDuracionDTO> cursos = new List<CursoHijoDuracionDTO>();
            cursos = ObtenerCursosCongeladosEstrucuraCurricular(idMatriculaCabecera);
            if (cursos.Count <= 0)
            {
                var query = string.Empty;
                //query = @"SELECT 
                //            Nombre, Duracion 
                //        FROM 
                //            pla.V_PgeneralCursosHijosporVersion 
                //        WHERE 
                //            IdMatriculaCabecera = @IdMatriculaCabecera AND Estado = 1 ORDER BY FechaCreacion ASC";
                query = @"SELECT Nombre,Duracion FROM pw.V_PW_EstructuraCurricularProgramaCongelada WHERE IdMatriculaCabecera = @IdMatriculaCabecera ORDER BY Orden";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    cursos = JsonConvert.DeserializeObject<List<CursoHijoDuracionDTO>>(respuestaDapper)!;
                }
            }
            return cursos;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos hijos congelados de un programa General para EstructuraCurricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<CursoHijoDuracionDTO> ObtenerCursosCongeladosEstrucuraCurricular(int idMatriculaCabecera)
        {
            List<CursoHijoDuracionDTO> cursos = new List<CursoHijoDuracionDTO>();
            var query = string.Empty;
            query = @"SELECT 
                        Nombre, Duracion 
                      FROM 
                        pla.V_PgeneralCursosCongeladosHijosporVersion 
                      WHERE 
                        IdMatriculaCabecera = @IdMatriculaCabecera AND Estado = 1 ORDER BY FechaCreacion ASC";
            var respuestaDapper = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
            if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
            {
                cursos = JsonConvert.DeserializeObject<List<CursoHijoDuracionDTO>>(respuestaDapper)!;
            }
            return cursos;
        }
        /// Autor: Gretel Canasa
        /// Fecha: 04/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Modulo.
        /// </summary>
        /// <returns> List<PGeneralASubPGeneralDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerModuloAsync()
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Descripcion as Nombre
                    FROM pla.T_Modulo
                    WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PgSpg-OVPA-001@Error en ObtenerModuloAsync() {ex.Message}", ex);
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 04/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Ciclo.
        /// </summary>
        /// <returns> List<PGeneralASubPGeneralDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerCicloAsync()
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Descripcion as Nombre
                    FROM pla.T_Ciclo
                    WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PgSpg-OVPA-001@Error en ObtenerModuloAsync() {ex.Message}", ex);
            }
        }
    }
}



