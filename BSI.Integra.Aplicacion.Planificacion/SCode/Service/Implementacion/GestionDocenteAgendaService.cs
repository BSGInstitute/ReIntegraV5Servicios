using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    /// Autor: Joseph Llanque
    /// Fecha: 20/02/2026
    /// Versión: 1.0
    /// <summary>
    /// Servicio de agenda de docentes. Orquesta las consultas del repositorio
    /// para construir las respuestas de los endpoints de GestionDocenteAgenda.
    /// </summary>
    public class GestionDocenteAgendaService : IGestionDocenteAgendaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GestionDocenteAgendaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// Autor: Jose Vega
        /// Fecha: 19/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle completo de un docente: cabecera con datos personales, flujo asignado
        /// y todos sus cronogramas con sesiones, priorizando el curso indicado.
        /// </summary>
        /// <param name="idProveedor">Identificador del docente/proveedor.</param>
        /// <param name="idPEspecifico">Identificador del curso a priorizar en la lista.</param>
        /// <param name="idGestionContacto">Identificador opcional del GestionContacto para obtener el flujo.</param>
        /// <returns>DocenteAgendaDetalleDTO con toda la información.</returns>
        public DocenteAgendaDetalleDTO ObtenerDetalleDocente(int idProveedor, int idPEspecifico, int? idGestionContacto)
        {
            try
            {
                var cabecera = _unitOfWork.GestionDocenteAgendaRepository.ObtenerCabeceraDocente(idProveedor);
                if (cabecera == null) return null;

                DocenteAgendaFlujoDTO flujo = null;
                if (idGestionContacto.HasValue)
                {
                    flujo = _unitOfWork.GestionDocenteAgendaRepository.ObtenerFlujoDocente(idGestionContacto.Value);
                }

                var cronogramas = _unitOfWork.GestionDocenteAgendaRepository.ObtenerCronogramasDocente(idProveedor, idPEspecifico);

                foreach (var cronograma in cronogramas)
                {
                    cronograma.Sesiones = _unitOfWork.GestionDocenteAgendaRepository.ObtenerSesionesPorCursoYDocente(idProveedor, cronograma.IdPEspecifico);
                }

                return new DocenteAgendaDetalleDTO
                {
                    Cabecera = cabecera,
                    Flujo = flujo,
                    Cronogramas = cronogramas,
                    IdGestionContactoDocenteFlujo = flujo?.IdGestionContactoDocenteFlujo
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuración de todos los tabs activos para un área de trabajo.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo (ej: "PLA").</param>
        /// <returns>Lista de AgendaTabConfiguracionPlanificacionAlternoDTO.</returns>
        public List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo)
        {
            try
            {
                return _unitOfWork.GestionDocenteAgendaRepository.ObtenerTabsConfigurados(codigoAreaTrabajo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Joseph Llanque / Jose Vega
        /// Fecha: 05/03/2026
        /// Versión: 1.1
        /// <summary>
        /// Obtiene las actividades de todos los tabs que tengan VisualizarActividad = true
        /// y CargarInformacionInicial = true, agrupadas por nombre de tab.
        /// Dentro de cada tab, las filas planas se agrupan por docente + centro de costo.
        /// </summary>
        public Dictionary<string, List<ActividadAgendaAgrupadaDTO>> ObtenerActividades(int idAsesor, string codigoAreaTrabajo)
        {
            try
            {
                var resultado = new Dictionary<string, List<ActividadAgendaAgrupadaDTO>>();
                var tabs = _unitOfWork.GestionDocenteAgendaRepository.ObtenerTabsConfigurados(codigoAreaTrabajo);

                foreach (var tab in tabs.Where(t => t.VisualizarActividad && t.CargarInformacionInicial))
                {
                    var actividades = _unitOfWork.GestionDocenteAgendaRepository.ObtenerActividades(tab, idAsesor);
                    resultado[tab.Nombre] = AgruparActividades(actividades);
                }

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Carga las actividades de un tab específico por su ID y retorna también la cantidad total.
        /// </summary>
        /// <param name="idTab">ID del tab (T_AgendaTabConfiguracionPlanificacion.Id).</param>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo.</param>
        /// <param name="idAsesor">ID del personal asignado; 0 para no filtrar.</param>
        /// <returns>CargarActividadPorTabResultadoDTO con actividades agrupadas y cantidad.</returns>
        public CargarActividadPorTabResultadoDTO CargarActividadSeleccionadaPorFiltro(int idTab, string codigoAreaTrabajo, int idAsesor)
        {
            try
            {
                var actividadesAgenda = new Dictionary<string, List<ActividadAgendaAgrupadaDTO>>();
                var tabs = _unitOfWork.GestionDocenteAgendaRepository.ObtenerTabsConfiguradosPorIdTab(codigoAreaTrabajo, idTab);

                foreach (var tab in tabs)
                {
                    var actividades = _unitOfWork.GestionDocenteAgendaRepository.ObtenerActividades(tab, idAsesor);
                    actividadesAgenda[tab.Nombre] = AgruparActividades(actividades);
                }

                int cantidad = actividadesAgenda.Values.Sum(v => v.Count);

                return new CargarActividadPorTabResultadoDTO
                {
                    ActividadesAgenda = actividadesAgenda,
                    Cantidad = cantidad
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 03/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de docentes que comparten el mismo centro de costo que la gestión de contacto proporcionada.
        /// Aplica agrupación por proveedor y curso en memoria.
        /// </summary>
        /// <param name="idGestionContacto">Identificador de la gestión de contacto base.</param>
        /// <returns>Lista de DocenteConCursoDTO.</returns>
        public List<DocenteConCursoDTO> ObtenerDocentesPorGestionContacto(int idGestionContacto)
        {
            try
            {
                var lista = _unitOfWork.GestionDocenteAgendaRepository.ObtenerDocentesPorGestionContacto(idGestionContacto);
                return lista
                    .GroupBy(x => new { x.IdProveedor, x.IdPEspecifico })
                    .Select(g => g.First())
                    .OrderBy(x => x.NombreDocente)
                    .ThenBy(x => x.NombreCurso)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jose Vega / Joseph Llanque
        /// Fecha: 03/03/2026 — Versión: 2.0 (23/03/2026)
        /// <summary>
        /// Obtiene información base del docente: email, encuestas, última comunicación y puntaje global.
        /// El historial de WhatsApp y correos se carga bajo demanda via ObtenerHistorialWhatsApp
        /// y ObtenerHistorialCorreos (reducido de 5 a 3 SPs).
        /// </summary>
        public InformacionFaltanteDocenteDTO ObtenerInformacionFaltanteDocente(int idProveedor, int idPEspecifico)
        {
            try
            {
                return _unitOfWork.GestionDocenteAgendaRepository.ObtenerInformacionFaltanteDocente(idProveedor, idPEspecifico);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 23/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el historial de WhatsApp del docente bajo demanda.
        /// </summary>
        public List<WhatsAppHistorialDocenteDTO> ObtenerHistorialWhatsApp(int idProveedor)
        {
            try
            {
                return _unitOfWork.GestionDocenteAgendaRepository.ObtenerHistorialWhatsApp(idProveedor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 23/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el historial de correos del docente bajo demanda.
        /// </summary>
        public List<CorreoResumenDocenteDTO> ObtenerHistorialCorreos(int idProveedor, int idPEspecifico)
        {
            try
            {
                return _unitOfWork.GestionDocenteAgendaRepository.ObtenerHistorialCorreos(idProveedor, idPEspecifico);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 03/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de un correo enviado al docente por su ID.
        /// </summary>
        public CorreoDetalleDocenteDTO ObtenerDetalleCorreo(int idCorreo)
        {
            try
            {
                return _unitOfWork.GestionDocenteAgendaRepository.ObtenerDetalleCorreo(idCorreo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 19/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el contador de alertas del docente.
        /// </summary>
        public ContadorAlertasDTO ObtenerContadorAlertas()
        {
            try
            {
                return _unitOfWork.GestionDocenteAgendaRepository.ObtenerContadorAlertas();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 05/03/2026
        /// Versión: 1.1
        /// <summary>
        /// Agrupa las filas planas del SP por docente + centro de costo.
        /// Las actividades se anidan jerárquicamente: Actividades (cabeceras) → Detalles.
        /// </summary>
        private List<ActividadAgendaAgrupadaDTO> AgruparActividades(List<ActividadAgendaPlanificacionDTO> actividades)
        {
            return actividades
                .GroupBy(a => new { a.IdProveedor, IdCentroCosto = a.IdCentroCosto ?? 0 })
                .Select(g =>
                {
                    var primero = g.First();
                    return new ActividadAgendaAgrupadaDTO
                    {
                        IdProveedor = primero.IdProveedor,
                        NombreDocente = primero.NombreDocente,
                        Celular = primero.Celular,
                        Celular2 = primero.Celular2,
                        Contacto1 = primero.Contacto1,
                        Contacto2 = primero.Contacto2,
                        Correo = primero.Correo,
                        IdGestionContacto = primero.IdGestionContacto,
                        IdPersonal_Asignado = primero.IdPersonal_Asignado,
                        IdClasificacionPersona= primero.IdClasificacionPersona,
                        PersonalAsignado = primero.PersonalAsignado,
                        IdGestionDocenteFlujo = primero.IdGestionDocenteFlujo,
                        NombreFlujo = primero.NombreFlujo,
                        IdCentroCosto = primero.IdCentroCosto,
                        NombreCentroCosto = primero.NombreCentroCosto,
                        CodigoCentroCosto = primero.CodigoCentroCosto,
                        ActividadesCabecera = primero.ActividadesCabecera,
                        NumeroActividades = primero.NumeroActividades,
                        Pais = primero.Pais,
                        Ciudad = primero.Ciudad,
                        IdPEspecifico = primero.IdPEspecifico,
                        NombreCurso = primero.NombreCurso,
                        CodigoCurso = primero.CodigoCurso,
                        FechaInicio = primero.FechaInicio,
                        FechaTermino = primero.FechaTermino,
                        IdProgramaGeneral = primero.IdProgramaGeneral,
                        ProgramaGeneral = primero.ProgramaGeneral,
                        ProximaClase = primero.ProximaClase,
                        HorarioClases = primero.HorarioClases,
                        IdPais = primero.IdPais,
                        PuntajeGlobal = primero.PuntajeGlobal,
                        IdCategoria = primero.IdCategoria,
                        NombreCategoria = primero.NombreCategoria,
                        EncuestaPromedio = primero.EncuestaPromedio,
                        EncuestaCantidad = primero.EncuestaCantidad,
                        EncuestaUltimoComentario = primero.EncuestaUltimoComentario,
                        UsuarioWeb = primero.UsuarioWeb,
                        ContraseniaWeb = primero.ContraseniaWeb,
                        FechaInscritoWeb = primero.FechaInscritoWeb,
                        Actividades = g
                            .Where(a => a.IdActividadCabecera.HasValue)
                            .GroupBy(a => new { a.IdActividadCabecera, a.NombreActividadCabecera })
                            .Select(gc => new ActividadCabeceraItemDTO
                            {
                                IdActividadCabecera = gc.Key.IdActividadCabecera,
                                NombreActividadCabecera = gc.Key.NombreActividadCabecera,
                                Detalles = gc
                                    .Where(a => a.IdActividadDetalle.HasValue)
                                    .Select(a => new ActividadDetalleItemDTO
                                    {
                                        IdActividadDetalle = a.IdActividadDetalle,
                                        NombreActividadDetalle = a.NombreActividadDetalle
                                    })
                                    .ToList()
                            })
                            .ToList()
                    };
                })
                .ToList();
        }
    }
}
