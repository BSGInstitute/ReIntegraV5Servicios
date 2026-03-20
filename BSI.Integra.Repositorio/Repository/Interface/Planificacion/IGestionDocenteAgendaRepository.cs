using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteAgendaRepository : IGenericRepository<TProveedor>
    {
        DocenteAgendaCabeceraDTO ObtenerCabeceraDocente(int idProveedor);
        List<DocenteAgendaCronogramaDTO> ObtenerCronogramasDocente(int idProveedor, int idPEspecificoPrioridad);
        List<DocenteAgendaSesionDTO> ObtenerSesionesPorCursoYDocente(int idProveedor, int idPEspecifico);
        List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo);
        List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfiguradosPorIdTab(string codigoAreaTrabajo, int idTab);
        List<ActividadAgendaPlanificacionDTO> ObtenerActividades(AgendaTabConfiguracionPlanificacionAlternoDTO tab, int idAsesor);
        List<DocenteConCursoDTO> ObtenerDocentesPorGestionContacto(int idGestionContacto);
        InformacionFaltanteDocenteDTO ObtenerInformacionFaltanteDocente(int idProveedor, int idPEspecifico);
        CorreoDetalleDocenteDTO ObtenerDetalleCorreo(int idCorreo);
        /// <summary>
        /// Obtiene el flujo asignado al docente a partir de su IdGestionContacto.
        /// Consulta T_GestionContactoDocenteFlujo JOIN T_GestionDocenteFlujo.
        /// </summary>
        DocenteAgendaFlujoDTO ObtenerFlujoDocente(int idGestionContacto);
        /// <summary>
        /// Obtiene el body HTML y archivos adjuntos de un correo desde mkt.T_GmailCorreo (BD),
        /// sin conectarse a IMAP. El IdCorreo es el PK de T_GmailCorreo.
        /// </summary>
        CorreoBodyDTO ObtenerCorreoBodyDB(int idCorreo);
        /// <summary>
        /// Obtiene el contador de alertas del docente ejecutando pla.SP_GestionDocenteAlertasContador.
        /// </summary>
        ContadorAlertasDTO ObtenerContadorAlertas();
    }
}
