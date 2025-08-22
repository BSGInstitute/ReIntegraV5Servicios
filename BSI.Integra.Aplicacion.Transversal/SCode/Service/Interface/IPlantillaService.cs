using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPlantillaService
    {
        Plantilla ObtenerPorId(int idPlantilla);
        PlantillaAsuntoCuerpoDTO ObtenerPlantillaCorreo(int idPlantilla);
        bool ExistePorId(int idPlantilla);
        IEnumerable<ComboFiltroDTO> ObtenerPlantillaChatIntegraSoporte(int idPlantilla);
        PlantillaEmailMandrillDTO ReemplazarSpeechChatSoporte(string emailPersonal, int idPersonal, int idPlantilla);
        PlantillaDTO ObtenerPorNombre(string nombreB, string nombreP);
        List<PlantillaTipoEnvioDTO> ObtenerListaPlantillasConfiguracionEnvio();
        DatosPlantillaDTO ObtenerPlantillaPorId(int idPlantilla);
        //ReemplazoEtiquetasResultadoDTO ReemplazarEtiquetasNuevasOportunidades(int idOportunidad, int idPlantilla, bool personalPorDefecto = false, int idCentroCosto = 0);
        public List<PlantillaDatoDTO> ObtenerListarPlantilla();
        public PlantillaValorDetalleDTO ObtenerPlantillaClaveValor(int idPlantilla);
        public bool Insertar(CompuestoPlantillaDTO Json, string usuario);
        public List<ComboDTO> ObtenerPlantillasSpeech();
        public List<ComboDTO> ObtenerAllPlantillaSpeechDespedida();

        IEnumerable<ComboPlantillaNombrePlantillaBaseDTO> ObtenerNombrePlantillaBaseEmail();

        IEnumerable<ComboPlantillaNombrePlantillaBaseDTO> ObtenerNombrePlantillaBaseWatsApp();
        PlantillaEmailMandrillDTO ReemplazarEtiquetasProcesoSeleccionReportePostulanteCursoAsesorCapacitacion(int IdPlantilla, int IdPostulanteProcesoSeleccion, Personal personal, DateTime? FechaGP);
        PlantillaEmailMandrillDTO ReemplazarEtiquetasProcesoSeleccion(int IdPlantilla, int IdPostulanteProcesoSeleccion, Personal personal, DateTime? FechaGP);
        PlantillaWhatsAppPostulante ReemplazarEtiquetasProcesoSeleccionWhatsApp(int IdPlantilla, int IdPostulanteProcesoSeleccion, Personal personal, DateTime? FechaGP);

    }
}
