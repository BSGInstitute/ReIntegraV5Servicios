using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class EstadoPespecifico : BaseIntegraEntity
    {
        [StringLength(20)]
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
        ///<value>0</value>
        public const int CANCELADO = 0;
        ///<value>1</value>
        public const int CONCLUIDO = 1;
        ///<value>2</value>
        public const int EJECUCION = 2;
        ///<value>3</value>
        public const int LANZAMIENTO = 3;
        ///<value>4</value>
        public const int PLANIFICACION = 4;
        ///<value>5</value>
        public const int POR_EJECUCION = 5;
    }
}
