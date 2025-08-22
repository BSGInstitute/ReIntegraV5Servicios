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
    /// Controlador: FormularioProgresivoTipoController
    /// Autor: Jorge Gamero.
    /// Fecha: 29/11/2024
    /// <summary>
    /// Gestión de Formulario Progresivo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FormularioProgresivoTipoController : Controller
    {
        private IUnitOfWork unitOfWork;
        public FormularioProgresivoTipoController(IUnitOfWork unitOfWork)
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
        /// <param name="FormularioProgresivoTipoEntradaDTO"> Datos necesarios para la inserción de datos </param>
        /// <returns> Entidad: FormularioProgresivoTipo </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] FormularioProgresivoTipoEntradaDTO formularioProgresivoTipoEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoTipoService = new FormularioProgresivoTipoService(unitOfWork);
                var formularioProgresivoTipo = new FormularioProgresivoTipo();
                formularioProgresivoTipo.Nombre = formularioProgresivoTipoEntradaDTO.Nombre;
                formularioProgresivoTipo.Estado = true;
                formularioProgresivoTipo.UsuarioCreacion = formularioProgresivoTipoEntradaDTO.Usuario;
                formularioProgresivoTipo.UsuarioModificacion = formularioProgresivoTipoEntradaDTO.Usuario;
                formularioProgresivoTipo.FechaCreacion = DateTime.Now;
                formularioProgresivoTipo.FechaModificacion = DateTime.Now;
                var resultado = formularioProgresivoTipoService.Add(formularioProgresivoTipo);
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
        /// <param name="FormularioProgresivoTipoEntradaDTO"> Datos necesarios para la actualización de datos </param>
        /// <returns> Entidad: FormularioProgresivoTipo </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] FormularioProgresivoTipoEntradaDTO formularioProgresivoTipoEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoTipoService = new FormularioProgresivoTipoService(unitOfWork);
                var formularioProgresivoTipo = new FormularioProgresivoTipo();
                formularioProgresivoTipo = formularioProgresivoTipoService.ObtenerPorId(formularioProgresivoTipoEntradaDTO.Id.Value);
                formularioProgresivoTipo.Nombre = formularioProgresivoTipoEntradaDTO.Nombre;
                formularioProgresivoTipo.UsuarioModificacion = formularioProgresivoTipoEntradaDTO.Usuario;
                formularioProgresivoTipo.FechaModificacion = DateTime.Now;
                var resultado = formularioProgresivoTipoService.Update(formularioProgresivoTipo);
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
                var formularioProgresivoTipoService = new FormularioProgresivoTipoService(unitOfWork);
                var resultado = formularioProgresivoTipoService.Delete(id, usuario);
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
                var formularioProgresivoTipoService = new FormularioProgresivoTipoService(unitOfWork);
                var resultado = formularioProgresivoTipoService.ObtenerRegistros();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
