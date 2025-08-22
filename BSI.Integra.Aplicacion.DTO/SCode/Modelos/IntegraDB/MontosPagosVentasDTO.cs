namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MontosPagosVentasDTO
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public string PrecioLetras { get; set; }
        public int IdMoneda { get; set; }
        public decimal Matricula { get; set; }
        public decimal Cuotas { get; set; }
        public int NroCuotas { get; set; }
        public int IdPrograma { get; set; }
        public int IdTipoPago { get; set; }
        public int IdPais { get; set; }
        public string Vencimiento { get; set; }
        public string PrimeraCuota { get; set; }
        public bool CuotaDoble { get; set; }
        public int IdTipoDescuento { get; set; }
        public int Formula { get; set; }
        public int PorcentajeGeneral { get; set; }
        public int PorcentajeMatricula { get; set; }
        public int FraccionesMatricula { get; set; }
        public int PorcentajeCuotas { get; set; }
        public int CuotasAdicionales { get; set; }
        public string NombrePlural { get; set; }
        public int CuotasTipoPago { get; set; }
        public int? Paquete { get; set; }
        public string Nombre { get; set; }
        public bool? VisibleWeb { get; set; }
        public decimal MontoDescontado { get; set; }
    }
}
