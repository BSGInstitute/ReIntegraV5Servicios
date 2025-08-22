using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: WhatsAppNumeroValidadoController
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/12/2022
    /// <summary>
    /// Gestión de WhatsApp Numero Validado 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppNumeroValidadoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private IConfiguration configuration;

        public WhatsAppNumeroValidadoController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="whatsAppNumeroValidadoDTO">Entidad a insertar</param>
        /// <returns> true or false </returns> 
        [Route("[action]")]
        [HttpPost]
        public ActionResult VerificarInsertarNumeroValidado([FromBody] WhatsAppNumeroValidadoDTO whatsAppNumeroValidadoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                WhatsAppNumeroValidadoService whatsAppNumeroValidadoService = new WhatsAppNumeroValidadoService(unitOfWork);
                if (!whatsAppNumeroValidadoService.VerificarNumeroValidado(whatsAppNumeroValidadoDTO.NumeroCelular))
                {
                    WhatsAppNumeroValidado whatsAppNumeroValidado = new WhatsAppNumeroValidado();
                    whatsAppNumeroValidado.IdAlumno = whatsAppNumeroValidadoDTO.IdAlumno;
                    whatsAppNumeroValidado.NumeroCelular = whatsAppNumeroValidadoDTO.NumeroCelular;
                    whatsAppNumeroValidado.IdPais = whatsAppNumeroValidadoDTO.IdPais;
                    whatsAppNumeroValidado.Estado = true;
                    whatsAppNumeroValidado.FechaCreacion = DateTime.Now;
                    whatsAppNumeroValidado.FechaModificacion = DateTime.Now;
                    whatsAppNumeroValidado.UsuarioCreacion = whatsAppNumeroValidadoDTO.Usuario;
                    whatsAppNumeroValidado.UsuarioModificacion = whatsAppNumeroValidadoDTO.Usuario;
                    whatsAppNumeroValidadoService.Add(whatsAppNumeroValidado);
                    return Ok(false);
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
