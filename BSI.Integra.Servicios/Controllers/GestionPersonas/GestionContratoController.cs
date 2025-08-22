using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: GestionContratoController
    /// Autor: Eliot Arias Flores
    /// Fecha: 28/12/2024
    /// <summary>
    /// Gestion de Contato para el Modulo Contrato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[Authorize]
    public class GestionContratoController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IUnitOfWork _unitOfWork;

        #region Servicios
        private IDatoContratoPersonalService _contratoPersonalService;
        #endregion

        public GestionContratoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            this._unitOfWork = unitOfWork;
            this._tokenManager = tokenManager;

            #region
            _contratoPersonalService = new DatoContratoPersonalService(unitOfWork);
            #endregion

        }

        /// Tipo Función: GET
        /// Autor: Eliot Arias F.
        /// Fecha: 28/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo para DatoContrato
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_contratoPersonalService.ObtenerCombos());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Eliot Arias F.
        /// Fecha: 08/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo para DatoContrato
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboContrato()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_contratoPersonalService.ObtenerComboContrato());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 28/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Contrato por filto, o toda la lista de contratos
        /// </summary>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ObtenerContratosPorFiltro([FromBody] ContratoFiltroDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var contrato = _contratoPersonalService.ObtenerContratosPorFiltro(Filtro);
                return Ok(contrato);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 30/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para el formulario de contrato
        /// </summary>
        [HttpPost]
        [Route("[action]/{IdPersonal}")]
        public ActionResult ObtenerDataFormulario(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_contratoPersonalService.ObtenerDataFormulario(IdPersonal));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 30/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista del historico de contratos
        /// </summary>
        [HttpPost]
        [Route("[action]/{IdPersonal}")]
        public ActionResult ObtenerContratosHistoricos(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_contratoPersonalService.ObtenerContratosHistoricos(IdPersonal));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 30/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Datos de remuneacion variable del personal
        /// </summary>
        [HttpPost]
        [Route("[action]/{IdPuestoTrabajo}")]
        public ActionResult ObtenerRemuneracionVariableDisplay(int IdPuestoTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_contratoPersonalService.ObtenerRemuneracionVariableDisplay(IdPuestoTrabajo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 30/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Inserta un nuevo Contrato
        /// </summary>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarContrato([FromBody] DatoContratoPersonalDTO ContratoPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_contratoPersonalService.InsertarContrato(ContratoPersonal));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 31/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Genera el pdf de Contrato y Formulario
        /// </summary>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerPDF([FromBody] string ContratoPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var pdf = _contratoPersonalService.GenerarPDFDatoContratoPersonalV2(ContratoPersonal);
                return File(pdf, "application/pdf", "ContratoPersonal.pdf");
                //return Ok(pdf);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 31/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo autocomplete de personal para filtro
        /// </summary>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ObtenerPersonalAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    return Ok(_contratoPersonalService.CargarPersonalAutoCompleteContrato(Filtros["valor"].ToString()));
                }
                else
                {
                    List<FiltroDTO> lista = new List<FiltroDTO>();
                    return Ok(lista);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 16/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos de remuneración variable
        /// </summary>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ObtenerComboDatosRemuneracionVariable()
        {
            try
            {
                return Ok(_contratoPersonalService.ObtenerComboDatosRemuneracionVariable());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
