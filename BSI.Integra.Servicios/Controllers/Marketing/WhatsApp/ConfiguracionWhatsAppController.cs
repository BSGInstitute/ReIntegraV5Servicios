using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Transactions;
using System.Globalization;

namespace BSI.Integra.Servicios.Controllers.Marketing.WhatsApp
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfiguracionWhatsAppController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ConfiguracionWhatsAppController(IUnitOfWork IUnitOfWork)
        {
            unitOfWork = IUnitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuracion de envio de una campania general
        /// </summary>
        /// <param name="id">Identificadro de campaniaGeneral</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Configuracion/Envio/porid/{id}")]
        [HttpGet]
        public IActionResult AgregarConfiguracionDeEnvio(int id)
        {
            return Ok(new ConfiguracionDeEnvioParaWhatsAppService(unitOfWork).GetById(id));
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Agrega una nueva configuracion de envio
        /// </summary>
        /// <param name="configuracion">Configuracion para el envio por WhatsApp</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Configuracion/Envio")]
        [HttpPost]
        public IActionResult AgregarConfiguracionDeEnvio([FromBody] ConfiguracionDeEnvioParaWhatsAppCreate configuracion)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
            return Ok(new ConfiguracionDeEnvioParaWhatsAppService(unitOfWork).Add(configuracion, usuario));
        }
        /// Tipo Función: PUT
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// actuliza una configuracion de envio
        /// </summary>
        /// <param name="configuracion">Configuracion para el envio por WhatsApp</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Configuracion/Envio/update")]
        [HttpPut]
        public IActionResult ActualizarConfiguracionDeEnvio([FromBody] ConfiguracionDeEnvioParaWhatsAppCreate configuracion)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
            return Ok(new ConfiguracionDeEnvioParaWhatsAppService(unitOfWork).Update(configuracion, usuario));
        }
        /// Tipo Función: DELETE
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Elimina una configuracion de envio
        /// </summary>
        /// <param name="idConfiguracionDeEnvioParaWhatsAppDTO">identificador unico de TConfiguracionDeEnvioParaWhatsApp</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Configuracion/Envio/delete/{idConfiguracionDeEnvioParaWhatsAppDTO}")]
        [HttpDelete]
        public IActionResult EliminarConfiguracionDeEnvio(int idConfiguracionDeEnvioParaWhatsAppDTO)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
            return Ok(new ConfiguracionDeEnvioParaWhatsAppService(unitOfWork).Delete(idConfiguracionDeEnvioParaWhatsAppDTO, usuario));
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// obtiene un personal encargado del envio
        /// </summary>
        /// <param name="id">identificador unico de TPersonalEncargadoDeEnvioDeConsultum</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Configuracion/Envio/segmento/personal/{id}")]
        [HttpGet]
        public IActionResult AgregarConfiguracionDeSegementacionPersonal(int id)
        {
            return Ok(new PersonalEncargadoDeEnvioDeConsultumService(unitOfWork).GetbyId(id));
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// obtiene un listado de personal encargado del envio
        /// </summary>
        /// <param name="idConfiguracionEnvio">identificador decampania general detalle</param>
        /// <returns>Una RespuestaGenerica</returns>
        [Route("Configuracion/Envio/segmento/personal/all/{idConfiguracionEnvio}")]
        [HttpGet]
        public IActionResult AgregarConfiguracionDeSegementacionPersonalAll(int idConfiguracionEnvio)
        {
            return Ok(new PersonalEncargadoDeEnvioDeConsultumService(unitOfWork).GetAllByConfiguracionEnvio(idConfiguracionEnvio));
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Asigna a un personal encargado del envio
        /// </summary>
        /// <param name="segmentoPersonal">Personal encargado de envio</param>
        /// <returns>Una RespuestaGenerica</returns>
        [Route("Configuracion/Envio/segmento/personal")]
        [HttpPost]
        public IActionResult AgregarConfiguracionDeSegementacionPersonal([FromBody] List<PersonalEncargadoDeEnvioDeConsultumDTO> segmentoPersonal)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
            return Ok(new PersonalEncargadoDeEnvioDeConsultumService(unitOfWork).Add(segmentoPersonal, usuario));
        }

        [Route("Configuracion/Envio/segmento/personal/actualizarEliminar/{idConfiguracionEnvio}")]
        [HttpPut]
        public IActionResult ActualizarEliminarConfiguracionDeSegementacionPersonal([FromBody] List<PersonalEncargadoDeEnvioDeConsultumDTO> segmentoPersonal, int idConfiguracionEnvio)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
            return Ok(new PersonalEncargadoDeEnvioDeConsultumService(unitOfWork).AddDelete(segmentoPersonal, idConfiguracionEnvio, usuario));
        }
        /// Tipo Función: PUT
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// actualiza un personal encargado del envio
        /// </summary>
        /// <param name="segmentoPersonal">Personal encargado de envio</param>
        /// <returns>Una RespuestaGenerica</returns>
        [Route("Configuracion/Envio/segmento/personal/actualizar")]
        [HttpPut]
        public IActionResult ActualizarConfiguracionDeSegementacionPersonal([FromBody] PersonalEncargadoDeEnvioDeConsultumDTO segmentoPersonal)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                //_respuestaCorrecta.RegistroClaimToken.UserName;
            return Ok(new PersonalEncargadoDeEnvioDeConsultumService(unitOfWork).Update(segmentoPersonal, usuario));
        }
        /// Tipo Función: DELETE
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// elimina un personal encargado del envio
        /// </summary>
        /// <param name="idPersonalEncargadoDeEnvioDeConsultum">identificador unico de Personal encargado de envio</param>
        /// <returns>Una RespuestaGenerica</returns>
        [Route("Configuracion/delete/segmento/personal/{idPersonalEncargadoDeEnvioDeConsultum}")]
        [HttpDelete]
        public IActionResult EliminarConfiguracionDeSegementacionPersonal(int idPersonalEncargadoDeEnvioDeConsultum)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
            return Ok(new PersonalEncargadoDeEnvioDeConsultumService(unitOfWork).Delete(idPersonalEncargadoDeEnvioDeConsultum, usuario));
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la cantida de datos por prioridad
        /// </summary>
        /// <param name="campaniageenraldetalle">ientificador unico de campania general detalle</param>
        /// <param name="idprioridad">prioridad de filtrado</param>
        /// <returns>Una RespuestaGenerica</returns>
        [Route("{idprioridad}/{campaniageenraldetalle}")]
        [HttpGet]
        public IActionResult ObtenerTotalDeDataPorPrioridad(int campaniageenraldetalle, int idprioridad)
        {
            var dat = new CampaniaWhatsAppFiltradoService(unitOfWork).ObtenerCantidadDeDatosPorPiroridad(idprioridad, campaniageenraldetalle);
            return Ok(dat);
        }
        /// <summary>
        /// Autor: Gian Miranda
        /// Descripción: Obtiene todos los envios fallidos por caida de servicios 2 para reenviar de los diferentes tipos
        /// </summary>
        /// <returns>Response 200, Caso contrario response 400 con el error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult EjecutarRecuperacionCaidaEnvioWhatsApp()
        {
            try
            {
                int cantidadSolicitudRechazada = new RegistroRecuperacionWhatsAppService(unitOfWork).ObtenerCantidadCaidaRecuperacionWhatsApp();

                if (cantidadSolicitudRechazada >= 5)
                {
                    new RegistroRecuperacionWhatsAppService(unitOfWork).EjecutarRecuperacionFallidoEnvioWhatsApp(2);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta un procedieminento para la recuperacion de errores
        /// </summary>
        /// <param name="IdTipoError">ientificador del tipo de error</param>
        /// <returns>Una resouesta en string</returns>
        [Route("[Action]/{IdTipoError}")]
        [HttpGet]
        public ActionResult EjecutarRecuperacionFallidoEnvioWhatsApp(int IdTipoError)
        {
            try
            {
                return Ok(new RegistroRecuperacionWhatsAppService(unitOfWork).EjecutarRecuperacionFallidoEnvioWhatsApp(IdTipoError));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Autor: Carlos Crispin Riquelme
        /// Descripción: La funcion  realice el envio respectivo ya a los datos validados  segun campania general
        /// </summary>
        /// <returns>Response 200, Caso contrario response 400 con el error</returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult EjecutarCampaniaGeneralEnvioWhatsApp()
        {
            try
            {
              return  Ok( new RegistroRecuperacionWhatsAppService(unitOfWork).EjecutarCampaniaGeneralEnvioWhatsApp());
            }catch(Exception e)
            {
                return BadRequest(e.Message);   
            }
        }
        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Esta funcion permite visualizar las listas pre procesadas por la vista del sistema
        /// </summary>
        /// <param name="ListasWhatsApp"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult VisualizacionListasWhatsAppEnvioAutomaticoMasivo([FromBody] List<ConjuntoListaDetalleWhatsAppDTO> ListasWhatsApp)
        {
            try
            {
                return Ok(new RegistroRecuperacionWhatsAppService(unitOfWork).VisualizacionListasWhatsAppEnvioAutomaticoMasivo(ListasWhatsApp));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Esta funcion nos permite ver el estado en el que se encuentra el pre-procesamiento
        /// </summary>
        /// <param name="IdConjuntoLista"></param>
        /// <returns></returns>
        [Route("[Action]/{IdConjuntoLista}")]
        [HttpPost]
        public ActionResult EstadoSeguimientoPreProcesoListaWhatsApp(int IdConjuntoLista)
        {
            try
            {
                return Ok(new RegistroRecuperacionWhatsAppService(unitOfWork).EstadoSeguimientoPreProcesoListaWhatsApp(IdConjuntoLista));
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la cantida de datos por prioridad
        /// </summary>
        /// <returns>Una RespuestaGenerica</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult EnvioMasivoReasignacionesOperaciones()
        {
            try
            {
                return Ok(new RegistroRecuperacionWhatsAppService(unitOfWork).EnvioMasivoReasignacionesOperaciones());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos
        /// Fecha: 21-03-2023
        /// version: 1.0
        /// <summary>
        /// Realiza la regulacion de las plantillas
        /// </summary>
        /// <param name="IdWhatsAppConfiguracionEnvio"></param>
        /// <returns></returns>
        [Route("[Action]/{IdWhatsAppConfiguracionEnvio}")]
        [HttpGet]
        public ActionResult RegularizarPlantilla(int IdWhatsAppConfiguracionEnvio)
        {
            try
            {
                return Ok(new RegistroRecuperacionWhatsAppService(unitOfWork).RegularizarPlantilla(IdWhatsAppConfiguracionEnvio));
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos
        /// Fecha: 21-03-2023
        /// version: 1.0
        /// <summary>
        /// realiza el procesamiento de listas de wpp para envio automatico y operacional
        /// </summary>
        /// <param name="ListaConjuntoListaDetalleWhatsApp"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarListasWhatsAppEnvioAutomaticoOperaciones([FromBody] List<ConjuntoListaDetalleWhatsAppDTO> ListaConjuntoListaDetalleWhatsApp)
        {
            try
            {
                new WhatsAppConfiguracionEnvioService(unitOfWork).ProcesarListasWhatsAppEnvioAutomaticoOperaciones(ListaConjuntoListaDetalleWhatsApp);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
