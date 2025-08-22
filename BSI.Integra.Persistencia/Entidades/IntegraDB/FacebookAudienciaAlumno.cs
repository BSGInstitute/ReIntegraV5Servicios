using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FacebookAudienciaAlumno : BaseIntegraEntity
    {
        public int IdFacebookAudiencia { get; set; }
        public int IdAlumno { get; set; }
        public Guid? IdMigracion { get; set; }

    }


}
