using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionEnvioMailingDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int IdPlantilla { get; set; }

    }
    public class ConfiguracionEnvioMailingMasivoDTO
    {
        public List<ConfiguracionEnvioMailing> ListaConfiguracionEnvioMailing { get; set; }
    }



}
