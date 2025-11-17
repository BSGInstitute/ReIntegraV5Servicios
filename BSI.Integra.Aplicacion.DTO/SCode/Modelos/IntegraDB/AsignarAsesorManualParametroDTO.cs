namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AsignarAsesorManualParametroDTO
    {
        public AsignarAsesorManualDTO AsignarAsesor { get; set; }
        // public OportunidadLog? OportunidadLogNueva { get; set; }
        public string Usuario { get; set; }
    }

    public class CerrarOportunidadODDTO
    {
        public List<int> IdOportunidades { get; set; }


        public string Usuario { get; set; }
    }


    public class AsignarAsesorManualParametroWhtasapDTO
    {
        public AsignarAsesorManuaWhatsapplDTO AsignarAsesor { get; set; }
        public string Usuario { get; set; }
    }
    public class AsignarAsesorManuaWhatsapplDTO
    {
        public int?[] IdOportunidades { get; set; }
        public int? IdAsesor { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public int? IdCentroCosto { get; set; }
        public string? Comentario { get; set; }
        public bool? SegunMejorPro { get; set; }
        public bool? envioWhats { get; set; }
    }
    public class AsignarActividadesAgendaV6DTO
    {
        public int IdAsesor { get; set; }
        public string? Agenda { get; set; }
    }
    public class AsesorUsoV6DTO
    {
        public int Id { get; set; }
        public int IdAsesor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class RespuestaCambioActividadCabeceraAgendaDTO
    {
        public int? IdAsesor { get; set; }
        public string? Estado { get; set; }
        public string? Mensaje { get; set; }
        public List<object>? Result { get; set; }
    }


}


