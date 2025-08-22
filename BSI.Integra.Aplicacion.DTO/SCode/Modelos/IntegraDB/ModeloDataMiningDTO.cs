namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ModeloDataMiningDTO
    {
        public int Id { get; set; }
        public decimal? ProbabilidadInicial { get; set; }
        public int? IdProbabilidadRegistroPwInicial { get; set; }
        public decimal? ProbabilidadActual { get; set; }
        public int? IdProbabilidadRegistroPwActual { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdAlumno { get; set; }
        public DateTime? FechaCreacionContacto { get; set; }
        public DateTime? FechaCreacionOportunidad { get; set; }
        public int? DiasEntreCreacionContactoOportunidad { get; set; }
        public int? Nombres { get; set; }
        public int? Apellidos { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdPais { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? Email1 { get; set; }
        public int? TelefonoFijo { get; set; }
        public int? TelefonoMovil { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTamanioEmpresa { get; set; }
        public int? Ciiuempresa { get; set; }
        public int? TelefonoEmpresa { get; set; }
        public int? IdCiudadEmpresa { get; set; }
        public int? IdPaisEmpresa { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdArea { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdCategoriaPrograma { get; set; }
        public string? ProgramaGeneralDuracion { get; set; }
        public int? IdPartner { get; set; }
        public int? IdPespecifico { get; set; }
        public int? Modalidad { get; set; }
        public int? PrecioProgramaEspecifico { get; set; }
        public int? PrecioProgramaEspecificoDolares { get; set; }
        public int? MonedaPrecioProgramaEspecifico { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdOrigen { get; set; }
        public string? FaseMaximaAlcanzada { get; set; }
        public string? FaseActual { get; set; }
        public int? IdActividadFinal { get; set; }
        public int? IdOcurrenciaFinal { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
    }
    public class ProbabilidadModeloDataMiningDTO
    {
        public int IdOportunidad { get; set; }
        public decimal? Probabilidad { get; set; }
        public int? IdProbabilidaRegistroPW { get; set; }
        public string Nombre { get; set; }
        public int? IdCargo { get; set; }
        public int? IdareaFormacion { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdCategoriaDato { get; set; }
    }
    public class ProbabilidadModeloDTO
    {
        public decimal? ProbabilidadInicial { get; set; }
        public decimal? ProbabilidadActual { get; set; }
        public int? IdProbabilidadRegistroPwInicial { get; set; }
        public int? IdProbabilidadRegistroPwActual { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdCategoriaOrigen { get; set; }
    }
}
