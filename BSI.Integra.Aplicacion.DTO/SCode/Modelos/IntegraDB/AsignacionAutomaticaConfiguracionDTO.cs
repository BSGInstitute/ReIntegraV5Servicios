namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AsignacionAutomaticaConfiguracionDTO
    {
        public int IdFaseOportunidad { get; set; }

        public int? IdTipoDato { get; set; }

        public int? IdOrigen { get; set; }

        public bool Inclusivo { get; set; }

        public bool Habilitado { get; set; }
        
    }


}
