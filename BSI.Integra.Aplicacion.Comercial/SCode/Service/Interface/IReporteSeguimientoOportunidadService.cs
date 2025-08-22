using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IReporteSeguimientoOportunidadService
    {
        ReporteSeguimientoOportunidadComboDTO ObtenerCombosReporte(int idPersonal);
        string ModificarLlamadaWebphone(EditarActividadLlamadaDTO obj, string usuario);
        (int IdLlamadaWebphoneAsterisk, int IdLlamadaWebphoneCruceCentral, string url) GenerarNuevaLlamadaActividad(NuevaLlamadaActividadDTO obj, string usuario);
        DatosLlamadaDTO ObtenerDatosNuevaLlamada(int idAlumno, int idPersonalAsignado);
        bool ActualizarCronogramaVersionFinal();
        List<ReporteSeguimientoOportunidadDTO> ReporteSeguimientoOportunidadTresCx(ReporteSeguimientoOportunidadesFiltrosDTO filtros);
        (List<ReporteSeguimientoNWActividadAlternoDTO>, List<BloqueHorarioProcesarBicDTO>) ObtenerListaOportunidadLog3cx(int idOportunidad);
    }
}
