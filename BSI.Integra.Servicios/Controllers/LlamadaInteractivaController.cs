using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: LlamadaInteractivaController
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/07/2023
    /// <summary>
    /// Gestión de Llamadas Interactivas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class LlamadaInteractivaController : ControllerBase
    {
        private ILlamadaInteractivaService _llamadaInteractivaService;
        public LlamadaInteractivaController(IUnitOfWork unitOfWork)
        {
            _llamadaInteractivaService = new LlamadaInteractivaService(unitOfWork);
        }
        /// Metodo HTTP: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/07/2023
        /// <summary>
        /// Obtiene la lista de cuotas de pagos asociado al Id de T_MatriculaCabecera
        /// </summary> 
        /// <param name="idMatriculaCabecera"> (PK) de T_MatriculaCabecera </param>
        /// <returns> CronogramaPagoDetalleDTO, ExcepcionRegistroDTO </returns>
        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerCronogramaPagoMatricula(int idMatriculaCabecera)
        {

            try
            {
                var listaCronograma = _llamadaInteractivaService.ListaMatriculaPagoAlumnoMatricula(idMatriculaCabecera);
                return Ok(new { CronogramaPagoCuotas = listaCronograma.Item1, Excepcion = listaCronograma.Item2 });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Metodo HTTP: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/07/2023
        /// <summary>
        /// Obtiene la pasarela de pago por IdMatriculaCabecera
        /// </summary> 
        /// <param name="idMatriculaCabecera"> (PK) de T_MatriculaCabecera </param>
        /// <returns> List<MedioPagoActivoPasarelaDTO> </returns>
        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public IActionResult ObtenerPasarelaPagoPorMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                var medioPagoActivoPasarelas = _llamadaInteractivaService.ObtenerMedioPagoPasarelaPorMatricula(idMatriculaCabecera);
                return Ok(medioPagoActivoPasarelas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Metodo HTTP: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/07/2023
        /// <summary>
        /// Realiza el pre-proceso de pago de las cuotas del alumno
        /// </summary> 
        /// <param name="registroPreProcesoPago"> Cuota a pagar, alumno, matriculaCabecera, formaPago </param>
        /// <returns> RespuestaRegistroPreProcesoPagoDTO </returns>
        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public IActionResult PreProcesoPagoCuotaAlumno([FromBody] RegistroPreProcesoPagoDTO registroPreProcesoPago)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var validacionClaim = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var respuestaRegistroPreProcesoPago = _llamadaInteractivaService.PreProcesoPagoCuotaAlumno(registroPreProcesoPago, validacionClaim);
                return Ok(respuestaRegistroPreProcesoPago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Metodo HTTP: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/07/2023
        /// <summary>
        /// Realiza el pre-proceso de pago de las cuotas del alumno
        /// </summary> 
        /// <param name="procesoPagoIvr"> Cuota a pagar, alumno, matriculaCabecera, formaPago </param>
        /// <returns> RespuestaRegistroPreProcesoPagoDTO </returns>
        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarProcesoPagoIvr(ProcesoPagoIvrDTO procesoPagoIvr)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var validacionClaim = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var respuestaRegistroPreProcesoPago = _llamadaInteractivaService.InsertarProcesoPagoIvr(procesoPagoIvr, validacionClaim.RegistroClaimToken.UserName);
                return Ok(respuestaRegistroPreProcesoPago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Metodo HTTP: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 04/08/2023
        /// <summary>
        /// Obtiene detalles del registro de pago por ivr
        /// </summary> 
        /// <param name="numeroCelular"> Numero celular del alumno </param>
        /// <returns> TransaccionAuditoriaPagoDTO </returns> 
        [Route("[action]/{numeroCelular}")]
        [HttpGet]
        public IActionResult ObtenerTransaction(string numeroCelular)
        {
            try
            {
                var respuesta = _llamadaInteractivaService.ObtenerTransactionPorCelular(numeroCelular);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Metodo HTTP: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/08/2023
        /// <summary>
        /// Valida si el número de tarjeta ingresado es valida usando el Algoritmo de Luhn
        /// </summary> 
        /// <param name="numeroTarjeta"> numero de tarjeta </param>
        /// <returns> bool </returns> 
        [Route("[action]/{numeroTarjeta}")]
        [HttpGet]
        public IActionResult ValidarNumeroTarjeta(string numeroTarjeta)
        {
            try
            {
                var respuesta = _llamadaInteractivaService.ValidarNumeroTarjeta(numeroTarjeta);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
