using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EstadoOcurrencia : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        ///<value>1</value>
        public const int Ejecutado = 1;
        ///<value>2</value>
        public const int NoEjecutado = 2;
        ///<value>2</value>
        public const int Reprogramada = 3;
        ///<value>3</value>
        public const int Ejecutado2 = 4;
        ///<value>5</value>
        public const int NoEjecutado2 = 5;
        ///<value>6</value>
        public const int Reprogramada2 = 6;
        ///<value>7</value>
        public const int AsignacionManual = 7;
    }
}
