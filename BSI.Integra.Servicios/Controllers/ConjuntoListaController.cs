using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Conjunto Lista
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 07/06/2023
    /// <summary>
    /// Gestión de Conjunto Lista
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConjuntoListaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public ConjuntoListaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// Tipo Función: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta Conjunto Lista
        /// </summary>
        /// <param name="ConjuntoListaDetalleCompleto">Entidad a insertar</param>
        /// <returns> true </returns>

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] ConjuntoListaDetalleCompletoListoDTO ConjuntoListaDetalleCompleto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var servicio = new ConjuntoListaService(unitOfWork);
                return Ok(servicio.Insertar(ConjuntoListaDetalleCompleto, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Conjunto Lista
        /// </summary>
        /// <param name="ConjuntoListaDetalleCompleto">Entidad a insertar</param>
        /// <returns> true </returns>

        [HttpPost("Actualizar")]
        public IActionResult Actualizar([FromBody] ConjuntoListaDetalleCompletoListoDTO ConjuntoListaDetalleCompleto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConjuntoListaService(unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();

                return Ok(servicio.Actualizar(ConjuntoListaDetalleCompleto, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: Delete
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina Conjunto Lista
        /// </summary>
        /// <param name="id">Id del conjunto Listaa</param>
        /// <returns> true </returns>

        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var servicio = new ConjuntoListaService(unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var respuesta = servicio.Delete(id, Usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Conjunto Lista
        /// </summary>

        [HttpGet("Obtener")]
        public IActionResult Obtener()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConjuntoListaService(unitOfWork);
                return Ok(servicio.Obtener());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Conjunto Lista poe id 
        /// </summary>
        ///  /// <param name="id">Id del conjunto Lista</param>
        /// <returns> true </returns>

        [HttpGet("ObtenerPorId/{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConjuntoListaService(unitOfWork);
                return Ok(servicio.Obtener(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Conjunto Lista Detalle poe id Conjunto lista
        /// </summary>
        ///  /// <param name="IdConjuntoLista">Id del conjunto Lista</param>
        /// <returns> true </returns>


        [HttpGet("ObtenerDetalle/{IdConjuntoLista}")]
        public IActionResult ObtenerDetalle(int IdConjuntoLista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConjuntoListaService(unitOfWork);
                return Ok(servicio.ObtenerDetalle(IdConjuntoLista));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Conjunto Lista Resultado poe id Conjunto lista
        /// </summary>
        ///  /// <param name="IdConjuntoLista">Id del conjunto Lista</param>
        /// <returns> true </returns>


        [HttpGet("ObtenerResultado/{IdConjuntoLista}")]
        public IActionResult ObtenerResultado(int IdConjuntoLista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConjuntoListaService(unitOfWork);
                return Ok(servicio.ObtenerConjuntoFiltro(IdConjuntoLista));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Duplica el registro de conjunto lista
        /// </summary>
        ///  /// <param name="id">Id del conjunto Lista</param>
        /// <returns> true </returns>

        [Route("Duplicar/{id}")]
        [HttpPost]
        public ActionResult Duplicar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
            
                var servicio = new ConjuntoListaService(unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();

                servicio.Duplicar(id, Usuario);
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Sube la lista de contactos a Conjunto Lista Resultado
        /// </summary>
        /// <param name="json">Entidad a insertar</param>
        /// <returns> true </returns>
        /// 
        [Route("SubirLista")]
        [HttpPost]
        public ActionResult SubirLista(ConjuntoListaSubirDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
                var servicio = new ConjuntoListaService(unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();

                servicio.SubirLista(json, Usuario);
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo de conjunto lista
        /// </summary>

        [HttpGet("ObtenerCombo")]
        public IActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ConjuntoListaService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("GenerarUrlFormulariosLink")]
        public IActionResult GenerarUrlFormulariosLink(GenerarFormularioDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var servicio = new ConjuntoListaService(unitOfWork);
                return Ok(servicio.GenerarUrlFormulariosLink(datos, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
