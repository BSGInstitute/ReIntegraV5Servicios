using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp.GeneracionDeDataParaConfiguracionPreEnvio;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp
{
    public class CampaniaWhatsAppService : ICampaniaWhatsAppService
    {
        private readonly IUnitOfWork unitOfWork;
        private RespuestaGenerica respuestaGenerica;
        private List<TPgeneral> Pgeneral = new List<TPgeneral>();
        public  CampaniaWhatsAppService (IUnitOfWork unitOfWork){
            this.unitOfWork = unitOfWork;
        }
        public RespuestaGenerica ObtenerPrioridadesDeFiltroWpp(int idCampaniaGeneral, int idCampaniaGeneralDetalle)
        {
            try
            {
                respuestaGenerica.SendingblueRespuesta=JsonConvert.SerializeObject(unitOfWork.ConfiguracionDeEnvioParaWhatsAppRepository.ObtenerPrioridadesDeFiltroWpp(idCampaniaGeneral, idCampaniaGeneralDetalle));
                respuestaGenerica.error = new ErrorGenerico
                {
                    Response = false
                };   
                return respuestaGenerica;
            }catch(Exception e)
            {
                respuestaGenerica.error = new ErrorGenerico
                {
                    Response = true,
                    Detalle = new DetailError
                    {
                        Codigo = "CWP-EX001",
                        Descripcion = "Este error fue generado en la funcion ObtenerPrioridadesDeFiltroWpp, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message,
                        Mensaje = "No se pudo obtener las prioridades del filtro wpp"
                    }
                };
                return respuestaGenerica;
            }
        }
        public bool GenerarDataParaWhatsAppConfiguracionPreEnvio(int idCampaniaGeneral)
        {
            try
            {
                //var dataBase = unitOfWork.ConfiguracionDeEnvioParaWhatsAppRepository.ObtenerDataParaGenerarWhatsAppConfiguracionPreenvio(idCampaniaGeneral);
                //var DataFiltrada = unitOfWork.campaniaWhatsappFiltradoRepository.FiltradoDeDatosParaWhatsappObtenerAllData(idCampaniaGeneral);
                //var DataWpublicidad = unitOfWork.WhatsAppMensajePublicidadRepository.ObtenerTodosLosmenajesPorIdCampaniaGeneral(idCampaniaGeneral).ToList();
                //Pgeneral = unitOfWork.PGeneralRepository.GetBy(x => x.Estado == true).ToList();

                //List<TWhatsAppConfiguracionPreEnvio> Preenvio = new List<TWhatsAppConfiguracionPreEnvio>();
                //foreach(var i in DataFiltrada)
                //{
                //   var dat = ReemplazarPlantilla(dataBase, i);
                //}


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //public string ReemplazarPlantilla(List<ObtenerGeneracionDeDataParaConfiguracionPreEnvio> data,TFiltradoDeDatosPorPrioridadWhatsApp filtrado)
        //{
        //    var datas = data.FirstOrDefault(x=>x.IdCampaniaGeneralDetalle == filtrado.IdCampaniaGeneralDetalle);
        //    //logica asunto
        //    if (datas.Valor.Contains("{tPLA_PGeneral.Nombre}"))
        //    {
        //        var valor = Pgeneral.FirstOrDefault(x=>x.Id==filtrado.IdProgramaGeneral).Nombre;
        //        datas.Valor.Replace("{tPLA_PGeneral.Nombre}", valor);
        //    }

        //    if (datas.Valor.Contains("{tpla_pgeneral.pw_duracion}"))
        //    {
        //        var valor = Pgeneral.FirstOrDefault(x => x.Id == filtrado.IdProgramaGeneral).PwDuracion;
        //        datas.Valor.Replace("{tpla_pgeneral.pw_duracion}", valor);
        //    }
        //    //dede aqui 
        //    string plantilla = string.Empty;
        //    string valor = string.Empty;
        //    string Numero = "";
        //    //PlantillaPwBO plantillaPw = new PlantillaPwBO();

        //    foreach (var AlumnoEtiqueta in NumeroAlumno)
        //    {
        //        try
        //        {
        //            AlumnoEtiqueta.objetoplantilla = new List<datoPlantillaWhatsApp>();

        //            Numero = AlumnoEtiqueta.Celular;
        //            if (Numero.StartsWith("51"))
        //            {
        //                Numero = Numero.Substring(2, 9);
        //            }
        //            else if (Numero.StartsWith("57"))
        //            {
        //                Numero = "00" + Numero;
        //            }
        //            else if (Numero.StartsWith("591"))
        //            {
        //                Numero = "00" + Numero;
        //            }
        //            else
        //            {

        //            }
        //            var Alumno = _repAlumno.FirstBy(w => w.Id == AlumnoEtiqueta.IdAlumno, y => new { y.Nombre1, y.Nombre2, y.ApellidoMaterno, y.ApellidoPaterno });
        //            //var Asesor = _repPersonal.FirstBy(w => w.Id == IdPersonal, y => new { y.Nombres, y.Apellidos, y.Anexo3Cx, y.Central, y.MovilReferencia });
        //            var Asesor = _repPersonal.ObtenerDatoPersonal(IdPersonal);



        //            plantilla = _repPlantillaClaveValor.GetBy(w => w.Estado == true && w.IdPlantilla == IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;

        //            PlantillaCentroCostoDTO rpta = new PlantillaCentroCostoDTO();
        //            ModalidadProgramaDTO FechaInicioPrograma = new ModalidadProgramaDTO();
        //            List<ModalidadProgramaDTO> fecha = new List<ModalidadProgramaDTO>();
        //            foreach (var item in ProgramaPrincipal)
        //            {
        //                rpta = _repCentroCosto.ObtenerRemplazoPlantilla(item.IdPgeneral);
        //                if (plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
        //                {
        //                    fecha = _repPgeneral.ObtenerFechaInicioProgramaGeneral(item.IdPgeneral, AlumnoEtiqueta.IdCodigoPais);

        //                    if (fecha.Count > 0)
        //                    {
        //                        FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL")).OrderBy(w => w.FechaReal).FirstOrDefault();
        //                        if (FechaInicioPrograma == null)
        //                        {
        //                            FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
        //                        }
        //                    }
        //                }
        //                //plantillaPw.ObtenerFechaInicioPrograma(item.IdPgeneral, rpta.IdCentroCosto);
        //            }


        //            foreach (string word in plantilla.Split('{'))
        //            {
        //                datoPlantillaWhatsApp plantillaEtiqueValor = new datoPlantillaWhatsApp();
        //                if (word.Contains('}'))
        //                {
        //                    string etiqueta = word.Split('}')[0];
        //                    //Separamos solo los Id´s

        //                    if (etiqueta.Contains("tPartner.nombre"))
        //                    {

        //                        valor = rpta.NombrePartner;
        //                    }
        //                    else if (etiqueta.Contains("tPEspecifico.nombre"))
        //                    {
        //                        valor = rpta.NombrePEspecifico;
        //                    }
        //                    else if (etiqueta.Contains("tPLA_PGeneral.Nombre"))
        //                    {
        //                        valor = rpta.NombrePgeneral;
        //                    }

        //                    if (etiqueta.Contains("T_Pespecifico.NombreDiaSemanaFechaInicioPrograma"))
        //                    {
        //                        if (fecha.Count != 0)
        //                        {
        //                            CultureInfo ci = new CultureInfo("es-ES");
        //                            DateTime FechaInicioetiqueta = new DateTime();
        //                            FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

        //                            valor = ci.DateTimeFormat.GetDayName(FechaInicioetiqueta.DayOfWeek);
        //                            valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
        //                        }
        //                        else
        //                        {
        //                            valor = "";
        //                        }
        //                    }
        //                    else if (etiqueta.Contains("T_Pespecifico.DiaFechaInicioPrograma"))
        //                    {
        //                        if (fecha.Count != 0)
        //                        {
        //                            DateTime FechaInicioetiqueta = new DateTime();
        //                            FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

        //                            valor = FechaInicioetiqueta.Day.ToString();
        //                        }
        //                        else
        //                        {
        //                            valor = "";
        //                        }
        //                    }
        //                    else if (etiqueta.Contains("T_Pespecifico.NombreMesFechaInicioPrograma"))
        //                    {
        //                        if (fecha.Count != 0)
        //                        {
        //                            //CultureInfo ci = new CultureInfo("es-Es");
        //                            DateTime FechaInicioetiqueta = new DateTime();
        //                            FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

        //                            valor = FechaInicioetiqueta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
        //                            valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
        //                        }
        //                        else
        //                        {
        //                            valor = "";
        //                        }
        //                    }
        //                    if (etiqueta.Contains("Template"))
        //                    {

        //                        valor = "";
        //                    }
        //                    else
        //                    {

        //                        if ((etiqueta == "tPersonal.email" || etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || etiqueta == "tPersonal.nombres" || etiqueta == "tPersonal.apellidos" || etiqueta == "tPersonal.UrlFirmaCorreos" || etiqueta == "tPersonal.Telefono" || etiqueta == "tAlumnos.apepaterno" || etiqueta == "tAlumnos.apematerno" || etiqueta == "tAlumnos.nombre1" || etiqueta == "tAlumnos.nombre2") && IdPersonal > 0)
        //                        {
        //                            switch (etiqueta)
        //                            {
        //                                case "tPersonal.PrimerNombreApellidoPaterno":
        //                                    valor = Asesor.PrimerNombreApellidoPaterno; break;
        //                                case "tPersonal.email":
        //                                    valor = Asesor.Email; break;
        //                                case "tPersonal.nombres":
        //                                    valor = Asesor.Nombres; break;
        //                                case "tPersonal.apellidos":
        //                                    valor = Asesor.Apellidos; break;
        //                                case "tPersonal.Telefono":
        //                                    {
        //                                        if (!string.IsNullOrEmpty(Asesor.MovilReferencia))
        //                                        {
        //                                            valor = Asesor.MovilReferencia;
        //                                        }
        //                                        else
        //                                        {
        //                                            if (Asesor.Central == "192.168.0.20")
        //                                            {
        //                                                //aqp
        //                                                valor = "(51) 54 258787 - Anexo " + Asesor.Anexo3Cx;
        //                                            }
        //                                            else
        //                                            {
        //                                                if (Asesor.Central == "192.168.2.20")
        //                                                {
        //                                                    //lima
        //                                                    valor = "(51) 1 207 2770 - Anexo " + Asesor.Anexo3Cx;
        //                                                }
        //                                                else
        //                                                {
        //                                                    valor = "(51) 54 258787";
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                    break;
        //                                case "tAlumnos.apepaterno":
        //                                    {
        //                                        if (Alumno != null)
        //                                        {
        //                                            valor = Alumno.ApellidoPaterno;
        //                                        }
        //                                    }
        //                                    break;
        //                                case "tAlumnos.apematerno":
        //                                    {
        //                                        if (Alumno != null)
        //                                        {
        //                                            valor = Alumno.ApellidoMaterno;
        //                                        }
        //                                    }
        //                                    break;
        //                                case "tAlumnos.nombre1":
        //                                    {
        //                                        if (Alumno != null)
        //                                        {
        //                                            valor = Alumno.Nombre1;
        //                                        }
        //                                    }
        //                                    break;
        //                                case "tAlumnos.nombre2":
        //                                    {
        //                                        if (Alumno != null)
        //                                        {
        //                                            valor = Alumno.Nombre2;
        //                                        }
        //                                    }
        //                                    break;
        //                                default:
        //                                    valor = ""; break;
        //                            }

        //                        }
        //                    }
        //                    if (valor != null)
        //                    {
        //                        valor = valor.Replace("#$%", "<br>");
        //                        plantilla = plantilla.Replace("{" + etiqueta + "}", valor);

        //                        plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
        //                        plantillaEtiqueValor.texto = valor;

        //                    }
        //                    else
        //                    {
        //                        plantilla = plantilla.Replace("{" + etiqueta + "}", "");

        //                        plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
        //                        plantillaEtiqueValor.texto = "";
        //                    }
        //                    AlumnoEtiqueta.objetoplantilla.Add(plantillaEtiqueValor);
        //                }
        //            }
        //            AlumnoEtiqueta.Plantilla = plantilla;
        //            //return Ok(new { plantilla, objetoplantilla });
        //        }
        //        catch (Exception ex)
        //        {
        //            List<string> correos = new List<string>();
        //            correos.Add("fvaldez@bsginstitute.com");

        //            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

        //            TMKMailDataDTO mailData = new TMKMailDataDTO();
        //            mailData.Sender = "fvaldez@bsginstitute.com";
        //            mailData.Recipient = string.Join(",", correos);
        //            mailData.Subject = "Error Proceso Plantillas";
        //            mailData.Message = "Alumno: " + AlumnoEtiqueta.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + AlumnoEtiqueta.IdConjuntoListaResultado.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
        //            mailData.Cc = "";
        //            mailData.Bcc = "";
        //            mailData.AttachedFiles = null;

        //            Mailservice.SetData(mailData);
        //            Mailservice.SendMessageTask();
        //        }
        //    }
        //    //hasta aqui
        //}
    }
}
