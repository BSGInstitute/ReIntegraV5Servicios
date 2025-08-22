using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RevertirFaseController
    /// Autor: Margiory Ramirez
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RevertirFaseController : Controller
    {
        private IUnitOfWork unitOfWork;
        public RevertirFaseController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros Oportunida dades de la tabla TOportunidad
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerOportunidades([FromBody] RevertirFaseFiltroGrillaDTO obj)
        {
            try
            {
                var _serviOportunidad = new OportunidadService(unitOfWork);


                var OportunidadManual = _serviOportunidad.ObtenerPorFiltroRevertirFase(obj.filtro, obj.paginador, obj.filter);

                return Ok(OportunidadManual);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("[Action]/{IdOportunidad}")]
        public ActionResult ObtenerDetalleOportunidad(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _serviOportunidadLog = new OportunidadLogService(unitOfWork);
                var oportunidadLog = _serviOportunidadLog.ObtenerDetalleOportunidad(IdOportunidad);
                return Ok(oportunidadLog);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [Route("[Action]")]
        [HttpPost]
        public ActionResult RevertirOportunidad([FromBody] RevertirFaseOportunidadFiltroDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _serviOportunidadLog = new OportunidadService(unitOfWork);
                var oportunidadLog = _serviOportunidadLog.RevertirOportunidad(Obj);
                return Ok(oportunidadLog);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult RevertirOportunidades([FromBody] List<RevertirFaseOportunidadFiltroDTO> Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _serviOportunidadLog = new OportunidadService(unitOfWork);
                foreach (var ob in Obj)
                {
                    var oportunidadLog = _serviOportunidadLog.RevertirOportunidad(ob);
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
