namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PanelDepositoDisponibleDTO
    {
        public int Id { get; set; }
        public int? IdFormaPago { get; set; }
        public string FormaPago { get; set; }
        public int DiasDeposito { get; set; }
        public int DiasDisponible { get; set; }
        public bool CuentaFeriados { get; set; }
        public bool CuentaFeriadosEstatales { get; set; }
        public bool ConsideraVSD { get; set; }
        public bool ConsideraDiasHabilesLunesViernes { get; set; }
        public bool ConsideraDiasHabilesLunesSabado { get; set; }
        public bool ConsideraDiasFijoSemana { get; set; }
        public int? IdDiaSemanaFijo { get; set; }
        public int HoraCorte { get; set; }
        public int MinutoCorte { get; set; }
        public decimal PorcentajeCobro { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    public class PagoActualizadoFechaDepositoDTO
    {
        public int IdCuota { get; set; }
        public DateTime? FechaDeposito { get; set; }
        public string Usuario { get; set; }
    }
}
