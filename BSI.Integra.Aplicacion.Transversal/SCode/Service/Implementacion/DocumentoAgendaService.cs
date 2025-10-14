using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Clases;
using BSI.Integra.Aplicacion.Transversal.SCode.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DocumentoAgendaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 03/08/2022
    /// <summary>
    /// Gestión general de T_DocumentoAgenda
    /// </summary>
    public class DocumentoAgendaService : IDocumentoAgendaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DocumentoAgendaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TDocumentoAgendum, DocumentoAgenda>(MemberList.None).ReverseMap();
                    cfg.CreateMap<DocumentoAgendaSinAuditoriaDTO, DocumentoAgendaDescargaDTO>(MemberList.None);
                    cfg.CreateMap<ProgramaCuotasDTO, ProgramaCuotasDetalleDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentoAgenda
        /// </summary>
        /// <returns> List<DocumentoAgendaDTO> </returns>
        public IEnumerable<DocumentoAgendaDTO> ObtenerDocumentoAgenda()
        {
            try
            {
                return _unitOfWork.DocumentoAgendaRepository.ObtenerDocumentoAgenda();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoAgenda para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoAgendaComboDTO> </returns>
        public IEnumerable<DocumentoAgendaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DocumentoAgendaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_DocumentoAgenda obviando los Campos de Auditoria.
        /// </summary>
        /// <returns> List<DocumentoAgendaSinAuditoriaDTO> </returns>
        public IEnumerable<DocumentoAgendaSinAuditoriaDTO> ObtenerDocumentoAgendaSinAuditoria()
        {
            try
            {
                return _unitOfWork.DocumentoAgendaRepository.ObtenerDocumentoAgendaSinAuditoria();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Documentos de Agenda para su Descarga (url o arreglo de bytes asignados) asociados a una Actividad Detalle.
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> List<DocumentoAgendaDescargaDTO> </returns>
        public DocumentoAgendaDetalleDTO ObtenerDocumentoAgendaDetallePorIdActividadDetalle(int idActividadDetalle)
        {
            try
            {
                IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                IPEspecificoService pEspecificoService = new PEspecificoService(_unitOfWork);
                IPGeneralService pGeneralService = new PGeneralService(_unitOfWork);

                var oportunidad = oportunidadService.ObtenerOportunidadCompuestoPorIdActividadDetalle(idActividadDetalle);
                var programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(oportunidad.IdCentroCosto!.Value);
                var programaGeneral = pGeneralService.ObtenerPGeneralAtributosPrincipalesPorId(programaEspecifico.IdProgramaGeneral!.Value);

                var documentosDescarga = _mapper.Map<List<DocumentoAgendaDescargaDTO>>(ObtenerDocumentoAgendaSinAuditoria());
                documentosDescarga = documentosDescarga.Select(
                    d => d.Generado ?
                        ObtenerBytesDocumentoAgenda(d, programaEspecifico, programaGeneral, oportunidad) :
                        ObtenerUrlDocumentoAgenda(d, programaEspecifico, programaGeneral)
                ).ToList();

                return new DocumentoAgendaDetalleDTO
                {
                    Oportunidad = oportunidad,
                    ProgramaEspecifico = programaEspecifico,
                    Documentos = documentosDescarga
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Url del Documento de Agenda asociado a un Id y a un Id de Pais
        /// </summary>
        /// <param name="idDocumentoAgenda">Id de Documento Agenda</param>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerDocumentoAgendaUrlPorPais(int idDocumentoAgenda, int idPais)
        {
            try
            {
                return _unitOfWork.DocumentoAgendaRepository.ObtenerDocumentoAgendaUrlPorPais(idDocumentoAgenda, idPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Url del Documento de Agenda asociado a un Id y a un Id de Pais
        /// </summary>
        /// <param name="documento">DocumentoAgenda base sobre el cual se añaden el Url y el Mensaje</param>
        /// <param name="programaEspecifico">Datos de Programa Especifico</param>
        /// <param name="programaGeneral">Datos de Programa Especifico</param>
        /// <returns> DocumentoAgendaDescargaDTO </returns>
        public DocumentoAgendaDescargaDTO ObtenerUrlDocumentoAgenda(
            DocumentoAgendaDescargaDTO documento,
            PEspecificoPorIdCentroCostoDTO programaEspecifico,
            PGeneralAtributosPrincipalesDTO programaGeneral
        )
        {
            try
            {
                if (documento.Id == 1) //Brochure BSG Inst.
                {
                    documento.Mensaje = "Correcto";
                    documento.Url = ObtenerDocumentoAgendaUrlPorPais(documento.Id, 0).Valor;
                }

                if (documento.Id == 2) //Brochure
                {

                    if (programaGeneral != null)
                    {
                        if (programaGeneral.UrlBrochurePrograma != null)
                        {
                            documento.Mensaje = "Correcto";
                            documento.Url = programaGeneral.UrlBrochurePrograma;
                        }
                        else
                        {
                            documento.Mensaje = "Incorrecto";
                        }
                    }
                }

                if (documento.Id == 3) //Cronograma Alumnos
                {
                    if (programaEspecifico.Tipo != "Online Asincronica" && programaEspecifico.UrlDocumentoCronograma != null)
                    {
                        documento.Mensaje = "Correcto";
                        documento.Url = programaEspecifico.UrlDocumentoCronograma;
                    }
                    else
                    {
                        documento.Mensaje = "Incorrecto";
                    }
                }

                if (documento.Id == 5) //Pagare Word
                {
                    if (programaEspecifico.Ciudad != null)
                    {
                        if (programaEspecifico.Ciudad == "BOGOTA")
                            documento.Url = ObtenerDocumentoAgendaUrlPorPais(documento.Id, 57).Valor; // Colombia

                        else if (new string[] { "SANTA CRUZ", "LA PAZ" }.Contains(programaEspecifico.Ciudad))
                            documento.Url = ObtenerDocumentoAgendaUrlPorPais(documento.Id, 591).Valor; // Bolivia

                        else
                            documento.Url = ObtenerDocumentoAgendaUrlPorPais(documento.Id, 0).Valor; // Peru y Extranjero

                        documento.Mensaje = "Correcto";
                    }
                    else
                    {
                        documento.Mensaje = "Incorrecto";
                    }
                }

                if (documento.Id == 6) //Pagare Excel
                {
                    if (programaEspecifico.Ciudad != null)
                    {
                        if (new string[] { "AREQUIPA", "LIMA" }.Contains(programaEspecifico.Ciudad.ToUpper()))
                            documento.Url = ObtenerDocumentoAgendaUrlPorPais(documento.Id, 51).Valor; // Peru

                        else if (programaEspecifico.Ciudad.ToUpper() == "BOGOTA")
                            documento.Url = ObtenerDocumentoAgendaUrlPorPais(documento.Id, 57).Valor; // Colombia

                        else if (new string[] { "SANTA CRUZ", "LA PAZ" }.Contains(programaEspecifico.Ciudad))
                            documento.Url = ObtenerDocumentoAgendaUrlPorPais(documento.Id, 591).Valor; // Bolivia

                        else
                            documento.Url = ObtenerDocumentoAgendaUrlPorPais(documento.Id, 0).Valor; // Peru y Extranjero

                        documento.Mensaje = "Correcto";
                    }
                    else
                    {
                        documento.Mensaje = "Incorrecto";
                    }
                }

                if (documento.Id == 13) //Cronograma Alumnos (Todos los Grupos)
                {
                    if (programaEspecifico.Tipo != "Online Asincronica" && programaEspecifico.UrlDocumentoCronogramaGrupos != null)
                    {
                        documento.Mensaje = "Correcto";
                        documento.Url = programaEspecifico.UrlDocumentoCronogramaGrupos;
                    }
                    else
                    {
                        documento.Mensaje = "Incorrecto";
                    }
                }

                return documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Añade el arreglo de bytes del documento correspondiente
        /// </summary>
        /// <param name="documento">DocumentoAgenda base sobre el cual se añaden el Url y el Mensaje</param>
        /// <param name="programaEspecifico">Datos de Programa Especifico</param>
        /// <param name="programaGeneral">Datos de Programa Especifico</param>
        /// <returns> DocumentoAgendaDescargaDTO </returns>
        public DocumentoAgendaDescargaDTO ObtenerBytesDocumentoAgenda(
            DocumentoAgendaDescargaDTO documento,
            PEspecificoPorIdCentroCostoDTO programaEspecifico,
            PGeneralAtributosPrincipalesDTO programaGeneral,
            OportunidadCompuestoDTO oportunidad
        )
        {
            try
            {
                if (documento.Id == 4) // Requisitos Programas Online
                {
                    if (programaEspecifico.Tipo == "Online Sincronica")
                    {
                        byte[] requisitos = GenerarRequisitos(oportunidad);
                        if (requisitos != null)
                        {
                            documento.Mensaje = "Correcto";
                            documento.DocumentoByte = requisitos;
                            documento.Url = "archivoGenerado";
                        }
                        else
                        {
                            documento.Mensaje = "Incorrecto";
                        }
                    }
                }

                if (documento.Id == 7) // Convenio de Capacitacion
                {
                    var documentoConvenio = GenerarConvenioCondicion(oportunidad, "Convenio", documento.Id);
                    if (documentoConvenio != null)
                    {
                        documento.Mensaje = "Correcto";
                        documento.Url = "archivoGenerado";
                        documento.DocumentoByte = documentoConvenio;
                    }
                    else
                    {
                        documento.Mensaje = "Incorrecto";
                    }
                }

                if (documento.Id == 8) // Condiciones y Caracteristicas
                {
                    documento.Mensaje = "Incorrecto";
                    if (programaEspecifico.Tipo == "Online Asincronica" || programaEspecifico.Tipo == "Online Sincronica")
                    {
                        var documentoCondiciones = GenerarConvenioCondicion(oportunidad, "Condiciones", documento.Id);
                        if (documentoCondiciones != null)
                        {
                            documento.Mensaje = "Correcto";
                            documento.Url = "archivoGenerado";
                            documento.DocumentoByte = documentoCondiciones;
                        }
                    }
                }

                if (documento.Id == 9) // Silabo del Programa
                {
                    documento.Mensaje = "Incorrecto";
                    string documentoSilabo = GeneraSilabo(programaGeneral);
                    if (!string.IsNullOrEmpty(documentoSilabo))
                    {
                        string _urlConcatenada = programaEspecifico.IdProgramaGeneral + "*" + documentoSilabo;
                        var DocumentoSilaboPDF = generar_silabo_bytes(programaGeneral);
                        documento.Mensaje = "Correcto";
                        documento.Url = _urlConcatenada;
                        documento.DocumentoByte = DocumentoSilaboPDF;
                    }
                }

                if (documento.Id == 10) // Contrato Uso de Datos
                {
                    documento.Mensaje = "Incorrecto";
                    if (programaEspecifico.Tipo == "Online Asincronica" || programaEspecifico.Tipo == "Online Sincronica" || programaEspecifico.Tipo == "Presencial")
                    {
                        byte[] documentoContrato = GenerarContratoUsoDatos(oportunidad, "Contrato", documento.Id);
                        if (documentoContrato != null)
                        {
                            documento.Mensaje = "Correcto";
                            documento.Url = "archivoGenerado";
                            documento.DocumentoByte = documentoContrato;
                        }
                    }
                }

                if (documento.Id == 11) // Lineamientos y Condiciones
                {
                    documento.Mensaje = "Incorrecto";
                    if (programaEspecifico.Tipo == "Online Asincronica" || programaEspecifico.Tipo == "Online Sincronica" || programaEspecifico.Tipo == "Presencial")
                    {
                        byte[] DocumentoContrato = GenerarContratoUsoDatos(oportunidad, "Lineamiento", documento.Id);
                        if (DocumentoContrato != null)
                        {
                            documento.Mensaje = "Correcto";
                            documento.Url = "archivoGenerado";
                            documento.DocumentoByte = DocumentoContrato;
                        }
                    }
                }

                if (documento.Id == 12) // Convenio de Prestacion de Servicios
                {
                    documento.Mensaje = "Incorrecto";
                    if (programaEspecifico.Tipo == "Online Asincronica" || programaEspecifico.Tipo == "Online Sincronica" || programaEspecifico.Tipo == "Presencial")
                    {
                        byte[] DocumentoContrato = GenerarContratoUsoDatos(oportunidad, "Convenio Prestacion", 11);//idDocumento 11
                        if (DocumentoContrato != null)
                        {
                            documento.Mensaje = "Correcto";
                            documento.Url = "archivoGenerado";
                            documento.DocumentoByte = DocumentoContrato;
                        }
                    }
                }

                return documento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera el Documento de Requisitos
        /// </summary>
        /// <param name="oportunidad">Datos de la Oportunidad</param>
        /// <returns> byte[] </returns>
        private byte[]? GenerarRequisitos(OportunidadCompuestoDTO oportunidad)
        {
            string modalidad = string.Empty;
            string nombrePrograma = string.Empty;
            int IdProgramaEspecifico = 0;
            byte[] RequisitosByte = new byte[100];

            var servicioAlumno = new AlumnoService(_unitOfWork);
            var alumnoUbicacion = servicioAlumno.ObtenerCiudadPaisPorIdAlumno(oportunidad.IdAlumno!.Value);

            if (alumnoUbicacion.IdCiudad == null)
            {
                throw new BadRequestException("El Alumno no tiene Ciudad.");
            }
            if (alumnoUbicacion.IdCodigoPais == null)
            {
                throw new BadRequestException("El Alumno no tiene Pais.");
            }
            var datosAlumno = servicioAlumno.ObtenerDatosDocumentoPorIdAlumno(oportunidad.IdAlumno.Value);

            try
            {
                var servicioMatricula = new MatriculaCabeceraService(_unitOfWork);
                ProgramaCuotasDetalleDTO? ListaCuotasProgramaDTO = ObtenerCuotasPrograma(oportunidad.Id);
                if (ListaCuotasProgramaDTO != null)
                {
                    modalidad = ListaCuotasProgramaDTO.TipoPrograma;
                    nombrePrograma = ListaCuotasProgramaDTO.NombreCurso ?? "";
                    IdProgramaEspecifico = ListaCuotasProgramaDTO.IdPespecifico;
                }
                else
                {
                    return null;
                }

                switch (modalidad)
                {
                    case "Online Asincronica":
                        RequisitosByte = GenerarDocumentoRequisitosOnline(datosAlumno, nombrePrograma, "Online Asincronica");
                        break;
                    case "Online Sincronica":
                        RequisitosByte = GenerarDocumentoRequisitosOnline(datosAlumno, nombrePrograma, "Online Sincronica");
                        break;
                }

                return RequisitosByte;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Cuotas del Programa
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> byte[] </returns>
        private ProgramaCuotasDetalleDTO? ObtenerCuotasPrograma(int idOportunidad)
        {
            MatriculaCabeceraCodigoFechaDTO? datosMatricula = _unitOfWork.MatriculaCabeceraRepository.ObtenerDatosMatriculaDesdeMontoPagoPorIdOportunidad(idOportunidad);

            if (datosMatricula != null && datosMatricula.Id > 0)
            {
                return ObtenerCronograma(datosMatricula.Id);
            }
            else
            {
                datosMatricula = _unitOfWork.MatriculaCabeceraRepository.ObtenerDatosMatriculaDesdeClasificacionPersonaPorIdOportunidad(idOportunidad);
                if (datosMatricula == null)
                {
                    return null;
                }
                return ObtenerCronograma(datosMatricula.Id);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Cuotas del Programa
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de Matricula Cabecera</param>
        /// <returns> ProgramaCuotasDetalleDTO </returns>
        public ProgramaCuotasDetalleDTO ObtenerCronograma(int idMatriculaCabecera)
        {
            var servicioCronogramaDetalle = new CronogramaPagoDetalleFinalService(_unitOfWork);
            var servicioCronogramaPago = new CronogramaPagoService(_unitOfWork);

            ProgramaCuotasDetalleDTO programaCuotas =
                _mapper.Map<ProgramaCuotasDetalleDTO>(servicioCronogramaPago.ObtenerProgramaCuotasPorIdMatriculaCabecera(idMatriculaCabecera));


            if (programaCuotas != null)
            {
                programaCuotas.Cuotas = new List<CronogramaPagoDetalleFinalCuotaDTO>();
                List<CronogramaPagoDetalleFinalCuotaDTO> CronogramaPagoDetalleMod = servicioCronogramaDetalle.ObtenerListaCuotaPorIdMatriculaCabecera(idMatriculaCabecera).ToList();

                if (CronogramaPagoDetalleMod != null)
                {
                    foreach (var _CronogramaPagoDetalleMod in CronogramaPagoDetalleMod)
                    {
                        programaCuotas.Cuotas.Add(_CronogramaPagoDetalleMod);
                    }
                }
                decimal Porcentaje = 0.005M;

                foreach (var cuotaT in programaCuotas.Cuotas)
                {
                    int NroDias = Convert.ToInt32((DateTime.Now.Date - cuotaT.FechaVencimiento!.Value).TotalDays);
                    if (NroDias > 0 && cuotaT.Cancelado!.Value == false)
                    {
                        cuotaT.Mora = cuotaT.Mora + decimal.Round(((cuotaT.Cuota!.Value + cuotaT.Mora!.Value) * Porcentaje) * NroDias, 2, MidpointRounding.AwayFromZero);
                    }
                }

                if (programaCuotas.WebMoneda == "2")
                {
                    foreach (var cuotaT in programaCuotas.Cuotas)
                    {
                        cuotaT.Cuota = cuotaT.MontoCuotaDescuento;
                        cuotaT.Mora = Math.Round(cuotaT.Mora!.Value * programaCuotas.WebTipoCambio!.Value, 2, MidpointRounding.AwayFromZero);
                        cuotaT.Moneda = "Pesos Colombianos";
                    }
                }

                return programaCuotas;
            }
            else
            {
                return programaCuotas;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Cuotas del Programa
        /// </summary>
        /// <param name="Alumno">Informacion del Alumno</param>
        /// <param name="NombrePrograma">Nombre del Programa</param>
        /// <param name="Alumno">Modalidad del Programa</param>
        /// <returns> byte[] </returns>
        private byte[] GenerarDocumentoRequisitosOnline(AlumnoDatosDocumentoDTO Alumno, string NombrePrograma, string Modalidad)
        {
            string NombreDocumento = string.Empty;
            string NombreDocumentoTemp = string.Empty;
            string SubCabecera = string.Empty;
            string Nombres, Apellidos;
            string _queryContenido;
            string contenido = string.Empty;

            string DireccionRequisitosSH = @"~/repositorioweb/documentos/requisitos-sh/";

            FontFactory.RegisterDirectories();
            Document document = new Document(PageSize.A4, 70, 50, 50, 50);
            try
            {
                if (Modalidad == "Online Asincronica")
                {
                    NombreDocumento = DireccionRequisitosSH + "declaracion-jurada-asincronico " + Alumno.Nombre1 + Alumno.ApellidoPaterno + ".pdf";
                    NombreDocumentoTemp = "declaracion-jurada-asincronico " + Alumno.Nombre1 + Alumno.ApellidoPaterno + ".pdf";

                    contenido = _unitOfWork.DocumentacionComercialPwRepository.ObtenerContenidoDocumentoComercial("Requisitos", "OnlineAsincronico", 0).Valor;

                    SubCabecera = "Declaración jurada para programas asincrónicos";
                }
                else
                {
                    NombreDocumento = DireccionRequisitosSH + "requisitos-hadware-software " + Alumno.Nombre1 + Alumno.ApellidoPaterno + ".pdf";
                    NombreDocumentoTemp = "requisitos-hadware-software " + Alumno.Nombre1 + Alumno.ApellidoPaterno + ".pdf";

                    contenido = _unitOfWork.DocumentacionComercialPwRepository.ObtenerContenidoDocumentoComercial("Requisitos", "OnlineAsincronico", 0).Valor;

                    SubCabecera = "Requisitos de hardware y software para participar en programas online";
                }

                //primero seteamos la informacion del alumno
                Nombres = Alumno.Nombre1 + " " + Alumno.Nombre2;
                Apellidos = Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno;

                contenido = contenido.Replace("##NOMBRESALUMNO##", Nombres + " " + Apellidos).Replace("##NRODOCUMENTO##", Alumno.Dni);
                contenido = contenido.Replace("##NOMBREPROGRAMA##", NombrePrograma);

                HTMLWorker htmlparser = new HTMLWorker(document);
                htmlparser.SetStyleSheet(GenerateStyleSheet2());

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
                Chunk titleFont2 = new iTextSharp.text.Chunk(SubCabecera, FontFactory.GetFont("Times New Roman", 10, Font.BOLD));

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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Generar Documentos de Convenio o Condicion segun los parametros.
        /// </summary>
        /// <param name="Oportunidad"> Parametros de Oportunidad</param>
        /// <param name="TipoDocumento"> Convenio o Condiciones</param>
        /// <param name="idDocumento"> Id del documento a generar</param>
        /// <returns> ProgramaCuotasDetalleDTO </returns>
        private byte[] GenerarConvenioCondicion(OportunidadCompuestoDTO oportunidad, string TipoDocumento, int idDocumento)
        {
            string Modalidad = string.Empty;
            string NombrePrograma = string.Empty;
            byte[] Convenio = new byte[1000];
            byte[] Condiciones = new byte[1000];
            int TipoPago = 0, Pais = 0, IdProgramaEspecifico = 0;

            var servicioAlumno = new AlumnoService(_unitOfWork);
            var alumnoUbicacion = servicioAlumno.ObtenerCiudadPaisPorIdAlumno(oportunidad.IdAlumno!.Value);

            if (alumnoUbicacion.IdCiudad == null)
            {
                throw new Exception("El Alumno no tiene Ciudad.");
            }
            if (alumnoUbicacion.IdCodigoPais == null)
            {
                throw new Exception("El Alumno no tiene Pais.");
            }

            var datosAlumno = servicioAlumno.ObtenerDatosDocumentoPorIdAlumno(oportunidad.IdAlumno.Value);
            MontoPagoCronogramaDocumentoDTO montoPago = _unitOfWork.MontoPagoCronogramaRepository.ObtenerConogramaPagoDocumentoPorIdOportunidad(oportunidad.Id);

            if (montoPago.IdMontoPago == null)
            {
                montoPago = _unitOfWork.MontoPagoCronogramaRepository.ObtenerConogramaPagoDocumentoPorIdOportunidadOperaciones(oportunidad.Id);
            }


            if (montoPago.IdMontoPago != null)
            {
                //Agregado para Etiquetas
                var servicioMoneda = new MonedaService(_unitOfWork);
                var moneda = servicioMoneda.ObtenerMonedaParaDocumento(montoPago.IdMoneda!.Value);

                oportunidad.CostoTotalConDescuento = moneda.Simbolo + " " + montoPago.PrecioDescuento + " " + moneda.NombrePlural;

                var servicioMontoPago = new MontoPagoService(_unitOfWork);
                string paquete = _unitOfWork.MontoPagoRepository.ObtenerPaquetePorIdMontoPago(montoPago.IdMontoPago!.Value).Valor;

                if (paquete != null)
                {
                    datosAlumno.Paquete = paquete == null ? "" : paquete;
                }
                else
                {
                    datosAlumno.Paquete = "";
                }
            }
            else
            {
                datosAlumno.Paquete = "";
            }
            try
            {
                ProgramaCuotasDetalleDTO? ListaCuotasProgramaDTO = ObtenerCuotasPrograma(oportunidad.Id);
                if (ListaCuotasProgramaDTO == null)
                {
                    return null;
                }
                Modalidad = ListaCuotasProgramaDTO.TipoPrograma;
                NombrePrograma = ListaCuotasProgramaDTO.NombreCurso;
                IdProgramaEspecifico = ListaCuotasProgramaDTO.IdPespecifico;

                if (datosAlumno != null)
                {
                    if (datosAlumno.IdCodigoPais == 57)
                    {
                        Pais = 57;
                    }
                    else if (datosAlumno.IdCodigoPais == 591)
                    {
                        Pais = 591;
                    }
                    else if (datosAlumno.IdCodigoPais == 52)
                    {
                        Pais = 52;
                    }
                    else if (datosAlumno.IdCodigoPais == 56)
                    {
                        Pais = 56;
                    }
                    else
                    {
                        Pais = 51;
                    }
                }
                else
                {
                    Pais = 51;
                }
                datosAlumno!.IdOportunidad = oportunidad.Id;
                if (TipoDocumento == "Convenio")
                {
                    switch (Modalidad)
                    {
                        case "Presencial":
                            Convenio = GenerarPDFConvenioCondiciones(datosAlumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Presencial");
                            break;
                        case "Online Asincronica":
                            Convenio = GenerarPDFConvenioCondiciones(datosAlumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");
                            break;
                        case "Online Sincronica":
                            Convenio = GenerarPDFConvenioCondiciones(datosAlumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");
                            break;
                    }
                    return Convenio;
                }
                else
                {
                    switch (Modalidad)
                    {
                        case "Online Asincronica":
                            Condiciones = GenerarPDFConvenioCondiciones(datosAlumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");

                            break;
                        case "Online Sincronica":
                            Condiciones = GenerarPDFConvenioCondiciones(datosAlumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");

                            break;
                    }
                    return Condiciones;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Cuotas del Programa
        /// </summary>
        /// <param name="Alumno">Informacion del Alumno</param>
        /// <param name="NombrePrograma">Nombre del Programa</param>
        /// <param name="Alumno">Modalidad del Programa</param>
        /// <returns> byte[] </returns>
        private byte[] GenerarPDFConvenioCondiciones(AlumnoDatosDocumentoDTO alumno, ProgramaCuotasDetalleDTO listaCuotasProgramaDTO, int tipoPago, int pais, string tipoDocumento, string modalidad)
        {
            try
            {
                string nombreArchivo = "";
                string blobDirecion = "";
                string raiz = "https://repositorioweb.blob.core.windows.net/documentos/convenios/";

                byte[] pdf = BlobGenerarConvenioCondiciones(alumno, listaCuotasProgramaDTO, tipoPago, pais, tipoDocumento, modalidad);

                //registrarDocumentoBlob(pdf, nombreArchivo, blobDirecion, "pdf");

                raiz = ToURLSlug(nombreArchivo) + ".pdf";
                //return raiz;
                return pdf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera convenio de condiciones, contrato mexico,lineamientos mexico,convenio de mexico.
        /// </summary>
        /// <param name="alumno">Informacion del Alumno</param>
        /// <param name="NombrePrograma">Nombre del Programa</param>
        /// <param name="Alumno">Modalidad del Programa</param>
        /// <returns> string </returns>
        private byte[] BlobGenerarConvenioCondiciones(AlumnoDatosDocumentoDTO alumno, ProgramaCuotasDetalleDTO listaCuotasProgramaDTO, int tipoPago, int pais, string tipoDocumento, string modalidad)
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
            string contenido = String.Empty;
            var servicioPais = new PaisService(_unitOfWork);
            var servicioCiudad = new CiudadService(_unitOfWork);
            var servicioMatricula = new MatriculaCabeceraService(_unitOfWork);
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var servicioMontoPago = new MontoPagoService(_unitOfWork);

            //Creacion del archivo en memoria
            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (iTextSharp.text.Document _document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 70, 50, 50, 50))
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
                        contenido = _unitOfWork.DocumentacionComercialPwRepository.ObtenerContenidoDocumentoComercial(tipoDocumento, modalidad, tipoPais).Valor;

                        if (contenido == null)
                        {
                            return null;
                        }

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

                        contenido = contenido.Replace("##PRIMERNOMBREALUMNO##", alumno.Nombre1.ToUpper());
                        contenido = contenido.Replace("##NOMBRESALUMNO##", nombres.ToUpper() + " " + apellidos.ToUpper()).Replace("##TIPONRODOCUMENTO##", alumno.Dni);
                        int codpais = 0;
                        if (alumno.IdCodigoPais != null)
                        {
                            codpais = Convert.ToInt32(alumno.IdCodigoPais);
                        }

                        string pais_ = _unitOfWork.PaisRepository.ObtenerNombrePaisPorId(codpais).Valor;

                        int codciudad = 0;
                        if (alumno.IdCiudad != null)
                        {
                            codciudad = Convert.ToInt32(alumno.IdCiudad);
                        }


                        string ciudad_ = servicioCiudad.ObtenerNombreCiudadPorId(codciudad).Valor;

                        contenido = contenido.Replace("##DIRECCION##", alumno.Direccion).Replace("##CIUDAD##", ciudad_);
                        contenido = contenido.Replace("##REGION##", alumno.NombreCiudad).Replace("##PAIS##", pais_);
                        contenido = contenido.Replace("##CORREO##", alumno.Correo);

                        if (alumno.Paquete == null)
                        {
                            alumno.Paquete = "";
                        }

                        MatriculaCabeceraCodigoFechaDTO? matricula = _unitOfWork.MatriculaCabeceraRepository.ObtenerDatosMatriculaDesdeMontoPagoPorIdOportunidad(alumno.IdOportunidad.Value);

                        var IdMatricula = "";

                        if (matricula != null && matricula.CodigoMatricula != null)
                        {

                            IdMatricula = matricula.CodigoMatricula;
                        }
                        else
                        {
                            matricula = _unitOfWork.MatriculaCabeceraRepository.ObtenerDatosMatriculaDesdeClasificacionPersonaPorIdOportunidad(alumno.IdOportunidad.Value);
                            if (matricula != null && matricula.CodigoMatricula != null)
                            {
                                IdMatricula = matricula.CodigoMatricula;
                            }
                        }

                        DateTime? fecha_matri = new DateTime(2017, 9, 10);
                        if (matricula != null && matricula.FechaMatricula != null)
                        {
                            fecha_matri = matricula.FechaMatricula;
                        }

                        DateTime validador_fecha = new DateTime(2017, 9, 11);

                        if (fecha_matri < validador_fecha)
                        {
                            contenido = contenido.Replace("##VERSION##", "");
                        }
                        else
                        {
                            contenido = contenido.Replace("##VERSION##", GenerarVersion(alumno.Paquete));
                        }

                        int? idBusqueda = 0;

                        //segundo remplazamos los datos del programa y del cronograma

                        idBusqueda = listaCuotasProgramaDTO.IdBusqueda;

                        contenido = contenido.Replace("##NOMBREPROGRAMA##", listaCuotasProgramaDTO.NombreCurso);
                        contenido = contenido.Replace("##DURACIONDIAS##", listaCuotasProgramaDTO.DuracionPespecifico + " horas");
                        contenido = contenido.Replace("##DURACIONMESES##", listaCuotasProgramaDTO.DuracionPGeneral + "");

                        if (listaCuotasProgramaDTO.NumeroCuotas != 1)
                        {
                            contenido = contenido.Replace("##FRACCIONADO##,", "en caso de solicitar el pago fraccionado, deberá pagar la suma señalada en el numeral 4 anterior");
                        }
                        else
                        {
                            contenido = contenido.Replace("##FRACCIONADO##,", ""); ;
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
                                case "5":
                                    simboloMoneda = "CLP$";
                                    nombreMoneda = "Peso Chileno";
                                    totalPago = listaCuotasProgramaDTO.WebTotalPagar.ToString();
                                    tipoCambio = listaCuotasProgramaDTO.WebTotalPagar.Value;
                                    break;
                            }

                            contenido = contenido.Replace("##MONTROTOTALCRONOGRAMA##", simboloMoneda + " " + totalPago + "");

                            if (((tipoDocumento == "Contrato" || tipoDocumento == "Lineamiento" || tipoDocumento == "Convenio Prestacion") && (codpais == 52 || codpais == 56)) || tipoDocumento == "Condiciones")
                            {
                                if (tipoDocumento == "Convenio Prestacion" && codpais == 56)
                                {
                                    if (listaCuotasProgramaDTO.NumeroCuotas == 1)
                                    {
                                        formatoPagos = "EL ALUMNO, se obliga a cubrir el costo del PROGRAMA el cual asciende a un valor de " + simboloMoneda + " " + totalPago + " a ser cancelados en una sola cuota antes del ";
                                        foreach (var pagos in listaCuotasProgramaDTO.Cuotas)
                                        {
                                            fecha_pago = pagos.FechaVencimiento.Value;
                                        }
                                        formatoPagos += fecha_pago.ToString("dd/MM/yyyy") + ".";
                                    }
                                    else
                                    {

                                        var valormatricula = listaCuotasProgramaDTO.Cuotas.Where(w => w.NroCuota == 1).Select(w => w).FirstOrDefault();
                                        var valorcuotas = listaCuotasProgramaDTO.Cuotas.Where(w => w.NroCuota != 1).Select(w => w).FirstOrDefault();


                                        formatoPagos = "El Alumno debera pagar: i) el valor de la matricula de " + simboloMoneda + " " + valormatricula.MontoCuotaDescuento.ToString() + ", a mas tardar el dia " + valormatricula.FechaVencimiento.Value.ToString("dd/MM/yyyy") + " , y ii) el valor del arancel del PROGRAMA " + simboloMoneda + " " + (listaCuotasProgramaDTO.WebTotalPagar - valormatricula.MontoCuotaDescuento).ToString() + ", el cual se pagara en " + (listaCuotasProgramaDTO.Cuotas.Count - 1).ToString() + " cuotas mensuales iguales y sucesivas de " + simboloMoneda + " " + valorcuotas.MontoCuotaDescuento.ToString() + " cada una, a mas tardar el dia " + valorcuotas.FechaVencimiento.Value.Day.ToString() + " del mes respectivo siguiendo el siguiente cronograma de pago, excepto si optare por el pago anticipado<br><br> ";
                                        formatoPagos += "<strong>Nro. Cuota &nbsp;&nbsp; Monto &nbsp;&nbsp; Fecha de vencimiento</strong><br>";
                                        foreach (var pagos in listaCuotasProgramaDTO.Cuotas.Where(w => w.NroCuota != 1).ToList())
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
                                else
                                {
                                    formatoPagos += "<strong>Nro. Cuota &nbsp;&nbsp; Monto(" + simboloMoneda + " " + totalPago + ") &nbsp;&nbsp; Fecha de vencimiento</strong><br>";
                                    foreach (var pagos in listaCuotasProgramaDTO.Cuotas)
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
                            else
                            {
                                if (listaCuotasProgramaDTO.NumeroCuotas == 1)
                                {
                                    formatoPagos = "EL ALUMNO, se obliga a cubrir el costo del PROGRAMA el cual asciende a un valor de " + simboloMoneda + " " + totalPago + " a ser cancelados en una sola cuota antes del ";
                                    foreach (var pagos in listaCuotasProgramaDTO.Cuotas)
                                    {
                                        fecha_pago = pagos.FechaVencimiento.Value;
                                    }
                                    formatoPagos += fecha_pago.ToString("dd/MM/yyyy") + ".";
                                }
                                else
                                {
                                    formatoPagos = "EL ALUMNO, se obliga a cubrir el costo del PROGRAMA el cual asciende a un valor de " + simboloMoneda + " " + totalPago + ". Para el caso en concreto, el alumno ha escogido la modalidad de pago en varias cuotas, por lo que se compromete a pagar el siguiente esquema de cuotas:<br><br>";
                                    formatoPagos += "<strong>Nro. Cuota &nbsp;&nbsp; Monto(" + simboloMoneda + " " + totalPago + ") &nbsp;&nbsp; Fecha de vencimiento</strong><br>";
                                    foreach (var pagos in listaCuotasProgramaDTO.Cuotas)
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
                        string piePaginaAnexo3 = "";
                        if (idBusqueda != 0)
                        {
                            var datosPGeneral = servicioPGeneral.ObtenerPGeneralPorIdBusqueda(idBusqueda.Value);
                            programa = new PgeneralDocumentoSeccionDTO()
                            {
                                Id = datosPGeneral.Id,
                                Nombre = datosPGeneral.Nombre
                            };

                            var listaSecciones = ObtenerListaSeccionDocumentoProgramaGeneralVersion(programa.Id, alumno.Paquete);
                            try
                            {
                                if (listaSecciones.Where(w => w.Seccion == "Beneficios") != null)
                                {
                                    var beneficio = listaSecciones.Where(w => w.Seccion == "Beneficios").Select(w => w.DetalleSeccion).FirstOrDefault();
                                    if (beneficio != null)
                                    {
                                        piePaginaAnexo3 = beneficio[0].PiePagina;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                piePaginaAnexo3 = "";
                            }


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
                                    //anexo2 += "<br>(*) Incluido solo en la versión profesional.<br>(**) Incluido solo en la versión gerencial.";
                                }
                            }
                        }
                        else //Imprime estructura curricular de la version 1
                        {
                            if (idBusqueda != 0)
                            {
                                var datosPGeneral = servicioPGeneral.ObtenerPGeneralPorIdBusqueda(idBusqueda.Value);
                                programa = new PgeneralDocumentoSeccionDTO()
                                {
                                    Id = datosPGeneral.Id,
                                    Nombre = datosPGeneral.Nombre
                                };

                                List<SeccionDocumentoDTO> seccion = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSecciones(programa.Id);
                                if (programa.ListaSeccion == null)
                                {
                                    programa.ListaSeccion = new List<SeccionDocumentoDTO>();
                                }
                                foreach (var item in seccion)
                                {
                                    programa.ListaSeccion.Add(item);
                                }
                                //programa.ListaSecciones = _tcrm_competidoresRepository.GetSeccionesbyIdPrograma(id_programa).ToList();
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
                                    //anexo2 += "<br>(*) Incluido solo en la versión profesional.<br>(**) Incluido solo en la versión gerencial.";
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

                        if (matricula != null)
                        {
                            listaBeneficios = _unitOfWork.MatriculaCabeceraBeneficiosRepository
                                .ObtenerBeneficiosPorMatriculaCabecera(matricula.Id).Select(b => b.Valor).ToList();
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
                            anexo3 += piePaginaAnexo3;
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
                                        // TODO[] Usar ValorEstatico en lugar de valores HardCodeados
                                        //  IdProgramaGeneralFFISO27001 = 598
                                        //  IdProgramaGeneralFFISO9001 = 686
                                        //  IdProgramaGeneralDSIG = 703
                                        //  IdProgramaGeneralFFISO45001 = 810
                                        //  IdProgramaGeneralFFISO37001 = 7633

                                        if (programa.Id != 598 && programa.Id != 686 &&
                                            programa.Id != 703 && programa.Id != 810 &&
                                            programa.Id != 7633)
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
                                anexo3 += piePaginaAnexo3;
                            }
                        }
                        formatoPagos += "<br>";
                        contenido = contenido.Replace("##TIPOPAGO##", formatoPagos);
                        DateTime hoy = DateTime.Now;
                        contenido = contenido.Replace("## DATEMONTH ##", hoy.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")));
                        contenido = contenido.Replace("##DATEDAYS##", hoy.Day.ToString());
                        contenido = contenido.Replace("##DATEYEAR##", hoy.Year.ToString());
                        HTMLWorker htmlparser = new HTMLWorker(_document);
                        htmlparser.SetStyleSheet(GenerateStyleSheet());

                        PdfWriter.GetInstance(_document, ms);
                        _document.Open();

                        htmlparser.Parse(new StringReader(contenido));

                        //_document.Add(new iTextSharp.text.Paragraph("_______________           ____________                 __________________", firmas));
                        //_document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                   Huella Digital                    BS GRUPO S.A.C.", firmas));

                        if (codpais == 591)
                        {
                            _document.Add(new iTextSharp.text.Paragraph("_______________          ___________           ________________________", firmas));
                            _document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                  Huella Digital      BSGINSTITUTE BOLIVIA S.R.L.", firmas));
                        }
                        else if (codpais == 57)
                        {
                            if (tipoDocumento != "Condiciones")
                            {
                                _document.Add(new iTextSharp.text.Paragraph("_______________          ___________           ________________________", firmas));
                                _document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                  Huella Digital       BS GRUPO COLOMBIA S.A.S.", firmas));
                            }
                        }
                        else if (codpais == 52 || codpais == 56)
                        {
                            if (tipoDocumento == "Convenio Prestacion")
                            {
                                _document.Add(new iTextSharp.text.Paragraph("_______________          ___________           ________________________", firmas));
                                _document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                  Huella Digital      BSGINSTITUTE. ", firmas));
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
                                _document.Add(new iTextSharp.text.Paragraph("_______________           ____________                 __________________", firmas));
                                _document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                   Huella Digital                    BS GRUPO S.A.C.", firmas));
                            }
                        }

                        if (codpais == 52 || codpais == 56)
                        {
                            if (tipoDocumento == "Contrato")
                            {
                                //No se adjunto anexo
                            }
                            else if (tipoDocumento == "Lineamiento")
                            {
                                if (!string.IsNullOrEmpty(anexo1))
                                {
                                    _document.NewPage();
                                    htmlparser.Parse(new StringReader(anexo1));
                                }
                            }
                            else if (tipoDocumento == "Convenio Prestacion")
                            {
                                if (!string.IsNullOrEmpty(anexo1))
                                {
                                    _document.NewPage();
                                    htmlparser.Parse(new StringReader(anexo1));
                                }
                                if (!string.IsNullOrEmpty(anexo2))
                                {
                                    _document.NewPage();
                                    htmlparser.Parse(new StringReader(anexo2));
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(anexo1))
                            {
                                _document.NewPage();
                                htmlparser.Parse(new StringReader(anexo1));
                            }
                            if (!string.IsNullOrEmpty(anexo2))
                            {
                                _document.NewPage();
                                htmlparser.Parse(new StringReader(anexo2));
                            }
                            if (!string.IsNullOrEmpty(anexo3))
                            {
                                _document.NewPage();
                                htmlparser.Parse(new StringReader(anexo3));
                            }
                        }
                        _document.Close();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }

                return ms.ToArray();
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
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

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 14/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de beneficios por la configuracion de beneficios del programa general mediante idpgeneral, idpais e idpaquete
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <param name="idPais"> Id de Pais </param>
        /// <param name="idPaquete"> Id de Paquete </param>
        /// <returns> List<string> </returns>
        public async Task<List<string>> ObtenerBeneficiosConfiguradosProgramaGeneralAsync(int idPGeneral, int? idPais, int? idPaquete)
        {
            try
            {
                List<string> listaBeneficios = new List<string>();
                if (idPais.HasValue)
                {
                    //List<PgeneralConfiguracionBeneficioDTO> listaBeneficiosConfigurado = await _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerBeneficiosConfiguradosProgramaGeneralAsync(idPGeneral, idPais ?? 0, idPaquete ?? 0);
                    //listaBeneficiosConfigurado.Select(x => x.Descripcion).ToList();
                }
                return listaBeneficios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Metodo que retorna lista de secciones de documento del programa general apuntando a la nueva version por paquete.
        /// </summary>
        /// <param name="idPGeneral">Id del programa general del cual se desea obtener las secciones del documento (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="version">Version de matricula alumno</param>
        /// <returns> List<ProgramaGeneralSeccionDocumentoDTO> </returns>
        public List<ProgramaGeneralSeccionDocumentoDTO> ObtenerListaSeccionDocumentoProgramaGeneralVersion(int idPGeneral, string version)
        {
            try
            {
                ProgramaGeneralDoumentoDTO programa = new ProgramaGeneralDoumentoDTO();

                var listaCursos = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijosVersion(idPGeneral, version);
                if (listaCursos.Count > 0)
                {
                    programa.EsProgramaPadre = true;
                    var secciones = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumento(idPGeneral);
                    if (secciones != null && secciones.Count > 0)
                    {
                        programa.ListaSeccionesContenidosDocumento = secciones;
                    }
                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();

                    foreach (var item in listaCursos)
                    {
                        var seccionEstructura = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoEstructuraCurricularPorIdsPGeneral(idPGeneral, item.Id);

                        if (seccionEstructura != null && seccionEstructura.Count > 0)
                        {
                            programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                        }
                    }
                }
                else
                {
                    programa.EsProgramaPadre = false;

                    var secciones = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumento(idPGeneral);

                    if (secciones != null && secciones.Count > 0)
                    {
                        programa.ListaSeccionesContenidosDocumento = secciones;
                    }

                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();


                    var seccionEstructura = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoEstructuraCurricular(idPGeneral);

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
                        int contadorcursos = listaCursosSecciones.Count();
                        int contadorinterno = 0;
                        foreach (var itemCurso in listaCursosSecciones)
                        {
                            contadorinterno++;
                            var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12 && x.IdPGeneral == itemCurso.IdPGeneral).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.IdPGeneral, x.Cabecera, x.PiePagina }).ToList();
                            ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                            obj.Seccion = itemCurso.Titulo;
                            obj.Titulo = itemCurso.NombreCurso;
                            obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();

                            if (contadorinterno < contadorcursos)
                            {
                                foreach (var itemSecion in listaCapitulosSecciones)
                                {
                                    ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                    {
                                        Contenido = itemSecion.Key.Contenido,
                                        Cabecera = itemSecion.Key.Cabecera,
                                        PiePagina = ""
                                    };
                                    obj.DetalleContenido.Add(tmp);

                                }
                            }
                            else
                            {
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
                            }


                            contenido.Add(obj);
                        }
                    }
                    else
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.Titulo }).ToList();
                        int contadorcursos = listaCapitulosSecciones.Count();
                        int contadorinterno = 0;
                        foreach (var item in listaCapitulosSecciones)
                        {
                            contadorinterno++;
                            ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                            var listaSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.Contenido == item.Key.Contenido).Select(x => x.NumeroFila).ToList();
                            var capituloSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.IdSeccionTipoDetalle_PW == 13 && listaSesiones.Contains(s.NumeroFila)).GroupBy(x => new { x.Contenido, x.NombreCurso, x.Cabecera, x.PiePagina }).ToList();
                            obj.Seccion = item.Key.Titulo;
                            obj.Titulo = item.Key.Contenido;
                            obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();

                            if (contadorinterno < contadorcursos)
                            {
                                if (capituloSesiones != null && capituloSesiones.Count > 0)
                                {
                                    foreach (var itemSecion in capituloSesiones)
                                    {
                                        ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                        {
                                            Contenido = itemSecion.Key.Contenido,
                                            Cabecera = itemSecion.Key.Cabecera,
                                            PiePagina = ""
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
                            else
                            {
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
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
                var programa = new ProgramaGeneralDoumentoDTO();

                var listaCursos = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijos(idPGeneral);
                if (listaCursos.Count > 0)
                {
                    programa.EsProgramaPadre = true;
                    var secciones = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumento(idPGeneral);

                    if (secciones != null && secciones.Count > 0)
                        programa.ListaSeccionesContenidosDocumento = secciones;

                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();
                    foreach (var item in listaCursos)
                    {
                        var seccionEstructura = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoEstructuraCurricularPorIdsPGeneral(idPGeneral, item.Id);

                        if (seccionEstructura != null && seccionEstructura.Count > 0)
                            programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                    }
                }
                else
                {
                    programa.EsProgramaPadre = false;

                    var secciones = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumento(idPGeneral);

                    if (secciones != null && secciones.Count > 0)
                        programa.ListaSeccionesContenidosDocumento = secciones;

                    var seccionEstructura = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoEstructuraCurricular(idPGeneral);

                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();
                    if (seccionEstructura != null && seccionEstructura.Count() > 0)
                        programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                }

                var contenido = new List<ProgramaGeneralEstructuraAgrupadoDTO>();
                if (programa.ListaSeccionesContenidosDocumentoEstructura != null && programa.ListaSeccionesContenidosDocumentoEstructura.Count > 0)
                {
                    if (programa.EsProgramaPadre)
                    {
                        var listaCursosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.GroupBy(x => new { x.IdPGeneral, x.NombreCurso, x.Titulo }).Select(x => new { x.Key.IdPGeneral, x.Key.NombreCurso, x.Key.Titulo }).ToList();

                        int contadorcursos = listaCursosSecciones.Count();
                        int contadorinterno = 0;

                        foreach (var itemCurso in listaCursosSecciones)
                        {
                            contadorinterno++;
                            var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12 && x.IdPGeneral == itemCurso.IdPGeneral).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.IdPGeneral, x.Cabecera, x.PiePagina }).ToList();
                            var obj = new ProgramaGeneralEstructuraAgrupadoDTO()
                            {
                                Seccion = itemCurso.Titulo,
                                Titulo = itemCurso.NombreCurso,
                                DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>()
                            };

                            if (contadorinterno < contadorcursos)
                            {
                                foreach (var itemSecion in listaCapitulosSecciones)
                                {
                                    var tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                    {
                                        Contenido = itemSecion.Key.Contenido,
                                        Cabecera = itemSecion.Key.Cabecera,
                                        PiePagina = ""
                                    };
                                    obj.DetalleContenido.Add(tmp);

                                }
                            }
                            else
                            {
                                foreach (var itemSecion in listaCapitulosSecciones)
                                {
                                    var tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                    {
                                        Contenido = itemSecion.Key.Contenido,
                                        Cabecera = itemSecion.Key.Cabecera,
                                        PiePagina = itemSecion.Key.PiePagina
                                    };
                                    obj.DetalleContenido.Add(tmp);

                                }
                            }
                            contenido.Add(obj);
                        }
                    }
                    else
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.Titulo }).ToList();
                        int contadorcursos = listaCapitulosSecciones.Count();
                        int contadorinterno = 0;

                        foreach (var item in listaCapitulosSecciones)
                        {
                            contadorinterno++;
                            var listaSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.Contenido == item.Key.Contenido).Select(x => x.NumeroFila).ToList();
                            var capituloSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.IdSeccionTipoDetalle_PW == 13 && listaSesiones.Contains(s.NumeroFila)).GroupBy(x => new { x.Contenido, x.NombreCurso, x.Cabecera, x.PiePagina }).ToList();
                            var obj = new ProgramaGeneralEstructuraAgrupadoDTO()
                            {
                                Seccion = item.Key.Titulo,
                                Titulo = item.Key.Contenido,
                                DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>()
                            };

                            if (contadorinterno < contadorcursos)
                            {
                                if (capituloSesiones != null && capituloSesiones.Count > 0)
                                {
                                    foreach (var itemSecion in capituloSesiones)
                                    {
                                        var tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                        {
                                            Contenido = itemSecion.Key.Contenido,
                                            Cabecera = itemSecion.Key.Cabecera,
                                            PiePagina = ""
                                        };
                                        obj.DetalleContenido.Add(tmp);
                                    }
                                    contenido.Add(obj);
                                }
                                else
                                {
                                    foreach (var itemSecion in capituloSesiones)
                                    {
                                        var tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                        {
                                            Contenido = item.Key.Contenido
                                        };
                                        obj.DetalleContenido.Add(tmp);
                                    }
                                    contenido.Add(obj);
                                }
                            }
                            else
                            {
                                if (capituloSesiones != null && capituloSesiones.Count > 0)
                                {
                                    foreach (var itemSecion in capituloSesiones)
                                    {
                                        var tmp = new ProgramaGeneralEstructuraDetalleDTO()
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
                                        var tmp = new ProgramaGeneralEstructuraDetalleDTO()
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
                }
                if (programa.ListaSeccionesContenidosDocumento != null && programa.ListaSeccionesContenidosDocumento.Count > 0)
                {
                    var listaCursosSecciones = programa.ListaSeccionesContenidosDocumento.GroupBy(x => new { x.IdSeccionTipoDetalle_PW, x.Titulo }).Select(x => new { x.Key.IdSeccionTipoDetalle_PW, x.Key.Titulo }).ToList();
                    foreach (var itemCurso in listaCursosSecciones)
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumento.Where(x => x.IdSeccionTipoDetalle_PW == itemCurso.IdSeccionTipoDetalle_PW).GroupBy(x => new { x.Contenido, x.Cabecera, x.PiePagina }).ToList();
                        var obj = new ProgramaGeneralEstructuraAgrupadoDTO()
                        {
                            Seccion = itemCurso.Titulo,
                            Titulo = "",//itemCurso.Titulo;
                            DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>()
                        };
                        foreach (var itemSecion in listaCapitulosSecciones)
                        {
                            var tmp = new ProgramaGeneralEstructuraDetalleDTO()
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

                //var listaProgramaGeneralDocumentoSeccion = contenido.GroupBy(x => x.Seccion).Select(x => new ProgramaGeneralSeccionDocumentoDTO
                //{
                //    Seccion = x.Key,
                //    DetalleSeccion = x.GroupBy(y => new { y.Titulo, y.DetalleContenido }).Select(y => new ProgramaGeneralSeccionDocumentoDetalleDTO
                //    {
                //        Titulo = y.Key.Titulo,
                //        Cabecera = y.Key.DetalleContenido.GroupBy(z => z.Cabecera).Select(z => z.Key).FirstOrDefault(),
                //        PiePagina = y.Key.DetalleContenido.GroupBy(z => z.PiePagina).Select(z => z.Key).FirstOrDefault(),
                //        DetalleContenido = y.Key.DetalleContenido.Select(z => z.Contenido).ToList()
                //    }).ToList()
                //}).ToList();

                //return listaProgramaGeneralDocumentoSeccion;

                //Paso 1: Agrupación inicial por Sección
                var agrupadoPorSeccion = contenido.GroupBy(x => x.Seccion);

                // Paso 2: Proyección de cada sección en una lista de objetos ProgramaGeneralSeccionDocumentoDTO
                var listaProgramaGeneralDocumentoSeccion = agrupadoPorSeccion.Select(x =>
                {
                    // Almacena la clave de la sección actual
                    var seccionActual = x.Key;

                    // Paso 3: Agrupación interna por Título dentro de cada Sección
                    var agrupadoPorTitulo = x.GroupBy(y => y.Titulo);

                    // Paso 4: Proyección de cada grupo de Título en múltiples entradas de ProgramaGeneralSeccionDocumentoDetalleDTO
                    var detalleSeccion = agrupadoPorTitulo.SelectMany(y =>
                    {
                        // Almacena el título actual
                        var tituloActual = y.Key;

                        // Paso 5: Accede a DetalleContenido dentro de cada título
                        var detalleContenidoPorCabecera = y.SelectMany(z => z.DetalleContenido)
                            .GroupBy(detalle => detalle.Cabecera);

                        // Paso 6: Proyección de cada grupo de Cabecera y sus contenidos en un objeto DTO
                        return detalleContenidoPorCabecera.Select(grupoCabecera =>
                        {
                            // Cabecera actual en el grupo
                            var cabeceraActual = grupoCabecera.Key;

                            // Obtener todos los contenidos asociados a esta cabecera en una lista
                            var listaContenido = grupoCabecera.Select(z => z.Contenido).ToList();

                            // Selecciona el primer PiePagina en caso de que exista
                            var piePaginaSeleccionado = grupoCabecera.Select(z => z.PiePagina).FirstOrDefault();

                            // Crear y retornar un objeto ProgramaGeneralSeccionDocumentoDetalleDTO para cada cabecera y su lista de contenido
                            return new ProgramaGeneralSeccionDocumentoDetalleDTO
                            {
                                Titulo = tituloActual,
                                Cabecera = cabeceraActual,
                                PiePagina = piePaginaSeleccionado,
                                DetalleContenido = listaContenido
                            };
                        });
                    }).ToList();

                    // Paso 7: Crear y retornar el objeto ProgramaGeneralSeccionDocumentoDTO con la sección actual y los detalles procesados
                    return new ProgramaGeneralSeccionDocumentoDTO
                    {
                        Seccion = seccionActual,
                        DetalleSeccion = detalleSeccion
                    };
                }).ToList();

                // Resultado final
                return listaProgramaGeneralDocumentoSeccion;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Metodo que retorna lista de secciones de documento del programa general apuntando a la nueva version
        /// </summary>
        /// <param name="idPGeneral">Id del programa general del cual se desea obtener las secciones del documento (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> List<ProgramaGeneralSeccionDocumentoDTO> </returns>
        public async Task<List<ProgramaGeneralSeccionDocumentoDTO>> ObtenerListaSeccionDocumentoProgramaGeneralAsync(int idPGeneral)
        {
            try
            {
                var programa = new ProgramaGeneralDoumentoDTO();
                programa.ListaSeccionesContenidosDocumento = new();
                var listaCursos = await _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijosAsync(idPGeneral);
                if (listaCursos.Count > 0)
                {
                    programa.EsProgramaPadre = true;
                    List<RegistroListaSeccionesDocumentoDTO> secciones = await _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoAsync(idPGeneral);

                    if (secciones != null && secciones.Count > 0)
                        programa.ListaSeccionesContenidosDocumento = secciones;

                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();

                    List<RegistroListaSeccionesDocumentoDTO> listaSeccionEstructura = await _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoEstructuraCurriculaPorIdsPGeneralAsync(idPGeneral, listaCursos.Select(x => x.Id).ToList());
                    if (listaSeccionEstructura != null && listaSeccionEstructura.Count() > 0)
                        programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(listaSeccionEstructura);
                }
                else
                {
                    programa.EsProgramaPadre = false;
                    var secciones = await _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoAsync(idPGeneral);
                    if (secciones != null && secciones.Count > 0)
                        programa.ListaSeccionesContenidosDocumento = secciones;

                    var seccionEstructura = await _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoEstructuraCurricularAsync(idPGeneral);
                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();
                    if (seccionEstructura != null && seccionEstructura.Count() > 0)
                        programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                }

                var contenido = new List<ProgramaGeneralEstructuraAgrupadoDTO>();
                const int Capitulo = 12;
                const int Sesion = 13;
                if (programa.ListaSeccionesContenidosDocumentoEstructura != null && programa.ListaSeccionesContenidosDocumentoEstructura.Count() > 0)
                {
                    if (programa.EsProgramaPadre)
                    {
                        var listaCursosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.GroupBy(x => new { x.IdPGeneral, x.NombreCurso, x.Titulo }).Select(x => new { x.Key.IdPGeneral, x.Key.NombreCurso, x.Key.Titulo }).ToList();
                        int contadorcursos = listaCursosSecciones.Count();
                        int contadorinterno = 0;
                        foreach (var itemCurso in listaCursosSecciones)
                        {
                            contadorinterno++;
                            var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == Capitulo && x.IdPGeneral == itemCurso.IdPGeneral).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.IdPGeneral, x.Cabecera, x.PiePagina }).ToList();

                            var obj = new ProgramaGeneralEstructuraAgrupadoDTO()
                            {
                                Seccion = itemCurso.Titulo,
                                Titulo = itemCurso.NombreCurso,
                                DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>()
                            };
                            if (contadorinterno < contadorcursos)
                            {
                                if (listaCapitulosSecciones != null && listaCapitulosSecciones.Count() > 0)
                                {
                                    obj.DetalleContenido = listaCapitulosSecciones.Select(x => new ProgramaGeneralEstructuraDetalleDTO
                                    {
                                        Contenido = x.Key.Contenido,
                                        Cabecera = x.Key.Cabecera,
                                        PiePagina = ""
                                    }).ToList();
                                }
                            }
                            else
                            {
                                if (listaCapitulosSecciones != null && listaCapitulosSecciones.Count() > 0)
                                {
                                    obj.DetalleContenido = listaCapitulosSecciones.Select(x => new ProgramaGeneralEstructuraDetalleDTO
                                    {
                                        Contenido = x.Key.Contenido,
                                        Cabecera = x.Key.Cabecera,
                                        PiePagina = x.Key.PiePagina
                                    }).ToList();
                                }
                            }

                            contenido.Add(obj);
                        }
                    }
                    else
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == Capitulo).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.Titulo }).ToList();
                        int contadorcursos = listaCapitulosSecciones.Count();
                        int contadorinterno = 0;
                        foreach (var item in listaCapitulosSecciones)
                        {
                            contadorinterno++;
                            var listaSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.Contenido == item.Key.Contenido).Select(x => x.NumeroFila).ToList();
                            var capituloSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.IdSeccionTipoDetalle_PW == Sesion && listaSesiones.Contains(s.NumeroFila)).GroupBy(x => new { x.Contenido, x.NombreCurso, x.Cabecera, x.PiePagina }).ToList();
                            var obj = new ProgramaGeneralEstructuraAgrupadoDTO()
                            {
                                Seccion = item.Key.Titulo,
                                Titulo = item.Key.Contenido,
                                DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>()
                            };

                            if (contadorinterno < contadorcursos)
                            {
                                if (capituloSesiones != null && capituloSesiones.Count() > 0)
                                {


                                    obj.DetalleContenido = capituloSesiones.Select(x => new ProgramaGeneralEstructuraDetalleDTO
                                    {
                                        Contenido = x.Key.Contenido,
                                        Cabecera = x.Key.Cabecera,
                                        PiePagina = ""
                                    }).ToList();
                                }
                            }
                            else
                            {
                                if (capituloSesiones != null && capituloSesiones.Count() > 0)
                                {


                                    obj.DetalleContenido = capituloSesiones.Select(x => new ProgramaGeneralEstructuraDetalleDTO
                                    {
                                        Contenido = x.Key.Contenido,
                                        Cabecera = x.Key.Cabecera,
                                        PiePagina = x.Key.PiePagina
                                    }).ToList();
                                }
                            }

                            contenido.Add(obj);
                        }
                    }
                }
                if (programa.ListaSeccionesContenidosDocumento != null && programa.ListaSeccionesContenidosDocumento.Count > 0)
                {
                    var listaCursosSecciones = programa.ListaSeccionesContenidosDocumento.GroupBy(x => new { x.IdSeccionTipoDetalle_PW, x.Titulo }).Select(x => new { x.Key.IdSeccionTipoDetalle_PW, x.Key.Titulo }).ToList();
                    foreach (var itemCurso in listaCursosSecciones)
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumento.Where(x => x.IdSeccionTipoDetalle_PW == itemCurso.IdSeccionTipoDetalle_PW).GroupBy(x => new { x.Contenido, x.Cabecera, x.PiePagina }).ToList();
                        var obj = new ProgramaGeneralEstructuraAgrupadoDTO()
                        {
                            Seccion = itemCurso.Titulo,
                            Titulo = "",//itemCurso.Titulo;
                            DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>()
                        };
                        if (listaCapitulosSecciones != null && listaCapitulosSecciones.Count() > 0)
                        {
                            obj.DetalleContenido = listaCapitulosSecciones.Select(x => new ProgramaGeneralEstructuraDetalleDTO
                            {
                                Contenido = x.Key.Contenido,
                                Cabecera = x.Key.Cabecera,
                                PiePagina = x.Key.PiePagina
                            }).ToList();
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
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Convierte nombre de archivo a minusculas y elimina "-".
        /// </summary>
        /// <param name="nombreArchivo">Nombre del Archivo</param>
        /// <returns> string </returns>
        private string ToURLSlug(string nombreArchivo)
        {
            return Regex.Replace(nombreArchivo, @"[^a-z0-9]+", "-", RegexOptions.IgnoreCase).Trim(new char[] { '-' }).ToLower();
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Nombre del Documento Silabo
        /// </summary>
        /// <param name="pgeneral">Datos de Programa General</param>
        /// <returns> string </returns>
        private string GeneraSilabo(PGeneralAtributosPrincipalesDTO pgeneral)
        {
            string archivo = string.Empty;
            try
            {
                List<PgeneralHijoDTO> listaCursos = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijos(pgeneral.Id);
                PgeneralDocumentoSeccionDTO registroPrograma = _unitOfWork.PGeneralRepository.ObtenerPgeneralDocumentoPorId(pgeneral.Id);
                if (listaCursos != null && listaCursos.Count() > 0)
                {
                    foreach (var programa in listaCursos)
                    {
                        List<SeccionDocumentoDTO> contenido = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDocumentoSeccion(pgeneral.Id);
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
                    if (registroPrograma != null)
                    {
                        registroPrograma.ListaSeccion = new List<SeccionDocumentoDTO>();
                        List<SeccionDocumentoDTO> contenido = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDocumentoSeccion(pgeneral.Id);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            registroPrograma.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                registroPrograma.ListaSeccion.Add(variable);
                            }
                        }
                        archivo = ValidarDocumentoSilabo(registroPrograma);
                    }
                }
                return archivo;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Nombre del Documento Silabo por Perfil
        /// </summary>
        /// <param name="listaCursos">lista de programa general</param>
        /// <param name="pgeneral"> campos de un programa general</param>
        /// <returns> string </returns>
        public string ValidarDocumentoPerfil(List<PgeneralHijoDTO> listaCursos, PgeneralDocumentoSeccionDTO pgeneral)
        {
            string nombreDocumentoTemp = string.Empty;
            string nombresDoc = string.Empty;
            bool bandera = true;
            try
            {
                nombresDoc = GenerateSlug("Silabo_" + pgeneral.Nombre);
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Nombre del Documento Silabo
        /// </summary>
        /// <param name="registroPrograma">Campos de programa general</param>
        /// <returns> string </returns>
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// ELimina Tildes
        /// </summary>
        /// <param name="s">Cadena de Caracteres</param>
        /// <returns> string </returns>
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Un Archivo En byte de acuerdo a programa
        /// </summary>
        /// <param name="pGeneral">Campos del programa General </param>
        /// <returns> byte[] </returns>
        private byte[] generar_silabo_bytes(PGeneralAtributosPrincipalesDTO pGeneral)
        {
            byte[] archivo;

            try
            {
                var servicioPGeneral = new PGeneralService(_unitOfWork);

                List<PgeneralHijoDTO> listaCursos = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijos(pGeneral.Id);

                if (listaCursos != null && listaCursos.Count() > 0)//Si es mayor es un perfil
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = servicioPGeneral.ObtenerPgeneralDocumentoPorId(pGeneral.Id);

                    foreach (var programa in listaCursos)
                    {
                        List<SeccionDocumentoDTO> contenido = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDocumentoSeccion(programa.IdPgeneral.Value);
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
                    PgeneralDocumentoSeccionDTO registroPrograma = servicioPGeneral.ObtenerPgeneralDocumentoPorId(pGeneral.Id);

                    if (registroPrograma != null)
                    {
                        registroPrograma.ListaSeccion = new List<SeccionDocumentoDTO>();
                        List<SeccionDocumentoDTO> contenido = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDocumentoSeccion(pGeneral.Id);
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera los bytes de un Documento Perfil
        /// </summary>
        /// <param name="modelo">Programa General Hijo</param>
        /// <param name="programa">Documento Seccion</param>
        /// <returns> byte[] </returns>
        private byte[] GenerarDocumentoPerfilBytes(List<PgeneralHijoDTO> modelo, PgeneralDocumentoSeccionDTO programa)
        {
            string nombreDocumento = string.Empty, nombreDocumentoTemp = string.Empty;
            string subcabecera = string.Empty;
            string nombresDoc = string.Empty;
            string direccionSilabo = @"~/repositorioweb/documentos/silabos/";

            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (Document _document = new Document(iTextSharp.text.PageSize.A4, 70f, 50f, 80f, 70f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                    pdfWriter.PageEvent = new ITextEventsDocumentos();
                    _document.AddTitle("Silabo-BSGInstitute");

                    nombresDoc = GenerateSlug("Silabo_" + programa.Nombre);
                    nombreDocumento = direccionSilabo + nombresDoc + ".pdf";
                    nombreDocumentoTemp = nombresDoc + ".pdf";

                    HTMLWorker htmlparser = new HTMLWorker(_document);
                    htmlparser.SetStyleSheet(GenerateStyleSheet3());

                    try
                    {
                        _document.Open();
                        string contenido = string.Empty;
                        contenido += "<h1>" + programa.Nombre + "</h1>";
                        contenido += "<p style='padding-bottom: 15px;border-bottom-style: solid;border-bottom-width: 1px;'><strong>Duracion: </strong>" + programa.pw_duracion + "</p>";

                        htmlparser.Parse(new StringReader(contenido));
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
                                            string _centenido = registro.Contenido.Replace("<table>", "<br/><table>");
                                            _centenido = _centenido.Replace("</table>", "</table><br/>");
                                            _centenido = _centenido.Replace("<li> ", "<li>").Replace(" <li>", "<li>");
                                            _centenido = _centenido.Replace("<li>", "<li> - ");

                                            htmlparser.Parse(new StringReader("<h1>" + registro.Titulo + "</h1>"));
                                            htmlparser.Parse(new StringReader(_centenido));
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
                            _document.NewPage();
                        }
                        _document.Close();
                    }
                    catch (Exception ex)
                    {
                        _document.Close();
                    }
                }
                return ms.ToArray();
            }

        }
        /// <summary> 
        /// Autor: Flavio R.M.F.
        /// Fecha: 18/11/2022
        /// Version: 1.0 
        /// Descripcion: Genera silabo por estrucutra curricular
        /// </summary> 
        private string GenerarEstructuraCurricular(List<SeccionDocumentoDTO> agrupadoestructura)
        {
            var agrupadocabecera = agrupadoestructura.GroupBy(w => w.NumeroFila).Select(x => x).ToList();
            var contenidofinal = "<h1>Estructura Curricular:</h1><br/>";
            var listacabecera = new List<string>();
            var listacapitulos = new List<string>();

            foreach (var registro in agrupadocabecera.OrderBy(w => w.Key).ToList())
            {
                var cabecera = registro.Where(w => w.NombreTitulo == "Capitulo").FirstOrDefault().Contenido;
                if (listacabecera.Any(x => x == cabecera))
                {
                    if (listacapitulos.Any(x => x == registro.Where(w => w.NombreTitulo == "Sesion").FirstOrDefault().Contenido))//si ya tiene ese capitulo
                    {
                        continue;
                    }
                    else
                    {
                        contenidofinal = contenidofinal + "-" + registro.Where(w => w.NombreTitulo == "Sesion").FirstOrDefault().Contenido + "<br/>";
                        listacapitulos.Add(registro.Where(w => w.NombreTitulo == "Sesion").FirstOrDefault().Contenido);
                    }
                }
                else
                {
                    listacabecera.Add(cabecera);
                    contenidofinal = contenidofinal + "<h1>" + cabecera + "</h1>";
                }
            }
            return contenidofinal;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera los bytes de un Documento Perfil
        /// </summary>
        /// <param name="modelo">Documento Seccion</param>
        /// <returns> byte[] </returns>
        private byte[] GenerarDocumentoSilaboBytes(PgeneralDocumentoSeccionDTO modelo)
        {

            string nombreDocumento = string.Empty, nombreDocumentoTemp = string.Empty;
            string subcabecera = string.Empty;
            string nombresDoc = string.Empty;

            string direccionSilabo = @"~/repositorioweb/documentos/silabos/";
            string contenido = String.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (Document _document = new Document(iTextSharp.text.PageSize.A4, 70f, 50f, 80f, 70f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                    pdfWriter.PageEvent = new TextEventsDocumentosService(modelo.Nombre);
                    _document.AddTitle("Silabo-BSGInstitute");

                    nombresDoc = GenerateSlug("Silabo_" + modelo.Nombre);
                    nombreDocumento = direccionSilabo + nombresDoc + ".pdf";
                    nombreDocumentoTemp = nombresDoc + ".pdf";

                    HTMLWorker htmlparser = new HTMLWorker(_document);
                    htmlparser.SetStyleSheet(GenerateStyleSheet3());

                    try
                    {
                        _document.Open();
                        contenido = string.Empty;
                        contenido += "<h1>" + modelo.Nombre + "</h1>";
                        contenido += "<p style='padding-bottom: 15px;border-bottom-style: solid;border-bottom-width: 1px;'><strong>Duracion: </strong>" + modelo.pw_duracion + "</p>";

                        htmlparser.Parse(new StringReader(contenido));

                        foreach (var registro in modelo.ListaSeccion)
                        {
                            if (!(string.IsNullOrEmpty(registro.Contenido)))
                            {
                                string _centenido = registro.Contenido.Replace("<table>", "<br/><table>");
                                _centenido = _centenido.Replace("</table>", "</table><br/>");
                                _centenido = _centenido.Replace("<li> ", "<li>").Replace(" <li>", "<li>");
                                _centenido = _centenido.Replace("<li>", "<li> - ");

                                htmlparser.Parse(new StringReader("<h1>" + registro.Titulo + "</h1>"));
                                htmlparser.Parse(new StringReader(_centenido));
                                htmlparser.Parse(new StringReader("<br/>"));
                            }
                        }
                        _document.Close();
                    }
                    catch (Exception ex)
                    {
                        _document.Close();
                    }
                }
                return ms.ToArray();
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Documentos de Contrato de uso de datos/lineamientos de condiciones/convenio de prestacion Mexico
        /// </summary>
        /// <param name="Oportunidad"> Parametros de Oportunidad</param>
        /// <param name="TipoDocumento"> Convenio o Condiciones</param>
        /// <param name="idDocumento"> Id del documento a generar</param>
        /// <returns> byte[] </returns>
        private byte[] GenerarContratoUsoDatos(OportunidadCompuestoDTO oportunidad, string TipoDocumento, int idDocumento)
        {
            string Modalidad = string.Empty;
            string NombrePrograma = string.Empty;
            byte[] Contrato = new byte[1000];
            string _queryAlumno = string.Empty;
            string _queryMontoPagoCronograma = string.Empty;
            string _queryMontoPago = string.Empty;
            int TipoPago = 0, Pais = 0, IdProgramaEspecifico = 0;

            var servicioAlumno = new AlumnoService(_unitOfWork);

            var alumnoUbicacion = servicioAlumno.ObtenerCiudadPaisPorIdAlumno(oportunidad.IdAlumno!.Value);

            if (alumnoUbicacion.IdCiudad == null)
            {
                throw new Exception("El Alumno no tiene Ciudad.");
            }
            if (alumnoUbicacion.IdCodigoPais == null)
            {
                throw new Exception("El Alumno no tiene Pais.");
            }

            var datosAlumno = servicioAlumno.ObtenerDatosDocumentoPorIdAlumno(oportunidad.IdAlumno.Value);

            var montoPago = _unitOfWork.MontoPagoCronogramaRepository.ObtenerConogramaPagoDocumentoPorIdOportunidad(oportunidad.Id);

            if (montoPago.IdMontoPago == null)
            {
                montoPago = _unitOfWork.MontoPagoCronogramaRepository.ObtenerConogramaPagoDocumentoPorIdOportunidadOperaciones(oportunidad.Id);
            }


            if (montoPago.IdMontoPago != null)
            {
                //Agregado para Etiquetas
                var servicioMoneda = new MonedaService(_unitOfWork);
                var moneda = servicioMoneda.ObtenerMonedaParaDocumento(montoPago.IdMoneda!.Value);

                oportunidad.CostoTotalConDescuento = moneda.Simbolo + " " + montoPago.PrecioDescuento + " " + moneda.NombrePlural;

                var servicioMontoPago = new MontoPagoService(_unitOfWork);
                string paquete = _unitOfWork.MontoPagoRepository.ObtenerPaquetePorIdMontoPago(montoPago.IdMontoPago!.Value).Valor;

                if (paquete != null)
                {
                    datosAlumno.Paquete = paquete == null ? "" : paquete;
                }
                else
                {
                    datosAlumno.Paquete = "";
                }
            }
            else
            {
                datosAlumno.Paquete = "";
            }
            try
            {
                ProgramaCuotasDetalleDTO? ListaCuotasProgramaDTO = ObtenerCuotasPrograma(oportunidad.Id);
                if (ListaCuotasProgramaDTO == null)
                {
                    return null;
                }
                Modalidad = ListaCuotasProgramaDTO.TipoPrograma;
                NombrePrograma = ListaCuotasProgramaDTO.NombreCurso;
                IdProgramaEspecifico = ListaCuotasProgramaDTO.IdPespecifico;

                if (datosAlumno != null)
                {
                    if (datosAlumno.IdCodigoPais == 57)
                    {
                        Pais = 57;
                    }
                    else if (datosAlumno.IdCodigoPais == 591)
                    {
                        Pais = 591;
                    }
                    else if (datosAlumno.IdCodigoPais == 52)
                    {
                        Pais = 52;
                    }
                    else if (datosAlumno.IdCodigoPais == 56)
                    {
                        Pais = 56;
                    }
                    else
                    {
                        Pais = 51;
                    }
                }
                else
                {
                    Pais = 51;
                }
                datosAlumno!.IdOportunidad = oportunidad.Id;

                switch (Modalidad)
                {
                    case "Presencial":
                        Contrato = GenerarPDFConvenioCondiciones(datosAlumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Presencial");
                        break;
                    case "Online Asincronica":
                        Contrato = GenerarPDFConvenioCondiciones(datosAlumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");
                        break;
                    case "Online Sincronica":
                        Contrato = GenerarPDFConvenioCondiciones(datosAlumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");
                        break;
                }
                return Contrato;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Version asociada a un Id
        /// </summary>
        /// <param name="Id">Id de la Version</param>
        /// <returns> string </returns>
        public string GenerarVersion(string Id)
        {
            switch (Id)
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera HTML a partir de la lista de secciones de documento del programa general
        /// </summary>
        /// <param name="listaProgramaGeneralDocumentoSeccion">Lista de ProgramaGeneralSeccionDocumentoDTO</param>	
        /// <returns> List<ProgramaGeneralSeccionAnexosHTMLDTO> </returns>
        public List<ProgramaGeneralSeccionAnexosHTMLDTO> GenerarHTMLProgramaGeneralDocumentoSeccion(List<ProgramaGeneralSeccionDocumentoDTO> listaProgramaGeneralDocumentoSeccion)
        {
            try
            {
                var lista = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
                if (listaProgramaGeneralDocumentoSeccion != null && listaProgramaGeneralDocumentoSeccion.Count() > 0)
                {
                    foreach (var item in listaProgramaGeneralDocumentoSeccion)
                    {
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
                        lista.Add(new ProgramaGeneralSeccionAnexosHTMLDTO()
                        {
                            Seccion = item.Seccion,
                            Contenido = contenido
                        });
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Metodo que retorna lista de secciones de documento del programa general apuntando a la nueva version
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>	
        /// <returns> List<ProgramaGeneralSeccionDocumentoDTO> </returns>
        public List<ProgramaGeneralSeccionDocumentoDTO> ObtenerInformacionProgramaGeneral(int idPGeneral)
        {
            try
            {
                var listaPrimera = ObtenerListaSeccionDocumentoProgramaGeneral(idPGeneral);
                var seccionesv2 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral);
                var seccionesv1 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV1(idPGeneral);
                ProgramaGeneralDoumentoDTO programa = new ProgramaGeneralDoumentoDTO();
                ProgramaGeneralDoumentoDTO programav2 = new ProgramaGeneralDoumentoDTO();

                List<RegistroListaSeccionesDocumentoDTO> seccionResultado = new List<RegistroListaSeccionesDocumentoDTO>();
                string[] listaTituloV1 = { "estructura curricular", "beneficios", "pre-requisitos", "certificación", "duración y horarios", "evaluación", "bibliografía", "material del curso", "pautas complementarias", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentación", "público objetivo", "metodología online de este programa", "modalidad", "inversion", "perfil del egresado", "mercado laboral", "expositores", "metodologia del programa" };
                string[] listaTituloV2 = { "estructura curricular", "beneficios", "prerrequisitos", "certificacion", "duracion y horarios", "evaluacion", "bibliografia", "material del curso", "pautas complementarias", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentacion", "publico objetivo", "metodolog&#237;a online de este programa", "Modalidad", "Inversion", "Perfil del egresado", "Mercado laboral", "Expositores", "Metodolog&#237;a del programa" };
                for (var i = 0; i < listaTituloV2.Length; i++)
                {
                    var secEstructuraCurricularV2 = seccionesv2.Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(listaTituloV2[i])).FirstOrDefault();
                    if (secEstructuraCurricularV2 != null)
                    {
                        if (secEstructuraCurricularV2.Contenido == null || secEstructuraCurricularV2.Contenido == "" || secEstructuraCurricularV2.Contenido == "<vacio></vacio>")
                        {
                            var secEstructuraCurricularV1 = seccionesv1.Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(listaTituloV1[i])).FirstOrDefault();
                            if (secEstructuraCurricularV1 != null)
                            {
                                if (secEstructuraCurricularV1.Contenido != null && secEstructuraCurricularV1.Contenido != "" && secEstructuraCurricularV1.Contenido != "<vacio></vacio>")
                                {
                                    seccionResultado.Add(secEstructuraCurricularV1);
                                }
                            }
                        }
                        else
                        {
                            seccionResultado.Add(secEstructuraCurricularV2);
                        }
                    }
                    else
                    {
                        var secEstructuraCurricularV1 = seccionesv1.Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(listaTituloV1[i])).FirstOrDefault();
                        if (secEstructuraCurricularV1 != null)
                        {
                            if (secEstructuraCurricularV1.Contenido != null && secEstructuraCurricularV1.Contenido != "" && secEstructuraCurricularV1.Contenido != "<vacio></vacio>")
                            {
                                seccionResultado.Add(secEstructuraCurricularV1);
                            }
                        }
                    }
                }

                if (listaPrimera != null && listaPrimera.Count > 0)
                {
                    foreach (var item in listaPrimera)
                    {
                        if (item.Seccion != null)
                        {
                            var temp = seccionResultado.Where(x => x.Titulo.ToLower().Equals(item.Seccion.ToLower())).FirstOrDefault();
                            if (temp != null) { seccionResultado.Remove(temp); }
                            if (LimpiarCadena(item.Seccion).ToLower() == "certificacion")
                            {
                                var temp2 = seccionResultado.Where(x => LimpiarCadena(x.Titulo).ToLower().Equals("certificacion")).FirstOrDefault();
                                if (temp2 != null) { seccionResultado.Remove(temp2); }
                            }
                            if (LimpiarCadena(item.Seccion).ToLower() == "prerrequisitos")
                            {
                                var temp3 = seccionResultado.Where(x => LimpiarCadena(x.Titulo).ToLower().Equals("pre-requisitos")).FirstOrDefault();
                                if (temp3 != null) { seccionResultado.Remove(temp3); }
                            }
                        }
                    }
                }

                programav2.ListaSeccionesContenidosDocumento = seccionResultado;

                List<ProgramaGeneralEstructuraAgrupadoDTO> Contenido = new List<ProgramaGeneralEstructuraAgrupadoDTO>();

                var _expositores = _unitOfWork.DocumentoSeccionPwRepository.ObtenerExpositoresPorIdGeneral(idPGeneral);
                foreach (var item in _expositores)
                {
                    ProgramaGeneralEstructuraAgrupadoDTO temp = new ProgramaGeneralEstructuraAgrupadoDTO();

                    var nombreExpositor = item.PrimerNombre + " " + item.SegundoNombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno + " - " + item.NombrePais;

                    temp.Seccion = "Expositores";
                    temp.Titulo = nombreExpositor;
                    temp.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();

                    ProgramaGeneralEstructuraDetalleDTO obj = new ProgramaGeneralEstructuraDetalleDTO()
                    {
                        Contenido = item.HojaVidaResumidaPerfil,
                        Cabecera = null,
                        PiePagina = null,
                    };
                    temp.DetalleContenido.Add(obj);

                    Contenido.Add(temp);
                }

                if (programav2.ListaSeccionesContenidosDocumento != null && programav2.ListaSeccionesContenidosDocumento.Count > 0)
                {
                    var _listaCursos2 = programav2.ListaSeccionesContenidosDocumento.GroupBy(x => new { x.Contenido, x.Titulo }).Select(x => new { x.Key.Contenido, x.Key.Titulo }).ToList();
                    foreach (var itemCurso in _listaCursos2)
                    {
                        var _listaCapitulos = programav2.ListaSeccionesContenidosDocumento.Where(x => x.Contenido == itemCurso.Contenido).GroupBy(x => new { x.Contenido, x.Cabecera, x.PiePagina }).ToList();
                        ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                        obj.Seccion = itemCurso.Titulo;
                        obj.Titulo = itemCurso.Titulo;
                        obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();
                        foreach (var itemSecion in _listaCapitulos)
                        {
                            ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                            {
                                Contenido = itemSecion.Key.Contenido,
                                Cabecera = itemSecion.Key.Cabecera,
                                PiePagina = itemSecion.Key.PiePagina
                            };
                            obj.DetalleContenido.Add(tmp);

                        }
                        Contenido.Add(obj);
                    }
                }

                var listaProgramaGeneralDocumentoSeccion = Contenido.GroupBy(x => x.Seccion).Select(x => new ProgramaGeneralSeccionDocumentoDTO
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
                listaProgramaGeneralDocumentoSeccion.AddRange(listaPrimera);
                return listaProgramaGeneralDocumentoSeccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ProgramaGeneralSeccionDocumentoV2DTO> ObtenerInformacionProgramaGeneralV2(int idPGeneral)
        {
            try
            {
                var listaPrimera = ObtenerListaSeccionDocumentoProgramaGeneral(idPGeneral);
                var seccionesv2old = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral);
                var seccionesv2 = seccionesv2old.Select(x => new RegistroListaSeccionesDocumentoV2DTO
                {
                    IdPGeneral = x.IdPGeneral,
                    Titulo = x.Titulo,
                    Contenido = HtmlToJsonHelper.ConvertHtmlToJson(x.Contenido),
                    IdSeccionTipoDetalle_PW = x.IdSeccionTipoDetalle_PW,
                    NumeroFila = x.NumeroFila,
                    Cabecera = x.Cabecera,
                    PiePagina = x.PiePagina,
                    OrdenWeb = x.OrdenWeb,
                    NombreCurso = x.NombreCurso
                }).ToList();
                var seccionesv1old = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV1(idPGeneral);
                var seccionesv1 = seccionesv1old.Select(x => new RegistroListaSeccionesDocumentoV2DTO
                {
                    IdPGeneral = x.IdPGeneral,
                    Titulo = x.Titulo,
                    Contenido = HtmlToJsonHelper.ConvertHtmlToJson(x.Contenido),
                    IdSeccionTipoDetalle_PW = x.IdSeccionTipoDetalle_PW,
                    NumeroFila = x.NumeroFila,
                    Cabecera = x.Cabecera,
                    PiePagina = x.PiePagina,
                    OrdenWeb = x.OrdenWeb,
                    NombreCurso = x.NombreCurso
                }).ToList();
                ProgramaGeneralDocumentoV2DTO programa = new ProgramaGeneralDocumentoV2DTO();
                ProgramaGeneralDocumentoV2DTO programav2 = new ProgramaGeneralDocumentoV2DTO();

                List<RegistroListaSeccionesDocumentoV2DTO> seccionResultado = new List<RegistroListaSeccionesDocumentoV2DTO>();
                string[] listaTituloV1 = { "estructura curricular", "beneficios", "pre-requisitos", "certificación", "duración y horarios", "evaluación", "bibliografía", "material del curso", "pautas complementarias", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentación", "público objetivo", "metodología online de este programa", "modalidad", "inversion", "perfil del egresado", "mercado laboral", "expositores", "metodologia del programa" };
                string[] listaTituloV2 = { "estructura curricular", "beneficios", "prerrequisitos", "certificacion", "duracion y horarios", "evaluacion", "bibliografia", "material del curso", "pautas complementarias", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentacion", "publico objetivo", "metodolog&#237;a online de este programa", "Modalidad", "Inversion", "Perfil del egresado", "Mercado laboral", "Expositores", "Metodolog&#237;a del programa" };
                for (var i = 0; i < listaTituloV2.Length; i++)
                {
                    var secEstructuraCurricularV2 = seccionesv2.Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(listaTituloV2[i])).FirstOrDefault();
                    if (secEstructuraCurricularV2 != null)
                    {
                        if (secEstructuraCurricularV2.Contenido == null || (secEstructuraCurricularV2.Contenido is List<object> list && !list.Any()))
                        {
                            var secEstructuraCurricularV1 = seccionesv1.Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(listaTituloV1[i])).FirstOrDefault();
                            if (secEstructuraCurricularV1 != null)
                            {
                                if (secEstructuraCurricularV1.Contenido != null && secEstructuraCurricularV1.Contenido.Any())
                                {
                                    seccionResultado.Add(secEstructuraCurricularV1);
                                }
                            }
                        }
                        else
                        {
                            seccionResultado.Add(secEstructuraCurricularV2);
                        }
                    }
                    else
                    {
                        var secEstructuraCurricularV1 = seccionesv1.Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(listaTituloV1[i])).FirstOrDefault();
                        if (secEstructuraCurricularV1 != null)
                        {
                            if (secEstructuraCurricularV1.Contenido != null && secEstructuraCurricularV1.Contenido.Any())
                            {
                                seccionResultado.Add(secEstructuraCurricularV1);
                            }
                        }
                    }
                }

                if (listaPrimera != null && listaPrimera.Count > 0)
                {
                    foreach (var item in listaPrimera)
                    {
                        if (item.Seccion != null)
                        {
                            var temp = seccionResultado.Where(x => x.Titulo.ToLower().Equals(item.Seccion.ToLower())).FirstOrDefault();
                            if (temp != null) { seccionResultado.Remove(temp); }
                            if (LimpiarCadena(item.Seccion).ToLower() == "certificacion")
                            {
                                var temp2 = seccionResultado.Where(x => LimpiarCadena(x.Titulo).ToLower().Equals("certificacion")).FirstOrDefault();
                                if (temp2 != null) { seccionResultado.Remove(temp2); }
                            }
                            if (LimpiarCadena(item.Seccion).ToLower() == "prerrequisitos")
                            {
                                var temp3 = seccionResultado.Where(x => LimpiarCadena(x.Titulo).ToLower().Equals("pre-requisitos")).FirstOrDefault();
                                if (temp3 != null) { seccionResultado.Remove(temp3); }
                            }
                        }
                    }
                }

                programav2.ListaSeccionesContenidosDocumento = seccionResultado;

                List<ProgramaGeneralEstructuraAgrupadoV2DTO> Contenido = new List<ProgramaGeneralEstructuraAgrupadoV2DTO>();

                var _expositores = _unitOfWork.DocumentoSeccionPwRepository.ObtenerExpositoresPorIdGeneral(idPGeneral);
                foreach (var item in _expositores)
                {
                    ProgramaGeneralEstructuraAgrupadoV2DTO temp = new ProgramaGeneralEstructuraAgrupadoV2DTO();

                    var nombreExpositor = item.PrimerNombre + " " + item.SegundoNombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno + " - " + item.NombrePais;

                    temp.Seccion = "Expositores";
                    temp.Titulo = nombreExpositor;
                    temp.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleV2DTO>();

                    ProgramaGeneralEstructuraDetalleV2DTO obj = new ProgramaGeneralEstructuraDetalleV2DTO()
                    {
                        Contenido = item.HojaVidaResumidaLimpia,
                        Cabecera = null,
                        PiePagina = null,
                    };

                    temp.DetalleContenido.Add(obj);

                    Contenido.Add(temp);
                }
                
                if (programav2.ListaSeccionesContenidosDocumento != null && programav2.ListaSeccionesContenidosDocumento.Count > 0)
                {
                    var _listaCursos2 = programav2.ListaSeccionesContenidosDocumento.GroupBy(x => new { x.Contenido, x.Titulo }).Select(x => new { x.Key.Contenido, x.Key.Titulo }).ToList();
                    foreach (var itemCurso in _listaCursos2)
                    {
                        var _listaCapitulos = programav2.ListaSeccionesContenidosDocumento.Where(x => x.Contenido == itemCurso.Contenido).GroupBy(x => new { x.Contenido, x.Cabecera, x.PiePagina }).ToList();
                        ProgramaGeneralEstructuraAgrupadoV2DTO obj = new ProgramaGeneralEstructuraAgrupadoV2DTO();
                        obj.Seccion = itemCurso.Titulo;
                        obj.Titulo = itemCurso.Titulo;
                        obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleV2DTO>();
                        foreach (var itemSecion in _listaCapitulos)
                        {
                            ProgramaGeneralEstructuraDetalleV2DTO tmp = new ProgramaGeneralEstructuraDetalleV2DTO()
                            {
                                Contenido = itemSecion.Key.Contenido,
                                Cabecera = itemSecion.Key.Cabecera,
                                PiePagina = itemSecion.Key.PiePagina
                            };
                            obj.DetalleContenido.Add(tmp);

                        }
                        Contenido.Add(obj);
                    }
                }

                var listaProgramaGeneralDocumentoSeccion = Contenido.GroupBy(x => x.Seccion).Select(x => new ProgramaGeneralSeccionDocumentoV2DTO
                {
                    Seccion = x.Key,
                    DetalleSeccion = x.GroupBy(y => new { y.Titulo, y.DetalleContenido }).Select(y => new ProgramaGeneralSeccionDocumentoDetalleV2DTO
                    {
                        Titulo = y.Key.Titulo,
                        Cabecera = y.Key.DetalleContenido.GroupBy(z => z.Cabecera).Select(z => z.Key).FirstOrDefault(),
                        PiePagina = y.Key.DetalleContenido.GroupBy(z => z.PiePagina).Select(z => z.Key).FirstOrDefault(),
                        DetalleContenido = y.Key.DetalleContenido.Select(z => z.Contenido).ToList()
                    }).ToList()
                }).ToList();
                var listaPrimera2 = listaPrimera.Select(x => new ProgramaGeneralSeccionDocumentoV2DTO
                {
                    Seccion = x.Seccion,
                    DetalleSeccion = x.DetalleSeccion.Select(d => new ProgramaGeneralSeccionDocumentoDetalleV2DTO
                    {
                        Titulo = d.Titulo,
                        Cabecera = d.Cabecera,
                        PiePagina = d.PiePagina,
                        DetalleContenido = d.DetalleContenido
                    }).ToList()
                }).ToList();
                listaProgramaGeneralDocumentoSeccion.AddRange(listaPrimera2);
                return listaProgramaGeneralDocumentoSeccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ProgramaGeneralSeccionDocumentoDTO> ObtenerInformacionProgramaGeneralJson(int idPGeneral)
        {
            try
            {
                var listaPrimera = ObtenerListaSeccionDocumentoProgramaGeneral(idPGeneral);
                var seccionesv2 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral);
                var seccionesv1 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV1(idPGeneral);
                ProgramaGeneralDoumentoDTO programa = new ProgramaGeneralDoumentoDTO();
                ProgramaGeneralDoumentoDTO programav2 = new ProgramaGeneralDoumentoDTO();

                //string[] listaTituloV1 = { "estructura curricular", "beneficios", "pre-requisitos", "certificación", "duración y horarios", "evaluación", "bibliografía", "material del curso", "pautas complementarias", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentación", "público objetivo", "metodología online de este programa", "modalidad", "inversion", "perfil del egresado", "mercado laboral", "expositores", "metodologia del programa" };
                //string[] listaTituloV2 = { "estructura curricular", "beneficios", "prerrequisitos", "certificacion", "duracion y horarios", "evaluacion", "bibliografia", "material del curso", "pautas complementarias", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentacion", "publico objetivo", "metodolog&#237;a online de este programa", "Modalidad", "Inversion", "Perfil del egresado", "Mercado laboral", "Expositores", "Metodolog&#237;a del programa" };

                // Se encarga de solo añadir las secciones que lleven por titulo palabras de listaTituloV2 o listaTituloV1 Y que tengan contenido.
                string[] listaTituloV1 = { "estructura curricular", "beneficios", "pre-requisitos", "certificación", "duración y horarios", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentación", "público objetivo", "metodología online de este programa", "modalidad", "inversion", "perfil del egresado", "mercado laboral", "expositores" };
                string[] listaTituloV2 = { "estructura curricular", "beneficios", "prerrequisitos", "certificacion", "duracion y horarios", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentacion", "publico objetivo", "metodolog&#237;a online de este programa", "Modalidad", "Inversion", "Perfil del egresado", "Mercado laboral", "Expositores" };
                var seccionResultado = new List<RegistroListaSeccionesDocumentoDTO>();
                for (var i = 0; i < listaTituloV2.Length; i++)
                {
                    var secEstructuraCurricular = seccionesv2.FirstOrDefault(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(listaTituloV2[i]));

                    if (secEstructuraCurricular == null || string.IsNullOrEmpty(secEstructuraCurricular.Contenido) || secEstructuraCurricular.Contenido == "<vacio></vacio>")
                        secEstructuraCurricular = seccionesv1.FirstOrDefault(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(listaTituloV1[i]));

                    if (secEstructuraCurricular != null && !string.IsNullOrEmpty(secEstructuraCurricular.Contenido) && secEstructuraCurricular.Contenido != "<vacio></vacio>")
                        seccionResultado.Add(secEstructuraCurricular);
                }

                //Si existe una seccion tanto en listaPrimera como en seccionResultado, reemplazaramos la de seccionResultado por la de listaPrimera
                if (listaPrimera != null && listaPrimera.Count > 0)
                {
                    if (listaPrimera.Any(item => item.Seccion.ToLower() == "prerrequisitos"))
                        seccionResultado.RemoveAll(x => LimpiarCadena(x.Titulo).ToLower().Equals("pre-requisitos"));

                    foreach (var item in listaPrimera)
                    {
                        if (item.Seccion == null) continue;
                        seccionResultado.RemoveAll(x => x.Titulo.ToLower().Equals(item.Seccion.ToLower()));
                    }
                }

                var seccionExpositores = new List<ProgramaGeneralEstructuraAgrupadoDTO>();
                var expositores = _unitOfWork.DocumentoSeccionPwRepository.ObtenerExpositoresPorIdGeneral(idPGeneral);
                foreach (var item in expositores)
                {
                    seccionExpositores.Add(new()
                    {
                        Seccion = "Expositores",
                        Titulo = string.Join(" ", item.PrimerNombre, item.SegundoNombre, item.ApellidoPaterno, item.ApellidoMaterno),
                        DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>() {
                            new()
                            {
                                Contenido = Regex.Replace(item.HojaVidaResumidaPerfil, "<.*?>|&nbsp;", ""),
                                Cabecera = null,
                                PiePagina = null,
                            }
                        }
                    });
                }

                if (programav2.ListaSeccionesContenidosDocumento != null && programav2.ListaSeccionesContenidosDocumento.Count > 0)
                {
                    var _listaCursos2 = programav2.ListaSeccionesContenidosDocumento.GroupBy(x => new { x.Contenido, x.Titulo }).Select(x => new { x.Key.Contenido, x.Key.Titulo }).ToList();
                    foreach (var itemCurso in _listaCursos2)
                    {
                        var _listaCapitulos = programav2.ListaSeccionesContenidosDocumento.Where(x => x.Contenido == itemCurso.Contenido).GroupBy(x => new { x.Contenido, x.Cabecera, x.PiePagina }).ToList();
                        ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                        obj.Seccion = itemCurso.Titulo;
                        obj.Titulo = itemCurso.Titulo;
                        obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();
                        foreach (var itemSecion in _listaCapitulos)
                        {
                            ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                            {
                                Contenido = itemSecion.Key.Contenido,
                                Cabecera = itemSecion.Key.Cabecera,
                                PiePagina = itemSecion.Key.PiePagina
                            };
                            obj.DetalleContenido.Add(tmp);

                        }
                        seccionExpositores.Add(obj);
                    }
                }

                var listaProgramaGeneralDocumentoSeccion = seccionExpositores.GroupBy(x => x.Seccion).Select(x => new ProgramaGeneralSeccionDocumentoDTO
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
                listaProgramaGeneralDocumentoSeccion.AddRange(listaPrimera);
                return listaProgramaGeneralDocumentoSeccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Flavio Rodrigo
        /// Fecha: 02/02/2022
        /// Version: 1.0
        /// <summary>
        /// Limpia la cadena de caracteres html y retiras las tildes de la cadena
        /// </summary>
        /// <returns>Cadena limpia</returns>
        private string LimpiarCadena(string valor)
        {
            string decodeString = HttpUtility.HtmlDecode(valor);
            string valorSinTildes = Regex.Replace(decodeString.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            return valorSinTildes;
        }

        /// Autor: Jashin Salazar
        /// Fecha: 18/08/2022 
        /// Version: 1.0 
        /// <summary>
        /// Genera estilos para hoja de calculo.
        /// </summary> 
        /// <returns> StyleSheet </returns>
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

        /// Autor: Jashin Salazar
        /// Fecha: 18/08/2022 
        /// Version: 1.0 
        /// <summary>
        /// Genera estilos Para Documento Pre-requisitos
        /// </summary> 
        /// <returns> StyleSheet </returns>
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
        /// Autor: Jashin Salazar
        /// Fecha: 18/08/2022 
        /// Version: 1.0 
        /// <summary>
        /// Genera estilos Para los Documentos 
        /// </summary> 
        /// <returns> StyleSheet </returns>
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
        public List<ProgramaGeneralSeccionDocumentoDTO> ObtenerInformacionProgramaGeneralSpeech(int idPGeneral)
        {
            try
            {
                var listaPrimera = ObtenerListaSeccionDocumentoProgramaGeneralSpeech(idPGeneral);
                var seccionesv2 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV2Objetivos(idPGeneral);
                var seccionesv1 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV1Speech(idPGeneral);
                var seccionesProgramaGeneralPresentacionArgumento = _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.ObtenerProgramaGeneralPresentacionArgumentoHtml(idPGeneral);
                ProgramaGeneralDoumentoDTO programa = new ProgramaGeneralDoumentoDTO();
                ProgramaGeneralDoumentoDTO programav2 = new ProgramaGeneralDoumentoDTO();

                List<RegistroListaSeccionesDocumentoDTO> seccionResultado = new List<RegistroListaSeccionesDocumentoDTO>();
                string[] listaTituloV1 = { "estructura curricular", "beneficios", "pre-requisitos", "certificación", "duración y horarios", "evaluación", "bibliografía", "material del curso", "pautas complementarias", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentación", "público objetivo", "metodología online de este programa", "modalidad", "inversion", "perfil del egresado", "mercado laboral", "expositores", "metodologia del programa" };
                string[] listaTituloV2 = { "estructura curricular", "beneficios", "prerrequisitos", "certificacion", "duracion y horarios", "evaluacion", "bibliografia", "material del curso", "pautas complementarias", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentacion", "publico objetivo", "metodolog&#237;a online de este programa", "Modalidad", "Inversion", "Perfil del egresado", "Mercado laboral", "Expositores", "Metodolog&#237;a del programa" };
                for (var i = 0; i < listaTituloV2.Length; i++)
                {
                    var secEstructuraCurricularV2 = seccionesv2.Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(listaTituloV2[i])).FirstOrDefault();
                    if (secEstructuraCurricularV2 != null)
                    {
                        if (secEstructuraCurricularV2.Contenido == null || secEstructuraCurricularV2.Contenido == "" || secEstructuraCurricularV2.Contenido == "<vacio></vacio>")
                        {
                            var secEstructuraCurricularV1 = seccionesv1.Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(listaTituloV1[i])).FirstOrDefault();
                            if (secEstructuraCurricularV1 != null)
                            {
                                if (secEstructuraCurricularV1.Contenido != null && secEstructuraCurricularV1.Contenido != "" && secEstructuraCurricularV1.Contenido != "<vacio></vacio>")
                                {
                                    seccionResultado.Add(secEstructuraCurricularV1);
                                }
                            }
                        }
                        else
                        {
                            seccionResultado.Add(secEstructuraCurricularV2);
                        }
                    }
                    else
                    {
                        var secEstructuraCurricularV1 = seccionesv1.Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(listaTituloV1[i])).FirstOrDefault();
                        if (secEstructuraCurricularV1 != null)
                        {
                            if (secEstructuraCurricularV1.Contenido != null && secEstructuraCurricularV1.Contenido != "" && secEstructuraCurricularV1.Contenido != "<vacio></vacio>")
                            {
                                seccionResultado.Add(secEstructuraCurricularV1);
                            }
                        }
                    }
                }
                seccionResultado.AddRange(seccionesProgramaGeneralPresentacionArgumento);
                if (listaPrimera != null && listaPrimera.Count > 0)
                {
                    foreach (var item in listaPrimera)
                    {
                        if (item.Seccion != null)
                        {
                            var temp = seccionResultado.Where(x => x.Titulo.ToLower().Equals(item.Seccion.ToLower())).FirstOrDefault();
                            if (temp != null) { seccionResultado.Remove(temp); }
                            if (LimpiarCadena(item.Seccion).ToLower() == "certificacion")
                            {
                                var temp2 = seccionResultado.Where(x => LimpiarCadena(x.Titulo).ToLower().Equals("certificacion")).FirstOrDefault();
                                if (temp2 != null) { seccionResultado.Remove(temp2); }
                            }
                            if (LimpiarCadena(item.Seccion).ToLower() == "prerrequisitos")
                            {
                                var temp3 = seccionResultado.Where(x => LimpiarCadena(x.Titulo).ToLower().Equals("pre-requisitos")).FirstOrDefault();
                                if (temp3 != null) { seccionResultado.Remove(temp3); }
                            }
                        }
                    }
                }

                programav2.ListaSeccionesContenidosDocumento = seccionResultado;

                List<ProgramaGeneralEstructuraAgrupadoDTO> Contenido = new List<ProgramaGeneralEstructuraAgrupadoDTO>();



                if (programav2.ListaSeccionesContenidosDocumento != null && programav2.ListaSeccionesContenidosDocumento.Count > 0)
                {
                    var _listaCursos2 = programav2.ListaSeccionesContenidosDocumento.GroupBy(x => new { x.Contenido, x.Titulo }).Select(x => new { x.Key.Contenido, x.Key.Titulo }).ToList();
                    foreach (var itemCurso in _listaCursos2)
                    {
                        var _listaCapitulos = programav2.ListaSeccionesContenidosDocumento.Where(x => x.Contenido == itemCurso.Contenido).GroupBy(x => new { x.Contenido, x.Cabecera, x.PiePagina }).ToList();
                        ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                        obj.Seccion = itemCurso.Titulo;
                        obj.Titulo = itemCurso.Titulo;
                        obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();
                        foreach (var itemSecion in _listaCapitulos)
                        {
                            ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                            {
                                Contenido = itemSecion.Key.Contenido,
                                Cabecera = itemSecion.Key.Cabecera,
                                PiePagina = itemSecion.Key.PiePagina
                            };
                            obj.DetalleContenido.Add(tmp);

                        }
                        Contenido.Add(obj);
                    }
                }

                var listaProgramaGeneralDocumentoSeccion = Contenido.GroupBy(x => x.Seccion).Select(x => new ProgramaGeneralSeccionDocumentoDTO
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
                listaProgramaGeneralDocumentoSeccion.AddRange(listaPrimera);
                return listaProgramaGeneralDocumentoSeccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public List<ProgramaGeneralSeccionDocumentoDTO> ObtenerListaSeccionDocumentoProgramaGeneralSpeech(int idPGeneral)
        {
            try
            {
                var programa = new ProgramaGeneralDoumentoDTO();

                var listaCursos = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPGeneralHijos(idPGeneral);
                if (listaCursos.Count > 0)
                {
                    programa.EsProgramaPadre = true;
                    var secciones = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoSpeech(idPGeneral);

                    if (secciones != null && secciones.Count > 0)
                        programa.ListaSeccionesContenidosDocumento = secciones;

                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();
                    foreach (var item in listaCursos)
                    {
                        var seccionEstructura = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoEstructuraCurricularPorIdsPGeneral(idPGeneral, item.Id);

                        if (seccionEstructura != null && seccionEstructura.Count > 0)
                            programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                    }
                }
                else
                {
                    programa.EsProgramaPadre = false;

                    var secciones = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumento(idPGeneral);

                    if (secciones != null && secciones.Count > 0)
                        programa.ListaSeccionesContenidosDocumento = secciones;

                    var seccionEstructura = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoEstructuraCurricular(idPGeneral);

                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();
                    if (seccionEstructura != null && seccionEstructura.Count() > 0)
                        programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                }

                var contenido = new List<ProgramaGeneralEstructuraAgrupadoDTO>();
                if (programa.ListaSeccionesContenidosDocumentoEstructura != null && programa.ListaSeccionesContenidosDocumentoEstructura.Count > 0)
                {
                    if (programa.EsProgramaPadre)
                    {
                        var listaCursosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.GroupBy(x => new { x.IdPGeneral, x.NombreCurso, x.Titulo }).Select(x => new { x.Key.IdPGeneral, x.Key.NombreCurso, x.Key.Titulo }).ToList();

                        int contadorcursos = listaCursosSecciones.Count();
                        int contadorinterno = 0;

                        foreach (var itemCurso in listaCursosSecciones)
                        {
                            contadorinterno++;
                            var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12 && x.IdPGeneral == itemCurso.IdPGeneral).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.IdPGeneral, x.Cabecera, x.PiePagina }).ToList();
                            var obj = new ProgramaGeneralEstructuraAgrupadoDTO()
                            {
                                Seccion = itemCurso.Titulo,
                                Titulo = itemCurso.NombreCurso,
                                DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>()
                            };

                            if (contadorinterno < contadorcursos)
                            {
                                foreach (var itemSecion in listaCapitulosSecciones)
                                {
                                    var tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                    {
                                        Contenido = itemSecion.Key.Contenido,
                                        Cabecera = itemSecion.Key.Cabecera,
                                        PiePagina = ""
                                    };
                                    obj.DetalleContenido.Add(tmp);

                                }
                            }
                            else
                            {
                                foreach (var itemSecion in listaCapitulosSecciones)
                                {
                                    var tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                    {
                                        Contenido = itemSecion.Key.Contenido,
                                        Cabecera = itemSecion.Key.Cabecera,
                                        PiePagina = itemSecion.Key.PiePagina
                                    };
                                    obj.DetalleContenido.Add(tmp);

                                }
                            }
                            contenido.Add(obj);
                        }
                    }
                    else
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.Titulo }).ToList();
                        int contadorcursos = listaCapitulosSecciones.Count();
                        int contadorinterno = 0;

                        foreach (var item in listaCapitulosSecciones)
                        {
                            contadorinterno++;
                            var listaSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.Contenido == item.Key.Contenido).Select(x => x.NumeroFila).ToList();
                            var capituloSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.IdSeccionTipoDetalle_PW == 13 && listaSesiones.Contains(s.NumeroFila)).GroupBy(x => new { x.Contenido, x.NombreCurso, x.Cabecera, x.PiePagina }).ToList();
                            var obj = new ProgramaGeneralEstructuraAgrupadoDTO()
                            {
                                Seccion = item.Key.Titulo,
                                Titulo = item.Key.Contenido,
                                DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>()
                            };

                            if (contadorinterno < contadorcursos)
                            {
                                if (capituloSesiones != null && capituloSesiones.Count > 0)
                                {
                                    foreach (var itemSecion in capituloSesiones)
                                    {
                                        var tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                        {
                                            Contenido = itemSecion.Key.Contenido,
                                            Cabecera = itemSecion.Key.Cabecera,
                                            PiePagina = ""
                                        };
                                        obj.DetalleContenido.Add(tmp);
                                    }
                                    contenido.Add(obj);
                                }
                                else
                                {
                                    foreach (var itemSecion in capituloSesiones)
                                    {
                                        var tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                        {
                                            Contenido = item.Key.Contenido
                                        };
                                        obj.DetalleContenido.Add(tmp);
                                    }
                                    contenido.Add(obj);
                                }
                            }
                            else
                            {
                                if (capituloSesiones != null && capituloSesiones.Count > 0)
                                {
                                    foreach (var itemSecion in capituloSesiones)
                                    {
                                        var tmp = new ProgramaGeneralEstructuraDetalleDTO()
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
                                        var tmp = new ProgramaGeneralEstructuraDetalleDTO()
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
                }
                if (programa.ListaSeccionesContenidosDocumento != null && programa.ListaSeccionesContenidosDocumento.Count > 0)
                {
                    var listaCursosSecciones = programa.ListaSeccionesContenidosDocumento.GroupBy(x => new { x.IdSeccionTipoDetalle_PW, x.Titulo }).Select(x => new { x.Key.IdSeccionTipoDetalle_PW, x.Key.Titulo }).ToList();
                    foreach (var itemCurso in listaCursosSecciones)
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumento.Where(x => x.IdSeccionTipoDetalle_PW == itemCurso.IdSeccionTipoDetalle_PW).GroupBy(x => new { x.Contenido, x.Cabecera, x.PiePagina }).ToList();
                        var obj = new ProgramaGeneralEstructuraAgrupadoDTO()
                        {
                            Seccion = itemCurso.Titulo,
                            Titulo = "",//itemCurso.Titulo;
                            DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>()
                        };
                        foreach (var itemSecion in listaCapitulosSecciones)
                        {
                            var tmp = new ProgramaGeneralEstructuraDetalleDTO()
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
    }
}
