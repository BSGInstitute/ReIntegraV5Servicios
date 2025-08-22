namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AsignacionOportunidadDTO
    {
        public int Id { get; set; }
        public int? IdAsignacionOportunidad { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdPersonalAnterior { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdCentroCostoAnt { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdAlumno { get; set; }
        public DateTime FechaLog { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
    }
}

