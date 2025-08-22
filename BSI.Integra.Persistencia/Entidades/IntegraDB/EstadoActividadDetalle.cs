using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EstadoActividadDetalle : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        ///<value>2</value>
        public const int Ejecutado = 2;
        ///<value>1</value>
        public const int NoEjecutado = 1;
    }
}
