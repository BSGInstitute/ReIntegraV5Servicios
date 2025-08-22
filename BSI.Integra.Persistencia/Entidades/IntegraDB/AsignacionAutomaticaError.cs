using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AsignacionAutomaticaError : BaseIntegraEntity
    {
        public string Campo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    
        public int? IdContacto { get; set; }
        public int? IdAsignacionAutomatica { get; set; }
        public int? IdAsignacionAutomaticaTipoError { get; set; }

    }
}
