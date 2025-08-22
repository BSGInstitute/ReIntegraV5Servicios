using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPaisService
    {
        IEnumerable<PaisDTO> ObtenerPais();
        IEnumerable<PaisComboDTO> ObtenerPaisCombo();
        PaisDTO RegistrarPais(RegistroPaisDTO registroPais, string usuario);
        IEnumerable<PaisZonaHorariaDTO> ObtenerPaisZonaHoraria();
        public IEnumerable<PaisDTO> ObtenerPaisConEstadoVisualizacion();
        UrlBlockStoragePais ObtenerRutaUrlBandera();
        UrlBlockStoragePais ObtenerRutaUrlIcono();
        IEnumerable<PaisMonedaComboDTO> ObtenerComboConMoneda();
        public IEnumerable<PaisZonaHorariaComboDTO> ObtenerComboConZonaHoraria();
        public IEnumerable<PaisZonaHorariaComboDTO> ObtenerComboZonaHorarioActivo();
        List<int> ObtenerTodoCodigoPais();
        bool Eliminar(int id, string usuario);
        List<ComboDTO> ObtenerCombo();
        IEnumerable<ComboDTO> ObtenerListaPais();
    }
}
