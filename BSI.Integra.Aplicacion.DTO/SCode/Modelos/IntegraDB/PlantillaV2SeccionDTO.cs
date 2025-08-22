namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlantillaV2SeccionDTO
    {
        public int Id { get; set; }
        public int IdPlantillaV2 { get; set; }
        public int IdSeccion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
    }



    public class PlantillaV2SeccionEnvio
    {
        public int Id { get; set; }
        public int IdPlantillaV2 { get; set; }
        public int IdSeccion { get; set; }
        public string? Usuario { get; set; } = null!;
    }

    public class PlantillaV2SeccionCombo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

    }

}
