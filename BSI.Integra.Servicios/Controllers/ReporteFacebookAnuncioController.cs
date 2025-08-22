using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteFacebookAnuncioController
    /// Autor: Margiory Ramirez ..
    /// Fecha: 27/12/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteFacebookAnuncioController : Controller
    {
        private IUnitOfWork unitOfWork;
        public ReporteFacebookAnuncioController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }



        /// Tipo Función: GET
        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de anuncio de Facebook Metrica
        /// </summary>
        /// <param name="IdAreaCapacitacion">Id del grupo de filtro de programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]/{IdAreaCapacitacion?}")]
        [HttpGet]
        public ActionResult ObtenerReporteAnuncioFacebookMetrica(int? IdAreaCapacitacion)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            try
            {
                //List<ReporteAnuncioFacebookMetricaDTO> listaReporteAnuncioFacebookMetrica = _repAnuncioFacebookMetrica.ObtenerReporteAnuncioFacebookMetrica(IdAreaCapacitacion);
                var serAnuncioFacebookMetrica = new AnuncioFacebookMetricaService(unitOfWork);
                List<ReporteAnuncioFacebookMetricaDiasDTO> listaReporteAnuncioFacebookMetrica = serAnuncioFacebookMetrica.EstructurarReporteAnuncioFacebook(IdAreaCapacitacion);
                // return Ok(JsonConvert.SerializeObject(listaReporteAnuncioFacebookMetrica));


                return Ok(listaReporteAnuncioFacebookMetrica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }






        /// Tipo Función: GET
        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion basica para alimentar el modulo
        /// </summary>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerInformacionBasica()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var servicio = new AnuncioFacebookMetricaService(unitOfWork);
                return Ok(servicio.ObtenerInformacionBasica());


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        /// Tipo Función: GET
        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos de los anuncios del modulo de Facebook Metrica
        /// </summary>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosAnuncioFacebookMetrica()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var servicio = new AnuncioFacebookMetricaService(unitOfWork);
                return Ok(servicio.ObtenerCombosAnuncioFacebookMetrica());


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        /// Tipo Función: POST
        /// Autor: 
        /// Fecha: 15/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza las metricas de Facebook por intervalo de fecha
        /// </summary>
        /// <param name="FechaFiltroDescarga">Objeto de clase AnuncioFacebookMetricaFechaDTO</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarMetricaFacebookAnuncioPorIntervaloFecha([FromBody] AnuncioFacebookMetricaFechaDTO FechaFiltroDescarga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AnuncioFacebookMetricaService(unitOfWork);
                return Ok(servicio.ActualizarMetricaFacebookAnuncioPorIntervaloFecha(FechaFiltroDescarga));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }





    }
}

