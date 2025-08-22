namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OrigenDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdTipodato { get; set; }
        public int Prioridad { get; set; }
        public int IdCategoriaOrigen { get; set; }
     
    }
    public class OrigenIdCategoriaOrigenDTO
    {
        public int Id { get; set; }
        public int IdCategoriaOrigen { get; set; }
    }
   
}
