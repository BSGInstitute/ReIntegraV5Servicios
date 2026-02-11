using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Clases;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using PdfSharp.Drawing;
using QRCoder;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using QRCode = QRCoder.QRCode;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DocumentoService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de Documentos
    /// </summary>
    public class DocumentoService : IDocumentoService
    {
        private IUnitOfWork _unitOfWork;
        private List<AlertDTO> ListadoAlertas;
        public DocumentoDTO documentoObjDTO = new DocumentoDTO();
        public CertificadoGeneradoAutomaticoContenido ContenidoCertificado = new CertificadoGeneradoAutomaticoContenido();
        public DocumentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Metodo que retorna lista de secciones de documento del programa general apuntando a la nueva version
        /// </summary>
        /// <param name="idPGeneral">Id del programa general del cual se desea obtener las secciones del documento (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> List<ProgramaGeneralSeccionDocumentoDTO> </returns>
        public List<ProgramaGeneralSeccionDocumentoDTO> ObtenerListaSeccionDocumentoProgramaGeneral(int idPGeneral)
        {
            try
            {
                ProgramaGeneralDoumentoDTO programa = new ProgramaGeneralDoumentoDTO();
                var servicioDocumentoSeccionPw = new DocumentoSeccionPwService(_unitOfWork);

                var listaCursos = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijos(idPGeneral);
                if (listaCursos.Count > 0)
                {
                    programa.EsProgramaPadre = true;
                    var secciones = servicioDocumentoSeccionPw.ObtenerSeccionDocumento(idPGeneral);
                    if (secciones != null && secciones.Count > 0)
                    {
                        programa.ListaSeccionesContenidosDocumento = secciones;
                    }
                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();

                    foreach (var item in listaCursos)
                    {
                        var seccionEstructura = servicioDocumentoSeccionPw.ObtenerSeccionDocumentoEstructuraCurricular(item.Id);

                        if (seccionEstructura != null && seccionEstructura.Count > 0)
                        {
                            programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                        }
                    }
                }
                else
                {
                    programa.EsProgramaPadre = false;

                    var secciones = servicioDocumentoSeccionPw.ObtenerSeccionDocumento(idPGeneral);

                    if (secciones != null && secciones.Count > 0)
                    {
                        programa.ListaSeccionesContenidosDocumento = secciones;
                    }

                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();


                    var seccionEstructura = servicioDocumentoSeccionPw.ObtenerSeccionDocumentoEstructuraCurricular(idPGeneral);

                    if (seccionEstructura != null && seccionEstructura.Count > 0)
                    {
                        programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                    }
                }

                List<ProgramaGeneralEstructuraAgrupadoDTO> contenido = new List<ProgramaGeneralEstructuraAgrupadoDTO>();
                if (programa.ListaSeccionesContenidosDocumentoEstructura != null && programa.ListaSeccionesContenidosDocumentoEstructura.Count > 0)
                {
                    if (programa.EsProgramaPadre)
                    {
                        var listaCursosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.GroupBy(x => new { x.IdPGeneral, x.NombreCurso, x.Titulo }).Select(x => new { x.Key.IdPGeneral, x.Key.NombreCurso, x.Key.Titulo }).ToList();
                        foreach (var itemCurso in listaCursosSecciones)
                        {
                            var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12 && x.IdPGeneral == itemCurso.IdPGeneral).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.IdPGeneral, x.Cabecera, x.PiePagina }).ToList();
                            ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                            obj.Seccion = itemCurso.Titulo;
                            obj.Titulo = itemCurso.NombreCurso;
                            obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();
                            foreach (var itemSecion in listaCapitulosSecciones)
                            {
                                ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                {
                                    Contenido = itemSecion.Key.Contenido,
                                    Cabecera = itemSecion.Key.Cabecera,
                                    PiePagina = itemSecion.Key.PiePagina
                                };
                                obj.DetalleContenido.Add(tmp);

                            }
                            contenido.Add(obj);
                        }
                    }
                    else
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.Titulo }).ToList();
                        foreach (var item in listaCapitulosSecciones)
                        {
                            ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                            var listaSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.Contenido == item.Key.Contenido).Select(x => x.NumeroFila).ToList();
                            var capituloSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.IdSeccionTipoDetalle_PW == 13 && listaSesiones.Contains(s.NumeroFila)).GroupBy(x => new { x.Contenido, x.NombreCurso, x.Cabecera, x.PiePagina }).ToList();
                            obj.Seccion = item.Key.Titulo;
                            obj.Titulo = item.Key.Contenido;
                            obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();

                            if (capituloSesiones != null && capituloSesiones.Count > 0)
                            {
                                foreach (var itemSecion in capituloSesiones)
                                {
                                    ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                    {
                                        Contenido = itemSecion.Key.Contenido,
                                        Cabecera = itemSecion.Key.Cabecera,
                                        PiePagina = itemSecion.Key.PiePagina
                                    };
                                    obj.DetalleContenido.Add(tmp);
                                }
                                contenido.Add(obj);
                            }
                            else
                            {
                                foreach (var itemSecion in capituloSesiones)
                                {
                                    ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                    {
                                        Contenido = item.Key.Contenido
                                    };
                                    obj.DetalleContenido.Add(tmp);
                                }
                                contenido.Add(obj);
                            }
                        }
                    }
                }
                if (programa.ListaSeccionesContenidosDocumento != null && programa.ListaSeccionesContenidosDocumento.Count > 0)
                {
                    var listaCursosSecciones = programa.ListaSeccionesContenidosDocumento.GroupBy(x => new { x.IdSeccionTipoDetalle_PW, x.Titulo }).Select(x => new { x.Key.IdSeccionTipoDetalle_PW, x.Key.Titulo }).ToList();
                    foreach (var itemCurso in listaCursosSecciones)
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumento.Where(x => x.IdSeccionTipoDetalle_PW == itemCurso.IdSeccionTipoDetalle_PW).GroupBy(x => new { x.Contenido, x.Cabecera, x.PiePagina }).ToList();
                        ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                        obj.Seccion = itemCurso.Titulo;
                        obj.Titulo = "";//itemCurso.Titulo;
                        obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();
                        foreach (var itemSecion in listaCapitulosSecciones)
                        {
                            ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                            {
                                Contenido = itemSecion.Key.Contenido,
                                Cabecera = itemSecion.Key.Cabecera,
                                PiePagina = itemSecion.Key.PiePagina
                            };
                            obj.DetalleContenido.Add(tmp);
                        }
                        contenido.Add(obj);
                    }
                }

                var listaProgramaGeneralDocumentoSeccion = contenido.GroupBy(x => x.Seccion).Select(x => new ProgramaGeneralSeccionDocumentoDTO
                {
                    Seccion = x.Key,
                    DetalleSeccion = x.GroupBy(y => new { y.Titulo, y.DetalleContenido }).Select(y => new ProgramaGeneralSeccionDocumentoDetalleDTO
                    {
                        Titulo = y.Key.Titulo,
                        Cabecera = y.Key.DetalleContenido.GroupBy(z => z.Cabecera).Select(z => z.Key).FirstOrDefault(),
                        PiePagina = y.Key.DetalleContenido.GroupBy(z => z.PiePagina).Select(z => z.Key).FirstOrDefault(),
                        DetalleContenido = y.Key.DetalleContenido.Select(z => z.Contenido).ToList()
                    }).ToList()
                }).ToList();

                return listaProgramaGeneralDocumentoSeccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Genera HTML a partir de la lista de secciones de documento del programa general
        /// </summary>
        /// <param name="listaProgramaGeneralDocumentoSeccion">Lista de objetos del tipo ProgramaGeneralSeccionDocumentoDTO</param>	
        /// <returns>Lista secciones documento: List<ProgramaGeneralSeccionAnexosHTMLDTO></returns> 
        public List<ProgramaGeneralSeccionAnexosHTMLDTO> GenerarHTMLProgramaGeneralDocumentoSeccion(List<ProgramaGeneralSeccionDocumentoDTO> listaProgramaGeneralDocumentoSeccion)
        {
            try
            {
                List<ProgramaGeneralSeccionAnexosHTMLDTO> lista = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
                foreach (var item in listaProgramaGeneralDocumentoSeccion)
                {
                    ProgramaGeneralSeccionAnexosHTMLDTO obj = new ProgramaGeneralSeccionAnexosHTMLDTO();
                    obj.Seccion = item.Seccion;
                    string contenido = "";
                    foreach (var detalleSeccion in item.DetalleSeccion)
                    {
                        contenido += "<h5><strong><b>" + detalleSeccion.Titulo + "</b></strong></h5>";
                        contenido += "<p>" + detalleSeccion.Cabecera + "</p>";
                        contenido += "<ul type='disc'>";
                        foreach (var contenidoSeccion in detalleSeccion.DetalleContenido)
                        {
                            contenido += "<li>&bull;&nbsp;&nbsp;&nbsp;" + contenidoSeccion + "</li>";
                        }
                        contenido += "</ul>";
                        contenido += "<p>" + detalleSeccion.PiePagina + "</p>";
                    }
                    obj.Contenido = contenido;
                    lista.Add(obj);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 28/11/2022
        /// Version: 1.0
        /// <summary>
        /// Descripcion: Genera vista previa del certificado.
        /// </summary>
        /// <param name="idPlantillaF"></param>
        /// <param name="idPlantillaP"></param>
        /// <param name="idOportunidad"></param>
        /// <param name="idplantillabase"></param>
        /// <param name="codigoCertificado"></param>
        /// <returns> byte[] </returns>
        public byte[] GenerarVistaPreviaCertificado(int idPlantillaF, int idPlantillaP, int idOportunidad, ref int idplantillabase, ref string codigoCertificado)
        {
            AlumnoService servicioAlumno = new AlumnoService(_unitOfWork);
            PersonalService servicioPersonal = new PersonalService(_unitOfWork);
            PGeneralService servicioPGeneral = new PGeneralService(_unitOfWork);
            PlantillaService servicioPlantilla = new PlantillaService(_unitOfWork);
            OportunidadService servicioOportunidad = new OportunidadService(_unitOfWork);
            PEspecificoService servicioPEspecifico = new PEspecificoService(_unitOfWork);
            MatriculaCabeceraService servicioMatriculaCabecera = new MatriculaCabeceraService(_unitOfWork);
            EsquemaEvaluacionService servicioEsquemaEvaluacion = new EsquemaEvaluacionService(_unitOfWork);
            DocumentoSeccionPwService servicioDocumentoSeccionPw = new DocumentoSeccionPwService(_unitOfWork);
            CertificadoGeneradoAutomaticoService servicioCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoService(_unitOfWork);
            OportunidadClasificacionOperacionesService servicioOportunidadClasificacionOperaciones = new OportunidadClasificacionOperacionesService(_unitOfWork);

            documentoObjDTO.contenidoCertificado = new CertificadoGeneradoAutomaticoContenido();
            int IdPlantillaBase = 0;
            var oportunidadClasificacionOperaciones = servicioOportunidadClasificacionOperaciones.ObtenerPorIdOportunidad(idOportunidad);
            var condicionMatriculaCabecera = servicioMatriculaCabecera.ObtenerPorIdMatriculaCabecera(oportunidadClasificacionOperaciones.IdMatriculaCabecera);
            if (condicionMatriculaCabecera == null)
            {
                throw new Exception("Matricula cabecera no valida!");
            }

            MatriculaCabeceraDatosCertificadoService servicioMatriculaCertificado = new MatriculaCabeceraDatosCertificadoService(_unitOfWork);
            MatriculaCabeceraDatosCertificado certificados = servicioMatriculaCertificado.ObtenerTotal(oportunidadClasificacionOperaciones.IdMatriculaCabecera);
            MatriculaCabeceraDatosCertificado nuevoCertificado = new MatriculaCabeceraDatosCertificado();

            var listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();
            var matriculaCabecera = servicioMatriculaCabecera.ObtenerPorIdMatriculaCabecera(oportunidadClasificacionOperaciones.IdMatriculaCabecera);
            var detalleMatriculaCabecera = servicioMatriculaCabecera.ObtenerDetalleMatricula(matriculaCabecera.Id);
            var plantilla = servicioPlantilla.ObtenerPorId(idPlantillaF);

            IdPlantillaBase = plantilla.IdPlantillaBase;
            idplantillabase = IdPlantillaBase;

            var plantillaBase = servicioPlantilla.ObtenerPlantillaCorreo(idPlantillaF);
            var alumno = servicioAlumno.ObtenerPorId(matriculaCabecera.IdAlumno.Value);

            var oportunidad = servicioOportunidad.ObtenerPorId(detalleMatriculaCabecera.IdOportunidad);
            var DatosCompuestosOportunidad = servicioOportunidad.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
            var personal = servicioPersonal.ObtenerPorId(oportunidad.IdPersonalAsignado.Value);

            var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

            foreach (var etiqueta in listaEtiqueta)
            {
                listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = "" });
            }
            List<string> sepacion = new List<string>();
            string FondoFrontalCertificado = "";
            string FondoReversoCertificado = "";
            string FondoFrontalConstancia = "";
            string FondoReversoConstancia = "";
            sepacion = plantillaBase.Cuerpo.Split("###").ToList();
            Dictionary<string, object> map = new Dictionary<string, object>();
            map["font_factory"] = new MyFontFactory();
            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                var config = new PdfGenerateConfig();
                if (IdPlantillaBase == 12)/*Certificado*/
                {
                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginTop = 144;
                    config.MarginBottom = 30;
                    config.MarginLeft = 111;
                    config.MarginRight = 83;
                }
                else
                {
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginLeft = 60;
                    config.MarginRight = 60;
                    config.MarginTop = 60;
                    config.MarginBottom = 25;
                }
                int Conteo = 0;
                List<string> estructura = new List<string>();
                string remplazo = "";
                foreach (var item in sepacion)
                {
                    string direccionCodigoQR = "";
                    string tipoletra = "Times New Roman";
                    remplazo = item;
                    string searchString = "/*";
                    int startIndex = remplazo.IndexOf(searchString);
                    searchString = "*/";
                    int endIndex = 0;
                    string substring = "";

                    if (startIndex != -1)
                    {
                        endIndex = remplazo.IndexOf(searchString, startIndex);
                        substring = remplazo.Substring(startIndex + searchString.Length, endIndex - searchString.Length);
                    }
                    if (substring != null && substring != "")
                    {
                        tipoletra = substring;
                    }
                    else
                    {
                        searchString = "text-align:";
                        startIndex = remplazo.IndexOf(searchString);
                        searchString = ";";

                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                    }
                    if (item.Contains("repositorioweb"))
                    {
                        if (IdPlantillaBase == 12)
                        {
                            FondoFrontalCertificado = item;
                        }
                        else
                        {
                            FondoFrontalConstancia = item;
                        }
                    }
                    if (item.Contains("acute"))
                    {
                        remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                    }
                    if (item.Contains("CODIGOQR"))
                    {
                        string url = "";
                        if (IdPlantillaBase == 12)/*Certificados*/
                        {
                            url = servicioAlumno.ObtenerCodigoCertificado(matriculaCabecera.Id);
                        }
                        else /*13:Constancia*/
                        {
                            url = servicioCertificadoGeneradoAutomatico.ObtenerCorrelativoCertificado().ToString();
                        }
                        var urlCodigo = "https://bsginstitute.com/informacionCertificado?cod=" + matriculaCabecera.Id + "." + url;
                        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                        QRCodeData qrDatos = qRCodeGenerator.CreateQrCode(urlCodigo, QRCodeGenerator.ECCLevel.Q);
                        QRCode qRCodigo = new QRCode(qrDatos);

                        System.Drawing.Bitmap qrImagen = qRCodigo.GetGraphic(7, System.Drawing.Color.Black, System.Drawing.Color.White, false);
                        System.Drawing.Image objImage = (System.Drawing.Image)qrImagen;

                        direccionCodigoQR = servicioAlumno.GuardarArchivosQR(ImageToByte2(objImage), "image/jpeg", url);
                        documentoObjDTO.contenidoCertificado.CodigoQr = direccionCodigoQR;
                        remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='{direccionCodigoQR}' width=55 height=55  />");
                    }
                    if (item.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        var nombrePrograma = "";
                        if (certificados == null)
                        {
                            nombrePrograma = detalleMatriculaCabecera.NombreProgramaGeneral.Replace(" - COLOMBIA", "").ToUpper();
                            nuevoCertificado.NombreCurso = nombrePrograma;
                        }
                        else
                        {
                            nombrePrograma = certificados.NombreCurso;
                        }
                        documentoObjDTO.contenidoCertificado.NombrePrograma = nombrePrograma;
                        if (nombrePrograma.ToUpper().StartsWith("CURSO") || nombrePrograma.ToUpper().StartsWith("PROGRAMA"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", "");
                        }
                        else
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", " programa");
                        }
                        remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", nombrePrograma);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                    {
                        var codigoPartner = servicioPGeneral.ObtenerCodigoPartner(matriculaCabecera.Id);
                        if (codigoPartner != null)
                        {
                            documentoObjDTO.contenidoCertificado.CodigoPartner = codigoPartner;
                            remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                        }
                        else
                        {
                            throw new Exception("No se puede Calcular CodigoPartner");
                        }
                    }
                    if (item.Contains("{T_Pgeneral.Pdus}"))
                    {
                        var pduCodigoPartner = servicioPGeneral.ObtenerPdu(matriculaCabecera.Id);
                        if (pduCodigoPartner != null)
                        {

                            documentoObjDTO.contenidoCertificado.PDUs = pduCodigoPartner;
                            remplazo = remplazo.Replace("{T_Pgeneral.Pdus}", pduCodigoPartner.ToString());

                        }
                        else
                        {
                            throw new Exception("No se puede Calcular Pdus");
                        }
                    }
                    //reemplazar nombre 1 alumno
                    if (item.Contains("{T_Alumno.NombreCompleto}"))
                    {
                    }
                    if (item.Contains("{tAlumnos.nombre1}"))
                    {
                        string nombreAlumno = "";
                        nombreAlumno = alumno.Nombre1.ToUpper();
                        //var parrafo = new iTextSharp.text.Phrase(alumno.NombreCompleto, fuente);
                        remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1.ToUpper());

                        if (item.Contains("{tAlumnos.nombre2}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.Nombre2.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2.ToUpper());
                            //documento.Add(new iTextSharp.text.Phrase(" " + alumno.Nombre2.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apepaterno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoPaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno.ToUpper());
                            //documento.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoPaterno.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apematerno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoMaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno.ToUpper());
                            //documento.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoMaterno.ToUpper(), fuente));
                        }
                        documentoObjDTO.contenidoCertificado.NombreAlumno = nombreAlumno;
                    }
                    if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                    {
                        string Duracion = DatosCompuestosOportunidad.PwDuracion;
                        remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", Duracion);
                    }
                    if (item.Contains("{tCentroCosto.centrocosto}"))
                    {
                        documentoObjDTO.contenidoCertificado.NombreCentroCosto = detalleMatriculaCabecera.NombreCentroCosto.ToUpper();
                        remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto.ToUpper());
                    }
                    if (item.Contains("{tPEspecifico.ciudad}"))
                    {
                        var PrimeraLetra = detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper();
                        var resto = detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower();
                        documentoObjDTO.contenidoCertificado.Ciudad = PrimeraLetra + resto;
                        if (documentoObjDTO.contenidoCertificado.Ciudad == null || documentoObjDTO.contenidoCertificado.Ciudad == "")
                        {
                            throw new Exception("No se Puede Calcular Ciudad");
                        }
                        remplazo = remplazo.Replace("{tPEspecifico.ciudad}", PrimeraLetra + resto);
                    }
                    if (item.Contains("{tPEspecifico.EscalaCalificacion}"))
                    {
                        var escalaCalificacion = detalleMatriculaCabecera.EscalaCalificacion;
                        documentoObjDTO.contenidoCertificado.EscalaCalificacion = escalaCalificacion;
                        if (escalaCalificacion == 0 || escalaCalificacion == null)
                        {
                            throw new Exception("No se Puede Calcular Escala Calificacion");
                        }
                        remplazo = remplazo.Replace("{tPEspecifico.EscalaCalificacion}", escalaCalificacion.ToString());
                    }
                    //reemplazar Fecha Inicio Capacitacion
                    if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                    {
                        var fechaInicioCapacitacion = "";
                        if (certificados == null)
                        {
                            fechaInicioCapacitacion = servicioAlumno.ObtenerFechaInicioCapacitacionPortalWeb(matriculaCabecera.Id);
                            try
                            {
                                nuevoCertificado.FechaInicio = servicioMatriculaCertificado.TransformarCadenaEnFecha(fechaInicioCapacitacion);
                            }
                            catch (Exception e)
                            {
                                nuevoCertificado.FechaInicio = DateTime.Now;
                            }
                        }
                        else
                        {
                            fechaInicioCapacitacion = servicioMatriculaCertificado.TransformarFechaEnCadena(certificados.FechaInicio);
                        }
                        if (fechaInicioCapacitacion == null || fechaInicioCapacitacion == "")
                        {
                            throw new Exception("No se Puede Calcular FechaInicioCapacitacion");
                        }
                        documentoObjDTO.contenidoCertificado.FechaInicioCapacitacion = fechaInicioCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", fechaInicioCapacitacion);
                    }
                    //reemplazar Fecha Fin Capacitacion
                    if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                    {
                        var fechaFinCapacitacion = "";
                        if (certificados == null)
                        {
                            fechaFinCapacitacion = servicioAlumno.ObtenerFechaFinCapacitacionPortalWeb(matriculaCabecera.Id);
                            nuevoCertificado.FechaFinal = servicioMatriculaCertificado.TransformarCadenaEnFecha(fechaFinCapacitacion);
                        }
                        else
                        {
                            fechaFinCapacitacion = servicioMatriculaCertificado.TransformarFechaEnCadena(certificados.FechaFinal);
                        }
                        if (fechaFinCapacitacion == null || fechaFinCapacitacion == "")
                        {
                            throw new Exception("No se Puede Calcular FehaFinCapacitacion");
                        }
                        documentoObjDTO.contenidoCertificado.FechaFinCapacitacion = fechaFinCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", fechaFinCapacitacion);
                    }
                    //reemplazar Calificacion Promedio
                    if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                    {
                        var calificacionPromedio = servicioAlumno.ObtenerNotaPromedio(matriculaCabecera.Id);
                        if (calificacionPromedio == null || calificacionPromedio == "")
                        {
                            throw new Exception("No se Puede Calcular Calificacion Promedio");
                        }
                        documentoObjDTO.contenidoCertificado.CalificacionPromedio = Convert.ToInt32(calificacionPromedio);
                        remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", (Convert.ToInt32(calificacionPromedio)).ToString());
                    }
                    //reemplazar Fecha Emision Certificado
                    if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                    {
                        var fechaEmision = servicioAlumno.ObtenerFechaEmision();
                        documentoObjDTO.contenidoCertificado.FechaEmisionCertificado = fechaEmision;
                        remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", fechaEmision);
                    }
                    //reemplazar Fecha Codigo Certificado
                    if (item.Contains("{T_Alumno.CodigoCertificado}"))
                    {
                        var CodigoCertificado = servicioAlumno.ObtenerCodigoCertificado(matriculaCabecera.Id);
                        if (CodigoCertificado == null || CodigoCertificado == "")
                        {
                            throw new Exception("No se Puede Calcular Codigo Certificado");
                        }
                        codigoCertificado = CodigoCertificado;
                        remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", CodigoCertificado);
                    }
                    //reemplazar duracion en horas de Programa Especifico
                    if (item.Contains("{tPEspecifico.duracion}"))
                    {
                        var duracionPespecifico = "";
                        if (certificados == null)
                        {
                            duracionPespecifico = _unitOfWork.EstructuraEspecificaRepository.ObtenerDuracionTotalPorIdMatriculaCabecera(matriculaCabecera.Id);
                            nuevoCertificado.Duracion = duracionPespecifico;
                        }
                        else
                        {
                            duracionPespecifico = certificados.Duracion.ToString();
                        }
                        if (duracionPespecifico == null || duracionPespecifico == "")
                        {
                            throw new Exception("No se Puede Calcular Duracion Pespecifico");
                        }
                        documentoObjDTO.contenidoCertificado.DuracionPespecifico = Int32.Parse(duracionPespecifico);
                        remplazo = remplazo.Replace("{tPEspecifico.duracion}", duracionPespecifico);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                    {
                        remplazo = remplazo.Replace("{T_MatriculaCabecera.CodigoMatricula}", matriculaCabecera.CodigoMatricula);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_Alumno.CorrelativoConstancia}"))
                    {
                        var correlativoConstancia = servicioCertificadoGeneradoAutomatico.ObtenerCorrelativoCertificado();
                        documentoObjDTO.contenidoCertificado.CorrelativoConstancia = correlativoConstancia;
                        remplazo = remplazo.Replace("{T_Alumno.CorrelativoConstancia}", correlativoConstancia.ToString());
                    }
                    /*reemplazar Cronograma de notas del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaNotas}"))
                    {
                        string tablaNota = "";
                        string estado = "";
                        var cronogramaNota = servicioAlumno.ObtenerCronogramaNota(matriculaCabecera.Id);
                        if (cronogramaNota == null || cronogramaNota.Count == 0)
                        {
                            var resultadocurso = servicioMatriculaCabecera.ObtenerAlumnoProgramaEspecificoLista(matriculaCabecera.Id);
                            var idPespecifico = resultadocurso.Select(s => new { s.IdPEspecifico }).ToList();
                            var cursos = resultadocurso.Select(s => new { s.PEspecifico }).ToList();

                            tablaNota += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                            tablaNota += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> NOTA </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ESTADO </td>
                                            </tr>";
                            int contador = 0;
                            for (int rows = 0; rows < resultadocurso.Count; rows++)
                            {
                                tablaNota += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;;text-align:left'> {resultadocurso[rows].PEspecifico}</td>";
                                EsquemaEvaluacionNotaCursoDTO nota = servicioEsquemaEvaluacion.ObtenerDetalleCalificacionPorCurso(matriculaCabecera.Id, idPespecifico[rows].IdPEspecifico, 1);
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {nota.NotaCurso}</td>";
                                if (nota.NotaCurso >= 60)
                                {
                                    estado = "APROBADO";
                                }
                                else
                                {
                                    estado = "DESAPROBADO";
                                }
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {estado}</td>";
                                contador = contador + 1;
                                tablaNota += "</tr>";
                            }
                        }
                        else
                        {
                            tablaNota += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                            tablaNota += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> NOTA </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ESTADO </td>
                                            </tr>";
                            int contador = 0;
                            for (int rows = 0; rows < cronogramaNota.Count; rows++)
                            {
                                tablaNota += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;;text-align:left'> {cronogramaNota[rows].Curso}</td>";
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Nota}</td>";
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Estado}</td>";
                                contador = contador + 1;
                                tablaNota += "</tr>";
                            }
                        }
                        tablaNota += "</table>";
                        documentoObjDTO.contenidoCertificado.CronogramaNota = tablaNota;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaNotas}", tablaNota);
                    }
                    /*reemplazar Cronograma de Asistencia del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaAsistencia}"))
                    {
                        string tablaAsistencia = "";
                        var cronogramaAsistencia = servicioAlumno.ObtenerCronogramaAsistencia(matriculaCabecera.Id);
                        if (cronogramaAsistencia == null || cronogramaAsistencia.Count == 0)
                        {
                            throw new Exception("No se Puede Calcular CronogramaAsistencia");
                        }
                        tablaAsistencia += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaAsistencia += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ASISTENCIA </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaAsistencia.Count; rows++)
                        {
                            tablaAsistencia += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:left;'> {cronogramaAsistencia[rows].Curso}</td>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:Center;'> {cronogramaAsistencia[rows].PorcentajeAsistencia}</td>";
                            contador = contador + 1;
                            tablaAsistencia += "</tr>";
                        }
                        tablaAsistencia += "</table>";
                        documentoObjDTO.contenidoCertificado.CronogramaAsistencia = tablaAsistencia;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaAsistencia}", tablaAsistencia);
                    }
                }
                var pdfFrontal = PdfGenerator.GeneratePdf(remplazo, config);
                var temmporal = ImportarPdfDocument(pdfFrontal);
                PdfSharp.Pdf.PdfPage page = temmporal.Pages[0];
                pdf.AddPage(page);

                PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[0], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                if (IdPlantillaBase == 12)
                {
                    string[] b = FondoFrontalCertificado.Split("http");
                    FondoFrontalCertificado = "http" + b[b.Count() - 1];
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalCertificado);
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 843, 595);
                }
                else
                {
                    string[] b = FondoFrontalConstancia.Split("http");
                    FondoFrontalConstancia = "http" + b[b.Count() - 1];
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalConstancia);
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 595, 843);
                }
                if (certificados == null
                    && nuevoCertificado.NombreCurso != null
                    && nuevoCertificado.FechaInicio.Year > 1900
                    && nuevoCertificado.FechaFinal.Year > 1900
                    && nuevoCertificado.Duracion != null)
                {
                    nuevoCertificado.IdMatriculaCabecera = oportunidadClasificacionOperaciones.IdMatriculaCabecera;
                    nuevoCertificado.EstadoCambioDatos = false;
                    nuevoCertificado.Estado = true;
                    nuevoCertificado.UsuarioCreacion = "SYSTEM";
                    nuevoCertificado.UsuarioModificacion = "SYSTEM";
                    nuevoCertificado.FechaCreacion = DateTime.Now;
                    nuevoCertificado.FechaModificacion = DateTime.Now;
                    servicioMatriculaCertificado.Add(nuevoCertificado);
                    certificados = servicioMatriculaCertificado.ObtenerTotal(oportunidadClasificacionOperaciones.IdMatriculaCabecera);
                };
                if (idPlantillaP != 0 && idPlantillaP != null)
                {
                    config = new PdfGenerateConfig();
                    if (IdPlantillaBase == 12)/*Certificado*/
                    {
                        config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                        config.PageSize = PdfSharp.PageSize.A4;
                        config.MarginLeft = 257;
                        config.MarginRight = 83;
                        config.MarginTop = 49;
                        config.MarginBottom = 0;
                    }
                    string estructuraCurricular = "";
                    listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();
                    var plantillaP = servicioPlantilla.ObtenerPorId(idPlantillaP);
                    var plantillaBaseP = servicioPlantilla.ObtenerPlantillaCorreo(idPlantillaP);
                    var listaEtiquetaP = plantillaBaseP.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();
                    foreach (var etiqueta in listaEtiquetaP)
                    {
                        listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = "" });
                    }
                    sepacion = new List<string>();
                    sepacion = plantillaBaseP.Cuerpo.Split("###").ToList();
                    foreach (var item in sepacion)
                    {
                        remplazo = item;
                        string tipoletra = "Times New Roman";
                        string searchString = "/*";
                        int startIndex = remplazo.IndexOf(searchString);
                        searchString = "*/";
                        int endIndex = 0;
                        string substring = "";
                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                        else
                        {
                            searchString = "text-align:";
                            startIndex = remplazo.IndexOf(searchString);
                            searchString = ";";

                            if (startIndex != -1)
                            {
                                endIndex = remplazo.IndexOf(searchString, startIndex);
                                substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                            }
                            if (substring != null && substring != "")
                            {
                                tipoletra = substring;
                            }
                        }
                        if (item.Contains("acute"))
                        {
                            remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                        }
                        if (item.Contains("repositorioweb"))
                        {
                            if (IdPlantillaBase == 12)
                            {
                                FondoReversoCertificado = item;
                            }
                            else
                            {
                                FondoReversoConstancia = item;
                            }
                        }
                        if (item.Contains("{tPLA_PGeneral.Nombre}"))
                        {
                            remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", ((certificados != null) ? certificados.NombreCurso : detalleMatriculaCabecera.NombreProgramaGeneral.Replace(" - COLOMBIA", "").ToUpper()));
                        }
                        if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                        {
                            var codigoPartner = servicioPGeneral.ObtenerCodigoPartner(detalleMatriculaCabecera.IdProgramaGeneral);
                            if (codigoPartner != null)
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                                documentoObjDTO.contenidoCertificado.CodigoPartner = codigoPartner;
                            }
                            else
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", "");
                            }
                        }
                        if (item.Contains("{tCentroCosto.centrocosto}"))
                        {
                            remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto);
                        }
                        if (item.Contains("{tPEspecifico.ciudad}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.ciudad}", detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper() + detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower()); ;
                        }
                        //reemplazar nombre 1 alumno
                        if (item.Contains("{T_Alumno.NombreCompleto}"))
                        {

                        }
                        if (item.Contains("{tAlumnos.nombre1}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1);

                            if (item.Contains("{tAlumnos.nombre2}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2);
                            }
                            if (item.Contains("{tAlumnos.apematerno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno);
                            }
                            if (item.Contains("{tAlumnos.apepaterno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno);
                            }
                        }
                        if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                        {
                            remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", (certificados != null) ? certificados.Duracion.ToString() : DatosCompuestosOportunidad.PwDuracion);
                        }
                        //reemplazar Fecha Inicio Capacitacion
                        if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", (certificados != null) ? servicioMatriculaCertificado.TransformarFechaEnCadena(certificados.FechaInicio) : servicioAlumno.ObtenerFechaInicioCapacitacion(matriculaCabecera.Id));
                        }
                        //reemplazar Fecha Fin Capacitacion
                        if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", (certificados != null) ? servicioMatriculaCertificado.TransformarFechaEnCadena(certificados.FechaFinal) : servicioAlumno.ObtenerFechaFinCapacitacion(matriculaCabecera.Id));
                        }
                        //reemplazar Calificacion Promedio
                        if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", servicioAlumno.ObtenerNotaPromedio(matriculaCabecera.Id));
                        }
                        //reemplazar Fecha Emision Certificado
                        if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", servicioAlumno.ObtenerFechaEmision());
                        }
                        //reemplazar Fecha Codigo Certificado
                        if (item.Contains("{T_Alumno.CodigoCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", servicioAlumno.ObtenerCodigoCertificado(matriculaCabecera.Id));
                        }
                        //reemplazar duracion en horas de Programa Especifico
                        if (item.Contains("{tPEspecifico.duracion}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.duracion}", (certificados != null) ? certificados.Duracion.ToString() : _unitOfWork.EstructuraEspecificaRepository.ObtenerDuracionTotalPorIdMatriculaCabecera(matriculaCabecera.Id));
                        }
                        if (item.Contains("{T_Pgeneral.EstructuraCurricular}"))
                        {
                            var estructuraPorVersion = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerCursosEstrucuraCurricular(matriculaCabecera.Id);
                            if (estructuraPorVersion.Count > 0)
                            {
                                string listaEstructura = $@"<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                listaEstructura += "<ul style='margin-top:-20px'>";
                                foreach (var hijo in estructuraPorVersion)
                                {
                                    listaEstructura += $@"<li style='padding-bottom:5px'>{hijo.Nombre.Replace(" - COLOMBIA", "")} ({hijo.Duracion} horas cronológicas)</li>";
                                }
                                listaEstructura += "</ul></td></tr></table>";
                                estructuraCurricular = listaEstructura;
                                documentoObjDTO.contenidoCertificado.EstructuraCurricular = listaEstructura;
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                            }
                            else
                            {
                                var estructuraCurso = servicioDocumentoSeccionPw.ObtenerEstructuraCurso(detalleMatriculaCabecera.IdProgramaGeneral);
                                if (estructuraCurso.Count > 0)
                                {
                                    string listaEstructura = $@"
                                                        <table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                    listaEstructura += "<ul style='margin-top:-20px'>";

                                    foreach (var capitulo in estructuraCurso)
                                    {
                                        listaEstructura += $@"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")} </li>";
                                    }

                                    listaEstructura += "</ul></td></tr></table>";
                                    estructuraCurricular = listaEstructura;
                                    documentoObjDTO.contenidoCertificado.EstructuraCurricular = listaEstructura;
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                                }
                                else
                                {
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", "");
                                }
                            }
                        }
                        if (item.Contains("Template"))
                        {
                            var etiq = listaObjetoWhasApp.Where(x => x.Codigo.Contains("Template")).ToList();
                            SeccionEtiquetaDTO valor = new SeccionEtiquetaDTO();
                            string etiqueta = "";

                            foreach (var a in etiq)
                            {
                                List<string> ListaPalabras = new List<string>();
                                char[] delimitador = new char[] { '.' };
                                string IdPlantilla = "";
                                string IdColumna = "";
                                string[] array = a.Codigo.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string s in array)
                                {
                                    ListaPalabras.Add(s);
                                }
                                IdPlantilla = ListaPalabras[3].ToString();
                                IdColumna = ListaPalabras[4].ToString();

                                var prevalor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna.Replace("}", "")), oportunidad.IdCentroCosto ?? default(int));

                                if (prevalor != null && estructuraCurricular == "")
                                {
                                    etiqueta = a.Codigo;
                                    valor = prevalor;
                                }
                                else
                                {
                                    remplazo = remplazo.Replace(a.Codigo, "");
                                }
                            }
                            if (etiqueta != "")
                            {
                                var tratamiento = valor.Valor.Replace("<p>", "<p id='estructura'<p>");
                                tratamiento = tratamiento.Replace("<li>", "<li id='liseparar'<li>");
                                tratamiento = tratamiento.Replace("&bull", "<li id='liseparar'");
                                estructura = tratamiento.Split("<p id='estructura'").Where(x => x.StartsWith("<p><strong>")).ToList();
                                List<string> todo = new List<string>();
                                int contador = 0;
                                string htmltabla = "";
                                List<string> total = new List<string>();
                                foreach (var li in estructura)
                                {
                                    foreach (var li1 in li.Split("<li id='liseparar'").ToList())
                                    {
                                        if (li1.Contains("<p>"))
                                            total.Add(li1.Replace("p>", "li>").Replace("<li>", "<li style='padding-bottom:5px'>").Replace("strong", "span").Replace("<ul>", "").Replace("</ul>", "").Replace("\"", "'").Replace("<div class='slide'>", "").Replace("<p style='padding-left:30px;'>", ""));
                                    }
                                }
                                int Cantidadcolumns = total.Count / 25;
                                int residuo = total.Count % 25;
                                if (residuo != 0)
                                {
                                    Cantidadcolumns++;
                                }
                                int reparticion = Cantidadcolumns;
                                PdfPTable PdfTable = new PdfPTable(Cantidadcolumns);
                                PdfTable.WidthPercentage = 100f;
                                PdfPCell PdfPCell = null;
                                string cadena1 = "";
                                string cadena2 = "";
                                string cadena3 = "";
                                string cadena4 = "";
                                List<string> registros = new List<string>();
                                foreach (var concatenar in total)
                                {
                                    if (Conteo < 22)
                                    {
                                        if (Conteo == 0)
                                        {
                                            cadena1 += "<ul style='margin-top:-20px'>";
                                        }
                                        cadena1 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                    }
                                    else
                                    {
                                        if (Conteo < 48)
                                        {
                                            cadena2 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                        }
                                        else
                                        {
                                            if (Conteo < 75)
                                            {
                                                cadena3 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                            else
                                            {
                                                cadena4 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                        }
                                    }
                                    Conteo++;
                                }
                                htmltabla += "<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>";
                                contador = 0;
                                for (int rows = 0; rows < 1; rows++)
                                {
                                    htmltabla += "<tr style='vertical-align: text-top;'>";
                                    for (int column = 0; column < Cantidadcolumns; column++)
                                    {
                                        if (column == 0)
                                        {
                                            if (cadena1.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena1 = cadena1 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena1}";
                                        }
                                        if (column == 1)
                                        {
                                            if (cadena2.StartsWith("<li>"))
                                            {
                                                cadena2 = "<ul>" + cadena2;
                                            }
                                            if (cadena2.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena2 = cadena2 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena2}";
                                        }
                                        if (column == 2)
                                        {
                                            if (cadena3.StartsWith("<li>"))
                                            {
                                                cadena3 = "<ul>" + cadena3;
                                            }
                                            if (cadena3.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena3 = cadena3 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena3}";
                                        }
                                        if (column == 3)
                                        {
                                            if (cadena4.StartsWith("<li>"))
                                            {
                                                cadena4 = "<ul>" + cadena4;
                                            }
                                            if (cadena4.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena4 = cadena4 + "</ul>";
                                            }
                                            htmltabla += $@"<th style='vertical-align:top;text-align:left;font-weight: normal;'> {cadena4}";
                                        }
                                        contador = contador + 1;
                                        htmltabla += "</td>";
                                    }
                                    htmltabla += "</tr>";
                                }
                                htmltabla += "</table>";
                                PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table
                                documentoObjDTO.contenidoCertificado.EstructuraCurricular = htmltabla;
                                remplazo = remplazo.Replace(etiqueta, htmltabla);
                            }
                        }
                    }
                    var pdfPosterior = PdfGenerator.GeneratePdf(remplazo, config);
                    temmporal = ImportarPdfDocument(pdfPosterior);
                    page = temmporal.Pages[0];
                    pdf.AddPage(page);

                    gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[1], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoReversoCertificado); ;
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 843, 595);
                }
                //documento.Close();
                pdf.Save(ms, false);
                return ms.ToArray();
            }
        }
        /// <summary> 
        /// Autor: Daniel Huaita
        /// Fecha: 16/03/2023 
        /// Version: 1.0 
        /// Descripcion: Genera vista previa del certificado.
        /// </summary> 
        public byte[] RepublicacionVistaPreviaCertificado(int IdPlantillaF, int IdPlantillaP, int IdOportunidad, TMatriculaCabeceraDatosCertificado solicitudCertificado, ref int Idplantillabase, ref string codigoCertificado)
        {
            CertificadoGeneradoAutomaticoContenido ContenidoCertificado = new CertificadoGeneradoAutomaticoContenido();
            this.ContenidoCertificado = new CertificadoGeneradoAutomaticoContenido();
            int IdPlantillaBase = 0;
            var oportunidadClasificacionOperaciones = _unitOfWork.OportunidadClasificacionOperacionesRepository.FirstBy(x => x.IdOportunidad == IdOportunidad);
            if (!_unitOfWork.MatriculaCabeceraRepository.Exist(x => x.Id == oportunidadClasificacionOperaciones.IdMatriculaCabecera))
            {
                throw new Exception("Matricula cabecera no valida!");
            }
            var certificados = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.ObtenerDatosCertificadoPorMatricula(oportunidadClasificacionOperaciones.IdMatriculaCabecera).First();
            TMatriculaCabeceraDatosCertificado NuevoCertificado = new TMatriculaCabeceraDatosCertificado();
            var listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();
            var matriculaCabecera = _unitOfWork.MatriculaCabeceraRepository.FirstBy(x => x.Id == oportunidadClasificacionOperaciones.IdMatriculaCabecera);
            var detalleMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleMatricula(matriculaCabecera.Id);
            var plantilla = _unitOfWork.PlantillaRepository.FirstById(IdPlantillaF);
            var plantillaBaseAll = _unitOfWork.PlantillaBaseRepository.FirstById(plantilla.IdPlantillaBase);

            IdPlantillaBase = plantilla.IdPlantillaBase;
            Idplantillabase = IdPlantillaBase;

            var plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(IdPlantillaF);
            var alumno = _unitOfWork.AlumnoRepository.FirstById(matriculaCabecera.IdAlumno.Value);
            var oportunidad = _unitOfWork.OportunidadRepository.FirstById(detalleMatriculaCabecera.IdOportunidad);
            var DatosCompuestosOportunidad = _unitOfWork.OportunidadRepository.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
            var personal = _unitOfWork.PersonalRepository.FirstById(oportunidad.IdPersonalAsignado.Value);
            var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();
            foreach (var etiqueta in listaEtiqueta)
            {
                listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = "" });
            }
            List<string> sepacion = new List<string>();
            string FondoFrontalCertificado = "";
            string FondoReversoCertificado = "";
            string FondoFrontalConstancia = "";
            string FondoReversoConstancia = "";
            sepacion = plantillaBase.Cuerpo.Split("###").ToList();
            Dictionary<string, object> map = new Dictionary<string, object>();
            map["font_factory"] = new MyFontFactory();
            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                var config = new PdfGenerateConfig();
                if (IdPlantillaBase == 12)/*Certificado*/
                {
                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginTop = 144;
                    config.MarginBottom = 30;
                    config.MarginLeft = 111;
                    config.MarginRight = 83;

                }
                else
                {
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginLeft = 60;
                    config.MarginRight = 60;
                    config.MarginTop = 60;
                    config.MarginBottom = 25;

                }
                int Conteo = 0;
                List<string> estructura = new List<string>();

                string remplazo = "";
                foreach (var item in sepacion)
                {
                    string direccionCodigoQR = "";
                    string tipoletra = "Times New Roman";
                    remplazo = item;
                    string searchString = "/*";
                    int startIndex = remplazo.IndexOf(searchString);
                    searchString = "*/";
                    int endIndex = 0;
                    string substring = "";
                    if (startIndex != -1)
                    {
                        endIndex = remplazo.IndexOf(searchString, startIndex);
                        substring = remplazo.Substring(startIndex + searchString.Length, endIndex - searchString.Length);
                    }
                    if (substring != null && substring != "")
                    {
                        tipoletra = substring;
                    }
                    else
                    {
                        searchString = "text-align:";
                        startIndex = remplazo.IndexOf(searchString);
                        searchString = ";";

                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                    }
                    if (item.Contains("repositorioweb"))
                    {
                        if (IdPlantillaBase == 12)
                        {
                            FondoFrontalCertificado = item;
                        }
                        else
                        {
                            FondoFrontalConstancia = item;
                        }

                    }
                    if (item.Contains("acute"))
                    {
                        remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                    }
                    if (item.Contains("CODIGOQR"))
                    {
                        string url = "";
                        if (IdPlantillaBase == 12)/*Certificados*/
                        {
                            url = _unitOfWork.AlumnoRepository.ObtenerCodigoCertificado(matriculaCabecera.Id);
                        }
                        else /*13:Constancia*/
                        {
                            url = _unitOfWork.CertificadoGeneradoAutomaticoRepository.ObtenerCorrelativoCertificado().ToString();
                        }

                        var urlCodigo = "https://bsginstitute.com/informacionCertificado?cod=" + matriculaCabecera.Id + "." + url;
                        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                        QRCodeData qrDatos = qRCodeGenerator.CreateQrCode(urlCodigo, QRCodeGenerator.ECCLevel.Q);
                        QRCode qRCodigo = new QRCode(qrDatos);

                        System.Drawing.Bitmap qrImagen = qRCodigo.GetGraphic(7, System.Drawing.Color.Black, System.Drawing.Color.White, false);
                        System.Drawing.Image objImage = (System.Drawing.Image)qrImagen;

                        direccionCodigoQR = _unitOfWork.AlumnoRepository.guardarArchivosQR(ImageToByte2(objImage), "image/jpeg", url);
                        this.ContenidoCertificado.CodigoQr = direccionCodigoQR;

                        remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='{direccionCodigoQR}' width=55 height=55  />");
                        //remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='data:image/jpeg;base64,{Convert.ToBase64String(ImageToByte2(objImage))}' width=55 height=55  />");

                    }
                    if (item.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        var nombrePrograma = "";
                        remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", solicitudCertificado.NombreCurso);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                    {
                        var codigoPartner = _unitOfWork.PGeneralRepository.ObtenerCodigoPartner(matriculaCabecera.Id);
                        if (codigoPartner != null)
                        {
                            this.ContenidoCertificado.CodigoPartner = codigoPartner;
                            remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                        }
                        else
                        {
                            throw new Exception("No se puede Calcular CodigoPartner");
                        }
                    }
                    //reemplazar nombre 1 alumno
                    if (item.Contains("{T_Alumno.NombreCompleto}"))
                    {
                    }
                    if (item.Contains("{tAlumnos.nombre1}"))
                    {
                        string nombreAlumno = "";
                        nombreAlumno = alumno.Nombre1.ToUpper();
                        //var parrafo = new iTextSharp.text.Phrase(alumno.NombreCompleto, fuente);
                        remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1.ToUpper());

                        if (item.Contains("{tAlumnos.nombre2}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.Nombre2.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2.ToUpper());
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.Nombre2.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apepaterno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoPaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno.ToUpper());
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoPaterno.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apematerno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoMaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno.ToUpper());
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoMaterno.ToUpper(), fuente));
                        }

                        this.ContenidoCertificado.NombreAlumno = nombreAlumno;
                    }
                    if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                    {

                        //string Duracion = DatosCompuestosOportunidad.PwDuracion;
                        remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", solicitudCertificado.Duracion);
                    }
                    if (item.Contains("{tCentroCosto.centrocosto}"))
                    {
                        this.ContenidoCertificado.NombreCentroCosto = detalleMatriculaCabecera.NombreCentroCosto.ToUpper();
                        remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto.ToUpper());
                    }
                    if (item.Contains("{tPEspecifico.ciudad}"))
                    {
                        var PrimeraLetra = detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper();
                        var resto = detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower();
                        this.ContenidoCertificado.Ciudad = PrimeraLetra + resto;
                        if (this.ContenidoCertificado.Ciudad == null || this.ContenidoCertificado.Ciudad == "")
                        {
                            throw new Exception("No se Puede Calcular Ciudad");
                        }
                        remplazo = remplazo.Replace("{tPEspecifico.ciudad}", PrimeraLetra + resto);
                    }
                    if (item.Contains("{tPEspecifico.EscalaCalificacion}"))
                    {
                        var escalaCalificacion = detalleMatriculaCabecera.EscalaCalificacion;
                        this.ContenidoCertificado.EscalaCalificacion = escalaCalificacion;
                        if (escalaCalificacion == 0 || escalaCalificacion == null)
                        {
                            throw new Exception("No se Puede Calcular Escala Calificacion");
                        }
                        remplazo = remplazo.Replace("{tPEspecifico.EscalaCalificacion}", escalaCalificacion.ToString());
                    }
                    //reemplazar Fecha Inicio Capacitacion
                    if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                    {
                        var fechaInicioCapacitacion = "";
                        if (certificados == null)
                        {
                            fechaInicioCapacitacion = _unitOfWork.AlumnoRepository.ObtenerFechaInicioCapacitacionPortalWeb(matriculaCabecera.Id);
                            NuevoCertificado.FechaInicio = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.TranformarCadenaEnFecha(fechaInicioCapacitacion);
                        }
                        else
                        {
                            fechaInicioCapacitacion = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.TranformarFechaEnCadena(certificados.FechaInicio.Value);
                        }
                        if (fechaInicioCapacitacion == null || fechaInicioCapacitacion == "")
                        {
                            throw new Exception("No se Puede Calcular FechaInicioCapacitacion");
                        }
                        if (fechaInicioCapacitacion == null || fechaInicioCapacitacion == "")
                        {
                            throw new Exception("No se Puede Calcular FechaInicioCapacitacion");
                        }
                        this.ContenidoCertificado.FechaInicioCapacitacion = fechaInicioCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", fechaInicioCapacitacion);
                    }

                    //reemplazar Fecha Fin Capacitacion
                    if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                    {
                        var fechaFinCapacitacion = "";
                        var fechaFin = solicitudCertificado.FechaFinal;
                        try
                        {
                            fechaFinCapacitacion = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.TranformarFechaEnCadena(fechaFin);
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se Puede Actualizar Fecha Fin");
                        }
                        this.ContenidoCertificado.FechaFinCapacitacion = fechaFinCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", fechaFinCapacitacion);
                    }
                    //reemplazar Calificacion Promedio
                    if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                    {
                        var calificacionPromedio = _unitOfWork.AlumnoRepository.ObtenerNotaPromedio(matriculaCabecera.Id);
                        if (calificacionPromedio == null || calificacionPromedio == "")
                        {
                            throw new Exception("No se Puede Calcular Calificacion Promedio");
                        }
                        this.ContenidoCertificado.CalificacionPromedio = Convert.ToInt32(calificacionPromedio);
                        remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", (Convert.ToInt32(calificacionPromedio)).ToString());
                    }
                    //reemplazar Fecha Emision Certificado
                    if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                    {
                        var fechaEmision = _unitOfWork.AlumnoRepository.ObtenerFechaEmision();
                        this.ContenidoCertificado.FechaEmisionCertificado = fechaEmision;
                        remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", fechaEmision);
                    }

                    //reemplazar Fecha Codigo Certificado
                    if (item.Contains("{T_Alumno.CodigoCertificado}"))
                    {
                        var CodigoCertificado = _unitOfWork.AlumnoRepository.ObtenerCodigoCertificado(matriculaCabecera.Id);
                        if (CodigoCertificado == null || CodigoCertificado == "")
                        {
                            throw new Exception("No se Puede Calcular Codigo Certificado");
                        }
                        codigoCertificado = CodigoCertificado;
                        remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", CodigoCertificado);
                    }

                    //reemplazar duracion en horas de Programa Especifico
                    if (item.Contains("{tPEspecifico.duracion}"))
                    {
                        var duracionPespecifico = "";
                        if (certificados == null)
                        {
                            duracionPespecifico = _unitOfWork.EstructuraEspecificaRepository.ObtenerDuracionTotalPorIdMatriculaCabecera(matriculaCabecera.Id);
                            NuevoCertificado.Duracion = duracionPespecifico;
                        }
                        else
                        {
                            duracionPespecifico = certificados.Duracion.ToString();
                        }
                        if (duracionPespecifico == null || duracionPespecifico == "")
                        {
                            throw new Exception("No se Puede Calcular Duracion Pespecifico");
                        }
                        this.ContenidoCertificado.DuracionPespecifico = Int32.Parse(duracionPespecifico);
                        remplazo = remplazo.Replace("{tPEspecifico.duracion}", duracionPespecifico);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                    {
                        remplazo = remplazo.Replace("{T_MatriculaCabecera.CodigoMatricula}", matriculaCabecera.CodigoMatricula);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_Alumno.CorrelativoConstancia}"))
                    {
                        var correlativoConstancia = _unitOfWork.CertificadoGeneradoAutomaticoRepository.ObtenerCorrelativoCertificado();
                        this.ContenidoCertificado.CorrelativoConstancia = correlativoConstancia;
                        remplazo = remplazo.Replace("{T_Alumno.CorrelativoConstancia}", correlativoConstancia.ToString());
                    }
                    /*reemplazar Cronograma de notas del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaNotas}"))
                    {
                        string tablaNota = "";
                        string estado = "";

                        var cronogramaNota = _unitOfWork.AlumnoRepository.ObtenerCronogramaNota(matriculaCabecera.Id);
                        if (cronogramaNota == null || cronogramaNota.Count == 0)
                        {

                            var resultadocurso = _unitOfWork.MatriculaCabeceraRepository.ObtenerAlumnoProgramaEspecifico(matriculaCabecera.Id);
                            var idPespecifico = resultadocurso.Select(s => new { s.IdPEspecifico }).ToList();
                            var cursos = resultadocurso.Select(s => new { s.PEspecifico }).ToList();

                            tablaNota += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                            tablaNota += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> NOTA </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ESTADO </td>
                                            </tr>";
                            int contador = 0;
                            for (int rows = 0; rows < resultadocurso.Count; rows++)
                            {
                                tablaNota += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;;text-align:left'> {resultadocurso[rows].PEspecifico}</td>";
                                EsquemaEvaluacionService esquemaBO = new EsquemaEvaluacionService(_unitOfWork);
                                var nota = esquemaBO.ObtenerDetalleCalificacionPorCurso(matriculaCabecera.Id, idPespecifico[rows].IdPEspecifico, 1);
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {nota.NotaCurso}</td>";
                                if (nota.NotaCurso >= 60)
                                {
                                    estado = "APROBADO";
                                }
                                else
                                {
                                    estado = "DESAPROBADO";

                                }
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {estado}</td>";

                                contador = contador + 1;
                                tablaNota += "</tr>";

                            }
                        }
                        else
                        {
                            tablaNota += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                            tablaNota += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> NOTA </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ESTADO </td>
                                            </tr>";
                            int contador = 0;
                            for (int rows = 0; rows < cronogramaNota.Count; rows++)
                            {
                                tablaNota += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;;text-align:left'> {cronogramaNota[rows].Curso}</td>";
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Nota}</td>";
                                tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Estado}</td>";

                                contador = contador + 1;
                                tablaNota += "</tr>";
                            }
                        }

                        tablaNota += "</table>";
                        this.ContenidoCertificado.CronogramaNota = tablaNota;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaNotas}", tablaNota);


                    }
                    /*reemplazar Cronograma de Asistencia del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaAsistencia}"))
                    {
                        string tablaAsistencia = "";
                        var cronogramaAsistencia = _unitOfWork.AlumnoRepository.ObtenerCronogramaAsistencia(matriculaCabecera.Id);
                        if (cronogramaAsistencia == null || cronogramaAsistencia.Count == 0)
                        {
                            throw new Exception("No se Puede Calcular CronogramaAsistencia");
                        }
                        tablaAsistencia += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaAsistencia += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ASISTENCIA </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaAsistencia.Count; rows++)
                        {
                            tablaAsistencia += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:left;'> {cronogramaAsistencia[rows].Curso}</td>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:Center;'> {cronogramaAsistencia[rows].PorcentajeAsistencia}</td>";

                            contador = contador + 1;
                            tablaAsistencia += "</tr>";
                        }
                        tablaAsistencia += "</table>";
                        this.ContenidoCertificado.CronogramaAsistencia = tablaAsistencia;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaAsistencia}", tablaAsistencia);
                    }
                }
                var pdfFrontal = PdfGenerator.GeneratePdf(remplazo, config);
                var temmporal = ImportarPdfDocument(pdfFrontal);
                PdfSharp.Pdf.PdfPage page = temmporal.Pages[0];
                pdf.AddPage(page);
                PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[0], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);
                if (IdPlantillaBase == 12)
                {
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalCertificado);
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 843, 595);
                }
                else
                {

                    string[] b = FondoFrontalConstancia.Split("http");
                    FondoFrontalConstancia = "http" + b[b.Count() - 1];

                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalConstancia);
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 595, 843);
                }

                if (certificados == null
                    && NuevoCertificado.NombreCurso != null
                    && NuevoCertificado.FechaInicio.Year > 1900
                    && NuevoCertificado.FechaFinal.Year > 1900
                    && NuevoCertificado.Duracion != null)
                {
                    NuevoCertificado.IdMatriculaCabecera = oportunidadClasificacionOperaciones.IdMatriculaCabecera;
                    NuevoCertificado.EstadoCambioDatos = false;
                    NuevoCertificado.Estado = true;
                    NuevoCertificado.UsuarioCreacion = "SYSTEM";
                    NuevoCertificado.UsuarioModificacion = "SYSTEM";
                    NuevoCertificado.FechaCreacion = DateTime.Now;
                    NuevoCertificado.FechaModificacion = DateTime.Now;
                    _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.Insert(NuevoCertificado);
                    certificados = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.ObtenerDatosCertificadoPorMatricula(oportunidadClasificacionOperaciones.IdMatriculaCabecera).First();
                };
                if (IdPlantillaP != 0 && IdPlantillaP != null)
                {
                    config = new PdfGenerateConfig();
                    if (IdPlantillaBase == 12)/*Certificado*/
                    {
                        config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                        config.PageSize = PdfSharp.PageSize.A4;
                        config.MarginLeft = 257;
                        config.MarginRight = 83;
                        config.MarginTop = 49;
                        config.MarginBottom = 0;

                    }
                    string _estructuraCurricular = "";
                    listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();
                    var plantillaP = _unitOfWork.PlantillaRepository.FirstById(IdPlantillaP);
                    var plantillaBaseP = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(IdPlantillaP);
                    if (plantillaBaseP != null)
                    {
                        if (plantillaBaseP.Cuerpo.Contains("{Template.RE-I&amp;D-041 Silabo.Estructura Curricular.0CE16B45-634A-46F2-ABEB-01BCF9A9560E.5F6E8B4A-C6F1-C020-A9D8-08D3C85CAB1B}"))
                        {
                            plantillaBaseP.Cuerpo = plantillaBaseP.Cuerpo.Replace("{Template.RE-I&amp;D-041 Silabo.Estructura Curricular.0CE16B45-634A-46F2-ABEB-01BCF9A9560E.5F6E8B4A-C6F1-C020-A9D8-08D3C85CAB1B}", "");
                        }
                        if (plantillaBaseP.Cuerpo.Contains("{Template.RE-I&amp;D-092 Perfil del Programa.Estructura Curricular.1BD1B4D3-A52A-4E7F-875F-A8EC20A315C6.500827DB-8411-4599-882B-ACEDB6334512}"))
                        {
                            plantillaBaseP.Cuerpo = plantillaBaseP.Cuerpo.Replace("{Template.RE-I&amp;D-092 Perfil del Programa.Estructura Curricular.1BD1B4D3-A52A-4E7F-875F-A8EC20A315C6.500827DB-8411-4599-882B-ACEDB6334512}", "{T_Pgeneral.EstructuraCurricular}");
                        }

                    }

                    var listaEtiquetaP = plantillaBaseP.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                    foreach (var etiqueta in listaEtiquetaP)
                    {
                        listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = "" });
                    }
                    sepacion = new List<string>();

                    sepacion = plantillaBaseP.Cuerpo.Split("###").ToList();

                    foreach (var item in sepacion)
                    {
                        remplazo = item;

                        string tipoletra = "Times New Roman";

                        string searchString = "/*";
                        int startIndex = remplazo.IndexOf(searchString);
                        searchString = "*/";
                        int endIndex = 0;
                        string substring = "";
                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                        else
                        {
                            searchString = "text-align:";
                            startIndex = remplazo.IndexOf(searchString);
                            searchString = ";";

                            if (startIndex != -1)
                            {
                                endIndex = remplazo.IndexOf(searchString, startIndex);
                                substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                            }
                            if (substring != null && substring != "")
                            {
                                tipoletra = substring;
                            }
                        }
                        if (item.Contains("acute"))
                        {
                            remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                        }
                        if (item.Contains("repositorioweb"))
                        {
                            if (IdPlantillaBase == 12)
                            {
                                FondoReversoCertificado = item;
                            }
                            else
                            {
                                FondoReversoConstancia = item;
                            }

                        }

                        if (item.Contains("{tPLA_PGeneral.Nombre}"))
                        {
                            remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", ((certificados != null) ? certificados.NombreCurso : detalleMatriculaCabecera.NombreProgramaGeneral.Replace(" - COLOMBIA", "").ToUpper()));
                        }
                        if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                        {
                            var codigoPartner = _unitOfWork.PGeneralRepository.ObtenerCodigoPartner(detalleMatriculaCabecera.IdProgramaGeneral);
                            if (codigoPartner != null)
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                                this.ContenidoCertificado.CodigoPartner = codigoPartner;
                            }
                            else
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", "");
                            }
                        }
                        if (item.Contains("{tCentroCosto.centrocosto}"))
                        {
                            remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto);
                        }
                        if (item.Contains("{tPEspecifico.ciudad}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.ciudad}", detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper() + detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower()); ;
                        }
                        //reemplazar nombre 1 alumno
                        if (item.Contains("{T_Alumno.NombreCompleto}"))
                        {

                        }
                        if (item.Contains("{tAlumnos.nombre1}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1);

                            if (item.Contains("{tAlumnos.nombre2}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2);
                            }
                            if (item.Contains("{tAlumnos.apematerno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno);
                            }
                            if (item.Contains("{tAlumnos.apepaterno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno);
                            }

                        }

                        if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                        {
                            remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", (certificados != null) ? certificados.Duracion.ToString() : DatosCompuestosOportunidad.PwDuracion);
                        }
                        //reemplazar Fecha Inicio Capacitacion
                        if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", (certificados != null) ? _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.TranformarFechaEnCadena(certificados.FechaInicio.Value) : _unitOfWork.AlumnoRepository.ObtenerFechaInicioCapacitacion(matriculaCabecera.Id));
                        }

                        //reemplazar Fecha Fin Capacitacion
                        if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", (certificados != null) ? _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.TranformarFechaEnCadena(certificados.FechaFinal.Value) : _unitOfWork.AlumnoRepository.ObtenerFechaFinCapacitacion(matriculaCabecera.Id));
                        }
                        //reemplazar Calificacion Promedio
                        if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", _unitOfWork.AlumnoRepository.ObtenerNotaPromedio(matriculaCabecera.Id));
                        }
                        //reemplazar Fecha Emision Certificado
                        if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", _unitOfWork.AlumnoRepository.ObtenerFechaEmision());
                        }

                        //reemplazar Fecha Codigo Certificado
                        if (item.Contains("{T_Alumno.CodigoCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", _unitOfWork.AlumnoRepository.ObtenerCodigoCertificado(matriculaCabecera.Id));
                        }

                        //reemplazar duracion en horas de Programa Especifico
                        if (item.Contains("{tPEspecifico.duracion}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.duracion}", (certificados != null) ? certificados.Duracion.ToString() : _unitOfWork.EstructuraEspecificaRepository.ObtenerDuracionTotalPorIdMatriculaCabecera(matriculaCabecera.Id));
                        }
                        if (item.Contains("{T_Pgeneral.EstructuraCurricular}"))
                        {
                            var estructuraPorVersion = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerCursosEstrucuraCurricular(matriculaCabecera.Id);
                            if (estructuraPorVersion.Count > 0)
                            {
                                string listaEstructura = $@"<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                listaEstructura += "<ul style='margin-top:-20px'>";
                                foreach (var hijo in estructuraPorVersion)
                                {
                                    if (estructuraPorVersion.Count == 1)
                                        listaEstructura += $@"<li style='padding-bottom:5px'>{hijo.Nombre.Replace(" - COLOMBIA", "")}</li>";

                                    else
                                        listaEstructura += $@"<li style='padding-bottom:5px'>{hijo.Nombre.Replace(" - COLOMBIA", "")} ({hijo.Duracion} horas cronológicas)</li>";

                                }


                                listaEstructura += "</ul></td></tr></table>";
                                _estructuraCurricular = listaEstructura;
                                this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                            }
                            else
                            {
                                var estructuraCurso = _unitOfWork.DocumentoSeccionPwRepository.ObtenerEstructuraCursoPorIdProgramaGeneral(detalleMatriculaCabecera.IdProgramaGeneral);
                                if (estructuraCurso.Count > 0)
                                {
                                    string listaEstructura = $@"
                                                        <table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                    listaEstructura += "<ul style='margin-top:-20px'>";

                                    foreach (var capitulo in estructuraCurso)
                                    {
                                        listaEstructura += $@"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")} </li>";
                                    }

                                    listaEstructura += "</ul></td></tr></table>";
                                    _estructuraCurricular = listaEstructura;
                                    this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                                }
                                else
                                {
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", "");
                                }

                            }

                        }
                        if (item.Contains("{Template.Estructura}"))
                        {

                            var EstructuraporVersion = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerCursosEstrucuraCurricular(matriculaCabecera.Id);
                            if (EstructuraporVersion.Count > 1)
                            {
                                string listaEstructura = $@"<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                listaEstructura += "<ul style='margin-top:-20px'>";
                                foreach (var hijo in EstructuraporVersion)
                                {
                                    //listaEstructura += $@"<li style='padding-bottom:5px'>{hijo.Nombre.Replace(" - COLOMBIA", "")} ({hijo.Duracion} horas cronológicas)</li>";
                                    listaEstructura += $@"<li style='padding-bottom:5px'>{hijo.Nombre.Replace(" - COLOMBIA", "")} ({hijo.Duracion} horas cronológicas)</li>";

                                }


                                listaEstructura += "</ul></td></tr></table>";
                                _estructuraCurricular = listaEstructura;
                                ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                remplazo = remplazo.Replace("{Template.Estructura}", listaEstructura);
                            }
                            else
                            {
                                var estructuraCurso = _unitOfWork.DocumentoSeccionPwRepository.ObtenerEstructuraCursoPorIdProgramaGeneral(detalleMatriculaCabecera.IdProgramaGeneral);
                                if (estructuraCurso.Count > 0)
                                {
                                    string listaEstructura = $@"
                                                        <table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                    listaEstructura += "<ul style='margin-top:-20px'>";

                                    foreach (var capitulo in estructuraCurso)
                                    {
                                        listaEstructura += $@"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")} </li>";
                                    }

                                    listaEstructura += "</ul></td></tr></table>";
                                    _estructuraCurricular = listaEstructura;
                                    ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                    remplazo = remplazo.Replace("{Template.Estructura}", listaEstructura);
                                }
                                else
                                {
                                    remplazo = remplazo.Replace("{Template.Estructura}", "*");
                                }
                            }

                        }

                    }
                    var pdfPosterior = PdfGenerator.GeneratePdf(remplazo, config);
                    temmporal = ImportarPdfDocument(pdfPosterior);
                    page = temmporal.Pages[0];
                    pdf.AddPage(page);


                    FondoReversoCertificado = WebUtility.HtmlDecode(FondoReversoCertificado ?? "");
                    FondoReversoCertificado = Regex.Replace(FondoReversoCertificado, "<.*?>", "").Trim();
                    if (!Uri.TryCreate(FondoReversoCertificado, UriKind.Absolute, out var uri) || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                    {
                        throw new Exception("URL inválida: " + FondoReversoCertificado);
                    }

                    gfx = XGraphics.FromPdfPage(pdf.Pages[1], XGraphicsPdfPageOptions.Prepend);

                    var webRequest = (HttpWebRequest)WebRequest.Create(uri);
                    webRequest.AllowWriteStreamBuffering = true;

                    using (var webResponse = webRequest.GetResponse())
                    using (var stream = webResponse.GetResponseStream())
                    {
                        var xImage = XImage.FromStream(stream);
                        gfx.DrawImage(xImage, 0, 0, 843, 595);
                    }
                }

                pdf.Save(ms, false);
                return ms.ToArray();
            }

        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// Convierte imagen en Byte.
        /// </summary> 
        public static byte[] ImageToByte2(System.Drawing.Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Funcion para importar documento PDF
        /// </summary>
        /// <param name="pdf1"></param>
        /// <returns></returns>
        public PdfSharp.Pdf.PdfDocument ImportarPdfDocument(PdfSharp.Pdf.PdfDocument pdf1)
        {
            using (var stream = new MemoryStream())
            {
                pdf1.Save(stream, false);
                stream.Position = 0;
                var result = PdfSharp.Pdf.IO.PdfReader.Open(stream, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import);
                return result;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Genera la fuente según tamaño, color, estilo
        /// </summary>
        public class MyFontFactory : IFontProvider
        {
            public Font GetFont(string fontname,
                    string encoding, bool embedded, float size,
                    int style, BaseColor color)
            {
                BaseFont bf;
                try
                {
                    string pathfuente = System.AppDomain.CurrentDomain.BaseDirectory + "IMPRISHA.TTF";
                    //bf = BaseFont.CreateFont("c:/windows/fonts/arialuni.ttf","Identity-H", BaseFont.EMBEDDED);
                    bf = BaseFont.CreateFont(pathfuente, "Identity-H", BaseFont.EMBEDDED);
                }
                catch (DocumentException e)
                {
                    //e.pr();
                    return new Font();
                }
                catch (IOException e)
                {
                    //e.printStackTrace();
                    return new Font();
                }
                return new Font(bf, size);
            }
            public bool IsRegistered(string fontname)
            {
                return false;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de beneficios por la configuracion de beneficios del programa general mediante idpgeneral, idpais e idpaquete
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <param name="idPais"> Id de Pais </param>
        /// <param name="idPaquete"> Id de Paquete </param>
        /// <returns> List<string> </returns>
        public List<string> ObtenerBeneficiosConfiguradosProgramaGeneral(int idPGeneral, int? idPais, int? idPaquete)
        {
            try
            {
                List<string> listaBeneficios = new List<string>();
                if (idPais.HasValue)
                {
                    var listaBeneficiosConfigurado = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerPGeneralConfiguracionBeneficios(idPGeneral);
                    listaBeneficios = listaBeneficiosConfigurado.Where(x => x.Versiones.Any(a => a == idPaquete) && x.Paises.Any(a => a == idPais)).Select(x => x.Descripcion).ToList();
                }
                return listaBeneficios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022v
        /// Version: 1.0
        /// <summary>
        /// Obtiene documentos por el Id de Alumno
        /// </summary>
        /// <param name="idAlumno"></param>
        public void ObtenerDocumentosIdAlumno(int idAlumno)
        {
            CargarListaDocumentos();
            OportunidadService oportunidadService = new OportunidadService(_unitOfWork);

            var registroDocumentos = _unitOfWork.PGeneralRepository.ObtenerPGeneralParaDocumentosPorIdAlumno(idAlumno);
            documentoObjDTO.Oportunidad = oportunidadService.ObtenerDatosCompuestosPorActividades(registroDocumentos.IdActividadDetalle);

            //Brochure BSG Institute
            documentoObjDTO.ListaDocumentos.Where(w => w.Id == 0).FirstOrDefault().Mensaje = "Correcto";
            documentoObjDTO.ListaDocumentos.Where(w => w.Id == 0).FirstOrDefault().Url = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/brochure/BSGINSTITUTE-2019.pdf";
            documentoObjDTO.ListaDocumentos.Where(w => w.Id == 0).FirstOrDefault().MensajeDetalle = "Disponible Para todos";

            //Brochure
            documentoObjDTO.ListaDocumentos.Where(w => w.Id == 1).FirstOrDefault().Mensaje = registroDocumentos.UrlBrochurePrograma == null ? "Incorrecto" : "Correcto";
            documentoObjDTO.ListaDocumentos.Where(w => w.Id == 1).FirstOrDefault().Url = registroDocumentos.UrlBrochurePrograma;
            documentoObjDTO.ListaDocumentos.Where(w => w.Id == 1).FirstOrDefault().MensajeDetalle = "Disponible Para <b> todos los programas</b> / No colocaron la URL del brochure en el Programa General";

            //Cronogram Alumnos 
            if (registroDocumentos.Tipo != "Online Asincronica")
            {
                if (registroDocumentos.UrlDocumentoCronograma != null)
                {
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Mensaje = "Correcto";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Url = registroDocumentos.UrlDocumentoCronograma;
                }
                else
                {
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Mensaje = "Incorrecto";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
                }
            }
            else
            {
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Mensaje = "Incorrecto";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
            }

            //Requisitos Programas Online
            if (registroDocumentos.Tipo == "Online Sincronica")//cambiar online Sincronica
            {
                byte[] DocumentoRequisitos = GenerarRequisitos(documentoObjDTO.Oportunidad, 3);//idDocumento
                if (DocumentoRequisitos != null)
                {
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Mensaje = "Correcto";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online sincrónico.</b>";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().DocumentoByte = DocumentoRequisitos;
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Url = "archivoGenerado";
                }
                else
                {
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Mensaje = "Incorrecto";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online sincrónico.</b>";
                }
            }
            else
            {
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Mensaje = "Incorrecto";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online sincrónico.</b>";
            }


            //Pagare Word
            if (registroDocumentos.Ciudad != null)
            {
                if (registroDocumentos.Ciudad == "BOGOTA")
                {
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Url = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/pagares/PagarePresencialOnline-Colombia.docx";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Correcto";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú y Colombia</b>";
                }
                else if (registroDocumentos.Ciudad == "SANTA CRUZ" || registroDocumentos.Ciudad == "LA PAZ")//bolivia
                {
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Url = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/pagares/PAGARE-BSGINSTITUTEBOLIVIA.docx";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Correcto";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú ,Colombia y Bolivia</b>";
                }
                else
                {
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Url = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/pagares/PagarePresencial-Nacional-Extranjero.docx";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Correcto";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú y Colombia</b>";
                }
            }
            else
            {
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Incorrecto";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú y Colombia</b>";
            }

            //Pagare Excel
            if (registroDocumentos.Ciudad != null)
            {
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Url = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/pagares/PagareOnline-NacionalExtranjero.xlsx";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Mensaje = "Correcto";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online asincrónico-sincrónico Extranjero</b>";
            }
            else
            {
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Mensaje = "Incorrecto";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online asincrónico-sincrónico Extranjero</b>";
            }

            //convenio
            var DocumentoConvenio = GenerarConvenioCondicionDocumentoDTO(documentoObjDTO.Oportunidad, "Convenio", 6);//idDocumento 6
            if (DocumentoConvenio != null)
            {
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().Mensaje = "Correcto";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().MensajeDetalle = "Disponible programa <b>Presencial Perú y Colombia, Extranjero Online sincrónico-asincrónico </b>, se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().Url = DocumentoConvenio.NombreArchivo;
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().DocumentoByte = DocumentoConvenio.registroMemoria;
            }
            else
            {
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().Mensaje = "Incorrecto";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().MensajeDetalle = "Disponible programa <b>Presencial Perú y Colombia, Extranjero Online sincrónico-asincrónico </b>, se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
            }

            //condiciones
            if (registroDocumentos.Tipo == "Online Asincronica" || registroDocumentos.Tipo == "Online Sincronica")
            {
                var DocumentoCondicion = GenerarConvenioCondicionDocumentoDTO(documentoObjDTO.Oportunidad, "Condiciones", 7);//idDocumento
                if (DocumentoCondicion != null)
                {
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Mensaje = "Correcto";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().MensajeDetalle = "Disponible para programas <b>Perú y Colombia Online sincrónico-asincrónico</b> se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Url = DocumentoCondicion.NombreArchivo;
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().DocumentoByte = DocumentoCondicion.registroMemoria;
                }
                else
                {
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Mensaje = "Incorrecto";
                    documentoObjDTO.ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().MensajeDetalle = "Disponible para programas <b>Perú y Colombia Online sincrónico-asincrónico</b> se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
                }
            }
            else
            {
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Mensaje = "Incorrecto";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().MensajeDetalle = "Disponible para programas <b>Perú y Colombia Online sincrónico-asincrónico</b> se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
            }

            //Silabo
            string DocumentoSilabo = GeneraSilabo(registroDocumentos);
            if (!string.IsNullOrEmpty(DocumentoSilabo))
            {
                string _urlConcatenada = registroDocumentos.IdProgramaGeneral + "*" + DocumentoSilabo;
                var DocumentoSilaboPDF = GenerarSilaboBytes(registroDocumentos);
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().Mensaje = "Correcto";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().MensajeDetalle = "Disponible para todos los programas / Programa nuevo, pendiente sílabo";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().Url = DocumentoSilaboPDF.NombreArchivo;//_urlConcatenada;
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().DocumentoByte = DocumentoSilaboPDF.registroMemoria;//DocumentoSilaboPDF;
            }
            else
            {
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().Mensaje = "Incorrecto";
                documentoObjDTO.ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().MensajeDetalle = "Disponible para todos los programas / Programa nuevo, pendiente sílabo";
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Generar Documento Silabo
        /// </summary>
        /// <param name="oportunidad">Campos de Oportunidad</param>
        /// <returns></returns>
        private byte[] GenerarRequisitos(DatosOportunidadDocumentosCompuestoDTO oportunidad, int idDocumento)
        {
            string modalidad = string.Empty;
            string NombrePrograma = string.Empty;
            string Requisitos = string.Empty;
            string _queryAlumno = string.Empty;
            int Pais = 0, IdProgramaEspecifico = 0;
            byte[] RequisitosByte = new byte[100];

            AlumnoService alumnoService = new AlumnoService(_unitOfWork);
            List<AlertDTO> listadoAlertas = new List<AlertDTO>();
            DatosOportunidadDocumentosCompuestoDTO Oportunidad = new DatosOportunidadDocumentosCompuestoDTO();

            var al = alumnoService.ObtenerCiudadPaisPorIdAlumno(Oportunidad.IdAlumno.Value);

            if (al.IdCiudad == null || al.IdCodigoPais == null)
            {
                if (al.IdCodigoPais == null)
                {
                    listadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(207) });
                }
                if (al.IdCiudad == null)
                {
                    listadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(208) });
                }
                return null;
            }

            AlumnoDatosDocumentoDTO Alumno = alumnoService.ObtenerDatosAlumnoDocumentoPorId(oportunidad.IdAlumno.Value);

            try
            {
                CuotasProgramaDTO listaCuotasProgramaDTO = ObtenerCuotasPrograma(oportunidad.Id);
                modalidad = listaCuotasProgramaDTO.TipoPrograma;
                NombrePrograma = listaCuotasProgramaDTO.NombreCurso;
                IdProgramaEspecifico = listaCuotasProgramaDTO.IdPespecifico;

                if (listaCuotasProgramaDTO.NombrePespecifico.IndexOf("BOGOTA") != -1)
                {
                    Pais = 57;
                }
                else
                {
                    Pais = 51;
                }
                switch (modalidad)
                {
                    case "Online Asincronica":
                        RequisitosByte = GenerarDocumentoRequisitosOnline(Alumno, NombrePrograma, "Online Asincronica");
                        break;
                    case "Online Sincronica":
                        RequisitosByte = GenerarDocumentoRequisitosOnline(Alumno, NombrePrograma, "Online Sincronica");
                        break;
                }
                return RequisitosByte;
            }
            catch (Exception e)
            {

            }
            return null;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Contenido de Documento Requisitos
        /// </summary>
        /// <param name="alumno"> campos de Alumno</param>
        /// <param name="nombrePrograma"></param>
        /// <param name="modalidad">modalidad Online Asincronica,Online Sincronica</param>
        /// <returns></returns>
        private byte[] GenerarDocumentoRequisitosOnline(AlumnoDatosDocumentoDTO alumno, string nombrePrograma, string modalidad)
        {
            string nombreDocumento = string.Empty;
            string nombreDocumentoTemp = string.Empty;
            string subCabecera = string.Empty;
            string nombres, apellidos;
            string direccionRequisitosSH = @"~/repositorioweb/documentos/requisitos-sh/";
            string contenido = string.Empty;
            FontFactory.RegisterDirectories();
            Document document = new Document(PageSize.A4, 70, 50, 50, 50);
            try
            {
                if (modalidad == "Online Asincronica")
                {
                    nombreDocumento = direccionRequisitosSH + "declaracion-jurada-asincronico " + alumno.Nombre1 + alumno.ApellidoPaterno + ".pdf";
                    nombreDocumentoTemp = "declaracion-jurada-asincronico " + alumno.Nombre1 + alumno.ApellidoPaterno + ".pdf";

                    DocumentoComercialContenidoDTO contenidoDocumento = _unitOfWork.DocumentacionComercialPwRepository.DocumentoComercialContenido("Requisitos", "OnlineAsincronico", 0);

                    contenido = contenidoDocumento.Contenido;
                    subCabecera = "Declaración jurada para programas asincrónicos";
                }
                else
                {
                    nombreDocumento = direccionRequisitosSH + "requisitos-hadware-software " + alumno.Nombre1 + alumno.ApellidoPaterno + ".pdf";
                    nombreDocumentoTemp = "requisitos-hadware-software " + alumno.Nombre1 + alumno.ApellidoPaterno + ".pdf";

                    DocumentoComercialContenidoDTO contenidoDocumento = _unitOfWork.DocumentacionComercialPwRepository.DocumentoComercialContenido("Requisitos", "Online", 0);

                    contenido = contenidoDocumento.Contenido;
                    subCabecera = "Requisitos de hardware y software para participar en programas online";
                }

                //primero seteamos la informacion del alumno
                nombres = alumno.Nombre1 + " " + alumno.Nombre2;
                apellidos = alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno;

                contenido = contenido.Replace("##NOMBRESALUMNO##", nombres + " " + apellidos).Replace("##NRODOCUMENTO##", alumno.Dni);
                contenido = contenido.Replace("##NOMBREPROGRAMA##", nombrePrograma);

                HTMLWorker htmlparser = new HTMLWorker(document);
                htmlparser.SetStyleSheet(GenerateStyleSheet2());

                //PdfWriter.GetInstance(document, new FileStream(System.Web.HttpContext.Current.Server.MapPath(NombreDocumento), FileMode.OpenOrCreate));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();


                PdfWriter.GetInstance(document, ms);
                document.Open();

                PdfPTable table = new PdfPTable(3);
                table.TotalWidth = 500f;
                int[] firstTablecellwidth = { 60, 350, 90 };
                table.SetWidths(firstTablecellwidth);
                table.LockedWidth = true;

                table.AddCell(" ");

                Chunk titleFont1 = new Chunk("SISTEMA DE GESTIÓN DE LA CALIDAD", FontFactory.GetFont("Times New Roman", 10));
                Chunk titleFont2 = new iTextSharp.text.Chunk(subCabecera, FontFactory.GetFont("Times New Roman", 10, Font.BOLD));

                Paragraph titulo1 = new Paragraph(titleFont1);
                titulo1.Alignment = Element.ALIGN_CENTER;
                Paragraph titulo2 = new Paragraph(titleFont2);
                titulo2.Alignment = Element.ALIGN_CENTER;

                PdfPTable columna2 = new PdfPTable(1);
                columna2.AddCell(titulo1);
                columna2.AddCell(titulo2);

                PdfPCell columnaFomat1 = new PdfPCell(columna2);
                columnaFomat1.Padding = 0f;

                table.AddCell(columnaFomat1);

                Chunk col31 = new iTextSharp.text.Chunk("RE-LAN-020", FontFactory.GetFont("Times New Roman", 10));
                Chunk col32 = new iTextSharp.text.Chunk("Revisión 00", FontFactory.GetFont("Times New Roman", 10));
                Chunk col33 = new iTextSharp.text.Chunk("14 feb 2012", FontFactory.GetFont("Times New Roman", 10));
                Paragraph titCel1 = new Paragraph(col31);
                Paragraph titCel2 = new Paragraph(col32);
                Paragraph titCel3 = new Paragraph(col33);

                PdfPTable columna3 = new PdfPTable(1);
                columna3.AddCell(titCel1);
                columna3.AddCell(titCel2);
                columna3.AddCell(titCel3);

                PdfPCell columnaFomat2 = new PdfPCell(columna3);
                columnaFomat2.HorizontalAlignment = Element.ALIGN_CENTER;
                columnaFomat2.Padding = 0f;

                table.AddCell(columnaFomat2);
                document.Add(table);

                var fc = new StringReader(contenido);
                htmlparser.Parse(fc);

                document.Add(new iTextSharp.text.Paragraph(" "));
                document.Add(new iTextSharp.text.Paragraph(" "));
                document.Add(new iTextSharp.text.Paragraph(" "));
                document.Add(new iTextSharp.text.Paragraph(" "));
                document.Add(new iTextSharp.text.Paragraph("_______________"));
                document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO   "));

                document.Close();
                byte[] bytesDocumento = ms.ToArray();
                return bytesDocumento;
            }
            catch (Exception ex)
            {
                document.Close();
                //return "LOG";
                return null;
            }
        }
        /// <summary>
        /// Genera estilos Para Documento Pre-requisitos
        /// </summary>
        /// <returns></returns>
        private static StyleSheet GenerateStyleSheet2()
        {
            FontFactory.RegisterDirectories();

            StyleSheet css = new StyleSheet();

            css.LoadTagStyle("h1", "size", "10pt");
            css.LoadTagStyle("h1", "style", "text-align:cen1ter;font-weight:bold;");
            css.LoadTagStyle("p", "style", "text-align:justify;");
            css.LoadTagStyle("div", "size", "10pt");
            css.LoadTagStyle("ul", "style", "display:block;list-style-type:circle;");
            css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDER, "0.1");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H1, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTSIZE, "10px");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTSIZE, "10px");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTSIZE, "10px");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTSIZE, "10px");
            return css;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Las Cuotas de un programa
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public CuotasProgramaDTO ObtenerCuotasPrograma(int idOportunidad)
        {
            MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
            MatriculaCabeceraCodigoFechaDTO idMatricula = matriculaCabeceraService.CodigoMatriculaPorIdOportunidad(idOportunidad);

            if (idMatricula != null)
            {
                return ObtenerCronograma(idMatricula.Id);
            }
            else
            {
                MatriculaTemporalDTO IdMatricula2 = matriculaCabeceraService.ObtenerMatriculaPorOportunidad(idOportunidad);
                if (IdMatricula2 == null)
                    return null;
                return ObtenerCronograma(IdMatricula2.IdMatricula);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene cronograma.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> CuotasProgramaDTO </returns>
        public CuotasProgramaDTO ObtenerCronograma(int id)
        {
            PGeneralService pGeneralService = new PGeneralService(_unitOfWork);
            CuotasProgramaDTO listaCuotasProgramaDTO = pGeneralService.ObtenerProgramaParaCuotas(id);

            if (listaCuotasProgramaDTO != null)
            {
                listaCuotasProgramaDTO.ListaCuotas = new List<ProgramaListaCuotaDTO>();
                CronogramaPagoDetalleFinalService cronogramaPagodetalleFinalService = new CronogramaPagoDetalleFinalService(_unitOfWork);
                List<ProgramaListaCuotaDTO> CronogramaPagoDetalleMod = cronogramaPagodetalleFinalService.ObtenerListaCuotaPrograma(listaCuotasProgramaDTO.IdMatricula);

                if (CronogramaPagoDetalleMod != null)
                {
                    foreach (var cronogramaPagoDetalleMod in CronogramaPagoDetalleMod)
                    {
                        listaCuotasProgramaDTO.ListaCuotas.Add(cronogramaPagoDetalleMod);
                    }
                }

                decimal Porcentaje = 0.005M;

                foreach (var cuotaT in listaCuotasProgramaDTO.ListaCuotas)
                {
                    int NroDias = Convert.ToInt32((DateTime.Now.Date - cuotaT.FechaVencimiento.Value).TotalDays);
                    if (NroDias > 0 && cuotaT.Cancelado.Value == false)
                    {
                        cuotaT.Mora = cuotaT.Mora + decimal.Round(((cuotaT.Cuota.Value + cuotaT.Mora.Value) * Porcentaje) * NroDias, 2, MidpointRounding.AwayFromZero);
                    }
                }

                if (listaCuotasProgramaDTO.WebMoneda == "2")
                {
                    foreach (var cuotaT in listaCuotasProgramaDTO.ListaCuotas)
                    {
                        cuotaT.Cuota = cuotaT.MontoCuotaDescuento;
                        cuotaT.Mora = Math.Round(cuotaT.Mora.Value * listaCuotasProgramaDTO.WebTipoCambio.Value, 2, MidpointRounding.AwayFromZero);
                        cuotaT.Moneda = "Pesos Colombianos";
                    }
                }
                return listaCuotasProgramaDTO;
            }
            else
            {
                return listaCuotasProgramaDTO;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary> 
        /// Descripcion: Genera Convenio de condicion.
        /// </summary> 
        private ArchivosAlumnoDTO GenerarConvenioCondicionDocumentoDTO(DatosOportunidadDocumentosCompuestoDTO oportunidad, string tipoDocumento, int idDocumento)
        {
            string modalidad = string.Empty;
            string nombrePrograma = string.Empty;
            ArchivosAlumnoDTO convenioCondicion = new ArchivosAlumnoDTO();
            int tipoPago = 0, pais = 0, idProgramaEspecifico = 0;

            AlumnoService alumnoService = new AlumnoService(_unitOfWork);

            var al = alumnoService.ObtenerCiudadPaisPorIdAlumno(oportunidad.IdAlumno.Value);

            if (al.IdCiudad == null || al.IdCodigoPais == null)
            {
                if (al.IdCodigoPais == null)
                {
                    ListadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(207) });
                }
                if (al.IdCiudad == null)
                {
                    ListadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(208) });
                }
                return null;
            }

            AlumnoDatosDocumentoDTO alumnoDTO = alumnoService.ObtenerDatosAlumnoDocumentoPorId(oportunidad.IdAlumno.Value);
            MontoPagoCronogramaDocumentoDTO montoPagoCronogramaDTO = _unitOfWork.MontoPagoCronogramaRepository.ObtenerConogramaPagoDocumentoPorIdOportunidad(oportunidad.Id);

            if (montoPagoCronogramaDTO.IdMoneda != null && montoPagoCronogramaDTO.IdMontoPago != null && montoPagoCronogramaDTO.PrecioDescuento != null)
            {
                //Agregado para Etiquetas
                MonedaService monedaService = new MonedaService(_unitOfWork);
                MontoPagoService montoPagoService = new MontoPagoService(_unitOfWork);

                MonedaNombrePluralSimboloDTO moneda = monedaService.ObtenerMonedaPorId(montoPagoCronogramaDTO.IdMoneda.Value);
                oportunidad.CostoTotalConDescuento = moneda.Simbolo + " " + montoPagoCronogramaDTO.PrecioDescuento + " " + moneda.NombrePlural;
                MontoPagoPaqueteDTO montoPago = montoPagoService.ObtenerPaquete(montoPagoCronogramaDTO.IdMontoPago.Value);

                if (montoPago != null)
                {
                    alumnoDTO.Paquete = montoPago.Paquete == null ? "" : montoPago.Paquete.ToString();
                }
                else
                {
                    alumnoDTO.Paquete = "";
                }
            }
            else
            {
                alumnoDTO.Paquete = "";
            }
            try
            {
                CuotasProgramaDTO listaCuotasProgramaDTO = ObtenerCuotasPrograma(oportunidad.Id);
                if (listaCuotasProgramaDTO == null)
                {
                    return null;
                }
                modalidad = listaCuotasProgramaDTO.TipoPrograma;
                nombrePrograma = listaCuotasProgramaDTO.NombreCurso;
                idProgramaEspecifico = listaCuotasProgramaDTO.IdPespecifico;

                if (alumnoDTO != null)
                {
                    if (alumnoDTO.IdCodigoPais == 57)
                    {
                        pais = 57;
                    }
                    else if (alumnoDTO.IdCodigoPais == 591)
                    {
                        pais = 591;
                    }
                    else
                    {
                        pais = 51;
                    }
                }
                else
                {
                    pais = 51;
                }
                alumnoDTO.IdOportunidad = oportunidad.Id;
                if (tipoDocumento == "Convenio")
                {
                    switch (modalidad)
                    {
                        case "Presencial":
                            convenioCondicion = GenerarPDFconvenioCondicionesDocumento(alumnoDTO, listaCuotasProgramaDTO, tipoPago, pais, tipoDocumento, "Presencial");
                            break;
                        case "Online Asincronica":
                            convenioCondicion = GenerarPDFconvenioCondicionesDocumento(alumnoDTO, listaCuotasProgramaDTO, tipoPago, pais, tipoDocumento, "Online");
                            break;
                        case "Online Sincronica":
                            convenioCondicion = GenerarPDFconvenioCondicionesDocumento(alumnoDTO, listaCuotasProgramaDTO, tipoPago, pais, tipoDocumento, "Online");
                            break;
                    }
                    return convenioCondicion;
                }
                else
                {
                    switch (modalidad)
                    {
                        case "Online Asincronica":
                            convenioCondicion = GenerarPDFconvenioCondicionesDocumento(alumnoDTO, listaCuotasProgramaDTO, tipoPago, pais, tipoDocumento, "Online");
                            break;
                        case "Online Sincronica":
                            convenioCondicion = GenerarPDFconvenioCondicionesDocumento(alumnoDTO, listaCuotasProgramaDTO, tipoPago, pais, tipoDocumento, "Online");
                            break;
                    }
                    return convenioCondicion;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera PDF de convenio por condiciones.
        /// </summary>
        /// <param name="alumno"></param>
        /// <param name="listaCuotasProgramaDTO"></param>
        /// <param name="tipoPago"></param>
        /// <param name="pais"></param>
        /// <param name="tipoDocumento"></param>
        /// <param name="modalidad"></param>
        /// <returns></returns>
        private ArchivosAlumnoDTO GenerarPDFconvenioCondicionesDocumento(AlumnoDatosDocumentoDTO alumno, CuotasProgramaDTO listaCuotasProgramaDTO, int tipoPago, int pais, string tipoDocumento, string modalidad)
        {
            ArchivosAlumnoDTO respuesta = new ArchivosAlumnoDTO();
            try
            {
                string nombreArchivo = "";
                string blobDirecion = "";
                string raiz = "";
                string urlDocumento = "https://repositorioweb.blob.core.windows.net/";

                if (tipoDocumento == "Convenio")
                {
                    nombreArchivo = "Convenio-capacitacion-formacion-" + alumno.Id + alumno.Nombre1.ToLower();
                    blobDirecion = @"documentos/convenios";
                }
                else
                {
                    nombreArchivo = "Condiciones-caracteristicas-servicio-" + alumno.Id + alumno.Nombre1.ToLower();
                    blobDirecion = @"documentos/condiciones";
                }

                byte[] pdf = BlobGenerarconvenioCondiciones(alumno, listaCuotasProgramaDTO, tipoPago, pais, tipoDocumento, modalidad);

                RegistrarDocumentoBlob(pdf, nombreArchivo, blobDirecion, "pdf");

                raiz = ToURLSlug(nombreArchivo) + ".pdf";
                //return raiz;

                respuesta.NombreArchivo = urlDocumento + blobDirecion + "/" + raiz;
                respuesta.registroMemoria = pdf;

                return respuesta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera convenio de condiciones, contrato mexico,lineamientos mexico,convenio de mexico.
        /// </summary>
        /// <param name="alumno"></param>
        /// <param name="listaCuotasProgramaDTO"></param>
        /// <param name="tipoPago"></param>
        /// <param name="pais"></param>
        /// <param name="tipoDocumento"></param>
        /// <param name="modalidad"></param>
        /// <returns> byte[] </returns>
        private byte[] BlobGenerarconvenioCondiciones(AlumnoDatosDocumentoDTO alumno, CuotasProgramaDTO listaCuotasProgramaDTO, int tipoPago, int pais, string tipoDocumento, string modalidad)
        {
            DateTime fecha_pago = new DateTime();
            string nombreDocumento = string.Empty, formatoPagos = string.Empty, simboloMoneda = string.Empty, totalPago = string.Empty;
            string nombres, apellidos, nombreMoneda;
            int tipoPais;
            decimal tipoCambio = 0;
            decimal pagoCuota = 0;
            string _queryContenidoDocumento = string.Empty;
            string DireccionConvenio = @"~/repositorioweb/documentos/convenios/";
            string DireccionCondicion = @"~/repositorioweb/documentos/condiciones/";
            MontoPagoService montoPagoService = new MontoPagoService(_unitOfWork);
            PGeneralService pGeneralService = new PGeneralService(_unitOfWork);
            DocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);

            //Creacion del archivo en memoria
            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (iTextSharp.text.Document documento = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 70, 50, 50, 50))
                {

                    FontFactory.GetFont("Times New Roman", 14, iTextSharp.text.BaseColor.BLACK);

                    var firmas = FontFactory.GetFont("Times New Roman", 14);


                    try
                    {
                        if (pais == 57)
                        {
                            tipoPais = 57;
                        }
                        else if (pais == 591)
                        {
                            tipoPais = 591;
                        }
                        else if (pais == 52)
                        {
                            tipoPais = 52;
                        }
                        else if (pais == 56)
                        {
                            tipoPais = 56;
                        }
                        else
                        {
                            tipoPais = 51;
                        }
                        DocumentoComercialContenidoDTO contenidoDocumento = _unitOfWork.DocumentacionComercialPwRepository.DocumentoComercialContenido(tipoDocumento, modalidad, tipoPais);

                        if (contenidoDocumento == null)
                        {
                            return null;
                        }

                        documentoObjDTO.Contenido = contenidoDocumento.Contenido;


                        if (tipoDocumento == "Convenio" || tipoDocumento == "Contrato" || tipoDocumento == "Lineamiento" || tipoDocumento == "Convenio Prestacion")
                        {
                            nombreDocumento = DireccionConvenio + "Convenio-capacitación-formación-" + alumno.Id + alumno.Nombre1.ToLower() + ".pdf";
                        }
                        else
                        {
                            nombreDocumento = DireccionCondicion + "Condiciones-caracteristicas-servicio-" + alumno.Id + alumno.Nombre1.ToLower() + ".pdf";
                        }

                        nombres = alumno.Nombre1 + " " + alumno.Nombre2;
                        apellidos = alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno;

                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##PRIMERNOMBREALUMNO##", alumno.Nombre1.ToUpper());
                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##NOMBRESALUMNO##", nombres.ToUpper() + " " + apellidos.ToUpper()).Replace("##TIPONRODOCUMENTO##", alumno.Dni);
                        int codpais = 0;
                        if (alumno.IdCodigoPais != null)
                        {
                            codpais = Convert.ToInt32(alumno.IdCodigoPais);
                        }

                        PaisService _repPais = new PaisService(_unitOfWork);
                        StringDTO pais__ = _unitOfWork.PaisRepository.ObtenerNombrePaisPorId(codpais);

                        string pais_ = pais__.Valor;

                        int codciudad = 0;
                        if (alumno.IdCiudad != null)
                        {
                            codciudad = Convert.ToInt32(alumno.IdCiudad);
                        }

                        CiudadService _repCiudad = new CiudadService(_unitOfWork);
                        StringDTO ciudad__ = _repCiudad.ObtenerNombreCiudadPorId(codciudad);

                        string ciudad_ = ciudad__.Valor;

                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##DIRECCION##", alumno.Direccion).Replace("##CIUDAD##", ciudad_);
                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##REGION##", alumno.NombreCiudad).Replace("##PAIS##", pais_);
                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##CORREO##", alumno.Correo);

                        MatriculaTemporalDTO matricula_ = new MatriculaTemporalDTO();

                        if (alumno.Paquete == null)
                        {
                            alumno.Paquete = "";
                        }

                        MatriculaCabeceraService _repMatriculaCabecera = new MatriculaCabeceraService(_unitOfWork);
                        MatriculaCabeceraCodigoFechaDTO matricula = _repMatriculaCabecera.CodigoMatriculaPorIdOportunidad(alumno.IdOportunidad.Value);

                        if (matricula != null)
                        {
                            documentoObjDTO.Oportunidad.IdMatricula = matricula.CodigoMatricula;
                            matricula_.IdMatricula = matricula.Id;
                            matricula_.CodigoMatricula = matricula.CodigoMatricula;
                            matricula_.FechaMatricula = matricula.FechaMatricula;
                        }
                        else
                        {
                            matricula_ = _repMatriculaCabecera.ObtenerMatriculaPorOportunidad(alumno.IdOportunidad.Value);

                            documentoObjDTO.Oportunidad.IdMatricula = matricula_.CodigoMatricula;
                        }

                        DateTime? fecha_matri = new DateTime(2017, 9, 10);
                        if (matricula_ != null)
                        {
                            fecha_matri = matricula_.FechaMatricula;
                        }

                        DateTime validador_fecha = new DateTime(2017, 9, 11);

                        if (fecha_matri < validador_fecha)
                        {
                            documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##VERSION##", "");
                        }
                        else
                        {
                            documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##VERSION##", GenerarVersion(alumno.Paquete));
                        }

                        int? idBusqueda = 0;

                        //segundo remplazamos los datos del programa y del cronograma

                        idBusqueda = listaCuotasProgramaDTO.IdBusqueda;

                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##NOMBREPROGRAMA##", listaCuotasProgramaDTO.NombreCurso);
                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##DURACIONDIAS##", listaCuotasProgramaDTO.DuracionPespecifico + " horas");
                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##DURACIONMESES##", listaCuotasProgramaDTO.DuracionPGeneral + "");

                        if (listaCuotasProgramaDTO.NumeroCuotas != 1)
                        {
                            documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##FRACCIONADO##,", "en caso de solicitar el pago fraccionado, deberá pagar la suma señalada en el numeral 4 anterior");
                        }
                        else
                        {
                            documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##FRACCIONADO##,", ""); ;
                        }

                        formatoPagos += "<br>";

                        if (true)
                        {
                            switch (listaCuotasProgramaDTO.WebMoneda)
                            {
                                case "0":
                                    simboloMoneda = "S/";
                                    nombreMoneda = "Soles";
                                    totalPago = listaCuotasProgramaDTO.TotalPagar.ToString();
                                    break;
                                case "1":
                                    simboloMoneda = "US$";
                                    nombreMoneda = "Dolares";
                                    totalPago = listaCuotasProgramaDTO.TotalPagar.ToString();
                                    break;
                                case "2":
                                    simboloMoneda = "COP";
                                    nombreMoneda = "Colombianos";
                                    totalPago = listaCuotasProgramaDTO.WebTotalPagar.ToString();
                                    tipoCambio = listaCuotasProgramaDTO.WebTotalPagar.Value;
                                    break;
                                case "3":
                                    simboloMoneda = "BS";
                                    nombreMoneda = "Bolivianos";
                                    totalPago = listaCuotasProgramaDTO.WebTotalPagar.ToString();
                                    tipoCambio = listaCuotasProgramaDTO.WebTotalPagar.Value;
                                    break;
                                case "4":
                                    simboloMoneda = "MXN$";
                                    nombreMoneda = "Peso Mexicano";
                                    totalPago = listaCuotasProgramaDTO.WebTotalPagar.ToString();
                                    tipoCambio = listaCuotasProgramaDTO.WebTotalPagar.Value;
                                    break;
                            }

                            documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##MONTROTOTALCRONOGRAMA##", simboloMoneda + " " + totalPago + "");

                            if (((tipoDocumento == "Contrato" || tipoDocumento == "Lineamiento" || tipoDocumento == "Convenio Prestacion") && codpais == 52) || tipoDocumento == "Condiciones")
                            {
                                formatoPagos += "<strong>Nro. Cuota &nbsp;&nbsp; Monto(" + simboloMoneda + " " + totalPago + ") &nbsp;&nbsp; Fecha de vencimiento</strong><br>";
                                foreach (var pagos in listaCuotasProgramaDTO.ListaCuotas)
                                {
                                    if (listaCuotasProgramaDTO.WebMoneda == "2")
                                    {
                                        pagoCuota = pagos.Cuota.Value;
                                    }
                                    else
                                    {
                                        pagoCuota = pagos.Cuota.Value;
                                    }
                                    fecha_pago = pagos.FechaVencimiento.Value;
                                    formatoPagos += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + pagos.NroCuota + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + simboloMoneda + " " + pagoCuota.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + fecha_pago.ToString("dd/MM/yyyy") + "<br>";
                                }

                            }
                            else
                            {
                                if (listaCuotasProgramaDTO.NumeroCuotas == 1)
                                {
                                    formatoPagos = "EL ALUMNO, se obliga a cubrir el costo del PROGRAMA el cual asciende a un valor de " + simboloMoneda + " " + totalPago + " a ser cancelados en una sola cuota antes del ";
                                    foreach (var pagos in listaCuotasProgramaDTO.ListaCuotas)
                                    {
                                        fecha_pago = pagos.FechaVencimiento.Value;
                                    }
                                    formatoPagos += fecha_pago.ToString("dd/MM/yyyy") + ".";
                                }
                                else
                                {
                                    formatoPagos = "EL ALUMNO, se obliga a cubrir el costo del PROGRAMA el cual asciende a un valor de " + simboloMoneda + " " + totalPago + ". Para el caso en concreto, el alumno ha escogido la modalidad de pago en varias cuotas, por lo que se compromete a pagar el siguiente esquema de cuotas:<br><br>";
                                    formatoPagos += "<strong>Nro. Cuota &nbsp;&nbsp; Monto(" + simboloMoneda + " " + totalPago + ") &nbsp;&nbsp; Fecha de vencimiento</strong><br>";
                                    foreach (var pagos in listaCuotasProgramaDTO.ListaCuotas)
                                    {
                                        if (listaCuotasProgramaDTO.WebMoneda == "2")
                                        {
                                            pagoCuota = pagos.Cuota.Value;
                                        }
                                        else
                                        {
                                            pagoCuota = pagos.Cuota.Value;
                                        }
                                        fecha_pago = pagos.FechaVencimiento.Value;
                                        formatoPagos += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + pagos.NroCuota + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + simboloMoneda + " " + pagoCuota.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + fecha_pago.ToString("dd/MM/yyyy") + "<br>";
                                    }
                                }
                            }


                        }

                        PgeneralDocumentoSeccionDTO programa = new PgeneralDocumentoSeccionDTO();
                        string anexo1 = string.Empty;
                        string anexo2 = string.Empty;
                        string anexo3CasoExcepcional = string.Empty;
                        if (idBusqueda != 0)
                        {

                            programa = pGeneralService.ObtenePgeneralPorIdBusqueda(idBusqueda.Value);
                            var listaSecciones = ObtenerListaSeccionDocumentoProgramaGeneralVersion(programa.Id, alumno.Paquete);
                            var seccion = GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                            if (programa.ListaSeccionV2 == null)
                            {
                                programa.ListaSeccionV2 = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
                            }
                            foreach (var item in seccion)
                            {
                                programa.ListaSeccionV2.Add(item);
                            }
                        }
                        if (programa.ListaSeccionV2.Count > 0) //Imprime estructura curricular de la version v2
                        {
                            foreach (var item in programa.ListaSeccionV2)
                            {
                                if (item.Seccion.Contains("Estructura Curricular"))
                                {
                                    anexo1 = "<h1>ANEXO 01</h1><br><h3>" + item.Seccion + "</h3><br/>";
                                    anexo1 += item.Contenido;
                                }
                                if (item.Seccion.Contains("Certificacion"))
                                {
                                    anexo2 = "<h1>ANEXO 02</h1><br><h3>" + item.Seccion + "</h3><br/>";
                                    anexo2 += item.Contenido;
                                }
                            }
                        }
                        else //Imprime estructura curricular de la version 1
                        {
                            if (idBusqueda != 0)
                            {
                                programa = pGeneralService.ObtenePgeneralPorIdBusqueda(idBusqueda.Value);

                                List<SeccionDocumentoDTO> seccion = documentoSeccionPwService.ObtenerSecciones(programa.Id);
                                if (programa.ListaSeccion == null)
                                {
                                    programa.ListaSeccion = new List<SeccionDocumentoDTO>();
                                }
                                foreach (var item in seccion)
                                {
                                    programa.ListaSeccion.Add(item);
                                }
                            }

                            foreach (var item in programa.ListaSeccion)
                            {
                                if (item.Titulo.Contains("Estructura Curricular"))
                                {
                                    anexo1 = "<h1>ANEXO 01</h1><h2>" + item.Titulo + "</h2><br/>";
                                    anexo1 += item.Contenido;
                                }
                                if (item.Titulo.Contains("Certificación"))
                                {
                                    anexo2 = "<h1>ANEXO 02</h1><h2>" + item.Titulo + "</h2><br/>";
                                    anexo2 += item.Contenido;
                                }
                            }
                        }


                        int? idPaquete = null;
                        if (!string.IsNullOrEmpty(alumno.Paquete))
                        {
                            idPaquete = Convert.ToInt32(alumno.Paquete);
                        }
                        List<string> listaBeneficios = new List<string>();
                        var anexo3 = "";

                        if (matricula_ != null)
                        {
                            listaBeneficios = ObtenerBeneficiosCongeladosPorMatriculaCabecera(matricula_.IdMatricula);
                        }
                        if (listaBeneficios.Count == 0)
                        {
                            listaBeneficios = ObtenerBeneficiosConfiguradosProgramaGeneral(programa.Id, alumno.IdCodigoPais, idPaquete);
                        }

                        if (listaBeneficios.Count > 0) //Imprime beneficios de la Version 2
                        {
                            anexo3 += "<h1>ANEXO 03</h1><br/>";
                            switch (alumno.Paquete)
                            {
                                case "1":
                                    anexo3 += "<h4><strong><b>Version Basica</b></strong></h4><br/>";
                                    break;
                                case "2":
                                    anexo3 += "<h4><strong><b>Version Profesional</b></strong></h4><br/>";
                                    break;
                                case "3":
                                    anexo3 += "<h4><strong><b>Version Gerencial</b></strong></h4><br/>";
                                    break;
                                default:
                                    anexo3 += "<h4><strong><b>Beneficios</b></strong></h4><br/>";
                                    break;
                            }

                            anexo3 += "<ul type = 'disc'>";
                            foreach (var item in listaBeneficios)
                            {
                                var beneficio = item;
                                beneficio = beneficio.Replace("<p>", "");
                                beneficio = beneficio.Replace("</p>", "");
                                anexo3 += "<li>&bull;&nbsp;&nbsp;&nbsp;" + beneficio + "</li>";
                            }
                            anexo3 += "</ul>";
                        }
                        else //Imprime beneficios de la version 1
                        {
                            var x = _unitOfWork.MontoPagoRepository.ObtenerBeneficiosAnexo03(programa.Id, alumno.IdCodigoPais.Value).ToList();
                            if (x.Count > 0)
                            {
                                anexo3 += "<h1>ANEXO 03</h1><br/>";
                                switch (alumno.Paquete)
                                {
                                    case "1":
                                        anexo3 += "<h2>Version Basica</h2><br/><ul>";
                                        break;
                                    case "2":
                                        anexo3 += "<h2>Version Profesional</h2><br/><ul>";
                                        break;
                                    case "3":
                                        anexo3 += "<h2>Version Gerencial</h2><br/><ul>";
                                        break;
                                }
                                foreach (var item in x)
                                {
                                    if (item.Beneficios.Contains("Todos los beneficios"))
                                    {
                                        item.Beneficios = "";
                                    }
                                    if (!string.IsNullOrEmpty(alumno.Paquete) && int.Parse(alumno.Paquete) == 1)
                                    {
                                        if (item.Paquete == 1)
                                        {
                                            anexo3 += "<li>" + item.Beneficios + "</li>";
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(alumno.Paquete) && int.Parse(alumno.Paquete) == 2)
                                    {
                                        if (item.Paquete == 2)
                                        {
                                            anexo3 += "<li>" + item.Beneficios + "</li>";
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(alumno.Paquete) && int.Parse(alumno.Paquete) == 3)
                                    {
                                        if (programa.Id != ValorEstatico.IdProgramaGeneralFFISO27001 && programa.Id != ValorEstatico.IdProgramaGeneralFFISO9001 &&
                                            programa.Id != ValorEstatico.IdProgramaGeneralDSIG && programa.Id != ValorEstatico.IdProgramaGeneralFFISO45001 &&
                                            programa.Id != ValorEstatico.IdProgramaGeneralFFISO37001)
                                            anexo3 += "<li>" + item.Beneficios + "</li>";
                                        else
                                        {
                                            if (item.Paquete == 3)
                                            {
                                                anexo3 += "<li>" + item.Beneficios + "</li>";
                                            }
                                        }
                                    }
                                }
                                anexo3 += "</ul>";
                            }
                        }

                        formatoPagos += "<br>";
                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##TIPOPAGO##", formatoPagos);
                        DateTime hoy = DateTime.Now;
                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("## DATEMONTH ##", hoy.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")));
                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##DATEDAYS##", hoy.Day.ToString());
                        documentoObjDTO.Contenido = documentoObjDTO.Contenido.Replace("##DATEYEAR##", hoy.Year.ToString());

                        HTMLWorker htmlparser = new HTMLWorker(documento);
                        htmlparser.SetStyleSheet(GenerateStyleSheet());

                        PdfWriter.GetInstance(documento, ms);
                        documento.Open();

                        htmlparser.Parse(new StringReader(documentoObjDTO.Contenido));

                        if (codpais == 591)
                        {
                            documento.Add(new iTextSharp.text.Paragraph("_______________          ___________           ________________________", firmas));
                            documento.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                  Huella Digital      BSGINSTITUTE BOLIVIA S.R.L.", firmas));
                        }
                        else if (codpais == 57)
                        {
                            if (tipoDocumento != "Condiciones")
                            {
                                documento.Add(new iTextSharp.text.Paragraph("_______________          ___________           ________________________", firmas));
                                documento.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                  Huella Digital       BS GRUPO COLOMBIA S.A.S.", firmas));
                            }
                        }
                        else if (codpais == 52)
                        {
                            if (tipoDocumento == "Convenio Prestacion")
                            {
                                documento.Add(new iTextSharp.text.Paragraph("_______________          ___________           ________________________", firmas));
                                documento.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                  Huella Digital      BSGINSTITUTE. ", firmas));
                            }
                            else
                            {
                                //No va campos Firma
                            }

                        }
                        else
                        {
                            if (tipoDocumento != "Condiciones")
                            {
                                documento.Add(new iTextSharp.text.Paragraph("_______________           ____________                 __________________", firmas));
                                documento.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                   Huella Digital                    BS GRUPO S.A.C.", firmas));
                            }
                        }

                        if (codpais == 52)
                        {
                            if (tipoDocumento == "Contrato")
                            {
                                //No se adjunto anexo
                            }
                            else if (tipoDocumento == "Lineamiento")
                            {
                                if (!string.IsNullOrEmpty(anexo1))
                                {
                                    documento.NewPage();
                                    htmlparser.Parse(new StringReader(anexo1));
                                }
                            }
                            else if (tipoDocumento == "Convenio Prestacion")
                            {
                                if (!string.IsNullOrEmpty(anexo1))
                                {
                                    documento.NewPage();
                                    htmlparser.Parse(new StringReader(anexo1));
                                }
                                if (!string.IsNullOrEmpty(anexo2))
                                {
                                    documento.NewPage();
                                    htmlparser.Parse(new StringReader(anexo2));
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(anexo1))
                            {
                                documento.NewPage();
                                htmlparser.Parse(new StringReader(anexo1));
                            }
                            if (!string.IsNullOrEmpty(anexo2))
                            {
                                documento.NewPage();
                                htmlparser.Parse(new StringReader(anexo2));
                            }
                            if (!string.IsNullOrEmpty(anexo3))
                            {
                                documento.NewPage();
                                htmlparser.Parse(new StringReader(anexo3));
                            }
                        }
                        documento.Close();
                    }
                    catch (Exception ex)
                    {
                        LoggerMessage.DefineScope(ex.ToString());
                    }
                }
                return ms.ToArray();
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Version por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> string </returns>
        public string GenerarVersion(string id)
        {
            switch (id)
            {
                case "1":
                    return " -  VERSION BASICA ";
                case "2":
                    return " -  VERSION PROFESIONAL ";
                case "3":
                    return " -  VERSION GERENCIAL ";
                default:
                    return "";
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Metodo que retorna lista de secciones de documento del programa general apuntando a la nueva version por paquete
        /// </summary>
        /// <param name="idPGeneral">Id del programa general del cual se desea obtener las secciones del documento (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="version">Version de matricula alumno</param>
        /// <returns> List<ProgramaGeneralSeccionDocumentoDTO> </returns>
        public List<ProgramaGeneralSeccionDocumentoDTO> ObtenerListaSeccionDocumentoProgramaGeneralVersion(int idPGeneral, string version)
        {
            try
            {
                DocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);
                ProgramaGeneralDoumentoDTO programa = new ProgramaGeneralDoumentoDTO();

                var listaCursos = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijosVersion(idPGeneral, version);
                if (listaCursos.Count > 0)
                {
                    programa.EsProgramaPadre = true;
                    var secciones = documentoSeccionPwService.ObtenerSeccionDocumento(idPGeneral);
                    if (secciones != null && secciones.Count > 0)
                    {
                        programa.ListaSeccionesContenidosDocumento = secciones;
                    }
                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();

                    foreach (var item in listaCursos)
                    {
                        var seccionEstructura = documentoSeccionPwService.ObtenerSeccionDocumentoEstructuraCurricular(item.Id);

                        if (seccionEstructura != null && seccionEstructura.Count > 0)
                        {
                            programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                        }
                    }
                }
                else
                {
                    programa.EsProgramaPadre = false;

                    var secciones = documentoSeccionPwService.ObtenerSeccionDocumento(idPGeneral);

                    if (secciones != null && secciones.Count > 0)
                    {
                        programa.ListaSeccionesContenidosDocumento = secciones;
                    }

                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();


                    var seccionEstructura = documentoSeccionPwService.ObtenerSeccionDocumentoEstructuraCurricular(idPGeneral);

                    if (seccionEstructura != null && seccionEstructura.Count > 0)
                    {
                        programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                    }
                }

                List<ProgramaGeneralEstructuraAgrupadoDTO> contenido = new List<ProgramaGeneralEstructuraAgrupadoDTO>();
                if (programa.ListaSeccionesContenidosDocumentoEstructura != null && programa.ListaSeccionesContenidosDocumentoEstructura.Count > 0)
                {
                    if (programa.EsProgramaPadre)
                    {
                        var listaCursosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.GroupBy(x => new { x.IdPGeneral, x.NombreCurso, x.Titulo }).Select(x => new { x.Key.IdPGeneral, x.Key.NombreCurso, x.Key.Titulo }).ToList();
                        foreach (var itemCurso in listaCursosSecciones)
                        {
                            var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12 && x.IdPGeneral == itemCurso.IdPGeneral).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.IdPGeneral, x.Cabecera, x.PiePagina }).ToList();
                            ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                            obj.Seccion = itemCurso.Titulo;
                            obj.Titulo = itemCurso.NombreCurso;
                            obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();
                            foreach (var itemSecion in listaCapitulosSecciones)
                            {
                                ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                {
                                    Contenido = itemSecion.Key.Contenido,
                                    Cabecera = itemSecion.Key.Cabecera,
                                    PiePagina = itemSecion.Key.PiePagina
                                };
                                obj.DetalleContenido.Add(tmp);

                            }
                            contenido.Add(obj);
                        }
                    }
                    else
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.Titulo }).ToList();
                        foreach (var item in listaCapitulosSecciones)
                        {
                            ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                            var listaSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.Contenido == item.Key.Contenido).Select(x => x.NumeroFila).ToList();
                            var capituloSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.IdSeccionTipoDetalle_PW == 13 && listaSesiones.Contains(s.NumeroFila)).GroupBy(x => new { x.Contenido, x.NombreCurso, x.Cabecera, x.PiePagina }).ToList();
                            obj.Seccion = item.Key.Titulo;
                            obj.Titulo = item.Key.Contenido;
                            obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();

                            if (capituloSesiones != null && capituloSesiones.Count > 0)
                            {
                                foreach (var itemSecion in capituloSesiones)
                                {
                                    ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                    {
                                        Contenido = itemSecion.Key.Contenido,
                                        Cabecera = itemSecion.Key.Cabecera,
                                        PiePagina = itemSecion.Key.PiePagina
                                    };
                                    obj.DetalleContenido.Add(tmp);
                                }
                                contenido.Add(obj);
                            }
                            else
                            {
                                foreach (var itemSecion in capituloSesiones)
                                {
                                    ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                    {
                                        Contenido = item.Key.Contenido
                                    };
                                    obj.DetalleContenido.Add(tmp);
                                }
                                contenido.Add(obj);
                            }
                        }
                    }
                }
                if (programa.ListaSeccionesContenidosDocumento != null && programa.ListaSeccionesContenidosDocumento.Count > 0)
                {
                    var listaCursosSecciones = programa.ListaSeccionesContenidosDocumento.GroupBy(x => new { x.IdSeccionTipoDetalle_PW, x.Titulo }).Select(x => new { x.Key.IdSeccionTipoDetalle_PW, x.Key.Titulo }).ToList();
                    foreach (var itemCurso in listaCursosSecciones)
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumento.Where(x => x.IdSeccionTipoDetalle_PW == itemCurso.IdSeccionTipoDetalle_PW).GroupBy(x => new { x.Contenido, x.Cabecera, x.PiePagina }).ToList();
                        ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                        obj.Seccion = itemCurso.Titulo;
                        obj.Titulo = "";//itemCurso.Titulo;
                        obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();
                        foreach (var itemSecion in listaCapitulosSecciones)
                        {
                            ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                            {
                                Contenido = itemSecion.Key.Contenido,
                                Cabecera = itemSecion.Key.Cabecera,
                                PiePagina = itemSecion.Key.PiePagina
                            };
                            obj.DetalleContenido.Add(tmp);
                        }
                        contenido.Add(obj);
                    }
                }

                var listaProgramaGeneralDocumentoSeccion = contenido.GroupBy(x => x.Seccion).Select(x => new ProgramaGeneralSeccionDocumentoDTO
                {
                    Seccion = x.Key,
                    DetalleSeccion = x.GroupBy(y => new { y.Titulo, y.DetalleContenido }).Select(y => new ProgramaGeneralSeccionDocumentoDetalleDTO
                    {
                        Titulo = y.Key.Titulo,
                        Cabecera = y.Key.DetalleContenido.GroupBy(z => z.Cabecera).Select(z => z.Key).FirstOrDefault(),
                        PiePagina = y.Key.DetalleContenido.GroupBy(z => z.PiePagina).Select(z => z.Key).FirstOrDefault(),
                        DetalleContenido = y.Key.DetalleContenido.Select(z => z.Contenido).ToList()
                    }).ToList()
                }).ToList();

                return listaProgramaGeneralDocumentoSeccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera vista previa del certificado
        /// </summary>
        /// <param name="idPlantillaF"></param>
        /// <param name="idPlantillaP"></param>
        /// <param name="idPgeneral"></param>
        /// <returns> byte[] </returns>
        public byte[] GenerarVistaModeloCertificado(int idPlantillaF, int idPlantillaP, int idPgeneral)
        {

            MatriculaCabeceraDatosCertificadoService matriculaCabeceraDatosCertificadoService = new MatriculaCabeceraDatosCertificadoService(_unitOfWork);
            CertificadoGeneradoAutomaticoService certificadoGeneradoAutomaticoService = new CertificadoGeneradoAutomaticoService(_unitOfWork);
            DocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);
            PEspecificoService pEspecificoService = new PEspecificoService(_unitOfWork);
            PlantillaService plantillaService = new PlantillaService(_unitOfWork);
            PGeneralService pGeneralService = new PGeneralService(_unitOfWork);
            AlumnoService alumnoService = new AlumnoService(_unitOfWork);
            CertificadoGeneradoAutomaticoContenido contenidoCertificado = new CertificadoGeneradoAutomaticoContenido();
            string _estructuraCurricular = "";
            var pGeneral = pGeneralService.ObtenerPorId(idPgeneral);
            int pdu = pGeneralService.ObtenerPduPorIdPGeneral(idPgeneral) ?? -1;


            var listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();
            var plantilla = plantillaService.ObtenerPorId(idPlantillaF);
            var idPlantillaBase = plantilla.IdPlantillaBase;
            var plantillaBase = plantillaService.ObtenerPlantillaCorreo(idPlantillaF);
            var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

            foreach (var etiqueta in listaEtiqueta)
            {
                listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = "" });
            }

            List<string> sepacion = new List<string>();
            string FondoFrontalCertificado = "";
            string FondoReversoCertificado = "";
            string FondoFrontalConstancia = "";
            string FondoReversoConstancia = "";
            sepacion = plantillaBase.Cuerpo.Split("###").ToList();
            Dictionary<string, object> map = new Dictionary<string, object>();
            map["font_factory"] = new MyFontFactory();

            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                var config = new PdfGenerateConfig();
                if (idPlantillaBase == 12)/*Certificado*/
                {
                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginTop = 144;
                    config.MarginBottom = 30;
                    config.MarginLeft = 111;
                    config.MarginRight = 83;
                }
                else
                {
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginLeft = 60;
                    config.MarginRight = 60;
                    config.MarginTop = 60;
                    config.MarginBottom = 25;
                }

                int Conteo = 0;
                List<string> estructura = new List<string>();
                string remplazo = "";

                foreach (var item in sepacion)
                {
                    string direccionCodigoQR = "";
                    string tipoletra = "Times New Roman";
                    remplazo = item;
                    string searchString = "/*";
                    int startIndex = remplazo.IndexOf(searchString);
                    searchString = "*/";
                    int endIndex = 0;
                    string substring = "";

                    if (startIndex != -1)
                    {
                        endIndex = remplazo.IndexOf(searchString, startIndex);
                        substring = remplazo.Substring(startIndex + searchString.Length, endIndex - searchString.Length);
                    }
                    if (substring != null && substring != "")
                    {
                        tipoletra = substring;
                    }
                    else
                    {
                        searchString = "text-align:";
                        startIndex = remplazo.IndexOf(searchString);
                        searchString = ";";

                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                    }
                    if (item.Contains("repositorioweb"))
                    {
                        if (idPlantillaBase == 12)
                        {
                            FondoFrontalCertificado = item;
                        }
                        else
                        {
                            FondoFrontalConstancia = item;
                        }
                    }
                    if (item.Contains("acute"))
                    {
                        remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                    }
                    if (item.Contains("CODIGOQR"))
                    {
                        var url = certificadoGeneradoAutomaticoService.ObtenerCorrelativoCertificado().ToString();
                        var urlCodigo = "https://bsginstitute.com/informacionCertificado?cod=" + 0 + "." + url;

                        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                        QRCodeData qrDatos = qRCodeGenerator.CreateQrCode(urlCodigo, QRCodeGenerator.ECCLevel.Q);
                        QRCode qRCodigo = new QRCode(qrDatos);

                        System.Drawing.Bitmap qrImagen = qRCodigo.GetGraphic(7, System.Drawing.Color.Black, System.Drawing.Color.White, false);
                        System.Drawing.Image objImage = (System.Drawing.Image)qrImagen;

                        direccionCodigoQR = alumnoService.GuardarArchivosQR(ImageToByte2(objImage), "image/jpeg", url);
                        contenidoCertificado.CodigoQr = direccionCodigoQR;
                        remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='{direccionCodigoQR}' width=55 height=55  />");
                    }
                    if (item.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", pGeneral.Nombre.ToUpper());
                    }
                    if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                    {
                        remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", "BSGI2022");
                    }
                    if (item.Contains("{T_Pgeneral.Pdus}"))
                    {
                        remplazo = remplazo.Replace("{T_Pgeneral.Pdus}", pdu > 0 ? pdu.ToString() : " ");
                    }
                    //reemplazar nombre 1 alumno
                    if (item.Contains("{T_Alumno.NombreCompleto}"))
                    {
                    }
                    if (item.Contains("{tAlumnos.nombre1}"))
                    {
                        remplazo = remplazo.Replace("{tAlumnos.nombre1}", "FERNANDO");

                        if (item.Contains("{tAlumnos.nombre2}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.nombre2}", "MIGUEL");
                            //documento.Add(new iTextSharp.text.Phrase(" " + alumno.Nombre2.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apepaterno}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.apepaterno}", "RODRIGUEZ");
                            //documento.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoPaterno.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apematerno}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.apematerno}", "DELGADO");
                            //documento.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoMaterno.ToUpper(), fuente));
                        }
                    }
                    if(item.Contains("{T_Pgeneral.TipoCapacitacion}"))
                    {
                                          
                        if (pGeneral.Nombre.ToUpper().StartsWith("CURSO") || pGeneral.Nombre.ToUpper().StartsWith("PROGRAMA"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", "");
                        }
                        else
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", " programa");
                        }
          
                    }
                    if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                    {
                        remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", "100 horas");
                    }
                    if (item.Contains("{tCentroCosto.centrocosto}"))
                    {
                        remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", "(centro costo)");
                    }
                    if (item.Contains("{tPEspecifico.ciudad}"))
                    {
                        remplazo = remplazo.Replace("{tPEspecifico.ciudad}", "Lima");
                    }
                    if (item.Contains("{tPEspecifico.EscalaCalificacion}"))
                    {
                        remplazo = remplazo.Replace("{tPEspecifico.EscalaCalificacion}", "(escala calificacion)");
                    }
                    //reemplazar Fecha Inicio Capacitacion
                    if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", matriculaCabeceraDatosCertificadoService.TransformarFechaEnCadena(DateTime.Now));
                    }

                    //reemplazar Fecha Fin Capacitacion
                    if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", matriculaCabeceraDatosCertificadoService.TransformarFechaEnCadena(DateTime.Now));
                    }
                    //reemplazar Calificacion Promedio
                    if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                    {

                        remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", "10.00");
                    }
                    //reemplazar Fecha Emision Certificado
                    if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                    {
                        var fechaEmision = alumnoService.ObtenerFechaEmision();
                        contenidoCertificado.FechaEmisionCertificado = fechaEmision;
                        remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", fechaEmision);
                    }
                    //reemplazar Fecha Codigo Certificado
                    if (item.Contains("{T_Alumno.CodigoCertificado}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", "ALUMNO2022");
                    }
                    //reemplazar duracion en horas de Programa Especifico
                    if (item.Contains("{tPEspecifico.duracion}"))
                    {
                        remplazo = remplazo.Replace("{tPEspecifico.duracion}", "100");
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                    {
                        remplazo = remplazo.Replace("{T_MatriculaCabecera.CodigoMatricula}", "111111A22222");
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_Alumno.CorrelativoConstancia}"))
                    {
                        var correlativoConstancia = certificadoGeneradoAutomaticoService.ObtenerCorrelativoCertificado();
                        contenidoCertificado.CorrelativoConstancia = correlativoConstancia;
                        remplazo = remplazo.Replace("{T_Alumno.CorrelativoConstancia}", correlativoConstancia.ToString());
                    }
                    /*reemplazar Cronograma de notas del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaNotas}"))
                    {
                        string tablaNota = "";
                        List<CronogramaNotaDTO> cronogramaNota = new List<CronogramaNotaDTO>();
                        cronogramaNota.Add(new CronogramaNotaDTO() { Curso = "curso 1", Nota = 20, Estado = "Aprovado" });
                        cronogramaNota.Add(new CronogramaNotaDTO() { Curso = "curso 2", Nota = 20, Estado = "Aprovado" });
                        cronogramaNota.Add(new CronogramaNotaDTO() { Curso = "curso 3", Nota = 20, Estado = "Aprovado" });
                        if (cronogramaNota == null || cronogramaNota.Count == 0)
                        {
                            throw new Exception("No se Puede Calcular cronogramaNota");
                        }
                        tablaNota += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaNota += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> NOTA </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ESTADO </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaNota.Count; rows++)
                        {
                            tablaNota += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;;text-align:left'> {cronogramaNota[rows].Curso}</td>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Nota}</td>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Estado}</td>";

                            contador = contador + 1;
                            tablaNota += "</tr>";
                        }
                        tablaNota += "</table>";
                        contenidoCertificado.CronogramaNota = tablaNota;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaNotas}", tablaNota);
                    }
                    /*reemplazar Cronograma de Asistencia del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaAsistencia}"))
                    {
                        string tablaAsistencia = "";
                        List<CronogramaAsistenciaDTO> cronogramaAsistencia = new List<CronogramaAsistenciaDTO>();
                        cronogramaAsistencia.Add(new CronogramaAsistenciaDTO() { Curso = "curso 1", PorcentajeAsistencia = "90%" });
                        cronogramaAsistencia.Add(new CronogramaAsistenciaDTO() { Curso = "curso 2", PorcentajeAsistencia = "90%" });
                        cronogramaAsistencia.Add(new CronogramaAsistenciaDTO() { Curso = "curso 3", PorcentajeAsistencia = "90%" });
                        if (cronogramaAsistencia == null || cronogramaAsistencia.Count == 0)
                        {
                            throw new Exception("No se Puede Calcular CronogramaAsistencia");
                        }
                        tablaAsistencia += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaAsistencia += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ASISTENCIA </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaAsistencia.Count; rows++)
                        {
                            tablaAsistencia += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:left;'> {cronogramaAsistencia[rows].Curso}</td>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:Center;'> {cronogramaAsistencia[rows].PorcentajeAsistencia}</td>";

                            contador = contador + 1;
                            tablaAsistencia += "</tr>";
                        }
                        tablaAsistencia += "</table>";
                        contenidoCertificado.CronogramaAsistencia = tablaAsistencia;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaAsistencia}", tablaAsistencia);
                    }
                }
                var pdfFrontal = PdfGenerator.GeneratePdf(remplazo, config);
                var temmporal = ImportarPdfDocument(pdfFrontal);

                PdfSharp.Pdf.PdfPage page = temmporal.Pages[0];
                pdf.AddPage(page);
                PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[0], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                if (idPlantillaBase == 12)
                {
                    int indexHttp = FondoFrontalCertificado.IndexOf("http");
                    if (indexHttp >= 0)
                        FondoFrontalCertificado = FondoFrontalCertificado.Substring(indexHttp).Trim();
                    else
                        FondoFrontalCertificado = string.Empty;

                    if (string.IsNullOrWhiteSpace(FondoFrontalCertificado))
                        throw new ArgumentException("La URL del fondo frontal está vacía.", nameof(FondoFrontalCertificado));

                    if (!Uri.TryCreate(FondoFrontalCertificado, UriKind.Absolute, out Uri uriValida))
                        throw new InvalidOperationException("La URL proporcionada no es válida o no es absoluta: " + FondoFrontalCertificado);

                    using (HttpClient httpClient = new HttpClient())
                    {
                        HttpResponseMessage response = httpClient.GetAsync(uriValida).GetAwaiter().GetResult();
                        response.EnsureSuccessStatusCode();

                        using (Stream imageStream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
                        {
                            XImage xImage = XImage.FromStream(imageStream);
                            gfx.DrawImage(xImage, 0, 0, 843, 595);
                        }
                    }
                }
                else
                {
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalConstancia);
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 595, 843);
                }

                if (idPlantillaP != 0 && idPlantillaP != null)
                {
                    config = new PdfGenerateConfig();
                    if (idPlantillaBase == 12)/*Certificado*/
                    {
                        config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                        config.PageSize = PdfSharp.PageSize.A4;
                        config.MarginLeft = 257;
                        config.MarginRight = 83;
                        config.MarginTop = 49;
                        config.MarginBottom = 0;
                    }
                    string estructuraCurricular = "";
                    listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();
                    var plantillaP = plantillaService.ObtenerPorId(idPlantillaP);
                    var plantillaBaseP = plantillaService.ObtenerPlantillaCorreo(idPlantillaP);

                    var listaEtiquetaP = plantillaBaseP.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                    foreach (var etiqueta in listaEtiquetaP)
                    {
                        listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = "" });
                    }

                    sepacion = new List<string>();
                    sepacion = plantillaBaseP.Cuerpo.Split("###").ToList();

                    foreach (var item in sepacion)
                    {
                        remplazo = item;

                        string tipoletra = "Times New Roman";

                        string searchString = "/*";
                        int startIndex = remplazo.IndexOf(searchString);
                        searchString = "*/";
                        int endIndex = 0;
                        string substring = "";
                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                        else
                        {
                            searchString = "text-align:";
                            startIndex = remplazo.IndexOf(searchString);
                            searchString = ";";

                            if (startIndex != -1)
                            {
                                endIndex = remplazo.IndexOf(searchString, startIndex);
                                substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                            }
                            if (substring != null && substring != "")
                            {
                                tipoletra = substring;
                            }
                        }
                        if (item.Contains("acute"))
                        {
                            remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                        }
                        if (item.Contains("repositorioweb"))
                        {
                            if (idPlantillaBase == 12)
                            {
                                FondoReversoCertificado = item;
                            }
                            else
                            {
                                FondoReversoConstancia = item;
                            }
                        }
                        if (item.Contains("{tPLA_PGeneral.Nombre}"))
                        {
                            remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", pGeneral.Nombre.ToUpper());
                        }
                        if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", "BSGI2022");
                        }
                        if (item.Contains("{T_Pgeneral.Pdus}"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.Pdus}", pdu > 0 ? pdu.ToString() : " ");
                        }
                        if (item.Contains("{tCentroCosto.centrocosto}"))
                        {
                            remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", "-(centro cosoto)-");
                        }
                        if (item.Contains("{tPEspecifico.ciudad}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.ciudad}", "Lima");
                        }
                        //reemplazar nombre 1 alumno
                        if (item.Contains("{T_Alumno.NombreCompleto}"))
                        {

                        }
                        if (item.Contains("{tAlumnos.nombre1}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.nombre1}", "Fernando");

                            if (item.Contains("{tAlumnos.nombre2}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.nombre2}", "Miguel");
                            }
                            if (item.Contains("{tAlumnos.apematerno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apematerno}", "Rodriguez");
                            }
                            if (item.Contains("{tAlumnos.apepaterno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apepaterno}", "Delgado");
                            }
                        }
                        if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                        {
                            remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", "100 horas");
                        }
                        //reemplazar Fecha Inicio Capacitacion
                        if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", matriculaCabeceraDatosCertificadoService.TransformarFechaEnCadena(DateTime.Now));
                        }
                        //reemplazar Fecha Fin Capacitacion
                        if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", matriculaCabeceraDatosCertificadoService.TransformarFechaEnCadena(DateTime.Now));
                        }
                        //reemplazar Calificacion Promedio
                        if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", "10.01");
                        }
                        //reemplazar Fecha Emision Certificado
                        if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", alumnoService.ObtenerFechaEmision());
                        }
                        //reemplazar Fecha Codigo Certificado
                        if (item.Contains("{T_Alumno.CodigoCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", "ALUMNO2022");
                        }
                        //reemplazar duracion en horas de Programa Especifico
                        if (item.Contains("{tPEspecifico.duracion}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.duracion}", "100");
                        }
                        if (item.Contains("{T_Pgeneral.EstructuraCurricular}"))
                        {
                              var estructuraCurso = _unitOfWork.DocumentoPwRepository.ObtenerEstructuraCurso(idPgeneral);

                            if (estructuraCurso.Count > 0)
                            {
                                string listaEstructura = $@"
                                                    <table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                    <tr style='vertical-align: text-top;'>
                                                    <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                listaEstructura += "<ul style='margin-top:-20px'>";

                                foreach (var capitulo in estructuraCurso)
                                {
                                    listaEstructura += $@"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")} </li>";
                                }

                                listaEstructura += "</ul></td></tr></table>";
                                estructuraCurricular = listaEstructura;
                                contenidoCertificado.EstructuraCurricular = listaEstructura;
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                            }
                            else
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", "");
                            }

                        }
                        if (item.Contains("{T_Pgeneral.EstructuraCurricularPDUs}"))
                        {

                            var EstructuraporVersion = _unitOfWork.DocumentoPwRepository.ObtenerProgramasEstrucuraCurricularPdus(idPgeneral);
                            if (EstructuraporVersion.Count > 1)
                            {
                                string listaEstructura = $@"<ul style='margin-top: -10px; padding-left: 20px;'>";

                                foreach (var hijo in EstructuraporVersion)
                                {
                                    string detalleCurso;

                                    if (!string.IsNullOrEmpty(hijo.CodigoPartner) && hijo.CantidadPdus != null && hijo.CantidadPdus > 0)
                                    {
                                        detalleCurso = $"<span style='font-size: smaller; line-height: 1.2; display: inline-block; vertical-align: middle;'>({hijo.CodigoPartner} – {hijo.CantidadPdus} PDUs)</span>";
                                    }
                                    else if (!string.IsNullOrEmpty(hijo.CodigoPartner))
                                    {
                                        detalleCurso = $"<span style='font-size: smaller; line-height: 1.2; display: inline-block; vertical-align: middle;'>({hijo.Duracion} horas cronológicas)</span>";
                                    }
                                    else if (hijo.CantidadPdus != null && hijo.CantidadPdus > 0)
                                    {
                                        detalleCurso = $"<span style='font-size: smaller; line-height: 1.2; display: inline-block; vertical-align: middle;'>({hijo.CantidadPdus} PDUs)</span>";
                                    }
                                    else
                                    {
                                        detalleCurso = $"<span style='font-size: smaller; line-height: 1.2; display: inline-block; vertical-align: middle;'>({hijo.Duracion} horas cronológicas)</span>";
                                    }

                                    listaEstructura += $@"<li style='padding-bottom: 5px; list-style-type: disc;'>{hijo.Nombre.Replace(" - COLOMBIA", "")} {detalleCurso}</li>";
                                }

                                listaEstructura += "</ul>";

                                _estructuraCurricular = listaEstructura;
                                ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricularPDUs}", listaEstructura);
                            }
                            else
                            {
                                var estructuraCurso = _unitOfWork.DocumentoPwRepository.ObtenerEstructuraCurso(idPgeneral);
                                if (estructuraCurso.Count > 0)
                                {
                                    string listaEstructura = $@"
                                  <table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                  <tr style='vertical-align: text-top;'>
                                  <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                    listaEstructura += "<ul style='margin-top:-20px'>";

                                    foreach (var capitulo in estructuraCurso)
                                    {
                                        listaEstructura += $@"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")} </li>";
                                    }

                                    listaEstructura += "</ul></td></tr></table>";
                                    _estructuraCurricular = listaEstructura;
                                    ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricularPDUs}", listaEstructura);
                                }
                                else
                                {
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricularPDUs}", "*");
                                }

                            }

                        }
                        if (item.Contains("Template"))
                        {
                            //var etiq = listaObjetoWhasApp.Where(x => x.Codigo.Contains("Template")).ToList();
                            //SeccionEtiquetaDTO valor = new SeccionEtiquetaDTO();
                            //string etiqueta = "";

                            //foreach (var a in etiq)
                            //{
                            //    List<string> ListaPalabras = new List<string>();
                            //    char[] delimitador = new char[] { '.' };
                            //    string IdPlantilla = "";
                            //    string IdColumna = "";
                            //    string[] array = a.Codigo.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);

                            //    foreach (string s in array)
                            //    {
                            //        ListaPalabras.Add(s);
                            //    }

                            //    IdPlantilla = ListaPalabras[3].ToString();
                            //    IdColumna = ListaPalabras[4].ToString();

                            //    var prevalor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna.Replace("}", "")), _unitOfWork.PEspecificoRepository.ObtenerPorId(idPgeneral).IdCentroCosto ?? default(int));

                            //    if (prevalor != null && estructuraCurricular == "")
                            //    {
                            //        etiqueta = a.Codigo;
                            //        valor = prevalor;
                            //    }
                            //    else
                            //    {
                            //        remplazo = remplazo.Replace(a.Codigo, "");
                            //    }
                            //}
                            //if (etiqueta != "")
                            //{
                            //    var tratamiento = "";
                            //    tratamiento = valor.Valor.Replace("<p>", "<p id='estructura'<p>");

                            //    tratamiento = tratamiento.Replace("<li>", "<li id='liseparar'<li>");
                            //    tratamiento = tratamiento.Replace("&bull", "<li id='liseparar'");
                            //    estructura = tratamiento.Split("<p id='estructura'").Where(x => x.StartsWith("<p><strong>")).ToList();
                            //    List<string> todo = new List<string>();
                            //    int contador = 0;
                            //    string htmltabla = "";
                            //    List<string> total = new List<string>();

                            //    foreach (var li in estructura)
                            //    {
                            //        foreach (var li1 in li.Split("<li id='liseparar'").ToList())
                            //        {
                            //            if (li1.Contains("<p>"))
                            //                total.Add(li1.Replace("p>", "li>").Replace("<li>", "<li style='padding-bottom:5px'>").Replace("strong", "span").Replace("<ul>", "").Replace("</ul>", "").Replace("\"", "'").Replace("<div class='slide'>", "").Replace("<p style='padding-left:30px;'>", ""));

                            //        }

                            //    }

                            //    int Cantidadcolumns = total.Count / 25;
                            //    int residuo = total.Count % 25;
                            //    if (residuo != 0)
                            //    {
                            //        Cantidadcolumns++;
                            //    }

                            //    int reparticion = Cantidadcolumns;
                            //    PdfPTable PdfTable = new PdfPTable(Cantidadcolumns);
                            //    PdfTable.WidthPercentage = 100f;
                            //    PdfPCell PdfPCell = null;
                            //    string cadena1 = "";
                            //    string cadena2 = "";
                            //    string cadena3 = "";
                            //    string cadena4 = "";
                            //    List<string> registros = new List<string>();
                            //    foreach (var concatenar in total)
                            //    {
                            //        if (Conteo < 22)
                            //        {
                            //            if (Conteo == 0)
                            //            {
                            //                cadena1 += "<ul style='margin-top:-20px'>";
                            //            }
                            //            cadena1 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                            //        }
                            //        else
                            //        {
                            //            if (Conteo < 48)
                            //            {
                            //                cadena2 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                            //            }
                            //            else
                            //            {
                            //                if (Conteo < 75)
                            //                {
                            //                    cadena3 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                            //                }
                            //                else
                            //                {
                            //                    cadena4 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                            //                }
                            //            }
                            //        }
                            //        Conteo++;
                            //    }

                            //    htmltabla += "<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>";
                            //    contador = 0;
                            //    for (int rows = 0; rows < 1; rows++)
                            //    {
                            //        htmltabla += "<tr style='vertical-align: text-top;'>";
                            //        for (int column = 0; column < Cantidadcolumns; column++)
                            //        {
                            //            if (column == 0)
                            //            {
                            //                if (cadena1.TrimEnd(' ').EndsWith("</li>\n"))
                            //                {
                            //                    cadena1 = cadena1 + "</ul>";
                            //                }
                            //                htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena1}";
                            //            }
                            //            if (column == 1)
                            //            {
                            //                if (cadena2.StartsWith("<li>"))
                            //                {
                            //                    cadena2 = "<ul>" + cadena2;
                            //                }
                            //                if (cadena2.TrimEnd(' ').EndsWith("</li>\n"))
                            //                {
                            //                    cadena2 = cadena2 + "</ul>";
                            //                }
                            //                htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena2}";
                            //            }
                            //            if (column == 2)
                            //            {
                            //                if (cadena3.StartsWith("<li>"))
                            //                {
                            //                    cadena3 = "<ul>" + cadena3;
                            //                }
                            //                if (cadena3.TrimEnd(' ').EndsWith("</li>\n"))
                            //                {
                            //                    cadena3 = cadena3 + "</ul>";
                            //                }
                            //                htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena3}";
                            //            }
                            //            if (column == 3)
                            //            {
                            //                if (cadena4.StartsWith("<li>"))
                            //                {
                            //                    cadena4 = "<ul>" + cadena4;
                            //                }
                            //                if (cadena4.TrimEnd(' ').EndsWith("</li>\n"))
                            //                {
                            //                    cadena4 = cadena4 + "</ul>";
                            //                }
                            //                htmltabla += $@"<th style='vertical-align:top;text-align:left;font-weight: normal;'> {cadena4}";
                            //            }
                            //            contador = contador + 1;
                            //            htmltabla += "</td>";
                            //        }
                            //        htmltabla += "</tr>";
                            //    }
                            //    htmltabla += "</table>";
                            //    PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table
                            //    contenidoCertificado.EstructuraCurricular = htmltabla;
                            //    remplazo = remplazo.Replace(etiqueta, htmltabla);
                            //}



                            var EstructuraporVersion = _unitOfWork.DocumentoPwRepository.ObtenerProgramasEstrucuraCurricularPdus(idPgeneral);
                            string listaEstructura = "";

                            if (EstructuraporVersion.Count > 1)
                            {
                                listaEstructura = "<ul style='margin-top: -10px; padding-left: 20px;'>";

                                foreach (var hijo in EstructuraporVersion)
                                {
                                    string textoDetalle;

                                    if (!string.IsNullOrEmpty(hijo.CodigoPartner) && hijo.CantidadPdus > 0)
                                    {
                                        textoDetalle = $"{hijo.CodigoPartner} – {hijo.CantidadPdus} PDUs";
                                    }
                                    else if (!string.IsNullOrEmpty(hijo.CodigoPartner))
                                    {
                                        textoDetalle = $"{hijo.Duracion} horas cronológicas";
                                    }
                                    else if (hijo.CantidadPdus > 0)
                                    {
                                        textoDetalle = $"{hijo.CantidadPdus} PDUs";
                                    }
                                    else
                                    {
                                        textoDetalle = $"{hijo.Duracion} horas cronológicas";
                                    }

                                    // Aquí aplicamos los paréntesis correctamente
                                    string detalleCurso = $"<span style='font-size: smaller; line-height: 1.2;'>( {textoDetalle} )</span>";

                                    listaEstructura += $"<li style='padding-bottom: 5px;'>{hijo.Nombre.Replace(" - COLOMBIA", "")} {detalleCurso}</li>";
                                }

                                listaEstructura += "</ul>";
                            }
                            else
                            {
                                var estructuraCurso = _unitOfWork.DocumentoPwRepository.ObtenerEstructuraCurso(idPgeneral);
                                if (estructuraCurso.Count > 0)
                                {
                                    listaEstructura = @"
<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
<tr style='vertical-align: text-top;'>
<td style='vertical-align: text-top;text-align:left;font-weight: normal;'>";

                                    listaEstructura += "<ul style='margin-top:-20px'>";

                                    foreach (var capitulo in estructuraCurso)
                                    {
                                        listaEstructura += $"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")}</li>";
                                    }

                                    listaEstructura += "</ul></td></tr></table>";
                                }
                                else
                                {
                                    listaEstructura = "*";
                                }
                            }

                            // Asignar estructura generada
                            _estructuraCurricular = listaEstructura;
                            ContenidoCertificado.EstructuraCurricular = listaEstructura;

                            // Reemplazar solo la PRIMERA etiqueta {Template.***} con contenido, las demás con vacío
                            var etiquetas = Regex.Matches(remplazo, @"\{Template\.[^}]+\}")
                                                 .Cast<Match>()
                                                 .Select(m => m.Value)
                                                 .Distinct()
                                                 .ToList();

                            bool reemplazoRealizado = false;

                            foreach (var etiqueta in etiquetas)
                            {
                                if (!reemplazoRealizado)
                                {
                                    remplazo = remplazo.Replace(etiqueta, listaEstructura);
                                    reemplazoRealizado = true;
                                }
                                else
                                {
                                    remplazo = remplazo.Replace(etiqueta, ""); // eliminar sin mostrar nada
                                }
                            }

                        }
                    }

                    var pdfPosterior = PdfGenerator.GeneratePdf(remplazo, config);
                    temmporal = ImportarPdfDocument(pdfPosterior);
                    page = temmporal.Pages[0];
                    pdf.AddPage(page);

                    gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[1], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);
                 
                    FondoReversoCertificado = FondoReversoCertificado
                    .Replace("<p>", "")
                    .Replace("</p>", "")
                    .Trim();

                    using (HttpClient httpClient = new HttpClient())
                    {
                        HttpResponseMessage response = httpClient.GetAsync(FondoReversoCertificado).GetAwaiter().GetResult();
                        response.EnsureSuccessStatusCode();

                        using (Stream imageStream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
                        {
                            PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(imageStream);
                            gfx.DrawImage(xImage, 0, 0, 843, 595);
                        }
                    }




                }

                //documento.Close();
                pdf.Save(ms, false);
                return ms.ToArray();
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary> 
        /// Descripcion: Genera estilos para hoja de calculo.
        /// </summary> 
        private StyleSheet GenerateStyleSheet()
        {
            FontFactory.RegisterDirectories();

            StyleSheet css = new StyleSheet();

            css.LoadTagStyle("h1", "size", "14pt");
            css.LoadTagStyle("h1", "style", "text-align:center;font-weight:bold;");
            css.LoadTagStyle("p", "style", "text-align:justify;");
            css.LoadTagStyle("ul", "style", "display:block;list-style-type:circle;");
            css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDER, "0.1");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.H1, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTSIZE, "14px");
            return css;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de beneficios por IdMatriculaCabecera si es que los beneficios se congelaron
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<string> ObtenerBeneficiosCongeladosPorMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                List<string> listaBeneficios = new List<string>();
                var datos = _unitOfWork.MatriculaCabeceraBeneficiosRepository.ObtenerBeneficiosPorMatriculaCabecera(idMatriculaCabecera).ToList();
                listaBeneficios = datos.Select(s => s.Valor).ToList();
                return listaBeneficios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary> 
        /// Descripcion: Convierte nombre de archivo a minusculas y elimina "-".
        /// </summary> 
        private string ToURLSlug(string nombreArchivo)
        {
            return Regex.Replace(nombreArchivo, @"[^a-z0-9]+", "-", RegexOptions.IgnoreCase).Trim(new char[] { '-' }).ToLower();
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary> 
        /// Descripcion: Registra un documento en BLOB.
        /// </summary> 
        private void RegistrarDocumentoBlob(byte[] pdf, string nombreArchivo, string blobDirecion, string tipo)
        {
            string azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";
            try
            {
                //Generar entrada al blob storage
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureStorageConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(blobDirecion);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(ToURLSlug(nombreArchivo) + "." + tipo);

                blockBlob.Properties.ContentType = "application/" + tipo;
                blockBlob.Metadata["filename"] = ToURLSlug(nombreArchivo) + "." + tipo;
                blockBlob.Metadata["filemime"] = "application/" + tipo;
                Stream stream = new MemoryStream(pdf);
                blockBlob.UploadFromStreamAsync(stream);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.ToString());
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Nombre del Documento Silabo
        /// </summary>
        /// <param name="pgeneral"> capos de Programa General</param>
        /// <returns> string </returns>
        private string GeneraSilabo(PGeneralAlternoDTO pgeneral)
        {
            string archivo = string.Empty;

            try
            {
                DocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);
                PGeneralService pGeneralService = new PGeneralService(_unitOfWork);

                List<PgeneralHijoDTO> listaCursos = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijos(pgeneral.Id);

                if (listaCursos != null && listaCursos.Count() > 0)
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = pGeneralService.ObtenerPgeneralDocumentoPorId(pgeneral.Id);

                    foreach (var programa in listaCursos)
                    {
                        List<SeccionDocumentoDTO> contenido = documentoSeccionPwService.ObtenerDocumentoSeccionCompleto(pgeneral.Id);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            programa.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                programa.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = ValidarDocumentoPerfil(listaCursos, registroPrograma);
                }
                else
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = pGeneralService.ObtenerPgeneralDocumentoPorId(pgeneral.Id);
                    if (registroPrograma != null)
                    {
                        List<SeccionDocumentoDTO> contenido = documentoSeccionPwService.ObtenerDocumentoSeccionCompleto(pgeneral.Id);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            registroPrograma.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                registroPrograma.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = ValidarDocumentoSilabo(registroPrograma);
                }
                return archivo;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary> 
        /// Descripcion: Genera silabo por Programa General
        /// </summary> 
        private string GeneraSilabo(InformacionProgramaDocumentosDTO pGeneral)
        {
            string archivo = string.Empty;

            try
            {
                DocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);
                PGeneralService pGeneralService = new PGeneralService(_unitOfWork);

                List<PgeneralHijoDTO> listaCursos = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijos(pGeneral.IdProgramaGeneral);

                if (listaCursos != null && listaCursos.Count() > 0)
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = pGeneralService.ObtenerPgeneralDocumentoPorId(pGeneral.IdProgramaGeneral);

                    foreach (var programa in listaCursos)
                    {
                        List<SeccionDocumentoDTO> contenido = documentoSeccionPwService.ObtenerDocumentoSeccionCompleto(pGeneral.IdProgramaGeneral);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            programa.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                programa.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = ValidarDocumentoPerfil(listaCursos, registroPrograma);
                }
                else
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = pGeneralService.ObtenerPgeneralDocumentoPorId(pGeneral.IdProgramaGeneral);

                    if (registroPrograma != null)
                    {
                        List<SeccionDocumentoDTO> contenido = documentoSeccionPwService.ObtenerDocumentoSeccionCompleto(pGeneral.IdProgramaGeneral);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            registroPrograma.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                registroPrograma.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = ValidarDocumentoSilabo(registroPrograma);
                }
                return archivo;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Nombre del Documento Silabo porPerfil
        /// </summary>
        /// <param name="listaCursos">lista de programa general</param>
        /// <param name="pgeneral"> campos de un programa general</param>
        /// <returns></returns>
        public string ValidarDocumentoPerfil(List<PgeneralHijoDTO> listaCursos, PgeneralDocumentoSeccionDTO pGeneral)
        {
            string nombreDocumentoTemp = string.Empty;
            string nombresDoc = string.Empty;
            bool bandera = true;

            try
            {
                nombresDoc = GenerateSlug("Silabo_" + pGeneral.Nombre);
                nombreDocumentoTemp = nombresDoc + ".pdf";
                bandera = true;

                foreach (var programas in listaCursos)
                {
                    foreach (var registro in programas.ListaSeccion)
                    {
                        if (string.IsNullOrEmpty(registro.Contenido))
                        {
                            bandera = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bandera = false;
            }
            if (bandera)
            {
                return nombreDocumentoTemp;
            }
            else
            {
                return "";
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Elimina tildes.
        /// </summary> 
        private string GenerateSlug(string s)
        {
            string datos = string.Empty;
            datos = s.ToLower();
            datos = datos.Replace("á", "a");
            datos = datos.Replace("é", "e");
            datos = datos.Replace("í", "i");
            datos = datos.Replace("ó", "o");
            datos = datos.Replace("ú", "u");
            datos = datos.Replace("ñ", "n");
            return Regex.Replace(datos, @"[^a-z0-9]+", "-", RegexOptions.IgnoreCase).Trim(new char[] { '-' });
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Nombre del Documento Silabo
        /// </summary>
        /// <param name="registroPrograma">Campos de programa general</param>
        /// <returns></returns>
        private string ValidarDocumentoSilabo(PgeneralDocumentoSeccionDTO registroPrograma)
        {
            string nombreDocumentoTemp = string.Empty;
            string nombresDoc = string.Empty;
            bool bandera = false;
            try
            {
                nombresDoc = GenerateSlug("Silabo_" + registroPrograma.Nombre);
                nombreDocumentoTemp = nombresDoc + ".pdf";
                bandera = true;

                foreach (var registro in registroPrograma.ListaSeccion)
                {
                    if (string.IsNullOrEmpty(registro.Contenido))
                    {
                        bandera = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                bandera = false;
            }
            if (bandera)
            {
                return nombreDocumentoTemp;
            }
            else
            {
                return "";
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Un Archivo En byte de acuerdo a programa
        /// </summary>
        /// <param name="pGeneral">Campos del programa General </param>
        /// <returns> byte[] </returns>
        public byte[] GenerarSilaboBytes(PGeneralAlternoDTO pGeneral)
        {
            byte[] archivo;

            try
            {
                DocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);
                PGeneralService pGeneralService = new PGeneralService(_unitOfWork);

                List<PgeneralHijoDTO> listaCursos = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijos(pGeneral.Id);

                if (listaCursos != null && listaCursos.Count() > 0)//Si es mayor es un perfil
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = pGeneralService.ObtenerPgeneralDocumentoPorId(pGeneral.Id);

                    foreach (var programa in listaCursos)
                    {
                        List<SeccionDocumentoDTO> contenido = documentoSeccionPwService.ObtenerDocumentoSeccionCompleto(programa.IdPgeneral.Value);
                        if (contenido.Count > 0)//Si hay secciones disponibles para ese programa general
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            programa.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                programa.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = GenerarDocumentoPerfilBytes(listaCursos, registroPrograma);
                }
                else // caso contrario un silabo
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = pGeneralService.ObtenerPgeneralDocumentoPorId(pGeneral.Id);

                    if (registroPrograma != null)
                    {
                        List<SeccionDocumentoDTO> contenido = documentoSeccionPwService.ObtenerDocumentoSeccionCompleto(pGeneral.Id);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            registroPrograma.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                registroPrograma.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = GenerarDocumentoSilaboBytes(registroPrograma);
                }
                return archivo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary> 
        /// Descripcion: Genera silabo por Programa General
        /// </summary> 
        /// <returns> ArchivosAlumnoDTO </returns>
        public ArchivosAlumnoDTO GenerarSilaboBytes(InformacionProgramaDocumentosDTO pGeneral)
        {
            try
            {
                DocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);
                PGeneralService pGeneralService = new PGeneralService(_unitOfWork);

                List<PgeneralHijoDTO> listaCursos = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijos(pGeneral.IdProgramaGeneral);

                if (listaCursos != null && listaCursos.Count() > 0)//Si es mayor es un perfil
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = pGeneralService.ObtenerPgeneralDocumentoPorId(pGeneral.IdProgramaGeneral);

                    foreach (var programa in listaCursos)
                    {
                        List<SeccionDocumentoDTO> contenido = documentoSeccionPwService.ObtenerDocumentoSeccionCompleto(pGeneral.IdProgramaGeneral);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            programa.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                programa.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    var archivo = GenerarDocumentoPerfilBytesDocumento(listaCursos, registroPrograma);
                    return archivo;
                }
                else // caso contrario un silabo
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = pGeneralService.ObtenerPgeneralDocumentoPorId(pGeneral.IdProgramaGeneral);

                    if (registroPrograma != null)
                    {
                        List<SeccionDocumentoDTO> contenido = documentoSeccionPwService.ObtenerDocumentoSeccionCompleto(pGeneral.IdProgramaGeneral);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            registroPrograma.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                registroPrograma.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    var archivo = GenerarDocumentoSilaboBytesDocumento(registroPrograma);
                    return archivo;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Contenido al Documento Silabo
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        private byte[] GenerarDocumentoSilaboBytes(PgeneralDocumentoSeccionDTO modelo)
        {
            string nombreDocumento = string.Empty, nombreDocumentoTemp = string.Empty;
            string nombresDoc = string.Empty;
            string Contenido = string.Empty;
            string direccionSilabo = @"~/repositorioweb/documentos/silabos/";

            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (Document documento = new Document(iTextSharp.text.PageSize.A4, 70f, 50f, 80f, 70f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(documento, ms);
                    pdfWriter.PageEvent = new ITextEventsDocumentos();
                    documento.AddTitle("Silabo-BSGInstitute");

                    nombresDoc = GenerateSlug("Silabo_" + modelo.Nombre);
                    nombreDocumento = direccionSilabo + nombresDoc + ".pdf";
                    nombreDocumentoTemp = nombresDoc + ".pdf";

                    HTMLWorker htmlparser = new HTMLWorker(documento);
                    htmlparser.SetStyleSheet(GenerateStyleSheet3());

                    try
                    {
                        documento.Open();
                        Contenido = string.Empty;
                        Contenido += "<h1>" + modelo.Nombre + "</h1>";
                        Contenido += "<p style='padding-bottom: 15px;border-bottom-style: solid;border-bottom-width: 1px;'><strong>Duracion: </strong>" + modelo.pw_duracion + "</p>";

                        htmlparser.Parse(new StringReader(Contenido));
                        var agrupado = modelo.ListaSeccion.GroupBy(x => x.Titulo).Select(x => x).ToList();

                        foreach (var valorlista in agrupado)
                        {
                            if (valorlista.FirstOrDefault().NumeroFila == null)//no sigue un orden //objetivos//material de curso
                            {
                                foreach (var registro in valorlista)
                                {
                                    if (!(string.IsNullOrEmpty(registro.Contenido)))
                                    {
                                        string contenido = registro.Contenido.Replace("<table>", "<br/><table>");
                                        contenido = contenido.Replace("</table>", "</table><br/>");
                                        contenido = contenido.Replace("<li> ", "<li>").Replace(" <li>", "<li>");
                                        contenido = contenido.Replace("<li>", "<li> - ");

                                        htmlparser.Parse(new StringReader("<h1>" + registro.Titulo + "</h1>"));
                                        htmlparser.Parse(new StringReader(contenido));
                                        htmlparser.Parse(new StringReader("<br/>"));
                                    }
                                }
                            }
                            else //estrcutura curricular
                            {
                                var agrupadoestructura = valorlista.ToList();
                                string contenidoEstructura = GenerarEstructuraCurricular(agrupadoestructura);
                                htmlparser.Parse(new StringReader(contenidoEstructura));
                            }
                        }
                        documento.Close();
                    }
                    catch (Exception ex)
                    {
                        documento.Close();
                    }
                }
                return ms.ToArray();
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary> 
        /// Descripcion: Genera silabo por modelo
        /// </summary> 
        private ArchivosAlumnoDTO GenerarDocumentoSilaboBytesDocumento(PgeneralDocumentoSeccionDTO modelo)
        {
            string nombreDocumento = string.Empty, nombreDocumentoTemp = string.Empty;
            string nombresDoc = string.Empty;
            string Contenido = string.Empty;
            string blobDirecion = @"documentos/silabos";
            string urlDocumento = "https://repositorioweb.blob.core.windows.net/";
            string direccionSilabo = @"~/repositorioweb/documentos/silabos/";

            ArchivosAlumnoDTO respuesta = new ArchivosAlumnoDTO();

            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (Document documento = new Document(iTextSharp.text.PageSize.A4, 70f, 50f, 80f, 70f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(documento, ms);
                    pdfWriter.PageEvent = new ITextEventsDocumentos();
                    documento.AddTitle("Silabo-BSGInstitute");

                    nombresDoc = GenerateSlug("Silabo_" + modelo.Nombre);
                    nombreDocumento = direccionSilabo + nombresDoc + ".pdf";
                    nombreDocumentoTemp = nombresDoc + ".pdf";

                    HTMLWorker htmlparser = new HTMLWorker(documento);
                    htmlparser.SetStyleSheet(GenerateStyleSheet3());

                    try
                    {
                        documento.Open();
                        Contenido = string.Empty;
                        Contenido += "<h1>" + modelo.Nombre + "</h1>";
                        Contenido += "<p style='padding-bottom: 15px;border-bottom-style: solid;border-bottom-width: 1px;'><strong>Duracion: </strong>" + modelo.pw_duracion + "</p>";

                        htmlparser.Parse(new StringReader(Contenido));

                        foreach (var registro in modelo.ListaSeccion)
                        {
                            if (!(string.IsNullOrEmpty(registro.Contenido)))
                            {
                                string contenido = registro.Contenido.Replace("<table>", "<br/><table>");
                                contenido = contenido.Replace("</table>", "</table><br/>");
                                contenido = contenido.Replace("<li> ", "<li>").Replace(" <li>", "<li>");
                                contenido = contenido.Replace("<li>", "<li> - ");

                                htmlparser.Parse(new StringReader("<h1>" + registro.Titulo + "</h1>"));
                                htmlparser.Parse(new StringReader(contenido));
                                htmlparser.Parse(new StringReader("<br/>"));
                            }
                        }
                        documento.Close();
                    }
                    catch (Exception ex)
                    {
                        documento.Close();
                    }
                }
                var pdf = ms.ToArray();
                RegistrarDocumentoBlob(pdf, nombresDoc, blobDirecion, "pdf");
                var raiz = ToURLSlug(nombresDoc) + ".pdf";

                respuesta.NombreArchivo = urlDocumento + blobDirecion + "/" + raiz;
                respuesta.registroMemoria = pdf;

                return respuesta;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera estilos Para los Documentos 
        /// </summary>
        /// <returns></returns>
        private StyleSheet GenerateStyleSheet3()
        {
            FontFactory.RegisterDirectories();
            StyleSheet css = new StyleSheet();

            css.LoadTagStyle("h1", "size", "12pt");
            css.LoadTagStyle("h1", "style", "font-weight:bold;");
            css.LoadTagStyle("p", "style", "text-align:justify;margin-bottom: 10px;");
            css.LoadTagStyle("ul", "style", "display:block;list-style-type:circle;");
            css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDER, "0.1");
            css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.H1, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTSIZE, "12px");
            css.LoadStyle(HtmlTags.UL, HtmlTags.STYLE, "list-style-type: disc;");
            return css;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary> 
        /// Descripcion: Genera silabo por estrucutra curricular
        /// </summary> 
        private string GenerarEstructuraCurricular(List<SeccionDocumentoDTO> agrupadoEstructura)
        {
            var agrupadoCabecera = agrupadoEstructura.GroupBy(w => w.NumeroFila).Select(x => x).ToList();
            var contenidoFinal = "<h1>Estructura Curricular:</h1><br/>";
            var listaCabecera = new List<string>();
            var listaCapitulos = new List<string>();

            foreach (var registro in agrupadoCabecera.OrderBy(w => w.Key).ToList())
            {
                var cabecera = registro.Where(w => w.NombreTitulo == "Capitulo").FirstOrDefault().Contenido;
                if (listaCabecera.Any(x => x == cabecera))
                {
                    if (listaCapitulos.Any(x => x == registro.Where(w => w.NombreTitulo == "Sesion").FirstOrDefault().Contenido))//si ya tiene ese capitulo
                    {
                        continue;
                    }
                    else
                    {
                        contenidoFinal = contenidoFinal + "-" + registro.Where(w => w.NombreTitulo == "Sesion").FirstOrDefault().Contenido + "<br/>";
                        listaCapitulos.Add(registro.Where(w => w.NombreTitulo == "Sesion").FirstOrDefault().Contenido);
                    }
                }
                else
                {
                    listaCabecera.Add(cabecera);
                    contenidoFinal = contenidoFinal + "<h1>" + cabecera + "</h1>";
                }
            }
            return contenidoFinal;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary> 
        /// Descripcion: Genera documento de perfil.
        /// </summary> 
        private ArchivosAlumnoDTO GenerarDocumentoPerfilBytesDocumento(List<PgeneralHijoDTO> modelo, PgeneralDocumentoSeccionDTO programa)
        {
            string nombreDocumento = string.Empty, nombreDocumentoTemp = string.Empty;
            string nombresDoc = string.Empty;
            string blobDirecion = @"documentos/perfiles";
            string urlDocumento = "https://repositorioweb.blob.core.windows.net/";
            string Contenido = string.Empty;
            string direccionSilabo = @"~/repositorioweb/documentos/silabos/";

            ArchivosAlumnoDTO respuesta = new ArchivosAlumnoDTO();

            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (Document documento = new Document(iTextSharp.text.PageSize.A4, 70f, 50f, 80f, 70f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(documento, ms);
                    pdfWriter.PageEvent = new ITextEventsDocumentos();
                    documento.AddTitle("Silabo-BSGInstitute");

                    nombresDoc = GenerateSlug("Perfil_" + programa.Nombre);
                    nombreDocumento = direccionSilabo + nombresDoc + ".pdf";
                    nombreDocumentoTemp = nombresDoc + ".pdf";

                    HTMLWorker htmlparser = new HTMLWorker(documento);
                    htmlparser.SetStyleSheet(GenerateStyleSheet3());

                    try
                    {
                        documento.Open();
                        Contenido = string.Empty;
                        Contenido += "<h1>" + programa.Nombre + "</h1>";
                        Contenido += "<p style='padding-bottom: 15px;border-bottom-style: solid;border-bottom-width: 1px;'><strong>Duracion: </strong>" + programa.pw_duracion + "</p>";

                        htmlparser.Parse(new StringReader(Contenido));
                        htmlparser.Parse(new StringReader("<p>_______________________________________________________________________</p>"));
                        htmlparser.Parse(new StringReader("<br/>"));

                        foreach (var programas in modelo)
                        {
                            htmlparser.Parse(new StringReader("<h1>" + programas.Nombre + "</h1>"));
                            htmlparser.Parse(new StringReader("<p><strong>Duracion: </strong>" + programas.pw_duracion + "</p>"));

                            if (programas.ListaSeccion.Count > 0)
                            {
                                foreach (var registro in programas.ListaSeccion)
                                {
                                    if (!(string.IsNullOrEmpty(registro.Contenido)))
                                    {
                                        string contenido = registro.Contenido.Replace("<table>", "<br/><table>");
                                        contenido = contenido.Replace("</table>", "</table><br/>");
                                        contenido = contenido.Replace("<li> ", "<li>").Replace(" <li>", "<li>");
                                        contenido = contenido.Replace("<li>", "<li> - ");

                                        htmlparser.Parse(new StringReader("<h1>" + registro.Titulo + "</h1>"));
                                        htmlparser.Parse(new StringReader(contenido));
                                        htmlparser.Parse(new StringReader("<br/>"));
                                    }
                                }
                                documento.NewPage();
                            }
                            else
                            {
                                break;
                            }
                        }
                        documento.Close();
                    }
                    catch (Exception ex)
                    {
                        documento.Close();
                    }
                }
                var pdf = ms.ToArray();
                RegistrarDocumentoBlob(pdf, nombresDoc, blobDirecion, "pdf");
                var raiz = ToURLSlug(nombresDoc) + ".pdf";

                respuesta.NombreArchivo = urlDocumento + blobDirecion + "/" + raiz;
                respuesta.registroMemoria = pdf;

                return respuesta;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera el Contenido del archivo Silabo
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="programa"></param>
        /// <returns></returns>
        private byte[] GenerarDocumentoPerfilBytes(List<PgeneralHijoDTO> modelo, PgeneralDocumentoSeccionDTO programa)
        {
            string nombreDocumento = string.Empty, nombreDocumentoTemp = string.Empty;
            string nombresDoc = string.Empty;
            string Contenido = string.Empty;
            string direccionSilabo = @"~/repositorioweb/documentos/silabos/";

            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (Document documento = new Document(iTextSharp.text.PageSize.A4, 70f, 50f, 80f, 70f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(documento, ms);
                    pdfWriter.PageEvent = new ITextEventsDocumentos();
                    documento.AddTitle("Silabo-BSGInstitute");

                    nombresDoc = GenerateSlug("Silabo_" + programa.Nombre);
                    nombreDocumento = direccionSilabo + nombresDoc + ".pdf";
                    nombreDocumentoTemp = nombresDoc + ".pdf";

                    HTMLWorker htmlparser = new HTMLWorker(documento);
                    htmlparser.SetStyleSheet(GenerateStyleSheet3());

                    try
                    {
                        documento.Open();
                        Contenido = string.Empty;
                        Contenido += "<h1>" + programa.Nombre + "</h1>";
                        Contenido += "<p style='padding-bottom: 15px;border-bottom-style: solid;border-bottom-width: 1px;'><strong>Duracion: </strong>" + programa.pw_duracion + "</p>";

                        htmlparser.Parse(new StringReader(Contenido));
                        //document.Add(new iTextSharp.text.Paragraph("_______________________________________________________________________"));
                        htmlparser.Parse(new StringReader("<p>_______________________________________________________________________</p>"));
                        htmlparser.Parse(new StringReader("<br/>"));

                        foreach (var programas in modelo)
                        {
                            var agrupado = programas.ListaSeccion.GroupBy(w => w.Titulo).Select(x => x).ToList();

                            htmlparser.Parse(new StringReader("<h1>" + programas.Nombre + "</h1>"));
                            htmlparser.Parse(new StringReader("<p><strong>Duracion: </strong>" + programas.pw_duracion + "</p>"));

                            foreach (var valorlista in agrupado)
                            {
                                if (valorlista.FirstOrDefault().NumeroFila == null)//no siguen un orden //objetvios//material
                                {
                                    foreach (var registro in valorlista)
                                    {

                                        if (!(string.IsNullOrEmpty(registro.Contenido)))
                                        {
                                            string contenido = registro.Contenido.Replace("<table>", "<br/><table>");
                                            contenido = contenido.Replace("</table>", "</table><br/>");
                                            contenido = contenido.Replace("<li> ", "<li>").Replace(" <li>", "<li>");
                                            contenido = contenido.Replace("<li>", "<li> - ");

                                            htmlparser.Parse(new StringReader("<h1>" + registro.Titulo + "</h1>"));
                                            htmlparser.Parse(new StringReader(contenido));
                                            htmlparser.Parse(new StringReader("<br/>"));
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    var agrupadoestructura = valorlista.ToList();
                                    string contenidoEstructura = GenerarEstructuraCurricular(agrupadoestructura);
                                    htmlparser.Parse(new StringReader(contenidoEstructura));
                                }
                            }
                            documento.NewPage();
                        }
                        documento.Close();
                    }
                    catch (Exception ex)
                    {
                        documento.Close();
                    }
                }
                return ms.ToArray();
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Version: 1.0
        /// <summary> 
        /// Descripcion: Obtiene una lista de todos los documentos
        /// </summary> 
        public void CargarListaDocumentos()
        {
            documentoObjDTO.ListaDocumentos = new List<ProgramaDocumentosDTO>();
            ListadoAlertas = new List<AlertDTO>();

            ProgramaDocumentosDTO BrochureGeneral = new ProgramaDocumentosDTO();
            BrochureGeneral.Id = 0;
            BrochureGeneral.NombreDocumento = "Brochure BSG Institute";
            BrochureGeneral.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(BrochureGeneral);

            ProgramaDocumentosDTO Brochure = new ProgramaDocumentosDTO();
            Brochure.Id = 1;
            Brochure.NombreDocumento = "Brochure";
            Brochure.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(Brochure);

            ProgramaDocumentosDTO CronogramaAlumnos = new ProgramaDocumentosDTO();
            CronogramaAlumnos.Id = 2;
            CronogramaAlumnos.NombreDocumento = "Cronograma de Alumnos";
            CronogramaAlumnos.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(CronogramaAlumnos);

            ProgramaDocumentosDTO Requisitosprogramasonline = new ProgramaDocumentosDTO();
            Requisitosprogramasonline.Id = 3;
            Requisitosprogramasonline.NombreDocumento = "Requisitos programas online";
            Requisitosprogramasonline.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(Requisitosprogramasonline);

            ProgramaDocumentosDTO PagareWord = new ProgramaDocumentosDTO();
            PagareWord.Id = 4;
            PagareWord.NombreDocumento = "Pagare 1 (Word)";
            PagareWord.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(PagareWord);

            ProgramaDocumentosDTO PagareExcel = new ProgramaDocumentosDTO();
            PagareExcel.Id = 5;
            PagareExcel.NombreDocumento = "Pagare 2 (Excel)";
            PagareExcel.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(PagareExcel);

            ProgramaDocumentosDTO Conveniocapacitacionformacion = new ProgramaDocumentosDTO();
            Conveniocapacitacionformacion.Id = 6;
            Conveniocapacitacionformacion.NombreDocumento = "Convenio de capacitación y/o formación (Firma o grabacion contrato de voz)";
            Conveniocapacitacionformacion.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(Conveniocapacitacionformacion);

            ProgramaDocumentosDTO Condicionesaracterísticasserviciocapacitación = new ProgramaDocumentosDTO();
            Condicionesaracterísticasserviciocapacitación.Id = 7;
            Condicionesaracterísticasserviciocapacitación.NombreDocumento = "Condiciones y Características del servicio de capacitación (grabación contrato de voz)";
            Condicionesaracterísticasserviciocapacitación.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(Condicionesaracterísticasserviciocapacitación);

            ProgramaDocumentosDTO SilaboPrograma = new ProgramaDocumentosDTO();
            SilaboPrograma.Id = 8;
            SilaboPrograma.NombreDocumento = "Sílabos del Programa";
            SilaboPrograma.Habilitado = false;
            documentoObjDTO.ListaDocumentos.Add(SilaboPrograma);

            ProgramaDocumentosDTO ContratoUsoDatos = new ProgramaDocumentosDTO();
            ContratoUsoDatos.Id = 9;
            ContratoUsoDatos.NombreDocumento = "Contrato Uso de Datos";
            ContratoUsoDatos.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(ContratoUsoDatos);

            ProgramaDocumentosDTO LineamientosCondiciones = new ProgramaDocumentosDTO();
            LineamientosCondiciones.Id = 10;
            LineamientosCondiciones.NombreDocumento = "Lineamientos de Condiciones";
            LineamientosCondiciones.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(LineamientosCondiciones);

            ProgramaDocumentosDTO ConvenioPrestacion = new ProgramaDocumentosDTO();
            ConvenioPrestacion.Id = 11;
            ConvenioPrestacion.NombreDocumento = "Convenio de Prestacion de Servicios";
            ConvenioPrestacion.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(ConvenioPrestacion);

            ProgramaDocumentosDTO CronogramaAlumnosGrupal = new ProgramaDocumentosDTO();
            CronogramaAlumnosGrupal.Id = 12;
            CronogramaAlumnosGrupal.NombreDocumento = "Cronograma de Alumnos(Todos los Grupos)";
            CronogramaAlumnosGrupal.Habilitado = true;
            documentoObjDTO.ListaDocumentos.Add(CronogramaAlumnosGrupal);
        }

        /// <summary> 
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera certificado IRCA
        /// </summary> 
        public byte[] GenerarCertificadoIrca(int idPlantillaF, int idContenidoCertificadoIrca, int idPEspecifico, ref string codigoCertificado, ref int idPgeneral)
        {
            var contenidoCertificado = new CertificadoGeneradoAutomaticoContenido();
            int idPlantillaBase = 0;

            if (!_unitOfWork.ContenidoCertificadoIrcaRepository.Exist(idContenidoCertificadoIrca))
            {
                throw new BadRequestException("Contenido Certificado Irca no Existe!");
            }
            var contenidoCertificadoIrca = _unitOfWork.ContenidoCertificadoIrcaRepository.ObtenerPorId(idContenidoCertificadoIrca)!;

            if (!_unitOfWork.OportunidadClasificacionOperacionesRepository.Exist(x => x.IdMatriculaCabecera == contenidoCertificadoIrca.IdMatriculaCabecera))
            {
                throw new BadRequestException("Matricula no esta Casificada!");
            }

            if (!_unitOfWork.MatriculaCabeceraRepository.Exist(x => x.Id == contenidoCertificadoIrca.IdMatriculaCabecera))
            {
                throw new BadRequestException("Matricula cabecera no valida!");
            }

            var matriculaCabecera = _unitOfWork.MatriculaCabeceraRepository.ObtenerPorId(contenidoCertificadoIrca.IdMatriculaCabecera)!;
            var detalleMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleMatricula(contenidoCertificadoIrca.IdMatriculaCabecera);
            if (detalleMatriculaCabecera == null || detalleMatriculaCabecera.IdProgramaGeneral == 0)
            {
                throw new BadRequestException($"No existe detalleMatriculaCabecera {contenidoCertificadoIrca.IdMatriculaCabecera}");
            }
            var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantillaF);
            if (plantilla == null || plantilla.Id == 0)
            {
                throw new BadRequestException($"No existe la plantilla {idPlantillaF}");
            }
            idPgeneral = detalleMatriculaCabecera.IdProgramaGeneral;
            idPlantillaBase = plantilla.IdPlantillaBase;

            var plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(idPlantillaF);
            var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(matriculaCabecera.IdAlumno);
            if (alumno == null || alumno.Id == 0)
            {
                throw new BadRequestException($"No existe el alumno {matriculaCabecera.IdAlumno}");
            }
            var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(detalleMatriculaCabecera.IdOportunidad);
            var datosCompuestosOportunidad = _unitOfWork.OportunidadRepository.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
            var personal = _unitOfWork.PersonalRepository.ObtenerPorId(oportunidad.IdPersonalAsignado.Value);

            var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();
            var listaObjetoWhasApp = listaEtiqueta.Select(etiqueta => new DatoPlantillaWhatsAppDTO
            {
                Codigo = string.Concat("{", etiqueta, "}"),
                Texto = ""
            });

            List<string> sepacion = new List<string>();
            string fondoFrontalCertificado = "";
            sepacion = plantillaBase.Cuerpo.Split("###").ToList();

            Dictionary<string, object> map = new Dictionary<string, object>();
            map["font_factory"] = new MyFontFactory();

            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                var config = new PdfGenerateConfig();

                config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                config.PageSize = PdfSharp.PageSize.A4;
                config.MarginTop = 215;
                config.MarginBottom = 20;
                config.MarginLeft = 105;
                config.MarginRight = 95;

                List<string> estructura = new List<string>();

                string remplazo = "";
                foreach (var item in sepacion)
                {
                    string direccionCodigoQR = "";
                    remplazo = item;

                    if (item.Contains("repositorioweb"))
                    {
                        fondoFrontalCertificado = item;
                    }
                    if (item.Contains("acute"))
                    {
                        remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                    }
                    if (item.Contains("CODIGOQR"))
                    {
                        string url = _unitOfWork.MatriculaCabeceraRepository.ObtenerCodigoCertificadoIrca(matriculaCabecera.Id);

                        var urlCodigo = "https://bsginstitute.com/informacionCertificado?cod=" + matriculaCabecera.Id + "." + url;
                        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                        QRCodeData qrDatos = qRCodeGenerator.CreateQrCode(urlCodigo, QRCodeGenerator.ECCLevel.Q);
                        QRCode qRCodigo = new QRCode(qrDatos);

                        System.Drawing.Bitmap qrImagen = qRCodigo.GetGraphic(7, System.Drawing.Color.Black, System.Drawing.Color.White, false);
                        System.Drawing.Image objImage = (System.Drawing.Image)qrImagen;

                        direccionCodigoQR = _unitOfWork.AlumnoRepository.guardarArchivosQR(ImageToByte2(objImage), "image/jpeg", url);
                        contenidoCertificado.CodigoQr = direccionCodigoQR;
                        remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='{direccionCodigoQR}' width=55 height=55  />");

                    }
                    if (item.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        string nombrePrograma = _unitOfWork.PGeneralRepository.ObtenerNombrePorIdPespecifico(idPEspecifico);

                        contenidoCertificado.NombrePrograma = nombrePrograma.ToUpper();
                        if (nombrePrograma.ToUpper().StartsWith("CURSO") || nombrePrograma.ToUpper().StartsWith("PROGRAMA"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", "");
                        }
                        else
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", " programa");
                        }
                        remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", nombrePrograma);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                    {
                        var codigoPartner = _unitOfWork.PGeneralRepository.ObtenerCodigoPartner(detalleMatriculaCabecera.IdProgramaGeneral);
                        if (codigoPartner != null)
                        {
                            this.ContenidoCertificado.CodigoPartner = codigoPartner;
                            remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                        }
                        else
                        {
                            throw new Exception("No se puede Calcular Codigo Partner");
                        }
                    }
                    if (item.Contains("{T_Pgeneral.Pdus}"))
                    {
                        var codigoPartnerPdu = _unitOfWork.PGeneralRepository.ObtenerPdu(detalleMatriculaCabecera.IdProgramaGeneral);
                        if (codigoPartnerPdu != null)
                        {
                            this.ContenidoCertificado.PDUs = codigoPartnerPdu;
                            remplazo = remplazo.Replace("{T_Pgeneral.Pdus}", codigoPartnerPdu.ToString());
                        }
                        else
                        {
                            throw new Exception("No se puede Calcular Codigo Partner");
                        }
                    }
                    //reemplazar nombre 1 alumno
                    if (item.Contains("{T_Alumno.NombreCompleto}"))
                    {
                    }
                    if (item.Contains("{tAlumnos.nombre1}"))
                    {
                        string nombreAlumno = alumno!.Nombre1.ToUpper();
                        remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1.ToUpper());

                        if (item.Contains("{tAlumnos.nombre2}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.Nombre2.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2.ToUpper());
                        }
                        if (item.Contains("{tAlumnos.apepaterno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoPaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno.ToUpper());
                        }
                        if (item.Contains("{tAlumnos.apematerno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoMaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno.ToUpper());
                        }

                        this.ContenidoCertificado.NombreAlumno = nombreAlumno;

                    }
                    if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                    {
                        remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", datosCompuestosOportunidad.PwDuracion);
                    }
                    if (item.Contains("{tCentroCosto.centrocosto}"))
                    {
                        contenidoCertificado.NombreCentroCosto = detalleMatriculaCabecera.NombreCentroCosto.ToUpper();
                        remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto.ToUpper());
                    }
                    if (item.Contains("{tPEspecifico.ciudad}"))
                    {
                        var PrimeraLetra = detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper();
                        var resto = detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower();
                        contenidoCertificado.Ciudad = PrimeraLetra + resto;
                        remplazo = remplazo.Replace("{tPEspecifico.ciudad}", PrimeraLetra + resto);
                    }
                    if (item.Contains("{tPEspecifico.EscalaCalificacion}"))
                    {
                        var escalaCalificacion = detalleMatriculaCabecera.EscalaCalificacion;
                        contenidoCertificado.EscalaCalificacion = escalaCalificacion;
                        remplazo = remplazo.Replace("{tPEspecifico.EscalaCalificacion}", escalaCalificacion.ToString());
                    }
                    //reemplazar Fecha Inicio Capacitacion
                    if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                    {
                        string fechaInicioCapacitacion = _unitOfWork.MatriculaCabeceraRepository.ObtenerFechaInicioCapacitacionModuloPespecifico(matriculaCabecera.Id, idPEspecifico);

                        if (fechaInicioCapacitacion == null)
                        {
                            throw new Exception("No se Pudo Calcular FechaInicioCapacitacion!");
                        }
                        contenidoCertificado.FechaInicioCapacitacion = fechaInicioCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", fechaInicioCapacitacion);
                    }

                    //reemplazar Fecha Fin Capacitacion
                    if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                    {
                        string? fechaFinCapacitacion = _unitOfWork.MatriculaCabeceraRepository.ObtenerFechaFinCapacitacionModuloPespecifico(matriculaCabecera.Id, idPEspecifico);

                        if (fechaFinCapacitacion == null)
                        {
                            throw new Exception("No se Pudo Calcular FechaFinCapacitacion!");
                        }
                        contenidoCertificado.FechaFinCapacitacion = fechaFinCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", fechaFinCapacitacion);
                    }
                    //reemplazar Calificacion Promedio
                    if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                    {
                        string? calificacionPromedio = _unitOfWork.MatriculaCabeceraRepository.ObtenerNotaPromedioModulo(matriculaCabecera.Id, idPEspecifico);

                        if (calificacionPromedio == null)
                        {
                            throw new Exception("No se Pudo Calcular Nota Promedio!");
                        }
                        contenidoCertificado.CalificacionPromedio = Convert.ToInt32(calificacionPromedio);
                        remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", (Convert.ToInt32(calificacionPromedio)).ToString());
                    }
                    //reemplazar Fecha Emision Certificado
                    if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                    {
                        var fechaEmision = _unitOfWork.AlumnoRepository.ObtenerFechaEmision();
                        contenidoCertificado.FechaEmisionCertificado = fechaEmision;
                        remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", fechaEmision);
                    }

                    //reemplazar Fecha Codigo Certificado
                    if (item.Contains("{T_Alumno.CodigoCertificado}"))
                    {
                        codigoCertificado = _unitOfWork.MatriculaCabeceraRepository.ObtenerCodigoCertificadoIrca(matriculaCabecera.Id);
                        remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", codigoCertificado);
                    }

                    //reemplazar duracion en horas de Programa Especifico
                    if (item.Contains("{tPEspecifico.duracion}"))
                    {
                        string? duracionPespecifico = _unitOfWork.PEspecificoRepository.ObtenerDuracionProgramaEspecificoModulo(idPEspecifico, matriculaCabecera.Id);

                        if (duracionPespecifico == null)
                        {
                            throw new Exception("No se Pudo Calcular Duracion!");
                        }
                        contenidoCertificado.DuracionPespecifico = int.Parse(duracionPespecifico);
                        remplazo = remplazo.Replace("{tPEspecifico.duracion}", duracionPespecifico);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                    {
                        remplazo = remplazo.Replace("{T_MatriculaCabecera.CodigoMatricula}", matriculaCabecera.CodigoMatricula);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_Alumno.CorrelativoConstancia}"))
                    {
                        var correlativoConstancia = _unitOfWork.CertificadoGeneradoAutomaticoRepository.ObtenerCorrelativoCertificado();
                        contenidoCertificado.CorrelativoConstancia = correlativoConstancia;
                        remplazo = remplazo.Replace("{T_Alumno.CorrelativoConstancia}", correlativoConstancia.ToString());
                    }
                    if (item.Contains("{T_Pgeneral.IdCursoIrca}"))
                    {
                        if (contenidoCertificadoIrca.CursoIrcaId == 0)
                        {
                            throw new BadRequestException("El Registro no tiene IdCursoIrca");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.IdCursoIrca}", contenidoCertificadoIrca.CursoIrcaId.ToString());
                    }
                    if (item.Contains("{T_Pgeneral.NombreCursoIrca}"))
                    {
                        if (contenidoCertificadoIrca.NombreCurso == "")
                        {
                            throw new Exception("El Registro no tiene NombreCurso");
                        }
                        contenidoCertificado.NombrePrograma = contenidoCertificadoIrca.NombreCurso;
                        remplazo = remplazo.Replace("{T_Pgeneral.NombreCursoIrca}", contenidoCertificadoIrca.NombreCurso);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoCursoIrca}"))
                    {
                        if (contenidoCertificadoIrca.CodigoCurso == "")
                        {
                            throw new Exception("El Registro no tiene CodigoCurso");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.CodigoCursoIrca}", contenidoCertificadoIrca.CodigoCurso);
                    }
                    if (item.Contains("{T_Pgeneral.FechaInicioCursoIrca}"))
                    {
                        contenidoCertificado.FechaInicioCapacitacion = contenidoCertificadoIrca.FechaInicio.ToString("dd-MM-yyyy");
                        remplazo = remplazo.Replace("{T_Pgeneral.FechaInicioCursoIrca}", contenidoCertificadoIrca.FechaInicio.ToString("dd-MM-yyyy"));
                    }
                    if (item.Contains("{T_Pgeneral.FechaFinCursoIrca}"))
                    {
                        contenidoCertificado.FechaFinCapacitacion = contenidoCertificadoIrca.FechaFin.ToString("dd-MM-yyyy");
                        remplazo = remplazo.Replace("{T_Pgeneral.FechaFinCursoIrca}", contenidoCertificadoIrca.FechaFin.ToString("dd-MM-yyyy"));
                    }
                    if (item.Contains("{T_Pgeneral.DuracionCursoIrca}"))
                    {
                        if (contenidoCertificadoIrca.DuracionCurso == 0)
                        {
                            throw new Exception("El Registro no tiene DuracionCursoIrca");
                        }
                        contenidoCertificado.DuracionPespecifico = contenidoCertificadoIrca.DuracionCurso;
                        remplazo = remplazo.Replace("{T_Pgeneral.DuracionCursoIrca}", contenidoCertificadoIrca.DuracionCurso.ToString());
                    }
                    if (item.Contains("{T_Pgeneral.DescripcionResultadoCursoIrca}"))
                    {
                        string? resultadoCursoIrca = _unitOfWork.ContenidoCertificadoIrcaRepository.ObtenerDescripcionResultadoIrca(contenidoCertificadoIrca.Id);
                        if (contenidoCertificadoIrca.ResultadoCurso == null)
                        {
                            throw new Exception("El Registro no tiene ResultadoCurso");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.DescripcionResultadoCursoIrca}", resultadoCursoIrca ?? "");
                    }
                }
                var pdfFrontal = PdfGenerator.GeneratePdf(remplazo, config);
                var temmporal = ImportarPdfDocument(pdfFrontal);
                PdfSharp.Pdf.PdfPage page = temmporal.Pages[0];
                pdf.AddPage(page);

                PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[0], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(fondoFrontalCertificado);
                webRequest.AllowWriteStreamBuffering = true;
                System.Net.WebResponse webResponse = webRequest.GetResponse();
                PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                gfx.DrawImage(xImage, 0, 0, 843, 595);
                pdf.Save(ms, false);
                return ms.ToArray();
            }

        }

        public byte[] GenerarCertificadoSinFondo(ContenidoCertificadoSinFondoDTO contenido)
        {
            var contenidoCertificado = new CertificadoGeneradoAutomaticoContenido();

            var ContenidoCertificadoIrca = _unitOfWork.ContenidoCertificadoIrcaRepository.FirstBy(w => w.IdMatriculaCabecera == contenido.IdMatriculaCabecera && w.NombreCurso == contenido.NombrePrograma);

            var oportunidadClasificacionOperaciones = _unitOfWork.OportunidadClasificacionOperacionesRepository.FirstBy(x => x.IdOportunidad == contenido.IdOportunidad);

            if (!_unitOfWork.MatriculaCabeceraRepository.Exist(x => x.Id == oportunidadClasificacionOperaciones.IdMatriculaCabecera))
            {
                throw new Exception("Matricula cabecera no valida!");
            }

            var listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();

            var plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(contenido.IdPlantillaFrontal);

            var oportunidad = _unitOfWork.OportunidadRepository.FirstById(contenido.IdOportunidad);

            var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

            foreach (var etiqueta in listaEtiqueta)
            {
                listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = "" });
            }
            List<string> sepacion = new List<string>();

            sepacion = plantillaBase.Cuerpo.Split("###").ToList();

            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                var config = new PdfGenerateConfig();
                if (contenido.IdPlantillaFrontal == 1338)
                {
                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginTop = 215;
                    config.MarginBottom = 20;
                    config.MarginLeft = 105;
                    config.MarginRight = 95;
                }
                else
                {
                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginTop = 144;
                    config.MarginBottom = 30;
                    config.MarginLeft = 111;
                    config.MarginRight = 83;
                }

                int Conteo = 0;
                //int cantidadItems = 0;
                List<string> estructura = new List<string>();

                string remplazo = "";
                foreach (var item in sepacion)
                {
                    string direccionCodigoQR = "";
                    string tipoletra = "Times New Roman";
                    remplazo = item;
                    string searchString = "/*";
                    int startIndex = remplazo.IndexOf(searchString);
                    searchString = "*/";
                    int endIndex = 0;
                    string substring = "";

                    if (startIndex != -1)
                    {
                        endIndex = remplazo.IndexOf(searchString, startIndex);
                        substring = remplazo.Substring(startIndex + searchString.Length, endIndex - searchString.Length);
                    }
                    if (substring != null && substring != "")
                    {
                        tipoletra = substring;
                    }
                    else
                    {
                        searchString = "text-align:";
                        startIndex = remplazo.IndexOf(searchString);
                        searchString = ";";

                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                    }
                    if (item.Contains("repositorioweb"))
                    {

                    }
                    if (item.Contains("acute"))
                    {
                        remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                    }
                    if (item.Contains("CODIGOQR"))
                    {
                        string url = "";
                        if (contenido.CodigoQR != null)
                        {
                            direccionCodigoQR = contenido.CodigoQR;
                        }
                        else
                        {
                            url = contenido.CodigoCertificado;
                            direccionCodigoQR = "https://repositorioweb.blob.core.windows.net/repositorioweb/certificados/CodigoQR/" + url;
                        }

                        contenidoCertificado.CodigoQr = direccionCodigoQR;
                        remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='{direccionCodigoQR}' width=55 height=55  />");

                    }
                    if (item.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        var nombrePrograma = contenido.NombrePrograma.Replace(" - COLOMBIA", "");
                        contenidoCertificado.NombrePrograma = nombrePrograma;
                        if (nombrePrograma.ToUpper().StartsWith("CURSO") || nombrePrograma.ToUpper().StartsWith("PROGRAMA"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", "");
                        }
                        else
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", " programa");
                        }
                        remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", contenido.NombrePrograma);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                    {
                        string codigoPartner = null;
                        if (contenido.CodigoPartner != null)
                        {
                            codigoPartner = contenido.CodigoPartner;
                        }
                        else
                        {
                            codigoPartner = _unitOfWork.PGeneralRepository.ObtenerCodigoPartner(contenido.IdMatriculaCabecera);
                        }

                        if (codigoPartner != null)
                        {
                            contenidoCertificado.CodigoPartner = codigoPartner;
                            remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                        }
                        else
                        {
                            throw new Exception("No se puede Calcular CodigoPartner");
                        }
                    }
                    if (item.Contains("{T_Pgeneral.Pdus}"))
                    {
                        int? codigoPartnerPdu ;
                        if (contenido.Pdu != null)
                        {
                            codigoPartnerPdu = contenido.Pdu;
                        }
                        else
                        {
                            codigoPartnerPdu = _unitOfWork.PGeneralRepository.ObtenerPdu(contenido.IdMatriculaCabecera);
                        }

                        if (codigoPartnerPdu != null)
                        {
                            contenidoCertificado.PDUs = codigoPartnerPdu;
                            remplazo = remplazo.Replace("{T_Pgeneral.Pdus}", codigoPartnerPdu.ToString());
                        }
                        else
                        {
                            throw new Exception("No se puede Calcular codigoPartnerPdu");
                        }
                    }
                    //reemplazar nombre 1 alumno
                    if (item.Contains("{T_Alumno.NombreCompleto}"))
                    {
                    }
                    if (item.Contains("{tAlumnos.nombre1}"))
                    {
                        remplazo = remplazo.Replace("{tAlumnos.nombre1}", "");

                        if (item.Contains("{tAlumnos.nombre2}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.nombre2}", "");
                        }
                        if (item.Contains("{tAlumnos.apepaterno}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.apepaterno}", "");
                        }
                        if (item.Contains("{tAlumnos.apematerno}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.apematerno}", contenido.NombreAlumno);
                        }
                    }
                    if (item.Contains("{tPEspecifico.ciudad}"))
                    {
                        remplazo = remplazo.Replace("{tPEspecifico.ciudad}", contenido.Ciudad);
                    }
                    //reemplazar Fecha Inicio Capacitacion
                    if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", contenido.FechaInicioCapacitacion);
                    }

                    //reemplazar Fecha Fin Capacitacion
                    if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", contenido.FechaFinCapacitacion);
                    }
                    //reemplazar Fecha Codigo Certificado
                    if (item.Contains("{T_Alumno.CodigoCertificado}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", contenido.CodigoCertificado);
                    }

                    //reemplazar duracion en horas de Programa Especifico
                    if (item.Contains("{tPEspecifico.duracion}"))
                    {
                        remplazo = remplazo.Replace("{tPEspecifico.duracion}", contenido.DuracionPespecifico.Value.ToString());
                    }
                    if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", contenido.FechaEmisionCertificado);
                    }

                    if (item.Contains("{T_Pgeneral.IdCursoIrca}"))
                    {
                        if (ContenidoCertificadoIrca.CursoIrcaId == 0)
                        {
                            throw new Exception("El Registro no tiene IdCursoIrca");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.IdCursoIrca}", ContenidoCertificadoIrca.CursoIrcaId.ToString());
                    }
                    if (item.Contains("{T_Pgeneral.NombreCursoIrca}"))
                    {

                        if (ContenidoCertificadoIrca.NombreCurso == "")
                        {
                            throw new Exception("El Registro no tiene NombreCurso");
                        }
                        contenidoCertificado.NombrePrograma = ContenidoCertificadoIrca.NombreCurso;
                        remplazo = remplazo.Replace("{T_Pgeneral.NombreCursoIrca}", ContenidoCertificadoIrca.NombreCurso);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoCursoIrca}"))
                    {
                        if (ContenidoCertificadoIrca.CodigoCurso == "")
                        {
                            throw new Exception("El Registro no tiene CodigoCurso");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.CodigoCursoIrca}", ContenidoCertificadoIrca.CodigoCurso);
                    }
                    if (item.Contains("{T_Pgeneral.FechaInicioCursoIrca}"))
                    {
                        contenidoCertificado.FechaInicioCapacitacion = ContenidoCertificadoIrca.FechaInicio.ToString("dd-MM-yyyy");
                        remplazo = remplazo.Replace("{T_Pgeneral.FechaInicioCursoIrca}", ContenidoCertificadoIrca.FechaInicio.ToString("dd-MM-yyyy"));
                    }
                    if (item.Contains("{T_Pgeneral.FechaFinCursoIrca}"))
                    {
                        contenidoCertificado.FechaFinCapacitacion = ContenidoCertificadoIrca.FechaFin.ToString("dd-MM-yyyy");
                        remplazo = remplazo.Replace("{T_Pgeneral.FechaFinCursoIrca}", ContenidoCertificadoIrca.FechaFin.ToString("dd-MM-yyyy"));
                    }
                    if (item.Contains("{T_Pgeneral.DuracionCursoIrca}"))
                    {
                        if (ContenidoCertificadoIrca.DuracionCurso == 0)
                        {
                            throw new Exception("El Registro no tiene DuracionCursoIrca");
                        }
                        contenidoCertificado.DuracionPespecifico = ContenidoCertificadoIrca.DuracionCurso;
                        remplazo = remplazo.Replace("{T_Pgeneral.DuracionCursoIrca}", ContenidoCertificadoIrca.DuracionCurso.ToString());
                    }
                    if (item.Contains("{T_Pgeneral.DescripcionResultadoCursoIrca}"))
                    {
                        string resultadoCursoIrca = null;
                        resultadoCursoIrca = _unitOfWork.ContenidoCertificadoIrcaRepository.ObtenerDescripcionResultadoIrca(ContenidoCertificadoIrca.Id);
                        if (ContenidoCertificadoIrca.ResultadoCurso == null)
                        {
                            throw new Exception("El Registro no tiene ResultadoCurso");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.DescripcionResultadoCursoIrca}", resultadoCursoIrca);
                    }
                }
                var pdfFrontal = PdfGenerator.GeneratePdf(remplazo, config);
                var temmporal = ImportarPdfDocument(pdfFrontal);
                PdfSharp.Pdf.PdfPage page = temmporal.Pages[0];
                pdf.AddPage(page);

                if (contenido.IdPlantillaPosterior != 0)
                {
                    config = new PdfGenerateConfig();

                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginLeft = 257;
                    config.MarginRight = 83;
                    config.MarginTop = 49;
                    config.MarginBottom = 0;

                    string _estructuraCurricular = "";
                    listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();
                    var plantillaP = _unitOfWork.PlantillaRepository.FirstById(contenido.IdPlantillaPosterior);
                    var plantillaBaseP = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(contenido.IdPlantillaPosterior);

                    var listaEtiquetaP = plantillaBaseP.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                    foreach (var etiqueta in listaEtiquetaP)
                    {
                        listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = "" });
                    }
                    sepacion = new List<string>();

                    sepacion = plantillaBaseP.Cuerpo.Split("###").ToList();

                    foreach (var item in sepacion)
                    {
                        remplazo = item;

                        if (item.Contains("acute"))
                        {
                            remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                        }
                        if (item.Contains("repositorioweb"))
                        {

                        }

                        if (item.Contains("{tPLA_PGeneral.Nombre}"))
                        {
                            remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", contenido.NombrePrograma);
                        }

                        if (item.Contains("{tPEspecifico.ciudad}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.ciudad}", contenido.Ciudad); ;
                        }
                        if (item.Contains("{tAlumnos.nombre1}"))
                        {

                            remplazo = remplazo.Replace("{tAlumnos.nombre1}", "");

                            if (item.Contains("{tAlumnos.nombre2}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.nombre2}", "");
                            }
                            if (item.Contains("{tAlumnos.apematerno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apematerno}", "");
                            }
                            if (item.Contains("{tAlumnos.apepaterno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apepaterno}", contenido.NombreAlumno);
                            }

                        }

                        //reemplazar Fecha Emision Certificado
                        if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", contenido.FechaEmisionCertificado);
                        }

                        if (item.Contains("{T_Pgeneral.EstructuraCurricular}"))
                        {
                            if (contenido.EstructuraCurricular != null)
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", contenido.EstructuraCurricular);
                                _estructuraCurricular = contenido.EstructuraCurricular;
                                contenidoCertificado.EstructuraCurricular = _estructuraCurricular;
                            }
                            else
                            {
                                var estructuraPorVersion = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerCursosEstrucuraCurricular(ContenidoCertificadoIrca.IdMatriculaCabecera);
                                if (estructuraPorVersion.Count > 1)
                                {
                                    string listaEstructura = $@"<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                    listaEstructura += "<ul style='margin-top:-20px'>";
                                    foreach (var hijo in estructuraPorVersion)
                                    {
                                        listaEstructura += $@"<li style='padding-bottom:5px'>{hijo.Nombre.Replace(" - COLOMBIA", "")} ({hijo.Duracion} horas cronológicas)</li>";
                                    }


                                    listaEstructura += "</ul></td></tr></table>";
                                    _estructuraCurricular = listaEstructura;
                                    contenidoCertificado.EstructuraCurricular = listaEstructura;
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                                }
                                else
                                {
                                    var estructuraCurso = _unitOfWork.DocumentoSeccionPwRepository.ObtenerEstructuraCurso(ContenidoCertificadoIrca.IdMatriculaCabecera);
                                    if (estructuraCurso.Count > 0)
                                    {
                                        string listaEstructura = $@"
                                                        <table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                        listaEstructura += "<ul style='margin-top:-20px'>";

                                        foreach (var capitulo in estructuraCurso)
                                        {
                                            listaEstructura += $@"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")} </li>";
                                        }

                                        listaEstructura += "</ul></td></tr></table>";
                                        _estructuraCurricular = listaEstructura;
                                        contenidoCertificado.EstructuraCurricular = listaEstructura;
                                        remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                                    }
                                    else
                                    {
                                        remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", "");
                                    }

                                }
                            }


                        }
                        if (item.Contains("Template"))
                        {

                            var etiq = listaObjetoWhasApp.Where(x => x.Codigo.Contains("Template")).ToList();
                            SeccionEtiquetaDTO valor = new SeccionEtiquetaDTO();
                            string etiqueta = "";

                            foreach (var a in etiq)
                            {
                                List<string> ListaPalabras = new List<string>();
                                char[] delimitador = new char[] { '.' };
                                string IdPlantilla = "";
                                string IdColumna = "";
                                string[] array = a.Codigo.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string s in array)
                                {
                                    ListaPalabras.Add(s);
                                }
                                IdPlantilla = ListaPalabras[3].ToString();
                                IdColumna = ListaPalabras[4].ToString();


                                var prevalor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna.Replace("}", "")), oportunidad.IdCentroCosto ?? default(int));

                                if (prevalor != null && _estructuraCurricular == "")
                                {
                                    etiqueta = a.Codigo;
                                    valor = prevalor;
                                }
                                else
                                {
                                    remplazo = remplazo.Replace(a.Codigo, "");
                                }

                            }
                            if (etiqueta != "")
                            {
                                if (contenido.EstructuraCurricular != null && contenido.EstructuraCurricular != "")
                                {
                                    remplazo = remplazo.Replace(etiqueta, contenido.EstructuraCurricular);
                                }
                                else
                                {
                                    var tratamiento = valor.Valor.Replace("<p>", "<p id='estructura'<p>");
                                    tratamiento = tratamiento.Replace("<li>", "<li id='liseparar'<li>");
                                    tratamiento = tratamiento.Replace("&bull", "<li id='liseparar'");
                                    estructura = tratamiento.Split("<p id='estructura'").Where(x => x.StartsWith("<p><strong>")).ToList();
                                    List<string> todo = new List<string>();

                                    int contador = 0;
                                    string htmltabla = "";
                                    List<string> total = new List<string>();
                                    foreach (var li in estructura)
                                    {
                                        foreach (var li1 in li.Split("<li id='liseparar'").ToList())
                                        {
                                            if (li1.Contains("<p>"))
                                                total.Add(li1.Replace("p>", "li>").Replace("<li>", "<li style='padding-bottom:5px'>").Replace("strong", "span").Replace("<ul>", "").Replace("</ul>", "").Replace("\"", "'").Replace("<div class='slide'>", "").Replace("<p style='padding-left:30px;'>", ""));

                                        }

                                    }
                                    int Cantidadcolumns = total.Count / 25;
                                    int residuo = total.Count % 25;
                                    if (residuo != 0)
                                    {
                                        Cantidadcolumns++;
                                    }
                                    int reparticion = Cantidadcolumns;

                                    PdfPTable PdfTable = new PdfPTable(Cantidadcolumns);
                                    PdfTable.WidthPercentage = 100f;
                                    PdfPCell PdfPCell = null;
                                    string cadena1 = "";
                                    string cadena2 = "";
                                    string cadena3 = "";
                                    string cadena4 = "";
                                    List<string> registros = new List<string>();
                                    foreach (var concatenar in total)
                                    {
                                        //int residuo = Conteo % 27;
                                        if (Conteo < 22)
                                        {
                                            if (Conteo == 0)
                                            {
                                                cadena1 += "<ul style='margin-top:-20px'>";
                                            }
                                            cadena1 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                        }
                                        else
                                        {
                                            if (Conteo < 48)
                                            {
                                                //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                                cadena2 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                            else
                                            {
                                                if (Conteo < 75)
                                                {
                                                    //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                                    cadena3 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                                }
                                                else
                                                {
                                                    //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                                    cadena4 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                                }
                                            }
                                        }

                                        Conteo++;
                                    }

                                    htmltabla += "<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>";
                                    contador = 0;
                                    for (int rows = 0; rows < 1; rows++)
                                    {
                                        htmltabla += "<tr style='vertical-align: text-top;'>";
                                        for (int column = 0; column < Cantidadcolumns; column++)
                                        {
                                            if (column == 0)
                                            {
                                                if (cadena1.TrimEnd(' ').EndsWith("</li>\n"))
                                                {
                                                    cadena1 = cadena1 + "</ul>";
                                                }
                                                htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena1}";
                                            }
                                            if (column == 1)
                                            {
                                                if (cadena2.StartsWith("<li>"))
                                                {
                                                    cadena2 = "<ul>" + cadena2;
                                                }
                                                if (cadena2.TrimEnd(' ').EndsWith("</li>\n"))
                                                {
                                                    cadena2 = cadena2 + "</ul>";
                                                }
                                                htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena2}";
                                            }
                                            if (column == 2)
                                            {
                                                if (cadena3.StartsWith("<li>"))
                                                {
                                                    cadena3 = "<ul>" + cadena3;
                                                }
                                                if (cadena3.TrimEnd(' ').EndsWith("</li>\n"))
                                                {
                                                    cadena3 = cadena3 + "</ul>";
                                                }
                                                htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena3}";
                                            }
                                            if (column == 3)
                                            {
                                                if (cadena4.StartsWith("<li>"))
                                                {
                                                    cadena4 = "<ul>" + cadena4;
                                                }
                                                if (cadena4.TrimEnd(' ').EndsWith("</li>\n"))
                                                {
                                                    cadena4 = cadena4 + "</ul>";
                                                }
                                                htmltabla += $@"<th style='vertical-align:top;text-align:left;font-weight: normal;'> {cadena4}";
                                            }

                                            contador = contador + 1;
                                            htmltabla += "</td>";
                                        }
                                        htmltabla += "</tr>";
                                    }
                                    htmltabla += "</table>";
                                    PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table
                                    contenidoCertificado.EstructuraCurricular = htmltabla;
                                    remplazo = remplazo.Replace(etiqueta, htmltabla);
                                }

                            }
                        }
                    }
                    var pdfPosterior = PdfGenerator.GeneratePdf(remplazo, config);
                    temmporal = ImportarPdfDocument(pdfPosterior);
                    page = temmporal.Pages[0];
                    pdf.AddPage(page);
                }

                //_document.Close();
                pdf.Save(ms, false);
                return ms.ToArray();
            }
        }
    }
}
