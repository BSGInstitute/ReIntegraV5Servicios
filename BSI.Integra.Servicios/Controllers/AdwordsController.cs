using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AdwordsController
    /// Autor: Adriana Chipana.
    /// Fecha: 14/02/2022
    /// <summary>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AdwordsController : Controller
    {
        private IUnitOfWork unitOfWork;
        public AdwordsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }



        [Route("[action]")]
        [HttpPost]
        public IActionResult ProcesarGoogleLeads(GoogleFormularioLeadgenDTO leads)
        {
            try
            {

                var idAsignacionAutomaticaTemp = new AdwordsService(unitOfWork).ProcesarGoogleLeads(leads);
                return Ok(idAsignacionAutomaticaTemp.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult CrearOportunidadAdwords(StringDTO idAsignacionAutomatica)
        {
            try
            {

                var resp = new AdwordsService(unitOfWork).CrearOportunidadWebhookAdwords(idAsignacionAutomatica.Valor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerTodoCampaniaAdwords()
        {
            try
            {

                var resp = new AdwordsService(unitOfWork).ObtenerTodoCampaniaAdwords();
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult ObtenerCampaniaAdwords(int id)
        {
            try
            {

                var resp = new AdwordsService(unitOfWork).ObtenerCampaniaAdwords(id);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarCampaniaAdwords(CampaniaAdwordsDTO datos)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                datos.Usuario = usuario;
                var resp = new AdwordsService(unitOfWork).InsertarCampaniaAdwords(datos);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarCampaniaAdwords(ActualzarCampaniaAdwordsDTO datos)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                datos.Usuario = usuario;
                var resp = new AdwordsService(unitOfWork).ActualizarCampaniaAdwords(datos);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult EliminarCampaniaAdwords(int id)
        {
            try
            {

                var resp = new AdwordsService(unitOfWork).EliminarCampaniaAdwords(id);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}









































