using BSI.Integra.Aplicacion.Base;
using System;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>DTO de configuración de cuenta de empleador en Glassdoor.</summary>
    public class GlassdoorConfiguracionDTO : BaseIntegraEntity
    {
        public string NombreEmpresa { get; set; }
        public string IdentificadorCuenta { get; set; }
        public decimal Valoracion { get; set; }
        public int ResenaTotal { get; set; }
        public string UrlPerfil { get; set; }
        public DateTime? FechaSincronizacion { get; set; }
    }
}
