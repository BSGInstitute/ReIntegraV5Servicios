using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FormularioProgresivoConfiguracionBotonController
    /// Autor: Jorge Gamero.
    /// Fecha: 03/03/2025
    /// <summary>
    /// Gestión de Formulario Progresivo Configuración Botón
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FormularioProgresivoConfiguracionBotonController : Controller
    {
        private IUnitOfWork unitOfWork;
        public FormularioProgresivoConfiguracionBotonController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 03/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica a la tabla
        /// </summary>
        /// <param name="FormularioProgresivoConfiguracionBotonEntradaDTO"> Datos necesarios para la inserción de datos </param>
        /// <returns> Entidad: FormularioProgresivoConfiguracionBoton </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] FormularioProgresivoConfiguracionBotonEntradaDTO formularioProgresivoConfiguracionBotonEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoConfiguracionBotonService = new FormularioProgresivoConfiguracionBotonService(unitOfWork);
                var formularioProgresivoConfiguracionBoton = new FormularioProgresivoConfiguracionBoton();
                formularioProgresivoConfiguracionBoton.IdFormularioProgresivo = formularioProgresivoConfiguracionBotonEntradaDTO.IdFormularioProgresivo;
                formularioProgresivoConfiguracionBoton.Estado = true;
                formularioProgresivoConfiguracionBoton.UsuarioCreacion = formularioProgresivoConfiguracionBotonEntradaDTO.Usuario;
                formularioProgresivoConfiguracionBoton.UsuarioModificacion = formularioProgresivoConfiguracionBotonEntradaDTO.Usuario;
                formularioProgresivoConfiguracionBoton.IdentificadorFilaGrilla = formularioProgresivoConfiguracionBotonEntradaDTO.IdentificadorFilaGrilla;
                formularioProgresivoConfiguracionBoton.TextoBoton = formularioProgresivoConfiguracionBotonEntradaDTO.TextoBoton;
                formularioProgresivoConfiguracionBoton.FechaCreacion = DateTime.Now;
                formularioProgresivoConfiguracionBoton.FechaModificacion = DateTime.Now;
                var resultado = formularioProgresivoConfiguracionBotonService.Add(formularioProgresivoConfiguracionBoton);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Jorge Gamero.
        /// Fecha: 03/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualización básica a la tabla
        /// </summary>
        /// <param name="FormularioProgresivoConfiguracionBotonEntradaDTO"> Datos necesarios para la actualización de datos </param>
        /// <returns> Entidad: FormularioProgresivoConfiguracionBoton </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] FormularioProgresivoConfiguracionBotonEntradaDTO formularioProgresivoConfiguracionBotonEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoConfiguracionBotonService = new FormularioProgresivoConfiguracionBotonService(unitOfWork);
                var formularioProgresivoConfiguracionBoton = new FormularioProgresivoConfiguracionBoton();
                formularioProgresivoConfiguracionBoton = formularioProgresivoConfiguracionBotonService.ObtenerPorId(formularioProgresivoConfiguracionBotonEntradaDTO.Id.Value);
                formularioProgresivoConfiguracionBoton.IdFormularioProgresivo = formularioProgresivoConfiguracionBotonEntradaDTO.IdFormularioProgresivo;
                formularioProgresivoConfiguracionBoton.UsuarioModificacion = formularioProgresivoConfiguracionBotonEntradaDTO.Usuario;
                formularioProgresivoConfiguracionBoton.FechaModificacion = DateTime.Now;
                var resultado = formularioProgresivoConfiguracionBotonService.Update(formularioProgresivoConfiguracionBoton);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Jorge Gamero.
        /// Fecha: 03/03/2025
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
                var formularioProgresivoConfiguracionBotonService = new FormularioProgresivoConfiguracionBotonService(unitOfWork);
                var resultado = formularioProgresivoConfiguracionBotonService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 04/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_FormularioProgresivoConfiguracionBoton por IdFormularioProgresivo.
        /// </summary>
        /// <param name="idFormularioProgresivo"> IdFormularioProgresivo de la entidad </param>
        /// <returns> FormularioProgresivoConfiguracionBoton </returns>
        [HttpGet("ObtenerPorIdFormularioProgresivo/{idFormularioProgresivo}")]
        public IActionResult ObtenerPorIdFormularioProgresivo(int idFormularioProgresivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var formularioProgresivoConfiguracionBotonService = new FormularioProgresivoConfiguracionBotonService(unitOfWork);
                var resultado = formularioProgresivoConfiguracionBotonService.ObtenerPorIdFormularioProgresivo(idFormularioProgresivo);

                if (resultado == null)
                {
                    return NotFound($"No se encontró un registro con IdFormularioProgresivo: {idFormularioProgresivo}");
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
