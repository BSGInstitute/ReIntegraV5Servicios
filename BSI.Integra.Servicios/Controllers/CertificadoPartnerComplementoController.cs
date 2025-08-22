using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Operacion;

namespace BSI.Integra.Servicios.Controllers
{

    /// Controlador: CertificadoPartnerComplementoController
    /// Autor: Marco Jose Villanueva Torres
    /// Fecha: 15/09/2022
    /// <summary>
    /// Certificado Partner Complemento
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CertificadoPartnerComplementoController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private ICertificadoPartnerComplementoService _certificadoPartnerComplementoService;
      
        public CertificadoPartnerComplementoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _certificadoPartnerComplementoService = new CertificadoPartnerComplementoService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Metodo HTTP: POST.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="certificadoPartnerComplementoDTO"> parametros de la nueva Plantilla_PW y sus detalles </param>
        /// <returns> bool </returns>



        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] CertificadoPartnerComplementoDTO certificadoPartnerComplementoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             var respuesta = _certificadoPartnerComplementoService.Insertar(certificadoPartnerComplementoDTO, _tokenManager.UserName);
             return Ok(respuesta);
          
        }


        /// Metodo HTTP: PUT.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la  tabla y sus detalles
        /// </summary>   
        /// <param name="certificadoPartnerComplementoDTO"> parametros de la nueva Plantilla_PW y sus detalles </param>
        

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] CertificadoPartnerComplementoDTO certificadoPartnerComplementoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _certificadoPartnerComplementoService.Actualizar(certificadoPartnerComplementoDTO, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// Metodo HTTP: Delete.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
        /// </summary>   
        /// <param name="id"> (PK) </param>
       
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int idCertificadoPartner)
        {
            var respuesta = _certificadoPartnerComplementoService.Eliminar(idCertificadoPartner, _tokenManager.UserName);
            return Ok(respuesta);
        }


        /// Metodo: HTTP: Get
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// <summary>
        /// Obtiene pregunta evaluacion por configuracion
        /// </summary>


        [HttpGet("[action]")]
        public IActionResult ObtenerTodo()
        {
            var resultado = _certificadoPartnerComplementoService.ObtenerTodo();
            return Ok(resultado);
        }

        /// Metodo HTTP: Post.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una asignacion por id y al conincidir asignar el centro de costo 
        /// </summary>   
        /// <param name="idCertificadoPartnerComplemento,idCentroCosto"> (PK) </param>
        /// return CentroCostoCertificadoDTO

        [HttpPost("[Action]/{idCertificadoPartnerComplemento}/{idCentroCosto}")]
        public IActionResult Asignar(int idCertificadoPartnerComplemento, int idCentroCosto)
        {
            var resultado = _certificadoPartnerComplementoService.Asignar(idCertificadoPartnerComplemento, idCentroCosto, _tokenManager.UserName);
            return Ok(resultado);
        }


        /// Metodo HTTP: Get.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene un Centro de costo Asignado por un Id
        /// </summary>   
        /// <param name="id"> (PK) </param>
        /// return List<CentroCostoAsignadoCertificadoPartnerComplementoDTO>
        [Route("[Action]/{Id}")]
        [HttpGet]
        public IActionResult ObtenerCentroCostoAsignado(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = _certificadoPartnerComplementoService.ObtenerCentroCostoAsociadoPorId(Id);

            return Ok(resultado);
        }
    }

}
