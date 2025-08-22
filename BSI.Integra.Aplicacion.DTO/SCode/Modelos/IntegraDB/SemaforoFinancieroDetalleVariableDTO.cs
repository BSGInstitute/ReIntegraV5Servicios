namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SemaforoFinancieroDetalleVariableDTO
    {
        public int? Id { get; set; }
        public int? IdSemaforoFinancieroDetalle { get; set; }
        public int? IdSemaforoFinancieroVariable { get; set; }
        public string Variable { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public int? IdMoneda { get; set; }
        public string Unidad { get; set; }
        public bool? AplicaUnidad { get; set; }
    }
    public class SemaforoFinancieroDetalleVariableInformacionDetalladaDTO
    {
        public int? Id { get; set; }
        public int? IdSemaforoFinancieroDetalle { get; set; }
        public int? IdSemaforoFinancieroVariable { get; set; }
        public string Variable { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public int? IdMoneda { get; set; }
        public string Unidad { get; set; }
        public bool? AplicaUnidad { get; set; }
    }
    public class SemaforoFinancieroDetalleVariableComboDTO
    {
        public int Id { get; set; }
        public string NombreDetalle { get; set; } = null!;
        public string NombreVariable { get; set; } = null!;
    }
}
