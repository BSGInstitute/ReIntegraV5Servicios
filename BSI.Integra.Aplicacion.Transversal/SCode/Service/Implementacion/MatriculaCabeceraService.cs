using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: MatriculaCabeceraService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión general de T_MatriculaCabecera
    /// </summary>
    public class MatriculaCabeceraService : IMatriculaCabeceraService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MatriculaCabeceraService(IUnitOfWork unitOfWork)

        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMatriculaCabecera, MatriculaCabecera>(MemberList.None).ReverseMap();
                cfg.CreateMap<MatriculaCabeceraDTO, MatriculaCabecera>(MemberList.None).ReverseMap();
                cfg.CreateMap<MatriculaCabeceraDTO, TMatriculaCabecera>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public MatriculaCabecera Add(MatriculaCabecera entidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MatriculaCabecera>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MatriculaCabecera Update(MatriculaCabecera entidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MatriculaCabecera>(modelo);
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
                _unitOfWork.MatriculaCabeceraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MatriculaCabecera> Add(List<MatriculaCabecera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MatriculaCabecera>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MatriculaCabecera> Update(List<MatriculaCabecera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MatriculaCabecera>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.MatriculaCabeceraRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea de un MatriculaCabeceraDTO a MatriculaCabecera
        /// </summary>
        /// <returns> Oportunidad </returns>
        public MatriculaCabecera MapeoEntidadDesdeDTO(MatriculaCabeceraDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<MatriculaCabecera>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MatriculaCabecera
        /// </summary>
        /// <returns> List<MatriculaCabeceraDTO> </returns>
        public IEnumerable<MatriculaCabeceraDTO> ObtenerMatriculaCabecera()
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerMatriculaCabecera();
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
        /// <param name="id">Id del Alumno</param>
        /// <returns> List<MatriculaCabeceraDTO> </returns>
        public MatriculaCabecera ObtenerMatriculaCabeceraPorId(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerMatriculaCabeceraPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MatriculaCabecera para mostrarse en combo.
        /// </summary>
        /// <returns> List<MatriculaCabeceraComboDTO> </returns>
        public IEnumerable<MatriculaCabeceraComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCombo();
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
        /// Obtiene el Id de la Matricula Cabecera asociado al Alumno y al Centro de Costo.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> ValorIntDTO </returns>
        public int ObtenerIdMatriculaCabeceraPorAlumnoCentroCosto(int idAlumno, int idCentroCosto)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerIdMatriculaCabeceraPorAlumnoCentroCosto(idAlumno, idCentroCosto);
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
        /// <returns>InformacionMatriculaCabeceraDTO</returns>
        public InformacionMatriculaCabeceraDTO ObtenerInformacionMatriculaCabeceraPorIdCronograma(int idCronograma)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerInformacionMatriculaCabeceraPorIdCronograma(idCronograma);
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
        /// Obtener el programa especifico del Alumno 
        /// </summary>
        /// <param name="codigoMatricula">Codigo Matricula </param>
        /// <returns>Retorna respuesta: int</returns> 
        public int ObtenerAlumnoProgramaEspecifico(string codigoMatricula)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerAlumnoProgramaEspecifico(codigoMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///Autor: Gilmer Quispe
        ///Fecha: 01/09/2022
        ////// Version: 1.0
        /// <summary>
        /// Obtener Programa General por Programa Especifico
        /// </summary>
        /// <param name="pespecifico">Id Programa Especifico </param>
        /// <returns> Retorna Respuesta: int</returns> 
        public int ObtenerProgramaGeneral(int pespecifico)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerProgramaGeneral(pespecifico);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerFechaFinalizacion(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///Autor: Gilmer Quispe
        ///Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza El estado de La matricula Por idMatriculaCabecera, idEstadoMatricula y codigoMatricula
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id de ña matricula cabecera</param>
        /// <param name="idEstadoMatricula"> Id del estado de la matricula</param>
        /// <param name="codigoMatricula"> Codigo de la matricula</param>
        /// <returns>True o False</returns
        public bool ActualizarEstadoMatricula(int idMatriculaCabecera, int idEstadoMatricula, string codigoMatricula)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ActualizarEstadoMatricula(idMatriculaCabecera, idEstadoMatricula, codigoMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
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
        public List<EstadoMatriculadoDTO> EstadoMatriculadoPorAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerEstadoMatriculadoPorAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerMatriculaAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
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
        public List<MatriculaDTO> MatriculaPorAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerMatriculaPorAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BeneficioSolicitadoReporteDTO> ObtenerTodoBeneficioSolicitado(FiltroBeneficiosSolicitadosPorAlumnos FiltroReporteSolcitud)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerTodoBeneficioSolicitado(FiltroReporteSolcitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DatoAdicionalPWDTO> ObtenerDatosAdicionalesPorCodigo(int idMatriculaCabeceraBeneficios)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerDatosAdicionalesPorCodigo(idMatriculaCabeceraBeneficios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EntregarBeneficio(int idMatriculaCabeceraBeneficio, string usuario)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.EntregarBeneficio(idMatriculaCabeceraBeneficio, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
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
        public EstadoMatriculadoDTO EstadoEvaluacionPorCodMatricula(string codigoMatricula)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerEstadoEvaluacionPorCodMatricula(codigoMatricula);
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
        /// Actualiza usuario coordinador academico de matricula cabecera en v3
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>s
        public bool ActualizarTMatriculaCabecera(string codigoMatricula, string usuario)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ActualizarTMatriculaCabecera(codigoMatricula, usuario);
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
        /// Obtiene registro de Matricula Cabecera por idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public MatriculaCabeceraDTO ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerPorIdMatriculaCabecera(idMatriculaCabecera);
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
        /// Obtiene en un objeto del tipo DetalleOportunidadOperacionesDTO los detalles de matriculas
        /// </summary>
        /// <returns>Objeto del tipo DetalleOportunidadOperacionesDTO</returns>
        public DetalleOportunidadOperacionesDTO ObtenerDetalleMatricula(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleMatricula(id);
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
        /// Obtiene la url de confirmacion de participacion webinar en base a dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias a analizar para obtener la URL</param>
        /// <returns>Cadena con la URL de confirmacion de los participantes de la sesion webinar</returns>
        public string ObtenerUrlConfirmacionParticipacionSesionWebinar(int id, int cantidadDias)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerUrlConfirmacionParticipacionSesionWebinar(id, cantidadDias);
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
        /// Obtiene la url de confirmacion de participacion webinar en base a dias
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias a analizar para obtener la URL</param>
        /// <returns>Cadena con la descripcion y fecha del trabajo a presentar</returns>
        public string ObtenerPresentacionTrabajoNDias(int id, int cantidadDias)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerPresentacionTrabajoNDias(id, cantidadDias);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerPresentacionTrabajoFinalNDias(id, cantidadDias);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerSesionesWebinarNDias(id, cantidadDias, mostrarUrlAcceso);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerSesionesWebinarConfirmadasNDias(id, cantidadDias, mostrarUrlAcceso);
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
        /// <param name="id">Id de la matricula cabecera que la cual se dese obtener las sesiones webinar confirmadas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias a partir del actual del que se desea obtener las sesiones webinar confirmadas</param>
        /// <returns>Lista de objetos (SesionWebinarDTO)</returns>
        public List<SesionWebinarDTO> ObtenerSesionesConfirmadasWebinarNDias(int id, int cantidadDias)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerSesionesConfirmadasWebinarNDias(id, cantidadDias);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCronogramaAutoEvaluacionCompleto(id);
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
        /// Obtiene las AutoEvaluaciones vencidas en html
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con las autoevaluaciones vencidas</returns>
        public string ObtenerAutoEvaluacionesVencidas(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerAutoEvaluacionesVencidas(id);
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
        /// Obtiene las AutoEvaluaciones completas en html
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las autoevaluaciones completas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con las autoevaluaciones completas</returns>
        public string ObtenerAutoEvaluacionesCompletas(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerAutoEvaluacionesCompletas(id);
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
        /// Cantidad de autoevaluaciones pendientes
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de autoevaluaciones pendientes por el alumno</returns>
        public int ObtenerCantidadAutoEvaluacionesPendientes(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCantidadAutoEvaluacionesPendientes(id);
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
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener el detalle de las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el dia actual</param>
        /// <param name="esFechaExacta">Flag para determinar si es una fecha exacta lo ingresado</param>
        /// <returns>Lista de objetos (AutoEvaluacionCronogramaDetalleDTO)</returns>
        public List<AutoEvaluacionCronogramaDetalleDTO> ObtenerDetalleAutoEvaluacionesVencidaDiaExacto(int id, int cantidadDias, bool esFechaExacta)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleAutoEvaluacionesVencidaDiaExacto(id, cantidadDias, esFechaExacta);
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
        /// Obtiene las proxima fecha de autoevaluacion
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener las autoevaluaciones vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el dia actual</param>
        /// <returns>Retorna objeto del tipo AutoEvaluacionCronogramaDetalleDTO</returns>
        public AutoEvaluacionCronogramaDetalleDTO ObtenerDetalleProximaAutoEvaluacion(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleProximaAutoEvaluacion(id);
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
        /// Obtiene la cantidad de autoevaluaciones vencidas
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de autoevaluaciones vencidas</returns>
        public int ObtenerCantidadAutoEvaluacionesVencidas(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCantidadAutoEvaluacionesVencidas(id);
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
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener el cronograma de pago completo (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="formatoHTMLMostrar">Enumerator de tipo FormatoHTMLMostrar, mostrando la lista y la tabla</param>
        /// <returns>Cadena formateada con el cronograma de pagos completo</returns>
        public string ObtenerCronogramaPagoCompleto(int id, FormatoHTMLMostrar formatoHTMLMostrar)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCronogramaPagoCompleto(id, formatoHTMLMostrar);
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
        /// Obtiene el monto total del programa en el formato Simbolo +" " + MontoTotal +" "+ NombrePlural
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener el monto total (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con el monto y ell simbolo de la moneda</returns>
        public string ObtenerMontoTotal(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerMontoTotal(id);
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
        /// <param name="id">Id de la matricula cabecera a la cual se desea obtener el cronograma de pago completo de cuotas vencidas (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada de las cuotas de pago vencidas</returns>
        public string ObtenerCronogramaPagoCompletoCuotasVencidas(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCronogramaPagoCompletoCuotasVencidas(id);
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
        /// Cantidad de cuotas pendientes
        /// </summary>
        /// <param name="id">Id de la matricula cabecera de la cual se desea obtener la cantidad de cuotas pendientes (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de cuotas pendientes</returns>
        public int ObtenerCantidadCuotasPendientes(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCantidadCuotasPendientes(id);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCuotasVencidas(id, cantidadDias, esFechaExacta, idPlantillaBase);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCantidadCuotasVencidas(id, cantidadDias, esFechaExacta, idPlantillaBase);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleCuotasVencidas(id, cantidadDias, esFechaExacta);
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
        /// Obtiene las proxima cuota
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabeceras)</param>
        /// <returns>Objeto del tipo CuotaCronogramaDetalleDTO</returns>
        public CuotaCronogramaDetalleDTO ObtenerDetalleProximaCuota(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleProximaCuota(id);
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
        /// Obtiene el descuento de cuotas pendientes acorde a un porcentaje dado
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="porcentaje">Porcentaje dado</param>
        /// <returns>Cadena formateada con el descuento de cuotas pendientes</returns>
        public string ObtenerDescuentoCuotasPendientesPorPorcentaje(int idMatriculaCabecera, decimal porcentaje)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerDescuentoCuotasPendientesPorPorcentaje(idMatriculaCabecera, porcentaje);
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
        /// Obtener detalle acceso aula virtual
        /// </summary>
        /// <param name="id">Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Objeto del tipo DetalleCursoActualAulaVirtualDTO</returns>
        public List<DetalleCursoActualAulaVirtualDTO> ObtenerCursoActualAlumnoMoodle(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCursoActualAlumnoMoodle(id);
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
        /// Obtener detalle acceso aula virtual
        /// </summary>
        /// <param name="id">Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Objeto del tipo DetalleAccesoAulaVirtualDTO</returns>
        public DetalleAccesoAulaVirtualDTO ObtenerDetalleAccesoAulaVirtual(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleAccesoAulaVirtual(id);
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
        /// Obtiene el acceso del portal web
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DetalleAccesoPortalWebDTO ObtenerDetalleAccesoDocentePortalWeb(int idProveedor)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleAccesoDocentePortalWeb(idProveedor);
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
        /// Obtiene el acceso del portal web
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DetalleAccesoPortalWebDTO ObtenerDetalleAccesoPortalWeb(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleAccesoPortalWeb(id);
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
        /// Obtiene las siguiente sesion
        /// </summary>
        /// <param name="id">Id del pespecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="cantidadDias">Cantidad de dias a partir del dia de hoy</param>
        /// <returns>Entero que retorna la proxima sesion</returns>
        public int ObtenerProximaSesion(int id, int cantidadDias)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerProximaSesion(id, cantidadDias);
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
        /// Materiales de cada PEspecifico
        /// </summary>
        /// <param name="listaIdMaterialPEspecificoDetalle">Lista de entero de IdMaterialPEspecificoDetalle</param>
        /// <returns>Cadena formateada de los materiales</returns>
        public string ObtenerMaterialesPorMaterialPEspecificoDetalle(List<int> listaIdMaterialPEspecificoDetalle)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerMaterialesPorMaterialPEspecificoDetalle(listaIdMaterialPEspecificoDetalle);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerUrlSesionesWebinarNDias(id, cantidadDias);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerAutoEvaluacionesVencidasDiasExacto(id, cantidadDias, esFechaExacta, idPlantillaBase);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleAutoEvaluacionesVencidas(id, cantidadDias, esFechaExacta);
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
        /// Obtiene la cantidad de cuotas vencidas
        /// </summary>
        /// <param name="id">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Entero con la cantidad de cuotas vencidas</returns>
        public int ObtenerTodoCantidadCuotasVencidas(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerTodoCantidadCuotasVencidas(id);
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
        /// <param name="idMaterialPEspecificoDetalle">Id del Material de un PEspecifico (PK de la tabla ope.T_MaterialPEspecificoDetalle)</param>
        /// <returns>Cadena formateada de los materiales de un PEspecifico</returns>
        public string ObtenerUrlMaterialesPorMaterialPEspecificoDetalle(int idMaterialPEspecificoDetalle)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerUrlMaterialesPorMaterialPEspecificoDetalle(idMaterialPEspecificoDetalle);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerRegistrosParaActualizar(idSolicitudOperaciones);
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
                return _unitOfWork.MatriculaCabeceraRepository.ActualizarCentroCosto(solicitudOperacion);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerRegistrosParaActualizarVersion(idSolicitudOperaciones);
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
                return _unitOfWork.MatriculaCabeceraRepository.EliminarBeneficiosMatriculaCabeceraIdMatricula(idMatriculaCabecera);
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
        /// <param name="nuevoPaquete"></param>
        /// <param name="idCronograma"></param>
        /// <returns></returns>
        /// <exception>StringDTO</exception>
        public StringDTO InsertarBeneficiosMatriculaCabeceraIdMatricula(int idMatriculaCabecera, int nuevoPaquete, int idCronograma)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.InsertarBeneficiosMatriculaCabeceraIdMatricula(idMatriculaCabecera, nuevoPaquete, idCronograma);
            }
            catch (Exception e)
            {
                throw e;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerSubEstadoMatriculaConfiguracionCoordinadora();
            }
            catch (Exception e)
            {
                throw e;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerSubEstadoMatricula();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        ///Autor: Gilmer Quispe
        ///Fecha: 09/11/2022
        /// <summary>
        /// Obtener Beneficio Solicitado Por Matricula
        /// </summary>
        /// <param name="codigoMatricula"> Codigo Matricula </param>
        /// <returns> Lista de beneficios solicitados: List<InformacionBeneficioSolicitadoDTO></returns> 
        public List<InformacionBeneficioSolicitadoDTO> ObtenerBeneficiosSolicitadosPorMatricula(string codigoMatricula)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerBeneficiosSolicitadosPorMatricula(codigoMatricula);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        ///Autor: Gilmer Quispe
        ///Fecha: 09/11/2022
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerBeneficiosCongeladosPorMatricula(codigoMatricula);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 09/11/2022
        /// <summary>
        /// Obtener Estado Programa General del Beneficio
        /// </summary>
        /// <param name="pPeneral">Id Programa General </param>
        /// <returns> Lista Estado Matricula: List<MatriculaCabeceraComboDTO> </returns> 
        public List<MatriculaCabeceraComboDTO> ObtenerEstadoPgeneralBeneficio(int idPGeneral)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerEstadoPgeneralBeneficio(idPGeneral);
            }
            catch (Exception e)
            {
                throw e;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerSubEstadoPgeneralBeneficio(idPGeneral);
            }
            catch (Exception e)
            {
                throw e;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerEstadoAlumno(codigoMatricula);
            }
            catch (Exception e)
            {
                throw e;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerSubestadoAlumno(codigoMatricula);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Beneficios por Código de Matrícula
        /// </summary>
        /// <param name="codigoMatricula"> Codigo Matricula </param>
        /// <returns> Beneficios por CodigoMatricula </returns>
        /// <returns> Objeto DTO : CorrespondeBeneficiosDTO </returns>
        public CorrespondeBeneficiosDTO ObtenerBeneficiosPorMatricula(string codigoMatricula)
        {
            try
            {
                var corresponde = true;
                var beneficios = new CorrespondeBeneficiosDTO();
                var beneficiosmatricula = ObtenerBeneficiosCongeladosPorMatricula(codigoMatricula);
                var pespecifico = ObtenerAlumnoProgramaEspecifico(codigoMatricula);
                var idPGeneral = ObtenerProgramaGeneral(pespecifico);
                var listestado = ObtenerEstadoPgeneralBeneficio(idPGeneral);
                var listsubestado = ObtenerSubEstadoPgeneralBeneficio(idPGeneral);
                var estadoalumno = ObtenerEstadoAlumno(codigoMatricula);
                var subestadoalumno = ObtenerSubestadoAlumno(codigoMatricula);
                foreach (var item in listestado)
                {
                    if (item.Id == estadoalumno) { corresponde = true; break; }
                }
                if (estadoalumno == 1 && subestadoalumno == 0) corresponde = false;
                else
                {
                    foreach (var item in listsubestado)
                    {
                        if (item.Id == subestadoalumno) { corresponde = false; break; }
                    }
                }
                beneficios.beneficios = beneficiosmatricula;
                beneficios.corresponde = corresponde;
                return (beneficios);
            }
            catch (Exception e)
            {
                throw e;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCursoMoodle(codigoMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCostosAdministrativos(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
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

                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCodigoMatriculaPEspecificoPorAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerIdentificadoresMatriculaComboPorAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerAlumnoProgramaEspecificoLista(idCabeceraMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.MatriculaCabeceraRepository.CodigoMatriculaPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerMatriculaPorOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCodigoMatriculaAutocompleto(nombre);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 24/04/2024
        /// Version: 1.0
        /// <summary>
        /// Permite obtener los Id del programa especifico, nombre completo del programa especifico enviandole como parametro 
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns> List<MatriculaCabeceraComboDTO> </returns>
        public IdMatriculaCelularDTO ObtenerIdMatriculaPorCelular(string celular)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerIdMatriculaPorCelular(celular);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/12/2022
        /// Version: 1.0
        /// <summary>
        /// modifica el gestor de la tabla T_CronogramaPagoDetalleFinal
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="usuario"> Usuario Responsable </param>
        /// <param name="gestor"> Nuevo gestor</param>
        /// <param name="idMatriculaCabecera"> Id de la matricula</param>
        /// <returns> int </returns>
        public int ModificarGestorDeCobranza(string usuario, string gestor, int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ModificarGestorDeCobranza(usuario, gestor, idMatriculaCabecera);
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
        /// Actualiza El estado de La matricula Por IdSolicitud
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <param name="nombreEstado"></param>
        /// <returns> bool </returns>
        public bool ActualizarEstadoMatriculaPorSolicitud(int idSolicitudOperaciones, string nombreEstado)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ActualizarEstadoMatriculaPorSolicitud(idSolicitudOperaciones, nombreEstado);
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
        /// Actualiza El estado de La matricula Por IdSolicitud
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <param name="nombreSubEstado"></param>
        /// <returns> bool </returns>
        public bool ActualizarSubEstadoMatriculaPorSolicitud(int idSolicitudOperaciones, string nombreSubEstado)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ActualizarSubEstadoMatriculaPorSolicitud(idSolicitudOperaciones, nombreSubEstado);
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
        /// Obtiene todos los registros de T_MatriculaCabecera.
        /// </summary>
        /// <returns> Entidad: MatriculaCabecera> </returns>
        public MatriculaCabecera ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerPorId(id);
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerIdAlumnoCoordinadorAcademico(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerPorCodigoMatricula(codigoMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/12/2022
        /// Version: 1.0
        /// <summary>
        /// Determina si una Plantilla Existe basado en su identificador
        /// </summary>
        /// <param name="id">Id de la Matricula Cabecera </param>
        /// <returns> bool </returns>
        public bool ExistePorId(int id)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.Exist(id);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerAlumnosMatriculados(codMatricula, dni);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Joseph Llanque
        /// Fecha: 27/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene programa general por idmatricula
        /// </summary>
        /// <param name="codMatricula"> idMatricula </param>
        /// <returns> List<FiltroMatriculaAlumnoDTO> </returns>
        public ProgramaGeneralMatriculaDTO ObtenerProgramaGeneralPorIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerProgramaGeneralPorIdMatricula(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerEstadosMatricula();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NuevoCompromisoAlumnoDTO GuardarFechaCompromiso(NuevoCompromisoAlumnoDTO nuevoCompromisoAlumnoDTO)
        {
            try
            {
                var compromisoAlumnoService = new CompromisoAlumnoService(_unitOfWork);
                var compromisoAlumno = new CompromisoAlumno();
                var fecha = Convert.ToDateTime(nuevoCompromisoAlumnoDTO.FechaCompromiso);
                var fechaActualizada = fecha.AddHours(-5);
                DateTime fechaActual = DateTime.Now;
                compromisoAlumno.IdCronogramaPagoDetalleFinal = nuevoCompromisoAlumnoDTO.Id;
                compromisoAlumno.FechaCompromiso = fechaActualizada;
                compromisoAlumno.FechaGeneracionCompromiso = fechaActual;
                compromisoAlumno.Monto = nuevoCompromisoAlumnoDTO.MontoCompromiso.Value;
                compromisoAlumno.IdMoneda = nuevoCompromisoAlumnoDTO.IdMoneda;
                compromisoAlumno.Version = nuevoCompromisoAlumnoDTO.Version.Value;
                compromisoAlumno.Estado = true;
                compromisoAlumno.UsuarioCreacion = nuevoCompromisoAlumnoDTO.Usuario;
                compromisoAlumno.UsuarioModificacion = nuevoCompromisoAlumnoDTO.Usuario;
                compromisoAlumno.FechaCreacion = fechaActual;
                compromisoAlumno.FechaModificacion = fechaActual;
                var nuevoCompromisoAlumno = compromisoAlumnoService.Add(compromisoAlumno);
                if (nuevoCompromisoAlumno.Id > 0)
                {
                    nuevoCompromisoAlumnoDTO.Flag = true;
                }
                else
                {
                    nuevoCompromisoAlumnoDTO.Flag = false;
                }
                return nuevoCompromisoAlumnoDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// genera la matricula y cronograma
        /// </summary>
        /// <returns>< object </returns>
        public object GenerarMatriculaCabecera(MatriculaCronogramaDTO MatriculaCronogramaDTO)
        {
            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var _repPespecificoRepositorio = _unitOfWork.PEspecificoRepository;
                var _repMatriculaDetalle = _unitOfWork.MatriculaDetalleRepository;
                var _repCronogramaPagoDetalle = _unitOfWork.CronogramaPagoDetalleRepository;
                var _repControlDocAlumno = _unitOfWork.ControlDocAlumnoRepository;
                var _repCronogramaPago = _unitOfWork.CronogramaPagoRepository;
                var _repControlDocumento = _unitOfWork.ControlDocRepository;
                var _repMoneda = _unitOfWork.MonedaRepository;
                var _repTipoCambio = _unitOfWork.TipoCambioRepository;
                var _repTipoCambioCol = _unitOfWork.TipoCambioColRepository;
                var _repTipoCambioMoneda = _unitOfWork.TipoCambioMonedumRepository;
                var _repPEspecificoMatriculaAlumno = _unitOfWork.PEspecificoMatriculaAlumnoRepository;

                List<PespecificoPadrePespecificoHijoDTO> listaPEspecificoPadrePespecificoHijo = new List<PespecificoPadrePespecificoHijoDTO>();

                using (TransactionScope scope = new TransactionScope())
                {

                    var listRpta = "";
                    bool CadaNDias = false;
                    int DiaPago = MatriculaCronogramaDTO.FechaInicioPago.Day;
                    var existe = _repMatriculaCabecera.ExisteMatriculaCabecera(MatriculaCronogramaDTO.IdAlumno, MatriculaCronogramaDTO.IdPespecifico);
                    var codigoMatricula = string.Empty;
                    int idMatriculaCabecera = 0;

                    if (existe)
                    {
                        codigoMatricula = "0";
                    }
                    else
                    {
                        var codigoBanco = _repPespecificoRepositorio.FirstBy(x => x.Id == MatriculaCronogramaDTO.IdPespecifico, x => new { x.CodigoBanco }).CodigoBanco;

                        MatriculaCabecera matriculaCabecera = new MatriculaCabecera()
                        {
                            CodigoMatricula = string.Concat(MatriculaCronogramaDTO.IdAlumno, codigoBanco),
                            IdAlumno = MatriculaCronogramaDTO.IdAlumno,
                            IdPespecifico = MatriculaCronogramaDTO.IdPespecifico,
                            UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                            IdEstadoPagoMatricula = ValorEstatico.IdEstadoPagoMatriculaPorMatricular,
                            EstadoMatricula = "pormatricular",
                            FechaMatricula = DateTime.Now,
                            IdCoordinador = MatriculaCronogramaDTO.IdCoordinador,
                            IdAsesor = MatriculaCronogramaDTO.IdAsesor,
                            IdEstadoMatricula = 1,
                            UsuarioCoordinadorAcademico = "0",
                            ObservacionGeneralOperaciones = "",
                            IdPaquete = 0
                        };
                        matriculaCabecera = this.Add(matriculaCabecera);
                        codigoMatricula = matriculaCabecera.CodigoMatricula;
                        idMatriculaCabecera = matriculaCabecera.Id;

                        //Verificar si es hijo o si es padre
                        listaPEspecificoPadrePespecificoHijo = _repPEspecificoMatriculaAlumno.ListaPespecificoPadrePespecificoHijo(MatriculaCronogramaDTO.IdPespecifico);

                        var IdUsuarioMoodle = _repPEspecificoMatriculaAlumno.IdUsuarioMoodle(MatriculaCronogramaDTO.IdAlumno);
                        //
                        if (listaPEspecificoPadrePespecificoHijo.Count > 0)
                        {

                            foreach (var item in listaPEspecificoPadrePespecificoHijo)
                            {
                                //IdCursoMoodle
                                var IdCursoMoodle = _repPEspecificoMatriculaAlumno.IdCursoMoodle(item.PEspecificoHijoId);

                                TPespecificoMatriculaAlumno matriculaBO = new TPespecificoMatriculaAlumno();
                                matriculaBO.IdMatriculaCabecera = idMatriculaCabecera;
                                matriculaBO.IdPespecifico = item.PEspecificoHijoId;
                                matriculaBO.IdPespecificoTipoMatricula = 1;
                                matriculaBO.IdCursoMoodle = IdCursoMoodle;
                                matriculaBO.IdUsuarioMoodle = IdUsuarioMoodle;
                                matriculaBO.Grupo = 1;
                                matriculaBO.AplicaNuevaAulaVirtual = _repPEspecificoMatriculaAlumno.ExisteNuevaAulaVirtual(item.PEspecificoHijoId);
                                matriculaBO.UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario;
                                matriculaBO.FechaCreacion = DateTime.Now;
                                matriculaBO.UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario;
                                matriculaBO.FechaModificacion = DateTime.Now;
                                matriculaBO.Estado = true;

                                _repPEspecificoMatriculaAlumno.Insert(matriculaBO);
                                _unitOfWork.Commit();
                            }

                        }
                        else
                        {
                            //IdCursoMoodle
                            var IdCursoMoodle = _repPEspecificoMatriculaAlumno.IdCursoMoodle(MatriculaCronogramaDTO.IdPespecifico);

                            TPespecificoMatriculaAlumno matriculaBO = new TPespecificoMatriculaAlumno();
                            matriculaBO.IdMatriculaCabecera = idMatriculaCabecera;
                            matriculaBO.IdPespecifico = MatriculaCronogramaDTO.IdPespecifico;
                            matriculaBO.IdPespecificoTipoMatricula = 1;
                            matriculaBO.IdCursoMoodle = IdCursoMoodle;
                            matriculaBO.IdUsuarioMoodle = IdUsuarioMoodle;
                            matriculaBO.Grupo = 1;
                            matriculaBO.AplicaNuevaAulaVirtual = _repPEspecificoMatriculaAlumno.ExisteNuevaAulaVirtual(MatriculaCronogramaDTO.IdPespecifico);
                            matriculaBO.UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario;
                            matriculaBO.FechaCreacion = DateTime.Now;
                            matriculaBO.UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario;
                            matriculaBO.FechaModificacion = DateTime.Now;
                            matriculaBO.Estado = true;
                            _repPEspecificoMatriculaAlumno.Insert(matriculaBO);
                            _unitOfWork.Commit();
                        }
                    }

                    if (codigoMatricula == "0")
                    {
                        listRpta = "0";
                    }
                    else
                    {
                        foreach (var item in MatriculaCronogramaDTO.CursosMatriculados)
                        {
                            TMatriculaDetalle matriculaDetalleBO = new TMatriculaDetalle()
                            {
                                IdMatriculaCabecera = idMatriculaCabecera,
                                IdCursoPespecifico = item,
                                UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaModificacion = DateTime.Now,
                                Estado = true,
                            };
                            _repMatriculaDetalle.Insert(matriculaDetalleBO);
                            _unitOfWork.Commit();
                        }

                        CadaNDias = MatriculaCronogramaDTO.OpcionPagoNDias;


                        //Generacion de campos WEB para el portal web
                        var moneda = _repMoneda.FirstBy(x => x.Id.Equals(MatriculaCronogramaDTO.IdMoneda));

                        var webMoneda = moneda.DigitoFinanzas;
                        var tipoCambioGeneral = _repTipoCambioMoneda.ObtenerTasaCambioMoneda(moneda.Id);
                        var webTotalPagar = MatriculaCronogramaDTO.TotalPagar;

                        CronogramaPago cronogramaPagoBO = new CronogramaPago()
                        {
                            IdMatriculaCabecera = idMatriculaCabecera,
                            IdAlumno = MatriculaCronogramaDTO.IdAlumno,
                            IdPespecifico = MatriculaCronogramaDTO.IdPespecifico,
                            Periodo = MatriculaCronogramaDTO.Periodo,
                            Moneda = (moneda.NombrePlural).ToLower(),
                            AcuerdoPago = MatriculaCronogramaDTO.AcuerdoPago,
                            TipoCambio = MatriculaCronogramaDTO.TipoCambio,
                            TotalPagar = MatriculaCronogramaDTO.TotalPagar,
                            NroCuotas = MatriculaCronogramaDTO.NroCuotas,
                            FechaIniPago = MatriculaCronogramaDTO.FechaInicioPago,
                            ConCuotaInicial = false,
                            CuotaInicial = 0,
                            CadaNdias = CadaNDias,
                            Ndias = MatriculaCronogramaDTO.Ndias,
                            UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                            WebMoneda = webMoneda.ToString(),
                            WebTipoCambio = tipoCambioGeneral.Cambio,
                            WebTotalPagar = webTotalPagar,
                            WebTotalPagarConv = webTotalPagar

                        };

                        cronogramaPagoBO = new CronogramaPagoService(_unitOfWork).Add(cronogramaPagoBO);

                        double MontoCuota = MatriculaCronogramaDTO.TotalPagar / MatriculaCronogramaDTO.NroCuotas;
                        MontoCuota = Math.Round(MontoCuota, 2);
                        double Saldo = MatriculaCronogramaDTO.TotalPagar;
                        for (int x = 1; x <= MatriculaCronogramaDTO.NroCuotas; x++)
                        {
                            double Total = Saldo;
                            Saldo = Total - MontoCuota;

                            if (CadaNDias == true)
                            {
                                MatriculaCronogramaDTO.FechaInicioPago = MatriculaCronogramaDTO.FechaInicioPago.AddDays(Convert.ToDouble(MatriculaCronogramaDTO.Ndias));
                            }

                            TCronogramaPagoDetalle cronogramaPagoDetalleBO = new TCronogramaPagoDetalle()
                            {
                                IdMatriculaCabecera = idMatriculaCabecera,
                                NroCuota = x,
                                FechaVencimiento = MatriculaCronogramaDTO.FechaInicioPago,
                                TotalPagar = Convert.ToDecimal(Total),
                                Cuota = Convert.ToDecimal(MontoCuota),
                                Saldo = Convert.ToDecimal(Saldo),
                                TipoCuota = "CUOTA",
                                Moneda = (moneda.NombrePlural).ToLower(),
                                UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaModificacion = DateTime.Now,
                                Cancelado = false,
                                Estado = true
                            };

                            _repCronogramaPagoDetalle.Insert(cronogramaPagoDetalleBO);
                            _unitOfWork.Commit();

                            if (CadaNDias == false)
                            {
                                try
                                {
                                    DateTime FechaPago = new DateTime(MatriculaCronogramaDTO.FechaInicioPago.Year, MatriculaCronogramaDTO.FechaInicioPago.Month + 1, DiaPago);
                                    MatriculaCronogramaDTO.FechaInicioPago = FechaPago;
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    MatriculaCronogramaDTO.FechaInicioPago = MatriculaCronogramaDTO.FechaInicioPago.AddMonths(1);
                                }
                            }
                        }

                        TControlDocAlumno controlDocAlumnoBO = new TControlDocAlumno()
                        {
                            IdMatriculaCabecera = idMatriculaCabecera,
                            IdCriterioCalificacion = 0,
                            QuienEntrego = "",
                            FechaEntregaDocumento = null,
                            Observaciones = "",
                            ComisionableEditable = "Ninguno",
                            MontoComisionable = 0,
                            ObservacionesComisionable = "",
                            PagadoComisionable = 0,
                            UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        _repControlDocAlumno.Insert(controlDocAlumnoBO);
                        _unitOfWork.Commit();
                        foreach (var item in MatriculaCronogramaDTO.ListaIdDocumento)
                        {
                            TControlDoc controlDocBO = new TControlDoc()
                            {
                                IdMatriculaCabecera = idMatriculaCabecera,
                                IdCriterioDoc = item,
                                EstadoDocumento = true,
                                UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            };
                            _repControlDocumento.Insert(controlDocBO);
                            _unitOfWork.Commit();
                        }
                        listRpta = codigoMatricula;
                    }
                    scope.Complete();
                    return new { listRpta };
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// obtiene el 
        /// </summary>
        /// <returns>< object </returns>
        public int ObtenerCronogramaBusqueda(string CodigoMatricula)
        {
            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var _repCronogramaPagoDetalleOriginal = _unitOfWork.CronogramaPagoDetalleOriginalRepository;
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                //obtenemos el idMatriculaCabecera
                var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id });
                var existe = _repMatriculaCabecera.GetBy(x => x.Id == matriculaCabecera.Id).Count();
                var count = _repCronogramaPagoDetalleOriginal.GetBy(x => x.IdMatriculaCabecera == matriculaCabecera.Id).Count();
                var count2 = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabecera.Id).Count();
                int valor;
                if (existe == 1)
                {
                    if (count == 0 && count2 == 0)
                    {
                        valor = _repMatriculaCabecera.FirstBy(x => x.Id == matriculaCabecera.Id, x => new { x.IdPespecifico }).IdPespecifico;
                    }
                    else
                    {
                        valor = 0;
                    }
                }
                else
                {
                    valor = -1;
                }
                return valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// obtiene el 
        /// </summary>
        /// <returns>< object </returns>
        public object CargarMatricula(string CodigoMatricula, int IdPEspecifico)
        {
            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var listaRpta = _repMatriculaCabecera.ObtenerDatosMatriculaManual(CodigoMatricula);

                var _serCentroCosto = new CentroCostoService(_unitOfWork);
                var listaDatosCentroCosto = _serCentroCosto.ObtenerDatosDelCentrodeCosto(IdPEspecifico);

                return new { listaRpta, listaDatosCentroCosto };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Miguel Quiñones
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Estados Matricula
        /// </summary>
        /// <returns>< ListaDTO: List<EstadosMatriculaDTO> </returns>
        public List<MatriculaPespecificoAlumnoDTO> ObtenerCursosMatriculados(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerCursosMatriculados(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DesmatricularCurso(int idPespecificoMatriculaAlumno, string usuario)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.desmatricularCurso(idPespecificoMatriculaAlumno, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cronograma Detalle Pago
        /// </summary>
        /// <returns> Retorna Objeto recibido: CronogramaPagoDetalleFinal</returns>

        public object ObtenerCronogramaDetallePagoFinal(string CodigoMatricula)
        {

            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                var _repCronogramaPagoDetalleOriginal = _unitOfWork.CronogramaPagoDetalleOriginalRepository;


                var cronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobada.Version, x => new { x.Id, x.Cancelado, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version, x.FechaDeposito }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota);
                return cronogramaPagoDetalleFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor:Joseph Llanque
        /// Fecha: 08/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene IdMatriculaCabecera por DNI
        /// </summary>
        /// <returns>< DTO: IdMatriculaDniDTO </returns>
        public IdMatriculaDniDTO obtenerIdMatriculaporDni(string DNI)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.obtenerIdMatriculaporDni(DNI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Joseph Llanque
        /// Fecha: 08/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene IdMatriculaCabecera por Correo
        /// </summary>
        /// <returns>< DTO: IdMatriculaDniDTO </returns>
        public IdMatriculaCorreoDTO obtenerIdMatriculaporCorreo(string correo)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.obtenerIdMatriculaporCorreo(correo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 08/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene IdMatriculaCabecera por Codigo
        /// </summary>
        /// <returns>< DTO: MatriculaCabeceraComboDTO </returns>
        public MatriculaCabeceraComboDTO obtenerIdMatriculaporCodigo(string codigo)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.obtenerIdMatriculaporCodigo(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object actualizarEstadoMatriculaBeneficio(int IdMatriculaCabeceraBeneficio, int IdConfiguracionBeneficioProgramaGeneral, string Usuario)
        {
            List<BeneficioDatosAdicionalesDTO> datosAdicionales = new List<BeneficioDatosAdicionalesDTO>();
            var beneficiosmatricula = 0;
            //  MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;

            datosAdicionales = _repMatriculaCabecera.ObtenerDatosAdicionalesPgeneralPorIdConfiguracion(IdConfiguracionBeneficioProgramaGeneral);

            if (datosAdicionales.Count > 0)
            {
                beneficiosmatricula = _repMatriculaCabecera.ActualizarEstadoMatriculaCabeceraBeneficio(IdMatriculaCabeceraBeneficio);
            }
            else
            {
                _repMatriculaCabecera.ActualizarEstadoMatriculaCabeceraBeneficio(IdMatriculaCabeceraBeneficio);
                beneficiosmatricula = _repMatriculaCabecera.PorAprobarSolicitudBeneficio(IdMatriculaCabeceraBeneficio);
            }

            var matriculaCabeceraBeneficio = _unitOfWork.MatriculaCabeceraBeneficiosRepository.ObtenerPorId(IdMatriculaCabeceraBeneficio);
            // FirstById(IdMatriculaCabeceraBeneficio);
            if (matriculaCabeceraBeneficio != null)
            {
                matriculaCabeceraBeneficio.FechaModificacion = DateTime.Now;
                matriculaCabeceraBeneficio.UsuarioModificacion = Usuario;
                matriculaCabeceraBeneficio.IdEstadoMatriculaCabeceraBeneficio = 1; //1 Pendiente

                // matriculaCabecera = this.Add(matriculaCabecera);

                //var matriculaCabeceraBeneficioService = new MatriculaCabeceraBeneficioService(_unitOfWork);
                _unitOfWork.MatriculaCabeceraBeneficiosRepository.Update(matriculaCabeceraBeneficio);
                _unitOfWork.Commit();
            }
            return (beneficiosmatricula, datosAdicionales);

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
                var rpta = _unitOfWork.MatriculaCabeceraRepository.GenerarReporteProyectoPresentadoPorAlumno(filtroReporte);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 24/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene version por idmatricula
        /// </summary>
        /// <param name="codMatricula"> idMatricula </param>
        /// <returns> List<FiltroMatriculaAlumnoDTO> </returns>
        public List<VersionMatriculaDisponibleDTO> ObtenerVersionDisponibleMatricula(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerVersionDisponibleMatricula(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 24/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene version por idmatricula
        /// </summary>
        /// <param name="codMatricula"> idMatricula </param>
        /// <returns> VersionMatriculaDTO </returns>
        public VersionMatriculaDTO ObtenerVersionMatricula(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerVersionMatricula(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 17/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el país al que pertenece una matrícula
        /// </summary>
        /// <param> IdMatriculaCabecera </param>
        /// <returns> List<FiltroMatriculaAlumnoDTO> </returns>
        public PaisMatriculaDTO ObtenerPaisMatricula(string IdMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraRepository.ObtenerPaisMatricula(IdMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 11/11/2024
        /// Version: 1.0
        /// <summary>
        /// Genera matricula Cabecera por postulante
        /// </summary>
        /// <param name="idPostulante"></param>
        /// <param name="Usuario"></param>
        /// <returns> string con el codigo matricual cabecera </returns>
        public string GenerarMatriculaCabeceraPorPostulante(int idPostulante, string Usuario)
        {            
            try
            {
                List<PespecificoPadrePespecificoHijoDTO> listaPEspecificoPadrePespecificoHijo = new List<PespecificoPadrePespecificoHijoDTO>();
                //PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
                
                var alumno = _unitOfWork.PostulanteRepository.ObtenerIdAlumnoDesdeidPostulanteSinMatricula(idPostulante);
                var idPEspecifico = 23003;
                using (TransactionScope scope = new TransactionScope())
                {

                    var listRpta = "";
                    bool CadaNDias = false;
                    int DiaPago = DateTime.Now.Day;
                    var existe = _unitOfWork.MatriculaCabeceraRepository.ExisteMatriculaCabecera(alumno.IdAlumno.GetValueOrDefault(), idPEspecifico);
                    var codigoMatricula = string.Empty;
                    int idMatriculaCabecera = 0;

                    if (existe)
                    {
                        codigoMatricula = "0";
                    }
                    else
                    {
                        var codigoBanco = _unitOfWork.PEspecificoRepository.FirstBy(x => x.Id == idPEspecifico, x => new { x.CodigoBanco }).CodigoBanco;

                        TMatriculaCabecera matriculaCabecera = new TMatriculaCabecera()
                        {
                            CodigoMatricula = string.Concat(alumno.IdAlumno, codigoBanco),
                            IdAlumno = alumno.IdAlumno.GetValueOrDefault(),
                            IdPespecifico = idPEspecifico,
                            UsuarioCreacion = Usuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = Usuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                            IdEstadoPagoMatricula = ValorEstatico.IdEstadoPagoMatriculaPorMatricular,
                            EstadoMatricula = "pormatricular",
                            FechaMatricula = DateTime.Now,
                            IdCoordinador = 126,
                            IdAsesor = 126,

                            IdEstadoMatricula = 1,
                            UsuarioCoordinadorAcademico = "0",
                            ObservacionGeneralOperaciones = "",
                            IdPaquete = 0
                        };
                        _unitOfWork.MatriculaCabeceraRepository.Insert(matriculaCabecera);
                        //_unitOfWork.Commit();
                        codigoMatricula = matriculaCabecera.CodigoMatricula;
                        idMatriculaCabecera = matriculaCabecera.Id;

                        //Verificar si es hijo o si es padre
                        listaPEspecificoPadrePespecificoHijo = _unitOfWork.PEspecificoMatriculaAlumnoRepository.ListaPespecificoPadrePespecificoHijo(idPEspecifico);

                        //IdUsuarioMoodle
                        var IdUsuarioMoodle = _unitOfWork.PEspecificoMatriculaAlumnoRepository.IdUsuarioMoodle(alumno.IdAlumno.GetValueOrDefault());
                        //var IdUsuarioMoodle = Int32.Parse(IdUsuario);


                        //var idmatriculacabecera = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula == codigoMatricula);
                        //
                        if (listaPEspecificoPadrePespecificoHijo.Count > 0)
                        {

                            foreach (var item in listaPEspecificoPadrePespecificoHijo)
                            {
                                //IdCursoMoodle
                                var IdCursoMoodle = _unitOfWork.PEspecificoMatriculaAlumnoRepository.IdCursoMoodle(item.PEspecificoHijoId);

                                TPespecificoMatriculaAlumno matricula = new TPespecificoMatriculaAlumno();
                                matricula.IdMatriculaCabecera = idMatriculaCabecera;
                                matricula.IdPespecifico = item.PEspecificoHijoId;
                                matricula.IdPespecificoTipoMatricula = 1;
                                matricula.IdCursoMoodle = IdCursoMoodle;
                                matricula.IdUsuarioMoodle = IdUsuarioMoodle;
                                matricula.Grupo = 1;
                                matricula.AplicaNuevaAulaVirtual = _unitOfWork.PEspecificoMatriculaAlumnoRepository.ExisteNuevaAulaVirtual(item.PEspecificoHijoId);
                                matricula.UsuarioCreacion = Usuario;
                                matricula.FechaCreacion = DateTime.Now;
                                matricula.UsuarioModificacion = Usuario;
                                matricula.FechaModificacion = DateTime.Now;
                                matricula.Estado = true;

                                _unitOfWork.PEspecificoMatriculaAlumnoRepository.Insert(matricula);
                                //_unitOfWork.Commit();

                            }

                        }
                        else
                        {
                            //IdCursoMoodle
                            var IdCursoMoodle = _unitOfWork.PEspecificoMatriculaAlumnoRepository.IdCursoMoodle(idPEspecifico);

                            TPespecificoMatriculaAlumno matriculaBO = new TPespecificoMatriculaAlumno();
                            matriculaBO.IdMatriculaCabecera = idMatriculaCabecera;
                            matriculaBO.IdPespecifico = idPEspecifico;
                            matriculaBO.IdPespecificoTipoMatricula = 1;
                            matriculaBO.IdCursoMoodle = IdCursoMoodle;
                            matriculaBO.IdUsuarioMoodle = IdUsuarioMoodle;
                            matriculaBO.Grupo = 1;
                            matriculaBO.AplicaNuevaAulaVirtual = _unitOfWork.PEspecificoMatriculaAlumnoRepository.ExisteNuevaAulaVirtual(idPEspecifico);
                            matriculaBO.UsuarioCreacion = Usuario;
                            matriculaBO.FechaCreacion = DateTime.Now;
                            matriculaBO.UsuarioModificacion = Usuario;
                            matriculaBO.FechaModificacion = DateTime.Now;
                            matriculaBO.Estado = true;
                            _unitOfWork.PEspecificoMatriculaAlumnoRepository.Insert(matriculaBO);
                            //_unitOfWork.Commit();

                        }
                    }

                    var CursosMatriculados = 26256;
                    if (codigoMatricula == "0")
                    {
                        listRpta = "0";
                    }
                    else
                    {

                        TMatriculaDetalle matriculaDetalleBO = new TMatriculaDetalle()
                        {
                            IdMatriculaCabecera = idMatriculaCabecera,
                            IdCursoPespecifico = CursosMatriculados,
                            UsuarioCreacion = Usuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = Usuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                        _unitOfWork.MatriculaDetalleRepository.Insert(matriculaDetalleBO);
                        //_unitOfWork.Commit();

                        //var OpcionPagoNDias = "false";
                        //if (OpcionPagoNDias == "Ndias")
                        //{
                        //    CadaNDias = true;
                        //}

                        //Generacion de campos WEB para el portal web
                        //var monedas = "1";

                        //var monedaFormulario = monedas == "1" ? "soles" : "dolares";
                        //var moneda = _unitOfWork.MonedaRepository.FirstBy(x => x.NombrePlural.Equals(monedaFormulario));

                        //var webMoneda = moneda.DigitoFinanzas;
                        //var tipoCambioSol = _unitOfWork.TipoCambioRepository.GetBy(x => x.Estado == true).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
                        //var tipoCambioCol = _unitOfWork.TipoCambioColRepository.GetBy(x => x.Estado == true).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
                        //var tipoCambioGeneral = _unitOfWork.TipoCambioMonedumRepository.GetBy(x => x.IdMoneda == moneda.Id).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();

                        //double? webTipoCambio = 0.0;
                        //if (moneda.Id == 20) //Soles
                        //{
                        //    webTipoCambio = tipoCambioSol.SolesDolares;
                        //}
                        //else if (moneda.Id == 10) // Colombiano
                        //{
                        //    webTipoCambio = tipoCambioCol.PesosDolares;
                        //}
                        //else if (moneda.Id == 19)// Otros
                        //{
                        //    webTipoCambio = null;
                        //}
                        //else
                        //{
                        //    webTipoCambio = tipoCambioGeneral.MonedaAdolar;
                        //}

                        //var webTotalPagar = 0;

                        //TCronogramaPago cronogramaPagoBO = new TCronogramaPago()
                        //{
                        //    IdMatriculaCabecera = idMatriculaCabecera,
                        //    IdAlumno = alumno.IdAlumno,
                        //    IdPespecifico = idPEspecifico,
                        //    Periodo = "2024",
                        //    Moneda = monedas == "1" ? "soles" : "dolares",
                        //    AcuerdoPago = "Beca",
                        //    TipoCambio = 3.9,
                        //    TotalPagar = webTotalPagar,
                        //    NroCuotas = 1,
                        //    FechaIniPago = DateTime.Now,
                        //    ConCuotaInicial = false,
                        //    CuotaInicial = 0,
                        //    CadaNdias = CadaNDias,
                        //    Ndias = 0,
                        //    UsuarioCreacion = Usuario,
                        //    FechaCreacion = DateTime.Now,
                        //    UsuarioModificacion = Usuario,
                        //    FechaModificacion = DateTime.Now,
                        //    Estado = true,
                        //    WebMoneda = webMoneda.ToString(),
                        //    WebTipoCambio = webTipoCambio,
                        //    WebTotalPagar = webTotalPagar,
                        //    WebTotalPagarConv = webTotalPagar,
                        //    Observaciones = "Capacitacion"

                        //};

                        //_unitOfWork.CronogramaPagoRepository.Insert(cronogramaPagoBO);
                        //_unitOfWork.Commit();
                        
                        //var fechaInicio = DateTime.Now;
                        //double MontoCuota = webTotalPagar / 1;
                        //MontoCuota = Math.Round(MontoCuota, 2);
                        //double Saldo = webTotalPagar;

                        //double Total = Saldo;
                        //Saldo = Total - MontoCuota;

                        //if (CadaNDias == true)
                        //{
                        //    fechaInicio = fechaInicio.AddDays(Convert.ToDouble(0));
                        //}

                        //TCronogramaPagoDetalle cronogramaPagoDetalleBO = new TCronogramaPagoDetalle()
                        //{
                        //    IdMatriculaCabecera = idMatriculaCabecera,
                        //    NroCuota = 1,
                        //    FechaVencimiento = fechaInicio,
                        //    TotalPagar = Convert.ToDecimal(Total),
                        //    Cuota = Convert.ToDecimal(MontoCuota),
                        //    Saldo = Convert.ToDecimal(Saldo),
                        //    TipoCuota = "CUOTA",
                        //    Moneda = monedas == "1" ? "soles" : "dolares",
                        //    UsuarioCreacion = Usuario,
                        //    FechaCreacion = DateTime.Now,
                        //    UsuarioModificacion = Usuario,
                        //    FechaModificacion = DateTime.Now,
                        //    Cancelado = false,
                        //    Estado = true
                        //};
                        //_unitOfWork.CronogramaPagoDetalleRepository.Insert(cronogramaPagoDetalleBO);
                        ////_unitOfWork.Commit();
                        //if (CadaNDias == false)
                        //{
                        //    try
                        //    {
                        //        DateTime FechaPago = new DateTime(fechaInicio.Year, fechaInicio.Month + 1, DiaPago);
                        //        fechaInicio = FechaPago;
                        //    }
                        //    catch (ArgumentOutOfRangeException)
                        //    {
                        //        fechaInicio = fechaInicio.AddMonths(1);
                        //    }
                        //}


                        //TControlDocAlumno controlDocAlumnoBO = new TControlDocAlumno()
                        //{
                        //    IdMatriculaCabecera = idMatriculaCabecera,
                        //    IdCriterioCalificacion = 0,
                        //    QuienEntrego = "",
                        //    FechaEntregaDocumento = null,
                        //    Observaciones = "",
                        //    ComisionableEditable = "Ninguno",
                        //    MontoComisionable = 0,
                        //    ObservacionesComisionable = "",
                        //    PagadoComisionable = 0,
                        //    UsuarioCreacion = Usuario,
                        //    FechaCreacion = DateTime.Now,
                        //    UsuarioModificacion = Usuario,
                        //    FechaModificacion = DateTime.Now,
                        //    Estado = true
                        //};

                        //int[] listaIdDocumento = { 1, 2, 3, 6, 7, 8 };
                        //_unitOfWork.ControlDocAlumnoRepository.Insert(controlDocAlumnoBO);
                        ////_unitOfWork.Commit();

                        //for (int x = 0; x < listaIdDocumento.Length; x++)
                        //{
                        //    TControlDoc controlDocBO = new TControlDoc()
                        //    {
                        //        IdMatriculaCabecera = idMatriculaCabecera,
                        //        IdCriterioDoc = listaIdDocumento[x],
                        //        EstadoDocumento = false,
                        //        UsuarioCreacion = Usuario,
                        //        FechaCreacion = DateTime.Now,
                        //        UsuarioModificacion = Usuario,
                        //        FechaModificacion = DateTime.Now,
                        //        Estado = true
                        //    };
                        //    _unitOfWork.ControlDocRepository.Insert(controlDocBO);
                        //    //_unitOfWork.Commit();
                        //    //_repControlDocumento.Insert(controlDocBO);
                        //}
                        listRpta = codigoMatricula;

                    }
                    _unitOfWork.Commit();
                    scope.Complete();
                    return listRpta;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 11/11/2024
        /// Version: 1.0
        /// <summary>
        /// Genera matricula Cabecera por postulante
        /// </summary>
        /// <param name="idPostulante"></param>
        /// <param name="Usuario"></param>
        /// <returns> string con el codigo matricual cabecera </returns>
        public int ActualizarCronogramaPagoPorPostulante(int idPostulante, string Usuario)
        {
            try
            {
                //integraDBContext _integraDBContext = new integraDBContext();
                //CronogramaPagoRepositorio _repCronogramaPago = new CronogramaPagoRepositorio(_integraDBContext);
                //CronogramaPagoDetalleRepositorio _repCronogramaPagoDetalle = new CronogramaPagoDetalleRepositorio(_integraDBContext);
                //CronogramaPagoDetalleModLogFinalRepositorio _repCronogramaPagoDetalleModLogFinal = new CronogramaPagoDetalleModLogFinalRepositorio(_integraDBContext);
                //CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinalRepositorio = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);
                //CronogramaPagoDetalleOriginalRepositorio _repCronogramaPagoDetalleOriginal = new CronogramaPagoDetalleOriginalRepositorio(_integraDBContext);
                //PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);

                using (var scope = new TransactionScope())
                {
                    var tipoCambio = "3.9";
                    var moneda = "1";

                    if (tipoCambio == "") tipoCambio = "0";
                    if (moneda == "1") moneda = "soles";
                    if (moneda == "2") moneda = "dolares";
                    var alumno = _unitOfWork.PostulanteRepository.ObtenerDatosMatriculaIdPostulante(idPostulante);
                    var idsCronogramaPagoDetalle = _unitOfWork.CronogramaPagoDetalleRepository.ObtenerListaDeIdCronogramaDetallePagoporIdCabecera(alumno.IdMatriculaCabecera ?? default(int));
                    var idMatriculaCabecera = alumno.IdMatriculaCabecera;
                    var listaRpta = _unitOfWork.CronogramaPagoDetalleRepository.Delete(idsCronogramaPagoDetalle, Usuario);
                    //_unitOfWork.Commit();
                    var listaCronogramaDetallePago = _unitOfWork.CronogramaPagoDetalleRepository.ObtenerListaDeCronogramaDetallePagoporIdMatricula(alumno.IdMatriculaCabecera ?? default(int));
                    for (int i = 0; i < listaCronogramaDetallePago.Count; i++)
                    {
                        TCronogramaPagoDetalleModLogFinal cronogramaPagoDetalleModLogFinalBO = new TCronogramaPagoDetalleModLogFinal()
                        {
                            IdMatriculaCabecera = listaCronogramaDetallePago[i].IdMatriculaCabecera,
                            Fecha = DateTime.Now,
                            NroCuota = listaCronogramaDetallePago[i].NroCuota,
                            NroSubCuota = 1,
                            FechaVencimiento = listaCronogramaDetallePago[i].FechaVencimiento,
                            TotalPagar = listaCronogramaDetallePago[i].TotalPagar,
                            Cuota = listaCronogramaDetallePago[i].Cuota,
                            Mora = 0,
                            MontoPagado = 0,
                            Saldo = listaCronogramaDetallePago[i].Saldo,
                            Cancelado = false,
                            TipoCuota = listaCronogramaDetallePago[i].TipoCuota,
                            Moneda = listaCronogramaDetallePago[i].Moneda,
                            //objCroFinal.TipoCambio = Convert.ToDecimal(tipocambio);
                            FechaPago = null,
                            IdFormaPago = null,
                            FechaPagoBanco = null,
                            Ultimo = false,
                            Observaciones = "CAPACITACION",
                            IdDocumentoPago = null,
                            NroDocumento = null,
                            MonedaPago = "soles",
                            TipoCambio = Convert.ToDecimal(3.9),
                            MensajeSistema = null,
                            FechaProcesoPago = null,
                            EstadoPrimerLog = "1",
                            Version = 0,
                            Aprobado = true,
                            Estado2 = true,
                            UsuarioCreacion = Usuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = Usuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        _unitOfWork.CronogramaPagoDetalleModLogFinalRepository.Insert(cronogramaPagoDetalleModLogFinalBO);
                        //_unitOfWork.Commit();
                        TCronogramaPagoDetalleFinal cronogramaPagoDetalleFinalBO = new TCronogramaPagoDetalleFinal()
                        {


                            IdMatriculaCabecera = listaCronogramaDetallePago[i].IdMatriculaCabecera,
                            NroCuota = listaCronogramaDetallePago[i].NroCuota,
                            NroSubCuota = 1,
                            FechaVencimiento = listaCronogramaDetallePago[i].FechaVencimiento,
                            TotalPagar = listaCronogramaDetallePago[i].TotalPagar,
                            Cuota = listaCronogramaDetallePago[i].Cuota,
                            Saldo = listaCronogramaDetallePago[i].Saldo,
                            MontoPagado = 0,
                            Cancelado = Convert.ToBoolean(0),
                            TipoCuota = listaCronogramaDetallePago[i].TipoCuota,
                            Moneda = listaCronogramaDetallePago[i].Moneda,
                            Mora = 0,
                            Version = 0,
                            Enviado = false,
                            Aprobado = true,
                            UsuarioCreacion = Usuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = Usuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        _unitOfWork.CronogramaPagoDetalleFinalRepository.Insert(cronogramaPagoDetalleFinalBO);
                        //_unitOfWork.Commit();

                        TCronogramaPagoDetalle cronogramaPagoDetalleBO = new TCronogramaPagoDetalle()
                        {
                            IdMatriculaCabecera = alumno.IdMatriculaCabecera,
                            NroCuota = listaCronogramaDetallePago[i].NroCuota,
                            FechaVencimiento = listaCronogramaDetallePago[i].FechaVencimiento,
                            TotalPagar = listaCronogramaDetallePago[i].TotalPagar,
                            Cuota = listaCronogramaDetallePago[i].Cuota,
                            Saldo = listaCronogramaDetallePago[i].Saldo,
                            TipoCuota = listaCronogramaDetallePago[i].TipoCuota,
                            Moneda = listaCronogramaDetallePago[i].Moneda,
                            Cancelado = false,
                            UsuarioCreacion = Usuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = Usuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };

                        _unitOfWork.CronogramaPagoDetalleRepository.Insert(cronogramaPagoDetalleBO);
                        //_unitOfWork.Commit();

                        //var original = _tcrm_CentroCostoService.IngresarCronogramaDetOriginal(listObjCronograma[i].matId, listObjCronograma[i].nroCuota, 1, 
                        //listObjCronograma[i].fechaVencimiento, listObjCronograma[i].totalPagar, listObjCronograma[i].cuota,
                        //listObjCronograma[i].saldo, false, listObjCronograma[i].tipocuota, (moneda == "1" ? "soles" : "dolares"), Convert.ToDecimal(tipocambio));
                        var monedas = "soles";
                        var cantidad = _unitOfWork.CronogramaPagoDetalleOriginalRepository.GetBy(x => x.IdMatriculaCabecera == listaCronogramaDetallePago[i].IdMatriculaCabecera && x.NroCuota == listaCronogramaDetallePago[i].NroCuota).Count();
                        if (cantidad == 0)
                        {
                            TCronogramaPagoDetalleOriginal cronogramaPagoDetalleOriginalBO = new TCronogramaPagoDetalleOriginal()
                            {
                                IdMatriculaCabecera = listaCronogramaDetallePago[i].IdMatriculaCabecera,
                                NroCuota = listaCronogramaDetallePago[i].NroCuota,
                                NroSubCuota = 1,
                                FechaVencimiento = listaCronogramaDetallePago[i].FechaVencimiento,
                                TotalPagar = listaCronogramaDetallePago[i].TotalPagar,
                                Cuota = listaCronogramaDetallePago[i].Cuota,
                                Saldo = listaCronogramaDetallePago[i].Saldo,
                                Cancelado = false,
                                TipoCuota = listaCronogramaDetallePago[i].TipoCuota,
                                Moneda = monedas == "1" ? "soles" : "dolares",
                                TipocCambio = Convert.ToDecimal(3.9),
                                UsuarioCreacion = Usuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = Usuario,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            };
                            _unitOfWork.CronogramaPagoDetalleOriginalRepository.Insert(cronogramaPagoDetalleOriginalBO);
                            //_unitOfWork.Commit();
                        }

                        //var final = _tcrm_CentroCostoService.InserTCroDetFinal(objCroFinal, User.Identity.Name);
                        //_unitOfWork.CronogramaPagoDetalleFinalRepository.Insert(cronogramaPagoDetalleFinalBO);
                        //_repCronogramaPagoDetalleFinalRepositorio.Insert(cronogramaPagoDetalleFinalBO);
                        //var finalLog = _tCronogramaPagosDetalle_Mod_Log_FinalService.Insert(objCroLogFinal);
                        
                        _unitOfWork.Commit();
                    }

                    bool afirmacion = true;

                    var cronogramaPago = _unitOfWork.CronogramaPagoRepository.FirstBy(x => x.IdMatriculaCabecera == alumno.IdMatriculaCabecera);
                    var version = _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerMaximaVersionCronograma(alumno.IdMatriculaCabecera ?? default(int));
                    var cronogramaPagoDetFin = _unitOfWork.CronogramaPagoDetalleFinalRepository.GetBy(x => x.IdMatriculaCabecera == alumno.IdMatriculaCabecera && x.Version == version).OrderBy(x => x.NroCuota).FirstOrDefault();
                    cronogramaPago.ConCuotaInicial = true;
                    cronogramaPago.CuotaInicial = cronogramaPagoDetFin.Cuota;
                    cronogramaPago.FechaModificacion = DateTime.Now;
                    cronogramaPago.UsuarioModificacion = Usuario;
                    _unitOfWork.CronogramaPagoRepository.Update(cronogramaPago);
                    _unitOfWork.Commit();
                    //_repCronogramaPago.Update(cronogramaPago);

                    scope.Complete();
                    return idMatriculaCabecera ?? default(int);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }

}