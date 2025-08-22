using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SeccionPreguntaFrecuente : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        
    }
}
