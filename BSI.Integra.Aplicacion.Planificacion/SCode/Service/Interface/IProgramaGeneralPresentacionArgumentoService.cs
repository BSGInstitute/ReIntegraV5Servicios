
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public  interface IProgramaGeneralPresentacionArgumentoService
    {
        IEnumerable<ProgramaGeneralPresentacionArgumentoDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
        ProgramaGeneralPresentacionArgumentoDTO Insertar(CompuestoPresentacionArgumentoModalidadDTO dto, string usuario);
        ProgramaGeneralPresentacionArgumentoDTO Actualizar(ProgramaGeneralPresentacionArgumentoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);

        List<ProgramaGeneralPresentacionArgumentoDetalleAgendaDTO> ObtenerProgramaGeneralPresentacionArgumentoParaAgendaPorIdOportunidad(int idOportunidad);
        IEnumerable<ProgramaGeneralPresentacionArgumentoAgendaDTO> ObtenerProgramaGeneralArgumentoParaAgendaPorIdOportunidad(int idOportunidad);
    }
}
