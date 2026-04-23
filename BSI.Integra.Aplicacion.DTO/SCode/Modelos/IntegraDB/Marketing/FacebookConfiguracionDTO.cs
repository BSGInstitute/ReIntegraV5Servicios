using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>DTO de configuración de página de Facebook.</summary>
    public class FacebookConfiguracionDTO : BaseIntegraEntity
    {
        public string IdentificadorPagina { get; set; }
        public string Nombre { get; set; }
        public string TokenAccesoPagina { get; set; }
        public int ResenaTotal { get; set; }
        public decimal Valoracion { get; set; }
        public bool Mostrar { get; set; }
    }
}
