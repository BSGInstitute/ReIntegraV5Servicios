using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteAdwordsApiVolumenBusquedaController
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 25/10/2022
    /// Comentario para cambios o modificaciones:Codigo legado en funciones --!!!!!! NO DEBE SER MODIFICADA SIN TENER ENCUENTA EL DANO EN V4 !!!!!!!--
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteAdwordsApiVolumenBusquedaController : Controller
    {
        private IUnitOfWork unitOfWork;
        private readonly IConfiguration configRoot;

        public ReporteAdwordsApiVolumenBusquedaController(IUnitOfWork unitOfWork, IConfiguration configRoot)
        {
            this.unitOfWork = unitOfWork;
            this.configRoot = configRoot;
        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 25/10/2022
        /// Versión: 1.0
        /// Autor mdificacion: Rodrigo Montesinos
        /// Fecha modificacion: 03/11/2023
        /// Modificacion, se cambio el servicio y se agrego el correo obtenido por el usuario.
        /// <summary>
        /// Genera un reporte por busqueda de palabras entre fechas especificas
        /// </summary>
        /// <returns> List<ReporteAdwordsApiPalabrasClaveRespuestaDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] FiltroReporteAdwordsApiVolumenBusquedaDTO FiltroReporteAdwordsApiVolumenBusquedaDTO)

        {

            try
            {
                var servicio = new AdworkCredencialApiService(unitOfWork,configRoot);
                var usuarioResponsable = new IntegraAspNetUserService(unitOfWork).ObtenerPorUsuario(FiltroReporteAdwordsApiVolumenBusquedaDTO.Usuario).FirstOrDefault();
                return Ok(servicio.GenerarReporte(FiltroReporteAdwordsApiVolumenBusquedaDTO,false,usuarioResponsable.Email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos
        /// Fecha: 03/11/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza un reporte por busqueda de palabras entre fechas especificas
        /// </summary>
        /// <returns> List<ReporteAdwordsApiPalabrasClaveRespuestaDTO> </returns>
        [AllowAnonymous]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarBusquedaPalabras([FromBody] FiltroReporteAdwordsApiVolumenBusquedaDTO FiltroReporteAdwordsApiVolumenBusquedaDTO)

        {

            try
            {
                var servicio = new AdworkCredencialApiService(unitOfWork, configRoot);
                var usuarioResponsable = new IntegraAspNetUserService(unitOfWork).ObtenerPorUsuario(FiltroReporteAdwordsApiVolumenBusquedaDTO.Usuario).FirstOrDefault();
                return Ok(servicio.GenerarReporte(FiltroReporteAdwordsApiVolumenBusquedaDTO, true,usuarioResponsable.Email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos
        /// Fecha: 03/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene codigos de pais que se encuentran aceptados en el api de google.ads
        /// </summary>
        /// <returns> List<TPai> </returns>
        [Route("obtener/codigo/google/pais")]
        [HttpGet]
        public ActionResult ObtenerCodigogsGoogleDePais()
        {
            try
            {
                return Ok(new AdworkCredencialApiService(unitOfWork).ObtenerPaises());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
