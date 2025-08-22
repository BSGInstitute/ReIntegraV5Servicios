namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TarifarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool VisiblePortalWeb { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
    public class TarifarioDetalleAgendaDTO
    {
        public int Id { get; set; }
        public int IdTarifario { get; set; }
        public string Concepto { get; set; }
        public string Descripcion { get; set; }
        public string MontoPeru { get; set; }
        public string MontoColombia { get; set; }
        public string MontoBolivia { get; set; }
        public string MontoMexico { get; set; }
        public string MontoExtranjero { get; set; }
    }


    public class VersionprogramaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class TarifarioDetalleConfiguracionDTO
    {
        public int Id { get; set; }
        public int IdTarifario { get; set; }
        public string Concepto { get; set; }
        public string Descripcion { get; set; }
        public string NombrePais { get; set; }
        public int IdPais { get; set; }
        public string NombrePlural { get; set; }
        public string Simbolo { get; set; }
        public int IdMoneda { get; set; }
        public decimal Monto { get; set; }
        public string TipoCantidad { get; set; }
        public string Estados { get; set; }
        public string SubEstados { get; set; }
        public string Usuario { get; set; }
    }
    public class ConfiguracionTarifarioDetalleDTO
    {
        public int IdTarifario { get; set; }
        public string Concepto { get; set; }
        public string Descripcion { get; set; }
        public string TipoCantidad { get; set; }
        public string Estados { get; set; }
        public string SubEstados { get; set; }
        public string Usuario { get; set; }
        public List<ConfiguracionTarifarioDetallePorPaisDTO> DetallePais { get; set; }
        public List<ConfiguracionTarifarioDetallePorMonedaDTO> DetalleMoneda { get; set; }
        public List<ConfiguracionTarifarioDetallePorMontoDTO> DetalleMonto { get; set; }
    }
    public class ConfiguracionTarifarioDetallePorPaisDTO
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public int IdMoneda { get; set; }
        public string NombrePlural { get; set; }
        public string Simbolo { get; set; }
        public decimal Monto { get; set; }
    }
    public class ConfiguracionTarifarioDetallePorMonedaDTO
    {
        public int IdMoneda { get; set; }
        public string NombrePlural { get; set; }
        public string Simbolo { get; set; }
    }
    public class ConfiguracionTarifarioDetallePorMontoDTO
    {
        public decimal Monto { get; set; }
    }
    public class TarifarioNuevoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool VisiblePortalWeb { get; set; }
        public string Usuario { get; set; }
        public List<TarifarioDetalleDTO> Detalles { get; set; }
    }
    public class TarifarioDetalleDTO
    {
        public int Id { get; set; }
        public int IdTarifario { get; set; }
        public string Concepto { get; set; }
        public string Descripcion { get; set; }
        public List<PaisesTarifarioDetalleDTO> ListaIdPaises { get; set; }
        public List<PaisesTarifarioDetalleV2DTO>? ListaConfiguracion { get; set; }
        public string TipoCantidad { get; set; }
        public string Estados { get; set; }
        public string SubEstados { get; set; }
        public string? Usuario { get; set; }
        public string? Estado { get; set; }
    }
    public class PaisesTarifarioDetalleDTO
    {
        public int Id { get; set; }
        public int? IdTarifario { get; set; }
        public int IdPais { get; set; }
        public int IdMoneda { get; set; }
        public decimal Monto { get; set; }
    }
    public class PaisesTarifarioDetalleV2DTO
    {
        public string Concepto { get; set; }
        public string Descripcion { get; set; }
        public string Estados { get; set; }
        public string SubEstados { get; set; }
        public int? IdTarifario { get; set; }
        public int IdPais { get; set; }
        public int IdMoneda { get; set; }
        public decimal Monto { get; set; }
        public string TipoCantidad { get; set; }
    }


    public class TarifarioDetalleMontoDTO
    {
        public int Id { get; set; }
        public int IdTarifario { get; set; }
        public string Detalle { get; set; }

    }
}
