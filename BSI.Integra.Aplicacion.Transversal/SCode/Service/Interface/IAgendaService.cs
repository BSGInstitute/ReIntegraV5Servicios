using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAgendaService
    {
        (Dictionary<string, List<ActividadAgendaDTO>> ActividadesAgenda, int CantidadRN2) CargarActividadSeleccionadaPorFiltro(int idTab, string codigoAreaTrabajo, Dictionary<string, string>? filtros, int idAsesor);
        //(Dictionary<string, List<ActividadAgendaV2DTO>> ActividadesAgenda, int CantidadRN2) CargarActividadSeleccionadaPorFiltroV2(int idTab, string codigoAreaTrabajo, Dictionary<string, string>? filtros, int idAsesor);
        List<ActividadAgendaDTO> ObtenerActividadFichaChat(int idTab, string codigoAreaTrabajo,int idAsesor,int idMatriculaCabecera);
        Dictionary<string, List<ActividadAgendaDTO>> ObtenerActividades(int idAsesor, string codigoAreaTrabajo);
        (Dictionary<string, List<ActividadAgendaDTO>> ActividadesAgenda, Dictionary<string, bool> EstadosTabs, string LogCarlos) ObtenerActividadesAgenda(int idAsesor, bool validarTabs, string codigoAreaTrabajo, bool flagAgendaWhatsapp);
        string GenerarPlantillaCentroCosto(int idCentroCosto, int idPlantilla);
        PlantillaWhatsAppCalculadoDTO GenerarPlantillaWhatsapp(int idOportunidad, int idPlantilla);
        PlantillaWhatsAppCalculadoDTO GenerarPlantillaWhatsappResumenGrabaciones(int idOportunidad, int idPlantilla, int idResumenGrabacionOnline, int idPEspecifico, int idProcesamientoTipoGenerar);
        PlantillaWhatsAppEnvioAccesoDTO GenerarPlantillaWhatsappAlumno(int idAlumno, int idPlantilla);
        PlantillaWhatsAppCalculadoDTO GenerarPlantillaWhatsappAlterno(int idOportunidad, int idPlantilla);
        PlantillaWhatsAppCalculadoDTO GenerarPlantillaWhatsappComercial(int idOportunidad, int idPlantilla);
        (List<ProgramaAsignadoDTO> ProgramasAsignados, List<CursoAsignadoDTO> CursosAsignados) ObtenerPEspecificoAccesoTemporalCombo();
        List<ActividadAgendaDTO> ObtenerMensajesRecibidosComercial(int idPersonal);
        List<ActividadAgendaDTO> ObtenerCorreosAgendaComercial(int idPersonal);
        IEnumerable<ComboDTO> ObtenerCentroCostoAgenda();
        AvatarAlumnoDTO ObtenerAvatar(int idAlumno);
        void EnviarCorreoOportunidadAutomatico(int idOportunidad, int idPlantilla, string usuario);
        void EnvioCorreoAsignacionAsesor(int idOportunidad, int idPersonalAsignado);
    }
}
