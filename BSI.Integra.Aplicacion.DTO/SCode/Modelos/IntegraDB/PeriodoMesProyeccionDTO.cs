namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PeriodoMesProyeccionDTO
    {
        public int Id { get; set; }
        public string Periodo { get; set; } = null!;
 
        public int Cantidad { get; set; }
        public bool  edit { get; set; }
        public bool delete { get; set; }
    }
    public class PeriodoMesProyeccionCombo
    {
        public int Id { get; set; }
        public string Periodo { get; set; } = null!;

    }


}
