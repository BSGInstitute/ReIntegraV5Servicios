using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IAsociarTagProgramaService
    {
        Task<AsociarTagProgramaComboDTO> ObtenerCombosModulo();
        IEnumerable<ParametroContenidoDTO> ObtenerTodoParametroPorIdTag(int idTag);
        List<PGeneralDTO> ObtenerProgramas();
        bool AsociarTag(List<int> idsTags, int idPgeneral, string usuario);
        bool DesasociarTag(int idPGeneral, int idTag, string usuario);
        IEnumerable<ComboDTO> ObtenerTagSinAsociarPw(int idPgeneral);
        IEnumerable<DatosTagPwDTO> ObtenerTodoTagPorPrograma(int idPGeneral);
        TagPw InsertarTagAsociar(CompuestoTagDTO dto, string usuario);
        TagPw ActualizarTag(CompuestoTagDTO dto, string usuario);
        void EliminacionLogicoPorTagPw(int idTag, string usuario, List<ParametroSeoAsociadosDTO> nuevos);
        bool EliminarTag(int idTag, string usuario);
    }
}
