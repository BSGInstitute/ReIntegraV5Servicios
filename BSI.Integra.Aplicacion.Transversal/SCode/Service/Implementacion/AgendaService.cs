using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Text.RegularExpressions;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AgendaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de Informacion de Oportunidades
    /// </summary>
    public class AgendaService : IAgendaService
    {
        private IUnitOfWork _unitOfWork;
        //private AgendaDTO _agenda;
        private List<AgendaTabConfiguracionAlternoDTO> _tabsAgenda;
        private Dictionary<string, List<ActividadAgendaDTO>> _actividadesAgenda = new();
        private string _logCarlos = string.Empty;
        public AgendaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab, con los filtros realizados
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> Dictionary<string, List<ActividadAgendaDTO>> </returns>
        public (Dictionary<string, List<ActividadAgendaDTO>> ActividadesAgenda, int CantidadRN2) CargarActividadSeleccionadaPorFiltro(int idTab, string codigoAreaTrabajo, Dictionary<string, string>? filtros, int idAsesor)
        {
            try
            {
                var tabsActividad = _unitOfWork.AgendaTabRepository.ObtenerTabsConfiguradosPorIdTab(codigoAreaTrabajo, idTab).ToList();
                Dictionary<string, List<ActividadAgendaDTO>> actividadesAgenda = new();
                int cantidadRN2 = 0;
                if (tabsActividad != null && tabsActividad.Count() > 0)
                {
                    foreach (var item in tabsActividad)
                    {
                        List<ActividadAgendaDTO> actividades = new();

                        if (idTab == 12)
                        {
                            item.CamposVista = item.CamposVista.Replace("TOP 10", "");
                        }
                        if (item.VistaBaseDatos.Contains("ActividadRealizadaLlamada"))
                        {
                            continue;
                        }
                        else if (item.VistaBaseDatos.Contains("ActividadProgramada"))
                        {
                            actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesProgramada(item, idAsesor, filtros);
                        }
                        else if (item.VistaBaseDatos.Contains("ActividadNoProgramada") && item.Probabilidad.Contains("Muy Alta"))
                        {
                            actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesNoProgramada(item, idAsesor, filtros);
                        }
                        else if (item.Nombre.Contains("Solicitudes Agrupadas"))
                        {
                            int idAsesorFiltro = 0;
                            if (filtros != null && filtros.ContainsKey("idAsesor") && !string.IsNullOrEmpty(filtros["idAsesor"]))
                            {
                                int.TryParse(filtros["idAsesor"], out idAsesorFiltro);
                            }
                            actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesSolicitudesAgrupadas(item, idAsesorFiltro, filtros);
                        }
                        else if (item.Nombre.Contains("Atraso")
                                || item.Nombre.Contains("AlDia")
                                || item.Nombre.Contains("Seguimiento")
                                || item.Nombre.Contains("Manual")
                                || item.Nombre.Contains("Reasignado")
                                || item.Nombre == "Culminado"
                                || item.Nombre.Contains("Culminado Deudor")
                                || item.Nombre.Contains("Reservado Con Deuda")
                                || item.Nombre.Contains("Reservado Sin Deuda")
                                || item.Nombre.Contains("Retirado")
                                || item.Nombre.Contains("Abandonado")
                                || item.Nombre.Contains("Evaluacion")
                                || item.Nombre.Contains("Solicitud")
                                || item.Nombre.Contains("Certificado")
                                || item.Nombre.Contains("1+ Cuota Atraso")
                                || item.Nombre.Contains("PorAbandonar")
                                || item.Nombre.Contains("Por Contactar")
                                || item.Nombre.Contains("En Negociacion")
                                || item.Nombre.Contains("En Cierre De Negociacion")
                                || item.Nombre.Contains("Bic")
                                || item.Nombre.Contains("Acceso Temporal")
                                || item.Nombre.Contains("Pre Reporte CR")
                                || item.Nombre.Contains("Reportado CR")
                                || item.Nombre.Contains("Curso Pendiente")
                                || item.Nombre.Contains("Pagos Atrasados")
                                || item.Nombre.Contains("Compromisos De Pagos")
                                || item.Nombre.Contains("En Recuperacion De Curso")
                                || item.Nombre.Contains("Proyecto Aplicacion Pendiente")
                                || item.Nombre.Contains("Notas pendientes")
                                || item.Nombre.Contains("Beneficios Pendientes")
                                || item.Nombre.Contains("Clases Online")
                                || item.Nombre.Contains("Sin Contacto")
                                || item.Nombre.Contains("Pagos del dia")
                                || item.Nombre.Contains("Pago Atrasado(MesActual-Previo)")
                                || item.Nombre.Contains("Contestan y Cortan")
                                || item.Nombre.Contains("BICs con Deuda"))
                        {
                            actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesOperaciones(item, idAsesor, filtros);
                            cantidadRN2 = _unitOfWork.AgendaTabRepository.CantidadActividadesPorTabOperaciones(item, idAsesor, filtros);
                        }
                        else if (item.Nombre.Contains("Profesores"))
                        {
                            actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesOperaciones(item, idAsesor, filtros);
                            cantidadRN2 = _unitOfWork.AgendaTabRepository.CantidadActividadesPorTabOperaciones(item, idAsesor, filtros);
                        }
                        else
                        {
                            actividades = _unitOfWork.AgendaTabRepository.ObtenerActividades(item, idAsesor, filtros);
                            if (item.Nombre == "RN2-B" || item.Nombre == "RN2-A")
                                cantidadRN2 = _unitOfWork.AgendaTabRepository.CantidadActividadesPorTab(item, idAsesor, filtros);
                        }
                        bool esActividadNoProgramadaMuyAlta = item.VistaBaseDatos.Contains("ActividadNoProgramada") && item.Probabilidad.Contains("Muy Alta");
                        if (!esActividadNoProgramadaMuyAlta && item.Nombre != "Solicitud Cambio")
                        {
                            if (item.Nombre == "ProgramadasAutomatica")
                            {
                                var actividadesPorFecha = actividades.Where(x => x.ActividadesManhana == 0 && x.ActividadesTarde == 0).ToList();
                                actividadesPorFecha = actividadesPorFecha.OrderBy(x => x.UltimaFechaProgramada).ToList();

                                var actividadesManhana = actividades.Where(x => x.ActividadesManhana != 0 && x.ActividadesTarde == 0).ToList();
                                actividadesManhana = actividadesManhana.OrderBy(x => x.ActividadesManhana).ThenBy(x => x.UltimaFechaProgramada).ToList();

                                var actividadesTarde = actividades.Where(x => x.ActividadesTarde != 0).ToList();
                                actividadesTarde = actividadesTarde.OrderBy(x => x.ActividadesTarde).ThenBy(x => x.UltimaFechaProgramada).ToList();

                                var todo = actividadesPorFecha.Concat(actividadesManhana).ToList();
                                todo = todo.Concat(actividadesTarde).ToList();
                                actividades = todo;
                                ////actividades = actividades.OrderBy(x =>
                                ////    {
                                ////        if (x.ActividadesTarde.GetValueOrDefault() == 0)
                                ////            return x.ActividadesManhana;
                                ////        //else if (x.ActividadesManhana < x.ActividadesTarde)
                                ////        //    return x.ActividadesTarde;
                                ////        else
                                ////            return x.ActividadesTarde; // En caso de que sean iguales
                                ////    }).ThenBy(x => x.UltimaFechaProgramada).ToList();

                                //actividades = actividades.OrderBy(x => x.UltimaFechaProgramada).ToList();
                            }
                            else
                            {
                                actividades = actividades.OrderBy(x => x.UltimaFechaProgramada).ToList();
                            }
                        }
                        if (!actividadesAgenda.ContainsKey(item.Nombre))
                            actividadesAgenda.Add(item.Nombre, actividades);
                        else
                            actividadesAgenda[item.Nombre].AddRange(actividades);
                    }
                }
                return (actividadesAgenda, cantidadRN2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// Autor: Jose Vega
        ///// Fecha: 10/12/2025
        ///// Version: 1.0
        ///// <summary>
        ///// Obtiene las Actividades para el Tab, con los filtros realizados
        ///// </summary>
        ///// <param name="idTab">Id del Tab configurado</param>
        ///// <param name="codigoAreaTrabajo">Código del área de trabajo</param>
        ///// <param name="filtros">Diccionario con los filtros de búsqueda</param>
        ///// <param name="idAsesor">Id del Asesor asignado</param>
        ///// <returns>Tupla con el diccionario de actividades y la cantidad de RN2</returns>
        //public (Dictionary<string, List<ActividadAgendaV2DTO>> ActividadesAgenda, int CantidadRN2) CargarActividadSeleccionadaPorFiltroV2(int idTab, string codigoAreaTrabajo, Dictionary<string, string>? filtros, int idAsesor)
        //{
        //    try
        //    {
        //        var tabsActividad = _unitOfWork.AgendaTabRepository.ObtenerTabsConfiguradosPorIdTab(codigoAreaTrabajo, idTab).ToList();
        //        Dictionary<string, List<ActividadAgendaV2DTO>> actividadesAgenda = new();
        //        int cantidadRN2 = 0;
        //        if (tabsActividad != null && tabsActividad.Count() > 0)
        //        {
        //            foreach (var item in tabsActividad)
        //            {
        //                List<ActividadAgendaV2DTO> actividades = new();

        //                if (idTab == 12)
        //                {
        //                    item.CamposVista = item.CamposVista.Replace("TOP 10", "");
        //                }
        //                if (item.VistaBaseDatos.Contains("ActividadRealizadaLlamada"))
        //                {
        //                    continue;
        //                }
        //                else if (item.VistaBaseDatos.Contains("ActividadProgramada"))
        //                {
        //                    actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesProgramadaV2(item, idAsesor, filtros);
        //                }
        //                else if (item.VistaBaseDatos.Contains("ActividadNoProgramada") && item.Probabilidad.Contains("Muy Alta"))
        //                {
        //                    actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesNoProgramadaV2(item, idAsesor, filtros);
        //                }
        //                else if (item.Nombre.Contains("Atraso")
        //                        || item.Nombre.Contains("AlDia")
        //                        || item.Nombre.Contains("Seguimiento")
        //                        || item.Nombre.Contains("Manual")
        //                        || item.Nombre.Contains("Reasignado")
        //                        || item.Nombre == "Culminado"
        //                        || item.Nombre.Contains("Culminado Deudor")
        //                        || item.Nombre.Contains("Reservado Con Deuda")
        //                        || item.Nombre.Contains("Reservado Sin Deuda")
        //                        || item.Nombre.Contains("Retirado")
        //                        || item.Nombre.Contains("Abandonado")
        //                        || item.Nombre.Contains("Evaluacion")
        //                        || item.Nombre.Contains("Solicitud")
        //                        || item.Nombre.Contains("Certificado")
        //                        || item.Nombre.Contains("1+ Cuota Atraso")
        //                        || item.Nombre.Contains("PorAbandonar")
        //                        || item.Nombre.Contains("Por Contactar")
        //                        || item.Nombre.Contains("En Negociacion")
        //                        || item.Nombre.Contains("En Cierre De Negociacion")
        //                        || item.Nombre.Contains("Bic")
        //                        || item.Nombre.Contains("Acceso Temporal")
        //                        || item.Nombre.Contains("Pre Reporte CR")
        //                        || item.Nombre.Contains("Reportado CR")
        //                        || item.Nombre.Contains("Curso Pendiente")
        //                        || item.Nombre.Contains("Pagos Atrasados")
        //                        || item.Nombre.Contains("Compromisos De Pagos")
        //                        || item.Nombre.Contains("En Recuperacion De Curso")
        //                        || item.Nombre.Contains("Proyecto Aplicacion Pendiente")
        //                        || item.Nombre.Contains("Notas pendientes")
        //                        || item.Nombre.Contains("Beneficios Pendientes")
        //                        || item.Nombre.Contains("Clases Online")
        //                        || item.Nombre.Contains("Sin Contacto")
        //                        || item.Nombre.Contains("Pagos del dia")
        //                        || item.Nombre.Contains("Pago Atrasado(MesActual-Previo)")
        //                        || item.Nombre.Contains("Contestan y Cortan")
        //                        || item.Nombre.Contains("BICs con Deuda"))
        //                {
        //                    actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesOperacionesV2(item, idAsesor, filtros);
        //                    cantidadRN2 = _unitOfWork.AgendaTabRepository.CantidadActividadesPorTabOperaciones(item, idAsesor, filtros);
        //                }
        //                else if (item.Nombre.Contains("Profesores"))
        //                {
        //                    actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesOperacionesV2(item, idAsesor, filtros);
        //                    cantidadRN2 = _unitOfWork.AgendaTabRepository.CantidadActividadesPorTabOperaciones(item, idAsesor, filtros);
        //                }
        //                else
        //                {
        //                    actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesV2(item, idAsesor, filtros);
        //                    if (item.Nombre == "RN2-B" || item.Nombre == "RN2-A")
        //                        cantidadRN2 = _unitOfWork.AgendaTabRepository.CantidadActividadesPorTab(item, idAsesor, filtros);
        //                }
        //                bool esActividadNoProgramadaMuyAlta = item.VistaBaseDatos.Contains("ActividadNoProgramada") && item.Probabilidad.Contains("Muy Alta");
        //                if (!esActividadNoProgramadaMuyAlta && item.Nombre != "Solicitud Cambio")
        //                {
        //                    if (item.Nombre == "ProgramadasAutomatica")
        //                    {
        //                        var actividadesPorFecha = actividades.Where(x => x.ActividadesManhana == 0 && x.ActividadesTarde == 0).ToList();
        //                        actividadesPorFecha = actividadesPorFecha.OrderBy(x => x.UltimaFechaProgramada).ToList();

        //                        var actividadesManhana = actividades.Where(x => x.ActividadesManhana != 0 && x.ActividadesTarde == 0).ToList();
        //                        actividadesManhana = actividadesManhana.OrderBy(x => x.ActividadesManhana).ThenBy(x => x.UltimaFechaProgramada).ToList();

        //                        var actividadesTarde = actividades.Where(x => x.ActividadesTarde != 0).ToList();
        //                        actividadesTarde = actividadesTarde.OrderBy(x => x.ActividadesTarde).ThenBy(x => x.UltimaFechaProgramada).ToList();

        //                        var todo = actividadesPorFecha.Concat(actividadesManhana).ToList();
        //                        todo = todo.Concat(actividadesTarde).ToList();
        //                        actividades = todo;
        //                        ////actividades = actividades.OrderBy(x =>
        //                        ////    {
        //                        ////        if (x.ActividadesTarde.GetValueOrDefault() == 0)
        //                        ////            return x.ActividadesManhana;
        //                        ////        //else if (x.ActividadesManhana < x.ActividadesTarde)
        //                        ////        //    return x.ActividadesTarde;
        //                        ////        else
        //                        ////            return x.ActividadesTarde; // En caso de que sean iguales
        //                        ////    }).ThenBy(x => x.UltimaFechaProgramada).ToList();

        //                        //actividades = actividades.OrderBy(x => x.UltimaFechaProgramada).ToList();
        //                    }
        //                    else
        //                    {
        //                        actividades = actividades.OrderBy(x => x.UltimaFechaProgramada).ToList();
        //                    }
        //                }
        //                if (!actividadesAgenda.ContainsKey(item.Nombre))
        //                    actividadesAgenda.Add(item.Nombre, actividades);
        //                else
        //                    actividadesAgenda[item.Nombre].AddRange(actividades);
        //            }
        //        }
        //        return (actividadesAgenda, cantidadRN2);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Actividades para el Tab, con los filtros realizados
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> Dictionary<string, List<ActividadAgendaDTO>> </returns>
        public List<ActividadAgendaDTO> ObtenerActividadFichaChat(int idTab, string codigoAreaTrabajo, int idAsesor, int idMatriculaCabecera)
        {
            try
            {
                var tabsActividad = _unitOfWork.AgendaTabRepository.ObtenerTabsConfiguradosPorIdTab(codigoAreaTrabajo, idTab).ToList();
                List<ActividadAgendaDTO> actividadesAgenda = new();
                if (tabsActividad != null && tabsActividad.Count() > 0)
                {
                    foreach (var item in tabsActividad)
                    {
                        if (item.Nombre.Contains("Atraso")
                                 || item.Nombre.Contains("AlDia")
                                 || item.Nombre.Contains("Seguimiento")
                                 || item.Nombre.Contains("Manual")
                                 || item.Nombre.Contains("Reasignado")
                                 || item.Nombre == "Culminado"
                                 || item.Nombre.Contains("Culminado Deudor")
                                 || item.Nombre.Contains("Reservado Con Deuda")
                                 || item.Nombre.Contains("Reservado Sin Deuda")
                                 || item.Nombre.Contains("Retirado")
                                 || item.Nombre.Contains("Abandonado")
                                 || item.Nombre.Contains("Evaluacion")
                                 || item.Nombre.Contains("Solicitud")
                                 || item.Nombre.Contains("Certificado")
                                 || item.Nombre.Contains("1+ Cuota Atraso")
                                 || item.Nombre.Contains("PorAbandonar")
                                 || item.Nombre.Contains("Por Contactar")
                                 || item.Nombre.Contains("En Negociacion")
                                 || item.Nombre.Contains("En Cierre De Negociacion")
                                 || item.Nombre.Contains("Bic")
                                 || item.Nombre.Contains("Acceso Temporal")
                                 || item.Nombre.Contains("Pre Reporte CR")
                                 || item.Nombre.Contains("Reportado CR")
                                 || item.Nombre.Contains("Curso Pendiente")
                                 || item.Nombre.Contains("Pagos Atrasados")
                                 || item.Nombre.Contains("Compromisos De Pagos")
                                 || item.Nombre.Contains("En Recuperacion De Curso")
                                 || item.Nombre.Contains("Proyecto Aplicacion Pendiente")
                                 || item.Nombre.Contains("Notas pendientes")
                                 || item.Nombre.Contains("Beneficios Pendientes")
                                 || item.Nombre.Contains("Clases Online")
                                 || item.Nombre.Contains("Sin Contacto")
                                 || item.Nombre.Contains("Pagos del dia")
                                 || item.Nombre.Contains("Pago Atrasado(MesActual-Previo)")
                                 || item.Nombre.Contains("Contestan y Cortan")
                                 || item.Nombre.Contains("BICs con Deuda"))
                        {
                            actividadesAgenda = _unitOfWork.AgendaTabRepository.ObtenerActividadesOperacionesFichaChat(item, idAsesor, idMatriculaCabecera);
                        }
                    }
                }
                return (actividadesAgenda);
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// Autor: Gilmer Quispe.
        /// Fecha: 01/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la lista de Actividades por cada Tab del Asesor
        /// <param name="idAsesor">Id del asesor</param>
        /// /// <param name="codigoAreaTrabajo">Area Trabajo del personal</param>
        /// </summary>
        /// <returns> Dictionary de actividades agendad por tabs </returns>
        public Dictionary<string, List<ActividadAgendaDTO>> ObtenerActividades(int idAsesor, string codigoAreaTrabajo)
        {
            try
            {
                List<AgendaTabConfiguracionAlternoDTO> tabsAgenda = _unitOfWork.AgendaTabRepository.ObtenerTabsConfigurados(codigoAreaTrabajo).ToList();
                Dictionary<string, List<ActividadAgendaDTO>> actividadesAgenda = new();
                foreach (var item in tabsAgenda)
                {
                    if (item.VisualizarActividad)
                    {
                        List<ActividadAgendaDTO> actividades = new();
                        if (item.CargarInformacionInicial)
                        {
                            if (item.VistaBaseDatos.Contains("ActividadRealizadaLlamada"))
                            {
                                //CargarActividadesRealizadas(item);
                            }
                            else if (item.VistaBaseDatos.Contains("ActividadProgramada"))
                            {
                                actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesProgramada(item, idAsesor, null);
                            }
                            else if (item.VistaBaseDatos.Contains("ActividadNoProgramada") && item.Probabilidad.Contains("Muy Alta"))
                            {
                                actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesNoProgramada(item, idAsesor, null);
                            }
                            else
                            {
                                actividades = _unitOfWork.AgendaTabRepository.ObtenerActividades(item, idAsesor, null);
                            }
                            if (!actividadesAgenda.ContainsKey(item.Nombre))
                                actividadesAgenda.Add(item.Nombre, actividades);
                            else
                                actividadesAgenda[item.Nombre].AddRange(actividades);
                        }
                    }
                }
                actividadesAgenda = actividadesAgenda.ToDictionary(x => x.Key, x => !x.Key.Contains("No Prog") ? x.Value.OrderBy(z => z.UltimaFechaProgramada).ToList() : x.Value);
                return actividadesAgenda;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/08/2022
        /// Autor Modificacion: Flavio Rodrigo M.F.
        /// Fecha Modificacion: 16/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las tabs de actividades realizadas en la agenda 
        /// </summary>
        /// <param name="idAsesor">Id del asesor</param>
        /// <param name="codigoAreaTrabajo">Codigo-Área de trabajo</param>
        /// <param name="validarTabs">True or False</param>
        /// <param name="diferenciaHoraria"></param>
        /// <returns> AgendaDTO2 </returns>
        public (Dictionary<string, List<ActividadAgendaDTO>> ActividadesAgenda, Dictionary<string, bool> EstadosTabs, string LogCarlos) ObtenerActividadesAgenda(int idAsesor, bool validarTabs, string codigoAreaTrabajo, bool flagAgendaWhatsapp)
        {
            var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(idAsesor);
            var horas = diferenciaHoraria != null ? diferenciaHoraria.Valor : 0;
            // ObtenerTabAgenda();
            Dictionary<string, string>? filtros = null;
            if (flagAgendaWhatsapp == true)
            {
                filtros = new()
                {
                    { "EsWhatsApp", "SI" }
                };
            }
            if (!validarTabs)
            {
                //ObtenerTabsSinValidacion;
                var tabsAgendaNoValidados = _unitOfWork.AgendaTabRepository.ObtenerTabsConfiguradosSinValidacion(codigoAreaTrabajo);
                ObtenerActividadesPorTab(tabsAgendaNoValidados, idAsesor, filtros);
            }
            var habilitarEstados = ObtenerTabsConValidacion(codigoAreaTrabajo, idAsesor, filtros, horas);
            return (_actividadesAgenda, habilitarEstados, _logCarlos);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/09/2022
        /// Autor Modificacion: Flavio Rodrigo M.F.
        /// Fecha Modificacion: 16/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los tabs con validacion
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, bool> ObtenerTabsConValidacion(string codigoAreaTrabajo, int idAsesor, Dictionary<string, string>? filtros, int? diferenciaHoraria)
        {
            var tabsConValidacion = _unitOfWork.AgendaTabRepository.ObtenerTabsConfiguradosConValidacion(codigoAreaTrabajo);
            //LlenarEstadosHabilitar
            Dictionary<string, bool> habilitarEstados = new();
            habilitarEstados = tabsConValidacion.Select(x => x.Nombre).Distinct().Select(x => new KeyValuePair<string, bool>(x, false)).ToDictionary(x => x.Key, x => x.Value);

            var tabVencidasIPICPF = tabsConValidacion.Where(x => x.Numeracion == 1 && x.ValidarFecha).ToList();
            DateTime? primeraFechaVencidasIPICPF = null;
            if (tabVencidasIPICPF != null && tabVencidasIPICPF.Count() > 0)
            {
                ObtenerActividadesPorTab(tabVencidasIPICPF, idAsesor, filtros);
                var listaActividadesIPICPF = _actividadesAgenda.Where(x => x.Key == tabVencidasIPICPF.FirstOrDefault()!.Nombre).FirstOrDefault();
                primeraFechaVencidasIPICPF = listaActividadesIPICPF.Value.Select(x => x.UltimaFechaProgramada).FirstOrDefault();
                tabVencidasIPICPF.Select(x => x.Nombre).Distinct().ToList().ForEach(x => habilitarEstados[x] = true);
            }
            var fechaIP = DateTime.Now;
            //MEXICO
            if (diferenciaHoraria != null)
            {
                fechaIP = DateTime.Now.AddHours(diferenciaHoraria.Value);
            }
            //Si primeraFechaVencidasIPICPF es mayor a la fecha actual no hay vencidas
            if ((primeraFechaVencidasIPICPF == null || primeraFechaVencidasIPICPF > fechaIP) && codigoAreaTrabajo != "OP")
            {
                var tabProgramadaManual = tabsConValidacion.Where(x => x.Numeracion == 2 && x.ValidarFecha).ToList();
                DateTime? primeraFechaVencidasProgramadaManual = null;
                //cargamos los datos del tab de programadas manual
                //preguntamos si tenemos vencidas
                if (tabProgramadaManual != null && tabProgramadaManual.Count() > 0)
                {
                    ObtenerActividadesPorTab(tabProgramadaManual, idAsesor, filtros);
                    var listaActividadesProgramadaManual = _actividadesAgenda.Where(x => x.Key == tabProgramadaManual.FirstOrDefault()!.Nombre).FirstOrDefault();
                    primeraFechaVencidasProgramadaManual = listaActividadesProgramadaManual.Value.Select(x => x.UltimaFechaProgramada).FirstOrDefault();
                    tabProgramadaManual.Select(x => x.Nombre).Distinct().ToList().ForEach(x => habilitarEstados[x] = true);
                }

                var fecha = DateTime.Now;
                if (diferenciaHoraria != null)
                {
                    fecha = DateTime.Now.AddHours(diferenciaHoraria.Value);
                }
                if (primeraFechaVencidasProgramadaManual == null || primeraFechaVencidasProgramadaManual > fecha)
                {
                    //Si no hay programadas manual vencidas cargamos no programadas (1sol, +1sol)
                    //cantidad registros de NoProg1Sol, NoProg+1Sol
                    var tabVencidasNoProg1SolMas1Sol = tabsConValidacion.Where(x => x.Numeracion == 3 && !x.ValidarFecha).ToList();
                    if (tabVencidasNoProg1SolMas1Sol != null && tabVencidasNoProg1SolMas1Sol.Count() > 0)
                    {
                        ObtenerActividadesPorTab(tabVencidasNoProg1SolMas1Sol, idAsesor, filtros);
                        var actividadesNoProg1solMas1Sol = _actividadesAgenda.Where(x => tabVencidasNoProg1SolMas1Sol.DistinctBy(w => w.Nombre).Select(w => w.Nombre).Contains(x.Key)).ToList().Select(x => x.Value).ToList();

                        int cantidadOportunidadesNoProgramadas2 = 0;
                        foreach (var item in actividadesNoProg1solMas1Sol)
                        {
                            cantidadOportunidadesNoProgramadas2 += item.Count;
                        }
                        if (cantidadOportunidadesNoProgramadas2 == 0)
                        {
                            var tabProgramaAutomatica_Rn2 = tabsConValidacion.Where(x => x.Numeracion == 4).ToList();
                            ObtenerActividadesPorTab(tabProgramaAutomatica_Rn2, idAsesor, filtros);
                            tabProgramaAutomatica_Rn2.Select(x => x.Nombre).Distinct().ToList().ForEach(x => habilitarEstados[x] = true);

                        }
                        _logCarlos += $" cantidad registros de NoProg1Sol, NoProg+1Sol {tabVencidasNoProg1SolMas1Sol.Count()}";
                        tabVencidasNoProg1SolMas1Sol.Select(x => x.Nombre).Distinct().ToList().ForEach(x => habilitarEstados[x] = true);
                    }
                    else
                    {
                        _logCarlos += $" cantidad registros de NoProg1Sol, NoProg+1Sol 0";
                    }
                }
            }
            return habilitarEstados;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades por tab
        /// </summary>
        /// <param name="tabAgendas">Lista de objetos de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <returns></returns>
        private void ObtenerActividadesPorTab(List<AgendaTabConfiguracionAlternoDTO> tabAgendas, int idAsesor, Dictionary<string, string>? filtros)
        {
            Dictionary<string, List<ActividadAgendaDTO>> actividadesAux = new();
            if (tabAgendas != null)
            {
                foreach (var item in tabAgendas)
                {
                    if (item.VisualizarActividad)
                    {
                        List<ActividadAgendaDTO> actividades = new();
                        if (item.CargarInformacionInicial)
                        {
                            if (item.VistaBaseDatos.Contains("ActividadRealizadaLlamada"))
                            {
                                //this.CargarActividadesRealizadas(item);
                            }
                            else if (item.VistaBaseDatos.Contains("ActividadProgramada"))
                            {
                                actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesProgramada(item, idAsesor, filtros);
                            }
                            else if (item.VistaBaseDatos.Contains("ActividadNoProgramada") && item.Probabilidad.Contains("Muy Alta"))
                            {
                                actividades = _unitOfWork.AgendaTabRepository.ObtenerActividadesNoProgramada(item, idAsesor, filtros);
                                _logCarlos += $" / idEstadoOportunidad {item.IdEstadoOportunidad} {item.Nombre} {item.Probabilidad} {item.Id} {idAsesor}";
                                if (filtros != null)
                                {
                                    _logCarlos += $" {filtros.Count()}";
                                }
                                _logCarlos += $": Total de ObtenerActividadesNoProgramada: {actividades.Count()}";
                            }
                            else
                            {
                                actividades = _unitOfWork.AgendaTabRepository.ObtenerActividades(item, idAsesor, filtros);
                            }
                        }
                        if (!actividadesAux.ContainsKey(item.Nombre))
                        {
                            actividadesAux.Add(item.Nombre, actividades);
                        }
                        else
                        {
                            actividadesAux[item.Nombre].AddRange(actividades);
                        }
                    }
                }
            }
            foreach (var item in actividadesAux)
            {
                if (!item.Key.Contains("No Prog"))
                {
                    _actividadesAgenda.Add(item.Key, item.Value.OrderBy(x => x.UltimaFechaProgramada).ToList());
                }
                else
                {
                    _actividadesAgenda.Add(item.Key, item.Value);
                }
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la actividad filtrada por el asesor y seguimiento academico
        /// </summary>
        /// <param name="ObjetoDTO">Objeto de clase ObjetoDTO</param>
        /// <returns> ObtenerRealizadasRespuestaDTO </returns>
        public ObtenerRealizadasRespuestaDTO ObtenerRealizadas(CompuestoAgendaFiltroDTO ObjetoDTO)
        {
            var servicioOportunidadLog = new OportunidadLogService(_unitOfWork);

            if (ObjetoDTO.IdCentroCosto == null || ObjetoDTO.IdCentroCosto == "") ObjetoDTO.IdCentroCosto = "0";
            if (ObjetoDTO.IdAlumno == null || ObjetoDTO.IdAlumno == "") ObjetoDTO.IdAlumno = "0";
            if (ObjetoDTO.IdEstado == null || ObjetoDTO.IdEstado == "") ObjetoDTO.IdEstado = "0";
            if (ObjetoDTO.IdFaseOportunidad == null || ObjetoDTO.IdFaseOportunidad == "") ObjetoDTO.IdFaseOportunidad = "0";
            if (ObjetoDTO.IdTipoDato == null || ObjetoDTO.IdTipoDato == "") ObjetoDTO.IdTipoDato = "0";
            if (ObjetoDTO.IdOrigen == null || ObjetoDTO.IdOrigen == "") ObjetoDTO.IdOrigen = "0";
            if (ObjetoDTO.Fecha == null || ObjetoDTO.Fecha == "") ObjetoDTO.Fecha = "00000000";
            if (ObjetoDTO.IdProbabilidadActual == null || ObjetoDTO.IdProbabilidadActual == "") ObjetoDTO.IdProbabilidadActual = "0";
            if (ObjetoDTO.categoria == null || ObjetoDTO.categoria == "") ObjetoDTO.categoria = "_";

            int diaFecha = Convert.ToInt32(ObjetoDTO.Fecha.Substring(6, 2));
            var idsAsesor = ObjetoDTO.IdsAsesores;
            var fecha = ObjetoDTO.Fecha;
            var idCentroCosto = Convert.ToInt32(ObjetoDTO.IdCentroCosto);
            var idAlumno = Convert.ToInt32(ObjetoDTO.IdAlumno);
            var idFaseOportunidad = Convert.ToInt32(ObjetoDTO.IdFaseOportunidad);
            var idTipoDato = Convert.ToInt32(ObjetoDTO.IdTipoDato);
            var idOrigen = Convert.ToInt32(ObjetoDTO.IdOrigen);
            var take = Convert.ToInt32(ObjetoDTO.pageSize);
            var skip = Convert.ToInt32(ObjetoDTO.skip);
            var idsCategoriaOrigen = ObjetoDTO.categoria;
            var idProbabilidad = Convert.ToInt32(ObjetoDTO.IdProbabilidadActual);
            var idEstado = Convert.ToInt32(ObjetoDTO.IdEstado);

            var temp = _unitOfWork.AgendaTabRepository.ObtenerActividadesRealizadasSP(idsAsesor, fecha, idCentroCosto, idAlumno, idFaseOportunidad, idTipoDato, idOrigen, take, skip, idsCategoriaOrigen, idProbabilidad, idEstado);
            var result = (from p in temp
                          group p by new
                          {
                              p.Id,
                              p.NombreCentroCosto,
                              p.Contacto,
                              p.CodigoFase,
                              p.NombreTipoDato,
                              p.Origen,
                              p.FechaProgramada,
                              p.FechaReal,
                              p.Duracion,
                              p.Actividad,
                              p.Ocurrencia,
                              p.Comentario,
                              p.Asesor,
                              p.IdContacto,
                              p.IdOportunidad,
                              p.ProbActual,
                              p.NombreCategoria,
                              p.IdCategoria,
                              p.FaseInicial,
                              p.FaseMaxima,
                              p.TotalOportunidades,
                              p.UnicoTimbrado,
                              p.UnicoContesto,
                              p.UnicoEstadoLlamada,
                              p.NumeroLlamadas,
                              p.EstadoOcurrencia,
                              p.UnicoClasificacion,
                              p.UnicoFechaLlamada,
                              p.NombreGrupo,
                              p.IdFaseOportunidadInicial,
                              p.FechaModificacion

                          } into g
                          select new CompuestoActividadesEjecutadasTempDTO
                          {
                              Id = g.Key.Id,
                              CentroCosto = g.Key.NombreCentroCosto,
                              Contacto = g.Key.Contacto,
                              CodigoFase = g.Key.CodigoFase,
                              NombreTipoDato = g.Key.NombreTipoDato,
                              Origen = g.Key.Origen,
                              FechaProgramada = g.Key.FechaProgramada,
                              FechaReal = g.Key.FechaReal,
                              Duracion = g.Key.Duracion,
                              Actividad = g.Key.Actividad,
                              Ocurrencia = g.Key.Ocurrencia,
                              Comentario = g.Key.Comentario,
                              Asesor = g.Key.Asesor,
                              IdContacto = g.Key.IdContacto,
                              IdOportunidad = g.Key.IdOportunidad,
                              ProbActual = g.Key.ProbActual,
                              Ca_nombre = g.Key.NombreCategoria,
                              IdCategoria = g.Key.IdCategoria,
                              FaseInicial = g.Key.FaseInicial,
                              FaseMaxima = g.Key.FaseMaxima,
                              TotalOportunidades = g.Key.TotalOportunidades,
                              UnicoTimbrado = g.Key.UnicoTimbrado.ToString(),
                              UnicoContesto = g.Key.UnicoContesto.ToString(),
                              UnicoEstadoLlamada = g.Key.UnicoEstadoLlamada,
                              UnicoClasificacion = g.Key.UnicoClasificacion,
                              UnicoFechaLlamada = g.Key.UnicoFechaLlamada,
                              NumeroLlamadas = g.Key.NumeroLlamadas,
                              Estado = g.Key.EstadoOcurrencia,
                              NombreGrupo = g.Key.NombreGrupo,
                              IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                              FechaModificacion = g.Key.FechaModificacion,

                              lista = g.Select(o => new CompuestoActividadesEjecutadasTemp_DetalleDTO
                              {
                                  Id = o.IdTcentralLLamada,
                                  DuracionTimbrado = o.DuracionTimbrado,
                                  DuracionContesto = o.DuracionContesto,
                                  EstadoLlamada = o.EstadoLlamada,
                                  FechaLlamada = o.FechaLlamada,
                                  FechaLlamadaFin = o.FechaLlamadaFin,
                                  SubEstadoLlamada = o.SubEstadoLlamada,
                                  NombreGrabacion = o.NombreGrabacionIntegra,

                              }).OrderByDescending(o => o.FechaLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                              llamadasTresCX = g.Select(o => new CompuestoActividadesEjecutadasTemp_DetalleDTO
                              {
                                  Id = o.IdTresCX,
                                  DuracionContesto = o.TiempoContestoTresCx.ToString(),
                                  DuracionTimbrado = o.TiempoTimbradoTresCx.ToString(),
                                  EstadoLlamada = o.EstadoLlamadaTresCX,
                                  FechaLlamada = o.FechaIncioLlamadaTresCX,
                                  FechaLlamadaFin = o.FechaFinLlamadaTresCX,
                                  SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                  NombreGrabacion = o.NombreGrabacionTresCX,

                              }).OrderBy(o => o.FechaLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()
                          }).OrderBy(x => x.FechaReal);

            List<CompuestoActividadesEjecutadasTempDTO> final = new List<CompuestoActividadesEjecutadasTempDTO>();
            var flag = false;
            var count = 0;
            double minutos = 0;
            double totalContesto = 0;
            double totalTimbrado = 0;
            double totalPerdido = 0;
            double mayorPerdido = 0;
            DateTime fechaTemp = new DateTime();
            DateTime fechaActual = DateTime.Now;
            foreach (var item in result)
            {
                CompuestoActividadesEjecutadasTempDTO itemDetalle = new CompuestoActividadesEjecutadasTempDTO()
                {
                    Id = item.Id,
                    CentroCosto = item.CentroCosto,
                    Contacto = item.Contacto,
                    CodigoFase = item.CodigoFase,
                    NombreTipoDato = item.NombreTipoDato,
                    Origen = item.Origen,
                    FechaProgramada = item.FechaProgramada,
                    FechaReal = item.FechaReal,
                    Duracion = item.Duracion,
                    Actividad = item.Actividad,
                    Ocurrencia = item.Ocurrencia,
                    Comentario = item.Comentario,
                    Asesor = item.Asesor,
                    IdContacto = item.IdContacto,
                    IdOportunidad = item.IdOportunidad,
                    ProbActual = item.ProbActual,
                    Ca_nombre = item.Ca_nombre,
                    IdCategoria = item.IdCategoria,
                    FaseInicial = item.FaseInicial,
                    FaseMaxima = item.FaseMaxima,
                    TotalOportunidades = item.TotalOportunidades,
                    UnicoTimbrado = item.UnicoTimbrado,
                    UnicoContesto = item.UnicoContesto,
                    UnicoEstadoLlamada = item.UnicoEstadoLlamada,
                    Estado = item.Estado,
                    NombreGrupo = item.NombreGrupo
                };

                if (item.lista != null && item.lista.Select(s => s.DuracionTimbrado).FirstOrDefault() != null)
                {
                    var ordenLlamadas = item.lista.OrderBy(x => x.FechaLlamada).ToList();
                    var fechaUltima = ordenLlamadas.Select(s => s.FechaLlamada).FirstOrDefault();
                    if (count > 0 && flag)
                    {
                        if (diaFecha == fechaTemp.Day)
                        {
                            var min = ((fechaUltima.Value - fechaTemp).TotalSeconds / 60).ToString("0.0");
                            minutos = Convert.ToDouble(min);
                        }
                        else
                        {
                            minutos = 0;
                        }
                    }
                    if (fechaUltima != null)
                    {
                        flag = true;
                        fechaTemp = item.lista.Select(x => x.FechaLlamada).FirstOrDefault().Value;
                        double contesto = Convert.ToDouble(item.lista.Select(x => x.DuracionContesto).FirstOrDefault());
                        double timbrado = Convert.ToDouble(item.lista.Select(x => x.DuracionTimbrado).FirstOrDefault());
                        fechaTemp = fechaTemp.AddSeconds(contesto + timbrado);
                    }
                    totalTimbrado += (item.lista.Select(s => Convert.ToDouble(s.DuracionTimbrado))).Sum();
                    totalContesto += (item.lista.Select(s => Convert.ToDouble(s.DuracionContesto))).Sum();
                    if (minutos >= 0)
                    {
                        totalPerdido += minutos;
                    }
                    itemDetalle.NumeroLlamadas = item.lista.Count().ToString();
                    item.lista = item.lista.OrderBy(x => x.FechaLlamada).ToList();

                    itemDetalle.DuracionTimbrado = String.Concat(item.lista.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + "<br />"));
                    itemDetalle.EstadoLlamada = String.Concat(item.lista.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
                    var existeFechaNull = item.lista.Select(x => x.FechaLlamadaFin == null).ToList();
                    if (existeFechaNull.Count() > 0)
                    {
                        itemDetalle.FechaLlamada = String.Concat(item.lista.Select(o => "<strong >I: </strong >" + o.FechaLlamada.Value.ToString("HH:mm:ss") + "<strong> T: </strong >" + o.FechaLlamadaFin.Value.ToString("HH:mm:ss") + "<br />"));
                    }
                    else
                    {
                        string html = "";
                        foreach (var llamada in item.lista)
                        {
                            if (llamada.FechaLlamadaFin != null)
                                html = html + "<strong >I: </strong >" + llamada.FechaLlamada.Value.ToString("HH:mm:ss") + "<strong> T: </strong >" + llamada.FechaLlamadaFin.Value.ToString("HH:mm:ss") + "<br />";
                            else
                                html = html + "<strong >I: </strong >" + llamada.FechaLlamada.Value.ToString("HH:mm:ss") + "<strong> T: -</strong ><br />";
                        }
                        itemDetalle.FechaLlamada = html;
                    }

                }
                else
                {
                    var fechaUltima = item.UnicoFechaLlamada;
                    if (count > 0 && flag)
                    {
                        if (diaFecha == fechaTemp.Day && fechaUltima != null)
                        {
                            var min = ((fechaUltima.Value - fechaTemp).TotalSeconds / 60).ToString("0.0");
                            minutos = Convert.ToDouble(min);
                        }
                        else
                        {
                            minutos = 0;
                        }
                    }
                    if (fechaUltima != null)
                    {
                        flag = true;
                        fechaTemp = fechaUltima.Value;
                        double contesto = Convert.ToDouble(item.UnicoContesto);
                        double timbrado = Convert.ToDouble(item.UnicoTimbrado);
                        fechaTemp = fechaTemp.AddSeconds(contesto + timbrado);
                    }
                    totalTimbrado += Convert.ToDouble(item.UnicoTimbrado);
                    totalContesto += Convert.ToDouble(item.UnicoContesto);
                    if (minutos >= 0)
                    {
                        totalPerdido += minutos;
                    }
                    string date = item.UnicoFechaLlamada == null ? "" : item.UnicoFechaLlamada.Value.ToString("yyyy/MM/dd HH:mm");
                    itemDetalle.NumeroLlamadas = "1";
                    //itemDetalle.DuracionTimbrado = String.Concat(item.lista.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + " <strong ><br />"));
                    //itemDetalle.EstadoLlamada = String.Concat(item.lista.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada) + "<br />");
                    //itemDetalle.FechaLlamada = String.Concat(item.lista.Select(o => " <strong >I: </strong >" + o.FechaLlamada.Value.ToString("yyyy/MM/dd HH:mm") + "<strong> T: </strong >" + o.FechaLlamadaFin.Value.ToString("yyyy/MM/dd HH:mm") + "<br />"));

                    itemDetalle.DuracionTimbrado = item.UnicoEstadoLlamada + " <strong >- TT:</strong >" + item.UnicoTimbrado + "  <strong >TC:</strong >" + item.UnicoContesto + " <strong >-</strong > " + date + "<br /><strong id='estadoNuevoT'>Nuevo Estado: </strong ><strong id='estadoNuevoC'>" + item.UnicoClasificacion + "</strong><br />";
                }
                itemDetalle.MinutosIntervale = minutos;
                itemDetalle.MinutosTotalContesto = totalContesto;
                itemDetalle.MinutosTotalTimbrado = totalTimbrado;
                itemDetalle.MinutosTotalPerdido = totalPerdido;
                count++;

                mayorPerdido = mayorPerdido > minutos ? mayorPerdido : minutos;
                itemDetalle.MayorTiempo = mayorPerdido;

                itemDetalle.TiemposTresCX = String.Concat(item.llamadasTresCX.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + "<br />"));
                itemDetalle.EstadosTresCX = String.Concat(item.llamadasTresCX.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
                var listaActividades = _unitOfWork.OportunidadLogRepository.ReporteActividadOcurrencia(item.IdOportunidad);

                // ValorEstatico.IdEstadoOcurrenciaEjecutado ValorEstatico.IdEstadoOcurrenciaNoEjecutado ValorEstatico.IdEstadoOcurrenciaAsignacionManual Respectivamente
                itemDetalle.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 1 && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
                itemDetalle.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 2 && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
                itemDetalle.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == 7 && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();

                var html1 = "";
                var html2 = "";
                foreach (var llamada in item.llamadasTresCX)
                {
                    if (llamada.NombreGrabacion != null)
                    {
                        html1 = html1 + "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamada3CX('" + llamada.NombreGrabacion + "')\"><b>Escuchar</b></a>";
                    }
                    html1 = html1 + "</br>";

                }
                foreach (var llamada in item.lista)
                {
                    if (llamada.NombreGrabacion != null)
                    {
                        html2 = html2 + "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamada('" + llamada.NombreGrabacion + "')\"><b>Escuchar</b></a>";
                    }
                    html2 = html2 + "</br>";

                }
                itemDetalle.NombreGrabacionTresCX = html1;
                itemDetalle.NombreGrabacionIntegra = html2;
                final.Add(itemDetalle);
            }
            var data = final.OrderByDescending(s => s.FechaReal).ToList();

            var total = 0;
            if (data.Count != 0)
            {
                total = data.FirstOrDefault().TotalOportunidades;
            }

            return new ObtenerRealizadasRespuestaDTO() { Records = data, Total = total };
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera plantilla para centro de costo para programa especifico.
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <param name="idPlantilla"></param>
        /// <returns> string: plantilla </returns>
        public string GenerarPlantillaCentroCosto(int idCentroCosto, int idPlantilla)
        {
            try
            {
                PlantillaClaveValorService plantillaClaveValorService = new PlantillaClaveValorService(_unitOfWork);
                DocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);
                ListadoEtiquetaService listadoEtiquetaService = new ListadoEtiquetaService(_unitOfWork);
                CentroCostoService centroCostoService = new CentroCostoService(_unitOfWork);
                PEspecificoService pEspecificoService = new PEspecificoService(_unitOfWork);
                PGeneralService pGeneralService = new PGeneralService(_unitOfWork);

                string plantilla = string.Empty;
                string valor = string.Empty;

                plantilla = plantillaClaveValorService.ObtenerPorIdPlantilla(idPlantilla).FirstOrDefault().Valor;
                var respuesta = centroCostoService.ObtenerCentroCostoParaPEspecifico(idCentroCosto);
                if (respuesta != null)
                {
                    plantilla = plantilla.Replace("{tPartner.nombre}", respuesta.NombrePartner);
                    plantilla = plantilla.Replace("{tPEspecifico.nombre}", respuesta.NombrePEspecifico);
                }

                foreach (string word in plantilla.Split('{'))
                {
                    if (word.Contains('}'))
                    {
                        string etiqueta = word.Split('}')[0];
                        if (etiqueta.Contains("TemplateV2"))
                        {
                            DocumentoService documentoService = new DocumentoService(_unitOfWork);
                            var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(idCentroCosto);
                            var pGeneral = pGeneralService.ObtenerPorId(pEspecifico.IdProgramaGeneral.Value);

                            List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = documentoService.ObtenerListaSeccionDocumentoProgramaGeneral(pGeneral.Id);

                            string valorV2 = string.Empty;
                            string[] array = etiqueta.Split(".");

                            string nombreSeccion = array[array.Length - 1];
                            bool conTitulo = nombreSeccion == "Estructura Curricular" ? true : false;

                            List<ProgramaGeneralSeccionAnexosHTMLDTO> seccion = listadoEtiquetaService.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones.Where(x => x.Seccion == nombreSeccion).ToList(), conTitulo);

                            foreach (ProgramaGeneralSeccionAnexosHTMLDTO item01 in seccion)
                                valorV2 += item01.Contenido;

                            if (valorV2 == "")
                            {
                                nombreSeccion = nombreSeccion == "Certificacion" ? "Certificación" : nombreSeccion;
                                List<SeccionDocumentoDTO> seccionV1 = documentoSeccionPwService.ObtenerSecciones(pGeneral.Id).Where(x => x.Titulo == nombreSeccion).ToList();
                                foreach (SeccionDocumentoDTO item01 in seccionV1)
                                    valorV2 += item01.Contenido;
                            }
                            if (valorV2 != null)
                                valor = valorV2;
                            else
                                valor = null;
                        }
                        //Separamos solo los Id´s
                        else if (etiqueta.Contains("Template") && !etiqueta.Contains("V2"))
                        {
                            List<string> ListaPalabras = new List<string>();
                            char[] delimitador = new char[] { '.' };
                            string IdPlantilla = "";
                            string IdColumna = "";
                            string[] array = etiqueta.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string s in array)
                            {
                                ListaPalabras.Add(s);
                            }
                            IdPlantilla = ListaPalabras[3].ToString();
                            IdColumna = ListaPalabras[4].ToString();
                            string Etiquetatemp = IdPlantilla + "." + IdColumna;
                            var result = _unitOfWork.PEspecificoRepository.ObtenerSeccionEtiquetaAgendaMensaje(IdColumna, IdPlantilla, idCentroCosto);
                            if (result != null)
                                valor = result.Valor;
                            else
                                valor = null;
                        }
                        else
                        {
                            valor = "";
                        }
                        if (valor != null)
                        {
                            valor = valor.Replace("#$%", "<br>");
                            plantilla = plantilla.Replace("{" + etiqueta + "}", valor);
                        }
                        else
                        {
                            plantilla = plantilla.Replace("{" + etiqueta + "}", "");
                        }
                    }
                }
                return plantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        public PlantillaWhatsAppCalculadoDTO GenerarPlantillaWhatsapp(int idOportunidad, int idPlantilla)
        {
            try
            {
                if (!_unitOfWork.OportunidadRepository.Exist(idOportunidad))
                {
                    throw new BadRequestException("Oportunidad no existente");
                }
                if (!_unitOfWork.PlantillaRepository.Exist(idPlantilla))
                {
                    throw new BadRequestException("Plantilla no existente");
                }

                IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                ReemplazoEtiquetaPlantillaDTO reemplazoEtiquetaO = new()
                {
                    IdOportunidad = idOportunidad,
                    IdPlantilla = idPlantilla
                };

                var plantillaWhatsApp = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(reemplazoEtiquetaO).WhatsAppReemplazado;
                return plantillaWhatsApp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 10/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp de resumen de grabaciones
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        public PlantillaWhatsAppCalculadoDTO GenerarPlantillaWhatsappResumenGrabaciones(int idOportunidad, int idPlantilla, int idResumenGrabacionOnline, int idPEspecificoSesion, int idProcesamientoTipoGenerar)
        {
            try
            {
                if (!_unitOfWork.OportunidadRepository.Exist(idOportunidad))
                {
                    throw new BadRequestException("Oportunidad no existente");
                }
                if (!_unitOfWork.PlantillaRepository.Exist(idPlantilla))
                {
                    throw new BadRequestException("Plantilla no existente");
                }
                if (!_unitOfWork.ResumenGrabacionOnlineRepository.Exist(idResumenGrabacionOnline))
                {
                    throw new BadRequestException("ResumenGrabacionOnline no existente");
                }

                IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                ReemplazoEtiquetaPlantillaDTO reemplazoEtiquetaO = new()
                {
                    IdOportunidad = idOportunidad,
                    IdPlantilla = idPlantilla,
                    IdResumenGrabacionOnline = idResumenGrabacionOnline,
                    IdPEspecificoSesion = idPEspecificoSesion,
                    IdProcesamientoTipoGenerar = idProcesamientoTipoGenerar
                };

                var plantillaWhatsApp = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(reemplazoEtiquetaO).WhatsAppReemplazado;
                return plantillaWhatsApp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 30/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        public PlantillaWhatsAppCalculadoDTO GenerarPlantillaWhatsappComercial(int idOportunidad, int idPlantilla)
        {
            try
            {
                if (!_unitOfWork.OportunidadRepository.Exist(idOportunidad))
                {
                    throw new BadRequestException("Oportunidad no existente");
                }
                if (!_unitOfWork.PlantillaRepository.Exist(idPlantilla))
                {
                    throw new BadRequestException("Plantilla no existente");
                }

                IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                ReemplazoEtiquetaPlantillaDTO reemplazoEtiquetaO = new()
                {
                    IdOportunidad = idOportunidad,
                    IdPlantilla = idPlantilla
                };

                var plantillaWhatsApp = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetasComercial(reemplazoEtiquetaO).WhatsAppReemplazado;

                plantillaWhatsApp = reemplazoEtiquetaPlantillaService.ReemplazarValoresTilde(plantillaWhatsApp);

                return plantillaWhatsApp;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        public PlantillaWhatsAppCalculadoDTO GenerarPlantillaWhatsappAlterno(int idOportunidad, int idPlantilla)
        {
            try
            {
                if (!_unitOfWork.OportunidadRepository.Exist(idOportunidad))
                {
                    throw new BadRequestException("Oportunidad no existente");
                }
                if (!_unitOfWork.PlantillaRepository.Exist(idPlantilla))
                {
                    throw new BadRequestException("Plantilla no existente");
                }
                IReemplazoEtiquetaPlantillaService ReemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta = new()
                {
                    IdOportunidad = idOportunidad,
                    IdPlantilla = idPlantilla
                };
                PlantillaWhatsAppCalculadoDTO whatsAppReemplazado = ReemplazoEtiquetaPlantillaService.ReemplazarEtiquetasNuevasOportunidades(reemplazoEtiqueta).WhatsAppReemplazado;

                if (whatsAppReemplazado == null)
                {
                    throw new BadRequestException("La Plantilla no corresponde a WhatsApp"); 
                }
                return whatsAppReemplazado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de programas asignados y cursos asignados
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        public (List<ProgramaAsignadoDTO> ProgramasAsignados, List<CursoAsignadoDTO> CursosAsignados) ObtenerPEspecificoAccesoTemporalCombo()
        {
            try
            {
                List<PEspecificoNuevoAulaVirtualDTO> resultado = _unitOfWork.PEspecificoRepository.ObtenerPEspecificoNuevoAulaVirtualTipo();

                List<ProgramaAsignadoDTO> programasAsignados = resultado.GroupBy(x => new
                {
                    x.IdPEspecifico,
                    x.NombrePEspecifico,
                    x.IdCentroCosto,
                    x.EstadoP,
                    x.Modalidad,
                    x.IdPGeneral,
                    x.Ciudad,
                    x.IdCursoMoodle,
                    x.IdCursoMoodlePrueba,
                    x.TipoPEspecifico
                }).Select(s => new ProgramaAsignadoDTO
                {
                    IdPEspecifico = s.Key.IdPEspecifico,
                    NombrePEspecifico = s.Key.NombrePEspecifico,
                    IdCentroCosto = s.Key.IdCentroCosto,
                    EstadoP = s.Key.EstadoP,
                    Modalidad = s.Key.Modalidad,
                    IdPGeneral = s.Key.IdPGeneral,
                    Ciudad = s.Key.Ciudad,
                    IdCursoMoodle = s.Key.IdCursoMoodle,
                    IdCursoMoodlePrueba = s.Key.IdCursoMoodlePrueba,
                    TipoPEspecifico = s.Key.TipoPEspecifico
                }).ToList();

                List<CursoAsignadoDTO> cursosAsignados = resultado.Select(s => new CursoAsignadoDTO
                {
                    IdPEspecificoPadre = s.IdPEspecifico,
                    IdPEspecifico = s.IdPEspecificoHijo,
                    NombrePEspecifico = s.NombrePEspecificoHijo
                }).ToList();

                return (programasAsignados, cursosAsignados);
            }
            catch
            {
                throw;
            }
        }
        /// Auto: Joseph Llanque
        /// Fecha: 18/08/2024
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        public PlantillaWhatsAppEnvioAccesoDTO GenerarPlantillaWhatsappAlumno(int idAlumno, int idPlantilla)
        {
            try
            {
                IOportunidadService oportunidadAlumnoService = new OportunidadService(_unitOfWork);
                var datoOportunidad = oportunidadAlumnoService.ObtenerDatosOportunidadAlumno(idAlumno);
                var idOportunidad = datoOportunidad.IdOportunidad;
                if (!_unitOfWork.OportunidadRepository.Exist(idOportunidad))
                {
                    throw new BadRequestException("Oportunidad no existente");
                }
                if (!_unitOfWork.PlantillaRepository.Exist(idPlantilla))
                {
                    throw new BadRequestException("Plantilla no existente");
                }

                IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                ReemplazoEtiquetaPlantillaDTO reemplazoEtiquetaO = new()
                {
                    IdOportunidad = idOportunidad,
                    IdPlantilla = idPlantilla
                };

                var plantillaWhatsApp = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(reemplazoEtiquetaO).WhatsAppReemplazado;
                PlantillaWhatsAppEnvioAccesoDTO resultado = new()
                {
                    Plantilla = plantillaWhatsApp.Plantilla,
                    ListaEtiquetas = plantillaWhatsApp.ListaEtiquetas,
                    DatoAlumno= datoOportunidad
                };


                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ActividadAgendaDTO> ObtenerMensajesRecibidosComercial(int idPersonal)
        {
            return _unitOfWork.AgendaTabRepository.ObtenerMensajesRecibidosComercial(idPersonal);
        }

        public List<ActividadAgendaDTO> ObtenerMensajesRecibidosChatPortal(int idPersonal)
        {
            return _unitOfWork.AgendaTabRepository.ObtenerMensajesRecibidosChatPortal(idPersonal);
        }
        public List<ActividadAgendaDTO> ObtenerCorreosAgendaComercial(int idPersonal)
        {
            return _unitOfWork.AgendaTabRepository.ObtenerCorreosAgendaComercial(idPersonal);
        }
        public IEnumerable<ComboDTO> ObtenerCentroCostoAgenda()
        {
            return _unitOfWork.CentroCostoRepository.ObtenerCentroCostoAgenda();
        }
        public AvatarAlumnoDTO ObtenerAvatar(int idAlumno)
        {
            try
            {
                String genero = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno).IdGenero == 2 ? "F" : "M";
                return _unitOfWork.AlumnoRepository.ObtenerAvatar(idAlumno, genero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Envio de correo automatico
        /// </summary>
        /// <param name="idOportunidad"> Id Oportunidad </param>
        /// <param name="idPersonalAsignado"> Id Plantilla </param>
        /// <returns></returns>
        public void EnvioCorreoAsignacionAsesor(int idOportunidad, int idPersonalAsignado)
        {
            try
            {
                if (idOportunidad == 0)
                    return;

                if (idPersonalAsignado == 125) // Asignacion Automatica
                    return;

                var personalAsignado = _unitOfWork.OportunidadLogRepository.ObtenerDetallePersonalAsignado(idOportunidad);

                // Validar asesores asignados
                if (personalAsignado == null || personalAsignado.Count() == 0)
                    return;

                if (personalAsignado.Any(x => idPersonalAsignado != x.Valor && x.Valor != 125))
                    return;

                if (personalAsignado.Where(x => idPersonalAsignado == x.Valor).ToList().Count() > 1)
                    return;

                // 827 Correo Informacion del Curso Completo
                EnviarCorreoOportunidadAutomatico(idOportunidad, 1967, "Automatico1967");

            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Envio de correo automatico
        /// </summary>
        /// <param name="idOportunidad"> Id Oportunidad </param>
        /// <param name="idPlantilla"> Id Plantilla </param>
        /// <param name="usuario"> Usuario </param>
        /// <returns></returns>
        public void EnviarCorreoOportunidadAutomatico(int idOportunidad, int idPlantilla, string usuario)
        {
            try
            {
                if (idOportunidad == 0)
                    return;

                var existeEnvio = _unitOfWork.MandrilEnvioCorreoRepository.ValidarEnvioCorreo(idOportunidad, usuario);
                if (existeEnvio)
                    return;

                var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaService(_unitOfWork);

                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad)!;

                //Si el idasesor es 125 no se envia nada
                if (oportunidad.IdPersonalAsignado == 125)
                {
                    return;
                }

                ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta = new()
                {
                    IdOportunidad = idOportunidad,
                    IdPlantilla = idPlantilla,
                };
                // 134 Correo Confirmación de Participación
                // 827 Correo Informacion del Curso Completo
                // 1967 Correo Informacion del Curso CompletoV2

                var resultadoReemplazo = reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades(reemplazoEtiqueta);

                var asesor = _unitOfWork.PersonalRepository.ObtenerNombreApellidoPorId(oportunidad.IdPersonalAsignado!.Value);

                var mensajeCorreo = resultadoReemplazo.EmailReemplazado.CuerpoHTML;

                if (!mensajeCorreo.Contains("https://repositorioweb.blob.core.windows.net/firmas/"))
                {
                    string firma = string.Empty;
                    string[] separacionEmail = asesor.Email.Split('@');
                    firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + separacionEmail[0] + ".png' />";
                    mensajeCorreo += "<br/>--<br/>" + firma;
                }
                var emailAlumno = _unitOfWork.AlumnoRepository.ObtenerEmailAlumno(oportunidad.IdAlumno!.Value)!;

                var mailData = new TMKMailDataDTO
                {
                    Sender = asesor.Email,
                    Recipient = emailAlumno.Email1,
                    Subject = resultadoReemplazo.EmailReemplazado.Asunto,
                    Message = mensajeCorreo,
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null,
                    RemitenteC = string.Concat(asesor.Nombres, ' ', asesor.Apellidos)
                };

                TMK_MailService serviceMail = new TMK_MailService();
                serviceMail.SetData(mailData);

                var listaMandrilEnvioCorreo = new List<MandrilEnvioCorreo>();
                List<TMKMensajeIdDTO> MensajeIdDTO = serviceMail.SendMessageTask();

                foreach (var mensaje in MensajeIdDTO)
                {
                    var mandrilEnvioCorreoEntidad = new MandrilEnvioCorreo
                    {
                        IdOportunidad = oportunidad.Id,
                        IdPersonal = asesor.Id,
                        IdAlumno = oportunidad.IdAlumno,
                        IdCentroCosto = oportunidad.IdCentroCosto,
                        IdMandrilTipoAsignacion = 0,
                        EstadoEnvio = 1,
                        IdMandrilTipoEnvio = 2,
                        FechaEnvio = DateTime.Now,
                        Asunto = resultadoReemplazo.EmailReemplazado.Asunto,
                        FkMandril = mensaje.MensajeId,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        EsEnvioMasivo = false
                    };
                    listaMandrilEnvioCorreo.Add(mandrilEnvioCorreoEntidad);
                }

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    GmailCorreo gmailCorreo = new GmailCorreo
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = resultadoReemplazo.EmailReemplazado.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = mensajeCorreo,
                        Seen = false,
                        Remitente = asesor.Email,
                        Cc = "",
                        Bcc = "",
                        Destinatarios = emailAlumno.Email1,
                        IdPersonal = asesor.Id,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        IdClasificacionPersona = oportunidad.IdClasificacionPersona,
                        Estado = true
                    };
                    _unitOfWork.GmailCorreoRepository.Add(gmailCorreo);

                    Interaccion interacionEntidad = new Interaccion()
                    {
                        IdActividadDetalle = oportunidad.IdActividadDetalleUltima,
                        IdTipoInteraccionGeneral = 1,
                        Fecha = DateTime.Now,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    _unitOfWork.InteraccionRepository.Add(interacionEntidad);

                    _unitOfWork.MandrilEnvioCorreoRepository.Add(listaMandrilEnvioCorreo);

                    _unitOfWork.Commit();
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
