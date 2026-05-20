namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlanSuscripcionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsPremium { get; set; }
        public bool PowerUpsIlimitados { get; set; }
        public bool IncluyeAnuncio { get; set; }
        public bool ContenidoExclusivo { get; set; }
        public int Orden { get; set; }
    }

    public class PlanSuscripcionInsertarDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsPremium { get; set; }
        public bool PowerUpsIlimitados { get; set; }
        public bool IncluyeAnuncio { get; set; }
        public bool ContenidoExclusivo { get; set; }
        public int Orden { get; set; }
    }

    public class PlanSuscripcionActualizarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsPremium { get; set; }
        public bool PowerUpsIlimitados { get; set; }
        public bool IncluyeAnuncio { get; set; }
        public bool ContenidoExclusivo { get; set; }
        public int Orden { get; set; }
    }
}
