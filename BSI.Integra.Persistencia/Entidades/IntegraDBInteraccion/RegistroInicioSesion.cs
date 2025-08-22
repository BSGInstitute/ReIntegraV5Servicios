using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDBInteraccion
{
    public class RegistroInicioSesion : BaseIntegraEntity
    {
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
}
