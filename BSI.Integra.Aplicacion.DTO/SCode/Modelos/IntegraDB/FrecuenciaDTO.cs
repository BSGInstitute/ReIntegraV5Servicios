namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FrecuenciaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int NumDias { get; set; }
    }

    public class DatosFrecuenciaGeneralDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? NumDias { get; set; }
    }


}
