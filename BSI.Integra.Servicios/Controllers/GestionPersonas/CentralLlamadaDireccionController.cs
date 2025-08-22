using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: CentralLlamadaDireccionController
    /// Autor: Victor Hinojosa
    /// Fecha: 25/09/2024
    /// <summary>
    /// Gestión de Central de llamadas 
    /// Interactura con la tabla 'conf.T_CentralDireccionLlamada'
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class CentralLlamadaDireccionController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ICentralLlamadaDireccionService _centralLlamadaDireccionService;
        private ITokenManager _tokenManager;

        public CentralLlamadaDireccionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _centralLlamadaDireccionService = new CentralLlamadaDireccionService(unitOfWork);
            _tokenManager = tokenManager;
            this.unitOfWork = unitOfWork;   
        }

        /// Método HTTP: GET
        /// Autor: Victor Hinojosa
        /// Fecha: 25/09/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las direcciones de central de llamadas
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            CentralLlamadaDireccionService centralLlamadaDireccionservice = new CentralLlamadaDireccionService(unitOfWork);
            var gestionCentral = centralLlamadaDireccionservice.Obtener();
            return Ok(gestionCentral);
        }

        /// Método HTTP: GET
        /// Autor: Victor Hinojosa
        /// Fecha: 25/09/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo DominioPbx
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerComboDominioPbx()
        {
            CentralLlamadaDireccionService centralLlamadaDireccionservice = new CentralLlamadaDireccionService(unitOfWork);
            var gestionDominio = centralLlamadaDireccionservice.ObtenerComboDominioPbx();
            return Ok(gestionDominio);
        }
    }
}
