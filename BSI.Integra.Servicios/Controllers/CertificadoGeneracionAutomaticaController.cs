using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CertificadoGeneracionAutomaticaController
    /// Autor: Jonathan Caipo
    /// Fecha: 26/11/2022
    /// <summary>
    /// Gestión de Agenda
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CertificadoGeneracionAutomaticaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public CertificadoGeneracionAutomaticaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan caipo
        /// Fecha: 26/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera la Contancia por Matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idPlantilla"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[Action]/{idMatriculaCabecera}/{idPlantilla}/{id}")]
        [HttpGet]
        public ActionResult GenerarConstanciaPorMatricula(int idMatriculaCabecera, int idPlantilla, int id)
        {
            TarifarioService servicioTarifarioRepositorio = new TarifarioService(unitOfWork);
            CertificadoDetalleService servicioCertificadoDetalle = new CertificadoDetalleService(unitOfWork);
            CertificadoGeneradoAutomaticoService servicioCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoService(unitOfWork);
            PGeneralConfiguracionPlantillaService servicioPGeneralConfiguracionPlantilla = new PGeneralConfiguracionPlantillaService(unitOfWork);
            CertificadoGeneradoAutomaticoContenidoService servicioCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoService(unitOfWork);

            var dato = servicioPGeneralConfiguracionPlantilla.ObtenerDatosParaConstanciasPorMatricula(idMatriculaCabecera);

            try
            {
                DocumentoService documentos = new DocumentoService(unitOfWork);
                int idPlantillaBase = 0;
                string codigoCertificado = "";
                var documentoByte = documentos.GenerarVistaPreviaCertificado(idPlantilla, 0, dato.IdOportunidad, ref idPlantillaBase, ref codigoCertificado);

                CertificadoGeneradoAutomatico certificadoGenerado = new CertificadoGeneradoAutomatico();
                certificadoGenerado.IdMatriculaCabecera = dato.IdMatriculaCabecera;
                certificadoGenerado.IdPgeneral = dato.IdPgeneral;
                certificadoGenerado.IdPgeneralConfiguracionPlantilla = dato.IdPgeneralConfiguracionPlantilla;
                certificadoGenerado.IdUrlBlockStorage = 1;
                certificadoGenerado.ContentType = "application/pdf";
                certificadoGenerado.NombreArchivo = idPlantillaBase == 12 ? codigoCertificado : documentos.documentoObjDTO.contenidoCertificado.CorrelativoConstancia.ToString();
                certificadoGenerado.IdPespecifico = dato.IdPespecifico;
                certificadoGenerado.IdPlantilla = idPlantilla;
                certificadoGenerado.FechaEmision = DateTime.Now;
                certificadoGenerado.FechaCreacion = DateTime.Now;
                certificadoGenerado.FechaModificacion = DateTime.Now;
                certificadoGenerado.UsuarioCreacion = "SYSTEM";
                certificadoGenerado.UsuarioModificacion = "SYSTEM";
                certificadoGenerado.Estado = true;
                certificadoGenerado.IdCronogramaPagoTarifario = id;
                servicioCertificadoGeneradoAutomatico.Add(certificadoGenerado);

                CertificadoGeneradoAutomaticoContenido contenidoCertificadoBO = documentos.documentoObjDTO.contenidoCertificado;
                contenidoCertificadoBO.IdCertificadoGeneradoAutomatico = certificadoGenerado.Id;
                contenidoCertificadoBO.FechaFinCapacitacion = contenidoCertificadoBO.FechaFinCapacitacion == null ? "" : contenidoCertificadoBO.FechaFinCapacitacion;
                contenidoCertificadoBO.Estado = true;
                contenidoCertificadoBO.FechaCreacion = DateTime.Now;
                contenidoCertificadoBO.FechaModificacion = DateTime.Now;
                contenidoCertificadoBO.UsuarioCreacion = "SYSTEM";
                contenidoCertificadoBO.UsuarioModificacion = "SYSTEM";

                servicioCertificadoGeneradoAutomaticoContenido.Add(contenidoCertificadoBO);

                var Url = servicioCertificadoDetalle.GuardarArchivoCertificado(documentoByte, "application/pdf", certificadoGenerado.NombreArchivo);

                //Consulta update -> id 
                var actualizar = servicioTarifarioRepositorio.ActualizarGestionadoCronogramaPagoTarifario(id);
                if (!actualizar)
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            List<string> correos = new List<string>
                    {
                        "fvaldez@bsginstitute.com",
                        "lpacsi@bsginstitute.com"
                    };
                TMK_MailService Mailservice = new TMK_MailService();

                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "fvaldez@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = "Error Proceso Constancia por matricula",
                    Message = "IdMatricula: " + dato.IdMatriculaCabecera.ToString() + ", IdPgeneralConfiguracionPlantilla: " + dato.IdPgeneralConfiguracionPlantilla.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString(),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
            }
            return Ok(true);
        }
    }
}
