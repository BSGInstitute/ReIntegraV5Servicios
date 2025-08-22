namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FormularioProgresivoConfiguracionBotonDTO
    {
        public int Id { get; set; }
        public int IdFormularioProgresivo { get; set; }
        public int IdFormularioProgresivoSeccionPortal { get; set; }
        public int IdFormularioProgresivoAccionBoton { get; set; }
        public int? IdRegistroArchivoStorage { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }

    public class FormularioProgresivoConfiguracionBotonEntradaDTO
    {
        public int? Id { get; set; }
        public long IdentificadorFilaGrilla { get; set; }
        public int IdFormularioProgresivo { get; set; }
        public int IdFormularioProgresivoSeccionPortal { get; set; }
        public int IdFormularioProgresivoAccionBoton { get; set; }
        public int? IdRegistroArchivoStorage { get; set; }
        public string TextoBoton { get; set; }
        public string Usuario { get; set; }
    }

}
