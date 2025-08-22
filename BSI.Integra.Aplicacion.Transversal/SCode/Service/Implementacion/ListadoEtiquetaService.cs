using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Globalization;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ListadoEtiquetaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 17/08/2022
    /// <summary>
    /// Gestión general de Etiquetas
    /// </summary>
    public class ListadoEtiquetaService : IListadoEtiquetaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ListadoEtiquetaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, Entity>(MemberList.None).ReverseMap());
            //_mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la central con el anexo del personal
        /// </summary>
        /// <param name="central"> Central </param>
        /// <param name="anexo3Cx"> Anexo 3CX </param>
        /// <returns>Cadena con la central y el anexo del personal : string </returns>
        public string ObtenerTelefonoPersonal(string central, string anexo3Cx)
        {
            string valor;

            if (central == "192.168.0.20" || central == "192.168.2.20")
            {
                //aqp //lima
                valor = "(51) 1 207 2770 - Anexo " + anexo3Cx;
            }
            else if (central == "192.168.3.20")
            {
                //bogota
                valor = "57 (601) 381 9462 - Anexo " + anexo3Cx;
            }
            else if (central == "192.168.4.20")
            {
                //cd mexico
                valor = "52 (55) 4000 3255 - Anexo " + anexo3Cx;
            }
            else if (central == "192.168.5.20")
            {
                //santiago
                valor = "56 (2) 2760 9120 - Anexo " + anexo3Cx;
            }
            else
            {
                valor = "No registra central asignada";
            }

            return valor;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Devuelve la firma del personal asignado con el cuermo HTML
        /// </summary>
        /// <param name="email"> Email </param>
        /// <returns>HTML incrustada la imagen de la firma del personal asignado : string </returns>
        public string EtiquetaUrlFirmaCorreo(string email)
        {
            string firma = string.Empty;

            if (!string.IsNullOrEmpty(email))
            {
                string[] usuario = email.Split("@");

                firma = string.Concat("<img src='https://repositorioweb.blob.core.windows.net/firmas/", usuario[0], ".png' align='left'>");
            }

            return firma;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera la plantilla basica por etiqueta de curso del area
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="idEtiqueta">Id de la lista curso area(PK de la tabla mkt.T_ListaCursoAreaEtiqueta)</param>
        /// <returns>string</returns>
        public string EtiquetaListaProgramasPorIdEtiqueta(int idOportunidad, int idEtiqueta)
        {
            var servicioOportunidad = new OportunidadService(_unitOfWork);
            var servicioClaveValor = new PlantillaClaveValorService(_unitOfWork);

            string resultado = string.Empty;
            int contador = 1;
            var oportunidad = servicioOportunidad.ObtenerPorId(idOportunidad);

            if (oportunidad != null)
            {
                var programas = servicioClaveValor.ObtenerMontosCursosRelacionados(idOportunidad, idEtiqueta);

                if (programas == null) return resultado;

                foreach (var item in programas)
                {
                    try
                    {
                        string url_video = string.Empty;

                        if (item.Url_Video != null)
                        {
                            url_video = "<a href='https://" + item.Url_Video + "' target = '_blank' >" + "Ver Presentaci&oacute;n" + "</a>";
                        }

                        resultado = resultado + "<p><b>" + contador.ToString() + ". " + item.Nombre + "</b></p>";
                        resultado = resultado + "<p><b>Modalidad: </b>" + item.Modalidad + " " + "<b> Duraci&oacute;n: </b>" + " " + item.Duracion + "<br/>";
                        resultado = resultado + "<b>Presentaci&oacute;n: </b>" + url_video + "<br/>";
                        resultado = resultado + "<b>Inversi&oacute;n Desde: </b>" + item.Inversion + "<br/>";
                    }
                    catch (Exception)
                    {
                    }
                    contador++;
                }
            }

            return resultado;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el armado del boton HTML mediante la urlVersion, validando que no sea nulo o vacio
        /// </summary>
        /// <param name="urlVersion"> Cadena de Versión Url </param>
        /// <returns>Boton HTML con la url incrustada de la version : string </returns>
        public string ObtenerUrlVersion(string urlVersion)
        {
            string valor = string.Empty;

            if (!string.IsNullOrEmpty(urlVersion))
                valor = "<a href='" + urlVersion + "' style='background-color: #3e8f3e;border-radius: 10px;padding:10px 10px;line-height: 1.5;text-decoration: none;color: #fff; font-family: Helvetica Neue,Helvetica,Arial,sans-serif;font-size: 16px'>Obtener acceso de prueba gratis</a>";

            return valor;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Devuelve los cursos relacionados al centro de costo indicado
        /// </summary>
        /// <param name="idCentroCosto"> Id Centro de Costo </param>
        /// <returns>HTML con los programas relacionados al centro de costo : string </returns>
        public string EtiquetaCursoRelacionado(int idCentroCosto)
        {
            var servicioClaveValor = new PlantillaClaveValorService(_unitOfWork);
            string valor = string.Empty;
            var urlCursosRelacionados = servicioClaveValor.ObtenerCursosRelacionadosPorIdCentroCosto(idCentroCosto);
            valor = urlCursosRelacionados != null ? urlCursosRelacionados.Aggregate(valor, (current, url) => current + "<a href='" + url.UrlPagina + "'>" + url.Nombre + "</a><br/><br/>") : string.Empty;

            return valor;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el armado del boton HTML mediante la urlBrochurePrograma, validando que no sea nulo o vacio
        /// </summary>
        /// <param name="urlBrochurePrograma"> url de brochure de programa </param>
        /// <returns>Boton HTML con la url incrustada del brochure : string </returns>
        public string ObtenerUrlBrochurePrograma(string urlBrochurePrograma)
        {
            string valor = string.Empty;

            if (!string.IsNullOrEmpty(urlBrochurePrograma))
                valor = "<a href='" + urlBrochurePrograma + "' style='background-color: #f5a623;border-radius: 10px;padding: 10px 10px;line-height: 1.5;text-decoration: none;color: #fff; font-family: Helvetica Neue,Helvetica,Arial,sans-serif;font-size: 16px'>Descargar brochure</a>";

            return valor;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el armado del boton HTML mediante la urlBrochurePrograma, validando que no sea nulo o vacio
        /// </summary>
        /// <param name="urlBrochurePrograma"> url de brochure de programa </param>
        /// <returns>Boton HTML con la url incrustada del brochure : string </returns>
        public string EtiquetaExpositor(int idPGeneral)
        {
            var expositores = _unitOfWork.ExpositorRepository.ObtenerExpositoresPorProgramaGeneral(idPGeneral);
            string result = string.Empty;

            result = expositores.Any() ? string.Concat("<div class='expositores'>", expositores.Aggregate(result, (current, expositor) => current + "<h3>" + expositor.Nombres + "</h3><p style='text-align: justify; text-justify: inter-word;'>" + expositor.HojaVida + "</p>")) : string.Empty;

            return result;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la duracion y horario logica especial
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns>HTML con la duracion y horario segun logica especial : string </returns>
        public string ObtenerDuracionAndHorario(int idPGeneral)
        {
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var servicioInformacion = new InformacionProgramaService(_unitOfWork);
            List<ModalidadProgramaDTO> modalidades = servicioPGeneral.ObtenerModalidadesPorProgramaGeneral(idPGeneral);

            return servicioInformacion.ObtenerContenidoHorarios(modalidades, string.Empty, idPGeneral);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el armado del boton HTML mediante la urlDocumentoCronograma, validando que no sea nulo o vacio
        /// </summary>
        /// <param name="urlDocumentoCronograma"> cadena de url de Documento de Cronograma </param>
        /// <returns>Boton HTML con la url incrustada del cronograma : string </returns>
        public string ObtenerUrlDocumentoCronograma(string urlDocumentoCronograma)
        {
            string valor = string.Empty;

            if (!string.IsNullOrEmpty(urlDocumentoCronograma))
                valor = "<a href='" + urlDocumentoCronograma + "' style='background-color: #f5a623;border-radius: 10px;padding: 10px 10px;line-height: 1.5;text-decoration: none;color: #fff; font-family: Helvetica Neue,Helvetica,Arial,sans-serif;font-size: 16px'>Descargar cronograma</a>";

            return valor;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera HTML a partir de la lista de secciones de documento del programa general
        /// </summary>
        /// <param name="listaProgramaGeneralDocumentoSeccion"> Lista de programas por documento sección </param>
        /// <param name="conTitulo"> Validación de Título </param>
        /// <returns>Retorna la estructura HTML de la lista enviada : List<ProgramaGeneralSeccionAnexosHTMLDTO> </returns>
        public List<ProgramaGeneralSeccionAnexosHTMLDTO> GenerarHTMLProgramaGeneralDocumentoSeccion(List<ProgramaGeneralSeccionDocumentoDTO> listaProgramaGeneralDocumentoSeccion, bool conTitulo)
        {
            try
            {
                var lista = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();

                foreach (var item in listaProgramaGeneralDocumentoSeccion)
                {
                    string contenido = string.Empty;
                    conTitulo = item.Seccion == "Estructura Curricular";

                    foreach (var detalleSeccion in item.DetalleSeccion)
                    {
                        contenido += conTitulo ? $"<p><strong>{detalleSeccion.Titulo}</strong></p>" : string.Empty;

                        contenido += (detalleSeccion.Cabecera != string.Empty) ? $"<p>{detalleSeccion.Cabecera}</p><ul>" : "<ul>";
                        contenido = detalleSeccion.DetalleContenido.Aggregate(contenido, (current, contenidoSeccion) => current + "<li>" + contenidoSeccion + "</li>");
                        contenido += (detalleSeccion.PiePagina != string.Empty) ? $"</ul><p>{detalleSeccion.PiePagina}</p>" : "</ul>";
                    }

                    lista.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
                    {
                        Seccion = item.Seccion,
                        Contenido = contenido
                    });
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene El Valor de la Etiqueta MontoPago V2 para las Plantillas
        /// </summary>
        /// <param name="oportunidad"> Información de Oportunidad </param>
        /// <param name="idPGeneral"> Id de programa general </param>
        /// <returns> string </returns>
        public string EtiquetaMontosPagoV2(Oportunidad oportunidad, int idPGeneral)
        {
            string valorTemporal = ObtenerMontosPagoPaquetesV2(oportunidad, idPGeneral);

            return valorTemporal ?? EtiquetaMontosPago(oportunidad.Id);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Una Tabla en Html de Monto Pago Segun Oportunidad V2, incluyendo beneficios de la vista V_BeniciosPartnerDocumento
        /// </summary>
        /// <param name="oportunidad"> Información de oportunidad </param>
        /// <param name="idPGeneral"> Id de programa general </param>
        /// <returns> string </returns>
        private string ObtenerMontosPagoPaquetesV2(Oportunidad oportunidad, int idPGeneral)
        {
            MontoPagoService montoPagoService = new MontoPagoService(_unitOfWork);
            PEspecificoService pEspecificoService = new PEspecificoService(_unitOfWork);
            AlumnoService alumnoService = new AlumnoService(_unitOfWork);
            PGeneralService pGeneralService = new PGeneralService(_unitOfWork);
            DocumentoService documentoService = new DocumentoService(_unitOfWork);

            var versiones = montoPagoService.ObtenerVersionesMontoPagoV2(oportunidad.Id);

            if (!versiones.Any())
                return null;

            var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(oportunidad.IdCentroCosto.Value);
            Alumno alumno = alumnoService.ObtenerPorId(oportunidad.IdAlumno.Value);

            List<VersionProgramaNombreUsuarioDTO> versionPrograma = _unitOfWork.VersionProgramaRepository.ObtenerTodo();
            List<string> listaBeneficios;

            string tabla = "";
            int contadorBeneficios = 0;

            tabla = "<table border cellpadding=2 cellspacing=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
            tabla += "<tr>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Versión </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Beneficios </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Tipo pago </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Inversión </th>";
            tabla += "</tr>";

            foreach (VersionProgramaNombreUsuarioDTO item in versionPrograma)
            {
                listaBeneficios = documentoService.ObtenerBeneficiosConfiguradosProgramaGeneral(pEspecifico.IdProgramaGeneral.Value, alumno.IdCodigoPais, item.Id);
                contadorBeneficios += listaBeneficios.Count;

                if (listaBeneficios.Count > 0)
                {
                    List<MontoPagoEtiquetaDTO> infoVersiones = versiones.Where(x => x.Paquete == item.Id).ToList();

                    tabla += "<tr>";

                    tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{(infoVersiones.Count != 0 ? infoVersiones.Count : 1)}'>{item.Nombre}</td>";

                    tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{(infoVersiones.Count != 0 ? infoVersiones.Count : 1)}'><ul>";

                    foreach (string beneficio in listaBeneficios)
                    {
                        tabla += $"<li>{beneficio}</li>";
                    }

                    tabla += "</ul></td>";
                    int i = 0;

                    foreach (var re in infoVersiones)
                    {
                        tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + re.tp_nombre + "</td>";
                        tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + (re.tp_cuotas == 2 ? re.Simbolo.Replace(".", " ") + re.mp_precio.ToString()
                                : "1 matricula de: " + re.Simbolo.Replace(".", "") + re.mp_matricula + " y " + re.mp_nro_cuotas + " cuotas de " + re.Simbolo.Replace(".", "") + re.mp_cuotas)
                            + "</td>";

                        i += 1;

                        if (i < infoVersiones.Count)
                            tabla += "</tr><tr>";
                    }

                    tabla += "</tr>";
                }
            }

            tabla += "</table>";

            var pieBeneficio = pGeneralService.SeccionIndividualPGeneral(idPGeneral, "Beneficios");

            if (pieBeneficio != null)
                tabla += $"<p>{pGeneralService.SeccionIndividualPGeneral(idPGeneral, "Beneficios").PiePagina}</p>";

            if (contadorBeneficios == 0)
                tabla = null;

            return tabla;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene El Valor de la Etiqueta MontoPago para las Plantillas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public string EtiquetaMontosPago(int idOportunidad)
        {
            var tabla = ObtenerMontosPagoPaquetes(idOportunidad);

            if (tabla == null)
            {
                string precioNormal = string.Empty;

                string precioContado = ObtenerPrecioContado(idOportunidad);
                string precioCuotas = ObtenerPrecioCuotas(idOportunidad);

                if (!string.IsNullOrEmpty(precioContado)) precioNormal = "<b>Al Contado: </b>" + precioContado;

                if (!string.IsNullOrEmpty(precioCuotas)) precioNormal += "<div style=' height:1px;'></div>" + "<b>Financiamiento: </b>" + precioCuotas;

                return precioNormal;
            }
            else
            {
                return tabla;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Una Tabla en Html de Monto Pago Segun Oportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> string </returns>
        private string ObtenerMontosPagoPaquetes(int idOportunidad)
        {
            MontoPagoService montoPagoService = new MontoPagoService(_unitOfWork);
            List<MontoPagoEtiquetaDTO> versiones = montoPagoService.ObtenerVersionesMontoPago(idOportunidad);
            List<MontoPagoEtiquetaAgrupadoDTO> agrupado = (from x in versiones
                                                           orderby x.OrdenBeneficio
                                                           group x by new { x.Paquete, x.tp_nombre, x.tp_cuotas, x.mp_precio, x.Simbolo, x.mp_matricula, x.mp_nro_cuotas, x.mp_cuotas } into gj
                                                           select new MontoPagoEtiquetaAgrupadoDTO
                                                           {
                                                               Paquete = gj.Key.Paquete,
                                                               tp_nombre = gj.Key.tp_nombre,
                                                               tp_cuotas = gj.Key.tp_cuotas,
                                                               mp_precio = gj.Key.mp_precio,
                                                               Simbolo = gj.Key.Simbolo,
                                                               mp_matricula = gj.Key.mp_matricula,
                                                               mp_nro_cuotas = gj.Key.mp_nro_cuotas,
                                                               mp_cuotas = gj.Key.mp_cuotas,
                                                               Beneficios = gj.Select(x => x.Titulo).ToList()
                                                           }).ToList();
            if (agrupado.Count() == 0)
            {
                return null;
            }
            else
            {
                string tabla = "";
                tabla = "<TABLE BORDER CELLPADDING=2 CELLSPACING=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
                tabla += "<tr>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Versión </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Beneficios </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Tipo pago </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Inversión </th>";
                tabla += "</tr>";
                foreach (var re in agrupado)
                {
                    var credito = agrupado.Where(s => s.Paquete == re.Paquete && s.tp_cuotas != 2).FirstOrDefault(); //add roy
                    tabla += "<tr>";
                    tabla += "<td style='border: 1px solid #E6E6E6' >" + ObtenerPaquete(re.Paquete == null ? "" : re.Paquete.ToString()) + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6'> " + string.Join(", ", re.Beneficios.Distinct()) + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + re.tp_nombre + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + (re.tp_cuotas == 2 ? re.Simbolo.Replace(".", " ") + re.mp_precio.ToString() /*+ (credito == null? "<br /><span style='color:red;'>" + "Con 25% de descuento:" + re.Simbolo.Replace(".", " ") + Math.Round(((re.mp_precio * 75) / 100), MidpointRounding.AwayFromZero).ToString() + "</span>" : "<br /><span style='color:red;'>" + "Con 25% de descuento:" + credito.Simbolo.Replace(".", " ") + Math.Round(((credito.mp_precio * 75) / 100), MidpointRounding.AwayFromZero).ToString() + "</span>")*///decomnetar para 25 % descuento
                            : "1 matricula de: " + re.Simbolo.Replace(".", "") + re.mp_matricula + " y " + re.mp_nro_cuotas + " cuotas de " + re.Simbolo.Replace(".", "") + re.mp_cuotas)
                            + "</td>";
                    tabla += "</tr>";

                }
                tabla += "</TABLE>";
                return tabla;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Los Paquetes Existentes
        /// </summary>
        /// <param name="val">identificador del Paquete</param>
        /// <returns> string </returns>
        private string ObtenerPaquete(string val)
        {
            switch (val)
            {
                case "1":
                    return "Versión Basica";
                case "2":
                    return "Versión Profesional";
                case "3":
                    return "Versión Gerencial";
                default:
                    return "";
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera una Tabla Para el Precio al Contado de un Programa 
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> string </returns>
        private string ObtenerPrecioContado(int idOportunidad)
        {
            MontoPagoService montoPagoService = new MontoPagoService(_unitOfWork);
            string respuesta = "";
            var contado = montoPagoService.ObtenerMontoPagoContadoPorIdOportunidad(idOportunidad);
            if (contado != null) respuesta = GenerateGridCronogamaPagos(contado);

            return respuesta;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Tablas De pagos deacuerdo a Formula
        /// </summary>
        /// <param name="data"> Datos Compuestos de monto Pago </param>
        /// <returns> string </returns>
        public string GenerateGridCronogamaPagos(MontoPagoCompuestoDTO data)
        {
            string tablaRespuesta = "";
            switch (data.tp_formula)
            {
                case 0://sin descuento                     
                    tablaRespuesta = GeneraHtmlPrecioCuotas(data);
                    break;
                case 1: //matricula
                    tablaRespuesta = GeneraHtml(GenerarGridMatricula(data));
                    break;
                case 2: //cuotas
                    tablaRespuesta = GeneraHtmlPrecioCuotas(data);
                    break;
                case 3: //ambos
                    tablaRespuesta = GeneraHtml(GenerarGridAmbos(data));
                    break;
                case 4: //general
                    tablaRespuesta = GeneraHtml(GenerarGridGeneral(data));
                    break;
                case 5:
                    tablaRespuesta = GeneraHtmlPrecioContado(GenerarGridNormal(data));
                    break;
            }
            return tablaRespuesta;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Html De Precio en Cuotas
        /// </summary>
        /// <param name="data"> información de monto de pago compuesto </param>
        /// <returns> string </returns>
        public string GeneraHtmlPrecioCuotas(MontoPagoCompuestoDTO data)
        {
            MonedaService monedaService = new MonedaService(_unitOfWork);
            string tabla = string.Empty;
            string moneda = string.Empty;

            var respuesta = monedaService.ObtenerMonedaPorId(data.mp_moneda);

            if (respuesta != null)
            {
                moneda = respuesta.Simbolo;
            }

            if (!string.IsNullOrEmpty(moneda))
            {
                tabla = "1 Matricula de " + moneda.Replace(".", " ") + " " + data.mp_matricula + " y " + data.mp_nro_cuotas + " cuotas de " + moneda + " " + data.mp_cuotas;
            }
            return tabla;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Una Tabla Con las Cuotas de Pago
        /// </summary>
        /// <param name="lista">campos de Cuotas Para Generar tabla</param>
        /// <returns> string </returns>
        public string GeneraHtml(List<PagoCuotaDTO> lista)
        {
            string tabla = "";
            tabla = "<TABLE BORDER CELLPADDING=2 CELLSPACING=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
            tabla += "<tr>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Descripcion </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Fecha pago </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Monto cuota con descuento </th>";
            tabla += "</tr>";
            foreach (var re in lista)
            {
                tabla += "<tr>";
                tabla += "<td style='border: 1px solid #E6E6E6' >" + re.cuotaDescripcion + "</td>";
                tabla += "<td style='border: 1px solid #E6E6E6'> " + re.fechapago.ToShortDateString() + "</td>";
                tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + re.montoCuotaDescuento + "</td>";
                tabla += "</tr>";
            }
            tabla += "</TABLE>";
            return tabla;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Html del Precio Contado
        /// </summary>
        /// <param name="lista"> Lista de información de pago de cuotas </param>
        /// <returns> string </returns>
        private string GeneraHtmlPrecioContado(List<PagoCuotaDTO> lista)
        {
            string tabla = "";
            tabla = lista[0].SimboloMoneda.Replace(".", " ") + " " + lista[0].montoCuota.ToString();
            return tabla;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera grid de matrícula
        /// </summary>
        /// <param name="data"> Información de monto pago compuesto </param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> GenerarGridMatricula(MontoPagoCompuestoDTO data)
        {
            var numeroco = 1;
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            DateTime fecha = new DateTime();
            var tamanioMatricula = 0;
            tamanioMatricula = data.tp_fracciones_matricula;
            if (tamanioMatricula == 0) tamanioMatricula = 1;

            for (var j = 0; j < tamanioMatricula; j++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO();
                obj.numeroCuota = numeroco;
                obj.cuotaDescripcion = "Matricula " + (numeroco);
                obj.montoCuota = data.mp_matricula;
                DateTime currentDate = DateTime.Now;
                obj.fechapago = currentDate;
                obj.montoCuotaDescuento = TipoDescuentoGeneral((data.mp_matricula / tamanioMatricula), data.tp_porcentaje_matricula);
                obj.ispagado = false;
                obj.es_matricula = true;
                lista.Add(obj);
                numeroco = numeroco + 1;
            }
            // Cuotas
            var tamaniocuotas = 0;
            var tamanioContador = 0;
            var tamanio = data.mp_nro_cuotas;
            numeroco = numeroco - 1;
            tamaniocuotas = tamanio;
            for (var i = 0; i < tamanio; i++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO();
                numeroco = numeroco + 1;
                tamanioContador = tamanioContador + 1;
                obj.numeroCuota = numeroco;
                obj.cuotaDescripcion = "Cuota - " + (numeroco);
                obj.montoCuota = data.mp_cuotas;
                obj.montoCuotaDescuento = TipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_cuotas);
                obj.ispagado = false;
                obj.es_matricula = false;
                fecha = CalcularFechaInicial(data, i);
                obj.fechapago = fecha;
                lista.Add(obj);
                if (tamanioContador != tamanio)
                {
                    var mes = fecha.Month;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        PagoCuotaDTO obj1 = new PagoCuotaDTO();
                        numeroco = numeroco + 1;
                        obj1.numeroCuota = numeroco;
                        obj1.cuotaDescripcion = "Cuota - " + (numeroco);
                        obj1.montoCuota = data.mp_cuotas;
                        obj1.montoCuotaDescuento = TipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_cuotas);
                        obj.es_matricula = false;
                        obj1.fechapago = CalcularFechaInicial(data, i);
                        lista.Add(obj1);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Tabla De monto Pago pa Contado y Credito
        /// </summary>
        /// <param name="data"> datos Coumpuestos de MontoPago </param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> GenerarGridAmbos(MontoPagoCompuestoDTO data)
        {
            var numeroco = 1;
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            // Matriculas
            var tamanioMatricula = 0;
            tamanioMatricula = data.tp_fracciones_matricula;

            if (tamanioMatricula == 0) tamanioMatricula = 1;
            for (var j = 0; j < tamanioMatricula; j++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO
                {
                    numeroCuota = numeroco,
                    cuotaDescripcion = "Matricula " + numeroco,
                    montoCuota = data.mp_matricula
                };
                DateTime currentDate = DateTime.Now;
                obj.fechapago = currentDate;
                obj.montoCuotaDescuento = TipoDescuentoGeneral((data.mp_matricula / tamanioMatricula), data.tp_porcentaje_matricula);
                obj.ispagado = false;
                obj.es_matricula = true;
                lista.Add(obj);
                numeroco = numeroco + 1;
            }

            /////cuotas///////////////////////////////////////////////////
            var tamanio = 0;
            var tamanioContador = 0;
            tamanio = data.mp_nro_cuotas + data.tp_cuotas_adicionales;
            numeroco = numeroco - 1;
            var tamanioCuotas = tamanio;
            var sinDescuento = data.mp_precio - data.mp_matricula;

            for (var i = 0; i < tamanio; i++)
            {
                numeroco = numeroco + 1;
                tamanioContador = tamanioContador + 1;
                PagoCuotaDTO obj = new PagoCuotaDTO
                {
                    numeroCuota = numeroco,
                    cuotaDescripcion = "Cuota - " + numeroco,
                    montoCuota = data.mp_cuotas,
                    montoCuotaDescuento =
                        TipoDescuentoGeneral(sinDescuento / tamanioCuotas, data.tp_porcentaje_cuotas),
                    ispagado = false,
                    es_matricula = false
                };
                DateTime fecha = CalcularFechaInicial(data, i);
                obj.fechapago = fecha;
                lista.Add(obj);
                if (tamanioContador != tamanio)
                {
                    var mes = fecha.Month + 1;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        PagoCuotaDTO obj1 = new PagoCuotaDTO();
                        numeroco = numeroco + 1;
                        obj1.numeroCuota = numeroco;
                        obj1.cuotaDescripcion = "Cuota - " + numeroco;
                        obj1.montoCuota = data.mp_cuotas;
                        obj1.montoCuotaDescuento = TipoDescuentoGeneral(sinDescuento / tamanioCuotas, data.tp_porcentaje_cuotas);
                        obj.es_matricula = false;
                        obj1.fechapago = CalcularFechaInicial(data, i);
                        lista.Add(obj1);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Tipo de Descuento
        /// </summary>
        /// <param name="va"> Precio matricula </param>
        /// <param name="des"> descuento </param>
        /// <returns> Tipo de descuento general : float </returns>
        private float TipoDescuentoGeneral(double? va, int des)
        {
            float valor = float.Parse(va.ToString());
            float des2 = float.Parse(des.ToString());
            var descuento = float.Parse(Convert.ToString(valor * des2 / 100));
            return (valor - descuento);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Calcula FechaInicial de la Primera Cuota
        /// </summary>
        /// <param name="obj"> Datos Compuestos de MontoPago</param>
        /// <param name="agregarMeses"> Cantidad de meses agregados </param>
        /// <returns> DateTime </returns>
        private DateTime CalcularFechaInicial(MontoPagoCompuestoDTO obj, int agregarMeses)
        {
            var myDate = new DateTime();
            myDate = DateTime.Now;
            // myDate = DateTime.Now;
            var mes = myDate.Month;
            int dia = 0;
            if (!string.IsNullOrEmpty(obj.mp_vencimiento))
            {
                dia = Int32.Parse(obj.mp_vencimiento);
            }
            else
            {
                myDate.AddDays(1);
            }
            if (obj.mp_primeraCuota != null)
            {
                DateTime fec_temp = new DateTime();
                fec_temp = DateTime.Now;
                myDate = ObtenerPrimeraFecha(obj.mp_primeraCuota, dia);
            }
            if (dia < 29)
            {
                myDate = myDate.AddDays(dia);
            }
            else
            {
                myDate = myDate.AddDays(28);
            }
            if (obj.mp_vencimiento != null)
            {
                mes = myDate.Month;
            }
            myDate = myDate.AddMonths(agregarMeses);
            return myDate;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene primera Fecha
        /// </summary>
        /// <param name="montName"> Monto Pago primera Cuota </param>
        /// <param name="diaInicio"> Día de inicio</param>
        /// <returns> DateTime </returns>
        private DateTime ObtenerPrimeraFecha(string montName, int diaInicio)
        {
            int tmp = 0;
            DateTime res = new DateTime();
            string[] ssize = montName.Split(new char[0]);
            string[] monthNames = (new System.Globalization.CultureInfo("en-US")).DateTimeFormat.MonthNames;
            for (int i = 0; i <= monthNames.Count() - 1; i++)
            {
                if (!ssize[0].Equals(monthNames[i])) continue;
                tmp = i;
                string tmpp;
                string tmppdia;
                if (tmp < 10)
                {
                    tmp++;
                    tmpp = "0" + tmp;
                    tmppdia = diaInicio < 10 ? "0" + diaInicio : diaInicio.ToString();
                }
                else
                {
                    tmp++;
                    tmpp = tmp.ToString();
                    tmppdia = diaInicio < 10 ? "0" + diaInicio : diaInicio.ToString();
                }
                string validFec = ssize[1] + tmpp + tmppdia;
                res = DateTime.ParseExact(validFec, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            return res;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Tabla de Cuotas de Pago de un Programa
        /// </summary>
        /// <param name="data"> Dato Compuesto de monto Pago </param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> GenerarGridGeneral(MontoPagoCompuestoDTO data)
        {
            string test = "";
            var tamanio = 0;
            var tamanioContador = 0;
            tamanio = data.mp_nro_cuotas;
            var numeroco = 1;
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            PagoCuotaDTO obj = new PagoCuotaDTO();
            obj.numeroCuota = numeroco;
            obj.cuotaDescripcion = "Matricula ";
            obj.montoCuota = data.mp_matricula;
            DateTime fpag = DateTime.Now;
            fpag = fpag.AddMonths(1);
            obj.fechapago = fpag;
            obj.montoCuotaDescuento = TipoDescuentoGeneral(data.mp_matricula, data.tp_porcentaje_general);
            obj.ispagado = false;
            obj.es_matricula = true;
            lista.Add(obj);
            for (var i = 0; i < tamanio; i++)
            {
                PagoCuotaDTO obj1 = new PagoCuotaDTO();
                numeroco = numeroco + 1;
                tamanioContador = tamanioContador + 1;
                obj1.numeroCuota = numeroco;
                obj1.cuotaDescripcion = "Cuota - " + (numeroco - 1);
                obj1.montoCuota = data.mp_cuotas;
                obj1.montoCuotaDescuento = TipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_general);
                obj1.ispagado = false;
                obj1.es_matricula = false;
                DateTime fecha = CalcularFechaInicial(data, i);
                obj1.fechapago = fecha;
                lista.Add(obj1);
                if (tamanioContador != tamanio)
                {
                    var mes = fecha.Month;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        PagoCuotaDTO obj2 = new PagoCuotaDTO();
                        numeroco = numeroco + 1;
                        obj2.numeroCuota = numeroco;
                        obj2.cuotaDescripcion = "Cuota - " + (numeroco - 1);
                        obj2.montoCuota = data.mp_cuotas;
                        obj2.montoCuotaDescuento = TipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_general);
                        obj2.es_matricula = false;
                        obj2.fechapago = CalcularFechaInicial(data, i);
                        lista.Add(obj2);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera grid normal
        /// </summary>
        /// <param name="data"> Dato Compuesto de monto pago </param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> GenerarGridNormal(MontoPagoCompuestoDTO data)
        {
            string simbolo = string.Empty;
            MonedaService monedaService = new MonedaService(_unitOfWork);
            var respuesta = monedaService.ObtenerMonedaPorId(data.mp_moneda);

            if (respuesta != null)
            {
                simbolo = respuesta.Simbolo;
            }
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            PagoCuotaDTO obj = new PagoCuotaDTO
            {
                numeroCuota = 1,
                cuotaDescripcion = "Contado",
                montoCuota = data.mp_cuotas
            };
            DateTime fpag = DateTime.Now;
            obj.fechapago = fpag;
            obj.montoCuotaDescuento = float.Parse(data.mp_cuotas.ToString());
            obj.ispagado = false;
            obj.es_matricula = true;
            obj.SimboloMoneda = simbolo;
            lista.Add(obj);
            return lista;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera una tabla para Precio en Cuotas de un Programa
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> string </returns>
        private string ObtenerPrecioCuotas(int idOportunidad)
        {
            string respuesta = "";
            MontoPagoService montoPagoService = new MontoPagoService(_unitOfWork);
            var montopagoCuotas = montoPagoService.ObtenerMontoPagoPorIdOportunidadSP(idOportunidad);
            if (montopagoCuotas != null) respuesta = GenerateGridCronogamaPagos(montopagoCuotas);

            return respuesta;
        }
    }
}
