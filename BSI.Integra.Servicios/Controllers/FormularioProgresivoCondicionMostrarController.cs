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
    /// Controlador: FormularioProgresivoCondicionMostrarController
    /// Autor: Jorge Gamero.
    /// Fecha: 29/11/2024
    /// <summary>
    /// Gestión de Formulario Progresivo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FormularioProgresivoCondicionMostrarController : Controller
    {
        private IUnitOfWork unitOfWork;
        public FormularioProgresivoCondicionMostrarController(IUnitOfWork unitOfWork)
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
        /// <param name="FormularioProgresivoCondicionMostrarEntradaDTO"> Datos necesarios para la inserción de datos </param>
        /// <returns> Entidad: FormularioProgresivoCondicionMostrar </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] FormularioProgresivoCondicionMostrarEntradaDTO formularioProgresivoCondicionMostrarEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoCondicionMostrarService = new FormularioProgresivoCondicionMostrarService(unitOfWork);
                var formularioProgresivoCondicionMostrar = new FormularioProgresivoCondicionMostrar();
                formularioProgresivoCondicionMostrar.Nombre = formularioProgresivoCondicionMostrarEntradaDTO.Nombre;
                formularioProgresivoCondicionMostrar.Estado = true;
                formularioProgresivoCondicionMostrar.UsuarioCreacion = formularioProgresivoCondicionMostrarEntradaDTO.Usuario;
                formularioProgresivoCondicionMostrar.UsuarioModificacion = formularioProgresivoCondicionMostrarEntradaDTO.Usuario;
                formularioProgresivoCondicionMostrar.FechaCreacion = DateTime.Now;
                formularioProgresivoCondicionMostrar.FechaModificacion = DateTime.Now;
                var resultado = formularioProgresivoCondicionMostrarService.Add(formularioProgresivoCondicionMostrar);
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
        /// <param name="FormularioProgresivoCondicionMostrarEntradaDTO"> Datos necesarios para la actualización de datos </param>
        /// <returns> Entidad: FormularioProgresivoCondicionMostrar </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] FormularioProgresivoCondicionMostrarEntradaDTO formularioProgresivoCondicionMostrarEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoCondicionMostrarService = new FormularioProgresivoCondicionMostrarService(unitOfWork);
                var formularioProgresivoCondicionMostrar = new FormularioProgresivoCondicionMostrar();
                formularioProgresivoCondicionMostrar = formularioProgresivoCondicionMostrarService.ObtenerPorId(formularioProgresivoCondicionMostrarEntradaDTO.Id.Value);
                formularioProgresivoCondicionMostrar.Nombre = formularioProgresivoCondicionMostrarEntradaDTO.Nombre;
                formularioProgresivoCondicionMostrar.UsuarioModificacion = formularioProgresivoCondicionMostrarEntradaDTO.Usuario;
                formularioProgresivoCondicionMostrar.FechaModificacion = DateTime.Now;
                var resultado = formularioProgresivoCondicionMostrarService.Update(formularioProgresivoCondicionMostrar);
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
                var formularioProgresivoCondicionMostrarService = new FormularioProgresivoCondicionMostrarService(unitOfWork);
                var resultado = formularioProgresivoCondicionMostrarService.Delete(id, usuario);
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
                var formularioProgresivoCondicionMostrarService = new FormularioProgresivoCondicionMostrarService(unitOfWork);
                var resultado = formularioProgresivoCondicionMostrarService.ObtenerRegistros();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
