using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp
{
    public class WhatsAppRemplazoEtiquetaService : IWhatsAppRemplazoEtiquetaService
    {
        private readonly IUnitOfWork unitOfWork;

        public WhatsAppRemplazoEtiquetaService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos 
        /// Fecha: 21-03-2023
        /// Descipcion: genera data para T_whatsAppConfiguracionPreEnvio funcion necesaria para genrar data usada anteriormente en wpp
        /// </summary>
        /// <param name="PreprocesamientoWhatsAppCampaniaGeneral"> objetoq ue contine informacion encesaria par ael preprocesameinto de datos </param>
        /// <returns></returns>
        public object FinalizarPreProcesamientoWhatsApp(PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO PreprocesamientoWhatsAppCampaniaGeneral)
        {
            try
            {
                // Hora de inicio
                var horaInicio = DateTime.Now;

                // Preparacion de ejecucion
                var campaniaGeneralService = new CampaniaGeneralService(unitOfWork);
                var campaniaGeneralDetalleService = new CampaniaGeneralDetalleService(unitOfWork);

                // Obtener CampaniaGeneralDetalle
                var campaniaGeneralDetalle = campaniaGeneralDetalleService.BuscarCampaniaGeneralDetallePorId(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle);

                if (!(campaniaGeneralDetalle != null && campaniaGeneralDetalle.Id > 0))
                {
                    return new { error = false, mensaje= "El detalle de la campania no existe" };
                }

                // Actualizacion de estado de ejecucion para integra
                try
                {
                    new CampaniaGeneralDetalleService(unitOfWork).ActualizarEstadoEjecucionCampaniaGeneralDetalle(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle, true, PreprocesamientoWhatsAppCampaniaGeneral.Usuario);
                }
                catch (Exception ex)
                {
                    try
                    {
                        var mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                        unitOfWork.LogRepository.Insert(new TLog
                        {
                            Ip = "-",
                            Usuario = "CampaniaGeneral",
                            Maquina = "-",
                            Ruta = "CampaniaGeneral/FinalizarPreProcesamientoWhatsApp",
                            Parametros = $"CampaniaGeneralDetalle={campaniaGeneralDetalle.Id}",
                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                            Tipo = "UPDATE",
                            IdPadre = 0,
                            UsuarioCreacion = "CampaniaGeneral",
                            UsuarioModificacion = "CampaniaGeneral",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        });
                        unitOfWork.Commit();
                    }
                    catch (Exception)
                    {
                    }
                }

                var campaniaGeneral = unitOfWork.campaniaGeneralRepositorio.FirstBy(x => x.Id == campaniaGeneralDetalle.IdCampaniaGeneral);

                if (campaniaGeneral == null)
                {
                    return new { error = true, mensaje = "La campania no existe"};
                }

                if (!campaniaGeneral.IdPlantillaWhatsapp.HasValue)
                {
                    return new { error = true, mensaje = "No tiene una plantilla asignada" };
                }

                // Reemplazar por el personal real
                var resultadoActualizacionContactos = unitOfWork.WhatsAppMensajePublicidadRepository.ActualizarContactosConPrimerPreprocesamientoCampaniaGeneral(PreprocesamientoWhatsAppCampaniaGeneral);

                var pEspecificoObtenido = unitOfWork.PEspecificoRepository.FirstBy(x => x.IdCentroCosto == campaniaGeneralDetalle.IdCentroCosto);
                var listaPrimerProcesado = unitOfWork.WhatsAppMensajePublicidadRepository.ObtenerListaWhatsAppPrimerProcesadoCampaniaGeneral(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle);
                bool resultadoInsercion = true;

                if (listaPrimerProcesado.Any())
                {
                    if (pEspecificoObtenido == null)
                        return new { error = true, mensaje = "Ocurrio un error al intentar enlazar el programa general" };

                    listaPrimerProcesado = ReemplazarEtiquetaCampaniaGeneral(listaPrimerProcesado, campaniaGeneral.IdPlantillaWhatsapp.Value, pEspecificoObtenido.IdProgramaGeneral.GetValueOrDefault(), campaniaGeneralDetalle.Id);

                    resultadoInsercion = unitOfWork.WhatsAppConfiguracionPreEnvioRepository.RegistraPreValidacionCampaniaGeneral(listaPrimerProcesado, pEspecificoObtenido.IdProgramaGeneral.GetValueOrDefault(), campaniaGeneral.IdPlantillaWhatsapp.Value);
                    unitOfWork.Commit();
                    // Hora de fin del procesamiento
                    DateTime horaFin = DateTime.Now;

                    string usuarioResponsable = unitOfWork.IntegraAspNetUserRepository.ObtenerEmailPorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario);

                    if (unitOfWork.IntegraAspNetUserRepository.ExistePorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario))
                    {
                        try
                        {
                            unitOfWork.IntegraAspNetUserRepository.ObtenerEmailPorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    List<string> copiaCorreos = new List<string>
                    {
                        "gmiranda@bsginstitute.com"
                    };
                    TMK_MailService mailservicePersonalizado = new TMK_MailService();
                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = "gmiranda@bsginstitute.com",
                        Recipient = usuarioResponsable,
                        Subject = string.Concat("Preparar WhatsApp prioridades Mailing - Correcto ", campaniaGeneral.Nombre),
                        Message = GenerarPlantillaNotificacionFinalizacionWhatsapp(campaniaGeneral.Nombre, campaniaGeneralDetalle.Nombre, listaPrimerProcesado.Count(), horaInicio, horaFin),
                        Cc = string.Empty,
                        Bcc = string.Join(",", copiaCorreos),
                        AttachedFiles = null
                    };
                    mailservicePersonalizado.SetData(mailDataPersonalizado);
                    mailservicePersonalizado.SendMessageTask();
                }

                try
                {
                    unitOfWork.CampaniaGeneralDetalleRepository.ActualizarEstadoEjecucionCampaniaGeneralDetalle(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle, false, PreprocesamientoWhatsAppCampaniaGeneral.Usuario);
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                }

                return new { error = false, mensaje = resultadoInsercion };
            }
            catch (Exception ex)
            {
                var campaniaGeneralDetalle = unitOfWork.CampaniaGeneralDetalleRepository.BuscarCampaniaGeneralDetallePorId(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle);

                unitOfWork.CampaniaGeneralDetalleRepository.ActualizarEstadoEjecucionCampaniaGeneralDetalle(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle, false, PreprocesamientoWhatsAppCampaniaGeneral.Usuario);
                unitOfWork.Commit();
                List<string> copiaCorreos = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                string usuarioResponsable = unitOfWork.IntegraAspNetUserRepository.ObtenerEmailPorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario);

                if (unitOfWork.IntegraAspNetUserRepository.ExistePorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario))
                {
                    try
                    {
                        unitOfWork.IntegraAspNetUserRepository.ObtenerEmailPorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario);
                    }
                    catch (Exception)
                    {
                    }
                }

                TMK_MailService mailservice = new TMK_MailService();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = string.Concat("Finalizacion de Procesamiento WhatsApp Campania General - Error ", campaniaGeneralDetalle.Nombre),
                    Message = string.Concat("Mensaje: ", JsonConvert.SerializeObject(ex)),
                    Cc = string.Empty,
                    Bcc = string.Join(",", copiaCorreos),
                    AttachedFiles = null
                };
                mailservice.SetData(mailData);
                mailservice.SendMessageTask();

                return new
                {
                    error = true,
                    mensaje =ex.Message
                };
            }
        }

        /// <summary>
        /// Obtiene el html para enviar el mesaje correcto
        /// </summary>
        /// <param name="nombreCampaniaGeneral">Nombre de la campania general</param>
        /// <param name="nombreCampaniaGeneralDetalle">Nombre del detalle de la campania general</param>
        /// <param name="cantidadWhatsApp">Cantidad de datos de WhatsApp</param>
        /// <param name="horaInicio">Hora de inicio del procesamiento</param>
        /// <param name="horaFin">Hora de fin del procesamiento</param>
        /// <returns>String</returns>
        public string GenerarPlantillaNotificacionFinalizacionWhatsapp(string nombreCampaniaGeneral, string nombreCampaniaGeneralDetalle, int cantidadWhatsApp, DateTime horaInicio, DateTime horaFin)
        {

            string texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
                <tr style='padding: 5px;'>
                    <td>
                        <div style = 'background-color: #224060; padding: 25px 100px;'>
                            <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
                        </div>
                    </td>
                </tr>
                <tr><td><br /></td></tr>
                <tr style='text-align: center;'>
                       <td>Ha finalizado correctamente la preparación de los datos de WhatsApp:</td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>CAMPAÑA:</h3>
                        <h3>{nombreCampaniaGeneral}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>PRIORIDAD:</h3>
                        <h3>{nombreCampaniaGeneralDetalle}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE INICIO:</h3>
                        <h3>{horaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE FIN:</h3>
                        <h3>{horaFin}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3>Se ha preparado <span style='color: #4A92DB;'>{cantidadWhatsApp}</span> contactos WhatsApp</h3>
                        <h3>(Configurados en el módulo)</h3>
                    </td>
                </tr>
            </table>";

            return texto;
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos 
        /// Fecha: 21-03-2023
        /// Descipcion:realzia el reemplazo de las etiquetas necesarias para el envio de campanias
        /// </summary>
        /// <param name="NumeroAlumno"></param>
        /// <param name="IdPlantilla"></param>
        /// <param name="IdPGeneral"></param>
        /// <param name="IdCampaniaGeneralDetalle"></param>
        /// <returns></returns>
        public List<WhatsAppResultadoCampaniaGeneralDTO> ReemplazarEtiquetaCampaniaGeneral(List<WhatsAppResultadoCampaniaGeneralDTO> NumeroAlumno, int IdPlantilla, int IdPGeneral, int IdCampaniaGeneralDetalle)
        {
            string valor = string.Empty;
            string plantillaBaseGeneral = string.Empty;

            try
            {
                var rpta = unitOfWork.CentroCostoRepository.ObtenerRemplazoPlantilla(IdPGeneral);
                plantillaBaseGeneral = unitOfWork.PlantillaClaveValorRepository.GetBy(x => x.IdPlantilla == IdPlantilla && x.Clave == "Texto", x => new { x.Valor }).FirstOrDefault().Valor;
                var listaPersonal = unitOfWork.PersonalRepository.GetBy(x => NumeroAlumno.Select(s => s.IdPersonal).Contains(x.Id)).ToList();
                var alumnos = unitOfWork.CampaniaGeneralDetalleRepository.ObtenerAlumnosPorCampaniaGeneralDetalle(IdCampaniaGeneralDetalle);

                foreach (var alumnoEtiqueta in NumeroAlumno)
                {
                    string plantillaBase = plantillaBaseGeneral;

                    if (alumnoEtiqueta.Validado)
                    {
                        try
                        {
                            var personal = listaPersonal.FirstOrDefault(x => x.Id == alumnoEtiqueta.IdPersonal);
                            alumnoEtiqueta.ListaObjetoPlantilla = new List<DatoPlantillaWhatsAppDTO>();

                            if (alumnoEtiqueta.Celular.StartsWith("51"))
                                alumnoEtiqueta.Celular = alumnoEtiqueta.Celular.Substring(2, 9);
                            else if (alumnoEtiqueta.Celular.StartsWith("57"))
                                alumnoEtiqueta.Celular = "00" + alumnoEtiqueta.Celular;
                            else if (alumnoEtiqueta.Celular.StartsWith("591"))
                                alumnoEtiqueta.Celular = "00" + alumnoEtiqueta.Celular;

                            var alumno = alumnos.FirstOrDefault(x => x.Id == alumnoEtiqueta.IdAlumno);

                            if (alumno != null)
                            {
                                var fechaInicioPrograma = new ModalidadProgramaDTO();
                                var fecha = new List<ModalidadProgramaDTO>();

                                if (plantillaBase.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || plantillaBase.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || plantillaBase.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
                                {
                                    fecha = unitOfWork.PGeneralRepository.ObtenerFechaInicioProgramaGeneral(IdPGeneral, alumnoEtiqueta.IdCodigoPais);

                                    if (fecha.Any())
                                    {
                                        fechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL")).OrderBy(w => w.FechaReal).FirstOrDefault();

                                        if (fechaInicioPrograma == null)
                                            fechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                    }
                                }

                                foreach (string word in plantillaBase.Split('{'))
                                {
                                    DatoPlantillaWhatsAppDTO PlantillaEtiqueValor = new DatoPlantillaWhatsAppDTO();
                                    if (word.Contains('}'))
                                    {
                                        string Etiqueta = word.Split('}')[0];
                                        //Separamos solo los Id's
                                        if (Etiqueta.Contains("tPartner.nombre"))
                                            valor = rpta.NombrePartner;
                                        else if (Etiqueta.Contains("tPEspecifico.nombre"))
                                            valor = rpta.NombrePEspecifico;
                                        else if (Etiqueta.Contains("tPLA_PGeneral.Nombre"))
                                            valor = rpta.NombrePGeneral;

                                        if (Etiqueta.Contains("T_Pespecifico.NombreDiaSemanaFechaInicioPrograma"))
                                        {
                                            if (fecha.Any())
                                            {
                                                CultureInfo ci = new CultureInfo("es-ES");
                                                DateTime FechaInicioetiqueta = new DateTime();
                                                FechaInicioetiqueta = fechaInicioPrograma.FechaReal.Value;

                                                valor = ci.DateTimeFormat.GetDayName(FechaInicioetiqueta.DayOfWeek);
                                                valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                            }
                                            else
                                                valor = string.Empty;
                                        }
                                        else if (Etiqueta.Contains("T_Pespecifico.DiaFechaInicioPrograma"))
                                        {
                                            if (fecha.Any())
                                            {
                                                DateTime FechaInicioetiqueta = new DateTime();
                                                FechaInicioetiqueta = fechaInicioPrograma.FechaReal.Value;

                                                valor = FechaInicioetiqueta.Day.ToString();
                                            }
                                            else
                                                valor = string.Empty;
                                        }
                                        else if (Etiqueta.Contains("T_Pespecifico.NombreMesFechaInicioPrograma"))
                                        {
                                            if (fecha.Any())
                                            {
                                                DateTime FechaInicioetiqueta = new DateTime();
                                                FechaInicioetiqueta = fechaInicioPrograma.FechaReal.Value;

                                                valor = FechaInicioetiqueta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                                                valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                            }
                                            else
                                                valor = string.Empty;
                                        }
                                        if (Etiqueta.Contains("Template"))
                                            valor = string.Empty;
                                        else
                                        {
                                            if ((Etiqueta == "tPersonal.email" || Etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || Etiqueta == "tPersonal.nombres" || Etiqueta == "tPersonal.apellidos" || Etiqueta == "tPersonal.UrlFirmaCorreos" || Etiqueta == "tPersonal.Telefono" || Etiqueta == "tAlumnos.apepaterno" || Etiqueta == "tAlumnos.apematerno" || Etiqueta == "tAlumnos.nombre1" || Etiqueta == "tAlumnos.nombre2") && alumnoEtiqueta.IdPersonal > 0)
                                            {
                                                switch (Etiqueta)
                                                {
                                                    case "tPersonal.PrimerNombreApellidoPaterno":
                                                        valor = personal.Nombres + personal.ApellidoPaterno; break;
                                                    case "tPersonal.email":
                                                        valor = personal.Email; break;
                                                    case "tPersonal.nombres":
                                                        valor = personal.Nombres; break;
                                                    case "tPersonal.apellidos":
                                                        valor = personal.Apellidos; break;
                                                    case "tPersonal.Telefono":
                                                        {
                                                            if (!string.IsNullOrEmpty(personal.MovilReferencia))
                                                                valor = personal.MovilReferencia;
                                                            else
                                                            {
                                                                if (personal.Central == "192.168.0.20" || personal.Central == "192.168.2.20") 
                                                                {
                                                                    //Arequipa //lima
                                                                    valor = "(51) 1 207 2770 - Anexo " + personal.Anexo3Cx;
                                                                }
                                                                else if (personal.Central == "192.168.3.20")
                                                                {
                                                                    //bogota
                                                                    valor = "57 (601) 381 9462 - Anexo " + personal.Anexo3Cx;
                                                                }
                                                                else if (personal.Central == "192.168.4.20")
                                                                {
                                                                    //cd mexico
                                                                    valor = "52 (55) 4000 3255 - Anexo " + personal.Anexo3Cx;
                                                                }
                                                                else if (personal.Central == "192.168.5.20")
                                                                {
                                                                    //santiago
                                                                    valor = "56 (2) 2760 9120 - Anexo " + personal.Anexo3Cx;
                                                                }
                                                                else
                                                                {
                                                                    valor = "No registra central asignada";
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    case "tAlumnos.apepaterno":
                                                        {
                                                            if (alumno != null)
                                                                valor = alumno.ApellidoPaterno;
                                                        }
                                                        break;
                                                    case "tAlumnos.apematerno":
                                                        {
                                                            if (alumno != null)
                                                                valor = alumno.ApellidoMaterno;
                                                        }
                                                        break;
                                                    case "tAlumnos.nombre1":
                                                        {
                                                            if (alumno != null)
                                                                valor = alumno.Nombre1;
                                                        }
                                                        break;
                                                    case "tAlumnos.nombre2":
                                                        {
                                                            if (alumno != null)
                                                                valor = alumno.Nombre2;
                                                        }
                                                        break;
                                                    default:
                                                        valor = string.Empty; break;
                                                }

                                            }
                                        }
                                        if (valor != null)
                                        {
                                            valor = valor.Replace("#$%", "<br>");
                                            plantillaBase = plantillaBase.Replace("{" + Etiqueta + "}", valor);

                                            PlantillaEtiqueValor.Codigo = "{ " + Etiqueta + "}";
                                            PlantillaEtiqueValor.Texto = valor;

                                        }
                                        else
                                        {
                                            plantillaBase = plantillaBase.Replace("{" + Etiqueta + "}", "");

                                            PlantillaEtiqueValor.Codigo = "{ " + Etiqueta + "}";
                                            PlantillaEtiqueValor.Texto = "";
                                        }
                                        alumnoEtiqueta.ListaObjetoPlantilla.Add(PlantillaEtiqueValor);
                                    }
                                }
                                alumnoEtiqueta.Plantilla = plantillaBase;
                            }
                        }
                        catch (Exception ex)
                        {
                            List<string> correos = new List<string>();
                            correos.Add("gmiranda@bsginstitute.com");

                            TMK_MailService mailService = new TMK_MailService();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "fvaldez@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error Proceso Plantillas Campania General";
                            mailData.Message = "Alumno: " + alumnoEtiqueta.IdAlumno.ToString() + "<br/>" + ex.Message + " <br/>Mensaje:<br/> " + ex.ToString();
                            mailData.Cc = string.Empty;
                            mailData.Bcc = string.Empty;
                            mailData.AttachedFiles = null;

                            mailService.SetData(mailData);
                            mailService.SendMessageTask();
                        }
                    }
                    else
                        alumnoEtiqueta.Plantilla = plantillaBase;
                }

                return NumeroAlumno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
