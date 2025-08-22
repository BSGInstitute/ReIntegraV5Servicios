using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FacebookFormularioLeadgen : BaseIntegraEntity
    {

        public string IdLeadgenFacebook { get; set; } = null!;

        public DateTime? FechaCreacionFacebook { get; set; }

        public string? IdCampanhaFacebook { get; set; }

        public string? NombreCampaniaFacebook { get; set; }

        public string? Email { get; set; }


        public string? NombreCompleto { get; set; }


        public string? AreaFormacion { get; set; }

        public string? Cargo { get; set; }

        public string? AreaTrabajo { get; set; }

        public string? Ciudad { get; set; }

        public string? Telefono { get; set; }

        public bool? EsProcesado { get; set; }

        public string? Industria { get; set; }

        public string? InicioCapacitacion { get; set; }

        public string? Excepcion { get; set; }



        public string? FacebookAnuncioId { get; set; }

        public string? FacebookAnuncioNombre { get; set; }
    }
}
