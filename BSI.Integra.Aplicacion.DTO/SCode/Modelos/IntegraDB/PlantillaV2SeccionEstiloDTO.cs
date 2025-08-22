namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlantillaV2SeccionEstiloDTO
    {
        public int Id { get; set; }
        public int IdPlanitillav2Seccion { get; set; }
        public int IdEstilo { get; set; }
        public string Valor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
    }



    public class PlantillaV2SeccionEstiloEnvio
    {
        public int Id { get; set; }
        public int IdPlanitillav2Seccion { get; set; }
        public int IdEstilo { get; set; }
        public string Valor { get; set; }
        public string? Usuario { get; set; } = null!;
    }

    public class PlantillaV2SeccionEstiloCombo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

    }

}
