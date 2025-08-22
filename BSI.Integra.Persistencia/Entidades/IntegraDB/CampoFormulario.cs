using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CampoFormulario : BaseIntegraEntity
    {

        public int? IdFormularioSolicitud { get; set; }

        public int IdCampoContacto { get; set; }

        public int? NroVisitas { get; set; }

        public string? Codigo { get; set; }

        public string? Campo { get; set; }

        public bool? Siempre { get; set; }

        public bool? Inteligente { get; set; }

        public bool? Probabilidad { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
