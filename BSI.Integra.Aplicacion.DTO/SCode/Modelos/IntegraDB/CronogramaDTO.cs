namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CronogramaNotaDTO
    {
        public string Curso { get; set; }
        public int Nota { get; set; }
        public string Estado { get; set; }
    }
    public class CronogramaAsistenciaDTO
    {
        public string Curso { get; set; }
        public string PorcentajeAsistencia { get; set; }
    }


    public class PagoCuotaCronogramaDTO
    {
        public string CodigoMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal MontoBase { get; set; }
        public decimal Mora { get; set; }
        public decimal MontoPago { get; set; }
        public string MonedaBase { get; set; }
        public string MonedaPago { get; set; }
        public decimal TipoCambio { get; set; }
        public int FormaPago { get; set; }
        public int Documento { get; set; }
        public string NroDocumento { get; set; }
        public Nullable<int> NroCuenta { get; set; }
        public string NroCheque { get; set; }
        public DateTime Fecha { get; set; }
        public string NroDeposito { get; set; }
        public string usuario { get; set; }

    }

    public class CambioMonedaCronogramaModificadoDTO
    {
        public List<CambioMonedaCronogramaFinalModificadoDTO> ListaCronograma { get; set; }
        public string CodigoMatricula { get; set; }
        public string UsuarioNombre { get; set; }
        public int IdPersonal { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public  int? IdMoneda { get; set;}
       
    }

    public class CambioMonedaCronogramaFinalModificadoDTO
    {
        public string Id { get; set; }
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
        public decimal TipoCambio { get; set; }
        public bool Enviado { get; set; }
        public string? MonedaPago { get; set; }
        public decimal? MontoPagado { get; set; }
    }
}
