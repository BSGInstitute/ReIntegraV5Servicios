using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class AsesorMarcadorController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IUnitOfWork unitOfWork;
        private IAsesorMarcadorService _asesorMarcadorService;
        public AsesorMarcadorController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _asesorMarcadorService = new AsesorMarcadorService(unitOfWork);
            _tokenManager = tokenManager;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _asesorMarcadorService.Obtener();
            return Ok(resultado);
        }
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] AsesorMarcadorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _asesorMarcadorService.Actualizar(dto, _tokenManager.UserName);
            ///
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                MarcadorAsesorDTO item = new MarcadorAsesorDTO();
                item.IdPersonal = dto.IdPersonal;
                item.MarcadorActivo = dto.MarcadorActivo;
                whatsAppMensajesService.ActualizarEstadoMarcador(item);
            }
            catch (Exception ex) { return Ok(respuesta); }

            return Ok(respuesta);
        }

        [HttpPut("[action]")]
        public IActionResult ActualizarAgenda([FromBody] AsesorMarcadorDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var item = _asesorMarcadorService.ObtenerPorIdPersonal(dto.IdPersonal);
                dto.Id = item.Id;
                var respuesta = _asesorMarcadorService.Actualizar(dto, _tokenManager.UserName);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _asesorMarcadorService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
        [HttpPost("[action]")]
        public IActionResult ObtenerReporteAsesorMarcadorAutomatico([FromBody] FiltroReporteAsesorMarcadorDTO filtro)
        {
            var respuestatiemposmuertos = _asesorMarcadorService.ObtenerReporteAsesorMarcadorAutomatico(filtro);
            var respuestatiempopromedio = _asesorMarcadorService.ObtenerReporteAsesorTiempoPromedio(filtro);


            var respuesta = new ReporteFinalMarcadorTotalDTO();
            respuesta.TiemposMuertos = respuestatiemposmuertos;
            respuesta.TiemposPromedios = respuestatiempopromedio;


            return Ok(respuesta);
        }
    }
}
