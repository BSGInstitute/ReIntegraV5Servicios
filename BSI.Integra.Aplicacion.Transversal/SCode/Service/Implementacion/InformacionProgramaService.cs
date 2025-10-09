using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OportunidadInformacionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 23/07/2022
    /// <summary>
    /// Gestión general de Informacion de Oportunidades
    /// </summary>
    public class InformacionProgramaService : IInformacionProgramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public InformacionProgramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, Entity>(MemberList.None).ReverseMap());
            //_mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Cargar Informacion Programa
        /// </summary>
        /// <param name="idCentroCosto">Id de Centro Costo</param>
        /// <returns> void </returns>
        public CargarInformacionProgramaAutomaticoRespuestaDTO CargarInformacionProgramaAutomatico(int idCentroCosto, int codigoPais, int idMatriculaCabecera, int idOportunidad)
        {
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var data = servicioPGeneral.ObtenerPGeneralPEspecificoPorCentroCosto(idCentroCosto);
            CargarInformacionProgramaRespuestaDTO informacionPrograma = null;
            List<ResumenProgramaV2DTO> resumenProgramasV2 = null;

            var idPGeneral = data != null ? data.IdProgramaGeneral : 0;
            var informacionProgramaAutomatico = new CargarInformacionProgramaAutomaticoRespuestaDTO();

            if (idPGeneral != 0)
            {
                informacionPrograma = CargarInformacionPrograma(idPGeneral, codigoPais, idMatriculaCabecera, idOportunidad);
            }
            if (data != null && data.IdArea != 0 && data.IdSubArea != 0)
            {
                var filtros = new Dictionary<string, string>
                {
                    { "idArea", data.IdArea.ToString() },
                    { "idSubArea", data.IdSubArea.ToString() },
                    { "codigoPais", codigoPais.ToString() }
                };
                resumenProgramasV2 = CargarResumenProgramasV2(filtros);

                informacionProgramaAutomatico.IdPGeneral = idPGeneral;
                informacionProgramaAutomatico.InformacionPrograma = informacionPrograma.InformacionPrograma;
                informacionProgramaAutomatico.ResumenProgramasV2 = resumenProgramasV2;
                informacionProgramaAutomatico.EtiquetaDuracionHorarios = informacionPrograma.EtiquetaDuracionHorarios;
                informacionProgramaAutomatico.EtiquetaExpositores = informacionPrograma.EtiquetaExpositores;
                informacionProgramaAutomatico.EtiquetaBeneficiosInversion = informacionPrograma.EtiquetaBeneficiosInversion;
                informacionProgramaAutomatico.EtiquetaFormasPago = informacionPrograma.EtiquetaFormasPago;
                informacionProgramaAutomatico.EtiquetaTarifarios = informacionPrograma.EtiquetaTarifarios;
                informacionProgramaAutomatico.ListaBeneficios = informacionPrograma.ListaBeneficios;
            }
            return informacionProgramaAutomatico;
        }



        public CargarInformacionProgramaEndpointsDTO ObtenerInformacionPrograma(int idCentroCosto, int codigoPais, int idMatriculaCabecera, int idOportunidad)
        {
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var data = servicioPGeneral.ObtenerPGeneralPEspecificoPorCentroCosto(idCentroCosto);
            CargarInformacionProgramaEndpointsDTO informacionPrograma = null;
            List<ResumenProgramaV2DTO> resumenProgramasV2 = null;

            var idPGeneral = data != null ? data.IdProgramaGeneral : 0;
            var informacionProgramaAutomatico = new CargarInformacionProgramaAutomaticoRespuestaDTO();

            if (idPGeneral != 0)
            {
     informacionPrograma = CargarInformacionProgramaSinHTML(idPGeneral,codigoPais,idMatriculaCabecera,idOportunidad);
            }
            return informacionPrograma;
        }



        public CargarInformacionProgramaEndpointsDTO CargarInformacionProgramaSinHTML(int idPGeneral,int codigoPais,int idMatriculaCabecera, int idOportunidad)
        {
            // Servicios
            var servicioConfiguracionBeneficio = new ConfiguracionBeneficioProgramaGeneralService(_unitOfWork);
            var servicioDocumentoAgenda = new DocumentoAgendaService(_unitOfWork);
            IPEspecificoService servicioPEspecifico = new PEspecificoService(_unitOfWork);
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var servicioOrigen = new OrigenService(_unitOfWork);

            // 1) Configuración de beneficios / introducciones
            var beneficiosV2 = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository
                .ObtenerPGeneralConfiguracionBeneficios(idPGeneral)
                ?? new List<PgeneralConfiguracionBeneficioDTO>();

            var inversion = ObtenerMontos2(idPGeneral, codigoPais);

            var introducciones = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository
                .ObtenerIntroduccionBeneficio(idPGeneral);
               

            // 2) Flags de programa (padre/técnico)
            bool esPadre = _unitOfWork.PGeneralRepository.ProgramaGeneralPadre(idPGeneral);
            bool esTecnico = _unitOfWork.PGeneralRepository.ProgramaGeneralEsTecnico(idPGeneral);

            // 3) Estructura técnica (raw por hijo)
            var cursosHijo = _unitOfWork.PGeneralRepository
                .ListaCursosHijoPorIdPGeneral(idPGeneral)
                ?? new List<ListaCursosPorProgramaDTO>();

            var duracionPorHijo = new Dictionary<int, object>();
            var contenidosPorHijo = new Dictionary<int, List<ContenidoHijoDTO>>();

            foreach (var item in cursosHijo)
            {
                var idHijo = item.IdHijo;
                var dur = _unitOfWork.PGeneralRepository.ObtenerDuracionCursoHijo(idHijo); // si conoces el tipo exacto, typarlo
                duracionPorHijo[idHijo] = dur;

                var cont = _unitOfWork.PGeneralRepository.ContenidoEstructuraHijoPadre(idHijo)
                    ?? new List<ContenidoHijoDTO>();
                contenidosPorHijo[idHijo] = cont;
            }

            // 4) Secciones de programa (raw, sin HTML)
            var seccionesPrograma = servicioDocumentoAgenda.ObtenerInformacionProgramaGeneral(idPGeneral)
                ?? new List<ProgramaGeneralSeccionDocumentoDTO>();

            // 5) PEspecífico (fechas/modalidades) y atributos del PGeneral (raw)
            var fechasInicioPrograma = servicioPEspecifico.ObtenerFechaInicioProgramaTodos(idPGeneral)
                ?? new List<PEspecificoPorIdPGeneral>();

            List<ModalidadProgramaDTO> modalidadGeneral = new List<ModalidadProgramaDTO>();


            var programaGeneral = servicioPGeneral.ObtenerPGeneralAtributosPrincipalesPorId(idPGeneral);
            var modalidadGeneralPortal = servicioPEspecifico.ObtenerFechaInicioProgramaTodos(idPGeneral);
            if (modalidadGeneralPortal == null)
            {
                modalidadGeneralPortal = new List<PEspecificoPorIdPGeneral>();
            }
            ModalidadProgramaDTO cambiarModelo;

            //Obtencion de Modalidades V2
            if (modalidadGeneralPortal != null)
            {
                foreach (var modalidadPortal in modalidadGeneralPortal)
                {
                    cambiarModelo = new ModalidadProgramaDTO()
                    {
                        Tipo = modalidadPortal.Tipo,
                        Ciudad = modalidadPortal.Ciudad,
                        TipoCiudad = "",
                        FechaHoraInicio = modalidadPortal.FechaInicioTexto,
                        NombrePG = programaGeneral.Nombre,
                        IdPEspecifico = modalidadPortal.Id,
                        NombreESP = modalidadPortal.Nombre,
                        NombreCentroCosto = modalidadPortal.CentroCosto,
                        Duracion = modalidadPortal.Duracion,
                        Pw_duracion = programaGeneral.PwDuracion,
                        FechaReal = modalidadPortal.FechaInicio
                    };
                    modalidadGeneral.Add(cambiarModelo);
                }
            }

            var modalidadAsincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Asincronica") && s.Ciudad.Equals("LIMA")).ToList();
            if (modalidadAsincronica.Count() == 0)
            {
                modalidadAsincronica = modalidadGeneral.Where(s => LimpiarCadena(s.Tipo).Equals("Online Asincronica")).ToList();
            }
            var modalidadSincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Sincronica")).ToList();
            var modalidadPresencial = modalidadGeneral.Where(s => LimpiarCadena(s.Tipo).Equals("Presencial")).ToList();
            List<ModalidadProgramaDTO> PruebaModalidad = new List<ModalidadProgramaDTO>();
            PruebaModalidad.AddRange(modalidadAsincronica);
            PruebaModalidad.AddRange(modalidadSincronica);
            PruebaModalidad.AddRange(modalidadPresencial);

            var modalidades = new List<ModalidadProgramaDTO>();
            modalidades.AddRange(PruebaModalidad);



            // 6) Área/Subárea y Área capacitación (raw)
            var areaSubArea = _unitOfWork.PGeneralRepository.ObtenerAreaSubAreaPorIdPGeneral(idPGeneral);
            AreaCapacitacion areaCapacitacion = null;
            if (areaSubArea != null)
            {
                var idArea = areaSubArea.IdArea; // ya tipado en PGeneralAreaSubAreaDTO
                areaCapacitacion = _unitOfWork.AreaCapacitacionRepository.ObtenerPorId(idArea);
            }

            // 7) Monto pagado (raw)
            var montosPagados = _unitOfWork.MontoPagoCronogramaRepository
                .ObtenerMontoPagado(idMatriculaCabecera, idOportunidad)
                ?? new List<MontoPagadoDTO>();

            // 8) Tarifarios (raw)
            var tarifarios = servicioOrigen.ObtenerTarifariosDetallesAgenda(idMatriculaCabecera)
                ?? new List<TarifarioDetalleAgendaDTO>();

            // 9) Beneficios filtrados por país
            var beneficiosFiltradosPorPais = beneficiosV2
                .Where(b => b.Paises != null && b.Paises.Contains(codigoPais))
                .ToList();

            // Ensamblar DTO agregado (SIN HTML)
            var dto = new CargarInformacionProgramaEndpointsDTO
            {
                ConfiguracionBeneficios = beneficiosV2,
                IntroduccionesBeneficio = introducciones,
                Inversion = inversion.MontosPorPais,
                Modalidades = modalidades,

                EsProgramaPadre = esPadre,
                EsProgramaTecnico = esTecnico,

                CursosHijo = cursosHijo,
                DuracionCursoHijoPorId = duracionPorHijo,
                ContenidoEstructuraPorHijoId = contenidosPorHijo,

                SeccionesPrograma = seccionesPrograma,

                FechasInicioPrograma = fechasInicioPrograma,
                ProgramaGeneral = programaGeneral, // PGeneralAtributosPrincipalesDTO

                AreaSubArea = areaSubArea,
                AreaCapacitacion = areaCapacitacion,

                MontoPagado = montosPagados,
                Tarifarios = tarifarios,

                ConfiguracionBeneficiosFiltradosPorPais = beneficiosFiltradosPorPais
            };

            return dto;
        }


        public CargarInformacionProgramaAutomaticoRespuestaDTO CargarInformacionProgramaAutomaticoSpeech(int idCentroCosto, int codigoPais, int idMatriculaCabecera, int idOportunidad)
        {
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var data = servicioPGeneral.ObtenerPGeneralPEspecificoPorCentroCosto(idCentroCosto);
            CargarInformacionProgramaRespuestaDTO informacionPrograma = null;
            List<ResumenProgramaV2DTO> resumenProgramasV2 = null;

            var idPGeneral = data != null ? data.IdProgramaGeneral : 0;
            var informacionProgramaAutomatico = new CargarInformacionProgramaAutomaticoRespuestaDTO();

            if (idPGeneral != 0)
            {
                informacionPrograma = CargarInformacionProgramaSpeech(idPGeneral, codigoPais, idMatriculaCabecera, idOportunidad);
            }
            if (data != null && data.IdArea != 0 && data.IdSubArea != 0)
            {
                var filtros = new Dictionary<string, string>
                {
                    { "idArea", data.IdArea.ToString() },
                    { "idSubArea", data.IdSubArea.ToString() },
                    { "codigoPais", codigoPais.ToString() }
                };
                resumenProgramasV2 = CargarResumenProgramasV2(filtros);

                informacionProgramaAutomatico.IdPGeneral = idPGeneral;
                informacionProgramaAutomatico.InformacionPrograma = informacionPrograma.InformacionPrograma;
                informacionProgramaAutomatico.ResumenProgramasV2 = resumenProgramasV2;
                informacionProgramaAutomatico.EtiquetaDuracionHorarios = informacionPrograma.EtiquetaDuracionHorarios;
                informacionProgramaAutomatico.EtiquetaExpositores = informacionPrograma.EtiquetaExpositores;
                informacionProgramaAutomatico.EtiquetaBeneficiosInversion = informacionPrograma.EtiquetaBeneficiosInversion;
                informacionProgramaAutomatico.EtiquetaFormasPago = informacionPrograma.EtiquetaFormasPago;
                informacionProgramaAutomatico.EtiquetaTarifarios = informacionPrograma.EtiquetaTarifarios;
                informacionProgramaAutomatico.ListaBeneficios = informacionPrograma.ListaBeneficios;
            }
            return informacionProgramaAutomatico;
        }






        public InformacionProgramaSpeechDTO CargarInformacionProgramaAutomaticoSpeechV2(int idCentroCosto, int codigoPais, int idMatriculaCabecera, int idOportunidad)
        {
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var sericioDocumentoAgendaService = new DocumentoAgendaService(_unitOfWork);
            IPEspecificoService servicioPEspecifico = new PEspecificoService(_unitOfWork);
            var data = servicioPGeneral.ObtenerPGeneralPEspecificoPorCentroCosto(idCentroCosto);
            CargarInformacionProgramaRespuestaDTO informacionPrograma = null;
            List<ResumenProgramaV2DTO> resumenProgramasV2 = null;
            var idPGeneral = data != null ? data.IdProgramaGeneral : 0;
            var informacionProgramaAutomatico = new CargarInformacionProgramaAutomaticoRespuestaDTO();
            List<PresentacionProgramadto> secciones = _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.ObtenerProgramaGeneralPresentacionArgumento(idPGeneral);
            List<RegistroListaSeccionesDocumentoDTO>  seccionV1 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral);
            List<PresentacionProgramadto> refuerzoConfianza = secciones.Where(s => string.Equals(s.Titulo?.Trim(), "Refuerzo de la confianza", StringComparison.OrdinalIgnoreCase)).ToList();
            List<PresentacionProgramadto> limitaciones = secciones.Where(s => string.Equals(s.Titulo?.Trim(), "Limitaciones", StringComparison.OrdinalIgnoreCase)).ToList();
            List<PresentacionProgramadto> demostracióndevalor = secciones.Where(s => string.Equals(s.Titulo?.Trim(), "Demostración de valor", StringComparison.OrdinalIgnoreCase)).ToList();
            List<PresentacionProgramadto> aspectosdiferenciadores = secciones.Where(s => string.Equals(s.Titulo?.Trim(), "Aspectos diferenciadores", StringComparison.OrdinalIgnoreCase)).ToList();
            List<PresentacionProgramadto> garantiadeprograma = secciones.Where(s => string.Equals(s.Titulo?.Trim(), "Garantia de programa", StringComparison.OrdinalIgnoreCase)).ToList();
            List<RegistroListaSeccionesDocumentoDTO> objetivos = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV2Objetivos(idPGeneral);
            ObtenerMontos2RespuestaDTO montosBeneficios = ObtenerMontoPresentacionPrograma(idPGeneral, codigoPais);
            List<PEspecificoPorIdPGeneral> modalidad = servicioPEspecifico.ObtenerFechaInicioProgramaTodos(idPGeneral);
            List<RegistroListaSeccionesDocumentoDTO>  horario = seccionV1.Where(s => {
                var t = (s.Titulo ?? "").Trim();
                return t.Equals("Duración y Horarios", StringComparison.OrdinalIgnoreCase)
                    || t.Equals("Duracion y Horarios", StringComparison.OrdinalIgnoreCase);
            })
               .ToList();
            List<RegistroListaSeccionesDocumentoDTO> publicoObjetivo = seccionV1.Where(s => string.Equals(s.Titulo?.Trim(), "Público Objetivo", StringComparison.OrdinalIgnoreCase)).ToList();
            List<RegistroListaSeccionesDocumentoDTO> metodologias = seccionV1.Where(s => string.Equals(s.Titulo?.Trim(), "Metodología Online De Este programa", StringComparison.OrdinalIgnoreCase)).ToList();
            List<RegistroListaSeccionesDocumentoDTO> presentacion = seccionV1.Where(s => string.Equals(s.Titulo?.Trim(), "Presentación", StringComparison.OrdinalIgnoreCase)).ToList();
            List<ProgramaGeneralSeccionDocumentoDTO> listaadicionales = sericioDocumentoAgendaService.ObtenerListaSeccionDocumentoProgramaGeneralSpeech(idPGeneral);
            List<ProgramaExpositoresDTO> expositores = _unitOfWork.DocumentoSeccionPwRepository.ObtenerExpositoresPorIdGeneral(idPGeneral);
            InformacionProgramaSpeechDTO resultado = new InformacionProgramaSpeechDTO
            {
                RefuerzodeConfianza = refuerzoConfianza,
                Limitaciones = limitaciones,
                Demostracióndevalor = demostracióndevalor,
                Aspectosdiferenciadores = aspectosdiferenciadores,
                Garantiadeprograma = garantiadeprograma,
                Modalidad = modalidad,
                Objetivos = objetivos,
                Montos = montosBeneficios,
                DuracionHorario = horario,
                PublicoObjetivo = publicoObjetivo,
                Metodologia = metodologias,
                Expositores = expositores,
                Presentacion= presentacion,
                DatosAdicionales = listaadicionales
                
            };

            return resultado;
        }

        /// Autor: Juan Diego Huanaco Quispe.
        /// Fecha: 24/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de los programas por CodigoPais, solo incluye aquellos con la categoria de Programa y Curso
        /// </summary>
        /// <param name="codigoPais">Codigo de Pais</param>
        /// <returns> List ProgramasPorCodigoPaisComboDTO </returns>
        public List<ProgramasPorCodigoPaisComboDTO> ObtenerProgramasPorCodigoPais(int codigoPais)
        {
            var programasYCursos = _unitOfWork.PGeneralRepository.ObtenerProgramasPorCodigoPais(codigoPais);
            return programasYCursos;
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion del programa 
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <param name="codigoPais">Codigo de Pais</param>
        /// <param name="idMatriculaCabecera">Id de Matricula Cabecera</param>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns> CargarInformacionProgramaRespuestaDTO </returns>
        public CargarInformacionProgramaRespuestaDTO CargarInformacionPrograma(int idPGeneral, int codigoPais, int idMatriculaCabecera, int idOportunidad)
        {
            var servicioConfiguracionBeneficio = new ConfiguracionBeneficioProgramaGeneralService(_unitOfWork);
            var servicioDocumentoAgenda = new DocumentoAgendaService(_unitOfWork);
            IPEspecificoService servicioPEspecifico = new PEspecificoService(_unitOfWork);
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var servicioOrigen = new OrigenService(_unitOfWork);

            var beneficiosV2 = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerPGeneralConfiguracionBeneficios(idPGeneral);
            var introduccionVersionBeneficio = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerIntroduccionBeneficio(idPGeneral);
            var beneficios = beneficiosV2.Where(x => x.Paises.Any(y => y == codigoPais)).ToList();

            //nueva logica
            var ProgramaPadre = _unitOfWork.PGeneralRepository.ProgramaGeneralPadre(idPGeneral);
            var programaTecnico = _unitOfWork.PGeneralRepository.ProgramaGeneralEsTecnico(idPGeneral);

            string contenidoStructuraTecnico = "";
            if (programaTecnico)
            {
                if (ProgramaPadre)
                {
                    var listaCursosHijo = _unitOfWork.PGeneralRepository.ListaCursosHijoPorIdPGeneral(idPGeneral);
                    foreach (var item in listaCursosHijo)
                    {

                        contenidoStructuraTecnico += "<h5><strong>" + item.Curso + "</strong></h5>";
                        var duracionCurso = _unitOfWork.PGeneralRepository.ObtenerDuracionCursoHijo(item.IdHijo);
                        var CursosHijo = _unitOfWork.PGeneralRepository.ContenidoEstructuraHijoPadre(item.IdHijo);

                        contenidoStructuraTecnico += "<ul type='disc'>";
                        var listaContenidoCurso = CursosHijo.GroupBy(x => x.Contenido).Select(x => x.First()).ToList();
                        foreach (var contenidoCurso in listaContenidoCurso)
                        {
                            contenidoStructuraTecnico += "<li>&nbsp;&nbsp;&nbsp;" + contenidoCurso.Contenido + "</li>";
                        }
                        contenidoStructuraTecnico += "</ul>";
                    }
                }
            }
            var estructuraTecnico = contenidoStructuraTecnico;

            var lista = servicioDocumentoAgenda.ObtenerInformacionProgramaGeneral(idPGeneral);
            var listaPiePagina = lista.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("beneficios")).FirstOrDefault();
            var addPiePagina = "";
            if (listaPiePagina != null)
            {
                addPiePagina = "<p>" + listaPiePagina.DetalleSeccion[0].PiePagina + "</p>";
                lista.Remove(listaPiePagina);
            }
            //Obtiene HTML y adapta los datos
            var seccionesV2Ordenado = servicioDocumentoAgenda.GenerarHTMLProgramaGeneralDocumentoSeccion(lista);
            foreach (var item in seccionesV2Ordenado)
            {
                string temporal = Regex.Replace(item.Contenido, "&bull;", "");
                string temporal2 = Regex.Replace(temporal, "&nbsp;", "");

                if (LimpiarCadena(item.Seccion).ToLower() != "estructura curricular" && LimpiarCadena(item.Seccion).ToLower() != "beneficios" && LimpiarCadena(item.Seccion).ToLower() != "certificacion" && LimpiarCadena(item.Seccion).ToLower() != "prerrequisitos" && LimpiarCadena(item.Seccion).ToLower() != "expositores")
                {
                    string temp3 = Regex.Replace(temporal2, "<br />", "<br /></li>");
                    string temp4 = Regex.Replace(temp3, "<br/>", "<br /></li>");
                    string temp5 = Regex.Replace(temp4, "<ul type='disc'><li>", "");

                    if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                    {
                        string temp6 = Regex.Replace(temp5, "<h5><strong><b>" + item.Seccion + "</b></strong></h5>", "");
                        item.Contenido = temp6;
                    }

                }
                else
                {
                    if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                    {
                        string temp3 = Regex.Replace(temporal2, "<br />", "<br /></li>");
                        string temp4 = Regex.Replace(temp3, "<br/>", "<br /></li>");
                        string temp5 = Regex.Replace(temp4, "<ul type='disc'><li>", "");

                        if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                        {
                            string temp6 = Regex.Replace(temp5, "<h5><strong><b>" + item.Seccion + "</b></strong></h5>", "");
                            item.Contenido = temp6;
                        }
                    }
                    else
                    {
                        string temp3 = Regex.Replace(temporal2, "<h5><strong><b>", "<h6>");
                        string temp4 = Regex.Replace(temp3, "</b></strong></h5>", "</h6>");

                        item.Contenido = temp4;
                    }

                }
                if (item.Contenido == null || item.Contenido == "")
                {
                    item.Seccion = "";
                }
            }

            var estructura = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("estructura curricular")).FirstOrDefault();
            var certificacion = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("certificacion")).FirstOrDefault();

            //Seccion Descripcion Estructura Curricular y Certificacion
            var concatenarEstructura = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("descripcion estructura")).FirstOrDefault();
            //var concatenarCertificacion = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("descripcion certificacion")).FirstOrDefault();
            if (concatenarEstructura != null && estructura != null)
            {
                estructura.Contenido = estructura.Contenido + concatenarEstructura.Contenido;
                seccionesV2Ordenado.Remove(concatenarEstructura);
            }
            //SE COMENTA ESTA PARTE PORQUE SE ESTABA MOSTRANDO DUPLICADO LA CERTIFICACION EN LOS CORREOS E INFORMACION DEL CURSO EN LA AGENDA, YA EN CERTIFICACION TRAE EL CONTENIDO DE DESCRIPCION DE CERTIFICACION
            //if (concatenarCertificacion != null && certificacion != null)
            //{
            //    certificacion.Contenido = certificacion.Contenido + concatenarCertificacion.Contenido;
            //    seccionesV2Ordenado.Remove(concatenarCertificacion);
            //}

            // Elimina Data de Video innecesarios
            var deleteVideos = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("video") || LimpiarCadena(x.Seccion).ToLower().Equals("vista previa") || LimpiarCadena(x.Seccion.ToLower()).Equals("video de presentacion")).ToList();
            if (deleteVideos != null)
            {
                foreach (var item in deleteVideos)
                {
                    seccionesV2Ordenado.Remove(item);
                }
            }

            //Ordena data para hacer el display de forma predeterminada
            List<ProgramaGeneralSeccionAnexosHTMLDTO> seccionesV2 = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
            string[] listaTituloV1Orden = { "presentación", "objetivos", "público objetivo", "pre-requisitos", "estructura curricular", "duración y horarios", "certificacion", "expositores", "metodología online de este programa", "material del curso", "pautas complementarias", "bibliografía", "modalidad", "inversion", "perfil del egresado", "mercado laboral", "metodologia del programa" };
            string[] listaTituloV2Orden = { "presentacion", "objetivos", "publico objetivo", "prerrequisitos", "estructura curricular", "duracion y horarios", "certificación", "expositores", "metodolog&#237;a online de este programa", "material del curso", "pautas complementarias", "bibliografia", "Modalidad", "Inversion", "Perfil del egresado", "Mercado laboral", "Metodolog&#237;a del programa" };

            for (var i = 0; i < listaTituloV1Orden.Length; i++)
            {
                var ordenarTempV2 = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena(listaTituloV2Orden[i])).FirstOrDefault();
                if (ordenarTempV2 != null)
                {
                    seccionesV2.Add(ordenarTempV2);
                }
                else
                {
                    var ordenarTempV1 = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena(listaTituloV1Orden[i])).FirstOrDefault();
                    if (ordenarTempV1 != null)
                    {
                        seccionesV2.Add(ordenarTempV1);
                    }
                }
            }

            // VALIDACION Y CREACION DE Items en Objeto para que no caiga en NULL
            var secciones = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
            ProgramaGeneralSeccionAnexosHTMLDTO modalidadAdd = new ProgramaGeneralSeccionAnexosHTMLDTO()
            {
                Seccion = "Modalidades",
                Contenido = "",
            };
            secciones.Add(modalidadAdd);
            var flagBeneficio = false;
            var flagInversion = false;
            foreach (var item in seccionesV2)
            {
                if (item.Seccion == "Inversion")
                {
                    flagInversion = true;
                }
                if (item.Seccion == "Beneficios")
                {
                    flagBeneficio = true;
                }
                if (item.Seccion == "Inversión")
                {
                    item.Seccion = "Inversion";
                    flagInversion = true;
                }

            }
            if (flagInversion == false)
            {
                ProgramaGeneralSeccionAnexosHTMLDTO inversionAdd = new ProgramaGeneralSeccionAnexosHTMLDTO()
                {
                    Seccion = "Inversion",
                    Contenido = "",
                };
                secciones.Add(inversionAdd);
            }
            if (flagBeneficio == false)
            {
                ProgramaGeneralSeccionAnexosHTMLDTO beneficiosAdd = new ProgramaGeneralSeccionAnexosHTMLDTO()
                {
                    Seccion = "Beneficios",
                    Contenido = "",
                };
                secciones.Add(beneficiosAdd);
            }
            secciones.AddRange(seccionesV2);

            //Quito a Cetpro los expositores
            var programaGeneralCetpro = _unitOfWork.PGeneralRepository.ObtenerAreaSubAreaPorIdPGeneral(idPGeneral);
            var areaCapacitacionCetpro = _unitOfWork.AreaCapacitacionRepository.ObtenerPorId(programaGeneralCetpro.IdArea);
            if (areaCapacitacionCetpro.Nombre == "CETPRO")
            {
                var seccionExpositor = secciones.Where(w => w.Seccion == "Expositores").FirstOrDefault();
                if (seccionExpositor != null)
                {
                    secciones.Remove(seccionExpositor);
                }
            }

            //Logica de Montos de V1
            var seccionesBeneficiosInversion = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();

            var montosBeneficios = ObtenerMontos2(idPGeneral, codigoPais);
            var montos = montosBeneficios.MontosPorPais;

            var contador = 0;
            if (montos.Count() < 1)
            {
                montosBeneficios = ObtenerMontos(idPGeneral, codigoPais);
                montos = montosBeneficios.MontosPorPais;
            }

            foreach (var item in montos)
            {
                if (item.Beneficios == "<ul></ul><br>" || item.Beneficios == null || item.Beneficios == "null")
                {
                    contador++;
                }
            }
            if (contador == montos.Count())
            {
                montosBeneficios = ObtenerMontos(idPGeneral, codigoPais);
                montos = montosBeneficios.MontosPorPais;
            }
            var montosb = montos.Where(s => s.Beneficios != null).ToArray();
            var montopagado = _unitOfWork.MontoPagoCronogramaRepository.ObtenerMontoPagado(idMatriculaCabecera, idOportunidad);

            //Obtencion de Modalidades VPortal
            ModalidadProgramaDTO cambiarModelo;
            List<ModalidadProgramaDTO> modalidadGeneral = new List<ModalidadProgramaDTO>();
            var modalidadGeneralPortal = servicioPEspecifico.ObtenerFechaInicioProgramaTodos(idPGeneral);
            if (modalidadGeneralPortal == null)
            {
                modalidadGeneralPortal = new List<PEspecificoPorIdPGeneral>();
            }
            var programaGeneral = servicioPGeneral.ObtenerPGeneralAtributosPrincipalesPorId(idPGeneral);
            //Obtencion de Modalidades V2
            if (modalidadGeneralPortal != null)
            {
                foreach (var modalidadPortal in modalidadGeneralPortal)
                {
                    cambiarModelo = new ModalidadProgramaDTO()
                    {
                        Tipo = modalidadPortal.Tipo,
                        Ciudad = modalidadPortal.Ciudad,
                        TipoCiudad = "",
                        FechaHoraInicio = modalidadPortal.FechaInicioTexto,
                        NombrePG = programaGeneral.Nombre,
                        IdPEspecifico = modalidadPortal.Id,
                        NombreESP = modalidadPortal.Nombre,
                        NombreCentroCosto = modalidadPortal.CentroCosto,
                        Duracion = modalidadPortal.Duracion,
                        Pw_duracion = programaGeneral.PwDuracion,
                        FechaReal = modalidadPortal.FechaInicio
                    };
                    modalidadGeneral.Add(cambiarModelo);
                }
            }

            var modalidadAsincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Asincronica") && s.Ciudad.Equals("LIMA")).ToList();
            if (modalidadAsincronica.Count() == 0)
            {
                modalidadAsincronica = modalidadGeneral.Where(s => LimpiarCadena(s.Tipo).Equals("Online Asincronica")).ToList();
            }
            var modalidadSincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Sincronica")).ToList();
            var modalidadPresencial = modalidadGeneral.Where(s => LimpiarCadena(s.Tipo).Equals("Presencial")).ToList();
            List<ModalidadProgramaDTO> PruebaModalidad = new List<ModalidadProgramaDTO>();
            PruebaModalidad.AddRange(modalidadAsincronica);
            PruebaModalidad.AddRange(modalidadSincronica);
            PruebaModalidad.AddRange(modalidadPresencial);

            var modalidades = new List<ModalidadProgramaDTO>();
            modalidades.AddRange(PruebaModalidad);

            var modalidadesV2 = new List<ModalidadProgramaSincronicoDTO>();

            var tarifarios = servicioOrigen.ObtenerTarifariosDetallesAgenda(idMatriculaCabecera);

            var seccionMontos = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("inversion")).FirstOrDefault();
            var seccionExpositores = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("expositores")).FirstOrDefault();

            if (seccionMontos != null)
            {
                if (montos.Count() > 0)
                {
                    seccionMontos.Contenido = "<table class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Precio Contado</strong></td><td><strong>Precio Credito</strong></td></tr>"
                        + string.Join("", montos.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.InversionContado + "</td><td>" + s.InversionCredito + "</td></tr>").ToArray()) + "</table>";

                    seccionesBeneficiosInversion.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
                    {
                        Seccion = seccionMontos.Seccion,
                        Contenido = "<table id=\"tablebeneficios\" class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Precio Contado</strong></td><td><strong>Precio Credito</strong></td></tr>"
                        + string.Join("", montos.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.InversionContado + "</td><td>" + s.InversionCredito + "</td></tr>").ToArray()) + "</table>"
                    });
                }
                else
                {
                    var itemToRemove = secciones.Single(r => LimpiarCadena(r.Seccion) == "Inversion");
                    secciones.Remove(itemToRemove);
                }
            }



            var seccionBeneficios = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("beneficios")).FirstOrDefault();
            string inicio = "<table class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Beneficios</strong></td></tr>";
            string fila = "";
            string descripcionSinVersion = "";
            string descripcionBasica = "";
            string descripcionProfesional = "";
            string descripcionGerencial = "";
            bool flagSinVersion = false;
            bool flagVersionBasica = false;
            bool flagVersionProfesional = false;
            bool flagVersionGerencial = false;
            bool flagIntroduccionBasica = false;
            bool flagIntroduccionProfesional = false;
            bool flagIntroduccionGerencial = false;
            if (beneficios != null)
            {
                foreach (var item in beneficios)
                {
                    foreach (var item2 in item.Versiones)
                    {

                        if (item2 == 0 || item2 == 4) // SIN VERSION
                        {
                            flagSinVersion = true;
                            descripcionSinVersion += "<li class='beneficioDetallePrograma' onclick='MostrarInformacionAdicionalBeneficio(" + item.IdBeneficio + "," + item.IdPGeneral + ")'>" + item.Descripcion + "</li>";
                        }
                        if (item2 == 1) // BASICA
                        {
                            flagVersionBasica = true;
                            descripcionBasica += "<li class='beneficioDetallePrograma' onclick='MostrarInformacionAdicionalBeneficio(" + item.IdBeneficio + "," + item.IdPGeneral + ")'>" + item.Descripcion + "</li>";
                            break;
                        }
                        if (item2 == 2) // PROFESIONAL
                        {
                            flagVersionProfesional = true;
                            descripcionProfesional += "<li class='beneficioDetallePrograma' onclick='MostrarInformacionAdicionalBeneficio(" + item.IdBeneficio + "," + item.IdPGeneral + ")'>" + item.Descripcion + "</li>";
                            break;
                        }
                        if (item2 == 3) // GERENCIAL
                        {
                            flagVersionGerencial = true;
                            descripcionGerencial += "<li class='beneficioDetallePrograma' onclick='MostrarInformacionAdicionalBeneficio(" + item.IdBeneficio + "," + item.IdPGeneral + ")'>" + item.Descripcion + "</li>";

                        }
                    }   
                }
                if (flagSinVersion == true)
                {
                    fila += "<tr><td>Sin Versión</td><td>" + descripcionSinVersion + "</td></tr>";
                }
                if (flagVersionBasica == true)
                {
                    if (introduccionVersionBeneficio != null && introduccionVersionBeneficio.Count()>0)
                    {
                        var introduccion1 = introduccionVersionBeneficio.Where(x => x.IdVersionPrograma == 1).FirstOrDefault().Introduccion;
                        fila += "<tr> <td><strong>Básica</strong></td><td>" + "<h3 style='margin-top: -3px; margin-bottom: 5px;'><strong>" + introduccion1 + "</strong></h3>" + descripcionBasica + " </td></tr>";
                    }   
                    else
                    {
                        fila += "<tr> <td><strong>Básica</strong></td><td>" + descripcionBasica + " </td></tr>";
                    }
                }
                if (flagVersionProfesional == true)
                {
                    if (introduccionVersionBeneficio != null && introduccionVersionBeneficio.Count() > 0)
                    {
                        var introduccion2 = introduccionVersionBeneficio.Where(x => x.IdVersionPrograma == 2).FirstOrDefault().Introduccion;
                        fila += "<tr> <td><strong>Profesional</strong></td><td>" + "<h3 style='margin-top: -3px; margin-bottom: 5px;'><strong>" + introduccion2 + "</strong></h3>" + descripcionProfesional + "</td></tr>";
                    }
                    else 
                    {
                        fila += "<tr> <td><strong>Profesional</strong></td><td>" + descripcionProfesional + "</td></tr>";
                    }
                }
                if (flagVersionGerencial == true)
                {
                    if (introduccionVersionBeneficio != null && introduccionVersionBeneficio.Count() > 0)
                    {
                        var introduccion3 = introduccionVersionBeneficio.Where(x => x.IdVersionPrograma == 3).FirstOrDefault().Introduccion;
                        fila += "<tr><td><strong>Gerencial</strong></td><td>" + "<h3 style='margin-top: -3px; margin-bottom: 5px;'><strong>" + introduccion3 + "</strong></h3>" + descripcionGerencial + "</td></tr>";
                    }
                    else
                    {
                        fila += "<tr><td><strong>Gerencial</strong></td><td>" + descripcionGerencial + "</td></tr>";
                    }
                }

            }

            if (seccionBeneficios != null && beneficios.Count() > 0)
            {
                seccionBeneficios.Contenido = inicio + fila + "</table>" + addPiePagina;

                //Seccion BENEFICIOS Operaciones
                seccionesBeneficiosInversion.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
                {
                    Seccion = seccionBeneficios.Seccion,
                    Contenido = "<table id=\"tableinversion\" class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Beneficios</strong></td></tr>" + fila + "</table>"
                });
            }
            else
            {
                var itemToRemove = secciones.Single(r => LimpiarCadena(r.Seccion).ToLower().Equals("beneficios"));
                secciones.Remove(itemToRemove);
            }

            //Montos Complementarios
            seccionesBeneficiosInversion.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
            {
                Seccion = "Monto Actual",
                Contenido = "<table id=\"tablemontomatricula\" class=\"table table-hover \"><tr><td><strong>Moneda</strong></td><td><strong>Costo Original</strong></td><td><strong>Descuento</strong></td><td><strong>Porcentaje Descuento</strong></td><td><strong>Costo Final</strong></td></tr>"
                + string.Join("", montopagado.Select(s => "<tr><td>" + s.Moneda + "</td><td>" + s.CostoOriginal + "</td><td>" + s.Descuento + "</td><td>" + s.PorcentajeDescuento + "</td><td>" + s.CostoFinal + "</td></tr>").ToArray()) + "</table>"
            });

            var etiquetaFormasPago = "<table style='font-family:arial,sans-serif;border-collapse:collapse;width:100%'><tr><th style='background-color:#4584a7;color:#fff;border:1px solid #d7d7d7;padding:10px'>Formas de Pago</th></tr><tr><td style='border:1px solid #d7d7d7;padding:10px'><h2>PERU</h2><p><span style='font-size:13.3333px'><strong>1.</strong>Pago por Tarjeta Visa, Mastercard, Amex, Diners a trav&eacute;s de&nbsp;</span><span style='font-size:13.3333px'>nuestra pagina web:&nbsp;</span><a href='https://bsginstitute.com/Cuenta' style='font-size:13.3333px'>https://bsginstitute.com/</a></p><p>&nbsp;</p><p><strong>2.</strong><span style='font-size:13.3333px'>Pago&nbsp;</span><span style='font-size:13.3333px'>mediante una transferencia interbancaria(Per&uacute;) de su banco a nuestro banco.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Raz&oacute;n social: BS Grupo SAC</span></li><li style='text-align:left'><span style='font-size:10pt'>Raz&oacute;n Comercial: BSG INSTITUTE</span></li><li style='text-align:left'><span style='font-size:10pt'>Ruc: 20454870591</span></li><li style='text-align:left'><span style='font-size:10pt'>Direcci&oacute;n: Pasaje Romaña-Calle 2 Nro 107 Urb. Le&oacute;n XIII Arequipa-Arequipa-Cayma</span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>215-1863341-0-42*</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001863341042-20</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta corriente en D&oacute;lares</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>215-1870934-1-48*</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001870934148-23</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco BBVA Continental</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>0011-0220-01-00131737</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>011 220 000100131737 17</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco Scotiabank</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>000-4654102</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>009-313-000004654102-85</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>* N&uacute;mero de cuenta autorizada solo para transferencias de empresas, no est&aacute; habilitado para dep&oacute;sitos en ventanilla.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>&nbsp;</span></p><p><h2>EXTRANJERO</h2><strong>1.</strong><span style='font-size:13.3333px'>Pago&nbsp;</span><span style='font-size:13.3333px'>mediante una transferencia bancaria Internacional de su banco a nuestro banco.</span></p><p style='margin-left:30px'><span style='font-size:10pt'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BSG Institute S.A.C</span></li><li><span style='font-size:10pt'>RUC: 20454870591</span></li><li><span style='font-size:10pt'>Address: Pasaje Romaña-Calle 2 Nro 107 Urb. Le&oacute;n XIII Arequipa-Arequipa-Cayma</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos del banco</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Bank: Banco de Cr&eacute;dito del Per&uacute;</span></li><li><span style='font-size:10pt'>Address: Street San Juan de Dios 123, Arequipa, Per&uacute;</span></li><li><span style='font-size:10pt'>Swift Code: BCPLPEPL</span></li><li><span style='font-size:10pt'>Account Number USD$: 215-1870934-1-48</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>COLOMBIA</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>Para transferencias Nacionales Colombia:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BS GRUPO COLOMBIA SAS</span></li><li><span style='font-size:10pt'>NIT: 900776296</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuenta de Ahorro en Pesos</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Datos del banco: Bancolombia</span></li><li><span style='font-size:10pt'>Numero de Cuenta: 65231918412</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>BOLIVIA</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>Para transferencias Nacionales Bolivia:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BSG INSTITUTE BOLIVIA</span></li><li><span style='font-size:10pt'>NIT: 376053024</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuenta corriente en Bolivianos</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Datos del banco: Banco de Credito Bolivia</span></li><li><span style='font-size:10pt'>Numero de Cuenta Bolivianos: 701-5051921-3-41</span></li><li><span style='font-size:10pt'>Numero de Cuenta D&oacute;lares: 701-5041553-2-04</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>MEXICO</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>1.	Pagos aceptado con Tarjeta Visa, Mastercard, American Express y Carnet (Débito y Crédito) a través de nuestra página web<a href='https://bsginstitute.com/Cuenta' style='font-size:13.3333px'>https://bsginstitute.com/</a></span></p><p style='margin-left:30px'><span style='font-size:10pt'>2. Pagos mediante D&eacute;positos Bancarios y Transferencia por SPEI*</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Raz&oacute;n Social: BSG Institute M&eacute;xico S.A. de C.V.</span></li><li><span style='font-size:10pt'>Raz&oacute;n Comercial: BSG Institute</span></li><li><span style='font-size:10pt'>RFC: BIM210209H26</span></li><li><span style='font-size:10pt'>Direcci&oacute;n: Montecito No. 38, Piso 33, Of. 4.</span></li><li><span style='font-size:10pt'>Edificio: World Trade Center – WTC</span></li><li><span style='font-size:10pt'>Colonia N&aacute;poles</span></li><li><span style='font-size:10pt'>Ciudad de M&eacute;xico</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuentas</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Cuenta en Pesos Mexicanos en el BBVA Bancomer</span></li><li><span style='font-size:10pt'>N&uacute;mero de Cuenta: 0116490468</span></li><li><span style='font-size:10pt'>Cuenta CLABE Interbancaria: 012180001164904687</span></li></ul><ul style='margin-left:30px'><li><span style='font-size:10pt'>Cuenta en Dólares Americanos en el BBVA Bancomer</span></li><li><span style='font-size:10pt'>N&uacute;mero de Cuenta: 0116490522</span></li><li><span style='font-size:10pt'>Cuenta CLABE Interbancaria: 012180001164905220</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>3. Liga (Enlace) de Pago</span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Deber&aacute; solicitar a su Asesor el envío de la liga de pago por correo electrónico, una vez recibido lo conduce a una p&aacute;gina web para completar el pago.</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* El pago podr&iacute;a est&aacute;r sujeto al cobro de comisiones del banco emisor, por favor verificar previamente.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p></td></tr></table>";
            var etiquetaTarifarios = ObtenerContenidoTarifario(tarifarios);
            var seccionModalidades = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("modalidades")).FirstOrDefault();

            foreach (var item in modalidades)
            {
                if (item.FechaReal != null)
                {
                    if (item.Tipo.ToLower() == "online asincronica")
                    {
                        var fechaReal = item.FechaReal?.ToString("MMM yyy", CultureInfo.CreateSpecificCulture("es-ES"));
                        fechaReal = fechaReal.Substring(0, 1).ToUpper() + fechaReal.Substring(1);
                        item.FechaHoraInicio = fechaReal;
                    }
                    else
                    {
                        var fechaReal = item.FechaReal?.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("es-ES"));
                        CultureInfo cultura = CultureInfo.CurrentCulture;
                        TextInfo ti = cultura.TextInfo;
                        item.FechaHoraInicio = ti.ToTitleCase(fechaReal);
                    }
                }
                else
                {
                    item.FechaHoraInicio = "Por definir";
                }
            }

            seccionModalidades.Contenido = "<table class=\"table table-hover \"><tr><td><strong>Modalidad</strong></td><td><strong>Centro de Costo</strong></td><td><strong>Fecha de Inicio</strong></td></tr>"
           + string.Join("", modalidades.Select(s => "<tr><td>" + s.Tipo + "</td><td>" + s.NombreCentroCosto + "</td><td>" + s.FechaHoraInicio + "</td></tr>").ToArray()) + "</table>";

            //Duracion y Horarios
            string contenido2 = secciones.Where(w => LimpiarCadena(w.Seccion).ToLower().Equals("duracion y horarios")).Select(w => w.Contenido).FirstOrDefault();
            string contenido = "";
            if (contenido2 != null)
            {
                if (contenido2.Contains("<ul><li>"))
                {
                    contenido = contenido2 + "</ul></ul>";
                }
                else contenido = contenido2;
            }

            string newcontenido = ObtenerContenidoHorarios(modalidades, contenido, idPGeneral);
            var seccionContenido = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("duracion y horarios")).FirstOrDefault();

            //Si no tiene nada configurado en el silavoV2
            if (contenido == null)
            {
                ProgramaGeneralSeccionAnexosHTMLDTO programaNuevo = new ProgramaGeneralSeccionAnexosHTMLDTO();
                programaNuevo.Seccion = "Duración y Horarios";
                programaNuevo.Contenido = newcontenido;
                secciones.Add(programaNuevo);
            }
            else
            {
                if (seccionContenido != null)
                {
                    //seccionContenido.Contenido = newcontenido;
                    newcontenido = seccionContenido.Contenido;
                }

            }

            string[] listaTituloV1 = { "estructura curricular", "beneficios", "prerrequisitos", "certificacion", "certificación", "duracion y horarios", "duración y horarios", "evaluacion", "evaluación", "bibliografia", "bibliografía", "material del curso", "pautas complementarias", "descripción certificación", "descripcion certificacion", "objetivos", "presentacion", "presentación", "público objetivo", "publico objetivo", "metodolog&#237;a online de este programa", "modalidad", "inversion", "perfil del egressado", "mercado laboral", "expositores", "metodologia del programa" };
            string[] listaTituloV2 = { "Estructura Curricular", "Beneficios", "Prerrequisitos", "Certificación", "Certificación", "duraci&#243;n y horarios", "Duración y Horarios", "Evaluación", "Evaluación", "Bibliografía", "Bibliografía", "Material del Curso", "Pautas Complementarias", "Descripción Certificación", "Descripción Certificación", "Objetivos", "Presentación", "Presentación", "Público Objetivo", "Público Objetivo", "Metodolog&#237;a Online de este Programa", "Modalidad", "Inversion", "Perfil del egressado", "Mercado laboral", "Expositores", "metodolog&#237;a del programa" };
            for (var i = 0; i < listaTituloV2.Length; i++)
            {
                var pendienteTildes = secciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena(listaTituloV1[i])).FirstOrDefault();
                if (pendienteTildes != null)
                {
                    if (pendienteTildes.Contenido == null || pendienteTildes.Contenido != "")
                    {
                        pendienteTildes.Seccion = listaTituloV2[i];
                    }
                }
            }

            if (estructuraTecnico != null && estructuraTecnico != "")
            {
                secciones.Add(new ProgramaGeneralSeccionAnexosHTMLDTO() { Seccion = "Estructura Curricular", Contenido = estructuraTecnico });
            }
            var html = string.Join("", secciones.Select(s => "<h4>" + s.Seccion + "</h4>" + s.Contenido + "<br />").ToArray());

            var etiquetaBeneficiosInversion = "";
            var etiquetaExpositores = "";
            var etiquetaDuracionHorarios = "";
            var informacionPrograma = "";

            if (seccionesBeneficiosInversion != null)
            {
                etiquetaBeneficiosInversion = string.Join("", seccionesBeneficiosInversion.Where(w => LimpiarCadena(w.Seccion).ToLower().Equals("inversion") || w.Seccion.ToLower().Equals("beneficios") || w.Seccion.ToLower().Equals("monto actual")).Select(s => "<br><h4 class='col-sm-10' Id='IdTabla" + s.Seccion + "'>" + s.Seccion + "</h4><br>" + s.Contenido).ToArray());
            }

            if (seccionExpositores != null)
            {
                etiquetaExpositores = seccionExpositores.Contenido;
            }

            if (newcontenido != null)
            {
                etiquetaDuracionHorarios = newcontenido;
            }

            if (html != null)
            {
                informacionPrograma = html;
            }

            return new CargarInformacionProgramaRespuestaDTO()
            {
                EtiquetaFormasPago = etiquetaFormasPago,
                EtiquetaBeneficiosInversion = etiquetaBeneficiosInversion,
                EtiquetaExpositores = etiquetaExpositores,
                EtiquetaDuracionHorarios = etiquetaDuracionHorarios,
                InformacionPrograma = informacionPrograma,
                EtiquetaTarifarios = etiquetaTarifarios,
                ListaBeneficios = montosBeneficios.ListaBeneficios
            };

        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Cargar Informacion Programa
        /// </summary>
        /// <param name="idCentroCosto">Id de Centro Costo</param>
        /// <returns> void </returns>
        public CargarInformacionProgramaAutomaticoRespuestaOperacionesAtcDTO CargarInformacionProgramaAutomatico5M(int idCentroCosto, int codigoPais, int idMatriculaCabecera, int idOportunidad)
        {
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var data = servicioPGeneral.ObtenerPGeneralPEspecificoPorCentroCosto(idCentroCosto);
            CargarInformacionProgramaRespuestaOperacionesAtcDTO informacionPrograma = null;
            List<ResumenProgramaV2DTO> resumenProgramasV2 = null;

            var idPGeneral = data != null ? data.IdProgramaGeneral : 0;

            if (idPGeneral != 0)
            {
                informacionPrograma = CargarInformacionPrograma5M(idPGeneral, codigoPais, idMatriculaCabecera, idOportunidad);
            }
            if (data.IdArea != 0 && data.IdSubArea != 0)
            {
                var filtros = new Dictionary<string, string>
                {
                    { "idArea", data.IdArea.ToString() },
                    { "idSubArea", data.IdSubArea.ToString() },
                    { "codigoPais", codigoPais.ToString() }
                };
                resumenProgramasV2 = CargarResumenProgramasV2(filtros);
            }
            return new CargarInformacionProgramaAutomaticoRespuestaOperacionesAtcDTO()
            {
                IdPGeneral = idPGeneral,
                InformacionPrograma = informacionPrograma.InformacionPrograma,
                InformacionProgramaV2 = informacionPrograma.InformacionProgramaV2,
                ResumenProgramasV2 = resumenProgramasV2,
                EtiquetaDuracionHorarios = informacionPrograma.EtiquetaDuracionHorarios,
                EtiquetaExpositores = informacionPrograma.EtiquetaExpositores,
                EtiquetaBeneficiosInversion = informacionPrograma.EtiquetaBeneficiosInversion,
                EtiquetaFormasPago = informacionPrograma.EtiquetaFormasPago,
                ListaTarifarios = informacionPrograma.ListaTarifarios,
                ListaBeneficios = informacionPrograma.ListaBeneficios,
                listaBeneficiosAtC = informacionPrograma.listaBeneficiosAtC,
                inversion = informacionPrograma.inversion,
                montopagado = informacionPrograma.montopagado,
                versionAlumno = informacionPrograma.versionAlumno
            };
        }

        /// Autor: Juan Diego Huanaco Quispe
        /// Fecha: 18/04/2024
        /// Version: 1.0
        /// <summary>
        /// Carga Informacion de los Programas
        /// </summary>
        /// <param name="idPGeneral">Id del programa General</param>
        /// <param name="codigoPais">Codigo del pais</param>
        /// <returns> List ProgramaGeneralSeccionDocumentoDTO  </returns>
        public List<ProgramaGeneralSeccionDocumentoDTO> CargarInformacionProgramaV2(int idPGeneral, int codigoPais)
        {

            var programaEspecificoService = new PEspecificoService(_unitOfWork);
            var programaGeneralService = new PGeneralService(_unitOfWork);
            var origenServicio = new OrigenService(_unitOfWork);
            var configuracionBeneficioService = new ConfiguracionBeneficioProgramaGeneralService(_unitOfWork);
            var documentoAgendaService = new DocumentoAgendaService(_unitOfWork);

            /* 'informacionPrograma' trae determinada data para cada seccion, a lo largo del tiempo se han solicitado cambios en cuanto a la data que debe 
             * enviar este servicio, sin embargo los View usados no se han actualizado, sino que se crearon unos nuevos y parte del ordenamiento
             * se hace en el mismo servicio, he de suponer que por temas de compatibilidad con otros servicios aun usan esos Views.
             * 
             * Por este motivo, se añade/modifica contenido a las secciones de 'informacionPrograma', para lograr enviar
             * la data que actualmente necesita el frontend.
            */
            var informacionPrograma = documentoAgendaService.ObtenerInformacionProgramaGeneral(idPGeneral);

            //Actualmente el programa jala dos secciones parecidas 'Pre-requisitos' y 'Prerrequisitos'. Eliminamos 'Pre-requisitos' de nuestra lista
            informacionPrograma.RemoveAll(x => x.Seccion.ToLower() == "pre-requisitos");

            #region Modalidades
            informacionPrograma.RemoveAll(x => LimpiarCadena(x.Seccion).ToLower() == "modalidades");

            var modalidadGeneral = new List<ModalidadProgramaDTO>();
            var modalidadGeneralPortal = programaEspecificoService.ObtenerFechaInicioProgramaTodos(idPGeneral) ?? new List<PEspecificoPorIdPGeneral>();

            foreach (var modalidadPortal in modalidadGeneralPortal)
            {
                modalidadGeneral.Add(new ModalidadProgramaDTO()
                {
                    Tipo = modalidadPortal.Tipo,
                    Ciudad = modalidadPortal.Ciudad,
                    FechaHoraInicio = modalidadPortal.FechaInicioTexto,
                    FechaReal = modalidadPortal.FechaInicio
                });
            }

            var modalidadAsincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Asincronica") && s.Ciudad.Equals("LIMA")).ToList();
            if (modalidadAsincronica.Count() == 0)
                modalidadAsincronica = modalidadGeneral.Where(s => LimpiarCadena(s.Tipo).Equals("Online Asincronica")).ToList();
            var modalidadSincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Sincronica")).ToList();
            var modalidadPresencial = modalidadGeneral.Where(s => LimpiarCadena(s.Tipo).Equals("Presencial")).ToList();

            var modalidades = modalidadAsincronica.Concat(modalidadSincronica).Concat(modalidadPresencial).ToList();
            var modalidadesContenidos = new List<ProgramaGeneralSeccionDocumentoDetalleDTO>()
            {
                new ()
                {
                    Titulo = "Modalidad",
                    DetalleContenido = new List<string>()
                },
                new ()
                {
                    Titulo = "Fecha de Inicio",
                    DetalleContenido = new List<string>()
                }
            };

            foreach (var item in modalidades)
            {
                if (item.FechaReal != null && item.Tipo.ToLower() == "online asincronica")
                {
                    var fechaReal = item.FechaReal?.ToString("MMM yyy", CultureInfo.CreateSpecificCulture("es-ES"));
                    fechaReal = fechaReal.Substring(0, 1).ToUpper() + fechaReal.Substring(1);
                    item.FechaHoraInicio = fechaReal;
                }
                else
                    item.FechaHoraInicio = "Por definir";

                modalidadesContenidos[0].DetalleContenido.Add(item.Tipo);
                modalidadesContenidos[1].DetalleContenido.Add(item.FechaHoraInicio);
            }

            informacionPrograma.Add(new()
            {
                Seccion = "Modalidades",
                DetalleSeccion = modalidadesContenidos
            });

            #endregion

            #region Inversion
            informacionPrograma.RemoveAll(x => LimpiarCadena(x.Seccion).ToLower() == "inversion");
            //todos los beneficios con su idVersion
            var beneficios = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerPGeneralConfiguracionBeneficios(idPGeneral)
               .Where(x => x.Paises.Any(y => y == codigoPais)).ToList();

            //Trae montos y beneficios segmentado por Version (Basica, Profesional, Gerencial).
            var inversion = ObtenerMontos2(idPGeneral, codigoPais);

            var inversionContenidos = new List<ProgramaGeneralSeccionDocumentoDetalleDTO>() {
                new(){
                    Titulo = "Versiones",
                    DetalleContenido = new List<string>()
                },
                new(){
                    Titulo = "Precio al contado",
                    DetalleContenido =new List<string>()
                },
                new(){
                    Titulo = "Precio al crédito",
                    DetalleContenido = new List<string>()
                }
            };

            foreach (var i in inversion.MontosPorPais)
            {
                inversionContenidos[0].DetalleContenido.Add(i.NombrePaquete);
                inversionContenidos[1].DetalleContenido.Add(i.InversionContado);
                inversionContenidos[2].DetalleContenido.Add(i.InversionCredito);
            }

            informacionPrograma.Add(new()
            {
                Seccion = "Inversion",
                DetalleSeccion = inversionContenidos
            });
            #endregion

            #region Beneficios
            var beneficioAntiguaSeccion = informacionPrograma.FirstOrDefault(x => LimpiarCadena(x.Seccion).ToLower().Equals("beneficios"));
            string piePaginaBeneficio = String.Empty;
            string cabeceraBeneficio = String.Empty;
            if (beneficioAntiguaSeccion != null)
            {
                piePaginaBeneficio = beneficioAntiguaSeccion.DetalleSeccion[0].PiePagina;
                cabeceraBeneficio = beneficioAntiguaSeccion.DetalleSeccion[0].Cabecera;
                informacionPrograma.Remove(beneficioAntiguaSeccion);
            }


            var beneficiosContenidos = new List<ProgramaGeneralSeccionDocumentoDetalleDTO>();
            foreach (var i in inversion.MontosPorPais)
            {
                beneficiosContenidos.Add(new()
                {
                    Cabecera = cabeceraBeneficio,
                    Titulo = i.NombrePaquete,
                    DetalleContenido = inversion.ListaBeneficios.Where(x => x.Paquete == i.Paquete).OrderBy(x => x.OrdenBeneficio).Select(x => x.Titulo).ToList(),
                    PiePagina = piePaginaBeneficio
                });
            }

            informacionPrograma.Add(new()
            {
                Seccion = "Beneficios",
                DetalleSeccion = beneficiosContenidos
            });

            #endregion


            //Decode the encoded caracters
            foreach (var item in informacionPrograma)
            {
                item.Seccion = WebUtility.HtmlDecode(item.Seccion);

                foreach (var i in item.DetalleSeccion)
                {
                    i.Titulo = WebUtility.HtmlDecode(i.Titulo);
                    i.Cabecera = WebUtility.HtmlDecode(i.Cabecera);
                    i.Cabecera = string.IsNullOrWhiteSpace(i.Cabecera) ? null : i.Cabecera;
                    i.PiePagina = WebUtility.HtmlDecode(i.PiePagina);
                    i.PiePagina = string.IsNullOrWhiteSpace(i.PiePagina) ? null : i.PiePagina;
                    i.DetalleContenido = i.DetalleContenido.ConvertAll(x => WebUtility.HtmlDecode(x));
                }

            }

            // Haremos una conversion del titulo de cada seccion para asegurar que los nombres seran siempre los mismos.
            var sectionsNewName = new Dictionary<string, string>
            {
                { "duración y horarios", "duracion y horarios" },
                { "descripción certificación", "descripcion certificacion" },
                { "descripción estructura", "descripcion estructura" },
                { "presentación", "presentacion" },
                { "público objetivo", "publico objetivo" },
                { "metodología online de este programa", "metodologia online de este programa" },
                { "inversión", "inversion" },
                { "certificación", "certificacion" },
                { "bibliografía", "bibliografia" }
            };

            foreach (var seccion in informacionPrograma)
            {
                seccion.Seccion = seccion.Seccion.ToLower();
                if (sectionsNewName.ContainsKey(seccion.Seccion))
                    seccion.Seccion = sectionsNewName[seccion.Seccion];
            }


            //Secciones a las que no limpiaremos el HTML ya que lo reutilizaremos en la vista.
            var allowedHTMLSections = new List<string>()
            {
                "duracion y horarios",
                "descripcion certificacion",
                "descripcion estructura",
                "presentacion",
                "publico objetivo",
                "objetivos",
                "metodologia online de este programa",
                "pautas complementarias",
                "material del curso",
                "bibliografia"
            };

            foreach (var seccion in informacionPrograma)
            {
                if (allowedHTMLSections.Exists(x => x == seccion.Seccion))
                    continue;

                foreach (var detalleSec in seccion.DetalleSeccion)
                {
                    detalleSec.Titulo = string.IsNullOrWhiteSpace(detalleSec.Titulo) ? null : Regex.Replace(detalleSec.Titulo, "<.*?>", "");
                    detalleSec.Cabecera = string.IsNullOrWhiteSpace(detalleSec.Cabecera) ? null : Regex.Replace(detalleSec.Cabecera, "<.*?>", "");
                    detalleSec.PiePagina = string.IsNullOrWhiteSpace(detalleSec.PiePagina) ? null : detalleSec.PiePagina;
                    for (var i = 0; i < detalleSec.DetalleContenido.Count(); i++)
                        detalleSec.DetalleContenido[i] = string.IsNullOrWhiteSpace(detalleSec.DetalleContenido[i]) ? null : Regex.Replace(detalleSec.DetalleContenido[i], "<.*?>", "");
                }
            }

            return informacionPrograma;
        }

        public CargarInformacionProgramaRespuestaOperacionesAtcDTO CargarInformacionPrograma5M(int idPGeneral, int codigoPais, int idMatriculaCabecera, int idOportunidad)
        {

            var servicioConfiguracionBeneficio = new ConfiguracionBeneficioProgramaGeneralService(_unitOfWork);
            var servicioDocumentoAgenda = new DocumentoAgendaService(_unitOfWork);
            var servicioPEspecifico = new PEspecificoService(_unitOfWork);
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var servicioOrigen = new OrigenService(_unitOfWork);

            List<listaBeneficiosDTO> listaBeneficios = new List<listaBeneficiosDTO>();

            var beneficiosV2 = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerPGeneralConfiguracionBeneficios(idPGeneral);
            var beneficios = beneficiosV2.Where(x => x.Paises.Any(y => y == codigoPais)).ToList();

            //nueva logica
            var ProgramaPadre = _unitOfWork.PGeneralRepository.ProgramaGeneralPadre(idPGeneral);
            var programaTecnico = _unitOfWork.PGeneralRepository.ProgramaGeneralEsTecnico(idPGeneral);

            string contenidoStructuraTecnico = "";
            if (programaTecnico)
            {
                if (ProgramaPadre)
                {
                    var listaCursosHijo = _unitOfWork.PGeneralRepository.ListaCursosHijoPorIdPGeneral(idPGeneral);
                    foreach (var item in listaCursosHijo)
                    {

                        contenidoStructuraTecnico += "<h5><strong>" + item.Curso + "</strong></h5>";
                        var duracionCurso = _unitOfWork.PGeneralRepository.ObtenerDuracionCursoHijo(item.IdHijo);
                        var CursosHijo = _unitOfWork.PGeneralRepository.ContenidoEstructuraHijoPadre(item.IdHijo);

                        contenidoStructuraTecnico += "<ul type='disc'>";
                        var listaContenidoCurso = CursosHijo.GroupBy(x => x.Contenido).Select(x => x.First()).ToList();
                        foreach (var contenidoCurso in listaContenidoCurso)
                        {
                            contenidoStructuraTecnico += "<li>&nbsp;&nbsp;&nbsp;" + contenidoCurso.Contenido + "</li>";
                        }
                        contenidoStructuraTecnico += "</ul>";
                    }
                }
            }
            var estructuraTecnico = contenidoStructuraTecnico;

            var lista = servicioDocumentoAgenda.ObtenerInformacionProgramaGeneral(idPGeneral);
            var listaPiePagina = lista.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("beneficios")).FirstOrDefault();
            var addPiePagina = "";
            if (listaPiePagina != null)
            {
                addPiePagina = "<p>" + listaPiePagina.DetalleSeccion[0].PiePagina + "</p>";
                lista.Remove(listaPiePagina);
            }
            //Obtiene HTML y adapta los datos
            var seccionesV2Ordenado = servicioDocumentoAgenda.GenerarHTMLProgramaGeneralDocumentoSeccion(lista);
            foreach (var item in seccionesV2Ordenado)
            {
                string temporal = Regex.Replace(item.Contenido, "&bull;", "");
                string temporal2 = Regex.Replace(temporal, "&nbsp;", "");

                if (LimpiarCadena(item.Seccion).ToLower() != "estructura curricular" && LimpiarCadena(item.Seccion).ToLower() != "beneficios" && LimpiarCadena(item.Seccion).ToLower() != "certificacion" && LimpiarCadena(item.Seccion).ToLower() != "prerrequisitos" && LimpiarCadena(item.Seccion).ToLower() != "expositores")
                {
                    string temp3 = Regex.Replace(temporal2, "<br />", "<br /></li>");
                    string temp4 = Regex.Replace(temp3, "<br/>", "<br /></li>");
                    string temp5 = Regex.Replace(temp4, "<ul type='disc'><li>", "");

                    if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                    {
                        string temp6 = Regex.Replace(temp5, "<h5><strong><b>" + item.Seccion + "</b></strong></h5>", "");
                        item.Contenido = temp6;
                    }

                }
                else
                {
                    if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                    {
                        string temp3 = Regex.Replace(temporal2, "<br />", "<br /></li>");
                        string temp4 = Regex.Replace(temp3, "<br/>", "<br /></li>");
                        string temp5 = Regex.Replace(temp4, "<ul type='disc'><li>", "");

                        if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                        {
                            string temp6 = Regex.Replace(temp5, "<h5><strong><b>" + item.Seccion + "</b></strong></h5>", "");
                            item.Contenido = temp6;
                        }
                    }
                    else
                    {
                        string temp3 = Regex.Replace(temporal2, "<h5><strong><b>", "<h6>");
                        string temp4 = Regex.Replace(temp3, "</b></strong></h5>", "</h6>");

                        item.Contenido = temp4;
                    }

                }
                if (item.Contenido == null || item.Contenido == "")
                {
                    item.Seccion = "";
                }
            }

            var estructura = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("estructura curricular")).FirstOrDefault();
            var certificacion = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("certificacion")).FirstOrDefault();

            //Seccion Descripcion Estructura Curricular y Certificacion
            var concatenarEstructura = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("descripcion estructura")).FirstOrDefault();
            var concatenarCertificacion = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("descripcion certificacion")).FirstOrDefault();
            if (concatenarEstructura != null && estructura != null)
            {
                estructura.Contenido = estructura.Contenido + concatenarEstructura.Contenido;
                seccionesV2Ordenado.Remove(concatenarEstructura);
            }
            if (concatenarCertificacion != null && certificacion != null)
            {
                certificacion.Contenido = certificacion.Contenido + concatenarCertificacion.Contenido;
                seccionesV2Ordenado.Remove(concatenarCertificacion);
            }

            // Elimina Data de Video innecesarios
            var deleteVideos = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("video") || LimpiarCadena(x.Seccion).ToLower().Equals("vista previa") || LimpiarCadena(x.Seccion.ToLower()).Equals("video de presentacion")).ToList();
            if (deleteVideos != null)
            {
                foreach (var item in deleteVideos)
                {
                    seccionesV2Ordenado.Remove(item);
                }
            }

            //Ordena data para hacer el display de forma predeterminada
            List<ProgramaGeneralSeccionAnexosHTMLDTO> seccionesV2 = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
            string[] listaTituloV1Orden = { "presentación", "objetivos", "público objetivo", "pre-requisitos", "estructura curricular", "duración y horarios", "certificacion", "expositores", "metodología online de este programa", "material del curso", "pautas complementarias", "bibliografía", "modalidad", "inversion", "perfil del egresado", "mercado laboral", "metodologia del programa" };
            string[] listaTituloV2Orden = { "presentacion", "objetivos", "publico objetivo", "prerrequisitos", "estructura curricular", "duracion y horarios", "certificación", "expositores", "metodolog&#237;a online de este programa", "material del curso", "pautas complementarias", "bibliografia", "Modalidad", "Inversion", "Perfil del egresado", "Mercado laboral", "Metodolog&#237;a del programa" };

            for (var i = 0; i < listaTituloV1Orden.Length; i++)
            {
                var ordenarTempV2 = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena(listaTituloV2Orden[i])).FirstOrDefault();
                if (ordenarTempV2 != null)
                {
                    seccionesV2.Add(ordenarTempV2);
                }
                else
                {
                    var ordenarTempV1 = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena(listaTituloV1Orden[i])).FirstOrDefault();
                    if (ordenarTempV1 != null)
                    {
                        seccionesV2.Add(ordenarTempV1);
                    }
                }
            }

            // VALIDACION Y CREACION DE Items en Objeto para que no caiga en NULL
            var secciones = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
            ProgramaGeneralSeccionAnexosHTMLDTO modalidadAdd = new ProgramaGeneralSeccionAnexosHTMLDTO()
            {
                Seccion = "Modalidades",
                Contenido = "",
            };
            secciones.Add(modalidadAdd);
            var flagBeneficio = false;
            var flagInversion = false;
            foreach (var item in seccionesV2)
            {
                if (item.Seccion == "Inversion")
                {
                    flagInversion = true;
                }
                if (item.Seccion == "Beneficios")
                {
                    flagBeneficio = true;
                }
                if (item.Seccion == "Inversión")
                {
                    item.Seccion = "Inversion";
                    flagInversion = true;
                }

            }
            if (flagInversion == false)
            {
                ProgramaGeneralSeccionAnexosHTMLDTO inversionAdd = new ProgramaGeneralSeccionAnexosHTMLDTO()
                {
                    Seccion = "Inversion",
                    Contenido = "",
                };
                secciones.Add(inversionAdd);
            }
            if (flagBeneficio == false)
            {
                ProgramaGeneralSeccionAnexosHTMLDTO beneficiosAdd = new ProgramaGeneralSeccionAnexosHTMLDTO()
                {
                    Seccion = "Beneficios",
                    Contenido = "",
                };
                secciones.Add(beneficiosAdd);
            }
            secciones.AddRange(seccionesV2);
            //Logica de Montos de V1
            var seccionesBeneficiosInversion = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();

            var montosBeneficios = ObtenerMontos2(idPGeneral, codigoPais);
            var montos = montosBeneficios.MontosPorPais;

            var contador = 0;
            if (montos.Count() < 1)
            {
                montosBeneficios = ObtenerMontos(idPGeneral, codigoPais);
                montos = montosBeneficios.MontosPorPais;
            }

            foreach (var item in montos)
            {
                if (item.Beneficios == "<ul></ul><br>" || item.Beneficios == null || item.Beneficios == "null")
                {
                    contador++;
                }
            }
            if (contador == montos.Count())
            {
                montosBeneficios = ObtenerMontos(idPGeneral, codigoPais);
                montos = montosBeneficios.MontosPorPais;
            }
            var montosb = montos.Where(s => s.Beneficios != null).ToArray();
            var montopagado = _unitOfWork.MontoPagoCronogramaRepository.ObtenerMontoPagado(idMatriculaCabecera, idOportunidad);

            //Obtencion de Modalidades VPortal
            ModalidadProgramaDTO cambiarModelo;
            List<ModalidadProgramaDTO> modalidadGeneral = new List<ModalidadProgramaDTO>();
            var modalidadGeneralPortal = servicioPEspecifico.ObtenerFechaInicioProgramaTodos(idPGeneral);
            if (modalidadGeneralPortal == null)
            {
                modalidadGeneralPortal = new List<PEspecificoPorIdPGeneral>();
            }
            var programaGeneral = servicioPGeneral.ObtenerPGeneralAtributosPrincipalesPorId(idPGeneral);
            //Obtencion de Modalidades V2
            foreach (var modalidadPortal in modalidadGeneralPortal)
            {
                cambiarModelo = new ModalidadProgramaDTO()
                {
                    Tipo = modalidadPortal.Tipo,
                    Ciudad = modalidadPortal.Ciudad,
                    TipoCiudad = "",
                    FechaHoraInicio = modalidadPortal.FechaInicioTexto,
                    NombrePG = programaGeneral.Nombre,
                    IdPEspecifico = modalidadPortal.Id,
                    NombreESP = modalidadPortal.Nombre,
                    Duracion = modalidadPortal.Duracion,
                    Pw_duracion = programaGeneral.PwDuracion,
                    FechaReal = modalidadPortal.FechaInicio
                };
                modalidadGeneral.Add(cambiarModelo);
            }

            var modalidadAsincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Asincronica") && s.Ciudad.Equals("LIMA")).ToList();
            if (modalidadAsincronica.Count() == 0)
            {
                modalidadAsincronica = modalidadGeneral.Where(s => LimpiarCadena(s.Tipo).Equals("Online Asincronica")).ToList();
            }
            var modalidadSincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Sincronica")).ToList();
            var modalidadPresencial = modalidadGeneral.Where(s => LimpiarCadena(s.Tipo).Equals("Presencial")).ToList();
            List<ModalidadProgramaDTO> PruebaModalidad = new List<ModalidadProgramaDTO>();
            PruebaModalidad.AddRange(modalidadAsincronica);
            PruebaModalidad.AddRange(modalidadSincronica);
            PruebaModalidad.AddRange(modalidadPresencial);

            var modalidades = new List<ModalidadProgramaDTO>();
            modalidades.AddRange(PruebaModalidad);

            var modalidadesV2 = new List<ModalidadProgramaSincronicoDTO>();

            var tarifarios = servicioOrigen.ObtenerTarifariosDetallesAgenda(idMatriculaCabecera);
            var versionAlumno = servicioOrigen.obtenerversionAlumno(idMatriculaCabecera);
            //if (versionAlumno == null)
            //{
            //    versionAlumno.Add(new VersionprogramaDTO() { Nombre = "Sin Version", Id = 4 });
            //}
            var seccionMontos = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("inversion")).FirstOrDefault();
            var seccionExpositores = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("expositores")).FirstOrDefault();

            if (seccionMontos != null)
            {
                if (montos.Count() > 0)
                {
                    seccionMontos.Contenido = "<table class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Precio Contado</strong></td><td><strong>Precio Credito</strong></td></tr>"
                        + string.Join("", montos.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.InversionContado + "</td><td>" + s.InversionCredito + "</td></tr>").ToArray()) + "</table>";

                    seccionesBeneficiosInversion.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
                    {
                        Seccion = seccionMontos.Seccion,
                        Contenido = "<table id=\"tablebeneficios\" class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Precio Contado</strong></td><td><strong>Precio Credito</strong></td></tr>"
                        + string.Join("", montos.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.InversionContado + "</td><td>" + s.InversionCredito + "</td></tr>").ToArray()) + "</table>"
                    });
                }
                else
                {
                    var itemToRemove = secciones.Single(r => LimpiarCadena(r.Seccion) == "Inversion");
                    secciones.Remove(itemToRemove);
                }
            }



            var seccionBeneficios = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("beneficios")).FirstOrDefault();
            string inicio = "<table class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Beneficios</strong></td></tr>";
            string fila = "";
            string descripcionSinVersion = "";
            string descripcionBasica = "";
            string descripcionProfesional = "";
            string descripcionGerencial = "";
            bool flagSinVersion = false;
            bool flagVersionBasica = false;
            bool flagVersionProfesional = false;
            bool flagVersionGerencial = false;
            if (beneficios != null)
            {
                foreach (var item in beneficios)
                {
                    foreach (var item2 in item.Versiones)
                    {
                        if (item2 == 0 || item2 == 4) // SIN VERSION
                        {
                            flagSinVersion = true;
                            descripcionSinVersion += "<ul><li>" + item.Descripcion + "</li></ul>";
                            // listaBeneficios.Where(x => );
                            //foreach (var item3 in listaBeneficios)
                            //{
                            //    if(item3.version == item2.IdVersion.ToString())
                            //    {
                            //        item3.beneficio += "<li class='beneficioDetallePrograma' onclick='MostrarInformacionAdicionalBeneficio(" + item.IdBeneficio + "," + item.IdPGeneral + ")'>" + item.Descripcion + "</li>";
                            //    }
                            //}
                            //listaBeneficios.version = "Sin Version";
                            //listaBeneficios.beneficio = descripcionSinVersion;
                        }
                        if (item2 == 1) // BASICA
                        {
                            flagVersionBasica = true;
                            descripcionBasica += "<ul><li>" + item.Descripcion + "</li></ul>";
                            //listaBeneficios.version = "Basica";
                            //listaBeneficios.beneficio = descripcionBasica;
                        }
                        if (item2 == 2) // PROFESIONAL
                        {
                            flagVersionProfesional = true;
                            descripcionProfesional += "<ul><li>" + item.Descripcion + "</li></ul>";
                            //listaBeneficios.version = "Profesional";
                            //listaBeneficios.beneficio = descripcionProfesional;
                        }
                        if (item2 == 3) // GERENCIAL
                        {
                            flagVersionGerencial = true;
                            descripcionGerencial += "<ul><li>" + item.Descripcion + "</li></ul>";
                            //listaBeneficios.version = "Gerencial";
                            //listaBeneficios.beneficio = descripcionGerencial;
                        }
                    }
                }

                if (flagSinVersion == true)
                {
                    fila += "<tr><td>Sin Versión</td><td>" + descripcionSinVersion + "</td></tr>";
                    listaBeneficios.Add(new listaBeneficiosDTO() { version = "Sin Version", beneficio = descripcionSinVersion });
                }
                if (flagVersionBasica == true)
                {
                    fila += "<tr><td>Básica</td><td>" + descripcionBasica + "</td></tr>";
                    listaBeneficios.Add(new listaBeneficiosDTO() { version = "Basica", beneficio = descripcionBasica });
                }
                if (flagVersionProfesional == true)
                {
                    fila += "<tr><td>Profesional</td><td>" + descripcionProfesional + "</td></tr>";
                    listaBeneficios.Add(new listaBeneficiosDTO() { version = "Profesional", beneficio = descripcionProfesional });
                }
                if (flagVersionGerencial == true)
                {
                    fila += "<tr><td>Gerencial</td><td>" + descripcionGerencial + "</td></tr>";
                    listaBeneficios.Add(new listaBeneficiosDTO() { version = "Gerencial", beneficio = descripcionGerencial });
                }
            }

            if (seccionBeneficios != null && beneficios.Count() > 0)
            {
                seccionBeneficios.Contenido = inicio + fila + "</table>" + addPiePagina;

                //Seccion BENEFICIOS Operaciones
                seccionesBeneficiosInversion.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
                {
                    Seccion = seccionBeneficios.Seccion,
                    Contenido = "<table id=\"tableinversion\" class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Beneficios</strong></td></tr>" + fila + "</table>"
                });
            }
            else
            {
                var itemToRemove = secciones.Single(r => LimpiarCadena(r.Seccion).ToLower().Equals("beneficios"));
                secciones.Remove(itemToRemove);
            }

            //Montos Complementarios
            seccionesBeneficiosInversion.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
            {
                Seccion = "Monto Actual",
                Contenido = "<table id=\"tablemontomatricula\" class=\"table table-hover \"><tr><td><strong>Moneda</strong></td><td><strong>Costo Original</strong></td><td><strong>Descuento</strong></td><td><strong>Porcentaje Descuento</strong></td><td><strong>Costo Final</strong></td></tr>"
                + string.Join("", montopagado.Select(s => "<tr><td>" + s.Moneda + "</td><td>" + s.CostoOriginal + "</td><td>" + s.Descuento + "</td><td>" + s.PorcentajeDescuento + "</td><td>" + s.CostoFinal + "</td></tr>").ToArray()) + "</table>"
            });

            //var etiquetaFormasPago = "<table style='font-family:arial,sans-serif;border-collapse:collapse;width:100%'><tr><th style='background-color:#4584a7;color:#fff;border:1px solid #d7d7d7;padding:10px'>Formas de Pago</th></tr><tr><td style='border:1px solid #d7d7d7;padding:10px'><h2>PERU</h2><p><span style='font-size:13.3333px'><strong>1.</strong>Pago por Tarjeta Visa, Mastercard, Amex, Diners a trav&eacute;s de&nbsp;</span><span style='font-size:13.3333px'>nuestra pagina web:&nbsp;</span><a href='https://bsginstitute.com/Cuenta' style='font-size:13.3333px'>https://bsginstitute.com/</a></p><p>&nbsp;</p><p><strong>2.</strong><span style='font-size:13.3333px'>Pago&nbsp;</span><span style='font-size:13.3333px'>mediante una transferencia interbancaria(Per&uacute;) de su banco a nuestro banco.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Raz&oacute;n social: BS Grupo SAC</span></li><li style='text-align:left'><span style='font-size:10pt'>Raz&oacute;n Comercial: BSG INSTITUTE</span></li><li style='text-align:left'><span style='font-size:10pt'>Ruc: 20454870591</span></li><li style='text-align:left'><span style='font-size:10pt'>Direcci&oacute;n: Pasaje Romaña-Calle 2 Nro 107 Urb. Le&oacute;n XIII Arequipa-Arequipa-Cayma</span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>215-1863341-0-42*</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001863341042-20</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta corriente en D&oacute;lares</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>215-1870934-1-48*</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001870934148-23</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco BBVA Continental</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>0011-0220-01-00131737</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>011 220 000100131737 17</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco Scotiabank</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>000-4654102</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>009-313-000004654102-85</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>* N&uacute;mero de cuenta autorizada solo para transferencias de empresas, no est&aacute; habilitado para dep&oacute;sitos en ventanilla.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>&nbsp;</span></p><p><h2>EXTRANJERO</h2><strong>1.</strong><span style='font-size:13.3333px'>Pago&nbsp;</span><span style='font-size:13.3333px'>mediante una transferencia bancaria Internacional de su banco a nuestro banco.</span></p><p style='margin-left:30px'><span style='font-size:10pt'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BSG Institute S.A.C</span></li><li><span style='font-size:10pt'>RUC: 20454870591</span></li><li><span style='font-size:10pt'>Address: Pasaje Romaña-Calle 2 Nro 107 Urb. Le&oacute;n XIII Arequipa-Arequipa-Cayma</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos del banco</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Bank: Banco de Cr&eacute;dito del Per&uacute;</span></li><li><span style='font-size:10pt'>Address: Street San Juan de Dios 123, Arequipa, Per&uacute;</span></li><li><span style='font-size:10pt'>Swift Code: BCPLPEPL</span></li><li><span style='font-size:10pt'>Account Number USD$: 215-1870934-1-48</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>COLOMBIA</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>Para transferencias Nacionales Colombia:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BS GRUPO COLOMBIA SAS</span></li><li><span style='font-size:10pt'>NIT: 900776296</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuenta de Ahorro en Pesos</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Datos del banco: Bancolombia</span></li><li><span style='font-size:10pt'>Numero de Cuenta: 65231918412</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>BOLIVIA</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>Para transferencias Nacionales Bolivia:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BSG INSTITUTE BOLIVIA</span></li><li><span style='font-size:10pt'>NIT: 376053024</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuenta corriente en Bolivianos</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Datos del banco: Banco de Credito Bolivia</span></li><li><span style='font-size:10pt'>Numero de Cuenta Bolivianos: 701-5051921-3-41</span></li><li><span style='font-size:10pt'>Numero de Cuenta D&oacute;lares: 701-5041553-2-04</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>MEXICO</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>1.	Pagos aceptado con Tarjeta Visa, Mastercard, American Express y Carnet (Débito y Crédito) a través de nuestra página web<a href='https://bsginstitute.com/Cuenta' style='font-size:13.3333px'>https://bsginstitute.com/</a></span></p><p style='margin-left:30px'><span style='font-size:10pt'>2. Pagos mediante D&eacute;positos Bancarios y Transferencia por SPEI*</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Raz&oacute;n Social: BSG Institute M&eacute;xico S.A. de C.V.</span></li><li><span style='font-size:10pt'>Raz&oacute;n Comercial: BSG Institute</span></li><li><span style='font-size:10pt'>RFC: BIM210209H26</span></li><li><span style='font-size:10pt'>Direcci&oacute;n: Montecito No. 38, Piso 33, Of. 4.</span></li><li><span style='font-size:10pt'>Edificio: World Trade Center – WTC</span></li><li><span style='font-size:10pt'>Colonia N&aacute;poles</span></li><li><span style='font-size:10pt'>Ciudad de M&eacute;xico</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuentas</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Cuenta en Pesos Mexicanos en el BBVA Bancomer</span></li><li><span style='font-size:10pt'>N&uacute;mero de Cuenta: 0116490468</span></li><li><span style='font-size:10pt'>Cuenta CLABE Interbancaria: 012180001164904687</span></li></ul><ul style='margin-left:30px'><li><span style='font-size:10pt'>Cuenta en Dólares Americanos en el BBVA Bancomer</span></li><li><span style='font-size:10pt'>N&uacute;mero de Cuenta: 0116490522</span></li><li><span style='font-size:10pt'>Cuenta CLABE Interbancaria: 012180001164905220</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>3. Liga (Enlace) de Pago</span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Deber&aacute; solicitar a su Asesor el envío de la liga de pago por correo electrónico, una vez recibido lo conduce a una p&aacute;gina web para completar el pago.</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* El pago podr&iacute;a est&aacute;r sujeto al cobro de comisiones del banco emisor, por favor verificar previamente.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p></td></tr></table>";
            //var etiquetaTarifarios = ObtenerContenidoTarifario(tarifarios);
            var seccionModalidades = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("modalidades")).FirstOrDefault();

            foreach (var item in modalidades)
            {
                if (item.FechaReal != null)
                {
                    if (item.Tipo.ToLower() == "online asincronica")
                    {
                        var fechaReal = item.FechaReal?.ToString("MMM yyy", CultureInfo.CreateSpecificCulture("es-ES"));
                        fechaReal = fechaReal.Substring(0, 1).ToUpper() + fechaReal.Substring(1);
                        item.FechaHoraInicio = fechaReal;
                    }
                }
                else
                {
                    item.FechaHoraInicio = "Por definir";
                }
            }

            seccionModalidades.Contenido = "<table class=\"table table-hover \"><tr><td><strong>Modalidad</strong></td><td><strong>Fecha de Inicio</strong></td></tr>"
           + string.Join("", modalidades.Select(s => "<tr><td>" + s.Tipo + "</td><td>" + s.FechaHoraInicio + "</td></tr>").ToArray()) + "</table>";

            //Duracion y Horarios
            string contenido2 = secciones.Where(w => LimpiarCadena(w.Seccion).ToLower().Equals("duracion y horarios")).Select(w => w.Contenido).FirstOrDefault();
            string contenido = "";
            if (contenido2 != null)
            {
                if (contenido2.Contains("<ul><li>"))
                {
                    contenido = contenido2 + "</ul></ul>";
                }
                else contenido = contenido2;
            }

            string newcontenido = ObtenerContenidoHorarios(modalidades, contenido, idPGeneral);
            var seccionContenido = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("duracion y horarios")).FirstOrDefault();

            if (contenido == null)
            {
                ProgramaGeneralSeccionAnexosHTMLDTO programaNuevo = new ProgramaGeneralSeccionAnexosHTMLDTO();
                programaNuevo.Seccion = "Duración y Horarios";
                programaNuevo.Contenido = newcontenido;
                secciones.Add(programaNuevo);
            }
            else
            {
                if (seccionContenido != null)
                {
                    seccionContenido.Contenido = newcontenido;
                }
            }

            string[] listaTituloV1 = { "estructura curricular", "beneficios", "prerrequisitos", "certificacion", "certificación", "duracion y horarios", "duración y horarios", "evaluacion", "evaluación", "bibliografia", "bibliografía", "material del curso", "pautas complementarias", "descripción certificación", "descripcion certificacion", "objetivos", "presentacion", "presentación", "público objetivo", "publico objetivo", "metodolog&#237;a online de este programa", "modalidad", "inversion", "perfil del egressado", "mercado laboral", "expositores", "metodologia del programa" };
            string[] listaTituloV2 = { "Estructura Curricular", "Beneficios", "Prerrequisitos", "Certificación", "Certificación", "duraci&#243;n y horarios", "Duración y Horarios", "Evaluación", "Evaluación", "Bibliografía", "Bibliografía", "Material del Curso", "Pautas Complementarias", "Descripción Certificación", "Descripción Certificación", "Objetivos", "Presentación", "Presentación", "Público Objetivo", "Público Objetivo", "Metodolog&#237;a Online de este Programa", "Modalidad", "Inversion", "Perfil del egressado", "Mercado laboral", "Expositores", "metodolog&#237;a del programa" };
            for (var i = 0; i < listaTituloV2.Length; i++)
            {
                var pendienteTildes = secciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena(listaTituloV1[i])).FirstOrDefault();
                if (pendienteTildes != null)
                {
                    if (pendienteTildes.Contenido == null || pendienteTildes.Contenido != "")
                    {
                        pendienteTildes.Seccion = listaTituloV2[i];
                    }
                }
            }

            if (estructuraTecnico != null && estructuraTecnico != "")
            {
                secciones.Add(new ProgramaGeneralSeccionAnexosHTMLDTO() { Seccion = "Estructura Curricular", Contenido = estructuraTecnico });
            }
            var html = string.Join("", secciones.Select(s => "<h4>" + s.Seccion + "</h4>" + s.Contenido + "<br />").ToArray());

            //var etiquetaBeneficiosInversion = "";
            var etiquetaExpositores = "";
            var etiquetaDuracionHorarios = "";
            var informacionPrograma = "";

            //if (seccionesBeneficiosInversion != null)
            //{
            //    etiquetaBeneficiosInversion = string.Join("", seccionesBeneficiosInversion.Where(w => LimpiarCadena(w.Seccion).ToLower().Equals("inversion") || w.Seccion.ToLower().Equals("beneficios") || w.Seccion.ToLower().Equals("monto actual")).Select(s => "<br><h4 class='col-sm-10' Id='IdTabla" + s.Seccion + "'>" + s.Seccion + "</h4><br>" + s.Contenido).ToArray());
            //}

            if (seccionExpositores != null)
            {
                etiquetaExpositores = seccionExpositores.Contenido;
            }

            if (newcontenido != null)
            {
                etiquetaDuracionHorarios = newcontenido;
            }

            if (html != null)
            {
                informacionPrograma = html;
            }

            return new CargarInformacionProgramaRespuestaOperacionesAtcDTO()
            {
                //EtiquetaFormasPago = etiquetaFormasPago,
                //EtiquetaBeneficiosInversion = etiquetaBeneficiosInversion,
                EtiquetaExpositores = etiquetaExpositores,
                EtiquetaDuracionHorarios = etiquetaDuracionHorarios,
                InformacionPrograma = informacionPrograma,
                InformacionProgramaV2 = CargarInformacionProgramaV2(idPGeneral, codigoPais),
                ListaTarifarios = tarifarios,
                ListaBeneficios = montosBeneficios.ListaBeneficios,
                listaBeneficiosAtC = listaBeneficios,
                inversion = montos,
                montopagado = montopagado,
                versionAlumno = versionAlumno
            };
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




        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener monto version 2
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <param name="codigoPais">Codigo de Pais</param>
        /// <returns>ObtenerMontos2RespuestaDTO</returns> 
        private ObtenerMontos2RespuestaDTO ObtenerMontos2(int idPGeneral, int codigoPais)
        {
            var servicioMontoPago = new MontoPagoService(_unitOfWork);
            var servicioBeneficioPrograma = new ConfiguracionBeneficioProgramaGeneralService(_unitOfWork);

            var montos = servicioMontoPago.ObtenerMontosPorId(idPGeneral);
            var montosPorPais = montos.Where(s => s.Pais.Equals(codigoPais)).OrderBy(x => x.Paquete).ToList();

            var listaBeneficios = new List<BeneficioDTO>();

            if (montosPorPais.Count() == 0)
            {
                montosPorPais = montos.Where(s => s.Pais.Equals(0)).OrderBy(x => x.Paquete).ToList();
                var beneficio = servicioBeneficioPrograma.ObtenerBeneficiosPGeneralTipo1V2Internacional(idPGeneral);
                if (beneficio.Count() > 0)
                {
                    foreach (var item in montosPorPais)
                    {
                        var items = beneficio.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.Titulo).Select(w => w.Titulo).ToList();
                        string detalles = "<ul>";
                        foreach (var _item in items)
                        {
                            detalles += "<li>" + _item + "</li>";
                        }
                        detalles += "</ul><br>";
                        item.Beneficios = detalles;
                    }
                }
                listaBeneficios = beneficio;
            }
            else
            {
                var beneficios = servicioBeneficioPrograma.ObtenerBeneficiosPGeneralTipo1V2(idPGeneral, codigoPais);
                if (beneficios.Count() > 0)
                {
                    foreach (var item in montosPorPais)
                    {
                        var items = beneficios.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                        string detalles = "<ul>";
                        if (items.Count() > 0)
                        {
                            foreach (var _item in items)
                            {
                                detalles += "<li>" + _item + "</li>";
                            }
                            detalles += "</ul><br>";
                            item.Beneficios = detalles;
                        }
                    }
                }
                listaBeneficios = beneficios;
            }
            return new ObtenerMontos2RespuestaDTO() { MontosPorPais = montosPorPais, ListaBeneficios = listaBeneficios };
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener monto
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <param name="codigoPais">Codigo de Pais</param>
        /// <returns>ObtenerMontos2RespuestaDTO</returns> 
        private ObtenerMontos2RespuestaDTO ObtenerMontos(int idPGeneral, int codigoPais)
        {
            var servicioMontoPago = new MontoPagoService(_unitOfWork);
            var servicioBeneficioPrograma = new ConfiguracionBeneficioProgramaGeneralService(_unitOfWork);

            var montos = servicioMontoPago.ObtenerMontosPorId(idPGeneral);
            var montosPorPais = montos.Where(s => s.Pais.Equals(codigoPais)).OrderBy(x => x.Paquete).ToList();

            var listaBeneficios = new List<BeneficioDTO>();

            if (montosPorPais.Count() == 0)
            {
                montosPorPais = montos.Where(s => s.Pais.Equals(0)).OrderBy(x => x.Paquete).ToList();
                if (montosPorPais.Where(x => x.Paquete == 0).ToList().Count() == 0)
                {
                    //tipo 1
                    var beneficios = servicioBeneficioPrograma.ObtenerBeneficiosPGeneralTipo1(idPGeneral);
                    foreach (var item in montosPorPais)
                    {
                        var items = beneficios.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                        string Detalles = "<ul>";
                        foreach (var item2 in items)
                        {
                            Detalles += "<li>" + item2 + "</li>";
                        }
                        Detalles += "</ul>";
                        item.Beneficios = Detalles;

                    }
                    listaBeneficios = beneficios;
                }
                else
                {
                    //tipo 2
                    var beneficio = servicioBeneficioPrograma.ObtenerBeneficiosPGeneralTipo2(idPGeneral);

                    foreach (var item2 in montosPorPais)
                    {
                        item2.Beneficios = beneficio.Titulo;
                    }
                    listaBeneficios.Add(beneficio);
                }
            }
            else
            {
                if (montosPorPais.Where(x => x.Paquete == 0).ToList().Count() == 0)
                {
                    var beneficios = servicioBeneficioPrograma.ObtenerBeneficiosPGeneralTipo1(idPGeneral, codigoPais);
                    foreach (var item in montosPorPais)
                    {
                        var items = beneficios.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                        string detalles = "<ul>";
                        foreach (var _item in items)
                        {
                            detalles += "<li>" + _item + "</li>";
                        }
                        detalles += "</ul><br>";
                        item.Beneficios = detalles;
                    }
                    listaBeneficios = beneficios;
                }
                else
                {
                    var beneficio = servicioBeneficioPrograma.ObtenerBeneficiosPGeneralTipo2(idPGeneral);
                    if (beneficio != null)
                    {
                        foreach (var item2 in montosPorPais)
                        {
                            item2.Beneficios = beneficio.Titulo;
                        }
                    }
                    listaBeneficios.Add(beneficio);
                }
            }
            return new ObtenerMontos2RespuestaDTO()
            {
                ListaBeneficios = listaBeneficios,
                MontosPorPais = montosPorPais
            };
        }




        private ObtenerMontos2RespuestaDTO ObtenerMontoPresentacionPrograma(int idPGeneral, int codigoPais)
        {
            var servicioMontoPago = new MontoPagoService(_unitOfWork);
            var servicioBeneficioPrograma = new ConfiguracionBeneficioProgramaGeneralService(_unitOfWork);

            var montos = servicioMontoPago.ObtenerMontosPorId(idPGeneral);
            var montosPorPais = montos.Where(s => s.Pais.Equals(codigoPais)).OrderBy(x => x.Paquete).ToList();

            var listaBeneficios = new List<BeneficioDTO>();

            if (montosPorPais.Count() == 0)
            {
                montosPorPais = montos.Where(s => s.Pais.Equals(0)).OrderBy(x => x.Paquete).ToList();
                var beneficio = servicioBeneficioPrograma.ObtenerBeneficiosPGeneralTipo1V2Internacional(idPGeneral);
                
                listaBeneficios = beneficio;
            }
            else
            {
                var beneficios = servicioBeneficioPrograma.ObtenerBeneficiosPGeneralTipo1V2(idPGeneral, codigoPais);
                
                listaBeneficios = beneficios;
            }
            return new ObtenerMontos2RespuestaDTO() { MontosPorPais = montosPorPais, ListaBeneficios = listaBeneficios };
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtienen los contenidos de los programas generales por modalidad
        /// </summary>
        /// <param name="modalidades"> Objeto Lista de tipo ModalidadProgramaDTO </param>
        /// <param name="initContenido"> Contenido Inicial </param>        
        /// <returns>Contenido Horarios: string </returns> 
        public string ObtenerContenidoHorarios(List<ModalidadProgramaDTO> modalidades, string initContenido, int idPGeneral)
        {
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var servicioExcepcionFrecuencia = new ExcepcionFrecuenciaPwService(_unitOfWork);

            string _filtroVista = string.Empty;

            var excepcionesFrecuencia = servicioExcepcionFrecuencia.ObtenerTodoProgramaGeneral().ToList();
            var padreEspecificoHijo = servicioPGeneral.ObtenerPadreHijoEspecificoV2(idPGeneral); //VERSION NUEVA
            var obtenerFrecuencia = servicioPGeneral.ObtenerFrecuenciasPorIdPGeneral(idPGeneral);
            var especificoSesion = servicioPGeneral.ObtenerSesionesProgramaGeneralValidadoVisualizacionAgenda(idPGeneral);
            var especificoSesionSinValidacionVisualizacion = servicioPGeneral.ObtenerSesionesPorProgramaGeneral(idPGeneral);

            var tipo = _unitOfWork.PGeneralParametroSeoPwRepository.ObtenerParametrosSEOPorIdPGeneral(idPGeneral).Where(w => w.NombreParametroSeo.Equals("description")).FirstOrDefault();
            string presencialDatos = "";
            string sincronicoDatos = "";
            string extraPresencial = "";
            string extraSincrono = "";

            var programaGeneral = modalidades.FirstOrDefault() == null && modalidades.Count() != 0 ? new ModalidadProgramaDTO { Pw_duracion = "0", NombrePG = "" } : modalidades.FirstOrDefault();
            if (programaGeneral != null)
            {
                if (programaGeneral.NombrePG.Contains("Curso") || programaGeneral.NombrePG.Contains("Programa"))
                {
                    //plantilla
                    presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p>El ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                    sincronicoDatos = "<p><strong>En la modalidad online sincrónica (clases en vivo):</strong></p><p> El ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                    extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                    extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicita tu cronograma.</i><p/>";
                }
                else
                {
                    if (tipo == null)
                    {
                        tipo = new PgeneralParametroSeoPwDTO();
                        tipo.Descripcion = "";
                    }
                    if (tipo.Descripcion.Contains("Curso"))
                    {
                        //plantilla
                        presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p> El Curso ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                        sincronicoDatos = "<p><strong>En la modalidad online sincrónica (clases en vivo):</strong></p><p> El Curso ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                        extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                        extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicita tu cronograma.</i><p/>";
                    }
                    if (tipo.Descripcion.Contains(" "))
                    {
                        //plantilla
                        presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p> El Programa ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                        sincronicoDatos = "<p><strong>En la modalidad online sincrónica (clases en vivo):</strong></p><p> El Programa ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                        extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                        extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicita tu cronograma.</i><p/>";
                    }
                }
            }

            var programaEspecifico = modalidades;
            string frecuencia = "";
            string todohtmlpresencial = this.ObtenerContenidoPresencial(programaEspecifico.Where(w => w.Tipo == "Presencial").ToList(), true, excepcionesFrecuencia, padreEspecificoHijo, obtenerFrecuencia, especificoSesion);
            if (todohtmlpresencial == "" || todohtmlpresencial == "NotFound")
            {
                presencialDatos = "";
                extraPresencial = "";
            }
            string todohtmlsincrono = this.ObtenerContenidoPresencial(programaEspecifico.Where(w => w.Tipo == "Online Sincronica").ToList(), false, excepcionesFrecuencia, padreEspecificoHijo, obtenerFrecuencia, especificoSesion);
            //Se creó la condición de retorno NotFound para validar en caso que no se haya configurado ninguna sesión con visualización en el portal para obtener todos los programas específicos
            if (todohtmlsincrono == "NotFound")
            {
                //En caso de no encontrar ningun horario buscamos con todos los programas específicos asociados
                todohtmlsincrono = this.ObtenerContenidoPresencial(programaEspecifico.Where(w => w.Tipo == "Online Sincronica").ToList(), false, excepcionesFrecuencia, padreEspecificoHijo, obtenerFrecuencia, especificoSesionSinValidacionVisualizacion);
            }
            if (todohtmlsincrono == "" || todohtmlsincrono == "NotFound")
            {
                sincronicoDatos = "";
                extraSincrono = "";
            }
            else
            {
                var sincrona = modalidades.Where(x => x.Tipo == "Online Sincronica").FirstOrDefault();
                if (sincrona.NombreESP.Contains("Curso"))
                {
                    var padre = padreEspecificoHijo.Where(w => w.IdPespecificoHijo == sincrona.IdPEspecifico).FirstOrDefault();
                    if (padre != null)
                        frecuencia = obtenerFrecuencia.Where(w => w.IdPEspecifico == padre.IdPespecificoHijo).Select(w => w.Nombre).FirstOrDefault();//this.ObtenerFrecuencia(padre.PEspecificoPadreId);
                    else
                        frecuencia = obtenerFrecuencia.Where(w => w.IdPEspecifico == sincrona.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();//this.ObtenerFrecuencia(sincrona.id);
                }
                else
                    frecuencia = frecuencia = obtenerFrecuencia.Where(w => w.IdPEspecifico == sincrona.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
            }
            string seContenido = initContenido;
            seContenido = presencialDatos + todohtmlpresencial + extraPresencial + sincronicoDatos + todohtmlsincrono + extraSincrono + seContenido;

            if (programaEspecifico.Count() != 0)
            {
                var sincronico = programaEspecifico.Where(x => x.Tipo.ToLower().Equals("online sincronica")).FirstOrDefault();
                if (sincronico != null)
                {
                    string temppe = seContenido.Replace("##DURACIONPE##", Convert.ToInt32(double.Parse(programaEspecifico.Where(x => x.Tipo.ToLower().Equals("online sincronica")).FirstOrDefault().Duracion, CultureInfo.InvariantCulture)) + " Horas").Replace("##DURACIONPG##", programaGeneral.Pw_duracion).Replace("##CURSODIPLOMA##", programaGeneral.NombrePG).Replace("##FRECUENCIA##", frecuencia);
                    temppe = temppe.Replace("®", "<sup>®</sup>");
                    seContenido = temppe;
                }
                else
                {
                    string temppe = seContenido.Replace("##DURACIONPE##", Convert.ToInt32(double.Parse(programaEspecifico.FirstOrDefault().Duracion, CultureInfo.InvariantCulture)) + " Horas").Replace("##DURACIONPG##", programaGeneral.Pw_duracion).Replace("##CURSODIPLOMA##", programaGeneral.NombrePG).Replace("##FRECUENCIA##", frecuencia); //Version 1 Para revertir
                    temppe = temppe.Replace("®", "<sup>®</sup>");
                    seContenido = temppe;
                }
            }
            else
            {
                if (seContenido != "" && programaGeneral != null)
                {
                    string temppe = seContenido.Replace("##DURACIONPE##", "" + " Horas").Replace("##DURACIONPG##", programaGeneral.Pw_duracion).Replace("##CURSODIPLOMA##", programaGeneral.NombrePG).Replace("##FRECUENCIA##", frecuencia);
                    temppe = temppe.Replace("®", "<sup>®</sup>");
                    seContenido = temppe;
                }
            }
            return seContenido;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener contenido presencial
        /// </summary>
        /// <param name="pEspecificos"> Objeto tipo ModalidadProgramaDTO </param>
        /// <param name="presencial"> Presencion </param>
        /// <param name="excepcionesFrecuencia"> Objeto tipo ExcepcionFrecuenciaPGeneralDTO </param>		
        /// <param name="padreEspecificoHijo"> Objeto tipo PadrePespecificoHijoDTO </param>		
        /// <param name="frecuenciaProgramas"> Objeto tipo FrecuenciaProgramaGeneralDTO </param>		
        /// <param name="especificoSesion"> Objeto tipo PEspecificoSesionDTO </param>		
        /// <returns>Contenido Presencial: strHTML </returns>
        private string ObtenerContenidoPresencial(List<ModalidadProgramaDTO> pEspecificos, bool presencial, List<ExcepcionFrecuenciaPGeneralDTO> excepcionesFrecuencia, List<PadrePespecificoHijoDTO> padreEspecificoHijo, List<FrecuenciaProgramaGeneralDTO> frecuenciaProgramas, List<PEspecificoSesionDTO> especificoSesion)
        {
            string horariosP1 = "<li>##DIA##: ##HORAINICIO## a ##HORAFIN## horas.</li>";
            string horariosP2 = "<li>##DIA##: ##HORAINICIO## a ##HORAFIN## - ##HORAINICIO2## a ##HORAFIN2## horas.</li>";
            string datosCiudad = "<p>En ##CIUDAD## con una frecuencia ##FRECUENCIA##:</p> <ul class='list'>##HORARIOS##</ul>";
            string datosSinCiudad = "<ul class='list'>##HORARIOS##</ul>";
            string[] arreglos = new string[pEspecificos.ToList().Count];
            int contador = 0;
            string strHTML = "";
            foreach (var item in pEspecificos)
            {
                //trae la lista de idpespecifico de excepciones
                var listaExcepciones = excepcionesFrecuencia.Select(w => w.IdPEspecifico).ToList();
                string frecuencia = "";
                if (listaExcepciones.Contains(item.IdPEspecifico))//lee tabla de excepciones
                {
                    frecuencia = "Diaria";
                }
                else //sigue flujo normal
                {
                    if (item.NombreESP.Contains("Curso"))
                    {
                        var padre = padreEspecificoHijo.Where(w => w.IdPespecificoHijo == item.IdPEspecifico).FirstOrDefault();
                        if (padre != null)
                            frecuencia = frecuenciaProgramas.Where(w => w.IdPEspecifico == padre.IdPespecificoPadre).Select(w => w.Nombre).FirstOrDefault();
                        else
                        {
                            frecuencia = frecuenciaProgramas.Where(w => w.IdPEspecifico == item.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
                        }
                    }
                    else
                    {
                        frecuencia = frecuenciaProgramas.Where(w => w.IdPEspecifico == item.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
                    }
                }
                List<PEspecificoSesionDTO> sesionestemp = new List<PEspecificoSesionDTO>();
                if (item.NombreESP.Contains("Curso"))
                {
                    var hijo = item;
                    //obtener las sesiones del pespcifico
                    sesionestemp = especificoSesion.Where(w => w.IdPEspecifico == hijo.IdPEspecifico && w.Predeterminado == true).ToList();
                }
                else
                {
                    //obtenemos un pespecifico
                    var hijo = padreEspecificoHijo.Where(w => w.IdPespecificoPadre == item.IdPEspecifico).ToList();
                    if (hijo.Count() == 0)
                    {
                        var hijotemp = item;
                        sesionestemp = especificoSesion.Where(w => w.IdPEspecifico == hijotemp.IdPEspecifico && w.Predeterminado == true).ToList();
                    }
                    else
                    {
                        foreach (var itemHijo in hijo)
                        {
                            var temporal = especificoSesion.Where(w => w.IdPEspecifico == itemHijo.IdPespecificoHijo && w.Predeterminado == true && w.Estado == true).ToList();
                            sesionestemp.AddRange(temporal);
                        }
                    }
                }
                //lista donde se almacenara las sesiones
                List<SesionTempDTO> lista = new List<SesionTempDTO>();
                //llenas las sesiones  dia horainicio horafin
                foreach (var item3 in sesionestemp.OrderBy(w => w.FechaHoraInicio))
                {
                    SesionTempDTO itemlista = new SesionTempDTO();

                    itemlista.Dia = new CultureInfo("es-PE", false).TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd"));
                    //itemlista.Dia
                    if (itemlista.Dia == "Monday")
                    {
                        itemlista.Dia = "Lunes";
                    }
                    else if (itemlista.Dia == "Tuesday")
                    {
                        itemlista.Dia = "Martes";
                    }
                    else if (itemlista.Dia == "Wednesday")
                    {
                        itemlista.Dia = "Miércoles";
                    }
                    else if (itemlista.Dia == "Thursday")
                    {
                        itemlista.Dia = "Jueves";
                    }
                    else if (itemlista.Dia == "Friday")
                    {
                        itemlista.Dia = "Viernes";
                    }
                    else if (itemlista.Dia == "Saturday")
                    {
                        itemlista.Dia = "Sábado";
                    }
                    else if (itemlista.Dia == "Sunday")
                    {
                        itemlista.Dia = "Domingo";
                    }
                    //Dia = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd"));
                    itemlista.Idciudad = this.GetIdDia(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd")));
                    itemlista.Horainicio = item3.FechaHoraInicio.Value.ToString("HH:mm");
                    itemlista.Veces = 1;
                    itemlista.Horafin = item3.FechaHoraInicio.Value.AddHours(Convert.ToDouble(item3.Duracion)).ToString("HH:mm");
                    itemlista.Ciudad = item.Ciudad;

                    lista.Add(itemlista);
                }
                lista = lista.Distinct().ToList();

                List<SesionTempDTO> listaFinal = new List<SesionTempDTO>();
                SesionTempDTO itemTempListaFinal = new SesionTempDTO();
                foreach (var item_lista in lista)
                {
                    if (!listaFinal.Any(w => w.Dia == item_lista.Dia && w.Ciudad == item_lista.Ciudad && w.Horainicio == item_lista.Horainicio && w.Horafin == item_lista.Horafin))
                        listaFinal.Add(item_lista);
                    else
                    {
                        itemTempListaFinal = listaFinal.Where(w => w.Dia == item_lista.Dia && w.Ciudad == item_lista.Ciudad && w.Horainicio == item_lista.Horainicio && w.Horafin == item_lista.Horafin).FirstOrDefault();
                        listaFinal.Remove(itemTempListaFinal);
                        itemTempListaFinal.Veces++;
                        listaFinal.Add(itemTempListaFinal);
                    }

                }
                lista = listaFinal;

                List<SesionTempDTO> listafinal = new List<SesionTempDTO>();

                foreach (var item_ in lista.OrderBy(w => w.Idciudad))
                {
                    var tempFechas = lista.Where(w => w.Dia == item_.Dia).ToList();
                    //valida
                    var contadorlista = lista.Where(w => w.Veces == 1).ToList().Count();
                    if (!listafinal.Any(w => w.Dia == tempFechas.FirstOrDefault().Dia))
                    {
                        if (tempFechas.Count() == 1)
                        {
                            if (listafinal == null)
                            {
                                listafinal = tempFechas;
                                listafinal.FirstOrDefault().Tipo = false;//tipo normal
                            }
                            else
                            {
                                tempFechas.FirstOrDefault().Tipo = false;
                                listafinal.Add(tempFechas.FirstOrDefault());
                            }
                        }
                        if (tempFechas.Count() == 2)
                        {
                            tempFechas = tempFechas.OrderByDescending(w => w.Veces).ToList();

                            if (Math.Abs(Convert.ToDateTime(tempFechas.First().Horainicio).Hour - Convert.ToDateTime(tempFechas.Last().Horainicio).Hour) < 2)
                            {
                                listafinal.Add(tempFechas.First());
                                continue;
                            }

                            if (tempFechas.First().Veces - tempFechas.Last().Veces > 5)
                            {
                                SesionTempDTO listafinaltempfinal = new SesionTempDTO();
                                listafinaltempfinal = tempFechas.First();
                                listafinaltempfinal.Tipo = false;
                                listafinal.Add(listafinaltempfinal);
                            }
                            else
                            {
                                tempFechas = tempFechas.OrderBy(w => w.Horainicio).ToList();

                                if (tempFechas.First().Dia == tempFechas.Last().Dia)
                                {
                                    SesionTempDTO listafinaltempfinal = new SesionTempDTO();

                                    listafinaltempfinal.Dia = tempFechas.First().Dia;
                                    listafinaltempfinal.Horainicio = tempFechas.First().Horainicio;
                                    listafinaltempfinal.Horafin = tempFechas.First().Horafin;
                                    listafinaltempfinal.Horainicio2 = tempFechas.Last().Horainicio;
                                    listafinaltempfinal.Horafin2 = tempFechas.Last().Horafin;
                                    listafinaltempfinal.Tipo = true;//tipo doble horario
                                    listafinal.Add(listafinaltempfinal);
                                }
                                else
                                {
                                    SesionTempDTO listafinaltempfinal = new SesionTempDTO();
                                    SesionTempDTO listafinaltemp2final = new SesionTempDTO();

                                    listafinaltempfinal.Dia = tempFechas.First().Dia;
                                    listafinaltempfinal.Horainicio = tempFechas.First().Horainicio;
                                    listafinaltempfinal.Horafin = tempFechas.First().Horafin;
                                    listafinaltempfinal.Horainicio2 = tempFechas.Last().Horainicio;
                                    listafinaltempfinal.Horafin2 = tempFechas.Last().Horafin;
                                    listafinaltempfinal.Tipo = false;//tipo doble horario

                                    listafinaltemp2final.Dia = tempFechas.Last().Dia;
                                    listafinaltemp2final.Horainicio = tempFechas.Last().Horainicio;
                                    listafinaltemp2final.Horafin = tempFechas.Last().Horafin;
                                    listafinaltemp2final.Horainicio2 = tempFechas.Last().Horainicio;
                                    listafinaltemp2final.Horafin2 = tempFechas.Last().Horafin;
                                    listafinaltemp2final.Tipo = false;//tipo doble horario

                                    listafinal.Add(listafinaltempfinal);
                                    listafinal.Add(listafinaltemp2final);
                                }
                            }
                        }
                        if (tempFechas.Count() == 3)
                        {
                            tempFechas = tempFechas.OrderByDescending(w => w.Veces).ToList();
                            if (tempFechas.First().Veces - tempFechas.Skip(1).Take(1).First().Veces > 5)
                            {
                                var _tempFechas = tempFechas;
                                tempFechas = new List<SesionTempDTO>();
                                tempFechas.Add(_tempFechas.First());
                            }
                            else
                            {
                                tempFechas.Remove(tempFechas.Last());
                            }
                            SesionTempDTO listafinaltemp = new SesionTempDTO();
                            SesionTempDTO listafinaltemp2 = new SesionTempDTO();
                            string day = tempFechas.First().Dia;
                            var tempa = tempFechas.Where(w => w.Dia == day).ToList();

                            if (tempa.Count == 1)
                            {
                                listafinaltemp.Dia = tempa.First().Dia;
                                listafinaltemp.Horainicio = tempa.First().Horainicio;
                                listafinaltemp.Horafin = tempa.First().Horafin;
                                listafinaltemp.Tipo = false;//tipo doble horario
                                listafinal.Add(listafinaltemp);
                            }
                            if (tempa.Count == 2)
                            {
                                if (Math.Abs(Convert.ToDateTime(tempa.First().Horainicio).Hour - Convert.ToDateTime(tempa.Last().Horainicio).Hour) < 2)
                                {
                                    tempa = tempa.OrderBy(w => w.Veces).ThenBy(w => w.Horainicio).ToList();
                                    listafinaltemp.Dia = tempa.First().Dia;
                                    listafinaltemp.Horainicio = tempa.First().Horainicio;
                                    listafinaltemp.Horafin = tempa.First().Horafin;
                                    listafinaltemp.Tipo = false;//tipo doble horario
                                    listafinal.Add(listafinaltemp);
                                }
                                else
                                {
                                    tempa = tempa.OrderBy(w => w.Horainicio).ToList();
                                    listafinaltemp.Dia = tempa.First().Dia;
                                    listafinaltemp.Horainicio = tempa.First().Horainicio;
                                    listafinaltemp.Horafin = tempa.First().Horafin;
                                    listafinaltemp.Horainicio2 = tempa.Last().Horainicio;
                                    listafinaltemp.Horafin2 = tempa.Last().Horafin;
                                    listafinaltemp.Tipo = true;//tipo doble horario
                                    listafinal.Add(listafinaltemp);
                                }
                            }
                        }
                        if (tempFechas.Count() > 3)
                        {
                            tempFechas = tempFechas.OrderByDescending(w => w.Veces).ToList();
                            SesionTempDTO listafinaltemp = new SesionTempDTO();

                            if (tempFechas.First().Veces - tempFechas.Skip(1).Take(1).First().Veces > 5)
                            {
                                SesionTempDTO listafinaltempfinal = new SesionTempDTO();
                                listafinaltempfinal = tempFechas.First();
                                listafinaltempfinal.Tipo = false;
                                listafinal.Add(listafinaltempfinal);
                            }
                            else
                            {
                                List<SesionTempDTO> tempvarios = new List<SesionTempDTO>();
                                tempvarios.Add(tempFechas.First());
                                tempvarios.Add(tempFechas.Skip(1).Take(1).First());
                                tempvarios = tempvarios.OrderBy(w => w.Horainicio).ToList();
                                listafinaltemp.Dia = tempvarios.First().Dia;
                                listafinaltemp.Horainicio = tempvarios.First().Horainicio;
                                listafinaltemp.Horafin = tempvarios.First().Horafin;
                                listafinaltemp.Horainicio2 = tempvarios.Skip(1).Take(1).First().Horainicio;
                                listafinaltemp.Horafin2 = tempvarios.Skip(1).Take(1).First().Horafin;
                                listafinaltemp.Tipo = true;//tipo doble horario
                                listafinal.Add(listafinaltemp);
                            }
                        }
                    }
                }
                foreach (var variable in listafinal)
                {
                    if (variable.Tipo == true)
                    {
                        arreglos[contador] += horariosP2.Replace("##DIA##", variable.Dia).Replace("##HORAINICIO##", variable.Horainicio).Replace("##HORAFIN##", variable.Horafin).Replace("##HORAINICIO2##", variable.Horainicio2).Replace("##HORAFIN2##", variable.Horafin2);
                    }
                    else
                    {
                        arreglos[contador] += horariosP1.Replace("##DIA##", variable.Dia).Replace("##HORAINICIO##", variable.Horainicio).Replace("##HORAFIN##", variable.Horafin);
                    }
                }
                if (presencial == true)
                    strHTML += datosCiudad.Replace("##CIUDAD##", item.Ciudad).Replace("##FRECUENCIA##", frecuencia).Replace("##HORARIOS##", arreglos[contador]);
                else
                    strHTML += datosSinCiudad.Replace("##HORARIOS##", arreglos[contador]);
                contador++;
            }
            return strHTML;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id segun el Nombre del Dia
        /// </summary>
        /// <param name="dia"> Nombre del Dia </param>		
        /// <returns> int </returns>
        private int GetIdDia(string dia)
        {
            switch (dia)
            {
                case "Lunes":
                    return 1;
                case "Martes":
                    return 2;
                case "Miércoles":
                    return 3;
                case "Jueves":
                    return 4;
                case "Viernes":
                    return 5;
                case "Sábado":
                    return 6;
                case "Domingo":
                    return 7;
                default:
                    return 0;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener contenido tarifario
        /// </summary>
        /// <param name="tarifarios"> Objeto tipo Lista TarifarioDetalleAgendaDTO </param>	
        /// <returns>string</returns> 
        public string ObtenerContenidoTarifario(List<TarifarioDetalleAgendaDTO> tarifarios)
        {
            string contenido = "<table class='table table-bordered'><thead class='text-center' style='background: #ffd800;'><tr><th>Nº</th><th>CONCEPTO</th><th>DESCRIPCION</th><th> MONTO PERU </th><th>MONTO COLOMBIA</th><th>MONTO BOLIVIA</th><th>MONTO EXTRANJERO</th></tr></thead>";
            contenido = contenido + "<tbody>";
            int contador = 1;
            foreach (var item in tarifarios)
            {
                contenido = contenido + "<tr>";

                contenido = contenido + "<td>" + contador + "</td>";
                contenido = contenido + "<td>" + item.Concepto + "</td>";
                contenido = contenido + "<td>" + item.Descripcion + "</td>";
                contenido = contenido + "<td class='text-center'><b><span>" + item.MontoPeru + "</span></b></td>";
                contenido = contenido + "<td class='text-center'><b><span>" + item.MontoColombia + "</span></b></td>";
                contenido = contenido + "<td class='text-center'><b><span>" + item.MontoBolivia + "</span></b></td>";
                contenido = contenido + "<td class='text-center'><b><span>" + item.MontoExtranjero + "</span></b></td>";
                contenido = contenido + "</tr>";
                contador++;
            }

            contenido = contenido + "</tbody> </table>";
            return contenido;

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Cargar Resumen Programas version 2
        /// </summary>
        /// <param name="filtros"> Diccionario de Filtros </param>	
        /// <returns>  </returns> 
        public List<ResumenProgramaV2DTO> CargarResumenProgramasV2(Dictionary<string, string> filtros)
        {
            try
            {
                var servicioPGeneral = new PGeneralService(_unitOfWork);
                var servicioDocumentoAgenda = new DocumentoAgendaService(_unitOfWork);

                var listaResumenPrograma = servicioPGeneral.ObtenerResumenProgramaV2(filtros);
                int idPais = Convert.ToInt32(filtros["codigoPais"]);
                var listaResumenProgramatemporal = listaResumenPrograma.Where(x => x.IdPais == idPais).ToList();
                if (listaResumenProgramatemporal.Count == 0)
                {
                    listaResumenPrograma = listaResumenPrograma.Where(x => x.IdPais == 0).ToList(); //Internacional
                }
                else
                {
                    listaResumenPrograma = listaResumenProgramatemporal;
                }
                var listaResumenProgramaAgrupado = listaResumenPrograma.GroupBy(x => new { x.IdPrograma, x.NombrePrograma, x.DuracionPrograma, x.IdArea, x.IdSubArea }).Select(x => new MontoProgramaAgrupadoDTO
                {
                    IdPrograma = x.Key.IdPrograma,
                    IdArea = x.Key.IdArea,
                    IdSubArea = x.Key.IdSubArea,
                    NombrePrograma = x.Key.NombrePrograma,
                    Duracion = x.Key.DuracionPrograma,
                    MontoDetalle = x.GroupBy(y => y.Descripcion).Select(y => new MontoProgramaDetalleDTO
                    {
                        Version = y.Key,
                        VersionDetalle = y.Select(z => new MontoProgramaVersionDetalle
                        {
                            IdTipoPago = z.IdTipoPago,
                            TipoPago = z.TipoPago,
                            SimboloMoneda = z.SimboloMoneda,
                            Matricula = z.Matricula,
                            Cuotas = z.Cuotas,
                            NroCuotas = z.NroCuotas
                        }).OrderByDescending(a => a.TipoPago).ToList()
                    }).ToList()
                }).ToList();

                foreach (var item in listaResumenProgramaAgrupado)
                {
                    var certificacionV2 = servicioDocumentoAgenda.ObtenerListaSeccionDocumentoProgramaGeneral(item.IdPrograma);
                    if (certificacionV2 == null || certificacionV2.Count == 0 || !certificacionV2.Any(a => a.Seccion.Contains("Certificacion")))
                    {
                        var certificacionV1 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSecciones(item.IdPrograma);
                        if (certificacionV1 != null && certificacionV1.Count > 0)
                            item.SeccionCertificadoV1 = certificacionV1.Where(x => x.Titulo.Contains("Certificación")).FirstOrDefault();
                    }
                    else
                    {
                        item.SeccionCertificadoV2 = certificacionV2.Where(a => a.Seccion.Contains("Certificacion")).FirstOrDefault();
                    }
                }
                var resumenProgramasV2 = ObtenerResumenProgramaHTML(listaResumenProgramaAgrupado);
                return resumenProgramasV2;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private List<ResumenProgramaV2DTO> ObtenerResumenProgramaHTML(List<MontoProgramaAgrupadoDTO> lista)
        {
            try
            {
                List<ResumenProgramaV2DTO> listaNueva = new List<ResumenProgramaV2DTO>();
                foreach (var item in lista)
                {
                    ResumenProgramaV2DTO obj = new ResumenProgramaV2DTO();
                    obj.IdArea = item.IdArea;
                    obj.IdSubArea = item.IdSubArea;
                    obj.NombrePrograma = item.NombrePrograma;
                    obj.Duracion = item.Duracion;
                    var inversion = "";
                    foreach (var inv in item.MontoDetalle)
                    {
                        inversion += "<strong>" + inv.Version + "</strong>";
                        foreach (var det in inv.VersionDetalle)
                        {
                            inversion += "<ul>";
                            inversion += "<li><strong>" + det.TipoPago + ": </strong>";
                            if (det.TipoPago.Equals("Contado"))
                            {
                                inversion += det.SimboloMoneda + " " + det.Matricula + ".";
                            }
                            if (det.TipoPago.Equals("Crédito"))
                            {
                                if (det.NroCuotas != null && det.Cuotas != null)
                                {
                                    inversion += "1 Cuota inicial de " + det.SimboloMoneda + " " + det.Matricula + " y " + det.NroCuotas + " cuotas mensuales desde " + det.SimboloMoneda + " " + det.Cuotas + ".";
                                }
                            }
                            inversion += "</li>";
                            inversion += "</ul>";
                        }
                    }
                    obj.Inversion = inversion;
                    var certificacion = "";
                    if (item.SeccionCertificadoV2 != null)
                    {
                        foreach (var contenido in item.SeccionCertificadoV2.DetalleSeccion)
                        {
                            certificacion += "<h5><strong><b>" + contenido.Titulo + "</b></strong></h5>";
                            certificacion += "<p>" + contenido.Cabecera + "</p>";
                            certificacion += "<ul type='disc'>";
                            foreach (var contenidoSeccion in contenido.DetalleContenido)
                            {
                                certificacion += "<li>" + contenidoSeccion + "</li>";
                            }
                            certificacion += "</ul>";
                            certificacion += "<p>" + contenido.PiePagina + "</p>";
                        }
                    }
                    else
                    {
                        if (item.SeccionCertificadoV1 != null)
                        {
                            certificacion += item.SeccionCertificadoV1.Contenido;
                        }

                    }
                    obj.Certificacion = certificacion;
                    listaNueva.Add(obj);
                }
                return listaNueva;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Carga la informacion obtenida de la DB 
        /// </summary>
        /// <param name="preguntaFrecuentePGeneralDTO2"> PreguntaFrecuentePGeneralDTO2</param>
        /// <returns>PreguntaFrecuenteSeccionesDTO</returns>
        public List<PreguntaFrecuenteSeccionesDTO> CargarInformacionProgramaChange(List<PreguntaFrecuentePGeneralRespuestaDTO> preguntaFrecuentePGeneralDTO)
        {
            var finalPreguntas = new List<PreguntaFrecuenteSeccionesDTO>();
            var a = preguntaFrecuentePGeneralDTO;
            if (preguntaFrecuentePGeneralDTO != null)
            {
                finalPreguntas = (from p in preguntaFrecuentePGeneralDTO
                                  group p by new
                                  {
                                      IdSeccion = p.IdSeccion,
                                      Nombre = p.Nombre
                                  } into g
                                  select new PreguntaFrecuenteSeccionesDTO
                                  {
                                      IdSeccion = g.Key.IdSeccion.Value,
                                      Nombre = g.Key.Nombre,

                                      Contenido = g.Select(o => new PreguntaFrecuenteRespuestasDTO
                                      {
                                          Pregunta = o.Pregunta,
                                          Respuesta = o.Respuesta
                                      }).ToList()
                                  }).ToList();

            }
            return finalPreguntas;
        }
        public List<PreguntaFrecuenteSeccionesDTO> CargarInformacionPrograma(List<PreguntaFrecuentePGeneralRespuestaDTO> repositorioPreguntaFrecuente)
        {
            List<PreguntaFrecuenteSeccionesDTO> preguntasFrecuente = new();
            if (repositorioPreguntaFrecuente != null)
            {
                preguntasFrecuente = (from p in repositorioPreguntaFrecuente
                                      group p by new
                                      {
                                          p.IdPrograma,
                                          p.IdSeccion,
                                          p.Nombre
                                      } into g
                                      select new PreguntaFrecuenteSeccionesDTO
                                      {
                                          IdPrograma = g.Key.IdPrograma.GetValueOrDefault(),
                                          IdSeccion = g.Key.IdSeccion.GetValueOrDefault(),
                                          Nombre = g.Key.Nombre,
                                          Contenido = g.Select(o => new PreguntaFrecuenteRespuestasDTO
                                          {
                                              Pregunta = o.Pregunta,
                                              Respuesta = o.Respuesta
                                          }).ToList()
                                      }).ToList();

            }
            return preguntasFrecuente;
        }


        public CargarInformacionProgramaRespuestaDTO CargarInformacionProgramaSpeech(int idPGeneral, int codigoPais, int idMatriculaCabecera, int idOportunidad)
        {
            var servicioConfiguracionBeneficio = new ConfiguracionBeneficioProgramaGeneralService(_unitOfWork);
            var servicioDocumentoAgenda = new DocumentoAgendaService(_unitOfWork);
            IPEspecificoService servicioPEspecifico = new PEspecificoService(_unitOfWork);
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var servicioOrigen = new OrigenService(_unitOfWork);



            //nueva logica
            var ProgramaPadre = _unitOfWork.PGeneralRepository.ProgramaGeneralPadre(idPGeneral);
            var programaTecnico = _unitOfWork.PGeneralRepository.ProgramaGeneralEsTecnico(idPGeneral);

            string contenidoStructuraTecnico = "";
            if (programaTecnico)
            {
                if (ProgramaPadre)
                {
                    var listaCursosHijo = _unitOfWork.PGeneralRepository.ListaCursosHijoPorIdPGeneral(idPGeneral);
                    foreach (var item in listaCursosHijo)
                    {

                        contenidoStructuraTecnico += "<h5><strong>" + item.Curso + "</strong></h5>";
                        var duracionCurso = _unitOfWork.PGeneralRepository.ObtenerDuracionCursoHijo(item.IdHijo);
                        var CursosHijo = _unitOfWork.PGeneralRepository.ContenidoEstructuraHijoPadre(item.IdHijo);

                        contenidoStructuraTecnico += "<ul type='disc'>";
                        var listaContenidoCurso = CursosHijo.GroupBy(x => x.Contenido).Select(x => x.First()).ToList();
                        foreach (var contenidoCurso in listaContenidoCurso)
                        {
                            contenidoStructuraTecnico += "<li>&nbsp;&nbsp;&nbsp;" + contenidoCurso.Contenido + "</li>";
                        }
                        contenidoStructuraTecnico += "</ul>";
                    }
                }
            }
            var estructuraTecnico = contenidoStructuraTecnico;

            var lista = servicioDocumentoAgenda.ObtenerInformacionProgramaGeneralSpeech(idPGeneral);
            var listaPiePagina = lista.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("beneficios")).FirstOrDefault();
            var addPiePagina = "";
            if (listaPiePagina != null)
            {
                addPiePagina = "<p>" + listaPiePagina.DetalleSeccion[0].PiePagina + "</p>";
                lista.Remove(listaPiePagina);
            }
            //Obtiene HTML y adapta los datos
            var seccionesV2Ordenado = servicioDocumentoAgenda.GenerarHTMLProgramaGeneralDocumentoSeccion(lista);
            foreach (var item in seccionesV2Ordenado)
            {
                string temporal = Regex.Replace(item.Contenido, "&bull;", "");
                string temporal2 = Regex.Replace(temporal, "&nbsp;", "");

                if (LimpiarCadena(item.Seccion).ToLower() != "estructura curricular" && LimpiarCadena(item.Seccion).ToLower() != "beneficios" && LimpiarCadena(item.Seccion).ToLower() != "certificacion" && LimpiarCadena(item.Seccion).ToLower() != "prerrequisitos" && LimpiarCadena(item.Seccion).ToLower() != "expositores")
                {
                    string temp3 = Regex.Replace(temporal2, "<br />", "<br /></li>");
                    string temp4 = Regex.Replace(temp3, "<br/>", "<br /></li>");
                    string temp5 = Regex.Replace(temp4, "<ul type='disc'><li>", "");

                    if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                    {
                        string temp6 = Regex.Replace(temp5, "<h5><strong><b>" + item.Seccion + "</b></strong></h5>", "");
                        item.Contenido = temp6;
                    }

                }
                else
                {
                    if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                    {
                        string temp3 = Regex.Replace(temporal2, "<br />", "<br /></li>");
                        string temp4 = Regex.Replace(temp3, "<br/>", "<br /></li>");
                        string temp5 = Regex.Replace(temp4, "<ul type='disc'><li>", "");

                        if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                        {
                            string temp6 = Regex.Replace(temp5, "<h5><strong><b>" + item.Seccion + "</b></strong></h5>", "");
                            item.Contenido = temp6;
                        }
                    }
                    else
                    {
                        string temp3 = Regex.Replace(temporal2, "<h5><strong><b>", "<h4>");
                        string temp4 = Regex.Replace(temp3, "</b></strong></h5>", "</h4>");

                        item.Contenido = temp4;
                    }

                }
                if (item.Contenido == null || item.Contenido == "")
                {
                    item.Seccion = "";
                }
            }

            var estructura = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("estructura curricular")).FirstOrDefault();
            var certificacion = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("certificacion")).FirstOrDefault();

            //Seccion Descripcion Estructura Curricular y Certificacion
            var concatenarEstructura = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("descripcion estructura")).FirstOrDefault();
            //var concatenarCertificacion = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("descripcion certificacion")).FirstOrDefault();
            if (concatenarEstructura != null && estructura != null)
            {
                estructura.Contenido = estructura.Contenido + concatenarEstructura.Contenido;
                seccionesV2Ordenado.Remove(concatenarEstructura);
            }
            var refuerzoConfianza = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("refuerzo de la confianza")).FirstOrDefault();
            var demostraciónValor = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("demostracion de valor")).FirstOrDefault();
            var garantiaPrograma = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("garantia de programa")).FirstOrDefault();
            var limitaciones = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("limitaciones")).FirstOrDefault();
            var apectosDiferenciadores = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("aspectos diferenciadores")).FirstOrDefault();
            //SE COMENTA ESTA PARTE PORQUE SE ESTABA MOSTRANDO DUPLICADO LA CERTIFICACION EN LOS CORREOS E INFORMACION DEL CURSO EN LA AGENDA, YA EN CERTIFICACION TRAE EL CONTENIDO DE DESCRIPCION DE CERTIFICACION
            //if (concatenarCertificacion != null && certificacion != null)
            //{
            //    certificacion.Contenido = certificacion.Contenido + concatenarCertificacion.Contenido;
            //    seccionesV2Ordenado.Remove(concatenarCertificacion);
            //}

            // Elimina Data de Video innecesarios
            var deleteVideos = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower().Equals("video") || LimpiarCadena(x.Seccion).ToLower().Equals("vista previa") || LimpiarCadena(x.Seccion.ToLower()).Equals("video de presentacion")).ToList();
            if (deleteVideos != null)
            {
                foreach (var item in deleteVideos)
                {
                    seccionesV2Ordenado.Remove(item);
                }
            }

            //Ordena data para hacer el display de forma predeterminada
            List<ProgramaGeneralSeccionAnexosHTMLDTO> seccionesV2 = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
            string[] listaTituloV1Orden = { "refuerzo de la confianza", "objetivos", "estructura curricular", "certificacion", "metodología online de este programa", "modalidad", "inversion", "metodologia del programa" };
            string[] listaTituloV2Orden = { "refuerzo de la confianza", "objetivos", "estructura curricular", "certificación", "metodolog&#237;a online de este programa", "Modalidad", "Inversion", "Metodolog&#237;a del programa" };

            for (var i = 0; i < listaTituloV1Orden.Length; i++)
            {
                var ordenarTempV2 = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena(listaTituloV2Orden[i])).FirstOrDefault();
                if (ordenarTempV2 != null)
                {
                    seccionesV2.Add(ordenarTempV2);
                }
                else
                {
                    var ordenarTempV1 = seccionesV2Ordenado.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena(listaTituloV1Orden[i])).FirstOrDefault();
                    if (ordenarTempV1 != null)
                    {
                        seccionesV2.Add(ordenarTempV1);
                    }
                }
            }

            // VALIDACION Y CREACION DE Items en Objeto para que no caiga en NULL
            var secciones = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
            ProgramaGeneralSeccionAnexosHTMLDTO modalidadAdd = new ProgramaGeneralSeccionAnexosHTMLDTO()
            {
                Seccion = "Modalidades",
                Contenido = "",
            };

            var flagBeneficio = false;
            var flagInversion = false;
            foreach (var item in seccionesV2)
            {
                if (item.Seccion == "Inversion")
                {
                    flagInversion = true;
                }

                if (item.Seccion == "Inversión")
                {
                    item.Seccion = "Inversion";
                    flagInversion = true;
                }

            }


            secciones.AddRange(seccionesV2);

            //Quito a Cetpro los expositores
            var programaGeneralCetpro = _unitOfWork.PGeneralRepository.ObtenerAreaSubAreaPorIdPGeneral(idPGeneral);
            var areaCapacitacionCetpro = _unitOfWork.AreaCapacitacionRepository.ObtenerPorId(programaGeneralCetpro.IdArea);
            if (areaCapacitacionCetpro.Nombre == "CETPRO")
            {
                var seccionExpositor = secciones.Where(w => w.Seccion == "Expositores").FirstOrDefault();
                if (seccionExpositor != null)
                {
                    secciones.Remove(seccionExpositor);
                }
            }

            //Logica de Montos de V1
            var seccionesBeneficiosInversion = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();

            var montosBeneficios = ObtenerMontos2(idPGeneral, codigoPais);
            var montos = montosBeneficios.MontosPorPais;

            var contador = 0;
            if (montos.Count() < 1)
            {
                montosBeneficios = ObtenerMontos(idPGeneral, codigoPais);
                montos = montosBeneficios.MontosPorPais;
            }

            foreach (var item in montos)
            {
                if (item.Beneficios == "<ul></ul><br>" || item.Beneficios == null || item.Beneficios == "null")
                {
                    contador++;
                }
            }
            if (contador == montos.Count())
            {
                montosBeneficios = ObtenerMontos(idPGeneral, codigoPais);
                montos = montosBeneficios.MontosPorPais;
            }
            var montosb = montos.Where(s => s.Beneficios != null).ToArray();
            var montopagado = _unitOfWork.MontoPagoCronogramaRepository.ObtenerMontoPagado(idMatriculaCabecera, idOportunidad);

            //Obtencion de Modalidades VPortal
            ModalidadProgramaDTO cambiarModelo;
            List<ModalidadProgramaDTO> modalidadGeneral = new List<ModalidadProgramaDTO>();
            var modalidadGeneralPortal = servicioPEspecifico.ObtenerFechaInicioProgramaTodos(idPGeneral);
            if (modalidadGeneralPortal == null)
            {
                modalidadGeneralPortal = new List<PEspecificoPorIdPGeneral>();
            }
            var programaGeneral = servicioPGeneral.ObtenerPGeneralAtributosPrincipalesPorId(idPGeneral);
            //Obtencion de Modalidades V2
            if (modalidadGeneralPortal != null)
            {
                foreach (var modalidadPortal in modalidadGeneralPortal)
                {
                    cambiarModelo = new ModalidadProgramaDTO()
                    {
                        Tipo = modalidadPortal.Tipo,
                        Ciudad = modalidadPortal.Ciudad,
                        TipoCiudad = "",
                        FechaHoraInicio = modalidadPortal.FechaInicioTexto,
                        NombrePG = programaGeneral.Nombre,
                        IdPEspecifico = modalidadPortal.Id,
                        NombreESP = modalidadPortal.Nombre,
                        NombreCentroCosto = modalidadPortal.CentroCosto,
                        Duracion = modalidadPortal.Duracion,
                        Pw_duracion = programaGeneral.PwDuracion,
                        FechaReal = modalidadPortal.FechaInicio
                    };
                    modalidadGeneral.Add(cambiarModelo);
                }
            }

            var modalidadAsincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Asincronica") && s.Ciudad.Equals("LIMA")).ToList();
            if (modalidadAsincronica.Count() == 0)
            {
                modalidadAsincronica = modalidadGeneral.Where(s => LimpiarCadena(s.Tipo).Equals("Online Asincronica")).ToList();
            }
            var modalidadSincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Sincronica")).ToList();
            var modalidadPresencial = modalidadGeneral.Where(s => LimpiarCadena(s.Tipo).Equals("Presencial")).ToList();
            List<ModalidadProgramaDTO> PruebaModalidad = new List<ModalidadProgramaDTO>();
            PruebaModalidad.AddRange(modalidadAsincronica);
            PruebaModalidad.AddRange(modalidadSincronica);
            PruebaModalidad.AddRange(modalidadPresencial);

            var modalidades = new List<ModalidadProgramaDTO>();
            modalidades.AddRange(PruebaModalidad);

            var modalidadesV2 = new List<ModalidadProgramaSincronicoDTO>();

            var tarifarios = servicioOrigen.ObtenerTarifariosDetallesAgenda(idMatriculaCabecera);
            secciones.Add(modalidadAdd);


            if (demostraciónValor == null)
            {
                demostraciónValor = new ProgramaGeneralSeccionAnexosHTMLDTO()
                {
                    Seccion = "Demostracion de Valor",
                    Contenido = "",
                };
            }
            if (garantiaPrograma == null)
            {
                garantiaPrograma = new ProgramaGeneralSeccionAnexosHTMLDTO()
                {
                    Seccion = "Garantia de Programa",
                    Contenido = "",
                };
            }
            if (limitaciones == null)
            {
                limitaciones = new ProgramaGeneralSeccionAnexosHTMLDTO()
                {
                    Seccion = "Limitaciones",
                    Contenido = "",
                };
            }
            if (apectosDiferenciadores == null)
            {
                apectosDiferenciadores = new ProgramaGeneralSeccionAnexosHTMLDTO()
                {
                    Seccion = "Aspectos Diferenciadores",
                    Contenido = "",
                };
            }



            secciones.Add(demostraciónValor);
            secciones.Add(garantiaPrograma);
            secciones.Add(limitaciones);
            secciones.Add(apectosDiferenciadores);
            if (flagInversion == false)
            {
                ProgramaGeneralSeccionAnexosHTMLDTO inversionAdd = new ProgramaGeneralSeccionAnexosHTMLDTO()
                {
                    Seccion = "Inversion",
                    Contenido = "",
                };
                secciones.Add(inversionAdd);
            }
            var seccionMontos = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("inversion")).FirstOrDefault();
            var seccionExpositores = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("expositores")).FirstOrDefault();

            if (seccionMontos != null)
            {
                if (montos.Count() > 0)
                {
                    seccionMontos.Contenido = "<table class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Precio Contado</strong></td><td><strong>Precio Credito</strong></td></tr>"
                        + string.Join("", montos.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.InversionContado + "</td><td>" + s.InversionCredito + "</td></tr>").ToArray()) + "</table>";

                    seccionesBeneficiosInversion.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
                    {
                        Seccion = seccionMontos.Seccion,
                        Contenido = "<table id=\"tablebeneficios\" class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Precio Contado</strong></td><td><strong>Precio Credito</strong></td></tr>"
                        + string.Join("", montos.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.InversionContado + "</td><td>" + s.InversionCredito + "</td></tr>").ToArray()) + "</table>"
                    });
                }
                else
                {
                    var itemToRemove = secciones.Single(r => LimpiarCadena(r.Seccion) == "Inversion");
                    secciones.Remove(itemToRemove);
                }
            }



            string fila = "";
            string descripcionSinVersion = "";
            string descripcionBasica = "";
            string descripcionProfesional = "";
            string descripcionGerencial = "";
            bool flagSinVersion = false;
            bool flagVersionBasica = false;
            bool flagVersionProfesional = false;
            bool flagVersionGerencial = false;




            //Montos Complementarios
            seccionesBeneficiosInversion.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
            {
                Seccion = "Monto Actual",
                Contenido = "<table id=\"tablemontomatricula\" class=\"table table-hover \"><tr><td><strong>Moneda</strong></td><td><strong>Costo Original</strong></td><td><strong>Descuento</strong></td><td><strong>Porcentaje Descuento</strong></td><td><strong>Costo Final</strong></td></tr>"
                + string.Join("", montopagado.Select(s => "<tr><td>" + s.Moneda + "</td><td>" + s.CostoOriginal + "</td><td>" + s.Descuento + "</td><td>" + s.PorcentajeDescuento + "</td><td>" + s.CostoFinal + "</td></tr>").ToArray()) + "</table>"
            });

            var etiquetaFormasPago = "<table style='font-family:arial,sans-serif;border-collapse:collapse;width:100%'><tr><th style='background-color:#4584a7;color:#fff;border:1px solid #d7d7d7;padding:10px'>Formas de Pago</th></tr><tr><td style='border:1px solid #d7d7d7;padding:10px'><h2>PERU</h2><p><span style='font-size:13.3333px'><strong>1.</strong>Pago por Tarjeta Visa, Mastercard, Amex, Diners a trav&eacute;s de&nbsp;</span><span style='font-size:13.3333px'>nuestra pagina web:&nbsp;</span><a href='https://bsginstitute.com/Cuenta' style='font-size:13.3333px'>https://bsginstitute.com/</a></p><p>&nbsp;</p><p><strong>2.</strong><span style='font-size:13.3333px'>Pago&nbsp;</span><span style='font-size:13.3333px'>mediante una transferencia interbancaria(Per&uacute;) de su banco a nuestro banco.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Raz&oacute;n social: BS Grupo SAC</span></li><li style='text-align:left'><span style='font-size:10pt'>Raz&oacute;n Comercial: BSG INSTITUTE</span></li><li style='text-align:left'><span style='font-size:10pt'>Ruc: 20454870591</span></li><li style='text-align:left'><span style='font-size:10pt'>Direcci&oacute;n: Pasaje Romaña-Calle 2 Nro 107 Urb. Le&oacute;n XIII Arequipa-Arequipa-Cayma</span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>215-1863341-0-42*</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001863341042-20</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta corriente en D&oacute;lares</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>215-1870934-1-48*</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001870934148-23</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco BBVA Continental</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>0011-0220-01-00131737</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>011 220 000100131737 17</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco Scotiabank</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>000-4654102</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>009-313-000004654102-85</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>* N&uacute;mero de cuenta autorizada solo para transferencias de empresas, no est&aacute; habilitado para dep&oacute;sitos en ventanilla.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>&nbsp;</span></p><p><h2>EXTRANJERO</h2><strong>1.</strong><span style='font-size:13.3333px'>Pago&nbsp;</span><span style='font-size:13.3333px'>mediante una transferencia bancaria Internacional de su banco a nuestro banco.</span></p><p style='margin-left:30px'><span style='font-size:10pt'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BSG Institute S.A.C</span></li><li><span style='font-size:10pt'>RUC: 20454870591</span></li><li><span style='font-size:10pt'>Address: Pasaje Romaña-Calle 2 Nro 107 Urb. Le&oacute;n XIII Arequipa-Arequipa-Cayma</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos del banco</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Bank: Banco de Cr&eacute;dito del Per&uacute;</span></li><li><span style='font-size:10pt'>Address: Street San Juan de Dios 123, Arequipa, Per&uacute;</span></li><li><span style='font-size:10pt'>Swift Code: BCPLPEPL</span></li><li><span style='font-size:10pt'>Account Number USD$: 215-1870934-1-48</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>COLOMBIA</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>Para transferencias Nacionales Colombia:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BS GRUPO COLOMBIA SAS</span></li><li><span style='font-size:10pt'>NIT: 900776296</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuenta de Ahorro en Pesos</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Datos del banco: Bancolombia</span></li><li><span style='font-size:10pt'>Numero de Cuenta: 65231918412</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>BOLIVIA</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>Para transferencias Nacionales Bolivia:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BSG INSTITUTE BOLIVIA</span></li><li><span style='font-size:10pt'>NIT: 376053024</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuenta corriente en Bolivianos</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Datos del banco: Banco de Credito Bolivia</span></li><li><span style='font-size:10pt'>Numero de Cuenta Bolivianos: 701-5051921-3-41</span></li><li><span style='font-size:10pt'>Numero de Cuenta D&oacute;lares: 701-5041553-2-04</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>MEXICO</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>1.	Pagos aceptado con Tarjeta Visa, Mastercard, American Express y Carnet (Débito y Crédito) a través de nuestra página web<a href='https://bsginstitute.com/Cuenta' style='font-size:13.3333px'>https://bsginstitute.com/</a></span></p><p style='margin-left:30px'><span style='font-size:10pt'>2. Pagos mediante D&eacute;positos Bancarios y Transferencia por SPEI*</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Raz&oacute;n Social: BSG Institute M&eacute;xico S.A. de C.V.</span></li><li><span style='font-size:10pt'>Raz&oacute;n Comercial: BSG Institute</span></li><li><span style='font-size:10pt'>RFC: BIM210209H26</span></li><li><span style='font-size:10pt'>Direcci&oacute;n: Montecito No. 38, Piso 33, Of. 4.</span></li><li><span style='font-size:10pt'>Edificio: World Trade Center – WTC</span></li><li><span style='font-size:10pt'>Colonia N&aacute;poles</span></li><li><span style='font-size:10pt'>Ciudad de M&eacute;xico</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuentas</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Cuenta en Pesos Mexicanos en el BBVA Bancomer</span></li><li><span style='font-size:10pt'>N&uacute;mero de Cuenta: 0116490468</span></li><li><span style='font-size:10pt'>Cuenta CLABE Interbancaria: 012180001164904687</span></li></ul><ul style='margin-left:30px'><li><span style='font-size:10pt'>Cuenta en Dólares Americanos en el BBVA Bancomer</span></li><li><span style='font-size:10pt'>N&uacute;mero de Cuenta: 0116490522</span></li><li><span style='font-size:10pt'>Cuenta CLABE Interbancaria: 012180001164905220</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>3. Liga (Enlace) de Pago</span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Deber&aacute; solicitar a su Asesor el envío de la liga de pago por correo electrónico, una vez recibido lo conduce a una p&aacute;gina web para completar el pago.</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* El pago podr&iacute;a est&aacute;r sujeto al cobro de comisiones del banco emisor, por favor verificar previamente.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p></td></tr></table>";
            var etiquetaTarifarios = ObtenerContenidoTarifario(tarifarios);
            var seccionModalidades = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("modalidades")).FirstOrDefault();

            foreach (var item in modalidades)
            {
                if (item.FechaReal != null)
                {
                    if (item.Tipo.ToLower() == "online asincronica")
                    {
                        var fechaReal = item.FechaReal?.ToString("MMM yyy", CultureInfo.CreateSpecificCulture("es-ES"));
                        fechaReal = fechaReal.Substring(0, 1).ToUpper() + fechaReal.Substring(1);
                        item.FechaHoraInicio = fechaReal;
                    }
                    else
                    {
                        var fechaReal = item.FechaReal?.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("es-ES"));
                        CultureInfo cultura = CultureInfo.CurrentCulture;
                        TextInfo ti = cultura.TextInfo;
                        item.FechaHoraInicio = ti.ToTitleCase(fechaReal);
                    }
                }
                else
                {
                    item.FechaHoraInicio = "Por definir";
                }
            }

            seccionModalidades.Contenido = "<table class=\"table table-hover \"><tr><td><strong>Modalidad</strong></td><td><strong>Centro de Costo</strong></td><td><strong>Fecha de Inicio</strong></td></tr>"
           + string.Join("", modalidades.Select(s => "<tr><td>" + s.Tipo + "</td><td>" + s.NombreCentroCosto + "</td><td>" + s.FechaHoraInicio + "</td></tr>").ToArray()) + "</table>";

            //Duracion y Horarios
            //string contenido2 = secciones.Where(w => LimpiarCadena(w.Seccion).ToLower().Equals("duracion y horarios")).Select(w => w.Contenido).FirstOrDefault();
            string contenido = "";
            //if (contenido2 != null)
            //{
            //    if (contenido2.Contains("<ul><li>"))
            //    {
            //        contenido = contenido2 + "</ul></ul>";
            //    }
            //    else contenido = contenido2;
            //}

            //string newcontenido = ObtenerContenidoHorarios(modalidades, contenido, idPGeneral);
            //var seccionContenido = secciones.Where(s => LimpiarCadena(s.Seccion).ToLower().Equals("duracion y horarios")).FirstOrDefault();

            ////Si no tiene nada configurado en el silavoV2
            //if (contenido == null)
            //{
            //    ProgramaGeneralSeccionAnexosHTMLDTO programaNuevo = new ProgramaGeneralSeccionAnexosHTMLDTO();
            //    programaNuevo.Seccion = "Duración y Horarios";
            //    programaNuevo.Contenido = newcontenido;
            //    secciones.Add(programaNuevo);
            //}
            //else
            //{
            //    if (seccionContenido != null)
            //    {
            //        //seccionContenido.Contenido = newcontenido;
            //        newcontenido = seccionContenido.Contenido;
            //    }

            //}

            string[] listaTituloV1 = { "estructura curricular", "beneficios", "prerrequisitos", "certificacion", "certificación", "duracion y horarios", "duración y horarios", "evaluacion", "evaluación", "bibliografia", "bibliografía", "material del curso", "pautas complementarias", "descripción certificación", "descripcion certificacion", "objetivos", "presentacion", "presentación", "público objetivo", "publico objetivo", "metodolog&#237;a online de este programa", "modalidad", "inversion", "perfil del egressado", "mercado laboral", "expositores", "metodologia del programa" };
            string[] listaTituloV2 = { "Estructura Curricular", "Beneficios", "Prerrequisitos", "Certificación", "Certificación", "duraci&#243;n y horarios", "Duración y Horarios", "Evaluación", "Evaluación", "Bibliografía", "Bibliografía", "Material del Curso", "Pautas Complementarias", "Descripción Certificación", "Descripción Certificación", "Objetivos", "Presentación", "Presentación", "Público Objetivo", "Público Objetivo", "Metodolog&#237;a Online de este Programa", "Modalidad", "Inversion", "Perfil del egressado", "Mercado laboral", "Expositores", "metodolog&#237;a del programa" };
            for (var i = 0; i < listaTituloV2.Length; i++)
            {
                var pendienteTildes = secciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena(listaTituloV1[i])).FirstOrDefault();
                if (pendienteTildes != null)
                {
                    if (pendienteTildes.Contenido == null || pendienteTildes.Contenido != "")
                    {
                        pendienteTildes.Seccion = listaTituloV2[i];
                    }
                }
            }

            if (estructuraTecnico != null && estructuraTecnico != "")
            {
                secciones.Add(new ProgramaGeneralSeccionAnexosHTMLDTO() { Seccion = "Estructura Curricular", Contenido = estructuraTecnico });
            }

            try
            {
                if (secciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena("Demostracion de Valor").ToLower()).FirstOrDefault() != null)
                {
                    if (secciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena("Demostracion de Valor").ToLower()).FirstOrDefault().Contenido == "")
                    {
                        secciones.Remove(demostraciónValor);
                    }
                }
                if (secciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena("Garantia de Programa").ToLower()).FirstOrDefault() != null)
                {
                    if (secciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena("Garantia de Programa").ToLower()).FirstOrDefault().Contenido == "")
                    {
                        secciones.Remove(garantiaPrograma);
                    }
                }
                if (secciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena("Limitaciones").ToLower()).FirstOrDefault() != null)
                {
                    if (secciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena("Limitaciones").ToLower()).FirstOrDefault().Contenido == "")
                    {
                        secciones.Remove(limitaciones);
                    }
                }
                if (secciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena("Aspectos Diferenciadores").ToLower()).FirstOrDefault() != null)
                {
                    if (secciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena("Aspectos Diferenciadores").ToLower()).FirstOrDefault().Contenido == "")
                    {
                        secciones.Remove(apectosDiferenciadores);
                    }
                }
            }
            catch (Exception e) { }

            foreach (var item in secciones)
            {
                if (LimpiarCadena(item.Seccion) == LimpiarCadena("Objetivos"))
                {
                    item.Seccion = "Objetivos del programa";
                }
                if (LimpiarCadena(item.Seccion) == LimpiarCadena("Estructura Curricular"))
                {
                    item.Seccion = "Estructura curricular del programa";
                }
                if (LimpiarCadena(item.Seccion) == LimpiarCadena("Metodología Online de este Programa"))
                {
                    item.Seccion = "Metodología";
                }
                if (LimpiarCadena(item.Seccion) == LimpiarCadena("Inversion"))
                {
                    item.Seccion = "Precio";
                }
            }

            //Estilos para que sean desplegables cada una de las secciones 
            var html = $@"
                        <div class='custom-accordion' >
                        " + string.Join("", secciones.Select(s => $@"
                            <div class='accordion'>
                                <h4 class='accordion-header' 
                                    onclick='this.nextElementSibling.style.display = this.nextElementSibling.style.display === ""none"" ? ""block"" : ""none""'>
                                    {s.Seccion} <span class='arrow'>&#9662;</span>
                                </h4>
                                <div class='accordion-content' style='display: none;'>
                                    {s.Contenido}
                                </div>
                            </div>
                            ").ToArray()) + @"
                        </div>
                        ";

            html += @"
                    <style>
                    /* Estilos aplicados solo dentro del contenedor .custom-accordion */
                    .custom-accordion .accordion {
                        margin-bottom: 10px;
                    }

                    .custom-accordion .accordion-header {
                        background-color: #5a72b5;
                        color: white; /* El texto del encabezado ahora es blanco */
                        padding: 10px 20px;
                        font-size: 16px;
                        border-radius: 10px;
                        display: flex;
                        justify-content: space-between;
                        cursor: pointer;
                        transition: background-color 0.3s;
                    }

                    .custom-accordion .accordion-header:hover {
                        background-color: #4761a3;
                    }

                    .custom-accordion .accordion-content {
                        padding: 10px;
                        background-color: #ffffff;
                        border: 1px solid #d3d3d3;
                        margin-top: 5px;
                        border-radius: 5px;
                    }

                    .custom-accordion .arrow {
                        transform: rotate(0deg);
                        transition: transform 0.3s;
                    }

                    .custom-accordion .accordion-header[aria-expanded='true'] .arrow {
                        transform: rotate(180deg);
                    }

                    </style>

                    <script>
                    document.querySelectorAll('.custom-accordion .accordion-header').forEach(header => {
                        header.addEventListener('click', function() {
                            let content = this.nextElementSibling;
                            let arrow = this.querySelector('.arrow');
        
                            if (content.style.display === 'block') {
                                content.style.display = 'none';
                                arrow.style.transform = 'rotate(0deg)';
                            } else {
                                content.style.display = 'block';
                                arrow.style.transform = 'rotate(180deg)';
                            }
                        });
                    });
                    </script>";

            //var html = string.Join("", secciones.Select(s => "<h4 style='font-size: 27px;color: #65c7d2;font-weight: 600;'> " + s.Seccion + "</h4>" + s.Contenido + "<br /><br /><hr style='height:3px;background:#65c7d2;border:none'/><br />").ToArray());

            var etiquetaBeneficiosInversion = "";
            var etiquetaExpositores = "";
            var etiquetaDuracionHorarios = "";
            var informacionPrograma = "";

            if (seccionesBeneficiosInversion != null)
            {
                etiquetaBeneficiosInversion = string.Join("", seccionesBeneficiosInversion.Where(w => LimpiarCadena(w.Seccion).ToLower().Equals("inversion") || w.Seccion.ToLower().Equals("beneficios") || w.Seccion.ToLower().Equals("monto actual")).Select(s => "<br><h4 class='col-sm-10' Id='IdTabla" + s.Seccion + "'>" + s.Seccion + "</h4><br>" + s.Contenido).ToArray());
            }

            //if (seccionExpositores != null)
            //{
            //    etiquetaExpositores = seccionExpositores.Contenido;
            //}

            //if (newcontenido != null)
            //{
            //    etiquetaDuracionHorarios = newcontenido;
            //}

            if (html != null)
            {
                informacionPrograma = html;
            }


            return new CargarInformacionProgramaRespuestaDTO()
            {
                EtiquetaFormasPago = etiquetaFormasPago,
                EtiquetaBeneficiosInversion = etiquetaBeneficiosInversion,
                //EtiquetaExpositores = etiquetaExpositores,
                EtiquetaDuracionHorarios = etiquetaDuracionHorarios,
                InformacionPrograma = informacionPrograma,
                EtiquetaTarifarios = etiquetaTarifarios,
                ListaBeneficios = montosBeneficios.ListaBeneficios
            };

        }






    }
}
