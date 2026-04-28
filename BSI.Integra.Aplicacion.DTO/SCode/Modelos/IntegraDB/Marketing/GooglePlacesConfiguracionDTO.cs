using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>DTO base de la entidad GooglePlacesConfiguracion (sede de Google Places).</summary>
    public class GooglePlacesConfiguracionDTO : BaseIntegraEntity
    {
        public string NombreSede { get; set; }
        public string IdentificadorCuenta { get; set; }
        public decimal Valoracion { get; set; }
        public int ResenaTotal { get; set; }
        public bool Mostrar { get; set; }
    }

    /// <summary>Combo de sedes de Google Places para selectores del frontend.</summary>
    public class GooglePlacesConfiguracionComboDTO
    {
        public int Id { get; set; }
        public string NombreSede { get; set; }
        public string IdentificadorCuenta { get; set; }
    }
}
