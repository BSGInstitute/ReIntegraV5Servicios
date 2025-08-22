using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MailingEnvioAutomaticoController
    /// Autor: Gilmer Quispe.
    /// Fecha: 12/12/2022
    /// <summary>
    /// Gestión general de Mailing envios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MailingEnvioAutomaticoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public MailingEnvioAutomaticoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/12/2022
        /// <summary>
        /// En base a un IdOportunidad, IdPlantilla y el flag de usar el personal por defecto o se genera y envía el correo especificado
        /// </summary>
        /// <param name="IdOportunidad">Id de la oportunidad que enviara el correo</param>
        /// <param name="IdPlantilla">Id de la plantilla que se enviara</param>
        /// <param name="PersonalPorDefecto">Flag para determinar si se usara el personal por defecto que se encuentra en la tabla conf.T_ConfiguracionFija</param>
        /// <returns>Response 200 si ha sido exitoso el envio o 400 si ha fallado</returns>
        [Route("[Action]/{idOportunidad}/{idPlantilla}/{personalPorDefecto?}")]
        [HttpGet]
        public ActionResult EnvioCorreoOportunidadPlantilla(int idOportunidad, int idPlantilla, bool personalPorDefecto = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var oportunidadService = new OportunidadService(unitOfWork);
                var personalService = new PersonalService(unitOfWork);
                var centroCostoService = new CentroCostoService(unitOfWork);
                var alumnoService = new AlumnoService(unitOfWork);
                var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaService(unitOfWork);
                var mandrilEnvioCorreoService = new MandrilEnvioCorreoService(unitOfWork);
                var oportunidad = oportunidadService.ObtenerPorId(idOportunidad);

                if (oportunidad.Id < 1)
                {
                    throw new Exception("Oportunidad no valida");
                }
                // int idPersonalFinal = PersonalPorDefecto ? ValorEstatico.IdPersonalCorreoPorDefecto : oportunidad.IdPersonalAsignado.Value;
                int idPersonalFinal = personalPorDefecto ? 4723 : oportunidad.IdPersonalAsignado.Value;
                var personal = personalService.ObtenerPorId(idPersonalFinal);

                if (personal.Id == 125)
                {
                    throw new Exception("Asesor automatico no es valido para envio de correos");
                }
                var centroCosto = unitOfWork.CentroCostoRepository.ObtenerPorId(oportunidad.IdCentroCosto.GetValueOrDefault());
                if (centroCosto.Nombre.Contains("INSTITUTO"))
                {
                    idPlantilla = 874; //ValorEstatico.IdPlantillaInformacionCarrera
                }

                var alumno = alumnoService.ObtenerPorId(oportunidad.IdAlumno.Value);

                List<string> correosPersonalizados = new List<string>
                {
                    alumno.Email1
                };

                ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta = new()
                {
                    IdOportunidad = idOportunidad,
                    IdPlantilla = idPlantilla,
                };
                var resultadoReemplazo = reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades(reemplazoEtiqueta, personalPorDefecto);
                PlantillaEmailMandrillDTO emailFinalEnvio = resultadoReemplazo.EmailReemplazado;
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = personal.Email,
                    Recipient = string.Join(",", correosPersonalizados.Distinct()),
                    Subject = emailFinalEnvio.Asunto,
                    Message = emailFinalEnvio.CuerpoHTML,
                    Cc = string.Empty,
                    Bcc = string.Empty,
                    AttachedFiles = null
                };

                TMK_MailService mailService = new TMK_MailService();
                mailService.SetData(mailDataPersonalizado);
                List<TMKMensajeIdDTO> listaIdsMailChimp = mailService.SendMessageTask();
                List<MandrilEnvioCorreo> listaMandrilEnvioCorreo = new List<MandrilEnvioCorreo>();

                foreach (var mensaje in listaIdsMailChimp)
                {
                    var mandrilEnvioCorreo = new MandrilEnvioCorreo
                    {
                        IdOportunidad = oportunidad.Id,
                        IdPersonal = oportunidad.IdPersonalAsignado,
                        IdAlumno = oportunidad.IdAlumno,
                        IdCentroCosto = oportunidad.IdCentroCosto,
                        IdMandrilTipoAsignacion = 7, //Envio masivo automatico nuevas oportunidades
                        EstadoEnvio = 1,
                        IdMandrilTipoEnvio = 1, // Correo enviado automaticamente
                        FechaEnvio = DateTime.Now,
                        Asunto = emailFinalEnvio.Asunto,
                        FkMandril = mensaje.MensajeId,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "EnvioAutomatico",
                        UsuarioModificacion = "EnvioAutomatico",
                        EsEnvioMasivo = false
                    };
                    if (listaMandrilEnvioCorreo != null)
                    {
                        listaMandrilEnvioCorreo.Add(mandrilEnvioCorreo);
                    }
                    else
                    {
                        continue;
                    }
                }
                mandrilEnvioCorreoService.Add(listaMandrilEnvioCorreo);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
