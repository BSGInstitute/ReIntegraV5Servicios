namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionProyeccionFurDTO
    {
        public int Id { get; set; }

        public int IdPeriodoProyeccion { get; set; }
 
        public DateTime FechaSemilla { get; set; }
        public DateTime FechaLimiteEnvio { get; set; }

        public bool Activo { get; set; }
        
    }

    public class GuardarConfiguracionProyeccionFurDTO
    {
        public List<ConfiguracionProyeccionFurDTO> ListaConfiguracion { get; set; }
        public List<int> IdEliminar { get; set; }
    }



}
