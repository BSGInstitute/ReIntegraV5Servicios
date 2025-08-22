using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfiguracionEnvioMailing : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int IdPlantilla { get; set; }
        public bool Activo { get; set; }
    }
}
