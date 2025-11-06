namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial
{
    public class OportunidadWhatsappDTO
    {
        public int IdPersonalAsignado { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdAlumno { get; set; }
        public int? IdOrigen { get; set; }
        public bool? Activo { get; set; }
    }
    public class OportunidadFichaDTO
    {
        public int IdPersonalAsignado { get; set; }
        //public int IdCentroCosto { get; set; }
        public int IdPgeneral { get; set; }
        public int IdOportunidadRN2 { get; set; }
        //public int IdAlumno { get; set; }
        //public int IdClasificacionPersona { get; set; }
        public int IdFaseOportunidad { get; set; }
        public string Usuario { get; set; }
    }
}
