using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PGeneralExpositor : BaseIntegraEntity
    { 
        public int? IdPgeneral { get; set; } 
        public int IdExpositor { get; set; } 
        public int Posicion { get; set; } 

        public int? IdModalidadCurso { get; set; }
        public virtual TPgeneral? IdPgeneralNavigation { get; set; }
    }

}
