using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades
{
    public class CategoriaAlumno : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EstadoCategoria { get; set; }
        public int Descuento { get; set; }
        public int AmpliacionFechaFinPrograma { get; set; }
        public int CantidadDiasVencimiento { get; set; }
    }
}
