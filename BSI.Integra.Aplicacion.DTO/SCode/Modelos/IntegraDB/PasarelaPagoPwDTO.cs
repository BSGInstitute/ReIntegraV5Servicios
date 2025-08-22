namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PasarelaPagoPwEnvioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdProveedor { get; set; }
        public int? IdPais { get; set; }
        public int? Prioridad { get; set; }
        public string Usuario { get; set; } = null!;
    }
    public class RegistroPasarelaPagoPWDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public int Prioridad { get; set; }
    }

    public class RegistroMedioPagoMatriculaCronogramaDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdMedioPago { get; set; }
        public bool Activo { get; set; }
        public string Usuario { get; set; }
    }
    public class PasarelaPagoPwComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class PasarelaPagoPwAgendaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdProveedor { get; set; }
        public int? IdPais { get; set; }
        public int? Prioridad { get; set; }
    }

}
