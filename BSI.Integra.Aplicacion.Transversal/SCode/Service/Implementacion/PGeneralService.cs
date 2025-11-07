using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Linq;
using System.Transactions;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PGeneralService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_PGeneral
    /// </summary>
    public class PGeneralService : IPGeneralService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PGeneralService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneral, PGeneral>(MemberList.None).ReverseMap();
                cfg.CreateMap<TroncalPgeneral, TTroncalPgeneral>(MemberList.None).ReverseMap();
                cfg.CreateMap<PGeneral, PGeneralDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ScoringCiudadProgramaDTO, ProgramaGeneralPerfilScoringCiudad>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPerfilCoeficienteDTO, ProgramaGeneralPerfilCiudadCoeficiente>(MemberList.None).ReverseMap();
                cfg.CreateMap<ScoringModalidadProgramaDTO, ProgramaGeneralPerfilScoringModalidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPerfilCoeficienteDTO, ProgramaGeneralPerfilModalidadCoeficiente>(MemberList.None).ReverseMap();
                cfg.CreateMap<ScoringAFormacionProgramaDTO, ProgramaGeneralPerfilScoringAformacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPerfilCoeficienteDTO, ProgramaGeneralPerfilAformacionCoeficiente>(MemberList.None).ReverseMap();
                cfg.CreateMap<ScoringIndustriaProgramaDTO, ProgramaGeneralPerfilScoringIndustria>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPerfilCoeficienteDTO, ProgramaGeneralPerfilIndustriaCoeficiente>(MemberList.None).ReverseMap();
                cfg.CreateMap<ScoringCargoProgramaDTO, ProgramaGeneralPerfilScoringCargo>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPerfilCoeficienteDTO, ProgramaGeneralPerfilCargoCoeficiente>(MemberList.None).ReverseMap();
                cfg.CreateMap<ScoringTrabajoProgramaDTO, ProgramaGeneralPerfilScoringAtrabajo>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPerfilCoeficienteDTO, ProgramaGeneralPerfilAtrabajoCoeficiente>(MemberList.None).ReverseMap();
                cfg.CreateMap<ScoringCategoriaProgramaDTO, ProgramaGeneralPerfilScoringCategoria>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPerfilCoeficienteDTO, ProgramaGeneralPerfilCategoriaCoeficiente>(MemberList.None).ReverseMap();
                cfg.CreateMap<EscalaProbabilidadDTO, ProgramaGeneralPerfilEscalaProbabilidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPerfilInterceptoDTO, ProgramaGeneralPerfilIntercepto>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDatoPerFilContactoProgramaDTO, ProgramaGeneralPerfilTipoDato>(MemberList.None).ReverseMap();
                cfg.CreateMap<PGeneralCriterioEvaluacionDTO, PgeneralCriterioEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PGeneralCriterioEvaluacionDTO, TPgeneralCriterioEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PgeneralCriterioEvaluacion, TPgeneralCriterioEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PgeneralConfiguracionPlantilla, TPgeneralConfiguracionPlantilla>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralBeneficioDTO, TProgramaGeneralBeneficio>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralBeneficioDTO, ProgramaGeneralBeneficio>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralMotivacionDTO, TProgramaGeneralMotivacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralMotivacionDTO, ProgramaGeneralMotivacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralProblemaDTO, TProgramaGeneralProblema>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralProblemaDTO, ProgramaGeneralProblema>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralModeloCertificadoDTO, ProgramaGeneralModeloCertificado>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<PGeneralComboDTO> </returns>
        public IEnumerable<PGeneralComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<PGeneralComboDTO> </returns>
        public IEnumerable<PGeneralComboDTO> ObtenerComboPorIdArea(int IdArea)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerComboPorIdArea(IdArea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<PGeneralComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerPGeneralLanzamientoPorEjecucion()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPGeneralLanzamientoPorEjecucion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 29/09/2022
        /// Version: 1.0
        /// <summary>
        /// Combo para el modulo conjunto campaña, trae el url
        /// </summary>
        /// <returns> List<PGeneralComboDTO> </returns>

        public IEnumerable<ProgramaGeneralComboDTO> ObtenerComboUrl()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerComboUrl();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProgramaGeneralComboDTO> ProgramaGneralconUrlVersion()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ProgramaGneralconUrlVersion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ProgramaGeneralComboDTO> ProgramaGneralconPEspecifico()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ProgramaGneralconPEspecifico();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PGeneral
        /// </summary>
        /// <returns> List<PGeneralAlternoDTO> </returns>
        public IEnumerable<PGeneralAlternoDTO> ObtenerPGeneral()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPGeneral();
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
        /// Obtiene Cabecera Speech para Agenda
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> PGeneralCabeceraSpeechAgendaDTO </returns>
        public PGeneralCabeceraSpeechAgendaDTO ObtenerCabeceraSpeechAgenda(int idOportunidad, int idCentroCosto)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerCabeceraSpeechAgenda(idOportunidad, idCentroCosto);
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
        /// Obtiene Publico Objetivo para Agenda
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<PGeneralPublicoObjetivoParaAgendaDTO> </returns>
        public IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> ObtenerPublicoObjetivoProgramaParaAgenda(int idCentroCosto, int idOportunidad)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPublicoObjetivoProgramaParaAgenda(idCentroCosto, idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Carlos Crispin R.
        /// Fecha: 09/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Publico Objetivo para Agenda Nueva V3
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<PGeneralPublicoObjetivoParaAgendaDTO> </returns>
        public IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3(int idCentroCosto, int idOportunidad)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3(idCentroCosto, idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 06/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene Publico Objetivo para Agenda Nueva V3
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> List<PGeneralPublicoObjetivoSalidaDTO> </returns>
        public IEnumerable<PGeneralPublicoObjetivoSalidaDTO> ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3PorAlumno(int idOportunidad, int idAlumno)
        {
            IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> datosRepo;
            try
            {
                datosRepo = _unitOfWork.PGeneralRepository.ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3PorAlumno(idOportunidad, idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (datosRepo == null)
            {
                return Enumerable.Empty<PGeneralPublicoObjetivoSalidaDTO>();
            }

            string ObtenerTextoRespuesta(int idRespuesta)
            {
                if (idRespuesta == 0) return null;
                _mapeoRespuestas.TryGetValue(idRespuesta, out string txt);
                return txt;
            }

            var resultadoTransformado = datosRepo.Select(prereq => new PGeneralPublicoObjetivoSalidaDTO
            {
                Id = prereq.Id,
                IdPGeneral = prereq.IdPGeneral,
                Contenido = prereq.Contenido,
                Respuesta = ObtenerTextoRespuesta(prereq.Respuesta)
            });

            return resultadoTransformado;
        }
        private static readonly Dictionary<int, string> _mapeoRespuestas = new Dictionary<int, string>
        {
            { 1, "Cumple al 100%" },
            { 2, "Cumple al 75%" },
            { 3, "Cumple al 50%" },
            { 4, "Cumple al 25%" },
            { 5, "No cumple" }
        };
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene atributos principales de T_PGeneral asociados a un Identificador
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <returns> PGeneralAtributosPrincipalesDTO </returns>
        public PGeneralAtributosPrincipalesDTO ObtenerPGeneralAtributosPrincipalesPorId(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPGeneralAtributosPrincipalesPorId(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene PGeneral Documento asociado a un Id de Programa General
        /// </summary>
        /// <param name="id">Id del Programa General</param>
        /// <returns> PgeneralDocumentoSeccionDTO </returns>
        public PgeneralDocumentoSeccionDTO ObtenerPgeneralDocumentoPorId(int id)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPgeneralDocumentoPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre del ProgramaGeneral asociado a un IdBusqueda
        /// </summary>
        /// <param name="idBusqueda">Id de Busqueda</param>
        /// <returns> PGeneralNombreDTO </returns>
        public PGeneralNombreDTO ObtenerPGeneralPorIdBusqueda(int idBusqueda)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPGeneralPorIdBusqueda(idBusqueda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene programa general, area y subArea por centro costo
        /// </summary>
        /// <param name="idCentroCosto">Id de Centro Costo</param>
        /// <returns> PGeneralAreaSubAreaDTO </returns>
        public PGeneralAreaSubAreaDTO ObtenerPGeneralPEspecificoPorCentroCosto(int idCentroCosto)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPGeneralPEspecificoPorCentroCosto(idCentroCosto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los padreEspecifico e hijoEspecifico de un programa general con restriccion de Lanzamiento y Estado
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<PadrePespecificoHijoDTO> </returns>
        public List<PadrePespecificoHijoDTO> ObtenerPadreHijoEspecificoV2(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPadreHijoEspecificoV2(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las frecuencias de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<FrecuenciaProgramaGeneralDTO> </returns>
        public List<FrecuenciaProgramaGeneralDTO> ObtenerFrecuenciasPorIdPGeneral(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerFrecuenciasPorIdPGeneral(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones de un programa general validando las sesiones configuradas para si visualización
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>List<PEspecificoSesionDTO></returns>  
        public List<PEspecificoSesionDTO> ObtenerSesionesProgramaGeneralValidadoVisualizacionAgenda(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerSesionesProgramaGeneralValidadoVisualizacionAgenda(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> Lista Sesiones Programa General: List<PEspecificoSesionDTO></returns>
        public List<PEspecificoSesionDTO> ObtenerSesionesPorProgramaGeneral(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerSesionesPorProgramaGeneral(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de monto de programa por idarea y subarea
        /// </summary>
        /// <param name="filtros">Objeto tipo Dictionary<string, string> </param>
        /// <returns> Lista Monto Pago Programa General: List<MontoPagoProgramaDTO></returns>  
        public List<MontoPagoProgramaDTO> ObtenerResumenProgramaV2(Dictionary<string, string> filtros)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerResumenProgramaV2(filtros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener modalidades por programa general
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<ModalidadProgramaDTO> </returns>
        public List<ModalidadProgramaDTO> ObtenerModalidadesPorProgramaGeneral(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerModalidadesPorProgramaGeneral(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de secciones de un programa general
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<InformacionProgramaDTO> </returns>
        public List<InformacionProgramaDTO> ObtenerSeccionesInformacionProgramaPorProgramaGeneral(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerSeccionesInformacionProgramaPorProgramaGeneral(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de las Personas a Enviar Correo
        /// </summary>
        /// <param name="idPersonal"> Id del Personal </param>
        /// <returns> List<CorreosGmailDTO> </returns>
        public List<CorreosGmailDTO> ObtenerCorreosIdPersonalAprobacion(int idPersonal)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerCorreosIdPersonalAprobacion(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de un programa general por el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<PGeneralAlternoDTO> </returns>
        public PGeneralAlternoDTO ObtenerPGeneralPorId(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPGeneralPorId(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> PGeneralAreaSubAreaDTO </returns>
        public IEnumerable<PGeneralPrincipalDTO> ObtenerTodoGrid()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerTodoGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 09/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros para ProgramaCriticoSubArea .
        /// </summary>
        /// <returns> PGeneralAreaSubAreaDTO </returns>
        public List<PGeneralProgramaCriticoSubAreaDTO> ObtenerPGeneralProgramaCriticoPorSubArea()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPGeneralProgramaCriticoPorSubArea();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros para ProgramaGeneral
        /// </summary>
        /// <returns> ComboDTO </returns>
        public IEnumerable<ComboDTO> ObtenerProgramasFiltro()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerProgramasFiltro();
            }
            catch
            {
                throw;
            }
        }


        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los beneficios por matricula
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera que se desea obtener los beneficios (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con los beneficios por version segun la matricula cabecera enviada</returns>
        public string ObtenerBeneficiosVersion(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerBeneficiosVersion(idMatriculaCabecera);
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
        /// Obtiene la version por matricula
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con la version formateada</returns>
        public string ObtenerVersion(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerVersion(idMatriculaCabecera);
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
        /// Obtiene la duracion del programa en meses
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la duracion del programa en meses</returns>
        public string ObtenerDuracionMeses(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerDuracionMeses(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos irca del alumno asociados al IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la duracion del programa en meses</returns>
        public List<PGeneralCursoIrcaDTO> ObtenerCursosIrcaAlumno(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerCursosIrcaAlumno(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos de Preguntas Frecuentes
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns>ProgramaCentroCostoDTO</returns>
        public ProgramaCentroCostoDTO ObtenerDatosPFrecuentes(int idCentroCosto)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerDatosPFrecuentes(idCentroCosto);
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
        /// Obtiene Codigo Partner por idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculacabecera"></param>
        /// <returns> string </returns>
        public string ObtenerCodigoPartner(int idMatriculacabecera)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerCodigoPartner(idMatriculacabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int? ObtenerPdu(int idMatriculacabecera)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPdu(idMatriculacabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int? ObtenerPduPorIdPGeneral(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPduPorIdPGeneral(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de programas generales con su subarea de capacitación
        /// </summary>
        /// <param></param>
        /// <returns>List<PGeneralSubAreaCapacitacionFiltroDTO></returns>
        public List<PGeneralSubAreaCapacitacionFiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerTodoFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los programas generales por id area
        /// </summary>
        /// <param name="listaAreas">Lista de indices de las areas (PK de la tabla pla.T_AreaCapacitacion)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerTodosPorIdArea(List<int> listaAreas)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerTodosPorIdArea(listaAreas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los programas generales por id sub area
        /// </summary>
        /// <param name="listaSubAreas">Lista de indices de las subareas (PK de la tabla pla.T_SubAreaCapacitacion)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerTodosPorIdSubArea(List<int> listaSubAreas)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerTodosPorIdSubArea(listaSubAreas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public PGeneral ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene seccion especifica dependiendo del parametro
        /// </summary>
        /// <param name="idPGeneral"> Id Programa General </param>
        /// <param name="seccion"> Sección </param>
        /// <returns> ProgramaSeccionIndividualDTO </returns>
        public ProgramaSeccionIndividualDTO SeccionIndividualPGeneral(int idPGeneral, string seccion)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.SeccionIndividualPGeneral(idPGeneral, seccion);
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
        /// Obtiene Programa para Cuotas
        /// </summary>
        /// <param name="idMatricula"></param>
        /// <returns> CuotasProgramaDTO </returns>
        public CuotasProgramaDTO ObtenerProgramaParaCuotas(int idMatricula)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerProgramaParaCuotas(idMatricula);
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
        /// Obtiene Programa General por IdBusqueda
        /// </summary>
        /// <param name="idBusqueda"></param>
        /// <returns> PgeneralDocumentoSeccionDTO </returns>
        public PgeneralDocumentoSeccionDTO ObtenePgeneralPorIdBusqueda(int idBusqueda)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenePgeneralPorIdBusqueda(idBusqueda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene pGeneralComboModuloDTO para el moduloo de Programa General
        /// </summary>
        /// <returns> PGeneralModuloComboDTO </returns>
        public async Task<PGeneralComboModuloDTO> ObtenerCombosModuloAsync()
        {
            try
            {
                var task_areaCapacitacion = _unitOfWork.AreaCapacitacionRepository.ObtenerFiltroAsync();
                var task_subAreacapacitacon = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltroAsync();
                var task_parametroSeo = _unitOfWork.ParametroSeoPwRepository.ObtenerComboAsync();
                var task_partnerPw = _unitOfWork.PartnerPwRepository.ObtenerComboAsync();
                var task_expositor = _unitOfWork.ExpositorRepository.ObtenerComboAsync();
                var task_categoriaPrograma = _unitOfWork.CategoriaProgramaRepository.ObtenerComboAsync();
                var task_visualizacionBsPlay = _unitOfWork.VisualizacionBsPlayRepository.ObtenerComboAsync();
                var task_titulo = _unitOfWork.TituloRepository.ObtenerComboAsync();
                var task_cargo = _unitOfWork.CargoRepository.ObtenerComboAsync();
                var task_areaFormacion = _unitOfWork.AreaFormacionRepository.ObtenerComboAsync();
                var task_areaTrabajo = _unitOfWork.AreaTrabajoRepository.ObtenerComboAsync();
                var task_industria = _unitOfWork.IndustriaRepository.ObtenerComboAsync();
                var task_ciudad = _unitOfWork.CiudadRepository.ObtenerComboAsync();
                var task_categoriaOrigenConHijos = _unitOfWork.CategoriaOrigenRepository.ObtenerCategoriaConHijosAsync();
                var task_tipoDato = _unitOfWork.TipoDatoRepository.ObtenerComboAsync();
                var task_pGeneral = _unitOfWork.PGeneralRepository.ObtenerProgramasFiltroAsync();
                var task_perfilContactoProgramaColumna = _unitOfWork.PerfilContactoProgramaColumnaRepository.ObtenerComboAsync();
                var task_modalidadCurso = _unitOfWork.ModalidadCursoRepository.ObtenerComboAsync();
                var task_paginaWebPw = _unitOfWork.PaginaWebPwRepository.ObtenerComboAsync();
                var task_versionPrograma = _unitOfWork.VersionProgramaRepository.ObtenerVersionProgramaAsync();
                var task_moduloPrograma = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerModuloAsync();
                var task_cicloPrograma = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerCicloAsync();
                var task_tipoPrograma = _unitOfWork.TipoProgramaRepository.ObtenerComboAsync();
                var task_proveedor = _unitOfWork.ProveedorRepository.ObtenerProveedorFiltroAsync();
                var task_pGeneralPeriodoAsincronico = _unitOfWork.PGeneralRepository.ObtenerPgeneralPeriodoAsincronicoAsync();

                var pGeneralComboModuloDTO = new PGeneralComboModuloDTO();
                pGeneralComboModuloDTO.AreaCapacitacion = await task_areaCapacitacion;
                pGeneralComboModuloDTO.SubAreaCapacitacion = await task_subAreacapacitacon;
                pGeneralComboModuloDTO.ParametroSeo = await task_parametroSeo;
                pGeneralComboModuloDTO.PartnerPw = await task_partnerPw;
                pGeneralComboModuloDTO.Expositor = await task_expositor;
                pGeneralComboModuloDTO.CategoriaPrograma = await task_categoriaPrograma;
                pGeneralComboModuloDTO.VisualizacionBsPlay = await task_visualizacionBsPlay;
                pGeneralComboModuloDTO.Titulo = await task_titulo;
                pGeneralComboModuloDTO.Cargo = await task_cargo;
                pGeneralComboModuloDTO.AreaFormacion = await task_areaFormacion;
                pGeneralComboModuloDTO.AreaTrabajo = await task_areaTrabajo;
                pGeneralComboModuloDTO.Industria = await task_industria;
                pGeneralComboModuloDTO.Ciudad = await task_ciudad;
                pGeneralComboModuloDTO.CategoriaOrigenConHijos = await task_categoriaOrigenConHijos;
                pGeneralComboModuloDTO.TipoDato = await task_tipoDato;
                pGeneralComboModuloDTO.PGeneral = await task_pGeneral;
                pGeneralComboModuloDTO.PerfilContactoProgramaColumna = await task_perfilContactoProgramaColumna;
                pGeneralComboModuloDTO.ModalidadCurso = await task_modalidadCurso;
                pGeneralComboModuloDTO.PaginaWebPw = await task_paginaWebPw;
                pGeneralComboModuloDTO.VersionPrograma = await task_versionPrograma;
                pGeneralComboModuloDTO.ModuloPrograma = await task_moduloPrograma;
                pGeneralComboModuloDTO.CicloPrograma = await task_cicloPrograma;
                pGeneralComboModuloDTO.TipoPrograma = await task_tipoPrograma;
                pGeneralComboModuloDTO.Proveedor = await task_proveedor;
                pGeneralComboModuloDTO.PGeneralPeriodoAsincronico = await task_pGeneralPeriodoAsincronico;
                return (pGeneralComboModuloDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene la lista de Programas  registradas en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> Lista DTO - List<PGeneralAlternoDTO>() </returns>
        public IEnumerable<PGeneralDTO> ListarProgramaGeneral(FiltroProgramaGeneralDTO dto)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ListarProgramaGeneral(dto);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Version: 1.0
        /// <summary>
        /// Guarda perfil del contacto del programa
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool GuardarPerfilContactoPrograma(CompuestoPerfilContactoProgramaDTO dto, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    EliminacionLogicoPorProgramaPCP(dto.IdPGeneral, usuario, dto);

                    PGeneral pGeneral = new PGeneral();

                    pGeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(dto.IdPGeneral);

                    pGeneral.ProgramaGeneralPerfilScoringCiudad = new List<ProgramaGeneralPerfilScoringCiudad>();
                    pGeneral.ProgramaGeneralPerfilCiudadCoeficiente = new List<ProgramaGeneralPerfilCiudadCoeficiente>();
                    foreach (var item in dto.Ciudad.CiudadScoring)
                    {
                        ProgramaGeneralPerfilScoringCiudad ciudad;
                        ciudad = _unitOfWork.ProgramaGeneralPerfilScoringCiudadRepository.ObtenerPorId(item.Id);
                        if (ciudad != null)
                        {
                            ciudad.Nombre = item.Nombre;
                            ciudad.IdCiudad = item.IdCiudad;
                            ciudad.IdSelect = item.IdSelect;
                            ciudad.Valor = item.Valor;
                            ciudad.Fila = item.Fila;
                            ciudad.Columna = item.Columna;
                            ciudad.Validar = item.Validar;
                            ciudad.FechaModificacion = DateTime.Now;
                            ciudad.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            ciudad = new ProgramaGeneralPerfilScoringCiudad();
                            ciudad.Nombre = item.Nombre;
                            ciudad.IdPgeneral = item.IdPGeneral;
                            ciudad.IdCiudad = item.IdCiudad;
                            ciudad.IdSelect = item.IdSelect;
                            ciudad.Valor = item.Valor;
                            ciudad.Fila = item.Fila;
                            ciudad.Columna = item.Columna;
                            ciudad.Validar = item.Validar;
                            ciudad.UsuarioCreacion = usuario;
                            ciudad.UsuarioModificacion = usuario;
                            ciudad.FechaCreacion = DateTime.Now;
                            ciudad.FechaModificacion = DateTime.Now;
                            ciudad.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilScoringCiudad.Add(ciudad);
                    }

                    foreach (var item in dto.Ciudad.CiudadCoefiente)
                    {
                        ProgramaGeneralPerfilCiudadCoeficiente ciudad;
                        ciudad = _unitOfWork.ProgramaGeneralPerfilCiudadCoeficienteRepository.ObtenerPorId(item.Id);
                        if (ciudad != null)
                        {
                            ciudad.Nombre = item.Nombre;
                            ciudad.Coeficiente = item.Coeficiente;
                            ciudad.IdSelect = item.IdSelect;
                            ciudad.Columna = item.IdColumna;
                            ciudad.FechaModificacion = DateTime.Now;
                            ciudad.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            ciudad = new ProgramaGeneralPerfilCiudadCoeficiente();
                            ciudad.Nombre = item.Nombre;
                            ciudad.IdPgeneral = item.IdPGeneral;
                            ciudad.Coeficiente = item.Coeficiente;
                            ciudad.IdSelect = item.IdSelect;
                            ciudad.Columna = item.IdColumna;
                            ciudad.UsuarioCreacion = usuario;
                            ciudad.UsuarioModificacion = usuario;
                            ciudad.FechaCreacion = DateTime.Now;
                            ciudad.FechaModificacion = DateTime.Now;
                            ciudad.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilCiudadCoeficiente.Add(ciudad);
                    }

                    pGeneral.ProgramaGeneralPerfilScoringModalidad = new List<ProgramaGeneralPerfilScoringModalidad>();
                    pGeneral.ProgramaGeneralPerfilModalidadCoeficiente = new List<ProgramaGeneralPerfilModalidadCoeficiente>();
                    foreach (var item in dto.Modalidad.ModalidadScoring)
                    {
                        ProgramaGeneralPerfilScoringModalidad modalidad;
                        modalidad = _unitOfWork.ProgramaGeneralPerfilScoringModalidadRepository.ObtenerPorId(item.Id);
                        if (modalidad != null)
                        {
                            modalidad.Nombre = item.Nombre;
                            modalidad.IdModalidadCurso = item.IdModalidad;
                            modalidad.IdSelect = item.IdSelect;
                            modalidad.Valor = item.Valor;
                            modalidad.Fila = item.Fila;
                            modalidad.Columna = item.Columna;
                            modalidad.Validar = item.Validar;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            modalidad = new ProgramaGeneralPerfilScoringModalidad();
                            modalidad.Nombre = item.Nombre;
                            modalidad.IdPgeneral = item.IdPGeneral;
                            modalidad.IdModalidadCurso = item.IdModalidad;
                            modalidad.IdSelect = item.IdSelect;
                            modalidad.Valor = item.Valor;
                            modalidad.Fila = item.Fila;
                            modalidad.Columna = item.Columna;
                            modalidad.Validar = item.Validar;
                            modalidad.UsuarioCreacion = usuario;
                            modalidad.UsuarioModificacion = usuario;
                            modalidad.FechaCreacion = DateTime.Now;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilScoringModalidad.Add(modalidad);
                    }

                    foreach (var item in dto.Modalidad.ModalidadCoefiente)
                    {
                        ProgramaGeneralPerfilModalidadCoeficiente modalidad;
                        modalidad = _unitOfWork.ProgramaGeneralPerfilModalidadCoeficienteRepository.ObtenerPorId(item.Id);
                        if (modalidad != null)
                        {
                            modalidad.Nombre = item.Nombre;
                            modalidad.Coeficiente = item.Coeficiente;
                            modalidad.IdSelect = item.IdSelect;
                            modalidad.Columna = item.IdColumna;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            modalidad = new ProgramaGeneralPerfilModalidadCoeficiente();
                            modalidad.Nombre = item.Nombre;
                            modalidad.IdPgeneral = item.IdPGeneral;
                            modalidad.Coeficiente = item.Coeficiente;
                            modalidad.IdSelect = item.IdSelect;
                            modalidad.Columna = item.IdColumna;
                            modalidad.UsuarioCreacion = usuario;
                            modalidad.UsuarioModificacion = usuario;
                            modalidad.FechaCreacion = DateTime.Now;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilModalidadCoeficiente.Add(modalidad);
                    }

                    pGeneral.ProgramaGeneralPerfilScoringAformacion = new List<ProgramaGeneralPerfilScoringAformacion>();
                    pGeneral.ProgramaGeneralPerfilAformacionCoeficiente = new List<ProgramaGeneralPerfilAformacionCoeficiente>();
                    foreach (var item in dto.Formacion.FormacionScoring)
                    {
                        ProgramaGeneralPerfilScoringAformacion formacion;
                        formacion = _unitOfWork.ProgramaGeneralPerfilScoringAformacionRepository.ObtenerPorId(item.Id);
                        if (formacion != null)
                        {
                            formacion.Nombre = item.Nombre;
                            formacion.IdAreaFormacion = item.IdAreaFormacion;
                            formacion.IdSelect = item.IdSelect;
                            formacion.Valor = item.Valor;
                            formacion.Fila = item.Fila;
                            formacion.Columna = item.Columna;
                            formacion.Validar = item.Validar;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            formacion = new ProgramaGeneralPerfilScoringAformacion();
                            formacion.Nombre = item.Nombre;
                            formacion.IdPgeneral = item.IdPGeneral;
                            formacion.IdAreaFormacion = item.IdAreaFormacion;
                            formacion.IdSelect = item.IdSelect;
                            formacion.Valor = item.Valor;
                            formacion.Fila = item.Fila;
                            formacion.Columna = item.Columna;
                            formacion.Validar = item.Validar;
                            formacion.UsuarioCreacion = usuario;
                            formacion.UsuarioModificacion = usuario;
                            formacion.FechaCreacion = DateTime.Now;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilScoringAformacion.Add(formacion);
                    }

                    foreach (var item in dto.Formacion.FormacionCoefiente)
                    {
                        ProgramaGeneralPerfilAformacionCoeficiente formacion;
                        formacion = _unitOfWork.ProgramaGeneralPerfilAformacionCoeficienteRepository.ObtenerPorId(item.Id);
                        if (formacion != null)
                        {
                            formacion.Nombre = item.Nombre;
                            formacion.Coeficiente = item.Coeficiente;
                            formacion.IdSelect = item.IdSelect;
                            formacion.Columna = item.IdColumna;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            formacion = new ProgramaGeneralPerfilAformacionCoeficiente();
                            formacion.Nombre = item.Nombre;
                            formacion.IdPgeneral = item.IdPGeneral;
                            formacion.Coeficiente = item.Coeficiente;
                            formacion.IdSelect = item.IdSelect;
                            formacion.Columna = item.IdColumna;
                            formacion.UsuarioCreacion = usuario;
                            formacion.UsuarioModificacion = usuario;
                            formacion.FechaCreacion = DateTime.Now;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilAformacionCoeficiente.Add(formacion);
                    }

                    pGeneral.ProgramaGeneralPerfilScoringIndustria = new List<ProgramaGeneralPerfilScoringIndustria>();
                    pGeneral.ProgramaGeneralPerfilIndustriaCoeficiente = new List<ProgramaGeneralPerfilIndustriaCoeficiente>();
                    foreach (var item in dto.Industria.IndustriaScoring)
                    {
                        ProgramaGeneralPerfilScoringIndustria industria;
                        industria = _unitOfWork.ProgramaGeneralPerfilScoringIndustriaRepository.ObtenerPorId(item.Id);
                        if (industria != null)
                        {
                            industria.Nombre = item.Nombre;
                            industria.IdIndustria = item.IdIndustria;
                            industria.IdSelect = item.IdSelect;
                            industria.Valor = item.Valor;
                            industria.Fila = item.Fila;
                            industria.Columna = item.Columna;
                            industria.Validar = item.Validar;
                            industria.FechaModificacion = DateTime.Now;
                            industria.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            industria = new ProgramaGeneralPerfilScoringIndustria();
                            industria.Nombre = item.Nombre;
                            industria.IdPgeneral = item.IdPGeneral;
                            industria.IdIndustria = item.IdIndustria;
                            industria.IdSelect = item.IdSelect;
                            industria.Valor = item.Valor;
                            industria.Fila = item.Fila;
                            industria.Columna = item.Columna;
                            industria.Validar = item.Validar;
                            industria.UsuarioCreacion = usuario;
                            industria.UsuarioModificacion = usuario;
                            industria.FechaCreacion = DateTime.Now;
                            industria.FechaModificacion = DateTime.Now;
                            industria.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilScoringIndustria.Add(industria);
                    }

                    foreach (var item in dto.Industria.IndustriaCoefiente)
                    {
                        ProgramaGeneralPerfilIndustriaCoeficiente industria;
                        industria = _unitOfWork.ProgramaGeneralPerfilIndustriaCoeficienteRepository.ObtenerPorId(item.Id);
                        if (industria != null)
                        {
                            industria.Nombre = item.Nombre;
                            industria.Coeficiente = item.Coeficiente;
                            industria.IdSelect = item.IdSelect;
                            industria.Columna = item.IdColumna;
                            industria.FechaModificacion = DateTime.Now;
                            industria.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            industria = new ProgramaGeneralPerfilIndustriaCoeficiente();
                            industria.Nombre = item.Nombre;
                            industria.IdPgeneral = item.IdPGeneral;
                            industria.Coeficiente = item.Coeficiente;
                            industria.IdSelect = item.IdSelect;
                            industria.Columna = item.IdColumna;
                            industria.UsuarioCreacion = usuario;
                            industria.UsuarioModificacion = usuario;
                            industria.FechaCreacion = DateTime.Now;
                            industria.FechaModificacion = DateTime.Now;
                            industria.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilIndustriaCoeficiente.Add(industria);
                    }

                    pGeneral.ProgramaGeneralPerfilScoringCargo = new List<ProgramaGeneralPerfilScoringCargo>();
                    pGeneral.ProgramaGeneralPerfilCargoCoeficiente = new List<ProgramaGeneralPerfilCargoCoeficiente>();
                    foreach (var item in dto.Cargo.CargoScoring)
                    {
                        ProgramaGeneralPerfilScoringCargo cargo;
                        cargo = _unitOfWork.ProgramaGeneralPerfilScoringCargoRepository.ObtenerPorId(item.Id);
                        if (cargo != null)
                        {
                            cargo.Nombre = item.Nombre;
                            cargo.IdCargo = item.IdCargo;
                            cargo.IdSelect = item.IdSelect;
                            cargo.Valor = item.Valor;
                            cargo.Fila = item.Fila;
                            cargo.Columna = item.Columna;
                            cargo.Validar = item.Validar;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            cargo = new ProgramaGeneralPerfilScoringCargo();
                            cargo.Nombre = item.Nombre;
                            cargo.IdPgeneral = item.IdPGeneral;
                            cargo.IdCargo = item.IdCargo;
                            cargo.IdSelect = item.IdSelect;
                            cargo.Valor = item.Valor;
                            cargo.Fila = item.Fila;
                            cargo.Columna = item.Columna;
                            cargo.Validar = item.Validar;
                            cargo.UsuarioCreacion = usuario;
                            cargo.UsuarioModificacion = usuario;
                            cargo.FechaCreacion = DateTime.Now;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilScoringCargo.Add(cargo);
                    }

                    foreach (var item in dto.Cargo.CargoCoefiente)
                    {
                        ProgramaGeneralPerfilCargoCoeficiente cargo;
                        cargo = _unitOfWork.ProgramaGeneralPerfilCargoCoeficienteRepository.ObtenerPorId(item.Id);
                        if (cargo != null)
                        {
                            cargo.Nombre = item.Nombre;
                            cargo.Coeficiente = item.Coeficiente;
                            cargo.IdSelect = item.IdSelect;
                            cargo.Columna = item.IdColumna;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            cargo = new ProgramaGeneralPerfilCargoCoeficiente();
                            cargo.Nombre = item.Nombre;
                            cargo.IdPgeneral = item.IdPGeneral;
                            cargo.Coeficiente = item.Coeficiente;
                            cargo.IdSelect = item.IdSelect;
                            cargo.Columna = item.IdColumna;
                            cargo.UsuarioCreacion = usuario;
                            cargo.UsuarioModificacion = usuario;
                            cargo.FechaCreacion = DateTime.Now;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilCargoCoeficiente.Add(cargo);
                    }

                    pGeneral.ProgramaGeneralPerfilScoringAtrabajo = new List<ProgramaGeneralPerfilScoringAtrabajo>();
                    pGeneral.ProgramaGeneralPerfilAtrabajoCoeficiente = new List<ProgramaGeneralPerfilAtrabajoCoeficiente>();
                    foreach (var item in dto.Trabajo.TrabajoScoring)
                    {
                        ProgramaGeneralPerfilScoringAtrabajo trabajo;
                        trabajo = _unitOfWork.ProgramaGeneralPerfilScoringAtrabajoRepository.ObtenerPorId(item.Id);
                        if (trabajo != null)
                        {
                            trabajo.Nombre = item.Nombre;
                            trabajo.IdAreaTrabajo = item.IdAreaTrabajo;
                            trabajo.IdSelect = item.IdSelect;
                            trabajo.Valor = item.Valor;
                            trabajo.Fila = item.Fila;
                            trabajo.Columna = item.Columna;
                            trabajo.Validar = item.Validar;
                            trabajo.FechaModificacion = DateTime.Now;
                            trabajo.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            trabajo = new ProgramaGeneralPerfilScoringAtrabajo();
                            trabajo.Nombre = item.Nombre;
                            trabajo.IdPgeneral = item.IdPGeneral;
                            trabajo.IdAreaTrabajo = item.IdAreaTrabajo;
                            trabajo.IdSelect = item.IdSelect;
                            trabajo.Valor = item.Valor;
                            trabajo.Fila = item.Fila;
                            trabajo.Columna = item.Columna;
                            trabajo.Validar = item.Validar;
                            trabajo.UsuarioCreacion = usuario;
                            trabajo.UsuarioModificacion = usuario;
                            trabajo.FechaCreacion = DateTime.Now;
                            trabajo.FechaModificacion = DateTime.Now;
                            trabajo.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilScoringAtrabajo.Add(trabajo);
                    }

                    foreach (var item in dto.Trabajo.TrabajoCoefiente)
                    {
                        ProgramaGeneralPerfilAtrabajoCoeficiente trabajo;
                        trabajo = _unitOfWork.ProgramaGeneralPerfilAtrabajoCoeficienteRepository.ObtenerPorId(item.Id);
                        if (trabajo != null)
                        {
                            trabajo.Nombre = item.Nombre;
                            trabajo.Coeficiente = item.Coeficiente;
                            trabajo.IdSelect = item.IdSelect;
                            trabajo.Columna = item.IdColumna;
                            trabajo.FechaModificacion = DateTime.Now;
                            trabajo.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            trabajo = new ProgramaGeneralPerfilAtrabajoCoeficiente();
                            trabajo.Nombre = item.Nombre;
                            trabajo.IdPgeneral = item.IdPGeneral;
                            trabajo.Coeficiente = item.Coeficiente;
                            trabajo.IdSelect = item.IdSelect;
                            trabajo.Columna = item.IdColumna;
                            trabajo.UsuarioCreacion = usuario;
                            trabajo.UsuarioModificacion = usuario;
                            trabajo.FechaCreacion = DateTime.Now;
                            trabajo.FechaModificacion = DateTime.Now;
                            trabajo.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilAtrabajoCoeficiente.Add(trabajo);
                    }

                    pGeneral.ProgramaGeneralPerfilScoringCategoria = new List<ProgramaGeneralPerfilScoringCategoria>();
                    pGeneral.ProgramaGeneralPerfilCategoriaCoeficiente = new List<ProgramaGeneralPerfilCategoriaCoeficiente>();
                    foreach (var item in dto.Categoria.CategoriaScoring)
                    {
                        ProgramaGeneralPerfilScoringCategoria categoria;
                        categoria = _unitOfWork.ProgramaGeneralPerfilScoringCategoriaRepository.ObtenerPorId(item.Id);
                        if (categoria != null)
                        {
                            categoria.Nombre = item.Nombre;
                            categoria.IdCategoriaOrigen = item.IdCategoriaOrigen;
                            categoria.IdSelect = item.IdSelect;
                            categoria.Valor = item.Valor;
                            categoria.Fila = item.Fila;
                            categoria.Columna = item.Columna;
                            categoria.Validar = item.Validar;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            categoria = new ProgramaGeneralPerfilScoringCategoria();
                            categoria.Nombre = item.Nombre;
                            categoria.IdPgeneral = item.IdPGeneral;
                            categoria.IdCategoriaOrigen = item.IdCategoriaOrigen;
                            categoria.IdSelect = item.IdSelect;
                            categoria.Valor = item.Valor;
                            categoria.Fila = item.Fila;
                            categoria.Columna = item.Columna;
                            categoria.Validar = item.Validar;
                            categoria.UsuarioCreacion = usuario;
                            categoria.UsuarioModificacion = usuario;
                            categoria.FechaCreacion = DateTime.Now;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilScoringCategoria.Add(categoria);
                    }

                    foreach (var item in dto.Categoria.CategoriaCoefiente)
                    {
                        ProgramaGeneralPerfilCategoriaCoeficiente categoria;
                        categoria = _unitOfWork.ProgramaGeneralPerfilCategoriaCoeficienteRepository.ObtenerPorId(item.Id);
                        if (categoria != null)
                        {
                            categoria.Nombre = item.Nombre;
                            categoria.Coeficiente = item.Coeficiente;
                            categoria.IdSelect = item.IdSelect;
                            categoria.Columna = item.IdColumna;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            categoria = new ProgramaGeneralPerfilCategoriaCoeficiente();
                            categoria.Nombre = item.Nombre;
                            categoria.IdPgeneral = item.IdPGeneral;
                            categoria.Coeficiente = item.Coeficiente;
                            categoria.IdSelect = item.IdSelect;
                            categoria.Columna = item.IdColumna;
                            categoria.UsuarioCreacion = usuario;
                            categoria.UsuarioModificacion = usuario;
                            categoria.FechaCreacion = DateTime.Now;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilCategoriaCoeficiente.Add(categoria);
                    }

                    pGeneral.ProgramaGeneralPerfilTipoDato = new List<ProgramaGeneralPerfilTipoDato>();
                    foreach (var item in dto.TipoDato)
                    {
                        ProgramaGeneralPerfilTipoDato tipoDato;
                        tipoDato = _unitOfWork.ProgramaGeneralPerfilTipoDatoRepository.ObtenerPorId(item.Id);
                        if (tipoDato != null)
                        {
                            tipoDato.IdPgeneral = dto.IdPGeneral;
                            tipoDato.IdTipoDato = item.IdTipoDato;
                            tipoDato.FechaModificacion = DateTime.Now;
                            tipoDato.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            tipoDato = new ProgramaGeneralPerfilTipoDato();
                            tipoDato.IdPgeneral = dto.IdPGeneral;
                            tipoDato.IdTipoDato = item.IdTipoDato;
                            tipoDato.UsuarioCreacion = usuario;
                            tipoDato.UsuarioModificacion = usuario;
                            tipoDato.FechaCreacion = DateTime.Now;
                            tipoDato.FechaModificacion = DateTime.Now;
                            tipoDato.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilTipoDato.Add(tipoDato);
                    }

                    pGeneral.ProgramaGeneralPerfilEscalaProbabilidad = new List<ProgramaGeneralPerfilEscalaProbabilidad>();
                    foreach (var item in dto.Escala)
                    {
                        ProgramaGeneralPerfilEscalaProbabilidad escala;
                        escala = _unitOfWork.ProgramaGeneralPerfilEscalaProbabilidadRepository.ObtenerPorId(item.Id);
                        if (escala != null)
                        {
                            escala.IdPgeneral = dto.IdPGeneral;
                            escala.ProbabilidadActual = item.ProbabilidadActual;
                            escala.ProbabilidadInicial = item.ProbabilidadInicial;
                            escala.Orden = item.Orden;
                            escala.Nombre = item.Nombre;
                            escala.FechaModificacion = DateTime.Now;
                            escala.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            escala = new ProgramaGeneralPerfilEscalaProbabilidad();
                            escala.IdPgeneral = dto.IdPGeneral;
                            escala.ProbabilidadActual = item.ProbabilidadActual;
                            escala.ProbabilidadInicial = item.ProbabilidadInicial;
                            escala.Orden = item.Orden;
                            escala.Nombre = item.Nombre;
                            escala.UsuarioCreacion = usuario;
                            escala.UsuarioModificacion = usuario;
                            escala.FechaCreacion = DateTime.Now;
                            escala.FechaModificacion = DateTime.Now;
                            escala.Estado = true;
                        }
                        pGeneral.ProgramaGeneralPerfilEscalaProbabilidad.Add(escala);
                    }

                    ProgramaGeneralPerfilIntercepto intercepto;
                    intercepto = _unitOfWork.ProgramaGeneralPerfilInterceptoRepository.ObtenerPorId(dto.Intercepto.Id);
                    if (intercepto != null)
                    {
                        intercepto.IdPgeneral = dto.IdPGeneral;
                        intercepto.PerfilIntercepto = dto.Intercepto.PerfilIntercepto;
                        intercepto.PerfilEstado = dto.Intercepto.PerfilEstado;
                        intercepto.FechaModificacion = DateTime.Now;
                        intercepto.UsuarioModificacion = usuario;
                    }
                    else
                    {
                        intercepto = new ProgramaGeneralPerfilIntercepto();
                        intercepto.IdPgeneral = dto.IdPGeneral;
                        intercepto.PerfilIntercepto = dto.Intercepto.PerfilIntercepto;
                        intercepto.PerfilEstado = dto.Intercepto.PerfilEstado;
                        intercepto.UsuarioCreacion = usuario;
                        intercepto.UsuarioModificacion = usuario;
                        intercepto.FechaCreacion = DateTime.Now;
                        intercepto.FechaModificacion = DateTime.Now;
                        intercepto.Estado = true;
                    }
                    pGeneral.ProgramaGeneralPerfilIntercepto = intercepto;
                    _unitOfWork.PGeneralRepository.Update(pGeneral);
                    _unitOfWork.Commit();
                    scope.Complete();
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Escoring Ciudad asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <param name="usuario"></param>
        /// <param name="dto"></param>
        public void EliminacionLogicoPorProgramaPCP(int idPGeneral, string usuario, CompuestoPerfilContactoProgramaDTO dto)
        {
            try
            {
                var listaBorrarPSC = _unitOfWork.ProgramaGeneralPerfilScoringCiudadRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPSC.RemoveAll(x => dto.Ciudad.CiudadScoring.Any(y => y.Id == x.Id));
                if (listaBorrarPSC != null && listaBorrarPSC.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilScoringCiudadRepository.Delete(listaBorrarPSC.Select(x => x.Id), usuario);
                }
                var listaBorrarPSM = _unitOfWork.ProgramaGeneralPerfilScoringModalidadRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPSM.RemoveAll(x => dto.Modalidad.ModalidadScoring.Any(y => y.Id == x.Id));
                if (listaBorrarPSM != null && listaBorrarPSM.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilScoringModalidadRepository.Delete(listaBorrarPSM.Select(x => x.Id), usuario);
                }
                var listaBorrarPSA = _unitOfWork.ProgramaGeneralPerfilScoringAformacionRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPSA.RemoveAll(x => dto.Formacion.FormacionScoring.Any(y => y.Id == x.Id));
                if (listaBorrarPSA != null && listaBorrarPSA.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilScoringAformacionRepository.Delete(listaBorrarPSA.Select(x => x.Id), usuario);
                }
                var listaBorrarPSI = _unitOfWork.ProgramaGeneralPerfilScoringIndustriaRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPSI.RemoveAll(x => dto.Industria.IndustriaScoring.Any(y => y.Id == x.Id));
                if (listaBorrarPSI != null && listaBorrarPSI.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilScoringIndustriaRepository.Delete(listaBorrarPSI.Select(x => x.Id), usuario);
                }
                var listaBorrarPSCargo = _unitOfWork.ProgramaGeneralPerfilScoringCargoRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPSCargo.RemoveAll(x => dto.Cargo.CargoScoring.Any(y => y.Id == x.Id));
                if (listaBorrarPSCargo != null && listaBorrarPSCargo.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilScoringCargoRepository.Delete(listaBorrarPSCargo.Select(x => x.Id), usuario);
                }
                var listaBorrarPSAT = _unitOfWork.ProgramaGeneralPerfilScoringAtrabajoRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPSAT.RemoveAll(x => dto.Trabajo.TrabajoScoring.Any(y => y.Id == x.Id));
                if (listaBorrarPSAT != null && listaBorrarPSAT.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilScoringAtrabajoRepository.Delete(listaBorrarPSAT.Select(x => x.Id), usuario);
                }
                var listaBorrarPSCategoria = _unitOfWork.ProgramaGeneralPerfilScoringCategoriaRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPSCategoria.RemoveAll(x => dto.Categoria.CategoriaScoring.Any(y => y.Id == x.Id));
                if (listaBorrarPSCategoria != null && listaBorrarPSCategoria.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilScoringCategoriaRepository.Delete(listaBorrarPSCategoria.Select(x => x.Id), usuario);
                }
                var listaBorrarPCC = _unitOfWork.ProgramaGeneralPerfilCiudadCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPCC.RemoveAll(x => dto.Ciudad.CiudadCoefiente.Any(y => y.Id == x.Id));
                if (listaBorrarPCC != null && listaBorrarPCC.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilCiudadCoeficienteRepository.Delete(listaBorrarPCC.Select(x => x.Id), usuario);
                }
                var listaBorrarPMC = _unitOfWork.ProgramaGeneralPerfilModalidadCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPMC.RemoveAll(x => dto.Modalidad.ModalidadCoefiente.Any(y => y.Id == x.Id));
                if (listaBorrarPMC != null && listaBorrarPMC.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilModalidadCoeficienteRepository.Delete(listaBorrarPMC.Select(x => x.Id), usuario);
                }
                var listaBorrarPCCo = _unitOfWork.ProgramaGeneralPerfilCategoriaCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPCCo.RemoveAll(x => dto.Categoria.CategoriaCoefiente.Any(y => y.Id == x.Id));
                if (listaBorrarPCCo != null && listaBorrarPCCo.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilCategoriaCoeficienteRepository.Delete(listaBorrarPCCo.Select(x => x.Id), usuario);
                }
                var listaBorrarPCaCo = _unitOfWork.ProgramaGeneralPerfilCargoCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPCaCo.RemoveAll(x => dto.Cargo.CargoCoefiente.Any(y => y.Id == x.Id));
                if (listaBorrarPCaCo != null && listaBorrarPCaCo.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilCargoCoeficienteRepository.Delete(listaBorrarPCaCo.Select(x => x.Id), usuario);
                }
                var listaBorrarPIC = _unitOfWork.ProgramaGeneralPerfilIndustriaCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPIC.RemoveAll(x => dto.Industria.IndustriaCoefiente.Any(y => y.Id == x.Id));
                if (listaBorrarPIC != null && listaBorrarPIC.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilIndustriaCoeficienteRepository.Delete(listaBorrarPIC.Select(x => x.Id), usuario);
                }
                var listaBorrarPAC = _unitOfWork.ProgramaGeneralPerfilAformacionCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPAC.RemoveAll(x => dto.Formacion.FormacionCoefiente.Any(y => y.Id == x.Id));
                if (listaBorrarPAC != null && listaBorrarPAC.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilAformacionCoeficienteRepository.Delete(listaBorrarPAC.Select(x => x.Id), usuario);
                }
                var listaBorrarPATC = _unitOfWork.ProgramaGeneralPerfilAtrabajoCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPATC.RemoveAll(x => dto.Trabajo.TrabajoCoefiente.Any(y => y.Id == x.Id));
                if (listaBorrarPATC != null && listaBorrarPATC.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilAtrabajoCoeficienteRepository.Delete(listaBorrarPATC.Select(x => x.Id), usuario);
                }
                var listaBorrarPTD = _unitOfWork.ProgramaGeneralPerfilTipoDatoRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPTD.RemoveAll(x => dto.TipoDato.Any(y => y.Id == x.Id));
                if (listaBorrarPTD != null && listaBorrarPTD.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilTipoDatoRepository.Delete(listaBorrarPTD.Select(x => x.Id), usuario);
                }
                var listaBorrarPEP = _unitOfWork.ProgramaGeneralPerfilEscalaProbabilidadRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarPEP.RemoveAll(x => dto.Escala.Any(y => y.Id == x.Id));
                if (listaBorrarPEP != null && listaBorrarPEP.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPerfilEscalaProbabilidadRepository.Delete(listaBorrarPEP.Select(x => x.Id), usuario);
                }
                _unitOfWork.Commit();
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda modelo predictivo
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool GuardarModeloPredictivo(CompuestoModeloPredictivoProgramaDTO dto, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
                {
                    PGeneral pGeneral = new PGeneral();

                    EliminacionLogicoPorProgramaMP(dto.IdPGeneral, usuario, dto);

                    pGeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(dto.IdPGeneral);

                    pGeneral.ModeloPredictivoIndustria = new List<ModeloPredictivoIndustria>();
                    foreach (var item in dto.Industria)
                    {
                        ModeloPredictivoIndustria industria;
                        industria = _unitOfWork.ModeloPredictivoIndustriaRepository.ObtenerPorId(item.Id);
                        if (industria != null)
                        {
                            industria.Nombre = item.Nombre;
                            industria.Valor = item.Valor;
                            industria.Validar = item.Validar;
                            industria.IdIndustria = item.IdIndustria;
                            industria.FechaModificacion = DateTime.Now;
                            industria.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            industria = new ModeloPredictivoIndustria();
                            industria.IdPgeneral = dto.IdPGeneral;
                            industria.Nombre = item.Nombre;
                            industria.Valor = item.Valor;
                            industria.Validar = item.Validar;
                            industria.IdIndustria = item.IdIndustria;
                            industria.UsuarioCreacion = usuario;
                            industria.UsuarioModificacion = usuario;
                            industria.FechaCreacion = DateTime.Now;
                            industria.FechaModificacion = DateTime.Now;
                            industria.Estado = true;
                        }
                        pGeneral.ModeloPredictivoIndustria.Add(industria);
                    }

                    pGeneral.ModeloPredictivoCargo = new List<ModeloPredictivoCargo>();
                    foreach (var item in dto.Cargo)
                    {
                        ModeloPredictivoCargo cargo;
                        cargo = _unitOfWork.ModeloPredictivoCargoRepository.ObtenerPorId(item.Id);
                        if (cargo != null)
                        {
                            cargo.Nombre = item.Nombre;
                            cargo.Valor = item.Valor;
                            cargo.Validar = item.Validar;
                            cargo.IdCargo = item.IdCargo;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            cargo = new ModeloPredictivoCargo();
                            cargo.IdPgeneral = dto.IdPGeneral;
                            cargo.Nombre = item.Nombre;
                            cargo.Valor = item.Valor;
                            cargo.Validar = item.Validar;
                            cargo.IdCargo = item.IdCargo;
                            cargo.UsuarioCreacion = usuario;
                            cargo.UsuarioModificacion = usuario;
                            cargo.FechaCreacion = DateTime.Now;
                            cargo.FechaModificacion = DateTime.Now;
                            cargo.Estado = true;
                        }
                        pGeneral.ModeloPredictivoCargo.Add(cargo);
                    }

                    pGeneral.ModeloPredictivoFormacion = new List<ModeloPredictivoFormacion>();
                    foreach (var item in dto.Formacion)
                    {
                        ModeloPredictivoFormacion formacion;
                        formacion = _unitOfWork.ModeloPredictivoFormacionRepository.ObtenerPorId(item.Id);
                        if (formacion != null)
                        {
                            formacion.Nombre = item.Nombre;
                            formacion.Valor = item.Valor;
                            formacion.Validar = item.Validar;
                            formacion.IdAreaFormacion = item.IdAreaFormacion;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            formacion = new ModeloPredictivoFormacion();
                            formacion.IdPgeneral = dto.IdPGeneral;
                            formacion.Nombre = item.Nombre;
                            formacion.Valor = item.Valor;
                            formacion.Validar = item.Validar;
                            formacion.IdAreaFormacion = item.IdAreaFormacion;
                            formacion.UsuarioCreacion = usuario;
                            formacion.UsuarioModificacion = usuario;
                            formacion.FechaCreacion = DateTime.Now;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.Estado = true;
                        }
                        pGeneral.ModeloPredictivoFormacion.Add(formacion);
                    }

                    pGeneral.ModeloPredictivoTrabajo = new List<ModeloPredictivoTrabajo>();
                    foreach (var item in dto.Trabajo)
                    {
                        ModeloPredictivoTrabajo formacion;
                        formacion = _unitOfWork.ModeloPredictivoTrabajoRepository.ObtenerPorId(item.Id);
                        if (formacion != null)
                        {
                            formacion.Nombre = item.Nombre;
                            formacion.Valor = item.Valor;
                            formacion.Validar = item.Validar;
                            formacion.IdAreaTrabajo = item.IdAreaTrabajo;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            formacion = new ModeloPredictivoTrabajo();
                            formacion.IdPgeneral = dto.IdPGeneral;
                            formacion.Nombre = item.Nombre;
                            formacion.Valor = item.Valor;
                            formacion.Validar = item.Validar;
                            formacion.IdAreaTrabajo = item.IdAreaTrabajo;
                            formacion.UsuarioCreacion = usuario;
                            formacion.UsuarioModificacion = usuario;
                            formacion.FechaCreacion = DateTime.Now;
                            formacion.FechaModificacion = DateTime.Now;
                            formacion.Estado = true;
                        }
                        pGeneral.ModeloPredictivoTrabajo.Add(formacion);
                    }

                    pGeneral.ModeloPredictivoCategoriaDato = new List<ModeloPredictivoCategoriaDato>();
                    foreach (var item in dto.CategoriaDato)
                    {
                        ModeloPredictivoCategoriaDato categoria;
                        categoria = _unitOfWork.ModeloPredictivoCategoriaDatoRepository.ObtenerPorId(item.Id);
                        if (categoria != null)
                        {
                            categoria.Nombre = item.Nombre;
                            categoria.Valor = item.Valor;
                            categoria.Validar = item.Validar;
                            categoria.IdCategoriaOrigen = item.IdCategoriaOrigen;
                            categoria.IdSubCategoriaDato = item.IdSubCategoriaDato;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            categoria = new ModeloPredictivoCategoriaDato();
                            categoria.IdPgeneral = dto.IdPGeneral;
                            categoria.Nombre = item.Nombre;
                            categoria.Valor = item.Valor;
                            categoria.Validar = item.Validar;
                            categoria.IdCategoriaOrigen = item.IdCategoriaOrigen;
                            categoria.IdSubCategoriaDato = item.IdSubCategoriaDato;
                            categoria.UsuarioCreacion = usuario;
                            categoria.UsuarioModificacion = usuario;
                            categoria.FechaCreacion = DateTime.Now;
                            categoria.FechaModificacion = DateTime.Now;
                            categoria.Estado = true;
                        }
                        pGeneral.ModeloPredictivoCategoriaDato.Add(categoria);
                    }

                    pGeneral.ModeloPredictivoTipoDato = new List<ModeloPredictivoTipoDato>();
                    foreach (var item in dto.TipoDato)
                    {
                        ModeloPredictivoTipoDato tipoDato;
                        tipoDato = _unitOfWork.ModeloPredictivoTipoDatoRepository.ObtenerPorId(item.Id);
                        if (tipoDato != null)
                        {
                            tipoDato.IdPgeneral = dto.IdPGeneral;
                            tipoDato.IdTipoDato = item.IdTipoDato;
                            tipoDato.FechaModificacion = DateTime.Now;
                            tipoDato.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            tipoDato = new ModeloPredictivoTipoDato();
                            tipoDato.IdPgeneral = dto.IdPGeneral;
                            tipoDato.IdTipoDato = item.IdTipoDato;
                            tipoDato.UsuarioCreacion = usuario;
                            tipoDato.UsuarioModificacion = usuario;
                            tipoDato.FechaCreacion = DateTime.Now;
                            tipoDato.FechaModificacion = DateTime.Now;
                            tipoDato.Estado = true;
                        }
                        pGeneral.ModeloPredictivoTipoDato.Add(tipoDato);
                    }

                    pGeneral.ModeloPredictivoEscalaProbabilidad = new List<ModeloPredictivoEscalaProbabilidad>();
                    foreach (var item in dto.Escala)
                    {
                        ModeloPredictivoEscalaProbabilidad escala;
                        escala = _unitOfWork.ModeloPredictivoEscalaProbabilidadRepository.ObtenerPorId(item.Id);
                        if (escala != null)
                        {
                            escala.IdPgeneral = dto.IdPGeneral;
                            escala.Orden = item.Orden;
                            escala.Nombre = item.Nombre;
                            escala.ProbabilidadActual = item.ProbabilidadActual;
                            escala.ProbabilidaIinicial = item.ProbabilidaIInicial;
                            escala.FechaModificacion = DateTime.Now;
                            escala.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            escala = new ModeloPredictivoEscalaProbabilidad();
                            escala.IdPgeneral = dto.IdPGeneral;
                            escala.Orden = item.Orden;
                            escala.Nombre = item.Nombre;
                            escala.ProbabilidadActual = item.ProbabilidadActual;
                            escala.ProbabilidaIinicial = item.ProbabilidaIInicial;
                            escala.UsuarioCreacion = usuario;
                            escala.UsuarioModificacion = usuario;
                            escala.FechaCreacion = DateTime.Now;
                            escala.FechaModificacion = DateTime.Now;
                            escala.Estado = true;
                        }
                        pGeneral.ModeloPredictivoEscalaProbabilidad.Add(escala);
                    }

                    ModeloPredictivo modelo;
                    modelo = _unitOfWork.ModeloPredictivoRepository.ObtenerPorId(dto.Intercepto.Id);
                    if (modelo != null)
                    {
                        modelo.IdPgeneral = dto.IdPGeneral;
                        modelo.PeIntercepto = dto.Intercepto.PeIntercepto;
                        modelo.PeEstado = dto.Intercepto.PeEstado;
                        modelo.FechaModificacion = DateTime.Now;
                        modelo.UsuarioModificacion = usuario;
                    }
                    else
                    {
                        modelo = new ModeloPredictivo();
                        modelo.IdPgeneral = dto.IdPGeneral;
                        modelo.PeIntercepto = dto.Intercepto.PeIntercepto;
                        modelo.PeEstado = dto.Intercepto.PeEstado;
                        modelo.UsuarioCreacion = usuario;
                        modelo.UsuarioModificacion = usuario;
                        modelo.FechaCreacion = DateTime.Now;
                        modelo.FechaModificacion = DateTime.Now;
                        modelo.Estado = true;
                    }
                    pGeneral.ModeloPredictivo = modelo;

                    ModeloGeneralPGeneral asociado = _unitOfWork.ModeloGeneralPGeneralRepository.ObtenerPorIdPGeneral(dto.IdPGeneral);
                    if (asociado != null && asociado.IdModeloGeneral != null)
                    {
                        ModeloGeneral modeloPrograma = _unitOfWork.ModeloGeneralRepository.ObtenerPorId(asociado.IdModeloGeneral.Value);
                        ModeloGeneral nuevo = new ModeloGeneral();
                        nuevo.PeIntercepto = modeloPrograma.PeIntercepto;
                        nuevo.Nombre = modeloPrograma.Nombre;
                        nuevo.PeEstado = modeloPrograma.PeEstado;
                        nuevo.PeVersion = 0;
                        nuevo.IdPadre = modeloPrograma.Id;
                        nuevo.UsuarioCreacion = usuario;
                        nuevo.UsuarioModificacion = usuario;
                        nuevo.FechaCreacion = DateTime.Now;
                        nuevo.FechaModificacion = DateTime.Now;
                        nuevo.Estado = true;

                        modeloPrograma.PeVersion = modeloPrograma.PeVersion + 1;
                        modeloPrograma.FechaModificacion = DateTime.Now;
                        modeloPrograma.UsuarioModificacion = usuario;
                        _unitOfWork.ModeloGeneralRepository.Add(nuevo);
                        _unitOfWork.ModeloGeneralRepository.Update(modeloPrograma);

                        asociado.IdModeloGeneral = nuevo.Id;
                        asociado.FechaModificacion = DateTime.Now;
                        asociado.UsuarioModificacion = usuario;
                        _unitOfWork.ModeloGeneralPGeneralRepository.Update(asociado);
                    }
                    _unitOfWork.PGeneralRepository.Update(pGeneral);
                    _unitOfWork.Commit();
                    scope.Complete();
                }

                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina logica por programa Modelo Predictivo
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorProgramaMP(int idPGeneral, string usuario, CompuestoModeloPredictivoProgramaDTO dto)
        {
            try
            {
                var listaBorrarI = _unitOfWork.ModeloPredictivoIndustriaRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarI.RemoveAll(x => dto.Industria.Any(y => y.Id == x.Id));
                if (listaBorrarI != null && listaBorrarI.Count() > 0)
                {
                    _unitOfWork.ModeloPredictivoIndustriaRepository.Delete(listaBorrarI.Select(x => x.Id), usuario);
                }

                var listaBorrarC = _unitOfWork.ModeloPredictivoCargoRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarC.RemoveAll(x => dto.Cargo.Any(y => y.Id == x.Id));
                if (listaBorrarC != null && listaBorrarC.Count() > 0)
                {
                    _unitOfWork.ModeloPredictivoCargoRepository.Delete(listaBorrarC.Select(x => x.Id), usuario);
                }

                var listaBorrarF = _unitOfWork.ModeloPredictivoFormacionRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarF.RemoveAll(x => dto.Formacion.Any(y => y.Id == x.Id));
                if (listaBorrarF != null && listaBorrarF.Count() > 0)
                {
                    _unitOfWork.ModeloPredictivoFormacionRepository.Delete(listaBorrarF.Select(x => x.Id), usuario);
                }

                var listaBorrarT = _unitOfWork.ModeloPredictivoTrabajoRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarT.RemoveAll(x => dto.Trabajo.Any(y => y.Id == x.Id));
                if (listaBorrarT != null && listaBorrarT.Count() > 0)
                {
                    _unitOfWork.ModeloPredictivoTrabajoRepository.Delete(listaBorrarT.Select(x => x.Id), usuario);
                }

                var listaBorrarCD = _unitOfWork.ModeloPredictivoCategoriaDatoRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarCD.RemoveAll(x => dto.CategoriaDato.Any(y => y.Id == x.Id));
                if (listaBorrarCD != null && listaBorrarCD.Count() > 0)
                {
                    _unitOfWork.ModeloPredictivoCategoriaDatoRepository.Delete(listaBorrarCD.Select(x => x.Id), usuario);
                }

                var listaBorrarTD = _unitOfWork.ModeloPredictivoTipoDatoRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrarTD.RemoveAll(x => dto.TipoDato.Any(y => y.Id == x.Id));
                if (listaBorrarTD != null && listaBorrarTD.Count() > 0)
                {
                    _unitOfWork.ModeloPredictivoTipoDatoRepository.Delete(listaBorrarTD.Select(x => x.Id), usuario);
                }
                _unitOfWork.Commit();
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda un PreRequisito en Especifico
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> Entidad - ProgramaGeneralPrerequisito </returns>
        public ProgramaGeneralPrerequisito GuardarPreRequisitos(CompuestoPreRequisitoModalidadAlternaDTO dto, string usuario)
        {
            try
            {
                bool flagRequisitos = false;
                List<ProgramaGeneralPrerequisitoModalidad> modalidadPreRequisito;

                ProgramaGeneralPrerequisito preRequisito;
                preRequisito = _unitOfWork.ProgramaGeneralPrerequisitoRepository.ObtenerPorId(dto.IdPreRequisito);
                if (preRequisito != null)
                {
                    preRequisito.Nombre = dto.NombrePreRequisito;
                    preRequisito.Tipo = dto.Tipo;
                    preRequisito.UsuarioModificacion = usuario;
                    preRequisito.FechaModificacion = DateTime.Now;

                    EliminacionLogicoPorBeneficioPGP(dto.IdPreRequisito, usuario, dto.Modalidades);
                }
                else
                {
                    preRequisito = new ProgramaGeneralPrerequisito();
                    preRequisito.Nombre = dto.NombrePreRequisito;
                    preRequisito.IdPgeneral = dto.IdPGeneral;
                    preRequisito.Tipo = dto.Tipo;
                    preRequisito.Orden = dto.Orden;
                    preRequisito.UsuarioCreacion = usuario;
                    preRequisito.UsuarioModificacion = usuario;
                    preRequisito.FechaCreacion = DateTime.Now;
                    preRequisito.FechaModificacion = DateTime.Now;
                    preRequisito.Estado = true;
                    flagRequisitos = true;
                }
                modalidadPreRequisito = new List<ProgramaGeneralPrerequisitoModalidad>();
                foreach (var subItem in dto.Modalidades)
                {
                    ProgramaGeneralPrerequisitoModalidad preRequisitoModalidad;
                    preRequisitoModalidad = _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.ObtenerPorId(subItem.Id.Value);
                    if (preRequisitoModalidad != null)
                    {
                        preRequisitoModalidad.Nombre = subItem.Nombre;
                        preRequisitoModalidad.IdModalidadCurso = subItem.IdModalidad;
                        preRequisitoModalidad.IdPgeneral = dto.IdPGeneral;
                        preRequisito.UsuarioModificacion = usuario;
                        preRequisito.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        preRequisitoModalidad = new ProgramaGeneralPrerequisitoModalidad();
                        preRequisitoModalidad.Nombre = subItem.Nombre;
                        preRequisitoModalidad.IdModalidadCurso = subItem.IdModalidad;
                        preRequisitoModalidad.IdPgeneral = dto.IdPGeneral;
                        preRequisitoModalidad.UsuarioCreacion = usuario;
                        preRequisitoModalidad.UsuarioModificacion = usuario;
                        preRequisitoModalidad.FechaCreacion = DateTime.Now;
                        preRequisitoModalidad.FechaModificacion = DateTime.Now;
                        preRequisitoModalidad.Estado = true;
                    }
                    modalidadPreRequisito.Add(preRequisitoModalidad);
                }
                preRequisito.ProgramaGeneralPrerequisitoModalidads = modalidadPreRequisito;
                if (flagRequisitos)
                {
                    _unitOfWork.ProgramaGeneralPrerequisitoRepository.Add(preRequisito);
                }
                else
                {
                    _unitOfWork.ProgramaGeneralPrerequisitoRepository.Update(preRequisito);
                }
                flagRequisitos = false;
                _unitOfWork.Commit();

                return preRequisito;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina (Actualiza estado a false ) todas las modalidades asociados a un PreRequisito segun un PreRequisito Padre
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorBeneficioPGP(int idPreRequisito, string usuario, List<ModalidadCursoProblemaDTO> lista)
        {
            try
            {
                var listaBorrar = _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.ObtenerPorIdPGeneralPreRequisito(idPreRequisito).ToList();
                listaBorrar.RemoveAll(x => lista.Any(y => y.Id == x.Id));
                if (listaBorrar != null && listaBorrar.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                    _unitOfWork.Commit();
                }
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Asocia Programas Asociados
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool AsociarProgramasAsociados(ProgramaRelacionadoDTO dto, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    EliminarLogicoPorPrograma(dto.IdPGeneral, usuario, dto.Cursos);

                    foreach (var item in dto.Cursos)
                    {
                        PgeneralRelacionado? relacionados = _unitOfWork.PGeneralRelacionadoRepository.ObtenerPorId(item.Id);
                        if (relacionados != null)
                        {
                            relacionados.IdPgeneralRelacionado = item.IdRelacionado;
                            relacionados.UsuarioModificacion = usuario;
                            relacionados.FechaModificacion = DateTime.Now;
                            relacionados.Estado = true;
                            _unitOfWork.PGeneralRelacionadoRepository.Update(relacionados);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            relacionados = new PgeneralRelacionado();
                            relacionados.IdPgeneral = dto.IdPGeneral;
                            relacionados.IdPgeneralRelacionado = item.IdRelacionado;
                            relacionados.UsuarioCreacion = usuario;
                            relacionados.UsuarioModificacion = usuario;
                            relacionados.FechaCreacion = DateTime.Now;
                            relacionados.FechaModificacion = DateTime.Now;
                            relacionados.Estado = true;
                            _unitOfWork.PGeneralRelacionadoRepository.Add(relacionados);
                            _unitOfWork.Commit();
                        }
                    }
                    scope.Complete();
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los programas Relacionados Por Programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        /// <exception cref="Exception"></exception>
        public void EliminarLogicoPorPrograma(int idPGeneral, string usuario, List<CursoRelacionadoProgramaDTO> nuevos)
        {
            try
            {
                var listaBorrar = _unitOfWork.PGeneralRelacionadoRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
                if (listaBorrar != null && listaBorrar.Count() > 0)
                {
                    _unitOfWork.PGeneralRelacionadoRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                    _unitOfWork.Commit();
                }
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Listar modalidades del curso
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista DTO - List<PGeneralModalidadDTO> </returns>
        public IEnumerable<PGeneralModalidadDTO> ListarModalidadesCurso(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralModalidadRepository.ListarModalidadesCurso(idPGeneral).ToList();
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene pGeneralComboModuloDTO para el moduloo de Programa General
        /// </summary>
        /// <returns> PGeneralComboConfiguracionPlantillaModuloDTO </returns>
        public async Task<PGeneralComboConfiguracionPlantillaModuloDTO> ObtenerCombosConfiguracionPlantillaAsync()
        {
            try
            {
                var task_plantillaCertificado = _unitOfWork.PlantillaRepository.ObtenerPlantillaCertificadoAsync();
                var task_modalidadCurso = _unitOfWork.ModalidadCursoRepository.ObtenerComboAsync();
                var task_estadoMatricula = _unitOfWork.EstadoMatriculaRepository.ObtenerComboAsync();
                var task_operadorComparacion = _unitOfWork.OperadorComparacionRepository.ObtenerComboAsync();
                var task_subEstadoMatricula = _unitOfWork.SubEstadoMatriculaRepository.ObtenerSubEstadoMatriculaFiltroAsync();
                var task_pais = _unitOfWork.PaisRepository.ObtenerListaPaisAsync();
                var task_beneficioDatoAdicional = _unitOfWork.BeneficioDatoAdicionalRepository.ObtenerComboAsync();
                //var task_pGeneralVersionPrograma = _unitOfWork.PGeneralVersionProgramaRepository.ObtenerVersionesProgramaPorPGeneralAsync(idPGeneral);

                var pGeneralComboConfiguracionPlantillaModuloDTO = new PGeneralComboConfiguracionPlantillaModuloDTO();

                pGeneralComboConfiguracionPlantillaModuloDTO.PlantilaCertificadoConstancia = await task_plantillaCertificado;
                pGeneralComboConfiguracionPlantillaModuloDTO.ModalidadCurso = await task_modalidadCurso;
                pGeneralComboConfiguracionPlantillaModuloDTO.EstadoMatricula = await task_estadoMatricula;
                pGeneralComboConfiguracionPlantillaModuloDTO.OperadorComparacion = await task_operadorComparacion;
                pGeneralComboConfiguracionPlantillaModuloDTO.SubEstadoMatricula = await task_subEstadoMatricula;
                pGeneralComboConfiguracionPlantillaModuloDTO.Pais = await task_pais;
                pGeneralComboConfiguracionPlantillaModuloDTO.BeneficioDatoAdicional = await task_beneficioDatoAdicional;
                //pGeneralComboConfiguracionPlantillaModuloDTO.PGeneralVersionPrograma = await task_pGeneralVersionPrograma;

                return (pGeneralComboConfiguracionPlantillaModuloDTO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: POST  
        /// Autor: Gilmer Qm.
        /// Fecha: 13/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Agrega registro nuevo de programa General
        /// </summary>
        /// <param name="programaGeneralDTO">Información Compuesta de Programa General</param>
        /// <returns> PGeneralDTO </returns> 
        public PGeneralDTO Insertar(ProgramaGeneralDTO programaGeneralDTO, string usuario)
        {
            try
            {
                  TroncalPgeneral troncalPgeneral = new TroncalPgeneral();
                troncalPgeneral.IdTroncalPartner = _unitOfWork.PartnerPwRepository.ObtenerPartnerAnterior(programaGeneralDTO.PGeneral.IdPartner.Value);
                troncalPgeneral.IdArea = _unitOfWork.AreaCapacitacionRepository.ObtenerAreaCapacitacionAnterior(programaGeneralDTO.PGeneral.IdArea.Value);
                troncalPgeneral.IdSubArea = _unitOfWork.SubAreaCapacitacionRepository.ObtenerSubAreaCapacitacionAnterior(programaGeneralDTO.PGeneral.IdSubArea.Value);
                troncalPgeneral.Codigo = programaGeneralDTO.PGeneral.Codigo;
                troncalPgeneral.IdBusqueda = 0;
                troncalPgeneral.Nombre = programaGeneralDTO.PGeneral.Nombre;
                troncalPgeneral.FechaCreacion = DateTime.Now;
                troncalPgeneral.FechaModificacion = DateTime.Now;
                troncalPgeneral.UsuarioCreacion = usuario;
                troncalPgeneral.UsuarioModificacion = usuario;
                troncalPgeneral.Estado = true;
                var resultadoTroncal = _unitOfWork.TroncalPgeneralRepository.Add(troncalPgeneral);
                _unitOfWork.Commit();

                troncalPgeneral = _mapper.Map<TroncalPgeneral>(resultadoTroncal);
                troncalPgeneral.IdBusqueda = troncalPgeneral.Id;
                _unitOfWork.DetachAll();
                _unitOfWork.TroncalPgeneralRepository.Update(troncalPgeneral);
                PGeneral pgeneral = new PGeneral();

                pgeneral = _mapper.Map<PGeneral>(programaGeneralDTO.PGeneral);
                pgeneral.IdPgeneral = troncalPgeneral.Id;
                pgeneral.IdBusqueda = troncalPgeneral.Id;
                pgeneral.UsuarioCreacion = usuario;
                pgeneral.UsuarioModificacion = usuario;

                if (!string.IsNullOrEmpty(programaGeneralDTO.PGeneral.LogoPrograma))
                {
                    pgeneral.LogoPrograma = programaGeneralDTO.PGeneral.LogoPrograma;
                    pgeneral.UrlLogoPrograma = "https://repositorioweb.blob.core.windows.net/repositorioweb/img/programas/logo/" + programaGeneralDTO.PGeneral.LogoPrograma.Replace(" ", "%20");
                }
                var idPgeneral = _unitOfWork.PGeneralRepository.InsertaPGeneralSinIdentity(pgeneral);
                pgeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(idPgeneral);
                pgeneral.PGeneralParametroSeoPw = new List<PgeneralParametroSeoPw>();
                pgeneral.ProgramaAreaRelacionada = new List<ProgramaAreaRelacionadum>();
                pgeneral.PGeneralExpositor = new List<PGeneralExpositor>();
                pgeneral.CarreraPreRequisitoPgeneral = new List<CarreraPreRequisitoPgeneral>();
                pgeneral.SuscripcionProgramaGeneral = new List<SuscripcionProgramaGeneral>();
                pgeneral.PgeneralModalidad = new List<PgeneralModalidad>();
                pgeneral.PgeneralVersionPrograma = new List<PgeneralVersionPrograma>();

                List<PgeneralModalidad> pgeneralModalidads = new List<PgeneralModalidad>();

                pgeneral.PGeneralParametroSeoPw = programaGeneralDTO.DetallesProgramaGeneral.ParametrosSeo.Select(x =>
                new PgeneralParametroSeoPw
                {
                    Descripcion = x.Descripcion,
                    IdParametroSeo = x.IdParametroSeo,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true,
                }).ToList();

                pgeneral.PGeneralExpositor = programaGeneralDTO.DetallesProgramaGeneral.Expositores.Select((x, index) => new PGeneralExpositor
                {
                    IdExpositor = x,
                    IdModalidadCurso=null,
                    Posicion = index,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true,
                }).ToList();

                if (programaGeneralDTO.DetallesProgramaGeneral.Docentes.Count > 0 && programaGeneralDTO.DetallesProgramaGeneral.Docentes != null)
                {
                    int posicionActual = pgeneral.PGeneralExpositor.Count;

                    var listaProveedores = programaGeneralDTO.DetallesProgramaGeneral.Docentes.Select((x, index) => new PGeneralExpositor
                    {

                        IdExpositor = x,
                        IdModalidadCurso = 1,
                        Posicion = posicionActual + index,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true,

                    });

                    pgeneral.PGeneralExpositor.AddRange(listaProveedores);

                }


                pgeneral.CarreraPreRequisitoPgeneral = programaGeneralDTO.DetallesProgramaGeneral.PreRequisitos.Select((x, index) => new CarreraPreRequisitoPgeneral
                {
                    IdPgeneralPreRequisito = x,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true,
                }).ToList();

                //pgeneral.ProgramaAreaRelacionada = programaGeneralDTO.DetallesProgramaGeneral.ProgramaAreasRelacionadas.Select(x => new ProgramaAreaRelacionadum
                //{
                //    IdAreaCapacitacion = x,
                //    UsuarioCreacion = usuario,
                //    UsuarioModificacion = usuario,
                //    FechaCreacion = DateTime.Now,
                //    FechaModificacion = DateTime.Now,
                //    Estado = true,
                //}).ToList();

                //pgeneral.SuscripcionProgramaGeneral = programaGeneralDTO.DetallesProgramaGeneral.Suscripciones.Select(x => new SuscripcionProgramaGeneral
                //{
                //    Titulo = x.Titulo,
                //    Descripcion = x.Descripcion,
                //    OrdenBeneficio = x.OrdenBeneficio,
                //    UsuarioCreacion = usuario,
                //    UsuarioModificacion = usuario,
                //    FechaCreacion = DateTime.Now,
                //    FechaModificacion = DateTime.Now,
                //    Estado = true,
                //}).ToList();
                pgeneral.PgeneralVersionPrograma = programaGeneralDTO.DetallesProgramaGeneral.PgeneralVersionPrograma.Select(x => new PgeneralVersionPrograma
                {
                    Duracion = x.Duracion,
                    IdVersionPrograma = x.IdVersionPrograma,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true,
                }).ToList();
                pgeneral.PgeneralModalidad = programaGeneralDTO.DetallesProgramaGeneral.Modalidad.Select(x => new PgeneralModalidad
                {
                    IdModalidadCurso = x,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true,
                }).ToList();

                _unitOfWork.PGeneralRepository.Update(pgeneral);
                _unitOfWork.Commit();

                foreach (var itemMontoPago in programaGeneralDTO.DetallesProgramaGeneral.MontoPago)
                {
                    MontoPago montoPago = new MontoPago()
                    {
                        Precio = itemMontoPago.Precio,
                        PrecioLetras = itemMontoPago.PrecioLetras,
                        IdMoneda = itemMontoPago.IdMoneda,
                        Matricula = itemMontoPago.Matricula,
                        Cuotas = itemMontoPago.Cuotas,
                        NroCuotas = itemMontoPago.NroCuotas,
                        IdTipoDescuento = itemMontoPago.IdTipoDescuento,
                        IdPrograma = pgeneral.Id,
                        IdTipoPago = itemMontoPago.IdTipoPago,
                        IdPais = itemMontoPago.IdPais,
                        Vencimiento = itemMontoPago.Vencimiento,
                        PrimeraCuota = itemMontoPago.PrimeraCuota,
                        CuotaDoble = itemMontoPago.CuotaDoble,
                        Descripcion = itemMontoPago.Descripcion,
                        VisibleWeb = itemMontoPago.VisibleWeb,
                        Paquete = itemMontoPago.Paquete,
                        PorDefecto = itemMontoPago.PorDefecto,
                        MontoDescontado = itemMontoPago.MontoDescontado,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true,
                    };
                    montoPago.MontoPagoPlataforma = itemMontoPago.PlataformasPagos.Select(x => new MontoPagoPlataforma
                    {
                        IdPlataformaPago = x,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true,
                    }).ToList();

                    montoPago.MontoPagoSuscripcion = itemMontoPago.SuscripcionesPagos.Select(x => new MontoPagoSuscripcion
                    {
                        IdSuscripcionProgramaGeneral = x,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true,
                    }).ToList();

                    _unitOfWork.MontoPagoRepository.Add(montoPago);
                    _unitOfWork.Commit();
                }
                if (programaGeneralDTO.DetallesProgramaGeneral.ConfiguracionPlantilla != null)
                {
                    foreach (var itemConfiguracionPlantilla in programaGeneralDTO.DetallesProgramaGeneral.ConfiguracionPlantilla)
                    {
                        PgeneralConfiguracionPlantilla pGeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantilla()
                        {
                            IdPgeneral = pgeneral.Id,
                            IdPlantillaFrontal = itemConfiguracionPlantilla.IdPlantillaFrontal,
                            IdPlantillaPosterior = itemConfiguracionPlantilla.IdPlantillaPosterior,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };

                        pGeneralConfiguracionPlantilla.PGeneralConfiguracionPlantillaDetalle = itemConfiguracionPlantilla.Detalle.Select(x => new PGeneralConfiguracionPlantillaDetalle
                        {
                            IdOperadorComparacion = x.IdOperadorComparacion,
                            IdModalidadCurso = x.IdModalidadCurso,
                            NotaAprobatoria = x.NotaAprobatoria,
                            DeudaPendiente = x.DeudaPendiente,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        }).ToList();
                        _unitOfWork.PGeneralConfiguracionPlantillaRepository.Add(pGeneralConfiguracionPlantilla);
                        _unitOfWork.Commit();
                    }
                }
                if (programaGeneralDTO.DetallesProgramaGeneral.PgeneralCodigoPartner != null)
                {
                    foreach (var itemPgeneralCodigoPartner in programaGeneralDTO.DetallesProgramaGeneral.PgeneralCodigoPartner)
                    {
                        PGeneralCodigoPartner pGeneralCodigoPartner = new PGeneralCodigoPartner()
                        {
                            IdPgeneral = pgeneral.Id,
                            Codigo = itemPgeneralCodigoPartner.Codigo,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };

                        pGeneralCodigoPartner.PgeneralCodigoPartnerModalidadCurso = itemPgeneralCodigoPartner.ModalidadesCurso.Select(x => new PgeneralCodigoPartnerModalidadCurso
                        {
                            IdPgeneralCodigoPartner = pGeneralCodigoPartner.Id,
                            IdModalidadCurso = x,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        }).ToList();

                        pGeneralCodigoPartner.PgeneralCodigoPartnerVersionPrograma = itemPgeneralCodigoPartner.VersionesPrograma.Select(x => new PgeneralCodigoPartnerVersionPrograma
                        {
                            IdPgeneralCodigoPartner = pGeneralCodigoPartner.Id,
                            IdVersionPrograma = x,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        }).ToList();

                        _unitOfWork.PgeneralCodigoPartnerRepository.Add(pGeneralCodigoPartner);
                        _unitOfWork.Commit();
                    }
                }

                if (programaGeneralDTO.DetallesProgramaGeneral.PgeneralProyectoAplicacion != null)
                {
                    foreach (var pGeneralProyectoAplicacionDTO in programaGeneralDTO.DetallesProgramaGeneral.PgeneralProyectoAplicacion)
                    {
                        PgeneralProyectoAplicacion pGeneralProyectoAplicacion = new PgeneralProyectoAplicacion()
                        {
                            IdPgeneral = pgeneral.Id,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                        pGeneralProyectoAplicacion.PgeneralProyectoAplicacionProveedor = pGeneralProyectoAplicacionDTO.Proveedores.Select(x => new PgeneralProyectoAplicacionProveedor
                        {
                            IdProveedor = x,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        }).ToList();
                        pGeneralProyectoAplicacion.PgeneralProyectoAplicacionModalidad = pGeneralProyectoAplicacionDTO.Modalidades.Select(x => new PgeneralProyectoAplicacionModalidad
                        {
                            IdModalidadCurso = x,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        }).ToList();

                        _unitOfWork.PgeneralProyectoAplicacionRepository.Add(pGeneralProyectoAplicacion);
                        _unitOfWork.Commit();
                    }
                }
                if (programaGeneralDTO.DetallesProgramaGeneral.PgeneralForoAsignacionProveedor != null && programaGeneralDTO.DetallesProgramaGeneral.PgeneralForoAsignacionProveedor.Count > 0)
                {
                    //Validación para evitar la duplicidad de datos
                    List<PGeneralForoAsignacionProveedorDTO> foroAsignacionProveedorDTO = new();
                    //Armamos una lista de datos lineal para distinción de proveedores y modalidades
                    foreach (var itemForoAsignacionProveedor in programaGeneralDTO.DetallesProgramaGeneral.PgeneralForoAsignacionProveedor)
                    {
                        foreach (var proveedor in itemForoAsignacionProveedor.Proveedores)
                        {
                            foroAsignacionProveedorDTO.Add(new PGeneralForoAsignacionProveedorDTO()
                            {
                                IdPgeneral = pgeneral.Id,
                                IdModalidadCurso = itemForoAsignacionProveedor.IdModalidadCurso,
                                IdProveedor = proveedor
                            });
                        }
                    }

                    PgeneralForoAsignacionProveedor pGeneralForoAsignacionProveedor;
                    if (foroAsignacionProveedorDTO.Count > 0)
                    {
                        //Se distingue lista para evitar repetición de registros
                        var foroAsignacionProveedorFinal = foroAsignacionProveedorDTO.Select(x => new { x.IdModalidadCurso, x.IdPgeneral, x.IdProveedor }).Distinct().ToList();
                        var foroAsignacionProveedorInsert = foroAsignacionProveedorFinal.Select(x => new PgeneralForoAsignacionProveedor
                        {
                            IdPgeneral = x.IdPgeneral,
                            IdModalidadCurso = x.IdModalidadCurso,
                            IdProveedor = x.IdProveedor,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }).ToList();
                        _unitOfWork.PgeneralForoAsignacionProveedorRepository.Add(foroAsignacionProveedorInsert);
                        _unitOfWork.Commit();
                    }
                }
                return _mapper.Map<PGeneralDTO>(pgeneral);

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 134/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de Plantillas de Documentos Asociados y No Asociados del programa
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> PlantillaDocumentoDTO </returns>
        public PlantillaDocumentoDTO ObtenerDocumentosAsociadosYNoAsociados(int idPGeneral)
        {
            PlantillaDocumentoDTO documentos = new PlantillaDocumentoDTO();
            documentos.PlantillaDocumentoAsociado = _unitOfWork.DocumentoPwRepository.ObtenerDocumentosAsociados(idPGeneral);
            documentos.PlantillaDocumentoNoAsociado = _unitOfWork.DocumentoPwRepository.ObtenerDocumentosNoAsociados(idPGeneral);
            return (documentos);
        }
        /// Autor: Gilmer Qm
        /// Fecha: 13/06/2023
        /// Version: 1.0
        /// <summary>
        /// Asocia documentos del PGeneral
        /// </summary>
        /// <param name="documentoAsociadoProgramaDTO"> Objeto que contiene los datos para su asociacion </param>
        /// <returns> bool </returns>
        public bool ActualizarDocumentosAsociados(DocumentoAsociadoProgramaDTO documentoAsociadoProgramaDTO, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    /*Obtenemos toda los detalles con estado=1*/
                    var listaBorrar = _unitOfWork.PGeneralDocumentoPwRepository.ObtenerPorIdPGeneral(documentoAsociadoProgramaDTO.IdPGeneral);

                    /*Separamos todos los detalles que se eliminaran logicamente*/
                    listaBorrar.RemoveAll(x => documentoAsociadoProgramaDTO.PGeneralDocumentoPws.Any(y => y.Id == x.Id));

                    /*Eliminamos logicamente los detalles separados*/
                    _unitOfWork.PGeneralDocumentoPwRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                    _unitOfWork.Commit();

                    /*Guardamos el IdDocumento (PK de pla.T_Documento_PW) de los detalles eliminados para Desasignarlos (Asignado=0)*/
                    var listaBorrado = listaBorrar.Select(x => x.IdDocumento).ToList();

                    /*Recorremos todos lo PGeneralDocumento_Pw enviados desde vistas*/
                    foreach (var item in documentoAsociadoProgramaDTO.PGeneralDocumentoPws)
                    {
                        DocumentoPw documento = _unitOfWork.DocumentoPwRepository.ObtenerPorId(item.IdDocumento);
                        documento.PGeneralDocumentoPws = new List<PGeneralDocumentoPw>();

                        PGeneralDocumentoPw pgeneralDocumento;
                        if (_unitOfWork.PGeneralDocumentoPwRepository.Exist(item.Id.Value))
                        {
                            pgeneralDocumento = _unitOfWork.PGeneralDocumentoPwRepository.ObtenerPorId(item.Id.Value);
                            pgeneralDocumento.IdDocumento = item.IdDocumento;
                            pgeneralDocumento.IdPgeneral = documentoAsociadoProgramaDTO.IdPGeneral;
                            pgeneralDocumento.UsuarioModificacion = usuario;
                            pgeneralDocumento.FechaModificacion = DateTime.Now;
                            documento.PGeneralDocumentoPws.Add(pgeneralDocumento);
                        }
                        else
                        {
                            pgeneralDocumento = new PGeneralDocumentoPw();
                            pgeneralDocumento.IdDocumento = item.IdDocumento;
                            pgeneralDocumento.IdPgeneral = documentoAsociadoProgramaDTO.IdPGeneral;
                            pgeneralDocumento.UsuarioCreacion = usuario;
                            pgeneralDocumento.UsuarioModificacion = usuario;
                            pgeneralDocumento.FechaCreacion = DateTime.Now;
                            pgeneralDocumento.FechaModificacion = DateTime.Now;
                            pgeneralDocumento.Estado = true;
                            documento.PGeneralDocumentoPws.Add(pgeneralDocumento);
                        }

                        documento.Asignado = true;
                        documento.UsuarioModificacion = usuario;
                        documento.FechaModificacion = DateTime.Now;
                        var actualizado = _unitOfWork.DocumentoPwRepository.Update(documento);
                        _unitOfWork.Commit();
                        _unitOfWork.DetachAll();
                    }
                    foreach (var item in listaBorrado)
                    {
                        DocumentoPw documento = _unitOfWork.DocumentoPwRepository.ObtenerPorId(item);
                        documento.Asignado = false;
                        documento.UsuarioModificacion = usuario;
                        documento.FechaModificacion = DateTime.Now;
                        _unitOfWork.DocumentoPwRepository.Update(documento);
                        _unitOfWork.Commit();
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 13/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos relacionados y no relacionados por el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"> PK T_PGeneral </param>
        /// <returns> (List<PGeneralProgramaRelacionadoDTO>, List<ComboDTO>)  </returns>
        public (List<PGeneralProgramaRelacionadoDTO>, List<ComboDTO>) ObtenerRelacionCursos(int idPGeneral)
        {
            var pGeneralProgramaRelacionados = _unitOfWork.PGeneralRelacionadoRepository.ObtenerCursosRelacionadosPorPrograma(idPGeneral).ToList();
            var pGeneralProgramaNoRelacionados = _unitOfWork.PGeneralRelacionadoRepository.ObtenerCursosNoRelacionadosPorPrograma(idPGeneral).ToList();
            return (pGeneralProgramaRelacionados, pGeneralProgramaNoRelacionados);
        }

        /// <summary>
        /// Obtiene configuracion de plantillas certificados.
        /// </summary>
        /// <param name="idPgeneral"></param>
        /// <returns> Lista PgeneralConfiguracionPlantillaDTO </returns>
        public List<PgeneralConfiguracionPlantillaDTO> ObtenerConfiguracionPlantilla(int idPgeneral)
        {
            try
            {
                return _unitOfWork.PGeneralConfiguracionPlantillaRepository.ObtenerPGeneralConfiguracionPlantillaPorIdPGeneral(idPgeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 16/06/2023
        /// <summary>
        /// Obtiene la  lista de  T_PGeneralCriterioEvaluacion por el (FK) IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> List<PGeneralCriterioEvaluacionDTO> </returns>
        public List<PGeneralCriterioEvaluacionDTO> ObtenerPGcriteriosEvaluacionAOnline(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralCriterioEvaluacionRepository.ObtenerPGcriteriosEvaluacionAOnline(idPGeneral);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> Lista PGeneralCriterioEvaluacionDTO </returns>
        public List<PGeneralCriterioEvaluacionDTO> ObtenerPGcriteriosEvaluacionOnline(int idPgeneral)
        {
            try
            {
                return _unitOfWork.PGeneralCriterioEvaluacionRepository.ObtenerPGcriteriosEvaluacionOnline(idPgeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> Lista PGeneralCriterioEvaluacionDTO </returns>
        public List<PGeneralCriterioEvaluacionDTO> ObtenerPGcriteriosEvaluacionPresencial(int idPgeneral)
        {
            try
            {
                return _unitOfWork.PGeneralCriterioEvaluacionRepository.ObtenerPGcriteriosEvaluacionPresencial(idPgeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 16/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información con detalles del PGeneral por Id
        /// </summary>
        /// <param name="idPGeneral"> (PK) Id de T_PGeneral </param>
        /// <returns>DetallesProgramasDTO</returns>
        public DetallesProgramasDTO ObtenerDetalleProgramas(int idPGeneral)
        {
            try
            {
                DetallesProgramasDTO detallePrograma = new();

                detallePrograma.ParametrosSeo = _unitOfWork.PGeneralParametroSeoPwRepository.ObtenerPgeneralParametroSeoPorIdPGeneral(idPGeneral).ToList();
                //detallePrograma.DescripcionesGenerales = _unitOfWork.PGeneralDescripcionRepository.ObtenerPGeneralDescripcionPorIdPGeneral(idPGeneral).ToList();
                //detallePrograma.DescripcionesAdicionales = _unitOfWork.AdicionalProgramaGeneralRepository.ObtenerAdicionalProgramaGeneralPorIdPGeneral(idPGeneral).ToList();

                detallePrograma.Expositores = _unitOfWork.PGeneralExpositorRepository.ObtenerPGeneralExpositorPorIdPGeneral(idPGeneral).Where(x => x.IdModalidadCurso == null)
                                                                                                                                     .Select(x => x.IdExpositor)  
                                                                                                                                     .ToList();

                //detallePrograma.Expositores = _unitOfWork.PGeneralExpositorRepository.ObtenerPGeneralExpositorPorIdPGeneral(idPGeneral).Where(x => x.IdModalidad == 2 ).ToList();

                detallePrograma.Docentes = _unitOfWork.PGeneralExpositorRepository.ObtenerPGeneralExpositorPorIdPGeneral(idPGeneral).Where(x => x.IdModalidadCurso == 1)
                                                                                                                                     .Select(x => x.IdExpositor)
                                                                                                                                     .ToList();

                detallePrograma.PreRequisitos = _unitOfWork.CarreraPreRequisitoPgeneralRepository.ObtenerCarreraPreRequisitoPGeneralPorIdPGeneral(idPGeneral).Select(x => x.IdPgeneralPrerequisito).ToList();
                detallePrograma.Modalidad = _unitOfWork.PGeneralModalidadRepository.ObtenerPGeneralModalidadPorIdPGeneral(idPGeneral).ToList().Select(x => x.IdModalidadCurso).ToList();
                //detallePrograma.ProgramaAreasRelacionadas = _unitOfWork.ProgramaAreaRelacionadaRepository.ObtenerProgramaAreaRelacionadaPorIdPGeneral(idPGeneral).Select(x => x.IdAreaCapacitacion).ToList();
                //detallePrograma.Suscripciones = _unitOfWork.SuscripcionProgramaGeneralRepository.ObtenerSuscripcionProgramaGeneralPorIdPGeneral(idPGeneral).ToList();
                detallePrograma.ConfiguracionPlantilla = _unitOfWork.PGeneralConfiguracionPlantillaRepository.ObtenerPGeneralConfiguracionPlantillaPorIdPGeneral(idPGeneral);
                detallePrograma.ConfiguracionBeneficio = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerPGeneralConfiguracionBeneficios(idPGeneral);
                detallePrograma.PgeneralVersionPrograma = _unitOfWork.PGeneralVersionProgramaRepository.ObtenerPGeneralVersionProgramaDetallePorIdPGeneral(idPGeneral).ToList();
                detallePrograma.PgeneralCodigoPartner = _unitOfWork.PgeneralCodigoPartnerRepository.ObtenerPgeneralCodigoPartnerPorIdPGeneral(idPGeneral).ToList();
                detallePrograma.PespecificoCodigoPartner = _unitOfWork.PEspecificoCodigoPartnerRepository.ObtenerPEspecificoCodigoPartner(idPGeneral);
                detallePrograma.PgeneralProyectoAplicacion = _unitOfWork.PgeneralProyectoAplicacionRepository.ObtenerPgeneralProyectoAplicacionPorIdPGeneral(idPGeneral).ToList();
                detallePrograma.PgeneralFechaInicioAonline = _unitOfWork.PGeneralRepository.ObtenerPgeneralFechaInicioAonline(idPGeneral);
                detallePrograma.PgeneralFechaInicioOnline = _unitOfWork.PGeneralRepository.ObtenerPGeneralFechaInicioOnline(idPGeneral);
                detallePrograma.PgeneralFechaInicioPresencial = _unitOfWork.PGeneralRepository.ObtenerPGeneralFechaInicioPresencial(idPGeneral);

                var sinAgrupacionPGeneralForoAsignacionProveedor = _unitOfWork.PgeneralForoAsignacionProveedorRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();

                if (sinAgrupacionPGeneralForoAsignacionProveedor != null)
                {
                    var agrupacionPGeneralForoAsignacionProveedor = sinAgrupacionPGeneralForoAsignacionProveedor.GroupBy(x => new { x.IdPgeneral, x.IdModalidadCurso }).Select(x => new PgeneralForoAsignacionProveedorAlternoDTO
                    {
                        IdModalidadCurso = x.Key.IdModalidadCurso,
                        Proveedores = x.GroupBy(y => y.IdProveedor).Select(y => y.Key).ToList(),
                    }).ToList();
                    detallePrograma.PgeneralForoAsignacionProveedor = agrupacionPGeneralForoAsignacionProveedor;
                }
                return (detallePrograma);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Carlos Crispin R.
        /// Fecha: 24/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las duraciones de los programas generales
        /// </summary>
        /// <param name="idPGeneral"> (PK) Id de T_PGeneral </param>
        /// <returns>DetallesProgramasDTO</returns>
        public IEnumerable<PGeneralVersionProgramaDetalleDTO> ObtenerDuracionProgramas(int idPGeneral)
        { 
            var Duraciones = _unitOfWork.PGeneralVersionProgramaRepository.ObtenerPGeneralVersionProgramaDetallePorIdPGeneral(idPGeneral).ToList();
            return Duraciones;
        }

        public void EliminarDetallesProgramaGeneral(ProgramaGeneralDTO programaGeneralDTO, string usuario)
        {
            try
            {
                bool flagEliminacion = false;
                /*Eliminamos los detalles T_PGeneralParametroSEO_PW*/
                var pGeneralParametroSeoPws = _unitOfWork.PGeneralParametroSeoPwRepository.ObtenerPorIdPGeneral(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();
                pGeneralParametroSeoPws.RemoveAll(x => programaGeneralDTO.DetallesProgramaGeneral.ParametrosSeo.Any(y => y.IdParametroSeo == x.IdParametroSeo));
                if (pGeneralParametroSeoPws != null && pGeneralParametroSeoPws.Count() > 0)
                {
                    flagEliminacion = true;
                    _unitOfWork.PGeneralParametroSeoPwRepository.Delete(pGeneralParametroSeoPws.Select(x => x.Id), usuario);
                }
                /*Eliminamos los detalles T_PGeneralVersionPrograma*/
                var pgeneralVersionProgramas = _unitOfWork.PGeneralVersionProgramaRepository.ObtenerPorIdPGeneral(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();
                pgeneralVersionProgramas.RemoveAll(x => programaGeneralDTO.DetallesProgramaGeneral.PgeneralVersionPrograma.Any(y => y.IdVersionPrograma == x.IdVersionPrograma));
                if (pgeneralVersionProgramas != null && pgeneralVersionProgramas.Count() > 0)
                {
                    flagEliminacion = true;
                    _unitOfWork.PGeneralVersionProgramaRepository.Delete(pgeneralVersionProgramas.Select(x => x.Id), usuario);
                }
                /*Eliminamos los detalles T_PGeneralExpositor*/
                var pGeneralExpositors = _unitOfWork.PGeneralExpositorRepository.ObtenerPorIdPGeneral(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();
                pGeneralExpositors.RemoveAll(x => x.IdModalidadCurso==null && programaGeneralDTO.DetallesProgramaGeneral.Expositores.Any(y => y == x.IdExpositor));
                /*Eliminamos los docentes de modalidad Aonline referente a Expositores*/
                pGeneralExpositors.RemoveAll(x => x.IdModalidadCurso==1 && programaGeneralDTO.DetallesProgramaGeneral.Docentes.Any(y => y == x.IdExpositor));
                if (pGeneralExpositors != null && pGeneralExpositors.Count() > 0)
                {
                    flagEliminacion = true;
                    _unitOfWork.PGeneralExpositorRepository.Delete(pGeneralExpositors.Select(x => x.Id), usuario);
                }
                /*Eliminamos los detalles TCarreraPreRequisitoPgeneral*/
                var carreraPreRequisitoPGenerals = _unitOfWork.CarreraPreRequisitoPgeneralRepository.ObtenerPorIdPGeneral(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();
                carreraPreRequisitoPGenerals.RemoveAll(x => programaGeneralDTO.DetallesProgramaGeneral.PreRequisitos.Any(y => y == x.IdPgeneralPreRequisito));
                if (carreraPreRequisitoPGenerals != null && carreraPreRequisitoPGenerals.Count() > 0)
                {
                    flagEliminacion = true;
                    _unitOfWork.CarreraPreRequisitoPgeneralRepository.Delete(carreraPreRequisitoPGenerals.Select(x => x.Id), usuario);
                }
                /*Eliminamos los detalles T_ProgramaAreaRelacionada*/
                //var programaAreaRelacionada = _unitOfWork.ProgramaAreaRelacionadaRepository.ObtenerPorIdPGeneral(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();
                //programaAreaRelacionada.RemoveAll(x => programaGeneralDTO.DetallesProgramaGeneral.ProgramaAreasRelacionadas.Any(y => y == x.IdAreaCapacitacion));
                //if (programaAreaRelacionada != null && programaAreaRelacionada.Count() > 0)
                //{
                //    flagEliminacion = true;
                //    _unitOfWork.ProgramaAreaRelacionadaRepository.Delete(programaAreaRelacionada.Select(x => x.Id), usuario);
                //}
                /*Eliminamos los detalles T_SuscripcionProgramaGeneral*/
                //var suscripcionProgramaGenerals = _unitOfWork.SuscripcionProgramaGeneralRepository.ObtenerPorIdPGeneral(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();
                //suscripcionProgramaGenerals.RemoveAll(x => programaGeneralDTO.DetallesProgramaGeneral.Suscripciones.Any(y => y.Id == x.Id));
                //if (suscripcionProgramaGenerals != null && suscripcionProgramaGenerals.Count() > 0)
                //{
                //    flagEliminacion = true;
                //    _unitOfWork.SuscripcionProgramaGeneralRepository.Delete(suscripcionProgramaGenerals.Select(x => x.Id), usuario);
                //}
                /*Eliminamos los detalles T_PGeneralConfiguracionPlantilla y con la condicion de IdPlantillaBase = 12 (Certificado)*/
                var pgeneralConfiguracionPlantillasCertificado = _unitOfWork.PGeneralConfiguracionPlantillaRepository.ObtenerPorIdPGeneralYIdPlantillaBase(programaGeneralDTO.PGeneral.IdPgeneral.Value, (int)PlantillaBaseEstatico.Certificado).ToList();
                pgeneralConfiguracionPlantillasCertificado.RemoveAll(x => programaGeneralDTO.DetallesProgramaGeneral.ConfiguracionPlantilla.Any(y => y.Id == x.Id));
                if (pgeneralConfiguracionPlantillasCertificado != null && pgeneralConfiguracionPlantillasCertificado.Count() > 0)
                {
                    flagEliminacion = true;
                    _unitOfWork.PGeneralConfiguracionPlantillaRepository.Delete(pgeneralConfiguracionPlantillasCertificado.Select(x => x.Id), usuario);
                }
                /*Eliminamos los detalles T_PGeneralConfiguracionPlantilla y con la condicion de IdPlantillaBase = 13 (Constancia)*/
                var pgeneralConfiguracionPlantillasConstancia = _unitOfWork.PGeneralConfiguracionPlantillaRepository.ObtenerPorIdPGeneralYIdPlantillaBase(programaGeneralDTO.PGeneral.IdPgeneral.Value, ((int)PlantillaBaseEstatico.Constancia)).ToList();
                pgeneralConfiguracionPlantillasConstancia.RemoveAll(x => programaGeneralDTO.DetallesProgramaGeneral.ConfiguracionPlantillaConstancia.Any(y => y.Id == x.Id));
                if (pgeneralConfiguracionPlantillasConstancia != null && pgeneralConfiguracionPlantillasConstancia.Count() > 0)
                {
                    flagEliminacion = true;
                    _unitOfWork.PGeneralConfiguracionPlantillaRepository.Delete(pgeneralConfiguracionPlantillasConstancia.Select(x => x.Id), usuario);
                }
                /*Eliminamos los detalles T_PgeneralCodigoPartner*/
                var pGeneralCodigoPartners = _unitOfWork.PgeneralCodigoPartnerRepository.ObtenerPorIdPGeneral(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();
                pGeneralCodigoPartners.RemoveAll(x => programaGeneralDTO.DetallesProgramaGeneral.PgeneralCodigoPartner.Any(y => y.Id == x.Id));
                if (pGeneralCodigoPartners != null && pGeneralCodigoPartners.Count() > 0)
                {
                    flagEliminacion = true;
                    _unitOfWork.PgeneralCodigoPartnerRepository.Delete(pGeneralCodigoPartners.Select(x => x.Id), usuario);
                }
                var pEspecificoCodigoPartners = _unitOfWork.PEspecificoCodigoPartnerRepository.ObtenerPEspecificoCodigoPartner(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();
                pEspecificoCodigoPartners.RemoveAll(x => programaGeneralDTO.DetallesProgramaGeneral.PgeneralCodigoPartner.Any(y => y.Id == x.Id));
                if (pEspecificoCodigoPartners != null && pEspecificoCodigoPartners.Count() > 0)
                {
                    flagEliminacion = true;
                    _unitOfWork.PEspecificoCodigoPartnerRepository.Delete(pEspecificoCodigoPartners.Select(x => x.Id), usuario);
                }
                /*Eliminamos los detalles T_PgeneralProyectoAplicacion*/
                var pGeneralProyectoAplicacions = _unitOfWork.PgeneralProyectoAplicacionRepository.ObtenerPorIdPGeneral(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();
                pGeneralProyectoAplicacions.RemoveAll(x => programaGeneralDTO.DetallesProgramaGeneral.PgeneralProyectoAplicacion.Any(y => y.Id == x.Id));
                if (pGeneralProyectoAplicacions != null && pGeneralProyectoAplicacions.Count() > 0)
                {
                    _unitOfWork.PgeneralProyectoAplicacionRepository.Delete(pGeneralProyectoAplicacions.Select(x => x.Id), usuario);
                    flagEliminacion = true;
                }
                /*Eliminamos los detalles T_PGeneralModalidad*/
                var pGeneralModalidads = _unitOfWork.PGeneralModalidadRepository.ObtenerPGeneralModalidadPorIdPGeneral(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();
                pGeneralModalidads.RemoveAll(x => programaGeneralDTO.DetallesProgramaGeneral.Modalidad.Any(y => y == x.IdModalidadCurso));
                if (pGeneralModalidads != null && pGeneralModalidads.Count() > 0)
                {
                    flagEliminacion = true;
                    _unitOfWork.PGeneralModalidadRepository.Delete(pGeneralModalidads.Select(x => x.Id.Value), usuario);
                }
                if (flagEliminacion == true)
                {
                    _unitOfWork.Commit();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void ProcesarDetallesProgramasGeneral(ProgramaGeneralDTO programaGeneralDTO, string usuario, ref PGeneral pgeneral)
        {
            try
            {
                var idPgeneral = programaGeneralDTO.PGeneral.IdPgeneral!.Value;
                List<PgeneralParametroSeoPw> listaParametroSeo = new();
                foreach (var item in programaGeneralDTO.DetallesProgramaGeneral.ParametrosSeo)
                {
                    PgeneralParametroSeoPw entidad;
                    if (_unitOfWork.PGeneralParametroSeoPwRepository.Exist(x => x.IdPgeneral == idPgeneral && x.IdParametroSeo == item.IdParametroSeo))
                    {
                        entidad = _unitOfWork.PGeneralParametroSeoPwRepository.ObtenerPorIdPGeneralIdParametroSeo(idPgeneral, item.IdParametroSeo)!;
                        entidad.Descripcion = item.Descripcion;
                        entidad.UsuarioModificacion = usuario;
                        entidad.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        entidad = new PgeneralParametroSeoPw()
                        {
                            Descripcion = item.Descripcion,
                            IdParametroSeo = item.IdParametroSeo,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                    }
                    listaParametroSeo.Add(entidad);
                }
                List<PGeneralExpositor> listaExpositores = new();
                int posicion = 0;
                foreach (var idExpositor in programaGeneralDTO.DetallesProgramaGeneral.Expositores)
                {
                    PGeneralExpositor entidad;
                    if (_unitOfWork.PGeneralExpositorRepository.Exist(x => x.IdPgeneral == idPgeneral && x.IdExpositor == idExpositor))
                    {
                        entidad = _unitOfWork.PGeneralExpositorRepository.ObtenerPorIdPgeneralIdExpositor(idPgeneral, idExpositor)!;
                        entidad.Posicion = posicion++;
                        entidad.IdModalidadCurso = null;
                        entidad.UsuarioModificacion = usuario;
                        entidad.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        entidad = new PGeneralExpositor()
                        {
                            IdExpositor = idExpositor,
                            IdModalidadCurso = null,
                            Posicion = posicion++,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                    }
                    listaExpositores.Add(entidad);
                }

                foreach (var idDocente in programaGeneralDTO.DetallesProgramaGeneral.Docentes)
                {
                    PGeneralExpositor entidad;
                    int idModalidadCurso = 1;

                    if (_unitOfWork.PGeneralExpositorRepository.Exist(x => x.IdPgeneral == idPgeneral && x.IdExpositor == idDocente && x.IdModalidadCurso== 1))
                    {
                        entidad = _unitOfWork.PGeneralExpositorRepository.ObtenerPorIdPgeneralIdExpositorModalidad(idPgeneral, idDocente, 1)!;
                        entidad.Posicion = posicion++;
                        entidad.IdModalidadCurso = 1;
                        entidad.UsuarioModificacion = usuario;
                        entidad.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        entidad = new PGeneralExpositor()
                        {
                            IdExpositor = idDocente,
                            IdModalidadCurso = 1,
                            Posicion = posicion++,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                    }
                    listaExpositores.Add(entidad);
                }

                //PreRequisitoInstituto
                List<CarreraPreRequisitoPgeneral> listaPreRequisitos = new();
                foreach (var idPgeneralPrerequisito in programaGeneralDTO.DetallesProgramaGeneral.PreRequisitos)
                {
                    CarreraPreRequisitoPgeneral entidad;
                    if (_unitOfWork.CarreraPreRequisitoPgeneralRepository.Exist(x => x.IdPgeneral == idPgeneral && x.IdPgeneralPrerequisito == idPgeneralPrerequisito))
                    {
                        entidad = _unitOfWork.CarreraPreRequisitoPgeneralRepository.ObtenerPorIdPgeneralIdPgeneralPrerequisito(idPgeneral, idPgeneralPrerequisito)!;
                        entidad.UsuarioModificacion = usuario;
                        entidad.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        entidad = new CarreraPreRequisitoPgeneral()
                        {
                            IdPgeneralPreRequisito = idPgeneralPrerequisito,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                    }
                    listaPreRequisitos.Add(entidad);
                }
                //PreRequisitoInstituto
                //List<ProgramaAreaRelacionadum> listaAreasCapacitacion = new();
                //foreach (var idAreaCapacitacion in programaGeneralDTO.DetallesProgramaGeneral.ProgramaAreasRelacionadas)
                //{
                //    ProgramaAreaRelacionadum entidad;
                //    if (_unitOfWork.ProgramaAreaRelacionadaRepository.Exist(x => x.IdPgeneral == idPgeneral && x.IdAreaCapacitacion == idAreaCapacitacion))
                //    {
                //        //entidad = _unitOfWork.ProgramaAreaRelacionadaRepository.ObtenerPorIdPgeneralIdAreaCapacitacion(idPgeneral, idAreaCapacitacion)!;
                //        //entidad.UsuarioModificacion = usuario;
                //        //entidad.FechaModificacion = DateTime.Now;
                //    }
                //    else
                //    {
                //        entidad = new ProgramaAreaRelacionadum()
                //        {
                //            IdAreaCapacitacion = idAreaCapacitacion,
                //            UsuarioCreacion = usuario,
                //            UsuarioModificacion = usuario,
                //            FechaCreacion = DateTime.Now,
                //            FechaModificacion = DateTime.Now,
                //            Estado = true,
                //        };
                //        listaAreasCapacitacion.Add(entidad);
                //    }
                //}
                //List<SuscripcionProgramaGeneral> listaSuscripciones = new List<SuscripcionProgramaGeneral>();
                //foreach (var itemSuscripcion in programaGeneralDTO.DetallesProgramaGeneral.Suscripciones)
                //{
                //    SuscripcionProgramaGeneral suscripcion;
                //    if (itemSuscripcion.Id != 0 && _unitOfWork.SuscripcionProgramaGeneralRepository.Exist(itemSuscripcion.Id))
                //    {
                //        suscripcion = _unitOfWork.SuscripcionProgramaGeneralRepository.ObtenerPorId(itemSuscripcion.Id)!;
                //        suscripcion.Titulo = itemSuscripcion.Titulo;
                //        suscripcion.Descripcion = itemSuscripcion.Descripcion;
                //        suscripcion.OrdenBeneficio = itemSuscripcion.OrdenBeneficio;
                //        suscripcion.UsuarioModificacion = usuario;
                //        suscripcion.FechaModificacion = DateTime.Now;
                //    }
                //    else
                //    {
                //        suscripcion = new SuscripcionProgramaGeneral();
                //        suscripcion.Titulo = itemSuscripcion.Titulo;
                //        suscripcion.Descripcion = itemSuscripcion.Descripcion;
                //        suscripcion.OrdenBeneficio = itemSuscripcion.OrdenBeneficio;
                //        suscripcion.UsuarioCreacion = usuario;
                //        suscripcion.UsuarioModificacion = usuario;
                //        suscripcion.FechaCreacion = DateTime.Now;
                //        suscripcion.FechaModificacion = DateTime.Now;
                //        suscripcion.Estado = true;
                //    }
                //    listaSuscripciones.Add(suscripcion);
                //}
                List<PgeneralModalidad> listaModalidades = new List<PgeneralModalidad>();
                foreach (var idModalidad in programaGeneralDTO.DetallesProgramaGeneral.Modalidad)
                {
                    PgeneralModalidad entidad;
                    if (_unitOfWork.PGeneralModalidadRepository.Exist(x => x.IdModalidadCurso == idModalidad && x.IdPgeneral == idPgeneral))
                    {
                        //entidad = _unitOfWork.PGeneralModalidadRepository.ObtenerPorIdPGeneralYIdModalidadCurso(idPgeneral, idModalidad)!;
                        //entidad.UsuarioModificacion = usuario;
                        //entidad.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        entidad = new PgeneralModalidad()
                        {
                            IdModalidadCurso = idModalidad,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                        listaModalidades.Add(entidad);
                    }
                }
                List<PgeneralVersionPrograma> listaVersiones = new List<PgeneralVersionPrograma>();
                foreach (var itemVersion in programaGeneralDTO.DetallesProgramaGeneral.PgeneralVersionPrograma)
                {
                    PgeneralVersionPrograma entidad;
                    if (_unitOfWork.PGeneralVersionProgramaRepository.Exist(x => x.IdPgeneral == idPgeneral && x.IdVersionPrograma == itemVersion.IdVersionPrograma))
                    {
                        entidad = _unitOfWork.PGeneralVersionProgramaRepository.ObtenerPorIdPgeneralIdVersionPrograma(idPgeneral, itemVersion.IdVersionPrograma.GetValueOrDefault())!;
                        entidad.Duracion = itemVersion.Duracion;
                        entidad.UsuarioModificacion = usuario;
                        entidad.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        entidad = new PgeneralVersionPrograma()
                        {
                            Duracion = itemVersion.Duracion,
                            IdVersionPrograma = itemVersion.IdVersionPrograma,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                    }
                    listaVersiones.Add(entidad);
                }
                pgeneral.PGeneralParametroSeoPw = listaParametroSeo;
                //pgeneral.PgeneralDescripcion;
                //pgeneral.AdicionalProgramaGeneral;
                //pgeneral.ProgramaAreaRelacionada = listaAreasCapacitacion;
                pgeneral.PGeneralExpositor = listaExpositores;
                pgeneral.CarreraPreRequisitoPgeneral = listaPreRequisitos;
                //pgeneral.SuscripcionProgramaGeneral = listaSuscripciones;
                pgeneral.PgeneralModalidad = listaModalidades;
                pgeneral.PgeneralVersionPrograma = listaVersiones;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 27/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Agrega registro nuevo de programa General
        /// </summary>
        /// <param name="programaGeneralDTO">Información Compuesta de Programa General</param>
        /// <returns> PGeneralDTO </returns> 
        public PGeneralDTO Actualizar(ProgramaGeneralDTO programaGeneralDTO, string usuario)
        {
            try
            {
                /*                programaGeneralDTO = new();
                                if (programaGeneralDTO.PGeneral == null)
                                {
                                    throw new BadRequestException("Programa general es nulo");
                                }*/
                if (programaGeneralDTO.PGeneral.IdPgeneral == null || programaGeneralDTO.PGeneral.IdPgeneral == 0)
                {
                    throw new BadRequestException("Id Pgeneral Invalido");
                }
                var troncal = _unitOfWork.TroncalPgeneralRepository.ObtenerPorId(programaGeneralDTO.PGeneral.IdPgeneral.Value);
                if (troncal == null || troncal.Id == 0)
                {
                    throw new BadRequestException($"No existe el troncal pgeneral con id {programaGeneralDTO.PGeneral.IdPgeneral.Value}");
                }
                // Creas la fecha
                var fechaActual = DateTime.Now;
                var fechaActualNueva = fechaActual.AddMonths(programaGeneralDTO.FechaAsincronicaNueva);
                troncal.IdTroncalPartner = _unitOfWork.PartnerPwRepository.ObtenerPartnerAnterior(programaGeneralDTO.PGeneral.IdPartner.Value);
                troncal.IdArea = _unitOfWork.AreaCapacitacionRepository.ObtenerAreaCapacitacionAnterior(programaGeneralDTO.PGeneral.IdArea.Value);
                troncal.IdSubArea = _unitOfWork.SubAreaCapacitacionRepository.ObtenerSubAreaCapacitacionAnterior(programaGeneralDTO.PGeneral.IdSubArea.Value);
                troncal.Codigo = programaGeneralDTO.PGeneral.Codigo;
                troncal.Nombre = programaGeneralDTO.PGeneral.Nombre;
                troncal.FechaModificacion = DateTime.Now;
                troncal.UsuarioModificacion = usuario;

                _unitOfWork.TroncalPgeneralRepository.Update(troncal);
                _unitOfWork.Commit();

                PGeneral pgeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(programaGeneralDTO.PGeneral.IdPgeneral.Value);
                pgeneral.IdPgeneral = programaGeneralDTO.PGeneral.IdPgeneral;
                pgeneral.Nombre = programaGeneralDTO.PGeneral.Nombre;
                if (programaGeneralDTO.PGeneral.PwImgPortada != null && programaGeneralDTO.PGeneral.PwImgPortada != "" && programaGeneralDTO.PGeneral.PwImgPortada.Length != 0)
                    pgeneral.PwImgPortada = programaGeneralDTO.PGeneral.PwImgPortada;

                pgeneral.PwImgPortadaAlf = programaGeneralDTO.PGeneral.PwImgPortadaAlf;
                pgeneral.IdPartner = programaGeneralDTO.PGeneral.IdPartner;
                pgeneral.IdArea = programaGeneralDTO.PGeneral.IdArea;
                pgeneral.IdSubArea = programaGeneralDTO.PGeneral.IdSubArea;
                pgeneral.IdCategoria = programaGeneralDTO.PGeneral.IdCategoria;
                pgeneral.PwEstado = programaGeneralDTO.PGeneral.PwEstado;
                pgeneral.PwMostrarBsplay = programaGeneralDTO.PGeneral.PwMostrarBsplay;
                pgeneral.PwDuracion = programaGeneralDTO.PGeneral.PwDuracion;
                pgeneral.IdBusqueda = pgeneral.IdBusqueda == 0 ? pgeneral.Id : pgeneral.IdBusqueda;
                pgeneral.PgTitulo = programaGeneralDTO.PGeneral.PgTitulo;
                pgeneral.Codigo = programaGeneralDTO.PGeneral.Codigo;
                pgeneral.UrlBrochurePrograma = programaGeneralDTO.PGeneral.UrlBrochurePrograma;
                pgeneral.PwTituloHtml = programaGeneralDTO.PGeneral.PwTituloHtml;
                //pgeneral.EsModulo = programaGeneralDTO.PGeneral.EsModulo;
                pgeneral.NombreCorto = programaGeneralDTO.PGeneral.NombreCorto;
                pgeneral.IdPagina = programaGeneralDTO.PGeneral.IdPagina;
                pgeneral.ChatActivo = programaGeneralDTO.PGeneral.ChatActivo;
                pgeneral.PwDescripcionGeneral = programaGeneralDTO.PGeneral.PwDescripcionGeneral;
                pgeneral.TieneProyectoDeAplicacion = programaGeneralDTO.PGeneral.TieneProyectoDeAplicacion;
                pgeneral.TieneCertificadoModular = programaGeneralDTO.PGeneral.TieneCertificadoModular;
                pgeneral.CertificadoRequierePago = programaGeneralDTO.PGeneral.CertificadoRequierePago;
                pgeneral.IdTipoPrograma = programaGeneralDTO.PGeneral.IdTipoPrograma;
                pgeneral.CodigoPartner = programaGeneralDTO.PGeneral.CodigoPartner == "" ? null : programaGeneralDTO.PGeneral.CodigoPartner;
                pgeneral.CreditosTeoricos = programaGeneralDTO.PGeneral.CreditosTeoricos;
                pgeneral.CreditosPracticos = programaGeneralDTO.PGeneral.CreditosPracticos;
                pgeneral.CreditosTotales = programaGeneralDTO.PGeneral.CreditosTotales;
                pgeneral.HorasTeoricas = programaGeneralDTO.PGeneral.HorasTeoricas;
                pgeneral.HorasPracticas = programaGeneralDTO.PGeneral.HorasPracticas;
                pgeneral.HorasTotales = programaGeneralDTO.PGeneral.HorasTotales;
                pgeneral.IdTipoProgramaCarrera = programaGeneralDTO.PGeneral.IdTipoProgramaCarrera;
                pgeneral.UsuarioModificacion = usuario;
                pgeneral.FechaModificacion = DateTime.Now;
                pgeneral.IdPgeneralPeriodoAsincronico = programaGeneralDTO.PGeneral.IdPgeneralPeriodoAsincronico;

                if (!string.IsNullOrEmpty(programaGeneralDTO.PGeneral.LogoPrograma))
                {
                    pgeneral.LogoPrograma = programaGeneralDTO.PGeneral.LogoPrograma;
                    pgeneral.UrlLogoPrograma = "https://repositorioweb.blob.core.windows.net/repositorioweb/img/programas/logo/" + programaGeneralDTO.PGeneral.LogoPrograma.Replace(" ", "%20");
                }
                EliminarDetallesProgramaGeneral(programaGeneralDTO, usuario);

                ProcesarDetallesProgramasGeneral(programaGeneralDTO, usuario, ref pgeneral);

                pgeneral.FechaInicioAsincronico = fechaActualNueva;
                _unitOfWork.PGeneralRepository.Update(pgeneral);
                _unitOfWork.Commit();

                PgeneralCriterioEvaluacionHijo pgeneralcriterioevaluacion = new PgeneralCriterioEvaluacionHijo();

                var criterioEvaluacionHijo = _unitOfWork.PgeneralCriterioEvaluacionHijoRepository.ObtenerModalidadesPorIdPGeneral(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();//Aqui estamos recuperando que modalidades tiene el curso en la tabla de hijos
                var criterioEvaluacion = _unitOfWork.PGeneralCriterioEvaluacionRepository.ObtenerModalidadesPorIdPGeneral(programaGeneralDTO.PGeneral.IdPgeneral.Value).ToList();

                foreach (var idModalidad in programaGeneralDTO.DetallesProgramaGeneral.Modalidad)
                {
                    if (!criterioEvaluacionHijo.Exists(w => w.IdModalidadCurso == idModalidad && w.IdPgeneral == programaGeneralDTO.PGeneral.IdPgeneral))
                    {
                        PgeneralModalidad modalidad = new PgeneralModalidad();
                        modalidad.IdPgeneral = programaGeneralDTO.PGeneral.IdPgeneral.Value;
                        modalidad.IdModalidadCurso = idModalidad;
                        _unitOfWork.PgeneralCriterioEvaluacionHijoRepository.InsertarModalidadPGHIjo(modalidad);
                    }
                    else
                    {
                        criterioEvaluacionHijo.RemoveAll(w => w.IdModalidadCurso == idModalidad);
                    }
                }
                foreach (var item in criterioEvaluacionHijo)
                {
                    _unitOfWork.PgeneralCriterioEvaluacionHijoRepository.EliminarPorIdPGeneralYIdModalidad(programaGeneralDTO.PGeneral.IdPgeneral.Value, item.IdModalidadCurso, usuario);
                }

                foreach (var item in programaGeneralDTO.DetallesProgramaGeneral.Modalidad)
                {
                    if (criterioEvaluacion.Exists(w => w.IdModalidadCurso == item && w.IdPgeneral == programaGeneralDTO.PGeneral.IdPgeneral))
                    {
                        criterioEvaluacion.RemoveAll(w => w.IdModalidadCurso == item);
                    }
                }
                foreach (var item in criterioEvaluacion)
                {
                    _unitOfWork.PGeneralCriterioEvaluacionRepository.EliminarPorIdPGeneralIdModalidad(programaGeneralDTO.PGeneral.IdPgeneral.Value, item.IdModalidadCurso, usuario);
                }
                foreach (var item in programaGeneralDTO.DetallesProgramaGeneral.MontoPago)
                {

                    /*Borramos los detalles de MontoPago */
                    #region Detalles Monto Pago
                    var montoPagoPlataformas = _unitOfWork.MontoPagoPlataformaRepository.ObtenerPorIdMontoPago(item.Id);
                    montoPagoPlataformas.RemoveAll(x => item.PlataformasPagos.Any(y => y == x.Valor!.Value));
                    _unitOfWork.MontoPagoPlataformaRepository.Delete(montoPagoPlataformas.Select(x => x.Id), usuario);

                    var montoPagoSuscripciones = _unitOfWork.MontoPagoSuscripcionRepository.ObtenerPorIdMontoPago(item.Id);
                    montoPagoSuscripciones.RemoveAll(x => item.SuscripcionesPagos.Any(y => y == x.Valor!.Value));
                    _unitOfWork.MontoPagoSuscripcionRepository.Delete(montoPagoSuscripciones.Select(x => x.Id), usuario);
                    _unitOfWork.Commit();
                    #endregion

                    MontoPago montoPago = new MontoPago();
                    if (_unitOfWork.MontoPagoRepository.Exist(x => x.Id == item.Id))
                    {
                        montoPago = _unitOfWork.MontoPagoRepository.ObtenerPorId(item.Id);
                        montoPago.Precio = item.Precio;
                        montoPago.PrecioLetras = item.PrecioLetras;
                        montoPago.IdMoneda = item.IdMoneda;
                        montoPago.Matricula = item.Matricula;
                        montoPago.Cuotas = item.Cuotas;
                        montoPago.NroCuotas = item.NroCuotas;
                        montoPago.IdTipoDescuento = item.IdTipoDescuento;
                        montoPago.IdPrograma = item.IdPrograma;
                        montoPago.IdTipoPago = item.IdTipoPago;
                        montoPago.IdPais = item.IdPais;
                        montoPago.Vencimiento = item.Vencimiento;
                        montoPago.PrimeraCuota = item.PrimeraCuota;
                        montoPago.CuotaDoble = item.CuotaDoble;
                        montoPago.Descripcion = item.Descripcion;
                        montoPago.VisibleWeb = item.VisibleWeb;
                        montoPago.Paquete = item.Paquete;
                        montoPago.PorDefecto = item.PorDefecto;
                        montoPago.MontoDescontado = item.MontoDescontado;
                        montoPago.UsuarioModificacion = usuario;
                        montoPago.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        montoPago.Id = 0;
                        montoPago.Precio = item.Precio;
                        montoPago.PrecioLetras = item.PrecioLetras;
                        montoPago.IdMoneda = item.IdMoneda;
                        montoPago.Matricula = item.Matricula;
                        montoPago.Cuotas = item.Cuotas;
                        montoPago.NroCuotas = item.NroCuotas;
                        montoPago.IdTipoDescuento = item.IdTipoDescuento;
                        montoPago.IdPrograma = item.IdPrograma;
                        montoPago.IdTipoPago = item.IdTipoPago;
                        montoPago.IdPais = item.IdPais;
                        montoPago.Vencimiento = item.Vencimiento;
                        montoPago.PrimeraCuota = item.PrimeraCuota;
                        montoPago.CuotaDoble = item.CuotaDoble;
                        montoPago.Descripcion = item.Descripcion;
                        montoPago.VisibleWeb = item.VisibleWeb;
                        montoPago.Paquete = item.Paquete;
                        montoPago.PorDefecto = item.PorDefecto;
                        montoPago.MontoDescontado = item.MontoDescontado;
                        montoPago.Estado = true;
                        montoPago.UsuarioModificacion = usuario;
                        montoPago.UsuarioCreacion = usuario;
                        montoPago.FechaModificacion = DateTime.Now;
                        montoPago.FechaCreacion = DateTime.Now;
                    }
                    montoPago.MontoPagoPlataforma = new List<MontoPagoPlataforma>();
                    foreach (var item2 in item.PlataformasPagos)
                    {
                        MontoPagoPlataforma plataforma;
                        if (_unitOfWork.MontoPagoPlataformaRepository.Exist(x => x.IdPlataformaPago == item2 && x.IdMontoPago == item.Id))
                        {
                            //plataforma = _unitOfWork.MontoPagoPlataformaRepository.ObtenerPorIdPlataformaPagoYIdMontoPago(item2, item.Id);
                            //plataforma.UsuarioModificacion = usuario;
                            //plataforma.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            plataforma = new MontoPagoPlataforma();
                            plataforma.IdPlataformaPago = item2;
                            plataforma.UsuarioCreacion = usuario;
                            plataforma.UsuarioModificacion = usuario;
                            plataforma.FechaCreacion = DateTime.Now;
                            plataforma.FechaModificacion = DateTime.Now;
                            plataforma.Estado = true;
                            montoPago.MontoPagoPlataforma.Add(plataforma);
                        }
                    }
                    if (montoPago.Id == 0)
                    {
                        _unitOfWork.MontoPagoRepository.Add(montoPago);
                    }
                    else
                    {
                        _unitOfWork.MontoPagoRepository.Update(montoPago);
                    }
                    _unitOfWork.Commit();
                }
                foreach (var item in programaGeneralDTO.DetallesProgramaGeneral.ConfiguracionPlantilla)
                {
                    //Revisar lineas de codigo en integraV4
                    //if (item.RemplazarCertificados == true)
                    //{
                    //    //_repCertificadoGeneradoAutomatico.ActualizarCertificadosGenerados(item.Id, pgeneral.Id);
                    //}
                    PgeneralConfiguracionPlantilla certificadoPlantilla;

                    if (item.Id != 0 && _unitOfWork.PGeneralConfiguracionPlantillaRepository.Exist(x => x.Id == item.Id))
                    {
                        certificadoPlantilla = _unitOfWork.PGeneralConfiguracionPlantillaRepository.ObtenerPorId(item.Id);
                        certificadoPlantilla.IdPgeneral = pgeneral.Id;
                        certificadoPlantilla.IdPlantillaFrontal = item.IdPlantillaFrontal;
                        certificadoPlantilla.IdPlantillaPosterior = item.IdPlantillaPosterior;
                        if (item.RemplazarCertificados == true)
                        {
                            certificadoPlantilla.UltimaFechaRemplazarCertificado = DateTime.Now;
                        }
                        certificadoPlantilla.UltimaFechaRemplazarCertificado = DateTime.Now;
                        certificadoPlantilla.UsuarioModificacion = usuario;
                        certificadoPlantilla.FechaModificacion = DateTime.Now;
                        _unitOfWork.PGeneralConfiguracionPlantillaRepository.Update(certificadoPlantilla);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        certificadoPlantilla = new PgeneralConfiguracionPlantilla()
                        {
                            IdPgeneral = pgeneral.Id,
                            IdPlantillaFrontal = item.IdPlantillaFrontal,
                            IdPlantillaPosterior = item.IdPlantillaPosterior,
                            UltimaFechaRemplazarCertificado = item.RemplazarCertificados == true ? DateTime.Now : null,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                        var resCertificado = _unitOfWork.PGeneralConfiguracionPlantillaRepository.Add(certificadoPlantilla);
                        _unitOfWork.Commit();
                        certificadoPlantilla.Id = resCertificado.Id;
                    }

                    var detalleConfiguracionPlantilla = _unitOfWork.PgeneralConfiguracionPlantillaDetalleRepository.ObtenerPorIdPgeneralConfiguracionPlantilla(certificadoPlantilla.Id).ToList();
                    if (detalleConfiguracionPlantilla.Count() > 0)
                    {
                        _unitOfWork.PgeneralConfiguracionPlantillaDetalleRepository.Delete(detalleConfiguracionPlantilla.Select(x => x.Id), usuario);
                        _unitOfWork.Commit();
                        foreach (var id in detalleConfiguracionPlantilla.Select(x => x.Id).ToList())
                        {
                            if (_unitOfWork.PgeneralConfiguracionPlantillaEstadoMatriculaRepository.Exist(w => w.IdPgeneralConfiguracionPlantillaDetalle == id))
                            {
                                var listaIds = _unitOfWork.PgeneralConfiguracionPlantillaEstadoMatriculaRepository.ObtenerPorIdPgeneralConfiguracionPlantillaDetalle(id).Select(w => w.Id);
                                _unitOfWork.PgeneralConfiguracionPlantillaEstadoMatriculaRepository.Delete(listaIds, usuario);
                                _unitOfWork.Commit();
                            }
                            if (_unitOfWork.PgeneralConfiguracionPlantillaSubEstadoMatriculaRepository.Exist(w => w.IdPgeneralConfiguracionPlantillaDetalle == id))
                            {
                                var listaIds = _unitOfWork.PgeneralConfiguracionPlantillaSubEstadoMatriculaRepository.ObtenerPorIdPgeneralConfiguracionPlantillaDetalle(id).Select(w => w.Id);
                                _unitOfWork.PgeneralConfiguracionPlantillaSubEstadoMatriculaRepository.Delete(listaIds, usuario);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                    List<PGeneralConfiguracionPlantillaDetalle> pGeneralConfiguracionPlantillaDetalles = new List<PGeneralConfiguracionPlantillaDetalle>();
                    foreach (var itemDetalle in item.Detalle)
                    {
                        PGeneralConfiguracionPlantillaDetalle plantillaCertificadoDetalle = new PGeneralConfiguracionPlantillaDetalle()
                        {
                            IdPgeneralConfiguracionPlantilla = certificadoPlantilla.Id,
                            IdOperadorComparacion = itemDetalle.IdOperadorComparacion,
                            IdModalidadCurso = itemDetalle.IdModalidadCurso,
                            NotaAprobatoria = itemDetalle.NotaAprobatoria,
                            DeudaPendiente = itemDetalle.DeudaPendiente,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                        plantillaCertificadoDetalle.PgeneralConfiguracionPlantillaEstadoMatriculas = itemDetalle.EstadosMatricula.Select(idEstado => new PgeneralConfiguracionPlantillaEstadoMatricula
                        {
                            IdEstadoMatricula = idEstado,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        }).ToList();

                        plantillaCertificadoDetalle.PgeneralConfiguracionPlantillaSubEstadoMatriculas = itemDetalle.SubEstadosMatricula.Select(idSubEstado => new PgeneralConfiguracionPlantillaSubEstadoMatricula
                        {
                            IdSubEstadoMatricula = idSubEstado,
                            IdPgeneralConfiguracionPlantillaDetalle = plantillaCertificadoDetalle.Id,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        }).ToList();
                        pGeneralConfiguracionPlantillaDetalles.Add(plantillaCertificadoDetalle);
                    }
                    _unitOfWork.PgeneralConfiguracionPlantillaDetalleRepository.Add(pGeneralConfiguracionPlantillaDetalles);
                    _unitOfWork.Commit();
                }
                foreach (var itemConfiguracionBeneficio in programaGeneralDTO.DetallesProgramaGeneral.ConfiguracionBeneficio)
                {
                    ConfiguracionBeneficioProgramaGeneral configuracionBeneficio;

                    if (itemConfiguracionBeneficio.Asosiar == true)
                    {
                        _unitOfWork.DetachAll();
                        if (_unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerPorId(itemConfiguracionBeneficio.Id) != null)
                        {
                            configuracionBeneficio = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerPorId(itemConfiguracionBeneficio.Id);
                            configuracionBeneficio.IdPgeneral = itemConfiguracionBeneficio.IdPGeneral;
                            configuracionBeneficio.IdBeneficio = itemConfiguracionBeneficio.IdBeneficio;
                            configuracionBeneficio.OrdenBeneficio = itemConfiguracionBeneficio.OrdenBeneficio;
                            configuracionBeneficio.DatosAdicionales = itemConfiguracionBeneficio.DatosAdicionales;
                            configuracionBeneficio.Tipo = itemConfiguracionBeneficio.TipoBeneficio.Value;
                            configuracionBeneficio.Asosiar = itemConfiguracionBeneficio.Asosiar;
                            configuracionBeneficio.DeudaPendiente = itemConfiguracionBeneficio.DeudaPendiente;
                            configuracionBeneficio.AvanceAcademico = itemConfiguracionBeneficio.AvanceAcademico;
                            configuracionBeneficio.Entrega = itemConfiguracionBeneficio.Entrega;
                            configuracionBeneficio.UsuarioModificacion = usuario;
                            configuracionBeneficio.FechaModificacion = DateTime.Now;
                            _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.Update(configuracionBeneficio);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            configuracionBeneficio = new ConfiguracionBeneficioProgramaGeneral()
                            {
                                IdPgeneral = itemConfiguracionBeneficio.IdPGeneral,
                                IdBeneficio = itemConfiguracionBeneficio.IdBeneficio,
                                OrdenBeneficio = itemConfiguracionBeneficio.OrdenBeneficio,
                                DatosAdicionales = itemConfiguracionBeneficio.DatosAdicionales,
                                Tipo = itemConfiguracionBeneficio.TipoBeneficio.Value,
                                Asosiar = itemConfiguracionBeneficio.Asosiar,
                                Entrega = itemConfiguracionBeneficio.Entrega,
                                DeudaPendiente = itemConfiguracionBeneficio.DeudaPendiente,
                                AvanceAcademico = itemConfiguracionBeneficio.AvanceAcademico,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true,
                            };
                            var resConfiguracionBeneficio = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.Add(configuracionBeneficio);
                            _unitOfWork.Commit();
                            configuracionBeneficio.Id = resConfiguracionBeneficio.Id;
                        }

                        var listaEstadoMatricula = _unitOfWork.ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository.ObtenerPorIdConfiguracionBeneficioPgneral(configuracionBeneficio.Id).ToList();
                        listaEstadoMatricula.RemoveAll(x => itemConfiguracionBeneficio.EstadosMatricula.Any(s => s == x.IdEstadoMatricula));
                        if (listaEstadoMatricula.Count() > 0)
                        {
                            _unitOfWork.ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository.Delete(listaEstadoMatricula.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }
                        configuracionBeneficio.ConfiguracionBeneficioProgramaGeneralEstadoMatriculas = new List<ConfiguracionBeneficioProgramaGeneralEstadoMatricula>();
                        foreach (var idEstadoMatricula in itemConfiguracionBeneficio.EstadosMatricula)
                        {
                            if (_unitOfWork.ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository.Exist(x => x.IdConfiguracionBeneficioPgneral == configuracionBeneficio.Id && x.IdEstadoMatricula == idEstadoMatricula))
                            {
                                //
                            }
                            else
                            {
                                ConfiguracionBeneficioProgramaGeneralEstadoMatricula configuracionBeneficioEstadoMatricula = new()
                                {
                                    IdConfiguracionBeneficioPgneral = configuracionBeneficio.Id,
                                    IdEstadoMatricula = idEstadoMatricula,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true,
                                };
                                _unitOfWork.ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository.Add(configuracionBeneficioEstadoMatricula);
                                _unitOfWork.Commit();
                            }
                        }

                        var listaSubEstado = _unitOfWork.ConfiguracionBeneficioProgramaGeneralSubEstadoRepository.ObtenerPorIdConfiguracionBeneficioPgneral(configuracionBeneficio.Id).ToList();
                        listaSubEstado.RemoveAll(x => itemConfiguracionBeneficio.SubEstadosMatricula.Any(s => s == x.IdSubEstadoMatricula));
                        if (listaSubEstado.Count() > 0)
                        {
                            _unitOfWork.ConfiguracionBeneficioProgramaGeneralSubEstadoRepository.Delete(listaSubEstado.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }
                        foreach (var idSubEstado in itemConfiguracionBeneficio.SubEstadosMatricula)
                        {
                            if (_unitOfWork.ConfiguracionBeneficioProgramaGeneralSubEstadoRepository.Exist(x => x.IdConfiguracionBeneficioPgneral == configuracionBeneficio.Id && x.IdSubEstadoMatricula == idSubEstado))
                            {
                                //
                            }
                            else
                            {
                                ConfiguracionBeneficioProgramaGeneralSubEstado configuracionBeneficioSubEstadoMatricula = new()
                                {
                                    IdConfiguracionBeneficioPgneral = configuracionBeneficio.Id,
                                    IdSubEstadoMatricula = idSubEstado,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true,
                                };
                                _unitOfWork.ConfiguracionBeneficioProgramaGeneralSubEstadoRepository.Add(configuracionBeneficioSubEstadoMatricula);
                                _unitOfWork.Commit();
                            }
                        }

                        var listaPaises = _unitOfWork.ConfiguracionBeneficioProgramaGeneralPaisRepository.ObtenerPorIdConfiguracionBeneficioPGneral(configuracionBeneficio.Id);
                        listaPaises.RemoveAll(x => itemConfiguracionBeneficio.Paises.Any(idPais => idPais == x.IdPais));
                        if (listaPaises.Count() > 0)
                        {
                            _unitOfWork.ConfiguracionBeneficioProgramaGeneralPaisRepository.Delete(listaPaises.Select(w => w.Id), usuario);
                            _unitOfWork.Commit();
                        }
                        foreach (var idPais in itemConfiguracionBeneficio.Paises)
                        {
                            if (_unitOfWork.ConfiguracionBeneficioProgramaGeneralPaisRepository.Exist(x => x.IdConfiguracionBeneficioPgneral == configuracionBeneficio.Id && x.IdPais == idPais))
                            {
                                //
                            }
                            else
                            {
                                ConfiguracionBeneficioProgramaGeneralPais configuracionBeneficioPais = new()
                                {
                                    IdConfiguracionBeneficioPgneral = configuracionBeneficio.Id,
                                    IdPais = idPais,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true,
                                };
                                _unitOfWork.ConfiguracionBeneficioProgramaGeneralPaisRepository.Add(configuracionBeneficioPais);
                                _unitOfWork.Commit();
                            }
                        }

                        if (itemConfiguracionBeneficio.DatosAdicionales == true && itemConfiguracionBeneficio.DatosAdicional != null)
                        {
                            var listaDatoAdicional = _unitOfWork.ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository.GetBy(x => x.IdConfiguracionBeneficioPgeneral == configuracionBeneficio.Id).ToList();
                            listaDatoAdicional.RemoveAll(x => itemConfiguracionBeneficio.DatosAdicional.Any(idBeneficioDatoAdicional => idBeneficioDatoAdicional == x.IdBeneficioDatoAdicional));
                            if (listaDatoAdicional.Count() > 0)
                            {
                                _unitOfWork.ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository.Delete(listaDatoAdicional.Select(w => w.Id), usuario);
                                _unitOfWork.Commit();
                            }

                            foreach (var idDatoAdicional in itemConfiguracionBeneficio.DatosAdicional)
                            {
                                if (_unitOfWork.ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository.Exist(x => x.IdConfiguracionBeneficioPgeneral == configuracionBeneficio.Id && x.IdBeneficioDatoAdicional == idDatoAdicional))
                                {
                                    //
                                }
                                else
                                {
                                    ConfiguracionBeneficioProgramaGeneralDatoAdicional configuracionBeneficioDatoAdicional = new()
                                    {
                                        IdConfiguracionBeneficioPgeneral = configuracionBeneficio.Id,
                                        IdBeneficioDatoAdicional = idDatoAdicional,
                                        UsuarioCreacion = usuario,
                                        UsuarioModificacion = usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        Estado = true,
                                    };
                                    _unitOfWork.ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository.Add(configuracionBeneficioDatoAdicional);
                                    _unitOfWork.Commit();
                                }
                            }
                        }
                        var listaVersiones = _unitOfWork.ConfiguracionBeneficioProgramaGeneralVersionRepository.GetBy(x => x.IdConfiguracionBeneficioPgneral == configuracionBeneficio.Id).ToList();
                        listaVersiones.RemoveAll(x => itemConfiguracionBeneficio.Versiones.Any(idVersion => idVersion == x.IdVersionPrograma));
                        if (listaVersiones.Count() > 0)
                        {
                            _unitOfWork.ConfiguracionBeneficioProgramaGeneralVersionRepository.Delete(listaVersiones.Select(w => w.Id), usuario);
                            _unitOfWork.Commit();
                        }
                        foreach (var idVersion in itemConfiguracionBeneficio.Versiones)
                        {
                            if (_unitOfWork.ConfiguracionBeneficioProgramaGeneralVersionRepository.Exist(x => x.IdConfiguracionBeneficioPgneral == configuracionBeneficio.Id && x.IdVersionPrograma == idVersion))
                            {
                                //
                            }
                            else
                            {
                                ConfiguracionBeneficioProgramaGeneralVersion configuracionBeneficioVersion = new()
                                {
                                    IdConfiguracionBeneficioPgneral = configuracionBeneficio.Id,
                                    IdVersionPrograma = idVersion == 0 ? VersionPrograma.SINVERSION : idVersion,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true,
                                };
                                _unitOfWork.ConfiguracionBeneficioProgramaGeneralVersionRepository.Add(configuracionBeneficioVersion);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                    else
                    {
                        if (itemConfiguracionBeneficio.Id != 0)
                        {
                            _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.Delete(itemConfiguracionBeneficio.Id, usuario);
                            _unitOfWork.Commit();
                            var listaEstado = _unitOfWork.ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository.GetBy(x => x.IdConfiguracionBeneficioPgneral == itemConfiguracionBeneficio.Id);
                            if (listaEstado.Count() > 0)
                            {
                                _unitOfWork.ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository.Delete(listaEstado.Select(w => w.Id), usuario);
                                _unitOfWork.Commit();
                            }
                            var listaSubEstado = _unitOfWork.ConfiguracionBeneficioProgramaGeneralSubEstadoRepository.GetBy(x => x.IdConfiguracionBeneficioPgneral == itemConfiguracionBeneficio.Id);
                            if (listaSubEstado.Count() > 0)
                            {
                                _unitOfWork.ConfiguracionBeneficioProgramaGeneralSubEstadoRepository.Delete(listaSubEstado.Select(w => w.Id), usuario);
                                _unitOfWork.Commit();
                            }

                            var listaPaises = _unitOfWork.ConfiguracionBeneficioProgramaGeneralPaisRepository.GetBy(x => x.IdConfiguracionBeneficioPgneral == itemConfiguracionBeneficio.Id);
                            if (listaPaises.Count() > 0)
                            {
                                _unitOfWork.ConfiguracionBeneficioProgramaGeneralPaisRepository.Delete(listaPaises.Select(w => w.Id), usuario);
                                _unitOfWork.Commit();
                            }
                            var listaVersiones = _unitOfWork.ConfiguracionBeneficioProgramaGeneralVersionRepository.GetBy(x => x.IdConfiguracionBeneficioPgneral == itemConfiguracionBeneficio.Id);
                            if (listaVersiones.Count() > 0)
                            {
                                var listaId = listaVersiones.Select(w => w.Id);
                                _unitOfWork.ConfiguracionBeneficioProgramaGeneralVersionRepository.Delete(listaId, usuario);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                }
                foreach (var itemCodigoPartner in programaGeneralDTO.DetallesProgramaGeneral.PgeneralCodigoPartner)
                {
                    PGeneralCodigoPartner pgeneralCodigoPartner;

                    if (_unitOfWork.PgeneralCodigoPartnerRepository.Exist(itemCodigoPartner.Id))
                    {
                        pgeneralCodigoPartner = _unitOfWork.PgeneralCodigoPartnerRepository.ObtenerPorId(itemCodigoPartner.Id)!;
                        pgeneralCodigoPartner.Codigo = itemCodigoPartner.Codigo;
                        pgeneralCodigoPartner.Pdu = itemCodigoPartner.Pdu;
                        pgeneralCodigoPartner.IdPgeneral = pgeneral.Id;
                        pgeneralCodigoPartner.UsuarioModificacion = usuario;
                        pgeneralCodigoPartner.FechaModificacion = DateTime.Now;
                        pgeneralCodigoPartner.Estado = true;
                        _unitOfWork.PgeneralCodigoPartnerRepository.Update(pgeneralCodigoPartner);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        pgeneralCodigoPartner = new PGeneralCodigoPartner()
                        {
                            Codigo = itemCodigoPartner.Codigo,
                            Pdu = itemCodigoPartner.Pdu,
                            IdPgeneral = pgeneral.Id,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                        var resCodigoPartner = _unitOfWork.PgeneralCodigoPartnerRepository.Add(pgeneralCodigoPartner);
                        _unitOfWork.Commit();
                        pgeneralCodigoPartner.Id = resCodigoPartner.Id;
                    }
                    var modalidadesActuales = _unitOfWork.PgeneralCodigoPartnerModalidadCursoRepository.GetBy(x => x.IdPgeneralCodigoPartner == pgeneralCodigoPartner.Id && x.Estado).ToList();
                    foreach (var idModalidad in itemCodigoPartner.ModalidadesCurso)
                    {
                        var existente = modalidadesActuales.FirstOrDefault(x => x.IdModalidadCurso == idModalidad);
                        if (existente == null)
                        {
                            var nuevaModalidad = new PgeneralCodigoPartnerModalidadCurso
                            {
                                IdPgeneralCodigoPartner = pgeneralCodigoPartner.Id,
                                IdModalidadCurso = idModalidad,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true,
                            };
                            _unitOfWork.PgeneralCodigoPartnerModalidadCursoRepository.Add(nuevaModalidad);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            // Si ya existe, puedes actualizar la FechaModificación si lo deseas
                            existente.FechaModificacion = DateTime.Now;
                            existente.UsuarioModificacion = usuario;
                            _unitOfWork.PgeneralCodigoPartnerModalidadCursoRepository.Update(existente);
                            _unitOfWork.Commit();
                        }
                    }

                    // Desactivar (soft delete) las modalidades que ya no están
                    var idsNuevos = itemCodigoPartner.ModalidadesCurso;
                    foreach (var modalidadActual in modalidadesActuales)
                    {
                        if (!idsNuevos.Contains(modalidadActual.IdModalidadCurso))
                        {
                            modalidadActual.Estado = false;
                            modalidadActual.FechaModificacion = DateTime.Now;
                            modalidadActual.UsuarioModificacion = usuario;
                            _unitOfWork.PgeneralCodigoPartnerModalidadCursoRepository.Update(modalidadActual);
                            _unitOfWork.Commit();
                        }
                    }
                    // Obtener versiones actuales en BD activas
                    var versionesActuales = _unitOfWork.PgeneralCodigoPartnerVersionProgramaRepository
                        .GetBy(x => x.IdPgeneralCodigoPartner == pgeneralCodigoPartner.Id && x.Estado)
                        .ToList();

                    // Insertar nuevas o mantener existentes
                    foreach (var idVersion in itemCodigoPartner.VersionesPrograma)
                    {
                        var existente = versionesActuales.FirstOrDefault(x => x.IdVersionPrograma == idVersion);
                        if (existente == null)
                        {
                            var nuevaVersion = new PgeneralCodigoPartnerVersionPrograma
                            {
                                IdPgeneralCodigoPartner = pgeneralCodigoPartner.Id,
                                IdVersionPrograma = idVersion,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true,
                            };
                            _unitOfWork.PgeneralCodigoPartnerVersionProgramaRepository.Add(nuevaVersion);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            existente.FechaModificacion = DateTime.Now;
                            existente.UsuarioModificacion = usuario;
                            _unitOfWork.PgeneralCodigoPartnerVersionProgramaRepository.Update(existente);
                            _unitOfWork.Commit();
                        }
                    }

                    // Desactivar (eliminar lógicamente) las versiones que ya no están
                    var idsVersionNuevos = itemCodigoPartner.VersionesPrograma;
                    foreach (var versionActual in versionesActuales)
                    {
                        if (versionActual.IdVersionPrograma.HasValue && !idsVersionNuevos.Contains(versionActual.IdVersionPrograma.Value))
                        {
                            versionActual.Estado = false;
                            versionActual.FechaModificacion = DateTime.Now;
                            versionActual.UsuarioModificacion = usuario;
                            _unitOfWork.PgeneralCodigoPartnerVersionProgramaRepository.Update(versionActual);
                            _unitOfWork.Commit();
                        }
                    }

                }

                foreach (var itemPEspecificoCodigoPartner in programaGeneralDTO.DetallesProgramaGeneral.PespecificoCodigoPartner)
                {
                    PespecificoCodigoPartner pespecificoCodigoPartner;
                    var fechaInicioPEspecifico = _unitOfWork.PEspecificoRepository.ObtenerFechaInicioCursoPorIdPEspeficico(itemPEspecificoCodigoPartner.IdPespecifico);
                    if (_unitOfWork.PEspecificoCodigoPartnerRepository.Exist(itemPEspecificoCodigoPartner.Id))
                    {
                        pespecificoCodigoPartner = _unitOfWork.PEspecificoCodigoPartnerRepository.ObtenerPorId(itemPEspecificoCodigoPartner.Id)!;
                        pespecificoCodigoPartner.Codigo = itemPEspecificoCodigoPartner.Codigo;
                        pespecificoCodigoPartner.Pdu = itemPEspecificoCodigoPartner.Pdu;
                        pespecificoCodigoPartner.IdPespecifico = itemPEspecificoCodigoPartner.IdPespecifico;
                        pespecificoCodigoPartner.FechaInicio = fechaInicioPEspecifico.FechaInicio;
                        pespecificoCodigoPartner.UsuarioModificacion = usuario;
                        pespecificoCodigoPartner.FechaModificacion = DateTime.Now;
                        pespecificoCodigoPartner.Estado = true;
                        _unitOfWork.PEspecificoCodigoPartnerRepository.Update(pespecificoCodigoPartner);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        pespecificoCodigoPartner = new PespecificoCodigoPartner()
                        {
                            Codigo = itemPEspecificoCodigoPartner.Codigo,
                            Pdu = itemPEspecificoCodigoPartner.Pdu,
                            IdPespecifico = itemPEspecificoCodigoPartner.IdPespecifico,
                            FechaInicio = fechaInicioPEspecifico.FechaInicio,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                        var resCodigoPartner = _unitOfWork.PEspecificoCodigoPartnerRepository.Add(pespecificoCodigoPartner);
                        _unitOfWork.Commit();

                    }
                }



                foreach (var itemProyectoAplicacion in programaGeneralDTO.DetallesProgramaGeneral.PgeneralProyectoAplicacion)
                {
                    PgeneralProyectoAplicacion pgeneralProyectoAplicacion;

                    if (_unitOfWork.PgeneralProyectoAplicacionRepository.Exist(itemProyectoAplicacion.Id))
                    {
                        pgeneralProyectoAplicacion = _unitOfWork.PgeneralProyectoAplicacionRepository.ObtenerPorId(itemProyectoAplicacion.Id)!;
                        pgeneralProyectoAplicacion.IdPgeneral = pgeneral.Id;
                        pgeneralProyectoAplicacion.UsuarioModificacion = usuario;
                        pgeneralProyectoAplicacion.FechaModificacion = DateTime.Now;
                        pgeneralProyectoAplicacion.Estado = true;
                        _unitOfWork.PgeneralProyectoAplicacionRepository.Update(pgeneralProyectoAplicacion);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        pgeneralProyectoAplicacion = new()
                        {
                            IdPgeneral = pgeneral.Id,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                        };
                        var resProyectoAplicacion = _unitOfWork.PgeneralProyectoAplicacionRepository.Add(pgeneralProyectoAplicacion);
                        _unitOfWork.Commit();
                        pgeneralProyectoAplicacion.Id = resProyectoAplicacion.Id;
                    }
                    foreach (var idModalidad in itemProyectoAplicacion.Modalidades)
                    {
                        PgeneralProyectoAplicacionModalidad pgeneralProyectoAplicacionModalidad;

                        if (_unitOfWork.PgeneralProyectoAplicacionModalidadRepository.Exist(w => w.IdModalidadCurso == idModalidad && w.IdPgeneralProyectoAplicacion == pgeneralProyectoAplicacion.Id))
                        {
                            pgeneralProyectoAplicacionModalidad = _unitOfWork.PgeneralProyectoAplicacionModalidadRepository.ObtenerPorIdModalidadCursoIdPgeneralProyectoAplicacion(idModalidad, pgeneralProyectoAplicacion.Id)!;
                            pgeneralProyectoAplicacionModalidad.UsuarioModificacion = usuario;
                            pgeneralProyectoAplicacionModalidad.FechaModificacion = DateTime.Now;
                            _unitOfWork.DetachAll();
                            _unitOfWork.PgeneralProyectoAplicacionModalidadRepository.Update(pgeneralProyectoAplicacionModalidad);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            pgeneralProyectoAplicacionModalidad = new()
                            {
                                IdPgeneralProyectoAplicacion = pgeneralProyectoAplicacion.Id,
                                IdModalidadCurso = idModalidad,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true,
                            };

                            _unitOfWork.PgeneralProyectoAplicacionModalidadRepository.Add(pgeneralProyectoAplicacionModalidad);
                            _unitOfWork.Commit();
                        }
                    }
                    foreach (var idProveedor in itemProyectoAplicacion.Proveedores)
                    {
                        PgeneralProyectoAplicacionProveedor pgeneralProyectoAplicacionProveedor;

                        if (_unitOfWork.PgeneralProyectoAplicacionProveedorRepository.Exist(w => w.IdProveedor == idProveedor && w.IdPgeneralProyectoAplicacion == pgeneralProyectoAplicacion.Id))
                        {
                            pgeneralProyectoAplicacionProveedor = _unitOfWork.PgeneralProyectoAplicacionProveedorRepository.ObtenerPorIdProveedorIdPgeneralProyectoAplicacion(idProveedor, pgeneralProyectoAplicacion.Id)!;
                            pgeneralProyectoAplicacionProveedor.UsuarioModificacion = usuario;
                            pgeneralProyectoAplicacionProveedor.FechaModificacion = DateTime.Now;
                            pgeneralProyectoAplicacionProveedor.Estado = true;
                            _unitOfWork.PgeneralProyectoAplicacionProveedorRepository.Update(pgeneralProyectoAplicacionProveedor);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            pgeneralProyectoAplicacionProveedor = new()
                            {
                                IdPgeneralProyectoAplicacion = pgeneralProyectoAplicacion.Id,
                                IdProveedor = idProveedor,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true,
                            };
                            _unitOfWork.PgeneralProyectoAplicacionProveedorRepository.Add(pgeneralProyectoAplicacionProveedor);
                            _unitOfWork.Commit();
                        }
                    }
                }
                if (programaGeneralDTO.DetallesProgramaGeneral.PgeneralForoAsignacionProveedor.Count > 0)
                {
                    //Se elimina registros anteriores
                    var registrosAnteriores = _unitOfWork.PgeneralForoAsignacionProveedorRepository.GetBy(x => x.IdPgeneral == programaGeneralDTO.PGeneral.Id).ToList();
                    //var detalleForo = programaGeneralDTO.DetallesProgramaGeneral.PgeneralForoAsignacionProveedor;
                    //registrosAnteriores.RemoveAll(x => !detalleForo.Any(s => s.Proveedores.Contains(x.IdProveedor)));
                    _unitOfWork.PgeneralForoAsignacionProveedorRepository.Delete(registrosAnteriores.Select(x => x.Id), usuario);
                    _unitOfWork.Commit();
                    //registrosAnteriores = _unitOfWork.PgeneralForoAsignacionProveedorRepository.GetBy(x => x.IdPgeneral == programaGeneralDTO.PGeneral.Id).ToList();

                    //var registrosNuevos = new List<PGeneralForoAsignacionProveedorDTO>();
                    //detalleForo.ForEach(x =>
                    //{
                    //    x.Proveedores.ForEach(s =>
                    //    {
                    //        registrosNuevos.Add(new PGeneralForoAsignacionProveedorDTO{ 
                    //            IdPgeneral = programaGeneralDTO.PGeneral.Id.Value,
                    //            IdModalidadCurso = x.IdModalidadCurso,
                    //            IdProveedor = s
                    //        });
                    //    });
                    //});

                    //Validación para evitar la duplicidad de datos
                    List<PGeneralForoAsignacionProveedorDTO> listaAuxiliarForoAsignacionProveedor = new List<PGeneralForoAsignacionProveedorDTO>();
                    PgeneralForoAsignacionProveedor nuevoRegistro;
                    //Armamos una lista de datos lineal para distinción de proveedores y modalidades
                    foreach (var configuracionForoAsignacionProveedor in programaGeneralDTO.DetallesProgramaGeneral.PgeneralForoAsignacionProveedor)
                    {
                        foreach (var proveedor in configuracionForoAsignacionProveedor.Proveedores)
                        {
                            listaAuxiliarForoAsignacionProveedor.Add(new PGeneralForoAsignacionProveedorDTO()
                            {
                                IdPgeneral = programaGeneralDTO.PGeneral.Id.Value,
                                IdModalidadCurso = configuracionForoAsignacionProveedor.IdModalidadCurso,
                                IdProveedor = proveedor
                            });
                        }
                    }
                    if (listaAuxiliarForoAsignacionProveedor.Count > 0)
                    {
                        //Se distingue lista para evitar repetición de registros
                        var listaAuxiliarForoAsignacionProveedorSinRepeticiones = listaAuxiliarForoAsignacionProveedor.Select(x => new { x.IdModalidadCurso, x.IdPgeneral, x.IdProveedor }).Distinct().ToList();
                        var nuevoRegistros = listaAuxiliarForoAsignacionProveedorSinRepeticiones.Select(x => new PgeneralForoAsignacionProveedor()
                        {
                            IdPgeneral = x.IdPgeneral,
                            IdModalidadCurso = x.IdModalidadCurso,
                            IdProveedor = x.IdProveedor,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        });
                        _unitOfWork.PgeneralForoAsignacionProveedorRepository.Add(nuevoRegistros);
                        _unitOfWork.Commit();
                    }
                }
                else
                {
                    var registrosAnteriores = _unitOfWork.PgeneralForoAsignacionProveedorRepository.GetBy(x => x.IdPgeneral == programaGeneralDTO.PGeneral.Id).ToList();
                    _unitOfWork.PgeneralForoAsignacionProveedorRepository.Delete(registrosAnteriores.Select(x => x.Id), usuario);
                    _unitOfWork.Commit();
                }
                return _mapper.Map<PGeneralDTO>(pgeneral);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor:Joseph Llanque.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuracion de plantillas certificados.
        /// </summary>
        /// <returns> List<PGeneralConfiguracionPlantilla> </returns>
        public List<PgeneralConfiguracionBeneficioDTO> ObtenerPgeneralConfiuracionBeneficios(int IdPGeneral)
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerPGeneralConfiguracionBeneficios(IdPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gilmer Qm.
        /// Fecha: 12/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos para el modulo (C) Videos y evaluaciones en estructura del programa
        /// </summary>
        /// <returns> PGeneralComboModuloConfigurarVideoProgramaDTO </returns>
        public PGeneralComboModuloConfigurarVideoProgramaDTO ObtenerCombosConfigurarVideoPrograma()
        {
            try
            {
                var combosProgramaGeneral = new PGeneralComboModuloConfigurarVideoProgramaDTO();
                combosProgramaGeneral.AreaCapacitacion = _unitOfWork.AreaCapacitacionRepository.ObtenerFiltro().ToList();
                combosProgramaGeneral.SubAreaCapacitacion = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltro().ToList();
                combosProgramaGeneral.PartnerPws = _unitOfWork.PartnerPwRepository.ObtenerCombo().ToList();
                combosProgramaGeneral.PGenerals = _unitOfWork.PGeneralRepository.ObtenerProgramasFiltro().ToList();
                combosProgramaGeneral.TipoVista = _unitOfWork.TipoVistumRepository.ObtenerCombo().ToList();
                combosProgramaGeneral.TipoEvaluacionTrabajo = _unitOfWork.TipoEvaluacionTrabajoRepository.ObtenerCombo().ToList();
                combosProgramaGeneral.TipoMarcador = _unitOfWork.TipoMarcadorRepository.ObtenerCombo().ToList();
                return (combosProgramaGeneral);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 08/07/2023
        /// Version: 1.0
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <summary>
        /// Obtiene perfil contacto por el IdPGeneral
        /// </summary>
        /// <returns> PerfilContactoProgramaDTO </returns>
        public PerfilContactoProgramaDTO ObtenerPerfilContacto(int idPGeneral)
        {
            PerfilContactoProgramaDTO perfilPrograma = new PerfilContactoProgramaDTO();
            CoeficienteScoringCiudadDTO ciudad = new CoeficienteScoringCiudadDTO();
            ciudad.CiudadScoring = _mapper.Map<List<ScoringCiudadProgramaDTO>>(_unitOfWork.ProgramaGeneralPerfilScoringCiudadRepository.ObtenerPorIdPGeneral(idPGeneral).Where(x => x.IdCiudad != null));
            ciudad.CiudadCoefiente = _mapper.Map<List<ProgramaGeneralPerfilCoeficienteDTO>>(_unitOfWork.ProgramaGeneralPerfilCiudadCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral));
            perfilPrograma.CoeficienteScoringCiudad = ciudad;

            CoeficienteScoringModalidadDTO modalidad = new CoeficienteScoringModalidadDTO();
            modalidad.ModalidadScoring = _mapper.Map<List<ScoringModalidadProgramaDTO>>(_unitOfWork.ProgramaGeneralPerfilScoringModalidadRepository.ObtenerPorIdPGeneral(idPGeneral));
            modalidad.ModalidadCoefiente = _mapper.Map<List<ProgramaGeneralPerfilCoeficienteDTO>>(_unitOfWork.ProgramaGeneralPerfilModalidadCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral));
            perfilPrograma.CoeficienteScoringModalidad = modalidad;

            CoeficienteScoringAFormacionDTO formacion = new CoeficienteScoringAFormacionDTO();
            formacion.FormacionScoring = _mapper.Map<List<ScoringAFormacionProgramaDTO>>(_unitOfWork.ProgramaGeneralPerfilScoringAformacionRepository.ObtenerPorIdPGeneral(idPGeneral));
            formacion.FormacionCoefiente = _mapper.Map<List<ProgramaGeneralPerfilCoeficienteDTO>>(_unitOfWork.ProgramaGeneralPerfilAformacionCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral));
            perfilPrograma.CoeficienteScoringAFormacion = formacion;

            CoeficienteScoringIndustriaDTO industria = new CoeficienteScoringIndustriaDTO();
            industria.IndustriaScoring = _mapper.Map<List<ScoringIndustriaProgramaDTO>>(_unitOfWork.ProgramaGeneralPerfilScoringIndustriaRepository.ObtenerPorIdPGeneral(idPGeneral));
            industria.IndustriaCoefiente = _mapper.Map<List<ProgramaGeneralPerfilCoeficienteDTO>>(_unitOfWork.ProgramaGeneralPerfilIndustriaCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral));
            perfilPrograma.CoeficienteScoringIndustria = industria;

            CoeficienteScoringCargoDTO cargo = new CoeficienteScoringCargoDTO();
            cargo.CargoScoring = _mapper.Map<List<ScoringCargoProgramaDTO>>(_unitOfWork.ProgramaGeneralPerfilScoringCargoRepository.ObtenerPorIdPGeneral(idPGeneral));
            cargo.CargoCoefiente = _mapper.Map<List<ProgramaGeneralPerfilCoeficienteDTO>>(_unitOfWork.ProgramaGeneralPerfilCargoCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral));
            perfilPrograma.CoeficienteScoringCargo = cargo;

            CoeficienteScoringATrabajoDTO trabajo = new CoeficienteScoringATrabajoDTO();
            trabajo.TrabajoScoring = _mapper.Map<List<ScoringTrabajoProgramaDTO>>(_unitOfWork.ProgramaGeneralPerfilScoringAtrabajoRepository.ObtenerPorIdPGeneral(idPGeneral));
            trabajo.TrabajoCoefiente = _mapper.Map<List<ProgramaGeneralPerfilCoeficienteDTO>>(_unitOfWork.ProgramaGeneralPerfilAtrabajoCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral));
            perfilPrograma.CoeficienteScoringATrabajo = trabajo;

            CoeficienteScoringCategoriaDTO categoria = new CoeficienteScoringCategoriaDTO();
            categoria.CategoriaScoring = _mapper.Map<List<ScoringCategoriaProgramaDTO>>(_unitOfWork.ProgramaGeneralPerfilScoringCategoriaRepository.ObtenerPorIdPGeneral(idPGeneral));
            categoria.CategoriaCoefiente = _mapper.Map<List<ProgramaGeneralPerfilCoeficienteDTO>>(_unitOfWork.ProgramaGeneralPerfilCategoriaCoeficienteRepository.ObtenerPorIdPGeneral(idPGeneral));
            perfilPrograma.CoeficienteScoringCategoria = categoria;

            perfilPrograma.EscalaProbabilidads = _mapper.Map<List<EscalaProbabilidadDTO>>(_unitOfWork.ProgramaGeneralPerfilEscalaProbabilidadRepository.ObtenerPorIdPGeneral(idPGeneral));
            perfilPrograma.ProgramaGeneralPerfilIntercepto = _mapper.Map<ProgramaGeneralPerfilInterceptoDTO>(_unitOfWork.ProgramaGeneralPerfilInterceptoRepository.ObtenerPorIdPGeneral(idPGeneral));
            perfilPrograma.TipoDatoPerFilContactoProgramas = _mapper.Map<List<TipoDatoPerFilContactoProgramaDTO>>(_unitOfWork.ProgramaGeneralPerfilTipoDatoRepository.ObtenerPorIdPGeneral(idPGeneral));
            return perfilPrograma;
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool ActualizarEstadoPrograma(int idPrograma, bool estado, string usuario)
        {
            try
            {
                var programaGeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(idPrograma);
                if (programaGeneral == null || programaGeneral.Id == 0)
                {
                    throw new BadRequestException($"No existe la entidad pgeneral {idPrograma}");
                }
                programaGeneral.Estado = estado;
                programaGeneral.FechaModificacion = DateTime.Now;
                programaGeneral.UsuarioModificacion = usuario;
                _unitOfWork.PGeneralRepository.Update(programaGeneral);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="beneficioDTO">Dto principal</param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public ProgramaGeneralBeneficioDTO GuardarBeneficiosVentas(CompuestoBeneficioModalidadDTO beneficioDTO, string usuario)
        {
            try
            {
                ProgramaGeneralBeneficioDTO resultado = new();
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_unitOfWork.ProgramaGeneralBeneficioRepository.Exist(beneficioDTO.IdBeneficio))
                    {
                        var beneficio = _unitOfWork.ProgramaGeneralBeneficioRepository.ObtenerPorId(beneficioDTO.IdBeneficio)!;
                        beneficio.IdPgeneral = beneficioDTO.IdPGeneral;
                        beneficio.Nombre = beneficioDTO.NombreBeneficio;
                        beneficio.UsuarioModificacion = usuario;
                        beneficio.FechaModificacion = DateTime.Now;

                        var listaBorrar = _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository
                            .GetBy(x => x.IdProgramaGeneralBeneficio == beneficioDTO.IdBeneficio).ToList();
                        listaBorrar.RemoveAll(x => beneficioDTO.BeneficiosArgumentos.Any(y => y.Id == x.Id));
                        if (listaBorrar != null && listaBorrar.Count() > 0)
                        {
                            _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }

                        var listaBorrar2 = _unitOfWork.ProgramaGeneralBeneficioModalidadRepository
                            .GetBy(x => x.IdProgramaGeneralBeneficio == beneficioDTO.IdBeneficio).ToList();
                        listaBorrar2.RemoveAll(x => beneficioDTO.Modalidades.Any(y => y.IdModalidad == x.IdModalidadCurso));
                        if (listaBorrar2 != null && listaBorrar2.Count() > 0)
                        {
                            _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Delete(listaBorrar2.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }

                        beneficio.ProgramaGeneralBeneficioArgumentos = new List<ProgramaGeneralBeneficioArgumento>();
                        foreach (var subItem in beneficioDTO.BeneficiosArgumentos)
                        {
                            ProgramaGeneralBeneficioArgumento argumento;
                            if (subItem.Id != 0 && _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Exist(subItem.Id))
                            {
                                argumento = _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.ObtenerPorId(subItem.Id)!;
                                argumento.Nombre = subItem.Nombre;
                                argumento.IdPgeneral = beneficioDTO.IdPGeneral;
                                argumento.UsuarioModificacion = usuario;
                                argumento.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                argumento = new ProgramaGeneralBeneficioArgumento()
                                {
                                    Nombre = subItem.Nombre,
                                    IdPgeneral = beneficioDTO.IdPGeneral,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }
                            beneficio.ProgramaGeneralBeneficioArgumentos.Add(argumento);
                        }
                        beneficio.ProgramaGeneralBeneficioModalidads = new List<ProgramaGeneralBeneficioModalidad>();
                        foreach (var subItem in beneficioDTO.Modalidades)
                        {
                            ProgramaGeneralBeneficioModalidad modalidad;
                            if (!_unitOfWork.ProgramaGeneralBeneficioModalidadRepository.Exist(x => x.IdProgramaGeneralBeneficio == beneficio.Id && x.IdModalidadCurso == subItem.IdModalidad))
                            {
                                modalidad = new ProgramaGeneralBeneficioModalidad()
                                {
                                    Nombre = subItem.Nombre ?? "",
                                    IdPgeneral = beneficioDTO.IdPGeneral,
                                    IdModalidadCurso = subItem.IdModalidad,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                                beneficio.ProgramaGeneralBeneficioModalidads.Add(modalidad);
                            }
                        }
                        _unitOfWork.ProgramaGeneralBeneficioRepository.Update(beneficio);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        var beneficio = new ProgramaGeneralBeneficio()
                        {
                            IdPgeneral = beneficioDTO.IdPGeneral,
                            Nombre = beneficioDTO.NombreBeneficio,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        beneficio.ProgramaGeneralBeneficioArgumentos = beneficioDTO.BeneficiosArgumentos.Select(s => new ProgramaGeneralBeneficioArgumento
                        {
                            Nombre = s.Nombre,
                            IdPgeneral = beneficioDTO.IdPGeneral,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        }).ToList();
                        beneficio.ProgramaGeneralBeneficioModalidads = beneficioDTO.Modalidades.Select(x => new ProgramaGeneralBeneficioModalidad
                        {
                            Nombre = x.Nombre ?? "",
                            IdPgeneral = beneficioDTO.IdPGeneral,
                            IdModalidadCurso = x.IdModalidad,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        }).ToList();
                        var res = _unitOfWork.ProgramaGeneralBeneficioRepository.Add(beneficio);
                        _unitOfWork.Commit();
                        beneficioDTO.IdBeneficio = res.Id;
                    }
                    scope.Complete();
                }
                resultado = _mapper.Map<ProgramaGeneralBeneficioDTO>(_unitOfWork.ProgramaGeneralBeneficioRepository.ObtenerPorId(beneficioDTO.IdBeneficio));
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla.
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de programas generales padre
        /// </summary>
        /// <returns>List<ProgramaGeneralSubAreaFiltroDTO></returns>
        public List<ProgramaGeneralSubAreaFiltroDTO> ObtenerProgramaGeneralPadre(int? tipo)
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerProgramaGeneralPadre(tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idProgramaGeneralBeneficio"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool EliminarBeneficioVenta(int idProgramaGeneralBeneficio, string usuario)
        {
            try
            {
                _unitOfWork.ProgramaGeneralBeneficioRepository.Delete(idProgramaGeneralBeneficio, usuario);

                var listaBorrar = _unitOfWork.ProgramaGeneralBeneficioModalidadRepository.GetBy(x => x.IdProgramaGeneralBeneficio == idProgramaGeneralBeneficio && x.Estado == true).ToList();
                _unitOfWork.ProgramaGeneralBeneficioModalidadRepository.Delete(listaBorrar.Select(x => x.Id), usuario);

                var listaBorrar2 = _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.GetBy(x => x.IdProgramaGeneralBeneficio == idProgramaGeneralBeneficio && x.Estado == true).ToList();
                _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Delete(listaBorrar2.Select(x => x.Id), usuario);

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<PGeneralCursoCriterioHijoVistaDTO> ObtenerPPadreCEvaluacionPresencial(int idPgeneral)
        {
            try
            {
                return _unitOfWork.PgeneralCriterioEvaluacionHijoRepository.ListarPadreCursosCriteriosPresencial(idPgeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idPgeneral"></param>
        /// <returns></returns>
        public List<PGeneralCursoCriterioHijoVistaDTO> ObtenerPPadreCEvaluacionAonline(int idPgeneral)
        {
            try
            {
                return _unitOfWork.PgeneralCriterioEvaluacionHijoRepository.ListarPadreCursosCriteriosAonline(idPgeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idPgeneral"></param>
        /// <returns></returns>
        public bool ActualizarInsertarPGCEvaluacionHijo(PgeneralCriterioEvaluacionHijoV2DTO dto, string usuario)
        {
            try
            {
                if (dto.Id == 0)
                {
                    PgeneralCriterioEvaluacionHijo pgCriterioEvaluacionBO = new();
                    pgCriterioEvaluacionBO.IdPgeneral = dto.IdPgeneral;
                    pgCriterioEvaluacionBO.IdModalidadCurso = dto.IdModalidadCurso;
                    pgCriterioEvaluacionBO.ConsiderarNota = dto.ConsiderarNota;
                    pgCriterioEvaluacionBO.Porcentaje = dto.Porcentaje;
                    pgCriterioEvaluacionBO.IdTipoPromedio = dto.IdTipoPromedio;
                    pgCriterioEvaluacionBO.Estado = true;
                    pgCriterioEvaluacionBO.FechaCreacion = DateTime.Now;
                    pgCriterioEvaluacionBO.FechaModificacion = DateTime.Now;
                    pgCriterioEvaluacionBO.UsuarioCreacion = usuario;
                    pgCriterioEvaluacionBO.UsuarioModificacion = usuario;

                    _unitOfWork.PgeneralCriterioEvaluacionHijoRepository.Add(pgCriterioEvaluacionBO);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    var pgCriterioEvaluacionBO = _unitOfWork.PgeneralCriterioEvaluacionHijoRepository.ObtenerPorId(dto.Id);
                    if (pgCriterioEvaluacionBO != null)
                    {
                        pgCriterioEvaluacionBO.IdModalidadCurso = dto.IdModalidadCurso;
                        pgCriterioEvaluacionBO.ConsiderarNota = dto.ConsiderarNota;
                        pgCriterioEvaluacionBO.Porcentaje = dto.Porcentaje;
                        pgCriterioEvaluacionBO.IdTipoPromedio = dto.IdTipoPromedio;
                        pgCriterioEvaluacionBO.FechaModificacion = DateTime.Now;
                        pgCriterioEvaluacionBO.UsuarioModificacion = usuario;

                        _unitOfWork.PgeneralCriterioEvaluacionHijoRepository.Update(pgCriterioEvaluacionBO);
                        _unitOfWork.Commit();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idPgeneral"></param>
        /// <returns></returns>
        public List<PGeneralCursoCriterioHijoVistaDTO> ObtenerPPadreCEvaluacionOnline(int idPgeneral)
        {
            try
            {
                return _unitOfWork.PgeneralCriterioEvaluacionHijoRepository.ListarPadreCursosCriteriosOnline(idPgeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public PGeneralCriterioEvaluacionDTO ActualizarInsertarPGCEvaluacion(PGeneralCriterioEvaluacionDTO dto, string usuario)
        {
            try
            {
                PgeneralCriterioEvaluacion? pgCriterioEvaluacion = new();
                if (dto.Id == 0)
                {
                    pgCriterioEvaluacion.IdPgeneral = dto.IdPgeneral;
                    pgCriterioEvaluacion.IdModalidadCurso = dto.IdModalidadCurso;
                    pgCriterioEvaluacion.IdCriterioEvaluacion = dto.IdCriterioEvaluacion;
                    pgCriterioEvaluacion.Nombre = "";
                    pgCriterioEvaluacion.Porcentaje = dto.Porcentaje;
                    pgCriterioEvaluacion.IdTipoPromedio = dto.IdTipoPromedio;
                    pgCriterioEvaluacion.IdCriterioEvaluacion = dto.IdCriterioEvaluacion;
                    pgCriterioEvaluacion.Estado = true;
                    pgCriterioEvaluacion.FechaCreacion = DateTime.Now;
                    pgCriterioEvaluacion.FechaModificacion = DateTime.Now;
                    pgCriterioEvaluacion.UsuarioCreacion = usuario;
                    pgCriterioEvaluacion.UsuarioModificacion = usuario;

                    var resultado = _unitOfWork.PGeneralCriterioEvaluacionRepository.Add(pgCriterioEvaluacion);
                    _unitOfWork.Commit();
                    return _mapper.Map<PGeneralCriterioEvaluacionDTO>(resultado);
                }
                else
                {
                    pgCriterioEvaluacion = _unitOfWork.PGeneralCriterioEvaluacionRepository.ObtenerPorId(dto.Id);
                    if (pgCriterioEvaluacion != null)
                    {
                        pgCriterioEvaluacion.IdModalidadCurso = dto.IdModalidadCurso;
                        pgCriterioEvaluacion.Porcentaje = dto.Porcentaje;
                        pgCriterioEvaluacion.IdTipoPromedio = dto.IdTipoPromedio;
                        pgCriterioEvaluacion.FechaModificacion = DateTime.Now;
                        pgCriterioEvaluacion.UsuarioModificacion = usuario;

                        _unitOfWork.PGeneralCriterioEvaluacionRepository.Update(pgCriterioEvaluacion);
                        _unitOfWork.Commit();
                        return _mapper.Map<PGeneralCriterioEvaluacionDTO>(pgCriterioEvaluacion);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool EliminarCriterioEvaluacion(int idCriterioEvaluacion, string usuario)
        {
            try
            {
                bool result = false;
                if (_unitOfWork.PGeneralCriterioEvaluacionRepository.Exist(idCriterioEvaluacion))
                {
                    result = _unitOfWork.PGeneralCriterioEvaluacionRepository.Delete(idCriterioEvaluacion, usuario);
                    _unitOfWork.Commit();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ProgramaGeneralModeloCertificadoDTO GuardarModeloCertificado(CompuestoModeloCertificadoModalidadDTO jsonDTO, string usuario)
        {
            try
            {
                string NombreArchivotemp = "";
                string ContentType = "";
                var urlArchivoRepositorio = "";
                if (jsonDTO.Files != null)
                {
                    foreach (var file in jsonDTO.Files)
                    {
                        ContentType = file.ContentType;
                        NombreArchivotemp = file.FileName;
                        NombreArchivotemp = string.Concat(NombreArchivotemp);
                        urlArchivoRepositorio = _unitOfWork.MaterialVersionRepository.SubirModeloCertificadoRepositorio(file.ConvertToByte(), file.ContentType, NombreArchivotemp);
                    }
                }
                else
                {
                    if (jsonDTO.UrlAnterior == null)
                    {
                        throw new BadRequestException("Necesita adjuntar");
                    }
                    else
                    {
                        urlArchivoRepositorio = jsonDTO.UrlAnterior;
                    }
                }
                var modalidades = jsonDTO.Modalidades.Split(",");
                List<ModalidadCursoProblemaDTO> modalidadCurso = new List<ModalidadCursoProblemaDTO>();
                foreach (var item in modalidades)
                {
                    ModalidadCursoProblemaDTO mod = new ModalidadCursoProblemaDTO();
                    var idModalidad = int.Parse(item);
                    var modalidadBD = _unitOfWork.ProgramaGeneralModeloCertificadoModalidadRepository.ObtenerPorIdModeloCertificadoIdModalidadCurso(jsonDTO.IdModeloCertificado, idModalidad);
                    if (modalidadBD != null)
                    {
                        mod.Id = modalidadBD.Id;
                        mod.Nombre = modalidadBD.Nombre;
                        mod.IdModalidad = modalidadBD.IdModalidadCurso;
                    }
                    else
                    {
                        mod.Id = 0;
                        mod.Nombre = _unitOfWork.ModalidadCursoRepository.ObtenerPorId(idModalidad)!.Nombre;
                        mod.IdModalidad = idModalidad;
                    }
                    modalidadCurso.Add(mod);
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_unitOfWork.ProgramaGeneralModeloCertificadoRepository.Exist(jsonDTO.IdModeloCertificado))
                    {
                        var modelo = _unitOfWork.ProgramaGeneralModeloCertificadoRepository.ObtenerPorId(jsonDTO.IdModeloCertificado)!;
                        modelo.IdPgeneral = jsonDTO.IdPGeneral;
                        modelo.Nombre = jsonDTO.NombreModeloCertificado;
                        modelo.Url = urlArchivoRepositorio;
                        modelo.UsuarioModificacion = usuario;
                        modelo.FechaModificacion = DateTime.Now;

                        var listaBorrar = _unitOfWork.ProgramaGeneralModeloCertificadoModalidadRepository.GetBy(x => x.IdProgramaGeneralModeloCertificado == jsonDTO.IdModeloCertificado && x.Estado == true).ToList();
                        listaBorrar.RemoveAll(x => modalidadCurso.Any(y => y.IdModalidad == x.IdModalidadCurso));
                        if (listaBorrar != null && listaBorrar.Count() > 0)
                        {
                            _unitOfWork.ProgramaGeneralModeloCertificadoModalidadRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }
                        modelo.ProgramaGeneralModeloCertificadoModalidads = new List<ProgramaGeneralModeloCertificadoModalidad>();
                        foreach (var subItem in modalidadCurso)
                        {
                            if (!_unitOfWork.ProgramaGeneralModeloCertificadoModalidadRepository
                                .Exist(x => x.IdProgramaGeneralModeloCertificado == jsonDTO.IdModeloCertificado
                                    && x.IdModalidadCurso == subItem.IdModalidad))
                            {
                                var modalidad = new ProgramaGeneralModeloCertificadoModalidad()
                                {
                                    Nombre = subItem.Nombre,
                                    IdPgeneral = jsonDTO.IdPGeneral,
                                    IdModalidadCurso = subItem.IdModalidad,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                                modelo.ProgramaGeneralModeloCertificadoModalidads.Add(modalidad);
                            }
                        }
                        _unitOfWork.ProgramaGeneralModeloCertificadoRepository.Update(modelo);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        var modelo = new ProgramaGeneralModeloCertificado();
                        modelo.IdPgeneral = jsonDTO.IdPGeneral;
                        modelo.Nombre = jsonDTO.NombreModeloCertificado;
                        modelo.Url = urlArchivoRepositorio;
                        modelo.UsuarioCreacion = usuario;
                        modelo.UsuarioModificacion = usuario;
                        modelo.FechaCreacion = DateTime.Now;
                        modelo.FechaModificacion = DateTime.Now;
                        modelo.Estado = true;

                        modelo.ProgramaGeneralModeloCertificadoModalidads = modalidadCurso.Select(x => new ProgramaGeneralModeloCertificadoModalidad()
                        {
                            Nombre = x.Nombre,
                            IdPgeneral = jsonDTO.IdPGeneral,
                            IdModalidadCurso = x.IdModalidad,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        }).ToList();
                        var res = _unitOfWork.ProgramaGeneralModeloCertificadoRepository.Add(modelo);
                        _unitOfWork.Commit();
                        jsonDTO.IdModeloCertificado = res.Id;
                    }
                    scope.Complete();
                }
                var resultado = _unitOfWork.ProgramaGeneralModeloCertificadoRepository.ObtenerPorId(jsonDTO.IdModeloCertificado);
                return _mapper.Map<ProgramaGeneralModeloCertificadoDTO>(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool EliminarModeloCertificado(int idProgramaGeneralModelo, string usuario)
        {

            try
            {
                _unitOfWork.ProgramaGeneralModeloCertificadoRepository.Delete(idProgramaGeneralModelo, usuario);
                var listaBorrar = _unitOfWork.ProgramaGeneralModeloCertificadoModalidadRepository.GetBy(x => x.IdProgramaGeneralModeloCertificado == idProgramaGeneralModelo && x.Estado == true).ToList();
                _unitOfWork.ProgramaGeneralModeloCertificadoModalidadRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ModeloPredictivoProgramaDTO ObtenerModeloPredictivo(int IdProgramaGeneral)
        {
            try
            {
                ModeloPredictivoProgramaDTO modeloPredicitivo = new ModeloPredictivoProgramaDTO
                {
                    Cargo = _unitOfWork.ModeloPredictivoCargoRepository.ObtenerCargoPorPrograma(IdProgramaGeneral),
                    Industria = _unitOfWork.ModeloPredictivoIndustriaRepository.ObtenerIndustriaPorPrograma(IdProgramaGeneral),
                    Formacion = _unitOfWork.ModeloPredictivoFormacionRepository.ObtenerAreaFormacionPorPrograma(IdProgramaGeneral),
                    CategoriaOrigen = _unitOfWork.ModeloPredictivoCategoriaDatoRepository.ObtenerCategoriaDatoPorPrograma(IdProgramaGeneral),
                    Trabajo = _unitOfWork.ModeloPredictivoTrabajoRepository.ObtenerTrabajoPorPrograma(IdProgramaGeneral),
                    Escala = _unitOfWork.ModeloPredictivoEscalaProbabilidadRepository.ObtenerEscalaPorPrograma(IdProgramaGeneral),
                    TipoDato = _unitOfWork.ModeloPredictivoTipoDatoRepository.ObtenerTipoDatoPorPrograma(IdProgramaGeneral),
                    Intercepto = _unitOfWork.ModeloPredictivoRepository.ObtenerInterceptoPorPrograma(IdProgramaGeneral)
                };

                return modeloPredicitivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool EliminarMotivacionVenta(int idProgramaGeneralMotivacion, string usuario)
        {

            try
            {
                _unitOfWork.ProgramaGeneralMotivacionRepository.Delete(idProgramaGeneralMotivacion, usuario);
                var listaBorrar = _unitOfWork.ProgramaGeneralMotivacionModalidadRepository.GetBy(x => x.IdProgramaGeneralMotivacion == idProgramaGeneralMotivacion && x.Estado == true).ToList();
                _unitOfWork.ProgramaGeneralMotivacionModalidadRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                var listaBorrar2 = _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.GetBy(x => x.IdProgramaGeneralMotivacion == idProgramaGeneralMotivacion && x.Estado == true).ToList();
                _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.Delete(listaBorrar2.Select(x => x.Id), usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ProgramaGeneralMotivacionDTO GuardarMotivacionesVentas(CompuestoMotivacionModalidadDTO motivacionDTO, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_unitOfWork.ProgramaGeneralMotivacionRepository.Exist(motivacionDTO.IdMotivacion))
                    {
                        var motivacion = _unitOfWork.ProgramaGeneralMotivacionRepository.ObtenerPorId(motivacionDTO.IdMotivacion)!;
                        motivacion.IdPgeneral = motivacionDTO.IdPGeneral;
                        motivacion.Nombre = motivacionDTO.NombreMotivacion;
                        motivacion.UsuarioModificacion = usuario;
                        motivacion.FechaModificacion = DateTime.Now;

                        var listaBorrar = _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository
                            .GetBy(x => x.IdProgramaGeneralMotivacion == motivacionDTO.IdMotivacion).ToList();
                        listaBorrar.RemoveAll(x => motivacionDTO.MotivacionesArgumentos.Any(y => y.Id == x.Id));
                        if (listaBorrar != null && listaBorrar.Count() > 0)
                        {
                            _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }

                        var listaBorrar2 = _unitOfWork.ProgramaGeneralMotivacionModalidadRepository
                            .GetBy(x => x.IdProgramaGeneralMotivacion == motivacionDTO.IdMotivacion).ToList();
                        listaBorrar2.RemoveAll(x => motivacionDTO.Modalidades.Any(y => y.IdModalidad == x.IdModalidadCurso));

                        if (listaBorrar2 != null && listaBorrar2.Count() > 0)
                        {
                            _unitOfWork.ProgramaGeneralMotivacionModalidadRepository.Delete(listaBorrar2.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }

                        motivacion.ProgramaGeneralMotivacionArgumentos = new List<ProgramaGeneralMotivacionArgumento>();
                        foreach (var subItem in motivacionDTO.MotivacionesArgumentos)
                        {
                            ProgramaGeneralMotivacionArgumento argumento;
                            if (_unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.Exist(subItem.Id))
                            {
                                argumento = _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.ObtenerPorId(subItem.Id)!;
                                argumento.Nombre = subItem.Nombre;
                                argumento.IdPgeneral = motivacionDTO.IdPGeneral;
                                motivacion.UsuarioModificacion = usuario;
                                motivacion.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                argumento = new ProgramaGeneralMotivacionArgumento();
                                argumento.Nombre = subItem.Nombre;
                                argumento.IdPgeneral = motivacionDTO.IdPGeneral;
                                argumento.UsuarioCreacion = usuario;
                                argumento.UsuarioModificacion = usuario;
                                argumento.FechaCreacion = DateTime.Now;
                                argumento.FechaModificacion = DateTime.Now;
                                argumento.Estado = true;
                            }
                            motivacion.ProgramaGeneralMotivacionArgumentos.Add(argumento);
                        }
                        motivacion.ProgramaGeneralMotivacionModalidads = new List<ProgramaGeneralMotivacionModalidad>();
                        foreach (var subItem in motivacionDTO.Modalidades)
                        {
                            ProgramaGeneralMotivacionModalidad modalidad;
                            if (!_unitOfWork.ProgramaGeneralMotivacionModalidadRepository.Exist(x => x.IdProgramaGeneralMotivacion == motivacion.Id && x.IdModalidadCurso == subItem.IdModalidad))
                            {
                                modalidad = new ProgramaGeneralMotivacionModalidad()
                                {
                                    Nombre = subItem.Nombre,
                                    IdPgeneral = motivacionDTO.IdPGeneral,
                                    IdModalidadCurso = subItem.IdModalidad,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                                motivacion.ProgramaGeneralMotivacionModalidads.Add(modalidad);
                            }
                        }
                        _unitOfWork.ProgramaGeneralMotivacionRepository.Update(motivacion);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        var motivacion = new ProgramaGeneralMotivacion();
                        motivacion.IdPgeneral = motivacionDTO.IdPGeneral;
                        motivacion.Nombre = motivacionDTO.NombreMotivacion;
                        motivacion.UsuarioCreacion = usuario;
                        motivacion.UsuarioModificacion = usuario;
                        motivacion.FechaCreacion = DateTime.Now;
                        motivacion.FechaModificacion = DateTime.Now;
                        motivacion.Estado = true;

                        motivacion.ProgramaGeneralMotivacionArgumentos = motivacionDTO.MotivacionesArgumentos
                            .Select(x => new ProgramaGeneralMotivacionArgumento
                            {
                                Nombre = x.Nombre,
                                IdPgeneral = motivacionDTO.IdPGeneral,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            }).ToList();
                        motivacion.ProgramaGeneralMotivacionModalidads = motivacionDTO.Modalidades
                            .Select(x => new ProgramaGeneralMotivacionModalidad()
                            {
                                Nombre = x.Nombre,
                                IdPgeneral = motivacionDTO.IdPGeneral,
                                IdModalidadCurso = x.IdModalidad,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            }).ToList();
                        var res = _unitOfWork.ProgramaGeneralMotivacionRepository.Add(motivacion);
                        _unitOfWork.Commit();
                        motivacionDTO.IdMotivacion = res.Id;
                    }
                    scope.Complete();
                }
                var resultado = _unitOfWork.ProgramaGeneralMotivacionRepository.ObtenerPorId(motivacionDTO.IdMotivacion);
                return _mapper.Map<ProgramaGeneralMotivacionDTO>(resultado);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ProgramaGeneralProblemaDTO GuardarProblemasVentas(CompuestoProblemaModalidadDTO problemaDTO, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_unitOfWork.ProgramaGeneralProblemaRepository.Exist(problemaDTO.IdProblema))
                    {
                        var problema = _unitOfWork.ProgramaGeneralProblemaRepository.ObtenerPorId(problemaDTO.IdProblema)!;
                        problema.IdPgeneral = problemaDTO.IdPGeneral;
                        problema.Nombre = problemaDTO.NombreProblema;
                        problema.EsVisibleAgenda = problemaDTO.EsVisibleAgenda;
                        problema.UsuarioModificacion = usuario;
                        problema.FechaModificacion = DateTime.Now;

                        var listaBorrar = _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository
                            .GetBy(x => x.IdProgramaGeneralProblema == problemaDTO.IdProblema).ToList();
                        listaBorrar.RemoveAll(x => problemaDTO.ProblemasArgumentos.Any(y => y.Id == x.Id));
                        if (listaBorrar != null && listaBorrar.Count() > 0)
                        {
                            _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }

                        var listaBorrar2 = _unitOfWork.ProgramaGeneralProblemaModalidadRepository
                            .GetBy(x => x.IdProgramaGeneralProblema == problemaDTO.IdProblema).ToList();
                        listaBorrar2.RemoveAll(x => problemaDTO.Modalidades.Any(y => y.Id == x.IdModalidadCurso));
                        if (listaBorrar2 != null && listaBorrar2.Count() > 0)
                        {
                            _unitOfWork.ProgramaGeneralProblemaModalidadRepository.Delete(listaBorrar2.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }

                        problema.ProgramaGeneralProblemaDetalleSolucion = new List<ProgramaGeneralProblemaDetalleSolucion>();
                        foreach (var subItem in problemaDTO.ProblemasArgumentos)
                        {
                            ProgramaGeneralProblemaDetalleSolucion detalleSolucion;
                            if (subItem.Id != null && _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.Exist(subItem.Id.Value))
                            {
                                detalleSolucion = _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.ObtenerPorId(subItem.Id.Value)!;
                                detalleSolucion.Detalle = subItem.Detalle;
                                detalleSolucion.Solucion = subItem.Solucion;
                                detalleSolucion.IdPgeneral = problemaDTO.IdPGeneral;
                                problema.UsuarioModificacion = usuario;
                                problema.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                detalleSolucion = new();
                                detalleSolucion.Detalle = subItem.Detalle;
                                detalleSolucion.Solucion = subItem.Solucion;
                                detalleSolucion.IdPgeneral = problemaDTO.IdPGeneral;
                                detalleSolucion.UsuarioCreacion = usuario;
                                detalleSolucion.UsuarioModificacion = usuario;
                                detalleSolucion.FechaCreacion = DateTime.Now;
                                detalleSolucion.FechaModificacion = DateTime.Now;
                                detalleSolucion.Estado = true;
                            }
                            problema.ProgramaGeneralProblemaDetalleSolucion.Add(detalleSolucion);
                        }
                        problema.ProgramaGeneralProblemaModalidad = new List<ProgramaGeneralProblemaModalidad>();
                        foreach (var subItem in problemaDTO.Modalidades)
                        {
                            ProgramaGeneralProblemaModalidad modalidad;
                            if (!_unitOfWork.ProgramaGeneralProblemaModalidadRepository.Exist(x => x.IdProgramaGeneralProblema == problema.Id && x.IdModalidadCurso == subItem.IdModalidad))
                            {
                                modalidad = new ProgramaGeneralProblemaModalidad()
                                {
                                    Nombre = subItem.Nombre,
                                    IdPgeneral = problemaDTO.IdPGeneral,
                                    IdModalidadCurso = subItem.IdModalidad,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                                problema.ProgramaGeneralProblemaModalidad.Add(modalidad);
                            }
                        }
                        _unitOfWork.ProgramaGeneralProblemaRepository.Update(problema);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        var problema = new ProgramaGeneralProblema();
                        problema.IdPgeneral = problemaDTO.IdPGeneral;
                        problema.Nombre = problemaDTO.NombreProblema;
                        problema.EsVisibleAgenda = problemaDTO.EsVisibleAgenda;
                        problema.UsuarioCreacion = usuario;
                        problema.UsuarioModificacion = usuario;
                        problema.FechaCreacion = DateTime.Now;
                        problema.FechaModificacion = DateTime.Now;
                        problema.Estado = true;

                        problema.ProgramaGeneralProblemaDetalleSolucion = problemaDTO.ProblemasArgumentos
                            .Select(x => new ProgramaGeneralProblemaDetalleSolucion
                            {
                                Detalle = x.Detalle,
                                Solucion = x.Solucion,
                                IdPgeneral = problemaDTO.IdPGeneral,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            }).ToList();
                        problema.ProgramaGeneralProblemaModalidad = problemaDTO.Modalidades
                            .Select(x => new ProgramaGeneralProblemaModalidad()
                            {
                                Nombre = x.Nombre,
                                IdPgeneral = problemaDTO.IdPGeneral,
                                IdModalidadCurso = x.IdModalidad,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            }).ToList();
                        var res = _unitOfWork.ProgramaGeneralProblemaRepository.Add(problema);
                        _unitOfWork.Commit();
                        problemaDTO.IdProblema = res.Id;
                    }
                    scope.Complete();
                }
                var resultado = _unitOfWork.ProgramaGeneralProblemaRepository.ObtenerPorId(problemaDTO.IdProblema);
                return _mapper.Map<ProgramaGeneralProblemaDTO>(resultado);
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
        /// <returns></returns>
        public ConfiguracionBeneficioProgramaGeneralAlternoDTO? ObtenerInformacionBeneficioRequisitpDetalle(int idProgramaGeneral, int idBeneficio)
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerPorIdPgeneralIdBeneficio(idProgramaGeneral, idBeneficio).FirstOrDefault();
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
        /// <returns></returns>
        public bool GuardarBeneficiosPreRequisitos(BeneficioPreRequisitoProgramaDTO jsonDTO, string usuario)
        {
            try
            {
                List<ProgramaGeneralBeneficioArgumento> argumentos;
                List<ProgramaGeneralBeneficioModalidad> modalidadBeneficios;
                List<ProgramaGeneralPrerequisitoModalidad> modalidadPreRequisito;
                bool isNewBeneficio = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    var eliminarPreRequisitos = _unitOfWork.ProgramaGeneralPrerequisitoRepository.GetBy(x => x.IdPgeneral == jsonDTO.IdPGeneral && x.Estado == true).Where(x => !jsonDTO.PreRequisitos.Any(y => y.IdPreRequisito == x.Id)).Select(x => x.Id).ToList();
                    if (eliminarPreRequisitos != null && eliminarPreRequisitos.Count() > 0)
                    {
                        _unitOfWork.ProgramaGeneralPrerequisitoRepository.Delete(eliminarPreRequisitos, usuario);
                        var eliminarPrerequisitoModalidad = _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.GetBy(x => eliminarPreRequisitos.Contains(x.IdProgramaGeneralPrerequisito) && x.Estado == true).Select(x => x.Id).ToList();
                        if (eliminarPrerequisitoModalidad != null && eliminarPrerequisitoModalidad.Count() > 0)
                            _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.Delete(eliminarPrerequisitoModalidad, usuario);
                        _unitOfWork.Commit();
                    }

                    var eliminarBeneficios = _unitOfWork.ProgramaGeneralBeneficioRepository.GetBy(x => x.IdPgeneral == jsonDTO.IdPGeneral && x.Estado == true).Where(x => !jsonDTO.Beneficios.Any(y => y.IdBeneficio == x.Id)).Select(x => x.Id).ToList();

                    if (eliminarBeneficios != null && eliminarBeneficios.Count() > 0)
                    {
                        _unitOfWork.ProgramaGeneralBeneficioRepository.Delete(eliminarBeneficios, usuario);
                        var eliminarBeneficioModalidad = _unitOfWork.ProgramaGeneralBeneficioModalidadRepository.GetBy(x => eliminarBeneficios.Contains(x.IdProgramaGeneralBeneficio) && x.Estado == true).Select(x => x.Id).ToList();
                        var eliminarBeneficioArgumento = _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.GetBy(x => eliminarBeneficios.Contains(x.IdProgramaGeneralBeneficio) && x.Estado == true).Select(x => x.Id).ToList();

                        if (eliminarBeneficioModalidad != null && eliminarBeneficioModalidad.Count() > 0)
                            _unitOfWork.ProgramaGeneralBeneficioModalidadRepository.Delete(eliminarBeneficioModalidad, usuario);
                        if (eliminarBeneficioArgumento != null && eliminarBeneficioArgumento.Count() > 0)
                            _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Delete(eliminarBeneficioArgumento, usuario);
                        _unitOfWork.Commit();
                    }
                    foreach (var item in jsonDTO.Beneficios)
                    {
                        ProgramaGeneralBeneficio beneficio;
                        if (_unitOfWork.ProgramaGeneralBeneficioRepository.Exist(item.IdBeneficio))
                        {

                            beneficio = _unitOfWork.ProgramaGeneralBeneficioRepository.ObtenerPorId(item.IdBeneficio)!;
                            beneficio.IdPgeneral = item.IdPGeneral;
                            beneficio.Nombre = item.NombreBeneficio;
                            beneficio.UsuarioModificacion = usuario;
                            beneficio.FechaModificacion = DateTime.Now;

                            var eliminarProgramaGeneralBeneficioArgumento = _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.GetBy(x => x.IdProgramaGeneralBeneficio == item.IdBeneficio && x.Estado == true).Where(x => !item.BeneficiosArgumentos.Any(y => y.Id == x.Id)).Select(x => x.Id).ToList();
                            if (eliminarProgramaGeneralBeneficioArgumento != null && eliminarProgramaGeneralBeneficioArgumento.Count() > 0)
                            {
                                _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Delete(eliminarProgramaGeneralBeneficioArgumento, usuario);
                                _unitOfWork.Commit();
                            }

                            var eliminarProgramaGeneralBeneficioModalidad = _unitOfWork.ProgramaGeneralBeneficioModalidadRepository.GetBy(x => x.IdProgramaGeneralBeneficio == item.IdBeneficio && x.Estado == true).Where(x => !item.Modalidades.Any(y => y.Id == x.Id)).Select(x => x.Id).ToList();
                            if (eliminarProgramaGeneralBeneficioModalidad != null && eliminarProgramaGeneralBeneficioModalidad.Count() > 0)
                            {
                                _unitOfWork.ProgramaGeneralBeneficioModalidadRepository.Delete(eliminarProgramaGeneralBeneficioModalidad, usuario);
                                _unitOfWork.Commit();
                            }
                        }
                        else
                        {
                            beneficio = new ProgramaGeneralBeneficio()
                            {
                                IdPgeneral = item.IdPGeneral,
                                Nombre = item.NombreBeneficio,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true,
                            };
                            isNewBeneficio = true;
                        }
                        argumentos = new List<ProgramaGeneralBeneficioArgumento>();
                        foreach (var subItem in item.BeneficiosArgumentos)
                        {
                            ProgramaGeneralBeneficioArgumento argumento;
                            if (_unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Exist(subItem.Id))
                            {
                                argumento = _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.ObtenerPorId(subItem.Id)!;
                                argumento.Nombre = subItem.Nombre;
                                argumento.IdPgeneral = item.IdPGeneral;
                                beneficio.UsuarioModificacion = usuario;
                                beneficio.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                argumento = new ProgramaGeneralBeneficioArgumento()
                                {
                                    Nombre = subItem.Nombre,
                                    IdPgeneral = item.IdPGeneral,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }
                            argumentos.Add(argumento);
                        }
                        modalidadBeneficios = new List<ProgramaGeneralBeneficioModalidad>();
                        foreach (var subItem in item.Modalidades)
                        {
                            ProgramaGeneralBeneficioModalidad modalidad;
                            if (subItem.Id.GetValueOrDefault() != 0 && _unitOfWork.ProgramaGeneralBeneficioModalidadRepository.Exist(subItem.Id.GetValueOrDefault()))
                            {
                                modalidad = _unitOfWork.ProgramaGeneralBeneficioModalidadRepository.ObtenerPorId(subItem.Id.Value)!;
                                modalidad.Nombre = subItem.Nombre ?? "";
                                modalidad.IdPgeneral = item.IdPGeneral;
                                modalidad.IdModalidadCurso = subItem.IdModalidad;
                                modalidad.UsuarioModificacion = usuario;
                                modalidad.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                modalidad = new ProgramaGeneralBeneficioModalidad()
                                {
                                    Nombre = subItem.Nombre ?? "",
                                    IdPgeneral = item.IdPGeneral,
                                    IdModalidadCurso = subItem.IdModalidad,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }
                            modalidadBeneficios.Add(modalidad);
                        }
                        beneficio.ProgramaGeneralBeneficioArgumentos = argumentos;
                        beneficio.ProgramaGeneralBeneficioModalidads = modalidadBeneficios;
                        if (isNewBeneficio)
                        {
                            _unitOfWork.ProgramaGeneralBeneficioRepository.Add(beneficio);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            _unitOfWork.ProgramaGeneralBeneficioRepository.Update(beneficio);
                            _unitOfWork.Commit();
                        }
                        isNewBeneficio = false;

                    }
                    bool isNewPrerequisito = false;
                    foreach (var item in jsonDTO.PreRequisitos)
                    {

                        ProgramaGeneralPrerequisito preRequisito;
                        if (item.IdPreRequisito != 0 && _unitOfWork.ProgramaGeneralPrerequisitoRepository.Exist(item.IdPreRequisito))
                        {
                            preRequisito = _unitOfWork.ProgramaGeneralPrerequisitoRepository.ObtenerPorId(item.IdPreRequisito)!;
                            preRequisito.Nombre = item.NombrePreRequisito;
                            preRequisito.Tipo = item.Tipo;
                            preRequisito.Orden = item.Orden;
                            preRequisito.UsuarioModificacion = usuario;
                            preRequisito.FechaModificacion = DateTime.Now;

                            var eliminarProgramaGeneralPrerequisitoModalidad = _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.GetByQuery(x => x.IdProgramaGeneralPrerequisito == item.IdPreRequisito && x.Estado == true).Where(x => !item.Modalidades.Any(y => y.Id == x.Id)).Select(x => x.Id).ToList();
                            if (eliminarProgramaGeneralPrerequisitoModalidad != null && eliminarProgramaGeneralPrerequisitoModalidad.Count() > 0)
                            {
                                _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.Delete(eliminarProgramaGeneralPrerequisitoModalidad, usuario);
                                _unitOfWork.Commit();
                            }
                        }
                        else
                        {
                            preRequisito = new ProgramaGeneralPrerequisito()
                            {
                                Nombre = item.NombrePreRequisito,
                                IdPgeneral = item.IdPGeneral,
                                Tipo = item.Tipo,
                                Orden = item.Orden,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            };
                            isNewPrerequisito = true;
                        }
                        modalidadPreRequisito = new List<ProgramaGeneralPrerequisitoModalidad>();
                        foreach (var subItem in item.Modalidades)
                        {
                            ProgramaGeneralPrerequisitoModalidad preRequisitoModalidad;
                            if (subItem.Id.GetValueOrDefault() != 0 && _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.Exist(subItem.Id.GetValueOrDefault()))
                            {
                                preRequisitoModalidad = _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.ObtenerPorId(subItem.Id!.Value)!;
                                preRequisitoModalidad.Nombre = subItem.Nombre ?? "";
                                preRequisitoModalidad.IdModalidadCurso = subItem.IdModalidad;
                                preRequisitoModalidad.IdPgeneral = item.IdPGeneral;
                                preRequisito.UsuarioModificacion = usuario;
                                preRequisito.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                preRequisitoModalidad = new ProgramaGeneralPrerequisitoModalidad()
                                {
                                    Nombre = subItem.Nombre ?? "",
                                    IdModalidadCurso = subItem.IdModalidad,
                                    IdPgeneral = item.IdPGeneral,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }
                            modalidadPreRequisito.Add(preRequisitoModalidad);
                        }
                        preRequisito.ProgramaGeneralPrerequisitoModalidads = modalidadPreRequisito;
                        if (isNewPrerequisito)
                        {
                            _unitOfWork.ProgramaGeneralPrerequisitoRepository.Add(preRequisito);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            _unitOfWork.ProgramaGeneralPrerequisitoRepository.Update(preRequisito);
                            _unitOfWork.Commit();
                        }
                        isNewPrerequisito = false;
                    }
                    scope.Complete();
                }
                return true;
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
        /// <returns></returns>
        public byte[]? GenerarVistaPreviaCertificado(int idPlantillaF, int idPlantillaP, int idPrograma)
        {
            try
            {
                IDocumentoService documentoService = new DocumentoService(_unitOfWork);
                byte[] documentoByte = null;

                var tipoPlantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantillaF);
                if (tipoPlantilla == null || tipoPlantilla.Id == 0)
                {
                    throw new BadRequestException($"No existe la plantilla {idPlantillaF}");
                }
                int IdPgeneral = 0;
                string codigoCertificado = "";
                if (tipoPlantilla.Nombre.ToUpper().Contains("IRCA"))
                {
                    var datosVistaPrevia = _unitOfWork.ContenidoCertificadoIrcaRepository.ObtenerValoresVistaPreviaIrca(idPrograma);
                    if (datosVistaPrevia != null && datosVistaPrevia.Id == 0)
                    {
                        datosVistaPrevia.Id = 2;
                        datosVistaPrevia.IdPespecifico = 8627;
                        documentoByte = documentoService.GenerarCertificadoIrca(idPlantillaF, datosVistaPrevia.Id, datosVistaPrevia.IdPespecifico, ref codigoCertificado, ref IdPgeneral);
                    }
                }
                else
                {
                    documentoByte = documentoService.GenerarVistaModeloCertificado(idPlantillaF, idPlantillaP, idPrograma);
                }
                return documentoByte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BeneficioPreRequisitoDTO ObtenerBeneficiosYPreRequisitos(int IdProgramaGeneral)
        {
            try
            {
                BeneficioPreRequisitoDTO beneficioPreRequisito = new BeneficioPreRequisitoDTO();

                beneficioPreRequisito.PreRequisitos = _unitOfWork.ProgramaGeneralPrerequisitoRepository.ObtenerPreRequisitosPorModalidades(IdProgramaGeneral);
                beneficioPreRequisito.Beneficios = _unitOfWork.ProgramaGeneralBeneficioRepository.ObteneBeneficiosPorModalidades(IdProgramaGeneral);
                beneficioPreRequisito.Motivaciones = _unitOfWork.ProgramaGeneralMotivacionRepository.ObteneMotivacionesPorModalidades(IdProgramaGeneral);
                beneficioPreRequisito.Certificaciones = _unitOfWork.ProgramaGeneralCertificacionRepository.ObteneCertificacionesPorModalidades(IdProgramaGeneral);
                beneficioPreRequisito.Problemas = _unitOfWork.ProgramaGeneralProblemaRepository.ObteneProblemasPorModalidades(IdProgramaGeneral);
                beneficioPreRequisito.Modelos = _unitOfWork.ProgramaGeneralModeloCertificadoRepository.ObteneCertificacionesPorModalidades(IdProgramaGeneral);
                beneficioPreRequisito.Argumentos = _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.ObtenePresentacionArgumentoPorModalidades(IdProgramaGeneral);

                return beneficioPreRequisito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualiza información de detalle beneficio 
        /// </summary>
        /// <param name="dto">Información codificada de Detalle de Requisitos</param>
        /// <returns>Objeto Agrupado, Bool de Estado y mensaje para interfaz</returns>
        public bool ActualizarInformacionBeneficioDetalleRequisito(ConfiguracionBeneficioProgramaGeneralAlternoDTO dto, string usuario)
        {
            try
            {
                if (dto.IdPgeneral > 0 && dto.IdBeneficio > 0)
                {
                    var registro = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.FirstBy(x => x.IdPgeneral == dto.IdPgeneral && x.IdBeneficio == dto.IdBeneficio);
                    if (registro != null)
                    {
                        registro.Requisitos = dto.Requisitos;
                        registro.ProcesoSolicitud = dto.ProcesoSolicitud;
                        registro.DetallesAdicionales = dto.DetallesAdicionales;
                        registro.UsuarioModificacion = usuario;
                        registro.FechaModificacion = DateTime.Now;
                        _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.Update(registro);
                        _unitOfWork.Commit();
                        return true;
                    }
                    else
                    {
                        throw new BadRequestException("El programa no tiene asociado este beneficio");
                    }
                }
                else
                {
                    throw new BadRequestException("Falta Información Obligatoria");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
