namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OcurrenciaAlternoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreM { get; set; }
        public int? NombreCs { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int? IdActividadCabecera { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int IdEstadoOcurrencia { get; set; }
        public bool Oportunidad { get; set; }
        public string RequiereLlamada { get; set; }
        public string Roles { get; set; } = null!;
        public string Color { get; set; } = null!;
        public int IdPersonalAreaTrabajo { get; set; }
        public int? IdTipoOcurrencia { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
    }
    public class OcurrenciaAlternoComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdFaseOportunidad { get; set; }
    }
}
