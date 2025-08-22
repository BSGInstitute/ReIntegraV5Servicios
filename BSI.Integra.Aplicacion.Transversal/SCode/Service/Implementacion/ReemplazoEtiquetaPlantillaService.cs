using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using DocumentFormat.OpenXml.Wordprocessing;
using Mandrill.Models;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ReemplazoEtiquetaPlantillaService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de Reemplazo de plantilla
    /// </summary>
    public class ReemplazoEtiquetaPlantillaService : IReemplazoEtiquetaPlantillaService
    {
        private IUnitOfWork _unitOfWork;
        private PlantillaAsuntoCuerpoDTO _plantillaCorreo = new();
        private List<DatoPlantillaWhatsAppDTO> _listaObjetoWhatsApp = new();
        private List<int> _listaIdMaterialPEspecificoDetalle = new();
        private int _idPlantillaBase = 0;
        public ReemplazoEtiquetaPlantillaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla
        /// </summary>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetas(ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta)
        {
            PlantillaEmailMandrillDTO emailReemplazado = new();
            PlantillaWhatsAppCalculadoDTO whatsAppReemplazado = new();
            try
            {
                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(reemplazoEtiqueta.IdOportunidad);
                if (oportunidad == null || oportunidad.Id == 0)
                    throw new BadRequestException("Oportunidad no existente!");

                var oportunidadClasificacionOperaciones = _unitOfWork.OportunidadClasificacionOperacionesRepository.ObtenerPorIdOportunidad(reemplazoEtiqueta.IdOportunidad);
                if (oportunidadClasificacionOperaciones == null || oportunidadClasificacionOperaciones.Id == 0)
                    throw new BadRequestException("Oportunidad no valida!");

                var matriculaCabecera = _unitOfWork.MatriculaCabeceraRepository.ObtenerPorId(oportunidadClasificacionOperaciones.IdMatriculaCabecera);
                if (matriculaCabecera == null || matriculaCabecera.Id == 0)
                    throw new BadRequestException("Matricula cabecera no valida!");

                var detalleMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleMatricula(oportunidadClasificacionOperaciones.IdMatriculaCabecera);
                var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(matriculaCabecera.IdAlumno);
                if (alumno == null || alumno.Id == 0)
                    throw new BadRequestException("Alumno no existente!");

                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(reemplazoEtiqueta.IdPlantilla);
                if (plantilla == null || plantilla.Id == 0)
                    throw new BadRequestException("Plantilla no valido!");

                var plantillaBase = _unitOfWork.PlantillaBaseRepository.ObtenerPorId(plantilla.IdPlantillaBase);
                if (plantillaBase == null || plantillaBase.Id == 0)
                    throw new BadRequestException("Plantilla Base no valido!");

                var resumenGrabacionOnline = _unitOfWork.ResumenGrabacionOnlineRepository.ObtenerResumenGrabacionOnlinePorId(reemplazoEtiqueta.IdResumenGrabacionOnline ?? 0);
                var resumenRegistroUrl = _unitOfWork.ResumenGrabacionOnlineRepository.ObtenerProcesamientoTipoGenerarPorId(reemplazoEtiqueta.IdProcesamientoTipoGenerar ?? 0);

                int idPEspecifico = _unitOfWork.PEspecificoSesionRepository.ObtenerPEspecificoPorPEspecificoSesion(reemplazoEtiqueta.IdPEspecificoSesion ?? 0);
                var pespecifico = _unitOfWork.PEspecificoRepository.ObtenerNombrePEspecifico(idPEspecifico);

                _plantillaCorreo = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(plantilla.Id);
                var datosCompuestosOportunidad = _unitOfWork.OportunidadRepository.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(oportunidad.IdPersonalAsignado.GetValueOrDefault());

                var listaEtiqueta = _plantillaCorreo.Cuerpo.Split("{", StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split("}", StringSplitOptions.None).First());

                _listaObjetoWhatsApp = listaEtiqueta.Select(x => new DatoPlantillaWhatsAppDTO
                {
                    Codigo = string.Concat("{", x, "}"),
                    Texto = ""
                }).ToList();

                _idPlantillaBase = plantilla.IdPlantillaBase;
                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.Nombre1}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.Nombre1}", personal.Nombres);

                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.email}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.email}", personal.Email);

                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.PrimerNombreApellidoPaterno}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.PrimerNombreApellidoPaterno}", personal.ApellidoPaterno);

                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.UrlConfirmacionParticipacionSesionWebinarDentro7Dias}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerUrlConfirmacionParticipacionSesionWebinar(matriculaCabecera.Id, 7);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.UrlConfirmacionParticipacionSesionWebinarDentro7Dias}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.PresentacionTrabajoHoy}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerPresentacionTrabajoNDias(matriculaCabecera.Id, 0);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.PresentacionTrabajoHoy}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.PresentacionTrabajoFinalHoy}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerPresentacionTrabajoFinalNDias(matriculaCabecera.Id, 0);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.PresentacionTrabajoFinalHoy}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.SesionWebinarDentro7Dias}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerSesionesWebinarNDias(matriculaCabecera.Id, 7, false);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.SesionWebinarDentro7Dias}", valor);
                }

                List<string> listaEtiquetasWebinarSesion7Dias = new List<string>()
                {
                    "{T_PEspecifico.FechaSesionWebinarDentro7Dias}",
                    "{T_PEspecifico.HoraInicioSesionWebinarDentro7Dias}",
                    "{T_PEspecifico.HoraFinSesionWebinarDentro7Dias}"
                };

                // Etiquetas texto plano
                if (listaEtiquetasWebinarSesion7Dias.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    SesionWebinarDTO valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerUrlSesionesWebinarNDias(matriculaCabecera.Id, 7).FirstOrDefault();
                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.FechaSesionWebinarDentro7Dias}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.FechaSesionWebinarDentro7Dias}", valor.FechaInicio.ToString("dd/MM/yyyy"));

                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.HoraInicioSesionWebinarDentro7Dias}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.HoraInicioSesionWebinarDentro7Dias}", valor.FechaInicio.ToString("hh:mm tt"));

                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.HoraFinSesionWebinarDentro7Dias}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.HoraFinSesionWebinarDentro7Dias}", valor.FechaTermino.ToString("hh:mm tt"));
                }
                List<string> listaEtiquetasMedioPago = new List<string>()
                {
                       "{BCP_soles}",
                       "{BCP_CCI_soles}",
                       "{BCP_dolares}",
                       "{BCP_CCI_dolares}",
                       "{BBVA_soles}",
                       "{BBVA_CCI_soles}",
                       "{Scotiabank_soles}",
                       "{Scotiabank_CCI_soles}",
                       "{Num_YAPE}",
                       "{Swift_Code}",
                       "{CuentaUSD}",
                       "{BanCol}",
                       "{Bancol_suc}",
                       "{BCP_Bolivianos}",
                       "{BCP_Dolares_Bolivia}",
                       "{BUnion_Bolivianos}",
                       "{BUnion_Dolares_Bolivia}",
                       "{BBVA_MXpesos}",
                       "{BBVA_CCI_MXpesos}",
                       "{BBVA_MXdol}",
                       "{BBVA_CCI_MXdol}",
                       "{Banorte_MXpesos}",
                       "{Banorte_CCI_MXpesos}",
                       "{Banorte_MXdol}",
                       "{Banorte_CCI_MXdol}"
                };
                if (listaEtiquetasMedioPago.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                    foreach (string etiqueta in listaEtiquetasMedioPago)
                    {
                        if (_plantillaCorreo.Cuerpo.Contains(etiqueta))
                        {
                            string valor = alumnoService.ObtenerMedioPago(etiqueta);
                            ReemplazarCuerpoCorreoWhatsApp(etiqueta, valor);
                        }
                    }
                }
                List<string> listEstiquetaContactoAtc = new List<string>()
                {
                       "{numPeru}",
                       "{numMexico}",
                       "{numColombia}",
                };
                if (listEstiquetaContactoAtc.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                    foreach (string etiqueta in listEstiquetaContactoAtc)
                    {
                        if (_plantillaCorreo.Cuerpo.Contains(etiqueta))
                        {
                            string valor = alumnoService.ObtenerNumeroAtc(etiqueta);
                            ReemplazarCuerpoCorreoWhatsApp(etiqueta, valor);
                        }
                    }
                }


                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.AccesoSesionWebinarDentro1Dia}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerSesionesWebinarConfirmadasNDias(matriculaCabecera.Id, 1, true);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.AccesoSesionWebinarDentro1Dia}", valor);
                }

                List<string> listaEtiquetasWebinarSesion1Dia = new List<string>()
                {
                    "{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}",
                    "{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}",
                    "{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}",
                    "{T_PEspecifico.LinkAccesoSesionWebinarDentro1Dia}",
                };

                // Etiquetas texto plano
                if (listaEtiquetasWebinarSesion1Dia.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    SesionWebinarDTO valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerSesionesConfirmadasWebinarNDias(matriculaCabecera.Id, 1).FirstOrDefault();
                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}", valor.FechaInicio.ToString("dd/MM/yyyy"));

                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}", valor.FechaInicio.ToString("hh:mm tt"));

                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}", valor.FechaTermino.ToString("hh:mm tt"));

                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.LinkAccesoSesionWebinarDentro1Dia}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.LinkAccesoSesionWebinarDentro1Dia}", valor.LinkWebinar);
                }
                if (listaEtiquetasWebinarSesion1Dia.Any(_plantillaCorreo.Asunto.Contains))
                {
                    SesionWebinarDTO valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerSesionesConfirmadasWebinarNDias(matriculaCabecera.Id, 1).FirstOrDefault();

                    if (_plantillaCorreo.Asunto.Contains("{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}")) { }
                    ReemplazarCuerpoCorreo("{T_PEspecifico.FechaAccesoSesionWebinarDentro1Dia}", valor.FechaInicio.ToString("dd/MM/yyyy"));

                    if (_plantillaCorreo.Asunto.Contains("{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}"))
                        ReemplazarCuerpoCorreo("{T_PEspecifico.HoraInicioAccesoSesionWebinarDentro1Dia}", valor.FechaInicio.ToString("hh:mm tt"));

                    if (_plantillaCorreo.Asunto.Contains("{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}"))
                        ReemplazarCuerpoCorreo("{T_PEspecifico.HoraFinAccesoSesionWebinarDentro1Dia}", valor.FechaTermino.ToString("hh:mm tt"));
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaAutoEvaluacionCompleto}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCronogramaAutoEvaluacionCompleto(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CronogramaAutoEvaluacionCompleto}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidas}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.AutoEvaluacionesVencidas}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesCompletas}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerAutoEvaluacionesCompletas(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.AutoEvaluacionesCompletas}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CantidadAutoEvaluacionesPendientes}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCantidadAutoEvaluacionesPendientes(matriculaCabecera.Id).ToString();
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CantidadAutoEvaluacionesPendientes}", valor);
                }

                // Nuevos solicitadas por Pilar
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace1Dia}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerAutoEvaluacionesVencidasDiasExacto(matriculaCabecera.Id, -1, false, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace1Dia}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace3Dias}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerAutoEvaluacionesVencidasDiasExacto(matriculaCabecera.Id, -3, false, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace3Dias}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace7Dias}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerAutoEvaluacionesVencidasDiasExacto(matriculaCabecera.Id, -7, false, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace7Dias}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencerProximos3Dias}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerAutoEvaluacionesVencidasDiasExacto(matriculaCabecera.Id, 3, true, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.AutoEvaluacionesVencerProximos3Dias}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoAutoEvaluacionesVencerProximos3Dias}"))
                {
                    List<AutoEvaluacionCronogramaDetalleDTO> detalleCuota = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleAutoEvaluacionesVencidas(matriculaCabecera.Id, 3, true);
                    string valor = "";
                    if (detalleCuota != null && detalleCuota.Count() > 0)
                        valor = detalleCuota.FirstOrDefault().FechaCronograma.ToString("dd/MM/yyyy");
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.FechaVencimientoAutoEvaluacionesVencerProximos3Dias}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencerHoy}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerAutoEvaluacionesVencidasDiasExacto(matriculaCabecera.Id, 0, true, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.AutoEvaluacionesVencerHoy}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoAutoEvaluacionesVencerHoy}"))
                {
                    List<AutoEvaluacionCronogramaDetalleDTO> detalleCuota = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleAutoEvaluacionesVencidas(matriculaCabecera.Id, 0, true);
                    string valor = "";
                    if (detalleCuota != null && detalleCuota.Count() > 0)
                        valor = detalleCuota.FirstOrDefault().FechaCronograma.ToString("dd/MM/yyyy");
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.FechaVencimientoAutoEvaluacionesVencerHoy}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoProximaAutoEvaluacion}"))
                {
                    AutoEvaluacionCronogramaDetalleDTO detalleCuota = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleProximaAutoEvaluacion(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.FechaVencimientoProximaAutoEvaluacion}", detalleCuota.FechaCronograma.ToString("dd/MM/yyyy"));
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CantidadAutoEvaluacionesVencidas}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCantidadAutoEvaluacionesVencidas(matriculaCabecera.Id).ToString();
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CantidadAutoEvaluacionesVencidas}", valor);
                }
                IPEspecificoService pEspecificoService = new PEspecificoService(_unitOfWork);
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.PeriodoDuracion}"))
                {
                    string valor = pEspecificoService.ObtenerPeriodoDuracion(matriculaCabecera.IdPespecifico, matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.PeriodoDuracion}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.UrlAccesoSesionOnline}"))
                {
                    string valor = _unitOfWork.PEspecificoRepository.ObtenerUrlAccesoSesionOnline(matriculaCabecera.IdPespecifico);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.UrlAccesoSesionOnline}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.BeneficiosVersion}"))
                {
                    string valor = _unitOfWork.PGeneralRepository.ObtenerBeneficiosVersion(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.BeneficiosVersion}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.ConjuntoSesion}"))
                {
                    string valor = pEspecificoService.ObtenerConjuntoSesion(matriculaCabecera.IdPespecifico);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.ConjuntoSesion}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesion}"))
                {
                    string valor = pEspecificoService.ObtenerProximoConjuntoSesion(matriculaCabecera.IdPespecifico, 0);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.ProximoConjuntoSesion}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro1Dia}"))
                {
                    string valor = pEspecificoService.ObtenerProximoConjuntoSesion(matriculaCabecera.IdPespecifico, 1);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.ProximoConjuntoSesionDentro1Dia}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro0DiaWebex}"))
                {
                    string valor = pEspecificoService.ObtenerProximoConjuntoSesionWebex(reemplazoEtiqueta.IdPEspecifico, 0, reemplazoEtiqueta.IncrementoZonaHoraria, reemplazoEtiqueta.NombrePais, false);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.ProximoConjuntoSesionDentro0DiaWebex}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro1DiaWebex}"))
                {
                    string valor = pEspecificoService.ObtenerProximoConjuntoSesionWebex(reemplazoEtiqueta.IdPEspecifico, 1, reemplazoEtiqueta.IncrementoZonaHoraria, reemplazoEtiqueta.NombrePais, false);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.ProximoConjuntoSesionDentro1DiaWebex}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro2DiaWebex}"))
                {
                    string valor = pEspecificoService.ObtenerProximoConjuntoSesionWebex(reemplazoEtiqueta.IdPEspecifico, 2, reemplazoEtiqueta.IncrementoZonaHoraria, reemplazoEtiqueta.NombrePais, true);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.ProximoConjuntoSesionDentro2DiaWebex}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro7DiaWebexSinNombreCurso}"))
                {
                    string valor = pEspecificoService.ObtenerProximoConjuntoSesionWebex(reemplazoEtiqueta.IdPEspecifico, 7, reemplazoEtiqueta.IncrementoZonaHoraria, reemplazoEtiqueta.NombrePais, false);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.ProximoConjuntoSesionDentro7DiaWebexSinNombreCurso}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentroNDiasWebinarConfirmacionSinNombreCurso}"))
                {
                    string valor = pEspecificoService.ObtenerProximoConjuntoSesionWebex(reemplazoEtiqueta.IdPEspecifico, reemplazoEtiqueta.FechaDinamicaRegularizar, reemplazoEtiqueta.IncrementoZonaHoraria, reemplazoEtiqueta.NombrePais, false);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.ProximoConjuntoSesionDentroNDiasWebinarConfirmacionSinNombreCurso}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro2DiaWebinarSinNombreCurso}"))
                {
                    string valor = pEspecificoService.ObtenerProximoConjuntoSesionWebex(reemplazoEtiqueta.IdPEspecifico, 2, reemplazoEtiqueta.IncrementoZonaHoraria, reemplazoEtiqueta.NombrePais, false);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.ProximoConjuntoSesionDentro2DiaWebinarSinNombreCurso}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro3DiaWebex}"))
                {
                    string valor = pEspecificoService.ObtenerProximoConjuntoSesionWebex(matriculaCabecera.IdPespecifico, 3, reemplazoEtiqueta.IncrementoZonaHoraria, reemplazoEtiqueta.NombrePais, false);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.ProximoConjuntoSesionDentro3DiaWebex}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesionDentro3Dias}"))
                {
                    string valor = pEspecificoService.ObtenerProximoConjuntoSesion(matriculaCabecera.IdPespecifico, 3);
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.ProximoConjuntoSesionDentro3Dias}", valor);
                }

                ///Cronograma pago completo
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaPagoCompleto}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCronogramaPagoCompleto(matriculaCabecera.Id, FormatoHTMLMostrar.Lista);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CronogramaPagoCompleto}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaPagoCompletoTabla}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCronogramaPagoCompleto(matriculaCabecera.Id, FormatoHTMLMostrar.Tabla);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CronogramaPagoCompletoTabla}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.MontoTotal}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerMontoTotal(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.MontoTotal}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_SolicitudOperaciones.ProgramaAccesoTemporalNuevaAula}"))
                {
                    string valor = _unitOfWork.SolicitudOperacionesRepository.ObtenerValorNuevo(true, true, reemplazoEtiqueta.IdOportunidad, 8).ValorNuevo;
                    ReemplazarCuerpoCorreoWhatsApp("{T_SolicitudOperaciones.ProgramaAccesoTemporalNuevaAula}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_SolicitudOperaciones.FechaFinAccesoTemporalNuevaAula}"))
                {
                    string valor = _unitOfWork.SolicitudOperacionesRepository.ObtenerValorNuevo(true, true, reemplazoEtiqueta.IdOportunidad, 8).ObservacionEncargado;
                    string[] fechas = valor.Split(",");
                    string temp = fechas.Length > 1 ? fechas[0] : string.Empty;
                    ReemplazarCuerpoCorreoWhatsApp("{T_SolicitudOperaciones.FechaFinAccesoTemporalNuevaAula}", temp);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_SolicitudOperaciones.FechaInicioAccesoTemporalNuevaAula}"))
                {
                    string valor = _unitOfWork.SolicitudOperacionesRepository.ObtenerValorNuevo(true, true, reemplazoEtiqueta.IdOportunidad, 8).ObservacionEncargado;
                    string[] fechas = valor.Split(",");
                    string temp = fechas.Length > 1 ? fechas[1] : string.Empty;
                    ReemplazarCuerpoCorreoWhatsApp("{T_SolicitudOperaciones.FechaInicioAccesoTemporalNuevaAula}", temp);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_SolicitudCertificadoFisico.Courier}"))
                {
                    SolicitudCertificadoFisico solicitud = _unitOfWork.SolicitudCertificadoFisicoRepository.ObtenerPorIdMatriculaCabecera(matriculaCabecera.Id);
                    string valor = _unitOfWork.SolicitudCertificadoFisicoRepository.ObtenerCourierPorNombre(solicitud.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{T_SolicitudCertificadoFisico.Courier}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_SolicitudCertificadoFisico.CodigoSeguimiento}"))
                {
                    string valor = _unitOfWork.SolicitudCertificadoFisicoRepository.ObtenerPorIdMatriculaCabecera(matriculaCabecera.Id).CodigoSeguimiento;
                    ReemplazarCuerpoCorreoWhatsApp("{T_SolicitudCertificadoFisico.CodigoSeguimiento}", valor);
                }
                DocumentoService documentoService = new DocumentoService(_unitOfWork);
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.Anexo1EstructuraCurricular}"))
                {
                    List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = documentoService.ObtenerListaSeccionDocumentoProgramaGeneral(detalleMatriculaCabecera.IdProgramaGeneral);
                    List<ProgramaGeneralSeccionAnexosHTMLDTO> seccionEstructura = documentoService.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);
                    ProgramaGeneralSeccionAnexosHTMLDTO estructuraV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "estructura curricular").FirstOrDefault();
                    string valor = "";
                    if (estructuraV2 != null)
                    {
                        valor = $"<strong>{estructuraV2.Seccion}</strong><br>";
                        valor += estructuraV2.Contenido;
                        valor = valor.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                    }
                    else
                    {
                        List<SeccionDocumentoDTO> valorFinal = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSecciones(detalleMatriculaCabecera.IdProgramaGeneral);
                        SeccionDocumentoDTO detalle = valorFinal.FirstOrDefault(x => x.Titulo.Contains("Estructura Curricular"));
                        if (detalle != null)
                        {
                            valor = $"<h2>{detalle.Titulo}</h2>";
                            valor += detalle.Contenido;
                        }
                    }
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.Anexo1EstructuraCurricular}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.Anexo2Certificacion}"))
                {
                    List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = documentoService.ObtenerListaSeccionDocumentoProgramaGeneral(detalleMatriculaCabecera.IdProgramaGeneral);
                    List<ProgramaGeneralSeccionAnexosHTMLDTO> seccionEstructura = documentoService.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);
                    ProgramaGeneralSeccionAnexosHTMLDTO certificacionV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "certificacion").FirstOrDefault();

                    string valor = "";
                    if (certificacionV2 != null)
                    {
                        valor = $"<strong>{certificacionV2.Seccion}</strong><br>";
                        valor += certificacionV2.Contenido;
                        valor = valor.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                    }
                    else
                    {
                        List<SeccionDocumentoDTO> valorFinal = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSecciones(detalleMatriculaCabecera.IdProgramaGeneral);
                        SeccionDocumentoDTO detalle = valorFinal.Where(x => x.Titulo.Contains("Certificación")).FirstOrDefault();
                        if (detalle != null)
                        {
                            valor = $"<h2>{detalle.Titulo}</h2>";
                            valor += detalle.Contenido;
                        }
                    }
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.Anexo2Certificacion}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaPagoCompletoCuotasVencidas}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCronogramaPagoCompletoCuotasVencidas(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CronogramaPagoCompletoCuotasVencidas}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CantidadCuotasPendientes}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCantidadCuotasPendientes(matriculaCabecera.Id).ToString();
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CantidadCuotasPendientes}", valor);
                }

                // Solicitados por Pilar
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasMayorIgual6Dias}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCuotasVencidas(matriculaCabecera.Id, -6, false, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CuotasVencidasMayorIgual6Dias}", valor);
                }

                // Solicitados por Celina
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasMayorIgual90Dias}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCuotasVencidas(matriculaCabecera.Id, -90, false, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CuotasVencidasMayorIgual90Dias}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CantidadCuotasVencidasMayorIgual6Dias}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCantidadCuotasVencidas(matriculaCabecera.Id, -6, false, plantilla.IdPlantillaBase).ToString();
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CantidadCuotasVencidasMayorIgual6Dias}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasHace1Dia}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCuotasVencidas(matriculaCabecera.Id, -1, true, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CuotasVencidasHace1Dia}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoCuotasVencidasHace1Dia}"))
                {
                    List<CuotaCronogramaDetalleDTO> detalleCuota = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleCuotasVencidas(matriculaCabecera.Id, -1, true);
                    string valorFormateado = "";
                    if (detalleCuota.Count() > 0)
                        valorFormateado = detalleCuota.FirstOrDefault().FechaVencimiento.ToString("dd/MM/yyyy");
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.FechaVencimientoCuotasVencidasHace1Dia}", valorFormateado);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasHace3Dias}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCuotasVencidas(matriculaCabecera.Id, -3, false, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CuotasVencidasHace3Dias}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasHace7Dias}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCuotasVencidas(matriculaCabecera.Id, -7, false, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CuotasVencidasHace7Dias}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencerHoy}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCuotasVencidas(matriculaCabecera.Id, 0, true, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CuotasVencerHoy}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoCuotasVencerHoy}"))
                {
                    List<CuotaCronogramaDetalleDTO> detalleCuota = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleCuotasVencidas(matriculaCabecera.Id, 0, true);
                    string valor = "";
                    if (detalleCuota.Count() > 0)
                        valor = detalleCuota.FirstOrDefault().FechaVencimiento.ToString("dd/MM/yyyy");
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.FechaVencimientoCuotasVencerHoy}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencerProximos3Dias}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerCuotasVencidas(matriculaCabecera.Id, 3, true, plantilla.IdPlantillaBase);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CuotasVencerProximos3Dias}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoCuotasVencerProximos3Dias}"))
                {
                    List<CuotaCronogramaDetalleDTO> detalleCuota = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleCuotasVencidas(matriculaCabecera.Id, 3, true);
                    string valor = "";
                    if (detalleCuota.Count() > 0)
                        valor = detalleCuota.FirstOrDefault().FechaVencimiento.ToString("dd/MM/yyyy");
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.FechaVencimientoCuotasVencerProximos3Dias}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.FechaVencimientoProximaCuota}"))
                {
                    CuotaCronogramaDetalleDTO detalleCuota = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleProximaCuota(matriculaCabecera.Id);
                    string valor = detalleCuota.FechaVencimiento.ToString("dd/MM/yyyy");
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.FechaVencimientoProximaCuota}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CantidadCuotasVencidas}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerTodoCantidadCuotasVencidas(matriculaCabecera.Id).ToString();
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CantidadCuotasVencidas}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.5PorcientoDescuentoTotalCuotasPendientes}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerDescuentoCuotasPendientesPorPorcentaje(matriculaCabecera.Id, Convert.ToDecimal(0.05));
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.5PorcientoDescuentoTotalCuotasPendientes}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.8PorcientoDescuentoTotalCuotasPendientes}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerDescuentoCuotasPendientesPorPorcentaje(matriculaCabecera.Id, Convert.ToDecimal(0.08));
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.8PorcientoDescuentoTotalCuotasPendientes}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.10PorcientoDescuentoTotalCuotasPendientes}"))
                {
                    string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerDescuentoCuotasPendientesPorPorcentaje(matriculaCabecera.Id, Convert.ToDecimal(0.10));
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.10PorcientoDescuentoTotalCuotasPendientes}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.FechaLimitePorAbandonar}"))
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.FechaLimitePorAbandonar}", ((DateTime.Now).AddMonths(1)).ToString("dd/MM/yyyy"));

                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.CodigoMatricula}", matriculaCabecera.CodigoMatricula);

                // Reemplazar nombre 1 alumno
                if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.NombreCompleto}"))
                {
                    AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{T_Alumno.NombreCompleto}", alumnoService.ObtenerNombreCompleto(alumno));
                }

                // Reemplazar Fecha Inicio Capacitacion
                if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                {
                    string valor = _unitOfWork.AlumnoRepository.ObtenerFechaInicioCapacitacion(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{T_Alumno.FechaInicioCapacitacion}", valor);
                }

                // Reemplazar Fecha Fin Capacitacion
                if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.FechaFinCapacitacion}"))
                {
                    string valor = _unitOfWork.AlumnoRepository.ObtenerFechaFinCapacitacion(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{T_Alumno.FechaFinCapacitacion}", valor);
                }

                // Reemplazar Calificacion Promedio
                if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.CalificacionPromedio}"))
                {
                    string valor = _unitOfWork.AlumnoRepository.ObtenerNotaPromedio(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{T_Alumno.CalificacionPromedio}", valor);
                }

                // Reemplazar Fecha Emision Certificado
                if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.FechaEmisionCertificado}"))
                {
                    string valor = _unitOfWork.AlumnoRepository.ObtenerFechaEmision();
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{T_Alumno.FechaEmisionCertificado}", valor);
                }

                // Reemplazar Fecha Codigo Certificado
                if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.CodigoCertificado}"))
                {
                    string valor = _unitOfWork.AlumnoRepository.ObtenerCodigoCertificado(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{T_Alumno.CodigoCertificado}", valor);
                }

                // Reemplazar duracion en horas de Programa Especifico
                if (_plantillaCorreo.Cuerpo.Contains("{tPEspecifico.duracion}"))
                {
                    string valor = _unitOfWork.EstructuraEspecificaRepository.ObtenerDuracionTotalPorIdMatriculaCabecera(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{tPEspecifico.duracion}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.NroDocumento}"))
                    ReemplazarCuerpoCorreoWhatsApp("{T_Alumno.NroDocumento}", alumno.Dni);
                if (_plantillaCorreo.Cuerpo.Contains("{tAlumnos.dni}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tAlumnos.dni}", alumno.Dni);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumnos.direccion}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tAlumnos.direccion}", alumno.Direccion == null ? "-" : alumno.Direccion.ToUpper());

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumnos.NombreCiudad}"))
                {
                    string valor = _unitOfWork.AlumnoRepository.ObtenerCiudadOrigen(alumno.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{tAlumnos.NombreCiudad}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.Version}"))
                {
                    string valor = _unitOfWork.PGeneralRepository.ObtenerVersion(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.Version}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{tpla_pgeneral.pw_duracion}"))
                {
                    string valor = _unitOfWork.PGeneralRepository.ObtenerDuracionMeses(matriculaCabecera.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{tpla_pgeneral.pw_duracion}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{tAlumnos.nombre1}"))
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{tAlumnos.nombre1}", alumno.Nombre1);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumnos.nombre2}"))
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{tAlumnos.nombre2}", alumno.Nombre2);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumnos.apematerno}"))
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{tAlumnos.apematerno}", alumno.ApellidoMaterno);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumnos.apepaterno}"))
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{tAlumnos.apepaterno}", alumno.ApellidoPaterno);

                if (_plantillaCorreo.Cuerpo.Contains("{tpla_pgeneral.pw_duracion}"))
                {
                    if (plantilla.IdPlantillaBase == PlantillaBase.Email)
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tpla_pgeneral.pw_duracion}", datosCompuestosOportunidad.PwDuracion);
                    else
                        _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals("{tpla_pgeneral.pw_duracion}")).Texto = datosCompuestosOportunidad.PwDuracion;
                }
                if (_plantillaCorreo.Cuerpo.Contains("{tAlumnos.NombrePais}"))
                {
                    string valor = _unitOfWork.AlumnoRepository.ObtenerPaisOrigen(alumno.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{tAlumnos.NombrePais}", valor);
                }

                //Nombre programa general
                if (_plantillaCorreo.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{tPLA_PGeneral.Nombre}", detalleMatriculaCabecera.NombreProgramaGeneral);

                //Nombre centro de costo
                if (_plantillaCorreo.Cuerpo.Contains("{T_CentroCosto.Nombre}"))
                    ReemplazarCuerpoCorreoWhatsApp("{T_CentroCosto.Nombre}", detalleMatriculaCabecera.NombreCentroCosto);

                //Horario atencion personal
                if (_plantillaCorreo.Cuerpo.Contains("{T_Personal.HorarioAtencion}"))
                {
                    string valor = _unitOfWork.PersonalRepository.ObtenerHorarioTrabajo(oportunidad.IdPersonalAsignado.Value);
                    ReemplazarCuerpoCorreoWhatsApp("{T_Personal.HorarioAtencion}", valor);
                }

                //Anexo personal
                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.Anexo}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.Anexo}", personal.Anexo);

                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.nombres}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.nombres}", personal.Nombres);

                //Apellidos del personal
                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.apellidos}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.apellidos}", personal.Apellidos);

                if (_plantillaCorreo.Cuerpo.Contains("{T_AlumnoMoodle.CursoActualMoodle}"))
                {
                    List<DetalleCursoActualAulaVirtualDTO> cursoMoodle = _unitOfWork.MatriculaCabeceraRepository.ObtenerCursoActualAlumnoMoodle(matriculaCabecera.Id);
                    string valor = "";
                    if (cursoMoodle != null && cursoMoodle.Count() > 0)
                        valor = cursoMoodle[0].NombreCurso;

                    ReemplazarCuerpoCorreo("{T_AlumnoMoodle.CursoActualMoodle}", personal.Apellidos);
                }

                //Telefono personal
                if (_plantillaCorreo.Cuerpo.Contains("{T_Personal.Telefono}"))
                {
                    AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                    string valor = alumnoService.ObtenerNroTelefonoCoordinador(alumno.IdCodigoPais.Value, alumno.IdCiudad.Value);
                    ReemplazarCuerpoCorreoWhatsApp("{T_Personal.Telefono}", valor);
                }
                if (_plantillaCorreo.Asunto.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                    ReemplazarAsuntoCorreo("{T_MatriculaCabecera.CodigoMatricula}", matriculaCabecera.CodigoMatricula);

                //Reemplazar nombre 1 alumno
                if (_plantillaCorreo.Asunto.Contains("{tAlumnos.nombre1}"))
                    ReemplazarAsuntoCorreo("{tAlumnos.nombre1}", alumno.Nombre1);

                //Nombre programa general
                if (_plantillaCorreo.Asunto.Contains("{tPLA_PGeneral.Nombre}"))
                    ReemplazarAsuntoCorreo("{tPLA_PGeneral.Nombre}", detalleMatriculaCabecera.NombreProgramaGeneral);

                //Nombre centro de costo
                if (_plantillaCorreo.Asunto.Contains("{T_CentroCosto.Nombre}"))
                    ReemplazarAsuntoCorreo("{T_CentroCosto.Nombre}", detalleMatriculaCabecera.NombreCentroCosto);

                //Numero whatsapp por pais alumno
                if (_plantillaCorreo.Asunto.Contains("{T_Alumno.NroWhatsAppCoordinador}"))
                {
                    AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                    ReemplazarAsuntoCorreo("{T_Alumno.NroWhatsAppCoordinador}", alumnoService.ObtenerNroWhatsAppCoordinador(alumno.IdCodigoPais.Value));
                }

                //Horario atencion personal
                if (_plantillaCorreo.Asunto.Contains("{T_Personal.HorarioAtencion}"))
                {
                    string valor = _unitOfWork.PersonalRepository.ObtenerHorarioTrabajo(oportunidad.IdPersonalAsignado.Value);
                    ReemplazarAsuntoCorreo("{T_Personal.HorarioAtencion}", valor);
                }

                //Anexo personal
                if (_plantillaCorreo.Asunto.Contains("{tPersonal.Anexo}"))
                    ReemplazarAsuntoCorreo("{tPersonal.Anexo}", personal.Anexo);

                List<string> listaArchivosAdjunto = new List<string>()
                {
                    "{ArchivoAdjunto.ManualIngresoAulaVirtual}",
                    "{ArchivoAdjunto.ManualBSPlay}",
                    "{ArchivoAdjunto.ManualConectarseSesionWebinar}",
                    "{ArchivoAdjunto.ManualConectarseSesionVirtual}"
                };

                List<Image> listaImagenes = new List<Image>();

                //logica documentos adjuntos
                if (listaArchivosAdjunto.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    if (_plantillaCorreo.Cuerpo.Contains("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))
                    {
                        emailReemplazado.ListaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))),
                            Name = "Manual para ingreso al Aula Virtual.pdf",
                            Type = "application/pdf"
                        });
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{ArchivoAdjunto.ManualIngresoAulaVirtual}", "");
                    }
                    if (_plantillaCorreo.Cuerpo.Contains("{ArchivoAdjunto.ManualBSPlay}"))
                    {
                        emailReemplazado.ListaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualBSPlay}"))),
                            Name = "Manual BS Play.pdf",
                            Type = "application/pdf"
                        });
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{ArchivoAdjunto.ManualBSPlay}", "");
                    }
                    if (_plantillaCorreo.Cuerpo.Contains("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))
                    {
                        emailReemplazado.ListaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))),
                            Name = "Manual para conectarse a la sesión webinar.pdf",
                            Type = "application/pdf"
                        });
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{ArchivoAdjunto.ManualConectarseSesionWebinar}", "");
                    }
                    if (_plantillaCorreo.Cuerpo.Contains("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))
                    {
                        emailReemplazado.ListaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))),
                            Name = "Manual para conectarse a la sesión virtual.pdf",
                            Type = "application/pdf"
                        });
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{ArchivoAdjunto.ManualConectarseSesionVirtual}", "");
                    }
                }

                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlDescargarAplicativoAndroid%7D", "{Link.UrlDescargarAplicativoAndroid}");
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlDescargarAplicativoIOS%7D", "{Link.UrlDescargarAplicativoIOS}");
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlGuiaAccederSesionWebinarPorVideo%7D", "{Link.UrlGuiaAccederSesionWebinarPorVideo}");
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlAulaVirtual%7D", "{Link.UrlAulaVirtual}");
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlManualBSPlay%7D", "{Link.UrlManualBSPlay}");
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlPortalWeb%7D", "{Link.UrlPortalWeb}");

                //{ link.urlimagenfelizcumpleanios}

                List<string> listaTextoPlano = new List<string>()
                {
                    "{Link.UrlAulaVirtual}",
                    "{Link.UrlDescargarAplicativoAndroid}",
                    "{Link.UrlDescargarAplicationIOS}",
                    "{Link.UrlGuiaAccederSesionWebinarPorVideo}",
                    "{Link.UrlImagenFelizCumpleanios}",
                    "{Link.UrlManualBSPlay}",
                    "{Link.UrlPortalWeb}"
                };
                //Etiquetas texto plano
                if (listaTextoPlano.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    if (_plantillaCorreo.Cuerpo.Contains("{Link.UrlPortalWeb}"))
                        ReemplazarCuerpoCorreoWhatsApp("{Link.UrlPortalWeb}", ValorEstaticoUtil.Get("{Link.UrlPortalWeb}"));

                    if (_plantillaCorreo.Cuerpo.Contains("{Link.UrlManualBSPlay}"))
                        ReemplazarCuerpoCorreoWhatsApp("{Link.UrlManualBSPlay}", ValorEstaticoUtil.Get("{Link.UrlManualBSPlay}"));

                    if (_plantillaCorreo.Cuerpo.Contains("{Link.UrlAulaVirtual}"))
                        ReemplazarCuerpoCorreoWhatsApp("{Link.UrlAulaVirtual}", ValorEstaticoUtil.Get("{Link.UrlAulaVirtual}"));

                    if (_plantillaCorreo.Cuerpo.Contains("{Link.UrlDescargarAplicativoAndroid}"))
                        ReemplazarCuerpoCorreoWhatsApp("{Link.UrlDescargarAplicativoAndroid}", ValorEstaticoUtil.Get("{Link.UrlDescargarAplicativoAndroid}"));

                    if (_plantillaCorreo.Cuerpo.Contains("{Link.UrlDescargarAplicativoIOS}"))
                        ReemplazarCuerpoCorreoWhatsApp("{Link.UrlDescargarAplicativoIOS}", ValorEstaticoUtil.Get("{Link.UrlDescargarAplicativoIOS}"));

                    if (_plantillaCorreo.Cuerpo.Contains("{Link.UrlGuiaAccederSesionWebinarPorVideo}"))
                        ReemplazarCuerpoCorreoWhatsApp("{Link.UrlGuiaAccederSesionWebinarPorVideo}", ValorEstaticoUtil.Get("{Link.UrlGuiaAccederSesionWebinarPorVideo}"));

                    if (_plantillaCorreo.Cuerpo.Contains("{Link.UrlImagenFelizCumpleanios}"))
                    {
                        string valor = _unitOfWork.AlumnoRepository.ObtenerUrlImagenFelizCumpleanios();
                        ReemplazarCuerpoCorreoWhatsApp("{Link.UrlImagenFelizCumpleanios}", valor);
                    }
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.FormaPago}"))
                {
                    AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                    PEspecificoValorDTO detallePEspecifico = _unitOfWork.PEspecificoRepository.ObtenerDetalle(matriculaCabecera.Id);
                    string valor = alumnoService.ObtenerFormaPagoReferencia(alumno.IdCodigoPais.Value, detallePEspecifico.IdModalidadCurso, detallePEspecifico.IdCiudad, matriculaCabecera.CodigoMatricula, detallePEspecifico.MonedaCronograma);
                    ReemplazarCuerpoCorreoWhatsApp("{T_Alumno.FormaPago}", valor);
                }
                List<string> listaAccesoAulaVirtual = new List<string>()
                {
                    "{T_Alumno.UsuarioAulaVirtual}",
                    "{T_Alumno.ClaveAulaVirtual}"
                };
                if (listaAccesoAulaVirtual.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    DetalleAccesoAulaVirtualDTO accesoAulaVirtual = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleAccesoAulaVirtual(matriculaCabecera.Id);
                    // Acceso aula virtual
                    if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.UsuarioAulaVirtual}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_Alumno.UsuarioAulaVirtual}", accesoAulaVirtual.UsuarioMoodle);

                    if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.ClaveAulaVirtual}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_Alumno.ClaveAulaVirtual}", accesoAulaVirtual.ClaveMoodle);
                }

                List<string> listaAccesoDocentePortalWeb = new List<string>()
                {
                    "{T_Docente.UsuarioPortalWeb}",
                    "{T_Docente.ClavePortalWeb}"
                };

                if (listaAccesoDocentePortalWeb.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    DetalleAccesoPortalWebDTO accesoPortalWeb = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleAccesoDocentePortalWeb(reemplazoEtiqueta.IdProveedor);
                    //Acceso PW
                    if (_plantillaCorreo.Cuerpo.Contains("{T_Docente.UsuarioPortalWeb}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_Docente.UsuarioPortalWeb}", accesoPortalWeb.Usuario);

                    if (_plantillaCorreo.Cuerpo.Contains("{T_Docente.ClavePortalWeb}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_Docente.ClavePortalWeb}", accesoPortalWeb.Clave);
                }

                List<string> listaAccesoPortalWeb = new List<string>()
                {
                    "{T_Alumno.UsuarioPortalWeb}",
                    "{T_Alumno.ClavePortalWeb}"
                };

                if (listaAccesoPortalWeb.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    DetalleAccesoPortalWebDTO accesoPortalWeb = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleAccesoPortalWeb(matriculaCabecera.Id);
                    //Acceso PW
                    if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.UsuarioPortalWeb}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_Alumno.UsuarioPortalWeb}", accesoPortalWeb.Usuario);

                    if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.ClavePortalWeb}"))
                        ReemplazarCuerpoCorreoWhatsApp("{T_Alumno.ClavePortalWeb}", accesoPortalWeb.Clave);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_Personal.FirmaCorreo}"))
                    ReemplazarCuerpoCorreoWhatsApp("{T_Personal.FirmaCorreo}", personal.FirmaHtml);

                if (_plantillaCorreo.Cuerpo.Contains("{T_Personal.FirmaCorreoImagen}"))
                {
                    PersonalService personalService = new PersonalService(_unitOfWork);
                    string valor = personalService.ObtenerFirmaCorreoImagen(personal.UrlFoto, alumno.IdCodigoPais, alumno.IdCiudad);
                    ReemplazarCuerpoCorreoWhatsApp("{T_Personal.FirmaCorreoImagen}", valor);
                }

                //Presencial
                //reemplaza en la etiqueta de firma tambien
                //Numero whatsapp por pais alumno
                if (_plantillaCorreo.Cuerpo.Contains("{T_Alumno.NroWhatsAppCoordinador}"))
                {
                    AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                    string valor = alumnoService.ObtenerNroWhatsAppCoordinador(alumno.IdCodigoPais.Value);
                    ReemplazarCuerpoCorreoWhatsApp("{T_Alumno.NroWhatsAppCoordinador}", valor);
                }

                //Calcular ultima sesion
                List<string> listaPEspecificoSesion = new List<string>()
                {
                    "{T_PEspecificoSesion.UrlUbicacionCiudad}",
                    "{T_PEspecificoSesion.DireccionDictadoClases}",
                    "{T_PEspecificoSesion.NombreCiudadDictadoClases}",
                    "{T_PEspecificoSesion.NombreDocente}"
                };

                if (listaPEspecificoSesion.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    int idPEspecificoSesion = _unitOfWork.MatriculaCabeceraRepository.ObtenerProximaSesion(matriculaCabecera.IdPespecifico, 0);
                    //Se calcula en base a una sesion
                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecificoSesion.UrlUbicacionCiudad}"))
                    {
                        string valor = _unitOfWork.PEspecificoSesionRepository.ObtenerUrlUbicacionCiudad(idPEspecificoSesion);
                        ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecificoSesion.UrlUbicacionCiudad}", valor);
                    }
                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecificoSesion.DireccionDictadoClases}"))
                    {
                        string valor = _unitOfWork.PEspecificoSesionRepository.ObtenerDireccionDictadoClases(idPEspecificoSesion);
                        ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecificoSesion.DireccionDictadoClases}", valor);
                    }
                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecificoSesion.NombreCiudadDictadoClases}"))
                    {
                        string valor = _unitOfWork.PEspecificoSesionRepository.ObtenerNombreCiudadDictadoClases(idPEspecificoSesion);
                        ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecificoSesion.NombreCiudadDictadoClases}", valor);
                    }
                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecificoSesion.NombreDocente}"))
                    {
                        string valor = _unitOfWork.PEspecificoSesionRepository.ObtenerNombreDocenteDictadoClases(idPEspecificoSesion);
                        ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecificoSesion.NombreDocente}", valor);
                    }
                }

                // Calcular ultima sesion
                List<string> listaPEspecificoSesionDentro3Dias = new List<string>()
                {
                    "{T_PEspecificoSesion.UrlUbicacionCiudadDentro3Dias}",
                    "{T_PEspecificoSesion.DireccionDictadoClasesDentro3Dias}",
                    "{T_PEspecificoSesion.NombreCiudadDictadoClasesDentro3Dias}",
                    "{T_PEspecificoSesion.NombreDocenteDentro3Dias}"
                };

                if (listaPEspecificoSesion.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    int idPEspecificoSesion = _unitOfWork.MatriculaCabeceraRepository.ObtenerProximaSesion(matriculaCabecera.IdPespecifico, 3);
                    // Se calcula en base a una sesion
                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecificoSesion.UrlUbicacionCiudadDentro3Dias}"))
                    {
                        string valor = _unitOfWork.PEspecificoSesionRepository.ObtenerUrlUbicacionCiudad(idPEspecificoSesion);
                        ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecificoSesion.UrlUbicacionCiudadDentro3Dias}", valor);
                    }
                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecificoSesion.DireccionDictadoClasesDentro3Dias}"))
                    {
                        string valor = _unitOfWork.PEspecificoSesionRepository.ObtenerDireccionDictadoClases(idPEspecificoSesion);
                        ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecificoSesion.DireccionDictadoClasesDentro3Dias}", valor);
                    }
                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecificoSesion.NombreCiudadDictadoClasesDentro3Dias}"))
                    {
                        string valor = _unitOfWork.PEspecificoSesionRepository.ObtenerNombreCiudadDictadoClases(idPEspecificoSesion);
                        ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecificoSesion.NombreCiudadDictadoClasesDentro3Dias}", valor);
                    }
                    if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecificoSesion.NombreDocenteDentro3Dias}"))
                    {
                        string valor = _unitOfWork.PEspecificoSesionRepository.ObtenerNombreDocenteDictadoClases(idPEspecificoSesion);
                        ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecificoSesion.NombreDocenteDentro3Dias}", valor);
                    }
                }
                // TODO (To Do: Por hacer)
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.UrlQuejaSugerenciaHoraActual}"))
                {
                    string valor = _unitOfWork.PEspecificoRepository.ObtenerUrlQuejaSugerenciaNDiasNHora(matriculaCabecera.Id, 0, 0);
                    ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecifico.UrlQuejaSugerenciaHoraActual}", valor);
                }
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecificoSesion.HorarioSemanaSesionWebex}"))
                {
                    string valor = _unitOfWork.PEspecificoSesionRepository.ObtenerHorarioSemanaSesionWebex(matriculaCabecera.IdPespecifico);
                    ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecificoSesion.HorarioSemanaSesionWebex}", valor);
                }

                // TODO (To Do: Por hacer)
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.NombreCursoEncuestaHoraActual}"))
                {
                    string valor = _unitOfWork.PEspecificoRepository.ObtenerNombreCursoEncuestaNDiasNHora(matriculaCabecera.Id, 0, 0);
                    ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecifico.NombreCursoEncuestaHoraActual}", valor);
                }

                // TODO (To Do: Por hacer)
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.UrlEncuestaHoraActual}"))
                {
                    string valor = _unitOfWork.PEspecificoRepository.ObtenerUrlEncuestaNDiasNHora(matriculaCabecera.Id, 0, 0);
                    ReemplazarCuerpoCorreoAddItemWhatsApp("{T_MatriculaCabecera.MaterialesDescargar}", valor);
                }

                // TODO (To Do: Por hacer)
                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.FechaEmisionCertificado}"))
                {
                    string valor = _unitOfWork.PEspecificoRepository.ObtenerFechaEmisionUltimoCertificado(1, 1);
                    ReemplazarCuerpoCorreoAddItemWhatsApp("{T_PEspecifico.FechaEmisionCertificado}", valor);
                }

                // SECCION NUEVOS TEMPLATES
                // ESTRUCTURA CURRICULAR
                if (_plantillaCorreo.Cuerpo.Contains("Template") && _plantillaCorreo.Cuerpo.Contains("Estructura Curricular"))
                {
                    // TODO
                    //string valor = "";
                    //if (plantilla.IdPlantillaBase == PlantillaBase.Email)
                    //{ }
                    //else if (plantilla.IdPlantillaBase == PlantillaBase.WhatsappFacebook)
                    //{ }
                }

                // CERTIFICACION
                if (_plantillaCorreo.Cuerpo.Contains("Template") && _plantillaCorreo.Cuerpo.Contains("Certificación"))
                {
                    // TODO
                    //foreach (var item in ListaObjetoWhasApp.Where(x => x.Codigo.Contains("Template")))
                    //{ }
                }
                //Templates
                if (_plantillaCorreo.Cuerpo.Contains("Template"))
                {
                    const int indexIdPlantilla = 3;
                    const int indexIdColumna = 4;
                    if (plantilla.IdPlantillaBase == PlantillaBase.Email)
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template")))
                        {
                            string[] array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                            SeccionEtiquetaDTO valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(array[indexIdColumna]), new Guid(array[indexIdColumna]), oportunidad.IdCentroCosto ?? default);
                            if (valor != null)
                            {
                                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(item.Codigo, valor.Valor);
                            }

                        }
                    }
                    else if (plantilla.IdPlantillaBase == PlantillaBase.WhatsappFacebook)
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template")))
                        {
                            string[] array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                            SeccionEtiquetaDTO valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(array[indexIdColumna]), new Guid(array[indexIdColumna]), oportunidad.IdCentroCosto ?? default);
                            if (valor != null)
                            {
                                _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor.Valor;
                            }

                        }
                    }
                    else
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template")))
                        {
                            string[] array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                            SeccionEtiquetaDTO valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(array[indexIdColumna]), new Guid(array[indexIdColumna]), oportunidad.IdCentroCosto ?? default);
                            if (valor != null)
                            {
                                _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor.Valor.Replace("<p>", "<p id='estructura'>");
                            }
                        }
                    }
                }

                //agregamos logica reemplazo
                //if (reemplazoEtiqueta.IdPEspecifico != 0) {}
                //if (reemplazoEtiqueta.Grupo != 0) {}
                if (_listaIdMaterialPEspecificoDetalle.Any())
                {
                    if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.MaterialesDescargar}"))
                    {
                        string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerMaterialesPorMaterialPEspecificoDetalle(_listaIdMaterialPEspecificoDetalle);
                        ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.MaterialesDescargar}", valor);
                    }
                }
                if (reemplazoEtiqueta.IdMaterialPEspecificoDetalle != 0)
                {
                    if (_plantillaCorreo.Cuerpo.Contains("{T_MatriculaCabecera.MaterialesDescargar}"))
                    {
                        string valor = _unitOfWork.MatriculaCabeceraRepository.ObtenerUrlMaterialesPorMaterialPEspecificoDetalle(reemplazoEtiqueta.IdMaterialPEspecificoDetalle);
                        ReemplazarCuerpoCorreoWhatsApp("{T_MatriculaCabecera.MaterialesDescargar}", valor);
                    }
                }
                DateTime fechaActual = DateTime.Now;

                if (_plantillaCorreo.Cuerpo.Contains("{ValorDinamico.DiaFechaActual}"))
                    ReemplazarCuerpoCorreoWhatsApp("{ValorDinamico.DiaFechaActual}", fechaActual.Day.ToString());

                if (_plantillaCorreo.Cuerpo.Contains("{ValorDinamico.NombreMesFechaActual}"))
                    ReemplazarCuerpoCorreoWhatsApp("{ValorDinamico.NombreMesFechaActual}", fechaActual.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")));

                if (_plantillaCorreo.Cuerpo.Contains("{ValorDinamico.AnioFechaActual}"))
                    ReemplazarCuerpoCorreoWhatsApp("{ValorDinamico.AnioFechaActual}", fechaActual.Year.ToString());

                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecifico.LinkConfirmacionWebinarPositiva}"))
                {
                    string link = $"https://bsginstitute.com/NotificacionAlumno/Webinar/ConfirmarParticipacion?ses={reemplazoEtiqueta.IdPEspecificoSesion}&mat={reemplazoEtiqueta.IdMatriculaCabecera}&est=True";
                    string valor = $"<a href={link}> Confirmar Participación </a>";
                    ReemplazarCuerpoCorreoWhatsApp("{T_PEspecifico.LinkConfirmacionWebinarPositiva}", valor);
                }

                if (_plantillaCorreo.Cuerpo.Contains("{ResumenGrabacion.Nombre}"))
                    ReemplazarCuerpoCorreoWhatsApp("{ResumenGrabacion.Nombre}", resumenGrabacionOnline.Nombre);

                if (_plantillaCorreo.Cuerpo.Contains("{PEspecifico.Nombre}"))
                    ReemplazarCuerpoCorreoWhatsApp("{PEspecifico.Nombre}", pespecifico);

                if (_plantillaCorreo.Cuerpo.Contains("{ProcesamientoTipoGenerar.RegistroUrl}"))
                    ReemplazarCuerpoCorreoWhatsApp("{ProcesamientoTipoGenerar.RegistroUrl}", resumenRegistroUrl.RegistroUrl);

                if (plantilla.IdPlantillaBase == PlantillaBase.Email)
                {
                    emailReemplazado.Asunto = _plantillaCorreo.Asunto;
                    emailReemplazado.CuerpoHTML = _plantillaCorreo.Cuerpo;
                }
                else if (plantilla.IdPlantillaBase == PlantillaBase.WhatsappFacebook)
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;

                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                else if (plantilla.IdPlantillaBase == PlantillaBase.Certificado || plantilla.IdPlantillaBase == PlantillaBase.Constancia)
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;
                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }

                return (emailReemplazado, whatsAppReemplazado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 20/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla
        /// </summary>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetasComercial(ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta)
        {
            PlantillaEmailMandrillDTO emailReemplazado = new();
            PlantillaWhatsAppCalculadoDTO whatsAppReemplazado = new();
            try
            {
                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(reemplazoEtiqueta.IdOportunidad);
                if (oportunidad == null || oportunidad.Id == 0)
                    throw new BadRequestException("Oportunidad no existente!");

                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(reemplazoEtiqueta.IdPlantilla);
                if (plantilla == null || plantilla.Id == 0)
                    throw new BadRequestException("Plantilla no valido!");

                var plantillaBase = _unitOfWork.PlantillaBaseRepository.ObtenerPorId(plantilla.IdPlantillaBase);
                if (plantillaBase == null || plantillaBase.Id == 0)
                    throw new BadRequestException("Alumno no valido!");

                _plantillaCorreo = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(plantilla.Id);
                var datosCompuestosOportunidad = _unitOfWork.OportunidadRepository.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(oportunidad.IdPersonalAsignado.GetValueOrDefault());
                var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId((Int32)datosCompuestosOportunidad.IdAlumno);

                var centroCosto = _unitOfWork.CentroCostoRepository.ObtenerPorId((int)oportunidad.IdCentroCosto);
                var pespecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(centroCosto.Id);
                var pgeneral = _unitOfWork.PGeneralRepository.ObtenerPorId((int)pespecifico.IdProgramaGeneral);

                var listaEtiqueta = _plantillaCorreo.Cuerpo.Split("{", StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split("}", StringSplitOptions.None).First());

                _listaObjetoWhatsApp = listaEtiqueta.Select(x => new DatoPlantillaWhatsAppDTO
                {
                    Codigo = string.Concat("{", x, "}"),
                    Texto = ""
                }).ToList();

                _idPlantillaBase = plantilla.IdPlantillaBase;
                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.NombreCompleto}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.NombreCompleto}", $"{personal.Nombre1} {personal.Nombre2} {personal.ApellidoPaterno} {personal.ApellidoMaterno}");

                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.Nombre1}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.Nombre1}", personal.Nombre1);

                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.Apellidos}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.Apellidos}", $"{personal.ApellidoPaterno} {personal.ApellidoMaterno}");

                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.email}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.email}", personal.Email);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Nombre1}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tAlumno.Nombre1}", alumno.Nombre1);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Nombre2}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tAlumno.Nombre2}", alumno.Nombre2);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Apellido}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tAlumno.Apellido}", $"{alumno.ApellidoPaterno} {alumno.ApellidoMaterno}");

                if (_plantillaCorreo.Cuerpo.Contains("{tPegeneral.Nombre}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPegeneral.Nombre}", pgeneral.Nombre);

                if (_plantillaCorreo.Cuerpo.Contains("{tPegeneral.Duracion}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPegeneral.Duracion}", pgeneral.PwDuracion);

                if (_plantillaCorreo.Cuerpo.Contains("{tPegeneral.Descripcion}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPegeneral.Descripcion}", pgeneral.PwDescripcionGeneral);

                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.PrimerNombreApellidoPaterno}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.PrimerNombreApellidoPaterno}", $"{personal.Nombre1} {personal.ApellidoPaterno}");

                List<string> listaEtiquetasMedioPago = new List<string>()
                {
                    "{BCP_soles}",
                    "{BCP_CCI_soles}",
                    "{BCP_dolares}",
                    "{BCP_CCI_dolares}",
                    "{BBVA_soles}",
                    "{BBVA_CCI_soles}",
                    "{Scotiabank_soles}",
                    "{Scotiabank_CCI_soles}",
                    "{Num_YAPE}",
                    "{Swift_Code}",
                    "{CuentaUSD}",
                    "{BanCol}",
                    "{Bancol_suc}",
                    "{BCP_Bolivianos}",
                    "{BCP_Dolares_Bolivia}",
                    "{BUnion_Bolivianos}",
                    "{BUnion_Dolares_Bolivia}",
                    "{BBVA_MXpesos}",
                    "{BBVA_CCI_MXpesos}",
                    "{BBVA_MXdol}",
                    "{BBVA_CCI_MXdol}",
                    "{Banorte_MXpesos}",
                    "{Banorte_CCI_MXpesos}",
                    "{Banorte_MXdol}",
                    "{Banorte_CCI_MXdol}"
                };

                if (listaEtiquetasMedioPago.Any(_plantillaCorreo.Cuerpo.Contains))
                {
                    AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                    foreach (string etiqueta in listaEtiquetasMedioPago)
                    {
                        if (_plantillaCorreo.Cuerpo.Contains(etiqueta))
                        {
                            string valor = alumnoService.ObtenerMedioPago(etiqueta);
                            ReemplazarCuerpoCorreoWhatsApp(etiqueta, valor);
                        }
                    }
                }

                if (_plantillaCorreo.Cuerpo.Contains("{tpla_pgeneral.pw_duracion}"))
                {
                    if (plantilla.IdPlantillaBase == PlantillaBase.Email)
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tpla_pgeneral.pw_duracion}", datosCompuestosOportunidad.PwDuracion);
                    else
                        _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals("{tpla_pgeneral.pw_duracion}")).Texto = datosCompuestosOportunidad.PwDuracion;
                }

                //Horario atencion personal
                if (_plantillaCorreo.Cuerpo.Contains("{T_Personal.HorarioAtencion}"))
                {
                    string valor = _unitOfWork.PersonalRepository.ObtenerHorarioTrabajo(oportunidad.IdPersonalAsignado.Value);
                    ReemplazarCuerpoCorreoWhatsApp("{T_Personal.HorarioAtencion}", valor);
                }

                //Anexo personal
                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.Anexo}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.Anexo}", personal.Anexo);

                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.nombres}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.nombres}", personal.Nombres);

                //Apellidos del personal
                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.apellidos}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.apellidos}", personal.Apellidos);

                //Horario atencion personal
                if (_plantillaCorreo.Asunto.Contains("{T_Personal.HorarioAtencion}"))
                {
                    string valor = _unitOfWork.PersonalRepository.ObtenerHorarioTrabajo(oportunidad.IdPersonalAsignado.Value);
                    ReemplazarAsuntoCorreo("{T_Personal.HorarioAtencion}", valor);
                }

                //Anexo personal
                if (_plantillaCorreo.Asunto.Contains("{tPersonal.Anexo}"))
                    ReemplazarAsuntoCorreo("{tPersonal.Anexo}", personal.Anexo);

                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlDescargarAplicativoAndroid%7D", "{Link.UrlDescargarAplicativoAndroid}");
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlDescargarAplicativoIOS%7D", "{Link.UrlDescargarAplicativoIOS}");
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlGuiaAccederSesionWebinarPorVideo%7D", "{Link.UrlGuiaAccederSesionWebinarPorVideo}");
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlAulaVirtual%7D", "{Link.UrlAulaVirtual}");
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlManualBSPlay%7D", "{Link.UrlManualBSPlay}");
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlPortalWeb%7D", "{Link.UrlPortalWeb}");

                if (_plantillaCorreo.Cuerpo.Contains("{T_Personal.FirmaCorreo}"))
                    ReemplazarCuerpoCorreoWhatsApp("{T_Personal.FirmaCorreo}", personal.FirmaHtml);

                //Templates
                if (_plantillaCorreo.Cuerpo.Contains("Template"))
                {
                    const int indexIdPlantilla = 3;
                    const int indexIdColumna = 4;
                    if (plantilla.IdPlantillaBase == PlantillaBase.Email)
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template")))
                        {
                            string[] array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                            SeccionEtiquetaDTO valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(array[indexIdColumna]), new Guid(array[indexIdColumna]), oportunidad.IdCentroCosto ?? default);
                            if (valor != null)
                            {
                                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(item.Codigo, valor.Valor);
                            }

                        }
                    }
                    else if (plantilla.IdPlantillaBase == PlantillaBase.WhatsappFacebook)
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template")))
                        {
                            string[] array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                            SeccionEtiquetaDTO valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(array[indexIdColumna]), new Guid(array[indexIdColumna]), oportunidad.IdCentroCosto ?? default);
                            if (valor != null)
                            {
                                _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor.Valor;
                            }

                        }
                    }
                    else
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template")))
                        {
                            string[] array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                            SeccionEtiquetaDTO valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(array[indexIdColumna]), new Guid(array[indexIdColumna]), oportunidad.IdCentroCosto ?? default);
                            _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor.Valor.Replace("<p>", "<p id='estructura'>");
                        }
                    }
                }

                DateTime fechaActual = DateTime.Now;

                if (_plantillaCorreo.Cuerpo.Contains("{ValorDinamico.DiaFechaActual}"))
                    ReemplazarCuerpoCorreoWhatsApp("{ValorDinamico.DiaFechaActual}", fechaActual.Day.ToString());

                if (_plantillaCorreo.Cuerpo.Contains("{ValorDinamico.NombreMesFechaActual}"))
                    ReemplazarCuerpoCorreoWhatsApp("{ValorDinamico.NombreMesFechaActual}", fechaActual.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")));

                if (_plantillaCorreo.Cuerpo.Contains("{ValorDinamico.AnioFechaActual}"))
                    ReemplazarCuerpoCorreoWhatsApp("{ValorDinamico.AnioFechaActual}", fechaActual.Year.ToString());

                if (plantilla.IdPlantillaBase == PlantillaBase.Email)
                {
                    emailReemplazado.Asunto = _plantillaCorreo.Asunto;
                    emailReemplazado.CuerpoHTML = _plantillaCorreo.Cuerpo;
                }
                else if (plantilla.IdPlantillaBase == PlantillaBase.WhatsappFacebook)
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;

                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                else if (plantilla.IdPlantillaBase == PlantillaBase.Certificado || plantilla.IdPlantillaBase == PlantillaBase.Constancia)
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;
                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                return (emailReemplazado, whatsAppReemplazado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PlantillaWhatsAppCalculadoDTO ReemplazarValoresTilde(PlantillaWhatsAppCalculadoDTO body)
        {

            body.Plantilla = body.Plantilla.Replace("á", "a");
            body.Plantilla = body.Plantilla.Replace("é", "e");
            body.Plantilla = body.Plantilla.Replace("í", "i");
            body.Plantilla = body.Plantilla.Replace("ó", "o");
            body.Plantilla = body.Plantilla.Replace("ú", "u");

            body.Plantilla = body.Plantilla.Replace("Á", "A");
            body.Plantilla = body.Plantilla.Replace("É", "E");
            body.Plantilla = body.Plantilla.Replace("Í", "I");
            body.Plantilla = body.Plantilla.Replace("Ó", "O");
            body.Plantilla = body.Plantilla.Replace("Ú", "U");

            //Elimina las Ñ
            body.Plantilla = body.Plantilla.Replace("ñ", "n");
            body.Plantilla = body.Plantilla.Replace("Ñ", "N");

            foreach (var item in body.ListaEtiquetas)
            {
                item.Texto = item.Texto.Replace("á", "a");
                item.Texto = item.Texto.Replace("é", "e");
                item.Texto = item.Texto.Replace("í", "i");
                item.Texto = item.Texto.Replace("ó", "o");
                item.Texto = item.Texto.Replace("ú", "u");

                item.Texto = item.Texto.Replace("Á", "A");
                item.Texto = item.Texto.Replace("É", "E");
                item.Texto = item.Texto.Replace("Í", "I");
                item.Texto = item.Texto.Replace("Ó", "O");
                item.Texto = item.Texto.Replace("Ú", "U");

                //Elimina las Ñ
                item.Texto = item.Texto.Replace("ñ", "n");
                item.Texto = item.Texto.Replace("Ñ", "N");
            }

            ////prueba 
            //string carlos = "Oficial de Preparación para el";
            //carlos = carlos.Replace("ó", "&oacute;");

            return body;
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos de oportunidades sin matriculas
        /// </summary>
        /// <param name="personalPorDefecto">Flag para determinar si se usara el personal por defecto que se encuentra en la tabla conf.T_ConfiguracionFija</param>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado, PlantillaSmsCalculadoDTO SmsReemplazado) ReemplazarEtiquetasNuevasOportunidades(ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta, bool personalPorDefecto = false, int idCentroCosto = 0)
        {
            try
            {
                IPEspecificoService pEspecificoService = new PEspecificoService(_unitOfWork);

                PlantillaSmsCalculadoDTO smsReemplazado = new();
                PlantillaEmailMandrillDTO emailReemplazado = new();
                PlantillaWhatsAppCalculadoDTO whatsAppReemplazado = new();

                var listadoEtiquetaService = new ListadoEtiquetaService(_unitOfWork);
                if (!_unitOfWork.OportunidadRepository.Exist(reemplazoEtiqueta.IdOportunidad))
                    throw new BadRequestException("Oportunidad no existente!");

                Oportunidad oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(reemplazoEtiqueta.IdOportunidad);
                oportunidad.IdCentroCosto = idCentroCosto > 0 ? idCentroCosto : oportunidad.IdCentroCosto;

                if (!_unitOfWork.AlumnoRepository.Exist(oportunidad.IdAlumno.Value))
                    throw new BadRequestException("Alumno no existente");

                Alumno alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(oportunidad.IdAlumno.Value);

                if (!_unitOfWork.PlantillaRepository.Exist(reemplazoEtiqueta.IdPlantilla))
                    throw new BadRequestException("Plantilla no existente");

                Plantilla plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(reemplazoEtiqueta.IdPlantilla);

                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                    throw new BadRequestException("Plantilla base no existente");

                _idPlantillaBase = plantilla.IdPlantillaBase;

                PlantillaAsuntoCuerpoDTO plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(reemplazoEtiqueta.IdPlantilla);
                _plantillaCorreo = plantillaBase;

                int idPersonalFinal = personalPorDefecto ? ValorEstatico.IdPersonalCorreoPorDefecto : oportunidad.IdPersonalAsignado.Value;

                if (!_unitOfWork.PersonalRepository.Exist(idPersonalFinal))
                    throw new BadRequestException("Personal no asignado");

                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(idPersonalFinal);

                if (!_unitOfWork.CentroCostoRepository.Exist(oportunidad.IdCentroCosto.GetValueOrDefault()))
                    throw new BadRequestException("Centro de costo no existente");

                CentroCosto centroCosto = _unitOfWork.CentroCostoRepository.ObtenerPorId(oportunidad.IdCentroCosto.GetValueOrDefault());

                PEspecificoPorIdCentroCostoDTO pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(centroCosto.Id);

                if (pEspecifico == null || pEspecifico.Id == 0)
                    throw new BadRequestException("Programa especifico no existente");

                PGeneral pGeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(pEspecifico.IdProgramaGeneral.Value);

                if (pGeneral == null || pGeneral.Id == 0)
                    throw new BadRequestException("Programa general no existente");


                PartnerPw partnerPw = _unitOfWork.PartnerPwRepository.ObtenerPorId(pGeneral.IdPartner.Value);

                List<string> listaEtiqueta = _plantillaCorreo.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                _listaObjetoWhatsApp = listaEtiqueta.Select(x => new DatoPlantillaWhatsAppDTO
                {
                    Codigo = string.Concat("{", x, "}"),
                    Texto = string.Empty
                }).ToList();

                // Seccion Asunto
                // Nombre del programa general
                if (plantillaBase.Asunto.Contains("{tPLA_PGeneral.Nombre}"))
                    ReemplazarAsuntoCorreo("{tPLA_PGeneral.Nombre}", pGeneral.Nombre);

                // Primer nombre del alumno
                if (plantillaBase.Cuerpo.Contains("{tAlumnos.nombre1}"))
                {
                    if (plantilla.IdPlantillaBase == PlantillaBase.Email || plantilla.IdPlantillaBase == PlantillaBase.MensajeTexto)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tAlumnos.nombre1}", alumno.Nombre1);
                    }
                    else if (plantilla.IdPlantillaBase == PlantillaBase.WhatsappFacebook
                        || plantilla.IdPlantillaBase == PlantillaBase.Certificado
                        || plantilla.IdPlantillaBase == PlantillaBase.Constancia)
                    {
                        _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals("{tAlumnos.nombre1}")).Texto = alumno.Nombre1;
                    }
                }
                // Primer nombre del alumno
                if (plantillaBase.Cuerpo.Contains("{tAlumnos.apepaterno}"))
                {
                    if (plantilla.IdPlantillaBase == PlantillaBase.Email || plantilla.IdPlantillaBase == PlantillaBase.MensajeTexto)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tAlumnos.apepaterno}", alumno.Nombre1);
                    }
                    else if (plantilla.IdPlantillaBase == PlantillaBase.WhatsappFacebook
                        || plantilla.IdPlantillaBase == PlantillaBase.Certificado
                        || plantilla.IdPlantillaBase == PlantillaBase.Constancia)
                    {
                        _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals("{tAlumnos.apepaterno}")).Texto = alumno.Nombre1;
                    }
                }
                // Nombres del personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.nombres}"))
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPersonal.nombres}", personal.Nombres);

                // Apellidos del personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.apellidos}"))
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPersonal.apellidos}", personal.Apellidos);

                //Telefono del personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.Telefono}"))
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPersonal.Telefono}", listadoEtiquetaService.ObtenerTelefonoPersonal(personal.Central, personal.Anexo3Cx));

                //Firma del personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.UrlFirmaCorreos}"))
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPersonal.UrlFirmaCorreos}", listadoEtiquetaService.EtiquetaUrlFirmaCorreo(personal.Email));

                /*Revisar versiones un programa*/
                // Monto Pago de versiones
                if (plantillaBase.Cuerpo.Contains("{TPW_MontoPago.Versiones}"))
                {
                    string valor = listadoEtiquetaService.EtiquetaMontosPagoV2(oportunidad, pGeneral.Id);
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{TPW_MontoPago.Versiones}", valor);
                }

                // Encabezado de correo del partner
                if (plantillaBase.Cuerpo.Contains("{TPW_Partner.EncabezadoCorreoPartner}"))
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{TPW_Partner.EncabezadoCorreoPartner}", partnerPw.EncabezadoCorreoPartner);

                // Lista de programas segun Cursos Ti1
                if (plantillaBase.Cuerpo.Contains("{NoTabla.ListaProgramasCursosTi1}"))
                {
                    string valor = listadoEtiquetaService.EtiquetaListaProgramasPorIdEtiqueta(reemplazoEtiqueta.IdOportunidad, ValorEstatico.IdListaCursoAreaEtiquetaTi1);
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{NoTabla.ListaProgramasCursosTi1}", valor);
                }

                // Nombre del programa general
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPLA_PGeneral.Nombre}", pGeneral.Nombre);

                // Version del programa general
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.urlVersion}"))
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPLA_PGeneral.urlVersion}", listadoEtiquetaService.ObtenerUrlVersion(pGeneral.UrlVersion));

                // Programas, cursos relacionados
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.CursosRelacionados}"))
                {
                    string valor = listadoEtiquetaService.EtiquetaCursoRelacionado(centroCosto.Id);
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPLA_PGeneral.CursosRelacionados}", valor);
                }

                // Brochure del programa general
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.UrlBrochurePrograma}"))
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPLA_PGeneral.UrlBrochurePrograma}", listadoEtiquetaService.ObtenerUrlBrochurePrograma(pGeneral.UrlBrochurePrograma));

                // Expositores
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Expositores}"))
                {
                    string valor = listadoEtiquetaService.EtiquetaExpositor(pGeneral.Id);
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPLA_PGeneral.Expositores}", valor);
                }

                // Duracion y Horarios
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.DuracionAndHorarios}"))
                {
                    string valor = listadoEtiquetaService.ObtenerDuracionAndHorario(pGeneral.Id);
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPEspecifico.DuracionAndHorarios}", valor);
                }

                // Ciudad del programa especifico
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.ciudad}"))
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPEspecifico.ciudad}", pEspecifico.Ciudad);

                // Fecha inicio del programa especifico
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.FechaInicioPrograma}"))
                {
                    string valor = pEspecificoService.FechaInicioProgramaV2(pGeneral.Id, pEspecifico.Id);
                    valor = string.IsNullOrEmpty(valor) ? "Por definir" : valor;
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPEspecifico.FechaInicioPrograma}", valor);
                }

                // Url Documento del programa especifico
                if (plantillaBase.Cuerpo.Contains("{tPEspecifico.UrlDocumentoCronograma}"))
                {
                    string valor = listadoEtiquetaService.ObtenerUrlDocumentoCronograma(pEspecifico.UrlDocumentoCronograma);
                    ReemplazarCuerpoCorreoMensajeTextoWhatsApp("{tPEspecifico.UrlDocumentoCronograma}", valor);
                }

                // Templates V2
                if (plantillaBase.Cuerpo.Contains("TemplateV2"))
                {
                    const string ETIQUETA_VACIO = "<vacio></vacio>";

                    var documentoService = new DocumentoService(_unitOfWork);
                    var listaSecciones = documentoService.ObtenerListaSeccionDocumentoProgramaGeneral(pGeneral.Id);
                    var listaSeccionesDocumentoV2 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV2(pGeneral.Id);

                    foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("TemplateV2")))
                    {
                        string[] array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                        string valor = string.Empty;

                        string nombreSeccion = array[array.Length - 1];
                        bool conTitulo = nombreSeccion == "Estructura Curricular";
                        string descripcionAdicional = string.Concat("Descripci&#243;n ", nombreSeccion.Split(" ")[0]);

                        var seccion = listadoEtiquetaService.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena(nombreSeccion).ToLower()).ToList(), conTitulo);

                        valor = seccion.Aggregate(valor, (current, item01) => current + item01.Contenido);

                        // Unir Descripcion adicional de etiquetas que tienen dicho contenido
                        if (listaSeccionesDocumentoV2.Exists(x => x.Titulo == descripcionAdicional))
                        {
                            string descripcion = listaSeccionesDocumentoV2.First(x => x.Titulo == descripcionAdicional).Contenido;
                            valor += descripcion != ETIQUETA_VACIO ? descripcion.Replace(ETIQUETA_VACIO, string.Empty) : string.Empty;
                        }

                        // Sacar etiquetas no agrupadas de V2
                        if (listaSeccionesDocumentoV2.Any())
                            valor += valor.Equals(string.Empty) ? listaSeccionesDocumentoV2.First(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(nombreSeccion).ToLower()).Contenido : string.Empty;

                        // Obtener etiquetas de V1 si en caso no encuentra
                        if (valor.Equals(string.Empty))
                        {
                            //nombreSeccion = nombreSeccion == "Certificacion" ? "Certificación" : nombreSeccion;
                            List<SeccionDocumentoDTO> seccionV1 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSecciones(pGeneral.Id).Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(nombreSeccion).ToLower()).ToList();
                            valor = seccionV1.Aggregate(valor, (current, item01) => current + item01.Contenido);
                        }

                        // Asignar valores
                        if (plantilla.IdPlantillaBase.Equals(2))
                            _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(item.Codigo, valor ?? string.Empty);
                        else if (plantilla.IdPlantillaBase.Equals(8))
                            _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor;
                        else
                            _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor.Replace("<p>", "<p id='estructura'>");
                    }
                }

                // Templates
                if (plantillaBase.Cuerpo.Contains("Template"))
                {
                    // Templates V1
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template") && !x.Codigo.Contains("V2")))
                        {
                            var array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");

                            var idPlantilla = array[3];
                            var idColumna = array[4];

                            var valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(idPlantilla), new Guid(idColumna), oportunidad.IdCentroCosto ?? default);

                            if (valor == null)
                                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(item.Codigo, "");
                            else
                                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(item.Codigo, valor.Valor);
                        }

                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template")))
                        {
                            var array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");

                            var idPlantilla = array[3];
                            var idColumna = array[4];

                            var valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(idPlantilla), new Guid(idColumna), oportunidad.IdCentroCosto ?? default);
                            _listaObjetoWhatsApp.Where(x => x.Codigo.Equals(item.Codigo)).FirstOrDefault().Texto = valor.Valor;
                        }
                    }
                    else
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template")))
                        {
                            var array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                            var idPlantilla = array[3];
                            var idColumna = array[4];
                            var valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(idPlantilla), new Guid(idColumna), oportunidad.IdCentroCosto ?? default);
                            if (valor != null)
                            {
                                _listaObjetoWhatsApp.Where(x => x.Codigo.Equals(item.Codigo)).FirstOrDefault().Texto = valor.Valor.Replace("<p>", "<p id='estructura'>");
                            }

                        }
                    }
                }
                if (_idPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    emailReemplazado.Asunto = _plantillaCorreo.Asunto;
                    emailReemplazado.CuerpoHTML = _plantillaCorreo.Cuerpo;
                }
                else if (_idPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;
                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                else if (_idPlantillaBase == PlantillaBase.MensajeTexto)
                {
                    smsReemplazado.Cuerpo = _plantillaCorreo.Cuerpo;
                }
                else if (_idPlantillaBase == PlantillaBase.Certificado || _idPlantillaBase == PlantillaBase.Constancia)
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;
                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                return (emailReemplazado, whatsAppReemplazado, smsReemplazado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos sin oportunidad
        /// </summary>
        /// <param name="idPersonal">Id del Personal que envia el correo</param>
        /// <param name="idCentroCosto">Id del Centro de Costo enlazado al programa general</param>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetasSinOportunidad(int idPlantilla, int idOportunidad, int? idPersonal, int idCentroCosto)
        {
            try
            {
                IPEspecificoService pEspecificoService = new PEspecificoService(_unitOfWork);
                IListadoEtiquetaService listadoEtiquetaService = new ListadoEtiquetaService(_unitOfWork);
                IDocumentoAgendaService documentoAgendaService = new DocumentoAgendaService(_unitOfWork);

                PlantillaEmailMandrillDTO emailReemplazado = new();
                PlantillaWhatsAppCalculadoDTO whatsAppReemplazado = new();

                var plantilla = new Plantilla();

                if (!_unitOfWork.PlantillaRepository.Exist(idPlantilla))
                    throw new Exception("Plantilla no existente");

                plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantilla);

                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                    throw new Exception("Plantilla base no existente");

                _idPlantillaBase = plantilla.IdPlantillaBase;

                _plantillaCorreo = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(idPlantilla);

                var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(idCentroCosto);

                if (pEspecifico == null || pEspecifico.Id == 0)
                    throw new Exception("Programa especifico no existente");

                PGeneral pgeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(pEspecifico.IdProgramaGeneral.Value);
                if (pgeneral == null)
                {
                    throw new Exception("Programa general no existente");
                }
                var partnerPw = _unitOfWork.PartnerPwRepository.ObtenerPorId(pgeneral.IdPartner.Value);

                List<string> listaEtiqueta = _plantillaCorreo.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                _listaObjetoWhatsApp = listaEtiqueta.Select(x => new DatoPlantillaWhatsAppDTO
                {
                    Codigo = string.Concat("{", x, "}"),
                    Texto = string.Empty
                }
                ).ToList();

                /*Lista de etiquetas para la generacion de plantillas genericas*/
                if (idPersonal.HasValue)
                {
                    var personal = _unitOfWork.PersonalRepository.ObtenerPorId(idPersonal.Value);
                    if (personal == null || personal.Id == 0)
                        throw new Exception("Personal no existe");
                    /*Nombres del personal*/
                    if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.nombres}"))
                        ReemplazarCuerpoCorreoWhatsApp("{tPersonal.nombres}", personal.Nombres);

                    /*Apellidos del personal*/
                    if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.apellidos}"))
                        ReemplazarCuerpoCorreoWhatsApp("{tPersonal.apellidos}", personal.Apellidos);

                    /*Telefono del personal*/
                    if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.Telefono}"))
                        ReemplazarCuerpoCorreoWhatsApp("{tPersonal.Telefono}", listadoEtiquetaService.ObtenerTelefonoPersonal(personal.Central, personal.Anexo3Cx));

                    //Firma del personal
                    if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.UrlFirmaCorreos}"))
                        ReemplazarCuerpoCorreoWhatsApp("{tPersonal.UrlFirmaCorreos}", listadoEtiquetaService.EtiquetaUrlFirmaCorreo(personal.Email));
                }

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumnos.nombre1}"))
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{tAlumnos.nombre1}", "");

                /*Nombre del programa general*/
                if (_plantillaCorreo.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{tPLA_PGeneral.Nombre}", pgeneral.Nombre);

                // Version del programa general
                if (_plantillaCorreo.Cuerpo.Contains("{tPLA_PGeneral.urlVersion}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPersonal.UrlFirmaCorreos}", listadoEtiquetaService.ObtenerUrlVersion(pgeneral.UrlVersion));

                // Version del programa general
                if (_plantillaCorreo.Cuerpo.Contains("{tPLA_PGeneral.UrlBrochurePrograma}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPLA_PGeneral.UrlBrochurePrograma}", listadoEtiquetaService.ObtenerUrlBrochurePrograma(pgeneral.UrlBrochurePrograma));

                // Ciudad del programa especifico
                if (_plantillaCorreo.Cuerpo.Contains("{tPEspecifico.ciudad}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPEspecifico.ciudad}", pEspecifico.Ciudad);

                // Fecha inicio del programa especifico
                if (_plantillaCorreo.Cuerpo.Contains("{tPEspecifico.FechaInicioPrograma}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPEspecifico.FechaInicioPrograma}", pEspecificoService.FechaInicioProgramaV2(pgeneral.Id, pEspecifico.Id));

                // Encabezado de correo del partner
                if (_plantillaCorreo.Cuerpo.Contains("{TPW_Partner.EncabezadoCorreoPartner}"))
                    ReemplazarCuerpoCorreoWhatsApp("{TPW_Partner.EncabezadoCorreoPartner}", partnerPw != null ? partnerPw.EncabezadoCorreoPartner : string.Empty);

                // Duracion y Horarios
                if (_plantillaCorreo.Cuerpo.Contains("{tPEspecifico.DuracionAndHorarios}"))
                {
                    string valor = listadoEtiquetaService.ObtenerDuracionAndHorario(pgeneral.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{tPEspecifico.DuracionAndHorarios}", valor);
                }

                // Lista de programas segun Cursos Ti1
                if (_plantillaCorreo.Cuerpo.Contains("{NoTabla.ListaProgramasCursosTi1}"))
                {
                    string valor = listadoEtiquetaService.EtiquetaListaProgramasPorIdEtiqueta(idOportunidad, 0);
                    ReemplazarCuerpoCorreoWhatsApp("{NoTabla.ListaProgramasCursosTi1}", valor);
                }

                // Expositores
                if (_plantillaCorreo.Cuerpo.Contains("{tPLA_PGeneral.Expositores}"))
                {
                    string valor = listadoEtiquetaService.EtiquetaExpositor(pgeneral.Id);
                    ReemplazarCuerpoCorreoWhatsApp("{tPLA_PGeneral.Expositores}", valor);
                }

                // Url Documento del programa especifico
                if (_plantillaCorreo.Cuerpo.Contains("{tPEspecifico.UrlDocumentoCronograma}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tPEspecifico.UrlDocumentoCronograma}", listadoEtiquetaService.ObtenerUrlDocumentoCronograma(pEspecifico.UrlDocumentoCronograma));

                // Programas, cursos relacionados
                if (_plantillaCorreo.Cuerpo.Contains("{tPLA_PGeneral.CursosRelacionados}"))
                {
                    string valor = listadoEtiquetaService.EtiquetaCursoRelacionado(idCentroCosto);
                    ReemplazarCuerpoCorreoWhatsApp("{tPLA_PGeneral.CursosRelacionados}", valor);
                }

                // Templates V2
                if (_plantillaCorreo.Cuerpo.Contains("TemplateV2"))
                {
                    const string ETIQUETA_VACIO = "<vacio></vacio>";

                    var listaSecciones = documentoAgendaService.ObtenerListaSeccionDocumentoProgramaGeneral(pgeneral.Id);
                    var listaSeccionesDocumentoV2 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV2(pgeneral.Id);

                    foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("TemplateV2")))
                    {
                        string[] array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                        string valor = string.Empty;

                        string nombreSeccion = array[array.Length - 1];
                        bool conTitulo = nombreSeccion == "Estructura Curricular";
                        string descripcionAdicional = string.Concat("Descripcion ", nombreSeccion.Split(" ")[0]);

                        var seccion = listadoEtiquetaService.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones.Where(x => LimpiarCadena(x.Seccion).ToLower() == LimpiarCadena(nombreSeccion).ToLower()).ToList(), conTitulo);

                        valor = seccion.Aggregate(valor, (current, item01) => current + item01.Contenido);

                        // Unir Descripcion adicional de etiquetas que tienen dicho contenido
                        if (listaSeccionesDocumentoV2.Exists(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(descripcionAdicional).ToLower()))
                        {
                            string descripcion = listaSeccionesDocumentoV2.First(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(descripcionAdicional).ToLower()).Contenido;

                            valor += descripcion != ETIQUETA_VACIO ? descripcion.Replace(ETIQUETA_VACIO, string.Empty) : string.Empty;
                        }

                        // Sacar etiquetas no agrupadas de V2
                        if (listaSeccionesDocumentoV2.Any())
                        {
                            nombreSeccion = nombreSeccion == "Certificacion" ? descripcionAdicional : nombreSeccion;
                            valor += valor.Equals(string.Empty) ? listaSeccionesDocumentoV2.FirstOrDefault(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(nombreSeccion).ToLower()).Contenido : string.Empty;
                        }
                        // Obtener etiquetas de V1 si en caso no encuentra
                        if (valor.Equals(string.Empty))
                        {
                            nombreSeccion = nombreSeccion == "Certificacion" ? "Certificación" : nombreSeccion;
                            List<SeccionDocumentoDTO> seccionV1 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSecciones(pgeneral.Id).Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(nombreSeccion).ToLower()).ToList();

                            valor = seccionV1.Aggregate(valor, (current, item01) => current + item01.Contenido);
                        }

                        // Asignar valores
                        if (plantilla.IdPlantillaBase.Equals(ValorEstatico.IdPlantillaBaseEmail))
                            _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(item.Codigo, valor ?? string.Empty);

                        else if (plantilla.IdPlantillaBase.Equals(ValorEstatico.IdPlantillaBaseWhatsAppFacebook))
                            _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor;
                        else
                            _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor.Replace("<p>", "<p id='estructura'>");
                    }
                }

                // Templates V3
                if (_plantillaCorreo.Cuerpo.Contains("TemplateV3"))
                {
                    const string etiquetavacio = "<vacio></vacio>";

                    var listaSecciones = documentoAgendaService.ObtenerListaSeccionDocumentoProgramaGeneral(pgeneral.Id);
                    var listaSeccionesDocumentoV2 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV2(pgeneral.Id);

                    foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("TemplateV3")))
                    {
                        string[] array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                        string valor = string.Empty;

                        string nombreSeccion = LimpiarCadena(array[array.Length - 1]);

                        if (nombreSeccion == "MercadoLaboral")
                        {
                            string descripcion = listaSeccionesDocumentoV2.First(x => LimpiarCadena(x.Titulo).ToLower() == "mercado laboral").Contenido;
                            valor += descripcion != etiquetavacio ? descripcion.Replace(etiquetavacio, string.Empty) : string.Empty;
                        }
                        if (nombreSeccion == "DescripcionEstructura")
                        {
                            string descripcion = listaSeccionesDocumentoV2.First(x => LimpiarCadena(x.Titulo).ToLower() == "descripcion estructura").Contenido;
                            valor += descripcion != etiquetavacio ? descripcion.Replace(etiquetavacio, string.Empty) : string.Empty;
                        }
                        if (nombreSeccion == "PerfilDelEgresado")
                        {
                            string descripcion = listaSeccionesDocumentoV2.First(x => LimpiarCadena(x.Titulo).ToLower() == "perfil del egresado").Contenido;

                            valor += descripcion != etiquetavacio ? descripcion.Replace(etiquetavacio, string.Empty) : string.Empty;
                        }
                        if (nombreSeccion == "Certificacion")
                        {
                            string descripcion = listaSeccionesDocumentoV2.First(x => LimpiarCadena(x.Titulo).ToLower() == "certificacion").Contenido;

                            valor += descripcion != etiquetavacio ? descripcion.Replace(etiquetavacio, string.Empty) : string.Empty;
                        }
                        if (nombreSeccion == "Duracion y Horarios")
                        {
                            string descripcion = listaSeccionesDocumentoV2.First(x => LimpiarCadena(x.Titulo).ToLower() == "duracion y horarios").Contenido;

                            valor += descripcion != etiquetavacio ? descripcion.Replace(etiquetavacio, string.Empty) : string.Empty;
                        }

                        if (plantilla.IdPlantillaBase.Equals(2))
                            _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(item.Codigo, valor ?? string.Empty);

                        //else if (plantilla.IdPlantillaBase.Equals(ValorEstatico.IdPlantillaBaseWhatsAppFacebook))
                        else if (plantilla.IdPlantillaBase.Equals(8))
                            _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor;
                        else
                            _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor.Replace("<p>", "<p id='estructura'>");
                    }
                }

                // Templates
                if (_plantillaCorreo.Cuerpo.Contains("Template"))
                {
                    // Templates V1
                    //if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    if (plantilla.IdPlantillaBase == 2)
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template") && !x.Codigo.Contains("V2")))
                        {
                            var array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");

                            var IdPlantillaArray = array[3];
                            var IdColumna = array[4];

                            var valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(IdPlantillaArray), new Guid(IdColumna), idCentroCosto);

                            if (valor == null)
                                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(item.Codigo, "");
                            else
                                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(item.Codigo, valor.Valor);
                        }

                    }
                    //else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    else if (plantilla.IdPlantillaBase == 8)
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template")))
                        {
                            var array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                            string idPlantillaPW = array[3];
                            string idColumnaPW = array[4];
                            var valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(idPlantillaPW), new Guid(idColumnaPW), idCentroCosto);
                            _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor.Valor;
                        }
                        //listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                    else
                    {
                        foreach (var item in _listaObjetoWhatsApp.Where(x => x.Codigo.Contains("Template")))
                        {
                            var array = item.Codigo.Replace("{", "").Replace("}", "").Split(".");
                            string idPlantillaPW = array[3];
                            string idColumnaPW = array[4];

                            var valor = _unitOfWork.PEspecificoRepository.ObtenerContenidoTemplate(new Guid(idPlantillaPW), new Guid(idColumnaPW), idCentroCosto);
                            _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(item.Codigo)).Texto = valor.Valor.Replace("<p>", "<p id='estructura'>");
                        }
                    }
                }

                if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    emailReemplazado.Asunto = _plantillaCorreo.Asunto;
                    emailReemplazado.CuerpoHTML = _plantillaCorreo.Cuerpo;
                }
                else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook || plantilla.IdPlantillaBase == 12 || plantilla.IdPlantillaBase == 13)
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;

                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                return (emailReemplazado, whatsAppReemplazado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para los correos que se envian cuando se solicitan certificados fisico
        /// </summary>
        /// <param name="datosAlumno">Tipo de dato DatosRegistroEnvioFisico: tiene los datos necesarios para el reemplazo de plantilla </param>
        /// <returns> Vacio, asigna a las propiedades locales los resultados </returns>
        public (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFisico(DatosRegistroEnvioFisicoDTO datosAlumno, int idPlantilla)
        {
            try
            {

                PlantillaEmailMandrillDTO emailReemplazado = new();
                PlantillaWhatsAppCalculadoDTO whatsAppReemplazado = new();

                Alumno alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(datosAlumno.IdAlumno);
                MatriculaCabecera matriculaCabecera = _unitOfWork.MatriculaCabeceraRepository.ObtenerPorId(datosAlumno.IdMatriculaCabecera);
                var detalleMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleMatricula(matriculaCabecera.Id);
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId((int)datosAlumno.IdPersonal);

                Plantilla plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantilla);

                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                    throw new BadRequestException("Plantilla base no existente");

                _idPlantillaBase = plantilla.IdPlantillaBase;
                _plantillaCorreo = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(idPlantilla);

                /*Lista de etiquetas para la generacion de plantillas genericas*/
                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Pais}"))
                    ReemplazarCuerpoCorreo("{tAlumno.Pais}", datosAlumno.Pais);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Region}"))
                    ReemplazarCuerpoCorreo("{tAlumno.Region}", datosAlumno.Region);
                /*Nombre del programa general*/
                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Ciudad}"))
                    ReemplazarCuerpoCorreo("{tAlumno.Ciudad}", datosAlumno.Ciudad);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Codigo}"))
                    ReemplazarCuerpoCorreo("{tAlumno.Codigo}", datosAlumno.CodigoPostal);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Direccion}"))
                    ReemplazarCuerpoCorreo("{tAlumno.Direccion}", datosAlumno.Direccion);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Referencia}"))
                    ReemplazarCuerpoCorreo("{tAlumno.Referencia}", datosAlumno.Referencia);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Nombres}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tAlumno.Nombres}", datosAlumno.Nombre);

                if (_plantillaCorreo.Cuerpo.Contains("{T_SolicitudCertificadoFisico.Courier}"))
                    ReemplazarCuerpoCorreoWhatsApp("{T_SolicitudCertificadoFisico.Courier}", datosAlumno.Courier);

                if (_plantillaCorreo.Cuerpo.Contains("{T_SolicitudCertificadoFisico.CodigoSeguimiento}"))
                    ReemplazarCuerpoCorreoWhatsApp("{T_SolicitudCertificadoFisico.CodigoSeguimiento}", datosAlumno.CodigoSeguimiento);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumnos.nombre1}"))
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{tAlumnos.nombre1}", alumno.Nombre1);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumnos.apepaterno}"))
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{tAlumnos.apepaterno}", alumno.ApellidoPaterno);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Apellido}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tAlumno.Apellido}", datosAlumno.Apellido);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.DNI}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tAlumno.DNI}", datosAlumno.DNI);

                if (_plantillaCorreo.Cuerpo.Contains("{tAlumno.Celular}"))
                    ReemplazarCuerpoCorreo("{tAlumno.Celular}", datosAlumno.Telefono);

                //nombre programa general
                if (_plantillaCorreo.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                    ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia("{tPLA_PGeneral.Nombre}", detalleMatriculaCabecera.NombreProgramaGeneral);

                if (_plantillaCorreo.Asunto.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace("{tPLA_PGeneral.Nombre}", detalleMatriculaCabecera.NombreProgramaGeneral);
                    }
                }
                if (_plantillaCorreo.Asunto.Contains("{tAlumno.Nombres}"))
                    ReemplazarCuerpoCorreoWhatsApp("{tAlumno.Nombres}", datosAlumno.Nombre);

                //reemplazar nombre 1 alumno
                if (_plantillaCorreo.Asunto.Contains("{tAlumnos.nombre1}"))
                    ReemplazarAsuntoCorreo("{tAlumnos.nombre1}", alumno.Nombre1);
                if (_plantillaCorreo.Cuerpo.Contains("{T_Personal.FirmaCorreoImagen}"))
                {
                    IPersonalService personalService = new PersonalService(_unitOfWork);
                    ReemplazarCuerpoCorreoWhatsApp("{T_Personal.FirmaCorreoImagen}", personalService.ObtenerFirmaCorreoImagen(personal.UrlFoto, alumno.IdCodigoPais, alumno.IdCiudad));
                }

                if (_idPlantillaBase == ValorEstatico.IdPlantillaBaseEmail) //ValorEstatico.IdPlantillaBaseEmail
                {
                    emailReemplazado.Asunto = _plantillaCorreo.Asunto;
                    emailReemplazado.CuerpoHTML = _plantillaCorreo.Cuerpo;
                }
                else if (_idPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook
                    || _idPlantillaBase == 12
                    || _idPlantillaBase == 13
                )
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;
                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                return (emailReemplazado, whatsAppReemplazado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/04/2023
        /// Version: 1.0
        /// <summary>
        /// Reemplazo de etiquetas para Plantilla Email WhatsApp
        /// </summary>
        /// <param name="etiqueta">Nombre Etiqueta</param>
        /// <param name="valor">Valor Reemplazo</param>
        private void ReemplazarCuerpoCorreoMensajeTextoWhatsApp(string etiqueta, string? valor)
        {
            if (_idPlantillaBase == PlantillaBase.Email || _idPlantillaBase == PlantillaBase.MensajeTexto)
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(etiqueta, valor ?? "");
            else if (_idPlantillaBase == PlantillaBase.WhatsappFacebook)
                _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(etiqueta)).Texto = valor ?? "";
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/04/2023
        /// Version: 1.0
        /// <summary>
        /// Reemplazo de etiquetas para Plantilla Email WhatsApp
        /// </summary>
        /// <param name="etiqueta">Nombre Etiqueta</param>
        /// <param name="valor">Valor Reemplazo</param>
        private void ReemplazarCuerpoCorreoWhatsApp(string etiqueta, string? valor)
        {
            if (_idPlantillaBase == PlantillaBase.Email)
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(etiqueta, valor ?? "");
            else if (_idPlantillaBase == PlantillaBase.WhatsappFacebook)
                _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(etiqueta)).Texto = valor ?? "";
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/04/2023
        /// Version: 1.0
        /// <summary>
        /// Reemplazo de etiquetas para Plantilla Email WhatsApp, Certificado, Constancia
        /// </summary>
        /// <param name="etiqueta">Nombre Etiqueta</param>
        /// <param name="valor">Valor Reemplazo</param>
        private void ReemplazarCuerpoCorreoWhatsAppCertificadoConstancia(string etiqueta, string? valor)
        {
            if (_idPlantillaBase == PlantillaBase.Email)
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(etiqueta, valor ?? "");
            else if (_idPlantillaBase == PlantillaBase.WhatsappFacebook
                || _idPlantillaBase == PlantillaBase.Certificado
                || _idPlantillaBase == PlantillaBase.Constancia)
                _listaObjetoWhatsApp.FirstOrDefault(x => x.Codigo.Equals(etiqueta)).Texto = valor ?? "";
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/04/2023
        /// Version: 1.0
        /// <summary>
        /// Reemplazo de etiquetas para Plantilla Correo y Agregar Nuevo Item a Plantilla WhatsApp
        /// </summary>
        /// <param name="etiqueta">Nombre Etiqueta</param>
        /// <param name="valor">Valor Reemplazo</param>
        private void ReemplazarCuerpoCorreoAddItemWhatsApp(string etiqueta, string? valor)
        {
            if (_idPlantillaBase == PlantillaBase.Email)
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(etiqueta, valor ?? "");
            else if (_idPlantillaBase == PlantillaBase.WhatsappFacebook)
                _listaObjetoWhatsApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = etiqueta, Texto = valor ?? "" });
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/04/2023
        /// Version: 1.0
        /// <summary>
        /// Reemplazo de etiquetas para Plantilla Correo
        /// </summary>
        /// <param name="etiqueta">Nombre Etiqueta</param>
        /// <param name="valor">Valor Reemplazo</param>
        private void ReemplazarCuerpoCorreo(string etiqueta, string? valor)
        {
            if (_idPlantillaBase == PlantillaBase.Email)
                _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace(etiqueta, valor ?? "");
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 24/05/2023
        /// Version: 1.0
        /// <summary>
        /// Reemplazo de etiquetas para Plantilla Correo Asunto
        /// </summary>
        /// <param name="etiqueta">Nombre Etiqueta</param>
        /// <param name="valor">Valor Reemplazo</param>
        private void ReemplazarAsuntoCorreo(string etiqueta, string? valor)
        {
            if (_idPlantillaBase == PlantillaBase.Email)
                _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace(etiqueta, valor ?? "");
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

        /// Autor: Edmundo Llaza 
        /// Fecha: 2023-08-09
        /// Version: 1.0
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para los correos que se envian cuando se reasignan los docentes para revision de foro
        /// </summary>
        /// <param name="datosAlumno"></param>
        /// <param name="idPlantilla"></param>
        public (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetasEnvioCorreoAsignacionForoDocente(ForoCorreoDetalleDTO datosAlumno, int idPlantilla)
        {
            PlantillaEmailMandrillDTO emailReemplazado = new();
            PlantillaWhatsAppCalculadoDTO whatsAppReemplazado = new();
            try
            {
                var ListadoEtiqueta = new ListadoEtiquetaService(_unitOfWork);

                List<DatoPlantillaWhatsAppDTO> listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();

                //if (!_repPlantilla.Exist(IdPlantilla))
                //    throw new Exception("Plantilla no existente");

                //var plantilla = new Plantilla();

                Plantilla plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantilla);

                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                    throw new BadRequestException("Plantilla base no existente");

                _idPlantillaBase = plantilla.IdPlantillaBase;

                _plantillaCorreo = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(idPlantilla);

                /*Lista de etiquetas para la generacion de plantillas genericas*/
                if (_plantillaCorreo.Cuerpo.Contains("{docente.nombre}"))
                {
                    var valor = datosAlumno.NombreProveedor;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{docente.nombre}", valor);
                    }
                }

                if (_plantillaCorreo.Cuerpo.Contains("{programa.nombre}"))
                {
                    var valor = datosAlumno.NombreCurso;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{programa.nombre}", valor);
                    }
                }
                /*Asunto del foro*/
                if (_plantillaCorreo.Cuerpo.Contains("{foro.asunto}"))
                {
                    var valor = datosAlumno.Asunto;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{foro.asunto}", valor);
                    }
                }
                /*Consulta del foro*/
                if (_plantillaCorreo.Cuerpo.Contains("{foro.consulta}"))
                {
                    var valor = datosAlumno.Consulta;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{foro.consulta}", valor);
                    }
                }
                /*Fecha de la consulta del foro*/
                if (_plantillaCorreo.Cuerpo.Contains("{foro.fecha.consulta}"))
                {
                    var valor = datosAlumno.FechaConsulta;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{foro.fecha.consulta}", valor);
                    }
                }
                /*Fecha límite para respuesta del docente*/
                if (_plantillaCorreo.Cuerpo.Contains("{foro.fecha.limite}"))
                {
                    var valor = datosAlumno.FechaLimite;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{foro.fecha.limite}", valor);
                    }
                }

                //Firma del personal
                if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.UrlFirmaCorreos}"))
                {
                    string firma = ListadoEtiqueta.EtiquetaUrlFirmaCorreo(datosAlumno.Email);

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tPersonal.UrlFirmaCorreos}", firma);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{tPersonal.UrlFirmaCorreos}")).FirstOrDefault().Texto = firma;
                    }
                }
                if (this._idPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    emailReemplazado.Asunto = _plantillaCorreo.Asunto;
                    emailReemplazado.CuerpoHTML = _plantillaCorreo.Cuerpo;
                }
                else if (this._idPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;

                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                else if (this._idPlantillaBase == 12 || this._idPlantillaBase == 13)
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;

                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                return (emailReemplazado, whatsAppReemplazado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-13
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para los correos que se envian a proveedores
        /// </summary>
        /// <param name="etiqueta"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        public (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) ReemplazarEtiquetasProveedor(ReemplazoEtiquetaPlantillaDTO etiqueta)
        {
            PlantillaEmailMandrillDTO emailReemplazado = new();
            PlantillaWhatsAppCalculadoDTO whatsAppReemplazado = new();
            try
            {
                var ListadoEtiqueta = new ListadoEtiquetaService(_unitOfWork);

                List<DatoPlantillaWhatsAppDTO> listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();

                Plantilla plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(etiqueta.IdPlantilla);
                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                {
                    throw new BadRequestException("Plantilla base no existente");
                }
                var plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(etiqueta.IdPlantilla);
                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                _idPlantillaBase = plantilla.IdPlantillaBase;
                _plantillaCorreo = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(etiqueta.IdPlantilla);

                foreach (var iten in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = "" });
                }
                var detalleEnvioMaterialProveedorImpresion = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerDetalleMaterialPEspecificoEnviarProveedor(etiqueta.IdMaterialPEspecificoDetalle);
                if (_plantillaCorreo.Cuerpo.Contains("{T_Proveedor.NombreContacto}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombreProveedor;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{T_Proveedor.NombreContacto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Proveedor.NombreContacto}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Cuerpo.Contains("{T_PEspecificoSesion.NombreDocente}"))
                {
                    var valor = "docnete temporal";
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{T_PEspecificoSesion.NombreDocente}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_PEspecificoSesion.NombreDocente}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Cuerpo.Contains("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.UrlArchivo;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Cuerpo.Contains("{T_MaterialPEspecificoDetalle.DireccionEntrega}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.DireccionEntrega;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{T_MaterialPEspecificoDetalle.DireccionEntrega}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_MaterialPEspecificoDetalle.DireccionEntrega}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Cuerpo.Contains("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.FechaEntrega.ToString("dd/MM/yyyy");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Cuerpo.Contains("{T_CentroCosto.Nombre}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombreCentroCosto;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{T_CentroCosto.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_CentroCosto.Nombre}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Cuerpo.Contains("{tPEspecifico.nombre}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombrePEspecifico;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tPEspecifico.nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{tPEspecifico.nombre}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Cuerpo.Contains("{tPEspecifico.Grupo}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.Grupo.ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tPEspecifico.Grupo}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{tPEspecifico.Grupo}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Cuerpo.Contains("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}"))
                {
                    var valorCantidad = detalleEnvioMaterialProveedorImpresion.Cantidad;
                    var valorTipoMaterial = detalleEnvioMaterialProveedorImpresion.NombreMaterialTipo;

                    var valor = $@"
                               <span>
                                    Tipo: {valorTipoMaterial}
                                    <br>
                                    Cantidad: {valorCantidad}
                                </span>
                            ";
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}")).FirstOrDefault().Texto = valor;
                    }
                }


                //asunto
                if (_plantillaCorreo.Asunto.Contains("{T_Proveedor.NombreContacto}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombreProveedor;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace("{T_Proveedor.NombreContacto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Proveedor.NombreContacto}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Asunto.Contains("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.UrlArchivo;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_MaterialPEspecificoDetalle.UrlMaterialVersionProveedor}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Asunto.Contains("{T_MaterialPEspecificoDetalle.DireccionEntrega}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.DireccionEntrega;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace("{T_MaterialPEspecificoDetalle.DireccionEntrega}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_MaterialPEspecificoDetalle.DireccionEntrega}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Asunto.Contains("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.FechaEntrega.ToString("dd/MM/yyyy");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_MaterialPEspecificoDetalle.FechaAproximadaRecepcion}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Asunto.Contains("{T_CentroCosto.Nombre}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombreCentroCosto;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace("{T_CentroCosto.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_CentroCosto.Nombre}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Asunto.Contains("{tPEspecifico.nombre}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.NombrePEspecifico;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace("{tPEspecifico.nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{tPEspecifico.nombre}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Asunto.Contains("{tPEspecifico.Grupo}"))
                {
                    var valor = detalleEnvioMaterialProveedorImpresion.Grupo.ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace("{tPEspecifico.Grupo}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{tPEspecifico.Grupo}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (_plantillaCorreo.Asunto.Contains("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}"))
                {
                    var valorCantidad = detalleEnvioMaterialProveedorImpresion.Cantidad;
                    var valorTipoMaterial = detalleEnvioMaterialProveedorImpresion.NombreMaterialTipo;

                    var valor = $@"
                               <span>
                                    Tipo: {valorTipoMaterial}
                                    <br>
                                    Cantidad: {valorCantidad}
                                </span>
                            ";
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_MaterialPEspecificoDetalle.DetalleMaterialesSolicitados}")).FirstOrDefault().Texto = valor;
                    }
                }
                if (this._idPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    emailReemplazado.Asunto = _plantillaCorreo.Asunto;
                    emailReemplazado.CuerpoHTML = _plantillaCorreo.Cuerpo;
                }
                else if (this._idPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;

                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                else if (this._idPlantillaBase == 12 || this._idPlantillaBase == 13)
                {
                    whatsAppReemplazado.Plantilla = _plantillaCorreo.Cuerpo;
                    whatsAppReemplazado.ListaEtiquetas = _listaObjetoWhatsApp;

                    foreach (var item in whatsAppReemplazado.ListaEtiquetas)
                    {
                        whatsAppReemplazado.Plantilla = whatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                return (emailReemplazado, whatsAppReemplazado);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos de proceso de seleccion
        /// </summary>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public PlantillaEmailMandrillDTO ReemplazarEtiquetasPostulanteCursoAsesorCapacitacion(int idPlantilla, int idPostulanteProcesoSeleccion)
        {
            try
            {
                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantilla)!;
                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                    throw new BadRequestException("Plantilla base no existente");

                _idPlantillaBase = plantilla.IdPlantillaBase;
                _plantillaCorreo = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(idPlantilla);

                var listaEtiqueta = _plantillaCorreo.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();


                var postulanteProcesoSeleccion = _unitOfWork.PostulanteRepository.ObtenerProcesoSeleccionInscrito(idPostulanteProcesoSeleccion);
                if (postulanteProcesoSeleccion == null)
                {
                    throw new BadRequestException("No se encontro el proceso seleccion del inscrito");
                }
                var postulante = _unitOfWork.PostulanteRepository.ObtenerPorId(postulanteProcesoSeleccion.IdPostulante);
                if (postulante == null)
                {
                    throw new BadRequestException("No se encontro el postulante");
                }

                var datosPostulanteCurso = _unitOfWork.PostulanteRepository.ObtenerDatosMatriculaIdPostulante(postulanteProcesoSeleccion.IdPostulante);

                if (datosPostulanteCurso == null)
                {
                    throw new BadRequestException("No se encontro los datos del postulante");
                }

                if (_plantillaCorreo.Cuerpo.Contains("{T_Postulante.Nombre1}"))
                {
                    var valor = postulante.Nombre;
                    _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{T_Postulante.Nombre1}", valor);
                }

                if (_plantillaCorreo.Cuerpo.Contains("{T_Postulante.Usuario}"))
                {
                    var valor = datosPostulanteCurso.Email;
                    _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{T_Postulante.Usuario}", valor);
                }

                if (_plantillaCorreo.Cuerpo.Contains("{T_Postulante.Clave}"))
                {
                    var valor = datosPostulanteCurso.Contraseña;
                    _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{T_Postulante.Clave}", valor);
                }

                //asunto
                if (_plantillaCorreo.Asunto.Contains("{T_Postulante.Nombre1}"))
                {
                    var valor = postulante.Nombre;
                    _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace("{T_Postulante.Nombre1}", valor);
                }

                PlantillaEmailMandrillDTO emailReemplazado = new();

                if (plantilla.IdPlantillaBase == PlantillaBase.Email)
                {
                    emailReemplazado.Asunto = _plantillaCorreo.Asunto;
                    emailReemplazado.CuerpoHTML = _plantillaCorreo.Cuerpo;
                }
                return emailReemplazado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Fecha: 24/06/2024
        /// Version: 1.0
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos de accesos temporales de Postulantes
        /// </summary>
        /// <param name="informacionAccesoTemporal"> Información de Acceso Temporal </param>
        /// <param name="idPespecifico">Id de Programa Específico</param>
        /// <param name="fechaInicio">Fecha Inicio de Acceso</param>
        /// <param name="fechaFin">Fecha Fin de Acceso</param>
        /// <param name="personalEmail">Emai lde Personal</param>
        /// <returns> Vacio, asigna a las propiedades locales los resultados </returns>
        public PlantillaEmailMandrillDTO ReemplazarEtiquetasAccesosTemporalesPostulante(int idPlantilla, InformacionAccesoPostulanteDTO informacionAccesoTemporal, int idPespecifico, DateTime fechaInicio, DateTime fechaFin, string personalEmail)
        {
            try
            {

                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantilla)!;
                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                    throw new BadRequestException("Plantilla base no existente");

                _idPlantillaBase = plantilla.IdPlantillaBase;
                _plantillaCorreo = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(idPlantilla);


                List<string> listaEtiqueta = _plantillaCorreo.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                /*Lista de etiquetas para la generacion de plantillas genericas*/
                if (informacionAccesoTemporal.IdAlumno.GetValueOrDefault() > 0)
                {
                    if (_plantillaCorreo.Asunto.Contains("{tAlumno.nombre1}"))
                    {
                        var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(informacionAccesoTemporal.IdAlumno.GetValueOrDefault())!;
                        var valorFormateado = alumno.Nombre1;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            _plantillaCorreo.Asunto = _plantillaCorreo.Asunto.Replace("{tAlumno.nombre1}", valorFormateado);
                        }
                    }
                    /*Curso Habilitado*/
                    if (_plantillaCorreo.Cuerpo.Contains("{tPespecifico.Nombre}"))
                    {
                        var valor = _unitOfWork.PEspecificoRepository.GetBy(x => x.Id == idPespecifico).Select(x => x.Nombre).FirstOrDefault();

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tPespecifico.Nombre}", valor);
                        }
                    }
                    /*Correo Usuario Acceso Portal*/
                    if (_plantillaCorreo.Cuerpo.Contains("{tAspNetUser.Usuario}"))
                    {
                        var valor = informacionAccesoTemporal.Usuario;

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tAspNetUser.Usuario}", valor);
                        }
                    }
                    /*Correo Clave Acceso Portal*/
                    if (_plantillaCorreo.Cuerpo.Contains("{tAspNetUser.Clave}"))
                    {
                        var valor = informacionAccesoTemporal.Clave;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tAspNetUser.Clave}", valor);
                        }
                    }
                    /*Fecha Inicio de Acceso*/
                    if (_plantillaCorreo.Cuerpo.Contains("{tPostulanteAccesoTemporalAulaVirtual.FechaInicio}"))
                    {
                        var valor = fechaInicio.ToString("MM/dd/yyyy");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tPostulanteAccesoTemporalAulaVirtual.FechaInicio}", valor);
                        }
                    }
                    /*Fecha Fin de Acceso*/
                    if (_plantillaCorreo.Cuerpo.Contains("{tPostulanteAccesoTemporalAulaVirtual.FechaFin}"))
                    {
                        var valor = fechaFin.ToString("MM/dd/yyyy");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tPostulanteAccesoTemporalAulaVirtual.FechaFin}", valor);
                        }
                    }
                    //Firma del personal
                    if (_plantillaCorreo.Cuerpo.Contains("{tPersonal.UrlFirmaCorreos}"))
                    {
                        IListadoEtiquetaService listadoEtiqueta = new ListadoEtiquetaService(_unitOfWork);

                        var firma = listadoEtiqueta.EtiquetaUrlFirmaCorreo(personalEmail);
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            _plantillaCorreo.Cuerpo = _plantillaCorreo.Cuerpo.Replace("{tPersonal.UrlFirmaCorreos}", firma);
                        }
                    }
                }
                PlantillaEmailMandrillDTO emailReemplazado = new();
                if (_idPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    emailReemplazado.Asunto = _plantillaCorreo.Asunto;
                    emailReemplazado.CuerpoHTML = _plantillaCorreo.Cuerpo;
                }
                return emailReemplazado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
