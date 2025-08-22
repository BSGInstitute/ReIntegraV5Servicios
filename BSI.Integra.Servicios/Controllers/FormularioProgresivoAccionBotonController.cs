using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FormularioProgresivoAccionBotonController
    /// Autor: Jorge Gamero.
    /// Fecha: 29/11/2024
    /// <summary>
    /// Gestión de Formulario Progresivo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FormularioProgresivoAccionBotonController : Controller
    {
        private IUnitOfWork unitOfWork;
        public FormularioProgresivoAccionBotonController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero.
        /// Fecha: 29/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica a la tabla
        /// </summary>
        /// <param name="FormularioProgresivoAccionBotonEntradaDTO"> Datos necesarios para la inserción de datos </param>
        /// <returns> Entidad: FormularioProgresivoAccionBoton </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] FormularioProgresivoAccionBotonEntradaDTO formularioProgresivoAccionBotonEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoAccionBotonService = new FormularioProgresivoAccionBotonService(unitOfWork);
                var formularioProgresivoAccionBoton = new FormularioProgresivoAccionBoton();
                formularioProgresivoAccionBoton.Nombre = formularioProgresivoAccionBotonEntradaDTO.Nombre;
                formularioProgresivoAccionBoton.Estado = true;
                formularioProgresivoAccionBoton.UsuarioCreacion = formularioProgresivoAccionBotonEntradaDTO.Usuario;
                formularioProgresivoAccionBoton.UsuarioModificacion = formularioProgresivoAccionBotonEntradaDTO.Usuario;
                formularioProgresivoAccionBoton.FechaCreacion = DateTime.Now;
                formularioProgresivoAccionBoton.FechaModificacion = DateTime.Now;
                var resultado = formularioProgresivoAccionBotonService.Add(formularioProgresivoAccionBoton);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Jorge Gamero.
        /// Fecha: 29/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualización básica a la tabla
        /// </summary>
        /// <param name="FormularioProgresivoAccionBotonEntradaDTO"> Datos necesarios para la actualización de datos </param>
        /// <returns> Entidad: FormularioProgresivoAccionBoton </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] FormularioProgresivoAccionBotonEntradaDTO formularioProgresivoAccionBotonEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoAccionBotonService = new FormularioProgresivoAccionBotonService(unitOfWork);
                var formularioProgresivoAccionBoton = new FormularioProgresivoAccionBoton();
                formularioProgresivoAccionBoton = formularioProgresivoAccionBotonService.ObtenerPorId(formularioProgresivoAccionBotonEntradaDTO.Id.Value);
                formularioProgresivoAccionBoton.Nombre = formularioProgresivoAccionBotonEntradaDTO.Nombre;
                formularioProgresivoAccionBoton.UsuarioModificacion = formularioProgresivoAccionBotonEntradaDTO.Usuario;
                formularioProgresivoAccionBoton.FechaModificacion = DateTime.Now;
                var resultado = formularioProgresivoAccionBotonService.Update(formularioProgresivoAccionBoton);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Jorge Gamero.
        /// Fecha: 29/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Elimina un registro de la tabla
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <param name="usuario"> Autor de la modificación </param>
        /// <returns> true or false </returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoAccionBotonService = new FormularioProgresivoAccionBotonService(unitOfWork);
                var resultado = formularioProgresivoAccionBotonService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 29/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerRegistros()
        {
            try
            {
                var formularioProgresivoAccionBotonService = new FormularioProgresivoAccionBotonService(unitOfWork);
                var resultado = formularioProgresivoAccionBotonService.ObtenerRegistros();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
