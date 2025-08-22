namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion
{
    public class RegistroInicioSesionDTO
    {
        public int? Id { get; set; }
        public int IdPersonal { get; set; }
        public string Usuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public int FechaInteraccionEntera { get; set; }
        public int HoraInteraccionEntera { get; set; }
        public string IpPublica { get; set; } = null!;
        public string? IpLocal { get; set; }
        public string? DireccionMac { get; set; }
    }
    public class RegistroInicioSesionLogueoDTO
    {
        public int IdPersonal { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string IpPublica { get; set; }
        public string? IpLocal { get; set; }
        public string? DireccionMac { get; set; }
    }
}
