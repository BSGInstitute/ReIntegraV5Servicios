using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAsignacionAutomaticaTempService
    {
        #region Metodos Base
        AsignacionAutomaticaTemp Add(AsignacionAutomaticaTemp entidad);
        AsignacionAutomaticaTemp Update(AsignacionAutomaticaTemp entidad);
        bool Delete(int id, string usuario);

        List<AsignacionAutomaticaTemp> Add(List<AsignacionAutomaticaTemp> listadoEntidad);
        List<AsignacionAutomaticaTemp> Update(List<AsignacionAutomaticaTemp> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public void MapearAsignacionAutomaticaTemp(ref AsignacionAutomaticaTemp asignacionAutomaticaTemp, AsignacionAutomaticaTempDTO nuevo);
        public void MarcarComoProcesados(string[] procesados, int idPagina);
        List<AsignacionAutomaticaError> Validar(AsignacionAutomatica valor);
        public void ValidarRegistroFormularioAsignacionAutomaticaTemp(int idAsignacionAutomaticaTemp, Dictionary<int, string> listaPaises, Dictionary<string, OrigenesCategoriaOrigenDTO> listaOrigenes);
        public AsignacionAutomaticaTemp ObtenerPorId(int idAsignacionAutomaticaTemp);
        public AsignacionAutomaticaTemp ProcesarAsignacionAutomaticaLeadgen(LeadgenInformacionDTO leadgenInformacionDTO);
        public AsignacionAutomaticaTemp ProcesarRegistroFormularioNuevoPortalWeb(string idRegistroPortalWeb, int idPagina);
        public AsignacionAutomaticaTemp MapearAsignacionAutomaticaTemporal(AsignacionAutomaticaTemModeloDTO nuevo);



    }
}
