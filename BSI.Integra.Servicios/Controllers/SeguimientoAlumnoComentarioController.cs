using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SeguimientoAlumnoComentarioController
    /// Autor: Daniel Huaita
    /// Fecha: 22/02/2023
    /// <summary>
    /// Gestión de comenatrios opereciones
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SeguimientoAlumnoComentarioController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public SeguimientoAlumnoComentarioController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] SeguimientoAlumnoComentarioDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SeguimientoAlumnoComentarioService(unitOfWork);
                var respuesta = servicio.Add(Objeto);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoSeguimientoAlumno()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SeguimientoAlumnoComentarioService(unitOfWork);
                var respuesta = servicio.ObtenerCombo();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarTipoSeguimiento([FromBody]  TipoSeguimientoEntradaDTO tipoSeguimientoEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudService = new SeguimientoAlumnoComentarioService(unitOfWork);
                var tipoSeguimiento = solicitudService.ObtenerPorId(tipoSeguimientoEntradaDTO.Id.Value);
                tipoSeguimiento.Nombre = tipoSeguimientoEntradaDTO.Nombre;
                tipoSeguimiento.UsuarioModificacion = tipoSeguimientoEntradaDTO.Usuario;
                tipoSeguimiento.FechaModificacion =DateTime.Now;
                var resultado = solicitudService.Update(tipoSeguimiento);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPut]
        public ActionResult InsertarTipoSeguimiento([FromBody] TipoSeguimientoEntradaDTO tipoSeguimientoEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudService = new SeguimientoAlumnoComentarioService(unitOfWork);  
                //var tipoSeguimiento = solicitudService.ObtenerPorId(tipoSeguimientoEntradaDTO.Id.Value);
                var resultado = solicitudService.InsertarTipoSeguimiento(tipoSeguimientoEntradaDTO);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("EliminarTipoSeguimiento/{id}/{usuario}")]
        public IActionResult EliminarTipoSeguimiento(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudService = new SeguimientoAlumnoComentarioService(unitOfWork);
                //var tipoSeguimiento = solicitudService.ObtenerPorId(tipoSeguimientoEntradaDTO.Id.Value);
                //tipoSeguimiento.UsuarioModificacion = tipoSeguimientoEntradaDTO.Usuario;
                var resultado = solicitudService.EliminarTipoSeguimiento(id,usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    
}