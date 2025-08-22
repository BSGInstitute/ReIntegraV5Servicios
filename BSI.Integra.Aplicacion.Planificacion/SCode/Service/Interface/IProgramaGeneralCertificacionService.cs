using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralCertificacionService
    {
        IEnumerable<ProgramaGeneralCertificacionDetalleAgendaDTO> ObtenerCertificacionesDetalleParaAgendaPorIdOportunidad(int idOportunidad);
        bool EliminarCertificacionVenta(int idProgramaGeneralCertificacion, string usuario);
        ProgramaGeneralCertificacionDTO GuardarCertificacionesVentas(CompuestoCertificacionModalidadDTO certificadoDTO, string usuario);
    }
}
