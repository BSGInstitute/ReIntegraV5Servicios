
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class PuestoTrabajoController : ControllerBase
    {

        private ITokenManager _tokenManager;
        private IPuestoTrabajoService _puestoTrabajoService;
        public PuestoTrabajoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _puestoTrabajoService = new PuestoTrabajoService(unitOfWork);
            _tokenManager = tokenManager;
        }
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<PuestoTrabajoPorFechaDTO>> Obtener()
        {
            var resultado = _puestoTrabajoService.Obtener();
            return Ok(resultado);
        }
        [HttpGet("[action]/{idPuestoTrabajo}")]
        public ActionResult<IEnumerable<PuestoTrabajoModuloSistemaDTO>> ObtenerGridAsignacionInterfaz(int idPuestoTrabajo)
        {
            var resultado = _puestoTrabajoService.ObtenerGridAsignacionInterfaz(idPuestoTrabajo);
            return Ok(resultado);
        }

        [HttpGet("[action]")]
        public IActionResult ObtenerCombos()
        {
            var resultado = _puestoTrabajoService.ObtenerCombos();
            return Ok(resultado);
        }

        [HttpPost("[action]")]
        public ActionResult<PuestoTrabajoInsertDTO> Insertar([FromBody] PuestoTrabajoInsertDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _puestoTrabajoService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [HttpPut("[action]")]
        public ActionResult<PuestoTrabajoInsertDTO> Actualizar([FromBody] PuestoTrabajoInsertDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _puestoTrabajoService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [HttpGet("[action]/{idPerfilPuestoTrabajo}")]
        public ActionResult<IEnumerable<ObtenerExamenDTO>> ObtenerPerfilPuestoTrabajo(int? idPerfilPuestoTrabajo)
        {
            var resultado = _puestoTrabajoService.ObtenerPerfilPuestoTrabajo(idPerfilPuestoTrabajo);
            return Ok(resultado);
        }

        [HttpGet("[action]/{idPuestoTrabajo}")]
        public ActionResult<IEnumerable<PuestoTrabajoVersionesDTO>> ObtenerListaHistoricoPerfilPuestoTrabajo(int? idPuestoTrabajo)
        {
            var resultado = _puestoTrabajoService.ObtenerListaHistoricoPerfilPuestoTrabajo(idPuestoTrabajo);
            return Ok(resultado);
        }

        [HttpPost("[action]")]
        public ActionResult<bool> InsertarActualizarPerfilPuestoTrabajo([FromBody] PerfilPuestoTrabajoInsertarActualizarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dto.IdPersonal = _tokenManager.IdPersonal;
            dto.Usuario = _tokenManager.UserName;


            var respuesta = _puestoTrabajoService.InsertarActualizarPerfilPuestoTrabajo(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _puestoTrabajoService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [HttpPost("[action]")]
        public ActionResult<PuestoTrabajoInsertDTO> InsertarActualizarInterfaz([FromBody] AsignarInterfazDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _puestoTrabajoService.InsertarActualizarInterfaz(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [HttpPost("[action]")]
        public ActionResult<PuestoTrabajoInsertDTO> AprobarRechazarVersionPerfilPuestoTrabajo([FromBody] AprobacionRechazoPerfilPuestoTrabajoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dto.IdPersonal = _tokenManager.IdPersonal;
            var respuesta = _puestoTrabajoService.AprobarRechazarVersionPerfilPuestoTrabajo(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<IntDTO>> EsPersonalAprobacionVersion()
        {
            var resultado = _puestoTrabajoService.EsPersonalAprobacionVersion(_tokenManager.IdPersonal);
            return Ok(resultado);
        }

    }
}
