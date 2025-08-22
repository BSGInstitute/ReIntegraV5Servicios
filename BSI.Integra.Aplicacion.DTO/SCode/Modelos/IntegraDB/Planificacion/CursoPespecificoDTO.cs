namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class CursoPespecificoDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public int Orden { get; set; }
        public int IdExpositor { get; set; }
        public int NroSesiones { get; set; }
    }
}