using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfiguracionProyeccionFur : BaseIntegraEntity
    {
        public int Id { get; set; }
      
        public int IdPeriodoProyeccion { get; set; }
      
        public DateTime FechaSemilla { get; set; }
        public DateTime FechaLimiteEnvio { get; set; }

        public bool Activo { get; set; }
       

    }
}
