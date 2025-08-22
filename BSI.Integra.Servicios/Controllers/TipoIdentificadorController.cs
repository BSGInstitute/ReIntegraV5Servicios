using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: TipoIdentificadorController
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/12/2022
    /// <summary>
    /// Gestión de Tipo Identificador
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TipoIdentificadorController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public TipoIdentificadorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;  
        }
        /// Metodo: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con el estado = 1
        /// </summary> 
        /// <returns> List<TipoIdentificadorComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            try
            {
                var _repTipoIdentificador = new TipoIdentificadorService(unitOfWork);
                return Ok(_repTipoIdentificador.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
