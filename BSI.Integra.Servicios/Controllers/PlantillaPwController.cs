using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PlantillaPwController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 08/08/2022
    /// <summary>
    /// Gestión de PlantillaPw
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PlantillaPwController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IPlantillaPwService _plantillaPwService;
        public PlantillaPwController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _plantillaPwService = new PlantillaPwService(_unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PlantillaPw
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerPlantillaPw")]
        public IActionResult ObtenerPlantillaPw()
        {
            try
            {
                return Ok(_plantillaPwService.Obtener());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PlantillaPw para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            try
            {
                return Ok(_plantillaPwService.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Adriana Chipana Ampuero.
        /// Fecha: 22/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Plantilla para combo de whatsapp.
        /// </summary

        [HttpGet("ObtenerComboWhatsapp")]
        public IActionResult ObtenerComboWhatsapp()
        {
            try
            {
                return Ok(_plantillaPwService.ObtenerComboWhatsapp());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Metodo HTTP: GET.
        /// Autor: Gilmer Qm
        /// Fecha: 22/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combos para el modulo de (C) Plantillas de documentos del portal web
        /// </summary> 
        /// <returns> plantillaPwComboModuloDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosModulo()
        {
            try
            {
                var respuesta = _plantillaPwService.ObtenerCombosModulo();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// IMPORTANTE: LA TABLA PLA.T_RevisionNivel_PW NO TIENE NINGUN REGISTRO A LA FECHA (2023-06-23) - ¿Realmente es necesario en el módulo?
        /// Metodo HTTP: GET.
        /// Autor: Gilmer Qm
        /// Fecha: 23/06/2023
        /// Version: 1.0 
        /// <summary>
        /// Obtiene el registro de T_RevisionNivel_PW por el idRevisionPw
        /// </summary>  
        /// <param name="idRevisionPw"> (PK) de T_Revision_Pw </param>
        /// <returns> RevisionNivelPw </returns>
        [Route("[action]/{idRevisionPw}")]
        [HttpGet]
        public ActionResult ObtenerRevisionNivelPwPorIdRevisionPw(int idRevisionPw)
        {
            try
            {
                IRevisionNivelPwService revisionNivelPwRepositorio = new RevisionNivelPwService(_unitOfWork);
                return Ok(revisionNivelPwRepositorio.ObtenerPorIdRevisionPw(idRevisionPw));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: GET.
        /// Autor: Gilmer Qm
        /// Fecha: 23/06/2023
        /// Version: 1.0 
        /// <summary>
        /// Obtiene las secciones plantillas contenidos por el IdPlantillaPw agrupados
        /// </summary>  
        /// <param name="idRevisionPw"> (PK) de T_Revision_Pw </param>
        /// <returns> List<seccionPwPlantillaPwAgrupadoDTO> </returns>
        [Route("[action]/{idRevisionPw}")]
        [HttpGet]
        public IActionResult ObtenerSeccionesPlantillaPorIdPlantillaPW(int idRevisionPw)
        {
            try
            {
                var seccionPwPlantillaPwAgrupados = _plantillaPwService.ObtenerSeccionesPorIdPlantillaPW(idRevisionPw);
                return Ok(seccionPwPlantillaPwAgrupados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: GET.
        /// Autor: Gilmer Qm
        /// Fecha: 26/06/2023
        /// Version: 1.0 
        /// <summary>
        /// Obtiene los paises asociados al IdPlantillaPw
        /// </summary> 
        /// <param name="idPlantillaPw"> (PK) de T_PLantilla_Pw </param>
        /// <returns> List<PlantillaPaisFiltroDTO> </returns>
        [Route("[action]/{idPlantillaPw}")]
        [HttpGet]
        public ActionResult ObtenerPaisesPorIdPlantillaPw(int idPlantillaPw)
        {
            try
            {
                var plantillaPaisFiltros = _plantillaPwService.ObtenerPaisesPorIdPlantillaPw(idPlantillaPw);
                return Ok(plantillaPaisFiltros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: POST.
        /// Autor: Gilmer Qm
        /// Fecha: 26/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la ta tabla y sus detalles
        /// </summary>   
        /// <param name="plantillaPwParametros"> parametros de la nueva Plantilla_PW y sus detalles </param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] PlantillaPwParametrosDTO plantillaPwParametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var plantillaPaisFiltros = _plantillaPwService.Insertar(plantillaPwParametros, registroClaimToken.UserName);
                return Ok(plantillaPaisFiltros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: PUT.
        /// Autor: Gilmer Qm
        /// Fecha: 26/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la ta tabla y sus detalles
        /// </summary>   
        /// <param name="plantillaPwParametros"> parametros de la nueva Plantilla_PW y sus detalles </param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] PlantillaPwParametrosDTO plantillaPwParametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var plantillaPaisFiltros = _plantillaPwService.Actualizar(plantillaPwParametros, registroClaimToken.UserName);
                return Ok(plantillaPaisFiltros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: DELETE.
        /// Autor: Gilmer Qm
        /// Fecha: 27/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica del registro y sus detalles
        /// </summary>   
        /// <param name="id"> (PK) </param>
        /// <returns> bool </returns>
        [Route("[action]/{id}")]
        [HttpDelete]
        public ActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var plantillaPaisFiltros = _plantillaPwService.Eliminar(id, registroClaimToken.UserName);
                return Ok(plantillaPaisFiltros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
