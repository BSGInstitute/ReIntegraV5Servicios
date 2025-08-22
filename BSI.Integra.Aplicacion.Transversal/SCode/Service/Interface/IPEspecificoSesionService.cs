using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPEspecificoSesionService
    {
        List<PEspecificoSesionFechaHoraInicioDTO> ObtenerFechaHoraInicioSesionPorIdPEspecifico(List<int> idPEspecifico, int tipo);
        IEnumerable<PespecificoSesionCompuestoDTO> ObtenerCronogramaIndividualPorPEspecificoAlterno(DatosProgramaEspecificoDTO programaEspecifico);
        VerificarFechaSesionDTO VerificarFechaSesion(int idSesion, DateTime fecha);
        bool ActualizarFechaParaSesionRecorrerFechas(PEspecificoSesion pEspecificoSesion, string usuario);
        (bool EstadoCruce, IEnumerable<CruceSesionPEspecificoDTO?> Cruces, string? Detalle) ActualizarDatosCronogramaSesiones(InformacionCronogramaSesionesDTO dto, string usuario);
        bool EstablecerSesionInicial(int idProgramaEspecificoSesion, string usuario);
        DateTime ObtenerFechaAsignar(InformacionPespecificoHijoDTO curso, DateTime fechaAsignar, byte dia, byte[] diasFrecuencia, IEnumerable<FeriadoDTO> listaFeriados);
        bool CancelarWebinar(CancelarWebinarDTO dto, string usuario);
        bool EliminarSesion(int idProgramaEspecifico, int idProgramaEspecificoSesion, string usuario);
        bool ConfirmarWebinar(ConfirmacionWebinarDTO dto, string usuario);
    }
}
