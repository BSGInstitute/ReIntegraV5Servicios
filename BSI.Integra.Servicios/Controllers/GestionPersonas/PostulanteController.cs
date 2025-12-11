using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: PostulanteController
    /// Autor: Flavio R.M.F.
    /// Fecha: 16/04/2024
    /// <summary>
    /// Gestion de Postulante
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class PostulanteController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IPostulanteService _postulanteService;
        private IUnitOfWork _unitOfWork;

        #region ComboPostulantes
        private IPaisService _pais;
        private ICiudadService _ciudad;
        private ITipoDocumentoPersonalService _documentos;
        private IProcesoSeleccionService _procesoSeleccion;
        private IPlantillaService _plantillaService;
        private IProveedorService _proveedor;
        private IPersonalService _personalService;
        private IProcesoSeleccionEtapaService _procesoSeleccionEtapaService;
        private IEstadoEtapaProcesoSeleccionService _estadoEtapaPSServoce;
        private IConvocatoriaPersonalService _convocatoriaPersonalService;
        private IPostulanteNivelPotencialService postulanteNivelPotencialService;
        private IRespuestaPreguntaService _respuestaPreguntaFactorDesaprovatorio;
        private ISexoService _sexoService;
        private ICentroEstudioService _centroEstudioService;
        private ITipoEstudioService _tipoEstudioService;
        private IAreaFormacionService _areaFormacionService;
        private IEmpresaService _empresaService;
        private ICargoService _cargoService;
        private IAreaTrabajoService _repAreaTrabajo;
        private IIndustriaService _repIndustria;
        private IMonedaService _repMoneda;
        private IOportunidadService _serviOportunidad;
        private IOperadorComparacionService _serviOperadorComparacion;
        #endregion
        public PostulanteController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _postulanteService = new PostulanteService(unitOfWork);
            _tokenManager = tokenManager;

            #region Combo Postulantes
            _pais = new PaisService(_unitOfWork);
            _ciudad = new CiudadService(_unitOfWork);
            _documentos = new TipoDocumentoPersonalService(_unitOfWork);
            _procesoSeleccion = new ProcesoSeleccionService(_unitOfWork);
            _plantillaService = new PlantillaService(_unitOfWork);
            _proveedor = new ProveedorService(_unitOfWork);
            _personalService = new PersonalService(_unitOfWork);
            _procesoSeleccionEtapaService = new ProcesoSeleccionEtapaService(_unitOfWork);
            _estadoEtapaPSServoce = new EstadoEtapaProcesoSeleccionService(_unitOfWork);
            _convocatoriaPersonalService = new ConvocatoriaPersonalService(_unitOfWork);
            postulanteNivelPotencialService = new PostulanteNivelPotencialService(_unitOfWork);
            _respuestaPreguntaFactorDesaprovatorio = new RespuestaPreguntaService(_unitOfWork);
            _sexoService = new SexoService(_unitOfWork);
            _centroEstudioService = new CentroEstudioService(_unitOfWork);
            _tipoEstudioService = new TipoEstudioService(_unitOfWork);
            _areaFormacionService = new AreaFormacionService(_unitOfWork);
            _empresaService = new EmpresaService(_unitOfWork);
            _cargoService = new CargoService(_unitOfWork);
            _repAreaTrabajo = new AreaTrabajoService(_unitOfWork);
            _repIndustria = new IndustriaService(_unitOfWork);
            _repMoneda = new MonedaService(_unitOfWork);
            _serviOportunidad = new OportunidadService(unitOfWork);
            _serviOperadorComparacion = new OperadorComparacionService(unitOfWork);
            #endregion
        }
        /// Tipo Función: POST
        /// Autor: Flavio R.M.F
        /// Fecha: 10/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros de postulantes filtrados por nombre
        /// </summary>
        /// <returns>Una RespuestaGenerica </returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerPostulanteFiltroAutocomplete([FromBody] StringDTO dto)
        {

            var resultado = _postulanteService.ObtenerPostulanteFiltroAutocomplete(dto.Valor);
            return Ok(resultado);
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias Flores
        /// Fecha: 28/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo de datos para el postulante
        /// </summary>
        /// <returns>Lista de listas con los datos </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCombosPostulante()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var documentos = _documentos.ObtenerComboDocumentos();
                var procesosSeleccion = _procesoSeleccion.ObtenerProcesosSeleccion();
                var procesoSeleccionTotal = _procesoSeleccion.ObtenerProcesoSeleccionTotal();
                var pais = _pais.ObtenerListaPais();
                var ciudad = _ciudad.ObtenerCombo();
                var estadoProcesoSeleccion = _procesoSeleccion.ObtenerEstadoProcesoSeleccion();
                //var plantillaEmail = _plantillaService.ObtenerNombrePlantillaBaseEmail();
                //var plantillaWhatsApp = _plantillaService.ObtenerNombrePlantillaBaseWatsApp();
                var proveedor = _proveedor.ObtenerProveedoresPaginasReclutadoras();
                var personalGP = _personalService.ObtenerComboPersonalGestionPersonas();
                var etapaProcesoSeleccion = _procesoSeleccionEtapaService.ObtenerComboProcesoSeleccionEtapa();
                var estadoEtapas = _estadoEtapaPSServoce.Obtener();
                var convocatoria = _convocatoriaPersonalService.ObtenerComboComvocatoriaPersonal();
                var nivelPotencial = postulanteNivelPotencialService.Obtener();
                //var FactorDesaprobatorio = _respuestaPreguntaFactorDesaprovatorio.ObtenerFactorDesaprovatorio();
                var sexo = _sexoService.ObtenerCombo();
                //var centroEstudio = _centroEstudioService.ObtenerComboCentroEstudio();
                //var tipoEstudio = _tipoEstudioService.ObtenerListaTipoEstudioCombo();
                //var estadoEstudio = _centroEstudioService.ObtenerListaEstadoEstudioCombo();
                //var areaFormacion = _areaFormacionService.ObtenerCombo();
                //var empresa = _empresaService.ObtenerCombo();
                //var cargo = _cargoService.ObtenerCombo();
                //var areaTrabajo = _repAreaTrabajo.ObtenerCombo();
                //var industria = _repIndustria.ObtenerCombo();
                //var moneda = _repMoneda.ObtenerCombo();

                return Ok(new
                {
                    ProcesoSeleccion = procesosSeleccion,
                    ProcesoSeleccionTotal = procesoSeleccionTotal,
                    Documento = documentos,
                    Pais = pais,
                    Ciudad = ciudad,
                    Sexo = sexo,
                    //CentroEstudio = centroEstudio,
                    //ListaEmpresa = empresa,
                    //Cargo = cargo,
                    //Industria = industria,
                    //Moneda = moneda,
                    //AreaTrabajo = areaTrabajo,
                    //Nivel = tipoEstudio,
                    //EstadoEstudio = estadoEstudio,
                    //AreaFormacion = areaFormacion,
                    EstadoProcesoSeleccion = estadoProcesoSeleccion,
                    //PlantillaEmail = plantillaEmail,
                    //PlantillaWhatsApp = plantillaWhatsApp,
                    ListaProveedor = proveedor,
                    ListaPersonal = personalGP,
                    ListaEtapasProcesoSeleccion = etapaProcesoSeleccion,
                    ListaEstadoEtapas = estadoEtapas,
                    ListaCodigoConvocatoria = convocatoria,
                    ListaNivelPotencial = nivelPotencial,
                    //ListaRespuestaDesaprobatoria = FactorDesaprobatorio
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias Flores
        /// Fecha: 17/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo de datos para el postulante
        /// </summary>
        /// <returns>Lista de listas con los datos </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerComboPlantillaEmailWhastAppPostulante()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var plantillaEmail = _plantillaService.ObtenerNombrePlantillaBaseEmail();
                var plantillaWhatsApp = _plantillaService.ObtenerNombrePlantillaBaseWatsApp();

                return Ok(new
                {
                    PlantillaEmail = plantillaEmail,
                    PlantillaWhatsApp = plantillaWhatsApp,
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Junior Llerena
        /// Fecha: 
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los postulantes inscritos con proceso
        /// </summary>
        /// <returns>Retorna una lista de tipo.... </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPostulantesInscritosConProcesos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _postulanteService.ObtenerPostulantesInscritosConProcesos();
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //obtenerEXAMENESPOSTULANTE
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPostulantesInscritosConProcesosExamenes([FromBody] PostulanteProcesoDTO DataPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _postulanteService.ObtenerPostulantesInscritosConProcesosExamenes(DataPostulante);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias Flores
        /// Fecha: 17/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo de datos experinecia y Formacion para el postulante
        /// </summary>
        /// <returns>Lista de listas con los datos </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCombosAreaFormacionExperiencia()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var centroEstudio = _centroEstudioService.ObtenerComboCentroEstudio();
                var tipoEstudio = _tipoEstudioService.ObtenerListaTipoEstudioCombo();
                var estadoEstudio = _centroEstudioService.ObtenerListaEstadoEstudioCombo();
                var areaFormacion = _areaFormacionService.ObtenerCombo();
                var empresa = _empresaService.ObtenerCombo();
                var cargo = _cargoService.ObtenerCombo();
                var areaTrabajo = _repAreaTrabajo.ObtenerCombo();
                var industria = _repIndustria.ObtenerCombo();
                var moneda = _repMoneda.ObtenerCombo();

                return Ok(new
                {
                    CentroEstudio = centroEstudio,
                    ListaEmpresa = empresa,
                    Cargo = cargo,
                    Industria = industria,
                    Moneda = moneda,
                    AreaTrabajo = areaTrabajo,
                    Nivel = tipoEstudio,
                    EstadoEstudio = estadoEstudio,
                    AreaFormacion = areaFormacion,
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        /// Tipo Función: POST
        /// Autor: Eliot Arias Flores
        /// Fecha: 19/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de postulantes
        /// </summary>
        /// <returns>Lista de PostulantesDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPostulantesInscritos([FromBody] PaginadorDTO paginador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _postulanteService.ObtenerPostulantesInscritos(paginador);
                return Ok(resultado);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias Flores
        /// Fecha: 28/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de postulantes
        /// </summary>
        /// <returns>btiene Lista de Registros de Postulantes y su información </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerFiltroDatosPostulanteManual([FromBody] DatosPostulanteFiltroGrillaDTO datosPostulanteFiltroGrillaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listado = _serviOperadorComparacion.ObtenerOperadorComparacion();

                var resultado = _postulanteService.ObtenerFiltroDatosPostulanteManual(
                    datosPostulanteFiltroGrillaDTO.filtro,
                    datosPostulanteFiltroGrillaDTO.filter,
                    listado);


                return Ok(resultado);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Eliot Arias Flores
        /// Fecha: 02/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Registra nuevo postulante
        /// </summary>
        /// <returns>True o False </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarNuevoPostulante([FromBody] InsertarPostulanteDTO InformacionPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var postulanteRegistrado = _postulanteService.InsertarPostulante(InformacionPostulante);

                return Ok(postulanteRegistrado);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Eliot Arias Flores
        /// Fecha: 06/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualiza los datos del postulante
        /// </summary>
        /// <returns>True o False </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarPostulante([FromBody] InsertarPostulanteDTO InformacionPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var postulanteRegistrado = _postulanteService.ActualizarPostulante(InformacionPostulante);

                return Ok(postulanteRegistrado);
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias Flores
        /// Fecha: 07/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Elimina a un postulante
        /// </summary>
        /// <returns>True o False </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarPostulante([FromBody] EliminarDTO Postulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var postulanteEliminado = _postulanteService.EliminarPostulante(Postulante);
                return Ok(postulanteEliminado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jashin Salazar.
        /// Fecha: 07/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Historial de datos de los Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpGet]
        [Route("[Action]/{IdPostulante}/{Clave}")]
        public ActionResult ObtenerHistorialPostulante(int IdPostulante, string Clave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
                var postulanteHistorial = _postulanteService.ObtenerHistorialPostulante(IdPostulante, Clave);
                return Ok(postulanteHistorial);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Eliot Arias F.
        /// Fecha: 07/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Experiencia de Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpGet]
        [Route("[Action]/{IdPostulante}")]
        public ActionResult ObtenerPostulanteExperiencia(int IdPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var PostulanteExperiencia = _postulanteService.ObtenerPostulanteExperiencia(IdPostulante);
                return Ok(PostulanteExperiencia);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 07/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Inserta Experiencia del Postulante
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult InsertarPostulanteExperiencia([FromBody] PostulanteExperienciaFormularioDTO experienciaPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.InsertarPostulanteExperiencia(experienciaPostulante));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 07/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la Experiencia del Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ActualizarPostulanteExperiencia([FromBody] PostulanteExperienciaFormularioDTO experienciaPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.ActualizarPostulanteExperiencia(experienciaPostulante));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 08/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Elimina la Experiencia del Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult EliminarPostulanteExperiencia([FromBody] EliminarDTO experienciaPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.EliminarPostulanteExperiencia(experienciaPostulante));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 08/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de historial de Experiencia del Postulantes
        /// </summary>
        /// <returns> Lista de HistorialPostulanteLog Agrupado </returns>
        [HttpGet]
        [Route("[Action]/{idPostulante}")]
        public ActionResult ObtenerHistorialPostulanteExperiencia(int IdPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.ObtenerHistorialPostulanteExperiencia(IdPostulante));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Eliot Arias F.
        /// Fecha: 08/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Formacion de Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpGet]
        [Route("[Action]/{IdPostulante}")]
        public ActionResult ObtenerPostulanteFormacion(int IdPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var postulanteFormacion = _postulanteService.ObtenerPostulanteFormacion(IdPostulante);
                return Ok(postulanteFormacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 08/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Formacion de Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult InsertarPostulanteFormacion([FromBody] PostulanteFormacionFormularioDTO FormacionPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var postulanteFormacion = _postulanteService.InsertarPostulanteFormacion(FormacionPostulante);
                return Ok(postulanteFormacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 08/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Formacion de Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ActualizarPostulanteFormacion([FromBody] PostulanteFormacionFormularioDTO FormacionPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var postulanteFormacion = _postulanteService.ActualizarPostulanteFormacion(FormacionPostulante);
                return Ok(postulanteFormacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 09/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Elimina Formacion de Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult EliminarPostulanteFormacion([FromBody] EliminarDTO FormacionPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var postulanteFormacion = _postulanteService.EliminarPostulanteFormacion(FormacionPostulante);
                return Ok(postulanteFormacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 09/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de historial de Formacion del Postulantes
        /// </summary>
        /// <returns> Lista de HistorialPostulanteFormacionLog Agrupado </returns>
        [HttpGet]
        [Route("[Action]/{idPostulante}")]
        public ActionResult ObtenerHistorialPostulanteFormacion(int IdPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.ObtenerHistorialPostulanteFormacion(IdPostulante));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 09/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Valida el correo Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpGet]
        [Route("[Action]/{idPostulante}")]
        public ActionResult ValidarCorreoPostulante(int IdPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.ValidarCorreoPostulante(IdPostulante));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 11/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Envia  el correo Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult EnviarPlantillaEmailMasivo([FromBody] EnvioPlantillaPostulanteDTO Postulantes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.EnviarPlantillaEmailMasivo(Postulantes));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Envio de correo a un solo postulante
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult EnviarMensajeEmailPostulante([FromBody] PostulanteProcesoSeleccionIdDTO PostulanteProcesoSeleccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.EnviarMensajeEmailPostulante(PostulanteProcesoSeleccion));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el Proceso seleccion de un postulante
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ActualizarProcesoPostulanteSinNota([FromBody] PostulanteProcesoNuevoDTO Informacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.ActualizarProcesoPostulanteSinNota(Informacion));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Versión: 1.0
        /// <summary>
        ///  Actualiza el Proceso seleccion de un postulante comparando notas con su proceso anterior
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpGet]
        [Route("[Action]/{IdPostulante}/{ProcesoOrigen}/{ProcesoDestino}")]
        public ActionResult CompararProcesosSeleccion(int IdPostulante, int ProcesoOrigen, int ProcesoDestino)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var postulanteHistorial = _postulanteService.CompararProcesosSeleccion(IdPostulante, ProcesoOrigen, ProcesoDestino);
                return Ok(postulanteHistorial);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Versión: 1.0
        /// <summary>
        ///  Actualiza el Proceso seleccion de un postulante comparando notas con su proceso anterior
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ActualizarProcesoPostulante([FromBody] PostulanteProcesoNuevoDTO Informacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.ActualizarProcesoPostulante(Informacion));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Envio de WhatsApp a postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult EnviarMensajeWhatsAppPostulante([FromBody] EnvioPlantillaPostulanteDTO Postulantes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.EnviarMensajeWhatsAppPostulante(Postulantes));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Importación de datos de un excel
        /// </summary>
        /// <returns> datos nuevos y datos repetidos </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ImportarExcel([FromForm] IFormFile files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.ImportarExcel(files));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 15/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Insertar Postulantes por Importacion 
        /// </summary>
        /// <returns>Ok 200, mas mensaje de confirmación</returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult InsertarPostulantePorImportacion([FromBody] PostulanteProcesoSeleccionConsolidadoDTO lista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.InsertarPostulantePorImportacion(lista));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 30/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener informacion del postulante por id
        /// </summary>
        /// <returns>Ok 200, mas mensaje de confirmación</returns>
        [HttpPost]
        [Route("[Action]/{IdPostulante}")]
        public ActionResult ObtenerPostulanteInformacion(int IdPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteService.ObtenerPostulanteInformacion(IdPostulante));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult HabilitarExamenes([FromBody] PostulanteExamenesDTO ParametrosHabilitables)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Resultado = _postulanteService.HabilitarExamenesEvaluaciones(ParametrosHabilitables);
                return Ok(Resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
