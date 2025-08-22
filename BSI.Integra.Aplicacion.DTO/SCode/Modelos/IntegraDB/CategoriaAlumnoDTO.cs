namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CategoriaAlumnoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EstadoCategoria { get; set; }
        public int Descuento { get; set; }
        public int AmpliacionFechaFinPrograma { get; set; }
        public int CantidadDiasVencimiento { get; set; }
        public String Estados { get; set; }
        public String IdEstados { get; set; }
        public String SubEstados { get; set; }
        public String IdSubEstados { get; set; }
    }
    public class FechaPagoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdEstado_matricula { get; set; }
        public int IdSubEstadoMatricul { get; set; }
        public int? IdCategoriaAlumno { get; set; }
        public String FechaVencimiento { get; set; }
        public String FechaPago { get; set; }
    }
}
