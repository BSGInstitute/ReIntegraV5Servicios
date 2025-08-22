using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConvocatoriaPersonalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreProcesoSeleccion { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string CuerpoConvocatoria { get; set; }
        public string UrlAviso { get; set; }
        public int? IdSedeTrabajo { get; set; }
        public string SedeTrabajo { get; set; }
        public int? IdPersonal { get; set; }
        public int IdProveedor { get; set; }
        public int? IdArea { get; set; }
        public string Area { get; set; }
        public bool? Activo { get; set; }
        public string Proveedor { get; set; }
        public string PersonalEncargado { get; set; }
        public int? IdEstadoConvocatoria { get; set; }
        public int? NroVacantes { get; set; }
        public int? IdModalidadTrabajo { get; set; }
        public int? IdCategoriaAsignacion { get; set; }
        public bool? VerEnPortal { get; set; }
        public bool? SoloMatriculado { get; set; }
        public List<int>? IdExperiencia { get; set; }
        public List<int>? IdNivelEstudio { get; set; }
        public List<int>? IdIdioma { get; set; }
        public string? InformacionAdicional { get; set; }
        public int? IdTipoContrato { get; set; }
        public string? TipoJornada { get; set; }
        public int? HoraSemanal { get; set; }
        public int? RemIdMoneda { get; set; }
        public decimal? MontoRemBruta { get; set; }
        public bool? VisualizarRem { get; set; }
        public bool? AplicaBono { get; set; }
        public int? BonoIdMoneda { get; set; }
        public decimal? MontoDesdeBono { get; set; }
        public decimal? MontoHastaBono { get; set; }
        public bool? AplicaComision { get; set; }
        public int? ComisionIdMoneda { get; set; }

        public decimal? MontoDesdeComision { get; set; }
        public decimal? MontoHastaComision { get; set; }

    }
    public class ConvocatoriaPersonalComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public decimal Valor { get; set; }
    }

    public class ConvocatoriaPersonalRecibidoDTO
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Nombre { get; set; } = null!;
        [StringLength(50)]
        public string Codigo { get; set; } = null!;
        public int IdProcesoSeleccion { get; set; }
        public int IdProveedor { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string CuerpoConvocatoria { get; set; } = null!;
        public int? IdSedeTrabajo { get; set; }
        public int? IdArea { get; set; }
        [StringLength(350)]
        public string? UrlAviso { get; set; }
        public int? IdPersonal { get; set; }
        public int? NroVacantes { get; set; }
        public int? IdModalidadTrabajo { get; set; }
        public int? IdCategoriaAsignacion { get; set; }
        public bool? VerEnPortal { get; set; }
        public bool? SoloMatriculado { get; set; }
        public List<int>? IdExperiencia { get; set; }
        public List<int>? IdNivelEstudio { get; set; }
        public List<int>? IdIdioma { get; set; }
        public List<IdiomaNivelInsertDTO>? IdIdiomaInsert { get; set; }
        public string? InformacionAdicional { get; set; }
        public int? IdTipoContrato { get; set; }
        public string? TipoJornada { get; set; }
        public int? HoraSemanal { get; set; }
        public int? RemIdMoneda { get; set; }
        public decimal? MontoRemBruta { get; set; }
        public bool? VisualizarRem { get; set; }
        public bool? AplicaBono { get; set; }
        public int? BonoIdMoneda { get; set; }
        public decimal? MontoDesdeBono { get; set; }
        public decimal? MontoHastaBono { get; set; }
        public bool? AplicaComision { get; set; }
        public int? ComisionIdMoneda { get; set; }

        public decimal? MontoDesdeComision { get; set; }
        public decimal? MontoHastaComision { get; set; }

        public int? IdEstadoConvocatoria { get; set; }
    }

    public class IdiomaNivelComboDTO
    {
        public int Id { get; set; }
        public int IdIdioma { get; set; }
        public int IdNivelIdioma { get; set; }
        public string NombreCompleto { get; set; }
    }

    public class IdiomaNivelInsertDTO
    {
        public int IdIdioma { get; set; }
        public int IdNivelIdioma { get; set; }
    }

    public class DetalleConvocatoriaDTO
    {
        public List<int>? IdIdioma { get; set; }
        public List<int>? IdNivelEstudio { get; set; }
        public List<int>? IdExperiencia { get; set; }
    }

    public class ConvocatoriaPersonalComboPostulanteDTO
    {
        public int IdConvocatoria { get; set; }
        public string NombreConvocatoria { get; set; } = null!;
        public int IdProcesoSeleccion { get; set; }
    }
}
