using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;


namespace BSI.Integra.Servicios.Controllers.Marketing.CampaniasMailingWhatsapp
{
    /// Controlador: CampaniaMailingFiltradoController
    /// Autor: Rodrigo Montesinos.
    /// Fecha: 05/12/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    
    [Authorize]
    [Route("api/[controller]")] 
    [ApiController]
    [EnableCors("CorsVista")]
    public class CampaniasMailingWhatsappController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private CampaniasMailingWhatsappService campaniasMailingWhatsappService;
        private CampaniaGeneralService campaniaGeneralService;

        public CampaniasMailingWhatsappController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            campaniasMailingWhatsappService = new CampaniasMailingWhatsappService(this.unitOfWork);
            campaniaGeneralService = new CampaniaGeneralService(this.unitOfWork);
        }

        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener las campanias mailing creadas
        /// </summary>
        /// <returns>Retorna una lista de campanias mailing</returns>
        [Route("ObtenerCampaniaMailingGrid")]
        [HttpGet]
        public IActionResult ObtenerCampaniaMailingGrid()
        {
            try
            {
                return Ok(campaniasMailingWhatsappService.ObtenerListaCampaniaMailing());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener las categorias origen
        /// </summary>
        /// <returns>Retorna una lsita de categoria origen  </returns>
        [Route("ObtenerListaCategoriaOrigen")]
        [HttpGet]
        public IActionResult ObtenerListaCategoriaOrigen()
        {
            try
            {
                return Ok(campaniasMailingWhatsappService.ObtenerListaCategoriaOrigen());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener un objeto que contiene una lista de asesores
        /// </summary>
        /// <param name="Id">identificador unico </param>
        /// <returns>Retorna CombosAreasSubAreasMailchimpDTO </returns>
        [Route("ObtenerListaCampaniaMailingDetalle/{Id}")]
        [HttpGet]
        public ActionResult ObtenerListaCampaniaMailingDetalle(int Id)
        {
            try
            {
                return Ok(campaniasMailingWhatsappService.ObtenerDetalle(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener un objeto que contiene una lista de asesores
        /// </summary>
        /// <returns>Retorna CombosAreasSubAreasMailchimpDTO </returns>
        [Route("ObtenerListaRemitenteMailingAsesor")]
        [HttpGet]
        public ActionResult ObtenerListaRemitenteMailingAsesor()
        {
            try
            {
                return Ok(campaniasMailingWhatsappService.ObtenerListaRemitenteMailingAsesor());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener los centros de costo
        /// </summary>
        /// <returns>Retorna uan lista de filtroDTO </returns>
        [Route("ObtenerCentroCosto")]
        [HttpGet]
        public ActionResult ObtenerCentroCosto()
        { 
            try
            {
                return Ok(campaniasMailingWhatsappService.ObtenerParaFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener un objeto que contiene areas, subareas, programas
        /// </summary>
        /// <returns>Retorna CombosAreasSubAreasMailchimpDTO </returns>
        [Route("ObteneAreasSubAreas")]
        [HttpGet]
        public ActionResult ObteneAreasSubAreas()
        {
            try
            {
                return Ok(campaniasMailingWhatsappService.ObteneAreasSubAreas());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener todas las areas
        /// </summary>
        /// <returns>Retorna una lista de filtroDTO</returns>
        [Route("ObteneAreasAreas")]
        [HttpGet]
        public ActionResult ObteneAreasAreas()
        {
            try
            {
                return Ok(campaniasMailingWhatsappService.ObteneAreas());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener las subareas que le perteneces a un area especifica
        /// </summary>
        /// <param name="idArea">Entidad a insertar</param>
        /// <returns>Retorna una lista de  areas</returns>
        [Route("ObtenerSubAreaPorIdDeArea")]
        [HttpGet]
        public ActionResult ObtenerSubAreaPorIdDeArea(int idArea)
        {
            try
            {
                return Ok(campaniasMailingWhatsappService.ObtenerSubAreas(idArea));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener el programa general por la subarea 
        /// </summary>
        /// <param name="idSubArea">identificador unico de subarea</param>
        /// <returns>Retorna una lista de subareas</returns>
        [Route("ObtenerProgramaGeneralPorIdDeSubArea")]
        [HttpGet]
        public ActionResult ObtenerProgramaGeneralPorIdDeSubArea(int idSubArea)
        {
            try
            {
                return Ok(campaniasMailingWhatsappService.ObtenerProgramaGeneral(idSubArea));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener las probabilidades
        /// </summary>
        /// <returns>Retorna una lista de probabilidades</returns>
        [Route("ObtenerProbabilidades")]
        [HttpGet]
        public ActionResult ObtenerProbabilidades()
        {
            try
            {
                return Ok(campaniasMailingWhatsappService.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para eliminar una entidad
        /// </summary>
        /// <param name="IdCampaniaMailing">Identificador de la entidad a eliminar</param>
        /// <returns>Retorna la entidad insertada</returns>
        [Route("Eliminar/{IdCampaniaMailing}")]
        [HttpDelete]
        public ActionResult Eliminar(int IdCampaniaMailing)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                return Ok(campaniasMailingWhatsappService.Eliminar(IdCampaniaMailing, _respuestaCorrecta.RegistroClaimToken.UserName));
            }
            catch (Exception e)
            {
                return BadRequest(new { Resultado = e.Message });
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion con datos de authorize para obtener al usuario logueado
        /// </summary>
        /// <returns>Retorna usuario logueado</returns>
        [Route("ObtenerUsuarioLogeado")]
        [HttpGet]
        public IActionResult ObtenerUsuarioLogueado()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                Dictionary<string, string> dataretornada = new Dictionary<string, string>();
                dataretornada.Add("usuario", usuario);
                return Ok(dataretornada);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("Ejecutar/replicado/{id}")]
        [HttpGet]
        public IActionResult EjecutarSPParaRepliacdo(int id) 
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                return Ok(JsonConvert.SerializeObject(campaniasMailingWhatsappService.EjecutarReplicado(id, usuario)));
            }catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jorge Gamero
        /// Fecha: 13/01/2025
        /// Versión: 1.0
        /// <summary>
        /// ObtenerSubAreaPorIdDeAreaLista
        /// </summary>
        /// <returns>rpta<returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult ObtenerSubAreaPorIdDeAreaLista(List<int> idArea)
        {
            try
            {
                return Ok(campaniasMailingWhatsappService.ObtenerSubAreaPorIdDeAreaLista(idArea));
            }
            catch
            {
                throw;
            }
        }

    }
}
