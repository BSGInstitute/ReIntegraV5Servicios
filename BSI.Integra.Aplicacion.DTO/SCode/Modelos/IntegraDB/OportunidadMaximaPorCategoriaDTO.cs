namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OportunidadMaximaPorCategoriaDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int IdPais { get; set; }
        public int OportunidadesMaximas { get; set; }
        public int OportunidadesSinGenerarIs { get; set; }
        public int Meta { get; set; }
        public string Grupo { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class OportunidadMaximaPorCategoriaComboDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string Grupo { get; set; } = null!;
    }
    public class SeguimientoAsesorDTO
    {
        public int OportunidadesMaximas { get; set; }
        public string Grupo { get; set; } = null!;
        public int TasaConversion { get; set; }
        public int OportunidadesCerradas { get; set; }
        public int OportunidadesRestantes { get; set; }
    }
}
