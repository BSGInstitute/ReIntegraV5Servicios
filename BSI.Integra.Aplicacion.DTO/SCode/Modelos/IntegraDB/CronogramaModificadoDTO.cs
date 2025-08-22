namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CronogramaModificadoDTO
    {
        public SolicitudCambioCronogramaDTO Objeto { get; set; }
        public List<ListaCambiosDTO> ListaCambiosOrden { get; set; }
        public List<CronogramaFinalModificadoDTO> ListaCronograma { get; set; }
        public string Usuario { get; set; }

    }

    public class SolicitudCambioCronogramaDTO
    {
        public string CodigoMatricula { get; set; }
        public int AprobadoPorId { get; set; }
        public int SolicitadoPorId { get; set; }
        public string Comentario { get; set; }
    }
    public class ListaCambiosDTO
    {
        public int id { get; set; }
        public string TipoCambio { get; set; }
        public int Orden { get; set; }
        public int Cuota { get; set; }
        public int SubCuota { get; set; }
    }
    public class ListaCambiosPorPeriodoDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public float MontoCambio { get; set; }
        public string TipoModificacion { get; set; }
        public DateTime Periodo { get; set; }
    }
    public class ListaCambiosCSVPorPeriodoDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public float MontoCambio { get; set; }
        public string TipoModificacion { get; set; }
        public DateTime Periodo { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class CronogramaFinalModificadoDTO
    {
        public int Id { get; set; }
        public bool Cancelado { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public string TipoCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal Cuota { get; set; }
        public decimal Mora { get; set; }
        public decimal Saldo { get; set; }
        public string Moneda { get; set; }
        public bool Enviado { get; set; }
    }
}
