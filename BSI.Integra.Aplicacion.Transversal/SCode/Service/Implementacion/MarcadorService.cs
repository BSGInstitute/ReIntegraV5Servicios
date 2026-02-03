using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: MarcadorService
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 28/06/2023
    /// <summary>
    /// Gestión general de Marcador Automatico
    /// </summary>
    public class MarcadorService : IMarcadorService
    {
        private Mapper _mapper;
        private IUnitOfWork _unitOfWork;
        public MarcadorService(IUnitOfWork unitOfWork)
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TActividadMarcadorLog, ActividadMarcadorLogDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TActividadMarcadorLog, ActividadMarcadorLog>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ActividadMarcadorLogDTO, ActividadMarcadorLog>(MemberList.None).ReverseMap();
                });
            _mapper = new Mapper(config);
            _unitOfWork = unitOfWork;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los tabs con validacion para el marcador automatico
        /// </summary>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorDTO? ObtenerActividad(int idAsesor)
        {
            try
            {
                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(idAsesor);
                var tabsConValidacion = _unitOfWork.AgendaTabRepository.ObtenerTabsConfiguradosConValidacionMarcador("VE");

                var tabVencidasIPICPF = tabsConValidacion.Where(x => x.Numeracion == 1 && x.ValidarFecha).ToList();

                DateTime? ultimaFechaProgramadaSinIntento = null;
                DateTime? ultimaFechaProgramadaConIntento = null;
                List<ActividadAgendaMarcadorDTO>? resultadoIPICPF = null;
                List<ActividadAgendaMarcadorDTO>? resultadoProgramadas = null;
                List<ActividadAgendaMarcadorDTO>? resultadoNoProg = null;
                List<ActividadAgendaMarcadorDTO>? resultadoAutomaticas = null;
                ActividadAgendaMarcadorDTO? prioridadConIntentoIPICPF = null;
                ActividadAgendaMarcadorDTO? prioridadSinIntentoIPICPF = null;
                ActividadAgendaMarcadorDTO? prioridadConIntentoProgramadas = null;
                ActividadAgendaMarcadorDTO? prioridadSinIntentoProgramadas = null;
                ActividadAgendaMarcadorDTO? prioridadConIntentoNoProg = null;
                ActividadAgendaMarcadorDTO? prioridadSinIntentoNoProg = null;
                ActividadAgendaMarcadorDTO? prioridadConIntentoAutomaticas = null;
                ActividadAgendaMarcadorDTO? prioridadSinIntentoAutomaticas = null;
                if (tabVencidasIPICPF != null && tabVencidasIPICPF.Count() > 0)
                {
                    var tabIpsIcpF = tabVencidasIPICPF.Where(x => x.IdEstadoOportunidad != "4").FirstOrDefault();
                    //resultadoIPICPF = ObtenerActividadMarcadorPorTab(tabVencidasIPICPF, idAsesor);
                    if (tabIpsIcpF != null)
                    {
                        //prioridadConIntentoIPICPF = resultadoIPICPF.Where(x => x.TotalIntento > 0 || x.Contestado > 0 || x.NoContestado > 0).OrderByDescending(x => x.TotalIntento).ThenBy(x => x.FechaProgramadaMarcador).FirstOrDefault();
                        //prioridadSinIntentoIPICPF = resultadoIPICPF.Where(x => x.TotalIntento == 0).OrderBy(x => x.FechaProgramadaMarcador).FirstOrDefault();
                        prioridadConIntentoIPICPF = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesIpIcPf(tabIpsIcpF, idAsesor, true);
                        prioridadSinIntentoIPICPF = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesIpIcPf(tabIpsIcpF, idAsesor, false);

                        if (prioridadConIntentoIPICPF != null)
                        {
                            ultimaFechaProgramadaConIntento = prioridadConIntentoIPICPF.FechaProgramadaMarcador;
                        }
                        if (prioridadSinIntentoIPICPF != null)
                        {
                            ultimaFechaProgramadaSinIntento = prioridadSinIntentoIPICPF.FechaProgramadaMarcador;
                        }
                    }
                }
                var fechaActual = DateTime.Now;
                //MEXICO
                if (diferenciaHoraria != null)
                {
                    fechaActual = DateTime.Now.AddHours(diferenciaHoraria.Valor!.Value);
                }
                bool flagContinuar = false;
                if (ultimaFechaProgramadaConIntento == null || ultimaFechaProgramadaConIntento > fechaActual)
                {
                    if (ultimaFechaProgramadaSinIntento == null || ultimaFechaProgramadaSinIntento > fechaActual)
                    {
                        flagContinuar = true;
                    }
                    else
                    {
                        return prioridadSinIntentoIPICPF;
                    }
                }
                else
                {
                    return prioridadConIntentoIPICPF;
                }
                if (flagContinuar == true)
                {
                    flagContinuar = false;
                    ultimaFechaProgramadaConIntento = null;
                    ultimaFechaProgramadaSinIntento = null;
                    var tabProgramadaManual = tabsConValidacion.Where(x => x.Numeracion == 2 && x.ValidarFecha).FirstOrDefault();
                    if (tabProgramadaManual != null)
                    {
                        prioridadConIntentoProgramadas = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesProgramadasManualesMarcador(tabProgramadaManual, idAsesor, true);
                        prioridadSinIntentoProgramadas = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesProgramadasManualesMarcador(tabProgramadaManual, idAsesor, false);
                        if (prioridadConIntentoProgramadas != null)
                        {
                            ultimaFechaProgramadaConIntento = prioridadConIntentoProgramadas.FechaProgramadaMarcador;
                        }
                        if (prioridadSinIntentoProgramadas != null)
                        {
                            ultimaFechaProgramadaSinIntento = prioridadSinIntentoProgramadas.FechaProgramadaMarcador;
                        }
                    }
                    fechaActual = DateTime.Now;
                    //MEXICO
                    if (diferenciaHoraria != null)
                    {
                        fechaActual = DateTime.Now.AddHours(diferenciaHoraria.Valor!.Value);
                    }
                    if (ultimaFechaProgramadaConIntento == null || ultimaFechaProgramadaConIntento > fechaActual)
                    {
                        if (ultimaFechaProgramadaSinIntento == null || ultimaFechaProgramadaSinIntento > fechaActual)
                        {
                            flagContinuar = true;
                        }
                        else
                        {
                            return prioridadSinIntentoProgramadas;
                        }
                    }
                    else
                    {
                        return prioridadConIntentoProgramadas;
                    }

                    if (flagContinuar == true)
                    {
                        flagContinuar = false;
                        ultimaFechaProgramadaConIntento = null;
                        ultimaFechaProgramadaSinIntento = null;
                        var tabNoProg = tabsConValidacion.Where(x => x.Numeracion == 3 && !x.ValidarFecha).ToList();
                        if (tabNoProg != null && tabNoProg.Count() > 0)
                        {
                            resultadoNoProg = ObtenerActividadMarcadorPorTab(tabNoProg, idAsesor);
                            if (resultadoNoProg != null)
                            {
                                prioridadConIntentoNoProg = resultadoNoProg.Where(x => x.TotalIntento > 0 || x.Contestado > 0 || x.NoContestado > 0).OrderByDescending(x => x.TotalIntento).ThenBy(x => x.FechaProgramadaMarcador).FirstOrDefault();
                                prioridadSinIntentoNoProg = resultadoNoProg.Where(x => x.TotalIntento == 0).OrderBy(x => x.FechaProgramadaMarcador).FirstOrDefault();
                                if (prioridadConIntentoNoProg != null)
                                {
                                    ultimaFechaProgramadaConIntento = prioridadConIntentoNoProg.FechaProgramadaMarcador;
                                }
                            }
                        }
                        //MEXICO
                        if (diferenciaHoraria != null)
                        {
                            fechaActual = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                        }
                        if (ultimaFechaProgramadaConIntento == null || ultimaFechaProgramadaConIntento > fechaActual)
                        {
                            if (ultimaFechaProgramadaConIntento == null && prioridadConIntentoNoProg != null)
                            {
                                return prioridadConIntentoNoProg;
                            }
                            if (prioridadSinIntentoNoProg == null)
                            {
                                flagContinuar = true;
                            }
                            else
                            {
                                return prioridadSinIntentoNoProg;
                            }
                        }
                        else
                        {
                            return prioridadConIntentoNoProg;
                        }
                        if (flagContinuar)
                        {
                            flagContinuar = false;
                            ultimaFechaProgramadaConIntento = null;
                            ultimaFechaProgramadaSinIntento = null;
                            var tabProgramaAutomatica_Rn2 = tabsConValidacion.Where(x => x.Numeracion == 4).FirstOrDefault();

                            if (tabProgramaAutomatica_Rn2 != null)
                            {
                                prioridadConIntentoAutomaticas = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesAutomaticaMarcador(tabProgramaAutomatica_Rn2, idAsesor, true);
                                prioridadSinIntentoAutomaticas = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesAutomaticaMarcador(tabProgramaAutomatica_Rn2, idAsesor, false);
                                if (prioridadConIntentoAutomaticas != null)
                                {
                                    ultimaFechaProgramadaConIntento = prioridadConIntentoAutomaticas.FechaProgramadaMarcador;
                                }
                                if (prioridadSinIntentoAutomaticas != null)
                                {
                                    ultimaFechaProgramadaSinIntento = prioridadSinIntentoAutomaticas.FechaProgramadaMarcador;
                                }
                            }
                            //MEXICO
                            if (diferenciaHoraria != null)
                            {
                                fechaActual = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                            }
                            if (ultimaFechaProgramadaConIntento == null || ultimaFechaProgramadaConIntento > fechaActual)
                            {
                                var fechaFin = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 23, 59, 59);
                                if (ultimaFechaProgramadaSinIntento == null || ultimaFechaProgramadaSinIntento > fechaFin)
                                {
                                    flagContinuar = true;
                                }
                                else
                                {
                                    return prioridadSinIntentoAutomaticas;
                                }
                            }
                            else
                            {
                                return prioridadConIntentoAutomaticas;
                            }
                        }
                    }
                }
                if (prioridadConIntentoIPICPF != null)
                {
                    return prioridadConIntentoIPICPF;
                }
                if (prioridadConIntentoProgramadas != null)
                {
                    return prioridadConIntentoProgramadas;
                }
                if (prioridadConIntentoNoProg != null)
                {
                    return prioridadConIntentoNoProg;
                }
                if (prioridadConIntentoAutomaticas != null)
                {
                    return prioridadConIntentoAutomaticas;
                }
                if (prioridadSinIntentoAutomaticas != null)
                {
                    return prioridadSinIntentoAutomaticas;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque 
        /// Fecha: 12/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los tabs con validacion para el marcador automatico
        /// </summary>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorDTO? ObtenerActividadWavix(int idAsesor)
        {
            try
            {
                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(idAsesor);
                var tabsConValidacion = _unitOfWork.AgendaTabRepository.ObtenerTabsConfiguradosConValidacionMarcador("VE");

                //var tabsNoProgramadaUnaMasdeUna = tabsConValidacion.Where(x => x.Numeracion == 3 && !x.ValidarFecha).ToList();
                var tabVencidasIPICPF = tabsConValidacion.Where(x => x.Numeracion == 1 && x.ValidarFecha).ToList();

                ActividadAgendaMarcadorDTO? prioridadNoProgramadaUnaMasdeUna = null;
                ActividadAgendaMarcadorDTO? prioridadIPICPF = null;
                ActividadAgendaMarcadorDTO? prioridadProgramadas = null;
                ActividadAgendaMarcadorDTO? prioridadAutomaticas = null;
                ActividadAgendaMarcadorDTO? prioridadNoProg = null;
                ActividadAgendaMarcadorDTO? prioridadRN = null;
                var fechaActual = DateTime.Now;
                if (diferenciaHoraria != null)
                {
                    fechaActual = DateTime.Now.AddHours(diferenciaHoraria.Valor!.Value);
                }
                if (tabVencidasIPICPF != null && tabVencidasIPICPF.Count() > 0)
                {
                    var tabIpsIcpF = tabVencidasIPICPF.Where(x => x.IdEstadoOportunidad != "4").FirstOrDefault();
                    if (tabIpsIcpF != null)
                    {
                        prioridadIPICPF = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesIpIcPfWavix(tabIpsIcpF, idAsesor, false ,fechaActual);
                    }
                }
                if(prioridadIPICPF != null)
                {
                    return prioridadIPICPF;
                }

                var tabProgramadaManual = tabsConValidacion.Where(x => x.Numeracion == 2 && x.ValidarFecha).FirstOrDefault();
                if (tabProgramadaManual != null)
                {
                    prioridadProgramadas = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesProgramadasManualesMarcadorWavix(tabProgramadaManual, idAsesor, false,fechaActual);
                }

                if (prioridadProgramadas != null)
                {
                    return prioridadProgramadas;
                }

                var tabNoProg = tabsConValidacion.Where(x => x.Numeracion == 3 && !x.ValidarFecha).ToList();
                if (tabNoProg != null && tabNoProg.Count() > 0)
                {
                    var resultadoNoProg = ObtenerActividadMarcadorPorTab(tabNoProg, idAsesor);
                    if (resultadoNoProg != null)
                    {
                        prioridadNoProg = resultadoNoProg.OrderBy(x => x.UltimaFechaProgramada).FirstOrDefault();
                    }
                }

                if (prioridadNoProg != null)
                {
                    return prioridadNoProg;
                }
                
                var tabProgramaAutomatica = tabsConValidacion.Where(x => x.Numeracion == 4 && x.Id==14).FirstOrDefault();
                if (tabProgramaAutomatica != null)
                {
                    prioridadAutomaticas = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesAutomaticaMarcadorWavix(tabProgramaAutomatica, idAsesor, false,fechaActual);
                }

                if (prioridadAutomaticas != null)
                {
                    return prioridadAutomaticas;
                }
                var tabProgramaAutomatica_Rn2B = tabsConValidacion.Where(x => x.Numeracion == 4 && (x.Id == 6 || x.Id == 53)).FirstOrDefault();
                if (tabProgramaAutomatica_Rn2B != null)
                {
                    prioridadRN = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesRN2BMarcadorWavix(tabProgramaAutomatica_Rn2B, idAsesor, false, fechaActual);

                }

                if (prioridadRN != null)
                {
                    return prioridadRN;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque 
        /// Fecha: 12/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los tabs con validacion para el marcador automatico
        /// </summary>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadWavixATC(int idAsesor)
        {
            try
            {
                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(idAsesor);
                var tabsConValidacion = _unitOfWork.AgendaTabRepository.ObtenerTabsConfiguradosConValidacionMarcador("OP");

                //var tabsNoProgramadaUnaMasdeUna = tabsConValidacion.Where(x => x.Numeracion == 3 && !x.ValidarFecha).ToList();

                ActividadAgendaMarcadorATCDTO? prioridadNoProgramadaUnaMasdeUna = null;
                ActividadAgendaMarcadorATCDTO? prioridadAsignadoReasignado = null;
                ActividadAgendaMarcadorATCDTO? prioridadClasesOnline = null;
                ActividadAgendaMarcadorATCDTO? prioridadProgramadas = null;
                ActividadAgendaMarcadorATCDTO? prioridadCompromisos = null;
                ActividadAgendaMarcadorATCDTO? prioridadPagoAtrasadoMyP = null;
                ActividadAgendaMarcadorATCDTO? prioridadPagoAtrasado = null;
                ActividadAgendaMarcadorATCDTO? prioridadPagoDelDia = null;
                ActividadAgendaMarcadorATCDTO? prioridadPagoAlDia = null;
                ActividadAgendaMarcadorATCDTO? prioridadSeguimientoAcademico = null;
                ActividadAgendaMarcadorATCDTO? prioridadPorAbandonar = null;
                ActividadAgendaMarcadorATCDTO? prioridadPrereporte = null;
                ActividadAgendaMarcadorATCDTO? prioridadReporte = null;
                ActividadAgendaMarcadorATCDTO? prioridadSinContacto = null;
                ActividadAgendaMarcadorATCDTO? prioridadReservaSinDeuda = null;
                ActividadAgendaMarcadorATCDTO? prioridadCulminado = null;
                ActividadAgendaMarcadorATCDTO? prioridadPreReporteReporte = null;
                ActividadAgendaMarcadorATCDTO? prioridadAutomaticas = null;
                ActividadAgendaMarcadorATCDTO? prioridadAlDiaAtrasadoMAYP = null;
                ActividadAgendaMarcadorATCDTO? prioridadSegPorAbandonar = null;
                ActividadAgendaMarcadorATCDTO? prioridadBeneficiosPendientes = null;

                var fechaActual = DateTime.Now;
                if (diferenciaHoraria != null)
                {
                    fechaActual = DateTime.Now.AddHours(diferenciaHoraria.Valor!.Value);
                }
                /// <remarks>
                /// PRIORIDAD 1 - ASIGNADOS/REASIGNADOS:
                /// Pestañas relacionadas con asignaciones y reasignaciones:
                /// - Reasignado
                /// </remarks>
                var tabAsignadosReasignados = tabsConValidacion.Where(x => x.Numeracion == 1).FirstOrDefault();
                if (tabAsignadosReasignados != null)
                {
                        prioridadAsignadoReasignado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesPrioridad1ATC(tabAsignadosReasignados, idAsesor, false, fechaActual);
                }
                if (prioridadAsignadoReasignado != null)
                {
                    return prioridadAsignadoReasignado;
                }


                /// <remarks>
                /// PRIORIDAD 2 - Clases Online:
                /// Pestañas relacionadas con la tab de Clases Online:
                /// - Programación Manual
                /// </remarks>
                var tabClasesOnline = tabsConValidacion.Where(x => x.Numeracion == 2 && x.Id==52 && x.ValidarFecha).FirstOrDefault();
                if (tabClasesOnline != null)
                {
                    prioridadClasesOnline = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesClasesOnlineATC(tabClasesOnline, idAsesor, false, fechaActual);
                }

                if (prioridadClasesOnline != null)
                {
                    return prioridadClasesOnline;
                }

                /// <remarks>
                /// PRIORIDAD 3 - PROGRAMACIÓN MANUAL:
                /// Pestañas relacionadas con la programación manual:
                /// - Programación Manual
                /// </remarks>
                var Prioridad3 = tabsConValidacion.Where(x => x.Numeracion == 2 && x.Id == 18 && x.ValidarFecha).FirstOrDefault();
                if (Prioridad3 != null)
                {
                    prioridadProgramadas = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesPrioridad2ATC(Prioridad3, idAsesor, false, fechaActual);
                }

                if (prioridadProgramadas != null)
                {
                    return prioridadProgramadas;
                }

                /// <remarks>
                /// PRIORIDAD 4 - Compromisos de Pago:
                /// Pestañas relacionadas con la programación manual:
                /// - Programación Manual
                /// </remarks>
                var tabCompromisos = tabsConValidacion.Where(x => x.Id == 46 ).FirstOrDefault();
                if (tabCompromisos != null)
                {
                    prioridadCompromisos = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesCompromisosATC(tabCompromisos, idAsesor, false, fechaActual);
                }

                if (prioridadCompromisos != null)
                {
                    return prioridadCompromisos;
                }

                /// <remarks>
                /// PRIORIDAD 5 - Pago Atrasado Mes Actual - Precio:
                /// Pestañas relacionadas con la tab de :
                /// - Pago Atrasado Mes Actual y Prev
                /// </remarks>
                var tabPagoAtrasadoMyP = tabsConValidacion.Where(x => x.Id == 55 ).FirstOrDefault();
                if (tabPagoAtrasadoMyP != null)
                {
                    prioridadPagoAtrasadoMyP = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesPagoAtrasadoMyPATC(tabPagoAtrasadoMyP, idAsesor, false, fechaActual);
                }

                if (prioridadPagoAtrasadoMyP != null)
                {
                    return prioridadPagoAtrasadoMyP;
                }
                /// <remarks>
                /// PRIORIDAD 6 - Pago Atrasado:
                /// Pestañas relacionadas con la tab de :
                /// - Pago Atrasado
                /// </remarks>
                var tabPagoAtrasado = tabsConValidacion.Where(x => x.Id == 45 && x.ValidarFecha).FirstOrDefault();
                if (tabPagoAtrasado != null)
                {
                    prioridadPagoAtrasado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesPagoAtrasadoATC(tabPagoAtrasado, idAsesor, false, fechaActual);
                }

                if (prioridadPagoAtrasado != null)
                {
                    return prioridadPagoAtrasado;
                }

                /// <remarks>
                /// PRIORIDAD 7 - Pago del Dia:
                /// Pestañas relacionadas con la tab de :
                /// - Pago Atrasado
                /// </remarks>
                var tabPagoDelDia = tabsConValidacion.Where(x => x.Id == 54 ).FirstOrDefault();
                if (tabPagoDelDia != null)
                {
                    prioridadPagoDelDia = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesPagoDelDiaATC(tabPagoDelDia, idAsesor, false, fechaActual);
                }

                if (prioridadPagoDelDia != null)
                {
                    return prioridadPagoDelDia;
                }

                /// <remarks>
                /// PRIORIDAD 8 - Pago al Dia:
                /// Pestañas relacionadas con la tab de :
                /// - Pago Atrasado
                /// </remarks>
                var tabPagoAlDia = tabsConValidacion.Where(x => x.Id == 16 && x.ValidarFecha).FirstOrDefault();
                if (tabPagoAlDia != null)
                {
                    prioridadPagoAlDia = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesPagoAlDiaATC(tabPagoAlDia, idAsesor, false, fechaActual);
                }

                if (prioridadPagoAlDia != null)
                {
                    return prioridadPagoAlDia;
                }


                /// <remarks>
                /// PRIORIDAD 9 - Seguimiento Academico:
                /// Pestañas relacionadas con la tab de :
                /// - Seguimiento Academico
                /// </remarks>
                var tabSeguimientoAcademico = tabsConValidacion.Where(x => x.Id == 17 && x.ValidarFecha).FirstOrDefault();
                if (tabSeguimientoAcademico != null)
                {
                    prioridadSeguimientoAcademico = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesSeguimientoATC(tabSeguimientoAcademico, idAsesor, false, fechaActual);
                }

                if (prioridadSeguimientoAcademico != null)
                {
                    return prioridadSeguimientoAcademico;
                }

                /// <remarks>
                /// PRIORIDAD 10 - Por Abandonar:
                /// Pestañas relacionadas con la tab de :
                /// - SPOr Abandonar
                /// </remarks>
                var tabPorAbandonar = tabsConValidacion.Where(x => x.Id == 35 && x.ValidarFecha).FirstOrDefault();
                if (tabPorAbandonar != null)
                {
                    prioridadPorAbandonar = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesPorAbandonarATC(tabPorAbandonar, idAsesor, false, fechaActual);
                }

                if (prioridadPorAbandonar != null)
                {
                    return prioridadPorAbandonar;
                }

                /// <remarks>
                /// PRIORIDAD 10 -Pre Reportado:
                /// Pestañas relacionadas con la tab de :
                /// - Pre Reporte
                /// </remarks>
                var tabPreReporte = tabsConValidacion.Where(x => x.Id == 42 && x.ValidarFecha).FirstOrDefault();
                if (tabPreReporte != null)
                {
                    prioridadPrereporte = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesPreReporteATC(tabPreReporte, idAsesor, false, fechaActual);
                }

                if (prioridadPrereporte != null)
                {
                    return prioridadPrereporte;
                }
                /// <remarks>
                /// PRIORIDAD 10 - Reporte:
                /// Pestañas relacionadas con la tab de :
                /// - Reportado
                /// </remarks>
                var tabReporte = tabsConValidacion.Where(x => x.Id == 43 && x.ValidarFecha).FirstOrDefault();
                if (tabReporte != null)
                {
                    prioridadReporte = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesReporteATC(tabReporte, idAsesor, false, fechaActual);
                }

                if (prioridadReporte != null)
                {
                    return prioridadReporte;
                }

                /// <remarks>
                /// PRIORIDAD 11 - Sin COntacto:
                /// Pestañas relacionadas con la tab de :
                /// - Sin Contacto
                /// </remarks>
                //var tabSinContacto = tabsConValidacion.Where(x => x.Id == 51 ).FirstOrDefault();
                //if (tabSinContacto != null)
                //{
                //    prioridadSinContacto = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesSinContactoATC(tabSinContacto, idAsesor, false, fechaActual);
                //}

                //if (prioridadSinContacto != null)
                //{
                //    return prioridadSinContacto;
                //}
                /// <remarks>
                /// PRIORIDAD 12 - Reserva Con Deuda:
                /// Pestañas relacionadas con la tab de :
                /// - Reservado Sin Deuda
                /// </remarks>
                var tabReservaSinDeuda = tabsConValidacion.Where(x => x.Id == 25 && x.ValidarFecha).FirstOrDefault();
                if (tabReservaSinDeuda != null)
                {
                    prioridadReservaSinDeuda = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesReservaConDeudaATC(tabReservaSinDeuda, idAsesor, false, fechaActual);
                }

                if (prioridadReservaSinDeuda != null)
                {
                    return prioridadReservaSinDeuda;
                }
                /// <remarks>
                /// PRIORIDAD 13 - Culminado:
                /// Pestañas relacionadas con la tab de :
                /// - Culminado
                /// </remarks>
                var tabCulminado = tabsConValidacion.Where(x => x.Id == 22 ).FirstOrDefault();
                if (tabCulminado != null)
                {
                    prioridadCulminado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesCulminadoATC(tabCulminado, idAsesor, false, fechaActual);
                }

                if (prioridadCulminado != null)
                {
                    return prioridadCulminado;
                }
               
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades por tab para el marcador
        /// </summary>
        /// <param name="tabAgendas">Lista de objetos de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        private List<ActividadAgendaMarcadorDTO>? ObtenerActividadMarcadorPorTab(List<AgendaTabConfiguracionAlternoDTO> tabAgendas, int idAsesor)
        {
            try
            {
                List<ActividadAgendaMarcadorDTO> actividades = new();
                if (tabAgendas != null)
                {
                    foreach (var item in tabAgendas)
                    {
                        if (item.VisualizarActividad)
                        {
                            List<ActividadAgendaMarcadorDTO>? resultado;
                            if (item.CargarInformacionInicial)
                            {
                                if (item.VistaBaseDatos.Contains("ActividadProgramada"))
                                {
                                    resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesProgramadaMarcador(item, idAsesor);
                                }
                                else if (item.VistaBaseDatos.Contains("ActividadNoProgramada") && item.Probabilidad.Contains("Muy Alta"))
                                {
                                    resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesNoProgramadaMarcador(item, idAsesor);
                                }
                                else
                                {
                                    resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesMarcador(item, idAsesor);
                                }
                                if (resultado != null)
                                {
                                    resultado.ForEach(x =>
                                    {
                                        x.IdAgendaTabMarcador = item.Id;
                                    });
                                    actividades.AddRange(resultado);
                                }
                            }
                        }
                    }
                }
                if (actividades.Count() > 0)
                {
                    return actividades;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/07/2023
        /// Version: 1.0
        /// <summary>
        /// Guarda ActividadMarcadorLog
        /// </summary>
        /// <param name="dto">ActividadMarcadorLogDTO</param>
        /// <param name="usuario">Usuario registro</param>
        /// <returns></returns>
        public ActividadMarcadorLogDTO GuardarActividadMarcador(ActividadMarcadorLogDTO dto, string usuario)
        {
            try
            {
                ActividadMarcadorLogDTO actividadMarcadorLogDTO;
                var resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerPorIdActividadDetalleIdOportunidad(dto.IdActividadDetalle, dto.IdOportunidad);
                if (resultado != null)
                {
                    resultado.FechaProgramada = dto.FechaProgramada;
                    resultado.TotalIntento = dto.TotalIntento ?? 0;
                    resultado.Contestado = dto.Contestado ?? 0;
                    resultado.NoContestado = dto.NoContestado ?? 0;
                    resultado.IdAgendaTab = dto.IdAgendaTab ?? 0;
                    resultado.FechaModificacion = DateTime.Now;
                    resultado.UsuarioModificacion = usuario;
                    if (resultado.TotalIntento < resultado.NoContestado + resultado.Contestado)
                    {
                        resultado.TotalIntento = resultado.NoContestado + resultado.Contestado;
                    }
                    var rpta = _unitOfWork.ActividadMarcadorLogRepository.Update(resultado);
                    _unitOfWork.Commit();
                    actividadMarcadorLogDTO = _mapper.Map<ActividadMarcadorLogDTO>(rpta);
                }
                else
                {
                    ActividadMarcadorLog actividadMarcadorLog = new()
                    {
                        IdOportunidad = dto.IdOportunidad,
                        IdActividadDetalle = dto.IdActividadDetalle,
                        FechaProgramada = dto.FechaProgramada,
                        TotalIntento = dto.TotalIntento ?? 0,
                        Contestado = dto.Contestado ?? 0,
                        NoContestado = dto.NoContestado ?? 0,
                        IdAgendaTab = dto.IdAgendaTab ?? 0,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var rpta = _unitOfWork.ActividadMarcadorLogRepository.Add(actividadMarcadorLog);
                    _unitOfWork.Commit();
                    actividadMarcadorLogDTO = _mapper.Map<ActividadMarcadorLogDTO>(rpta);
                }
                return actividadMarcadorLogDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/07/2023
        /// Version: 1.0
        /// <summary>
        /// Guarda ActividadMarcadorLog
        /// </summary>
        /// <param name="dto">ActividadMarcadorLogDTO</param>
        /// <param name="usuario">Usuario registro</param>
        /// <returns></returns>
        public ActividadMarcadorLogDTO GuardarIntentoMarcador(ActividadMarcadorLogDTO dto, string usuario)
        {
            try
            {
                ActividadMarcadorLogDTO actividadMarcadorLogDTO;
                var resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerPorIdActividadDetalleIdOportunidad(dto.IdActividadDetalle, dto.IdOportunidad);
                if (resultado != null)
                {
                    resultado.TotalIntento = resultado.TotalIntento + 1;
                    if (resultado.IdAgendaTab == 0 || resultado.IdAgendaTab == null)
                    {
                        resultado.IdAgendaTab = dto.IdAgendaTab ?? 0;
                    }
                    if (dto.TotalIntento > resultado.TotalIntento)
                    {
                        resultado.TotalIntento = dto.TotalIntento;
                    }
                    if (resultado.TotalIntento == 0)
                    {
                        resultado.TotalIntento = 1;
                    }
                    resultado.FechaModificacion = DateTime.Now;
                    resultado.UsuarioModificacion = usuario;
                    if (resultado.TotalIntento < resultado.NoContestado + resultado.Contestado)
                    {
                        resultado.TotalIntento = resultado.NoContestado + resultado.Contestado;
                    }
                    var rpta = _unitOfWork.ActividadMarcadorLogRepository.Update(resultado);
                    _unitOfWork.Commit();
                    actividadMarcadorLogDTO = _mapper.Map<ActividadMarcadorLogDTO>(rpta);
                }
                else
                {
                    ActividadMarcadorLog actividadMarcadorLog = new()
                    {
                        IdOportunidad = dto.IdOportunidad,
                        IdActividadDetalle = dto.IdActividadDetalle,
                        FechaProgramada = null,
                        TotalIntento = dto.TotalIntento ?? 1,
                        Contestado = dto.Contestado ?? 0,
                        NoContestado = dto.NoContestado ?? 0,
                        IdAgendaTab = dto.IdAgendaTab ?? 0,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    if (actividadMarcadorLog.TotalIntento == 0)
                    {
                        actividadMarcadorLog.TotalIntento = 1;
                    }
                    var rpta = _unitOfWork.ActividadMarcadorLogRepository.Add(actividadMarcadorLog);
                    _unitOfWork.Commit();
                    actividadMarcadorLogDTO = _mapper.Map<ActividadMarcadorLogDTO>(rpta);
                }
                return actividadMarcadorLogDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/07/2023
        /// Version: 1.0
        /// <summary>
        /// Guarda ActividadMarcadorLog
        /// </summary>
        /// <param name="dto">ActividadMarcadorLogDTO</param>
        /// <param name="usuario">Usuario registro</param>
        /// <returns></returns>
        public ActividadMarcadorLogDTO GuardarNoContestadoMarcador(ActividadMarcadorLogDTO dto, string usuario)
        {
            try
            {
                ActividadMarcadorLogDTO actividadMarcadorLogDTO;
                var resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerPorIdActividadDetalleIdOportunidad(dto.IdActividadDetalle, dto.IdOportunidad);
                if (resultado != null)
                {
                    resultado.NoContestado = resultado.NoContestado + 1;
                    resultado.FechaModificacion = DateTime.Now;
                    resultado.UsuarioModificacion = usuario;
                    if (dto.NoContestado > resultado.NoContestado)
                    {
                        resultado.NoContestado = dto.NoContestado;
                    }
                    if (resultado.NoContestado == 0)
                    {
                        resultado.NoContestado = 1;
                    }
                    if (resultado.NoContestado == 1)
                    {
                        resultado.FechaProgramada = dto.FechaProgramada;
                    }
                    if (resultado.TotalIntento < resultado.NoContestado + resultado.Contestado)
                    {
                        resultado.TotalIntento = resultado.NoContestado + resultado.Contestado;
                    }
                    var rpta = _unitOfWork.ActividadMarcadorLogRepository.Update(resultado);
                    _unitOfWork.Commit();
                    actividadMarcadorLogDTO = _mapper.Map<ActividadMarcadorLogDTO>(rpta);
                }
                else
                {
                    ActividadMarcadorLog actividadMarcadorLog = new()
                    {
                        IdOportunidad = dto.IdOportunidad,
                        IdActividadDetalle = dto.IdActividadDetalle,
                        FechaProgramada = dto.FechaProgramada,
                        TotalIntento = dto.TotalIntento ?? 0,
                        Contestado = dto.Contestado ?? 0,
                        NoContestado = dto.NoContestado ?? 0,
                        IdAgendaTab = dto.IdAgendaTab ?? 0,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    if (actividadMarcadorLog.TotalIntento == 0)
                    {
                        actividadMarcadorLog.TotalIntento = 1;
                    }
                    if (actividadMarcadorLog.NoContestado == 0)
                    {
                        actividadMarcadorLog.NoContestado = 1;
                    }
                    if (actividadMarcadorLog.TotalIntento < actividadMarcadorLog.NoContestado + actividadMarcadorLog.Contestado)
                    {
                        actividadMarcadorLog.TotalIntento = actividadMarcadorLog.NoContestado + actividadMarcadorLog.Contestado;
                    }
                    var rpta = _unitOfWork.ActividadMarcadorLogRepository.Add(actividadMarcadorLog);
                    _unitOfWork.Commit();
                    actividadMarcadorLogDTO = _mapper.Map<ActividadMarcadorLogDTO>(rpta);
                }
                return actividadMarcadorLogDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/07/2023
        /// Version: 1.0
        /// <summary>
        /// Guarda ActividadMarcadorLog
        /// </summary>
        /// <param name="dto">ActividadMarcadorLogDTO</param>
        /// <param name="usuario">Usuario registro</param>
        /// <returns></returns>
        public ActividadMarcadorLogDTO GuardarContestadoMarcador(ActividadMarcadorLogDTO dto, string usuario)
        {
            try
            {
                ActividadMarcadorLogDTO actividadMarcadorLogDTO;
                var resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerPorIdActividadDetalleIdOportunidad(dto.IdActividadDetalle, dto.IdOportunidad);
                if (resultado != null)
                {
                    resultado.Contestado = resultado.Contestado + 1;
                    resultado.FechaModificacion = DateTime.Now;
                    resultado.UsuarioModificacion = usuario;
                    if (dto.Contestado > resultado.Contestado)
                    {
                        resultado.Contestado = dto.Contestado;
                    }
                    if (resultado.TotalIntento < resultado.NoContestado + resultado.Contestado)
                    {
                        resultado.TotalIntento = resultado.NoContestado + resultado.Contestado;
                    }
                    var rpta = _unitOfWork.ActividadMarcadorLogRepository.Update(resultado);
                    _unitOfWork.Commit();
                    actividadMarcadorLogDTO = _mapper.Map<ActividadMarcadorLogDTO>(rpta);
                }
                else
                {
                    ActividadMarcadorLog actividadMarcadorLog = new()
                    {
                        IdOportunidad = dto.IdOportunidad,
                        IdActividadDetalle = dto.IdActividadDetalle,
                        FechaProgramada = null,
                        TotalIntento = dto.TotalIntento ?? 0,
                        Contestado = dto.Contestado ?? 0,
                        NoContestado = dto.NoContestado ?? 0,
                        IdAgendaTab = dto.IdAgendaTab ?? 0,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    if (actividadMarcadorLog.TotalIntento == 0)
                    {
                        actividadMarcadorLog.TotalIntento = 1;
                    }
                    if (actividadMarcadorLog.Contestado == 0)
                    {
                        actividadMarcadorLog.Contestado = 1;
                    }
                    if (actividadMarcadorLog.TotalIntento < actividadMarcadorLog.NoContestado + actividadMarcadorLog.Contestado)
                    {
                        actividadMarcadorLog.TotalIntento = actividadMarcadorLog.NoContestado + actividadMarcadorLog.Contestado;
                    }
                    var rpta = _unitOfWork.ActividadMarcadorLogRepository.Add(actividadMarcadorLog);
                    _unitOfWork.Commit();
                    actividadMarcadorLogDTO = _mapper.Map<ActividadMarcadorLogDTO>(rpta);
                }
                return actividadMarcadorLogDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la actividad del marcador del dato
        /// </summary>
        /// <param name="idActividadDetalle">idActividadDetalle</param>
        /// <param name="idOportunidad">idOportunidad</param>
        /// <returns></returns>
        public ActividadMarcadorLogDTO ObtenerPorIdActividadDetalleIdOportunidad(int idActividadDetalle, int idOportunidad)
        {
            try
            {
                var resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerPorIdActividadDetalleIdOportunidad(idActividadDetalle, idOportunidad);
                return _mapper.Map<ActividadMarcadorLogDTO>(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Joseph Llanque 
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades por tab para el marcador
        /// </summary>
        /// <param name="tabAgendas">Lista de objetos de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        private List<ActividadAgendaMarcadorATCDTO>? ObtenerActividadMarcadorPrioridadLista(List<AgendaTabConfiguracionAlternoDTO> tabAgendas, int idAsesor)
        {
            try
            {
                List<ActividadAgendaMarcadorATCDTO> actividades = new();
                var fechaActual = DateTime.Now;
                if (tabAgendas != null)
                {
                    foreach (var item in tabAgendas)
                    {
                        if (item.VisualizarActividad)
                        {
                            List<ActividadAgendaMarcadorATCDTO>? resultado;
                            if (item.CargarInformacionInicial)
                            {
                                if (item.VistaBaseDatos.Contains("ActividadCuotaVenceHoy"))
                                {
                                    resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesCuotaVenceHoyATC(item, idAsesor, false, fechaActual);
                                }
                                else if (item.VistaBaseDatos.Contains("ActividadPagoAtrasadoMesActualPrevio"))
                                {
                                    resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesCuotaVenceHoyATC(item, idAsesor, false, fechaActual);
                                }
                                else {
                                    resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerActividadesMasdeUnaTabATC(item, idAsesor,false, fechaActual);

                                }
                               
                                if (resultado != null)
                                {
                                    resultado.ForEach(x =>
                                    {
                                        x.IdTabAgenda = item.Id;
                                    });
                                    actividades.AddRange(resultado);
                                }
                            }
                        }
                    }
                }
                if (actividades.Count() > 0)
                {
                    return actividades;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
