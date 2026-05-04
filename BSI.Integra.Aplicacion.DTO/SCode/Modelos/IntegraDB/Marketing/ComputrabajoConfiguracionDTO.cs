using BSI.Integra.Aplicacion.Base;
using System;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>DTO de configuración de cuenta de empleador en Computrabajo.</summary>
    public class ComputrabajoConfiguracionDTO : BaseIntegraEntity
    {
        public string NombreEmpresa { get; set; }
        public decimal Valoracion { get; set; }
        public int ResenaTotal { get; set; }
        public string UrlPerfil { get; set; }
        public DateTime? FechaSincronizacion { get; set; }
    }
}
