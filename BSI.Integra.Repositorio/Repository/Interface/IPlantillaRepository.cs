using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaRepository : IGenericRepository<TPlantilla>
    {
        #region Metodos Base
        TPlantilla Add(Plantilla entidad);
        TPlantilla Update(Plantilla entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPlantilla> Add(IEnumerable<Plantilla> listadoEntidad);
        IEnumerable<TPlantilla> Update(IEnumerable<Plantilla> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<Plantilla> ObtenerPlantilla();
        Plantilla? ObtenerPorId(int idPlantilla);
        Task<Plantilla> ObtenerPorIdAsync(int idPlantilla);
        PlantillaAsuntoCuerpoDTO ObtenerPlantillaCorreo(int idPlantilla);
        Task<PlantillaAsuntoCuerpoDTO> ObtenerPlantillaCorreoAsync(int idPlantilla);
        IEnumerable<ComboFiltroDTO> ObtenerPlantillaChatIntegraSoporte(int idPlantilla);
        PlantillaDTO? ObtenerPorNombre(string nombreB, string nombreP);
        DatosPlantillaDTO ObtenerPlantillaPorId(int IdPlantilla);
        List<PlantillaTipoEnvioDTO> ObtenerListaPlantillasConfiguracionEnvio();
        public Plantilla ObtenerPlantillaBienvenidaPresencial();
        IEnumerable<PlantillaTipoEnvioDTO> ObtenerPlantillaNombreCorreoOperaciones();
        Task<IEnumerable<PlantillaTipoEnvioDTO>> ObtenerPlantillaNombreCorreoOperacionesAsync();
        IEnumerable<PlantillaTipoEnvioDTO> ObtenerPlantillaNombreWhatsAppOperaciones();
        Task<IEnumerable<PlantillaTipoEnvioDTO>> ObtenerPlantillaNombreWhatsAppOperacionesAsync();
        public List<PlantillaDatoDTO> ObtenerListarPlantilla();
        public List<PlantillaAsociacionModuloSistemaDTO> ObtenerPorPlantlla(List<int> listaIdPlantilla);
        public PlantillaValorDetalleDTO ObtenerPlantillaClaveValor(int idPlantilla);
        public List<ComboDTO> ObtenerPlantillasSpeech();
        public List<ComboDTO> ObtenerAllPlantillaSpeechDespedida();
        Task<IEnumerable<PlantilaCertificadoConstanciaDTO>> ObtenerPlantillaCertificadoAsync();
        List<PlantilaCertificadoConstanciaDTO> ObtenerListaPlantillaCertificadoOperaciones();
        FiltroPlantillaTipadaDTO ObtenerFiltros();
        public List<InsertarDetallePlantillaDTO> InsertarDetallePlantilla(InsertarDetallePlantillaDTO plantillaDetalle);

        IEnumerable<ComboPlantillaNombrePlantillaBaseDTO> ObtenerNombrePlantillaBaseEmail();

        IEnumerable<ComboPlantillaNombrePlantillaBaseDTO> ObtenerNombrePlantillaBaseWatsApp();
    }
}
