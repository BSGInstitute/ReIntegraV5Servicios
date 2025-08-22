using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AvatarController
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/09/2022
    /// <summary>
    /// Gestión general de la tabla T_Avatar
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AvatarController : Controller
    {

        private IUnitOfWork unitOfWork;
        private IAvatarService iAvatarService;
        public AvatarController(IUnitOfWork unitOfWork, IAvatarService iAvatarService)
        {
            this.iAvatarService = iAvatarService;
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 07/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene avatar por usuario
        /// </summary>
        /// <returns> objeto DTO : AvatarDTO</returns>
        [Route("[action]/{Usuario}")]
        [HttpGet]
        public ActionResult ObtenerAvatar(string Usuario)
        {
            try
            {
                var resultado = iAvatarService.ObtenerAvatar(Usuario);
                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest("No se encontró usuario.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <returns> codifica caracteres especiales de html </returns>
        [Route("[action]/{parametro}")]
        [HttpGet]
        public ActionResult EncodeHtml(string parametro)
        {
            string s = parametro;
            string a = HttpUtility.HtmlDecode(s);

            return Ok(a);
        }
    }
}
