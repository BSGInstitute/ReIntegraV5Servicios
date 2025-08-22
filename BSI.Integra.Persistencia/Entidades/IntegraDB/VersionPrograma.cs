using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class VersionPrograma : BaseIntegraEntity
    {
        [StringLength(30)]
        public string Nombre { get; set; } = null!;
        ///<value>1</value>
        public const int BASICA = 1;
        ///<value>2</value>
        public const int PROFESIONAL = 2;
        ///<value>3</value>
        public const int GERENCIAL = 3;
        ///<value>4</value>
        public const int SINVERSION = 4;
    }
}
