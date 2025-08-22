using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MatriculaCabeceraRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión general de T_MatriculaCabecera
    /// </summary>
    public class MatriculaCabeceraRepository : GenericRepository<TMatriculaCabecera>, IMatriculaCabeceraRepository
    {
        private Mapper _mapper;
        private const string columnsBase = @"
                Id,
		        CodigoMatricula,
		        IdAlumno,
		        IdPEspecifico,
		        IdEstadoPagoMatricula,
		        EstadoMatricula,
		        FechaMatricula,
		        EmpresaRuc,
		        EmpresaNombre,
		        EmpresaContacto,
		        EmpresaEmail,
		        EmpresaPaga,
		        EmpresaObservaciones,
		        IdDocumentoPago,
		        IdCoordinador,
		        IdAsesor,
		        IdEstado_matricula,
		        FechaSuspendido,
		        UsuarioCoordinadorAcademico,
		        ObservacionGeneralOperaciones,
		        UsuarioCoordinadorSupervision,
		        IdCronograma,
		        IdPeriodo,
		        UsuarioCoordinadorPreAsignacion,
		        VerificacionConforme,
		        FechaMatriculaValidada,
		        FechaPagoValidada,
		        FechaRetiro,
		        Estado,
		        UsuarioCreacion,
		        UsuarioModificacion,
		        FechaCreacion,
		        FechaModificacion,
		        RowVersion,
		        IdMigracion,
		        GrupoCurso,
		        IdSubEstadoMatricula,
		        IdPaquete,
		        FechaFinalizacion,
		        IdEstadoMatriculaCertificado,
		        IdSubEstadoMatriculaCertificado,
		        EsInhouse,
		        FechaPorMatricularMatriculado,
		        IdCategoriaAlumno";

        public MatriculaCabeceraRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMatriculaCabecera, MatriculaCabecera>(MemberList.None).ReverseMap();
                cfg.CreateMap<MatriculaCabeceraDTO, MatriculaCabecera>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMatriculaCabecera MapeoEntidad(MatriculaCabecera entidad)
        {
            try
            {
                //crea la entidad padre
                TMatriculaCabecera modelo = _mapper.Map<TMatriculaCabecera>(entidad);

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

        public TMatriculaCabecera Add(MatriculaCabecera entidad)
        {
            try
            {
                var MatriculaCabecera = MapeoEntidad(entidad);
                base.Insert(MatriculaCabecera);
                return MatriculaCabecera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMatriculaCabecera Update(MatriculaCabecera entidad)
        {
            try
            {
                var MatriculaCabecera = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MatriculaCabecera.RowVersion = entidadExistente.RowVersion;

                base.Update(MatriculaCabecera);
                return MatriculaCabecera;
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


        public IEnumerable<TMatriculaCabecera> Add(IEnumerable<MatriculaCabecera> listadoEntidad)
        {
            try
            {
                List<TMatriculaCabecera> listado = new List<TMatriculaCabecera>();
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

        public IEnumerable<TMatriculaCabecera> Update(IEnumerable<MatriculaCabecera> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMatriculaCabecera> listado = new List<TMatriculaCabecera>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MatriculaCabecera.
        /// </summary>
        /// <returns> List<MatriculaCabeceraDTO> </returns>
        public IEnumerable<MatriculaCabeceraDTO> ObtenerMatriculaCabecera()
        {
            try
            {
                List<MatriculaCabeceraDTO> rpta = new List<MatriculaCabeceraDTO>();
                var query = @"
                    SELECT Id,CodigoMatricula,IdAlumno,IdPEspecifico,IdEstadoPagoMatricula,EstadoMatricula,FechaMatricula,EmpresaRuc,EmpresaNombre,EmpresaContacto,
	                    EmpresaEmail,EmpresaPaga,EmpresaObservaciones,IdDocumentoPago,IdCoordinador,IdAsesor,IdEstado_matricula,FechaSuspendido,UsuarioCoordinadorAcademico,
	                    ObservacionGeneralOperaciones,UsuarioCoordinadorSupervision,IdCronograma,IdPeriodo,UsuarioCoordinadorPreAsignacion,VerificacionConforme,FechaMatriculaValidada,
	                    FechaPagoValidada,FechaRetiro,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,GrupoCurso,IdSubEstadoMatricula,IdPaquete,FechaFinalizacion,
	                    IdEstadoMatriculaCertificado,IdSubEstadoMatriculaCertificado,EsInhouse,FechaPorMatricularMatriculado, Estado
                    FROM fin.T_MatriculaCabecera WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MatriculaCabeceraDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar Taco
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_MatriculaCabecera por Id
        /// </summary>
        /// <returns> List<MatriculaCabeceraDTO> </returns>
        public MatriculaCabecera ObtenerMatriculaCabeceraPorId(int id)
        {
            try
            {
                MatriculaCabecera rpta = new MatriculaCabecera();
                var query = @"
                    SELECT Id,CodigoMatricula,IdAlumno,IdPEspecifico,IdEstadoPagoMatricula,EstadoMatricula,FechaMatricula,EmpresaRuc,EmpresaNombre,EmpresaContacto,
	                    EmpresaEmail,EmpresaPaga,EmpresaObservaciones,IdDocumentoPago,IdCoordinador,IdAsesor,IdEstado_matricula AS IdEstadoMatricula,FechaSuspendido,UsuarioCoordinadorAcademico,
	                    ObservacionGeneralOperaciones,UsuarioCoordinadorSupervision,IdCronograma,IdPeriodo,UsuarioCoordinadorPreAsignacion,VerificacionConforme,FechaMatriculaValidada,
	                    FechaPagoValidada,FechaRetiro,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,GrupoCurso,IdSubEstadoMatricula,IdPaquete,FechaFinalizacion,
	                    IdEstadoMatriculaCertificado,IdSubEstadoMatriculaCertificado,EsInhouse,FechaPorMatricularMatriculado
                    FROM fin.T_MatriculaCabecera WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<MatriculaCabecera>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MatriculaCabecera para mostrarse en combo.
        /// </summary>
        /// <returns> List<MatriculaCabeceraComboDTO> </returns>
        public IEnumerable<MatriculaCabeceraComboDTO> ObtenerCombo()
        {
            try
            {
                IEnumerable<MatriculaCabeceraComboDTO> rpta = new List<MatriculaCabeceraComboDTO>();
                string query = @"
                                SELECT 
                                    Id, CodigoMatricula 
                                FROM 
                                    fin.T_MatriculaCabecera 
                                WHERE 
                                    Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<MatriculaCabeceraComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerFiltro()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id de la Matricula Cabecera asociado al Alumno y al Centro de Costo.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> ValorIntDTO </returns>
        public int ObtenerIdMatriculaCabeceraPorAlumnoCentroCosto(int idAlumno, int idCentroCosto)
        {
            try
            {
                RegistroMedioPagoMatriculaCronogramaDTO registro = new RegistroMedioPagoMatriculaCronogramaDTO();
                string query = @"SELECT TOP 1 TMC.Id as IdMatriculaCabecera from fin.T_MatriculaCabecera AS TMC 
                                  INNER JOIN pla.T_PEspecifico AS TPE ON TPE.IdCentroCosto = @IdCentroCosto 
                                  WHERE TMC.Estado = 1 AND TMC.IdAlumno = @IdAlumno";
                var registroQuery = _dapperRepository.FirstOrDefault(query, new { idCentroCosto, idAlumno });
                if (!string.IsNullOrEmpty(registroQuery) && registroQuery != "null")
                {
                    registro = JsonConvert.DeserializeObject<RegistroMedioPagoMatriculaCronogramaDTO>(registroQuery)!;
                }
                return registro.IdMatriculaCabecera;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id, Codigo y Fecha de Matricula asociado a una Oportunidad utilizando T_MontoPagoCronograma
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> MatriculaCabeceraCodigoFechaDTO </returns>
        public MatriculaCabeceraCodigoFechaDTO? ObtenerDatosMatriculaDesdeMontoPagoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var query = @"
                    SELECT Id,CodigoMatricula,FechaMatricula
                    FROM fin.V_TMatriculaCabecera_MatriculaPorIdOportunidad
                    WHERE IdOportunidad = @IdOportunidad AND EsAprobado = 1 AND Estado = 1";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    return JsonConvert.DeserializeObject<MatriculaCabeceraCodigoFechaDTO>(resultadoQuery);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id, Codigo y Fecha de Matricula asociado a una Oportunidad utilizando T_ClasificacionPersona
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> MatriculaCabeceraCodigoFechaDTO </returns>
        public MatriculaCabeceraCodigoFechaDTO? ObtenerDatosMatriculaDesdeClasificacionPersonaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                MatriculaCabeceraCodigoFechaDTO datosMatricula = new MatriculaCabeceraCodigoFechaDTO();
                var query = @"
                    SELECT  IdMatricula AS Id, CodigoMatricula, FechaMatricula
                    FROM com.V_TOportunidad_CodigoMatricula
                    WHERE IdOportunidad = @IdOportunidad AND EstadoPE = 1";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    return JsonConvert.DeserializeObject<MatriculaCabeceraCodigoFechaDTO>(resultadoQuery);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion de la matriculaCabecera por IdCronograma
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public InformacionMatriculaCabeceraDTO ObtenerInformacionMatriculaCabeceraPorIdCronograma(int idCronograma)
        {
            try
            {
                InformacionMatriculaCabeceraDTO matricula = new InformacionMatriculaCabeceraDTO();
                var _query = "SELECT Id,CodigoMatricula,IdPaquete FROM fin.T_MatriculaCabecera WHERE  Estado = 1 and IdCronograma=@idCronograma";
                var subQuery = _dapperRepository.FirstOrDefault(_query, new { idCronograma });
                if (!string.IsNullOrEmpty(subQuery) && subQuery != "null")
                {
                    matricula = JsonConvert.DeserializeObject<InformacionMatriculaCabeceraDTO>(subQuery);
                }
                return matricula;
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
        /// Obtener el programa especifico del Alumno 
        /// </summary>
        /// <param name="codigoMatricula">Codigo Matricula </param>
        /// <returns>Retorna respuesta: ValorIntDTO</returns> 
        public int ObtenerAlumnoProgramaEspecifico(string codigoMatricula)
        {

            try
            {
                ValorIntDTO pespecifico = new ValorIntDTO();
                var query = "Select IdPespecifico as Valor From fin.T_MatriculaCabecera Where CodigoMatricula=@codigoMatricula";
                var queryDB = _dapperRepository.FirstOrDefault(query, new { CodigoMatricula = codigoMatricula });
                if (!string.IsNullOrEmpty(queryDB) && queryDB != "null")
                {
                    pespecifico = JsonConvert.DeserializeObject<ValorIntDTO>(queryDB);
                }
                var resultado = pespecifico.Valor;
                return resultado.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Autor: Gilmer Quispe
        ///Fecha: 01/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Programa General por Programa Especifico
        /// </summary>
        /// <param name="pespecifico">Id Programa Especifico </param>
        /// <returns> Retorna Respuesta: ValorIntDTO </returns> 
        public int ObtenerProgramaGeneral(int pespecifico)
        {
            try
            {
                ValorIntDTO pgeneral = new ValorIntDTO();
                var query = "Select IdProgramaGeneral as Valor From [pla].[T_PEspecifico] Where Id=@pespecifico";
                var queryDB = _dapperRepository.FirstOrDefault(query, new { pespecifico });
                if (!string.IsNullOrEmpty(queryDB) && queryDB != "null")
                {
                    pgeneral = JsonConvert.DeserializeObject<ValorIntDTO>(queryDB);
                }
                return pgeneral.Valor.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Autor: Gilmer Quispe
        ///Fecha: 06/09/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene Campo fechaFinalizacion de Matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public string ObtenerFechaFinalizacion(int idMatriculaCabecera)
        {
            try
            {
                StringDTO rpta = new StringDTO();
                var _query = "Select FechaFinalizacion as Valor From fin.T_MatriculaCabecera Where Id=@IdMatriculacabecera";
                var query = _dapperRepository.FirstOrDefault(_query, new { IdMatriculacabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(query) && query != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(query);
                }
                return rpta.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza El estado de La matricula Por idMatriculaCabecera, idEstadoMatricula y codigoMatricula
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id de la matricula cabecera</param>
        /// <param name="idEstadoMatricula"> Id del estado de la matricula</param>
        /// <param name="codigoMatricula"> Codigo de la matricula</param>
        /// <returns>True o False</returns>
        public bool ActualizarEstadoMatricula(int idMatriculaCabecera, int idEstadoMatricula, string codigoMatricula)
        {
            try
            {
                var _query = "ope.SP_ActualizarEstadoMatricula";
                var pEspecificoDB = _dapperRepository.QuerySPDapper(_query, new { idMatriculaCabecera, idEstadoMatricula, codigoMatricula });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el estado del matriculado por el IdAlumno
        /// </summary>
        /// <param name="idAlumno"> Id del Alumno</param>
        /// <returns>List<EstadoMatriculadoDTO> </returns>
        public List<EstadoMatriculadoDTO> ObtenerEstadoMatriculadoPorAlumno(int idAlumno)
        {
            try
            {
                var estadoMatriculado = new List<EstadoMatriculadoDTO>();
                var _query = "com.SP_ObtenerEstadoMatriculado";
                var pEspecificoDB = _dapperRepository.QuerySPDapper(_query, new { idAlumno });
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<EstadoMatriculadoDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion de la matricula por el IdAlumno
        /// </summary>
        /// <param name="idAlumno"> Id del Alumno</param>
        /// <returns>List<MatriculaDTO></returns>
        public List<MatriculaDTO> ObtenerMatriculaPorAlumno(int idAlumno)
        {
            try
            {
                string _querymatricula = @"SELECT  Id,CodigoMatricula,NombreProgramaGeneral,
                                            VersionPrograma,IdCentroCosto,Documentos 
                                            FROM fin.V_ObtenerMatriculasPorAlumno 
                                            WHERE IdAlumno = @IdAlumno ";
                var _Matricula = _dapperRepository.QueryDapper(_querymatricula, new { idAlumno });
                return JsonConvert.DeserializeObject<List<MatriculaDTO>>(_Matricula);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el estado de la evaluacion por el codigo de la matricula
        /// </summary>
        /// <param name="codigoMatricula"> codigo de la matricula</param>
        /// <returns>EstadoMatriculadoDTO</returns>
        public EstadoMatriculadoDTO ObtenerEstadoEvaluacionPorCodMatricula(string codigoMatricula)
        {
            try
            {
                var estadoMatriculado = new EstadoMatriculadoDTO();
                var _query = "com.SP_ObtenerEstadoEvaluacion";
                var pEspecifico = _dapperRepository.QuerySPFirstOrDefault(_query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(pEspecifico) && pEspecifico != "null")
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<EstadoMatriculadoDTO>(pEspecifico);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 05/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el estado de la evaluacion por el codigo de la matricula
        /// </summary>
        /// <returns>FiltroDTO/returns>
        public List<ComboDTO> ObtenerObservacionMatricula()
        {
            try
            {
                List<ComboDTO> resultado = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM fin.T_MatriculaObservacion WHERE Estado=1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ComboDTO>>(resultadoQuery)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza usuario coordinador academico de matricula cabecera en v3
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>s
        public bool ActualizarTMatriculaCabecera(string codigoMatricula, string usuario)
        {
            try
            {
                var resultado = new Dictionary<string, bool>();

                string query = _dapperRepository.QuerySPFirstOrDefault("fin.SP_ActualizarMatriculaCabeceraV3", new { CodigoMatricula = codigoMatricula, Usuario = usuario });
                if (!string.IsNullOrEmpty(query))
                {
                    resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query)!;
                }
                return resultado.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MatriculaCabecera.
        /// </summary>
        /// <returns> List<MatriculaCabeceraDTO> </returns>
        public MatriculaCabeceraDTO ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                MatriculaCabeceraDTO respuesta = new MatriculaCabeceraDTO();
                var query = @"
                    SELECT Id,CodigoMatricula,IdAlumno,IdPEspecifico,IdEstadoPagoMatricula,EstadoMatricula,FechaMatricula,EmpresaRuc,EmpresaNombre,EmpresaContacto,
	                    EmpresaEmail,EmpresaPaga,EmpresaObservaciones,IdDocumentoPago,IdCoordinador,IdAsesor,IdEstado_matricula,FechaSuspendido,UsuarioCoordinadorAcademico,
	                    ObservacionGeneralOperaciones,UsuarioCoordinadorSupervision,IdCronograma,IdPeriodo,UsuarioCoordinadorPreAsignacion,VerificacionConforme,FechaMatriculaValidada,
	                    FechaPagoValidada,FechaRetiro,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,GrupoCurso,IdSubEstadoMatricula,IdPaquete,FechaFinalizacion,
	                    IdEstadoMatriculaCertificado,IdSubEstadoMatriculaCertificado,EsInhouse,FechaPorMatricularMatriculado
                    FROM fin.T_MatriculaCabecera WHERE Estado = 1 AND Id = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<MatriculaCabeceraDTO>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene detalle por matricula
        /// </summary>
        /// <param name="id">Id de la matricula cabecera que se desea averiguar(PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Objeto DetalleOportunidadOperacionesDTO para su consulta posterior</returns>
        public DetalleOportunidadOperacionesDTO? ObtenerDetalleMatricula(int id)
        {
            try
            {
                var _query = $@"
                        SELECT Oportunidad.Id AS IdOportunidad, 
                               CentroCosto.Id AS IdCentroCosto, 
                               CentroCosto.Nombre AS NombreCentroCosto, 
                               PGeneral.Id AS IdProgramaGeneral,
                               PGeneral.Nombre AS NombreProgramaGeneral,
                               PEspecifico.Ciudad AS NombreCiudad,
                               Case When Pespecifico.TipoId=1 then 100 
                                    else Escala.EscalaCalificacion
                               End EscalaCalificacion
                        FROM ope.T_OportunidadClasificacionOperaciones AS OportunidadClasificacionOperaciones
                             INNER JOIN com.T_Oportunidad AS Oportunidad ON OportunidadClasificacionOperaciones.IdOportunidad = Oportunidad.Id
                             INNER JOIN pla.T_CentroCosto AS CentroCosto ON CentroCosto.Id = Oportunidad.IdCentroCosto
                             INNER JOIN pla.T_PEspecifico AS PEspecifico ON PEspecifico.IdCentroCosto = CentroCosto.id
                             INNER JOIN pla.T_PGeneral AS PGeneral ON PGeneral.Id = PEspecifico.IdProgramaGeneral
                             INNER JOIN pla.T_AreaCapacitacion AS AreaCapacitacion ON AreaCapacitacion.Id = PGeneral.IdArea
                             INNER JOIN pla.T_SubAreaCapacitacion AS SubAreaCapacitacion ON SubAreaCapacitacion.Id = PGeneral.IdSubArea
                             LEFT  JOIN pla.V_EscalaCalificacionPespecifico AS Escala ON Escala.CodigoCiudad = PEspecifico.Ciudad
                        WHERE IdMatriculaCabecera = @id;
                            ";

                var resultado = _dapperRepository.FirstOrDefault(_query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<DetalleOportunidadOperacionesDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la url de confirmacion de participacion webinar en base a dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias a analizar para obtener la URL</param>
        /// <returns>Cadena con la URL de confirmacion de los participantes de la sesion webinar</returns>
        public string ObtenerUrlConfirmacionParticipacionSesionWebinar(int id, int cantidadDias)
        {
            try
            {
                var respuesta = new StringDTO();
                var query = "ope.SP_ObtenerUrlConfirmacionParticipacionSesionesWebinarNDias";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return respuesta.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la url de confirmacion de participacion webinar en base a dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias a analizar para obtener la URL</param>
        /// <returns>Cadena con la descripcion y fecha del trabajo a presentar</returns>
        public string ObtenerPresentacionTrabajoNDias(int id, int cantidadDias)
        {
            try
            {
                var listaTrabajoCurso = new List<TrabajoCursoAlumnoDTO>();
                var query = $@"ope.SP_ObtenerPresentacionTrabajoNDias";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listaTrabajoCurso = JsonConvert.DeserializeObject<List<TrabajoCursoAlumnoDTO>>(resultado);
                }
                var _htmlFinal = "";

                if (listaTrabajoCurso.Count() > 0 && listaTrabajoCurso != null)
                {
                    var ultimo = listaTrabajoCurso.Last();
                    foreach (var item in listaTrabajoCurso)
                    {
                        _htmlFinal += $@"
                                     <span>
                                            Descripción: {item.DescripcionTrabajo}
                                            Forma de entrega: {item.NombreFormaEntrega}
                                            Fecha límite de entrega: {item.FechaEntrega}
                                     </span>
                                     <br>
                                     <br>";

                        if (!item.Equals(ultimo))
                        {
                            _htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return _htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la presentacion de trabajo final en N Dias (No existe el SP en produccion)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cantidadDias"></param>
        /// <returns></returns>
        public string ObtenerPresentacionTrabajoFinalNDias(int id, int cantidadDias)
        {
            try
            {
                var listaTrabajoCurso = new List<TrabajoCursoAlumnoDTO>();
                var query = $@"ope.SP_ObtenerPresentacionTrabajoFinalNDias";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listaTrabajoCurso = JsonConvert.DeserializeObject<List<TrabajoCursoAlumnoDTO>>(resultado);
                }
                var _htmlFinal = "";

                if (listaTrabajoCurso.Count() > 0 && listaTrabajoCurso != null)
                {
                    var ultimo = listaTrabajoCurso.Last();
                    foreach (var item in listaTrabajoCurso)
                    {
                        _htmlFinal += $@"
                                     <span>
                                            Descripción: {item.DescripcionTrabajo}
                                            Forma de entrega: {item.NombreFormaEntrega}
                                            Fecha límite de entrega: {item.FechaEntrega}
                                     </span>
                                     <br>
                                     <br>";

                        if (!item.Equals(ultimo))
                        {
                            _htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return _htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la sesiones webinar de un dia especifico
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias"></param>
        /// <returns>Obtiene en cadena las sesiones webinar de un dia especifico</returns>
        public string ObtenerSesionesWebinarNDias(int id, int cantidadDias, bool mostrarUrlAcceso)
        {
            try
            {
                var listadoSesionWebinar = new List<SesionWebinarDTO>();
                var query = $@"ope.SP_ObtenerSesionesWebinarNDias";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listadoSesionWebinar = JsonConvert.DeserializeObject<List<SesionWebinarDTO>>(resultado);
                }
                var _htmlFinal = "";

                if (listadoSesionWebinar.Count() > 0 && listadoSesionWebinar != null)
                {
                    //listadoSesionWebinar = listadoSesionWebinar.OrderBy(x => x.NombreAutoEvaluacion).ToList();
                    var ultimo = listadoSesionWebinar.Last();
                    foreach (var item in listadoSesionWebinar)
                    {
                        _htmlFinal += $@"
                                     <span>
                                         <strong>Fecha:</strong> {item.FechaInicio.ToString("dd/MM/yyyy")}
                                         <br/>
                                         <strong>Hora de inicio:</strong> {item.FechaInicio.ToString("hh:mm tt")}
                                         <br/>
                                         <strong>Hora de término:</strong> {item.FechaTermino.ToString("hh:mm tt")}
                                     </span>
                                     <br>
                                     <br>";
                        if (mostrarUrlAcceso)
                        {
                            _htmlFinal += $@"<span> Para conectarse al webinar programado presione el siguiente bot&oacute;n:
                                                <a href='{item.LinkWebinar}' target='_blank'>
                                                    <span>
                                                        conectarse al webinar
                                                    </span>
                                                </a> 
                                            </span>";
                        }

                        if (!item.Equals(ultimo))
                        {
                            _htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return _htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la url del acceso sesion webinar en base a la cantidad de dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las sesiones (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias que se desea analizar desde el dia actual</param>
        /// <returns>Cadena con la sesion confirmada</returns>
        public string ObtenerSesionesWebinarConfirmadasNDias(int id, int cantidadDias, bool mostrarUrlAcceso)
        {
            try
            {
                var listadoSesionWebinar = new List<SesionWebinarDTO>();
                var query = $@"ope.SP_ObtenerSesionesWebinarConfirmadoNDias";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listadoSesionWebinar = JsonConvert.DeserializeObject<List<SesionWebinarDTO>>(resultado);
                }
                var htmlFinal = "";

                if (listadoSesionWebinar.Count() > 0 && listadoSesionWebinar != null)
                {
                    //listadoSesionWebinar = listadoSesionWebinar.OrderBy(x => x.NombreAutoEvaluacion).ToList();
                    var ultimo = listadoSesionWebinar.Last();
                    foreach (var item in listadoSesionWebinar)
                    {
                        htmlFinal += $@"
                                     <span>
                                         <strong>Fecha:</strong> {item.FechaInicio.ToString("dd/MM/yyyy")}
                                         <br/>
                                         <strong>Hora de inicio:</strong> {item.FechaInicio.ToString("hh:mm tt")}
                                         <br/>
                                         <strong>Hora de término:</strong> {item.FechaTermino.ToString("hh:mm tt")}
                                     </span>
                                     <br>
                                     <br>";
                        if (mostrarUrlAcceso)
                        {
                            htmlFinal += $@"<span> Para conectarse al webinar programado presione el siguiente bot&oacute;n:
                                                <a href='{item.LinkWebinar}' target='_blank'>
                                                    <span>
                                                        conectarse al webinar
                                                    </span>
                                                </a> 
                                            </span>";
                        }

                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la url del acceso sesion webinar confirmado en base a la cantidad de dias
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera que la cual se dese obtener las sesiones webinar confirmadas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias a partir del actual del que se desea obtener las sesiones webinar confirmadas</param>
        /// <returns>Lista de objetos (SesionWebinarDTO)</returns>
        public List<SesionWebinarDTO> ObtenerSesionesConfirmadasWebinarNDias(int idMatriculaCabecera, int cantidadDias)
        {
            try
            {
                var listadoSesionWebinar = new List<SesionWebinarDTO>();
                var query = "ope.SP_ObtenerSesionesConfirmadasWebinarNDias";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera, CantidadDias = cantidadDias });
                if (!string.IsNullOrEmpty(resultado))
                {
                    listadoSesionWebinar = JsonConvert.DeserializeObject<List<SesionWebinarDTO>>(resultado);
                }
                return listadoSesionWebinar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el cronograma de pago completo en html
        /// </summary>
        /// <param name="id">Id de la matriculacabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con el cronograma de autoevaluacion completo</returns>
        public string ObtenerCronogramaAutoEvaluacionCompleto(int id)
        {
            try
            {
                var resultadoFinal = new List<AutoEvaluacionCronogramaDetalleDTO>();
                var query = $@"ope.SP_ObtenerAutoevaluacionesAlumnoMatriculado";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<AutoEvaluacionCronogramaDetalleDTO>>(resultado);
                }
                var htmlFinal = "";

                if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                {
                    var ultimo = resultadoFinal.Last();

                    foreach (var item in resultadoFinal)
                    {
                        htmlFinal += $@"
                                     <span>
                                         {item.NombreAutoEvaluacion}
                                         <br/>
                                         Fecha Límite de Autoevaluación: {item.FechaCronograma.ToString("dd/MM/yyyy")}
                                     </span>";
                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>";
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las AutoEvaluaciones vencidas en html
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con las autoevaluaciones vencidas</returns>
        public string ObtenerAutoEvaluacionesVencidas(int id)
        {
            try
            {
                var resultadoFinal = new List<AutoEvaluacionCronogramaDetalleDTO>();
                var query = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculadoTotal";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<AutoEvaluacionCronogramaDetalleDTO>>(resultado);
                }

                var htmlFinal = "";

                if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                {
                    var ultimo = resultadoFinal.Last();
                    foreach (var item in resultadoFinal)
                    {
                        htmlFinal += $@"
                                     <span>
                                         {item.NombreAutoEvaluacion}
                                         <br/>
                                         Fecha Límite de Autoevaluación: {item.FechaCronograma.ToString("dd/MM/yyyy")}
                                     </span>";
                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>";
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las AutoEvaluaciones completas en html
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las autoevaluaciones completas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con las autoevaluaciones completas</returns>
        public string ObtenerAutoEvaluacionesCompletas(int id)
        {
            try
            {
                var resultadoFinal = new List<AutoEvaluacionCompletaCronogramaDetalleDTO>();
                var query = $@"ope.SP_ObtenerAutoevaluacionesCompletasAlumnoMatriculado";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<AutoEvaluacionCompletaCronogramaDetalleDTO>>(resultado);
                }

                var htmlFinal = "";

                if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                {
                    var ultimo = resultadoFinal.Last();
                    foreach (var item in resultadoFinal)
                    {
                        htmlFinal += $@"
                                     <span>
                                      {item.NombreAutoEvaluacion}
                                     <br/>
                                      Nota Obtenida: {item.Nota}
                                     <br/>
                                     Fecha de Evaluación: {item.FechaCronograma.ToString("dd/MM/yyyy")}
                                     </span>";
                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>";
                        }
                    }
                }

                htmlFinal += "";

                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Cantidad de autoevaluaciones pendientes
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de autoevaluaciones pendientes por el alumno</returns>
        public int ObtenerCantidadAutoEvaluacionesPendientes(int id)
        {
            try
            {
                var respuesta = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerCantidadAutoevaluacionesPendientes";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return respuesta.Valor.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las AutoEvaluaciones vencidas en una cantidad de dias exacta
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener el detalle de las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el dia actual</param>
        /// <param name="esFechaExacta">Flag para determinar si es una fecha exacta lo ingresado</param>
        /// <returns>Lista de objetos (AutoEvaluacionCronogramaDetalleDTO)</returns>
        public List<AutoEvaluacionCronogramaDetalleDTO> ObtenerDetalleAutoEvaluacionesVencidaDiaExacto(int id, int cantidadDias, bool esFechaExacta)
        {
            try
            {
                var resultadoAutoEvaluacionesFinal = new List<AutoEvaluacionCronogramaDetalleDTO>();

                var resultadoBool = new BoolDTO();
                var query = $@"ope.SP_CumpleCriterioAutoEvaluacionesVencidas";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoBool = JsonConvert.DeserializeObject<BoolDTO>(resultado);
                }
                if (resultadoBool.Valor.Value)
                {
                    var queryAutoEvaluaciones = "";

                    if (esFechaExacta)
                    {
                        queryAutoEvaluaciones = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculadoFechaExacta";
                    }
                    else
                    {
                        queryAutoEvaluaciones = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculado";
                    }
                    var resultadoAutoEvaluaciones = _dapperRepository.QuerySPDapper(queryAutoEvaluaciones, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                    if (!string.IsNullOrEmpty(resultadoAutoEvaluaciones))
                    {
                        resultadoAutoEvaluacionesFinal = JsonConvert.DeserializeObject<List<AutoEvaluacionCronogramaDetalleDTO>>(resultadoAutoEvaluaciones);
                    }

                }
                return resultadoAutoEvaluacionesFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las proxima fecha de autoevaluacion
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el dia actual</param>
        /// <returns>Retorna objeto del tipo AutoEvaluacionCronogramaDetalleDTO</returns>
        public AutoEvaluacionCronogramaDetalleDTO ObtenerDetalleProximaAutoEvaluacion(int id)
        {
            try
            {
                var proximaCuota = new AutoEvaluacionCronogramaDetalleDTO();
                var query = $@"ope.SP_ObtenerProximaAutoEvaluacion";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    proximaCuota = JsonConvert.DeserializeObject<AutoEvaluacionCronogramaDetalleDTO>(resultado);
                }
                return proximaCuota;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cantidad de autoevaluaciones vencidas
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de autoevaluaciones vencidas</returns>
        public int ObtenerCantidadAutoEvaluacionesVencidas(int id)
        {
            try
            {
                var respuesta = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerCantidadAutoevaluacionesVencidas";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return respuesta.Valor.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el cronograma de pago completo en html
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener el cronograma de pago completo (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="formatoHTMLMostrar">Enumerator de tipo FormatoHTMLMostrar, mostrando la lista y la tabla</param>
        /// <returns>Cadena formateada con el cronograma de pagos completo</returns>
        public string ObtenerCronogramaPagoCompleto(int id, FormatoHTMLMostrar formatoHTMLMostrar)
        {
            try
            {
                var resultadoFinal = new List<CuotaCronogramaDetalleDTO>();
                var query = $@"fin.SP_ObtenerCronogramaPagoCuotasCompleto";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultado);
                }

                var htmlFinal = "";

                if (formatoHTMLMostrar == FormatoHTMLMostrar.Lista)
                {
                    if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                    {
                        var totalCuotas = resultadoFinal.Max(x => x.NroCuota);
                        var ultimo = resultadoFinal.Last();
                        foreach (var item in resultadoFinal)
                        {
                            htmlFinal += $@"
                                     <span>
                                        Nro. cuota {item.NroCuota} de {totalCuotas}
                                        <br/>
                                        Fecha de vencimiento: {item.FechaVencimiento.ToString("dd/MM/yyyy")}
                                        <br/>
                                        Monto ({item.Moneda}): {item.Cuota}
                                     </span>";
                            if (!item.Equals(ultimo))
                            {
                                htmlFinal += $@"
                                            <br/>
                                            <br/>";
                            }
                        }
                    }
                }
                else if (formatoHTMLMostrar == FormatoHTMLMostrar.Tabla)
                {
                    if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                    {

                        var totalCuotas = resultadoFinal.Max(x => x.NroCuota);
                        var ultimo = resultadoFinal.Last();

                        htmlFinal += $@"
                                    <table style='width: 390px;text-align: center;'>
                                        <tr>
                                            <th style='width:103px;height:28px;text-align: center;'> Nro. Cuota </th>
                                            <th style='width:140px;height:28px;text-align: center;'> Monto </th>
                                            <th style='width:193px;height:28px;text-align: center;'> Fecha de vencimiento </th>
                                        </tr>
                                        ";
                        foreach (var item in resultadoFinal)
                        {
                            htmlFinal += $@"
                                            <tr>
                                               <td style='width:103px;height:23px;text-align:center;'> {item.NroCuota} </td>
                                               <td style='width:140px;height:23px;text-align:center;'> {item.SimboloMoneda} {item.Cuota} </td>
                                               <td style='width:193px;height:23px;text-align:center;'> {item.FechaVencimiento.ToString("dd/MM/yyyy")} </td>
                                           </tr>
                                          ";
                        }
                        htmlFinal += $@"</table>";
                    }
                }
                return htmlFinal.Replace("dolares", "dólares");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el monto total del programa en el formato Simbolo +" " + MontoTotal +" "+ NombrePlural
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener el monto total (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con el monto y ell simbolo de la moneda</returns>
        public string ObtenerMontoTotal(int id)
        {
            try
            {
                var resultadoFinal = new ResumenCronogramaDTO();
                var query = $@"fin.SP_ObtenerMontoTotal";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ResumenCronogramaDTO>(resultado);
                }
                return string.Concat(resultadoFinal.SimboloMoneda, " ", resultadoFinal.MontoTotal, " ", resultadoFinal.NombrePluralMoneda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el cronograma de pago completo en html
        /// </summary>
        /// <param name="id">Id de la matricula cabecera a la cual se desea obtener el cronograma de pago completo de cuotas vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada de las cuotas de pago vencidas</returns>
        public string ObtenerCronogramaPagoCompletoCuotasVencidas(int id)
        {
            try
            {
                var resultadoFinal = new List<CuotaCronogramaDetalleDTO>();
                var query = $@"fin.SP_ObtenerCronogramaPagoCompletoCuotasVencidas";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultado);
                }

                var htmlFinal = string.Empty;

                if (resultadoFinal.Count() > 0 && resultadoFinal != null)
                {
                    var totalCuotas = resultadoFinal.Max(x => x.NroCuota);
                    var vencidos = resultadoFinal.Where(x => x.FechaVencimiento <= DateTime.Now);

                    if (!vencidos.Any())
                        return string.Empty;

                    var ultimo = vencidos.Last();
                    foreach (var item in vencidos)
                    {
                        htmlFinal += $@"
                                     <span>
                                        Nro. cuota {item.NroCuota} de {totalCuotas}
                                        <br/>
                                        Fecha de vencimiento: {item.FechaVencimiento.ToString("dd/MM/yyyy")}
                                        <br/>
                                        Monto ({item.Moneda}): {item.Cuota}
                                     </span>";
                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>";
                        }
                    }
                }
                return htmlFinal.Replace("dolares", "dólares");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Cantidad de cuotas pendientes
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener la cantidad de cuotas pendientes (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de cuotas pendientes</returns>
        public int ObtenerCantidadCuotasPendientes(int id)
        {
            try
            {
                var respuesta = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerCantidadCuotasPendientes";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return respuesta.Valor.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las cuotas vencidas en N dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias desde el dia actual de la consulta</param>
        /// <param name="esFechaExacta">Flag para determinar si la fecha ingresada es exacta o es un intervalo</param>
        /// <param name="idPlantillaBase">Id de la plantilla base (PK de la tabla pla.T_PlantillaBase)</param>
        /// <returns>Cadena con las cuotas vencidas dependiendo de los parametros ingresados</returns>
        public string ObtenerCuotasVencidas(int id, int cantidadDias, bool esFechaExacta, int idPlantillaBase)
        {
            try
            {
                var resultadoFinal = new BoolDTO();

                var query = "";
                if (esFechaExacta)
                {
                    query = $@"ope.SP_CumpleCriterioCuotasVencidas";
                }
                else
                {
                    query = $@"ope.SP_CumpleCriterioCuotasVencidasFechaNoExacta";
                }

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<BoolDTO>(resultado);
                }

                var htmlFinal = "";
                if (resultadoFinal.Valor.Value)
                {
                    var cantidadMaximaCuotas = this.ObtenerMaximaCuota(id);

                    var respuestaCuotas = new List<CuotaCronogramaDetalleDTO>();
                    var queryCuotas = "";
                    if (esFechaExacta)
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCuotasVencidasFechaExacta";
                    }
                    else
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCuotasVencidas";
                    }

                    var resultadoCuotas = _dapperRepository.QuerySPDapper(queryCuotas, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                    if (!string.IsNullOrEmpty(resultadoCuotas))
                    {
                        respuestaCuotas = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultadoCuotas);
                    }

                    if (respuestaCuotas.Count() > 0 && respuestaCuotas != null)
                    {
                        var ultimo = respuestaCuotas.Last();
                        foreach (var item in respuestaCuotas)
                        {
                            if (idPlantillaBase == 2)
                            {

                                if (cantidadDias == 0 || cantidadDias == 3)
                                {
                                    htmlFinal += $@"
                                     <span>
                                     <strong>Nro. cuota: </strong>{item.NroCuota} de {cantidadMaximaCuotas}
                                     <br/>
                                     <strong>Fecha de vencimiento: </strong>{item.FechaVencimiento.ToString("dd/MM/yyyy")}
                                     <br/>
                                     <strong>Monto ({item.Moneda}): </strong>{item.Cuota}
                                     </span>";
                                }
                                else
                                {
                                    htmlFinal += $@"
                                     <span>
                                     Nro. cuota: {item.NroCuota} de {cantidadMaximaCuotas}
                                     <br/>
                                     Fecha de vencimiento: {item.FechaVencimiento.ToString("dd/MM/yyyy")}
                                     <br/>
                                     Monto ({item.Moneda}): {item.Cuota}
                                     </span>";
                                }
                                if (!item.Equals(ultimo))
                                {
                                    htmlFinal += $@"
                                            <br/>
                                            <br/>";
                                }
                            }
                            else if (idPlantillaBase == 8)
                            {
                                htmlFinal += $@"Nro. cuota {item.NroCuota} de {cantidadMaximaCuotas}\nFecha de vencimiento: {item.FechaVencimiento.ToString("dd/MM/yyyy")}\nMonto ({item.Moneda}): {item.Cuota}\n";
                                if (!item.Equals(ultimo))
                                {
                                    htmlFinal += $@"";
                                }

                            }
                        }
                    }

                }
                return htmlFinal.Replace("dolares", "dólares");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las siguiente sesion
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener la maxima cuota</param>
        /// <returns>Entero con el numero maximo de cuotas de la matricula cabecera ingresada</returns>
        public int ObtenerMaximaCuota(int id)
        {
            try
            {
                var resultadoFinal = new ValorIntDTO();
                var query = $@"fin.SP_ObtenerMaximoNroCuota";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }

                return resultadoFinal.Valor.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cantidad de cuotas vencidas en N dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener la cantidad de cuotas vencidas en N dias</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde la fecha actual</param>
        /// <param name="esFechaExacta">Flag para determinar si la fecha ingresada es Fecha exacta o un intervalo</param>
        /// <param name="idPlantillaBase">Id de la plantilla base (PK de la tabla pla.T_PlantillaBase)</param>
        /// <returns>Entero con la cantidad de cuotas vencidas</returns>
        public int ObtenerCantidadCuotasVencidas(int id, int cantidadDias, bool esFechaExacta, int idPlantillaBase)
        {
            try
            {
                var resultadoFinal = new BoolDTO();
                var cantidadCuotas = new ValorIntDTO();
                cantidadCuotas.Valor = 0;
                var query = "";
                if (esFechaExacta)
                {
                    query = $@"ope.SP_CumpleCriterioCuotasVencidas";
                }
                else
                {
                    query = $@"ope.SP_CumpleCriterioCuotasVencidasFechaNoExacta";
                }

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<BoolDTO>(resultado);
                }

                if (resultadoFinal.Valor.Value)
                {
                    var queryCuotas = "";
                    if (esFechaExacta)
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCantidadCuotasVencidasFechaExacta";
                    }
                    else
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCantidadCuotasVencidas";
                    }
                    var queryCantidad = _dapperRepository.QuerySPFirstOrDefault(queryCuotas, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });
                    if (!string.IsNullOrEmpty(queryCantidad))
                    {
                        cantidadCuotas = JsonConvert.DeserializeObject<ValorIntDTO>(queryCantidad);
                    }
                }
                return (int)cantidadCuotas.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las cuotas vencidas en N dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias desde la fecha actual</param>
        /// <param name="esFechaExacta">Flag para determinar si es fecha exacta o un intervalo lo ingresado</param>
        /// <returns>Retorna una lista de objetos de tipo CuotaCronogramaDetalleDTO</returns>
        public List<CuotaCronogramaDetalleDTO> ObtenerDetalleCuotasVencidas(int id, int cantidadDias, bool esFechaExacta)
        {
            try
            {
                var resultadoCuotasFinal = new List<CuotaCronogramaDetalleDTO>();

                var resultadoFinal = new BoolDTO();
                var query = $@"ope.SP_CumpleCriterioCuotasVencidas";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<BoolDTO>(resultado);
                }

                if (resultadoFinal.Valor.Value)
                {
                    var queryCuotas = "";
                    if (esFechaExacta)
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCuotasVencidasFechaExacta";
                    }
                    else
                    {
                        queryCuotas = $@"fin.SP_ObtenerCronogramaPagoCuotasVencidas";
                    }

                    var resultadoCuotas = _dapperRepository.QuerySPDapper(queryCuotas, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                    if (!string.IsNullOrEmpty(resultadoCuotas))
                    {
                        resultadoCuotasFinal = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultadoCuotas);
                    }
                }
                return resultadoCuotasFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las proxima cuota
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabeceras)</param>
        /// <returns>Objeto del tipo CuotaCronogramaDetalleDTO</returns>
        public CuotaCronogramaDetalleDTO ObtenerDetalleProximaCuota(int id)
        {
            try
            {
                var proximaCuota = new CuotaCronogramaDetalleDTO();
                var query = $@"ope.SP_ObtenerProximaCuota";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    proximaCuota = JsonConvert.DeserializeObject<CuotaCronogramaDetalleDTO>(resultado)!;
                }
                return proximaCuota;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el descuento de cuotas pendientes acorde a un porcentaje dado
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="porcentaje">Porcentaje dado</param>
        /// <returns>Cadena formateada con el descuento de cuotas pendientes</returns>
        public string ObtenerDescuentoCuotasPendientesPorPorcentaje(int idMatriculaCabecera, decimal porcentaje)
        {
            try
            {
                var resultadoFinal = new MontoMonedaDTO();
                var query = $@"fin.SP_CalcularDescuentoCuotasPendientes";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera, PorcentajeDescuento = porcentaje });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    resultadoFinal = JsonConvert.DeserializeObject<MontoMonedaDTO>(resultado);
                }
                return resultadoFinal.Cuota + " " + resultadoFinal.Moneda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener detalle acceso aula virtual
        /// </summary>
        /// <param name="id">Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Objeto del tipo DetalleCursoActualAulaVirtualDTO</returns>
        public List<DetalleCursoActualAulaVirtualDTO> ObtenerCursoActualAlumnoMoodle(int id)
        {
            try
            {
                List<DetalleCursoActualAulaVirtualDTO> resultadoFinal = new List<DetalleCursoActualAulaVirtualDTO>();
                string _queryCursoMoodle = "SELECT TOP 1 * from [ope].[V_ObtenerCursoActualAlumnoMoodle] WHERE IdMatriculaCabecera = @id";
                var resultado = _dapperRepository.QueryDapper(_queryCursoMoodle, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<DetalleCursoActualAulaVirtualDTO>>(resultado)!;
                }
                else
                {
                    resultadoFinal = null;
                }
                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener detalle acceso aula virtual
        /// </summary>
        /// <param name="id">Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Objeto del tipo DetalleAccesoAulaVirtualDTO</returns>
        public DetalleAccesoAulaVirtualDTO ObtenerDetalleAccesoAulaVirtual(int id)
        {
            try
            {
                var resultadoFinal = new DetalleAccesoAulaVirtualDTO();
                var query = "ope.SP_ObtenerDetalleAulaVirtual";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<DetalleAccesoAulaVirtualDTO>(resultado);
                }
                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el acceso del portal web
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DetalleAccesoPortalWebDTO ObtenerDetalleAccesoDocentePortalWeb(int idProveedor)
        {
            try
            {
                var respuesta = new DetalleAccesoPortalWebDTO();
                var query = "ope.SP_ObtenerDetalleAccesoDocentePortalWeb";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdProveedor = idProveedor });

                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<DetalleAccesoPortalWebDTO>(resultado);
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el acceso del portal web
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DetalleAccesoPortalWebDTO ObtenerDetalleAccesoPortalWeb(int id)
        {
            try
            {
                var _resultado = new DetalleAccesoPortalWebDTO();
                var query = "ope.SP_ObtenerDetalleAccesoPortalWeb";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<DetalleAccesoPortalWebDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las siguiente sesion
        /// </summary>
        /// <param name="id">Id del pespecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="cantidadDias">Cantidad de dias a partir del dia de hoy</param>
        /// <returns>Entero que retorna la proxima sesion</returns>
        public int ObtenerProximaSesion(int id, int cantidadDias)
        {
            try
            {
                var resultadoFinal = new ProgramaEspecificoSesionDetalleDTO();
                var query = $@"ope.SP_ObtenerProximaSesionProgramaEspecifico";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPEspecifico = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ProgramaEspecificoSesionDetalleDTO>(resultado);
                }

                return resultadoFinal.IdPEspecificoSesion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Materiales de cada PEspecifico
        /// </summary>
        /// <param name="listaIdMaterialPEspecificoDetalle">Lista de entero de IdMaterialPEspecificoDetalle</param>
        /// <returns>Cadena formateada de los materiales</returns>
        public string ObtenerMaterialesPorMaterialPEspecificoDetalle(List<int> listaIdMaterialPEspecificoDetalle)
        {
            try
            {
                var listaMateriales = new List<MaterialDescargarDTO>();
                var query = $@"ope.SP_ObtenerListaMaterialesPorMaterialPEspecificoDetalle";
                var resultado = _dapperRepository.QuerySPDapper(query, new { ListaMaterialPEspecificoDetalle = string.Join(",", listaIdMaterialPEspecificoDetalle.Select(x => x)) });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listaMateriales = JsonConvert.DeserializeObject<List<MaterialDescargarDTO>>(resultado);
                }
                var htmlFinal = "";

                if (listaMateriales.Count() > 0 && listaMateriales != null)
                {
                    //listadoSesionWebinar = listadoSesionWebinar.OrderBy(x => x.NombreAutoEvaluacion).ToList();
                    var ultimo = listaMateriales.Last();
                    foreach (var item in listaMateriales)
                    {
                        htmlFinal += $@"
                                     <span>
                                         <a href='{item.UrlArchivo}'> {item.NombreArchivo} </a>
                                     </span>
                                     <br>";

                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la url del acceso sesion webinar en base a la cantidad de dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera que se desea analizar (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias que se desea obtener las sesiones webinar</param>
        /// <returns>Una lista de objetos (SesionWebinarDTO)</returns>
        public List<SesionWebinarDTO> ObtenerUrlSesionesWebinarNDias(int id, int cantidadDias)
        {
            try
            {
                var listadoSesionWebinar = new List<SesionWebinarDTO>();
                var query = $@"ope.SP_ObtenerSesionesWebinarNDias";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listadoSesionWebinar = JsonConvert.DeserializeObject<List<SesionWebinarDTO>>(resultado);
                }
                return listadoSesionWebinar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las AutoEvaluaciones vencidas en una cantidad de dias exacta
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el dia actual</param>
        /// <param name="esFechaExacta">Flag para determinar si la fecha ingresada es exacta</param>
        /// <param name="idPlantillaBase">Id de la plantilla base de la cual se buscara la informacion</param>
        /// <returns>Cadena con las autoevaluaciones vencidas</returns>
        public string ObtenerAutoEvaluacionesVencidasDiasExacto(int id, int cantidadDias, bool esFechaExacta, int idPlantillaBase)
        {
            try
            {
                var resultadoFinal = new BoolDTO();
                var query = $@"ope.SP_CumpleCriterioAutoEvaluacionesVencidas";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<BoolDTO>(resultado);
                }
                var htmlFinal = "";
                if (resultadoFinal.Valor.Value)
                {
                    var resultadoAutoEvaluacionesFinal = new List<AutoEvaluacionCronogramaDetalleDTO>();
                    var queryAutoEvaluaciones = "";

                    if (esFechaExacta)
                    {
                        queryAutoEvaluaciones = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculadoFechaExacta";
                    }
                    else
                    {
                        queryAutoEvaluaciones = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculado";
                    }
                    var resultadoAutoEvaluaciones = _dapperRepository.QuerySPDapper(queryAutoEvaluaciones, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                    if (!string.IsNullOrEmpty(resultadoAutoEvaluaciones))
                    {
                        resultadoAutoEvaluacionesFinal = JsonConvert.DeserializeObject<List<AutoEvaluacionCronogramaDetalleDTO>>(resultadoAutoEvaluaciones);
                    }

                    if (resultadoAutoEvaluacionesFinal.Count() > 0 && resultadoAutoEvaluacionesFinal != null)
                    {
                        var ultimo = resultadoAutoEvaluacionesFinal.Last();
                        foreach (var item in resultadoAutoEvaluacionesFinal)
                        {
                            if (idPlantillaBase == 2)
                            {
                                htmlFinal += $@"
                                     <span>
                                         {item.NombreAutoEvaluacion}
                                         <br/>
                                         Fecha Límite de Autoevaluación: {item.FechaCronograma.ToString("dd/MM/yyyy")}
                                     </span>";
                                if (!item.Equals(ultimo))
                                {
                                    htmlFinal += $@"
                                            <br/>
                                            <br/>";
                                }
                            }
                            else if (idPlantillaBase == 8)
                            {
                                htmlFinal += $@"{item.NombreAutoEvaluacion} \nFecha Límite de Autoevaluación: {item.FechaCronograma.ToString("dd/MM/yyyy")}";
                                if (!item.Equals(ultimo))
                                {
                                    htmlFinal += $@"\n";
                                }
                            }
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las AutoEvaluaciones vencidas en una cantidad de dias exacta
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener el detalle de las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el dia actual</param>
        /// <param name="esFechaExacta">Flag para determinar si es una fecha exacta lo ingresado</param>
        /// <returns>Lista de objetos (AutoEvaluacionCronogramaDetalleDTO)</returns>
        public List<AutoEvaluacionCronogramaDetalleDTO> ObtenerDetalleAutoEvaluacionesVencidas(int id, int cantidadDias, bool esFechaExacta)
        {
            try
            {
                var resultadoAutoEvaluacionesFinal = new List<AutoEvaluacionCronogramaDetalleDTO>();

                var resultadoBool = new BoolDTO();
                var query = $@"ope.SP_CumpleCriterioAutoEvaluacionesVencidas";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoBool = JsonConvert.DeserializeObject<BoolDTO>(resultado);
                }
                if (resultadoBool.Valor.Value)
                {
                    var queryAutoEvaluaciones = "";

                    if (esFechaExacta)
                    {
                        queryAutoEvaluaciones = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculadoFechaExacta";
                    }
                    else
                    {
                        queryAutoEvaluaciones = $@"ope.SP_ObtenerAutoevaluacionesVencidasAlumnoMatriculado";
                    }
                    var resultadoAutoEvaluaciones = _dapperRepository.QuerySPDapper(queryAutoEvaluaciones, new { IdMatriculaCabecera = id, CantidadDias = cantidadDias });

                    if (!string.IsNullOrEmpty(resultadoAutoEvaluaciones))
                    {
                        resultadoAutoEvaluacionesFinal = JsonConvert.DeserializeObject<List<AutoEvaluacionCronogramaDetalleDTO>>(resultadoAutoEvaluaciones);
                    }

                }
                return resultadoAutoEvaluacionesFinal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cantidad de cuotas vencidas
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de cuotas vencidas</returns>
        public int ObtenerTodoCantidadCuotasVencidas(int id)
        {
            try
            {
                var resultadoFinal = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerCantidadCuotasVencidas";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return resultadoFinal.Valor.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la url del acceso sesion webinar en base a la cantidad de dias
        /// </summary>
        /// <param name="idMaterialPEspecificoDetalle">Id del Material de un PEspecifico (PK de la tabla ope.T_MaterialPEspecificoDetalle)</param>
        /// <returns>Cadena formateada de los materiales de un PEspecifico</returns>
        public string ObtenerUrlMaterialesPorMaterialPEspecificoDetalle(int idMaterialPEspecificoDetalle)
        {
            try
            {
                var listaMateriales = new List<MaterialDescargarDTO>();
                var query = $@"ope.SP_ObtenerMaterialesPorMaterialPEspecificoDetalle";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMaterialPEspecificoDetalle = idMaterialPEspecificoDetalle });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listaMateriales = JsonConvert.DeserializeObject<List<MaterialDescargarDTO>>(resultado);
                }
                var htmlFinal = "";

                if (listaMateriales.Count() > 0 && listaMateriales != null)
                {
                    //listadoSesionWebinar = listadoSesionWebinar.OrderBy(x => x.NombreAutoEvaluacion).ToList();
                    var ultimo = listaMateriales.Last();
                    foreach (var item in listaMateriales)
                    {
                        htmlFinal += $@"
                                     <span>
                                         <a href='{item.UrlArchivo}'> {item.NombreArchivo} </a>
                                     </span>
                                     <br>
                                     <br>";

                        if (!item.Equals(ultimo))
                        {
                            htmlFinal += $@"
                                            <br/>
                                            <br/>
                                            ";
                        }
                    }
                }
                return htmlFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros para actuaizar
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <returns></returns>
        public CambioCentroCostoDTO ObtenerRegistrosParaActualizar(int idSolicitudOperaciones)
        {
            try
            {
                var respuesta = new CambioCentroCostoDTO();
                var query = $@"ope.SP_ObtenerRegistrosParaActualizar";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { idSolicitudOperaciones });

                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<CambioCentroCostoDTO>(resultado);
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualia Centro Costo
        /// </summary>
        /// <param name="solicitudOperacion"></param>
        /// <returns></returns>
        public bool ActualizarCentroCosto(CambioCentroCostoDTO solicitudOperacion)
        {
            try
            {

                var respuesta = new BoolDTO();
                var query = $@"ope.SP_ActualizarCentroCosto";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query,
                    new
                    {
                        solicitudOperacion.IdOportunidadV4,
                        solicitudOperacion.IdOportunidadV3,
                        solicitudOperacion.IdOportunidadPadreV3,
                        solicitudOperacion.IdOportunidadPadreV4,
                        solicitudOperacion.IdMatriculaCabeceraV3,
                        solicitudOperacion.IdMatriculaCabeceraV4,
                        solicitudOperacion.IdCronogramaPagoV3,
                        solicitudOperacion.IdCronogramaPagoV4,
                        solicitudOperacion.IdCentroCostoV3,
                        solicitudOperacion.IdCentroCostoV4,
                        solicitudOperacion.IdPespecificoV3,
                        solicitudOperacion.IdPespecificoV4
                    });
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene rogistros para actualizar version
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <returns></returns>
        public CambioCentroCostoDTO ObtenerRegistrosParaActualizarVersion(int idSolicitudOperaciones)
        {
            try
            {

                var respuesta = new CambioCentroCostoDTO();
                var query = $@"ope.SP_ObtenerRegistrosParaActualizarVersion";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { idSolicitudOperaciones });

                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<CambioCentroCostoDTO>(resultado);
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene beneficios por el codigo de matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public StringDTO EliminarBeneficiosMatriculaCabeceraIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                StringDTO resultado = new StringDTO();
                var solicitudesCambiosDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_EliminarBeneficiosMatriculaCabecera", new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(solicitudesCambiosDB) && !solicitudesCambiosDB.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<StringDTO>(solicitudesCambiosDB)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene beneficios por el codigo de matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="nuevoPaquete"></param>
        /// <param name="idCronograma"></param>
        /// <returns></returns>
        /// <exception>StringDTO</exception>
        public StringDTO InsertarBeneficiosMatriculaCabeceraIdMatricula(int idMatriculaCabecera, int nuevoPaquete, int idCronograma)
        {
            try
            {
                StringDTO resultado = new StringDTO();
                var solicitudesCambiosDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_InsertarBeneficiosMatriculaCabecera", new { idMatriculaCabecera, nuevoPaquete, idCronograma });
                if (!string.IsNullOrEmpty(solicitudesCambiosDB) && !solicitudesCambiosDB.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<StringDTO>(solicitudesCambiosDB)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de Subestados de matricula para ser usados en filtros
        /// </summary>
        /// <param></param>
        /// <returns>Lista</returns>
        public List<SubEstadoMatriculaFiltroConfiguracionCoordinadoraDTO> ObtenerSubEstadoMatriculaConfiguracionCoordinadora()
        {
            try
            {
                List<SubEstadoMatriculaFiltroConfiguracionCoordinadoraDTO> estadoMatriculado = new List<SubEstadoMatriculaFiltroConfiguracionCoordinadoraDTO>();
                var query = @"SELECT 
                                Id IdSubEstadoMatricula,Nombre SubEstadoMatricula,IdEstadoMatricula 
                            FROM 
                                fin.V_ObtenerSubEstadoMatricula 
                            WHERE 
                                Estado=1";
                var pEspecificoDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<SubEstadoMatriculaFiltroConfiguracionCoordinadoraDTO>>(pEspecificoDB)!;
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de Subestados de matricula para ser usados en combo
        /// </summary>
        /// <param></param>
        /// <returns>Lista</returns>
        public List<SubEstadoMatriculaFiltroDTO> ObtenerSubEstadoMatricula()
        {
            try
            {
                List<SubEstadoMatriculaFiltroDTO> estadoMatriculado = new List<SubEstadoMatriculaFiltroDTO>();
                var query = @"SELECT Id,Nombre,IdEstadoMatricula FROM fin.V_ObtenerSubEstadoMatricula WHERE Estado = 1";
                var pEspecificoDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<SubEstadoMatriculaFiltroDTO>>(pEspecificoDB)!;
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 09/11/2022
        /// <summary>
        /// Obtener Beneficio Solicitado Por Matricula
        /// </summary>
        /// <param name="codigoMatricula"> Codigo Matricula </param>
        /// <returns> Lista de beneficios solicitados: List<InformacionBeneficioSolicitadoDTO></returns> 
        public List<InformacionBeneficioSolicitadoDTO> ObtenerBeneficiosSolicitadosPorMatricula(string codigoMatricula)
        {
            try
            {
                var beneficiosCodigoMatricula = new List<InformacionBeneficioSolicitadoDTO>();
                var query = @"SELECT Id,Beneficio,CentroCosto,Programa,FechaSolicitud,FechaProgramada,Coordinador,EstadoSolicitud,FechaEntregaBeneficio  
                                FROM com.V_BeneficiosSolicitados 
                                WHERE CodigoMatricula =  @codigoMatricula";
                var beneficiosCodigoMatriculaDB = _dapperRepository.QueryDapper(query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<InformacionBeneficioSolicitadoDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 09/11/2022
        /// Version:1.1
        /// <summary>
        /// Retorna los beneficios correspondientes a los alumnos.
        /// </summary>
        /// <param name="codigoMatricula">Codigo de la matricula del alumno</param>
        /// <returns>Lista de los beneficios del alumno: List<MatriculaCabeceraBeneficiosDTO></returns>
        public List<MatriculaCabeceraBeneficioDTO> ObtenerBeneficiosCongeladosPorMatricula(string codigoMatricula)
        {
            try
            {
                var beneficiosCodigoMatricula = new List<MatriculaCabeceraBeneficioDTO>();
                var query = @"SELECT Id,Titulo , EstadoMatriculaCabeceraBeneficio,FechaSolicitud,EstadoSolicitudBeneficio,FechaProgramada,
                                     IdConfiguracionBeneficioProgramaGeneral,FechaEntregaBeneficio 
                                FROM com.V_MatriculaCabeceraBeneficios 
                                WHERE CodigoMatricula =  @codigoMatricula AND Estado=1 AND Titulo not like '%BSG%' AND Titulo not like '%horas%'";
                var beneficiosCodigoMatriculaDB = _dapperRepository.QueryDapper(query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<MatriculaCabeceraBeneficioDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 09/11/2022
        /// <summary>
        /// Obtener Estado Programa General del Beneficio
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> Lista Estado Matricula: List<MatriculaCabeceraComboDTO> </returns> 
        public List<MatriculaCabeceraComboDTO> ObtenerEstadoPgeneralBeneficio(int idPGeneral)
        {
            try
            {
                var estado = new List<MatriculaCabeceraComboDTO>();
                var query = "Select IdEstadoMatricula as Id From [ope].[V_ObtenerEstadoConfiguracionBeneficio]  Where IdPGeneral=@idPGeneral";
                var queryDB = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("null"))
                {
                    estado = JsonConvert.DeserializeObject<List<MatriculaCabeceraComboDTO>>(queryDB);
                }
                return estado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 09/11/2022
        /// <summary>
        /// Obtener Sub Estado del Programa General Beneficios
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> Lista Subestado Programa General Beneficio: List<EstadosMatriculaDTO></returns> 
        public List<MatriculaCabeceraComboDTO> ObtenerSubEstadoPgeneralBeneficio(int idPGeneral)
        {

            try
            {
                var estado = new List<MatriculaCabeceraComboDTO>();
                var query = "Select IdSubEstadoMatricula as Id From [ope].[V_ObtenerSubEstadoConfiguracionBeneficio]  Where IdPGeneral=@idPGeneral";
                var queryDB = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("null"))
                {
                    estado = JsonConvert.DeserializeObject<List<MatriculaCabeceraComboDTO>>(queryDB);
                }
                return estado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 09/11/2022
        /// <summary>
        /// Obtiene el IdEstado_matricula por el codigo matricula
        /// </summary>
        /// <param name="codigoMatricula"> Codigo Matricula </param>
        /// <returns> int </returns> 
        public int ObtenerEstadoAlumno(string codigoMatricula)
        {
            try
            {
                var estadoalumno = new ValorIntDTO();
                var query = "Select IdEstado_matricula as Valor From fin.T_MatriculaCabecera Where CodigoMatricula=@codigoMatricula";
                var queryDB = _dapperRepository.FirstOrDefault(query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(queryDB) && queryDB != "null")
                {
                    estadoalumno = JsonConvert.DeserializeObject<ValorIntDTO>(queryDB);
                }
                else
                {
                    estadoalumno.Valor = 0;
                }
                return estadoalumno.Valor.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 09/11/2022
        /// <summary>
        /// Obtiene el IdSubEstadoMatricula por el codigo matricula
        /// </summary>
        /// <param name="codigoMatricula"> Codigo Matricula </param>
        /// <returns> int </returns> 
        public int ObtenerSubestadoAlumno(string codigoMatricula)
        {
            try
            {
                var estadoalumno = new ValorIntDTO();
                var query = "Select IdSubEstadoMatricula as Valor From fin.T_MatriculaCabecera Where CodigoMatricula=@codigoMatricula";
                var queryDB = _dapperRepository.FirstOrDefault(query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(queryDB) && queryDB != "null" && !queryDB.Contains("null"))
                {
                    estadoalumno = JsonConvert.DeserializeObject<ValorIntDTO>(queryDB);
                }
                else
                {
                    estadoalumno.Valor = 0;
                }
                return estadoalumno.Valor.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 11/11/2022
        /// <summary>
        /// Obtiene los cursos Moodle filtrado por el codido de matricula
        /// </summary>
        /// <param name="codigoMatricula"> Codigo Matricula </param>
        /// <returns> List<CursoMoodleDTO> </returns> 
        public List<CursoMoodleDTO> ObtenerCursoMoodle(string codigoMatricula)
        {
            try
            {
                var cursoMoodle = new List<CursoMoodleDTO>();
                var _query = "Select CodigoMatricula,IdUsuario,IdCurso,NombreCurso, IdMatriculaMoodle From ope.V_ObtenerCursosPorMatricula Where CodigoMatricula = @CodigoMatricula";
                var _cursoMoodle = _dapperRepository.QueryDapper(_query, new { CodigoMatricula = codigoMatricula });
                if (!string.IsNullOrEmpty(_cursoMoodle) && !_cursoMoodle.Contains("[]"))
                {
                    cursoMoodle = JsonConvert.DeserializeObject<List<CursoMoodleDTO>>(_cursoMoodle);
                }
                return cursoMoodle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 12/11/2022
        /// <summary>
        /// Obtiene los costos administrativod filtrado por el IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> Codigo Matricula </param>
        /// <returns> List<CostosAdministrativosDTO> </returns> 
        public List<CostosAdministrativosDTO> ObtenerCostosAdministrativos(int idMatriculaCabecera)
        {
            try
            {
                List<CostosAdministrativosDTO> costos = new List<CostosAdministrativosDTO>();
                var _query = "Select Id,Concepto,IdMatriculaCabecera,Moneda,Monto,Gestionado,FechaCreacion,UrlDocumento,FechaEntregaEstimada,FechaEntregaReal,SolicitudCF,IdEstadoCertificadoFisico,IdCertificadoGeneradoAutomatico From pla.V_ObtenerCertificadoCronogramaPagoTarifario Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var _cursoMoodle = _dapperRepository.QueryDapper(_query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(_cursoMoodle) && !_cursoMoodle.Contains("[]"))
                {
                    costos = JsonConvert.DeserializeObject<List<CostosAdministrativosDTO>>(_cursoMoodle);
                }
                return costos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Codigo de Matricula y Programa del alumno
        /// </summary>
        /// <param name="idAlumno"> Id Alumno </param>        
        /// <returns> Lista Cronograma del Alumno : List<CodigoMatriculaPEspecificoDTO> </returns>
        public List<CodigoMatriculaPEspecificoDTO> ObtenerCodigoMatriculaPEspecificoPorAlumno(int idAlumno)
        {
            try
            {
                List<CodigoMatriculaPEspecificoDTO> codigosMatriculaPEspecifico = new List<CodigoMatriculaPEspecificoDTO>();
                var query = @"
                            SELECT 
                                CodigoMatricula, PEspecifico 
                            FROM 
                                fin.V_ObtenerCodigoMatriculaPEspecifico 
                            WHERE  
                                IdAlumno = @idAlumno";
                var codigosMatriculaPEspecificoDB = _dapperRepository.QueryDapper(query, new { idAlumno });
                if (!string.IsNullOrEmpty(codigosMatriculaPEspecificoDB) && !codigosMatriculaPEspecificoDB.Contains("[]"))
                {
                    codigosMatriculaPEspecifico = JsonConvert.DeserializeObject<List<CodigoMatriculaPEspecificoDTO>>(codigosMatriculaPEspecificoDB)!;
                }
                return codigosMatriculaPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Devuelve los identificadores importantes por matricula de alumno  
        /// </summary>
        /// <param name="idAlumno"> Id Alumno </param>        
        /// <returns> Lista Identificadores Matricula : List<IdentificadorMatriculaComboDTO> </returns>
        public List<IdentificadorMatriculaComboDTO> ObtenerIdentificadoresMatriculaComboPorAlumno(int idAlumno)
        {
            try
            {
                List<IdentificadorMatriculaComboDTO> codigosMatriculaPEspecifico = new List<IdentificadorMatriculaComboDTO>();
                var query = @"
                            SELECT 
                                IdMatriculaCabecera, CodigoMatricula, IdOportunidad, PEspecifico 
                            FROM 
                                ope.V_ObtenerIdentificadoresMatricula 
                            WHERE 
                                IdAlumno = @idAlumno";
                var codigosMatriculaPEspecificoDB = _dapperRepository.QueryDapper(query, new { idAlumno });
                if (!string.IsNullOrEmpty(codigosMatriculaPEspecificoDB) && !codigosMatriculaPEspecificoDB.Contains("[]"))
                {
                    codigosMatriculaPEspecifico = JsonConvert.DeserializeObject<List<IdentificadorMatriculaComboDTO>>(codigosMatriculaPEspecificoDB)!;
                }
                return codigosMatriculaPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el alumno por el codigo del programa especifico
        /// </summary>
        /// <param name="idCabeceraMatricula"></param>
        /// <returns></returns>
        public List<AlumnoProgramaEspecificoDTO> ObtenerAlumnoProgramaEspecificoLista(int idCabeceraMatricula)
        {
            try
            {
                List<AlumnoProgramaEspecificoDTO> alumnoProgramaEspecifico = new List<AlumnoProgramaEspecificoDTO>();
                var _query = "SELECT CodigoMatricula,IdPEspecifico,PEspecifico,NombreCompletoAlumno NombreCompleto FROM " +
                    "fin.V_ObtenerAlumnoProgramaEspecifico WHERE IdMatriculaCabecera = @idCabeceraMatricula AND EstadoPEspecifico = 1 AND EstadoAlumno = 1";
                var alumnoProgramaEspecificoDB = _dapperRepository.QueryDapper(_query, new { idCabeceraMatricula });
                if (!string.IsNullOrEmpty(alumnoProgramaEspecificoDB) && !alumnoProgramaEspecificoDB.Contains("[]"))
                {
                    alumnoProgramaEspecifico = JsonConvert.DeserializeObject<List<AlumnoProgramaEspecificoDTO>>(alumnoProgramaEspecificoDB);
                }
                return alumnoProgramaEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el codigo matricula por IdOportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> MatriculaCabeceraCodigoFechaDTO </returns>
        public MatriculaCabeceraCodigoFechaDTO CodigoMatriculaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                string queryIdMatricula1 = @"SELECT 
                                                Id, CodigoMatricula, FechaMatricula 
                                            FROM 
                                                fin.V_TMatriculaCabecera_MatriculaPorIdOportunidad  
                                            WHERE 
                                                IdOportunidad = @IdOportunidad AND EsAprobado = 1 AND Estado = 1";
                var idMatricula1 = _dapperRepository.FirstOrDefault(queryIdMatricula1, new { IdOportunidad = idOportunidad });
                return JsonConvert.DeserializeObject<MatriculaCabeceraCodigoFechaDTO>(idMatricula1)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BeneficioSolicitadoReporteDTO> ObtenerTodoBeneficioSolicitado(FiltroBeneficiosSolicitadosPorAlumnos FiltroReporteSolcitud)
        {
            try
            {
                List<BeneficioSolicitadoReporteDTO> beneficiosCodigoMatricula = new List<BeneficioSolicitadoReporteDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerBeneficiosSolicitudAlumno", new
                {
                    FiltroReporteSolcitud.CodigoMatricula,
                    FiltroReporteSolcitud.BeneficioSolicitado,
                    FiltroReporteSolcitud.IdEstadoSolicitudBeneficio,
                    FiltroReporteSolcitud.FechaProgramadaInicio,
                    FiltroReporteSolcitud.FechaProgramadaFin,
                    FiltroReporteSolcitud.FechaCongelamientoInicio,
                    FiltroReporteSolcitud.FechaCongelamientoFin
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<BeneficioSolicitadoReporteDTO>>(resultado);
                }
                return beneficiosCodigoMatricula;
                //var _query = "SELECT Id, Alumno, CodigoMatricula, Estado_Matricula, SubEstado_Matricula, Version, Beneficio, CentroCosto, Programa, FechaSolicitud, Coordinador, " +
                //    "EstadoSolicitud, FechaAprobacion, FechaProgramada, FechaEntrega, UsuarioAprobacion, UsuarioEntregoBeneficio, FechaMatricula, FechaCongelamiento " +
                //    "FROM com.V_BeneficiosSolicitadosReporte ORDER BY beneficio";
                //var beneficiosCodigoMatriculaDB = _dapperRepository.QueryDapper(_query, new { });
                //if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                //{
                //    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<BeneficioSolicitadoReporteDTO>>(beneficiosCodigoMatriculaDB);
                //}
                //return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DatoAdicionalPWDTO> ObtenerDatosAdicionalesPorCodigo(int idMatriculaCabeceraBeneficios)
        {
            try
            {
                List<DatoAdicionalPWDTO> beneficiosCodigoMatricula = new List<DatoAdicionalPWDTO>();
                var _query = "SELECT IdBeneficioDatoAdicional as Id, Contenido FROM com.V_ContenidoDatoAdicional where IdMatriculaCabeceraBeneficios = @idMatriculaCabeceraBeneficios";
                var beneficiosCodigoMatriculaDB = _dapperRepository.QueryDapper(_query, new { idMatriculaCabeceraBeneficios });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<DatoAdicionalPWDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int EntregarBeneficio(int idMatriculaCabeceraBeneficio, string usuario)
        {
            try
            {

                var _query = "com.SP_EntregarBeneficio";
                var beneficiosCodigoMatriculaDB = _dapperRepository.QuerySPDapper(_query, new { idMatriculaCabeceraBeneficio, usuario });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la matricula por la Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Es el Id de la Oportunidad</param>
        /// <returns> Retorna informacion de la matricula </returns>
        public MatriculaTemporalDTO ObtenerMatriculaPorOportunidad(int idOportunidad)
        {
            try
            {
                string queryMatricula = @"SELECT 
                                                IdMatricula, CodigoMatricula, FechaMatricula 
                                          FROM 
                                                com.V_TOportunidad_CodigoMatricula 
                                          WHERE 
                                                IdOportunidad = @IdOportunidad AND EstadoPE = 1";
                var matricula = _dapperRepository.FirstOrDefault(queryMatricula, new { IdOportunidad = idOportunidad });
                return JsonConvert.DeserializeObject<MatriculaTemporalDTO>(matricula)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: miguel quiñones
        /// Fecha: 12/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la matricula por la Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Es el Id de la Oportunidad</param>
        /// <returns> Retorna informacion de la matricula </returns>
        public CodigoMatriculaStringDTO ObtenerMatriculaPorOportunidadOperaciones(int idOportunidad)
        {
            try
            {
                var codigoMatriculas = new CodigoMatriculaStringDTO();
                string queryMatricula = @"
											SELECT mc.CodigoMatricula 
                                            FROM ope.T_OportunidadClasificacionOperaciones AS OCO
												INNER JOIN fin.T_MatriculaCabecera AS MC ON OCO.IdMatriculaCabecera = MC.Id
											WHERE OCO.IdOportunidad = @idOportunidad";
                var matricula = _dapperRepository.FirstOrDefault(queryMatricula, new { IdOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(matricula) && !matricula.Contains("[]"))
                {
                    codigoMatriculas = JsonConvert.DeserializeObject<CodigoMatriculaStringDTO>(matricula)!;
                }
                return codigoMatriculas;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 15/12/2022
        /// Version: 1.0
        /// <summary>
        /// Permite obtener los Id del programa especifico, nombre completo del programa especifico enviandole como parametro 
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns> List<MatriculaCabeceraComboDTO> </returns>
        public IEnumerable<MatriculaCabeceraComboDTO> ObtenerCodigoMatriculaAutocompleto(string nombre)
        {
            try
            {
                IEnumerable<MatriculaCabeceraComboDTO> rpta = new List<MatriculaCabeceraComboDTO>();
                string query = @"
                                SELECT 
                                    Id, CodigoMatricula 
                                FROM 
                                    fin.T_MatriculaCabecera 
                                WHERE
                                    CodigoMatricula LIKE CONCAT('%',@nombre,'%') AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { nombre });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<MatriculaCabeceraComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerCodigoMatriculaAutocompleto()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/12/2022
        /// Version: 1.0
        /// <summary>
        /// modifica el gestor de la tabla T_CronogramaPagoDetalleFinal
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="Usuario"> Usuario Responsable </param>
        /// <param name="Gestor"> Nuevo gestor</param>
        /// <param name="IdMatriculaCabecera"> Id de la matricula</param>
        /// <returns> int </returns>
        public int ModificarGestorDeCobranza(string usuario, string gestor, int idMatriculaCabecera)
        {
            try
            {
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_ActualizarGestorDeCobranza", new { usuario, gestor, idMatriculaCabecera });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de las vista V_Reclamo
        /// </summary>
        /// <param name="codMatricula"> Codigo de matricula </param>
        /// <param name="dni"> Numero documento DNI</param>
        /// <returns> List<FiltroMatriculaAlumnoDTO> </returns>
        public List<FiltroMatriculaAlumnoDTO> ObtenerAlumnosMatriculados(string codMatricula, string dni)
        {
            try
            {

                string condicion = string.Empty;
                if (!string.IsNullOrEmpty(codMatricula))
                {
                    condicion = condicion + " CodigoMatricula =@codMatricula ";
                }
                if (!string.IsNullOrEmpty(dni))
                {
                    if (!string.IsNullOrEmpty(dni) && !string.IsNullOrEmpty(codMatricula))
                    {
                        condicion = condicion + " and ";
                    }
                    condicion = condicion + " DNI = @DNI ";
                }
                List<FiltroMatriculaAlumnoDTO> lista = new List<FiltroMatriculaAlumnoDTO>();
                var _query = string.Empty;

                _query = "SELECT IdMatricula,CodigoMatricula, DNI, nombreAlumno, PersonalAsignado, CentroCosto, EstadoMatricula FROM mkt.V_Reclamo where " + condicion;
                var listaDB = _dapperRepository.QueryDapper(_query, new { codMatricula = codMatricula, DNI = dni });
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<FiltroMatriculaAlumnoDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza El estado de La matricula Por IdSolicitud
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <param name="nombreEstado"></param>
        /// <returns> bool </returns>
        public bool ActualizarEstadoMatriculaPorSolicitud(int idSolicitudOperaciones, string nombreEstado)
        {
            try
            {
                var _query = "ope.SP_ActualizarEstadoMatriculaPorSolicitud";
                var pEspecificoDB = _dapperRepository.QuerySPDapper(_query, new { idSolicitudOperaciones, nombreEstado });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza El estado de La matricula Por IdSolicitud
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <param name="nombreSubEstado"></param>
        /// <returns> bool </returns>
        public bool ActualizarSubEstadoMatriculaPorSolicitud(int idSolicitudOperaciones, string nombreSubEstado)
        {
            try
            {
                var _query = "ope.SP_ActualizarSubEstadoMatriculaPorSolicitud";
                var pEspecificoDB = _dapperRepository.QuerySPDapper(_query, new { idSolicitudOperaciones, nombreSubEstado });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MatriculaCabecera por Id.
        /// </summary>
        /// <returns> Entidad: MatriculaCabecera> </returns>
        public MatriculaCabecera? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        CodigoMatricula,
                        IdAlumno,
                        IdPEspecifico AS IdPespecifico,
                        IdEstadoPagoMatricula,
                        EstadoMatricula,
                        FechaMatricula,
                        EmpresaRuc,
                        EmpresaNombre,
                        EmpresaContacto,
                        EmpresaEmail,
                        EmpresaPaga,
                        EmpresaObservaciones,
                        IdDocumentoPago,
                        IdCoordinador,
                        IdAsesor,
                        IdEstado_matricula AS IdEstadoMatricula,
                        FechaSuspendido,
                        UsuarioCoordinadorAcademico,
                        ObservacionGeneralOperaciones,
                        UsuarioCoordinadorSupervision,
                        IdCronograma,
                        IdPeriodo,
                        UsuarioCoordinadorPreAsignacion,
                        VerificacionConforme,
                        FechaMatriculaValidada,
                        FechaPagoValidada,
                        FechaRetiro,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        GrupoCurso,
                        IdSubEstadoMatricula,
                        IdPaquete,
                        FechaFinalizacion,
                        IdEstadoMatriculaCertificado,
                        IdSubEstadoMatriculaCertificado,
                        EsInhouse,
                        FechaPorMatricularMatriculado,
                        IdCategoriaAlumno
                    FROM 
                        fin.T_MatriculaCabecera 
                    WHERE 
                        Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<MatriculaCabecera>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdAlumno, UsuarioCoordinadorAcademico de todas una matricula
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns> DTO: DatosAlumnoCoordinadorMatriculaCabeceraDTO </returns>
        public DatosAlumnoCoordinadorMatriculaCabeceraDTO ObtenerIdAlumnoCoordinadorAcademico(int idMatriculaCabecera)
        {
            try
            {
                DatosAlumnoCoordinadorMatriculaCabeceraDTO matricula = new DatosAlumnoCoordinadorMatriculaCabeceraDTO();
                var query = @"
                            SELECT 
                                IdAlumno, UsuarioCoordinadorAcademico 
                            FROM 
                                fin.T_MatriculaCabecera 
                            WHERE 
                                Estado = 1 and Id=@IdMatriculaCabecera";
                var subQuery = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]"))
                {
                    matricula = JsonConvert.DeserializeObject<DatosAlumnoCoordinadorMatriculaCabeceraDTO>(subQuery)!;
                }
                return matricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MatriculaCabecera por CodigoMatricula.
        /// </summary>
        /// <returns> Entidad: MatriculaCabecera> </returns>
        public MatriculaCabecera ObtenerPorCodigoMatricula(string codigoMatricula)
        {
            try
            {
                var query = @"
                            SELECT 
                                Id, CodigoMatricula, IdAlumno, IdPEspecifico, IdEstadoPagoMatricula, EstadoMatricula, FechaMatricula, EmpresaRuc, EmpresaNombre, EmpresaContacto, EmpresaEmail,
                                EmpresaPaga, EmpresaObservaciones, IdDocumentoPago, IdCoordinador, IdAsesor, IdEstado_matricula, FechaSuspendido, UsuarioCoordinadorAcademico, IdCategoriaAlumno, 
                                UsuarioCoordinadorSupervision, IdCronograma, IdPeriodo, UsuarioCoordinadorPreAsignacion, VerificacionConforme, FechaMatriculaValidada, ObservacionGeneralOperaciones,
                                FechaRetiro, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion, GrupoCurso, IdSubEstadoMatricula, IdPaquete,
                                FechaFinalizacion, IdEstadoMatriculaCertificado, IdSubEstadoMatriculaCertificado, EsInhouse, FechaPorMatricularMatriculado, FechaPagoValidada
                            FROM 
                                fin.T_MatriculaCabecera 
                            WHERE 
                                Estado = 1 AND CodigoMatricula = @CodigoMatricula";
                var resultado = _dapperRepository.FirstOrDefault(query, new { CodigoMatricula = codigoMatricula });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<MatriculaCabecera>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory ramirez
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MatriculaCabecera.
        /// </summary>
        /// <returns> Entidad: MatriculaCabecera> </returns>
        public List<CodigoMatriculaIdStringDTO> ObtenerCodigoMatricula(string Codigo)
        {
            try
            {
                List<CodigoMatriculaIdStringDTO> codigosMatriculaPEspecifico = new List<CodigoMatriculaIdStringDTO>();
                var _query = "SELECT CodigoMatricula as Id FROM fin.T_MatriculaCabecera WHERE  CodigoMatricula like CONCAT('%',@Codigo,'%') AND Estado=1";
                var codigosMatriculaDB = _dapperRepository.QueryDapper(_query, new { Codigo });
                if (!string.IsNullOrEmpty(codigosMatriculaDB) && !codigosMatriculaDB.Contains("[]"))
                {
                    codigosMatriculaPEspecifico = JsonConvert.DeserializeObject<List<CodigoMatriculaIdStringDTO>>(codigosMatriculaDB);
                }
                return codigosMatriculaPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// Autor: Margiory ramirez
        /// Fecha: 17/01/2023
        /// <summary>
        /// Obtener Codigo de Matricula y Programa del alumno
        /// </summary>
        /// <param name="idAlumno"> Id Alumno </param>        
        /// <returns> Lista Cronograma del Alumno : List<CodigoMatriculaPEspecificoDTO> </returns>
        public List<CodigoMatriculaPEspecificoDTO> ObtenerCodigoMatriculaPEspecificoPorAlumnos(int idAlumno)
        {
            try
            {
                List<CodigoMatriculaPEspecificoDTO> codigosMatriculaPEspecifico = new List<CodigoMatriculaPEspecificoDTO>();
                var query = "SELECT CodigoMatricula, PEspecifico FROM fin.V_ObtenerCodigoMatriculaPEspecifico WHERE  IdAlumno = @idAlumno";
                var codigosMatriculaPEspecificoDB = _dapperRepository.QueryDapper(query, new { idAlumno });
                if (!string.IsNullOrEmpty(codigosMatriculaPEspecificoDB) && !codigosMatriculaPEspecificoDB.Contains("[]"))
                {
                    codigosMatriculaPEspecifico = JsonConvert.DeserializeObject<List<CodigoMatriculaPEspecificoDTO>>(codigosMatriculaPEspecificoDB);
                }
                return codigosMatriculaPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Margiory ramirez
        /// Fecha: 17/01/2023
        /// <summary>
        /// Obtiene el alumno por el codigo del programa especifico
        /// </summary>
        /// <param name="idCabeceraMatricula"></param>
        /// <returns></returns>
        public List<AlumnoProgramaEspecificoDTO> ObtenerAlumnoProgramaEspecifico(int idCabeceraMatricula)
        {
            try
            {
                List<AlumnoProgramaEspecificoDTO> alumnoProgramaEspecifico = new List<AlumnoProgramaEspecificoDTO>();
                var _query = "SELECT CodigoMatricula, IdPEspecifico, PEspecifico, NombreCompletoAlumno NombreCompleto, Dni, NombresAlumno, ApellidosAlumno, DireccionAlumno, " +
                    "CelularAlumno, IdCiudad, NombreCiudad, CorreoAlumno, FechaMatricula, NombreCentroCosto FROM fin.V_ObtenerAlumnoProgramaEspecifico " +
                    "WHERE IdMatriculaCabecera = @idCabeceraMatricula AND EstadoPEspecifico = 1 AND EstadoAlumno = 1";
                var alumnoProgramaEspecificoDB = _dapperRepository.QueryDapper(_query, new { idCabeceraMatricula });
                if (!string.IsNullOrEmpty(alumnoProgramaEspecificoDB) && !alumnoProgramaEspecificoDB.Contains("[]"))
                {
                    alumnoProgramaEspecifico = JsonConvert.DeserializeObject<List<AlumnoProgramaEspecificoDTO>>(alumnoProgramaEspecificoDB);
                }
                return alumnoProgramaEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory ramirez
        /// Fecha: 17/01/2023
        /// <summary>
        /// Obtiene el alumno por el codigo del programa especifico
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public CodigoMatriculaV2DTO ObtenerIdMatriculaPorCodigo(string Codigo)
        {
            try
            {
                List<CodigoMatriculaV2DTO> codigosMatriculaPEspecifico = new List<CodigoMatriculaV2DTO>();
                var _query = "SELECT top 1 Id,CodigoMatricula, EstadoMatricula,IdAlumno FROM fin.T_MatriculaCabecera WHERE  CodigoMatricula=@Codigo ";
                var codigosMatriculaDB = _dapperRepository.QueryDapper(_query, new { Codigo });
                if (!string.IsNullOrEmpty(codigosMatriculaDB) && !codigosMatriculaDB.Contains("[]"))
                {
                    codigosMatriculaPEspecifico = JsonConvert.DeserializeObject<List<CodigoMatriculaV2DTO>>(codigosMatriculaDB);
                }
                else
                {
                    return null;
                }
                return codigosMatriculaPEspecifico[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Margiory ramirez
        /// Fecha: 17/01/2023
        /// Obtiene los datos de una matricula por codigoMatricual y version
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <param name="version"></param>
        public List<DatosMatriculaDTO> ObtenerDatosMatriculaPorCodigoMatriculaVersion(string codigoMatricula, int? version)
        {
            try
            {
                List<DatosMatriculaDTO> datosMatricula = new List<DatosMatriculaDTO>();
                var _query = @"
                    SELECT 
                        CodigoMatricula As Id, IdPEspecifico, 
                        Moneda,TipoCambio,Max(TotalAPagar) AS TotalAPagar, 
                        Max(NroCuotas) AS NroCuotas,EstadoMatricula, 
                        Lower(Periodo) AS Periodo, Programa, Coordinador, 
                        Asesor, Paquete, Titulo, Observaciones, 
                        EmpresaPaga,EmpresaNombre, IdCoordinador,IdAsesor 
                    FROM fin.V_ObtenerDatosMatriculaV5
                    WHERE CodigoMatricula = @codigoMatricula AND  version = @version 
                    GROUP  BY 
                        id,IdPEspecifico, 
                        EstadoMatricula,CodigoMatricula, 
                        moneda,TipoCambio, 
                        Periodo,Programa, 
                        Coordinador, Asesor, Paquete, 
                        Titulo, Observaciones, EmpresaPaga, 
                        EmpresaNombre, IdCoordinador, IdAsesor";

                var datosMatriculaDB = _dapperRepository.QueryDapper(_query, new { codigoMatricula, version });
                if (!string.IsNullOrEmpty(datosMatriculaDB) && !datosMatriculaDB.Contains("[]"))
                {
                    datosMatricula = JsonConvert.DeserializeObject<List<DatosMatriculaDTO>>(datosMatriculaDB);
                }
                return datosMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Margiory ramirez
        /// Fecha: 17/01/2023
        /// Obtiene beneficios por el codigo de matricula
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <returns></returns>
        public List<BeneficiosCodigoMatriculaDTO> ObtenerBeneficiosMatriculaCabecera(string codigoMatricula)
        {
            try
            {
                List<BeneficiosCodigoMatriculaDTO> beneficiosCodigoMatricula = new List<BeneficiosCodigoMatriculaDTO>();
                var _query = "SELECT Titulo , CodigoMatricula  FROM com.V_MatriculaCabeceraBeneficios WHERE CodigoMatricula =  @codigoMatricula AND Estado=1 ";
                var beneficiosCodigoMatriculaDB = _dapperRepository.QueryDapper(_query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<BeneficiosCodigoMatriculaDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 17/09/2024
        /// <summary>
        /// Obtiene el país al que pertenece una matrícula
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public PaisMatriculaDTO ObtenerPaisMatricula(string CodigoMatricula)
        {
            try
            {
                List<PaisMatriculaDTO> paisMatricula = new List<PaisMatriculaDTO>();
                var _query = "SELECT TOP 1 * FROM fin.V_ObtenerPaisMatricula WHERE CodigoMatricula = @CodigoMatricula ";
                var resp = _dapperRepository.QueryDapper(_query, new { CodigoMatricula });
                if (!string.IsNullOrEmpty(resp) && !resp.Contains("[]"))
                {
                    paisMatricula = JsonConvert.DeserializeObject<List<PaisMatriculaDTO>>(resp);
                }
                else
                {
                    return null;
                }
                return paisMatricula[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<MatriculaPespecificoAlumnoDTO> ObtenerCursosMatriculados(int idMatriculaCabecera)
        {
            try
            {
                List<MatriculaPespecificoAlumnoDTO> pEspecificoMatriculaAlumno = new List<MatriculaPespecificoAlumnoDTO>();
                var query = @"
                            SELECT
                                P.Nombre,
                                PM.Id,
                                P.Duracion,
                                P.Tipo

                            FROM ope.T_PEspecificoMatriculaAlumno AS PM
                                INNER JOIN pla.T_PEspecifico AS P ON P.Id=PM.IdPEspecifico
                            WHERE PM.Estado =1 AND IdMatriculaCabecera = @idMatriculaCabecera";
                var fasesOportunidadDB = _dapperRepository.QueryDapper(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(fasesOportunidadDB) && !fasesOportunidadDB.Contains("[]"))
                {
                    pEspecificoMatriculaAlumno = JsonConvert.DeserializeObject<List<MatriculaPespecificoAlumnoDTO>>(fasesOportunidadDB)!;
                }
                return pEspecificoMatriculaAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool desmatricularCurso(int idPespecificoMatriculaAlumno, string usuario)
        {
            try
            {

                var _query = "[ope].SP_DesmatricularAlumnoPespecifico";
                var pEspecificoDB = _dapperRepository.QuerySPDapper(_query, new { @Id = idPespecificoMatriculaAlumno, @usuarioModificacion = usuario, });
                return true;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Estados Matricula
        /// </summary>
        /// <returns>< ListaDTO: List<EstadosMatriculaDTO> </returns>
        public List<EstadosMatriculaDTO> ObtenerEstadosMatricula()
        {

            try
            {
                List<EstadosMatriculaDTO> fasesOportunidad = new List<EstadosMatriculaDTO>();
                var query = @"
                            SELECT 
                                Id, Nombre 
                            FROM 
                                fin.V_TEstadosMatriculas 
                            WHERE 
                                Estado = 1";
                var fasesOportunidadDB = _dapperRepository.QueryDapper(query, new { });
                if (!string.IsNullOrEmpty(fasesOportunidadDB) && !fasesOportunidadDB.Contains("[]"))
                {
                    fasesOportunidad = JsonConvert.DeserializeObject<List<EstadosMatriculaDTO>>(fasesOportunidadDB)!;
                }
                return fasesOportunidad;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 11/02/2023
        /// <summary>
        /// Obtiene el listado de alumnos matricula, filtrado por el codigo especifico
        /// </summary>
        /// <param name="idProgramaEspecifico"></param>
        /// <returns></returns>
        public List<AlumnoMatriculaDTO> ObtenerListadoAlumnosMatriculaCodigoPEspecifico(int idProgramaEspecifico)
        {
            try
            {
                List<AlumnoMatriculaDTO> listadoAlumnosMatricula = new List<AlumnoMatriculaDTO>();
                var _query = "SELECT CodigoAlumno, CodigoProgramaEspecifico, NombreProgramaEspecifico, CodigoMatricula," +
                    " NombreCompletoAlumno,FechaMatricula,Estado FROM fin.V_ObtenerListadoAlumnosMatricula" +
                    " WHERE  IdProgramaEspecifico= @idProgramaEspecifico";
                var listadoAlumnosMatriculaDB = _dapperRepository.QueryDapper(_query, new { idProgramaEspecifico });
                if (!string.IsNullOrEmpty(listadoAlumnosMatriculaDB) && !listadoAlumnosMatriculaDB.Contains("[]"))
                {
                    listadoAlumnosMatricula = JsonConvert.DeserializeObject<List<AlumnoMatriculaDTO>>(listadoAlumnosMatriculaDB);
                }
                return listadoAlumnosMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 11/02/2023
        /// <summary>
        /// Obtiene el listado de alumnos matricula, filtrado por el id del alumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns></returns>
        public List<AlumnoMatriculaDTO> ObtenerListadoAlumnosMatriculaIdAlumno(int idAlumno)
        {
            try
            {
                List<AlumnoMatriculaDTO> listadoAlumnosMatricula = new List<AlumnoMatriculaDTO>();
                var _query = "SELECT CodigoAlumno, CodigoProgramaEspecifico, NombreProgramaEspecifico, CodigoMatricula," +
                    " NombreCompletoAlumno,FechaMatricula,Estado FROM fin.V_ObtenerListadoAlumnosMatricula WHERE CodigoAlumno= @idAlumno";
                var listadoAlumnosMatriculaDB = _dapperRepository.QueryDapper(_query, new { idAlumno });
                if (!string.IsNullOrEmpty(listadoAlumnosMatriculaDB) && !listadoAlumnosMatriculaDB.Contains("[]"))
                {
                    listadoAlumnosMatricula = JsonConvert.DeserializeObject<List<AlumnoMatriculaDTO>>(listadoAlumnosMatriculaDB);
                }
                return listadoAlumnosMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de alumnos matricula, filtrado por el Id Matricula
        /// </summary>
        /// <param name="codMat"></param>
        /// <returns></returns>
        public List<AlumnoMatriculaDTO> ObtenerListadoAlumnosMatriculaICodigoMatricula(string codMat)
        {
            try
            {
                List<AlumnoMatriculaDTO> listadoAlumnosMatricula = new List<AlumnoMatriculaDTO>();
                var _query = "SELECT CodigoAlumno, CodigoProgramaEspecifico, NombreProgramaEspecifico, CodigoMatricula," +
                    " NombreCompletoAlumno,FechaMatricula,Estado FROM fin.V_ObtenerListadoAlumnosMatricula WHERE CodigoMatricula= @codMat";
                var listadoAlumnosMatriculaDB = _dapperRepository.QueryDapper(_query, new { codMat });
                if (!string.IsNullOrEmpty(listadoAlumnosMatriculaDB) && !listadoAlumnosMatriculaDB.Contains("[]"))
                {
                    listadoAlumnosMatricula = JsonConvert.DeserializeObject<List<AlumnoMatriculaDTO>>(listadoAlumnosMatriculaDB);
                }
                return listadoAlumnosMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 11/02/2023
        /// <summary>
        /// Obtiene el listado de alumnos matricula 
        /// </summary>
        /// <returns></returns>
        public List<AlumnoMatriculaDTO> ObtenerListadoAlumnosMatricula()
        {
            try
            {
                List<AlumnoMatriculaDTO> listadoAlumnosMatricula = new List<AlumnoMatriculaDTO>();
                var _query = "SELECT CodigoAlumno, CodigoProgramaEspecifico, NombreProgramaEspecifico, CodigoMatricula," +
                    " NombreCompletoAlumno,FechaMatricula,Estado FROM fin.V_ObtenerListadoAlumnosMatricula";
                var listadoAlumnosMatriculaDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listadoAlumnosMatriculaDB) && !listadoAlumnosMatriculaDB.Contains("[]"))
                {
                    listadoAlumnosMatricula = JsonConvert.DeserializeObject<List<AlumnoMatriculaDTO>>(listadoAlumnosMatriculaDB);
                }
                return listadoAlumnosMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 11/02/2023
        /// <summary>
        /// Obtener las solicitudes de cambio de cronograma por personal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<SolicitudCambioDTO> ObtenerSolicitudesCambioCronograma(int idPersonal)
        {
            try
            {
                List<SolicitudCambioDTO> solicitudesCambios = new List<SolicitudCambioDTO>();
                var solicitudesCambiosDB = _dapperRepository.QuerySPDapper("fin.ObtenerSolicitudesCambioCronogramaPorVersion", new { idPersonal });
                if (!string.IsNullOrEmpty(solicitudesCambiosDB) && !solicitudesCambiosDB.Contains("[]"))
                {
                    solicitudesCambios = JsonConvert.DeserializeObject<List<SolicitudCambioDTO>>(solicitudesCambiosDB);
                }
                return solicitudesCambios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Jonathan Caipo
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// retorna true o false dependiendo si existe un matricula para ese alumno con ese programa especifico
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public bool ExisteMatriculaCabecera(int idAlumno, int idPEspecifico)
        {
            try
            {
                bool existe = false;
                var cantidad = GetBy(x => x.IdAlumno == idAlumno && x.IdPespecifico == idPEspecifico, x => new { x.Id }).Count();
                if (cantidad > 0)
                {
                    existe = true;
                }
                else
                {
                    existe = false;
                }
                return existe;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de la matricula manual por el codigo de matricula
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <returns></returns>
        public DatosMatriculaManualDTO ObtenerDatosMatriculaManual(string codigoMatricula)
        {
            try
            {
                DatosMatriculaManualDTO datosMatriculaManual = new DatosMatriculaManualDTO();
                var query = "SELECT Id, CodigoMatricula, NombreCompletoAlumno, IdAlumno, Moneda, LEFT(convert(varchar,FechaIniPago,120),10) FechaIniPago, convert(decimal(10,2),TipoCambio) TipoCambio, convert(decimal(10,2), max(TotalPagar))TotalPagar, max(NroCuotas) NroCuotas, Periodo,  NombreProgramaCentroCosto AS NombrePrograma, IdPEspecifico, Coordinador, Asesor, TituloAcuerdoPago FROM fin.V_ObtenerDatosMatriculaManual WHERE CodigoMatricula = @codigoMatricula AND EstadoMatriculaCabecera = 1 AND EstadoProgramaEspecifico =1 AND EstadoCentroCosto = 1 AND EstadoAlumno = 1 Group by Id, CodigoMatricula, NombreCompletoAlumno, IdAlumno, FechaIniPago, Periodo, Moneda, NombreProgramaCentroCosto, IdPEspecifico, NombreProgramaEspecifico, TipoCambio, Coordinador, Asesor, TituloAcuerdoPago";
                var datosMatriculaManualDB = _dapperRepository.FirstOrDefault(query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(datosMatriculaManualDB))
                {
                    datosMatriculaManual = JsonConvert.DeserializeObject<DatosMatriculaManualDTO>(datosMatriculaManualDB);
                }
                return datosMatriculaManual;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 11/02/2023
        /// <summary>
        /// Obtiene el centro de costo por matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public CentroCostoNombreDTO ObtenerCentroCostoPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                CentroCostoNombreDTO nombresCentroCosto = new CentroCostoNombreDTO();
                var query = "SELECT NombreCentroCosto FROM fin.V_ObtenerCentroCostoPorMatricula WHERE IdMatriculaCabecera = @idMatriculaCabecera AND EstadoMatriculaCabecera = 1 AND EstadoPEspecifico = 1 AND EstadoCentroCosto = 1";
                var nombreCentroCostoDB = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(nombreCentroCostoDB) && !nombreCentroCostoDB.Contains("null"))
                {
                    nombresCentroCosto = JsonConvert.DeserializeObject<CentroCostoNombreDTO>(nombreCentroCostoDB);
                }
                return nombresCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene idMatriculaCabecera y DNI
        /// </summary>
        /// <returns>< DTO: IdMatriculaDniDTO </returns>
        public IdMatriculaDniDTO obtenerIdMatriculaporDni(string DNI)
        {
            try
            {
                IdMatriculaDniDTO datosIdMatricula = new IdMatriculaDniDTO();
                var query = @"
                           SELECT DNI,
                                idMatriculaCabecera 
                           FROM ope.V_ObtenerIdMatriculaPorDni 
                           WHERE DNI=@DNI";
                var datoMatricula = _dapperRepository.FirstOrDefault(query, new { DNI = DNI });
                if (!string.IsNullOrEmpty(datoMatricula) && !datoMatricula.Contains("[]"))
                {
                    datosIdMatricula = JsonConvert.DeserializeObject<IdMatriculaDniDTO>(datoMatricula)!;
                }
                return datosIdMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor:Joseph Llanque
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene idMatriculaCabecera y Correo
        /// </summary>
        /// <returns>< DTO: IdMatriculaCorreoDTO </returns>
        public IdMatriculaCorreoDTO obtenerIdMatriculaporCorreo(string correo)
        {
            try
            {
                IdMatriculaCorreoDTO datosIdMatricula = new IdMatriculaCorreoDTO();
                var query = @"
                           SELECT Correo,
                                  idMatriculaCabecera 
                                  FROM ope.V_ObtenerIdMatriculaPorCorreo 
                                  WHERE Correo=@correo";
                var datoMatricula = _dapperRepository.FirstOrDefault(query, new { correo = correo });
                if (!string.IsNullOrEmpty(datoMatricula) && !datoMatricula.Contains("[]"))
                {
                    datosIdMatricula = JsonConvert.DeserializeObject<IdMatriculaCorreoDTO>(datoMatricula)!;
                }
                return datosIdMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor:Joseph Llanque
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene idMatriculaCabecera y codigoMatricula
        /// </summary>
        /// <returns>< DTO: MatriculaCabeceraComboDTO </returns>
        public MatriculaCabeceraComboDTO obtenerIdMatriculaporCodigo(string codigo)
        {
            try
            {
                MatriculaCabeceraComboDTO datosIdMatricula = new MatriculaCabeceraComboDTO();
                var query = @"
                           SELECT Id,
                                  CodigoMatricula 
                                  FROM fin.T_MatriculaCabecera 
                                  WHERE CodigoMatricula=@codigo";
                var datoMatricula = _dapperRepository.FirstOrDefault(query, new { codigo = codigo });
                if (!string.IsNullOrEmpty(datoMatricula) && !datoMatricula.Contains("[]"))
                {
                    datosIdMatricula = JsonConvert.DeserializeObject<MatriculaCabeceraComboDTO>(datoMatricula)!;
                }
                return datosIdMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BeneficioDatosAdicionalesDTO> ObtenerDatosAdicionalesPgeneralPorIdConfiguracion(int IdConfiguracionBeneficio)
        {
            try
            {
                List<BeneficioDatosAdicionalesDTO> beneficiosCodigoMatricula = new List<BeneficioDatosAdicionalesDTO>();
                var _query = "SELECT IdPgeneral,IdConfiguracionBeneficio,IdDatoAdicional FROM [com].[V_BeneficioDatosAdicionalesMatriculaCabecera] where IdConfiguracionBeneficio = @IdConfiguracionBeneficio";
                var beneficiosCodigoMatriculaDB = _dapperRepository.QueryDapper(_query, new { IdConfiguracionBeneficio });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    beneficiosCodigoMatricula = JsonConvert.DeserializeObject<List<BeneficioDatosAdicionalesDTO>>(beneficiosCodigoMatriculaDB);
                }
                return beneficiosCodigoMatricula;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public int ActualizarEstadoMatriculaCabeceraBeneficio(int IdMatriculaCabeceraBeneficio)
        {
            try
            {

                var _query = "com.SP_ActualizarMatriculaCabeceraBeneficio";
                var beneficiosCodigoMatriculaDB = _dapperRepository.QuerySPDapper(_query, new { IdMatriculaCabeceraBeneficio });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public int PorAprobarSolicitudBeneficio(int IdMatriculaCabeceraBeneficio)
        {
            try
            {

                var _query = "com.SP_PorAprobarBeneficioMatriculaCabecera";
                var beneficiosCodigoMatriculaDB = _dapperRepository.QuerySPDapper(_query, new { IdMatriculaCabeceraBeneficio });
                if (!string.IsNullOrEmpty(beneficiosCodigoMatriculaDB) && !beneficiosCodigoMatriculaDB.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: TMatriculaCabeceraRepositorioAula
        /// Autor: Miguel Mora
        /// Fecha: 14/09/2022
        /// <summary>
        /// Obtiene información de la matrícula de un curso padre según la matricula y el IdPEspecífico
        /// </summary>
        /// <returns> registroDatosMatriculaCabeceraDTO </returns>  
        public registroDatosMatriculaCabeceraCompletoDTO obtenerGrupoMatriculaIdPorCurso(int IdMatriculaCabecera, int IdPEspecifico)
        {
            try
            {
                registroDatosMatriculaCabeceraCompletoDTO rpta = new registroDatosMatriculaCabeceraCompletoDTO();
                string _query = "Select * From pw.V_PW_DatoMatriculaAlumnoPorCurso WHERE IdMatriculaCabecera=@IdMatriculaCabecera AND IdPEspecifico=@IdPEspecifico";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdMatriculaCabecera = IdMatriculaCabecera, IdPEspecifico = IdPEspecifico });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    rpta = JsonConvert.DeserializeObject<registroDatosMatriculaCabeceraCompletoDTO>(respuesta);
                    return rpta;
                }
                return null;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// Repositorio: TMatriculaCabeceraRepositorioAula
        /// Autor: Daniel Huaita
        /// Fecha: 28/03/2023
        /// <summary>
        /// Obtiene información de la matrícula de un curso padre según la matricula y el IdPEspecífico sin validar el estado matricula
        /// </summary>
        /// <returns> registroDatosMatriculaCabeceraDTO </returns>  
        public registroDatosMatriculaCabeceraCompletoDTO obtenerGrupoMatriculaIdPorCursoGeneral(int IdMatriculaCabecera, int IdPEspecifico)
        {
            try
            {
                registroDatosMatriculaCabeceraCompletoDTO rpta = new registroDatosMatriculaCabeceraCompletoDTO();
                string _query = "Select * From ope.V_DatoMatriculaAlumnoPorCursoGeneral WHERE IdMatriculaCabecera=@IdMatriculaCabecera AND IdPEspecifico=@IdPEspecifico";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdMatriculaCabecera = IdMatriculaCabecera, IdPEspecifico = IdPEspecifico });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    rpta = JsonConvert.DeserializeObject<registroDatosMatriculaCabeceraCompletoDTO>(respuesta);
                    return rpta;
                }
                return null;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los idsubestado de los estados para seguimiento academico,al dia, atrasado
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
        /// <returnsList<ConfiguracionCoordinadoraSubEstadoMatricula></returns>
        public IntDTO? ObtenerSubEstadoPorIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                var query = "SELECT IdSubEstado AS Valor FROM ope.V_MatriculaSubEstado WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
		/// Fecha: 20/10/2022
		/// Version: 1.0
        /// <summary>
        /// Obtiene la cantidad de T_MatriculaCabecera por IdPEspecifico y UsuarioCoordinadorAcademico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <param name="usuarioCoordinador"></param>
        /// <returns></returns>
        public IntDTO ObtenerCantidadPorIdPespecificoUsuarioCoordinador(int idPEspecifico, string usuarioCoordinador)
        {
            try
            {
                IntDTO respuesta = new IntDTO();
                var query = @"SELECT COUNT(Id) AS Valor
                            FROM fin.T_MatriculaCabecera 
                            WHERE IdPEspecifico = @IdEspecifico AND UsuarioCoordinadorAcademico = @UsuarioCoordinador";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdEspecifico = idPEspecifico, UsuarioCoordinador = usuarioCoordinador });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 05/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los detalles de la MatriculaCabecera por el (PK)
        /// </summary>
        /// <param name="id"> (PK) Primary Key</param> 
        /// <returns> MatriculaCabeceraDetallesDTO </returns>
        public MatriculaCabeceraDetallesDTO ObtenerDetallesPorId(int id)
        {
            try
            {
                var _query = @"SELECT IdMatriculaCabecera,
                               CodigoMatricula,
                               IdAlumno,
                               PGeneral,
                               IdBusqueda,
                               IdPGeneral,
                               IdPEspecifico
                        FROM [fin].[V_CronogramaMatriculadoAlumno]
                        WHERE  IdMatriculaCabecera = @IdMatriculaCabecera;";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdMatriculaCabecera = id });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null")
                    return JsonConvert.DeserializeObject<MatriculaCabeceraDetallesDTO>(respuesta);

                return new MatriculaCabeceraDetallesDTO();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 05/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de cuotas a pagar por el Id (PK)
        /// </summary>
        /// <param name="id"> (PK) Primary Key</param> 
        /// <returns> List<ListaCuotaPagoDTO> </returns>
        public IEnumerable<ListaCuotaPagoDTO> ObtenerCuotasMatriculaoPorId(int id)
        {
            try
            {
                var _query = @"SELECT IdCuota,
                                   NroCuota,
                                   Cuota,
                                   Mora,
                                   MontoPagado,
                                   TipoCuota,
                                   FechaVencimiento,
                                   FechaPago,
                                   Cancelado,
                                   Moneda,
                                   WebMoneda,
                                   IdMatriculaCabecera,
                                   Simbolo,
                                   NombreMoneda,
                                   MoraCalculada,
                                   Version,
                                   IdAlumno
                            FROM [pw].[V_CronogramaCuotaMatriculadoAlumno]
                            WHERE   IdMatriculaCabecera = @IdMatriculaCabecera";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdMatriculaCabecera = id });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ListaCuotaPagoDTO>>(respuesta);
                }
                return new List<ListaCuotaPagoDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 05/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el resumen de cuota por el Id (PK)
        /// </summary>
        /// <param name="id"> (PK) Primary Key</param> 
        /// <returns> ResumenCronogramaPagoDTO </returns>
        public ResumenCronogramaPagoDTO ObtenerResumenCronogramaCuotaPorId(int id)
        {
            try
            {
                var _query = @"SELECT IdAlumno,
                                       IdMatriculaCabecera,
                                       NumeroCuota,
                                       CuotasPagadas,
                                       CuotasPendientes,
                                       FechaVencimiento
                                FROM pw.V_ResumenEstadoCronogramaPagoAlumno
                                WHERE IdMatriculaCabecera = @IdMatriculaCabecera;";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdMatriculaCabecera = id });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null")
                    return JsonConvert.DeserializeObject<ResumenCronogramaPagoDTO>(respuesta);

                return new ResumenCronogramaPagoDTO();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 07/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Registro Medio Pago asociado al IdMatriculaCabecera
        /// </summary>
        /// <param name="id"> (PK) Primary Key de T_MatriculaCabecera </param>  
        /// <returns> IntDTO </returns>
        public IntDTO ObtenerRegistroMedioPagoPorId(int id)
        {
            try
            {
                var _query = "SELECT IdMedioPago as Valor FROM fin.V_RegistroMedioPagoMatriculaCronograma WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdMatriculaCabecera = id });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                    return JsonConvert.DeserializeObject<IntDTO>(respuesta);
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 07/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Registros de Medios de Pago asociado al IdMatriculaCabecera  
        /// </summary>
        /// <param name="tieneMedioConfigurado"> flag de validacion Medio Pago Configurado </param>  
        /// <param name="id"> (PK) Primary Key de T_MatriculaCabecera </param>  
        /// <param name="idPasarelaPago"> (PK) Primary Key de T_PasarelaPago_Pw </param>  
        /// <returns> IEnumerable<MedioPagoActivoPasarelaDTO> </returns>
        public IEnumerable<MedioPagoActivoPasarelaDTO> ObtenerListaMedioPagoMatriculaCronograma(bool tieneMedioConfigurado, int id, int idPasarelaPago)
        {
            try
            {
                var _query = string.Empty;
                IEnumerable<MedioPagoActivoPasarelaDTO> listaAreas = new List<MedioPagoActivoPasarelaDTO>();
                if (tieneMedioConfigurado)
                {
                    _query = @"SELECT IdPasarelaPago,
                                       PasarelaNombre,
                                       MedioPago,
                                       MedioCodigo,
                                       Imagen,
                                       IdFormaPago,
                                       DatosCapturados
                                FROM pw.V_MedioPagoMatriculaCronogramaProveedor
                                WHERE IdMatriculaCabecera = @IdMatriculaCabecera
                                      AND IdPasarelaPago = @IdPasarelaPago ";
                    var respuesta = _dapperRepository.QueryDapper(_query, new { IdMatriculaCabecera = id, IdPasarelaPago = idPasarelaPago });
                    if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                    {
                        listaAreas = JsonConvert.DeserializeObject<IEnumerable<MedioPagoActivoPasarelaDTO>>(respuesta);
                    }
                }
                else
                {
                    _query = @"SELECT IdPasarelaPago,
                                       PasarelaNombre,
                                       MedioPago,
                                       MedioCodigo,
                                       Imagen,
                                       IdFormaPago,
                                       DatosCapturados
                                FROM pw.V_MedioPagoMatriculaCronogramaProveedor
                                WHERE IdMatriculaCabecera = @IdMatriculaCabecera
                                      AND Prioridad = 1 ";
                    var respuesta = _dapperRepository.QueryDapper(_query, new { IdMatriculaCabecera = id });
                    if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                    {
                        listaAreas = JsonConvert.DeserializeObject<IEnumerable<MedioPagoActivoPasarelaDTO>>(respuesta);
                    }
                }
                return listaAreas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Cesar Santillana
        /// Fecha: 21/06/2021
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data de proyectos presentados por el alumno, segun los filtros suministrados.
        /// </summary>
        /// <returns>Retorma una lista List<ProyectoPresentadoPorAlumnoDTO> </returns>
        public List<ProyectoPresentadoPorAlumnoDTO> GenerarReporteProyectoPresentadoPorAlumno(ProyectoPresentadoPorAlumnoFiltroDTO filtroReporte)
        {
            try
            {
                string programaEspecifico = null, docente = null, centroCosto = null, coordinadora = null, codigoMatricula = null;
                if (filtroReporte.ProgramaEspecifico != null && filtroReporte.ProgramaEspecifico.Count() > 0) programaEspecifico = String.Join(",", filtroReporte.ProgramaEspecifico);
                if (filtroReporte.Docente != null && filtroReporte.Docente.Count() > 0) docente = String.Join(",", filtroReporte.Docente);
                if (filtroReporte.CentroCosto != null && filtroReporte.CentroCosto.Count() > 0) centroCosto = String.Join(",", filtroReporte.CentroCosto);
                if (filtroReporte.Coordinadora != null && filtroReporte.Coordinadora.Count() > 0) coordinadora = String.Join(",", filtroReporte.Coordinadora);
                if (filtroReporte.CodigoMatricula != null && filtroReporte.CodigoMatricula != 0) codigoMatricula = String.Join(",", filtroReporte.CodigoMatricula);

                DateTime fechainicio = new DateTime(filtroReporte.FechaInicial.Year, filtroReporte.FechaInicial.Month, filtroReporte.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);
                List<ProyectoPresentadoPorAlumnoDTO> reporteProyectoPorAlumno = new List<ProyectoPresentadoPorAlumnoDTO>();
                var query = _dapperRepository.QuerySPDapper("[pla].[SP_ReporteProyectoPresentadoPorAlumno]", new { fechainicio, fechafin, programaEspecifico, centroCosto, docente, coordinadora, codigoMatricula });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteProyectoPorAlumno = JsonConvert.DeserializeObject<List<ProyectoPresentadoPorAlumnoDTO>>(query);
                    if (filtroReporte.EstadoRevision == 2)
                    {

                        reporteProyectoPorAlumno = reporteProyectoPorAlumno.Where(x => x.FechaCalificacion == null).ToList();

                    }
                    if (filtroReporte.EstadoRevision == 1)
                    {
                        reporteProyectoPorAlumno = reporteProyectoPorAlumno.Where(x => x.FechaCalificacion != null).ToList();

                    }
                }
                return reporteProyectoPorAlumno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
		/// Fecha: 27/07/2023
		/// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns> 
        public string? ObtenerCodigoCertificadoIrca(int idMatriculaCabecera)
        {
            try
            {
                string _query = "SELECT CodigoCertificado AS Valor FROM ope.V_ObtenerCodigoCertificadoIrca Where IdMatriculaCabecera = @IdMatriculaCabecera ";
                var resultado = _dapperRepository.FirstOrDefault(_query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<StringDTO>(resultado)!.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
		/// Fecha: 27/07/2023
		/// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idPespecifico"></param>
        /// <returns></returns> 
        public string? ObtenerFechaInicioCapacitacionModuloPespecifico(int idMatriculaCabecera, int idPespecifico)
        {
            try
            {
                string query = "SELECT FechaInicio as Valor FROM pw.V_PW_FechaInicioFinCapacitacionPortalWebModular WHERE IdMatriculaCabecera= @idMatriculaCabecera AND IdPespecifico=@idPespecifico";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera, idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    return JsonConvert.DeserializeObject<StringDTO>(resultado)!.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
		/// Fecha: 27/07/2023
		/// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idPespecifico"></param>
        /// <returns></returns> 
        public string? ObtenerFechaFinCapacitacionModuloPespecifico(int idMatriculaCabecera, int idPespecifico)
        {
            try
            {
                string query = "SELECT FechaFin  as Valor FROM pw.V_PW_FechaInicioFinCapacitacionPortalWebModular WHERE IdMatriculaCabecera= @idMatriculaCabecera AND IdPespecifico=@idPespecifico";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera, idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<StringDTO>(resultado)!.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
		/// Fecha: 27/07/2023
		/// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idCurso"></param>
        /// <returns></returns> 
        public string? ObtenerNotaPromedioModulo(int idMatriculaCabecera, int idCurso)
        {
            try
            {
                string query = "SELECT Nota AS Valor  FROM ope.V_ObtenerNotaPromedioModulo Where IdMatriculaCabecera = @idMatriculaCabecera and IdCurso =@idCurso";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera, idCurso });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    return JsonConvert.DeserializeObject<StringDTO>(resultado)!.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-05
        /// <summary>
        /// Obtiene el alumnos matriculados en un programa especifico y grupo
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <param name="grupo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ValorEnteroDTO> ObtenerAlumnosMatriculaProgramaEspecificoGrupo(int idPEspecifico, int grupo)
        {
            try
            {
                var lista = new List<ValorEnteroDTO>();
                var _query = "ope.SP_ObtenerOportunidadesAlumnosMatriculadosProgramaEspecificoGrupo";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { IdPEspecifico = idPEspecifico, Grupo = grupo });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ValorEnteroDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
		/// Fecha: 27/10/2023
		/// Version: 1.0
        /// <summary>
        /// Obtiene pgeneral por idmatricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public ProgramaGeneralMatriculaDTO ObtenerProgramaGeneralPorIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                ProgramaGeneralMatriculaDTO respuesta = new ProgramaGeneralMatriculaDTO();
                var query = @"SELECT Id,
                                     Nombre
                              FROM [ope].[V_ObtenerProgramaGeneralAlumno]
                              WHERE IdMatriculaCabecera = @idMatriculaCabecera
                                    AND Estado = 1;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<ProgramaGeneralMatriculaDTO>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor:Joseph LLanque 
        /// Fecha: 27/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el estado del matriculado por el IdAlumno
        /// </summary>
        /// <param name="idAlumno"> Id del Alumno</param>
        /// <returns>List<EstadoMatriculadoDTO> </returns>
        public IdMatriculaCelularDTO ObtenerIdMatriculaPorCelular(string celular)
        {
            try
            {
                IdMatriculaCelularDTO respuesta = new IdMatriculaCelularDTO();
                var _query = "ope.SP_ObtenerMatriculaCabeceraPorCelular";
                var datoAlumno = _dapperRepository.QuerySPFirstOrDefault(_query, new { celular });
                if (!string.IsNullOrEmpty(datoAlumno) && !datoAlumno.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<IdMatriculaCelularDTO>(datoAlumno);
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 19/03/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el estado del matriculado por el IdAlumno
        /// </summary>
        /// <param name="idAlumno"> Id del Alumno</param>
        /// <returns>List<EstadoMatriculadoDTO> </returns>
        public List<MatriculaAlumnoDTO> ObtenerMatriculaAlumno(int idAlumno)
        {
            try
            {
                var estadoMatriculado = new List<MatriculaAlumnoDTO>();
                var _query = "ope.SP_ObtenerMatriculaAlumno";
                var pEspecificoDB = _dapperRepository.QuerySPDapper(_query, new { idAlumno });
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<MatriculaAlumnoDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
		/// Fecha: 27/10/2023
		/// Version: 1.0
        /// <summary>
        /// Obtiene versiones de programa disponibles para la matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<VersionMatriculaDisponibleDTO> ObtenerVersionDisponibleMatricula(int idMatriculaCabecera)
        {
            try
            {
                List<VersionMatriculaDisponibleDTO> respuesta = new List<VersionMatriculaDisponibleDTO>();
                var query = @"SELECT Id,
                                     IdVersion,
                                     Nombre
                              FROM [ope].[V_VersionDisponibleMatricula]
                              WHERE Id = @idMatriculaCabecera";
                var resultado = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<VersionMatriculaDisponibleDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
		/// Fecha: 27/10/2023
		/// Version: 1.0
        /// <summary>
        /// Obtiene pgeneral por idmatricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public VersionMatriculaDTO ObtenerVersionMatricula(int idMatriculaCabecera)
        {
            try
            {
                VersionMatriculaDTO respuesta = new VersionMatriculaDTO();
                var query = @"SELECT Id,
                                     IdVersion,
                                     Version
                              FROM [ope].[V_VersionMatriculaCabecera]
                              WHERE Id = @idMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<VersionMatriculaDTO>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}