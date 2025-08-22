using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PanelDepositoDisponibleController
    /// Autor: Margiory  Ramirez Neyra.
    /// Fecha: 14/12/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PanelDepositoDisponibleController : Controller
    {
        private IUnitOfWork unitOfWork;
        public PanelDepositoDisponibleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }



        /// Tipo Función: GET
        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en TPanelIngresoDisponible
        /// </summary>
        /// <returns> List<TPanelIngresoDisponible> </returns>


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPanelDepositoDisponible()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var serPanelIngresoRep = new PanelIngresoDisponibleService(unitOfWork);
                var Records = serPanelIngresoRep.ObtenerPanelDepositoDisponible();
                var Total = Records.Count();
                return Ok(new { Records, Total });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en TPanelIngresoDisponible
        /// </summary>
        /// <returns> List<TPanelIngresoDisponible> </returns>

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerDiaSemana()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var serDiaSemanaRep = new DiaSemanaService(unitOfWork);
                return Ok(serDiaSemanaRep.ObtenerCombo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta todos los registros guardados en TPanelIngresoDisponible
        /// </summary>
        /// <returns> List<TPanelIngresoDisponible> </returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarPanelDepositoDisponible([FromBody] PanelDepositoDisponibleDTO Json)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PanelIngresoDisponibleService(unitOfWork);
                var respuesta = servicio.InsertarPanelDepositoDisponible(Json);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza todos los registros guardados en TPanelIngresoDisponible
        /// </summary>
        /// <returns> List<TPanelIngresoDisponible> </returns>


        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarPanelDepositoDisponible([FromBody] PanelDepositoDisponibleDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PanelIngresoDisponibleService(unitOfWork);
                var respuesta = servicio.ActualizarPanelDepositoDisponible(Json);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina todos los registros guardados en TPanelIngresoDisponible
        /// </summary>
        /// <returns> List<TPanelIngresoDisponible> </returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarPanelDepositoDisponible([FromBody] EliminarDTO eliminarDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PanelIngresoDisponibleService(unitOfWork);
                var respuesta = servicio.EliminarPanelDepositoDisponible(eliminarDTO);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}