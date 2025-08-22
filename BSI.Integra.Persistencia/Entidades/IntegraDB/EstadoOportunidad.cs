using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EstadoOportunidad : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        ///<value>1</value>
        public const int Ejecutada = 1;
        ///<value>2</value>
        public const int NoProgramada = 2;
        ///<value>3</value>
        public const int Reasignada = 3;
        ///<value>6</value>
        public const int Programada = 6;
        ///<value>7</value>
        public const int ReasignadaVentaCruzada = 7;
        ///<value>8</value>
        public const int AsignadoSegundoPrograma = 8;
    }
}