using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class CursoPespecifico : BaseIntegraEntity
    {
        public int IdPespecifico { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public int Orden { get; set; }
        public int IdExpositor { get; set; }
        public int NroSesiones { get; set; }
    }
}
