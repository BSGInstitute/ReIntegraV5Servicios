using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaClaveValorRepository : IGenericRepository<TPlantillaClaveValor>
    {
        #region Metodos Base
        TPlantillaClaveValor Add(PlantillaClaveValor entidad);
        TPlantillaClaveValor Update(PlantillaClaveValor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPlantillaClaveValor> Add(IEnumerable<PlantillaClaveValor> listadoEntidad);
        IEnumerable<TPlantillaClaveValor> Update(IEnumerable<PlantillaClaveValor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlantillaClaveValorDTO> ObtenerPlantillaClaveValor();
        IEnumerable<PlantillaClaveValorComboDTO> ObtenerCombo();
        IEnumerable<PlantillaValorDTO> ObtenerPlantillaPorNombrePlantillaBase(string nombrePlantillaBase);
        IEnumerable<PlantillaMailingAgendaDTO> ObtenerPlantillasMailing();
        IEnumerable<PlantillaClaveValorAreaEtiquetaDTO> ObtenerPlantillasPorIdFaseOportunidad(int idFaseOportunidad);
        IEnumerable<PlantillaWhatsAppAgendaDTO> ObtenerPlantillaWhatsAppAgenda();
        IEnumerable<PlantillaWhatsAppAgendaDTO> ObtenerPlantillaWhatsAppAgendaComercial();
        List<ProblemaCausaDTO> ObtenerCausaProblemaPorIdOportunidad(int idOportunidad);
        Task<List<ProblemaCausaDTO>> ObtenerCausaProblemaPorIdOportunidadAsync(int idOportunidad);
        List<PGeneralCursoRelacionadoDTO> ObtenerCursosRelacionadosPorIdCentroCosto(int idCentroCosto);
        Task<List<PGeneralCursoRelacionadoDTO>> ObtenerCursosRelacionadosPorIdCentroCostoAsync(int idCentroCosto);
        IEnumerable<FiltroDTO> ObtenerPlantillaGenerarMensaje(int idFaseOportunidad);
        List<CursosRelacionadosDTO> ObtenerMontosCursosRelacionados(int idOportunidad, int idEtiqueta);
        IEnumerable<PlantillaClaveValor> ObtenerPorIdPlantilla(int idPlantilla);
        List<PlantillaValorDTO> ObtenerPlantillaPorPlantillaBase(string nombrePlantillaBase);
        List<ContenidoPlantillaDTO> ObtenerTodoPlantillasMailing();
        List<PlantillaWhatsAppDTO> ObtenterPlantillaWhatsAppOperaciones();
        List<FiltroDTO> ObtenerPlantillaGenerarMensajeOperaciones();
        List<FiltroDTO> ObtenerPlantillasModuloAgenda();
        IEnumerable<PlantillaClaveValor> ObtenerPorIdPlantillaTodos(int idPlantilla);
        void EliminacionLogicoPorPlantilla(int idPlantilla, string usuario, List<PlantillaClavesValoresDTO> nuevos);
        PlantillaClaveValor ObtenerPorClaveYPorIdPlantilla(string clave, int idPlantilla);


    }
}