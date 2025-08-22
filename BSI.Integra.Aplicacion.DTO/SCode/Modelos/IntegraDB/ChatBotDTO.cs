namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
  
    public class InteraccionesPorPasoDTO
    {
        public int Paso { get; set; }
        public int Interaccion { get; set; }
    }
    public class InteraccionesPorPasoProgramasDTO
    {
        public string CodigoPGeneral { get; set; }
        public List<InteraccionesPorPasoDTO> ListaInteracionesPorPaso { get; set; }
    }
   
      public class FiltroBotDTO
    {
        public bool EstaRegistrado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}


