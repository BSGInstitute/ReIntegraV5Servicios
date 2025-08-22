using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPlantillaClaveValorService
    {
        #region Metodos Base
        PlantillaClaveValor Add(PlantillaClaveValor entidad);
        PlantillaClaveValor Update(PlantillaClaveValor entidad);
        bool Delete(int id, string usuario);

        List<PlantillaClaveValor> Add(List<PlantillaClaveValor> listadoEntidad);
        List<PlantillaClaveValor> Update(List<PlantillaClaveValor> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PlantillaClaveValorDTO> ObtenerPlantillaClaveValor();
        IEnumerable<PlantillaClaveValorComboDTO> ObtenerCombo();
        IEnumerable<PlantillaValorDTO> ObtenerPlantillaPorNombrePlantillaBase(string nombrePlantillaBase);
        IEnumerable<PlantillaMailingAgendaDTO> ObtenerPlantillasMailing();
        IEnumerable<PlantillaClaveValorAreaEtiquetaDTO> ObtenerPlantillasPorIdFaseOportunidad(int idFaseOportunidad);
        IEnumerable<PlantillaWhatsAppAgendaDTO> ObtenerPlantillaWhatsAppAgenda();
        IEnumerable<ProblemaCausaDTO> ObtenerCausaProblemaPorIdOportunidad(int idOportunidad);
        IEnumerable<PGeneralCursoRelacionadoDTO> ObtenerCursosRelacionadosPorIdCentroCosto(int idCentroCosto);
        IEnumerable<FiltroDTO> ObtenerPlantillaGenerarMensaje(int idFaseOportunidad);
        List<CursosRelacionadosDTO> ObtenerMontosCursosRelacionados(int idOportunidad, int idEtiqueta);
        IEnumerable<PlantillaClaveValor> ObtenerPorIdPlantilla(int idPlantilla);
        List<PlantillaValorDTO> ObtenerPlantillaPorPlantillaBase(string nombrePlantillaBase);
        List<ContenidoPlantillaDTO> ObtenerTodoPlantillasMailing();
        List<PlantillaWhatsAppDTO> ObtenterPlantillaWhatsAppOperaciones();
        List<FiltroDTO> ObtenerPlantillaGenerarMensajeOperaciones();
        List<ContenidoPlantillaDTO> ObtenerTodoPlantillaPorPersonalAreaTrabajo(int idPersonalAreaTrabajo);
        IEnumerable<PlantillaClaveValor> ObtenerPorIdPlantillaTodos(int idPlantilla);
        IEnumerable<PlantillaAsociacionModuloSistema> ObtenerPlantillaAsociacionModuloSistemaPorIdPlantilla(int idPlantilla);
    }
}
