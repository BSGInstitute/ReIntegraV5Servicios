using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ProyeccionFurController
    /// Autor: Griselberto Huaman
    /// Fecha: 14/03/2023
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[Authorize]
    public class ProyeccionFurController : Controller
    {
        private IUnitOfWork unitOfWork;
        public ProyeccionFurController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Obtiene la configuracion activa.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [HttpGet("ObtenerConfiguracionProyeccionFurActivo")]
        public IActionResult ObtenerConfiguracionProyeccionFurActivo()
        {
            try
            {
                var servicio = new ConfiguracionProyeccionFurService(unitOfWork);
                return Ok(servicio.ObtenerConfiguracionProyeccionFurActivos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Obtiene la configuracion activa.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [HttpGet("ObtenerFurConfiguracionAutomaticaByIdArea/{IdArea}")]
        public IActionResult ObtenerFurConfiguracionAutomaticaByIdArea(int IdArea)
        {
            try
            {
                var servicio = new FurConfiguracionAutomaticaService(unitOfWork);
                return Ok(servicio.ObtenerFurConfiguracionAutomaticaByIdArea(IdArea));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        /// Tipo Función: GET
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Obtiene la configuracion activa.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [HttpGet("ObtenerFurConfiguracionAutomaticaByIdAreaActivo/{IdArea}")]
        public IActionResult ObtenerFurConfiguracionAutomaticaByIdAreaActivo(int IdArea)
        {
            try
            {
                var servicio = new FurConfiguracionAutomaticaService(unitOfWork);
                return Ok(servicio.ObtenerFurConfiguracionAutomaticaByIdAreaActivo(IdArea));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Obtiene la configuracion activa.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [HttpPost("ObtenerFurConfiguracionAutomaticaNoValida")]
        public IActionResult ObtenerFurConfiguracionAutomaticaNoValida([FromBody] ParametrosEnvioDTO data)
        {
            try
            {
                var servicio = new FurConfiguracionAutomaticaService(unitOfWork);
                return Ok(servicio.ObtenerFurConfiguracionAutomaticaNoValida(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Proyecta FURS general.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [HttpGet("ValidarCabeceraFurConfiguracionAutomaticaEnProcesoByIdArea/{IdArea}")]
        public IActionResult ValidarCabeceraFurConfiguracionAutomaticaEnProcesoByIdArea(int IdArea)
        {
            try
            {
                var servicio = new CabeceraFurConfiguracionAutomaticaService(unitOfWork);
                return Ok(servicio.ValidarCabeceraFurConfiguracionAutomaticaEnProcesoByIdArea(IdArea));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Proyecta FURS general.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [HttpGet("ObtenerCongelamientoProyeccionFur/{IdCabecera}")]
        public IActionResult ObtenerCongelamientoProyeccionFur(int IdCabecera)
        {
            try
            {
                var servicio = new CongelamientoProyeccionFurService(unitOfWork);
                return Ok(servicio.ObtenerCongelamientoProyeccionFur(IdCabecera));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Obtiene la configuracion por Id.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [HttpGet("ObtenerConfiguracionProyeccionFurById/{Id}")]
        public IActionResult  ObtenerConfiguracionProyeccionFurById(int Id)
        {
            try
            {
                var servicio = new ConfiguracionProyeccionFurService(unitOfWork);
                return Ok(servicio.ObtenerConfiguracionProyeccionFurById(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Cambia el estado a En REvision.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [HttpPost("CambiarEstadoAEnRevision")]
        public IActionResult CambiarEstadoAEnRevision([FromBody] CambioDeEstadoDTO data)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new CabeceraFurConfiguracionAutomaticaService(unitOfWork);
                    return Ok(servicio.CambiarEstadoAEnRevision(data, Usuario));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
            
        }

        /// Tipo Función: POST
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Cambia el estado a En REvision.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [HttpPost("CambiarEstadoArechazado")]
        public IActionResult CambiarEstadoArechazado([FromBody] RechazoProyeccionDTO data )
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new CabeceraFurConfiguracionAutomaticaService(unitOfWork);
                    return Ok(servicio.CambiarEstadoArechazado(data, Usuario));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }

        }
        /// Tipo Función: POST
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// elimina logicamente los FURS.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [HttpPost("EliminarLogicamenteFurProyectadoPorHistorico")]
        public IActionResult EliminarLogicamenteFurProyectadoPorHistorico([FromBody] EliminarFurProyectadoDTO data)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    data.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.EliminarLogicamenteFurProyectadoPorHistorico(data));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }

        }

        /// Tipo Función: POST
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Proyecta FURS general.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [HttpPost("ProyectarFurCostosFijos")]
        public IActionResult ProyectarFurCostosFijos([FromBody] ProyeccionFurDTO data)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new ProyeccionFurService(unitOfWork);
                    return Ok(servicio.ProyectarFurCostosFijos(data, _respuestaCorrecta.RegistroClaimToken.UserName));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
           
        }
       



    }
}









































