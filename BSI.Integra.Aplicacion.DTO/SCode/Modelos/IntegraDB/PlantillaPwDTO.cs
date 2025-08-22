using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlantillaPwDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int IdPlantillaMaestroPw { get; set; }
        public int IdRevisionPw { get; set; }
    }
    public class PlantillaPwServiceDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPlantillaMaestroPw { get; set; }
        public int IdRevisionPw { get; set; }
        public int IdPlantillaBase { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPlantilla { get; set; }
        public Guid? IdMigracion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public PlantillaEmailMandrillDTO EmailReemplazado { get; set; }
        public List<SeccionEtiquetaDTO> DatosEtiquetas { get; set; }
        public List<CursosRelacionadosDTO> ListaCursosRelacionados { get; set; }
        public List<ProblemaCausaDTO> ListaProblemasCausa { get; set; }
        public List<PGeneralCursoRelacionadoDTO> UrlCursosRelacionados { get; set; }
        public List<ClaveValorDTO> ListaTemplateV2ReemplazoEtiqueta { get; set; }
        public string etiquetaMontosPagoPaquetes { get; set; }
        public string cronogramaPagos { get; set; }
        public string FechaInicioPrograma { get; set; }
        public OportunidadAlumnoDTO DatosOportunidadAlumno { get; set; }
    }
    public class PlantillaPwComboWhatsappDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
        public int IdPlantillaBase { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
    }
    public class EtiquetaMontoPagoArgumentosDTO
    {
        public int? IdPGeneral { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdCodigoPais { get; set; }
    }
    public class PagoCuotaDTO
    {
        public int numeroCuota { get; set; }
        public string cuotaDescripcion { get; set; }
        public double? montoCuota { get; set; }
        public float montoCuotaDescuento { get; set; }
        public bool ispagado { get; set; }
        public bool es_matricula { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fechapago { get; set; }
        public string SimboloMoneda { get; set; }
    }
    public class EtiquetaMontoPagoRespuestaDTO
    {
        public List<string>? DatosEtiquetas { get; set; }
        public List<ProblemaCausaDTO>? ProblemasCausa { get; set; }
        public List<PGeneralCursoRelacionadoDTO>? CursosRelacionados { get; set; }
        public string? EtiquetaMontosPagoPaquetes { get; set; }
        public string? CronogramaPagos { get; set; }
        public List<ClaveValorDTO>? ListaTemplateV2ReemplazoEtiqueta { get; set; }
        public OportunidadAlumnoDTO? DatosOportunidadAlumno { get; set; }
    }
    public class PlantillaPwComboModuloDTO
    {
        public List<PlantillaMaestroPwDTO> PlantillaMaestroPw { get; set; }
        public List<RevisionPwDTO> RevisionPw { get; set; }
        public List<PlantillaPwDTO> PlantillaPw { get; set; }
        public List<ComboDTO> Pais { get; set; }
        public List<ComboDTO> SeccionTipoContenidoPw { get; set; }
    }
    public class PlantillaPwParametrosDTO
    {
        public PlantillaPwDTO PlantillaPw { get; set; }
        public List<int> Paises { get; set; }
        public List<SeccionPwDTO> SeccionPw { get; set; }
    }
}
