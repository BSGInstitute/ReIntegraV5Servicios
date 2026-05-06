using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>DTO de configuración de página de LinkedIn.</summary>
    public class LinkedinConfiguracionDTO : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public string EnlacePagina { get; set; }
        public int ResenaTotal { get; set; }
    }
}
