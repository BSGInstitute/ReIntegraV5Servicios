using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FormularioSolicitudTextoBoton : BaseIntegraEntity
    {


        public string TextoBoton { get; set; } = null!;

        public string? Descripcion { get; set; }

        public bool PorDefecto { get; set; }

        public Guid? IdMigracion { get; set; }

    }

}
